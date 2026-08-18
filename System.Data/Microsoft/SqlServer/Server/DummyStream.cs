using System;
using System.Data;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200029C RID: 668
	internal sealed class DummyStream : Stream
	{
		// Token: 0x06002273 RID: 8819 RVA: 0x0028C018 File Offset: 0x0028B418
		private void DontDoIt()
		{
			throw new Exception(Res.GetString("Sql_InternalError"));
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x0028C038 File Offset: 0x0028B438
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x0028C048 File Offset: 0x0028B448
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x0028C058 File Offset: 0x0028B458
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x0028C068 File Offset: 0x0028B468
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x0028C088 File Offset: 0x0028B488
		public override long Position
		{
			get
			{
				return this.m_size;
			}
			set
			{
				this.m_size = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x0028C0A8 File Offset: 0x0028B4A8
		public override long Length
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x0028C0C8 File Offset: 0x0028B4C8
		public override void SetLength(long value)
		{
			this.m_size = value;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x0028C0E8 File Offset: 0x0028B4E8
		public override long Seek(long value, SeekOrigin loc)
		{
			this.DontDoIt();
			return -1L;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0028C108 File Offset: 0x0028B508
		public override void Flush()
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x0028C118 File Offset: 0x0028B518
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.DontDoIt();
			return -1;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0028C138 File Offset: 0x0028B538
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_size += (long)count;
		}

		// Token: 0x04001669 RID: 5737
		private long m_size;
	}
}
