using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000E2 RID: 226
	internal struct XmlAttributeHolder
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x000195F4 File Offset: 0x000177F4
		public XmlAttributeHolder(string prefix, string localName, string ns, string value)
		{
			this.prefix = prefix;
			this.localName = localName;
			this.ns = ns;
			this.value = value;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00019613 File Offset: 0x00017813
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001961B File Offset: 0x0001781B
		public string NamespaceUri
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00019623 File Offset: 0x00017823
		public string LocalName
		{
			get
			{
				return this.localName;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001962B File Offset: 0x0001782B
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019633 File Offset: 0x00017833
		public void WriteTo(XmlWriter writer)
		{
			writer.WriteStartAttribute(this.prefix, this.localName, this.ns);
			writer.WriteString(this.value);
			writer.WriteEndAttribute();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00019660 File Offset: 0x00017860
		public static void WriteAttributes(XmlAttributeHolder[] attributes, XmlWriter writer)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				attributes[i].WriteTo(writer);
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00019688 File Offset: 0x00017888
		public static XmlAttributeHolder[] ReadAttributes(XmlDictionaryReader reader)
		{
			int maxValue = int.MaxValue;
			return XmlAttributeHolder.ReadAttributes(reader, ref maxValue);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000196A4 File Offset: 0x000178A4
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

		// Token: 0x06000630 RID: 1584 RVA: 0x00019770 File Offset: 0x00017970
		private static void Deduct(string s, ref int maxSizeOfHeaders)
		{
			int num = s.Length * 2;
			if (num > maxSizeOfHeaders)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("XmlBufferQuotaExceeded")));
			}
			maxSizeOfHeaders -= num;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000197AC File Offset: 0x000179AC
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

		// Token: 0x04000788 RID: 1928
		private string prefix;

		// Token: 0x04000789 RID: 1929
		private string ns;

		// Token: 0x0400078A RID: 1930
		private string localName;

		// Token: 0x0400078B RID: 1931
		private string value;

		// Token: 0x0400078C RID: 1932
		public static XmlAttributeHolder[] emptyArray = new XmlAttributeHolder[0];
	}
}
