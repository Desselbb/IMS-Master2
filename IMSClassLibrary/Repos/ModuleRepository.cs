


namespace IMSClassLibrary.Repos
{
    public class ModuleRepository : IInterface<Module>
    {
        SystemDbContext _context;

        public ModuleRepository(SystemDbContext context)
        {
            this._context = context;
        }

        //my code

        public ResultObject<Module> Add(Module module)
        {
            try
            {
                if (this.Get(module.Url.Trim()).Data.Id < 1)
                {
                    _context.Modules.Add(module);
                    _context.SaveChanges();
                    return ResultObject<Module>.Success("Module has been added successfully");
                }
                else
                {
                    return ResultObject<Module>.Failure("There is already another Menu with the same Url");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Module>.Failure("There is already another Module with the same Url");
            }
        }
        //end


        public ResultObject<Module> AddRange(List<Module> modules)
        {
            try
            {
                bool addAll = true;
                foreach (var md in modules)
                {
                    if (_context.Modules.Where(d=>d.Url.Trim() == md.Url.Trim()).FirstOrDefault() == null)
                    {
                        addAll = false;
                        break;
                    }
                }
                if (addAll)
                {
                    _context.Modules.AddRange(modules);
                    _context.SaveChanges();

                    return ResultObject<Module>.Success("Modules have been added successfully");
                }
                else
                {
                    return ResultObject<Module>.Failure("Some Module in the submited list have Emails similar to the ones in the system already,this will cause conflicts. the operation has been aborted");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d.Message + "\n" + d.StackTrace);
                return ResultObject<Module>.Failure("Could not add the Modules, Something went wrong");
            }

        }

        //end

        public ResultObject<Module> DeleteById(int Id)
        {
            try
            {
                _context.Modules.Remove(_context.Modules.Single(b => b.Id == Id));
                _context.SaveChanges();
                return ResultObject<Module>.Success("The Menu has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Module>.Failure("Could not delete the Module, something went wrong");
            }
        }
        //end
        public ResultObject<Module> Delete(Module module)
        {
            try
            {
                _context.Modules.Remove(module);
                _context.SaveChanges();
                return ResultObject<Module>.Success("The Module has been deleted successfuly");
            }
            catch
            {
                return ResultObject<Module>.Failure("Could not delete the Module, something went wrong");
            }
        }
        public ResultObject<Module> Get(int Id)
        {
            try
            {
                return ResultObject<Module>.Success("Module Retrived", _context.Modules.Single(b => b.Id == Id));
            }
            catch (Exception d)
            {
                return ResultObject<Module>.Failure("Could not retrieve the Module Something went wrong", new Module());
            }
        }
        //end get metod by Id


        public ResultObject<Module> Get(string url)
        {
            try
            {
                return ResultObject<Module>.Success("",_context.Modules.Single(b => b.Url.Trim() == url));
            }
            catch (Exception d)
            {
                return ResultObject<Module>.Failure("Could not retrive the object", new Module());
            }
        } 
   

 

        public ResultObject<List<Module>> GetAll()
        {
            try
            {
                return ResultObject<List<Module>>.Success("",_context.Modules.ToList());
            }
            catch
            {
                return ResultObject<List<Module>>.Failure("Could not retrive the object", new List<Module>());
            }
        }
		//end byAll

		public ResultObject<List<Module>> GetChildMenus(int ParentId)
		{
			try
			{
				return ResultObject<List<Module>>.Success("", _context.Modules.Where(d=>d.ParentId==ParentId).ToList());
			}
			catch
			{
				return ResultObject<List<Module>>.Failure("Could not retrive the object", new List<Module>());
			}
		}
		//end byAll

		public ResultObject<Module> Update(Module module)
        {
            try
            {
                if (_context.Modules.Where(f=>f.Url.Trim() == module.Url.Trim() && f.Id!=module.Id).FirstOrDefault() ==  null)
                {
                    _context.Modules.Update(module);
                    _context.SaveChanges();
                    return ResultObject<Module>.Success("Object Updated successfuly");

                }
                else
                {
                    _context.Entry(module).Reload();
                    return ResultObject<Module>.Failure("There is already another Module with the same Parent");
                }
            }
            catch
            {
                _context.Entry(module).Reload();
                return ResultObject<Module>.Failure("Could not update the Module, Something went wrong!");
            }
        }



      
    }
}
