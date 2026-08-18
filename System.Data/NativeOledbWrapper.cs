using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

// Token: 0x02000009 RID: 9
[CLSCompliant(false)]
internal class NativeOledbWrapper
{
	// Token: 0x06000072 RID: 114 RVA: 0x001D5D14 File Offset: 0x001D5114
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static int IChapteredRowsetReleaseChapter(IntPtr ptr, IntPtr chapter)
	{
		int result = -2147418113;
		uint num = 0U;
		ulong num2 = chapter.ToPointer();
		IChapteredRowset* ptr2 = null;
		IUnknown* ptr3 = (IUnknown*)ptr.ToPointer();
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
		}
		finally
		{
			result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr3, ref <Module>.IID_IChapteredRowset, ref ptr2, *(*(long*)ptr3));
			if (null != ptr2)
			{
				result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ptr2, num2, ref num, *(*(long*)ptr2 + 32L));
				IChapteredRowset* ptr4 = ptr2;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
			}
		}
		return result;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x001D5D98 File Offset: 0x001D5198
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static int ITransactionAbort(IntPtr ptr)
	{
		int result = -2147418113;
		ITransactionLocal* ptr2 = null;
		IUnknown* ptr3 = (IUnknown*)ptr.ToPointer();
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
		}
		finally
		{
			result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr3, ref <Module>.IID_ITransactionLocal, ref ptr2, *(*(long*)ptr3));
			if (null != ptr2)
			{
				result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,BOID*,System.Int32,System.Int32), ptr2, 0L, 0, 0, *(*(long*)ptr2 + 32L));
				ITransactionLocal* ptr4 = ptr2;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
			}
		}
		return result;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x001D5E14 File Offset: 0x001D5214
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static int ITransactionCommit(IntPtr ptr)
	{
		int result = -2147418113;
		ITransactionLocal* ptr2 = null;
		IUnknown* ptr3 = (IUnknown*)ptr.ToPointer();
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
		}
		finally
		{
			result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr3, ref <Module>.IID_ITransactionLocal, ref ptr2, *(*(long*)ptr3));
			if (null != ptr2)
			{
				result = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)), ptr2, 0, 2, 0, *(*(long*)ptr2 + 24L));
				ITransactionLocal* ptr4 = ptr2;
				object obj = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr4, *(*(long*)ptr4 + 16L));
			}
		}
		return result;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x001D5E8C File Offset: 0x001D528C
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool MemoryCompare(IntPtr buf1, IntPtr buf2, int count)
	{
		Debug.Assert(buf1 != buf2, "buf1 and buf2 are the same");
		byte condition;
		if (buf1.ToInt64() >= buf2.ToInt64() && buf2.ToInt64() + (long)count > buf1.ToInt64())
		{
			condition = 0;
		}
		else
		{
			condition = 1;
		}
		Debug.Assert(condition != 0, "overlapping region buf1");
		byte condition2;
		if (buf2.ToInt64() >= buf1.ToInt64() && buf1.ToInt64() + (long)count > buf2.ToInt64())
		{
			condition2 = 0;
		}
		else
		{
			condition2 = 1;
		}
		Debug.Assert(condition2 != 0, "overlapping region buf2");
		byte condition3 = (0 <= count) ? 1 : 0;
		Debug.Assert(condition3 != 0, "negative count");
		ulong num = (ulong)((long)count);
		void* ptr = buf2.ToPointer();
		void* ptr2 = buf1.ToPointer();
		int num2;
		if (num != 0UL)
		{
			sbyte b = *(sbyte*)ptr2;
			sbyte b2 = *(sbyte*)ptr;
			if (b >= b2)
			{
				while (b <= b2)
				{
					if (num == 1UL)
					{
						goto IL_E1;
					}
					num -= 1UL;
					ptr2 = (void*)((byte*)ptr2 + 1L);
					ptr = (void*)((byte*)ptr + 1L);
					b = *(sbyte*)ptr2;
					b2 = *(sbyte*)ptr;
					if (b < b2)
					{
						break;
					}
				}
			}
			num2 = 1;
			goto IL_E4;
		}
		IL_E1:
		num2 = 0;
		IL_E4:
		return (byte)num2 != 0;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x001D5F80 File Offset: 0x001D5380
	internal static void MemoryCopy(IntPtr dst, IntPtr src, int count)
	{
		Debug.Assert(dst != src, "dst and src are the same");
		byte condition;
		if (dst.ToInt64() >= src.ToInt64() && src.ToInt64() + (long)count > dst.ToInt64())
		{
			condition = 0;
		}
		else
		{
			condition = 1;
		}
		Debug.Assert(condition != 0, "overlapping region dst");
		byte condition2;
		if (src.ToInt64() >= dst.ToInt64() && dst.ToInt64() + (long)count > src.ToInt64())
		{
			condition2 = 0;
		}
		else
		{
			condition2 = 1;
		}
		Debug.Assert(condition2 != 0, "overlapping region src");
		byte condition3 = (0 <= count) ? 1 : 0;
		Debug.Assert(condition3 != 0, "negative count");
		cpblk(dst.ToPointer(), src.ToPointer(), (long)count);
	}

	// Token: 0x0400006B RID: 107
	internal static int SizeOfPROPVARIANT = 24;
}
