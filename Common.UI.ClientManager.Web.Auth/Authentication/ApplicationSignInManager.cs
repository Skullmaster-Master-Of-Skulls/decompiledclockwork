using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication
{
	// Token: 0x02000010 RID: 16
	public class ApplicationSignInManager : SignInManager<ClockWorkApplicationUser, string>
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004D61 File Offset: 0x00002F61
		public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004D70 File Offset: 0x00002F70
		public override Task<ClaimsIdentity> CreateUserIdentityAsync(ClockWorkApplicationUser user)
		{
			return user.GenerateUserIdentityAsync((ApplicationUserManager)base.UserManager);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004D94 File Offset: 0x00002F94
		public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
		{
			return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004DB8 File Offset: 0x00002FB8
		[DebuggerStepThrough]
		public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
		{
			ApplicationSignInManager.<PasswordSignInAsync>d__3 <PasswordSignInAsync>d__ = new ApplicationSignInManager.<PasswordSignInAsync>d__3();
			<PasswordSignInAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SignInStatus>.Create();
			<PasswordSignInAsync>d__.<>4__this = this;
			<PasswordSignInAsync>d__.userName = userName;
			<PasswordSignInAsync>d__.password = password;
			<PasswordSignInAsync>d__.isPersistent = isPersistent;
			<PasswordSignInAsync>d__.shouldLockout = shouldLockout;
			<PasswordSignInAsync>d__.<>1__state = -1;
			<PasswordSignInAsync>d__.<>t__builder.Start<ApplicationSignInManager.<PasswordSignInAsync>d__3>(ref <PasswordSignInAsync>d__);
			return <PasswordSignInAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004E1C File Offset: 0x0000301C
		[DebuggerStepThrough]
		public override Task SignInAsync(ClockWorkApplicationUser user, bool isPersistent, bool rememberBrowser)
		{
			ApplicationSignInManager.<SignInAsync>d__4 <SignInAsync>d__ = new ApplicationSignInManager.<SignInAsync>d__4();
			<SignInAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SignInAsync>d__.<>4__this = this;
			<SignInAsync>d__.user = user;
			<SignInAsync>d__.isPersistent = isPersistent;
			<SignInAsync>d__.rememberBrowser = rememberBrowser;
			<SignInAsync>d__.<>1__state = -1;
			<SignInAsync>d__.<>t__builder.Start<ApplicationSignInManager.<SignInAsync>d__4>(ref <SignInAsync>d__);
			return <SignInAsync>d__.<>t__builder.Task;
		}
	}
}
