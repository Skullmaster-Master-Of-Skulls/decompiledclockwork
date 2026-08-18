using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Data.OracleClient
{
	// Token: 0x0200003C RID: 60
	internal sealed class OciFileDescriptor : OciHandle
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0005C6C4 File Offset: 0x0005BAC4
		internal OciFileDescriptor(OciHandle parent) : base(parent, OCI.HTYPE.OCI_DTYPE_FILE)
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0005C6E4 File Offset: 0x0005BAE4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal int OCILobFileSetNameWrapper(OciHandle envhp, OciHandle errhp, byte[] dirAlias, ushort dirAliasLength, byte[] fileName, ushort fileNameLength)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			int result;
			try
			{
				base.DangerousAddRef(ref flag);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					IntPtr handle = base.DangerousGetHandle();
					result = UnsafeNativeMethods.OCILobFileSetName(envhp, errhp, ref handle, dirAlias, dirAliasLength, fileName, fileNameLength);
					this.handle = handle;
				}
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}
	}
}
