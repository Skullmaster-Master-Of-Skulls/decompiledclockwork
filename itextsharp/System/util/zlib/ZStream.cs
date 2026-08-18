using System;

namespace System.util.zlib
{
	// Token: 0x020001D3 RID: 467
	public sealed class ZStream
	{
		// Token: 0x0600122B RID: 4651 RVA: 0x000684C4 File Offset: 0x000674C4
		public int inflateInit()
		{
			return this.inflateInit(15);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000684CE File Offset: 0x000674CE
		public int inflateInit(bool nowrap)
		{
			return this.inflateInit(15, nowrap);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000684D9 File Offset: 0x000674D9
		public int inflateInit(int w)
		{
			return this.inflateInit(w, false);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000684E3 File Offset: 0x000674E3
		public int inflateInit(int w, bool nowrap)
		{
			this.istate = new Inflate();
			return this.istate.inflateInit(this, nowrap ? (-w) : w);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00068504 File Offset: 0x00067504
		public int inflate(int f)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflate(this, f);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00068520 File Offset: 0x00067520
		public int inflateEnd()
		{
			if (this.istate == null)
			{
				return -2;
			}
			int result = this.istate.inflateEnd(this);
			this.istate = null;
			return result;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0006854D File Offset: 0x0006754D
		public int inflateSync()
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSync(this);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00068566 File Offset: 0x00067566
		public int inflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00068581 File Offset: 0x00067581
		public int deflateInit(int level)
		{
			return this.deflateInit(level, 15);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0006858C File Offset: 0x0006758C
		public int deflateInit(int level, bool nowrap)
		{
			return this.deflateInit(level, 15, nowrap);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00068598 File Offset: 0x00067598
		public int deflateInit(int level, int bits)
		{
			return this.deflateInit(level, bits, false);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000685A3 File Offset: 0x000675A3
		public int deflateInit(int level, int bits, bool nowrap)
		{
			this.dstate = new Deflate();
			return this.dstate.deflateInit(this, level, nowrap ? (-bits) : bits);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000685C5 File Offset: 0x000675C5
		public int deflate(int flush)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflate(this, flush);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000685E0 File Offset: 0x000675E0
		public int deflateEnd()
		{
			if (this.dstate == null)
			{
				return -2;
			}
			int result = this.dstate.deflateEnd();
			this.dstate = null;
			return result;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0006860C File Offset: 0x0006760C
		public int deflateParams(int level, int strategy)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateParams(this, level, strategy);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00068627 File Offset: 0x00067627
		public int deflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00068644 File Offset: 0x00067644
		internal void flush_pending()
		{
			int pending = this.dstate.pending;
			if (pending > this.avail_out)
			{
				pending = this.avail_out;
			}
			if (pending == 0)
			{
				return;
			}
			if (this.dstate.pending_buf.Length > this.dstate.pending_out && this.next_out.Length > this.next_out_index && this.dstate.pending_buf.Length >= this.dstate.pending_out + pending)
			{
				int num = this.next_out.Length;
				int num2 = this.next_out_index + pending;
			}
			Array.Copy(this.dstate.pending_buf, this.dstate.pending_out, this.next_out, this.next_out_index, pending);
			this.next_out_index += pending;
			this.dstate.pending_out += pending;
			this.total_out += (long)pending;
			this.avail_out -= pending;
			this.dstate.pending -= pending;
			if (this.dstate.pending == 0)
			{
				this.dstate.pending_out = 0;
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0006875C File Offset: 0x0006775C
		internal int read_buf(byte[] buf, int start, int size)
		{
			int num = this.avail_in;
			if (num > size)
			{
				num = size;
			}
			if (num == 0)
			{
				return 0;
			}
			this.avail_in -= num;
			if (this.dstate.noheader == 0)
			{
				this.adler = this._adler.adler32(this.adler, this.next_in, this.next_in_index, num);
			}
			Array.Copy(this.next_in, this.next_in_index, buf, start, num);
			this.next_in_index += num;
			this.total_in += (long)num;
			return num;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000687EC File Offset: 0x000677EC
		public void free()
		{
			this.next_in = null;
			this.next_out = null;
			this.msg = null;
			this._adler = null;
		}

		// Token: 0x04000CC6 RID: 3270
		private const int MAX_WBITS = 15;

		// Token: 0x04000CC7 RID: 3271
		private const int DEF_WBITS = 15;

		// Token: 0x04000CC8 RID: 3272
		private const int Z_NO_FLUSH = 0;

		// Token: 0x04000CC9 RID: 3273
		private const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04000CCA RID: 3274
		private const int Z_SYNC_FLUSH = 2;

		// Token: 0x04000CCB RID: 3275
		private const int Z_FULL_FLUSH = 3;

		// Token: 0x04000CCC RID: 3276
		private const int Z_FINISH = 4;

		// Token: 0x04000CCD RID: 3277
		private const int MAX_MEM_LEVEL = 9;

		// Token: 0x04000CCE RID: 3278
		private const int Z_OK = 0;

		// Token: 0x04000CCF RID: 3279
		private const int Z_STREAM_END = 1;

		// Token: 0x04000CD0 RID: 3280
		private const int Z_NEED_DICT = 2;

		// Token: 0x04000CD1 RID: 3281
		private const int Z_ERRNO = -1;

		// Token: 0x04000CD2 RID: 3282
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04000CD3 RID: 3283
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04000CD4 RID: 3284
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04000CD5 RID: 3285
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04000CD6 RID: 3286
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04000CD7 RID: 3287
		public byte[] next_in;

		// Token: 0x04000CD8 RID: 3288
		public int next_in_index;

		// Token: 0x04000CD9 RID: 3289
		public int avail_in;

		// Token: 0x04000CDA RID: 3290
		public long total_in;

		// Token: 0x04000CDB RID: 3291
		public byte[] next_out;

		// Token: 0x04000CDC RID: 3292
		public int next_out_index;

		// Token: 0x04000CDD RID: 3293
		public int avail_out;

		// Token: 0x04000CDE RID: 3294
		public long total_out;

		// Token: 0x04000CDF RID: 3295
		public string msg;

		// Token: 0x04000CE0 RID: 3296
		internal Deflate dstate;

		// Token: 0x04000CE1 RID: 3297
		internal Inflate istate;

		// Token: 0x04000CE2 RID: 3298
		internal int data_type;

		// Token: 0x04000CE3 RID: 3299
		public long adler;

		// Token: 0x04000CE4 RID: 3300
		internal Adler32 _adler = new Adler32();
	}
}
