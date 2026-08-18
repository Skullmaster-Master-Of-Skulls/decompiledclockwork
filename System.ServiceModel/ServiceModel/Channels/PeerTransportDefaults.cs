using System;
using System.Net;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000796 RID: 1942
	internal static class PeerTransportDefaults
	{
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x0010ECF0 File Offset: 0x0010CEF0
		internal static bool ResolverAvailable
		{
			get
			{
				return PnrpPeerResolver.IsPnrpAvailable;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x0010ECF7 File Offset: 0x0010CEF7
		internal static bool ResolverInstalled
		{
			get
			{
				return PnrpPeerResolver.IsPnrpInstalled;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x060049B5 RID: 18869 RVA: 0x0010ECFE File Offset: 0x0010CEFE
		internal static Type ResolverType
		{
			get
			{
				return typeof(PnrpPeerResolver);
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x0010ED0A File Offset: 0x0010CF0A
		internal static Type ResolverBindingElementType
		{
			get
			{
				return typeof(PnrpPeerResolverBindingElement);
			}
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0010ED16 File Offset: 0x0010CF16
		internal static PeerResolver CreateResolver()
		{
			return new PnrpPeerResolver();
		}

		// Token: 0x04002EA9 RID: 11945
		internal const IPAddress ListenIPAddress = null;

		// Token: 0x04002EAA RID: 11946
		internal const int Port = 0;

		// Token: 0x04002EAB RID: 11947
		internal const string ResolverTypeString = null;

		// Token: 0x04002EAC RID: 11948
		internal const PeerAuthenticationMode PeerNodeAuthenticationMode = PeerAuthenticationMode.Password;

		// Token: 0x04002EAD RID: 11949
		internal const bool MessageAuthentication = false;
	}
}
