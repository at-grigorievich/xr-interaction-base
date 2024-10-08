namespace ATG.UI
{
    public enum SkinType: ushort
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Sixth = 6,
        Seventh = 7,
        Eighth = 8,
        Ninth = 9
    }

    public interface ISkinSwitcher
    {
        void SwitchToSkin(SkinType skinType);
    }
}