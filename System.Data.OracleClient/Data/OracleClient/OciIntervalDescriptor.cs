using System;

namespace System.Data.OracleClient
{
	// Token: 0x0200003D RID: 61
	internal sealed class OciIntervalDescriptor : OciHandle
	{
		// Token: 0x06000204 RID: 516 RVA: 0x0005C774 File Offset: 0x0005BB74
		internal OciIntervalDescriptor(OciHandle parent) : base(parent, OCI.HTYPE.OCI_DTYPE_INTERVAL_DS)
		{
		}
	}
}
