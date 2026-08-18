using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000021 RID: 33
	public class EmailTokenProvider<TUser, TKey> : TotpSecurityStampBasedTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E30 File Offset: 0x00001030
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002E41 File Offset: 0x00001041
		public string Subject
		{
			get
			{
				return this._subject ?? string.Empty;
			}
			set
			{
				this._subject = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002E4A File Offset: 0x0000104A
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002E5B File Offset: 0x0000105B
		public string BodyFormat
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

		// Token: 0x06000068 RID: 104 RVA: 0x0000300C File Offset: 0x0000120C
		public override async Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
		{
			string email = await manager.GetEmailAsync(user.Id).WithCurrentCulture<string>();
			return !string.IsNullOrWhiteSpace(email) && await manager.IsEmailConfirmedAsync(user.Id).WithCurrentCulture<bool>();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003178 File Offset: 0x00001378
		public override async Task<string> GetUserModifierAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
		{
			string email = await manager.GetEmailAsync(user.Id).WithCurrentCulture<string>();
			return "Email:" + purpose + ":" + email;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031D8 File Offset: 0x000013D8
		public override Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return manager.SendEmailAsync(user.Id, this.Subject, string.Format(CultureInfo.CurrentCulture, this.BodyFormat, new object[]
			{
				token
			}));
		}

		// Token: 0x04000010 RID: 16
		private string _body;

		// Token: 0x04000011 RID: 17
		private string _subject;
	}
}
