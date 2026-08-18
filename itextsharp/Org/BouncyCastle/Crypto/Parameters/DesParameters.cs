using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000349 RID: 841
	public class DesParameters : KeyParameter
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x000B5848 File Offset: 0x000B4848
		public DesParameters(byte[] key) : base(key)
		{
			if (DesParameters.IsWeakKey(key))
			{
				throw new ArgumentException("attempt to create weak DES key");
			}
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x000B5864 File Offset: 0x000B4864
		public DesParameters(byte[] key, int keyOff, int keyLen) : base(key, keyOff, keyLen)
		{
			if (DesParameters.IsWeakKey(key, keyOff))
			{
				throw new ArgumentException("attempt to create weak DES key");
			}
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x000B5884 File Offset: 0x000B4884
		public static bool IsWeakKey(byte[] key, int offset)
		{
			if (key.Length - offset < 8)
			{
				throw new ArgumentException("key material too short.");
			}
			for (int i = 0; i < 16; i++)
			{
				bool flag = false;
				for (int j = 0; j < 8; j++)
				{
					if (key[j + offset] != DesParameters.DES_weak_keys[i * 8 + j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000B58DB File Offset: 0x000B48DB
		public static bool IsWeakKey(byte[] key)
		{
			return DesParameters.IsWeakKey(key, 0);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000B58E4 File Offset: 0x000B48E4
		public static void SetOddParity(byte[] bytes)
		{
			for (int i = 0; i < bytes.Length; i++)
			{
				int num = (int)bytes[i];
				bytes[i] = (byte)((num & 254) | ((num >> 1 ^ num >> 2 ^ num >> 3 ^ num >> 4 ^ num >> 5 ^ num >> 6 ^ num >> 7 ^ 1) & 1));
			}
		}

		// Token: 0x04001506 RID: 5382
		public const int DesKeyLength = 8;

		// Token: 0x04001507 RID: 5383
		private const int N_DES_WEAK_KEYS = 16;

		// Token: 0x04001508 RID: 5384
		private static readonly byte[] DES_weak_keys = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			31,
			31,
			31,
			31,
			14,
			14,
			14,
			14,
			224,
			224,
			224,
			224,
			241,
			241,
			241,
			241,
			254,
			254,
			254,
			254,
			254,
			254,
			254,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			31,
			224,
			31,
			224,
			14,
			241,
			14,
			241,
			1,
			224,
			1,
			224,
			1,
			241,
			1,
			241,
			31,
			254,
			31,
			254,
			14,
			254,
			14,
			254,
			1,
			31,
			1,
			31,
			1,
			14,
			1,
			14,
			224,
			254,
			224,
			254,
			241,
			254,
			241,
			254,
			254,
			1,
			254,
			1,
			254,
			1,
			254,
			1,
			224,
			31,
			224,
			31,
			241,
			14,
			241,
			14,
			224,
			1,
			224,
			1,
			241,
			1,
			241,
			1,
			254,
			31,
			254,
			31,
			254,
			14,
			254,
			14,
			31,
			1,
			31,
			1,
			14,
			1,
			14,
			1,
			254,
			224,
			254,
			224,
			254,
			241,
			254,
			241
		};
	}
}
