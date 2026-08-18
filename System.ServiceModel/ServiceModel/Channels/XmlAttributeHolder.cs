using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009C1 RID: 2497
	internal struct XmlAttributeHolder
	{
		// Token: 0x06006224 RID: 25124 RVA: 0x0016D6C4 File Offset: 0x0016B8C4
		public XmlAttributeHolder(string prefix, string localName, string ns, string value)
		{
			this.prefix = prefix;
			this.localName = localName;
			this.ns = ns;
			this.value = value;
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06006225 RID: 25125 RVA: 0x0016D6E3 File Offset: 0x0016B8E3
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x0016D6EB File Offset: 0x0016B8EB
		public string NamespaceUri
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06006227 RID: 25127 RVA: 0x0016D6F3 File Offset: 0x0016B8F3
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06006228 RID: 25128 RVA: 0x0016D6FB File Offset: 0x0016B8FB
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06006229 RID: 25129 RVA: 0x0016D703 File Offset: 0x0016B903
		public void WriteTo(XmlWriter writer)
		{
			writer.WriteStartAttribute(this.prefix, this.localName, this.ns);
			writer.WriteString(this.value);
			writer.WriteEndAttribute();
		}

		// Token: 0x0600622A RID: 25130 RVA: 0x0016D730 File Offset: 0x0016B930
		public static void WriteAttributes(XmlAttributeHolder[] attributes, XmlWriter writer)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				attributes[i].WriteTo(writer);
			}
		}

		// Token: 0x0600622B RID: 25131 RVA: 0x0016D758 File Offset: 0x0016B958
		public static XmlAttributeHolder[] ReadAttributes(XmlDictionaryReader reader)
		{
			int maxValue = int.MaxValue;
			return XmlAttributeHolder.ReadAttributes(reader, ref maxValue);
		}

		// Token: 0x0600622C RID: 25132 RVA: 0x0016D774 File Offset: 0x0016B974
		public static XmlAttributeHolder[] ReadAttributes(XmlDictionaryReader reader, ref int maxSizeOfHeaders)
		{
			if (reader.AttributeCount == 0)
			{
				return XmlAttributeHolder.emptyArray;
			}
			XmlAttributeHolder[] array = new XmlAttributeHolder[reader.AttributeCount];
			reader.MoveToFirstAttribute();
			for (int i = 0; i < array.Length; i++)
			{
				string namespaceURI = reader.NamespaceURI;
				string s = reader.LocalName;
				string s2 = reader.Prefix;
				string text = string.Empty;
				while (reader.ReadAttributeValue())
				{
					if (text.Length == 0)
					{
						text = reader.Value;
					}
					else
					{
						text += reader.Value;
					}
				}
				XmlAttributeHolder.Deduct(s2, ref maxSizeOfHeaders);
				XmlAttributeHolder.Deduct(s, ref maxSizeOfHeaders);
				XmlAttributeHolder.Deduct(namespaceURI, ref maxSizeOfHeaders);
				XmlAttributeHolder.Deduct(text, ref maxSizeOfHeaders);
				array[i] = new XmlAttributeHolder(s2, s, namespaceURI, text);
				reader.MoveToNextAttribute();
			}
			reader.MoveToElement();
			return array;
		}

		// Token: 0x0600622D RID: 25133 RVA: 0x0016D840 File Offset: 0x0016BA40
		private static void Deduct(string s, ref int maxSizeOfHeaders)
		{
			int num = s.Length * 2;
			if (num > maxSizeOfHeaders)
			{
				string @string = SR.GetString("XmlBufferQuotaExceeded");
				Exception innerException = new QuotaExceededException(@string);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(@string, innerException));
			}
			maxSizeOfHeaders -= num;
		}

		// Token: 0x0600622E RID: 25134 RVA: 0x0016D888 File Offset: 0x0016BA88
		public static string GetAttribute(XmlAttributeHolder[] attributes, string localName, string ns)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].LocalName == localName && attributes[i].NamespaceUri == ns)
				{
					return attributes[i].Value;
				}
			}
			return null;
		}

		// Token: 0x040038F6 RID: 14582
		private string prefix;

		// Token: 0x040038F7 RID: 14583
		private string ns;

		// Token: 0x040038F8 RID: 14584
		private string localName;

		// Token: 0x040038F9 RID: 14585
		private string value;

		// Token: 0x040038FA RID: 14586
		public static XmlAttributeHolder[] emptyArray = new XmlAttributeHolder[0];
	}
}
