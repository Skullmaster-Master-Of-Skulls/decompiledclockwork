using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication
{
	// Token: 0x02000016 RID: 22
	public class SmsService : IIdentityMessageService
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00005454 File Offset: 0x00003654
		public Task SendAsync(IdentityMessage message)
		{
			return Task.FromResult<int>(0);
		}
	}
}
