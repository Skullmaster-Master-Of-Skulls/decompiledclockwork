using System;
using System.IO;
using System.Text;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x0200003F RID: 63
	internal class ZipContainer
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0001126E File Offset: 0x0000F46E
		public ZipContainer(object o)
		{
			this._zf = (o as ZipFile);
			this._zos = (o as ZipOutputStream);
			this._zis = (o as ZipInputStream);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001129A File Offset: 0x0000F49A
		public ZipFile ZipFile
		{
			get
			{
				return this._zf;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000112A2 File Offset: 0x0000F4A2
		public ZipOutputStream ZipOutputStream
		{
			get
			{
				return this._zos;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000112AA File Offset: 0x0000F4AA
		public string Name
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Name;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos.Name;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000112D9 File Offset: 0x0000F4D9
		public string Password
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._Password;
				}
				if (this._zis != null)
				{
					return this._zis._Password;
				}
				return this._zos._password;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0001130E File Offset: 0x0000F50E
		public Zip64Option Zip64
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf._zip64;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return this._zos._zip64;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0001133D File Offset: 0x0000F53D
		public int BufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.BufferSize;
				}
				if (this._zis != null)
				{
					throw new NotSupportedException();
				}
				return 0;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00011362 File Offset: 0x0000F562
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0001138D File Offset: 0x0000F58D
		public ParallelDeflateOutputStream ParallelDeflater
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflater;
				}
				if (this._zis != null)
				{
					return null;
				}
				return this._zos.ParallelDeflater;
			}
			set
			{
				if (this._zf != null)
				{
					this._zf.ParallelDeflater = value;
					return;
				}
				if (this._zos != null)
				{
					this._zos.ParallelDeflater = value;
				}
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public long ParallelDeflateThreshold
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflateThreshold;
				}
				return this._zos.ParallelDeflateThreshold;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000113D9 File Offset: 0x0000F5D9
		public int ParallelDeflateMaxBufferPairs
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ParallelDeflateMaxBufferPairs;
				}
				return this._zos.ParallelDeflateMaxBufferPairs;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000113FA File Offset: 0x0000F5FA
		public int CodecBufferSize
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.CodecBufferSize;
				}
				if (this._zis != null)
				{
					return this._zis.CodecBufferSize;
				}
				return this._zos.CodecBufferSize;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0001142F File Offset: 0x0000F62F
		public CompressionStrategy Strategy
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.Strategy;
				}
				return this._zos.Strategy;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00011450 File Offset: 0x0000F650
		public Zip64Option UseZip64WhenSaving
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.UseZip64WhenSaving;
				}
				return this._zos.EnableZip64;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00011471 File Offset: 0x0000F671
		public Encoding AlternateEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncoding;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncoding;
				}
				return null;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0001149C File Offset: 0x0000F69C
		public Encoding DefaultEncoding
		{
			get
			{
				if (this._zf != null)
				{
					return ZipFile.DefaultEncoding;
				}
				if (this._zos != null)
				{
					return ZipOutputStream.DefaultEncoding;
				}
				return null;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000114BB File Offset: 0x0000F6BB
		public ZipOption AlternateEncodingUsage
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.AlternateEncodingUsage;
				}
				if (this._zos != null)
				{
					return this._zos.AlternateEncodingUsage;
				}
				return ZipOption.Default;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600031B RID: 795 RVA: 0x000114E6 File Offset: 0x0000F6E6
		public Stream ReadStream
		{
			get
			{
				if (this._zf != null)
				{
					return this._zf.ReadStream;
				}
				return this._zis.ReadStream;
			}
		}

		// Token: 0x04000197 RID: 407
		private ZipFile _zf;

		// Token: 0x04000198 RID: 408
		private ZipOutputStream _zos;

		// Token: 0x04000199 RID: 409
		private ZipInputStream _zis;
	}
}
