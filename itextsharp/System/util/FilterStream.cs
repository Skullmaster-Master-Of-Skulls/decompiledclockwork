using System;
using System.IO;

namespace System.util
{
	// Token: 0x020000F6 RID: 246
	public class FilterStream : Stream
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x00032A94 File Offset: 0x00031A94
		public FilterStream(Stream s)
		{
			this.s = s;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00032AA3 File Offset: 0x00031AA3
		public override bool CanRead
		{
			get
			{
				return this.s.CanRead;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00032AB0 File Offset: 0x00031AB0
		public override bool CanSeek
		{
			get
			{
				return this.s.CanSeek;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00032ABD File Offset: 0x00031ABD
		public override bool CanWrite
		{
			get
			{
				return this.s.CanWrite;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00032ACA File Offset: 0x00031ACA
		public override long Length
		{
			get
			{
				return this.s.Length;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00032AD7 File Offset: 0x00031AD7
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00032AE4 File Offset: 0x00031AE4
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

		// Token: 0x060009CA RID: 2506 RVA: 0x00032AF2 File Offset: 0x00031AF2
		public override void Close()
		{
			this.s.Close();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00032AFF File Offset: 0x00031AFF
		public override void Flush()
		{
			this.s.Flush();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00032B0C File Offset: 0x00031B0C
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.s.Seek(offset, origin);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00032B1B File Offset: 0x00031B1B
		public override void SetLength(long value)
		{
			this.s.SetLength(value);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00032B29 File Offset: 0x00031B29
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.s.Read(buffer, offset, count);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00032B39 File Offset: 0x00031B39
		public override int ReadByte()
		{
			return this.s.ReadByte();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00032B46 File Offset: 0x00031B46
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.s.Write(buffer, offset, count);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00032B56 File Offset: 0x00031B56
		public override void WriteByte(byte value)
		{
			this.s.WriteByte(value);
		}

		// Token: 0x04000804 RID: 2052
		private readonly Stream s;
	}
}
