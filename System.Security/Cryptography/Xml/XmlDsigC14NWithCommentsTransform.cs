using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000AE RID: 174
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigC14NWithCommentsTransform : XmlDsigC14NTransform
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x00014533 File Offset: 0x00013533
		public XmlDsigC14NWithCommentsTransform() : base(true)
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";
		}
	}
}
