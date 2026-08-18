using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication
{
	// Token: 0x02000015 RID: 21
	public class EmailService : IIdentityMessageService
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x0000543C File Offset: 0x0000363C
		public Task SendAsync(IdentityMessage message)
		{
			return Task.FromResult<int>(0);
		}
	}
}
