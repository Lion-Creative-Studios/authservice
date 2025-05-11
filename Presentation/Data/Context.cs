using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contexts;
/* Updateded to make User based on Int generated code by chatgpt 4o */
/* User to int reverted, was trying to tie the login/register microsoft core identity user to be a member in the project */
public class Context(DbContextOptions<Context> options) : IdentityDbContext<UserEntity>(options)
{
}
