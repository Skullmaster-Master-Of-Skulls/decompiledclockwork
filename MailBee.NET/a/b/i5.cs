using System;

namespace a.b
{
	// Token: 0x02000248 RID: 584
	internal class i5
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x00059A59 File Offset: 0x00058A59
		public byte[] b()
		{
			return this.d;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00059A61 File Offset: 0x00058A61
		public void a(byte[] A_0)
		{
			this.d = A_0;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00059A6A File Offset: 0x00058A6A
		public void b(int A_0)
		{
			this.c = A_0;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00059A73 File Offset: 0x00058A73
		public void a(int A_0)
		{
			this.a = A_0;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00059A7C File Offset: 0x00058A7C
		public void c(int A_0)
		{
			this.b = A_0;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00059A88 File Offset: 0x00058A88
		public h3[] a()
		{
			h3[] array = new h3[(this.d.Length - this.a - this.b) / this.c];
			int num = 0;
			for (int i = this.a; i < this.d.Length - this.b; i += 16)
			{
				int num2 = 0;
				string text = Convert.ToString(this.d[i + num2++], 16);
				string text2 = Convert.ToString(this.d[i + num2++], 16);
				string text3 = Convert.ToString(this.d[i + num2++], 16);
				string text4 = Convert.ToString(this.d[i + num2++], 16);
				while (text3.Length < 2)
				{
					text3 = "0" + text3;
				}
				while (text4.Length < 2)
				{
					text4 = "0" + text4;
				}
				while (text.Length < 2)
				{
					text = "0" + text;
				}
				while (text2.Length < 2)
				{
					text2 = "0" + text2;
				}
				if (text3.Length > 2)
				{
					text3 = text3.Substring(text3.Length - 2, 2);
				}
				if (text4.Length > 2)
				{
					text4 = text4.Substring(text4.Length - 2, 2);
				}
				if (text.Length > 2)
				{
					text = text.Substring(text.Length - 2, 2);
				}
				if (text2.Length > 2)
				{
					text2 = text2.Substring(text2.Length - 2, 2);
				}
				h3 h = new h3((text2 + text).ToUpper(), (text4 + text3).ToUpper());
				array[num++] = h;
			}
			return array;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00059C47 File Offset: 0x00058C47
		public i5()
		{
			this.d = null;
			this.a = 0;
			this.b = 0;
			this.c = 16;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00059C6C File Offset: 0x00058C6C
		public static bool a(byte[] A_0, byte[] A_1)
		{
			if (A_0.Length != A_1.Length)
			{
				return false;
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] != A_1[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00059C9C File Offset: 0x00058C9C
		public byte[] b(string A_0, string A_1)
		{
			byte b = (byte)Convert.ToInt64(A_1.Substring(0, 2), 16);
			byte b2 = (byte)Convert.ToInt64(A_1.Substring(2, 2), 16);
			byte b3 = (byte)Convert.ToInt64(A_0.Substring(0, 2), 16);
			byte b4 = (byte)Convert.ToInt64(A_0.Substring(2, 2), 16);
			for (int i = this.a; i < this.d.Length - this.b; i += 16)
			{
				int num = 0;
				if (this.d[i + num++] == b4 && this.d[i + num++] == b3 && this.d[i + num++] == b2 && this.d[i + num++] == b)
				{
					return new byte[]
					{
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++],
						this.d[i + num++]
					};
				}
			}
			return null;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00059E74 File Offset: 0x00058E74
		public void a(string A_0, string A_1)
		{
			byte b = (byte)Convert.ToInt64(A_0.Substring(0, 2), 16);
			byte b2 = (byte)Convert.ToInt64(A_0.Substring(2, 2), 16);
			byte b3 = (byte)Convert.ToInt64(A_1.Substring(0, 2), 16);
			byte b4 = (byte)Convert.ToInt64(A_1.Substring(2, 2), 16);
			byte[] array = new byte[this.d.Length - 16];
			for (int i = 0; i < this.a; i++)
			{
				array[i] = this.d[i];
			}
			int num = 0;
			for (int j = this.a; j < this.d.Length - this.b; j += 16)
			{
				if (this.d[j] == b4 && this.d[j + 1] == b3 && this.d[j + 2] == b2 && this.d[j + 3] == b)
				{
					num++;
				}
				else
				{
					for (int k = 0; k < 16; k++)
					{
						array[j + k - num * 16] = this.d[j + k];
					}
				}
			}
			for (int l = array.Length - this.b; l < array.Length; l++)
			{
				array[l] = this.d[l + 16];
			}
			this.d = array;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00059FC4 File Offset: 0x00058FC4
		public string a(sbyte[] A_0)
		{
			string text = string.Empty;
			if (A_0 == null)
			{
				return null;
			}
			for (int i = 0; i < A_0.Length; i++)
			{
				string text2 = Convert.ToString((short)A_0[i], 16);
				while (text2.Length < 2)
				{
					text2 = "0" + text2;
				}
				text2 = text2.Substring(text2.Length - 2, 2);
				text = text + text2 + " ";
			}
			return text;
		}

		// Token: 0x04000FA2 RID: 4002
		private int a;

		// Token: 0x04000FA3 RID: 4003
		private int b;

		// Token: 0x04000FA4 RID: 4004
		private int c;

		// Token: 0x04000FA5 RID: 4005
		private byte[] d;
	}
}
