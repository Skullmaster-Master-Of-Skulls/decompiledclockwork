using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000031 RID: 49
	internal class XmlRegisteredNonCachedStream : Stream
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00007404 File Offset: 0x00006404
		internal XmlRegisteredNonCachedStream(Stream stream, XmlDownloadManager downloadManager, string host)
		{
			this.stream = stream;
			this.downloadManager = downloadManager;
			this.host = host;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007424 File Offset: 0x00006424
		~XmlRegisteredNonCachedStream()
		{
			if (this.downloadManager != null)
			{
				this.downloadManager.Remove(this.host);
			}
			this.stream = null;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000746C File Offset: 0x0000646C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.stream != null)
				{
					if (this.downloadManager != null)
					{
						this.downloadManager.Remove(this.host);
					}
					this.stream.Close();
				}
				this.stream = null;
				GC.SuppressFinalize(this);
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000074D0 File Offset: 0x000064D0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000074E4 File Offset: 0x000064E4
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000074F3 File Offset: 0x000064F3
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007501 File Offset: 0x00006501
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000750F File Offset: 0x0000650F
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000751C File Offset: 0x0000651C
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000752C File Offset: 0x0000652C
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007539 File Offset: 0x00006539
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00007548 File Offset: 0x00006548
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007556 File Offset: 0x00006556
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007566 File Offset: 0x00006566
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007574 File Offset: 0x00006574
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007581 File Offset: 0x00006581
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000758E File Offset: 0x0000658E
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000759B File Offset: 0x0000659B
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000075A8 File Offset: 0x000065A8
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000075B5 File Offset: 0x000065B5
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

		// Token: 0x040004B6 RID: 1206
		protected Stream stream;

		// Token: 0x040004B7 RID: 1207
		private XmlDownloadManager downloadManager;

		// Token: 0x040004B8 RID: 1208
		private string host;
	}
}
