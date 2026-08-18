using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlTypes
{
	// Token: 0x0200034C RID: 844
	internal class UnicodeString : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002D41 RID: 11585 RVA: 0x002CD248 File Offset: 0x002CC648
		public UnicodeString(string path) : base(true)
		{
			this.Initialize(path);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x002CD268 File Offset: 0x002CC668
		protected override bool ReleaseHandle()
		{
			if (this.handle == IntPtr.Zero)
			{
				return true;
			}
			Marshal.FreeHGlobal(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x002CD2A8 File Offset: 0x002CC6A8
		private void Initialize(string path)
		{
			UnsafeNativeMethods.UNICODE_STRING unicode_STRING;
			unicode_STRING.length = (ushort)(path.Length * 2);
			unicode_STRING.maximumLength = (ushort)(path.Length * 2);
			unicode_STRING.buffer = path;
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(unicode_STRING));
				if (intPtr != IntPtr.Zero)
				{
					base.SetHandle(intPtr);
				}
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = base.DangerousGetHandle();
				Marshal.StructureToPtr(unicode_STRING, ptr, false);
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
		}
	}
}
