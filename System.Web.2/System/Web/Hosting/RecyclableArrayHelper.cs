using System;

namespace System.Web.Hosting
{
	// Token: 0x020007C2 RID: 1986
	internal class RecyclableArrayHelper
	{
		// Token: 0x06005F26 RID: 24358 RVA: 0x0014870C File Offset: 0x0014690C
		internal static int[] GetIntegerArray(int minimumLength)
		{
			if (minimumLength <= 128)
			{
				return (int[])RecyclableArrayHelper.s_IntegerArrayAllocator.GetBuffer();
			}
			return new int[minimumLength];
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x0014872C File Offset: 0x0014692C
		internal static IntPtr[] GetIntPtrArray(int minimumLength)
		{
			if (minimumLength <= 128)
			{
				return (IntPtr[])RecyclableArrayHelper.s_IntPtrArrayAllocator.GetBuffer();
			}
			return new IntPtr[minimumLength];
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x0014874C File Offset: 0x0014694C
		internal static void ReuseIntegerArray(int[] array)
		{
			if (array != null && array.Length == 128)
			{
				RecyclableArrayHelper.s_IntegerArrayAllocator.ReuseBuffer(array);
			}
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x00148766 File Offset: 0x00146966
		internal static void ReuseIntPtrArray(IntPtr[] array)
		{
			if (array != null && array.Length == 128)
			{
				RecyclableArrayHelper.s_IntPtrArrayAllocator.ReuseBuffer(array);
			}
		}

		// Token: 0x04003197 RID: 12695
		private const int ARRAY_SIZE = 128;

		// Token: 0x04003198 RID: 12696
		private const int MAX_FREE_ARRAYS = 64;

		// Token: 0x04003199 RID: 12697
		private static IntegerArrayAllocator s_IntegerArrayAllocator = new IntegerArrayAllocator(128, 64);

		// Token: 0x0400319A RID: 12698
		private static IntPtrArrayAllocator s_IntPtrArrayAllocator = new IntPtrArrayAllocator(128, 64);
	}
}
