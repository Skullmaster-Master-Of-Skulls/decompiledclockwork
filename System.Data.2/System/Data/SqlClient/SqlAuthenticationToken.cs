using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001E0 RID: 480
	public class SqlAuthenticationToken
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x000D3A44 File Offset: 0x000D2E44
		public DateTimeOffset ExpiresOn { get; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x000D3A58 File Offset: 0x000D2E58
		public string AccessToken { get; }

		// Token: 0x06001E1B RID: 7707 RVA: 0x000D3A6C File Offset: 0x000D2E6C
		public SqlAuthenticationToken(string accessToken, DateTimeOffset expiresOn)
		{
			if (string.IsNullOrEmpty(accessToken))
			{
				throw SQL.ParameterCannotBeEmpty("AccessToken");
			}
			this.AccessToken = accessToken;
			this.ExpiresOn = expiresOn;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x000D3AA0 File Offset: 0x000D2EA0
		internal SqlAuthenticationToken(byte[] accessToken, DateTimeOffset expiresOn) : this(SqlAuthenticationToken.AccessTokenStringFromBytes(accessToken), expiresOn)
		{
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000D3ABC File Offset: 0x000D2EBC
		internal SqlFedAuthToken ToSqlFedAuthToken()
		{
			byte[] array = SqlAuthenticationToken.AccessTokenBytesFromString(this.AccessToken);
			return new SqlFedAuthToken
			{
				accessToken = array,
				dataLen = (uint)array.Length,
				expirationFileTime = this.ExpiresOn.ToFileTime()
			};
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000D3B00 File Offset: 0x000D2F00
		internal static string AccessTokenStringFromBytes(byte[] bytes)
		{
			return Encoding.Unicode.GetString(bytes);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000D3B18 File Offset: 0x000D2F18
		internal static byte[] AccessTokenBytesFromString(string token)
		{
			return Encoding.Unicode.GetBytes(token);
		}
	}
}
