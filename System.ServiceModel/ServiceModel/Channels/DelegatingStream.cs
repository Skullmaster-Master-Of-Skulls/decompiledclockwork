using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B9 RID: 2233
	internal abstract class DelegatingStream : Stream
	{
		// Token: 0x06005518 RID: 21784 RVA: 0x00138B92 File Offset: 0x00136D92
		protected DelegatingStream(Stream stream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.stream = stream;
		}

		// Token: 0x170014EE RID: 5358
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x00138BB4 File Offset: 0x00136DB4
		protected Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x0600551A RID: 21786 RVA: 0x00138BBC File Offset: 0x00136DBC
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x0600551B RID: 21787 RVA: 0x00138BC9 File Offset: 0x00136DC9
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x00138BD6 File Offset: 0x00136DD6
		public override bool CanTimeout
		{
			get
			{
				return this.stream.CanTimeout;
			}
		}

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x0600551D RID: 21789 RVA: 0x00138BE3 File Offset: 0x00136DE3
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x0600551E RID: 21790 RVA: 0x00138BF0 File Offset: 0x00136DF0
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x0600551F RID: 21791 RVA: 0x00138BFD File Offset: 0x00136DFD
		// (set) Token: 0x06005520 RID: 21792 RVA: 0x00138C0A File Offset: 0x00136E0A
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x06005521 RID: 21793 RVA: 0x00138C18 File Offset: 0x00136E18
		// (set) Token: 0x06005522 RID: 21794 RVA: 0x00138C25 File Offset: 0x00136E25
		public override int ReadTimeout
		{
			get
			{
				return this.stream.ReadTimeout;
			}
			set
			{
				this.stream.ReadTimeout = value;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x06005523 RID: 21795 RVA: 0x00138C33 File Offset: 0x00136E33
		// (set) Token: 0x06005524 RID: 21796 RVA: 0x00138C40 File Offset: 0x00136E40
		public override int WriteTimeout
		{
			get
			{
				return this.stream.WriteTimeout;
			}
			set
			{
				this.stream.WriteTimeout = value;
			}
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x00138C4E File Offset: 0x00136E4E
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x00138C62 File Offset: 0x00136E62
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06005527 RID: 21799 RVA: 0x00138C76 File Offset: 0x00136E76
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06005528 RID: 21800 RVA: 0x00138C83 File Offset: 0x00136E83
		public override int EndRead(IAsyncResult result)
		{
			return this.stream.EndRead(result);
		}

		// Token: 0x06005529 RID: 21801 RVA: 0x00138C91 File Offset: 0x00136E91
		public override void EndWrite(IAsyncResult result)
		{
			this.stream.EndWrite(result);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x00138C9F File Offset: 0x00136E9F
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600552B RID: 21803 RVA: 0x00138CAC File Offset: 0x00136EAC
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x0600552C RID: 21804 RVA: 0x00138CBC File Offset: 0x00136EBC
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x0600552D RID: 21805 RVA: 0x00138CC9 File Offset: 0x00136EC9
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x00138CD8 File Offset: 0x00136ED8
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x00138CE6 File Offset: 0x00136EE6
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06005530 RID: 21808 RVA: 0x00138CF6 File Offset: 0x00136EF6
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x0400335C RID: 13148
		private Stream stream;
	}
}
