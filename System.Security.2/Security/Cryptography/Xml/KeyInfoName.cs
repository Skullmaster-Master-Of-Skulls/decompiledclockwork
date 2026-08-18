using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000046 RID: 70
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class KeyInfoName : KeyInfoClause
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000A1ED File Offset: 0x000083ED
		public KeyInfoName() : this(null)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A1F6 File Offset: 0x000083F6
		public KeyInfoName(string keyName)
		{
			this.Value = keyName;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000A205 File Offset: 0x00008405
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000A20D File Offset: 0x0000840D
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

		// Token: 0x06000239 RID: 569 RVA: 0x0000A218 File Offset: 0x00008418
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A23C File Offset: 0x0000843C
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyName", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.AppendChild(xmlDocument.CreateTextNode(this.m_keyName));
			return xmlElement;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A270 File Offset: 0x00008470
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_keyName = value.InnerText.Trim();
		}

		// Token: 0x040003EB RID: 1003
		private string m_keyName;
	}
}
