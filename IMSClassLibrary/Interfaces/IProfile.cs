namespace IMSClassLibrary.Interfaces
{
    internal interface IProfile
    {
        bool Add(Profile profile);

        Profile Get(int Id);

        List<Profile> GetAll();  

        bool Update(Profile profile);

        bool DeleteById(int Id);  
    }
}
