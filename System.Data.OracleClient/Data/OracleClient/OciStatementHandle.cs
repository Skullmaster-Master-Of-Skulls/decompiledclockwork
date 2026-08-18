using System;

namespace System.Data.OracleClient
{
	// Token: 0x02000044 RID: 68
	internal sealed class OciStatementHandle : OciHandle
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0005C894 File Offset: 0x0005BC94
		internal OciStatementHandle(OciHandle parent) : base(parent, OCI.HTYPE.OCI_HTYPE_STMT)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0005C8B4 File Offset: 0x0005BCB4
		internal OciParameterDescriptor GetDescriptor(int i, OciErrorHandle errorHandle)
		{
			IntPtr value;
			int num = TracedNativeMethods.OCIParamGet(this, base.HandleType, errorHandle, out value, i + 1);
			if (num != 0)
			{
				OracleException.Check(errorHandle, num);
			}
			return new OciParameterDescriptor(this, value);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0005C8F4 File Offset: 0x0005BCF4
		internal OciRowidDescriptor GetRowid(OciHandle environmentHandle, OciErrorHandle errorHandle)
		{
			OciRowidDescriptor ociRowidDescriptor = new OciRowidDescriptor(environmentHandle);
			ociRowidDescriptor.GetRowid(this, errorHandle);
			return ociRowidDescriptor;
		}
	}
}
