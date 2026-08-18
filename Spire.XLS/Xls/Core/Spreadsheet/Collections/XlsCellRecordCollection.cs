using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000208 RID: 520
	public class XlsCellRecordCollection : XlsObject, IDictionary
	{
		// Token: 0x06001DB6 RID: 7606 RVA: 0x000FCD90 File Offset: 0x000FBD90
		internal XlsCellRecordCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ();
			this.ᜁ = new SFTable(this.ᜃ.MaxRowCount, this.ᜃ.MaxColumnCount);
			this.ᜀ = new sprủ(this.ᜃ.MaxRowCount, this.ᜂ);
			this.ᜅ = new RecordExtractor();
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x000FCDF4 File Offset: 0x000FBDF4
		private void ᜁ()
		{
			int a_ = 5;
			for (;;)
			{
				this.ᜂ = (base.FindParent(typeof(IInternalWorksheet)) as IInternalWorksheet);
				if (this.ᜂ != null)
				{
					goto IL_7E;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_4C;
				}
			}
			IL_4C:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬺尼䴾⑀ⵂㅄ", a_), RecordTableEnumerator.b("砺尼儾晀㝂敄ⅆ⁈╊⥌潎⅐㉒❔㉖㝘⽚絜⡞๠ᅢ๤ᑦŨ๪࡬᭮", a_));
			IL_7E:
			this.ᜃ = this.ᜂ.ParentWorkbook;
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x000FCE90 File Offset: 0x000FBE90
		public int FirstRow
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
				return this.ᜂ.FirstRow;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x000FCED8 File Offset: 0x000FBED8
		public int LastRow
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
				return this.ᜂ.LastRow;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x000FCF20 File Offset: 0x000FBF20
		public int FirstColumn
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
				return this.ᜂ.FirstColumn;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x000FCF68 File Offset: 0x000FBF68
		public int LastColumn
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
				return this.ᜂ.LastColumn;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x000FCFB0 File Offset: 0x000FBFB0
		internal IInternalWorksheet sheet
		{
			get
			{
				while (this.ᜂ is XlsExternWorksheet)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return (XlsExternWorksheet)this.ᜂ;
					}
				}
				return (XlsWorksheet)this.ᜂ;
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x000FD014 File Offset: 0x000FC014
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x000FD058 File Offset: 0x000FC058
		internal sprủ Table
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
				return this.ᜀ;
			}
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x000FD09C File Offset: 0x000FC09C
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x000FD0E0 File Offset: 0x000FC0E0
		public bool UseCache
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
				return this.ᜄ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_AC;
					case 2:
						goto IL_AC;
					case 3:
						num = 6;
						continue;
					case 4:
						return;
					case 5:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42;
						default:
							if (false)
							{
							}
							this.ᜀ();
							num = 1;
							continue;
						}
						break;
					case 6:
						if (value)
						{
							num = 5;
							continue;
						}
						goto IL_42;
					}
					if (value != this.ᜄ)
					{
						num = 3;
						continue;
					}
					break;
					IL_42:
					this.ᜁ = null;
					num = 2;
					continue;
					IL_AC:
					this.ᜄ = value;
					num = 4;
				}
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x000FD1B0 File Offset: 0x000FC1B0
		public ExcelVersion Version
		{
			set
			{
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜀ.ᜂ(this.ᜃ.MaxRowCount);
						int num = 6;
						for (;;)
						{
							if (true)
							{
							}
							int num2;
							int num3;
							sprᱧ sprᱧ;
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 1:
								this.ᜂ.LastRow = num2 + 1;
								num = 8;
								continue;
							case 2:
							{
								int lastRow;
								if (num3 >= lastRow)
								{
									num = 0;
									continue;
								}
								spr\u17FF spr_u17FF = this.ᜃ.AppImplementation;
								sprᱧ = this.ᜀ.ᜀ(num3, spr_u17FF.ᜅ(), false, value);
								num = 7;
								continue;
							}
							case 3:
								if (num2 >= 0)
								{
									num = 1;
									continue;
								}
								return;
							case 4:
								goto IL_92;
							case 5:
								goto IL_17E;
							case 6:
								if (this.FirstRow >= 0)
								{
									num = 9;
									continue;
								}
								return;
							case 7:
								if (sprᱧ == null)
								{
									goto IL_B3;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_17E;
								default:
									if (false)
									{
									}
									num = 5;
									continue;
								}
								break;
							case 8:
								return;
							case 9:
							{
								num2 = -1;
								num3 = this.FirstRow - 1;
								int lastRow = this.LastRow;
								num = 10;
								continue;
							}
							case 10:
								goto IL_92;
							case 11:
								goto IL_B3;
							}
							break;
							IL_92:
							num = 2;
							continue;
							IL_B3:
							num3++;
							num = 4;
							continue;
							IL_17E:
							sprᱧ.ᜀ(value, base.AppImplementation.ᜨ());
							num2 = num3;
							num = 11;
						}
					}
					return;
				}
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x000FD364 File Offset: 0x000FC364
		internal RecordExtractor RecordExtractor
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
				return this.ᜅ;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x000FD3A8 File Offset: 0x000FC3A8
		public int Count
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x000FD3E8 File Offset: 0x000FC3E8
		public bool IsFixedSize
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
				return false;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x000FD424 File Offset: 0x000FC424
		public bool IsReadOnly
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
				return false;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x000FD460 File Offset: 0x000FC460
		public ICollection Keys
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x000FD4A0 File Offset: 0x000FC4A0
		public ICollection Values
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000B1E RID: 2846
		public object this[object key]
		{
			get
			{
				int a_ = 6;
				while (key is long)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return this[(long)key];
					}
				}
				throw new NotSupportedException(RecordTableEnumerator.b("爻儽⸿扁ൃ⡅㱇祉繋湍㭏㝑ⵓ╕硗㭙⹛㭝䁟ౡୣብ䡧ᥩᥫṭoᵱٳɵ", a_));
			}
			set
			{
				int a_ = 10;
				while (key is long)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this[(long)key] = (value as spr\u23A5);
						return;
					}
				}
				if (true)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("฿ⵁ⩃晅Ň⑉㡋絍扏牑㽓㍕⅗⥙籛㽝቟ݡ䑣ࡥݧṩ䱫ᵭկɱѳ᥵੷๹", a_));
			}
		}

		// Token: 0x17000B1F RID: 2847
		[CLSCompliant(false)]
		internal spr\u23A5 this[long A_0]
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				int a_ = sprṔ.ᜁ(A_0) - 1;
				int a_2 = sprṔ.ᜀ(A_0) - 1;
				return this.ᜀ.ᜋ(a_, a_2) as spr\u23A5;
			}
			set
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
				int a_ = sprṔ.ᜁ(A_0);
				int a_2 = sprṔ.ᜀ(A_0);
				this[a_, a_2] = value;
			}
		}

		// Token: 0x17000B20 RID: 2848
		[CLSCompliant(false)]
		internal spr\u23A5 this[int A_0, int A_1]
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
				return this.ᜀ.ᜋ(A_0 - 1, A_1 - 1) as spr\u23A5;
			}
			set
			{
				for (;;)
				{
					if (true)
					{
					}
					if (value != null)
					{
						goto IL_3C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_23;
					}
				}
				IL_23:
				if (false)
				{
				}
				this.Remove(A_0, A_1);
				return;
				IL_3C:
				this.ᜀ.ᜀ(A_0 - 1, A_1 - 1, value);
				sprᜑ.ᜁ(this.ᜂ, A_1);
				sprᜑ.ᜀ(this.ᜂ, A_0);
			}
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000FD740 File Offset: 0x000FC740
		public void Clear()
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
			this.ᜀ.ᜊ();
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000FD788 File Offset: 0x000FC788
		public void Add(object key, object value)
		{
			for (;;)
			{
				IL_00:
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_6E;
						case 1:
							this.ᜂ((long)key, value as spr\u23A5);
							num = 0;
							continue;
						}
						if (!(key is long))
						{
							goto IL_70;
						}
						num = 1;
						break;
					}
				}
			}
			IL_6E:
			IL_70:
			if (true)
			{
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000FD810 File Offset: 0x000FC810
		public IDictionaryEnumerator GetEnumerator()
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
			return new RecordTableEnumerator(this);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x000FD854 File Offset: 0x000FC854
		[CLSCompliant(false)]
		internal void ᜀ(spr\u23A5 A_0)
		{
			int a_ = 6;
			int num;
			int num2;
			for (;;)
			{
				num = A_0.ᜄ();
				num2 = A_0.ᜅ();
				if (!this.ᜀ.ᜊ(num, num2))
				{
					goto IL_6A;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_46;
				}
			}
			IL_46:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("缻儽ⰿ⹁⅃╅㱇⍉⍋⁍灏㍑㡓⑕㵗㭙㡛❝䁟šୣࡥᱧ୩իmͯ剱ݳ͵᭷ቹ屻፽慎ꒉ", a_));
			IL_6A:
			this.ᜁ(num + 1, num2 + 1, A_0);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000FD8D8 File Offset: 0x000FC8D8
		public void Remove(object key)
		{
			for (;;)
			{
				IL_00:
				int num = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 1:
							if (true)
							{
							}
							this.Remove((long)key);
							num = 2;
							continue;
						case 2:
							return;
						}
						if (!(key is long))
						{
							return;
						}
						num = 1;
						break;
					}
				}
			}
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x000FD958 File Offset: 0x000FC958
		public bool Contains(object key)
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
			return this.Contains((long)key);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x000FD9A0 File Offset: 0x000FC9A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return new RecordTableEnumerator(this);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x000FD9E4 File Offset: 0x000FC9E4
		[CLSCompliant(false)]
		internal void ᜂ(long A_0, spr\u23A5 A_1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ(A_1);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x000FDA28 File Offset: 0x000FCA28
		public void Remove(long key)
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
			int a_ = sprṔ.ᜁ(key) - 1;
			int a_2 = sprṔ.ᜀ(key) - 1;
			this.ᜀ.ᜀ(a_, a_2, null);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000FDA84 File Offset: 0x000FCA84
		public void Remove(int iRow, int iColumn)
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
			this.ᜀ.ᜀ(iRow - 1, iColumn - 1, null);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000FDAD4 File Offset: 0x000FCAD4
		public bool ContainsRow(int iRowIndex)
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
			return this.ᜀ.ᜆ(iRowIndex);
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x000FDB1C File Offset: 0x000FCB1C
		public bool Contains(long key)
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
			int a_ = sprṔ.ᜁ(key) - 1;
			int a_2 = sprṔ.ᜀ(key) - 1;
			return this.ᜀ.ᜊ(a_, a_2);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x000FDB78 File Offset: 0x000FCB78
		public bool Contains(int iRow, int iColumn)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜀ.ᜊ(iRow - 1, iColumn - 1);
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x000FDBC4 File Offset: 0x000FCBC4
		public bool IsSynchronized
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x000FDC04 File Offset: 0x000FCC04
		public object SyncRoot
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x000FDC44 File Offset: 0x000FCC44
		public void CopyTo(Array array, int index)
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
			throw new NotSupportedException();
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000FDC84 File Offset: 0x000FCC84
		[CLSCompliant(false)]
		internal int ᜀ(RecordArrayList A_0, List<spr\u2466> A_1)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 30;
				int num8;
				for (;;)
				{
					int num2;
					spr\u2466 spr_u;
					int num3;
					List<sprᱧ> list;
					ExcelVersion a_2;
					int num4;
					int num6;
					int num9;
					switch (num)
					{
					case 0:
						goto IL_1B3;
					case 1:
						if (num2 != 0)
						{
							num = 26;
							continue;
						}
						goto IL_E7;
					case 2:
						goto IL_1D9;
					case 3:
					{
						spr_u.ᜀ(num3);
						int count;
						sprᱧ sprᱧ = list[count - 1];
						num = 8;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3DA;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					case 5:
						goto IL_270;
					case 6:
						goto IL_C4;
					case 7:
					{
						sprᱧ sprᱧ2;
						num4 = sprᱧ2.ᜀ(a_2);
						goto IL_38C;
					}
					case 8:
					{
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num = 28;
							continue;
						}
						goto IL_1D9;
					}
					case 9:
					{
						sprᱧ sprᱧ2;
						if (sprᱧ2 == null)
						{
							num = 23;
							continue;
						}
						num = 7;
						continue;
					}
					case 10:
					{
						sprᱧ sprᱧ2;
						A_0.ᜀ(sprᱧ2);
						num = 15;
						continue;
					}
					case 11:
					{
						sprᱧ sprᱧ;
						if (sprᱧ.ᜀ(a_2) > 0)
						{
							num = 22;
							continue;
						}
						goto IL_1D9;
					}
					case 12:
						if (num2 > 0)
						{
							num = 10;
							continue;
						}
						goto IL_C9;
					case 13:
					{
						int num5;
						spr_u.ᜂ()[0] = (ushort)num5;
						num6 = 0;
						int count;
						int num7 = count - 1;
						num = 0;
						continue;
					}
					case 14:
						goto IL_E7;
					case 15:
						goto IL_C9;
					case 16:
					{
						int count;
						if (count > 0)
						{
							num = 13;
							continue;
						}
						goto IL_1D9;
					}
					case 17:
						goto IL_3DA;
					case 18:
						return num8;
					case 19:
						num4 = 0;
						goto IL_38C;
					case 20:
						goto IL_270;
					case 21:
					{
						int lastRow;
						if (num9 > lastRow)
						{
							num = 18;
							continue;
						}
						int num5 = 0;
						num3 = 0;
						int firstColumn;
						int lastColumn;
						num9 = this.ᜀ(A_0, list, num9, ref num5, ref num3, lastRow, firstColumn, lastColumn, ExcelVersion.Version97to2003);
						spr_u = (spr\u2466)spr\u175E.ᜀ(TBIFFRecord.DBCell);
						int count = list.Count;
						spr_u.ᜀ(new ushort[count]);
						A_1.Add(spr_u);
						num = 16;
						continue;
					}
					case 22:
					{
						sprᱧ sprᱧ;
						A_0.ᜀ(sprᱧ);
						spr\u2466 spr_u2 = spr_u;
						spr_u2.ᜀ(spr_u2.ᜁ() + (sprᱧ.ᜀ(a_2) + 4));
						num = 2;
						continue;
					}
					case 23:
						num = 19;
						continue;
					case 24:
						goto IL_1B3;
					case 25:
					{
						if (this.FirstRow < 0)
						{
							num = 17;
							continue;
						}
						int firstRow = this.FirstRow;
						int lastRow = this.LastRow;
						int firstColumn = this.FirstColumn;
						int lastColumn = this.LastColumn;
						list = new List<sprᱧ>(32);
						num9 = firstRow;
						num = 5;
						continue;
					}
					case 26:
						num2 += 4;
						num = 14;
						continue;
					case 27:
					{
						int num7;
						if (num6 >= num7)
						{
							num = 3;
							continue;
						}
						sprᱧ sprᱧ2 = list[num6];
						num = 9;
						continue;
					}
					case 28:
						num = 11;
						continue;
					case 29:
					{
						sprᱧ sprᱧ2;
						if (sprᱧ2 != null)
						{
							num = 4;
							continue;
						}
						goto IL_C9;
					}
					}
					if (A_0 == null)
					{
						num = 6;
						continue;
					}
					a_2 = ExcelVersion.Version97to2003;
					num8 = 0;
					num = 25;
					continue;
					IL_C9:
					num3 += num2;
					num6++;
					num = 24;
					continue;
					IL_E7:
					spr_u.ᜂ()[num6 + 1] = (ushort)num2;
					num = 29;
					continue;
					IL_1B3:
					num = 27;
					continue;
					IL_1D9:
					A_0.ᜀ(spr_u);
					num8++;
					list.Clear();
					num9++;
					num = 20;
					continue;
					IL_270:
					num = 21;
					continue;
					IL_38C:
					num2 = num4;
					num = 1;
				}
				IL_C4:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
				IL_3DA:
				if (true)
				{
				}
				return num8;
			}
			}
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000FE0BC File Offset: 0x000FD0BC
		private int ᜀ(RecordArrayList A_0, List<sprᱧ> A_1, int A_2, ref int A_3, ref int A_4, int A_5, int A_6, int A_7, ExcelVersion A_8)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 26;
				for (;;)
				{
					spr\u20BA spr_u20BA;
					sprᱧ sprᱧ;
					int num3;
					switch (num)
					{
					case 0:
						if (spr_u20BA != null)
						{
							num = 22;
							continue;
						}
						num = 10;
						continue;
					case 1:
						A_2++;
						num = 4;
						continue;
					case 2:
						goto IL_328;
					case 3:
						goto IL_24C;
					case 4:
						goto IL_275;
					case 5:
						num = 8;
						continue;
					case 6:
						goto IL_3B2;
					case 7:
					{
						int num2;
						num2++;
						num = 29;
						continue;
					}
					case 8:
						if (sprᱧ.ᜈ() > 0)
						{
							num = 15;
							continue;
						}
						goto IL_193;
					case 9:
						spr_u20BA = sprᱧ.ᜀ(this.ᜃ);
						spr_u20BA.ᜀ(this.ᜂ as XlsWorksheet);
						spr_u20BA.ᜆ((ushort)(A_2 - 1));
						num = 2;
						continue;
					case 10:
					{
						int num2;
						if (A_2 == num2)
						{
							num = 7;
							continue;
						}
						goto IL_24C;
					}
					case 11:
						goto IL_404;
					case 12:
						if (sprᱧ != null)
						{
							num = 5;
							continue;
						}
						goto IL_193;
					case 13:
					{
						if (A_1 == null)
						{
							num = 14;
							continue;
						}
						num3 = 0;
						int num2 = A_2;
						double num4 = (double)this.ᜂ.DefaultPrintRowHeight;
						num = 21;
						continue;
					}
					case 14:
						goto IL_443;
					case 15:
						A_1.Add(sprᱧ);
						num = 28;
						continue;
					case 16:
						goto IL_110;
					case 17:
						goto IL_C2;
					case 18:
						goto IL_328;
					case 19:
					{
						int num2;
						if (A_2 != num2)
						{
							num = 16;
							continue;
						}
						goto IL_404;
					}
					case 20:
						goto IL_2B1;
					case 21:
						goto IL_275;
					case 22:
					{
						A_0.ᜀ(spr_u20BA);
						A_1.Add(sprᱧ);
						int num5 = spr_u20BA.GetStoreSize(A_8) + 4;
						A_4 += num5;
						num = 31;
						continue;
					}
					case 23:
						if (sprᱧ != null)
						{
							num = 9;
							continue;
						}
						spr_u20BA = null;
						num = 18;
						continue;
					case 24:
					{
						spr_u20BA = (spr\u20BA)spr\u175E.ᜀ(TBIFFRecord.Row);
						spr_u20BA.ᜆ((ushort)(A_2 - 1));
						double num4;
						spr_u20BA.ᜄ((ushort)num4);
						spr_u20BA.ᜃ((ushort)this.ᜃ.DefaultXFIndex);
						num = 6;
						continue;
					}
					case 25:
					{
						int num5;
						A_3 += num5;
						num = 3;
						continue;
					}
					case 27:
						if (num3 != 32)
						{
							num = 1;
							continue;
						}
						return A_2;
					case 28:
						if (spr_u20BA == null)
						{
							num = 24;
							continue;
						}
						goto IL_3B2;
					case 29:
						goto IL_24C;
					case 30:
						goto IL_24C;
					case 31:
					{
						int num2;
						if (A_2 != num2)
						{
							num = 25;
							continue;
						}
						goto IL_24C;
					}
					case 32:
						if (A_2 <= A_5)
						{
							int num6;
							int num7;
							sprᱧ = this.ᜀ(A_2, A_6, A_7, out num6, out num7, A_8);
							num = 23;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_110;
						default:
							if (false)
							{
							}
							num = 20;
							continue;
						}
						break;
					}
					if (A_0 == null)
					{
						num = 17;
						continue;
					}
					num = 13;
					continue;
					IL_110:
					if (true)
					{
					}
					int num8;
					A_3 += num8;
					num = 11;
					continue;
					IL_193:
					num = 0;
					continue;
					IL_24C:
					num3++;
					num = 27;
					continue;
					IL_275:
					num = 32;
					continue;
					IL_328:
					num = 12;
					continue;
					IL_3B2:
					spr_u20BA.ᜅ((ushort)sprᱧ.\u171C());
					spr_u20BA.ᜀ((ushort)sprᱧ.\u171E());
					num8 = spr_u20BA.MaximumRecordSize + 4;
					num = 19;
					continue;
					IL_404:
					A_4 += num8;
					A_0.ᜀ(spr_u20BA);
					num = 30;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ⡊≌㵎㕐⁒", a_));
				IL_2B1:
				return A_2;
				IL_443:
				throw new ArgumentNullException(RecordTableEnumerator.b("㕆⡈╊⩌⩎≐", a_));
			}
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x000FE514 File Offset: 0x000FD514
		[CLSCompliant(false)]
		internal sprᱧ ᜀ(int A_0, int A_1, int A_2, out int A_3, out int A_4, ExcelVersion A_5)
		{
			sprᱧ sprᱧ;
			for (;;)
			{
				sprᱧ = this.ᜀ.ᜄ().ᜁ(A_0 - 1);
				A_3 = int.MinValue;
				A_4 = int.MaxValue;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (A_5 != sprᱧ.ᜆ())
						{
							goto IL_F6;
						}
						goto IL_66;
					case 2:
						sprᱧ = (sprᱧ)sprᱧ.ᜀ(IntPtr.Zero);
						sprᱧ.ᜀ(A_5, base.AppImplementation.ᜨ());
						if (true)
						{
						}
						num = 4;
						continue;
					case 3:
						if (sprᱧ != null)
						{
							num = 0;
							continue;
						}
						return sprᱧ;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F6;
						default:
							if (false)
							{
							}
							goto IL_66;
						}
						break;
					case 5:
						return sprᱧ;
					}
					break;
					IL_66:
					A_3 = sprᱧ.\u171C() + 1;
					A_4 = sprᱧ.\u171E() + 1;
					num = 5;
					continue;
					IL_F6:
					num = 2;
				}
			}
			return sprᱧ;
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000FE628 File Offset: 0x000FD628
		[CLSCompliant(false)]
		internal void ᜀ(sprἛ A_0, bool A_1, Dictionary<int, int> A_2, IDecryptor A_3)
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
			this.ᜀ.ᜀ(A_0, A_1, this.ᜃ.InnerSST, this.ᜂ as XlsWorksheet, A_3);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000FE688 File Offset: 0x000FD688
		[CLSCompliant(false)]
		internal bool ᜀ(spr\u218B A_0, sprἛ A_1, bool A_2, Dictionary<int, int> A_3)
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
			return this.ᜀ.ᜀ(A_0, A_1, A_2, this.ᜃ.InnerSST, this.ᜂ as XlsWorksheet);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000FE6E8 File Offset: 0x000FD6E8
		[CLSCompliant(false)]
		internal void ᜀ(BiffRecordRaw A_0, bool A_1)
		{
			int a_ = 11;
			int num = 3;
			for (;;)
			{
				TBIFFRecord typeCode;
				switch (num)
				{
				case 0:
					switch (typeCode)
					{
					case TBIFFRecord.MulRK:
						goto IL_78;
					case TBIFFRecord.MulBlank:
						goto IL_44;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_86;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					}
					break;
				case 1:
					goto IL_5A;
				case 2:
					goto IL_42;
				case 4:
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				IL_86:
				typeCode = A_0.TypeCode;
				num = 0;
			}
			IL_42:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂♄⡆㭈⽊᥌⁎ၐ㝒ㅔ", a_));
			IL_44:
			this.ᜀ((sprᲀ)A_0, A_1);
			return;
			IL_5A:
			this.ᜀ((spr\u23A5)A_0, A_1);
			return;
			IL_78:
			this.ᜀ((sprᨾ)A_0, A_1);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x000FE7DC File Offset: 0x000FD7DC
		[CLSCompliant(false)]
		internal void ᜀ(spr\u23A5 A_0, bool A_1)
		{
			int a_ = 14;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					A_0.ᜀ((ushort)this.ᜃ.DefaultXFIndex);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8F;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 1:
					goto IL_38;
				case 2:
					if (A_1)
					{
						goto IL_8F;
					}
					goto IL_B0;
				case 3:
					goto IL_78;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
				IL_8F:
				num = 0;
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("❃⍅⑇♉", a_));
			IL_78:
			IL_B0:
			this.ᜁ(A_0.ᜄ() + 1, A_0.ᜅ() + 1, A_0);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x000FE8B0 File Offset: 0x000FD8B0
		private void ᜀ(sprᨾ A_0, bool A_1)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_121;
					case 2:
						return;
					case 3:
						goto IL_115;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_121;
						default:
							if (false)
							{
							}
							goto IL_115;
						}
						break;
					case 5:
						goto IL_4D;
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					List<sprᨾ.ᜀ> list = A_0.ᜀ();
					int a_2 = A_0.\u1714();
					int num2 = A_0.ᜅ();
					int num3 = 0;
					int num4 = A_0.ᜁ();
					num = 4;
					continue;
					IL_121:
					if (num2 > num4)
					{
						num = 2;
						continue;
					}
					sprỔ sprỔ = (sprỔ)this.ᜅ.ᜀ(638);
					sprỔ.ᜀ(list[num3]);
					sprỔ.ᜇ(a_2);
					sprỔ.ᜆ(num2);
					this.ᜀ(sprỔ, A_1);
					num2++;
					num3++;
					if (true)
					{
					}
					num = 3;
					continue;
					IL_115:
					num = 1;
				}
				IL_4D:
				throw new ArgumentNullException(RecordTableEnumerator.b("⡄㉆╈᥊ٌ", a_));
			}
			}
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000FE9F4 File Offset: 0x000FD9F4
		private void ᜀ(sprᲀ A_0, bool A_1)
		{
			int a_ = 17;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BC;
				case 1:
					goto IL_68;
				case 2:
				{
					int num2 = A_0.ᜆ();
					int num3 = A_0.ᜁ();
					num = 7;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D8;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					}
					break;
				case 4:
					if (!A_1)
					{
						num = 2;
						continue;
					}
					return;
				case 5:
				{
					int num2;
					int num3;
					if (num2 > num3)
					{
						num = 6;
						continue;
					}
					this.ᜀ(A_0.ᜃ(num2), A_1);
					num2++;
					num = 0;
					continue;
				}
				case 6:
					return;
				case 7:
					goto IL_BC;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				goto IL_D8;
				IL_BC:
				num = 5;
				continue;
				IL_D8:
				num = 4;
			}
			IL_68:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩆㱈❊ཌ⍎ぐ㵒㹔", a_));
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000FEAF4 File Offset: 0x000FDAF4
		private void ᜀ(spr᱒ A_0, spr\u21DF A_1, bool A_2)
		{
			int a_ = 9;
			if (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.ᜀ(A_0, A_2);
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("夾⹀ㅂ⡄㉆╈⩊", a_));
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000FEB5C File Offset: 0x000FDB5C
		internal bool ᜁ(spr᱒ A_0)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					FormulaToken tokenCode;
					switch (num)
					{
					case 0:
						goto IL_BA;
					case 1:
						goto IL_BA;
					case 2:
						goto IL_9D;
					case 3:
						goto IL_125;
					case 4:
						return false;
					case 5:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 4;
							continue;
						}
						Ptg[] array;
						tokenCode = array[num2].TokenCode;
						num = 3;
						continue;
					}
					case 7:
					{
						if (FormulaUtil.ᜀ(FormulaUtil.\u171C, tokenCode) != -1)
						{
							num = 2;
							continue;
						}
						int num2;
						num2++;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					case 8:
						num = 7;
						continue;
					case 9:
						goto IL_79;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_125:
						if (FormulaUtil.ᜀ(FormulaUtil.\u171D, tokenCode) == -1)
						{
							num = 8;
							continue;
						}
						return true;
					default:
					{
						if (false)
						{
						}
						if (A_0 == null)
						{
							num = 9;
							continue;
						}
						Ptg[] array = A_0.ᜑ();
						int num2 = 0;
						int num3 = array.Length;
						num = 1;
						continue;
					}
					}
					IL_BA:
					num = 5;
				}
				IL_79:
				throw new ArgumentNullException(RecordTableEnumerator.b("堽⼿ぁ⥃㍅⑇⭉", a_));
				IL_9D:
				return true;
			}
			}
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000FECB4 File Offset: 0x000FDCB4
		public XlsCellRecordCollection Clone(object parent)
		{
			int a_ = 18;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (parent == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("㡇⭉㹋⭍㹏♑", a_));
				}
				break;
			}
			if (true)
			{
			}
			XlsCellRecordCollection xlsCellRecordCollection = (XlsCellRecordCollection)base.MemberwiseClone();
			xlsCellRecordCollection.SetParent(parent);
			xlsCellRecordCollection.ᜁ();
			xlsCellRecordCollection.ᜀ = (sprủ)this.ᜀ.ᜀ(xlsCellRecordCollection.ᜂ);
			xlsCellRecordCollection.ᜁ = new SFTable(this.ᜃ.MaxRowCount, this.ᜃ.MaxColumnCount);
			return xlsCellRecordCollection;
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000FED68 File Offset: 0x000FDD68
		[CLSCompliant(false)]
		public void SetRange(long iKey, XlsRange range)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					case 1:
						goto IL_4C;
					default:
						goto IL_4C;
					}
					IL_5C:
					int iRow = sprṔ.ᜁ(iKey);
					int iColumn = sprṔ.ᜀ(iKey);
					this.SetRange(iRow, iColumn, range);
					num = 1;
					continue;
					IL_4C:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_5C;
				}
				case 1:
					return;
				}
				if (!this.ᜄ)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x000FEDF4 File Offset: 0x000FDDF4
		[CLSCompliant(false)]
		public void SetRange(int iRow, int iColumn, XlsRange range)
		{
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						break;
					}
					this.ᜁ[iRow - 1, iColumn - 1] = range;
					num = 0;
					continue;
				}
				if (!this.ᜄ)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000FEE7C File Offset: 0x000FDE7C
		public XlsRange GetRange(long iKey)
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
			int iRow = sprṔ.ᜁ(iKey);
			int iColumn = sprṔ.ᜀ(iKey);
			return this.GetRange(iRow, iColumn);
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000FEED0 File Offset: 0x000FDED0
		public XlsRange GetRange(int iRow, int iColumn)
		{
			XlsRange result;
			for (;;)
			{
				result = null;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							result = (this.ᜁ[iRow - 1, iColumn - 1] as XlsRange);
							num = 0;
							continue;
						}
						break;
					case 2:
						if (this.ᜄ)
						{
							num = 1;
							continue;
						}
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000FEF60 File Offset: 0x000FDF60
		[CLSCompliant(false)]
		internal void ᜁ(long A_0, spr\u23A5 A_1)
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
			this[A_0] = A_1;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000FEFA4 File Offset: 0x000FDFA4
		[CLSCompliant(false)]
		internal void ᜁ(int A_0, int A_1, spr\u23A5 A_2)
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
			this[A_0, A_1] = A_2;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000FEFE8 File Offset: 0x000FDFE8
		[CLSCompliant(false)]
		internal spr\u23A5 ᜄ(long A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this[A_0];
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000FF02C File Offset: 0x000FE02C
		[CLSCompliant(false)]
		internal spr\u23A5 ᜄ(int A_0, int A_1)
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
			return this[A_0, A_1];
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000FF070 File Offset: 0x000FE070
		public void ClearRange(Rectangle rect)
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						int top = rect.Top;
						int left = rect.Left;
						int bottom = rect.Bottom;
						int right = rect.Right;
						int a_ = this.ᜃ.AppImplementation.ᜨ();
						int num = top;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								sprᱧ sprᱧ;
								if (sprᱧ != null)
								{
									num2 = 1;
									continue;
								}
								goto IL_76;
							}
							case 1:
							{
								sprᱧ sprᱧ;
								sprᱧ.ᜁ(left, right, a_);
								num2 = 6;
								continue;
							}
							case 2:
								goto IL_F8;
							case 3:
							{
								if (num > bottom)
								{
									num2 = 4;
									continue;
								}
								sprᱧ sprᱧ = this.ᜀ.ᜄ().ᜁ(num);
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num2 = 0;
									continue;
								}
								break;
							}
							case 4:
								return;
							case 5:
								goto IL_F8;
							case 6:
								goto IL_76;
							}
							break;
							IL_76:
							num++;
							num2 = 5;
							continue;
							IL_F8:
							if (true)
							{
							}
							num2 = 3;
						}
					}
					break;
				}
			}
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000FF19C File Offset: 0x000FE19C
		public void CopyCells(XlsCellRecordCollection sourceCells, Dictionary<string, string> hashStyleNames, Dictionary<string, string> hashWorksheetNames, Dictionary<int, int> hashExtFormatIndexes, Dictionary<int, int> dicNewNameIndexes, Dictionary<int, int> dicFontIndexes, Dictionary<int, int> dictExternSheet)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			XlsWorkbook xlsWorkbook = sourceCells.ᜃ;
			SSTDictionary a_ = xlsWorkbook.InnerSST;
			SSTDictionary a_2 = this.ᜃ.InnerSST;
			this.Clear();
			this.ᜀ.ᜀ(sourceCells.ᜀ, a_, a_2, hashExtFormatIndexes, hashWorksheetNames, dicNewNameIndexes, dicFontIndexes, dictExternSheet);
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000FF214 File Offset: 0x000FE214
		public RichTextString GetRTFString(long iCellIndex, bool bAutofitRows)
		{
			spr\u23A5 spr_u23A;
			spr\u223A spr_u223A;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						spr_u23A = this.ᜄ(iCellIndex);
						int num = 0;
						for (;;)
						{
							string formulaStringValue;
							double number;
							switch (num)
							{
							case 0:
							{
								if (spr_u23A == null)
								{
									num = 3;
									continue;
								}
								TBIFFRecord typeCode = spr_u23A.get_TypeCode();
								num = 13;
								continue;
							}
							case 1:
								goto IL_220;
							case 2:
								num = 17;
								continue;
							case 3:
								goto IL_96;
							case 4:
							{
								TBIFFRecord typeCode;
								switch (typeCode)
								{
								case TBIFFRecord.Number:
									goto IL_C9;
								case TBIFFRecord.Label:
									goto IL_2F9;
								case TBIFFRecord.BoolErr:
									goto IL_222;
								default:
									num = 21;
									continue;
								}
								break;
							}
							case 5:
								num = 16;
								continue;
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									spr_u223A.ᜁ(this.ᜀ(spr_u23A as spr᱒));
									num = 1;
									continue;
								}
								break;
							case 7:
								num = 14;
								continue;
							case 8:
								goto IL_14E;
							case 9:
								goto IL_2F4;
							case 10:
								num = 11;
								continue;
							case 11:
							{
								TBIFFRecord typeCode;
								if (typeCode != TBIFFRecord.Formula)
								{
									num = 7;
									continue;
								}
								goto IL_C9;
							}
							case 12:
								num = 18;
								continue;
							case 13:
							{
								TBIFFRecord typeCode;
								if (typeCode <= TBIFFRecord.LabelSST)
								{
									num = 10;
									continue;
								}
								num = 4;
								continue;
							}
							case 14:
							{
								TBIFFRecord typeCode;
								if (typeCode != TBIFFRecord.LabelSST)
								{
									num = 12;
									continue;
								}
								goto IL_2D8;
							}
							case 15:
								spr_u223A.ᜁ(formulaStringValue);
								num = 9;
								continue;
							case 16:
								goto IL_1EC;
							case 17:
								if (double.IsNaN(number))
								{
									num = 6;
									continue;
								}
								goto IL_12A;
							case 18:
								goto IL_1FD;
							case 19:
							{
								TBIFFRecord typeCode;
								if (typeCode != TBIFFRecord.RK)
								{
									num = 5;
									continue;
								}
								goto IL_C9;
							}
							case 20:
								if (formulaStringValue != null)
								{
									num = 15;
									continue;
								}
								number = this.GetNumber(iCellIndex);
								num = 22;
								continue;
							case 21:
								num = 19;
								continue;
							case 22:
								if (spr_u23A.get_TypeCode() == TBIFFRecord.Formula)
								{
									num = 2;
									continue;
								}
								goto IL_12A;
							}
							break;
							IL_C9:
							spr_u223A = new spr\u223A();
							formulaStringValue = this.GetFormulaStringValue(iCellIndex);
							num = 20;
							continue;
							IL_12A:
							sprᤅ sprᤅ = this.ᜁ(iCellIndex);
							spr_u223A.ᜁ(sprᤅ.ᜀ(number, true));
							num = 8;
						}
					}
					break;
				}
			}
			IL_96:
			return null;
			IL_14E:
			IL_1C7:
			return new RangeRichTextString((spr\u2158)base.ReservedHandle, this.ᜂ, iCellIndex, spr_u223A);
			IL_1EC:
			IL_1FD:
			goto IL_2F9;
			IL_220:
			goto IL_1C7;
			IL_222:
			spr\u223A spr_u223A2 = new spr\u223A();
			spr_u223A2.ᜁ(XlsRange.ᜀ((spr\u249B)spr_u23A));
			return new RangeRichTextString((spr\u2158)base.ReservedHandle, this.ᜂ, iCellIndex, spr_u223A2);
			IL_2D8:
			return this.GetLabelSSTRTFString(iCellIndex, bAutofitRows);
			IL_2F4:
			goto IL_1C7;
			IL_2F9:
			return null;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000FF51C File Offset: 0x000FE51C
		public void FillRTFString(long cellIndex, bool bAutofitRows, RichTextString richText)
		{
			switch (0)
			{
			default:
			{
				spr\u23A5 spr_u23A;
				string text;
				for (;;)
				{
					richText.ClearFormatting();
					richText.Text = string.Empty;
					spr_u23A = this.ᜄ(cellIndex);
					int num = 3;
					for (;;)
					{
						spr\u192F spr_u192F;
						switch (num)
						{
						case 0:
							num = 15;
							continue;
						case 1:
							return;
						case 2:
							goto IL_17A;
						case 3:
						{
							if (spr_u23A == null)
							{
								num = 1;
								continue;
							}
							TBIFFRecord typeCode = spr_u23A.get_TypeCode();
							num = 7;
							continue;
						}
						case 4:
							return;
						case 5:
							goto IL_147;
						case 6:
							if (spr_u23A.get_TypeCode() == TBIFFRecord.Formula)
							{
								num = 0;
								continue;
							}
							goto IL_2D5;
						case 7:
						{
							TBIFFRecord typeCode;
							if (typeCode <= TBIFFRecord.LabelSST)
							{
								num = 17;
								continue;
							}
							if (true)
							{
							}
							num = 20;
							continue;
						}
						case 8:
							if (text == null)
							{
								num = 23;
								continue;
							}
							goto IL_1D6;
						case 9:
							goto IL_216;
						case 10:
							goto IL_CC;
						case 11:
						{
							double number;
							if (!double.IsNaN(number))
							{
								num = 22;
								continue;
							}
							text = string.Empty;
							num = 2;
							continue;
						}
						case 12:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.Formula)
							{
								num = 19;
								continue;
							}
							goto IL_D1;
						}
						case 13:
							text = this.ᜀ(spr_u23A as spr᱒);
							num = 10;
							continue;
						case 14:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.RK)
							{
								num = 4;
								continue;
							}
							goto IL_D1;
						}
						case 15:
						{
							double number;
							if (double.IsNaN(number))
							{
								num = 13;
								continue;
							}
							goto IL_2D5;
						}
						case 16:
							goto IL_245;
						case 17:
							num = 12;
							continue;
						case 18:
							num = 14;
							continue;
						case 19:
							num = 21;
							continue;
						case 20:
						{
							TBIFFRecord typeCode;
							switch (typeCode)
							{
							case TBIFFRecord.Number:
								goto IL_D1;
							case TBIFFRecord.Label:
								return;
							case TBIFFRecord.BoolErr:
								richText.ClearFormatting();
								richText.Text = XlsRange.ᜀ((spr\u249B)spr_u23A);
								num = 16;
								continue;
							default:
								num = 18;
								continue;
							}
							break;
						}
						case 21:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.LabelSST)
							{
								num = 5;
								continue;
							}
							goto IL_1E4;
						}
						case 22:
						{
							sprᤅ sprᤅ = spr_u192F.\u1718();
							double number;
							text = sprᤅ.ᜀ(number, true);
							num = 9;
							continue;
						}
						case 23:
						{
							double number = this.GetNumber(cellIndex);
							num = 6;
							continue;
						}
						}
						break;
						IL_D1:
						text = this.GetFormulaStringValue(cellIndex);
						int a_ = (int)spr_u23A.ᜆ();
						spr_u192F = this.ᜃ.InnerExtFormats.ᜁ(a_);
						richText.DefaultFontIndex = spr_u192F.\u173B();
						num = 8;
						continue;
						IL_2D5:
						num = 11;
					}
				}
				return;
				IL_CC:
				goto IL_1D6;
				IL_147:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				return;
				IL_17A:
				IL_1D6:
				richText.ClearFormatting();
				richText.Text = text;
				return;
				IL_1E4:
				spr\u1C7C a_2 = (spr\u1C7C)spr_u23A;
				this.ᜀ(a_2, bAutofitRows, richText);
				return;
				IL_216:
				goto IL_1D6;
				IL_245:
				return;
			}
			}
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000FF858 File Offset: 0x000FE858
		public RichTextString GetLabelSSTRTFString(long iCellIndex, bool bAutofitRows)
		{
			RangeRichTextString rangeRichTextString;
			string text;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					if (true)
					{
					}
					for (;;)
					{
						rangeRichTextString = ((XlsWorksheet)this.sheet).CreateLabelSSTRTFString(iCellIndex);
						sprᤅ sprᤅ = this.ᜁ(iCellIndex);
						text = sprᤅ.ᜀ(rangeRichTextString.Text, true);
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (text == rangeRichTextString.Text)
								{
									return rangeRichTextString;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num = 3;
									continue;
								}
								break;
							case 1:
								if (bAutofitRows)
								{
									num = 2;
									continue;
								}
								goto IL_BE;
							case 2:
								goto IL_BC;
							case 3:
								num = 1;
								continue;
							}
							break;
						}
					}
					break;
				}
			}
			return rangeRichTextString;
			IL_BC:
			return rangeRichTextString;
			IL_BE:
			IFont font = this.ᜀ(iCellIndex);
			RichTextString richTextString = new RichTextString((spr\u2158)base.ReservedHandle, this.ᜃ, false, true);
			richTextString.Text = text;
			richTextString.SetFont(0, text.Length - 1, font);
			return richTextString;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000FF960 File Offset: 0x000FE960
		[CLSCompliant(false)]
		internal void ᜀ(spr\u1C7C A_0, bool A_1, RichTextString A_2)
		{
			spr\u192F spr_u192F;
			string text;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜀ(A_2, A_0.ᜁ());
						int a_ = (int)A_0.\u1712();
						spr_u192F = this.ᜃ.InnerExtFormats.ᜁ(a_);
						int a_2 = spr_u192F.ᝊ();
						sprᤅ sprᤅ = this.ᜃ.InnerFormats.ᜁ(a_2);
						text = sprᤅ.ᜀ(A_2.Text, true);
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_107;
							case 1:
								if (true)
								{
								}
								num = 2;
								continue;
							case 2:
								if (!A_1)
								{
									num = 0;
									continue;
								}
								goto IL_109;
							case 3:
								if (text == A_2.Text)
								{
									goto IL_109;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							}
							break;
						}
					}
					break;
				}
			}
			IL_107:
			IFont font = spr_u192F.ᜀ();
			A_2.Text = text;
			A_2.SetFont(0, text.Length - 1, font);
			return;
			IL_109:
			A_2.DefaultFontIndex = spr_u192F.\u173B();
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000FFA84 File Offset: 0x000FEA84
		public string GetText(long iCellIndex)
		{
			switch (0)
			{
			default:
			{
				spr\u23A5 spr_u23A;
				for (;;)
				{
					IL_4B:
					spr_u23A = this.ᜄ(iCellIndex);
					int num = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D6;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								num = 4;
								continue;
							case 1:
								if (true)
								{
								}
								if (spr_u23A != null)
								{
									num = 0;
									continue;
								}
								goto IL_FD;
							case 2:
								goto IL_A1;
							case 3:
								goto IL_D4;
							case 4:
								if (spr_u23A.get_TypeCode() == TBIFFRecord.Label)
								{
									num = 3;
									continue;
								}
								num = 5;
								continue;
							case 5:
								if (spr_u23A.get_TypeCode() == TBIFFRecord.LabelSST)
								{
									num = 2;
									continue;
								}
								goto IL_FD;
							}
							goto IL_4B;
						}
					}
				}
				IL_A1:
				goto IL_D6;
				IL_D4:
				return ((spr\u2170)spr_u23A).ᜁ();
				IL_D6:
				spr\u1C7C spr_u1C7C = (spr\u1C7C)spr_u23A;
				int a_ = spr_u1C7C.ᜁ();
				spr\u223A spr_u223A = this.ᜃ.InnerSST[a_];
				return spr_u223A.ᜏ();
				IL_FD:
				return null;
			}
			}
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000FFB90 File Offset: 0x000FEB90
		public string GetError(long iCellIndex)
		{
			int a_ = 5;
			string result;
			for (;;)
			{
				for (;;)
				{
					spr\u249B spr_u249B = this.ᜄ(iCellIndex) as spr\u249B;
					int num = 3;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
						{
							result = RecordTableEnumerator.b("ᠺ猼ှ@", a_);
							int key = (int)spr_u249B.ᜄ();
							num = 1;
							continue;
						}
						case 1:
						{
							int key;
							if (FormulaUtil.ErrorCodeToName.ContainsKey(key))
							{
								num = 5;
								continue;
							}
							return result;
						}
						case 2:
							num = 4;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (spr_u249B != null)
								{
									num = 2;
									continue;
								}
								goto IL_F6;
							}
							break;
						case 4:
							if (spr_u249B.ᜂ())
							{
								num = 0;
								continue;
							}
							goto IL_F6;
						case 5:
						{
							int key;
							result = FormulaUtil.ErrorCodeToName[key];
							num = 6;
							continue;
						}
						case 6:
							return result;
						}
						break;
					}
				}
			}
			return result;
			IL_F6:
			return null;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000FFC94 File Offset: 0x000FEC94
		public bool GetBool(long iCellIndex, out bool value)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4F:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_34;
			}
			spr\u249B spr_u249B;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_90;
				case 1:
					num = 2;
					continue;
				case 2:
					if (true)
					{
					}
					if (!spr_u249B.ᜂ())
					{
						num = 0;
						continue;
					}
					return false;
				case 3:
					goto IL_4C;
				}
				goto IL_34;
			}
			IL_4C:
			if (spr_u249B != null)
			{
				goto IL_4F;
			}
			return false;
			IL_90:
			value = (spr_u249B.ᜄ() > 0);
			return true;
			IL_34:
			spr_u249B = (this.ᜄ(iCellIndex) as spr\u249B);
			value = false;
			num = 3;
			goto IL_1E;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000FFD34 File Offset: 0x000FED34
		public bool ContainNumber(long iCellIndex)
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
			spr\u2230 spr_u = this.ᜄ(iCellIndex) as spr\u2230;
			return spr_u != null;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x000FFD84 File Offset: 0x000FED84
		public bool ContainBoolOrError(long iCellIndex)
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
			spr\u249B spr_u249B = this.ᜄ(iCellIndex) as spr\u249B;
			return spr_u249B != null;
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000FFDD4 File Offset: 0x000FEDD4
		public bool ContainFormulaNumber(long iCellIndex)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4C:
				if (true)
				{
				}
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_34;
			}
			spr᱒ spr᱒;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_8A;
				case 1:
					num = 2;
					continue;
				case 2:
					if (!spr᱒.ᜋ())
					{
						num = 0;
						continue;
					}
					return false;
				case 3:
					goto IL_49;
				}
				goto IL_34;
			}
			IL_49:
			if (spr᱒ != null)
			{
				goto IL_4C;
			}
			return false;
			IL_8A:
			return !spr᱒.ᜄ();
			IL_34:
			spr᱒ = (this.ᜄ(iCellIndex) as spr᱒);
			num = 3;
			goto IL_1E;
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000FFE70 File Offset: 0x000FEE70
		public bool ContainFormulaBoolOrError(long iCellIndex)
		{
			spr᱒ spr᱒;
			for (;;)
			{
				for (;;)
				{
					spr᱒ = (this.ᜄ(iCellIndex) as spr᱒);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (!spr᱒.ᜋ())
							{
								num = 1;
								continue;
							}
							return true;
						case 1:
							goto IL_8C;
						case 2:
							if (spr᱒ != null)
							{
								num = 3;
								continue;
							}
							return false;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						break;
					}
				}
			}
			return true;
			IL_8C:
			return spr᱒.ᜄ();
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000FFF0C File Offset: 0x000FEF0C
		public double GetNumber(long iCellIndex)
		{
			if (true)
			{
			}
			spr\u2230 spr_u;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				spr_u = (this.ᜄ(iCellIndex) as spr\u2230);
				if (spr_u == null)
				{
					return double.MinValue;
				}
				break;
			}
			return spr_u.ᜀ();
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000FFF6C File Offset: 0x000FEF6C
		public double GetNumberWithoutFormula(long iCellIndex)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_54:
				num = 3;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_3C;
			}
			spr\u2230 spr_u;
			for (;;)
			{
				IL_26:
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					goto IL_51;
				case 2:
					if (spr_u.get_TypeCode() == TBIFFRecord.Formula)
					{
						num = 0;
						continue;
					}
					goto IL_8D;
				case 3:
					num = 2;
					continue;
				}
				goto IL_3C;
			}
			IL_51:
			if (spr_u != null)
			{
				goto IL_54;
			}
			IL_5E:
			return double.MinValue;
			IL_8B:
			goto IL_5E;
			IL_8D:
			return spr_u.ᜀ();
			IL_3C:
			spr_u = (this.ᜄ(iCellIndex) as spr\u2230);
			num = 1;
			goto IL_26;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0010000C File Offset: 0x000FF00C
		public double GetFormulaNumberValue(long iCellIndex)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				spr᱒ spr᱒ = this.ᜄ(iCellIndex) as spr᱒;
				if (spr᱒ != null)
				{
					return spr᱒.ᜌ();
				}
				break;
			}
			}
			if (true)
			{
			}
			return double.MinValue;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0010006C File Offset: 0x000FF06C
		public void SetStringValue(long iCellIndex, string strValue)
		{
			int a_ = 10;
			if (true)
			{
			}
			int a_3;
			sprᱧ sprᱧ;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				int a_2 = sprṔ.ᜁ(iCellIndex) - 1;
				a_3 = sprṔ.ᜀ(iCellIndex) - 1;
				sprᱧ = this.ᜀ.ᜄ().ᜁ(a_2);
				if (sprᱧ == null)
				{
					throw new NotSupportedException(RecordTableEnumerator.b("ᐿ⩁ⵃ㕅桇㩉㹋⅍⁏㝑♓≕⅗穙㕛ⵝ䁟ൡ੣੥ᅧ䩩੫ŭɯ剱ታ᥵੷᝹ॻችꊁﶍ뺏", a_));
				}
				break;
			}
			}
			sprᱧ.ᜀ(a_3, strValue, base.ReservedHandle.\u171D());
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x00100100 File Offset: 0x000FF100
		public string GetFormulaStringValue(long iCellIndex)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			int a_ = sprṔ.ᜁ(iCellIndex) - 1;
			int a_2 = sprṔ.ᜀ(iCellIndex) - 1;
			sprᱧ sprᱧ = this.ᜀ.ᜄ().ᜁ(a_);
			return sprᱧ.ᜌ(a_2);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00100168 File Offset: 0x000FF168
		public DateTime GetDateTime(long iCellIndex)
		{
			double numberWithoutFormula;
			for (;;)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						numberWithoutFormula = this.GetNumberWithoutFormula(iCellIndex);
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (true)
								{
								}
								if (numberWithoutFormula == -1.7976931348623157E+308)
								{
									num = 2;
									continue;
								}
								sprᤅ sprᤅ = this.ᜁ(iCellIndex);
								CellFormatType cellFormatType = sprᤅ.ᜀ(numberWithoutFormula);
								num = 1;
								continue;
							}
							case 1:
							{
								CellFormatType cellFormatType;
								if (cellFormatType == CellFormatType.DateTime)
								{
									num = 3;
									continue;
								}
								goto IL_A5;
							}
							case 2:
								goto IL_6A;
							case 3:
								goto IL_9D;
							}
							break;
						}
						break;
					}
					}
				}
			}
			IL_6A:
			return DateTime.MinValue;
			IL_9D:
			return UtilityMethods.ᜀ(numberWithoutFormula);
			IL_A5:
			return DateTime.MinValue;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00100220 File Offset: 0x000FF220
		[CLSCompliant(false)]
		internal bool ᜀ(spr\u23A5 A_0, string A_1, IDictionary A_2, long A_3, XlsWorkbook A_4, Dictionary<int, int> A_5, CopyRangeOptions A_6)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 11;
				bool result;
				spr\u23A5 spr_u23A;
				for (;;)
				{
					spr᱒ spr᱒;
					int num2;
					int num3;
					int num5;
					int num6;
					switch (num)
					{
					case 0:
						result = true;
						spr᱒ = (spr᱒)spr_u23A;
						num = 10;
						continue;
					case 1:
						num = 20;
						continue;
					case 2:
						spr_u23A = this.ᜄ(A_3);
						num = 9;
						continue;
					case 3:
						if (spr_u23A.get_TypeCode() == TBIFFRecord.LabelSST)
						{
							num = 6;
							continue;
						}
						goto IL_242;
					case 4:
						if (true)
						{
						}
						goto IL_269;
					case 5:
					{
						spr\u21DF spr_u21DF = (spr\u21DF)spr\u175E.ᜀ(TBIFFRecord.String);
						spr_u21DF.ᜀ(A_1);
						this.ᜀ.ᜀ(num2 + 1, num3 + 1, spr᱒.\u170D, spr_u21DF);
						num = 13;
						continue;
					}
					case 6:
					{
						spr\u1C7C spr_u1C7C = (spr\u1C7C)spr_u23A;
						int num4 = spr_u1C7C.ᜁ();
						SSTDictionary sstdictionary = this.ᜃ.InnerSST;
						SSTDictionary sourceSST = A_4.InnerSST;
						num4 = sstdictionary.AddCopy(num4, sourceSST, A_5);
						spr_u1C7C.ᜀ(num4);
						num = 17;
						continue;
					}
					case 7:
						if (A_1 == null)
						{
							goto IL_373;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_242;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 8:
					{
						bool flag;
						if (!flag)
						{
							num = 1;
							continue;
						}
						num = 18;
						continue;
					}
					case 9:
						goto IL_269;
					case 10:
					{
						if (this.sheet.IsArrayFormula(A_3))
						{
							num = 2;
							continue;
						}
						bool flag = (A_6 & CopyRangeOptions.UpdateFormulas) != CopyRangeOptions.None;
						num = 21;
						continue;
					}
					case 12:
						num5 = num2 - A_0.ᜄ();
						goto IL_1AA;
					case 13:
						goto IL_20D;
					case 14:
						num = 15;
						continue;
					case 15:
						num5 = 0;
						goto IL_1AA;
					case 16:
						goto IL_90;
					case 17:
						goto IL_269;
					case 18:
						num6 = num3 - A_0.ᜅ();
						goto IL_115;
					case 19:
						if (spr_u23A.get_TypeCode() == TBIFFRecord.Formula)
						{
							num = 0;
							continue;
						}
						goto IL_269;
					case 20:
						num6 = 0;
						goto IL_115;
					case 21:
					{
						bool flag;
						if (!flag)
						{
							num = 14;
							continue;
						}
						num = 12;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 16;
						continue;
					}
					result = false;
					num2 = sprṔ.ᜁ(A_3) - 1;
					num3 = sprṔ.ᜀ(A_3) - 1;
					ICloneable cloneable = (ICloneable)A_0;
					spr_u23A = (spr\u23A5)cloneable.Clone();
					num = 3;
					continue;
					IL_115:
					int a_2 = num6;
					int a_3;
					spr᱒.ᜁ(((XlsWorksheet)this.sheet).ᜀ(spr᱒.ᜑ(), a_3, a_2));
					num = 4;
					continue;
					IL_1AA:
					a_3 = num5;
					num = 8;
					continue;
					IL_242:
					num = 19;
					continue;
					IL_269:
					int num7 = (int)spr_u23A.ᜆ();
					num7 = this.ᜀ(num7, A_2, A_6);
					spr_u23A.ᜀ((ushort)num7);
					spr_u23A.ᜄ(num3);
					spr_u23A.ᜃ(num2);
					this.ᜁ(A_3, spr_u23A);
					num = 7;
				}
				IL_90:
				throw new ArgumentNullException(RecordTableEnumerator.b("崽┿⹁⡃", a_));
				IL_20D:
				IL_373:
				sprᜑ.ᜁ(this.ᜂ, spr_u23A.ᜅ() + 1);
				sprᜑ.ᜀ(this.ᜂ, spr_u23A.ᜄ() + 1);
				return result;
			}
			}
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x001005CC File Offset: 0x000FF5CC
		internal sprủ ᜀ(IXLSRange A_0, IXLSRange A_1, out Rectangle A_2)
		{
			int a_ = 6;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
				{
					int num = 10;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							goto IL_160;
						case 1:
						{
							sprᱧ sprᱧ;
							if (sprᱧ != null)
							{
								num = 5;
								continue;
							}
							goto IL_1CB;
						}
						case 2:
							goto IL_84;
						case 3:
							goto IL_2C8;
						case 4:
							goto IL_160;
						case 5:
						{
							sprᱧ sprᱧ;
							sprᱧ a_2 = sprᱧ.ᜃ(A_2.Left - 1, A_2.Right - 1, base.ReservedHandle.\u171D());
							sprủ sprủ;
							sprủ.ᜀ(num2 - 1, a_2);
							num = 15;
							continue;
						}
						case 6:
						{
							if (A_0.Worksheet != A_1.Worksheet)
							{
								if (true)
								{
								}
								num = 8;
								continue;
							}
							int column = A_1.Column;
							int row = A_1.Row;
							int height = A_1.LastRow - row + 1;
							int width = A_1.LastColumn - column + 1;
							Rectangle rectangle = new Rectangle(A_0.Column, A_0.Row, width, height);
							Rectangle rectangle2 = new Rectangle(column, row, width, height);
							num = 12;
							continue;
						}
						case 7:
							if (A_1 == null)
							{
								num = 14;
								continue;
							}
							num = 6;
							continue;
						case 8:
							goto IL_1C9;
						case 9:
							if (A_2.Width != 0)
							{
								num = 13;
								continue;
							}
							goto IL_14F;
						case 11:
						{
							if (num2 >= A_2.Bottom)
							{
								num = 18;
								continue;
							}
							sprᱧ sprᱧ = this.ᜀ.ᜄ().ᜁ(num2 - 1);
							num = 1;
							continue;
						}
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
							{
								if (false)
								{
								}
								Rectangle rectangle;
								Rectangle rectangle2;
								if (!UtilityMethods.ᜀ(rectangle, rectangle2))
								{
									num = 3;
									continue;
								}
								A_2 = Rectangle.Intersect(rectangle, rectangle2);
								num = 9;
								continue;
							}
							}
							break;
						case 13:
							num = 17;
							continue;
						case 14:
							goto IL_14D;
						case 15:
							goto IL_1CB;
						case 16:
							goto IL_DA;
						case 17:
						{
							if (A_2.Height == 0)
							{
								num = 16;
								continue;
							}
							sprủ sprủ = new sprủ(this.ᜃ.MaxRowCount, this.ᜂ);
							num2 = A_2.Top;
							num = 4;
							continue;
						}
						case 18:
						{
							sprủ sprủ;
							return sprủ;
						}
						}
						if (A_0 == null)
						{
							num = 2;
							continue;
						}
						num = 7;
						continue;
						IL_160:
						num = 11;
						continue;
						IL_1CB:
						num2++;
						num = 0;
					}
					break;
				}
				}
			}
			IL_84:
			throw new ArgumentNullException(RecordTableEnumerator.b("堻嬽㌿㙁ⵃ⡅⥇㹉╋⅍㹏", a_));
			IL_DA:
			goto IL_14F;
			IL_14D:
			throw new ArgumentNullException(RecordTableEnumerator.b("伻儽㔿ぁ❃⍅", a_));
			IL_14F:
			A_2 = Rectangle.FromLTRB(-1, -1, -1, -1);
			return null;
			IL_1C9:
			A_2 = Rectangle.FromLTRB(-1, -1, -1, -1);
			return null;
			IL_2C8:
			A_2 = Rectangle.FromLTRB(-1, -1, -1, -1);
			return null;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x001008E0 File Offset: 0x000FF8E0
		public int GetMinimumRowIndex(int iStartColumn, int iEndColumn)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					int firstRow = this.ᜂ.FirstRow;
					num = this.ᜂ.LastRow;
					int num2 = firstRow;
					int num3 = 7;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							int num4;
							if (num4 > iEndColumn)
							{
								num3 = 4;
								continue;
							}
							num3 = 2;
							continue;
						}
						case 1:
							goto IL_6B;
						case 2:
						{
							int num4;
							if (this.ᜀ.ᜊ(num2 - 1, num4 - 1))
							{
								if (true)
								{
								}
								num3 = 9;
								continue;
							}
							num4++;
							num3 = 5;
							continue;
						}
						case 3:
							goto IL_134;
						case 4:
							goto IL_124;
						case 5:
							goto IL_6B;
						case 6:
							return num;
						case 7:
							goto IL_F3;
						case 8:
							goto IL_124;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_134;
							}
							if (false)
							{
							}
							num = num2;
							num3 = 8;
							continue;
						case 10:
						{
							if (num2 >= num)
							{
								num3 = 6;
								continue;
							}
							int num4 = iStartColumn;
							num3 = 1;
							continue;
						}
						}
						break;
						IL_6B:
						num3 = 0;
						continue;
						IL_F3:
						num3 = 10;
						continue;
						IL_134:
						goto IL_F3;
						IL_124:
						num2++;
						num3 = 3;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00100A34 File Offset: 0x000FFA34
		public int GetMaximumRowIndex(int iStartColumn, int iEndColumn)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					if (true)
					{
					}
					int lastRow = this.ᜂ.LastRow;
					num = this.ᜂ.FirstRow;
					int num2 = lastRow;
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							long key;
							if (this.Contains(key))
							{
								num3 = 8;
								continue;
							}
							int num4;
							num4++;
							num3 = 1;
							continue;
						}
						case 1:
							goto IL_70;
						case 2:
							goto IL_13E;
						case 3:
							goto IL_C7;
						case 4:
							goto IL_70;
						case 5:
							goto IL_C7;
						case 6:
							goto IL_102;
						case 7:
						{
							int num4;
							if (num4 > iEndColumn)
							{
								num3 = 6;
								continue;
							}
							long key = sprṔ.ᜀ(num4, num2);
							num3 = 0;
							continue;
						}
						case 8:
							num = num2;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13E;
							default:
								if (false)
								{
								}
								num3 = 2;
								continue;
							}
							break;
						case 9:
							return num;
						case 10:
						{
							if (num2 < num)
							{
								num3 = 9;
								continue;
							}
							int num4 = iStartColumn;
							num3 = 4;
							continue;
						}
						}
						break;
						IL_70:
						num3 = 7;
						continue;
						IL_C7:
						num3 = 10;
						continue;
						IL_102:
						num2--;
						num3 = 5;
						continue;
						IL_13E:
						goto IL_102;
					}
				}
				return num;
			}
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00100B84 File Offset: 0x000FFB84
		public int GetMinimumColumnIndex(int iStartRow, int iEndRow)
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
			return this.ᜀ.ᜀ(iStartRow - 1, iEndRow - 1) + 1;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00100BD4 File Offset: 0x000FFBD4
		public int GetMaximumColumnIndex(int iStartRow, int iEndRow)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜀ.ᜉ(iStartRow - 1, iEndRow - 1) + 1;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00100C24 File Offset: 0x000FFC24
		public string GetFormula(long iCelIndex)
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
			return this.GetFormula(iCelIndex, false);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00100C68 File Offset: 0x000FFC68
		public string GetFormula(long iCelIndex, bool isR1C1)
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
			return this.GetFormula(iCelIndex, isR1C1, null);
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00100CAC File Offset: 0x000FFCAC
		public string GetFormula(long iCelIndex, bool isR1C1, NumberFormatInfo numberInfo)
		{
			int a_ = 7;
			spr᱒ spr᱒ = this.ᜄ(iCelIndex) as spr᱒;
			if (spr᱒ != null)
			{
				string result;
				try
				{
					FormulaUtil formulaUtil = this.ᜃ.FormulaUtil;
					result = RecordTableEnumerator.b("<", a_) + formulaUtil.ᜀ(spr᱒.ᜑ(), spr᱒.\u1714(), spr᱒.\u1713(), isR1C1, numberInfo, false);
				}
				catch (Exception)
				{
					result = null;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return result;
				}
			}
			return null;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00100D58 File Offset: 0x000FFD58
		public string GetValue(long cellIndex, int row, int column, IXLSRange range)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 16;
				string text2;
				for (;;)
				{
					double numberWithoutFormula;
					DateTime dateTime;
					switch (num)
					{
					case 0:
						if (numberWithoutFormula != -1.7976931348623157E+308)
						{
							num = 1;
							continue;
						}
						goto IL_213;
					case 1:
						num = 4;
						continue;
					case 2:
						goto IL_EB;
					case 3:
						goto IL_1F3;
					case 4:
						if (numberWithoutFormula == 0.0)
						{
							num = 19;
							continue;
						}
						goto IL_136;
					case 5:
						if (range.Worksheet is Worksheet)
						{
							num = 27;
							continue;
						}
						goto IL_136;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36D;
						default:
							goto IL_1D2;
						}
						break;
					case 7:
					{
						string text;
						if (text != null)
						{
							num = 9;
							continue;
						}
						text = this.GetText(cellIndex);
						num = 25;
						continue;
					}
					case 8:
					{
						bool flag;
						text2 = flag.ToString();
						num = 2;
						continue;
					}
					case 9:
						goto IL_2AC;
					case 10:
					{
						text2 = "";
						bool flag2 = false;
						string text = this.GetFormula(cellIndex);
						num = 7;
						continue;
					}
					case 11:
					{
						string text;
						text2 = text;
						num = 24;
						continue;
					}
					case 12:
						goto IL_20E;
					case 13:
						if (dateTime != DateTime.MinValue)
						{
							num = 20;
							continue;
						}
						return text2;
					case 14:
						if (true)
						{
						}
						goto IL_213;
					case 15:
					{
						string text;
						if (text != null)
						{
							num = 11;
							continue;
						}
						goto IL_353;
					}
					case 17:
					{
						bool flag2;
						if (flag2)
						{
							num = 12;
							continue;
						}
						string text = this.GetError(cellIndex);
						num = 15;
						continue;
					}
					case 18:
						goto IL_213;
					case 19:
						num = 5;
						continue;
					case 20:
						text2 = dateTime.ToString();
						num = 6;
						continue;
					case 21:
					{
						string text;
						text2 = text;
						bool flag2 = true;
						num = 3;
						continue;
					}
					case 22:
						if (!(range.Worksheet as Worksheet).WindowTwo.ᜄ())
						{
							num = 26;
							continue;
						}
						goto IL_136;
					case 23:
					{
						bool flag;
						if (this.GetBool(cellIndex, out flag))
						{
							goto IL_36D;
						}
						goto IL_EB;
					}
					case 24:
						goto IL_353;
					case 25:
					{
						string text;
						if (text != null)
						{
							num = 21;
							continue;
						}
						goto IL_1F3;
					}
					case 26:
						text2 = "";
						num = 14;
						continue;
					case 27:
						num = 22;
						continue;
					}
					if (this.Contains(cellIndex))
					{
						num = 10;
						continue;
					}
					goto IL_37E;
					IL_EB:
					numberWithoutFormula = this.GetNumberWithoutFormula(cellIndex);
					num = 0;
					continue;
					IL_136:
					text2 = numberWithoutFormula.ToString();
					num = 18;
					continue;
					IL_1F3:
					num = 17;
					continue;
					IL_213:
					dateTime = this.GetDateTime(cellIndex);
					num = 13;
					continue;
					IL_353:
					num = 23;
					continue;
					IL_36D:
					num = 8;
				}
				IL_1D2:
				if (false)
				{
				}
				return text2;
				IL_20E:
				text2 = text2.Replace(RecordTableEnumerator.b("橇", a_), RecordTableEnumerator.b("橇桉", a_));
				return '"' + text2 + '"';
				IL_2AC:
				text2 = range[row, column].NumberText;
				return text2;
				IL_37E:
				return string.Empty;
			}
			}
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x001010E8 File Offset: 0x001000E8
		public int GetExtendedFormatIndex(long iCellIndex)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			int row = sprṔ.ᜁ(iCellIndex);
			int column = sprṔ.ᜀ(iCellIndex);
			return this.GetExtendedFormatIndex(row, column);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0010113C File Offset: 0x0010013C
		public int GetExtendedFormatIndex(int row, int column)
		{
			int result;
			for (;;)
			{
				row--;
				column--;
				sprᱧ sprᱧ = this.Table.ᜄ().ᜁ(row);
				result = int.MinValue;
				int num = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					case 1:
						goto IL_22;
					default:
						goto IL_22;
					}
					IL_64:
					if (sprᱧ != null)
					{
						num = 2;
						continue;
					}
					goto IL_83;
					IL_22:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_64;
					case 1:
						goto IL_81;
					case 2:
						result = sprᱧ.\u171C(column);
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_81:
			IL_83:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x001011D8 File Offset: 0x001001D8
		public IFont GetCellFont(long iCellIndex)
		{
			int extendedFormatIndex = this.GetExtendedFormatIndex(iCellIndex);
			if (extendedFormatIndex >= 0)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					spr\u192F spr_u192F = this.ᜃ.InnerExtFormats.ᜁ(extendedFormatIndex);
					return spr_u192F.ᜀ();
				}
				}
			}
			return null;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0010123C File Offset: 0x0010023C
		public void CopyStyle(int iSourceRow, int iSourceColumn, int iDestRow, int iDestColumn)
		{
			if (true)
			{
			}
			spr\u23A5 spr_u23A = this.ᜄ(iSourceRow, iSourceColumn);
			if (spr_u23A != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					ushort iXFIndex = spr_u23A.ᜆ();
					this.SetCellStyle(iDestRow, iDestColumn, (int)iXFIndex);
					return;
				}
				}
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00101298 File Offset: 0x00100298
		[CLSCompliant(false)]
		internal spr\u23A5 ᜀ(int A_0, int A_1, TBIFFRecord A_2)
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
			spr\u23A5 spr_u23A = (spr\u23A5)spr\u175E.ᜀ(A_2);
			spr_u23A.ᜃ(A_0 - 1);
			spr_u23A.ᜄ(A_1 - 1);
			return spr_u23A;
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x001012F4 File Offset: 0x001002F4
		[CLSCompliant(false)]
		internal spr\u23A5 ᜁ(int A_0, int A_1, TBIFFRecord A_2)
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
			spr\u23A5 spr_u23A = this.ᜀ(A_0, A_1, A_2);
			this.ᜁ(A_0, A_1, spr_u23A);
			return spr_u23A;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00101344 File Offset: 0x00100344
		public IStyle GetCellStyle(long iCellIndex)
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
			int index = (int)this.ᜄ(iCellIndex).ᜆ();
			return this.ᜃ.InnerStyles.GetByXFIndex(index);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x001013A0 File Offset: 0x001003A0
		internal IExtendedFormat ᜂ(long A_0)
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
			int a_ = (int)this.ᜄ(A_0).ᜆ();
			return this.ᜃ.InnerExtFormats.ᜁ(a_);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x001013FC File Offset: 0x001003FC
		public void SetNumberValue(int iRow, int iCol, double dValue)
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
			this.SetNumberValue(iCol, iRow, dValue, this.ᜃ.DefaultXFIndex);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0010144C File Offset: 0x0010044C
		public void SetNumberValue(long iCellIndex, double dValue)
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
			int iCol = sprṔ.ᜁ(iCellIndex);
			int iRow = sprṔ.ᜀ(iCellIndex);
			this.SetNumberValue(iRow, iCol, dValue);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x001014A0 File Offset: 0x001004A0
		public void SetNumberValue(int iRow, int iCol, double dValue, int iXFIndex)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			spr\u19FF spr_u19FF = (spr\u19FF)this.ᜅ.ᜀ(515);
			spr_u19FF.ᜀ(dValue);
			spr_u19FF.ᜇ(iRow - 1);
			spr_u19FF.ᜆ(iCol - 1);
			spr_u19FF.ᜁ((ushort)iXFIndex);
			this[iRow, iCol] = spr_u19FF;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0010151C File Offset: 0x0010051C
		public void SetBooleanValue(int iRow, int iCol, bool bValue)
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
			this.SetBooleanValue(iCol, iRow, bValue, this.ᜃ.DefaultXFIndex);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0010156C File Offset: 0x0010056C
		public void SetBooleanValue(long iCellIndex, bool bValue)
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
			int iCol = sprṔ.ᜁ(iCellIndex);
			int iRow = sprṔ.ᜀ(iCellIndex);
			this.SetBooleanValue(iRow, iCol, bValue);
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x001015C0 File Offset: 0x001005C0
		public void SetBooleanValue(int iRow, int iCol, bool bValue, int iXFIndex)
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
			spr\u249B spr_u249B = (spr\u249B)this.ᜅ.ᜀ(517);
			spr_u249B.ᜀ(false);
			spr_u249B.ᜀ(bValue ? 1 : 0);
			spr_u249B.ᜇ(iRow - 1);
			spr_u249B.ᜆ(iCol - 1);
			spr_u249B.ᜁ((ushort)iXFIndex);
			this.ᜁ(sprṔ.ᜀ(iCol, iRow), spr_u249B);
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00101654 File Offset: 0x00100654
		public void SetErrorValue(int iRow, int iCol, string strValue)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.SetErrorValue(iCol, iRow, strValue, this.ᜃ.DefaultXFIndex);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x001016A4 File Offset: 0x001006A4
		public void SetErrorValue(long iCellIndex, string strValue)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			int iCol = sprṔ.ᜁ(iCellIndex);
			int iRow = sprṔ.ᜀ(iCellIndex);
			this.SetErrorValue(iRow, iCol, strValue);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x001016F8 File Offset: 0x001006F8
		public void SetErrorValue(int iRow, int iCol, string strValue, int iXFIndex)
		{
			int a_ = 14;
			if (true)
			{
			}
			int num;
			if (FormulaUtil.ErrorNameToCode.TryGetValue(strValue, out num))
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_38;
					}
				}
				IL_38:
				if (false)
				{
				}
				this.SetErrorValue(iRow, iCol, (byte)num, iXFIndex);
				return;
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝃㉅㩇᱉ⵋ≍╏㝑", a_));
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00101770 File Offset: 0x00100770
		public void SetErrorValue(int iRow, int iCol, byte errorCode, int iXFIndex)
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
			spr\u249B spr_u249B = (spr\u249B)this.ᜅ.ᜀ(517);
			spr_u249B.ᜀ(true);
			spr_u249B.ᜀ(errorCode);
			spr_u249B.ᜇ(iRow - 1);
			spr_u249B.ᜆ(iCol - 1);
			spr_u249B.ᜁ((ushort)iXFIndex);
			this.ᜁ(sprṔ.ᜀ(iCol, iRow), spr_u249B);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x001017F8 File Offset: 0x001007F8
		public void SetFormula(int iRow, int iCol, string strValue, int iXFIndex)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.SetFormula(iRow, iCol, strValue, iXFIndex, false);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0010183F File Offset: 0x0010083F
		public void SetFormula(int iRow, int iCol, string strValue, int iXFIndex, bool isR1C1, NumberFormatInfo formatInfo)
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
			this.SetFormula(iRow, iCol, strValue, iXFIndex, isR1C1, true, formatInfo);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0010187F File Offset: 0x0010087F
		public void SetFormula(int iRow, int iCol, string strValue, int iXFIndex, bool isR1C1)
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
			this.SetFormula(iRow, iCol, strValue, iXFIndex, isR1C1, true, null);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x001018C0 File Offset: 0x001008C0
		public void SetFormula(int iRow, int iCol, string strValue, int iXFIndex, bool isR1C1, bool bParse, NumberFormatInfo formatInfo)
		{
			spr᱒ spr᱒;
			for (;;)
			{
				if (true)
				{
				}
				spr᱒ = (spr᱒)this.ᜅ.ᜀ(6);
				strValue = strValue.Substring(1);
				FormulaUtil formulaUtil = this.ᜂ.ParentWorkbook.FormulaUtil;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6C:
					if (!bParse)
					{
						goto IL_B5;
					}
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6C;
					case 1:
						formulaUtil.NumberFormat = NumberFormatInfo.InvariantInfo;
						spr᱒.ᜁ(formulaUtil.ᜀ(strValue, this.sheet, null, iRow - 1, iCol - 1, isR1C1));
						formulaUtil.NumberFormat = null;
						num = 2;
						continue;
					case 2:
						goto IL_B3;
					}
					break;
				}
			}
			IL_B3:
			IL_B5:
			spr᱒.ᜇ(iRow - 1);
			spr᱒.ᜆ(iCol - 1);
			spr᱒.ᜁ((ushort)iXFIndex);
			this.ᜁ(sprṔ.ᜀ(iCol, iRow), spr᱒);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x001019B8 File Offset: 0x001009B8
		public void SetBlank(int iRow, int iCol, int iXFIndex)
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
			spr\u171D spr_u171D = (spr\u171D)this.ᜅ.ᜀ(513);
			spr_u171D.ᜇ(iRow - 1);
			spr_u171D.ᜆ(iCol - 1);
			spr_u171D.ᜁ((ushort)iXFIndex);
			this.ᜁ(iRow, iCol, spr_u171D);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00101A2C File Offset: 0x00100A2C
		internal void ᜀ(int A_0, int A_1, int A_2, spr\u223A A_3)
		{
			int a_ = 15;
			int num = 1;
			spr\u1C7C spr_u1C7C;
			for (;;)
			{
				SortedList<int, int> sortedList;
				switch (num)
				{
				case 0:
					goto IL_62;
				case 2:
					if (true)
					{
					}
					num = 6;
					continue;
				case 3:
					goto IL_41;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
						if (false)
						{
						}
						if (sortedList != null)
						{
							num = 2;
							continue;
						}
						goto IL_118;
					}
					break;
				case 5:
					goto IL_116;
				case 6:
					if (sortedList.Count <= 1)
					{
						num = 0;
						continue;
					}
					goto IL_118;
				}
				if (A_3 == null)
				{
					num = 3;
					continue;
				}
				spr_u1C7C = (spr\u1C7C)this.ᜅ.ᜀ(253);
				spr_u1C7C.ᜇ(A_0 - 1);
				spr_u1C7C.ᜆ(A_1 - 1);
				spr_u1C7C.ᜁ((ushort)A_2);
				sortedList = A_3.ᜊ();
				num = 4;
				continue;
				IL_62:
				A_3.ᜇ().Clear();
				num = 5;
			}
			IL_41:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄㍆⽈", a_));
			IL_116:
			IL_118:
			spr_u1C7C.ᜀ(this.ᜃ.InnerSST.AddIncrease(A_3));
			this.ᜁ(A_0, A_1, spr_u1C7C);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00101B74 File Offset: 0x00100B74
		public void SetSingleStringValue(int iRow, int iCol, int iXFIndex, int iSSTIndex)
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
			spr\u1C7C spr_u1C7C = (spr\u1C7C)this.ᜅ.ᜀ(253);
			spr_u1C7C.ᜇ(iRow - 1);
			spr_u1C7C.ᜆ(iCol - 1);
			spr_u1C7C.ᜁ((ushort)iXFIndex);
			spr_u1C7C.ᜀ(iSSTIndex);
			this.ᜃ.InnerSST.AddIncrease(iSSTIndex);
			this.ᜀ(spr_u1C7C);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00101C00 File Offset: 0x00100C00
		internal void ᜀ(int A_0, int A_1, int A_2, string A_3)
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
			spr\u2170 spr_u = (spr\u2170)this.ᜅ.ᜀ(516);
			spr_u.ᜇ(A_0 - 1);
			spr_u.ᜆ(A_1 - 1);
			spr_u.ᜁ((ushort)A_2);
			spr_u.ᜀ(A_3);
			this.ᜁ(A_0, A_1, spr_u);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x00101C7C File Offset: 0x00100C7C
		public void FreeRange(int iRow, int iColumn)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.SetRange(iRow, iColumn, null);
					spr\u23A5 spr_u23A = this.ᜄ(iRow, iColumn);
					int num = 14;
					for (;;)
					{
						spr\u216E spr_u216E;
						spr\u216E spr_u216E2;
						switch (num)
						{
						case 0:
						{
							int num2 = (int)spr_u216E.ᜌ();
							num = 6;
							continue;
						}
						case 1:
						{
							XlsWorksheet xlsWorksheet;
							if (xlsWorksheet == null)
							{
								num = 5;
								continue;
							}
							num = 15;
							continue;
						}
						case 2:
							goto IL_109;
						case 3:
							return;
						case 4:
						{
							int num3 = (int)spr_u23A.ᜆ();
							int num2 = this.ᜃ.DefaultXFIndex;
							spr\u2502 spr_u = sprᜑ.ᜂ(this.sheet, iRow);
							XlsWorksheet xlsWorksheet = this.sheet as XlsWorksheet;
							num = 1;
							continue;
						}
						case 5:
							num = 11;
							continue;
						case 6:
							goto IL_17D;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1EA;
							default:
								if (false)
								{
								}
								num = 16;
								continue;
							}
							break;
						case 8:
							this.Remove(iRow, iColumn);
							num = 3;
							continue;
						case 9:
						{
							spr\u2502 spr_u;
							if (spr_u != null)
							{
								num = 12;
								continue;
							}
							goto IL_109;
						}
						case 10:
							goto IL_1EA;
						case 11:
							spr_u216E2 = null;
							goto IL_1DC;
						case 12:
						{
							spr\u2502 spr_u;
							int num2 = (int)spr_u.ᜃ();
							num = 2;
							continue;
						}
						case 13:
						{
							int num2;
							int num3;
							if (num3 == num2)
							{
								num = 8;
								continue;
							}
							return;
						}
						case 14:
							if (spr_u23A != null)
							{
								num = 7;
								continue;
							}
							return;
						case 15:
						{
							XlsWorksheet xlsWorksheet;
							spr_u216E2 = xlsWorksheet.ColumnInformation[iColumn];
							goto IL_1DC;
						}
						case 16:
							if (spr_u23A.get_TypeCode() == TBIFFRecord.Blank)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
						IL_109:
						num = 13;
						continue;
						IL_17D:
						num = 9;
						continue;
						IL_1EA:
						if (spr_u216E != null)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_17D;
						IL_1DC:
						spr_u216E = spr_u216E2;
						num = 10;
					}
				}
				return;
			}
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00101E90 File Offset: 0x00100E90
		public void ClearData()
		{
			for (;;)
			{
				int num = 0;
				int num2 = this.ᜀ.ᜆ();
				int num3 = 6;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						sprᱧ sprᱧ;
						sprᱧ.\u1716();
						sprᱧ.ᜀ((ushort)this.ᜃ.DefaultXFIndex);
						num3 = 4;
						continue;
					}
					case 1:
					{
						if (num >= num2)
						{
							num3 = 3;
							continue;
						}
						sprᱧ sprᱧ = this.ᜀ.ᜄ().ᜁ(num);
						num3 = 5;
						continue;
					}
					case 2:
						goto IL_A1;
					case 3:
						return;
					case 4:
						IL_72:
						goto IL_44;
					case 5:
					{
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num3 = 0;
							continue;
						}
						goto IL_44;
					}
					case 6:
						if (true)
						{
						}
						goto IL_A1;
					}
					break;
					IL_44:
					num++;
					num3 = 2;
					continue;
					IL_A1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_72;
					default:
						if (false)
						{
						}
						num3 = 1;
						break;
					}
				}
			}
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00101F80 File Offset: 0x00100F80
		[CLSCompliant(false)]
		internal void ᜀ(spr\u225F A_0)
		{
			int a_ = 13;
			if (A_0 == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
				}
				IL_24:
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄⑆♈㥊⥌", a_));
			}
			sprᱧ sprᱧ = this.ᜀ.ᜄ().ᜁ(A_0.ᜉ());
			sprᱧ.ᜀ(A_0.ᜈ(), A_0, base.ReservedHandle.\u171D());
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0010200C File Offset: 0x0010100C
		[CLSCompliant(false)]
		internal spr\u225F ᜁ(int A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				sprᱧ sprᱧ;
				spr\u252B spr_u252B;
				for (;;)
				{
					A_0--;
					A_1--;
					sprᱧ = this.ᜀ.ᜄ().ᜁ(A_0);
					int num = 0;
					for (;;)
					{
						spr᱒ spr᱒;
						spr᱒ spr᱒2;
						switch (num)
						{
						case 0:
							if (sprᱧ == null)
							{
								num = 4;
								continue;
							}
							num = 12;
							continue;
						case 1:
							if (spr_u252B.ᜇ() == A_0)
							{
								goto IL_1E9;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1C2;
							default:
								if (false)
								{
								}
								num = 9;
								continue;
							}
							break;
						case 2:
							spr᱒ = null;
							goto IL_195;
						case 3:
							num = 2;
							continue;
						case 4:
							goto IL_87;
						case 5:
							goto IL_1BC;
						case 6:
						{
							Ptg ptg;
							if (ptg.TokenCode != FormulaToken.tExp)
							{
								num = 8;
								continue;
							}
							spr_u252B = (ptg as spr\u252B);
							num = 1;
							continue;
						}
						case 7:
						{
							Ptg[] array;
							if (array.Length != 1)
							{
								num = 10;
								continue;
							}
							Ptg ptg = array[0];
							num = 6;
							continue;
						}
						case 8:
							goto IL_B1;
						case 9:
							sprᱧ = this.ᜀ.ᜄ().ᜁ(spr_u252B.ᜇ());
							num = 11;
							continue;
						case 10:
							goto IL_DB;
						case 11:
							goto IL_193;
						case 12:
							if (!sprᱧ.\u1716(A_1))
							{
								num = 3;
								continue;
							}
							num = 14;
							continue;
						case 13:
						{
							if (spr᱒2 == null)
							{
								num = 5;
								continue;
							}
							Ptg[] array = spr᱒2.ᜑ();
							num = 7;
							continue;
						}
						case 14:
							spr᱒ = (sprᱧ.ᜆ(A_1, base.AppImplementation.ᜨ()) as spr᱒);
							goto IL_195;
						}
						break;
						IL_195:
						spr᱒2 = spr᱒;
						if (true)
						{
						}
						num = 13;
					}
				}
				IL_87:
				return null;
				IL_B1:
				return null;
				IL_DB:
				return null;
				IL_193:
				goto IL_1E9;
				IL_1BC:
				IL_1C2:
				return null;
				IL_1E9:
				return sprᱧ.\u1713(spr_u252B.ᜆ());
			}
			}
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x00102210 File Offset: 0x00101210
		public void UpdateFormula(int iCurIndex, int iSourceIndex, Rectangle sourceRect, int iDestIndex, Rectangle destRect)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ.ᜀ(iCurIndex, iSourceIndex, sourceRect, iDestIndex, destRect);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x00102260 File Offset: 0x00101260
		public void RemoveLastColumn(int iColumnIndex)
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
			this.ᜀ.ᜅ(iColumnIndex - 1);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x001022AC File Offset: 0x001012AC
		public void RemoveRow(int iRowIndex)
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
			this.ᜀ.ᜄ(iRowIndex - 1);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x001022F8 File Offset: 0x001012F8
		public void UpdateNameIndexes(XlsWorkbook book, int[] arrNewIndex)
		{
			int a_ = 7;
			if (arrNewIndex == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
				}
				IL_24:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("尼䴾㍀ൂ⁄うH╊⥌⩎⥐", a_));
			}
			if (true)
			{
			}
			this.ᜀ.ᜀ(book, arrNewIndex);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x00102364 File Offset: 0x00101364
		public void UpdateNameIndexes(XlsWorkbook book, IDictionary<int, int> dicNewIndex)
		{
			int a_ = 5;
			if (dicNewIndex == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
				}
				IL_24:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("强吼尾ཀ♂㉄ๆ❈⽊⡌㝎", a_));
			}
			this.ᜀ.ᜀ(book, dicNewIndex);
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x001023D0 File Offset: 0x001013D0
		[CLSCompliant(false)]
		public void ReplaceSharedFormula()
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
			this.ᜀ.ᜀ(this.ᜃ);
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0010241C File Offset: 0x0010141C
		public void UpdateStringIndexes(List<int> arrNewIndexes)
		{
			int a_ = 15;
			if (true)
			{
			}
			if (arrNewIndexes == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_2C;
					}
				}
				IL_2C:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⑄㕆㭈Պ⡌㡎ᡐ㵒ㅔ㉖⅘㹚⹜", a_));
			}
			this.ᜀ.ᜀ(arrNewIndexes);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00102488 File Offset: 0x00101488
		public List<long> Find(IXLSRange range, string findValue, FindType flags, bool bIsFindFirst)
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
			return this.ᜀ.ᜀ(range, findValue, flags, bIsFindFirst, this.ᜃ);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x001024DC File Offset: 0x001014DC
		public List<long> Find(IXLSRange range, double findValue, FindType flags, bool bIsFindFirst)
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
			return this.ᜀ.ᜀ(range, findValue, flags, bIsFindFirst, this.ᜃ);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x00102530 File Offset: 0x00101530
		internal List<long> ᜀ(IXLSRange A_0, string A_1, FindType A_2, ExcelFindOptions A_3, bool A_4)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return this.ᜀ.ᜀ(A_0, A_1, A_2, A_3, A_4, this.ᜃ);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00102584 File Offset: 0x00101584
		public List<long> Find(IXLSRange range, byte findValue, bool bIsError, bool bIsFindFirst)
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
			return this.ᜀ.ᜀ(range, findValue, bIsError, bIsFindFirst, this.ᜃ);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x001025D8 File Offset: 0x001015D8
		public List<long> Find(Dictionary<int, object> dictIndexes)
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
			return this.ᜀ.ᜀ(dictIndexes);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00102620 File Offset: 0x00101620
		internal sprủ ᜀ(XlsRange A_0, int A_1, int A_2, ref int A_3, ref int A_4)
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
			Rectangle a_ = A_0.GetRectangles()[0];
			sprủ result = this.ᜀ.ᜀ(a_, A_1, A_2, ref A_3, ref A_4);
			A_3++;
			A_4++;
			return result;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00102690 File Offset: 0x00101690
		public void UpdateExtendedFormatIndex(Dictionary<int, int> dictFormats)
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
			this.ᜀ.ᜀ(dictFormats);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x001026D8 File Offset: 0x001016D8
		public void UpdateExtendedFormatIndex(int[] arrFormats)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ.ᜀ(arrFormats);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00102720 File Offset: 0x00101720
		public void UpdateExtendedFormatIndex(int maxCount)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ.ᜀ(maxCount);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00102768 File Offset: 0x00101768
		public void SetCellStyle(int iRow, int iColumn, int iXFIndex)
		{
			for (;;)
			{
				IL_14:
				sprᱧ sprᱧ = this.Table.ᜀ(iRow - 1, ((spr\u17FF)base.ReservedHandle).ᜅ(), true, this.ᜂ.Version);
				sprᱧ.ᜁ(iRow - 1, iColumn - 1, iXFIndex, base.ReservedHandle.\u171D());
				for (;;)
				{
					IL_57:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (iXFIndex != this.ᜃ.DefaultXFIndex)
							{
								num = 2;
								continue;
							}
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_57;
							default:
								if (false)
								{
								}
								sprᜑ.ᜁ(this.ᜂ, iColumn);
								sprᜑ.ᜀ(this.ᜂ, iRow);
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						goto IL_14;
					}
				}
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00102840 File Offset: 0x00101840
		public void ReAddAllStrings()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = this.FirstRow;
					int lastRow = this.LastRow;
					int num2 = 0;
					for (;;)
					{
						sprᱧ sprᱧ;
						switch (num2)
						{
						case 0:
							goto IL_F1;
						case 1:
						{
							if (num > lastRow)
							{
								num2 = 3;
								continue;
							}
							spr\u17FF spr_u17FF = this.ᜃ.AppImplementation;
							sprᱧ = this.ᜀ.ᜀ(num - 1, spr_u17FF.ᜅ(), false, ExcelVersion.Version97to2003);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_DA;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						}
						case 2:
							goto IL_DA;
						case 3:
							return;
						case 4:
							goto IL_4F;
						case 5:
							sprᱧ.ᜀ(this.ᜃ.InnerSST);
							num2 = 4;
							continue;
						case 6:
							goto IL_F1;
						}
						break;
						IL_4F:
						num++;
						num2 = 6;
						continue;
						IL_F1:
						num2 = 1;
						continue;
						IL_DA:
						if (sprᱧ == null)
						{
							goto IL_4F;
						}
						num2 = 5;
					}
				}
				return;
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0010295C File Offset: 0x0010195C
		internal void ᜀ(bool[] A_0)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ.ᜀ(A_0);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x001029A4 File Offset: 0x001019A4
		internal void ᜀ(int[] A_0)
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
			this.ᜀ.ᜁ(A_0);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x001029EC File Offset: 0x001019EC
		private void ᜀ(int A_0, int A_1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.Table.ᜌ(A_0, A_1);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00102A34 File Offset: 0x00101A34
		private string ᜀ(spr᱒ A_0)
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int key;
					string result;
					if (!FormulaUtil.ErrorCodeToName.TryGetValue(key, out result))
					{
						num = 3;
						continue;
					}
					return result;
				}
				case 1:
					goto IL_99;
				case 2:
					if (true)
					{
					}
					break;
				case 3:
					goto IL_110;
				case 4:
					goto IL_4C;
				case 5:
				{
					if (A_0.ᜋ())
					{
						num = 6;
						continue;
					}
					int key = (int)A_0.ᜏ();
					num = 0;
					continue;
				}
				case 6:
					goto IL_7A;
				case 7:
					if (!double.IsNaN(A_0.ᜌ()))
					{
						num = 1;
						continue;
					}
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 7;
				}
			}
			for (;;)
			{
				IL_4C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_CA;
				}
			}
			IL_CA:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ㙃⭅㵇♉ⵋ", a_));
			IL_7A:
			return A_0.ᜁ().ToString().ToUpper();
			IL_99:
			throw new ArgumentException(RecordTableEnumerator.b("ؿⵁ㙃⭅㵇♉ⵋ湍≏㝑㝓㥕⩗㹙籛㩝ཟݡᝣࡥݧṩ䱫ᵭկɱѳ᥵੷๹屻᭽ꢇﺋ꺍﶑ﮓ歹뚗", a_));
			IL_110:
			return RecordTableEnumerator.b("挿ు歃݅", a_);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00102B68 File Offset: 0x00101B68
		private void ᜀ()
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
			this.ᜁ = new SFTable(this.ᜃ.MaxRowCount, this.ᜃ.MaxColumnCount);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00102BC4 File Offset: 0x00101BC4
		private void ᜀ(spr᱒ A_0, IDictionary A_1, XlsWorkbook A_2)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					string text;
					int num2;
					Ptg ptg;
					spr\u2086 spr_u;
					switch (num)
					{
					case 0:
						goto IL_102;
					case 1:
						goto IL_12D;
					case 2:
						goto IL_107;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_185;
						default:
							if (false)
							{
							}
							if (A_1.Contains(text))
							{
								num = 11;
								continue;
							}
							goto IL_12D;
						}
						break;
					case 4:
						return;
					case 5:
					{
						if (A_2 == null)
						{
							num = 0;
							continue;
						}
						Ptg[] array = A_0.ᜑ();
						num2 = 0;
						int num3 = array.Length;
						num = 13;
						continue;
					}
					case 6:
						if (A_1 != null)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						goto IL_12D;
					case 8:
						goto IL_154;
					case 9:
						num = 3;
						continue;
					case 10:
						goto IL_75;
					case 11:
						text = (string)A_1[text];
						num = 1;
						continue;
					case 12:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 4;
							continue;
						}
						Ptg[] array;
						ptg = array[num2];
						num = 14;
						continue;
					}
					case 13:
						goto IL_154;
					case 14:
						goto IL_185;
					case 15:
					{
						spr_u = (spr\u2086)ptg;
						ushort reference = spr_u.ᜁ();
						text = A_2.GetSheetNameByReference((int)reference);
						num = 6;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 5;
					continue;
					IL_107:
					num2++;
					num = 8;
					continue;
					IL_185:
					if (ptg is spr\u2086)
					{
						num = 15;
						continue;
					}
					goto IL_107;
					IL_12D:
					int num4 = this.ᜃ.AddSheetReference(text);
					spr_u.ᜂ((ushort)num4);
					num = 2;
					continue;
					IL_154:
					num = 12;
				}
				IL_75:
				throw new ArgumentNullException(RecordTableEnumerator.b("倵圷䠹儻䬽ⰿ⍁", a_));
				IL_102:
				throw new ArgumentNullException(RecordTableEnumerator.b("吵圷唹圻", a_));
			}
			}
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00102DEC File Offset: 0x00101DEC
		private void ᜀ(IDictionary A_0)
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
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00102E28 File Offset: 0x00101E28
		private sprᤅ ᜁ(long A_0)
		{
			spr\u23A5 spr_u23A = this.ᜄ(A_0);
			if (spr_u23A == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_0B;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return null;
			}
			IL_0B:
			int a_ = (int)spr_u23A.ᜆ();
			spr\u192F spr_u192F = this.ᜃ.InnerExtFormats.ᜁ(a_);
			int a_2 = spr_u192F.ᝊ();
			return this.ᜃ.InnerFormats.ᜁ(a_2);
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x00102EA4 File Offset: 0x00101EA4
		private IFont ᜀ(long A_0)
		{
			spr\u23A5 spr_u23A = this.ᜄ(A_0);
			if (spr_u23A == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return null;
				}
			}
			if (true)
			{
			}
			int a_ = (int)spr_u23A.ᜆ();
			spr\u192F spr_u192F = this.ᜃ.InnerExtFormats.ᜁ(a_);
			int index = spr_u192F.\u173B();
			return this.ᜃ.InnerFonts[index];
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00102F20 File Offset: 0x00101F20
		private int ᜀ(int A_0, IDictionary A_1, CopyRangeOptions A_2)
		{
			int num = 0;
			for (;;)
			{
				IL_0A:
				switch (num)
				{
				case 1:
					if (A_1.Contains(A_0))
					{
						num = 3;
						continue;
					}
					return A_0;
				case 2:
					A_0 = this.ᜃ.DefaultXFIndex;
					num = 5;
					continue;
				case 3:
					A_0 = (int)A_1[A_0];
					num = 7;
					continue;
				case 4:
					if (A_1 != null)
					{
						num = 6;
						continue;
					}
					return A_0;
				case 5:
					return A_0;
				case 6:
					if (true)
					{
					}
					num = 1;
					continue;
				case 7:
					return A_0;
				}
				while ((A_2 & CopyRangeOptions.CopyStyles) == CopyRangeOptions.None)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 2;
						goto IL_0A;
					}
				}
				num = 4;
			}
			return A_0;
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00103010 File Offset: 0x00102010
		internal void ᜀ(Dictionary<int, int> A_0, spr\u202C A_1)
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
			this.ᜀ.ᜀ(A_0, A_1);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00103058 File Offset: 0x00102058
		private void ᜀ(RichTextString A_0, int A_1)
		{
			object obj;
			spr\u223A spr_u223A;
			spr\u223A spr_u223A2;
			for (;;)
			{
				if (true)
				{
				}
				obj = this.ᜃ.InnerSST[A_1];
				spr_u223A = (obj as spr\u223A);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4C;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						}
						if (false)
						{
						}
						if (spr_u223A2 != null)
						{
							num = 3;
							continue;
						}
						goto IL_B3;
					case 2:
						if (spr_u223A != null)
						{
							num = 0;
							continue;
						}
						goto IL_61;
					case 3:
						goto IL_A4;
					}
					break;
					IL_61:
					spr_u223A2 = A_0.TextObject;
					num = 1;
				}
			}
			IL_4C:
			A_0.ᜀ(spr_u223A.\u170D());
			return;
			IL_A4:
			spr_u223A2.ᜉ();
			spr_u223A2.ᜁ(obj as string);
			return;
			IL_B3:
			A_0.ᜀ((spr\u223A)obj);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x00103124 File Offset: 0x00102124
		internal XlsWorksheet.TRangeValueType ᜃ(int A_0, int A_1)
		{
			sprᱧ sprᱧ = this.ᜀ.ᜄ().ᜁ(A_0 - 1);
			if (sprᱧ != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return XlsWorksheet.TRangeValueType.Blank;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return sprᱧ.ᜁ(A_1 - 1, false);
			}
			return XlsWorksheet.TRangeValueType.Blank;
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00103184 File Offset: 0x00102184
		protected override void OnDispose()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_47;
				case 2:
					if (this.ᜁ != null)
					{
						num = 4;
						continue;
					}
					goto IL_CD;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						if (false)
						{
						}
						this.ᜀ.ᜉ();
						this.ᜀ = null;
						num = 7;
						continue;
					}
					break;
				case 4:
					this.ᜁ.Clear();
					this.ᜁ = null;
					num = 8;
					continue;
				case 5:
					if (this.ᜀ != null)
					{
						num = 3;
						continue;
					}
					goto IL_65;
				case 6:
					goto IL_E6;
				case 7:
					goto IL_65;
				case 8:
					goto IL_CD;
				}
				if (!this.m_bIsDisposed)
				{
					num = 1;
					continue;
				}
				break;
				IL_47:
				num = 5;
				continue;
				IL_65:
				if (true)
				{
				}
				num = 2;
				continue;
				IL_CD:
				this.ᜃ = null;
				this.ᜂ = null;
				num = 6;
			}
			IL_E6:
			base.OnDispose();
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x001032A4 File Offset: 0x001022A4
		internal int ᜀ(TBIFFRecord A_0, int A_1, int A_2, int A_3)
		{
			if (true)
			{
			}
			sprᱧ sprᱧ = this.ᜀ.ᜄ().ᜁ(A_1 - 1);
			if (sprᱧ != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					return sprᱧ.ᜀ(A_0, A_2 - 1, A_3 - 1) + 1;
				}
			}
			return A_3 + 1;
		}

		// Token: 0x040010B9 RID: 4281
		private sprủ ᜀ;

		// Token: 0x040010BA RID: 4282
		private SFTable ᜁ;

		// Token: 0x040010BB RID: 4283
		private long \u2609\u0088\u0084\u00A0;

		// Token: 0x040010BC RID: 4284
		private bool \u2593\u0099\u00A8\u00AE;

		// Token: 0x040010BD RID: 4285
		private IInternalWorksheet ᜂ;

		// Token: 0x040010BE RID: 4286
		private XlsWorkbook ᜃ;

		// Token: 0x040010BF RID: 4287
		private int[] \u25D8\u0083\u00A1\u009A;

		// Token: 0x040010C0 RID: 4288
		private bool ᜄ;

		// Token: 0x040010C1 RID: 4289
		private RecordExtractor ᜅ;
	}
}
