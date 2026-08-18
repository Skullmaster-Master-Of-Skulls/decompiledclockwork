using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009CD RID: 2509
	internal abstract class DictionaryHeader : MessageHeader
	{
		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x060062AF RID: 25263 RVA: 0x0016F7C4 File Offset: 0x0016D9C4
		public override string Name
		{
			get
			{
				return this.DictionaryName.Value;
			}
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x060062B0 RID: 25264 RVA: 0x0016F7D1 File Offset: 0x0016D9D1
		public override string Namespace
		{
			get
			{
				return this.DictionaryNamespace.Value;
			}
		}

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x060062B1 RID: 25265
		public abstract XmlDictionaryString DictionaryName { get; }

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x060062B2 RID: 25266
		public abstract XmlDictionaryString DictionaryNamespace { get; }

		// Token: 0x060062B3 RID: 25267 RVA: 0x0016F7DE File Offset: 0x0016D9DE
		protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteStartElement(this.DictionaryName, this.DictionaryNamespace);
			base.WriteHeaderAttributes(writer, messageVersion);
		}
	}
}
