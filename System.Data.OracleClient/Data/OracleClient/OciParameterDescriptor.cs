using System;

namespace System.Data.OracleClient
{
	// Token: 0x02000048 RID: 72
	internal sealed class OciParameterDescriptor : OciSimpleHandle
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0005C984 File Offset: 0x0005BD84
		internal OciParameterDescriptor(OciHandle parent, IntPtr value) : base(parent, OCI.HTYPE.OCI_DTYPE_PARAM, value)
		{
		}
	}
}
