using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Data.SqlClient
{
	// Token: 0x020001D9 RID: 473
	public class SqlAuthenticationParameters
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x000D3088 File Offset: 0x000D2488
		public SqlAuthenticationMethod AuthenticationMethod { get; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x000D309C File Offset: 0x000D249C
		public string Resource { get; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000D30B0 File Offset: 0x000D24B0
		public string Authority { get; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000D30C4 File Offset: 0x000D24C4
		public string UserId { get; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000D30D8 File Offset: 0x000D24D8
		public string Password { get; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000D30EC File Offset: 0x000D24EC
		public Guid ConnectionId { get; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x000D3100 File Offset: 0x000D2500
		public string ServerName { get; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000D3114 File Offset: 0x000D2514
		public string DatabaseName { get; }

		// Token: 0x06001E00 RID: 7680 RVA: 0x000D3128 File Offset: 0x000D2528
		protected SqlAuthenticationParameters(SqlAuthenticationMethod authenticationMethod, string serverName, string databaseName, string resource, string authority, string userId, string password, Guid connectionId)
		{
			this.AuthenticationMethod = authenticationMethod;
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.Resource = resource;
			this.Authority = authority;
			this.UserId = userId;
			this.Password = password;
			this.ConnectionId = connectionId;
		}

		// Token: 0x020003CB RID: 971
		internal class Builder
		{
			// Token: 0x0600353D RID: 13629 RVA: 0x00144490 File Offset: 0x00143890
			public static implicit operator SqlAuthenticationParameters(SqlAuthenticationParameters.Builder builder)
			{
				return new SqlAuthenticationParameters(builder._authenticationMethod, builder._serverName, builder._databaseName, builder._resource, builder._authority, builder._userId, builder._password, builder._connectionId);
			}

			// Token: 0x0600353E RID: 13630 RVA: 0x001444D4 File Offset: 0x001438D4
			public SqlAuthenticationParameters.Builder WithUserId(string userId)
			{
				this._userId = userId;
				return this;
			}

			// Token: 0x0600353F RID: 13631 RVA: 0x001444EC File Offset: 0x001438EC
			public SqlAuthenticationParameters.Builder WithPassword(string password)
			{
				this._password = password;
				return this;
			}

			// Token: 0x06003540 RID: 13632 RVA: 0x00144504 File Offset: 0x00143904
			public SqlAuthenticationParameters.Builder WithPassword(SecureString password)
			{
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					intPtr = Marshal.SecureStringToGlobalAllocUnicode(password);
					this._password = Marshal.PtrToStringUni(intPtr);
				}
				finally
				{
					Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
				}
				return this;
			}

			// Token: 0x06003541 RID: 13633 RVA: 0x00144550 File Offset: 0x00143950
			public SqlAuthenticationParameters.Builder WithConnectionId(Guid connectionId)
			{
				this._connectionId = connectionId;
				return this;
			}

			// Token: 0x06003542 RID: 13634 RVA: 0x00144568 File Offset: 0x00143968
			internal Builder(SqlAuthenticationMethod authenticationMethod, string resource, string authority, string serverName, string databaseName)
			{
				this._authenticationMethod = authenticationMethod;
				this._serverName = serverName;
				this._databaseName = databaseName;
				this._resource = resource;
				this._authority = authority;
			}

			// Token: 0x040020E1 RID: 8417
			private readonly SqlAuthenticationMethod _authenticationMethod;

			// Token: 0x040020E2 RID: 8418
			private readonly string _serverName;

			// Token: 0x040020E3 RID: 8419
			private readonly string _databaseName;

			// Token: 0x040020E4 RID: 8420
			private readonly string _resource;

			// Token: 0x040020E5 RID: 8421
			private readonly string _authority;

			// Token: 0x040020E6 RID: 8422
			private string _userId;

			// Token: 0x040020E7 RID: 8423
			private string _password;

			// Token: 0x040020E8 RID: 8424
			private Guid _connectionId = Guid.NewGuid();
		}
	}
}
