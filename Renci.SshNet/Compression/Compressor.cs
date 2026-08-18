using System;
using System.IO;
using Renci.SshNet.Security;

namespace Renci.SshNet.Compression
{
	// Token: 0x020000DF RID: 223
	public abstract class Compressor : Algorithm, IDisposable
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x000201DB File Offset: 0x0001E3DB
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x000201E3 File Offset: 0x0001E3E3
		protected bool IsActive { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x000201EC File Offset: 0x0001E3EC
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x000201F4 File Offset: 0x0001E3F4
		private protected Session Session { protected get; private set; }

		// Token: 0x06000998 RID: 2456 RVA: 0x00020200 File Offset: 0x0001E400
		protected Compressor()
		{
			this._compressorStream = new MemoryStream();
			this._decompressorStream = new MemoryStream();
			this._compressor = new ZlibStream(this._compressorStream, CompressionMode.Compress);
			this._decompressor = new ZlibStream(this._decompressorStream, CompressionMode.Decompress);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002024D File Offset: 0x0001E44D
		public virtual void Init(Session session)
		{
			this.Session = session;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00020256 File Offset: 0x0001E456
		public virtual byte[] Compress(byte[] data)
		{
			return this.Compress(data, 0, data.Length);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00020264 File Offset: 0x0001E464
		public virtual byte[] Compress(byte[] data, int offset, int length)
		{
			if (this.IsActive)
			{
				this._compressorStream.SetLength(0L);
				this._compressor.Write(data, offset, length);
				return this._compressorStream.ToArray();
			}
			if (offset == 0 && length == data.Length)
			{
				return data;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(data, offset, array, 0, length);
			return array;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x000202BD File Offset: 0x0001E4BD
		public virtual byte[] Decompress(byte[] data)
		{
			return this.Decompress(data, 0, data.Length);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x000202CC File Offset: 0x0001E4CC
		public virtual byte[] Decompress(byte[] data, int offset, int length)
		{
			if (this.IsActive)
			{
				this._decompressorStream.SetLength(0L);
				this._decompressor.Write(data, offset, length);
				return this._decompressorStream.ToArray();
			}
			if (offset == 0 && length == data.Length)
			{
				return data;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(data, offset, array, 0, length);
			return array;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00020325 File Offset: 0x0001E525
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00020334 File Offset: 0x0001E534
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				MemoryStream compressorStream = this._compressorStream;
				if (compressorStream != null)
				{
					compressorStream.Dispose();
					this._compressorStream = null;
				}
				MemoryStream decompressorStream = this._decompressorStream;
				if (decompressorStream != null)
				{
					decompressorStream.Dispose();
					this._decompressorStream = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00020384 File Offset: 0x0001E584
		~Compressor()
		{
			this.Dispose(false);
		}

		// Token: 0x040003BD RID: 957
		private readonly ZlibStream _compressor;

		// Token: 0x040003BE RID: 958
		private readonly ZlibStream _decompressor;

		// Token: 0x040003BF RID: 959
		private MemoryStream _compressorStream;

		// Token: 0x040003C0 RID: 960
		private MemoryStream _decompressorStream;

		// Token: 0x040003C3 RID: 963
		private bool _isDisposed;
	}
}
