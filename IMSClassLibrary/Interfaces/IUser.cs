namespace IMSClassLibrary.Interfaces
{
    internal interface IUser
    {
        bool Add(User User);

        bool AddRange(List<User> Users);

        User Get(int Id);

        List<User> GetAll();

        bool Update(User User);

        bool DeleteById(int Id);
    }
}
