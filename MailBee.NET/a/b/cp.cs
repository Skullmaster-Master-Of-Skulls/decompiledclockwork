using System;
using System.IO;

namespace a.b
{
	// Token: 0x0200032B RID: 811
	internal class cp : Stream
	{
		// Token: 0x06001D3B RID: 7483 RVA: 0x0007E8F0 File Offset: 0x0007D8F0
		public cp(Stream A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0007E906 File Offset: 0x0007D906
		protected override void Dispose(bool disposing)
		{
			this.b = null;
			base.Dispose(disposing);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0007E916 File Offset: 0x0007D916
		public override int ReadByte()
		{
			if (this.a != -1)
			{
				int result = this.a;
				this.a = -1;
				return result;
			}
			return this.b.ReadByte();
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0007E93A File Offset: 0x0007D93A
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.a != -1 && count > 0)
			{
				buffer[offset] = (byte)this.a;
				this.a = -1;
				return 1;
			}
			return this.b.Read(buffer, offset, count);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x0007E96A File Offset: 0x0007D96A
		public virtual void a(int A_0)
		{
			if (this.a != -1)
			{
				throw new InvalidOperationException("Can only push back one byte");
			}
			this.a = (A_0 & 255);
			this.b.Position -= (long)A_0;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x0007E9A1 File Offset: 0x0007D9A1
		public override bool get_CanRead()
		{
			return this.b.CanRead;
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x0007E9AE File Offset: 0x0007D9AE
		public override bool get_CanSeek()
		{
			return this.b.CanSeek;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0007E9BB File Offset: 0x0007D9BB
		public override bool get_CanWrite()
		{
			return this.b.CanWrite;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0007E9C8 File Offset: 0x0007D9C8
		public override long get_Length()
		{
			return this.b.Length;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0007E9D5 File Offset: 0x0007D9D5
		public override long get_Position()
		{
			return this.b.Position;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0007E9E2 File Offset: 0x0007D9E2
		public override void set_Position(long value)
		{
			this.b.Position = value;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0007E9F0 File Offset: 0x0007D9F0
		public override void Close()
		{
			this.b.Close();
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0007E9FD File Offset: 0x0007D9FD
		public override void Flush()
		{
			this.b.Flush();
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0007EA0A File Offset: 0x0007DA0A
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.b.Seek(offset, origin);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0007EA19 File Offset: 0x0007DA19
		public override void SetLength(long value)
		{
			this.b.SetLength(value);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0007EA27 File Offset: 0x0007DA27
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.b.Write(buffer, offset, count);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0007EA37 File Offset: 0x0007DA37
		public override void WriteByte(byte value)
		{
			this.b.WriteByte(value);
		}

		// Token: 0x04001380 RID: 4992
		private int a = -1;

		// Token: 0x04001381 RID: 4993
		private Stream b;
	}
}
