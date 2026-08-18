using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000873 RID: 2163
	internal class SharedHttpsTransportManager : SharedHttpTransportManager
	{
		// Token: 0x060051DE RID: 20958 RVA: 0x0012D36F File Offset: 0x0012B56F
		public SharedHttpsTransportManager(Uri listenUri, HttpChannelListener factory) : base(listenUri, factory)
		{
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x0012D379 File Offset: 0x0012B579
		internal override string Scheme
		{
			get
			{
				return Uri.UriSchemeHttps;
			}
		}

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x060051E0 RID: 20960 RVA: 0x0012D380 File Offset: 0x0012B580
		internal static UriPrefixTable<ITransportManagerRegistration> StaticTransportManagerTable
		{
			get
			{
				return SharedHttpsTransportManager.transportManagerTable;
			}
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x060051E1 RID: 20961 RVA: 0x0012D387 File Offset: 0x0012B587
		internal override UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return SharedHttpsTransportManager.transportManagerTable;
			}
		}

		// Token: 0x0400322B RID: 12843
		private static UriPrefixTable<ITransportManagerRegistration> transportManagerTable = new UriPrefixTable<ITransportManagerRegistration>(true);
	}
}
