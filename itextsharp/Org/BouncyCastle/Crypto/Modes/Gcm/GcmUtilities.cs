using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x020002A2 RID: 674
	internal abstract class GcmUtilities
	{
		// Token: 0x06001968 RID: 6504 RVA: 0x00094220 File Offset: 0x00093220
		internal static uint[] AsUints(byte[] bs)
		{
			return new uint[]
			{
				Pack.BE_To_UInt32(bs, 0),
				Pack.BE_To_UInt32(bs, 4),
				Pack.BE_To_UInt32(bs, 8),
				Pack.BE_To_UInt32(bs, 12)
			};
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00094260 File Offset: 0x00093260
		internal static void MultiplyP(uint[] x)
		{
			bool flag = (x[3] & 1U) != 0U;
			GcmUtilities.ShiftRight(x);
			if (flag)
			{
				x[0] ^= 3774873600U;
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0009429C File Offset: 0x0009329C
		internal static void MultiplyP8(uint[] x)
		{
			for (int num = 8; num != 0; num--)
			{
				GcmUtilities.MultiplyP(x);
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000942BC File Offset: 0x000932BC
		internal static void ShiftRight(byte[] block)
		{
			int num = 0;
			byte b = 0;
			for (;;)
			{
				byte b2 = block[num];
				block[num] = (byte)(b2 >> 1 | (int)b);
				if (++num == 16)
				{
					break;
				}
				b = (byte)(b2 << 7);
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000942EC File Offset: 0x000932EC
		internal static void ShiftRight(uint[] block)
		{
			int num = 0;
			uint num2 = 0U;
			for (;;)
			{
				uint num3 = block[num];
				block[num] = (num3 >> 1 | num2);
				if (++num == 4)
				{
					break;
				}
				num2 = num3 << 31;
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00094318 File Offset: 0x00093318
		internal static void Xor(byte[] block, byte[] val)
		{
			for (int i = 15; i >= 0; i--)
			{
				int num = i;
				block[num] ^= val[i];
			}
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0009434C File Offset: 0x0009334C
		internal static void Xor(uint[] block, uint[] val)
		{
			for (int i = 3; i >= 0; i--)
			{
				block[i] ^= val[i];
			}
		}
	}
}
