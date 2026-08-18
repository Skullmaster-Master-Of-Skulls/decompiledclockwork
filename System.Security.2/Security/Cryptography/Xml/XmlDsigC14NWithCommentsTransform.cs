using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000058 RID: 88
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigC14NWithCommentsTransform : XmlDsigC14NTransform
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000FD94 File Offset: 0x0000DF94
		public XmlDsigC14NWithCommentsTransform() : base(true)
		{
			base.Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments";
		}
	}
}
