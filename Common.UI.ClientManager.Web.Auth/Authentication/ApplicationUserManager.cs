using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication
{
	// Token: 0x02000011 RID: 17
	public class ApplicationUserManager : UserManager<ClockWorkApplicationUser>
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00004E75 File Offset: 0x00003075
		public ApplicationUserManager(IUserStore<ClockWorkApplicationUser> store) : base(store)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004E80 File Offset: 0x00003080
		public override Task<ClockWorkApplicationUser> FindAsync(string userName, string password)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004E88 File Offset: 0x00003088
		[DebuggerStepThrough]
		public override Task<ClaimsIdentity> CreateIdentityAsync(ClockWorkApplicationUser user, string authenticationType)
		{
			ApplicationUserManager.<CreateIdentityAsync>d__2 <CreateIdentityAsync>d__ = new ApplicationUserManager.<CreateIdentityAsync>d__2();
			<CreateIdentityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ClaimsIdentity>.Create();
			<CreateIdentityAsync>d__.<>4__this = this;
			<CreateIdentityAsync>d__.user = user;
			<CreateIdentityAsync>d__.authenticationType = authenticationType;
			<CreateIdentityAsync>d__.<>1__state = -1;
			<CreateIdentityAsync>d__.<>t__builder.Start<ApplicationUserManager.<CreateIdentityAsync>d__2>(ref <CreateIdentityAsync>d__);
			return <CreateIdentityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004EDC File Offset: 0x000030DC
		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
		{
			ApplicationUserManager applicationUserManager = new ApplicationUserManager(new ClockWorkUserStore());
			applicationUserManager.UserValidator = new UserValidator<ClockWorkApplicationUser>(applicationUserManager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};
			applicationUserManager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = true,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true
			};
			applicationUserManager.UserLockoutEnabledByDefault = true;
			applicationUserManager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5.0);
			applicationUserManager.MaxFailedAccessAttemptsBeforeLockout = 5;
			applicationUserManager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ClockWorkApplicationUser>
			{
				MessageFormat = "Your security code is {0}"
			});
			applicationUserManager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ClockWorkApplicationUser>
			{
				Subject = "Security Code",
				BodyFormat = "Your security code is {0}"
			});
			applicationUserManager.EmailService = new EmailService();
			applicationUserManager.SmsService = new SmsService();
			IDataProtectionProvider dataProtectionProvider = options.DataProtectionProvider;
			bool flag = dataProtectionProvider != null;
			if (flag)
			{
				applicationUserManager.UserTokenProvider = new DataProtectorTokenProvider<ClockWorkApplicationUser>(dataProtectionProvider.Create(new string[]
				{
					"ASP.NET Identity"
				}));
			}
			return applicationUserManager;
		}
	}
}
