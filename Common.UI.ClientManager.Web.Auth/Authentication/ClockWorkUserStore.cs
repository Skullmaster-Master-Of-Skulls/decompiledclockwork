using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth.Authentication
{
	// Token: 0x02000013 RID: 19
	public class ClockWorkUserStore : IUserStore<ClockWorkApplicationUser>, IUserStore<ClockWorkApplicationUser, string>, IDisposable
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000050A8 File Offset: 0x000032A8
		public void Dispose()
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004E80 File Offset: 0x00003080
		public Task CreateAsync(ClockWorkApplicationUser user)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004E80 File Offset: 0x00003080
		public Task UpdateAsync(ClockWorkApplicationUser user)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004E80 File Offset: 0x00003080
		public Task DeleteAsync(ClockWorkApplicationUser user)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000050AC File Offset: 0x000032AC
		public Task<ClockWorkApplicationUser> FindByIdAsync(string userId)
		{
			ClockWorkApplicationUser clockWorkApplicationUser = (ClockWorkApplicationUser)Activator.CreateInstance(typeof(ClockWorkApplicationUser));
			clockWorkApplicationUser.UserName = userId;
			return Task.FromResult<ClockWorkApplicationUser>(clockWorkApplicationUser);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004E80 File Offset: 0x00003080
		public Task<ClockWorkApplicationUser> FindByNameAsync(string userName)
		{
			throw new NotImplementedException();
		}
	}
}
