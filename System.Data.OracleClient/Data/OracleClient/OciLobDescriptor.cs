using System;

namespace System.Data.OracleClient
{
	// Token: 0x0200003E RID: 62
	internal sealed class OciLobDescriptor : OciHandle
	{
		// Token: 0x06000205 RID: 517 RVA: 0x0005C794 File Offset: 0x0005BB94
		internal OciLobDescriptor(OciHandle parent) : base(parent, OCI.HTYPE.OCI_DTYPE_FIRST)
		{
		}
	}
}
