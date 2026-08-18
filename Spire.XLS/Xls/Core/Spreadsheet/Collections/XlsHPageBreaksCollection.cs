using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200003F RID: 63
	public class XlsHPageBreaksCollection : CollectionExtended<IHPageBreak>, IHPageBreaks, IRecordStorage
	{
		// Token: 0x1700015E RID: 350
		protected internal IHPageBreak this[IXLSRange location]
		{
			get
			{
				int num = this.ᜀ(location);
				if (num < 0)
				{
					for (;;)
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
							goto IL_2C;
						}
					}
					IL_2C:
					if (false)
					{
					}
					return null;
				}
				return base.List[num];
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00026148 File Offset: 0x00025148
		protected internal IHPageBreak Add(IXLSRange location)
		{
			int a_ = 4;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					goto IL_3C;
				case 3:
					if (((XlsRange)location).IsSingleCell)
					{
						goto IL_A1;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A1;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (location == null)
				{
					if (true)
					{
					}
					num = 1;
				}
				else
				{
					num = 3;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("嘹医崽ℿ㙁ⵃ⥅♇", a_));
			IL_8B:
			throw new ArgumentException(RecordTableEnumerator.b("根崻倽✿❁摃⭅㵇㥉㡋湍㉏㝑瑓╕ㅗ㑙㭛㉝՟䉡ݣͥѧ٩䉫", a_));
			IL_A1:
			HPageBreak hpageBreak = new HPageBreak((spr\u2158)base.ReservedHandle, this, location);
			this.ᜀ(hpageBreak);
			return hpageBreak;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0002621C File Offset: 0x0002521C
		protected internal IHPageBreak Remove(IXLSRange location)
		{
			IHPageBreak result;
			for (;;)
			{
				result = null;
				int num = this.ᜀ(location);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_3C;
					case 1:
						if (num >= 0)
						{
							num2 = 0;
							continue;
						}
						return result;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C;
						default:
							goto IL_70;
						}
						break;
					}
					break;
					IL_3C:
					result = base.List[num];
					base.RemoveAt(num);
					num2 = 2;
				}
			}
			IL_70:
			if (true)
			{
			}
			if (false)
			{
			}
			return result;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000262AC File Offset: 0x000252AC
		protected internal IHPageBreak GetPageBreak(int iRow)
		{
			switch (0)
			{
			default:
			{
				IHPageBreak result;
				for (;;)
				{
					result = null;
					int num = 0;
					int count = base.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_BD;
						case 1:
							goto IL_BD;
						case 2:
						{
							XlsHPageBreak xlsHPageBreak;
							if (xlsHPageBreak.Location.Row == iRow)
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
									num2 = 6;
									continue;
								}
							}
							else
							{
								num++;
							}
							num2 = 0;
							continue;
						}
						case 3:
							return result;
						case 4:
						{
							if (num >= count)
							{
								num2 = 5;
								continue;
							}
							XlsHPageBreak xlsHPageBreak = base[num] as XlsHPageBreak;
							num2 = 2;
							continue;
						}
						case 5:
							return result;
						case 6:
						{
							XlsHPageBreak xlsHPageBreak;
							result = xlsHPageBreak;
							num2 = 3;
							continue;
						}
						}
						break;
						IL_BD:
						if (true)
						{
						}
						num2 = 4;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000263A0 File Offset: 0x000253A0
		private new int ᜀ(IXLSRange A_0)
		{
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					result = -1;
					int row = A_0.Row;
					int column = A_0.Column;
					int num = 0;
					int count = base.Count;
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return result;
						case 1:
						{
							if (num >= count)
							{
								num2 = 8;
								continue;
							}
							XlsHPageBreak xlsHPageBreak = base[num] as XlsHPageBreak;
							IXLSRange ixlsrange = xlsHPageBreak.Location;
							num2 = 4;
							continue;
						}
						case 2:
							goto IL_EE;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return result;
							default:
								if (false)
								{
								}
								result = num;
								num2 = 0;
								continue;
							}
							break;
						case 4:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.Row == row)
							{
								num2 = 7;
								continue;
							}
							goto IL_63;
						}
						case 5:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.Column == column)
							{
								num2 = 3;
								continue;
							}
							goto IL_63;
						}
						case 6:
							goto IL_EE;
						case 7:
							if (true)
							{
							}
							num2 = 5;
							continue;
						case 8:
							return result;
						}
						break;
						IL_63:
						num++;
						num2 = 2;
						continue;
						IL_EE:
						num2 = 1;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000264E8 File Offset: 0x000254E8
		public int ManualBreakCount
		{
			get
			{
				int num = 0;
				IEnumerator<IHPageBreak> enumerator = base.List.GetEnumerator();
				try
				{
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_78:
						num2 = 5;
						break;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
					XlsHPageBreak xlsHPageBreak;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_93;
						case 2:
							if (!enumerator.MoveNext())
							{
								num2 = 3;
								continue;
							}
							goto IL_6C;
						case 3:
							num2 = 4;
							continue;
						case 4:
							goto IL_C6;
						case 5:
							if (xlsHPageBreak.Type == PageBreakType.Manual)
							{
								num2 = 6;
								continue;
							}
							goto IL_93;
						case 6:
							num++;
							num2 = 0;
							continue;
						}
						goto IL_62;
						IL_93:
						num2 = 2;
						continue;
						IL_62:
						if (true)
						{
						}
						goto IL_93;
					}
					IL_6C:
					xlsHPageBreak = (XlsHPageBreak)enumerator.Current;
					goto IL_78;
					IL_C6:;
				}
				finally
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							enumerator.Dispose();
							num2 = 1;
							continue;
						case 1:
							goto IL_FF;
						}
						if (enumerator == null)
						{
							break;
						}
						num2 = 0;
					}
					IL_FF:;
				}
				return num;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00026608 File Offset: 0x00025608
		internal XlsHPageBreaksCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜁ();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00026624 File Offset: 0x00025624
		private new void ᜁ()
		{
			int a_ = 14;
			this.ᜀ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜀ == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_44;
					}
				}
				IL_44:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑃❅㩇⽉≋㩍", a_));
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000266A4 File Offset: 0x000256A4
		internal new void ᜀ(spr\u2539 A_0)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					int num2;
					int num3;
					spr\u2539.ᜀ[] array;
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_D3;
					case 2:
					{
						spr\u2539.ᜀ ᜀ;
						HPageBreak item = new HPageBreak((spr\u2158)base.ReservedHandle, this, ᜀ);
						base.Add(item);
						num = 5;
						continue;
					}
					case 3:
					{
						spr\u2539.ᜀ ᜀ;
						if ((int)ᜀ.ᜀ() < this.ᜀ.MaxColumnCount)
						{
							num = 2;
							continue;
						}
						goto IL_5E;
					}
					case 4:
						goto IL_D3;
					case 5:
						goto IL_5E;
					case 6:
						if (num2 >= num3)
						{
							num = 0;
							continue;
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
							spr\u2539.ᜀ ᜀ = array[num2];
							num = 3;
							continue;
						}
						}
						break;
					case 7:
						goto IL_5C;
					}
					IL_4D:
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					array = A_0.ᜀ();
					num2 = 0;
					num3 = array.Length;
					num = 4;
					continue;
					goto IL_4D;
					IL_5E:
					if (true)
					{
					}
					num2++;
					num = 1;
					continue;
					IL_D3:
					num = 6;
				}
				IL_5C:
				throw new ArgumentNullException(RecordTableEnumerator.b("似娾≀ⱂ㝄⍆", a_));
			}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000267F4 File Offset: 0x000257F4
		internal new void ᜀ(RecordArrayList A_0)
		{
			int a_ = 12;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_38;
				case 1:
				{
					spr\u2539 spr_u;
					A_0.ᜀ(spr_u);
					num = 3;
					continue;
				}
				case 2:
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
						spr\u2539 spr_u;
						if (spr_u == null)
						{
							return;
						}
						break;
					}
					}
					if (true)
					{
					}
					num = 1;
					continue;
				case 3:
					return;
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					spr\u2539 spr_u = this.ᜀ();
					num = 2;
				}
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃╅❇㡉⡋㵍", a_));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000268B0 File Offset: 0x000258B0
		private new spr\u2539 ᜀ()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				spr\u2539.ᜀ[] array;
				for (;;)
				{
					int count = base.Count;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_C5;
						case 1:
							goto IL_C5;
						case 2:
						{
							if (count == 0)
							{
								num = 5;
								continue;
							}
							array = new spr\u2539.ᜀ[count];
							List<IHPageBreak> innerList = base.InnerList;
							int num2 = 0;
							num = 0;
							continue;
						}
						case 3:
						{
							int num2;
							if (num2 >= count)
							{
								num = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C5;
							}
							if (false)
							{
							}
							List<IHPageBreak> innerList;
							XlsHPageBreak xlsHPageBreak = innerList[num2] as XlsHPageBreak;
							array[num2] = xlsHPageBreak.HPageBreak;
							num2++;
							num = 1;
							continue;
						}
						case 4:
							goto IL_E1;
						case 5:
							goto IL_53;
						}
						break;
						IL_C5:
						num = 3;
					}
				}
				IL_53:
				return null;
				IL_E1:
				spr\u2539 spr_u = (spr\u2539)spr\u175E.ᜀ(TBIFFRecord.HorizontalPageBreaks);
				spr_u.ᜀ(array);
				return spr_u;
			}
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000269B8 File Offset: 0x000259B8
		public override object Clone(object parent)
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
			XlsHPageBreaksCollection xlsHPageBreaksCollection = (XlsHPageBreaksCollection)base.Clone(parent);
			xlsHPageBreaksCollection.ᜁ();
			return xlsHPageBreaksCollection;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00026A08 File Offset: 0x00025A08
		internal new void ᜀ(XlsHPageBreak A_0)
		{
			int a_ = 16;
			int num = 2;
			for (;;)
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (this.ᜀ(A_0.Location) < 0)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						goto IL_68;
					case 3:
						return;
					case 4:
						goto IL_66;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num = 0;
					continue;
				}
				IL_68:
				base.Add(A_0);
				num = 3;
			}
			IL_66:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙅⥇ⵉ⥋్≏㝑㕓㵕", a_));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00026AC8 File Offset: 0x00025AC8
		internal new void ᜂ()
		{
			List<XlsHPageBreak> list;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				switch (0)
				{
				default:
				{
					list = new List<XlsHPageBreak>();
					IEnumerator<IHPageBreak> enumerator = base.List.GetEnumerator();
					try
					{
						int num = 11;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								spr\u2539.ᜀ ᜀ;
								XlsWorkbook xlsWorkbook;
								ᜀ.ᜂ((ushort)(xlsWorkbook.MaxColumnCount - 1));
								num = 4;
								continue;
							}
							case 1:
							{
								spr\u2539.ᜀ ᜀ;
								XlsWorkbook xlsWorkbook;
								if ((int)ᜀ.ᜁ() > xlsWorkbook.MaxColumnCount - 1)
								{
									num = 6;
									continue;
								}
								break;
							}
							case 3:
							{
								spr\u2539.ᜀ ᜀ;
								XlsWorkbook xlsWorkbook;
								if ((int)ᜀ.ᜃ() > xlsWorkbook.MaxRowCount)
								{
									num = 8;
									continue;
								}
								num = 12;
								continue;
							}
							case 4:
								goto IL_12A;
							case 5:
							{
								if (!enumerator.MoveNext())
								{
									num = 7;
									continue;
								}
								XlsHPageBreak xlsHPageBreak = (XlsHPageBreak)enumerator.Current;
								XlsWorksheet xlsWorksheet = ((XlsPageSetup)base.Parent).Worksheet;
								XlsWorkbook xlsWorkbook = (XlsWorkbook)xlsWorksheet.Workbook;
								spr\u2539.ᜀ ᜀ = xlsHPageBreak.HPageBreak;
								num = 3;
								continue;
							}
							case 6:
							{
								spr\u2539.ᜀ ᜀ;
								XlsWorkbook xlsWorkbook;
								ᜀ.ᜁ((ushort)(xlsWorkbook.MaxColumnCount - 1));
								num = 2;
								continue;
							}
							case 7:
								num = 9;
								continue;
							case 8:
							{
								XlsHPageBreak xlsHPageBreak;
								list.Add(xlsHPageBreak);
								num = 10;
								continue;
							}
							case 9:
								goto IL_25E;
							case 12:
							{
								spr\u2539.ᜀ ᜀ;
								XlsWorkbook xlsWorkbook;
								if ((int)ᜀ.ᜀ() > xlsWorkbook.MaxColumnCount - 1)
								{
									num = 0;
									continue;
								}
								goto IL_12A;
							}
							}
							goto IL_125;
							IL_12A:
							num = 1;
							continue;
							IL_161:
							num = 5;
							continue;
							IL_125:
							goto IL_161;
						}
						IL_25E:
						break;
					}
					finally
					{
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_2A0;
							case 1:
								enumerator.Dispose();
								num = 0;
								continue;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 1;
						}
						IL_2A0:;
					}
					return;
				}
				}
				break;
			}
			for (;;)
			{
				using (List<XlsHPageBreak>.Enumerator enumerator2 = list.GetEnumerator())
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							num = 3;
							continue;
						case 2:
						{
							if (!enumerator2.MoveNext())
							{
								num = 1;
								continue;
							}
							XlsHPageBreak item = enumerator2.Current;
							base.Remove(item);
							num = 4;
							continue;
						}
						case 3:
							goto IL_BF;
						}
						IL_7C:
						num = 2;
						continue;
						goto IL_7C;
					}
					IL_BF:
					break;
				}
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00026DAC File Offset: 0x00025DAC
		public TBIFFRecord TypeCode
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
				return TBIFFRecord.Unknown;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00026DE8 File Offset: 0x00025DE8
		public int RecordCode
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
				return 0;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00026E24 File Offset: 0x00025E24
		public bool NeedDataArray
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
				return false;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00026E60 File Offset: 0x00025E60
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x00026EA0 File Offset: 0x00025EA0
		public long StreamPos
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
				return -1L;
			}
			set
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
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00026EDC File Offset: 0x00025EDC
		public int GetStoreSize(ExcelVersion version)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int count = base.Count;
				if (count > 0)
				{
					return 6 * count + 2;
				}
				break;
			}
			}
			return -4;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00026F2C File Offset: 0x00025F2C
		public int FillStream(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
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
				spr\u2539 spr_u = this.ᜀ();
				if (spr_u != null)
				{
					return spr_u.FillStream(writer, provider, encryptor, streamPosition);
				}
				if (true)
				{
				}
				break;
			}
			}
			return 0;
		}

		// Token: 0x040000B7 RID: 183
		private float \u2593\u0080\u00A0\u00AC;

		// Token: 0x040000B8 RID: 184
		private bool[] \u2609\u0083\u00A8\u0090;

		// Token: 0x040000B9 RID: 185
		private float[] \u2593\u0090\u009F\u0093;

		// Token: 0x040000BA RID: 186
		private bool[] \u25D9\u009Bª\u008E;

		// Token: 0x040000BB RID: 187
		private new XlsWorkbook ᜀ;
	}
}
