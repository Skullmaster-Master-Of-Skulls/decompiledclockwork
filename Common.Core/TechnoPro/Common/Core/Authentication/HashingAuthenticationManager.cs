using System;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Security.Hashing;

namespace TechnoPro.Common.Core.Authentication
{
	// Token: 0x02000126 RID: 294
	public class HashingAuthenticationManager : IHashingAuthenticationManager, IBaseOperationContext<HashingOperationContext>
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0005692C File Offset: 0x00054B2C
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00056934 File Offset: 0x00054B34
		public HashingOperationContext OpContext { get; set; }

		// Token: 0x06000C62 RID: 3170 RVA: 0x0005693D File Offset: 0x00054B3D
		public HashingAuthenticationManager(HashingOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00056950 File Offset: 0x00054B50
		public bool ValidateClockWorkHash(ClockWorkHashAuthentication hashAuth)
		{
			DateTime t;
			bool flag = !string.IsNullOrEmpty(hashAuth.StampTime) && DateTime.TryParse(hashAuth.StampTime, out t);
			bool result;
			if (flag)
			{
				DateTime now = DateTime.Now;
				bool flag2 = this.OpContext.TokenLifetimeInMinutes > 0 && (t < now.AddMinutes((double)(-(double)this.OpContext.TokenLifetimeInMinutes)) || t > now.AddMinutes((double)this.OpContext.TokenLifetimeInMinutes));
				if (flag2)
				{
					result = false;
				}
				else
				{
					string password = hashAuth.Username + hashAuth.StampTime + (hashAuth.Seed ?? string.Empty) + this.OpContext.HashingKey;
					result = PasswordHashFactory.GetHashingProvider(eHashingType.ClockWorkDefault).ValidatePassword(password, hashAuth.HashValue, null);
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00056A28 File Offset: 0x00054C28
		public bool ValidateHash(eHashingType hashingType, HashAuthentication hashAuth)
		{
			DateTime t;
			bool flag = string.IsNullOrEmpty(hashAuth.StampTime) || !DateTime.TryParse(hashAuth.StampTime, out t);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = (this.OpContext.TokenLifetimeInMinutes > 0) ? this.OpContext.TokenLifetimeInMinutes : 30;
				DateTime now = DateTime.Now;
				bool flag2 = t < now.AddMinutes((double)(-(double)num)) || t > now.AddMinutes((double)num);
				if (flag2)
				{
					result = false;
				}
				else
				{
					string password = hashAuth.Username + hashAuth.StampTime + (hashAuth.Seed ?? string.Empty) + this.OpContext.HashingKey;
					PasswordHashContext context = new PasswordHashContext
					{
						SecretKey = hashAuth.SecretKey
					};
					result = PasswordHashFactory.GetHashingProvider(hashingType).ValidatePassword(password, hashAuth.HashValue, context);
				}
			}
			return result;
		}

		// Token: 0x04000253 RID: 595
		private const int DEFAULT_TOKENT_LIFETIME_IN_MINUTES = 30;
	}
}
