using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009BD RID: 2493
	internal abstract class ReceivedMessage : Message
	{
		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x060061F5 RID: 25077 RVA: 0x0016CAAE File Offset: 0x0016ACAE
		public override bool IsEmpty
		{
			get
			{
				return this.isEmpty;
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x060061F6 RID: 25078 RVA: 0x0016CAB6 File Offset: 0x0016ACB6
		public override bool IsFault
		{
			get
			{
				return this.isFault;
			}
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x0016CABE File Offset: 0x0016ACBE
		protected static bool HasHeaderElement(XmlDictionaryReader reader, EnvelopeVersion envelopeVersion)
		{
			return reader.IsStartElement(XD.MessageDictionary.Header, envelopeVersion.DictionaryNamespace);
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x0016CAD8 File Offset: 0x0016ACD8
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			if (!this.isEmpty)
			{
				using (XmlDictionaryReader xmlDictionaryReader = this.OnGetReaderAtBodyContents())
				{
					if (xmlDictionaryReader.ReadState == ReadState.Error || xmlDictionaryReader.ReadState == ReadState.Closed)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MessageBodyReaderInvalidReadState", new object[]
						{
							xmlDictionaryReader.ReadState.ToString()
						})));
					}
					while (xmlDictionaryReader.NodeType != XmlNodeType.EndElement && !xmlDictionaryReader.EOF)
					{
						writer.WriteNode(xmlDictionaryReader, false);
					}
					base.ReadFromBodyContentsToEnd(xmlDictionaryReader);
				}
			}
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x0016CB7C File Offset: 0x0016AD7C
		protected bool ReadStartBody(XmlDictionaryReader reader)
		{
			return Message.ReadStartBody(reader, this.Version.Envelope, out this.isFault, out this.isEmpty);
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x0016CB9C File Offset: 0x0016AD9C
		protected static EnvelopeVersion ReadStartEnvelope(XmlDictionaryReader reader)
		{
			EnvelopeVersion result;
			if (reader.IsStartElement(XD.MessageDictionary.Envelope, XD.Message12Dictionary.Namespace))
			{
				result = EnvelopeVersion.Soap12;
			}
			else
			{
				if (!reader.IsStartElement(XD.MessageDictionary.Envelope, XD.Message11Dictionary.Namespace))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("MessageVersionUnknown")));
				}
				result = EnvelopeVersion.Soap11;
			}
			if (reader.IsEmptyElement)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("MessageBodyMissing")));
			}
			reader.Read();
			return result;
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x0016CC35 File Offset: 0x0016AE35
		protected static void VerifyStartBody(XmlDictionaryReader reader, EnvelopeVersion version)
		{
			if (!reader.IsStartElement(XD.MessageDictionary.Body, version.DictionaryNamespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("MessageBodyMissing")));
			}
		}

		// Token: 0x040038E4 RID: 14564
		private bool isFault;

		// Token: 0x040038E5 RID: 14565
		private bool isEmpty;
	}
}
