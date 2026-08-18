using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication
{
	// Token: 0x02000012 RID: 18
	public class ClockWorkApplicationUser : IdentityUser
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004FFF File Offset: 0x000031FF
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00005007 File Offset: 0x00003207
		public string StudentNumber { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00005010 File Offset: 0x00003210
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00005018 File Offset: 0x00003218
		public int PersonId { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00005021 File Offset: 0x00003221
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00005029 File Offset: 0x00003229
		public int NotetakerId { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00005032 File Offset: 0x00003232
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000503A File Offset: 0x0000323A
		public int InstructorId { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005043 File Offset: 0x00003243
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000504B File Offset: 0x0000324B
		public int AlternateContactId { get; set; }

		// Token: 0x06000096 RID: 150 RVA: 0x00005054 File Offset: 0x00003254
		[DebuggerStepThrough]
		public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ClockWorkApplicationUser> manager)
		{
			ClockWorkApplicationUser.<GenerateUserIdentityAsync>d__20 <GenerateUserIdentityAsync>d__ = new ClockWorkApplicationUser.<GenerateUserIdentityAsync>d__20();
			<GenerateUserIdentityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ClaimsIdentity>.Create();
			<GenerateUserIdentityAsync>d__.<>4__this = this;
			<GenerateUserIdentityAsync>d__.manager = manager;
			<GenerateUserIdentityAsync>d__.<>1__state = -1;
			<GenerateUserIdentityAsync>d__.<>t__builder.Start<ClockWorkApplicationUser.<GenerateUserIdentityAsync>d__20>(ref <GenerateUserIdentityAsync>d__);
			return <GenerateUserIdentityAsync>d__.<>t__builder.Task;
		}
	}
}
