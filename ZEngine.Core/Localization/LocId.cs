namespace ZEngine.Core.Localization
{
    public struct LocId
    {
        public int Id;
        
        public static implicit operator string(LocId locId)
        {
            return LocIdResolver.Instance.Resolve(locId);
        }

        public override string ToString()
        {
            return this;
        }
    }
}