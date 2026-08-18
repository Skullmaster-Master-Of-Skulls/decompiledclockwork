using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin.Security.DataProtection;

namespace Microsoft.AspNet.Identity.Owin
{
	// Token: 0x02000007 RID: 7
	public class DataProtectorTokenProvider<TUser, TKey> : IUserTokenProvider<TUser, TKey> where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002759 File Offset: 0x00000959
		public DataProtectorTokenProvider(IDataProtector protector)
		{
			if (protector == null)
			{
				throw new ArgumentNullException("protector");
			}
			this.Protector = protector;
			this.TokenLifespan = TimeSpan.FromDays(1.0);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000278A File Offset: 0x0000098A
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002792 File Offset: 0x00000992
		public IDataProtector Protector { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000279B File Offset: 0x0000099B
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000027A3 File Offset: 0x000009A3
		public TimeSpan TokenLifespan { get; set; }

		// Token: 0x06000022 RID: 34 RVA: 0x000029D8 File Offset: 0x00000BD8
		public async Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			MemoryStream ms = new MemoryStream();
			using (BinaryWriter writer = ms.CreateWriter())
			{
				writer.Write(DateTimeOffset.UtcNow);
				writer.Write(Convert.ToString(user.Id, CultureInfo.InvariantCulture));
				writer.Write(purpose ?? "");
				string stamp = null;
				if (manager.SupportsUserSecurityStamp)
				{
					stamp = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture<string>();
				}
				writer.Write(stamp ?? "");
			}
			byte[] protectedBytes = this.Protector.Protect(ms.ToArray());
			return Convert.ToBase64String(protectedBytes);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002CFC File Offset: 0x00000EFC
		public async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser, TKey> manager, TUser user)
		{
			try
			{
				byte[] unprotectedData = this.Protector.Unprotect(Convert.FromBase64String(token));
				MemoryStream ms = new MemoryStream(unprotectedData);
				using (BinaryReader reader = ms.CreateReader())
				{
					DateTimeOffset creationTime = reader.ReadDateTimeOffset();
					DateTimeOffset expirationTime = creationTime + this.TokenLifespan;
					if (expirationTime < DateTimeOffset.UtcNow)
					{
						return false;
					}
					string userId = reader.ReadString();
					if (!string.Equals(userId, Convert.ToString(user.Id, CultureInfo.InvariantCulture)))
					{
						return false;
					}
					string purp = reader.ReadString();
					if (!string.Equals(purp, purpose))
					{
						return false;
					}
					string stamp = reader.ReadString();
					if (reader.PeekChar() != -1)
					{
						return false;
					}
					if (manager.SupportsUserSecurityStamp)
					{
						string expectedStamp = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture<string>();
						return stamp == expectedStamp;
					}
					return stamp == "";
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D63 File Offset: 0x00000F63
		public Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002D6B File Offset: 0x00000F6B
		public Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
		{
			return Task.FromResult<int>(0);
		}
	}
}
