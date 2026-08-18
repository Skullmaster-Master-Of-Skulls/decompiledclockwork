using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000972 RID: 2418
	internal abstract class WsrmMessageHeader : DictionaryHeader, IMessageHeaderWithSharedNamespace
	{
		// Token: 0x06005DC3 RID: 24003 RVA: 0x0015AA80 File Offset: 0x00158C80
		protected WsrmMessageHeader(ReliableMessagingVersion reliableMessagingVersion)
		{
			this.reliableMessagingVersion = reliableMessagingVersion;
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x0015AA8F File Offset: 0x00158C8F
		XmlDictionaryString IMessageHeaderWithSharedNamespace.SharedPrefix
		{
			get
			{
				return XD.WsrmFeb2005Dictionary.Prefix;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x06005DC5 RID: 24005 RVA: 0x0015AA9B File Offset: 0x00158C9B
		XmlDictionaryString IMessageHeaderWithSharedNamespace.SharedNamespace
		{
			get
			{
				return WsrmIndex.GetNamespace(this.reliableMessagingVersion);
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x0015AAA8 File Offset: 0x00158CA8
		public override XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return WsrmIndex.GetNamespace(this.reliableMessagingVersion);
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x0015AAB5 File Offset: 0x00158CB5
		public override string Namespace
		{
			get
			{
				return WsrmIndex.GetNamespaceString(this.reliableMessagingVersion);
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x06005DC8 RID: 24008 RVA: 0x0015AAC2 File Offset: 0x00158CC2
		protected ReliableMessagingVersion ReliableMessagingVersion
		{
			get
			{
				return this.reliableMessagingVersion;
			}
		}

		// Token: 0x040037AC RID: 14252
		private ReliableMessagingVersion reliableMessagingVersion;
	}
}
