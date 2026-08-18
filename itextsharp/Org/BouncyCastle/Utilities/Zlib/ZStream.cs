using System;

namespace Org.BouncyCastle.Utilities.Zlib
{
	// Token: 0x02000337 RID: 823
	public sealed class ZStream
	{
		// Token: 0x06001DA5 RID: 7589 RVA: 0x000B1F47 File Offset: 0x000B0F47
		public int inflateInit()
		{
			return this.inflateInit(15);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x000B1F51 File Offset: 0x000B0F51
		public int inflateInit(bool nowrap)
		{
			return this.inflateInit(15, nowrap);
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x000B1F5C File Offset: 0x000B0F5C
		public int inflateInit(int w)
		{
			return this.inflateInit(w, false);
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x000B1F66 File Offset: 0x000B0F66
		public int inflateInit(int w, bool nowrap)
		{
			this.istate = new Inflate();
			return this.istate.inflateInit(this, nowrap ? (-w) : w);
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x000B1F87 File Offset: 0x000B0F87
		public int inflate(int f)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflate(this, f);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x000B1FA4 File Offset: 0x000B0FA4
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

		// Token: 0x06001DAB RID: 7595 RVA: 0x000B1FD1 File Offset: 0x000B0FD1
		public int inflateSync()
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSync(this);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000B1FEA File Offset: 0x000B0FEA
		public int inflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.istate == null)
			{
				return -2;
			}
			return this.istate.inflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000B2005 File Offset: 0x000B1005
		public int deflateInit(int level)
		{
			return this.deflateInit(level, 15);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x000B2010 File Offset: 0x000B1010
		public int deflateInit(int level, bool nowrap)
		{
			return this.deflateInit(level, 15, nowrap);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x000B201C File Offset: 0x000B101C
		public int deflateInit(int level, int bits)
		{
			return this.deflateInit(level, bits, false);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x000B2027 File Offset: 0x000B1027
		public int deflateInit(int level, int bits, bool nowrap)
		{
			this.dstate = new Deflate();
			return this.dstate.deflateInit(this, level, nowrap ? (-bits) : bits);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000B2049 File Offset: 0x000B1049
		public int deflate(int flush)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflate(this, flush);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x000B2064 File Offset: 0x000B1064
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

		// Token: 0x06001DB3 RID: 7603 RVA: 0x000B2090 File Offset: 0x000B1090
		public int deflateParams(int level, int strategy)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateParams(this, level, strategy);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x000B20AB File Offset: 0x000B10AB
		public int deflateSetDictionary(byte[] dictionary, int dictLength)
		{
			if (this.dstate == null)
			{
				return -2;
			}
			return this.dstate.deflateSetDictionary(this, dictionary, dictLength);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x000B20C8 File Offset: 0x000B10C8
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

		// Token: 0x06001DB6 RID: 7606 RVA: 0x000B21E0 File Offset: 0x000B11E0
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

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000B2270 File Offset: 0x000B1270
		public void free()
		{
			this.next_in = null;
			this.next_out = null;
			this.msg = null;
			this._adler = null;
		}

		// Token: 0x04001454 RID: 5204
		private const int MAX_WBITS = 15;

		// Token: 0x04001455 RID: 5205
		private const int DEF_WBITS = 15;

		// Token: 0x04001456 RID: 5206
		private const int Z_NO_FLUSH = 0;

		// Token: 0x04001457 RID: 5207
		private const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04001458 RID: 5208
		private const int Z_SYNC_FLUSH = 2;

		// Token: 0x04001459 RID: 5209
		private const int Z_FULL_FLUSH = 3;

		// Token: 0x0400145A RID: 5210
		private const int Z_FINISH = 4;

		// Token: 0x0400145B RID: 5211
		private const int MAX_MEM_LEVEL = 9;

		// Token: 0x0400145C RID: 5212
		private const int Z_OK = 0;

		// Token: 0x0400145D RID: 5213
		private const int Z_STREAM_END = 1;

		// Token: 0x0400145E RID: 5214
		private const int Z_NEED_DICT = 2;

		// Token: 0x0400145F RID: 5215
		private const int Z_ERRNO = -1;

		// Token: 0x04001460 RID: 5216
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04001461 RID: 5217
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04001462 RID: 5218
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04001463 RID: 5219
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04001464 RID: 5220
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04001465 RID: 5221
		public byte[] next_in;

		// Token: 0x04001466 RID: 5222
		public int next_in_index;

		// Token: 0x04001467 RID: 5223
		public int avail_in;

		// Token: 0x04001468 RID: 5224
		public long total_in;

		// Token: 0x04001469 RID: 5225
		public byte[] next_out;

		// Token: 0x0400146A RID: 5226
		public int next_out_index;

		// Token: 0x0400146B RID: 5227
		public int avail_out;

		// Token: 0x0400146C RID: 5228
		public long total_out;

		// Token: 0x0400146D RID: 5229
		public string msg;

		// Token: 0x0400146E RID: 5230
		internal Deflate dstate;

		// Token: 0x0400146F RID: 5231
		internal Inflate istate;

		// Token: 0x04001470 RID: 5232
		internal int data_type;

		// Token: 0x04001471 RID: 5233
		public long adler;

		// Token: 0x04001472 RID: 5234
		internal Adler32 _adler = new Adler32();
	}
}
