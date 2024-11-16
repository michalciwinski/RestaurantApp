using System;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RestaurantApp.Entities;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Files;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;
using RestaurantApp.Model;
using System.Text.Json;
using System.Net.Http.Headers;

namespace RestaurantApp.Services.Implementation
{
    public class DatabaseListenerService : BackgroundService
    {
        private string _connectionString { get; set; }
        private string _openAIClient { get; set; }
        private string _assistantsEndpoint { get; set; }
        private RestaurantDbContext _context;
        public DatabaseListenerService(string connectionString, string openAIClient, string assistantsEndpoint, RestaurantDbContext context) {
            _connectionString = connectionString;
            _openAIClient = openAIClient;
            _assistantsEndpoint = assistantsEndpoint;
            _context = context;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync(stoppingToken);

                using (var cmd = new NpgsqlCommand("LISTEN table_change", conn))
                {
                    await cmd.ExecuteNonQueryAsync(stoppingToken);
                }

                conn.Notification += async (sender, e) =>
                {
                    Console.WriteLine($"Received: {e.Payload}");
                    if (e.Payload == "menu")
                    {
                        await openAiFunctions();
                    }
                };

                while (!stoppingToken.IsCancellationRequested)
                {
                    await conn.WaitAsync(stoppingToken);
                }
            }
        }
        public async Task openAiFunctions()
        {
            //definitions
            const string file1 = "JSON_Asisstant/MenuIngredients.json";
            const string file2 = "JSON_Asisstant/MenuPricesDishType.json";

            
            //Connect to Open AI
            using var api = new OpenAIClient("sk-" + _openAIClient);

            //Connest to the Assistant endpoint 
            var assistant = await api.AssistantsEndpoint.RetrieveAssistantAsync(_assistantsEndpoint);

            //Take a list of exisitng files in assistant
            var filesList = await api.AssistantsEndpoint.ListFilesAsync(_assistantsEndpoint);

            foreach (var file in filesList.Items)
            {
                Console.WriteLine($"Plik ID: {file.Id}");
            }
            //Delete old files from Open AI DB
            var tasks = new List<Task>();
            foreach (var file in filesList.Items)
            {
                tasks.Add(assistant.DeleteFileAsync(file.Id));
            }
            //*await Task.WhenAll(tasks);

            //Take view from DB
            List<ModelMenuWithIngredients> menuIngredients = new List<ModelMenuWithIngredients>();
            List<ModelMenuPricesDishTypes> menuPricesDishType = new List<ModelMenuPricesDishTypes>();
            try
            {
                var dishesList = _context.TvMenuIngredients.ToList();
                var dishesListGroup = dishesList.GroupBy(x => new { x.Id, x.Name }); // 
                foreach (var group in dishesListGroup)
                {
                    var dish = new ModelMenuWithIngredients()
                    {
                        Id = group.Key.Id ?? 0, // ?? 0 to check
                        Name = group.Key.Name,
                        NameOfIngredients = group.Select(x => x.NameOfIngredient).ToList()
                    };
                    menuIngredients.Add(dish);
                }
                var options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true 
                };
                var json = JsonSerializer.Serialize(menuIngredients, options);
                var jsonText = json.TrimStart('[').TrimEnd(']');
                //delete old files
                File.Delete(file1);
                //create new files
                File.WriteAllText(file1, jsonText);
                Console.WriteLine(jsonText);


                var menuList = _context.TvMenuPricesDishTypes.ToList();
                //var menuListGroup = menuList.GroupBy(x => new { x.Name }); // 
                foreach (var menu in menuList)
                {
                    var dish = new ModelMenuPricesDishTypes()
                    {
                        //Id = group.Key.Id,
                        Name = menu.Name,
                        Description = menu.Description,
                        Price = menu.Price,
                        Dishtype = menu.Dishtype
                    };
                    menuPricesDishType.Add(dish);
                }
                options = new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
                json = JsonSerializer.Serialize(menuPricesDishType, options);
                jsonText = json.TrimStart('[').TrimEnd(']');
                //delete old files
                File.Delete(file2);
                //create new files
                File.WriteAllText(file2, jsonText);
                Console.WriteLine(jsonText);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            //Upload new Files to OpenAI and Assistant
            string[] filePath = { file1, file2 };
            var filesUploadTasks = new List<Task>();
            foreach (var fileUpload in filePath)
            {
                async Task UploadAndAttachAsync(string file)
                {
                    var fileUploadRequest = new FileUploadRequest(file, "assistants");
                    var file_ = await api.FilesEndpoint.UploadFileAsync(fileUploadRequest); 
                    await assistant.AttachFileAsync(file_);
                }
                filesUploadTasks.Add(UploadAndAttachAsync(fileUpload));
            }
            await Task.WhenAll(filesUploadTasks);
            Console.WriteLine("Uploaded new files done");

        }

    }
}


