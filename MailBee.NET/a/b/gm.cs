using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002C3 RID: 707
	internal class gm : Stream
	{
		// Token: 0x0600186C RID: 6252 RVA: 0x0006ED5D File Offset: 0x0006DD5D
		public gm(Stream A_0)
		{
			this.a = A_0;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0006ED6C File Offset: 0x0006DD6C
		public int a()
		{
			return this.a.ReadByte();
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0006ED79 File Offset: 0x0006DD79
		public override int Read(byte[] b, int off, int len)
		{
			return this.a.Read(b, off, len);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0006ED89 File Offset: 0x0006DD89
		public override void Close()
		{
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0006ED8B File Offset: 0x0006DD8B
		public override void Flush()
		{
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x0006ED8D File Offset: 0x0006DD8D
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0006ED91 File Offset: 0x0006DD91
		public override void SetLength(long value)
		{
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0006ED93 File Offset: 0x0006DD93
		public override bool get_CanRead()
		{
			return true;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0006ED96 File Offset: 0x0006DD96
		public override bool get_CanSeek()
		{
			return true;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0006ED99 File Offset: 0x0006DD99
		public override bool get_CanWrite()
		{
			return false;
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x0006ED9C File Offset: 0x0006DD9C
		public override long get_Length()
		{
			return this.a.Length;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x0006EDA9 File Offset: 0x0006DDA9
		public override long get_Position()
		{
			return this.a.Position;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0006EDB6 File Offset: 0x0006DDB6
		public override void set_Position(long value)
		{
			this.a.Position = (long)Convert.ToInt32(value);
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0006EDCA File Offset: 0x0006DDCA
		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		// Token: 0x04001237 RID: 4663
		private Stream a;
	}
}
