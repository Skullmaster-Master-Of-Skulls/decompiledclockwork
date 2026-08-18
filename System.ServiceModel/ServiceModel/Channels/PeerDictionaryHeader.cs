using System;
using System.Globalization;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A0E RID: 2574
	internal class PeerDictionaryHeader : DictionaryHeader
	{
		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x060065DF RID: 26079 RVA: 0x0017BA15 File Offset: 0x00179C15
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x060065E0 RID: 26080 RVA: 0x0017BA1D File Offset: 0x00179C1D
		public override XmlDictionaryString DictionaryNamespace
		{
			get
			{
				return this.nameSpace;
			}
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x0017BA25 File Offset: 0x00179C25
		public PeerDictionaryHeader(XmlDictionaryString name, XmlDictionaryString nameSpace, string value)
		{
			this.name = name;
			this.nameSpace = nameSpace;
			this.value = value;
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x0017BA42 File Offset: 0x00179C42
		public PeerDictionaryHeader(XmlDictionaryString name, XmlDictionaryString nameSpace, XmlDictionaryString value)
		{
			this.name = name;
			this.nameSpace = nameSpace;
			this.value = value.Value;
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x0017BA64 File Offset: 0x00179C64
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteString(this.value);
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x0017BA72 File Offset: 0x00179C72
		internal static PeerDictionaryHeader CreateHopCountHeader(ulong hopcount)
		{
			return new PeerDictionaryHeader(XD.PeerWireStringsDictionary.HopCount, XD.PeerWireStringsDictionary.HopCountNamespace, hopcount.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x0017BA99 File Offset: 0x00179C99
		internal static PeerDictionaryHeader CreateViaHeader(Uri via)
		{
			return new PeerDictionaryHeader(XD.PeerWireStringsDictionary.PeerVia, XD.PeerWireStringsDictionary.Namespace, via.ToString());
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x0017BABA File Offset: 0x00179CBA
		internal static PeerDictionaryHeader CreateFloodRole()
		{
			return new PeerDictionaryHeader(XD.PeerWireStringsDictionary.FloodAction, XD.PeerWireStringsDictionary.Namespace, XD.PeerWireStringsDictionary.Demuxer);
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x0017BADF File Offset: 0x00179CDF
		internal static PeerDictionaryHeader CreateToHeader(Uri to)
		{
			return new PeerDictionaryHeader(XD.PeerWireStringsDictionary.PeerTo, XD.PeerWireStringsDictionary.Namespace, to.ToString());
		}

		// Token: 0x060065E8 RID: 26088 RVA: 0x0017BB00 File Offset: 0x00179D00
		internal static PeerDictionaryHeader CreateMessageIdHeader(UniqueId messageId)
		{
			return new PeerDictionaryHeader(XD.AddressingDictionary.MessageId, XD.PeerWireStringsDictionary.Namespace, messageId.ToString());
		}

		// Token: 0x04003AC3 RID: 15043
		private string value;

		// Token: 0x04003AC4 RID: 15044
		private XmlDictionaryString name;

		// Token: 0x04003AC5 RID: 15045
		private XmlDictionaryString nameSpace;
	}
}
