using System;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200020C RID: 524
	public class SFTable : ICloneParent
	{
		// Token: 0x06001EBB RID: 7867 RVA: 0x00104264 File Offset: 0x00103264
		public SFTable(int iRowCount, int iColumnCount)
		{
			this.ᜀ = iRowCount;
			this.ᜁ = iColumnCount;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00104288 File Offset: 0x00103288
		protected SFTable(SFTable data, bool clone)
		{
			this.ᜀ = data.ᜀ;
			this.ᜁ = data.ᜁ;
			if (data.ᜂ != null && clone)
			{
				this.ᜃ = data.ᜃ;
				this.ᜂ = (spr\u259B<object>)data.ᜂ.ᜀ();
			}
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x001042E8 File Offset: 0x001032E8
		protected SFTable(SFTable data, bool clone, object parent)
		{
			this.ᜀ = data.ᜀ;
			this.ᜁ = data.ᜁ;
			if (data.ᜂ != null && clone)
			{
				this.ᜃ = data.ᜃ;
				this.ᜂ = (spr\u259B<object>)data.ᜂ.ᜀ(parent);
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00104348 File Offset: 0x00103348
		public virtual object Clone()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new SFTable(this, true);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x0010438C File Offset: 0x0010338C
		public virtual object Clone(object parent)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new SFTable(this, true, parent);
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x001043D0 File Offset: 0x001033D0
		internal spr\u259B<object> Rows
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_54;
					case 2:
						goto IL_6F;
					}
					if (this.ᜂ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_54:
					this.ᜂ = new spr\u259B<object>();
					if (true)
					{
					}
					num = 2;
				}
				IL_6F:
				return this.ᜂ;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00104454 File Offset: 0x00103454
		public int RowCount
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x00104498 File Offset: 0x00103498
		public int ColCount
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜁ;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x001044DC File Offset: 0x001034DC
		public int CellCount
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜃ;
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00104520 File Offset: 0x00103520
		public void Clear()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ = null;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00104564 File Offset: 0x00103564
		internal virtual spr\u259B<object> CreateCellCollection()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new spr\u259B<object>();
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x001045A4 File Offset: 0x001035A4
		public bool Contains(int rowIndex, int colIndex)
		{
			int num = 1;
			spr\u259B<object> spr_u259B;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 2:
					if (colIndex >= 0)
					{
						num = 7;
						continue;
					}
					return false;
				case 3:
					if (colIndex >= this.ᜁ)
					{
						num = 4;
						continue;
					}
					if (true)
					{
					}
					spr_u259B = (this.Rows.ᜀ(rowIndex) as spr\u259B<object>);
					num = 9;
					continue;
				case 4:
					return false;
				case 5:
					goto IL_79;
				case 6:
					if (rowIndex < this.ᜀ)
					{
						num = 0;
						continue;
					}
					return false;
				case 7:
					num = 3;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_71;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 9:
					if (spr_u259B != null)
					{
						goto IL_71;
					}
					return false;
				}
				if (rowIndex >= 0)
				{
					num = 8;
					continue;
				}
				return false;
				IL_71:
				num = 5;
			}
			IL_79:
			return spr_u259B.ᜀ(colIndex) != null;
		}

		// Token: 0x17000B58 RID: 2904
		public object this[int rowIndex, int colIndex]
		{
			get
			{
				int num = 4;
				spr\u259B<object> spr_u259B;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_8F;
					case 1:
						num = 7;
						continue;
					case 2:
						if (colIndex < this.ᜁ)
						{
							num = 1;
							continue;
						}
						goto IL_107;
					case 3:
						if (true)
						{
						}
						num = 2;
						continue;
					case 5:
						if (rowIndex >= 0)
						{
							num = 3;
							continue;
						}
						goto IL_107;
					case 6:
						if (spr_u259B != null)
						{
							goto IL_6E;
						}
						goto IL_109;
					case 7:
						if (colIndex < 0)
						{
							num = 0;
							continue;
						}
						spr_u259B = (this.Rows.ᜀ(rowIndex) as spr\u259B<object>);
						num = 6;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 9:
						goto IL_76;
					}
					if (rowIndex < this.ᜀ)
					{
						num = 8;
						continue;
					}
					goto IL_107;
					IL_6E:
					num = 9;
				}
				IL_76:
				return spr_u259B.ᜀ(colIndex);
				IL_8F:
				IL_107:
				return null;
				IL_109:
				return null;
			}
			set
			{
				int a_ = 4;
				int num = 0;
				spr\u259B<object> spr_u259B2;
				for (;;)
				{
					object obj;
					switch (num)
					{
					case 1:
						this.ᜃ++;
						num = 14;
						continue;
					case 2:
						goto IL_FF;
					case 3:
						if (rowIndex < 0)
						{
							num = 7;
							continue;
						}
						num = 19;
						continue;
					case 4:
						num = 3;
						continue;
					case 5:
						goto IL_172;
					case 6:
						goto IL_8E;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_172;
						default:
							goto IL_215;
						}
						break;
					case 8:
						if (value == null)
						{
							num = 16;
							continue;
						}
						goto IL_255;
					case 9:
						goto IL_18F;
					case 10:
						if (value != null)
						{
							num = 1;
							continue;
						}
						goto IL_255;
					case 11:
						num = 13;
						continue;
					case 12:
					{
						if (colIndex < 0)
						{
							num = 9;
							continue;
						}
						spr\u259B<object> spr_u259B = this.Rows;
						spr_u259B2 = (spr_u259B.ᜀ(rowIndex) as spr\u259B<object>);
						num = 15;
						continue;
					}
					case 13:
					{
						if (value == null)
						{
							num = 18;
							continue;
						}
						spr\u259B<object> spr_u259B;
						spr_u259B.ᜀ(rowIndex, spr_u259B2 = this.CreateCellCollection());
						num = 6;
						continue;
					}
					case 14:
						goto IL_14F;
					case 15:
						if (spr_u259B2 == null)
						{
							num = 11;
							continue;
						}
						goto IL_8E;
					case 16:
						this.ᜃ--;
						num = 2;
						continue;
					case 17:
						num = 8;
						continue;
					case 18:
						return;
					case 19:
						if (colIndex < this.ᜁ)
						{
							num = 5;
							continue;
						}
						goto IL_1CE;
					case 20:
						if (obj != null)
						{
							num = 17;
							continue;
						}
						num = 10;
						continue;
					}
					if (true)
					{
					}
					if (rowIndex < this.ᜀ)
					{
						num = 4;
						continue;
					}
					goto IL_104;
					IL_8E:
					obj = spr_u259B2.ᜀ(colIndex);
					num = 20;
					continue;
					IL_172:
					num = 12;
				}
				IL_FF:
				goto IL_255;
				IL_104:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠹医䤽िⱁ⁃⍅ぇ", a_));
				IL_14F:
				goto IL_255;
				IL_18F:
				IL_1CE:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夹医刽िⱁ⁃⍅ぇ", a_));
				IL_215:
				if (false)
				{
				}
				goto IL_104;
				IL_255:
				spr_u259B2.ᜀ(colIndex, value);
			}
		}

		// Token: 0x040010C5 RID: 4293
		private bool \u2460\u00AD\u009D\u008D;

		// Token: 0x040010C6 RID: 4294
		private long[] \u2593\u008C\u0095ª;

		// Token: 0x040010C7 RID: 4295
		private int ᜀ;

		// Token: 0x040010C8 RID: 4296
		private float[] \u2593\u009A\u0095\u0098;

		// Token: 0x040010C9 RID: 4297
		private int ᜁ;

		// Token: 0x040010CA RID: 4298
		private float[] \u25D9\u0093\u00A6\u0085;

		// Token: 0x040010CB RID: 4299
		private spr\u259B<object> ᜂ;

		// Token: 0x040010CC RID: 4300
		private int ᜃ;
	}
}
