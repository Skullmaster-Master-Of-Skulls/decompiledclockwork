using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005A RID: 90
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigExcC14NWithCommentsTransform : XmlDsigExcC14NTransform
	{
		// Token: 0x0600035C RID: 860 RVA: 0x00010108 File Offset: 0x0000E308
		public XmlDsigExcC14NWithCommentsTransform() : base(true)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001011C File Offset: 0x0000E31C
		public XmlDsigExcC14NWithCommentsTransform(string inclusiveNamespacesPrefixList) : base(true, inclusiveNamespacesPrefixList)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}
	}
}
