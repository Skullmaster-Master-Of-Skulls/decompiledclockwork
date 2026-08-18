using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000053 RID: 83
	internal struct StoreApplicationReference
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000733D File Offset: 0x0000553D
		[SecuritySafeCritical]
		public StoreApplicationReference(Guid RefScheme, string Id, string NcData)
		{
			this.Size = (uint)Marshal.SizeOf(typeof(StoreApplicationReference));
			this.Flags = StoreApplicationReference.RefFlags.Nothing;
			this.GuidScheme = RefScheme;
			this.Identifier = Id;
			this.NonCanonicalData = NcData;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007370 File Offset: 0x00005570
		[SecurityCritical]
		public IntPtr ToIntPtr()
		{
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
			Marshal.StructureToPtr(this, intPtr, false);
			return intPtr;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000073A6 File Offset: 0x000055A6
		[SecurityCritical]
		public static void Destroy(IntPtr ip)
		{
			if (ip != IntPtr.Zero)
			{
				Marshal.DestroyStructure(ip, typeof(StoreApplicationReference));
				Marshal.FreeCoTaskMem(ip);
			}
		}

		// Token: 0x04000164 RID: 356
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x04000165 RID: 357
		[MarshalAs(UnmanagedType.U4)]
		public StoreApplicationReference.RefFlags Flags;

		// Token: 0x04000166 RID: 358
		public Guid GuidScheme;

		// Token: 0x04000167 RID: 359
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Identifier;

		// Token: 0x04000168 RID: 360
		[MarshalAs(UnmanagedType.LPWStr)]
		public string NonCanonicalData;

		// Token: 0x02000525 RID: 1317
		[Flags]
		public enum RefFlags
		{
			// Token: 0x040037AE RID: 14254
			Nothing = 0
		}
	}
}
