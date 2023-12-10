namespace RestaurantApp.Model
{
    public class ModelOrder
    {
        public DateTime DateOfOrder { get; set; }
        public int Bill { get; set; }
        public string AdditionalComment { get; set; }
        public int StateId { get; set; }
        public List<ModelOrderPosition> OrderPositions { get; set; }

    }
}
