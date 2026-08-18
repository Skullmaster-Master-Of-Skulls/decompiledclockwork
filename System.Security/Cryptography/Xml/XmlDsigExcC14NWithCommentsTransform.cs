using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000B0 RID: 176
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigExcC14NWithCommentsTransform : XmlDsigExcC14NTransform
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x000148A7 File Offset: 0x000138A7
		public XmlDsigExcC14NWithCommentsTransform() : base(true)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000148BB File Offset: 0x000138BB
		public XmlDsigExcC14NWithCommentsTransform(string inclusiveNamespacesPrefixList) : base(true, inclusiveNamespacesPrefixList)
		{
			base.Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#WithComments";
		}
	}
}
