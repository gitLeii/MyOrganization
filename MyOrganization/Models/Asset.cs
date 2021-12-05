namespace MyOrganization.Models
{
    public class Asset
    {
        public int AssetID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int OrganizationID { get; set; }
        public virtual Organization Organization { get; set; }

    }
}