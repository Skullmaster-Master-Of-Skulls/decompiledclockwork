using System;
using System.Diagnostics;
using System.IdentityModel.Diagnostics;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200003B RID: 59
	internal class EncryptionMethodElement
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000091BE File Offset: 0x000073BE
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000091C6 File Offset: 0x000073C6
		public string Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000091CF File Offset: 0x000073CF
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000091D7 File Offset: 0x000073D7
		public string Parameters
		{
			get
			{
				return this._parameters;
			}
			set
			{
				this._parameters = value;
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000091E0 File Offset: 0x000073E0
		public void ReadXml(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (!reader.IsStartElement("EncryptionMethod", "http://www.w3.org/2001/04/xmlenc#"))
			{
				return;
			}
			this._algorithm = reader.GetAttribute("Algorithm", null);
			if (!reader.IsEmptyElement)
			{
				string text = reader.ReadOuterXml();
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					TraceUtility.TraceString(TraceEventType.Warning, SR.GetString("ID8024", new object[]
					{
						reader.Name,
						reader.NamespaceURI,
						text
					}), new object[0]);
					return;
				}
			}
			else
			{
				reader.Read();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000927C File Offset: 0x0000747C
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("xenc", "EncryptionMethod", "http://www.w3.org/2001/04/xmlenc#");
			writer.WriteAttributeString("Algorithm", null, this._algorithm);
			writer.WriteEndElement();
		}

		// Token: 0x0400014D RID: 333
		private string _algorithm;

		// Token: 0x0400014E RID: 334
		private string _parameters;
	}
}
