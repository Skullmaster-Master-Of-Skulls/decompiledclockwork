using System;
using System.IO;

namespace a.b
{
	// Token: 0x02000323 RID: 803
	internal abstract class a3
	{
		// Token: 0x06001CE9 RID: 7401 RVA: 0x0007DBFA File Offset: 0x0007CBFA
		protected a3(bool A_0, int A_1, bool A_2)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x06001CEA RID: 7402
		protected abstract int a(byte[] A_0);

		// Token: 0x06001CEB RID: 7403
		protected abstract int b(int A_0);

		// Token: 0x06001CEC RID: 7404 RVA: 0x0007DC18 File Offset: 0x0007CC18
		public byte[] a(Stream A_0)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.a(A_0, memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0007DC58 File Offset: 0x0007CC58
		public void a(Stream A_0, Stream A_1)
		{
			byte[] array = new byte[4096];
			int num = this.a(array);
			byte[] array2 = new byte[16 + this.b];
			int num2;
			while ((num2 = A_0.ReadByte()) != -1)
			{
				for (int i = 1; i < 256; i <<= 1)
				{
					if ((num2 & i) > 0 ^ this.a)
					{
						int a_;
						if ((a_ = A_0.ReadByte()) != -1)
						{
							array[num & 4095] = a3.a(a_);
							num++;
							A_1.WriteByte(a3.a(a_));
						}
					}
					else
					{
						int num3 = A_0.ReadByte();
						int num4 = A_0.ReadByte();
						if (num3 == -1 || num4 == -1)
						{
							break;
						}
						int num5 = (num4 & 15) + this.b;
						int num6;
						if (this.c)
						{
							num6 = (num3 << 4) + (num4 >> 4);
						}
						else
						{
							num6 = num3 + ((num4 & 240) << 4);
						}
						num6 = this.b(num6);
						for (int j = 0; j < num5; j++)
						{
							array2[j] = array[num6 + j & 4095];
							array[num + j & 4095] = array2[j];
						}
						A_1.Write(array2, 0, num5);
						num += num5;
					}
				}
			}
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0007DD95 File Offset: 0x0007CD95
		public static byte a(int A_0)
		{
			if (A_0 < 128)
			{
				return (byte)A_0;
			}
			return (byte)(A_0 - 256);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0007DDAA File Offset: 0x0007CDAA
		public static int a(byte A_0)
		{
			if (A_0 >= 0)
			{
				return (int)A_0;
			}
			return (int)A_0 + 256;
		}

		// Token: 0x0400136F RID: 4975
		private bool a;

		// Token: 0x04001370 RID: 4976
		private int b;

		// Token: 0x04001371 RID: 4977
		private bool c;
	}
}
