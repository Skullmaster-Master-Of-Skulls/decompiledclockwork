using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002FE RID: 766
	internal sealed class BinaryNegotiation
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x00061274 File Offset: 0x0005F474
		public BinaryNegotiation(string valueTypeUri, byte[] negotiationData)
		{
			if (valueTypeUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("valueTypeUri");
			}
			if (negotiationData == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("negotiationData");
			}
			this.valueTypeUriDictionaryString = null;
			this.valueTypeUri = valueTypeUri;
			this.negotiationData = negotiationData;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000612C4 File Offset: 0x0005F4C4
		public BinaryNegotiation(XmlDictionaryString valueTypeDictionaryString, byte[] negotiationData)
		{
			if (valueTypeDictionaryString == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("valueTypeDictionaryString");
			}
			if (negotiationData == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("negotiationData");
			}
			this.valueTypeUriDictionaryString = valueTypeDictionaryString;
			this.valueTypeUri = valueTypeDictionaryString.Value;
			this.negotiationData = negotiationData;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00061318 File Offset: 0x0005F518
		public void Validate(XmlDictionaryString valueTypeUriDictionaryString)
		{
			if (this.valueTypeUri != valueTypeUriDictionaryString.Value)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("IncorrectBinaryNegotiationValueType", new object[]
				{
					this.valueTypeUri
				})));
			}
			this.valueTypeUriDictionaryString = valueTypeUriDictionaryString;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00061368 File Offset: 0x0005F568
		public void WriteTo(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString ns, XmlDictionaryString valueTypeLocalName, XmlDictionaryString valueTypeNs)
		{
			writer.WriteStartElement(prefix, localName, ns);
			writer.WriteStartAttribute(valueTypeLocalName, valueTypeNs);
			if (this.valueTypeUriDictionaryString != null)
			{
				writer.WriteString(this.valueTypeUriDictionaryString);
			}
			else
			{
				writer.WriteString(this.valueTypeUri);
			}
			writer.WriteEndAttribute();
			writer.WriteStartAttribute(XD.SecurityJan2004Dictionary.EncodingType, null);
			writer.WriteString(XD.SecurityJan2004Dictionary.EncodingTypeValueBase64Binary);
			writer.WriteEndAttribute();
			writer.WriteBase64(this.negotiationData, 0, this.negotiationData.Length);
			writer.WriteEndElement();
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x000613F3 File Offset: 0x0005F5F3
		public string ValueTypeUri
		{
			get
			{
				return this.valueTypeUri;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000613FB File Offset: 0x0005F5FB
		public byte[] GetNegotiationData()
		{
			return this.negotiationData;
		}

		// Token: 0x04001CE3 RID: 7395
		private byte[] negotiationData;

		// Token: 0x04001CE4 RID: 7396
		private XmlDictionaryString valueTypeUriDictionaryString;

		// Token: 0x04001CE5 RID: 7397
		private string valueTypeUri;
	}
}
