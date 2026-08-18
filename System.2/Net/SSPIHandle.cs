using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001F9 RID: 505
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct SSPIHandle
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x00064935 File Offset: 0x00062B35
		public bool IsZero
		{
			get
			{
				return this.HandleHi == IntPtr.Zero && this.HandleLo == IntPtr.Zero;
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0006495B File Offset: 0x00062B5B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetToInvalid()
		{
			this.HandleHi = IntPtr.Zero;
			this.HandleLo = IntPtr.Zero;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00064973 File Offset: 0x00062B73
		public override string ToString()
		{
			return this.HandleHi.ToString("x") + ":" + this.HandleLo.ToString("x");
		}

		// Token: 0x04001552 RID: 5458
		private IntPtr HandleHi;

		// Token: 0x04001553 RID: 5459
		private IntPtr HandleLo;
	}
}
