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
    public class AssetDetails
    {
        public enum ConditionType
        {
            New,
            Good,
            Damaged,
            Disposed
        }

        public string Location { get; set; }
        public ConditionType Condition { get; set; }
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}