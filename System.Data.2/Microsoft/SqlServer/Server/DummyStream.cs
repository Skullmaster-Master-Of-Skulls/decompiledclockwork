using System;
using System.Data;
using System.IO;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000070 RID: 112
	internal sealed class DummyStream : Stream
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x00047700 File Offset: 0x00046B00
		private void DontDoIt()
		{
			throw new Exception(Res.GetString("Sql_InternalError"));
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0004771C File Offset: 0x00046B1C
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0004772C File Offset: 0x00046B2C
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0004773C File Offset: 0x00046B3C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0004774C File Offset: 0x00046B4C
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00047760 File Offset: 0x00046B60
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00047774 File Offset: 0x00046B74
		public override long Length
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00047788 File Offset: 0x00046B88
		public override void SetLength(long value)
		{
			this.m_size = value;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0004779C File Offset: 0x00046B9C
		public override long Seek(long value, SeekOrigin loc)
		{
			this.DontDoIt();
			return -1L;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000477B4 File Offset: 0x00046BB4
		public override void Flush()
		{
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000477C4 File Offset: 0x00046BC4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.DontDoIt();
			return -1;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000477D8 File Offset: 0x00046BD8
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_size += (long)count;
		}

		// Token: 0x040001ED RID: 493
		private long m_size;
	}
}
