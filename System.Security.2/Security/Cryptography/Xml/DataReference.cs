using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003A RID: 58
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class DataReference : EncryptedReference
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00007EE6 File Offset: 0x000060E6
		public DataReference()
		{
			base.ReferenceType = "DataReference";
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007EF9 File Offset: 0x000060F9
		public DataReference(string uri) : base(uri)
		{
			base.ReferenceType = "DataReference";
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007F0D File Offset: 0x0000610D
		public DataReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "DataReference";
		}
	}
}
