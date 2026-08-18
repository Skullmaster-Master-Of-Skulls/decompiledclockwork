using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using ClockWorkLogger;
using Microsoft.Exchange.WebServices.Data;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Exchange.Impl.Adapters
{
	// Token: 0x02000009 RID: 9
	public static class SyncOperationContextAdapter
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00006FD4 File Offset: 0x000051D4
		public static ExchangeService GetExchangeService(this SyncOperationContext OpContext)
		{
			bool flag = SyncOperationContextAdapter.exchangeService != null;
			ExchangeService result;
			if (flag)
			{
				result = SyncOperationContextAdapter.exchangeService;
			}
			else
			{
				ExchangeVersion requestedServerVersion = ExchangeVersion.Exchange2007_SP1;
				bool flag2 = Enum.IsDefined(typeof(ExchangeVersion), OpContext.SyncSettings.SyncConnection.ApplicationVersion);
				if (flag2)
				{
					requestedServerVersion = (ExchangeVersion)Enum.Parse(typeof(ExchangeVersion), OpContext.SyncSettings.SyncConnection.ApplicationVersion);
				}
				CWLogger.Logger.Trace("SyncOperationContextAdapter::GetExchangeService: Creating ExchangeService ...");
				SyncOperationContextAdapter.exchangeService = new ExchangeService(requestedServerVersion)
				{
					Credentials = new WebCredentials(OpContext.SyncSettings.SyncConnection.UserCredentials.Username, OpContext.SyncSettings.SyncConnection.UserCredentials.Password),
					Timeout = 1800000
				};
				CWLogger.Logger.Trace("SyncOperationContextAdapter::GetExchangeService: ExchangeService created successfuly");
				bool useAutoDiscoverUrl = OpContext.SyncSettings.SyncConnection.UseAutoDiscoverUrl;
				if (useAutoDiscoverUrl)
				{
					CWLogger.Logger.Trace("SyncOperationContextAdapter::GetExchangeService: Using AutoDiscoverUrl emailaddress= '{0}'", OpContext.SyncSettings.SyncConnection.ApplicationUrl);
					SyncOperationContextAdapter.exchangeService.AutodiscoverUrl(OpContext.SyncSettings.SyncConnection.ApplicationUrl, (string url) => true);
					CWLogger.Logger.Trace("SyncOperationContextAdapter::GetExchangeService: AutoDiscoverUrl Url = '{0}'", (SyncOperationContextAdapter.exchangeService != null) ? SyncOperationContextAdapter.exchangeService.Url.AbsoluteUri : "NULL");
				}
				else
				{
					CWLogger.Logger.Trace("SyncOperationContextAdapter::GetExchangeService: Using Url = '{0}'", OpContext.SyncSettings.SyncConnection.ApplicationUrl);
					SyncOperationContextAdapter.exchangeService.Url = new Uri(OpContext.SyncSettings.SyncConnection.ApplicationUrl);
				}
				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(SyncOperationContextAdapter.CertificateValidationCallBack);
				result = SyncOperationContextAdapter.exchangeService;
			}
			return result;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000071B4 File Offset: 0x000053B4
		private static bool CertificateValidationCallBack(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000071C8 File Offset: 0x000053C8
		private static bool RedirectionUrlValidationCallback(string redirectionUrl)
		{
			CWLogger.Logger.Trace("SyncOperationContextAdapter::RedirectionUrlValidationCallback: RedirectionUrl = '{0}'", redirectionUrl ?? "NULL");
			return !string.IsNullOrEmpty(redirectionUrl);
		}

		// Token: 0x04000015 RID: 21
		private static ExchangeService exchangeService;
	}
}
