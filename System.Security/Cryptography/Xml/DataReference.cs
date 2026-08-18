using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C1 RID: 193
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class DataReference : EncryptedReference
	{
		// Token: 0x060004A3 RID: 1187 RVA: 0x000175D6 File Offset: 0x000165D6
		public DataReference()
		{
			base.ReferenceType = "DataReference";
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000175E9 File Offset: 0x000165E9
		public DataReference(string uri) : base(uri)
		{
			base.ReferenceType = "DataReference";
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000175FD File Offset: 0x000165FD
		public DataReference(string uri, TransformChain transformChain) : base(uri, transformChain)
		{
			base.ReferenceType = "DataReference";
		}
	}
}
