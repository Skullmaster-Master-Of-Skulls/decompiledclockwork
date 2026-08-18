using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure;
using TechnoPro.Common.Security.Hashing;

namespace TechnoPro.Common.Core.Azure.Storage
{
	// Token: 0x02000003 RID: 3
	public static class TokenBasedClientCredentialsFactory
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002C2C File Offset: 0x00000E2C
		public static TokenBasedClientCredentialsDTO GenerateToken(string clientId, string privateKey = null)
		{
			DateTimeOffset utcNow = DateTimeOffset.UtcNow;
			return new TokenBasedClientCredentialsDTO
			{
				ClientId = clientId,
				TokenIssuedDateTime = utcNow,
				Token = PasswordHashFactory.GetHashingProvider(eHashingType.PBKDF2_SHA1).CreateHash(clientId + (privateKey ?? "_ARFjfJ4(KJFS$4#%kdjf(y3we_+TR743") + utcNow.ToString("yyyy-MM-dd hh:mm:ss"), null)
			};
		}

		// Token: 0x04000003 RID: 3
		private const string PrivateKey = "_ARFjfJ4(KJFS$4#%kdjf(y3we_+TR743";
	}
}
