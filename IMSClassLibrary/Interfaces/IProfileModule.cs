namespace IMSClassLibrary.Interfaces
{
    internal interface IProfileModule
    {
        bool Add(ProfileModule profileModule);
        bool AddRange(List<ProfileModule> ProfileModules);
        ProfileModule Get(int Id);
        List<ProfileModule> GetAll();
        bool Update(ProfileModule ProfileModule);
        bool Delete(int Id);
    }
}
