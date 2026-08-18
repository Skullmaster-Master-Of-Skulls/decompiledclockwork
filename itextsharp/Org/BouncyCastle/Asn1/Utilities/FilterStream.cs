using System;
using System.IO;

namespace Org.BouncyCastle.Asn1.Utilities
{
	// Token: 0x020000B5 RID: 181
	public class FilterStream : Stream
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x0001CF94 File Offset: 0x0001BF94
		public FilterStream(Stream s)
		{
			this.s = s;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001CFA3 File Offset: 0x0001BFA3
		public override bool CanRead
		{
			get
			{
				return this.s.CanRead;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001CFB0 File Offset: 0x0001BFB0
		public override bool CanSeek
		{
			get
			{
				return this.s.CanSeek;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001CFBD File Offset: 0x0001BFBD
		public override bool CanWrite
		{
			get
			{
				return this.s.CanWrite;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0001CFCA File Offset: 0x0001BFCA
		public override long Length
		{
			get
			{
				return this.s.Length;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001CFD7 File Offset: 0x0001BFD7
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0001CFE4 File Offset: 0x0001BFE4
		public override long Position
		{
			get
			{
				return this.s.Position;
			}
			set
			{
				this.s.Position = value;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001CFF2 File Offset: 0x0001BFF2
		public override void Close()
		{
			this.s.Close();
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001CFFF File Offset: 0x0001BFFF
		public override void Flush()
		{
			this.s.Flush();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001D00C File Offset: 0x0001C00C
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.s.Seek(offset, origin);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001D01B File Offset: 0x0001C01B
		public override void SetLength(long value)
		{
			this.s.SetLength(value);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001D029 File Offset: 0x0001C029
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.s.Read(buffer, offset, count);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001D039 File Offset: 0x0001C039
		public override int ReadByte()
		{
			return this.s.ReadByte();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001D046 File Offset: 0x0001C046
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.s.Write(buffer, offset, count);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001D056 File Offset: 0x0001C056
		public override void WriteByte(byte value)
		{
			this.s.WriteByte(value);
		}

		// Token: 0x040002BC RID: 700
		private readonly Stream s;
	}
}
