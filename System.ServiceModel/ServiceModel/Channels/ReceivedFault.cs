using System;
using System.Collections.Generic;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009CB RID: 2507
	internal class ReceivedFault : MessageFault
	{
		// Token: 0x06006285 RID: 25221 RVA: 0x0016ECE8 File Offset: 0x0016CEE8
		private ReceivedFault(FaultCode code, FaultReason reason, string actor, string node, XmlBuffer detail, EnvelopeVersion version)
		{
			this.code = code;
			this.reason = reason;
			this.actor = actor;
			this.node = node;
			this.receivedVersion = version;
			this.hasDetail = this.InferHasDetail(detail);
			this.detail = (this.hasDetail ? detail : null);
		}

		// Token: 0x170017C5 RID: 6085
		// (get) Token: 0x06006286 RID: 25222 RVA: 0x0016ED41 File Offset: 0x0016CF41
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x170017C6 RID: 6086
		// (get) Token: 0x06006287 RID: 25223 RVA: 0x0016ED49 File Offset: 0x0016CF49
		public override FaultCode Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x170017C7 RID: 6087
		// (get) Token: 0x06006288 RID: 25224 RVA: 0x0016ED51 File Offset: 0x0016CF51
		public override bool HasDetail
		{
			get
			{
				return this.hasDetail;
			}
		}

		// Token: 0x170017C8 RID: 6088
		// (get) Token: 0x06006289 RID: 25225 RVA: 0x0016ED59 File Offset: 0x0016CF59
		public override string Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x170017C9 RID: 6089
		// (get) Token: 0x0600628A RID: 25226 RVA: 0x0016ED61 File Offset: 0x0016CF61
		public override FaultReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x0016ED6C File Offset: 0x0016CF6C
		private bool InferHasDetail(XmlBuffer detail)
		{
			bool result = false;
			if (detail != null)
			{
				XmlDictionaryReader reader = detail.GetReader(0);
				if (!reader.IsEmptyElement && reader.Read())
				{
					result = (reader.MoveToContent() != XmlNodeType.EndElement);
				}
				reader.Close();
			}
			return result;
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x0016EDAC File Offset: 0x0016CFAC
		protected override void OnWriteDetail(XmlDictionaryWriter writer, EnvelopeVersion version)
		{
			using (XmlReader reader = this.detail.GetReader(0))
			{
				base.OnWriteStartDetail(writer, version);
				while (reader.MoveToNextAttribute())
				{
					if (this.ShouldWriteDetailAttribute(version, reader.Prefix, reader.LocalName, reader.Value))
					{
						writer.WriteAttributeString(reader.Prefix, reader.LocalName, reader.NamespaceURI, reader.Value);
					}
				}
				reader.MoveToElement();
				reader.Read();
				while (reader.NodeType != XmlNodeType.EndElement)
				{
					writer.WriteNode(reader, false);
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x0600628D RID: 25229 RVA: 0x0016EE58 File Offset: 0x0016D058
		protected override void OnWriteStartDetail(XmlDictionaryWriter writer, EnvelopeVersion version)
		{
			using (XmlReader reader = this.detail.GetReader(0))
			{
				base.OnWriteStartDetail(writer, version);
				while (reader.MoveToNextAttribute())
				{
					if (this.ShouldWriteDetailAttribute(version, reader.Prefix, reader.LocalName, reader.Value))
					{
						writer.WriteAttributeString(reader.Prefix, reader.LocalName, reader.NamespaceURI, reader.Value);
					}
				}
			}
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x0016EEDC File Offset: 0x0016D0DC
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			using (XmlReader reader = this.detail.GetReader(0))
			{
				reader.Read();
				while (reader.NodeType != XmlNodeType.EndElement)
				{
					writer.WriteNode(reader, false);
				}
			}
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x0016EF30 File Offset: 0x0016D130
		protected override XmlDictionaryReader OnGetReaderAtDetailContents()
		{
			XmlDictionaryReader reader = this.detail.GetReader(0);
			reader.Read();
			return reader;
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x0016EF54 File Offset: 0x0016D154
		private bool ShouldWriteDetailAttribute(EnvelopeVersion targetVersion, string prefix, string localName, string attributeValue)
		{
			bool flag = this.receivedVersion == EnvelopeVersion.Soap12 && targetVersion == EnvelopeVersion.Soap11 && string.IsNullOrEmpty(prefix) && localName == "xmlns" && attributeValue == XD.Message12Dictionary.Namespace.Value;
			return !flag;
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x0016EFA9 File Offset: 0x0016D1A9
		public static ReceivedFault CreateFaultNone(XmlDictionaryReader reader, int maxBufferSize)
		{
			return ReceivedFault.CreateFault12Driver(reader, maxBufferSize, EnvelopeVersion.None);
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x0016EFB8 File Offset: 0x0016D1B8
		private static ReceivedFault CreateFault12Driver(XmlDictionaryReader reader, int maxBufferSize, EnvelopeVersion version)
		{
			reader.ReadStartElement(XD.MessageDictionary.Fault, version.DictionaryNamespace);
			reader.ReadStartElement(XD.Message12Dictionary.FaultCode, version.DictionaryNamespace);
			FaultCode faultCode = ReceivedFault.ReadFaultCode12Driver(reader, version);
			reader.ReadEndElement();
			List<FaultReasonText> list = new List<FaultReasonText>();
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("AtLeastOneFaultReasonMustBeSpecified")));
			}
			reader.ReadStartElement(XD.Message12Dictionary.FaultReason, version.DictionaryNamespace);
			while (reader.IsStartElement(XD.Message12Dictionary.FaultText, version.DictionaryNamespace))
			{
				list.Add(ReceivedFault.ReadTranslation12(reader));
			}
			reader.ReadEndElement();
			string text = "";
			string text2 = "";
			if (reader.IsStartElement(XD.Message12Dictionary.FaultNode, version.DictionaryNamespace))
			{
				text2 = reader.ReadElementContentAsString();
			}
			if (reader.IsStartElement(XD.Message12Dictionary.FaultRole, version.DictionaryNamespace))
			{
				text = reader.ReadElementContentAsString();
			}
			XmlBuffer xmlBuffer = null;
			if (reader.IsStartElement(XD.Message12Dictionary.FaultDetail, version.DictionaryNamespace))
			{
				xmlBuffer = new XmlBuffer(maxBufferSize);
				XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(reader.Quotas);
				xmlDictionaryWriter.WriteNode(reader, false);
				xmlBuffer.CloseSection();
				xmlBuffer.Close();
			}
			reader.ReadEndElement();
			FaultReason faultReason = new FaultReason(list);
			return new ReceivedFault(faultCode, faultReason, text, text2, xmlBuffer, version);
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x0016F118 File Offset: 0x0016D318
		private static FaultCode ReadFaultCode12Driver(XmlDictionaryReader reader, EnvelopeVersion version)
		{
			reader.ReadStartElement(XD.Message12Dictionary.FaultValue, version.DictionaryNamespace);
			string name;
			string ns;
			XmlUtil.ReadContentAsQName(reader, out name, out ns);
			reader.ReadEndElement();
			if (reader.IsStartElement(XD.Message12Dictionary.FaultSubcode, version.DictionaryNamespace))
			{
				reader.ReadStartElement();
				FaultCode subCode = ReceivedFault.ReadFaultCode12Driver(reader, version);
				reader.ReadEndElement();
				return new FaultCode(name, ns, subCode);
			}
			return new FaultCode(name, ns);
		}

		// Token: 0x06006294 RID: 25236 RVA: 0x0016F189 File Offset: 0x0016D389
		public static ReceivedFault CreateFault12(XmlDictionaryReader reader, int maxBufferSize)
		{
			return ReceivedFault.CreateFault12Driver(reader, maxBufferSize, EnvelopeVersion.Soap12);
		}

		// Token: 0x06006295 RID: 25237 RVA: 0x0016F198 File Offset: 0x0016D398
		private static FaultReasonText ReadTranslation12(XmlDictionaryReader reader)
		{
			string xmlLangAttribute = XmlUtil.GetXmlLangAttribute(reader);
			string text = reader.ReadElementContentAsString();
			return new FaultReasonText(text, xmlLangAttribute);
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x0016F1BC File Offset: 0x0016D3BC
		public static ReceivedFault CreateFault11(XmlDictionaryReader reader, int maxBufferSize)
		{
			reader.ReadStartElement(XD.MessageDictionary.Fault, XD.Message11Dictionary.Namespace);
			reader.ReadStartElement(XD.Message11Dictionary.FaultCode, XD.Message11Dictionary.FaultNamespace);
			string name;
			string ns;
			XmlUtil.ReadContentAsQName(reader, out name, out ns);
			FaultCode faultCode = new FaultCode(name, ns);
			reader.ReadEndElement();
			string xmlLang = reader.XmlLang;
			reader.MoveToContent();
			string text = reader.ReadElementContentAsString(XD.Message11Dictionary.FaultString.Value, XD.Message11Dictionary.FaultNamespace.Value);
			FaultReasonText translation = new FaultReasonText(text, xmlLang);
			string text2 = "";
			if (reader.IsStartElement(XD.Message11Dictionary.FaultActor, XD.Message11Dictionary.FaultNamespace))
			{
				text2 = reader.ReadElementContentAsString();
			}
			XmlBuffer xmlBuffer = null;
			if (reader.IsStartElement(XD.Message11Dictionary.FaultDetail, XD.Message11Dictionary.FaultNamespace))
			{
				xmlBuffer = new XmlBuffer(maxBufferSize);
				XmlDictionaryWriter xmlDictionaryWriter = xmlBuffer.OpenSection(reader.Quotas);
				xmlDictionaryWriter.WriteNode(reader, false);
				xmlBuffer.CloseSection();
				xmlBuffer.Close();
			}
			reader.ReadEndElement();
			FaultReason faultReason = new FaultReason(translation);
			return new ReceivedFault(faultCode, faultReason, text2, text2, xmlBuffer, EnvelopeVersion.Soap11);
		}

		// Token: 0x04003920 RID: 14624
		private FaultCode code;

		// Token: 0x04003921 RID: 14625
		private FaultReason reason;

		// Token: 0x04003922 RID: 14626
		private string actor;

		// Token: 0x04003923 RID: 14627
		private string node;

		// Token: 0x04003924 RID: 14628
		private XmlBuffer detail;

		// Token: 0x04003925 RID: 14629
		private bool hasDetail;

		// Token: 0x04003926 RID: 14630
		private EnvelopeVersion receivedVersion;
	}
}
