namespace Saving
{
    public interface ISavable
    {
        void Load(string obj);
        string Save();
    }
}