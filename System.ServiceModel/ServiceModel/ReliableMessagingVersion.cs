using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x020000B6 RID: 182
	[TypeConverter(typeof(ReliableMessagingVersionConverter))]
	public abstract class ReliableMessagingVersion
	{
		// Token: 0x06000309 RID: 777 RVA: 0x00011E82 File Offset: 0x00010082
		internal ReliableMessagingVersion(string ns, XmlDictionaryString dictionaryNs)
		{
			this.ns = ns;
			this.dictionaryNs = dictionaryNs;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00011E98 File Offset: 0x00010098
		public static ReliableMessagingVersion Default
		{
			get
			{
				return ReliableSessionDefaults.ReliableMessagingVersion;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00011E9F File Offset: 0x0001009F
		public static ReliableMessagingVersion WSReliableMessaging11
		{
			get
			{
				return WSReliableMessaging11Version.Instance;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00011EA6 File Offset: 0x000100A6
		public static ReliableMessagingVersion WSReliableMessagingFebruary2005
		{
			get
			{
				return WSReliableMessagingFebruary2005Version.Instance;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00011EAD File Offset: 0x000100AD
		internal XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return this.dictionaryNs;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00011EB5 File Offset: 0x000100B5
		internal string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00011EBD File Offset: 0x000100BD
		internal static bool IsDefined(ReliableMessagingVersion reliableMessagingVersion)
		{
			return reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 || reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
		}

		// Token: 0x04000961 RID: 2401
		private XmlDictionaryString dictionaryNs;

		// Token: 0x04000962 RID: 2402
		private string ns;
	}
}
