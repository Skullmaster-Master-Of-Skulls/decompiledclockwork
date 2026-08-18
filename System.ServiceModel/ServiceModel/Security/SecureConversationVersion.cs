using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000283 RID: 643
	[__DynamicallyInvokable]
	public abstract class SecureConversationVersion
	{
		// Token: 0x0600127B RID: 4731 RVA: 0x00043C74 File Offset: 0x00041E74
		internal SecureConversationVersion(XmlDictionaryString ns, XmlDictionaryString prefix)
		{
			this.scNamespace = ns;
			this.prefix = prefix;
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x00043C8A File Offset: 0x00041E8A
		[__DynamicallyInvokable]
		public XmlDictionaryString Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.scNamespace;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00043C92 File Offset: 0x00041E92
		[__DynamicallyInvokable]
		public XmlDictionaryString Prefix
		{
			[__DynamicallyInvokable]
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x00043C9A File Offset: 0x00041E9A
		[__DynamicallyInvokable]
		public static SecureConversationVersion Default
		{
			[__DynamicallyInvokable]
			get
			{
				return SecureConversationVersion.WSSecureConversationFeb2005;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00043CA1 File Offset: 0x00041EA1
		[__DynamicallyInvokable]
		public static SecureConversationVersion WSSecureConversationFeb2005
		{
			[__DynamicallyInvokable]
			get
			{
				return SecureConversationVersion.WSSecureConversationVersionFeb2005.Instance;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00043CA8 File Offset: 0x00041EA8
		public static SecureConversationVersion WSSecureConversation13
		{
			get
			{
				return SecureConversationVersion.WSSecureConversationVersion13.Instance;
			}
		}

		// Token: 0x040019F4 RID: 6644
		private readonly XmlDictionaryString scNamespace;

		// Token: 0x040019F5 RID: 6645
		private readonly XmlDictionaryString prefix;

		// Token: 0x02000B1E RID: 2846
		private class WSSecureConversationVersionFeb2005 : SecureConversationVersion
		{
			// Token: 0x06006FA4 RID: 28580 RVA: 0x0019E498 File Offset: 0x0019C698
			protected WSSecureConversationVersionFeb2005() : base(XD.SecureConversationFeb2005Dictionary.Namespace, XD.SecureConversationFeb2005Dictionary.Prefix)
			{
			}

			// Token: 0x17001A06 RID: 6662
			// (get) Token: 0x06006FA5 RID: 28581 RVA: 0x0019E4B4 File Offset: 0x0019C6B4
			public static SecureConversationVersion Instance
			{
				get
				{
					return SecureConversationVersion.WSSecureConversationVersionFeb2005.instance;
				}
			}

			// Token: 0x04003FDC RID: 16348
			private static readonly SecureConversationVersion.WSSecureConversationVersionFeb2005 instance = new SecureConversationVersion.WSSecureConversationVersionFeb2005();
		}

		// Token: 0x02000B1F RID: 2847
		private class WSSecureConversationVersion13 : SecureConversationVersion
		{
			// Token: 0x06006FA7 RID: 28583 RVA: 0x0019E4C7 File Offset: 0x0019C6C7
			protected WSSecureConversationVersion13() : base(DXD.SecureConversationDec2005Dictionary.Namespace, DXD.SecureConversationDec2005Dictionary.Prefix)
			{
			}

			// Token: 0x17001A07 RID: 6663
			// (get) Token: 0x06006FA8 RID: 28584 RVA: 0x0019E4E3 File Offset: 0x0019C6E3
			public static SecureConversationVersion Instance
			{
				get
				{
					return SecureConversationVersion.WSSecureConversationVersion13.instance;
				}
			}

			// Token: 0x04003FDD RID: 16349
			private static readonly SecureConversationVersion.WSSecureConversationVersion13 instance = new SecureConversationVersion.WSSecureConversationVersion13();
		}
	}
}
