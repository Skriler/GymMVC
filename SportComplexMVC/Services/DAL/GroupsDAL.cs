using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Services.DAL
{
    public class GroupsDAL : EntityDAL
    {
        public GroupsDAL(ApplicationContext context)
            : base(context)
        { }
        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            Group group = await db.Groups
                .Include(g => g.Clients)
                .ThenInclude(c => c.ApplicationUser)
                .ThenInclude(u => u.Gender)
                .Where(g => g.Id == groupId)
                .FirstOrDefaultAsync();

            return group;
        }
    }
}
