using System;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000320 RID: 800
	internal class d0 : gc
	{
		// Token: 0x06001CC6 RID: 7366 RVA: 0x0007D6BE File Offset: 0x0007C6BE
		public int aq()
		{
			return (int)(this.a.Length - this.a.Position);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0007D6D8 File Offset: 0x0007C6D8
		public d0(Stream A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0007D6E7 File Offset: 0x0007C6E7
		public int ReadByte()
		{
			return (int)((byte)this.a2());
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0007D6F0 File Offset: 0x0007C6F0
		public int a2()
		{
			int num;
			try
			{
				num = this.a.ReadByte();
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
			d0.a(num);
			return num;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0007D728 File Offset: 0x0007C728
		public double aw()
		{
			return BitConverter.Int64BitsToDouble(this.ax());
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0007D738 File Offset: 0x0007C738
		public int a0()
		{
			int num;
			int num2;
			int num3;
			int num4;
			try
			{
				num = this.a.ReadByte();
				num2 = this.a.ReadByte();
				num3 = this.a.ReadByte();
				num4 = this.a.ReadByte();
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
			d0.a(num | num2 | num3 | num4);
			return (num4 << 24) + (num3 << 16) + (num2 << 8) + num;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0007D7A8 File Offset: 0x0007C7A8
		public long ax()
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			int num8;
			try
			{
				num = this.a.ReadByte();
				num2 = this.a.ReadByte();
				num3 = this.a.ReadByte();
				num4 = this.a.ReadByte();
				num5 = this.a.ReadByte();
				num6 = this.a.ReadByte();
				num7 = this.a.ReadByte();
				num8 = this.a.ReadByte();
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
			d0.a(num | num2 | num3 | num4 | num5 | num6 | num7 | num8);
			return ((long)num8 << 56) + ((long)num7 << 48) + ((long)num6 << 40) + ((long)num5 << 32) + ((long)num4 << 24) + (long)((long)num3 << 16) + (long)((long)num2 << 8) + (long)num;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0007D878 File Offset: 0x0007C878
		public short az()
		{
			return (short)this.a1();
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0007D884 File Offset: 0x0007C884
		public int a1()
		{
			int num;
			int num2;
			try
			{
				num = this.a.ReadByte();
				num2 = this.a.ReadByte();
			}
			catch (IOException a_)
			{
				throw new RuntimeException(a_);
			}
			d0.a(num | num2);
			return (num2 << 8) + num;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0007D8D0 File Offset: 0x0007C8D0
		private static void a(int A_0)
		{
			if (A_0 < 0)
			{
				throw new RuntimeException("Unexpected end-of-file");
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0007D8E1 File Offset: 0x0007C8E1
		public void ay(byte[] A_0)
		{
			this.av(A_0, 0, A_0.Length);
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0007D8F0 File Offset: 0x0007C8F0
		public void av(byte[] A_0, int A_1, int A_2)
		{
			int num = A_1 + A_2;
			for (int i = A_1; i < num; i++)
			{
				byte b;
				try
				{
					b = (byte)this.a.ReadByte();
				}
				catch (IOException a_)
				{
					throw new RuntimeException(a_);
				}
				d0.a((int)b);
				A_0[i] = b;
			}
		}

		// Token: 0x0400136B RID: 4971
		private Stream a;
	}
}
