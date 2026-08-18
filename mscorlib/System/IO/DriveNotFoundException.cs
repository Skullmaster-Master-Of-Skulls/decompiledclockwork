using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x020005B2 RID: 1458
	[ComVisible(true)]
	[Serializable]
	public class DriveNotFoundException : IOException
	{
		// Token: 0x060035AD RID: 13741 RVA: 0x000B2F8D File Offset: 0x000B1F8D
		public DriveNotFoundException() : base(Environment.GetResourceString("Arg_DriveNotFoundException"))
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000B2FAA File Offset: 0x000B1FAA
		public DriveNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000B2FBE File Offset: 0x000B1FBE
		public DriveNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024893);
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000B2FD3 File Offset: 0x000B1FD3
		protected DriveNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
