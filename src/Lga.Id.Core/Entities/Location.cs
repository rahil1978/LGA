namespace Lga.Id.Core.Entities
{
    public class Location : BaseEntity
    {        
        public int? Code { get; set; }     
        public string PlaceName { get; set; }
        public State State { get; set; }
    }
}