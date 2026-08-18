using System;

namespace SevenZip
{
	// Token: 0x02000024 RID: 36
	public class CRC
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00007190 File Offset: 0x00005390
		static CRC()
		{
			for (uint num = 0U; num < 256U; num += 1U)
			{
				uint num2 = num;
				for (int i = 0; i < 8; i++)
				{
					if ((num2 & 1U) != 0U)
					{
						num2 = (num2 >> 1 ^ 3988292384U);
					}
					else
					{
						num2 >>= 1;
					}
				}
				CRC.Table[(int)((UIntPtr)num)] = num2;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000071E8 File Offset: 0x000053E8
		public void Init()
		{
			this._value = uint.MaxValue;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000071F1 File Offset: 0x000053F1
		public void UpdateByte(byte b)
		{
			this._value = (CRC.Table[(int)((byte)this._value ^ b)] ^ this._value >> 8);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007214 File Offset: 0x00005414
		public void Update(byte[] data, uint offset, uint size)
		{
			for (uint num = 0U; num < size; num += 1U)
			{
				this._value = (CRC.Table[(int)((byte)this._value ^ data[(int)((UIntPtr)(offset + num))])] ^ this._value >> 8);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007250 File Offset: 0x00005450
		public uint GetDigest()
		{
			return this._value ^ uint.MaxValue;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000725C File Offset: 0x0000545C
		private static uint CalculateDigest(byte[] data, uint offset, uint size)
		{
			CRC crc = new CRC();
			crc.Update(data, offset, size);
			return crc.GetDigest();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000727E File Offset: 0x0000547E
		private static bool VerifyDigest(uint digest, byte[] data, uint offset, uint size)
		{
			return CRC.CalculateDigest(data, offset, size) == digest;
		}

		// Token: 0x040000DE RID: 222
		public static readonly uint[] Table = new uint[256];

		// Token: 0x040000DF RID: 223
		private uint _value = uint.MaxValue;
	}
}
