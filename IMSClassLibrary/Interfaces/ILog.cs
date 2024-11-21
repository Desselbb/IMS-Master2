
namespace IMSClassLibrary.Interfaces
{
    internal interface ILog
    {
        bool Add(Comment comment);

        Comment Get(int Id);

        List<Comment>GetAll();

    }
}
