using System;
using System.Collections.Immutable;

namespace System.Reflection.Internal
{
	// Token: 0x02000160 RID: 352
	internal static class Hash
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x0001F3BE File Offset: 0x0001D5BE
		internal static int Combine(int newKey, int currentKey)
		{
			return currentKey * -1521134295 + newKey;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001F3BE File Offset: 0x0001D5BE
		internal static int Combine(uint newKey, int currentKey)
		{
			return currentKey * -1521134295 + (int)newKey;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001F3C9 File Offset: 0x0001D5C9
		internal static int Combine(bool newKeyPart, int currentKey)
		{
			return Hash.Combine(currentKey, newKeyPart ? 1 : 0);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		internal static int GetFNVHashCode(byte[] data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)data[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001F408 File Offset: 0x0001D608
		internal static int GetFNVHashCode(ImmutableArray<byte> data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)data[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x04000912 RID: 2322
		internal const int FnvOffsetBias = -2128831035;

		// Token: 0x04000913 RID: 2323
		internal const int FnvPrime = 16777619;
	}
}
