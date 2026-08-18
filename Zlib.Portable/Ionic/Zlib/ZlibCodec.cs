using System;

namespace Ionic.Zlib
{
	// Token: 0x0200001C RID: 28
	public sealed class ZlibCodec
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000A5F8 File Offset: 0x000087F8
		public int Adler32
		{
			get
			{
				return (int)this._Adler32;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000A600 File Offset: 0x00008800
		public ZlibCodec()
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000A618 File Offset: 0x00008818
		public ZlibCodec(CompressionMode mode)
		{
			if (mode == CompressionMode.Compress)
			{
				if (this.InitializeDeflate() != 0)
				{
					throw new ZlibException("Cannot initialize for deflate.");
				}
			}
			else
			{
				if (mode != CompressionMode.Decompress)
				{
					throw new ZlibException("Invalid ZlibStreamFlavor.");
				}
				if (this.InitializeInflate() != 0)
				{
					throw new ZlibException("Cannot initialize for inflate.");
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000A672 File Offset: 0x00008872
		public int InitializeInflate()
		{
			return this.InitializeInflate(this.WindowBits);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000A680 File Offset: 0x00008880
		public int InitializeInflate(bool expectRfc1950Header)
		{
			return this.InitializeInflate(this.WindowBits, expectRfc1950Header);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000A68F File Offset: 0x0000888F
		public int InitializeInflate(int windowBits)
		{
			this.WindowBits = windowBits;
			return this.InitializeInflate(windowBits, true);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public int InitializeInflate(int windowBits, bool expectRfc1950Header)
		{
			this.WindowBits = windowBits;
			if (this.dstate != null)
			{
				throw new ZlibException("You may not call InitializeInflate() after calling InitializeDeflate().");
			}
			this.istate = new InflateManager(expectRfc1950Header);
			return this.istate.Initialize(this, windowBits);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000A6D5 File Offset: 0x000088D5
		public int Inflate(FlushType flush)
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Inflate(flush);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000A6F6 File Offset: 0x000088F6
		public int EndInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			int result = this.istate.End();
			this.istate = null;
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000A71D File Offset: 0x0000891D
		public int SyncInflate()
		{
			if (this.istate == null)
			{
				throw new ZlibException("No Inflate State!");
			}
			return this.istate.Sync();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000A73D File Offset: 0x0000893D
		public int InitializeDeflate()
		{
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000A746 File Offset: 0x00008946
		public int InitializeDeflate(CompressionLevel level)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000A756 File Offset: 0x00008956
		public int InitializeDeflate(CompressionLevel level, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000A766 File Offset: 0x00008966
		public int InitializeDeflate(CompressionLevel level, int bits)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(true);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000A77D File Offset: 0x0000897D
		public int InitializeDeflate(CompressionLevel level, int bits, bool wantRfc1950Header)
		{
			this.CompressLevel = level;
			this.WindowBits = bits;
			return this._InternalInitializeDeflate(wantRfc1950Header);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000A794 File Offset: 0x00008994
		private int _InternalInitializeDeflate(bool wantRfc1950Header)
		{
			if (this.istate != null)
			{
				throw new ZlibException("You may not call InitializeDeflate() after calling InitializeInflate().");
			}
			this.dstate = new DeflateManager();
			this.dstate.WantRfc1950HeaderBytes = wantRfc1950Header;
			return this.dstate.Initialize(this, this.CompressLevel, this.WindowBits, this.Strategy);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A7E9 File Offset: 0x000089E9
		public int Deflate(FlushType flush)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.Deflate(flush);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A80A File Offset: 0x00008A0A
		public int EndDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate = null;
			return 0;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A827 File Offset: 0x00008A27
		public void ResetDeflate()
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			this.dstate.Reset();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A847 File Offset: 0x00008A47
		public int SetDeflateParams(CompressionLevel level, CompressionStrategy strategy)
		{
			if (this.dstate == null)
			{
				throw new ZlibException("No Deflate State!");
			}
			return this.dstate.SetParams(level, strategy);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A869 File Offset: 0x00008A69
		public int SetDictionary(byte[] dictionary)
		{
			if (this.istate != null)
			{
				return this.istate.SetDictionary(dictionary);
			}
			if (this.dstate != null)
			{
				return this.dstate.SetDictionary(dictionary);
			}
			throw new ZlibException("No Inflate or Deflate state!");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		internal void flush_pending()
		{
			int num = this.dstate.pendingCount;
			if (num > this.AvailableBytesOut)
			{
				num = this.AvailableBytesOut;
			}
			if (num == 0)
			{
				return;
			}
			if (this.dstate.pending.Length <= this.dstate.nextPending || this.OutputBuffer.Length <= this.NextOut || this.dstate.pending.Length < this.dstate.nextPending + num || this.OutputBuffer.Length < this.NextOut + num)
			{
				throw new ZlibException(string.Format("Invalid State. (pending.Length={0}, pendingCount={1})", new object[]
				{
					this.dstate.pending.Length,
					this.dstate.pendingCount
				}));
			}
			Array.Copy(this.dstate.pending, this.dstate.nextPending, this.OutputBuffer, this.NextOut, num);
			this.NextOut += num;
			this.dstate.nextPending += num;
			this.TotalBytesOut += (long)num;
			this.AvailableBytesOut -= num;
			this.dstate.pendingCount -= num;
			if (this.dstate.pendingCount == 0)
			{
				this.dstate.nextPending = 0;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.AvailableBytesIn;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.AvailableBytesIn -= num;
			if (this.dstate.WantRfc1950HeaderBytes)
			{
				this._Adler32 = Adler.Adler32(this._Adler32, this.InputBuffer, this.NextIn, num);
			}
			Array.Copy(this.InputBuffer, this.NextIn, buf, start, num);
			this.NextIn += num;
			this.TotalBytesIn += (long)num;
			return num;
		}

		// Token: 0x0400013D RID: 317
		public byte[] InputBuffer;

		// Token: 0x0400013E RID: 318
		public int NextIn;

		// Token: 0x0400013F RID: 319
		public int AvailableBytesIn;

		// Token: 0x04000140 RID: 320
		public long TotalBytesIn;

		// Token: 0x04000141 RID: 321
		public byte[] OutputBuffer;

		// Token: 0x04000142 RID: 322
		public int NextOut;

		// Token: 0x04000143 RID: 323
		public int AvailableBytesOut;

		// Token: 0x04000144 RID: 324
		public long TotalBytesOut;

		// Token: 0x04000145 RID: 325
		public string Message;

		// Token: 0x04000146 RID: 326
		internal DeflateManager dstate;

		// Token: 0x04000147 RID: 327
		internal InflateManager istate;

		// Token: 0x04000148 RID: 328
		internal uint _Adler32;

		// Token: 0x04000149 RID: 329
		public CompressionLevel CompressLevel = CompressionLevel.Default;

		// Token: 0x0400014A RID: 330
		public int WindowBits = 15;

		// Token: 0x0400014B RID: 331
		public CompressionStrategy Strategy;
	}
}
