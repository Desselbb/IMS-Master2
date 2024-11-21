
namespace IMSClassLibrary.Repos
{



    public class CommentRepository : IInterface<Comment>
    {
        SystemDbContext _context;
        public CommentRepository(SystemDbContext context)
        {
            this._context = context;
        }



        public ResultObject<Comment> Add(Comment item)
        {
            try
            {
                    _context.Comments.Add(item);
                    _context.SaveChanges();
                    return ResultObject<Comment>.Success("Comment has been added successfully");
               
               
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Comment>.Failure("Could not add Comment, Something went wrong");
            }
        }

        public ResultObject<Comment> AddRange(List<Comment> items)
        {
            try
            {
             
                
                    _context.Comments.AddRange(items);
                    _context.SaveChanges();

                    return ResultObject<Comment>.Success("Comment have been added successfully");
              
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Comment>.Failure("Could not add the Comment, Something went wrong");
            }
        }

        public ResultObject<Comment> Delete(Comment comment)
        {

            try
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return ResultObject<Comment>.Success("The Comment has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Comment>.Failure("Could not delete the Comment, something went wrong");
            }



        }

        public ResultObject<Comment> DeleteById(int Id)
        {
            try
            {
                _context.Comments.Remove(_context.Comments.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<Comment>.Success("The Comment has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Comment>.Failure("Could not delete the Comment, something went wrong");
            }
        }

        public ResultObject<List<Comment>> Get(int ProjectId)
        {
            try
            {
                return ResultObject<List<Comment>>.Success("Comment Retrived", _context.Comments.Where(b => b.Id == ProjectId).ToList());
            }
            catch (Exception d)
            {
                return ResultObject<List<Comment>>.Failure("Could not retrieve the Comment, Something went wrong", new List<Comment>());
            }
        
        }

        public ResultObject<List<Comment>> GetAll()
        {
            try
            {
                return ResultObject<List<Comment>>.Success("", _context.Comments.Include(s => s.Project).Include(d=> d.User).ToList());
            }
            catch
            {
                return ResultObject<List<Comment>>.Failure("Could not retrive the object", new List<Comment>());
            }
        }

        public ResultObject<Comment> Update(Comment item)
        {
            try
            {
                
                    _context.Comments.Update(item);
                    _context.SaveChanges();
                    return ResultObject<Comment>.Success("Object Updated successfuly");
             
            }
            catch
            {
                _context.Entry(item).Reload();
                return ResultObject<Comment>.Failure("Could not update the Comment, Something went wrong!");
            }
        }

        ResultObject<Comment> IInterface<Comment>.Get(int Id)
        {
            try
            {
                return ResultObject<Comment>.Success("Comment Retrived", _context.Comments.Include(w => w.UserId).Include(w => w.ProjectId).FirstOrDefault());
            }
            catch (Exception d)
            {
                return ResultObject<Comment>.Failure("Could not retrieve the Comment, Something went wrong", new Comment());
            }
        }
    }
}
