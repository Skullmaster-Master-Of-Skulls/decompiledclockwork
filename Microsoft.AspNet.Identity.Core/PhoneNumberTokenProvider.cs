using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000023 RID: 35
	public class PhoneNumberTokenProvider<TUser, TKey> : TotpSecurityStampBasedTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003238 File Offset: 0x00001438
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003249 File Offset: 0x00001449
		public string MessageFormat
		{
			get
			{
				return this._body ?? "{0}";
			}
			set
			{
				this._body = value;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003410 File Offset: 0x00001610
		public override async Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			string phoneNumber = await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture<string>();
			return !string.IsNullOrWhiteSpace(phoneNumber) && await manager.IsPhoneNumberConfirmedAsync(user.Id).WithCurrentCulture<bool>();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003590 File Offset: 0x00001790
		public override async Task<string> GetUserModifierAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			string phoneNumber = await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture<string>();
			return "PhoneNumber:" + purpose + ":" + phoneNumber;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035F0 File Offset: 0x000017F0
		public override Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return manager.SendSmsAsync(user.Id, string.Format(CultureInfo.CurrentCulture, this.MessageFormat, new object[]
			{
				token
			}));
		}

		// Token: 0x04000012 RID: 18
		private string _body;
	}
}
