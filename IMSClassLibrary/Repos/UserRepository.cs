

namespace IMSClassLibrary.Repos
{
    public class UserRepository : IInterface<User>
    {
        SystemDbContext _context;
        public UserRepository(SystemDbContext context)
        {
            this._context = context;
        }

        public ResultObject<User> Add(User user)
        {
            try
            {
                if (_context.Users.Where(u=>u.Email == user.Email.Trim()).FirstOrDefault() == null)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    EmailRepository ee = new EmailRepository();
                    ee.sendMail(user.Email, "Intern Information System", "You have been registered as an Intern on Intern Information System, Kindly login in at ");

                    return ResultObject<User>.Success("User has been added successfully");
                }
                else
                {
                    return ResultObject<User>.Failure("There is already another User with the same Email");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<User>.Failure("Could not add user, something went wrong.");
            }
        }


        public ResultObject<User> AddRange(List<User> users)
        {
            try
            {
                bool addAll = true;
                foreach (var md in users)
                {
                    if (Get(md.Email.Trim()).Data.Id > 0)
                    {
                        addAll = false;
                        break;
                    }
                }
                if (addAll)
                {
                    _context.Users.AddRange(users);
                    _context.SaveChanges();

                    return ResultObject<User>.Success("Users have been added successfully");
                }
                else
                {
                    return ResultObject<User>.Failure("Some User in the submited list have Emails similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<User>.Failure("Could not add the Users, Something went wrong");
            }


            
        }

        public ResultObject<User> Delete(User item)
        {
            try
            {
                _context.Users.Remove(item);
                _context.SaveChanges();
                return ResultObject<User>.Success("The User has been deleted successfuly");
            }
            catch
            {
                return ResultObject<User>.Failure("Could not delete the User, something went wrong");
            }
        }

        public ResultObject<User> DeleteById(int Id)
        {
            try
            {
                _context.Users.Remove(_context.Users.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<User>.Success("The User has been deleted successfuly");
            }
            catch
            {
                return ResultObject<User>.Failure("Could not delete the User, something went wrong");
            }
        }




        public ResultObject<User> Get(int Id)
        {
            try
            {
                return ResultObject<User>.Success("User Retrived", _context.Users.Include(b => b.Department.Name).Single(b => b.Id == Id));
            }
            catch (Exception d)
            {
                return ResultObject<User>.Failure("Could not retrieve the User, Something went wrong", new User());
            }
        }
        public ResultObject<User> Get(string email)
        {
            try
            {
                return ResultObject<User>.Success("", _context.Users.Include(b => b.Department.Name).Single(b => b.Email.Trim() == email.Trim()));
            }
            catch (Exception d)
            {
                return ResultObject<User>.Failure("Could not retrive the object", new User());
            }


        }
       
            public ResultObject<List<User>> GetAll()
            {
                try
                {
                    return ResultObject<List<User>>.Success("", _context.Users.ToList());
                }
                catch
                {
                    return ResultObject<List<User>>.Failure("Could not retrive the object", new List<User>());
                }
            }

        public ResultObject<User> Update(User user)
        {
            try
            {
                if (_context.Users.Where(w=>w.Email.Trim() == user.Email && user.Id!=w.Id).FirstOrDefault() == null)
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return ResultObject<User>.Success("User Updated successfuly");

                }
                else
                {
                    _context.Entry(user).Reload();
                    return ResultObject<User>.Failure("There is already another User with the same Email");
                }
            }
            catch
            {
                _context.Entry(user).Reload();
                return ResultObject<User>.Failure("Could not update the User, Something went wrong!");
            }

        }
    }
}
