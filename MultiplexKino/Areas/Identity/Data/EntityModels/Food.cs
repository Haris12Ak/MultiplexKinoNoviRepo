namespace MultiplexKino.Areas.Identity.Data.EntityModels
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public byte[] Image { get; set; }
        public float Price { get; set; }

        public override string ToString()
        {
            return Price.ToString();
        }
    }
}
