using System;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000121 RID: 289
	public class GenericXmlSecurityKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x060007EB RID: 2027 RVA: 0x000212C6 File Offset: 0x0001F4C6
		public GenericXmlSecurityKeyIdentifierClause(XmlElement referenceXml) : this(referenceXml, null, 0)
		{
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000212D1 File Offset: 0x0001F4D1
		public GenericXmlSecurityKeyIdentifierClause(XmlElement referenceXml, byte[] derivationNonce, int derivationLength) : base(null, derivationNonce, derivationLength)
		{
			if (referenceXml == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("referenceXml");
			}
			this.referenceXml = referenceXml;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000212F6 File Offset: 0x0001F4F6
		public XmlElement ReferenceXml
		{
			get
			{
				return this.referenceXml;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00021300 File Offset: 0x0001F500
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			GenericXmlSecurityKeyIdentifierClause genericXmlSecurityKeyIdentifierClause = keyIdentifierClause as GenericXmlSecurityKeyIdentifierClause;
			return this == genericXmlSecurityKeyIdentifierClause || (genericXmlSecurityKeyIdentifierClause != null && genericXmlSecurityKeyIdentifierClause.Matches(this.ReferenceXml));
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002132B File Offset: 0x0001F52B
		private bool Matches(XmlElement xmlElement)
		{
			return xmlElement != null && this.CompareNodes(this.referenceXml, xmlElement);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00021340 File Offset: 0x0001F540
		private bool CompareNodes(XmlNode originalNode, XmlNode newNode)
		{
			if (originalNode.OuterXml == newNode.OuterXml)
			{
				return true;
			}
			if (originalNode.LocalName != newNode.LocalName || originalNode.InnerText != newNode.InnerText)
			{
				return false;
			}
			if (originalNode.InnerXml == newNode.InnerXml)
			{
				return true;
			}
			if (!originalNode.HasChildNodes)
			{
				return !newNode.HasChildNodes;
			}
			if (!newNode.HasChildNodes || originalNode.ChildNodes.Count != newNode.ChildNodes.Count)
			{
				return false;
			}
			bool flag = true;
			for (int i = 0; i < originalNode.ChildNodes.Count; i++)
			{
				flag &= this.CompareNodes(originalNode.ChildNodes[i], newNode.ChildNodes[i]);
			}
			return flag;
		}

		// Token: 0x04000AE5 RID: 2789
		private XmlElement referenceXml;
	}
}
