using System;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000045 RID: 69
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class KeyInfoClause
	{
		// Token: 0x06000232 RID: 562
		public abstract XmlElement GetXml();

		// Token: 0x06000233 RID: 563 RVA: 0x0000A1CC File Offset: 0x000083CC
		internal virtual XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xml = this.GetXml();
			return (XmlElement)xmlDocument.ImportNode(xml, true);
		}

		// Token: 0x06000234 RID: 564
		public abstract void LoadXml(XmlElement element);
	}
}
