namespace RestaurantApp.Entities
{
    public class TState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //RELATION
        public virtual List<TOrder> TOrder { get; set; }
    }
}
