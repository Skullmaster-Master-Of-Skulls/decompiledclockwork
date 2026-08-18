using System;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
	// Token: 0x020001DF RID: 479
	internal class ActiveDirectoryNativeAuthenticationProvider : SqlAuthenticationProvider
	{
		// Token: 0x06001E14 RID: 7700 RVA: 0x000D3964 File Offset: 0x000D2D64
		public override Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
		{
			return Task.Run<SqlAuthenticationToken>(delegate()
			{
				long fileTime = 0L;
				byte[] accessToken;
				if (parameters.AuthenticationMethod == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
				{
					accessToken = ADALNativeWrapper.ADALGetAccessTokenForWindowsIntegrated(parameters.Authority, parameters.Resource, parameters.ConnectionId, "4d079b4c-cab7-4b7c-a115-8fd51b6f8239", ref fileTime);
					return new SqlAuthenticationToken(accessToken, DateTimeOffset.FromFileTime(fileTime));
				}
				accessToken = ADALNativeWrapper.ADALGetAccessToken(parameters.UserId, parameters.Password, parameters.Authority, parameters.Resource, parameters.ConnectionId, "4d079b4c-cab7-4b7c-a115-8fd51b6f8239", ref fileTime);
				return new SqlAuthenticationToken(accessToken, DateTimeOffset.FromFileTime(fileTime));
			});
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000D3990 File Offset: 0x000D2D90
		public override bool IsSupported(SqlAuthenticationMethod authentication)
		{
			return authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated || authentication == SqlAuthenticationMethod.ActiveDirectoryPassword;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000D39A8 File Offset: 0x000D2DA8
		public override void BeforeLoad(SqlAuthenticationMethod authentication)
		{
			this._logger.LogInfo(this._type, "BeforeLoad", string.Format("being loaded into SqlAuthProviders for {0}.", authentication));
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x000D39DC File Offset: 0x000D2DDC
		public override void BeforeUnload(SqlAuthenticationMethod authentication)
		{
			this._logger.LogInfo(this._type, "BeforeUnload", string.Format("being unloaded from SqlAuthProviders for {0}.", authentication));
		}

		// Token: 0x0400112B RID: 4395
		private readonly string _type = typeof(ActiveDirectoryNativeAuthenticationProvider).Name;

		// Token: 0x0400112C RID: 4396
		private readonly SqlClientLogger _logger = new SqlClientLogger();
	}
}
