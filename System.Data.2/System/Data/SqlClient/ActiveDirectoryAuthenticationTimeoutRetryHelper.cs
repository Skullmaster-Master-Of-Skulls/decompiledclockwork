using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001DB RID: 475
	internal class ActiveDirectoryAuthenticationTimeoutRetryHelper
	{
		// Token: 0x06001E01 RID: 7681 RVA: 0x000D3178 File Offset: 0x000D2578
		public ActiveDirectoryAuthenticationTimeoutRetryHelper()
		{
			this._typeName = base.GetType().Name;
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000D31A8 File Offset: 0x000D25A8
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x000D31BC File Offset: 0x000D25BC
		public ActiveDirectoryAuthenticationTimeoutRetryState State
		{
			get
			{
				return this._state;
			}
			set
			{
				switch (this._state)
				{
				case ActiveDirectoryAuthenticationTimeoutRetryState.NotStarted:
					if (value != ActiveDirectoryAuthenticationTimeoutRetryState.Retrying && value != ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn)
					{
						throw new InvalidOperationException(string.Format("Cannot transit from {0} to {1}.", this._state, value));
					}
					break;
				case ActiveDirectoryAuthenticationTimeoutRetryState.Retrying:
					if (value != ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn)
					{
						throw new InvalidOperationException(string.Format("Cannot transit from {0} to {1}.", this._state, value));
					}
					break;
				case ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn:
					throw new InvalidOperationException(string.Format("Cannot transit from {0} to {1}.", this._state, value));
				default:
					throw new InvalidOperationException(string.Format("Unsupported state: {0}.", value));
				}
				this._sqlAuthLogger.LogInfo(this._typeName, "SetState", string.Format("State changed from {0} to {1}.", this._state, value));
				this._state = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x000D32A8 File Offset: 0x000D26A8
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x000D32F8 File Offset: 0x000D26F8
		public SqlFedAuthToken CachedToken
		{
			get
			{
				if (this._sqlAuthLogger.IsLoggingEnabled)
				{
					this._sqlAuthLogger.LogInfo(this._typeName, "GetCachedToken", "Retrieved cached token " + ActiveDirectoryAuthenticationTimeoutRetryHelper.GetTokenHash(this._token) + ".");
				}
				return this._token;
			}
			set
			{
				if (this._sqlAuthLogger.IsLoggingEnabled)
				{
					this._sqlAuthLogger.LogInfo(this._typeName, "SetCachedToken", string.Concat(new string[]
					{
						"CachedToken changed from ",
						ActiveDirectoryAuthenticationTimeoutRetryHelper.GetTokenHash(this._token),
						" to ",
						ActiveDirectoryAuthenticationTimeoutRetryHelper.GetTokenHash(value),
						"."
					}));
				}
				this._token = value;
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x000D336C File Offset: 0x000D276C
		public bool CanRetryWithSqlException(SqlException sqlex)
		{
			string method = "CheckCanRetry";
			if (this._sqlAuthLogger.LogAssert(this._state == ActiveDirectoryAuthenticationTimeoutRetryState.NotStarted, this._typeName, method, string.Format("Cannot retry due to state == {0}.", this._state)) && this._sqlAuthLogger.LogAssert(this.CachedToken != null, this._typeName, method, "Cannot retry when cached token is null.") && this._sqlAuthLogger.LogAssert(ActiveDirectoryAuthenticationTimeoutRetryHelper.IsConnectTimeoutError(sqlex), this._typeName, method, "Cannot retry when exception is not timeout."))
			{
				this._sqlAuthLogger.LogInfo(this._typeName, method, "All checks passed.");
				return true;
			}
			return false;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x000D340C File Offset: 0x000D280C
		private static bool IsConnectTimeoutError(SqlException sqlex)
		{
			Win32Exception ex = sqlex.InnerException as Win32Exception;
			return ex != null && (ex.NativeErrorCode == 10054 || ex.NativeErrorCode == 258);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000D3448 File Offset: 0x000D2848
		private static string GetTokenHash(SqlFedAuthToken token)
		{
			if (token == null)
			{
				return "null";
			}
			string s = SqlAuthenticationToken.AccessTokenStringFromBytes(token.accessToken);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			string result;
			using (SHA256 sha = SHA256.Create())
			{
				byte[] inArray = sha.ComputeHash(bytes);
				result = Convert.ToBase64String(inArray);
			}
			return result;
		}

		// Token: 0x0400111D RID: 4381
		private ActiveDirectoryAuthenticationTimeoutRetryState _state;

		// Token: 0x0400111E RID: 4382
		private SqlFedAuthToken _token;

		// Token: 0x0400111F RID: 4383
		private readonly string _typeName;

		// Token: 0x04001120 RID: 4384
		private readonly SqlClientLogger _sqlAuthLogger = new SqlClientLogger();
	}
}
