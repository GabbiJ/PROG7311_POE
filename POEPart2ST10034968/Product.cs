namespace POEPart2ST10034968
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProdType { get; set; } //can be produce, tools, livestock or other
        public DateOnly ProductionDate { get; set; }
        public string Farmer { get; set; }

        public Product(string id, string name, string prodType, DateOnly productionDate, string farmer)
        {
            Id = id;
            Name = name;
            ProdType = prodType;
            ProductionDate = productionDate;
            Farmer = farmer;
        }
    }
}
