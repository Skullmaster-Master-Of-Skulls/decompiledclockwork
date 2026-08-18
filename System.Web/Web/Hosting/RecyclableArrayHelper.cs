using System;

namespace System.Web.Hosting
{
	// Token: 0x0200029F RID: 671
	internal class RecyclableArrayHelper
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x00096A28 File Offset: 0x00095A28
		internal static int[] GetIntegerArray(int minimumLength)
		{
			if (minimumLength <= 128)
			{
				return (int[])RecyclableArrayHelper.s_IntegerArrayAllocator.GetBuffer();
			}
			return new int[minimumLength];
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00096A48 File Offset: 0x00095A48
		internal static IntPtr[] GetIntPtrArray(int minimumLength)
		{
			if (minimumLength <= 128)
			{
				return (IntPtr[])RecyclableArrayHelper.s_IntPtrArrayAllocator.GetBuffer();
			}
			return new IntPtr[minimumLength];
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x00096A68 File Offset: 0x00095A68
		internal static void ReuseIntegerArray(int[] array)
		{
			if (array != null && array.Length == 128)
			{
				RecyclableArrayHelper.s_IntegerArrayAllocator.ReuseBuffer(array);
			}
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x00096A82 File Offset: 0x00095A82
		internal static void ReuseIntPtrArray(IntPtr[] array)
		{
			if (array != null && array.Length == 128)
			{
				RecyclableArrayHelper.s_IntPtrArrayAllocator.ReuseBuffer(array);
			}
		}

		// Token: 0x04001B7F RID: 7039
		private const int ARRAY_SIZE = 128;

		// Token: 0x04001B80 RID: 7040
		private const int MAX_FREE_ARRAYS = 64;

		// Token: 0x04001B81 RID: 7041
		private static IntegerArrayAllocator s_IntegerArrayAllocator = new IntegerArrayAllocator(128, 64);

		// Token: 0x04001B82 RID: 7042
		private static IntPtrArrayAllocator s_IntPtrArrayAllocator = new IntPtrArrayAllocator(128, 64);
	}
}
