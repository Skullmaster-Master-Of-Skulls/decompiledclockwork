using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.IdentityModel
{
	// Token: 0x02000094 RID: 148
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct SSPIHandle
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001201A File Offset: 0x0001021A
		public bool IsZero
		{
			get
			{
				return this.HandleHi == IntPtr.Zero && this.HandleLo == IntPtr.Zero;
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00012040 File Offset: 0x00010240
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetToInvalid()
		{
			this.HandleHi = IntPtr.Zero;
			this.HandleLo = IntPtr.Zero;
		}

		// Token: 0x04000452 RID: 1106
		private IntPtr HandleHi;

		// Token: 0x04000453 RID: 1107
		private IntPtr HandleLo;
	}
}
