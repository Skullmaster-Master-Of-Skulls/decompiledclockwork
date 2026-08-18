using System;

namespace System.util.zlib
{
	// Token: 0x02000177 RID: 375
	public sealed class Deflate
	{
		// Token: 0x06000E86 RID: 3718 RVA: 0x00053938 File Offset: 0x00052938
		static Deflate()
		{
			Deflate.config_table = new Deflate.Config[10];
			Deflate.config_table[0] = new Deflate.Config(0, 0, 0, 0, 0);
			Deflate.config_table[1] = new Deflate.Config(4, 4, 8, 4, 1);
			Deflate.config_table[2] = new Deflate.Config(4, 5, 16, 8, 1);
			Deflate.config_table[3] = new Deflate.Config(4, 6, 32, 32, 1);
			Deflate.config_table[4] = new Deflate.Config(4, 4, 16, 16, 2);
			Deflate.config_table[5] = new Deflate.Config(8, 16, 32, 32, 2);
			Deflate.config_table[6] = new Deflate.Config(8, 16, 128, 128, 2);
			Deflate.config_table[7] = new Deflate.Config(8, 32, 128, 256, 2);
			Deflate.config_table[8] = new Deflate.Config(32, 128, 258, 1024, 2);
			Deflate.config_table[9] = new Deflate.Config(32, 258, 258, 4096, 2);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00053A90 File Offset: 0x00052A90
		internal Deflate()
		{
			this.dyn_ltree = new short[1146];
			this.dyn_dtree = new short[122];
			this.bl_tree = new short[78];
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00053B1C File Offset: 0x00052B1C
		internal void lm_init()
		{
			this.window_size = 2 * this.w_size;
			this.head[this.hash_size - 1] = 0;
			for (int i = 0; i < this.hash_size - 1; i++)
			{
				this.head[i] = 0;
			}
			this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
			this.good_match = Deflate.config_table[this.level].good_length;
			this.nice_match = Deflate.config_table[this.level].nice_length;
			this.max_chain_length = Deflate.config_table[this.level].max_chain;
			this.strstart = 0;
			this.block_start = 0;
			this.lookahead = 0;
			this.match_length = (this.prev_length = 2);
			this.match_available = 0;
			this.ins_h = 0;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00053BF4 File Offset: 0x00052BF4
		internal void tr_init()
		{
			this.l_desc.dyn_tree = this.dyn_ltree;
			this.l_desc.stat_desc = StaticTree.static_l_desc;
			this.d_desc.dyn_tree = this.dyn_dtree;
			this.d_desc.stat_desc = StaticTree.static_d_desc;
			this.bl_desc.dyn_tree = this.bl_tree;
			this.bl_desc.stat_desc = StaticTree.static_bl_desc;
			this.bi_buf = 0U;
			this.bi_valid = 0;
			this.last_eob_len = 8;
			this.init_block();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00053C80 File Offset: 0x00052C80
		internal void init_block()
		{
			for (int i = 0; i < 286; i++)
			{
				this.dyn_ltree[i * 2] = 0;
			}
			for (int j = 0; j < 30; j++)
			{
				this.dyn_dtree[j * 2] = 0;
			}
			for (int k = 0; k < 19; k++)
			{
				this.bl_tree[k * 2] = 0;
			}
			this.dyn_ltree[512] = 1;
			this.opt_len = (this.static_len = 0);
			this.last_lit = (this.matches = 0);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00053D08 File Offset: 0x00052D08
		internal void pqdownheap(short[] tree, int k)
		{
			int num = this.heap[k];
			for (int i = k << 1; i <= this.heap_len; i <<= 1)
			{
				if (i < this.heap_len && Deflate.smaller(tree, this.heap[i + 1], this.heap[i], this.depth))
				{
					i++;
				}
				if (Deflate.smaller(tree, num, this.heap[i], this.depth))
				{
					break;
				}
				this.heap[k] = this.heap[i];
				k = i;
			}
			this.heap[k] = num;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00053D94 File Offset: 0x00052D94
		internal static bool smaller(short[] tree, int n, int m, byte[] depth)
		{
			short num = tree[n * 2];
			short num2 = tree[m * 2];
			return num < num2 || (num == num2 && depth[n] <= depth[m]);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00053DC4 File Offset: 0x00052DC4
		internal void scan_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			tree[(max_code + 1) * 2 + 1] = -1;
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						short[] array = this.bl_tree;
						int num7 = num6 * 2;
						array[num7] += (short)num3;
					}
					else if (num6 != 0)
					{
						if (num6 != num)
						{
							short[] array2 = this.bl_tree;
							int num8 = num6 * 2;
							array2[num8] += 1;
						}
						short[] array3 = this.bl_tree;
						int num9 = 32;
						array3[num9] += 1;
					}
					else if (num3 <= 10)
					{
						short[] array4 = this.bl_tree;
						int num10 = 34;
						array4[num10] += 1;
					}
					else
					{
						short[] array5 = this.bl_tree;
						int num11 = 36;
						array5[num11] += 1;
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00053EF4 File Offset: 0x00052EF4
		internal int build_bl_tree()
		{
			this.scan_tree(this.dyn_ltree, this.l_desc.max_code);
			this.scan_tree(this.dyn_dtree, this.d_desc.max_code);
			this.bl_desc.build_tree(this);
			int num = 18;
			while (num >= 3 && this.bl_tree[(int)(Tree.bl_order[num] * 2 + 1)] == 0)
			{
				num--;
			}
			this.opt_len += 3 * (num + 1) + 5 + 5 + 4;
			return num;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00053F78 File Offset: 0x00052F78
		internal void send_all_trees(int lcodes, int dcodes, int blcodes)
		{
			this.send_bits(lcodes - 257, 5);
			this.send_bits(dcodes - 1, 5);
			this.send_bits(blcodes - 4, 4);
			for (int i = 0; i < blcodes; i++)
			{
				this.send_bits((int)this.bl_tree[(int)(Tree.bl_order[i] * 2 + 1)], 3);
			}
			this.send_tree(this.dyn_ltree, lcodes - 1);
			this.send_tree(this.dyn_dtree, dcodes - 1);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00053FEC File Offset: 0x00052FEC
		internal void send_tree(short[] tree, int max_code)
		{
			int num = -1;
			int num2 = (int)tree[1];
			int num3 = 0;
			int num4 = 7;
			int num5 = 4;
			if (num2 == 0)
			{
				num4 = 138;
				num5 = 3;
			}
			for (int i = 0; i <= max_code; i++)
			{
				int num6 = num2;
				num2 = (int)tree[(i + 1) * 2 + 1];
				if (++num3 >= num4 || num6 != num2)
				{
					if (num3 < num5)
					{
						do
						{
							this.send_code(num6, this.bl_tree);
						}
						while (--num3 != 0);
					}
					else if (num6 != 0)
					{
						if (num6 != num)
						{
							this.send_code(num6, this.bl_tree);
							num3--;
						}
						this.send_code(16, this.bl_tree);
						this.send_bits(num3 - 3, 2);
					}
					else if (num3 <= 10)
					{
						this.send_code(17, this.bl_tree);
						this.send_bits(num3 - 3, 3);
					}
					else
					{
						this.send_code(18, this.bl_tree);
						this.send_bits(num3 - 11, 7);
					}
					num3 = 0;
					num = num6;
					if (num2 == 0)
					{
						num4 = 138;
						num5 = 3;
					}
					else if (num6 == num2)
					{
						num4 = 6;
						num5 = 3;
					}
					else
					{
						num4 = 7;
						num5 = 4;
					}
				}
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000540F9 File Offset: 0x000530F9
		internal void put_byte(byte[] p, int start, int len)
		{
			Array.Copy(p, start, this.pending_buf, this.pending, len);
			this.pending += len;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00054120 File Offset: 0x00053120
		internal void put_byte(byte c)
		{
			this.pending_buf[this.pending++] = c;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00054148 File Offset: 0x00053148
		internal void put_short(int w)
		{
			this.pending_buf[this.pending++] = (byte)w;
			this.pending_buf[this.pending++] = (byte)(w >> 8);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0005418C File Offset: 0x0005318C
		internal void putShortMSB(int b)
		{
			this.pending_buf[this.pending++] = (byte)(b >> 8);
			this.pending_buf[this.pending++] = (byte)b;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000541D0 File Offset: 0x000531D0
		internal void send_code(int c, short[] tree)
		{
			int num = c * 2;
			this.send_bits((int)tree[num] & 65535, (int)tree[num + 1] & 65535);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000541FC File Offset: 0x000531FC
		internal void send_bits(int val, int length)
		{
			if (this.bi_valid > 16 - length)
			{
				this.bi_buf |= (uint)((uint)val << this.bi_valid);
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.pending_buf[this.pending++] = (byte)(this.bi_buf >> 8);
				this.bi_buf = (uint)val >> 16 - this.bi_valid;
				this.bi_valid += length - 16;
				return;
			}
			this.bi_buf |= (uint)((uint)val << this.bi_valid);
			this.bi_valid += length;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000542BC File Offset: 0x000532BC
		internal void _tr_align()
		{
			this.send_bits(2, 3);
			this.send_code(256, StaticTree.static_ltree);
			this.bi_flush();
			if (1 + this.last_eob_len + 10 - this.bi_valid < 9)
			{
				this.send_bits(2, 3);
				this.send_code(256, StaticTree.static_ltree);
				this.bi_flush();
			}
			this.last_eob_len = 7;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00054324 File Offset: 0x00053324
		internal bool _tr_tally(int dist, int lc)
		{
			this.pending_buf[this.d_buf + this.last_lit * 2] = (byte)(dist >> 8);
			this.pending_buf[this.d_buf + this.last_lit * 2 + 1] = (byte)dist;
			this.pending_buf[this.l_buf + this.last_lit] = (byte)lc;
			this.last_lit++;
			if (dist == 0)
			{
				short[] array = this.dyn_ltree;
				int num = lc * 2;
				array[num] += 1;
			}
			else
			{
				this.matches++;
				dist--;
				short[] array2 = this.dyn_ltree;
				int num2 = ((int)Tree._length_code[lc] + 256 + 1) * 2;
				array2[num2] += 1;
				short[] array3 = this.dyn_dtree;
				int num3 = Tree.d_code(dist) * 2;
				array3[num3] += 1;
			}
			if ((this.last_lit & 8191) == 0 && this.level > 2)
			{
				int num4 = this.last_lit * 8;
				int num5 = this.strstart - this.block_start;
				for (int i = 0; i < 30; i++)
				{
					num4 += (int)((long)this.dyn_dtree[i * 2] * (5L + (long)Tree.extra_dbits[i]));
				}
				num4 >>= 3;
				if (this.matches < this.last_lit / 2 && num4 < num5 / 2)
				{
					return true;
				}
			}
			return this.last_lit == this.lit_bufsize - 1;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0005448C File Offset: 0x0005348C
		internal void compress_block(short[] ltree, short[] dtree)
		{
			int num = 0;
			if (this.last_lit != 0)
			{
				do
				{
					int num2 = ((int)this.pending_buf[this.d_buf + num * 2] << 8 & 65280) | (int)(this.pending_buf[this.d_buf + num * 2 + 1] & byte.MaxValue);
					int num3 = (int)(this.pending_buf[this.l_buf + num] & byte.MaxValue);
					num++;
					if (num2 == 0)
					{
						this.send_code(num3, ltree);
					}
					else
					{
						int num4 = (int)Tree._length_code[num3];
						this.send_code(num4 + 256 + 1, ltree);
						int num5 = Tree.extra_lbits[num4];
						if (num5 != 0)
						{
							num3 -= Tree.base_length[num4];
							this.send_bits(num3, num5);
						}
						num2--;
						num4 = Tree.d_code(num2);
						this.send_code(num4, dtree);
						num5 = Tree.extra_dbits[num4];
						if (num5 != 0)
						{
							num2 -= Tree.base_dist[num4];
							this.send_bits(num2, num5);
						}
					}
				}
				while (num < this.last_lit);
			}
			this.send_code(256, ltree);
			this.last_eob_len = (int)ltree[513];
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00054594 File Offset: 0x00053594
		internal void set_data_type()
		{
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < 7)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 128)
			{
				num += (int)this.dyn_ltree[i * 2];
				i++;
			}
			while (i < 256)
			{
				num2 += (int)this.dyn_ltree[i * 2];
				i++;
			}
			this.data_type = ((num2 > num >> 2) ? 0 : 1);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00054608 File Offset: 0x00053608
		internal void bi_flush()
		{
			if (this.bi_valid == 16)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.pending_buf[this.pending++] = (byte)(this.bi_buf >> 8);
				this.bi_buf = 0U;
				this.bi_valid = 0;
				return;
			}
			if (this.bi_valid >= 8)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.bi_buf >>= 8;
				this.bi_buf &= 255U;
				this.bi_valid -= 8;
			}
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000546C4 File Offset: 0x000536C4
		internal void bi_windup()
		{
			if (this.bi_valid > 8)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
				this.pending_buf[this.pending++] = (byte)(this.bi_buf >> 8);
			}
			else if (this.bi_valid > 0)
			{
				this.pending_buf[this.pending++] = (byte)this.bi_buf;
			}
			this.bi_buf = 0U;
			this.bi_valid = 0;
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00054752 File Offset: 0x00053752
		internal void copy_block(int buf, int len, bool header)
		{
			this.bi_windup();
			this.last_eob_len = 8;
			if (header)
			{
				this.put_short((int)((short)len));
				this.put_short((int)((short)(~(short)len)));
			}
			this.put_byte(this.window, buf, len);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00054783 File Offset: 0x00053783
		internal void flush_block_only(bool eof)
		{
			this._tr_flush_block((this.block_start >= 0) ? this.block_start : -1, this.strstart - this.block_start, eof);
			this.block_start = this.strstart;
			this.strm.flush_pending();
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000547C4 File Offset: 0x000537C4
		internal int deflate_stored(int flush)
		{
			int num = 65535;
			if (num > this.pending_buf_size - 5)
			{
				num = this.pending_buf_size - 5;
			}
			for (;;)
			{
				if (this.lookahead <= 1)
				{
					this.fill_window();
					if (this.lookahead == 0 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_D7;
					}
				}
				this.strstart += this.lookahead;
				this.lookahead = 0;
				int num2 = this.block_start + num;
				if (this.strstart == 0 || this.strstart >= num2)
				{
					this.lookahead = this.strstart - num2;
					this.strstart = num2;
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
				if (this.strstart - this.block_start >= this.w_size - 262)
				{
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			return 0;
			IL_D7:
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush != 4)
				{
					return 0;
				}
				return 2;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000548CE File Offset: 0x000538CE
		internal void _tr_stored_block(int buf, int stored_len, bool eof)
		{
			this.send_bits(eof ? 1 : 0, 3);
			this.copy_block(buf, stored_len, true);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000548E8 File Offset: 0x000538E8
		internal void _tr_flush_block(int buf, int stored_len, bool eof)
		{
			int num = 0;
			int num2;
			int num3;
			if (this.level > 0)
			{
				if (this.data_type == 2)
				{
					this.set_data_type();
				}
				this.l_desc.build_tree(this);
				this.d_desc.build_tree(this);
				num = this.build_bl_tree();
				num2 = this.opt_len + 3 + 7 >> 3;
				num3 = this.static_len + 3 + 7 >> 3;
				if (num3 <= num2)
				{
					num2 = num3;
				}
			}
			else
			{
				num3 = (num2 = stored_len + 5);
			}
			if (stored_len + 4 <= num2 && buf != -1)
			{
				this._tr_stored_block(buf, stored_len, eof);
			}
			else if (num3 == num2)
			{
				this.send_bits(2 + (eof ? 1 : 0), 3);
				this.compress_block(StaticTree.static_ltree, StaticTree.static_dtree);
			}
			else
			{
				this.send_bits(4 + (eof ? 1 : 0), 3);
				this.send_all_trees(this.l_desc.max_code + 1, this.d_desc.max_code + 1, num + 1);
				this.compress_block(this.dyn_ltree, this.dyn_dtree);
			}
			this.init_block();
			if (eof)
			{
				this.bi_windup();
			}
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000549E8 File Offset: 0x000539E8
		internal void fill_window()
		{
			for (;;)
			{
				int num = this.window_size - this.lookahead - this.strstart;
				int num2;
				if (num == 0 && this.strstart == 0 && this.lookahead == 0)
				{
					num = this.w_size;
				}
				else if (num == -1)
				{
					num--;
				}
				else if (this.strstart >= this.w_size + this.w_size - 262)
				{
					Array.Copy(this.window, this.w_size, this.window, 0, this.w_size);
					this.match_start -= this.w_size;
					this.strstart -= this.w_size;
					this.block_start -= this.w_size;
					num2 = this.hash_size;
					int num3 = num2;
					do
					{
						int num4 = (int)this.head[--num3] & 65535;
						this.head[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num2 = this.w_size;
					num3 = num2;
					do
					{
						int num4 = (int)this.prev[--num3] & 65535;
						this.prev[num3] = (short)((num4 >= this.w_size) ? (num4 - this.w_size) : 0);
					}
					while (--num2 != 0);
					num += this.w_size;
				}
				if (this.strm.avail_in == 0)
				{
					break;
				}
				num2 = this.strm.read_buf(this.window, this.strstart + this.lookahead, num);
				this.lookahead += num2;
				if (this.lookahead >= 3)
				{
					this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask);
				}
				if (this.lookahead >= 262 || this.strm.avail_in == 0)
				{
					return;
				}
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00054BE4 File Offset: 0x00053BE4
		internal int deflate_fast(int flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < 262)
				{
					this.fill_window();
					if (this.lookahead < 262 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_2C6;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
					num = ((int)this.head[this.ins_h] & 65535);
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				if ((long)num != 0L && (this.strstart - num & 65535) <= this.w_size - 262 && this.strategy != 2)
				{
					this.match_length = this.longest_match(num);
				}
				bool flag;
				if (this.match_length >= 3)
				{
					flag = this._tr_tally(this.strstart - this.match_start, this.match_length - 3);
					this.lookahead -= this.match_length;
					if (this.match_length <= this.max_lazy_match && this.lookahead >= 3)
					{
						this.match_length--;
						do
						{
							this.strstart++;
							this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
							num = ((int)this.head[this.ins_h] & 65535);
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
						while (--this.match_length != 0);
						this.strstart++;
					}
					else
					{
						this.strstart += this.match_length;
						this.match_length = 0;
						this.ins_h = (int)(this.window[this.strstart] & byte.MaxValue);
						this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 1] & byte.MaxValue)) & this.hash_mask);
					}
				}
				else
				{
					flag = this._tr_tally(0, (int)(this.window[this.strstart] & byte.MaxValue));
					this.lookahead--;
					this.strstart++;
				}
				if (flag)
				{
					this.flush_block_only(false);
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
			}
			return 0;
			IL_2C6:
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00054EE0 File Offset: 0x00053EE0
		internal int deflate_slow(int flush)
		{
			int num = 0;
			for (;;)
			{
				if (this.lookahead < 262)
				{
					this.fill_window();
					if (this.lookahead < 262 && flush == 0)
					{
						break;
					}
					if (this.lookahead == 0)
					{
						goto IL_325;
					}
				}
				if (this.lookahead >= 3)
				{
					this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
					num = ((int)this.head[this.ins_h] & 65535);
					this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
					this.head[this.ins_h] = (short)this.strstart;
				}
				this.prev_length = this.match_length;
				this.prev_match = this.match_start;
				this.match_length = 2;
				if (num != 0 && this.prev_length < this.max_lazy_match && (this.strstart - num & 65535) <= this.w_size - 262)
				{
					if (this.strategy != 2)
					{
						this.match_length = this.longest_match(num);
					}
					if (this.match_length <= 5 && (this.strategy == 1 || (this.match_length == 3 && this.strstart - this.match_start > 4096)))
					{
						this.match_length = 2;
					}
				}
				if (this.prev_length >= 3 && this.match_length <= this.prev_length)
				{
					int num2 = this.strstart + this.lookahead - 3;
					bool flag = this._tr_tally(this.strstart - 1 - this.prev_match, this.prev_length - 3);
					this.lookahead -= this.prev_length - 1;
					this.prev_length -= 2;
					do
					{
						if (++this.strstart <= num2)
						{
							this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[this.strstart + 2] & byte.MaxValue)) & this.hash_mask);
							num = ((int)this.head[this.ins_h] & 65535);
							this.prev[this.strstart & this.w_mask] = this.head[this.ins_h];
							this.head[this.ins_h] = (short)this.strstart;
						}
					}
					while (--this.prev_length != 0);
					this.match_available = 0;
					this.match_length = 2;
					this.strstart++;
					if (flag)
					{
						this.flush_block_only(false);
						if (this.strm.avail_out == 0)
						{
							return 0;
						}
					}
				}
				else if (this.match_available != 0)
				{
					bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
					if (flag)
					{
						this.flush_block_only(false);
					}
					this.strstart++;
					this.lookahead--;
					if (this.strm.avail_out == 0)
					{
						return 0;
					}
				}
				else
				{
					this.match_available = 1;
					this.strstart++;
					this.lookahead--;
				}
			}
			return 0;
			IL_325:
			if (this.match_available != 0)
			{
				bool flag = this._tr_tally(0, (int)(this.window[this.strstart - 1] & byte.MaxValue));
				this.match_available = 0;
			}
			this.flush_block_only(flush == 4);
			if (this.strm.avail_out == 0)
			{
				if (flush == 4)
				{
					return 2;
				}
				return 0;
			}
			else
			{
				if (flush != 4)
				{
					return 1;
				}
				return 3;
			}
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00055264 File Offset: 0x00054264
		internal int longest_match(int cur_match)
		{
			int num = this.max_chain_length;
			int num2 = this.strstart;
			int num3 = this.prev_length;
			int num4 = (this.strstart > this.w_size - 262) ? (this.strstart - (this.w_size - 262)) : 0;
			int num5 = this.nice_match;
			int num6 = this.w_mask;
			int num7 = this.strstart + 258;
			byte b = this.window[num2 + num3 - 1];
			byte b2 = this.window[num2 + num3];
			if (this.prev_length >= this.good_match)
			{
				num >>= 2;
			}
			if (num5 > this.lookahead)
			{
				num5 = this.lookahead;
			}
			do
			{
				int num8 = cur_match;
				if (this.window[num8 + num3] == b2 && this.window[num8 + num3 - 1] == b && this.window[num8] == this.window[num2] && this.window[++num8] == this.window[num2 + 1])
				{
					num2 += 2;
					num8++;
					while (this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && this.window[++num2] == this.window[++num8] && num2 < num7)
					{
					}
					int num9 = 258 - (num7 - num2);
					num2 = num7 - 258;
					if (num9 > num3)
					{
						this.match_start = cur_match;
						num3 = num9;
						if (num9 >= num5)
						{
							break;
						}
						b = this.window[num2 + num3 - 1];
						b2 = this.window[num2 + num3];
					}
				}
			}
			while ((cur_match = ((int)this.prev[cur_match & num6] & 65535)) > num4 && --num != 0);
			if (num3 <= this.lookahead)
			{
				return num3;
			}
			return this.lookahead;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000554CB File Offset: 0x000544CB
		internal int deflateInit(ZStream strm, int level, int bits)
		{
			return this.deflateInit2(strm, level, 8, bits, 8, 0);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000554D9 File Offset: 0x000544D9
		internal int deflateInit(ZStream strm, int level)
		{
			return this.deflateInit(strm, level, 15);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000554E8 File Offset: 0x000544E8
		internal int deflateInit2(ZStream strm, int level, int method, int windowBits, int memLevel, int strategy)
		{
			int num = 0;
			strm.msg = null;
			if (level == -1)
			{
				level = 6;
			}
			if (windowBits < 0)
			{
				num = 1;
				windowBits = -windowBits;
			}
			if (memLevel < 1 || memLevel > 9 || method != 8 || windowBits < 9 || windowBits > 15 || level < 0 || level > 9 || strategy < 0 || strategy > 2)
			{
				return -2;
			}
			strm.dstate = this;
			this.noheader = num;
			this.w_bits = windowBits;
			this.w_size = 1 << this.w_bits;
			this.w_mask = this.w_size - 1;
			this.hash_bits = memLevel + 7;
			this.hash_size = 1 << this.hash_bits;
			this.hash_mask = this.hash_size - 1;
			this.hash_shift = (this.hash_bits + 3 - 1) / 3;
			this.window = new byte[this.w_size * 2];
			this.prev = new short[this.w_size];
			this.head = new short[this.hash_size];
			this.lit_bufsize = 1 << memLevel + 6;
			this.pending_buf = new byte[this.lit_bufsize * 4];
			this.pending_buf_size = this.lit_bufsize * 4;
			this.d_buf = this.lit_bufsize / 2;
			this.l_buf = 3 * this.lit_bufsize;
			this.level = level;
			this.strategy = strategy;
			this.method = (byte)method;
			return this.deflateReset(strm);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00055654 File Offset: 0x00054654
		internal int deflateReset(ZStream strm)
		{
			strm.total_in = (strm.total_out = 0L);
			strm.msg = null;
			strm.data_type = 2;
			this.pending = 0;
			this.pending_out = 0;
			if (this.noheader < 0)
			{
				this.noheader = 0;
			}
			this.status = ((this.noheader != 0) ? 113 : 42);
			strm.adler = strm._adler.adler32(0L, null, 0, 0);
			this.last_flush = 0;
			this.tr_init();
			this.lm_init();
			return 0;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x000556DC File Offset: 0x000546DC
		internal int deflateEnd()
		{
			if (this.status != 42 && this.status != 113 && this.status != 666)
			{
				return -2;
			}
			this.pending_buf = null;
			this.head = null;
			this.prev = null;
			this.window = null;
			if (this.status != 113)
			{
				return 0;
			}
			return -3;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00055738 File Offset: 0x00054738
		internal int deflateParams(ZStream strm, int _level, int _strategy)
		{
			int result = 0;
			if (_level == -1)
			{
				_level = 6;
			}
			if (_level < 0 || _level > 9 || _strategy < 0 || _strategy > 2)
			{
				return -2;
			}
			if (Deflate.config_table[this.level].func != Deflate.config_table[_level].func && strm.total_in != 0L)
			{
				result = strm.deflate(1);
			}
			if (this.level != _level)
			{
				this.level = _level;
				this.max_lazy_match = Deflate.config_table[this.level].max_lazy;
				this.good_match = Deflate.config_table[this.level].good_length;
				this.nice_match = Deflate.config_table[this.level].nice_length;
				this.max_chain_length = Deflate.config_table[this.level].max_chain;
			}
			this.strategy = _strategy;
			return result;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00055808 File Offset: 0x00054808
		internal int deflateSetDictionary(ZStream strm, byte[] dictionary, int dictLength)
		{
			int num = dictLength;
			int sourceIndex = 0;
			if (dictionary == null || this.status != 42)
			{
				return -2;
			}
			strm.adler = strm._adler.adler32(strm.adler, dictionary, 0, dictLength);
			if (num < 3)
			{
				return 0;
			}
			if (num > this.w_size - 262)
			{
				num = this.w_size - 262;
				sourceIndex = dictLength - num;
			}
			Array.Copy(dictionary, sourceIndex, this.window, 0, num);
			this.strstart = num;
			this.block_start = num;
			this.ins_h = (int)(this.window[0] & byte.MaxValue);
			this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[1] & byte.MaxValue)) & this.hash_mask);
			for (int i = 0; i <= num - 3; i++)
			{
				this.ins_h = ((this.ins_h << this.hash_shift ^ (int)(this.window[i + 2] & byte.MaxValue)) & this.hash_mask);
				this.prev[i & this.w_mask] = this.head[this.ins_h];
				this.head[this.ins_h] = (short)i;
			}
			return 0;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00055930 File Offset: 0x00054930
		internal int deflate(ZStream strm, int flush)
		{
			if (flush > 4 || flush < 0)
			{
				return -2;
			}
			if (strm.next_out == null || (strm.next_in == null && strm.avail_in != 0) || (this.status == 666 && flush != 4))
			{
				strm.msg = Deflate.z_errmsg[4];
				return -2;
			}
			if (strm.avail_out == 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			this.strm = strm;
			int num = this.last_flush;
			this.last_flush = flush;
			if (this.status == 42)
			{
				int num2 = 8 + (this.w_bits - 8 << 4) << 8;
				int num3 = (this.level - 1 & 255) >> 1;
				if (num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if (this.strstart != 0)
				{
					num2 |= 32;
				}
				num2 += 31 - num2 % 31;
				this.status = 113;
				this.putShortMSB(num2);
				if (this.strstart != 0)
				{
					this.putShortMSB((int)(strm.adler >> 16));
					this.putShortMSB((int)(strm.adler & 65535L));
				}
				strm.adler = strm._adler.adler32(0L, null, 0, 0);
			}
			if (this.pending != 0)
			{
				strm.flush_pending();
				if (strm.avail_out == 0)
				{
					this.last_flush = -1;
					return 0;
				}
			}
			else if (strm.avail_in == 0 && flush <= num && flush != 4)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (this.status == 666 && strm.avail_in != 0)
			{
				strm.msg = Deflate.z_errmsg[7];
				return -5;
			}
			if (strm.avail_in != 0 || this.lookahead != 0 || (flush != 0 && this.status != 666))
			{
				int num4 = -1;
				switch (Deflate.config_table[this.level].func)
				{
				case 0:
					num4 = this.deflate_stored(flush);
					break;
				case 1:
					num4 = this.deflate_fast(flush);
					break;
				case 2:
					num4 = this.deflate_slow(flush);
					break;
				}
				if (num4 == 2 || num4 == 3)
				{
					this.status = 666;
				}
				if (num4 == 0 || num4 == 2)
				{
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
					}
					return 0;
				}
				if (num4 == 1)
				{
					if (flush == 1)
					{
						this._tr_align();
					}
					else
					{
						this._tr_stored_block(0, 0, false);
						if (flush == 3)
						{
							for (int i = 0; i < this.hash_size; i++)
							{
								this.head[i] = 0;
							}
						}
					}
					strm.flush_pending();
					if (strm.avail_out == 0)
					{
						this.last_flush = -1;
						return 0;
					}
				}
			}
			if (flush != 4)
			{
				return 0;
			}
			if (this.noheader != 0)
			{
				return 1;
			}
			this.putShortMSB((int)(strm.adler >> 16));
			this.putShortMSB((int)(strm.adler & 65535L));
			strm.flush_pending();
			this.noheader = -1;
			if (this.pending == 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000A89 RID: 2697
		private const int MAX_MEM_LEVEL = 9;

		// Token: 0x04000A8A RID: 2698
		private const int Z_DEFAULT_COMPRESSION = -1;

		// Token: 0x04000A8B RID: 2699
		private const int MAX_WBITS = 15;

		// Token: 0x04000A8C RID: 2700
		private const int DEF_MEM_LEVEL = 8;

		// Token: 0x04000A8D RID: 2701
		private const int STORED = 0;

		// Token: 0x04000A8E RID: 2702
		private const int FAST = 1;

		// Token: 0x04000A8F RID: 2703
		private const int SLOW = 2;

		// Token: 0x04000A90 RID: 2704
		private const int NeedMore = 0;

		// Token: 0x04000A91 RID: 2705
		private const int BlockDone = 1;

		// Token: 0x04000A92 RID: 2706
		private const int FinishStarted = 2;

		// Token: 0x04000A93 RID: 2707
		private const int FinishDone = 3;

		// Token: 0x04000A94 RID: 2708
		private const int PRESET_DICT = 32;

		// Token: 0x04000A95 RID: 2709
		private const int Z_FILTERED = 1;

		// Token: 0x04000A96 RID: 2710
		private const int Z_HUFFMAN_ONLY = 2;

		// Token: 0x04000A97 RID: 2711
		private const int Z_DEFAULT_STRATEGY = 0;

		// Token: 0x04000A98 RID: 2712
		private const int Z_NO_FLUSH = 0;

		// Token: 0x04000A99 RID: 2713
		private const int Z_PARTIAL_FLUSH = 1;

		// Token: 0x04000A9A RID: 2714
		private const int Z_SYNC_FLUSH = 2;

		// Token: 0x04000A9B RID: 2715
		private const int Z_FULL_FLUSH = 3;

		// Token: 0x04000A9C RID: 2716
		private const int Z_FINISH = 4;

		// Token: 0x04000A9D RID: 2717
		private const int Z_OK = 0;

		// Token: 0x04000A9E RID: 2718
		private const int Z_STREAM_END = 1;

		// Token: 0x04000A9F RID: 2719
		private const int Z_NEED_DICT = 2;

		// Token: 0x04000AA0 RID: 2720
		private const int Z_ERRNO = -1;

		// Token: 0x04000AA1 RID: 2721
		private const int Z_STREAM_ERROR = -2;

		// Token: 0x04000AA2 RID: 2722
		private const int Z_DATA_ERROR = -3;

		// Token: 0x04000AA3 RID: 2723
		private const int Z_MEM_ERROR = -4;

		// Token: 0x04000AA4 RID: 2724
		private const int Z_BUF_ERROR = -5;

		// Token: 0x04000AA5 RID: 2725
		private const int Z_VERSION_ERROR = -6;

		// Token: 0x04000AA6 RID: 2726
		private const int INIT_STATE = 42;

		// Token: 0x04000AA7 RID: 2727
		private const int BUSY_STATE = 113;

		// Token: 0x04000AA8 RID: 2728
		private const int FINISH_STATE = 666;

		// Token: 0x04000AA9 RID: 2729
		private const int Z_DEFLATED = 8;

		// Token: 0x04000AAA RID: 2730
		private const int STORED_BLOCK = 0;

		// Token: 0x04000AAB RID: 2731
		private const int STATIC_TREES = 1;

		// Token: 0x04000AAC RID: 2732
		private const int DYN_TREES = 2;

		// Token: 0x04000AAD RID: 2733
		private const int Z_BINARY = 0;

		// Token: 0x04000AAE RID: 2734
		private const int Z_ASCII = 1;

		// Token: 0x04000AAF RID: 2735
		private const int Z_UNKNOWN = 2;

		// Token: 0x04000AB0 RID: 2736
		private const int Buf_size = 16;

		// Token: 0x04000AB1 RID: 2737
		private const int REP_3_6 = 16;

		// Token: 0x04000AB2 RID: 2738
		private const int REPZ_3_10 = 17;

		// Token: 0x04000AB3 RID: 2739
		private const int REPZ_11_138 = 18;

		// Token: 0x04000AB4 RID: 2740
		private const int MIN_MATCH = 3;

		// Token: 0x04000AB5 RID: 2741
		private const int MAX_MATCH = 258;

		// Token: 0x04000AB6 RID: 2742
		private const int MIN_LOOKAHEAD = 262;

		// Token: 0x04000AB7 RID: 2743
		private const int MAX_BITS = 15;

		// Token: 0x04000AB8 RID: 2744
		private const int D_CODES = 30;

		// Token: 0x04000AB9 RID: 2745
		private const int BL_CODES = 19;

		// Token: 0x04000ABA RID: 2746
		private const int LENGTH_CODES = 29;

		// Token: 0x04000ABB RID: 2747
		private const int LITERALS = 256;

		// Token: 0x04000ABC RID: 2748
		private const int L_CODES = 286;

		// Token: 0x04000ABD RID: 2749
		private const int HEAP_SIZE = 573;

		// Token: 0x04000ABE RID: 2750
		private const int END_BLOCK = 256;

		// Token: 0x04000ABF RID: 2751
		private static Deflate.Config[] config_table;

		// Token: 0x04000AC0 RID: 2752
		private static string[] z_errmsg = new string[]
		{
			"need dictionary",
			"stream end",
			"",
			"file error",
			"stream error",
			"data error",
			"insufficient memory",
			"buffer error",
			"incompatible version",
			""
		};

		// Token: 0x04000AC1 RID: 2753
		internal ZStream strm;

		// Token: 0x04000AC2 RID: 2754
		internal int status;

		// Token: 0x04000AC3 RID: 2755
		internal byte[] pending_buf;

		// Token: 0x04000AC4 RID: 2756
		internal int pending_buf_size;

		// Token: 0x04000AC5 RID: 2757
		internal int pending_out;

		// Token: 0x04000AC6 RID: 2758
		internal int pending;

		// Token: 0x04000AC7 RID: 2759
		internal int noheader;

		// Token: 0x04000AC8 RID: 2760
		internal byte data_type;

		// Token: 0x04000AC9 RID: 2761
		internal byte method;

		// Token: 0x04000ACA RID: 2762
		internal int last_flush;

		// Token: 0x04000ACB RID: 2763
		internal int w_size;

		// Token: 0x04000ACC RID: 2764
		internal int w_bits;

		// Token: 0x04000ACD RID: 2765
		internal int w_mask;

		// Token: 0x04000ACE RID: 2766
		internal byte[] window;

		// Token: 0x04000ACF RID: 2767
		internal int window_size;

		// Token: 0x04000AD0 RID: 2768
		internal short[] prev;

		// Token: 0x04000AD1 RID: 2769
		internal short[] head;

		// Token: 0x04000AD2 RID: 2770
		internal int ins_h;

		// Token: 0x04000AD3 RID: 2771
		internal int hash_size;

		// Token: 0x04000AD4 RID: 2772
		internal int hash_bits;

		// Token: 0x04000AD5 RID: 2773
		internal int hash_mask;

		// Token: 0x04000AD6 RID: 2774
		internal int hash_shift;

		// Token: 0x04000AD7 RID: 2775
		internal int block_start;

		// Token: 0x04000AD8 RID: 2776
		internal int match_length;

		// Token: 0x04000AD9 RID: 2777
		internal int prev_match;

		// Token: 0x04000ADA RID: 2778
		internal int match_available;

		// Token: 0x04000ADB RID: 2779
		internal int strstart;

		// Token: 0x04000ADC RID: 2780
		internal int match_start;

		// Token: 0x04000ADD RID: 2781
		internal int lookahead;

		// Token: 0x04000ADE RID: 2782
		internal int prev_length;

		// Token: 0x04000ADF RID: 2783
		internal int max_chain_length;

		// Token: 0x04000AE0 RID: 2784
		internal int max_lazy_match;

		// Token: 0x04000AE1 RID: 2785
		internal int level;

		// Token: 0x04000AE2 RID: 2786
		internal int strategy;

		// Token: 0x04000AE3 RID: 2787
		internal int good_match;

		// Token: 0x04000AE4 RID: 2788
		internal int nice_match;

		// Token: 0x04000AE5 RID: 2789
		internal short[] dyn_ltree;

		// Token: 0x04000AE6 RID: 2790
		internal short[] dyn_dtree;

		// Token: 0x04000AE7 RID: 2791
		internal short[] bl_tree;

		// Token: 0x04000AE8 RID: 2792
		internal Tree l_desc = new Tree();

		// Token: 0x04000AE9 RID: 2793
		internal Tree d_desc = new Tree();

		// Token: 0x04000AEA RID: 2794
		internal Tree bl_desc = new Tree();

		// Token: 0x04000AEB RID: 2795
		internal short[] bl_count = new short[16];

		// Token: 0x04000AEC RID: 2796
		internal int[] heap = new int[573];

		// Token: 0x04000AED RID: 2797
		internal int heap_len;

		// Token: 0x04000AEE RID: 2798
		internal int heap_max;

		// Token: 0x04000AEF RID: 2799
		internal byte[] depth = new byte[573];

		// Token: 0x04000AF0 RID: 2800
		internal int l_buf;

		// Token: 0x04000AF1 RID: 2801
		internal int lit_bufsize;

		// Token: 0x04000AF2 RID: 2802
		internal int last_lit;

		// Token: 0x04000AF3 RID: 2803
		internal int d_buf;

		// Token: 0x04000AF4 RID: 2804
		internal int opt_len;

		// Token: 0x04000AF5 RID: 2805
		internal int static_len;

		// Token: 0x04000AF6 RID: 2806
		internal int matches;

		// Token: 0x04000AF7 RID: 2807
		internal int last_eob_len;

		// Token: 0x04000AF8 RID: 2808
		internal uint bi_buf;

		// Token: 0x04000AF9 RID: 2809
		internal int bi_valid;

		// Token: 0x02000178 RID: 376
		internal class Config
		{
			// Token: 0x06000EAE RID: 3758 RVA: 0x00055BE9 File Offset: 0x00054BE9
			internal Config(int good_length, int max_lazy, int nice_length, int max_chain, int func)
			{
				this.good_length = good_length;
				this.max_lazy = max_lazy;
				this.nice_length = nice_length;
				this.max_chain = max_chain;
				this.func = func;
			}

			// Token: 0x04000AFA RID: 2810
			internal int good_length;

			// Token: 0x04000AFB RID: 2811
			internal int max_lazy;

			// Token: 0x04000AFC RID: 2812
			internal int nice_length;

			// Token: 0x04000AFD RID: 2813
			internal int max_chain;

			// Token: 0x04000AFE RID: 2814
			internal int func;
		}
	}
}
