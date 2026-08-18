using System;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.Security.Cryptography;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200037F RID: 895
	internal class LogonToken : IDisposable
	{
		// Token: 0x06002127 RID: 8487 RVA: 0x0007B019 File Offset: 0x00079219
		public LogonToken(string userName, string password, byte[] salt, ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies)
		{
			this.userName = userName;
			this.passwordHash = LogonToken.ComputeHMACSHA256Hash(password, salt);
			this.salt = salt;
			this.authorizationPolicies = SecurityUtils.CloneAuthorizationPoliciesIfNecessary(authorizationPolicies);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0007B04C File Offset: 0x0007924C
		public bool PasswordEquals(string password)
		{
			byte[] b = LogonToken.ComputeHMACSHA256Hash(password, this.salt);
			return CryptoHelper.IsEqual(this.passwordHash, b);
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x0007B072 File Offset: 0x00079272
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0007B07A File Offset: 0x0007927A
		public ReadOnlyCollection<IAuthorizationPolicy> GetAuthorizationPolicies()
		{
			return SecurityUtils.CloneAuthorizationPoliciesIfNecessary(this.authorizationPolicies);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0007B087 File Offset: 0x00079287
		public void Dispose()
		{
			SecurityUtils.DisposeAuthorizationPoliciesIfNecessary(this.authorizationPolicies);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0007B094 File Offset: 0x00079294
		private static byte[] ComputeHMACSHA256Hash(string password, byte[] key)
		{
			byte[] result;
			using (HMACSHA256 hmacsha = new HMACSHA256(key))
			{
				result = hmacsha.ComputeHash(Encoding.Unicode.GetBytes(password));
			}
			return result;
		}

		// Token: 0x04001F34 RID: 7988
		private string userName;

		// Token: 0x04001F35 RID: 7989
		private byte[] passwordHash;

		// Token: 0x04001F36 RID: 7990
		private byte[] salt;

		// Token: 0x04001F37 RID: 7991
		private ReadOnlyCollection<IAuthorizationPolicy> authorizationPolicies;
	}
}
