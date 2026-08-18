using System;
using System.Collections;
using System.Text;

namespace a
{
	// Token: 0x020004F9 RID: 1273
	internal class ao
	{
		// Token: 0x06002A4D RID: 10829 RVA: 0x000C6852 File Offset: 0x000C5852
		public ao(byte[] A_0)
		{
			this.a = A_0;
			this.b = 0;
			this.c = A_0.Length;
			this.d = null;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000C6878 File Offset: 0x000C5878
		public ao(byte[] A_0, int A_1)
		{
			this.a = A_0;
			this.b = 0;
			this.c = A_1;
			this.d = null;
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000C689C File Offset: 0x000C589C
		public ao(ao A_0, int A_1, int A_2)
		{
			this.d = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000C68BC File Offset: 0x000C58BC
		public static ao a(ao A_0, int A_1, int A_2)
		{
			if (A_2 == 0)
			{
				return new ao(A_0, A_1, A_2);
			}
			int num = A_1;
			int num2 = 0;
			for (;;)
			{
				num = Array.IndexOf<byte>(A_0.d(), 92, num, A_1 + A_2 - num);
				if (num <= -1 || num >= A_1 + A_2 - 1)
				{
					break;
				}
				if (A_0.d()[num + 1] == 34 || A_0.d()[num + 1] == 92)
				{
					num2++;
				}
				num += 2;
			}
			if (num2 == 0)
			{
				return new ao(A_0, A_1, A_2);
			}
			byte[] array = new byte[A_2 - num2];
			num = A_1;
			int num3 = A_1;
			int num4 = 0;
			for (;;)
			{
				num = Array.IndexOf<byte>(A_0.d(), 92, num, A_1 + A_2 - num);
				if (num <= -1 || num >= A_1 + A_2 - 1)
				{
					break;
				}
				if (A_0.d()[num + 1] == 34 || A_0.d()[num + 1] == 92)
				{
					Buffer.BlockCopy(A_0.d(), num3, array, num4, num - num3);
					num4 += num - num3;
					num3 = num + 1;
				}
				num += 2;
			}
			if (num3 < A_1 + A_2)
			{
				Buffer.BlockCopy(A_0.d(), num3, array, num4, A_1 + A_2 - num3);
			}
			return new ao(array);
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000C69BC File Offset: 0x000C59BC
		public byte[] c()
		{
			byte[] src;
			if (this.d == null)
			{
				if (this.c >= this.a.Length)
				{
					return this.a;
				}
				src = this.a;
			}
			else
			{
				src = this.d.d();
			}
			byte[] array = new byte[this.c];
			Buffer.BlockCopy(src, this.b, array, 0, this.c);
			return array;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000C6A20 File Offset: 0x000C5A20
		public string a(Encoding A_0)
		{
			byte[] bytes = (this.d == null) ? this.a : this.d.d();
			return A_0.GetString(bytes, this.b, this.c);
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000C6A5C File Offset: 0x000C5A5C
		private string a()
		{
			return this.a(Encoding.GetEncoding(0));
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000C6A6C File Offset: 0x000C5A6C
		public string a(Encoding A_0, int A_1, int A_2)
		{
			byte[] bytes = (this.d == null) ? this.a : this.d.d();
			return A_0.GetString(bytes, A_1, A_2);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000C6A9E File Offset: 0x000C5A9E
		public static ao a(string A_0, Encoding A_1)
		{
			return new ao(A_1.GetBytes(A_0));
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000C6AAC File Offset: 0x000C5AAC
		public void a(byte[] A_0, int A_1, int A_2)
		{
			ao ao = (this.d == null) ? this : this.d;
			this.a(A_2);
			Buffer.BlockCopy(A_0, A_1, ao.d(), this.b + this.c, A_2);
			this.c += A_2;
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000C6AFC File Offset: 0x000C5AFC
		public void a(int A_0)
		{
			if (A_0 <= 0)
			{
				return;
			}
			ao ao = (this.d == null) ? this : this.d;
			byte[] array = ao.d();
			if (array.Length - (this.b + this.c) < A_0)
			{
				byte[] array2 = new byte[array.Length + A_0];
				Buffer.BlockCopy(array, 0, array2, 0, array.Length);
				ao.a(array2);
			}
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000C6B5C File Offset: 0x000C5B5C
		public static string[] a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			string[] array = new string[A_0.Count];
			for (int i = 0; i < A_0.Count; i++)
			{
				try
				{
					array[i] = ((ao)A_0[i]).a(A_1);
				}
				catch
				{
					return null;
				}
			}
			return array;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000C6BBC File Offset: 0x000C5BBC
		public byte[] d()
		{
			if (this.d == null)
			{
				return this.a;
			}
			return this.d.d();
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000C6BD8 File Offset: 0x000C5BD8
		private void a(byte[] A_0)
		{
			if (this.d == null)
			{
				this.a = A_0;
				return;
			}
			this.d.a(A_0);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000C6BF6 File Offset: 0x000C5BF6
		public int b()
		{
			return this.b;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000C6BFE File Offset: 0x000C5BFE
		public int e()
		{
			return this.c;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x000C6C06 File Offset: 0x000C5C06
		public void b(int A_0)
		{
			this.c = A_0;
		}

		// Token: 0x04001D3A RID: 7482
		private byte[] a;

		// Token: 0x04001D3B RID: 7483
		private int b;

		// Token: 0x04001D3C RID: 7484
		private int c;

		// Token: 0x04001D3D RID: 7485
		private ao d;
	}
}
