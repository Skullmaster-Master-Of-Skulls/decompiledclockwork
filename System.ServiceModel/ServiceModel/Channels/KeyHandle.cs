using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A49 RID: 2633
	internal sealed class KeyHandle : SafeHandle
	{
		// Token: 0x0600681F RID: 26655
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern bool CryptDestroyKey(IntPtr hKey);

		// Token: 0x06006820 RID: 26656 RVA: 0x00184875 File Offset: 0x00182A75
		private KeyHandle() : base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06006821 RID: 26657 RVA: 0x00184883 File Offset: 0x00182A83
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x00184895 File Offset: 0x00182A95
		protected override bool ReleaseHandle()
		{
			return KeyHandle.CryptDestroyKey(this.handle);
		}
	}
}
