namespace IMSClassLibrary.Interfaces
{
    internal interface IUserProfile
    {

        bool Add(UserProfile userProfile);

        UserProfile Get(int Id);

        List<UserProfile> GetAll();

        bool Update(UserProfile userProfile);

        bool DeleteById(int Id);
    }
}
