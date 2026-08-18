using System;
using System.Net;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000793 RID: 1939
	internal static class HttpTransportDefaults
	{
		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x0010EC9D File Offset: 0x0010CE9D
		internal static TimeSpan RequestInitializationTimeout
		{
			get
			{
				return TimeSpanHelper.FromMilliseconds(0, "00:00:00");
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0010ECAA File Offset: 0x0010CEAA
		internal static int GetEffectiveMaxPendingAccepts(int maxPendingAccepts)
		{
			if (maxPendingAccepts != 0)
			{
				return maxPendingAccepts;
			}
			return 10;
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x0010ECB3 File Offset: 0x0010CEB3
		internal static WebSocketTransportSettings GetDefaultWebSocketTransportSettings()
		{
			return new WebSocketTransportSettings();
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x0010ECBA File Offset: 0x0010CEBA
		internal static MessageEncoderFactory GetDefaultMessageEncoderFactory()
		{
			return new TextMessageEncoderFactory(MessageVersion.Default, TextEncoderDefaults.Encoding, 64, 16, EncoderDefaults.ReaderQuotas);
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x0010ECD4 File Offset: 0x0010CED4
		internal static SecurityAlgorithmSuite MessageSecurityAlgorithmSuite
		{
			get
			{
				return SecurityAlgorithmSuite.Default;
			}
		}

		// Token: 0x04002E94 RID: 11924
		internal const bool AllowCookies = false;

		// Token: 0x04002E95 RID: 11925
		internal const AuthenticationSchemes AuthenticationScheme = AuthenticationSchemes.Anonymous;

		// Token: 0x04002E96 RID: 11926
		internal const bool BypassProxyOnLocal = false;

		// Token: 0x04002E97 RID: 11927
		internal const bool DecompressionEnabled = true;

		// Token: 0x04002E98 RID: 11928
		internal const HostNameComparisonMode HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;

		// Token: 0x04002E99 RID: 11929
		internal const bool KeepAliveEnabled = true;

		// Token: 0x04002E9A RID: 11930
		internal const Uri ProxyAddress = null;

		// Token: 0x04002E9B RID: 11931
		internal const AuthenticationSchemes ProxyAuthenticationScheme = AuthenticationSchemes.Anonymous;

		// Token: 0x04002E9C RID: 11932
		internal const string Realm = "";

		// Token: 0x04002E9D RID: 11933
		internal const TransferMode TransferMode = TransferMode.Buffered;

		// Token: 0x04002E9E RID: 11934
		internal const bool UnsafeConnectionNtlmAuthentication = false;

		// Token: 0x04002E9F RID: 11935
		internal const bool UseDefaultWebProxy = true;

		// Token: 0x04002EA0 RID: 11936
		internal const string UpgradeHeader = "Upgrade";

		// Token: 0x04002EA1 RID: 11937
		internal const string ConnectionHeader = "Connection";

		// Token: 0x04002EA2 RID: 11938
		internal const HttpMessageHandlerFactory MessageHandlerFactory = null;

		// Token: 0x04002EA3 RID: 11939
		internal const string RequestInitializationTimeoutString = "00:00:00";

		// Token: 0x04002EA4 RID: 11940
		private const int PendingAcceptsConstant = 10;

		// Token: 0x04002EA5 RID: 11941
		internal const int DefaultMaxPendingAccepts = 0;

		// Token: 0x04002EA6 RID: 11942
		internal const int MaxPendingAcceptsUpperLimit = 100000;
	}
}
