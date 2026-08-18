using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200009D RID: 157
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoName : KeyInfoClause
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000FEBD File Offset: 0x0000EEBD
		public KeyInfoName() : this(null)
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000FEC6 File Offset: 0x0000EEC6
		public KeyInfoName(string keyName)
		{
			this.Value = keyName;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000FED5 File Offset: 0x0000EED5
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000FEDD File Offset: 0x0000EEDD
		public string Value
		{
			get
			{
				return this.m_keyName;
			}
			set
			{
				this.m_keyName = value;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000FEE8 File Offset: 0x0000EEE8
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000FF0C File Offset: 0x0000EF0C
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyName", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.AppendChild(xmlDocument.CreateTextNode(this.m_keyName));
			return xmlElement;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000FF40 File Offset: 0x0000EF40
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_keyName = value.InnerText.Trim();
		}

		// Token: 0x04000501 RID: 1281
		private string m_keyName;
	}
}
