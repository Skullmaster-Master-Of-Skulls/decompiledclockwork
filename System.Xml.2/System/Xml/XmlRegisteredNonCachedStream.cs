using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000080 RID: 128
	internal class XmlRegisteredNonCachedStream : Stream
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x000122B4 File Offset: 0x000104B4
		internal XmlRegisteredNonCachedStream(Stream stream, XmlDownloadManager downloadManager, string host)
		{
			this.stream = stream;
			this.downloadManager = downloadManager;
			this.host = host;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000122D4 File Offset: 0x000104D4
		~XmlRegisteredNonCachedStream()
		{
			if (this.downloadManager != null)
			{
				this.downloadManager.Remove(this.host);
			}
			this.stream = null;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001231C File Offset: 0x0001051C
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

		// Token: 0x060004BA RID: 1210 RVA: 0x00012380 File Offset: 0x00010580
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00012394 File Offset: 0x00010594
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000123A8 File Offset: 0x000105A8
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.stream.EndRead(asyncResult);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x000123B6 File Offset: 0x000105B6
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.stream.EndWrite(asyncResult);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000123C4 File Offset: 0x000105C4
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000123D1 File Offset: 0x000105D1
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000123E1 File Offset: 0x000105E1
		public override int ReadByte()
		{
			return this.stream.ReadByte();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000123EE File Offset: 0x000105EE
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000123FD File Offset: 0x000105FD
		public override void SetLength(long value)
		{
			this.stream.SetLength(value);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001240B File Offset: 0x0001060B
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001241B File Offset: 0x0001061B
		public override void WriteByte(byte value)
		{
			this.stream.WriteByte(value);
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00012429 File Offset: 0x00010629
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00012436 File Offset: 0x00010636
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00012443 File Offset: 0x00010643
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00012450 File Offset: 0x00010650
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001245D File Offset: 0x0001065D
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0001246A File Offset: 0x0001066A
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

		// Token: 0x040001EF RID: 495
		protected Stream stream;

		// Token: 0x040001F0 RID: 496
		private XmlDownloadManager downloadManager;

		// Token: 0x040001F1 RID: 497
		private string host;
	}
}
