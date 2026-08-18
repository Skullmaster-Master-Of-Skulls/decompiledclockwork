using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003B RID: 59
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class KeyReference : EncryptedReference
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00007F22 File Offset: 0x00006122
		public KeyReference()
		{
			base.ReferenceType = "KeyReference";
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007F35 File Offset: 0x00006135
		public KeyReference(string uri) : base(uri)
		{
			base.ReferenceType = "KeyReference";
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007F49 File Offset: 0x00006149
		public KeyReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "KeyReference";
		}
	}
}
