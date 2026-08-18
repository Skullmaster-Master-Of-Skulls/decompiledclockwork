using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200051D RID: 1309
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct SSPIHandle
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x000A5F21 File Offset: 0x000A4F21
		public bool IsZero
		{
			get
			{
				return this.HandleHi == IntPtr.Zero && this.HandleLo == IntPtr.Zero;
			}
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000A5F47 File Offset: 0x000A4F47
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void SetToInvalid()
		{
			this.HandleHi = IntPtr.Zero;
			this.HandleLo = IntPtr.Zero;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000A5F5F File Offset: 0x000A4F5F
		public override string ToString()
		{
			return this.HandleHi.ToString("x") + ":" + this.HandleLo.ToString("x");
		}

		// Token: 0x04002782 RID: 10114
		private IntPtr HandleHi;

		// Token: 0x04002783 RID: 10115
		private IntPtr HandleLo;
	}
}
