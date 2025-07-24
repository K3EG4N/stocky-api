using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Provider.Context;

namespace Host.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly StockyContext _context;

        public UserController(StockyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ListUserDto>> GetUsers()
        {
            var userRoles = _context.UserRoles
                                            .Include(x => x.User)
                                            .Include(x => x.Role)
                                         .ToList();

            var response = from ur in userRoles
                        group ur by ur.User
                        into g
                        select new ListUserDto()
                        {
                            UserName = g.Key.UserName,
                            Roles = (from r in g
                                    select r.Role.Name).ToList()
                        };
            
            return response.ToList();
        }
    }
}
