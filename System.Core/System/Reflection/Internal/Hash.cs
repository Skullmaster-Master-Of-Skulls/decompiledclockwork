using System;

namespace System.Reflection.Internal
{
	// Token: 0x02000084 RID: 132
	internal static class Hash
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00008174 File Offset: 0x00006374
		internal static int Combine(int newKey, int currentKey)
		{
			return currentKey * -1521134295 + newKey;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000817F File Offset: 0x0000637F
		internal static int Combine(uint newKey, int currentKey)
		{
			return currentKey * -1521134295 + (int)newKey;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000818A File Offset: 0x0000638A
		internal static int Combine(bool newKeyPart, int currentKey)
		{
			return Hash.Combine(currentKey, newKeyPart ? 1 : 0);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000819C File Offset: 0x0000639C
		internal static int GetFNVHashCode(byte[] data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)data[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x04000491 RID: 1169
		internal const int FnvOffsetBias = -2128831035;

		// Token: 0x04000492 RID: 1170
		internal const int FnvPrime = 16777619;
	}
}
