using System;
using System.IdentityModel.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace System.IdentityModel
{
	// Token: 0x02000041 RID: 65
	internal sealed class HashStream : Stream
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0000A51B File Offset: 0x0000871B
		public HashStream(HashAlgorithm hash)
		{
			if (hash == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("hash");
			}
			this.Reset(hash);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00002D09 File Offset: 0x00000F09
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00002D09 File Offset: 0x00000F09
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A53D File Offset: 0x0000873D
		public HashAlgorithm Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A545 File Offset: 0x00008745
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A545 File Offset: 0x00008745
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override long Position
		{
			get
			{
				return this.length;
			}
			set
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000024C1 File Offset: 0x000006C1
		public override void Flush()
		{
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A54D File Offset: 0x0000874D
		public void FlushHash()
		{
			this.FlushHash(null);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A556 File Offset: 0x00008756
		public void FlushHash(MemoryStream preCanonicalBytes)
		{
			this.hash.TransformFinalBlock(CryptoHelper.EmptyBuffer, 0, 0);
			if (DigestTraceRecordHelper.ShouldTraceDigest)
			{
				DigestTraceRecordHelper.TraceDigest(this.logStream, this.hash);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A583 File Offset: 0x00008783
		public byte[] FlushHashAndGetValue()
		{
			return this.FlushHashAndGetValue(null);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A58C File Offset: 0x0000878C
		public byte[] FlushHashAndGetValue(MemoryStream preCanonicalBytes)
		{
			this.FlushHash(preCanonicalBytes);
			return this.hash.Hash;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A5A0 File Offset: 0x000087A0
		public void Reset()
		{
			if (this.hashNeedsReset)
			{
				this.hash.Initialize();
				this.hashNeedsReset = false;
			}
			this.length = 0L;
			if (DigestTraceRecordHelper.ShouldTraceDigest)
			{
				this.logStream = new MemoryStream();
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A5D6 File Offset: 0x000087D6
		public void Reset(HashAlgorithm hash)
		{
			this.hash = hash;
			this.hashNeedsReset = false;
			this.length = 0L;
			if (DigestTraceRecordHelper.ShouldTraceDigest)
			{
				this.logStream = new MemoryStream();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A600 File Offset: 0x00008800
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.hash.TransformBlock(buffer, offset, count, buffer, offset);
			this.length += (long)count;
			this.hashNeedsReset = true;
			if (DigestTraceRecordHelper.ShouldTraceDigest)
			{
				this.logStream.Write(buffer, offset, count);
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override void SetLength(long length)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A63E File Offset: 0x0000883E
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.logStream != null)
			{
				this.logStream.Dispose();
				this.logStream = null;
			}
			this.disposed = true;
		}

		// Token: 0x04000171 RID: 369
		private HashAlgorithm hash;

		// Token: 0x04000172 RID: 370
		private long length;

		// Token: 0x04000173 RID: 371
		private bool disposed;

		// Token: 0x04000174 RID: 372
		private bool hashNeedsReset;

		// Token: 0x04000175 RID: 373
		private MemoryStream logStream;
	}
}
