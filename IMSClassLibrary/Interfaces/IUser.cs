
namespace IMSClassLibrary.Interfaces
{
    internal interface IUser<T>
    {
        T Add(T item);

        List<T> AddRange(List<T> items);

        T Update(T item);

        T Get(int Id);

        List<T> GetAll();

        bool DeleteById(int Id);

        bool Delete(T item);
    }


}
