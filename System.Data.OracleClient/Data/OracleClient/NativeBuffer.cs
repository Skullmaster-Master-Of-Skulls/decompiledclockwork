using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Data.OracleClient
{
	// Token: 0x0200001D RID: 29
	internal class NativeBuffer : DbBuffer
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0005AF34 File Offset: 0x0005A334
		public NativeBuffer(int initialSize, bool zeroBuffer) : base(initialSize, zeroBuffer)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0005AF54 File Offset: 0x0005A354
		public NativeBuffer(int initialSize) : base(initialSize, false)
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0005AF74 File Offset: 0x0005A374
		internal IntPtr DangerousGetDataPtr()
		{
			return base.DangerousGetHandle();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0005AF94 File Offset: 0x0005A394
		internal IntPtr DangerousGetDataPtr(int offset)
		{
			return ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0005AFB4 File Offset: 0x0005A3B4
		internal IntPtr DangerousGetDataPtrWithBaseOffset(int offset)
		{
			return ADP.IntPtrOffset(base.DangerousGetHandle(), offset + base.BaseOffset);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0005AFD4 File Offset: 0x0005A3D4
		internal static IntPtr HandleValueToTrace(NativeBuffer buffer)
		{
			return buffer.DangerousGetHandle();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0005AFF4 File Offset: 0x0005A3F4
		internal string PtrToStringAnsi(int offset)
		{
			offset += base.BaseOffset;
			base.Validate(offset, 1);
			string result = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				int num = UnsafeNativeMethods.lstrlenA(ptr);
				result = Marshal.PtrToStringAnsi(ptr, num);
				base.Validate(offset, num + 1);
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

		// Token: 0x060001B2 RID: 434 RVA: 0x0005B074 File Offset: 0x0005A474
		internal string PtrToStringAnsi(int offset, int length)
		{
			offset += base.BaseOffset;
			base.Validate(offset, length);
			string result = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				result = Marshal.PtrToStringAnsi(ptr, length);
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

		// Token: 0x060001B3 RID: 435 RVA: 0x0005B0E4 File Offset: 0x0005A4E4
		internal object PtrToStructure(int offset, Type oftype)
		{
			offset += base.BaseOffset;
			object result = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				base.DangerousAddRef(ref flag);
				IntPtr ptr = ADP.IntPtrOffset(base.DangerousGetHandle(), offset);
				result = Marshal.PtrToStructure(ptr, oftype);
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

		// Token: 0x060001B4 RID: 436 RVA: 0x0005B154 File Offset: 0x0005A554
		internal static void SafeDispose(ref NativeBuffer_LongColumnData handle)
		{
			if (handle != null)
			{
				handle.Dispose();
			}
			handle = null;
		}
	}
}
