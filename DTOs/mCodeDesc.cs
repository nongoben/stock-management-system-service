namespace StockManagementSystem.DTOs
{
    public class mCodeDesc
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public mCodeDesc(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}