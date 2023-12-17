using IdaraTech_Admin.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using IdaraTech_Admin.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace IdaraTechApi.Services
{
    public class ContextSeedService
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ContextSeedService(Context context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public UserManager<User> UserManager => _userManager;

        public async Task InitializeContextAsync()
        {
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
            {
                // applique toute migration en attente dans notre base de données
                await _context.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.SuperAdminRole });
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.AdminRole });
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.ManagerRole });
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.CustomerRole });
                await _roleManager.CreateAsync(new IdentityRole { Name = SD.CitizenRole });
            }

            if (!_userManager.Users.AnyAsync().GetAwaiter().GetResult())
            {
                var superAdmin = new User
                {
                    FirstName = "Super Admin",
                    LastName = "Mustapha",
                    UserName = "superAdmin@example.com",
                    Email = "superAdmin@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(superAdmin, "123456");
                await _userManager.AddToRolesAsync(superAdmin, new[] { SD.SuperAdminRole, SD.AdminRole, SD.ManagerRole, SD.CustomerRole, SD.CitizenRole });
                await _userManager.AddClaimsAsync(superAdmin, new Claim[]
                {
                    new Claim(ClaimTypes.Email, superAdmin.Email),
                    new Claim(ClaimTypes.Surname, superAdmin.LastName)
                });


                var admin = new User
                {
                    FirstName = "Admin",
                    LastName = "Nacer",
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(admin, "123456");
                await _userManager.AddToRolesAsync(admin, new[] { SD.AdminRole, SD.ManagerRole, SD.CustomerRole, SD.CitizenRole });
                await _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.Surname, admin.LastName)
                });

                var manager = new User
                {
                    FirstName = "manager",
                    LastName = "Nacer",
                    UserName = "manager@example.com",
                    Email = "manager@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(manager, "123456");
                await _userManager.AddToRolesAsync(manager, new[] { SD.ManagerRole, SD.CustomerRole, SD.CitizenRole });
                await _userManager.AddClaimsAsync(manager, new Claim[]
                {
                    new Claim(ClaimTypes.Email, manager.Email),
                    new Claim(ClaimTypes.Surname, manager.LastName)
                });

                var customer = new User
                {
                    FirstName = "customer",
                    LastName = "Zineb",
                    UserName = "customer@example.com",
                    Email = "customer@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(customer, "123456");
                await _userManager.AddToRolesAsync(customer, new[] { SD.CustomerRole, SD.CitizenRole });
                await _userManager.AddClaimsAsync(customer, new Claim[]
                {
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim(ClaimTypes.Surname, customer.LastName)
                });


                var citoyen = new User
                {
                    FirstName = "citoyen",
                    LastName = "Hanane",
                    UserName = "citizen@example.com",
                    Email = "citizen@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(citoyen, "123456");
                await _userManager.AddToRoleAsync(citoyen, SD.CitizenRole);
                await _userManager.AddClaimsAsync(citoyen, new Claim[]
                {
                    new Claim(ClaimTypes.Email, citoyen.Email),
                    new Claim(ClaimTypes.Surname, citoyen.LastName)
                });

                var citoyenship = new User
                {
                    FirstName = "shipcitoyen",
                    LastName = "Adel",
                    UserName = "shipcitoyen@example.com",
                    Email = "shipcitoyen@example.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(citoyenship, "123456");
                await _userManager.AddToRoleAsync(citoyenship, SD.CitizenRole);
                await _userManager.AddClaimsAsync(citoyenship, new Claim[]
                {
                    new Claim(ClaimTypes.Email, citoyenship.Email),
                    new Claim(ClaimTypes.Surname, citoyenship.LastName)
                });


            }
        }
    }
}
