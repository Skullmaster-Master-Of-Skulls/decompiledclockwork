using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Data.SqlTypes
{
	// Token: 0x0200015D RID: 349
	internal class UnicodeString : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x000A2D68 File Offset: 0x000A2168
		public UnicodeString(string path) : base(true)
		{
			this.Initialize(path);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000A2D84 File Offset: 0x000A2184
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

		// Token: 0x06001598 RID: 5528 RVA: 0x000A2DBC File Offset: 0x000A21BC
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
