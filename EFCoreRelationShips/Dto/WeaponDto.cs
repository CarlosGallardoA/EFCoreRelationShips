namespace EFCoreRelationShips.Dto
{
    public class WeaponDto
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; } = 10;
        public int CharaterId { get; set; }
    }
}
