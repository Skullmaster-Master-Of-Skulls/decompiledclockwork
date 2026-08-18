using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002C9 RID: 713
	internal class e5 : Stream
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x0006F140 File Offset: 0x0006E140
		public e5()
		{
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0006F148 File Offset: 0x0006E148
		public e5(byte[] A_0)
		{
			this.a = A_0;
			this.b = 0;
			this.d = A_0.Length;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0006F167 File Offset: 0x0006E167
		public e5(byte[] A_0, int A_1, int A_2)
		{
			this.a = A_0;
			this.b = A_1;
			this.d = Math.Min(A_1 + A_2, A_0.Length);
			this.c = A_1;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0006F198 File Offset: 0x0006E198
		public virtual int @as()
		{
			int num2;
			lock (this)
			{
				int num;
				if (this.b >= this.d)
				{
					num = -1;
				}
				else
				{
					byte[] array = this.a;
					num2 = this.b;
					this.b = num2 + 1;
					num = (array[num2] & 255);
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0006F200 File Offset: 0x0006E200
		public override int Read(byte[] b, int off, int len)
		{
			int result;
			lock (this)
			{
				if (b == null)
				{
					throw new NullReferenceException();
				}
				if (off < 0 || len < 0 || len > b.Length - off)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.b >= this.d)
				{
					result = -1;
				}
				else
				{
					int num = this.d - this.b;
					if (len > num)
					{
						len = num;
					}
					if (len <= 0)
					{
						result = 0;
					}
					else
					{
						Array.Copy(this.a, this.b, b, off, len);
						this.b += len;
						result = len;
					}
				}
			}
			return result;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0006F2A8 File Offset: 0x0006E2A8
		public virtual int aq()
		{
			return this.d - this.b;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0006F2B7 File Offset: 0x0006E2B7
		public virtual bool cc()
		{
			return true;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0006F2BA File Offset: 0x0006E2BA
		public virtual void ar(int A_0)
		{
			this.c = this.b;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0006F2C8 File Offset: 0x0006E2C8
		public virtual void at()
		{
			this.b = this.c;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0006F2D6 File Offset: 0x0006E2D6
		public override void Close()
		{
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0006F2D8 File Offset: 0x0006E2D8
		public override bool get_CanRead()
		{
			return true;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0006F2DB File Offset: 0x0006E2DB
		public override bool get_CanWrite()
		{
			return false;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0006F2DE File Offset: 0x0006E2DE
		public override bool get_CanSeek()
		{
			return true;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0006F2E1 File Offset: 0x0006E2E1
		public override void Flush()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0006F2E8 File Offset: 0x0006E2E8
		public override long get_Length()
		{
			return (long)this.d;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0006F2F1 File Offset: 0x0006E2F1
		public override long get_Position()
		{
			return (long)this.b;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0006F2FA File Offset: 0x0006E2FA
		public override void set_Position(long value)
		{
			this.b = (int)value;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0006F304 File Offset: 0x0006E304
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException();
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (0L > offset)
				{
					throw new ArgumentOutOfRangeException("offset", "offset must be positive");
				}
				this.Position = ((offset < this.Length) ? offset : this.Length);
				break;
			case SeekOrigin.Current:
				this.Position = ((this.Position + offset < this.Length) ? (this.Position + offset) : this.Length);
				break;
			case SeekOrigin.End:
				this.Position = this.Length;
				break;
			default:
				throw new ArgumentException("incorrect SeekOrigin", "origin");
			}
			return this.Position;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0006F3AE File Offset: 0x0006E3AE
		public override void SetLength(long value)
		{
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0006F3B0 File Offset: 0x0006E3B0
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001241 RID: 4673
		protected byte[] a;

		// Token: 0x04001242 RID: 4674
		protected int b;

		// Token: 0x04001243 RID: 4675
		protected int c;

		// Token: 0x04001244 RID: 4676
		protected int d;
	}
}
