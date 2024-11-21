using System;
using System.Collections.Generic;
using System.Linq;


namespace IMSClassLibrary.Repos
{
    public class Repository<T> : IInterface<T>
    {
       protected SystemDbContext _context;

        public Repository(SystemDbContext context)
        {
            _context = context;
        }

        public ResultObject<T> Add(T item)
        {
            try
            {
                _context.Add(item);
                _context.SaveChanges();
                return ResultObject<T>.Success("Item added Successfully");
            }
            catch (Exception ex) 
            {
                return ResultObject<T>.Failure("Could not Add Item");
            }
        }

        public ResultObject<T> AddRange(List<T> items)
        {
            try
            {
                    _context.AddRange(items);
                    _context.SaveChanges();

                return ResultObject<T>.Success("Items have been added Successfully");
                
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<T>.Failure("Could not add the Items, Something went wrong");
            }
        }

        public ResultObject<T> Delete(T item)
        {
            throw new NotImplementedException();
        }

        public ResultObject<T> DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public ResultObject<T> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public ResultObject<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResultObject<T> Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
