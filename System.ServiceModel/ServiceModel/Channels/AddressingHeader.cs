using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009A6 RID: 2470
	internal abstract class AddressingHeader : DictionaryHeader, IMessageHeaderWithSharedNamespace
	{
		// Token: 0x060060DE RID: 24798 RVA: 0x00169F58 File Offset: 0x00168158
		protected AddressingHeader(AddressingVersion version)
		{
			this.version = version;
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x060060DF RID: 24799 RVA: 0x00169F67 File Offset: 0x00168167
		internal AddressingVersion Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x060060E0 RID: 24800 RVA: 0x00169F6F File Offset: 0x0016816F
		XmlDictionaryString IMessageHeaderWithSharedNamespace.SharedPrefix
		{
			get
			{
				return XD.AddressingDictionary.Prefix;
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x060060E1 RID: 24801 RVA: 0x00169F7B File Offset: 0x0016817B
		XmlDictionaryString IMessageHeaderWithSharedNamespace.SharedNamespace
		{
			get
			{
				return this.version.DictionaryNamespace;
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x060060E2 RID: 24802 RVA: 0x00169F88 File Offset: 0x00168188
		public override XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return this.version.DictionaryNamespace;
			}
		}

		// Token: 0x040038AB RID: 14507
		private AddressingVersion version;
	}
}
