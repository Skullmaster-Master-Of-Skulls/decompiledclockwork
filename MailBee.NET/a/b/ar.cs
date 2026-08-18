using System;
using System.Text;

namespace a.b
{
	// Token: 0x0200030E RID: 782
	internal class ar
	{
		// Token: 0x06001BE7 RID: 7143 RVA: 0x0007AAF1 File Offset: 0x00079AF1
		public ar(byte[] A_0, int A_1)
		{
			this.b(A_0, A_1);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0007AB04 File Offset: 0x00079B04
		public ar()
		{
			this.a = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				this.a[i] = 0;
			}
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0007AB3A File Offset: 0x00079B3A
		public int b()
		{
			return 16;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0007AB3E File Offset: 0x00079B3E
		public byte[] a()
		{
			return this.a;
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0007AB48 File Offset: 0x00079B48
		public void a(byte[] A_0)
		{
			for (int i = 0; i < this.a.Length; i++)
			{
				this.a[i] = A_0[i];
			}
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0007AB74 File Offset: 0x00079B74
		public byte[] b(byte[] A_0, int A_1)
		{
			this.a = new byte[16];
			this.a[0] = A_0[3 + A_1];
			this.a[1] = A_0[2 + A_1];
			this.a[2] = A_0[1 + A_1];
			this.a[3] = A_0[A_1];
			this.a[4] = A_0[5 + A_1];
			this.a[5] = A_0[4 + A_1];
			this.a[6] = A_0[7 + A_1];
			this.a[7] = A_0[6 + A_1];
			for (int i = 8; i < 16; i++)
			{
				this.a[i] = A_0[i + A_1];
			}
			return this.a;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0007AC14 File Offset: 0x00079C14
		public void a(byte[] A_0, int A_1)
		{
			if (A_0.Length < 16)
			{
				throw new ArrayTypeMismatchException("Destination byte[] must have room for at least 16 bytes, but has a length of only " + A_0.Length + ".");
			}
			A_0[A_1] = this.a[3];
			A_0[1 + A_1] = this.a[2];
			A_0[2 + A_1] = this.a[1];
			A_0[3 + A_1] = this.a[0];
			A_0[4 + A_1] = this.a[5];
			A_0[5 + A_1] = this.a[4];
			A_0[6 + A_1] = this.a[7];
			A_0[7 + A_1] = this.a[6];
			for (int i = 8; i < 16; i++)
			{
				A_0[i + A_1] = this.a[i];
			}
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x0007ACC8 File Offset: 0x00079CC8
		public override bool Equals(object o)
		{
			if (o == null || !(o is ar))
			{
				return false;
			}
			ar ar = (ar)o;
			if (this.a.Length != ar.a.Length)
			{
				return false;
			}
			for (int i = 0; i < this.a.Length; i++)
			{
				if (this.a[i] != ar.a[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0007AD25 File Offset: 0x00079D25
		public override int GetHashCode()
		{
			return Encoding.UTF8.GetString(this.a).GetHashCode();
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0007AD3C File Offset: 0x00079D3C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(38);
			stringBuilder.Append('{');
			for (int i = 0; i < 16; i++)
			{
				stringBuilder.Append(f5.a(this.a[i]));
				if (i == 3 || i == 5 || i == 7 || i == 9)
				{
					stringBuilder.Append('-');
				}
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x04001345 RID: 4933
		protected byte[] a;

		// Token: 0x04001346 RID: 4934
		public const int b = 16;
	}
}
