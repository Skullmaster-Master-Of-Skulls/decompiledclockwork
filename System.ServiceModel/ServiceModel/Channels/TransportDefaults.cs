using System;
using System.Security.Authentication;
using System.Security.Principal;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200078F RID: 1935
	internal static class TransportDefaults
	{
		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x0010EC07 File Offset: 0x0010CE07
		internal static SslProtocols SslProtocols
		{
			get
			{
				if (LocalAppContextSwitches.DontEnableSystemDefaultTlsVersions)
				{
					return SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
				}
				return SslProtocols.None;
			}
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x0010EC17 File Offset: 0x0010CE17
		internal static MessageEncoderFactory GetDefaultMessageEncoderFactory()
		{
			return new BinaryMessageEncodingBindingElement().CreateMessageEncoderFactory();
		}

		// Token: 0x04002E6D RID: 11885
		internal const bool ExtractGroupsForWindowsAccounts = true;

		// Token: 0x04002E6E RID: 11886
		internal const HostNameComparisonMode HostNameComparisonMode = HostNameComparisonMode.Exact;

		// Token: 0x04002E6F RID: 11887
		internal const TokenImpersonationLevel ImpersonationLevel = TokenImpersonationLevel.Identification;

		// Token: 0x04002E70 RID: 11888
		internal const bool ManualAddressing = false;

		// Token: 0x04002E71 RID: 11889
		internal const long MaxReceivedMessageSize = 65536L;

		// Token: 0x04002E72 RID: 11890
		internal const int MaxDrainSize = 65536;

		// Token: 0x04002E73 RID: 11891
		internal const long MaxBufferPoolSize = 524288L;

		// Token: 0x04002E74 RID: 11892
		internal const int MaxBufferSize = 65536;

		// Token: 0x04002E75 RID: 11893
		internal const bool RequireClientCertificate = false;

		// Token: 0x04002E76 RID: 11894
		internal const int MaxFaultSize = 65536;

		// Token: 0x04002E77 RID: 11895
		internal const int MaxSecurityFaultSize = 16384;

		// Token: 0x04002E78 RID: 11896
		internal const SslProtocols OldDefaultSslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;

		// Token: 0x04002E79 RID: 11897
		internal const int MaxRMFaultSize = 65536;
	}
}
