using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200096A RID: 2410
	internal class WsrmSequenceFaultHeader : WsrmMessageHeader
	{
		// Token: 0x06005D70 RID: 23920 RVA: 0x00159659 File Offset: 0x00157859
		public WsrmSequenceFaultHeader(ReliableMessagingVersion reliableMessagingVersion, WsrmFault fault) : base(reliableMessagingVersion)
		{
			this.fault = fault;
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x00159669 File Offset: 0x00157869
		public WsrmFault Fault
		{
			get
			{
				return this.fault;
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06005D72 RID: 23922 RVA: 0x00159671 File Offset: 0x00157871
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.WsrmFeb2005Dictionary.SequenceFault;
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x0015967D File Offset: 0x0015787D
		public string Subcode
		{
			get
			{
				return this.fault.Subcode;
			}
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x0015968A File Offset: 0x0015788A
		public static XmlDictionaryReader GetReaderAtDetailContents(string detailName, string detailNamespace, XmlDictionaryReader headerReader, ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return WsrmSequenceFaultHeader.GetReaderAtDetailContentsFeb2005(detailName, detailNamespace, headerReader);
			}
			return WsrmSequenceFaultHeader.GetReaderAtDetailContents11(detailName, detailNamespace, headerReader);
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x001596A8 File Offset: 0x001578A8
		public static XmlDictionaryReader GetReaderAtDetailContents11(string detailName, string detailNamespace, XmlDictionaryReader headerReader)
		{
			XmlDictionaryString @namespace = DXD.Wsrm11Dictionary.Namespace;
			headerReader.ReadFullStartElement(XD.WsrmFeb2005Dictionary.SequenceFault, @namespace);
			headerReader.Skip();
			headerReader.ReadFullStartElement(XD.Message12Dictionary.FaultDetail, @namespace);
			if (headerReader.NodeType != XmlNodeType.Element || headerReader.NamespaceURI != detailNamespace || headerReader.LocalName != detailName)
			{
				headerReader.Close();
				return null;
			}
			return headerReader;
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x00159718 File Offset: 0x00157918
		public static XmlDictionaryReader GetReaderAtDetailContentsFeb2005(string detailName, string detailNamespace, XmlDictionaryReader headerReader)
		{
			XmlDictionaryReader result;
			try
			{
				WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
				XmlDictionaryString @namespace = wsrmFeb2005Dictionary.Namespace;
				XmlBuffer xmlBuffer = null;
				int sectionIndex = 0;
				int depth = headerReader.Depth;
				headerReader.ReadFullStartElement(wsrmFeb2005Dictionary.SequenceFault, @namespace);
				while (headerReader.Depth > depth)
				{
					if (headerReader.NodeType == XmlNodeType.Element && headerReader.NamespaceURI == detailNamespace && headerReader.LocalName == detailName)
					{
						if (xmlBuffer != null)
						{
							return null;
						}
						xmlBuffer = new XmlBuffer(int.MaxValue);
						try
						{
							sectionIndex = xmlBuffer.SectionCount;
							XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(headerReader.Quotas);
							xmlDictionaryWriter.WriteNode(headerReader, false);
							continue;
						}
						finally
						{
							xmlBuffer.CloseSection();
						}
					}
					if (headerReader.Depth == depth)
					{
						break;
					}
					headerReader.Read();
				}
				if (xmlBuffer == null)
				{
					result = null;
				}
				else
				{
					xmlBuffer.Close();
					XmlDictionaryReader reader = xmlBuffer.GetReader(sectionIndex);
					result = reader;
				}
			}
			finally
			{
				headerReader.Close();
			}
			return result;
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x0015980C File Offset: 0x00157A0C
		public static string GetSubcode(XmlDictionaryReader headerReader, ReliableMessagingVersion reliableMessagingVersion)
		{
			string result = null;
			try
			{
				WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
				XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
				headerReader.ReadStartElement(wsrmFeb2005Dictionary.SequenceFault, @namespace);
				headerReader.ReadStartElement(wsrmFeb2005Dictionary.FaultCode, @namespace);
				string a;
				XmlUtil.ReadContentAsQName(headerReader, out result, out a);
				if (a != WsrmIndex.GetNamespaceString(reliableMessagingVersion))
				{
					result = null;
				}
				headerReader.ReadEndElement();
				while (headerReader.IsStartElement())
				{
					headerReader.Skip();
				}
				headerReader.ReadEndElement();
			}
			finally
			{
				headerReader.Close();
			}
			return result;
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x00159894 File Offset: 0x00157A94
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			writer.WriteStartElement("r", "FaultCode", this.Namespace);
			writer.WriteXmlnsAttribute(null, this.Namespace);
			writer.WriteQualifiedName(this.Subcode, this.Namespace);
			writer.WriteEndElement();
			bool flag = base.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			if (flag)
			{
				writer.WriteStartElement("r", XD.Message12Dictionary.FaultDetail, this.DictionaryNamespace);
			}
			this.fault.WriteDetail(writer);
			if (flag)
			{
				writer.WriteEndElement();
			}
		}

		// Token: 0x0400378A RID: 14218
		private WsrmFault fault;
	}
}
