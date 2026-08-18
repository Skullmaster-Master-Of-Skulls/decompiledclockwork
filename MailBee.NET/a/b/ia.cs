using System;
using System.IO;

namespace a.b
{
	// Token: 0x0200030A RID: 778
	internal class ia : Stream
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x0007A8DF File Offset: 0x000798DF
		public ia(Stream A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0007A8EE File Offset: 0x000798EE
		public int c()
		{
			return (int)(this.a.Length - this.a.Position);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0007A908 File Offset: 0x00079908
		public void d()
		{
			this.a.Close();
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0007A915 File Offset: 0x00079915
		public void a(int A_0)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0007A91C File Offset: 0x0007991C
		public bool b()
		{
			return false;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0007A91F File Offset: 0x0007991F
		public int e()
		{
			return this.a.ReadByte();
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0007A92C File Offset: 0x0007992C
		public int a(byte[] A_0)
		{
			int i = 0;
			int num = 4611;
			while (i < A_0.Length)
			{
				num = this.a.ReadByte();
				if (num == -1)
				{
					break;
				}
				A_0[i++] = (byte)num;
			}
			if (i == 0 && num == -1)
			{
				return -1;
			}
			return i;
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x0007A96C File Offset: 0x0007996C
		public override int Read(byte[] bf, int s, int l)
		{
			return this.a.Read(bf, s, l);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x0007A97C File Offset: 0x0007997C
		public void a()
		{
			this.a.Seek(0L, SeekOrigin.Begin);
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0007A98D File Offset: 0x0007998D
		public long a(long A_0)
		{
			return this.a.Seek(A_0, SeekOrigin.Begin);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0007A99C File Offset: 0x0007999C
		public override bool get_CanRead()
		{
			return true;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0007A99F File Offset: 0x0007999F
		public override bool get_CanSeek()
		{
			return true;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0007A9A2 File Offset: 0x000799A2
		public override bool get_CanWrite()
		{
			return false;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0007A9A5 File Offset: 0x000799A5
		public override void Flush()
		{
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0007A9A7 File Offset: 0x000799A7
		public override long get_Length()
		{
			return this.a.Length;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0007A9B4 File Offset: 0x000799B4
		public override long get_Position()
		{
			return this.a.Position;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0007A9C1 File Offset: 0x000799C1
		public override void set_Position(long value)
		{
			this.a.Position = value;
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0007A9CF File Offset: 0x000799CF
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.a.Seek(offset, origin);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0007A9DE File Offset: 0x000799DE
		public override void SetLength(long value)
		{
			this.a.SetLength(value);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0007A9EC File Offset: 0x000799EC
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001341 RID: 4929
		protected Stream a;
	}
}
