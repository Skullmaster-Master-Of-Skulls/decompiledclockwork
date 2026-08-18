using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001F0 RID: 496
	public class XlsBordersCollection : CollectionExtended<IBorder>, IBorders
	{
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x000F5B64 File Offset: 0x000F4B64
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x000F5C38 File Offset: 0x000F4C38
		public ExcelColors KnownColor
		{
			get
			{
				for (;;)
				{
					ExcelColors knownColor = base.InnerList[0].KnownColor;
					int num = 1;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_A5;
						case 1:
							goto IL_A5;
						case 2:
							return knownColor;
						case 3:
							if (num >= base.Count)
							{
								num2 = 2;
								continue;
							}
							num2 = 5;
							continue;
						case 4:
							goto IL_87;
						case 5:
							if (knownColor != base.InnerList[num].KnownColor)
							{
								num2 = 4;
								continue;
							}
							if (true)
							{
							}
							num++;
							num2 = 0;
							continue;
						}
						break;
						IL_A5:
						num2 = 3;
					}
				}
				IL_87:
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
				return ExcelColors.Black;
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num = 0;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_48:
						num2 = 0;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 2;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= base.Count)
							{
								num2 = 1;
								continue;
							}
							((XlsBorder)base.InnerList[num]).KnownColor = value;
							num++;
							num2 = 3;
							continue;
						case 1:
							return;
						case 2:
							goto IL_46;
						case 3:
							goto IL_93;
						}
						goto IL_18;
					}
					IL_46:
					IL_93:
					goto IL_48;
				}
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x000F5CDC File Offset: 0x000F4CDC
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x000F5DEC File Offset: 0x000F4DEC
		public Color Color
		{
			get
			{
				switch (0)
				{
				default:
				{
					Color result;
					for (;;)
					{
						result = base.InnerList[0].Color;
						int num = result.ToArgb();
						int num2 = 1;
						int num3 = 3;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								if (num != base.InnerList[num2].Color.ToArgb())
								{
									num3 = 4;
									continue;
								}
								num2++;
								num3 = 2;
								continue;
							case 1:
								return result;
							case 2:
								goto IL_BA;
							case 3:
								goto IL_BA;
							case 4:
								result = spr\u1D39.ᜂ;
								num3 = 6;
								continue;
							case 5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									if (num2 >= base.Count)
									{
										num3 = 1;
										continue;
									}
									num3 = 0;
									continue;
								}
								break;
							case 6:
								return result;
							}
							break;
							IL_BA:
							num3 = 5;
						}
					}
					return result;
				}
				}
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num = 0;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_40:
						num2 = 2;
						break;
					default:
						if (false)
						{
						}
						num2 = 3;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_93;
						case 2:
							if (num >= base.Count)
							{
								num2 = 0;
								continue;
							}
							((XlsBorder)base.InnerList[num]).Color = value;
							num++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						case 3:
							goto IL_3E;
						}
						goto IL_18;
					}
					IL_3E:
					IL_93:
					goto IL_40;
				}
			}
		}

		// Token: 0x17000A88 RID: 2696
		public IBorder this[BordersLineType index]
		{
			get
			{
				for (;;)
				{
					for (;;)
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_AB;
							case 1:
								switch (index)
								{
								case BordersLineType.DiagonalDown:
									goto IL_7E;
								case BordersLineType.DiagonalUp:
									goto IL_C4;
								case BordersLineType.EdgeLeft:
									goto IL_64;
								case BordersLineType.EdgeTop:
									goto IL_93;
								case BordersLineType.EdgeBottom:
									goto IL_71;
								case BordersLineType.EdgeRight:
									goto IL_B7;
								default:
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
										continue;
									}
									break;
								}
								break;
							case 2:
								num = 0;
								continue;
							}
							break;
						}
					}
				}
				IL_64:
				return base.InnerList[3];
				IL_71:
				return base.InnerList[2];
				IL_7E:
				if (true)
				{
				}
				return base.InnerList[0];
				IL_93:
				return base.InnerList[5];
				IL_AB:
				return null;
				IL_B7:
				return base.InnerList[4];
				IL_C4:
				return base.InnerList[1];
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x000F5F70 File Offset: 0x000F4F70
		// (set) Token: 0x06001C61 RID: 7265 RVA: 0x000F6044 File Offset: 0x000F5044
		public LineStyleType LineStyle
		{
			get
			{
				for (;;)
				{
					LineStyleType lineStyle = base.InnerList[0].LineStyle;
					int num = 1;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= base.Count)
							{
								num2 = 2;
								continue;
							}
							num2 = 4;
							continue;
						case 1:
							goto IL_87;
						case 2:
							return lineStyle;
						case 3:
							goto IL_A5;
						case 4:
							if (lineStyle != base.InnerList[num].LineStyle)
							{
								num2 = 1;
								continue;
							}
							num++;
							if (true)
							{
							}
							num2 = 5;
							continue;
						case 5:
							goto IL_A5;
						}
						break;
						IL_A5:
						num2 = 0;
					}
				}
				IL_87:
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
				return LineStyleType.None;
			}
			set
			{
				for (;;)
				{
					IL_18:
					int num = 0;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_48:
						num2 = 1;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 2;
						break;
					}
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_8E;
						case 1:
							if (num >= base.Count)
							{
								num2 = 3;
								continue;
							}
							base.InnerList[num].LineStyle = value;
							num++;
							num2 = 0;
							continue;
						case 2:
							goto IL_46;
						case 3:
							return;
						}
						goto IL_18;
					}
					IL_46:
					IL_8E:
					goto IL_48;
				}
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x000F60E4 File Offset: 0x000F50E4
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x000F6128 File Offset: 0x000F5128
		public LineStyleType Value
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
				return this.LineStyle;
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
				this.LineStyle = value;
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000F616C File Offset: 0x000F516C
		internal XlsBordersCollection(spr\u1DF5 A_0, object A_1, bool A_2) : base(A_0, A_1)
		{
			this.ᜀ();
			if (A_2)
			{
				base.InnerList.AddRange(new XlsBorder[6]);
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000F61A0 File Offset: 0x000F51A0
		internal XlsBordersCollection(spr\u1DF5 A_0, object A_1, IInternalAddtionalFormat A_2) : this(A_0, A_1, false)
		{
			base.InnerList.Add(new spr\u24D1((spr\u2158)A_0, this, A_2, BordersLineType.DiagonalDown));
			base.InnerList.Add(new spr\u24D1((spr\u2158)A_0, this, A_2, BordersLineType.DiagonalUp));
			base.InnerList.Add(new spr\u24D1((spr\u2158)A_0, this, A_2, BordersLineType.EdgeBottom));
			base.InnerList.Add(new spr\u24D1((spr\u2158)A_0, this, A_2, BordersLineType.EdgeLeft));
			base.InnerList.Add(new spr\u24D1((spr\u2158)A_0, this, A_2, BordersLineType.EdgeRight));
			base.InnerList.Add(new spr\u24D1((spr\u2158)A_0, this, A_2, BordersLineType.EdgeTop));
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000F6250 File Offset: 0x000F5250
		public override bool Equals(object obj)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					XlsBordersCollection xlsBordersCollection = obj as XlsBordersCollection;
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 4;
							continue;
						case 1:
							return false;
						case 2:
						{
							int num2;
							int count;
							if (num2 < count)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							bool flag;
							return flag;
						}
						case 3:
						{
							int num2;
							List<IBorder> innerList;
							List<IBorder> innerList2;
							if (!innerList[num2].Equals(innerList2[num2]))
							{
								num = 10;
								continue;
							}
							num2++;
							num = 6;
							continue;
						}
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_77;
							default:
							{
								if (false)
								{
								}
								bool flag;
								if (!flag)
								{
									num = 5;
									continue;
								}
								num = 3;
								continue;
							}
							}
							break;
						case 5:
						{
							bool flag;
							return flag;
						}
						case 6:
							goto IL_E9;
						case 7:
						{
							bool flag;
							return flag;
						}
						case 8:
						{
							if (xlsBordersCollection == null)
							{
								num = 1;
								continue;
							}
							List<IBorder> innerList = base.InnerList;
							List<IBorder> innerList2 = xlsBordersCollection.InnerList;
							bool flag = true;
							int num2 = 0;
							int count = base.Count;
							num = 9;
							continue;
						}
						case 9:
							goto IL_E9;
						case 10:
						{
							bool flag = false;
							goto IL_77;
						}
						}
						break;
						IL_77:
						num = 7;
						continue;
						IL_E9:
						num = 2;
					}
				}
				return false;
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000F63B0 File Offset: 0x000F53B0
		public override int GetHashCode()
		{
			int num;
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
						num = 0;
						List<IBorder> innerList = base.InnerList;
						int num2 = 0;
						int count = base.Count;
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_4C;
							case 1:
								if (num2 >= count)
								{
									num3 = 2;
									continue;
								}
								num ^= innerList[num2].GetHashCode();
								num2++;
								num3 = 3;
								continue;
							case 2:
								return num;
							case 3:
								goto IL_4C;
							}
							break;
							IL_4C:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num3 = 1;
								break;
							}
						}
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000F646C File Offset: 0x000F546C
		private new void ᜀ()
		{
			int a_ = 7;
			this.ᜀ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜀ == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("洼帾㍀♂⭄㍆楈⑊⽌╎㑐げ⅔睖㩘㩚㍜ㅞ๠ᝢ䕤զ౨䭪୬nѰᵲᅴ奶", a_));
				}
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000F64EC File Offset: 0x000F54EC
		internal new void ᜀ(BordersLineType A_0, IBorder A_1)
		{
			for (;;)
			{
				for (;;)
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_AF;
						case 2:
							if (true)
							{
							}
							switch (A_0)
							{
							case BordersLineType.DiagonalDown:
								goto IL_88;
							case BordersLineType.DiagonalUp:
								goto IL_C9;
							case BordersLineType.EdgeLeft:
								goto IL_6C;
							case BordersLineType.EdgeTop:
								goto IL_96;
							case BordersLineType.EdgeBottom:
								goto IL_7A;
							case BordersLineType.EdgeRight:
								goto IL_BB;
							default:
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
						break;
					}
				}
			}
			IL_6C:
			base.InnerList[3] = A_1;
			return;
			IL_7A:
			base.InnerList[2] = A_1;
			return;
			IL_88:
			base.InnerList[0] = A_1;
			return;
			IL_96:
			base.InnerList[5] = A_1;
			return;
			IL_AF:
			this.Add(A_1);
			return;
			IL_BB:
			base.InnerList[4] = A_1;
			return;
			IL_C9:
			base.InnerList[1] = A_1;
		}

		// Token: 0x0400107E RID: 4222
		private float \u25D9\u00A4\u00AE\u0094;

		// Token: 0x0400107F RID: 4223
		private new XlsWorkbook ᜀ;
	}
}
