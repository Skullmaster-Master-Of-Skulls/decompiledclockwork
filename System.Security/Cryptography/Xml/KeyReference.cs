using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C2 RID: 194
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class KeyReference : EncryptedReference
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x00017612 File Offset: 0x00016612
		public KeyReference()
		{
			base.ReferenceType = "KeyReference";
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00017625 File Offset: 0x00016625
		public KeyReference(string uri) : base(uri)
		{
			base.ReferenceType = "KeyReference";
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017639 File Offset: 0x00016639
		public KeyReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "KeyReference";
		}
	}
}
