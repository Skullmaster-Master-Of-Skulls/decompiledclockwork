using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200009C RID: 156
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class KeyInfoClause
	{
		// Token: 0x060002F3 RID: 755
		public abstract XmlElement GetXml();

		// Token: 0x060002F4 RID: 756 RVA: 0x0000FE9C File Offset: 0x0000EE9C
		internal virtual XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xml = this.GetXml();
			return (XmlElement)xmlDocument.ImportNode(xml, true);
		}

		// Token: 0x060002F5 RID: 757
		public abstract void LoadXml(XmlElement element);
	}
}
