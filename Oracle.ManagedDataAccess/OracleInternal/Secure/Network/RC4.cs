using System;
using \u0005;

namespace OracleInternal.Secure.Network
{
	// Token: 0x02000351 RID: 849
	internal class RC4 : EncryptionAlgorithm
	{
		// Token: 0x06001DFD RID: 7677 RVA: 0x001250F0 File Offset: 0x001232F0
		public RC4()
		{
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x00125108 File Offset: 0x00123308
		public RC4(bool cp, int key_size)
		{
			if (key_size <= 56)
			{
				if (key_size != 40 && key_size != 56)
				{
					goto IL_41;
				}
			}
			else if (key_size != 128 && key_size != 256)
			{
				goto IL_41;
			}
			this.\u0011 = key_size;
			this.\u000F = cp;
			return;
			IL_41:
			throw new Exception(global::\u0005.\u0001.\u0001(580));
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x00125170 File Offset: 0x00123370
		public RC4(int key_size, byte[] key, byte[] iv)
		{
			if (key_size <= 56)
			{
				if (key_size != 40 && key_size != 56)
				{
					goto IL_41;
				}
			}
			else if (key_size != 128 && key_size != 256)
			{
				goto IL_41;
			}
			this.\u0011 = key_size;
			this.init(key, iv);
			return;
			IL_41:
			throw new Exception(global::\u0005.\u0001.\u0001(580));
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x001251D8 File Offset: 0x001233D8
		public RC4(int key_size, byte[] key, byte[] iv, bool _dataIntegrityMode)
		{
			this.\u0011 = key_size;
			this.\u0010 = true;
			this.\u000F = false;
			this.init(key, iv);
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0012520C File Offset: 0x0012340C
		public override void init(byte[] key, byte[] iv)
		{
			this.\u0007 = new RC4.\u0001();
			this.\u0008 = new RC4.\u0001();
			this.\u000E = new RC4.\u0001();
			this.setSessionKey(key, iv);
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x00125238 File Offset: 0x00123438
		public override byte[] decrypt(byte[] ciphertext)
		{
			if (this.\u000F)
			{
				byte[] sourceArray = this.\u000E.\u0001(ciphertext, ciphertext.Length - 1);
				byte[] array = new byte[ciphertext.Length - 1];
				Array.Copy(sourceArray, 0, array, 0, ciphertext.Length - 1);
				return array;
			}
			return this.\u000E.\u0001(ciphertext, ciphertext.Length);
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0012528C File Offset: 0x0012348C
		public override byte[] decrypt(byte[] buffer, int length)
		{
			if (length > buffer.Length)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(609));
			}
			if (this.\u000F)
			{
				byte[] sourceArray = this.\u000E.\u0001(buffer, length - 1);
				byte[] array = new byte[length - 1];
				Array.Copy(sourceArray, 0, array, 0, length - 1);
				return array;
			}
			return this.\u000E.\u0001(buffer, length);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x001252F0 File Offset: 0x001234F0
		public override byte[] encrypt(byte[] buffer, int length)
		{
			if (length > buffer.Length)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(609));
			}
			byte[] array = this.\u0008.\u0001(buffer, length);
			if (this.\u000F)
			{
				byte[] array2 = new byte[length + 1];
				Array.Copy(array, 0, array2, 0, length);
				array2[length] = 0;
				return array2;
			}
			return array;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00125344 File Offset: 0x00123544
		public override byte[] encrypt(byte[] plaintext)
		{
			byte[] array = this.\u0008.\u0001(plaintext, plaintext.Length);
			if (this.\u000F)
			{
				byte[] array2 = new byte[plaintext.Length + 1];
				Array.Copy(array, 0, array2, 0, plaintext.Length);
				array2[plaintext.Length] = 0;
				return array2;
			}
			return array;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0012538C File Offset: 0x0012358C
		public override int maxDelta()
		{
			return 1;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00125390 File Offset: 0x00123590
		public override void setSessionKey(byte[] key, byte[] iv)
		{
			if (key == null && iv == null)
			{
				this.\u0001();
				return;
			}
			int num = this.\u0011 / 8;
			if (key.Length < num)
			{
				throw new Exception(global::\u0005.\u0001.\u0001(646));
			}
			int num2 = 0;
			if (iv != null)
			{
				num2 = iv.Length;
			}
			byte[] array = new byte[num + 1 + num2];
			Array.Copy(key, key.Length - num, array, 0, num);
			if (!this.\u0010)
			{
				array[num] = 123;
			}
			else
			{
				array[num] = byte.MaxValue;
			}
			if (iv != null)
			{
				Array.Copy(iv, 0, array, num + 1, iv.Length);
			}
			this.\u0007.\u0001(array, array.Length);
			this.\u0001();
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00125428 File Offset: 0x00123628
		internal new void \u0001()
		{
			byte[] array = new byte[]
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32,
				32
			};
			int num = this.\u0011 / 8;
			if (!this.\u0010)
			{
				byte[] array2 = this.\u0007.\u0001(array, num);
				if (this.\u000F)
				{
					byte[] array3 = array2;
					int num2 = num - 1;
					array3[num2] ^= 170;
				}
				this.\u000E.\u0001(array2, num);
				byte[] array4 = array2;
				int num3 = num - 1;
				array4[num3] ^= 170;
				this.\u0008.\u0001(array2, num);
				return;
			}
			byte[] array5 = new byte[num + 1];
			this.\u0007.\u0001(array5, array, num);
			array5[num] = 180;
			this.\u000E.\u0001(array5, num + 1);
			array5[num] = 90;
			this.\u0008.\u0001(array5, num + 1);
		}

		// Token: 0x0400203F RID: 8255
		private new const byte \u0001 = 123;

		// Token: 0x04002040 RID: 8256
		private const byte \u0002 = 255;

		// Token: 0x04002041 RID: 8257
		private const int \u0003 = 170;

		// Token: 0x04002042 RID: 8258
		private const int \u0004 = 85;

		// Token: 0x04002043 RID: 8259
		private const int \u0005 = 180;

		// Token: 0x04002044 RID: 8260
		private const int \u0006 = 90;

		// Token: 0x04002045 RID: 8261
		private RC4.\u0001 \u0007;

		// Token: 0x04002046 RID: 8262
		private RC4.\u0001 \u0008;

		// Token: 0x04002047 RID: 8263
		private RC4.\u0001 \u000E;

		// Token: 0x04002048 RID: 8264
		private bool \u000F = true;

		// Token: 0x04002049 RID: 8265
		private bool \u0010;

		// Token: 0x0400204A RID: 8266
		private int \u0011 = 40;

		// Token: 0x02000352 RID: 850
		internal new class \u0001
		{
			// Token: 0x06001E09 RID: 7689 RVA: 0x00125504 File Offset: 0x00123704
			internal \u0001()
			{
			}

			// Token: 0x06001E0A RID: 7690 RVA: 0x0012551C File Offset: 0x0012371C
			public void \u0001(byte[] \u0002, int \u0003)
			{
				for (int i = 0; i < 256; i++)
				{
					this.\u0002[i] = (byte)i;
				}
				this.\u0003 = (this.\u0004 = 0);
				int j = 0;
				int num = 0;
				int num2 = 0;
				while (j < 256)
				{
					byte b = this.\u0002[j];
					if (num2 == \u0003)
					{
						num2 = 0;
					}
					num = (num + (int)b + (int)\u0002[num2] & 255);
					this.\u0002[j] = this.\u0002[num];
					this.\u0002[num] = b;
					j++;
					num2++;
				}
			}

			// Token: 0x06001E0B RID: 7691 RVA: 0x001255A8 File Offset: 0x001237A8
			public byte[] \u0001(byte[] \u0002, int \u0003)
			{
				byte[] array = new byte[\u0003];
				this.\u0001(array, \u0002, \u0003);
				return array;
			}

			// Token: 0x06001E0C RID: 7692 RVA: 0x001255C8 File Offset: 0x001237C8
			public void \u0001(byte[] \u0002, byte[] \u0003, int \u0004)
			{
				int num = this.\u0003;
				int num2 = this.\u0004;
				for (int i = 0; i < \u0004; i++)
				{
					num = (num + 1 & 255);
					int num3 = (int)this.\u0002[num];
					num2 = (num2 + num3 & 255);
					int num4 = (int)this.\u0002[num2];
					this.\u0002[num] = (byte)(num4 & 255);
					this.\u0002[num2] = (byte)(num3 & 255);
					int num5 = num3 + num4 & 255;
					\u0002[i] = ((\u0003[i] ^ this.\u0002[num5]) & byte.MaxValue);
				}
				this.\u0003 = num;
				this.\u0004 = num2;
			}

			// Token: 0x0400204B RID: 8267
			private const ushort \u0001 = 256;

			// Token: 0x0400204C RID: 8268
			private byte[] \u0002 = new byte[256];

			// Token: 0x0400204D RID: 8269
			private int \u0003;

			// Token: 0x0400204E RID: 8270
			private int \u0004;
		}
	}
}
