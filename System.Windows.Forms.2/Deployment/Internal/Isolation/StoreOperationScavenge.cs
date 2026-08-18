using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005B RID: 91
	internal struct StoreOperationScavenge
	{
		// Token: 0x0600019F RID: 415 RVA: 0x000077B0 File Offset: 0x000059B0
		[SecuritySafeCritical]
		public StoreOperationScavenge(bool Light, ulong SizeLimit, ulong RunLimit, uint ComponentLimit)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreOperationScavenge));
			this.Flags = StoreOperationScavenge.OpFlags.Nothing;
			if (Light)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.Light;
			}
			this.SizeReclaimationLimit = SizeLimit;
			if (SizeLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitSize;
			}
			this.RuntimeLimit = RunLimit;
			if (RunLimit != 0UL)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitTime;
			}
			this.ComponentCountLimit = ComponentLimit;
			if (ComponentLimit != 0U)
			{
				this.Flags |= StoreOperationScavenge.OpFlags.LimitCount;
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007834 File Offset: 0x00005A34
		public StoreOperationScavenge(bool Light)
		{
			this = new StoreOperationScavenge(Light, 0UL, 0UL, 0U);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000072B6 File Offset: 0x000054B6
		public void Destroy()
		{
		}

		// Token: 0x0400018A RID: 394
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x0400018B RID: 395
		[MarshalAs(UnmanagedType.U4)]
		public StoreOperationScavenge.OpFlags Flags;

		// Token: 0x0400018C RID: 396
		[MarshalAs(UnmanagedType.U8)]
		public ulong SizeReclaimationLimit;

		// Token: 0x0400018D RID: 397
		[MarshalAs(UnmanagedType.U8)]
		public ulong RuntimeLimit;

		// Token: 0x0400018E RID: 398
		[MarshalAs(UnmanagedType.U4)]
		public uint ComponentCountLimit;

		// Token: 0x02000531 RID: 1329
		[Flags]
		public enum OpFlags
		{
			// Token: 0x040037CF RID: 14287
			Nothing = 0,
			// Token: 0x040037D0 RID: 14288
			Light = 1,
			// Token: 0x040037D1 RID: 14289
			LimitSize = 2,
			// Token: 0x040037D2 RID: 14290
			LimitTime = 4,
			// Token: 0x040037D3 RID: 14291
			LimitCount = 8
		}
	}
}
