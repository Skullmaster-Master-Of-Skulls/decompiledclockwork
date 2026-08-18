using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using Spire.CompoundFile.XLS;
using Spire.CompoundFile.XLS.Native;
using Spire.Xls.Collections;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.Security;
using Spire.Xls.Core.Spreadsheet.Shapes;
using Spire.Xls.Core.Spreadsheet.Sorting;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200060B RID: 1547
	public class XlsWorkbook : XlsObject, IWorkbook
	{
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06005B81 RID: 23425 RVA: 0x00391048 File Offset: 0x00390048
		public IWorksheet ActiveSheet
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
				return this.\u1734 as IWorksheet;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06005B82 RID: 23426 RVA: 0x00391090 File Offset: 0x00390090
		// (set) Token: 0x06005B83 RID: 23427 RVA: 0x003910E4 File Offset: 0x003900E4
		public int ActiveSheetIndex
		{
			get
			{
				if (this.\u1734 != null)
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
						return this.\u1734.RealIndex;
					}
				}
				if (true)
				{
				}
				return -1;
			}
			set
			{
				int a_ = 3;
				int num = 2;
				for (;;)
				{
					XlsWorksheetBase u;
					switch (num)
					{
					case 0:
						num = 4;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9A;
						default:
							if (false)
							{
							}
							u.Unselect(false);
							num = 6;
							continue;
						}
						break;
					case 3:
						goto IL_FC;
					case 4:
					{
						if (value >= this.ObjectCount)
						{
							num = 3;
							continue;
						}
						u = this.\u1734;
						this.\u1734 = (this.Objects[value] as XlsWorksheetBase);
						spr\u252A spr_u252A = (spr\u252A)this.\u1734;
						this.WindowOne.ᜇ((ushort)spr_u252A.get_RealIndex());
						num = 5;
						continue;
					}
					case 5:
						goto IL_9A;
					case 6:
						return;
					}
					if (value >= 0)
					{
						num = 0;
						continue;
					}
					break;
					IL_9A:
					if (u == null)
					{
						return;
					}
					num = 1;
				}
				IL_C6:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("砸堺䤼嘾㝀♂ᙄ⽆ⱈ⹊㥌َ㽐㝒ご⽖", a_));
				IL_FC:
				if (true)
				{
				}
				goto IL_C6;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06005B84 RID: 23428 RVA: 0x0039120C File Offset: 0x0039020C
		// (set) Token: 0x06005B85 RID: 23429 RVA: 0x00391258 File Offset: 0x00390258
		public string Author
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
				return this.ᝫ[BuiltInPropertyType.Author].Text;
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
				this.ᝫ[BuiltInPropertyType.Author].Text = value;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06005B86 RID: 23430 RVA: 0x003912A8 File Offset: 0x003902A8
		public IBuiltInDocumentProperties BuiltInDocumentProperties
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
				return this.ᝫ;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06005B87 RID: 23431 RVA: 0x003912EC File Offset: 0x003902EC
		// (set) Token: 0x06005B88 RID: 23432 RVA: 0x00391330 File Offset: 0x00390330
		public string CodeName
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
				return this.ᝊ;
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
				this.ᝊ = value;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06005B89 RID: 23433 RVA: 0x00391374 File Offset: 0x00390374
		public ICustomDocumentProperties CustomDocumentProperties
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
				return this.ᝬ;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06005B8A RID: 23434 RVA: 0x003913B8 File Offset: 0x003903B8
		// (set) Token: 0x06005B8B RID: 23435 RVA: 0x003913FC File Offset: 0x003903FC
		public bool Date1904
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
				return this.ᝁ;
			}
			set
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
				this.ᝁ = value;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06005B8C RID: 23436 RVA: 0x00391440 File Offset: 0x00390440
		// (set) Token: 0x06005B8D RID: 23437 RVA: 0x00391484 File Offset: 0x00390484
		public bool IsDisplayPrecision
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
				return this.ᝂ;
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
				this.ᝂ = value;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06005B8E RID: 23438 RVA: 0x003914C8 File Offset: 0x003904C8
		public bool IsCellProtection
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
				return this.ᝈ;
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06005B8F RID: 23439 RVA: 0x0039150C File Offset: 0x0039050C
		public bool IsWindowProtection
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
				return this.ᝉ;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06005B90 RID: 23440 RVA: 0x00391550 File Offset: 0x00390550
		public INameRanges Names
		{
			[DebuggerStepThrough]
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
				return this.\u1752;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06005B91 RID: 23441 RVA: 0x00391594 File Offset: 0x00390594
		// (set) Token: 0x06005B92 RID: 23442 RVA: 0x003915D8 File Offset: 0x003905D8
		public bool ReadOnly
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
				return this.ᝃ;
			}
			internal set
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
				this.ᝃ = value;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06005B93 RID: 23443 RVA: 0x0039161C File Offset: 0x0039061C
		// (set) Token: 0x06005B94 RID: 23444 RVA: 0x00391660 File Offset: 0x00390660
		public bool Saved
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
				return this.ᝄ;
			}
			set
			{
				int num = 4;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_72;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_74;
						default:
							if (false)
							{
							}
							this.Save();
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_74;
					case 3:
						if (value != this.ᝄ)
						{
							num = 1;
							continue;
						}
						goto IL_92;
					}
					if (!this.ᝄ)
					{
						num = 2;
						continue;
					}
					break;
					IL_74:
					num = 3;
				}
				IL_72:
				IL_92:
				this.ᝄ = value;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06005B95 RID: 23445 RVA: 0x00391708 File Offset: 0x00390708
		public IStyles Styles
		{
			[DebuggerStepThrough]
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
				return this.\u1736;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06005B96 RID: 23446 RVA: 0x0039174C File Offset: 0x0039074C
		public IWorksheets Worksheets
		{
			[DebuggerStepThrough]
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
				return this.\u1735;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06005B97 RID: 23447 RVA: 0x00391790 File Offset: 0x00390790
		// (set) Token: 0x06005B98 RID: 23448 RVA: 0x003917D4 File Offset: 0x003907D4
		public bool HasMacros
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
				return this.ᝋ;
			}
			internal set
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
				this.ᝋ = value;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06005B99 RID: 23449 RVA: 0x00391818 File Offset: 0x00390818
		public Color[] Palette
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
				return this.ᝏ.ToArray();
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06005B9A RID: 23450 RVA: 0x00391860 File Offset: 0x00390860
		// (set) Token: 0x06005B9B RID: 23451 RVA: 0x003918A8 File Offset: 0x003908A8
		public int DisplayedTab
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
				return (int)this.WindowOne.ᜈ();
			}
			set
			{
				int a_ = 11;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 2:
						if (value <= this.\u1754.Count)
						{
							goto IL_AA;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_A8;
					}
					if (true)
					{
					}
					if (value < 0)
					{
						break;
					}
					num = 0;
				}
				IL_3F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Հ⩂㙄㝆╈⩊㑌⩎㕐ݒ㑔㕖", a_), RecordTableEnumerator.b("ᝀ≂⥄㉆ⱈ歊㹌❎㹐♒㥔㍖祘㥚㡜罞٠ᅢd٦ᵨ๪Ὤ佮հ᭲ᑴ᥶奸ź᡼ൾꎂꮊ떔漢뾞슠첢키즦\udda8讪슬즮醰쒲\udab4얶트좺햼\udabe꓀럂뛄", a_));
				IL_A8:
				goto IL_3F;
				IL_AA:
				this.WindowOne.ᜆ((ushort)value);
				this.WindowOne.ᜇ((ushort)value);
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x0039197C File Offset: 0x0039097C
		public ICharts Charts
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
				return this.\u1753;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06005B9D RID: 23453 RVA: 0x003919C0 File Offset: 0x003909C0
		// (set) Token: 0x06005B9E RID: 23454 RVA: 0x00391A04 File Offset: 0x00390A04
		public bool ThrowOnUnknownNames
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
				return this.\u1758;
			}
			set
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
				this.\u1758 = value;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06005B9F RID: 23455 RVA: 0x00391A48 File Offset: 0x00390A48
		// (set) Token: 0x06005BA0 RID: 23456 RVA: 0x00391A90 File Offset: 0x00390A90
		public bool IsHScrollBarVisible
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
				return this.WindowOne.ᜃ();
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
				this.WindowOne.ᜁ(value);
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06005BA1 RID: 23457 RVA: 0x00391AD8 File Offset: 0x00390AD8
		// (set) Token: 0x06005BA2 RID: 23458 RVA: 0x00391B20 File Offset: 0x00390B20
		public bool IsVScrollBarVisible
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
				return this.WindowOne.ᜂ();
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
				this.WindowOne.ᜄ(value);
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06005BA3 RID: 23459 RVA: 0x00391B68 File Offset: 0x00390B68
		// (set) Token: 0x06005BA4 RID: 23460 RVA: 0x00391BAC File Offset: 0x00390BAC
		public bool DisableMacrosStart
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
				return this.ᝎ;
			}
			set
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
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᝎ = value;
							this.Saved = false;
							num = 1;
							continue;
						case 1:
							goto IL_73;
						}
						if (value == this.ᝎ)
						{
							break;
						}
						num = 0;
					}
					IL_73:
					break;
				}
				}
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06005BA5 RID: 23461 RVA: 0x00391C30 File Offset: 0x00390C30
		// (set) Token: 0x06005BA6 RID: 23462 RVA: 0x00391C84 File Offset: 0x00390C84
		public double StandardFontSize
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
				return ((XlsFont)this.\u1737[0]).Size;
			}
			set
			{
				int a_ = 3;
				for (;;)
				{
					IL_09:
					if (true)
					{
					}
					for (;;)
					{
						((XlsFont)this.\u1737[0]).Size = (double)((int)value);
						FontWrapper fontWrapper = this.Styles[RecordTableEnumerator.b("眸吺似刾⁀⽂", a_)].Font as FontWrapper;
						this.\u177D = -1.0;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								fontWrapper.ᜁ();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_09;
								default:
									if (false)
									{
									}
									num = 2;
									continue;
								}
								break;
							case 1:
								if (fontWrapper.Index < 4)
								{
									num = 0;
									continue;
								}
								return;
							case 2:
								return;
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06005BA7 RID: 23463 RVA: 0x00391D5C File Offset: 0x00390D5C
		// (set) Token: 0x06005BA8 RID: 23464 RVA: 0x00391DB0 File Offset: 0x00390DB0
		public string StandardFont
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
				return ((XlsFont)this.\u1737[0]).FontName;
			}
			set
			{
				for (;;)
				{
					int num = 0;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							goto IL_24;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_24;
							default:
								goto IL_4E;
							}
							break;
						case 2:
							if (num >= 4)
							{
								num2 = 1;
								continue;
							}
							((XlsFont)this.\u1737[0]).FontName = value;
							num++;
							num2 = 0;
							continue;
						case 3:
							goto IL_24;
						}
						break;
						IL_24:
						num2 = 2;
					}
				}
				IL_4E:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06005BA9 RID: 23465 RVA: 0x00391E50 File Offset: 0x00390E50
		internal XlsFont DefaultFont
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
				return (XlsFont)this.\u1737[0];
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x00391E9C File Offset: 0x00390E9C
		// (set) Token: 0x06005BAB RID: 23467 RVA: 0x00391EE0 File Offset: 0x00390EE0
		public bool Allow3DRangesInDataValidation
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
				return this.ᝡ;
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
				this.ᝡ = value;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x00391F24 File Offset: 0x00390F24
		public IAddInFunctions AddInFunctions
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
				return this.ᝣ;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06005BAD RID: 23469 RVA: 0x00391F68 File Offset: 0x00390F68
		internal ICalculationOptions CalculationOptions
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
				return this.ᝥ;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06005BAE RID: 23470 RVA: 0x00391FAC File Offset: 0x00390FAC
		public string RowSeparator
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
				return this.FormulaUtil.ArrayRowSeparator;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06005BAF RID: 23471 RVA: 0x00391FF4 File Offset: 0x00390FF4
		public string ArgumentsSeparator
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
				return this.FormulaUtil.OperandsSeparator;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x0039203C File Offset: 0x0039103C
		public IWorksheetGroup WorksheetGroup
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
				return this.ᝩ;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06005BB1 RID: 23473 RVA: 0x00392080 File Offset: 0x00391080
		// (set) Token: 0x06005BB2 RID: 23474 RVA: 0x003920C8 File Offset: 0x003910C8
		public bool IsRightToLeft
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
				return this.\u1735.IsRightToLeft;
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
				this.\u1735.IsRightToLeft = value;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06005BB3 RID: 23475 RVA: 0x00392110 File Offset: 0x00391110
		// (set) Token: 0x06005BB4 RID: 23476 RVA: 0x00392158 File Offset: 0x00391158
		public bool DisplayWorkbookTabs
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
				return this.WindowOne.ᜐ();
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
				this.WindowOne.ᜀ(value);
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06005BB5 RID: 23477 RVA: 0x003921A0 File Offset: 0x003911A0
		public ITabSheets TabSheets
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
				return this.\u1754;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06005BB6 RID: 23478 RVA: 0x003921E4 File Offset: 0x003911E4
		// (set) Token: 0x06005BB7 RID: 23479 RVA: 0x00392228 File Offset: 0x00391228
		public bool DetectDateTimeInValue
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
				return this.ᝯ;
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
				this.ᝯ = value;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06005BB8 RID: 23480 RVA: 0x0039226C File Offset: 0x0039126C
		// (set) Token: 0x06005BB9 RID: 23481 RVA: 0x003922B4 File Offset: 0x003912B4
		public bool UseFastStringSearching
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
				return this.\u173D.UseHashForSearching;
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
				this.\u173D.UseHashForSearching = value;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06005BBA RID: 23482 RVA: 0x003922FC File Offset: 0x003912FC
		// (set) Token: 0x06005BBB RID: 23483 RVA: 0x00392354 File Offset: 0x00391354
		public bool ReadOnlyRecommended
		{
			get
			{
				if (true)
				{
				}
				if (this.ᝮ == null)
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
						return false;
					}
				}
				return this.ᝮ.ᜃ() != 0;
			}
			set
			{
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E8;
					case 1:
						if (this.ᝮ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						this.ᝮ.ᜀ(0);
						num = 5;
						continue;
					case 3:
						if (true)
						{
						}
						this.ᝮ = (sprẋ)spr\u175E.ᜀ(TBIFFRecord.FileSharing);
						num = 0;
						continue;
					case 4:
						num = 6;
						continue;
					case 5:
						goto IL_C1;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (this.ᝮ == null)
							{
								num = 3;
								continue;
							}
							goto IL_64;
						}
						break;
					}
					IL_30:
					if (value)
					{
						num = 4;
						continue;
					}
					num = 1;
					continue;
					goto IL_30;
				}
				IL_64:
				this.ᝮ.ᜀ(1);
				return;
				IL_C1:
				return;
				IL_E8:
				goto IL_64;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06005BBC RID: 23484 RVA: 0x00392450 File Offset: 0x00391450
		// (set) Token: 0x06005BBD RID: 23485 RVA: 0x00392494 File Offset: 0x00391494
		public string PasswordToOpen
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
				return this.\u1772;
			}
			set
			{
				if (true)
				{
				}
				for (;;)
				{
					this.\u1772 = value;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
					{
						if (false)
						{
						}
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (value.Length == 0)
								{
									num = 1;
									continue;
								}
								goto IL_84;
							case 1:
								goto IL_82;
							case 2:
								num = 0;
								continue;
							case 3:
								if (value != null)
								{
									num = 2;
									continue;
								}
								goto IL_62;
							}
							break;
						}
						break;
					}
					}
				}
				IL_62:
				this.\u1773 = EncryptionType.None;
				return;
				IL_82:
				goto IL_62;
				IL_84:
				this.\u1773 = EncryptionType.Standard;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06005BBE RID: 23486 RVA: 0x0039252C File Offset: 0x0039152C
		public int MaxRowCount
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
				return this.\u1775;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x00392570 File Offset: 0x00391570
		public int MaxColumnCount
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
				return this.\u1776;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x003925B4 File Offset: 0x003915B4
		public int MaxXFCount
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
				return this.\u1777;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06005BC1 RID: 23489 RVA: 0x003925F8 File Offset: 0x003915F8
		public int MaxIndent
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
				return this.\u1778;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06005BC2 RID: 23490 RVA: 0x0039263C File Offset: 0x0039163C
		// (set) Token: 0x06005BC3 RID: 23491 RVA: 0x00392680 File Offset: 0x00391680
		internal int MaxImportRows
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
				return this.\u1779;
			}
			set
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
				this.\u1779 = value;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06005BC4 RID: 23492 RVA: 0x003926C4 File Offset: 0x003916C4
		// (set) Token: 0x06005BC5 RID: 23493 RVA: 0x00392708 File Offset: 0x00391708
		internal ExcelParseOptions Options
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
				return this.ប;
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
				this.ប = value;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06005BC6 RID: 23494 RVA: 0x0039274C File Offset: 0x0039174C
		internal List<Stream> PreservesPivotCache
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_6F;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						default:
							if (false)
							{
							}
							this.ន = new List<Stream>();
							num = 1;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_2C:
					num = 2;
					continue;
					IL_1C:
					if (true)
					{
					}
					if (this.ន == null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_6F:
				return this.ន;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06005BC7 RID: 23495 RVA: 0x003927D0 File Offset: 0x003917D0
		internal sprវ DataHolder
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
				return this.\u177C;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06005BC8 RID: 23496 RVA: 0x00392814 File Offset: 0x00391814
		// (set) Token: 0x06005BC9 RID: 23497 RVA: 0x00392858 File Offset: 0x00391858
		internal Workbook InnerWorkBook
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
				return this.\u173B;
			}
			set
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
				this.\u173B = value;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06005BCA RID: 23498 RVA: 0x0039289C File Offset: 0x0039189C
		internal sprឦ InnerNamesColection
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
				return this.\u1752;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06005BCB RID: 23499 RVA: 0x003928E0 File Offset: 0x003918E0
		public XlsAddInFunctionsCollection InnerAddInFunctions
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
				return this.ᝣ;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06005BCC RID: 23500 RVA: 0x00392924 File Offset: 0x00391924
		// (set) Token: 0x06005BCD RID: 23501 RVA: 0x00392968 File Offset: 0x00391968
		public string FullFileName
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
				return this.ᝀ;
			}
			[DebuggerStepThrough]
			internal set
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
				this.ᝀ = value;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06005BCE RID: 23502 RVA: 0x003929AC File Offset: 0x003919AC
		public XlsFontsCollection InnerFonts
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
				return this.\u1737;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06005BCF RID: 23503 RVA: 0x003929F0 File Offset: 0x003919F0
		internal sprᢖ InnerExtFormats
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
				return this.\u1738;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x00392A34 File Offset: 0x00391A34
		internal spr\u21FF InnerFormats
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
				return this.\u173A;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06005BD1 RID: 23505 RVA: 0x00392A78 File Offset: 0x00391A78
		internal SSTDictionary InnerSST
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
				return this.\u173D;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06005BD2 RID: 23506 RVA: 0x00392ABC File Offset: 0x00391ABC
		// (set) Token: 0x06005BD3 RID: 23507 RVA: 0x00392B00 File Offset: 0x00391B00
		public bool Loading
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
				return this.ᝆ;
			}
			internal set
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
				this.ᝆ = value;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06005BD4 RID: 23508 RVA: 0x00392B44 File Offset: 0x00391B44
		// (set) Token: 0x06005BD5 RID: 23509 RVA: 0x00392B88 File Offset: 0x00391B88
		public bool Saving
		{
			[DebuggerStepThrough]
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
				return this.ᝇ;
			}
			[DebuggerStepThrough]
			internal set
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
				this.ᝇ = value;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06005BD6 RID: 23510 RVA: 0x00392BCC File Offset: 0x00391BCC
		internal spr\u17B5 WindowOne
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_76;
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (false)
							{
							}
							this.ᝑ = (spr\u17B5)spr\u175E.ᜀ(TBIFFRecord.WindowOne);
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_24:
					num = 1;
					continue;
					IL_1C:
					if (this.ᝑ == null)
					{
						goto IL_24;
					}
					break;
				}
				IL_76:
				return this.ᝑ;
			}
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06005BD7 RID: 23511 RVA: 0x00392C58 File Offset: 0x00391C58
		public int ObjectCount
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
				return this.\u1754.Count;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06005BD8 RID: 23512 RVA: 0x00392CA0 File Offset: 0x00391CA0
		public double MaxDigitWidth
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_79;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							this.\u177D = this.GetMaxDigitWidth();
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_35:
					num = 2;
					continue;
					IL_1C:
					if (true)
					{
					}
					if (this.\u177D <= 0.0)
					{
						goto IL_35;
					}
					break;
				}
				IL_79:
				return this.\u177D;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06005BD9 RID: 23513 RVA: 0x00392D30 File Offset: 0x00391D30
		// (set) Token: 0x06005BDA RID: 23514 RVA: 0x00392D74 File Offset: 0x00391D74
		internal Stream SSTStream
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
				return this.ត;
			}
			set
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
				this.ត = value;
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06005BDB RID: 23515 RVA: 0x00392DB8 File Offset: 0x00391DB8
		// (set) Token: 0x06005BDC RID: 23516 RVA: 0x00392DFC File Offset: 0x00391DFC
		internal bool HasInlineStrings
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
				return this.ថ;
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
				this.ថ = value;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06005BDD RID: 23517 RVA: 0x00392E40 File Offset: 0x00391E40
		// (set) Token: 0x06005BDE RID: 23518 RVA: 0x00392ECC File Offset: 0x00391ECC
		internal spr\u24C3 Password
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						default:
							if (false)
							{
							}
							this.\u1755 = (spr\u24C3)spr\u175E.ᜀ(TBIFFRecord.Password);
							num = 2;
							continue;
						}
						break;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						goto IL_76;
					}
					goto IL_24;
					IL_2C:
					num = 0;
					continue;
					IL_24:
					if (this.\u1755 == null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_76:
				return this.\u1755;
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
				this.\u1755 = value;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06005BDF RID: 23519 RVA: 0x00392F10 File Offset: 0x00391F10
		// (set) Token: 0x06005BE0 RID: 23520 RVA: 0x00392FA0 File Offset: 0x00391FA0
		internal spr\u1938 PasswordRev4
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_79;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (false)
							{
							}
							this.\u1756 = (spr\u1938)spr\u175E.ᜀ(TBIFFRecord.PasswordRev4);
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_24:
					num = 2;
					continue;
					IL_1C:
					if (this.\u1756 == null)
					{
						goto IL_24;
					}
					break;
				}
				IL_79:
				return this.\u1756;
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
				this.\u1756 = value;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x00392FE4 File Offset: 0x00391FE4
		// (set) Token: 0x06005BE2 RID: 23522 RVA: 0x00393074 File Offset: 0x00392074
		internal spr\u237D ProtectionRev4
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_79;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.\u1757 = (spr\u237D)spr\u175E.ᜀ(TBIFFRecord.ProtectionRev4);
							num = 0;
							continue;
						}
						break;
					}
					goto IL_1C;
					IL_24:
					num = 1;
					continue;
					IL_1C:
					if (this.\u1757 == null)
					{
						goto IL_24;
					}
					break;
				}
				IL_79:
				return this.\u1757;
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
				this.\u1757 = value;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06005BE3 RID: 23523 RVA: 0x003930B8 File Offset: 0x003920B8
		// (set) Token: 0x06005BE4 RID: 23524 RVA: 0x003930FC File Offset: 0x003920FC
		public int CurrentObjectId
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
				return this.\u175B;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_28;
						default:
							if (false)
							{
							}
							this.\u175B = value;
							num = 1;
							continue;
						}
						break;
					}
					goto IL_24;
					IL_28:
					num = 2;
					continue;
					IL_24:
					if (value > 0)
					{
						goto IL_28;
					}
					break;
				}
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06005BE5 RID: 23525 RVA: 0x00393174 File Offset: 0x00392174
		// (set) Token: 0x06005BE6 RID: 23526 RVA: 0x003931B8 File Offset: 0x003921B8
		public int CurrentShapeId
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
				return this.\u175C;
			}
			set
			{
				int a_ = 15;
				if (value >= 0)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_0F;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					this.\u175C = value;
					return;
				}
				IL_0F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙄⽆⡈㭊⡌潎㡐㝒", a_));
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06005BE7 RID: 23527 RVA: 0x00393220 File Offset: 0x00392220
		// (set) Token: 0x06005BE8 RID: 23528 RVA: 0x00393264 File Offset: 0x00392264
		public int CurrentHeaderId
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
				return this.\u175D;
			}
			set
			{
				int a_ = 8;
				if (true)
				{
				}
				if (value >= 0)
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
						this.\u175D = value;
						return;
					}
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴽⠿⍁㑃⍅桇⍉⡋", a_));
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06005BE9 RID: 23529 RVA: 0x003932CC File Offset: 0x003922CC
		internal List<sprỶ> InnerExtFormatRecords
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
				return this.\u175E;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06005BEA RID: 23530 RVA: 0x00393310 File Offset: 0x00392310
		// (set) Token: 0x06005BEB RID: 23531 RVA: 0x00393354 File Offset: 0x00392354
		protected internal XlsWorkbookObjectsCollection Objects
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
				return this.\u1754;
			}
			internal set
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
				this.\u1754 = value;
			}
		}

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06005BEC RID: 23532 RVA: 0x00393398 File Offset: 0x00392398
		internal XlsStylesCollection InnerStyles
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
				return this.\u1736;
			}
		}

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06005BED RID: 23533 RVA: 0x003933DC File Offset: 0x003923DC
		protected internal XlsWorksheetsCollection InnerWorksheets
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
				return this.\u1735;
			}
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06005BEE RID: 23534 RVA: 0x00393420 File Offset: 0x00392420
		protected internal XlsChartsCollection InnerCharts
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
				return this.\u1753;
			}
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06005BEF RID: 23535 RVA: 0x00393464 File Offset: 0x00392464
		public XlsExternBookCollection ExternWorkbooks
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
				return this.ᝢ;
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06005BF0 RID: 23536 RVA: 0x003934A8 File Offset: 0x003924A8
		internal sprỆ InnerCalculation
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
				return this.ᝥ;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x003934EC File Offset: 0x003924EC
		public Graphics InnerGraphics
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
				return this.ᝧ;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06005BF2 RID: 23538 RVA: 0x00393530 File Offset: 0x00392530
		public FormulaUtil FormulaUtil
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						default:
							if (false)
							{
							}
							this.ᝨ = new FormulaUtil(base.ReservedHandle, this);
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_76;
					}
					goto IL_1C;
					IL_2C:
					num = 0;
					continue;
					IL_1C:
					if (true)
					{
					}
					if (this.ᝨ == null)
					{
						goto IL_2C;
					}
					break;
				}
				IL_76:
				return this.ᝨ;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06005BF3 RID: 23539 RVA: 0x003935BC File Offset: 0x003925BC
		internal spr\u233D InnerWorksheetGroup
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
				return this.ᝩ;
			}
		}

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06005BF4 RID: 23540 RVA: 0x00393600 File Offset: 0x00392600
		// (set) Token: 0x06005BF5 RID: 23541 RVA: 0x00393644 File Offset: 0x00392644
		internal bool? IsStartsOrEndsWith
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
				return this.ឋ;
			}
			set
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
				this.ឋ = value;
			}
		}

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06005BF6 RID: 23542 RVA: 0x00393688 File Offset: 0x00392688
		// (set) Token: 0x06005BF7 RID: 23543 RVA: 0x003936CC File Offset: 0x003926CC
		public bool HasDuplicatedNames
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
				return this.ᝪ;
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
				this.ᝪ = value;
			}
		}

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06005BF8 RID: 23544 RVA: 0x00393710 File Offset: 0x00392710
		public XlsWorkbookShapeData ShapesData
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
				return this.\u1759;
			}
		}

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06005BF9 RID: 23545 RVA: 0x00393754 File Offset: 0x00392754
		public XlsWorkbookShapeData HeaderFooterData
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
				return this.ᝤ;
			}
		}

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06005BFA RID: 23546 RVA: 0x00393798 File Offset: 0x00392798
		internal sprᦖ ExternSheet
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
				return this.\u173E;
			}
		}

		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x003937DC File Offset: 0x003927DC
		// (set) Token: 0x06005BFC RID: 23548 RVA: 0x00393820 File Offset: 0x00392820
		protected internal bool InternalSaved
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
				return this.ᝄ;
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
				this.ᝄ = value;
			}
		}

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x00393864 File Offset: 0x00392864
		// (set) Token: 0x06005BFE RID: 23550 RVA: 0x003938A8 File Offset: 0x003928A8
		public int FirstCharSize
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
				return this.ᝰ;
			}
			set
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
				this.ᝰ = value;
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x003938EC File Offset: 0x003928EC
		// (set) Token: 0x06005C00 RID: 23552 RVA: 0x00393930 File Offset: 0x00392930
		public int SecondCharSize
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
				return this.\u1771;
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
				this.\u1771 = value;
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06005C01 RID: 23553 RVA: 0x00393974 File Offset: 0x00392974
		internal bool IsConverted
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
				return this.ធ;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06005C02 RID: 23554 RVA: 0x003939B8 File Offset: 0x003929B8
		// (set) Token: 0x06005C03 RID: 23555 RVA: 0x003939FC File Offset: 0x003929FC
		public ExcelVersion Version
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
				return this.\u177A;
			}
			set
			{
				switch (0)
				{
				default:
					for (;;)
					{
						bool flag = false;
						int num = 8;
						for (;;)
						{
							sprឦ sprឦ;
							int num2;
							int count;
							XlsWorksheet xlsWorksheet;
							switch (num)
							{
							case 0:
								this.\u173D.RemoveUnnecessaryStrings();
								num = 38;
								continue;
							case 1:
								if (value == ExcelVersion.Version2010)
								{
									num = 37;
									continue;
								}
								goto IL_328;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1AA;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							case 3:
								goto IL_1BB;
							case 4:
								num = 25;
								continue;
							case 5:
								goto IL_21F;
							case 6:
								goto IL_27D;
							case 7:
								if (true)
								{
								}
								if (sprឦ != null)
								{
									num = 29;
									continue;
								}
								return;
							case 8:
								if (this.ឍ)
								{
									num = 24;
									continue;
								}
								goto IL_328;
							case 9:
								num = 31;
								continue;
							case 10:
								this.ឍ = true;
								goto IL_1AA;
							case 11:
								this.\u1714();
								flag = true;
								num = 33;
								continue;
							case 12:
								if (this.\u177A == ExcelVersion.Version97to2003)
								{
									num = 39;
									continue;
								}
								goto IL_328;
							case 13:
								if (this.\u177A == ExcelVersion.Version2007)
								{
									num = 32;
									continue;
								}
								goto IL_368;
							case 14:
								goto IL_21F;
							case 15:
								switch (value)
								{
								case ExcelVersion.Version97to2003:
									this.\u1775 = 65536;
									this.\u1776 = 256;
									this.\u1738.ᜂ(4075);
									this.\u1777 = 4075;
									this.\u1738.ᜂ(4095);
									this.\u1777 = 4095;
									this.\u1778 = 15;
									this.ᜃ();
									this.\u1714();
									num = 14;
									continue;
								case ExcelVersion.Version2007:
								case ExcelVersion.Version2010:
									this.\u1775 = 1048576;
									this.\u1776 = 16384;
									this.\u1738.ᜂ(64000);
									this.\u1777 = 64000;
									this.\u1778 = 250;
									num = 36;
									continue;
								default:
									num = 19;
									continue;
								}
								break;
							case 16:
								if (this.\u177A == ExcelVersion.Version2010)
								{
									num = 4;
									continue;
								}
								goto IL_3C0;
							case 17:
							{
								PivotTableCollection pivotTables;
								pivotTables.ᜀ();
								num = 27;
								continue;
							}
							case 18:
								goto IL_2DA;
							case 19:
								num = 5;
								continue;
							case 20:
								goto IL_368;
							case 21:
								if (this.\u177A != value)
								{
									num = 30;
									continue;
								}
								return;
							case 22:
								if (value != ExcelVersion.Version2010)
								{
									num = 20;
									continue;
								}
								goto IL_3E9;
							case 23:
								if (flag)
								{
									num = 17;
									continue;
								}
								goto IL_38E;
							case 24:
								num = 12;
								continue;
							case 25:
								if (value == ExcelVersion.Version2007)
								{
									num = 34;
									continue;
								}
								goto IL_3C0;
							case 26:
								if (value != ExcelVersion.Version2007)
								{
									num = 2;
									continue;
								}
								goto IL_350;
							case 27:
								goto IL_38E;
							case 28:
							{
								if (num2 >= count)
								{
									num = 9;
									continue;
								}
								xlsWorksheet = (XlsWorksheet)this.\u1735[num2];
								PivotTableCollection pivotTables = xlsWorksheet.PivotTables;
								num = 23;
								continue;
							}
							case 29:
								sprឦ.ᜀ(value);
								num = 18;
								continue;
							case 30:
							{
								ExcelVersion u177A = this.\u177A;
								this.\u177A = value;
								this.ᝋ = false;
								num = 15;
								continue;
							}
							case 31:
								if (value == ExcelVersion.Version97to2003)
								{
									num = 0;
									continue;
								}
								goto IL_23B;
							case 32:
								num = 22;
								continue;
							case 33:
								goto IL_21F;
							case 34:
								goto IL_2FE;
							case 35:
								goto IL_1BB;
							case 36:
							{
								ExcelVersion u177A;
								if (u177A == ExcelVersion.Version97to2003)
								{
									num = 11;
									continue;
								}
								goto IL_21F;
							}
							case 37:
								goto IL_350;
							case 38:
								goto IL_23B;
							case 39:
								num = 26;
								continue;
							case 40:
								if (!this.ឍ)
								{
									num = 10;
									continue;
								}
								goto IL_27D;
							case 41:
								goto IL_27D;
							}
							break;
							IL_1AA:
							num = 6;
							continue;
							IL_1BB:
							num = 28;
							continue;
							IL_21F:
							num2 = 0;
							count = this.\u1735.Count;
							num = 35;
							continue;
							IL_23B:
							sprឦ = this.InnerNamesColection;
							num = 7;
							continue;
							IL_27D:
							num = 13;
							continue;
							IL_328:
							num = 40;
							continue;
							IL_350:
							this.ធ = true;
							num = 41;
							continue;
							IL_368:
							num = 16;
							continue;
							IL_38E:
							xlsWorksheet.Version = value;
							num2++;
							num = 3;
							continue;
							IL_3C0:
							num = 21;
						}
					}
					IL_2DA:
					return;
					IL_2FE:
					IL_3E9:
					this.\u177A = value;
					return;
				}
			}
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x00393F1C File Offset: 0x00392F1C
		private void \u1714()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
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
						this.ᝦ.Clear();
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				}
				if (this.ᝦ == null)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x00393F9C File Offset: 0x00392F9C
		// (set) Token: 0x06005C06 RID: 23558 RVA: 0x00393FE0 File Offset: 0x00392FE0
		public int DefaultXFIndex
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
				return this.\u177B;
			}
			set
			{
				int a_ = 10;
				while (value >= 0)
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
						this.\u177B = value;
						return;
					}
				}
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("п❁≃❅㵇♉㡋ᙍᙏ᭑㩓㉕㵗≙", a_));
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06005C07 RID: 23559 RVA: 0x00394048 File Offset: 0x00393048
		public List<Color> InnerPalette
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
				return this.ᝏ;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06005C08 RID: 23560 RVA: 0x0039408C File Offset: 0x0039308C
		public IntPtr HeapHandle
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_78;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.\u177E = Heap.HeapCreate(0, 131072, 0);
							num = 1;
							continue;
						}
						break;
					}
					if (!(this.\u177E == IntPtr.Zero))
					{
						goto IL_82;
					}
					num = 2;
				}
				IL_78:
				if (true)
				{
				}
				IL_82:
				return this.\u177E;
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x00394124 File Offset: 0x00393124
		internal XlsPivotCachesCollection PivotCaches
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
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
							this.ᝦ = new PivotCachesCollection((spr\u2158)base.AppImplementation, this);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_7B;
					}
					if (this.ᝦ != null)
					{
						break;
					}
					num = 0;
				}
				IL_7B:
				return this.ᝦ;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06005C0A RID: 23562 RVA: 0x003941B4 File Offset: 0x003931B4
		XlsPivotCachesCollection IWorkbook.PivotCaches
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							this.ᝦ = new PivotCachesCollection((spr\u2158)base.AppImplementation, this);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_7B;
					}
					if (true)
					{
					}
					if (this.ᝦ != null)
					{
						break;
					}
					num = 0;
				}
				IL_7B:
				return this.ᝦ;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x00394244 File Offset: 0x00393244
		// (set) Token: 0x06005C0C RID: 23564 RVA: 0x00394288 File Offset: 0x00393288
		internal Stream ControlsStream
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
				return this.ខ;
			}
			set
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
				this.ខ = value;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x003942CC File Offset: 0x003932CC
		// (set) Token: 0x06005C0E RID: 23566 RVA: 0x00394310 File Offset: 0x00393310
		internal Stream CustomTableStylesStream
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
				return this.ង;
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
				this.ង = value;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x00394354 File Offset: 0x00393354
		// (set) Token: 0x06005C10 RID: 23568 RVA: 0x00394398 File Offset: 0x00393398
		internal int MaxTableIndex
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
				return this.គ;
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
				this.គ = value;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x003943DC File Offset: 0x003933DC
		internal bool IsCreated
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
				return this.ឆ;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06005C12 RID: 23570 RVA: 0x00394420 File Offset: 0x00393420
		public bool IsLoaded
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
				return this.ច;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x00394464 File Offset: 0x00393464
		// (set) Token: 0x06005C14 RID: 23572 RVA: 0x003944A8 File Offset: 0x003934A8
		internal Dictionary<string, XlsFont> MajorFonts
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
				return this.ជ;
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
				this.ជ = value;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06005C15 RID: 23573 RVA: 0x003944EC File Offset: 0x003934EC
		// (set) Token: 0x06005C16 RID: 23574 RVA: 0x00394530 File Offset: 0x00393530
		internal Dictionary<string, XlsFont> MinorFonts
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
				return this.ឈ;
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
				this.ឈ = value;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06005C17 RID: 23575 RVA: 0x00394574 File Offset: 0x00393574
		// (set) Token: 0x06005C18 RID: 23576 RVA: 0x003945CC File Offset: 0x003935CC
		internal bool CheckCompabilityVersion
		{
			get
			{
				while (this.ណ != null)
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
						return this.ណ.ᜀ() != 0U;
					}
				}
				if (true)
				{
				}
				return false;
			}
			set
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3A;
						default:
							goto IL_77;
						}
						break;
					case 3:
						goto IL_3A;
					}
					if (this.ណ == null)
					{
						num = 3;
						continue;
					}
					goto IL_4F;
					IL_3A:
					this.ណ = new sprᬡ();
					num = 0;
					continue;
					IL_4F:
					num = 1;
				}
				IL_77:
				if (false)
				{
				}
				this.ណ.ᜀ((!value) ? 1U : 0U);
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06005C19 RID: 23577 RVA: 0x0039466C File Offset: 0x0039366C
		// (set) Token: 0x06005C1A RID: 23578 RVA: 0x003946B0 File Offset: 0x003936B0
		internal bool HasApostrophe
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
				return this.ដ;
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
				this.ដ = value;
			}
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x003946F4 File Offset: 0x003936F4
		internal Color ᜂ(int A_0)
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
			return this.ក[A_0];
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x0039473C File Offset: 0x0039373C
		protected internal IExtendedFormat CreateExtFormat(bool bForceAdd)
		{
			spr\u192F spr_u192F;
			for (;;)
			{
				spr_u192F = new spr\u192F(base.ReservedHandle, this);
				spr_u192F.ᜃ((int)((ushort)this.\u1738.Count));
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (bForceAdd)
						{
							num = 1;
							continue;
						}
						this.\u1738.ᜁ(spr_u192F);
						num = 2;
						continue;
					case 1:
						this.\u1738.ᜀ(spr_u192F);
						num = 3;
						continue;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_89;
						}
						break;
					case 3:
						return spr_u192F;
					}
					break;
				}
			}
			IL_89:
			if (false)
			{
			}
			return spr_u192F;
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06005C1D RID: 23581 RVA: 0x003947F8 File Offset: 0x003937F8
		internal bool IsEqualColor
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
				return this.ញ;
			}
		}

		// Token: 0x06005C1E RID: 23582 RVA: 0x0039483C File Offset: 0x0039383C
		protected internal IExtendedFormat CreateExtFormat(IExtendedFormat baseFormat, bool bForceAdd)
		{
			int a_ = 1;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (bForceAdd)
					{
						num = 3;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						spr\u192F spr_u192F;
						return spr_u192F;
					}
					default:
					{
						if (false)
						{
						}
						spr\u192F spr_u192F = this.\u1738.ᜁ(spr_u192F);
						num = 4;
						continue;
					}
					}
					break;
				case 2:
					goto IL_3C;
				case 3:
				{
					spr\u192F spr_u192F;
					this.\u1738.ᜀ(spr_u192F);
					num = 5;
					continue;
				}
				case 4:
				{
					spr\u192F spr_u192F;
					return spr_u192F;
				}
				case 5:
				{
					spr\u192F spr_u192F;
					return spr_u192F;
				}
				}
				if (baseFormat == null)
				{
					num = 2;
				}
				else
				{
					spr\u192F spr_u192F = this.ᜀ(baseFormat);
					num = 0;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("唶堸䠺堼社⹀ㅂ⡄♆㵈", a_));
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x00394918 File Offset: 0x00393918
		internal spr\u192F ᜀ(IExtendedFormat A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				XlsShapeFill a_2;
				spr\u192F spr_u192F;
				for (;;)
				{
					a_2 = null;
					int num = 9;
					for (;;)
					{
						sprỶ a_3;
						spr\u192F spr_u192F2;
						switch (num)
						{
						case 0:
							goto IL_6F;
						case 1:
							spr_u192F = (spr\u192F)A_0;
							a_3 = (sprỶ)spr_u192F.ᜑ().Clone();
							num = 3;
							continue;
						case 2:
							if (this.Version != ExcelVersion.Version97to2003)
							{
								num = 7;
								continue;
							}
							goto IL_265;
						case 3:
							goto IL_13A;
						case 4:
							goto IL_1B6;
						case 5:
							if (spr_u192F.ᝐ() != null)
							{
								num = 8;
								continue;
							}
							goto IL_CD;
						case 6:
							if (A_0 is spr\u192F)
							{
								num = 1;
								continue;
							}
							num = 13;
							continue;
						case 7:
							spr_u192F.ᜡ().ᜀ(spr_u192F2.ᜡ(), false);
							spr_u192F.\u173F().ᜀ(spr_u192F2.\u173F(), false);
							spr_u192F.ᝅ().ᜀ(spr_u192F2.ᝅ(), false);
							spr_u192F.\u1756().ᜀ(spr_u192F2.\u1756(), false);
							num = 4;
							continue;
						case 8:
							a_2 = ((XlsShapeFill)spr_u192F.ᝐ()).Clone(spr_u192F);
							num = 12;
							continue;
						case 9:
							if (A_0 == null)
							{
								num = 0;
								continue;
							}
							num = 6;
							continue;
						case 10:
							goto IL_13A;
						case 11:
							spr_u192F = ((AddtionalFormatWrapper)A_0).Wrapped;
							a_3 = (sprỶ)spr_u192F.ᜑ().Clone();
							num = 10;
							continue;
						case 12:
							goto IL_CD;
						case 13:
							if (!(A_0 is AddtionalFormatWrapper))
							{
								goto IL_91;
							}
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
								num = 11;
								continue;
							}
							break;
						}
						break;
						IL_CD:
						spr_u192F2 = spr_u192F;
						spr_u192F = new spr\u192F(base.AppImplementation, this, a_3);
						spr_u192F.ᝄ().ᜀ(spr_u192F2.ᝄ(), false);
						spr_u192F.\u1754().ᜀ(spr_u192F2.\u1754(), false);
						num = 2;
						continue;
						IL_13A:
						num = 5;
					}
				}
				IL_6F:
				throw new ArgumentNullException(RecordTableEnumerator.b("夺尼䰾⑀Ղ⩄㕆⑈⩊㥌", a_));
				IL_91:
				throw new ArgumentException(RecordTableEnumerator.b("爺匼䤾⁀⽂ⱄ⍆楈㽊㑌㽎㑐獒㙔㙖⩘⽚獜", a_));
				IL_1B6:
				IL_265:
				spr_u192F.ᜀ(a_2);
				return spr_u192F;
			}
			}
		}

		// Token: 0x06005C20 RID: 23584 RVA: 0x00394B94 File Offset: 0x00393B94
		internal spr\u192F ᜀ(spr\u192F A_0)
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
			return this.ᜀ(A_0, false);
		}

		// Token: 0x06005C21 RID: 23585 RVA: 0x00394BD8 File Offset: 0x00393BD8
		internal spr\u192F ᜀ(spr\u192F A_0, bool A_1)
		{
			int a_ = 2;
			int num = 3;
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
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						goto IL_B6;
					case 2:
						goto IL_8B;
					case 4:
						goto IL_60;
					case 5:
						if (!A_1)
						{
							num = 2;
							continue;
						}
						num = 1;
						continue;
					}
					if (A_0 == null)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					num = 5;
					continue;
				}
				IL_8B:
				num = 0;
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("帷唹主匽ℿ㙁", a_));
			IL_6A:
			spr\u192F spr_u192F = this.\u1738.ᜁ(A_0);
			goto IL_C4;
			IL_B6:
			spr_u192F = this.\u1738.ᜀ(A_0);
			IL_C4:
			A_0 = spr_u192F;
			return A_0;
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x00394CAC File Offset: 0x00393CAC
		protected internal void CopyToClipboard(XlsWorksheet sheet)
		{
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_9C;
				case 2:
					goto IL_53;
				case 3:
					sheet = (this.\u1734 as XlsWorksheet);
					num = 1;
					continue;
				case 4:
					goto IL_A9;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9C;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 6:
					if (sheet != null)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
				}
				if (sheet == null)
				{
					num = 3;
					continue;
				}
				IL_56:
				num = 6;
				continue;
				IL_9C:
				goto IL_56;
			}
			IL_53:
			IWorksheet worksheet = sheet;
			goto IL_B7;
			IL_A9:
			worksheet = this.Worksheets[0];
			IL_B7:
			IWorksheet a_ = worksheet;
			spr\u214D spr_u214D = base.AppImplementation.ᜀ(a_);
			spr_u214D.ᜄ();
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x00394D84 File Offset: 0x00393D84
		protected internal void Paste()
		{
			int a_ = 11;
			for (;;)
			{
				IDataObject dataObject = Clipboard.GetDataObject();
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (false)
							{
							}
							object data = dataObject.GetData(RecordTableEnumerator.b("̀⩂⍄ⅆ煈", a_));
							num = 4;
							continue;
						}
						}
						break;
					case 2:
					{
						object data;
						sprἛ sprἛ = new sprἛ((Stream)data);
						sprἛ.ᜁ();
						this.ᜀ(sprἛ);
						num = 6;
						continue;
					}
					case 3:
						if (true)
						{
						}
						if (dataObject.GetDataPresent(RecordTableEnumerator.b("̀⩂⍄ⅆ煈", a_), true))
						{
							num = 1;
							continue;
						}
						return;
					case 4:
					{
						object data;
						if (data != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					case 5:
						if (dataObject != null)
						{
							num = 0;
							continue;
						}
						return;
					case 6:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x00394E94 File Offset: 0x00393E94
		protected int InsertSelfSupbook()
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
			return this.ᝢ.InsertSelfSupbook();
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x00394EDC File Offset: 0x00393EDC
		protected internal int AddSheetReference(string sheetName)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				IWorksheet worksheet;
				IWorksheet worksheet2;
				for (;;)
				{
					string[] array = sheetName.Split(new char[]
					{
						':'
					});
					int num = 16;
					for (;;)
					{
						int result;
						Match match;
						string text;
						switch (num)
						{
						case 0:
							result = this.AddBrokenSheetReference();
							num = 9;
							continue;
						case 1:
							if (worksheet != null)
							{
								num = 4;
								continue;
							}
							goto IL_1D4;
						case 2:
						{
							if (true)
							{
							}
							int length;
							if (length >= 2)
							{
								num = 10;
								continue;
							}
							goto IL_139;
						}
						case 3:
						{
							text = match.Groups[RecordTableEnumerator.b("稷唹医唽฿⍁⥃⍅", a_)].Value;
							int length = text.Length;
							result = 0;
							num = 8;
							continue;
						}
						case 4:
							goto IL_134;
						case 5:
							num = 14;
							continue;
						case 6:
							goto IL_139;
						case 7:
							if (match.Success)
							{
								num = 5;
								continue;
							}
							goto IL_295;
						case 8:
						{
							int length;
							if (length == 0)
							{
								num = 0;
								continue;
							}
							num = 2;
							continue;
						}
						case 9:
							return result;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_26C;
							default:
							{
								if (false)
								{
								}
								int length;
								text = text.Substring(1, length - 2);
								num = 6;
								continue;
							}
							}
							break;
						case 11:
							return result;
						case 12:
							if (worksheet2 != null)
							{
								num = 15;
								continue;
							}
							goto IL_1D4;
						case 13:
							goto IL_96;
						case 14:
							goto IL_26C;
						case 15:
							num = 1;
							continue;
						case 16:
						{
							if (array.Length > 2)
							{
								num = 13;
								continue;
							}
							sheetName = array[0];
							string sheetName2 = array[array.Length - 1];
							worksheet2 = this.Worksheets[sheetName];
							worksheet = this.Worksheets[sheetName2];
							num = 12;
							continue;
						}
						}
						break;
						IL_139:
						string value = match.Groups[RecordTableEnumerator.b("欷刹夻嬽㐿ు╃⭅ⵇ", a_)].Value;
						result = this.ᜀ(text, value);
						num = 11;
						continue;
						IL_1D4:
						match = XlsWorkbook.ᜬ.Match(sheetName);
						num = 7;
						continue;
						IL_26C:
						if (!(match.Value == sheetName))
						{
							goto IL_295;
						}
						num = 3;
					}
				}
				IL_96:
				throw new ArgumentException(RecordTableEnumerator.b("䬷刹夻嬽㐿ు╃⭅ⵇ", a_));
				IL_134:
				return this.AddSheetReference(worksheet2, worksheet);
				IL_295:
				throw new ArgumentException(RecordTableEnumerator.b("洷吹圻倽⼿㕁⩃晅㩇⽉⩋⭍≏㝑㩓㕕㵗㹙籛ⵝ࡟ݡţብ䡧ѩ൫ͭᕯ", a_));
			}
			}
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x00395194 File Offset: 0x00394194
		private int ᜀ(string A_0, string A_1)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 1;
				int num2;
				int num3;
				for (;;)
				{
					XlsExternWorkbook xlsExternWorkbook;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 2:
						goto IL_98;
					case 3:
						num = 10;
						continue;
					case 4:
						goto IL_226;
					case 5:
						if (A_0.Length == 0)
						{
							num = 4;
							continue;
						}
						goto IL_270;
					case 6:
						goto IL_270;
					case 7:
						if (A_1 != null)
						{
							num = 8;
							continue;
						}
						goto IL_2C9;
					case 8:
						num2 = xlsExternWorkbook.IndexOf(A_1);
						num = 9;
						continue;
					case 9:
						goto IL_1B4;
					case 10:
						if (this.Loading)
						{
							num = 0;
							continue;
						}
						goto IL_212;
					case 11:
						goto IL_1F2;
					case 12:
						if (this.Version != ExcelVersion.Version2007)
						{
							num = 22;
							continue;
						}
						goto IL_176;
					case 13:
						if (A_0 != null)
						{
							num = 23;
							continue;
						}
						goto IL_226;
					case 14:
					{
						int num4;
						num3 = num4 - 1;
						xlsExternWorkbook = this.ᝢ[num3];
						num = 11;
						continue;
					}
					case 15:
					{
						int num4;
						if (int.TryParse(A_0, out num4))
						{
							num = 14;
							continue;
						}
						goto IL_212;
					}
					case 16:
						goto IL_176;
					case 17:
						if (xlsExternWorkbook == null)
						{
							num = 3;
							continue;
						}
						num3 = xlsExternWorkbook.Index;
						num = 21;
						continue;
					case 18:
						if (A_1 == null)
						{
							num = 20;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_270;
						default:
							if (false)
							{
							}
							num3 = -1;
							num2 = 65534;
							num = 13;
							continue;
						}
						break;
					case 19:
						if (this.Version == ExcelVersion.Version2010)
						{
							num = 16;
							continue;
						}
						goto IL_212;
					case 20:
						goto IL_16C;
					case 21:
						goto IL_1F2;
					case 22:
						num = 19;
						continue;
					case 23:
						num = 5;
						continue;
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num = 18;
					continue;
					IL_176:
					num = 15;
					continue;
					IL_1F2:
					num = 7;
					continue;
					IL_226:
					A_0 = A_1;
					A_1 = null;
					num = 6;
					continue;
					IL_270:
					xlsExternWorkbook = this.ᝢ[A_0];
					num = 17;
				}
				IL_98:
				throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似紾⹀ⱂ⹄ॆ⡈♊⡌", a_));
				IL_16C:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似氾⥀♂⁄㍆݈⩊⁌⩎", a_));
				IL_1B4:
				goto IL_2C9;
				IL_212:
				throw new ArgumentNullException(RecordTableEnumerator.b("稸娺匼ᠾ㕀捂⍄⹆❈⽊浌⩎⥐❒ご╖㝘筚⩜ぞ፠ࡢݤࡦ٨j", a_));
				IL_2C9:
				return this.AddSheetReference(num3, num2, num2);
			}
			}
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x00395474 File Offset: 0x00394474
		protected internal int AddSheetReference(IWorksheet sheet)
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
			return this.AddSheetReference(sheet, sheet);
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x003954B8 File Offset: 0x003944B8
		protected internal int AddSheetReference(IWorksheet sheet, IWorksheet lastSheet)
		{
			int a_ = 7;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_80;
					default:
						goto IL_4C;
					}
					break;
				case 1:
					goto IL_34;
				case 2:
					goto IL_80;
				}
				if (sheet == null)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
				IL_80:
				if (sheet.Workbook == this)
				{
					goto IL_A7;
				}
				num = 0;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("丼圾⑀♂ㅄ", a_));
			IL_4C:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("砼䜾㕀♂㝄⥆⡈❊浌㡎㹐⅒㹔⑖ㅘ㹚㡜⭞በ䍢٤٦ݨ䭪ͬnհ卲᝴ቶ奸ᵺቼ੾ꮄ", a_));
			IL_A7:
			int a_2 = this.InsertSelfSupbook();
			int realIndex = ((spr\u252A)sheet).get_RealIndex();
			int realIndex2 = ((spr\u252A)lastSheet).get_RealIndex();
			return this.\u173E.ᜀ(a_2, realIndex, realIndex);
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x0039559C File Offset: 0x0039459C
		protected internal int AddSheetReference(ITabSheet sheet)
		{
			int a_ = 0;
			int num = 5;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_4E;
				case 1:
					return -1;
				case 2:
					if (!(sheet is IWorksheet))
					{
						num = 1;
						continue;
					}
					goto IL_D1;
				case 3:
					if (sheet.Workbook != this)
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B0;
					default:
						goto IL_80;
					}
					break;
				}
				if (sheet == null)
				{
					num = 0;
					continue;
				}
				IL_B0:
				num = 3;
			}
			IL_4E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
			IL_80:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("猵䀷丹夻䰽⸿⍁⡃晅㽇╉㹋╍⍏㩑ㅓ㍕ⱗ⥙籛㵝şౡ䑣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻偽", a_));
			IL_D1:
			int a_2 = this.InsertSelfSupbook();
			int realIndex = ((spr\u252A)sheet).get_RealIndex();
			return this.\u173E.ᜀ(a_2, realIndex, realIndex);
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x0039569C File Offset: 0x0039469C
		protected internal int AddSheetReference(int supIndex, int firstSheetIndex, int lastSheetIndex)
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
			return this.\u173E.ᜀ(supIndex, firstSheetIndex, lastSheetIndex);
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x003956E8 File Offset: 0x003946E8
		protected internal int AddBrokenSheetReference()
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
			int a_ = this.InsertSelfSupbook();
			return this.\u173E.ᜀ(a_, 65535, 65535);
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x00395740 File Offset: 0x00394740
		protected internal void DecreaseSheetIndex(int index)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					sprᦖ.ᜀ ᜀ;
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						num2 = (int)ᜀ.ᜀ();
						num = 4;
						continue;
					case 1:
						goto IL_322;
					case 2:
						if (num2 != 65535)
						{
							num = 27;
							continue;
						}
						goto IL_186;
					case 3:
						goto IL_1F8;
					case 4:
						if (num2 > index)
						{
							num = 24;
							continue;
						}
						goto IL_186;
					case 6:
						if (num2 > index)
						{
							num = 25;
							continue;
						}
						goto IL_1D6;
					case 7:
						num = 16;
						continue;
					case 8:
						if (num2 == index)
						{
							num = 23;
							continue;
						}
						goto IL_174;
					case 9:
						if (num2 == index)
						{
							num = 13;
							continue;
						}
						goto IL_322;
					case 10:
						goto IL_1F8;
					case 11:
						goto IL_217;
					case 12:
						goto IL_174;
					case 13:
						ᜀ.ᜂ(ushort.MaxValue);
						num = 1;
						continue;
					case 14:
					{
						int num4;
						if (num3 >= num4)
						{
							num = 11;
							continue;
						}
						sprᦖ.ᜀ[] array;
						ᜀ = array[num3];
						num = 19;
						continue;
					}
					case 15:
						if (num2 != 65534)
						{
							num = 28;
							continue;
						}
						goto IL_186;
					case 16:
						if (num2 != 65534)
						{
							num = 18;
							continue;
						}
						goto IL_1D6;
					case 17:
						goto IL_286;
					case 18:
					{
						sprᦖ.ᜀ ᜀ2 = ᜀ;
						ᜀ2.ᜁ(ᜀ2.ᜂ() - 1);
						num = 12;
						continue;
					}
					case 19:
					{
						int firstInternalIndex;
						if ((int)ᜀ.ᜁ() == firstInternalIndex)
						{
							num = 0;
							continue;
						}
						goto IL_174;
					}
					case 20:
						goto IL_174;
					case 21:
						goto IL_322;
					case 22:
						num = 26;
						continue;
					case 23:
						goto IL_133;
					case 24:
						num = 2;
						continue;
					case 25:
						num = 29;
						continue;
					case 26:
						if (this.\u173E.ᜃ() != null)
						{
							int firstInternalIndex = this.ᝢ.GetFirstInternalIndex();
							sprᦖ.ᜀ[] array = this.\u173E.ᜃ();
							num3 = 0;
							int num4 = array.Length;
							num = 10;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_133;
						default:
							if (false)
							{
							}
							num = 17;
							continue;
						}
						break;
					case 27:
						num = 15;
						continue;
					case 28:
					{
						sprᦖ.ᜀ ᜀ3 = ᜀ;
						ᜀ3.ᜂ(ᜀ3.ᜀ() - 1);
						num = 21;
						continue;
					}
					case 29:
						if (num2 != 65535)
						{
							num = 7;
							continue;
						}
						goto IL_1D6;
					}
					if (this.\u173E != null)
					{
						num = 22;
						continue;
					}
					break;
					IL_133:
					ᜀ.ᜁ(ushort.MaxValue);
					num = 20;
					continue;
					IL_174:
					num3++;
					num = 3;
					continue;
					IL_186:
					num = 9;
					continue;
					IL_1D6:
					num = 8;
					continue;
					IL_1F8:
					num = 14;
					continue;
					IL_322:
					num2 = (int)ᜀ.ᜂ();
					num = 6;
				}
				return;
				IL_217:
				if (true)
				{
				}
				return;
				IL_286:
				return;
			}
			}
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x00395AA8 File Offset: 0x00394AA8
		protected internal void IncreaseSheetIndex(int index)
		{
			switch (0)
			{
			default:
			{
				int num = 12;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						sprᦖ.ᜀ ᜀ2;
						sprᦖ.ᜀ ᜀ = ᜀ2;
						ᜀ.ᜁ(ᜀ.ᜂ() + 1);
						num = 7;
						continue;
					}
					case 1:
						goto IL_DF;
					case 2:
					{
						sprᦖ.ᜀ ᜀ2;
						if ((int)ᜀ2.ᜂ() >= index)
						{
							num = 0;
							continue;
						}
						goto IL_A3;
					}
					case 3:
						num = 13;
						continue;
					case 4:
						goto IL_105;
					case 5:
					{
						sprᦖ.ᜀ ᜀ2;
						sprᦖ.ᜀ ᜀ3 = ᜀ2;
						ᜀ3.ᜂ(ᜀ3.ᜀ() + 1);
						num = 1;
						continue;
					}
					case 6:
					{
						sprᦖ.ᜀ ᜀ2;
						if ((int)ᜀ2.ᜀ() >= index)
						{
							num = 5;
							continue;
						}
						goto IL_DF;
					}
					case 7:
						goto IL_A3;
					case 8:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 9;
							continue;
						}
						sprᦖ.ᜀ[] array;
						sprᦖ.ᜀ ᜀ2 = array[num2];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_19F;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					}
					case 9:
						return;
					case 10:
						goto IL_DD;
					case 11:
						goto IL_19F;
					case 13:
					{
						if (this.\u173E.ᜃ() == null)
						{
							num = 10;
							continue;
						}
						sprᦖ.ᜀ[] array = this.\u173E.ᜃ();
						num2 = 0;
						int num3 = array.Length;
						if (true)
						{
						}
						num = 11;
						continue;
					}
					}
					if (this.\u173E != null)
					{
						num = 3;
						continue;
					}
					break;
					IL_A3:
					num2++;
					num = 4;
					continue;
					IL_DF:
					num = 2;
					continue;
					IL_105:
					num = 8;
					continue;
					IL_19F:
					goto IL_105;
				}
				return;
				IL_DD:
				return;
			}
			}
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x00395C5C File Offset: 0x00394C5C
		protected internal void MoveSheetIndex(int iOldIndex, int iNewIndex)
		{
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C7;
						default:
							if (false)
							{
							}
							if (this.\u173E.ᜃ() == null)
							{
								if (true)
								{
								}
								num = 5;
								continue;
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						goto IL_154;
					case 2:
						goto IL_154;
					case 3:
					{
						if (iOldIndex == iNewIndex)
						{
							num = 10;
							continue;
						}
						sprᦖ.ᜀ[] array = this.\u173E.ᜃ();
						num2 = 0;
						int num3 = array.Length;
						num = 1;
						continue;
					}
					case 4:
						goto IL_C7;
					case 5:
						goto IL_12C;
					case 7:
					{
						sprᦖ.ᜀ[] array;
						sprᦖ.ᜀ ᜀ = array[num2];
						ᜀ.ᜂ((ushort)this.ᜀ((int)ᜀ.ᜀ(), iOldIndex, iNewIndex));
						ᜀ.ᜁ((ushort)this.ᜀ((int)ᜀ.ᜂ(), iOldIndex, iNewIndex));
						num = 4;
						continue;
					}
					case 8:
						num = 0;
						continue;
					case 9:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 11;
							continue;
						}
						num = 12;
						continue;
					}
					case 10:
						return;
					case 11:
						return;
					case 12:
						if (this.IsLocalReference(num2))
						{
							num = 7;
							continue;
						}
						goto IL_C7;
					}
					if (this.\u173E != null)
					{
						num = 8;
						continue;
					}
					break;
					IL_C7:
					num2++;
					num = 2;
					continue;
					IL_154:
					num = 9;
				}
				return;
				IL_12C:
				return;
			}
			}
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x00395E00 File Offset: 0x00394E00
		protected internal void UpdateActiveSheetAfterMove(int iOldIndex, int iNewIndex)
		{
			int num;
			for (;;)
			{
				num = this.ActiveSheetIndex;
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 4;
						continue;
					case 1:
						if (num < iOldIndex)
						{
							num2 = 2;
							continue;
						}
						goto IL_155;
					case 2:
						num2 = 11;
						continue;
					case 3:
						num2 = 1;
						continue;
					case 4:
						if (num > iOldIndex)
						{
							num2 = 12;
							continue;
						}
						goto IL_155;
					case 5:
						if (iOldIndex < iNewIndex)
						{
							num2 = 3;
							continue;
						}
						num2 = 13;
						continue;
					case 6:
						goto IL_D6;
					case 7:
						if (iOldIndex == num)
						{
							num2 = 8;
							continue;
						}
						num2 = 5;
						continue;
					case 8:
						if (true)
						{
						}
						num = iNewIndex;
						num2 = 6;
						continue;
					case 9:
						goto IL_C4;
					case 10:
						num++;
						num2 = 14;
						continue;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F2;
						default:
							if (false)
							{
							}
							if (num >= iNewIndex)
							{
								num2 = 10;
								continue;
							}
							goto IL_155;
						}
						break;
					case 12:
						goto IL_F2;
					case 13:
						if (num <= iNewIndex)
						{
							num2 = 0;
							continue;
						}
						goto IL_155;
					case 14:
						goto IL_91;
					}
					break;
					IL_F2:
					num--;
					num2 = 9;
				}
			}
			IL_91:
			IL_C4:
			IL_D6:
			IL_155:
			this.ActiveSheetIndex = num;
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x00395F6C File Offset: 0x00394F6C
		private int ᜀ(int A_0, int A_1, int A_2)
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return A_2;
				case 1:
				{
					int num2;
					if (A_0 >= num2)
					{
						num = 2;
						continue;
					}
					return A_0;
				}
				case 2:
					num = 6;
					continue;
				case 3:
					goto IL_5C;
				case 4:
					goto IL_C5;
				case 5:
					return A_0;
				case 6:
				{
					int num3;
					if (A_0 > num3)
					{
						num = 3;
						continue;
					}
					num = 8;
					continue;
				}
				case 7:
					goto IL_82;
				case 8:
					if (A_1 > A_2)
					{
						num = 4;
						continue;
					}
					goto IL_FD;
				}
				if (A_1 == A_2)
				{
					num = 5;
					continue;
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
					num = 7;
					continue;
				}
				IL_82:
				if (A_0 == A_1)
				{
					num = 0;
				}
				else
				{
					int num2 = Math.Min(A_1, A_2);
					int num3 = Math.Max(A_1, A_2 - 1);
					num = 1;
				}
			}
			return A_0;
			IL_5C:
			return A_0;
			IL_C5:
			if (true)
			{
			}
			return A_0 + 1;
			IL_FD:
			return A_0 - 1;
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x0039607C File Offset: 0x0039507C
		protected internal string GetSheetNameByReference(int reference)
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
			return this.GetSheetNameByReference(reference, true);
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x003960C0 File Offset: 0x003950C0
		protected internal string GetSheetNameByReference(int reference, bool throwArgumentOutOfRange)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				XlsExternWorkbook xlsExternWorkbook;
				sprᦖ.ᜀ ᜀ;
				int num2;
				for (;;)
				{
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E3;
						case 1:
							goto IL_15A;
						case 2:
							if (xlsExternWorkbook.IsInternalReference)
							{
								num = 4;
								continue;
							}
							num = 1;
							continue;
						case 3:
							if (reference < 0)
							{
								num = 0;
								continue;
							}
							ᜀ = this.\u173E.ᜃ()[reference];
							num2 = (int)ᜀ.ᜁ();
							goto IL_BB;
						case 4:
							num = 8;
							continue;
						case 5:
							num = 3;
							continue;
						case 6:
							goto IL_106;
						case 7:
							goto IL_E1;
						case 8:
							goto IL_114;
						case 9:
							if ((int)this.\u173E.ᜅ() > reference)
							{
								num = 5;
								continue;
							}
							goto IL_E3;
						case 10:
							if (num2 > this.ᝢ.Count)
							{
								num = 7;
								continue;
							}
							xlsExternWorkbook = this.ᝢ[num2];
							num = 2;
							continue;
						case 11:
							if (throwArgumentOutOfRange)
							{
								if (true)
								{
								}
								num = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_BB;
							default:
								goto IL_17B;
							}
							break;
						}
						break;
						IL_BB:
						num = 10;
						continue;
						IL_E3:
						num = 11;
					}
				}
				IL_E1:
				throw new spr\u2313();
				IL_106:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䰽┿⑁⅃㑅ⵇ⑉⽋⭍", a_));
				IL_114:
				return this.ᜀ(ᜀ);
				IL_15A:
				return this.ᜀ(xlsExternWorkbook, ᜀ, num2);
				IL_17B:
				if (false)
				{
				}
				return null;
			}
			}
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x00396268 File Offset: 0x00395268
		private string ᜀ(XlsExternWorkbook A_0, sprᦖ.ᜀ A_1, int A_2)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int index;
				string str;
				for (;;)
				{
					index = (int)A_1.ᜀ();
					string text = this.ᜁ(A_0.URL);
					str = null;
					string fileName = Path.GetFileName(A_0.URL);
					int num = 1;
					for (;;)
					{
						int num3;
						switch (num)
						{
						case 0:
						{
							int num2 = 0;
							num3 = A_2 - 1;
							num = 12;
							continue;
						}
						case 1:
							if (this.ᝇ)
							{
								num = 0;
								continue;
							}
							str = string.Concat(new object[]
							{
								text,
								'[',
								fileName,
								']'
							});
							num = 10;
							continue;
						case 2:
							goto IL_B7;
						case 3:
							goto IL_95;
						case 4:
						{
							if (num3 < 0)
							{
								num = 9;
								continue;
							}
							XlsExternWorkbook xlsExternWorkbook = this.ᝢ[num3];
							num = 5;
							continue;
						}
						case 5:
						{
							XlsExternWorkbook xlsExternWorkbook;
							if (!xlsExternWorkbook.IsInternalReference)
							{
								num = 11;
								continue;
							}
							goto IL_194;
						}
						case 6:
							goto IL_188;
						case 7:
							goto IL_194;
						case 8:
						{
							XlsExternWorkbook xlsExternWorkbook;
							if (string.IsNullOrEmpty(xlsExternWorkbook.URL))
							{
								num = 7;
								continue;
							}
							goto IL_B7;
						}
						case 9:
						{
							int num2;
							str = string.Format(RecordTableEnumerator.b("ᩀ㡂畄㩆ᑈ", a_), A_2 - num2 + 1);
							goto IL_17C;
						}
						case 10:
							goto IL_13F;
						case 11:
							num = 8;
							continue;
						case 12:
							goto IL_95;
						}
						break;
						IL_95:
						num = 4;
						continue;
						IL_B7:
						num3--;
						num = 3;
						continue;
						IL_17C:
						num = 6;
						continue;
						IL_194:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17C;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							int num2;
							num2++;
							num = 2;
							break;
						}
						}
					}
				}
				IL_13F:
				IL_188:
				return str + A_0.GetSheetName(index);
			}
			}
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x00396480 File Offset: 0x00395480
		private string ᜀ(sprᦖ.ᜀ A_0)
		{
			int a_ = 6;
			string text;
			for (;;)
			{
				text = null;
				int num = (int)A_0.ᜀ();
				int num2 = 11;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						object obj = this.Objects[(int)A_0.ᜂ()];
						num2 = 6;
						continue;
					}
					case 1:
						goto IL_1DB;
					case 2:
						goto IL_16D;
					case 3:
					{
						object obj;
						text = text + RecordTableEnumerator.b("ػ", a_) + ((IWorksheet)obj).Name;
						num2 = 8;
						continue;
					}
					case 4:
						num2 = 12;
						continue;
					case 5:
						if (this.ObjectCount > num)
						{
							num2 = 4;
							continue;
						}
						goto IL_1B8;
					case 6:
					{
						object obj;
						if (obj is IWorksheet)
						{
							num2 = 3;
							continue;
						}
						return text;
					}
					case 7:
					{
						object obj;
						text = ((IWorksheet)obj).Name;
						num2 = 14;
						continue;
					}
					case 8:
						goto IL_1B6;
					case 9:
						text = RecordTableEnumerator.b("Ἳ氽Կс", a_);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 10:
					{
						object obj;
						if (obj is IWorksheet)
						{
							num2 = 7;
							continue;
						}
						goto IL_73;
					}
					case 11:
						if (num == 65535)
						{
							num2 = 9;
							continue;
						}
						num2 = 5;
						continue;
					case 12:
					{
						if (num < 0)
						{
							num2 = 1;
							continue;
						}
						object obj = this.Objects[num];
						num2 = 10;
						continue;
					}
					case 13:
						if (A_0.ᜀ() != A_0.ᜂ())
						{
							goto IL_9E;
						}
						return text;
					case 14:
						goto IL_73;
					}
					break;
					IL_73:
					if (true)
					{
					}
					num2 = 13;
					continue;
					IL_9E:
					num2 = 0;
				}
			}
			IL_16D:
			IL_1B6:
			return text;
			IL_1B8:
			throw new spr\u2313();
			IL_1DB:
			goto IL_1B8;
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x0039666C File Offset: 0x0039566C
		private string ᜁ(string A_0)
		{
			int a_ = 9;
			int num = 8;
			for (;;)
			{
				string text;
				switch (num)
				{
				case 0:
					if (A_0.StartsWith(RecordTableEnumerator.b("圾㕀㝂㕄絆晈摊", a_)))
					{
						num = 10;
						continue;
					}
					text = Path.GetDirectoryName(A_0);
					num = 1;
					continue;
				case 1:
					if (text != null)
					{
						num = 3;
						continue;
					}
					return text;
				case 2:
					return text;
				case 3:
					if (true)
					{
					}
					num = 4;
					continue;
				case 4:
					if (text.Length > 0)
					{
						num = 5;
						continue;
					}
					return text;
				case 5:
					num = 7;
					continue;
				case 6:
					return text;
				case 7:
					if (text[text.Length - 1] != '\\')
					{
						num = 9;
						continue;
					}
					return text;
				case 9:
					text += '\\';
					num = 2;
					continue;
				case 10:
					goto IL_137;
				case 11:
					goto IL_54;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
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
					text = null;
					num = 0;
					continue;
				}
				IL_137:
				int num2 = A_0.LastIndexOf('/');
				text = A_0.Substring(0, num2 + 1);
				num = 6;
			}
			IL_54:
			return null;
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x003967DC File Offset: 0x003957DC
		protected internal IWorksheet GetSheetByReference(int reference)
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
			return this.GetSheetByReference(reference, true);
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x00396820 File Offset: 0x00395820
		protected internal IWorksheet GetSheetByReference(int reference, bool bThrowExceptions)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num = 13;
				object obj;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 16;
						continue;
					case 1:
						if (bThrowExceptions)
						{
							num = 7;
							continue;
						}
						goto IL_1CF;
					case 2:
						if (bThrowExceptions)
						{
							num = 17;
							continue;
						}
						goto IL_CE;
					case 3:
						goto IL_293;
					case 4:
						if (bThrowExceptions)
						{
							num = 3;
							continue;
						}
						goto IL_1F5;
					case 5:
					{
						int num2;
						if (num2 < 0)
						{
							num = 11;
							continue;
						}
						obj = this.Objects[num2];
						if (true)
						{
						}
						num = 15;
						continue;
					}
					case 6:
						num = 5;
						continue;
					case 7:
						goto IL_1B1;
					case 8:
						goto IL_C9;
					case 9:
					{
						XlsExternWorkbook xlsExternWorkbook;
						if (!xlsExternWorkbook.IsInternalReference)
						{
							num = 12;
							continue;
						}
						sprᦖ.ᜀ ᜀ;
						int num2 = (int)ᜀ.ᜀ();
						num = 22;
						continue;
					}
					case 10:
						if (bThrowExceptions)
						{
							num = 8;
							continue;
						}
						goto IL_22C;
					case 11:
						goto IL_AB;
					case 12:
						num = 2;
						continue;
					case 14:
					{
						int num3;
						if (num3 > this.ᝢ.Count)
						{
							num = 20;
							continue;
						}
						XlsExternWorkbook xlsExternWorkbook = this.ᝢ[num3];
						num = 9;
						continue;
					}
					case 15:
						if (obj is IWorksheet)
						{
							num = 23;
							continue;
						}
						goto IL_170;
					case 16:
					{
						if (reference < 0)
						{
							num = 19;
							continue;
						}
						sprᦖ.ᜀ ᜀ = this.\u173E.ᜃ()[reference];
						int num3 = (int)ᜀ.ᜁ();
						num = 14;
						continue;
					}
					case 17:
						goto IL_270;
					case 18:
						goto IL_18E;
					case 19:
						goto IL_275;
					case 20:
						num = 1;
						continue;
					case 21:
						if (bThrowExceptions)
						{
							num = 18;
							continue;
						}
						goto IL_2E4;
					case 22:
					{
						int num2;
						if (this.ObjectCount <= num2)
						{
							goto IL_AB;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_170;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					}
					case 23:
						goto IL_154;
					}
					if ((int)this.\u173E.ᜅ() > reference)
					{
						num = 0;
						continue;
					}
					goto IL_275;
					IL_AB:
					num = 10;
					continue;
					IL_170:
					num = 21;
					continue;
					IL_275:
					num = 4;
				}
				IL_C9:
				throw new spr\u2313();
				IL_CE:
				return null;
				IL_154:
				return (IWorksheet)obj;
				IL_18E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬻儽㈿⥁㝃⹅ⵇ⽉㡋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ɣ॥ᵧѩ࡫䁭", a_));
				IL_1B1:
				throw new spr\u2313();
				IL_1CF:
				return null;
				IL_1F5:
				return null;
				IL_22C:
				return null;
				IL_270:
				throw new spr\u2313();
				IL_293:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("主嬽☿❁㙃⍅♇⥉⥋", a_));
				IL_2E4:
				return null;
			}
			}
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x00396B14 File Offset: 0x00395B14
		protected internal void CheckForInternalReference(int iRef)
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 > this.ᝢ.Count)
					{
						num = 7;
						continue;
					}
					XlsExternWorkbook xlsExternWorkbook = this.ᝢ[num2];
					num = 6;
					continue;
				}
				case 1:
					goto IL_116;
				case 3:
				{
					if (iRef < 0)
					{
						num = 1;
						continue;
					}
					int num2 = (int)this.\u173E.ᜃ()[iRef].ᜁ();
					sprᦖ.ᜀ ᜀ = this.\u173E.ᜃ()[iRef];
					num = 0;
					continue;
				}
				case 4:
					goto IL_B0;
				case 5:
					num = 3;
					continue;
				case 6:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_116;
					default:
					{
						if (false)
						{
						}
						XlsExternWorkbook xlsExternWorkbook;
						if (!xlsExternWorkbook.IsInternalReference)
						{
							num = 4;
							continue;
						}
						return;
					}
					}
					break;
				case 7:
					goto IL_FA;
				}
				if ((int)this.\u173E.ᜅ() <= iRef)
				{
					goto IL_12C;
				}
				num = 5;
			}
			IL_B0:
			throw new NotSupportedException(RecordTableEnumerator.b("紷䈹䠻嬽㈿ⱁ╃⩅桇⍉≋⩍㕏⩑ㅓ╕硗㭙⹛㭝䁟ౡୣብ䡧ᥩᥫṭoᵱٳɵᵷṹ剻偽", a_));
			IL_FA:
			throw new spr\u2313();
			IL_116:
			IL_12C:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("儷根夻堽", a_));
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x00396C68 File Offset: 0x00395C68
		protected internal bool IsLocalReference(int reference)
		{
			if (true)
			{
			}
			int num = 3;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (num2 > this.ᝢ.Count)
					{
						num = 2;
						continue;
					}
					goto IL_C3;
				case 1:
					goto IL_BF;
				case 2:
					return false;
				case 4:
				{
					if (reference < 0)
					{
						num = 1;
						continue;
					}
					sprᦖ.ᜀ ᜀ = this.\u173E.ᜃ()[reference];
					num2 = (int)ᜀ.ᜁ();
					num = 0;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3E;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				goto IL_30;
				IL_3E:
				num = 5;
				continue;
				IL_30:
				if ((int)this.\u173E.ᜅ() > reference)
				{
					goto IL_3E;
				}
				return false;
			}
			return false;
			IL_BF:
			return false;
			IL_C3:
			return this.ᝢ[num2].IsInternalReference;
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x00396D4C File Offset: 0x00395D4C
		public bool IsExternalReference(int reference)
		{
			int a_ = 12;
			int num = 11;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u173E.ᜅ() >= 0)
					{
						num = 6;
						continue;
					}
					goto IL_143;
				case 1:
					goto IL_129;
				case 2:
					if (num2 >= this.ᝢ.Count)
					{
						goto IL_11E;
					}
					goto IL_180;
				case 3:
					return false;
				case 4:
					if (num2 >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_12F;
				case 5:
					return false;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11E;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 7:
				{
					if ((int)this.\u173E.ᜅ() <= reference)
					{
						num = 8;
						continue;
					}
					if (true)
					{
					}
					sprᦖ.ᜀ ᜀ = this.\u173E.ᜃ()[reference];
					num = 10;
					continue;
				}
				case 8:
					goto IL_17E;
				case 9:
					num = 2;
					continue;
				case 10:
				{
					sprᦖ.ᜀ ᜀ;
					if (ᜀ.ᜀ() == 65535)
					{
						num = 3;
						continue;
					}
					num2 = (int)ᜀ.ᜁ();
					num = 4;
					continue;
				}
				}
				if (reference == 65535)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
				IL_11E:
				num = 1;
			}
			return false;
			IL_129:
			IL_12F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ㅁㅃ㙅⩇╉⍋╍᥏㱑こ㍕⁗", a_));
			IL_143:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ぁ⅃⁅ⵇ㡉≋ⵍ㕏", a_));
			IL_17E:
			goto IL_143;
			IL_180:
			return !this.ᝢ[num2].IsInternalReference;
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x00396EF0 File Offset: 0x00395EF0
		internal void ᜀ(sprṨ A_0)
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
			this.\u173F.Add(A_0);
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x00396F38 File Offset: 0x00395F38
		protected internal int CurrentStyleNumber(string pre)
		{
			switch (0)
			{
			default:
			{
				int num;
				for (;;)
				{
					for (;;)
					{
						num = 0;
						IStyles styles = this.Styles;
						int num2 = 0;
						int count = styles.Count;
						int num3 = 5;
						for (;;)
						{
							switch (num3)
							{
							case 0:
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
									string name;
									int num4;
									string s = name.Substring(num4 + pre.Length, name.Length - pre.Length - num4);
									num3 = 1;
									continue;
								}
								}
								break;
							case 1:
							{
								string s;
								double num5;
								if (double.TryParse(s, NumberStyles.Integer, null, out num5))
								{
									num3 = 2;
									continue;
								}
								goto IL_6D;
							}
							case 2:
							{
								double num5;
								int num6 = (int)num5;
								num3 = 10;
								continue;
							}
							case 3:
								goto IL_113;
							case 4:
							{
								int num4;
								if (num4 >= 0)
								{
									num3 = 0;
									continue;
								}
								goto IL_6D;
							}
							case 5:
								goto IL_F7;
							case 6:
								goto IL_F7;
							case 7:
							{
								if (num2 >= count)
								{
									num3 = 3;
									continue;
								}
								IStyle style = styles[num2];
								string name = style.Name;
								int num4 = name.IndexOf(pre);
								num3 = 4;
								continue;
							}
							case 8:
							{
								int num6;
								num = num6;
								num3 = 9;
								continue;
							}
							case 9:
								goto IL_6D;
							case 10:
							{
								int num6;
								if (num6 > num)
								{
									num3 = 8;
									continue;
								}
								goto IL_6D;
							}
							}
							break;
							IL_6D:
							num2++;
							num3 = 6;
							continue;
							IL_F7:
							num3 = 7;
						}
					}
				}
				IL_113:
				if (true)
				{
				}
				return num;
			}
			}
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x003970C8 File Offset: 0x003960C8
		protected double Sqr(double value)
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
			return value * value;
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x00397108 File Offset: 0x00396108
		protected internal double ColorDistance(Color color1, Color color2)
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
			return (double)(Math.Abs((int)(color1.R - color2.R)) + Math.Abs((int)(color1.G - color2.G)) + Math.Abs((int)(color1.B - color2.B)));
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x00397184 File Offset: 0x00396184
		public void ClearInternalReferences()
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
			this.\u173E.ᜀ(new sprᦖ.ᜀ[0]);
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x003971D0 File Offset: 0x003961D0
		private void \u1713()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ព(this, EventArgs.Empty);
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_6D;
				}
				IL_1C:
				if (this.ព != null)
				{
					num = 0;
					continue;
				}
				break;
				goto IL_1C;
			}
			IL_6D:
			if (true)
			{
			}
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x00397254 File Offset: 0x00396254
		private void ᜀ(string A_0)
		{
			int a_ = 2;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					ReadOnlyFileEventArgs readOnlyFileEventArgs;
					if (readOnlyFileEventArgs.ShouldRewrite)
					{
						num = 1;
						continue;
					}
					return;
				}
				case 1:
					goto IL_D6;
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
						ReadOnlyFileEventArgs readOnlyFileEventArgs = new ReadOnlyFileEventArgs();
						this.ភ(this, readOnlyFileEventArgs);
						num = 0;
						continue;
					}
					}
					break;
				}
				if (this.ភ == null)
				{
					break;
				}
				num = 2;
			}
			throw new ApplicationException(RecordTableEnumerator.b("縷匹倻嬽怿", a_) + A_0 + RecordTableEnumerator.b("ᠷ匹伻ḽ㈿❁╃≅桇╉≋≍⥏繑瑓㕕㥗㑙籛そཟᙡ䑣ѥ൧䩩᭫ᱭὯٱᅳ塵", a_));
			IL_D6:
			if (true)
			{
			}
			FileAttributes fileAttributes = File.GetAttributes(A_0);
			fileAttributes &= ~FileAttributes.ReadOnly;
			File.SetAttributes(A_0, fileAttributes);
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x0039733C File Offset: 0x0039633C
		public IExtendedFormat GetExtFormat(int index)
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
			return this.\u1738.ᜁ(index);
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x00397384 File Offset: 0x00396384
		public void UpdateFormula(IXLSRange sourceRange, IXLSRange destRange)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9E;
					case 2:
						goto IL_45;
					case 3:
						if (destRange == null)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_B4;
					}
					if (sourceRange == null)
					{
						num = 2;
					}
					else
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
							num = 3;
							break;
						}
					}
				}
				IL_45:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤹医䬽㈿⅁⅃ᑅ⥇⑉⭋⭍", a_));
				IL_9E:
				throw new ArgumentNullException(RecordTableEnumerator.b("帹夻䴽㐿၁╃⡅⽇⽉", a_));
				IL_B4:
				XlsRange xlsRange = (XlsRange)sourceRange;
				XlsRange xlsRange2 = (XlsRange)destRange;
				XlsWorksheet innerWorksheet = xlsRange.InnerWorksheet;
				XlsWorksheet innerWorksheet2 = xlsRange2.InnerWorksheet;
				int iSourceIndex = this.AddSheetReference(innerWorksheet);
				int iDestIndex = this.AddSheetReference(innerWorksheet2);
				Rectangle rectSource = Rectangle.FromLTRB(xlsRange.FirstColumn - 1, xlsRange.FirstRow - 1, xlsRange.LastColumn - 1, xlsRange.LastRow - 1);
				Rectangle rectDest = Rectangle.FromLTRB(xlsRange2.FirstColumn - 1, xlsRange2.FirstRow - 1, xlsRange2.LastColumn - 1, xlsRange2.LastRow - 1);
				this.UpdateFormula(iSourceIndex, rectSource, iDestIndex, rectDest);
				return;
			}
			}
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x003974D0 File Offset: 0x003964D0
		public void UpdateFormula(int iSourceIndex, Rectangle rectSource, int iDestIndex, Rectangle rectDest)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int count = this.\u1754.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_48;
						case 1:
							if (num < count)
							{
								XlsWorksheetBase xlsWorksheetBase = this.\u1754[num] as XlsWorksheetBase;
								int iCurIndex = this.AddSheetReference(xlsWorksheetBase);
								xlsWorksheetBase.UpdateFormula(iCurIndex, iSourceIndex, rectSource, iDestIndex, rectDest);
								num++;
								num2 = 0;
								continue;
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
								num2 = 3;
								continue;
							}
							break;
						case 2:
							goto IL_48;
						case 3:
							return;
						}
						break;
						IL_48:
						num2 = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x0039759C File Offset: 0x0039659C
		public int GetReferenceIndex(int iNameBookIndex)
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
			return this.\u173E.ᜀ(iNameBookIndex);
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x003975E4 File Offset: 0x003965E4
		public int GetBookIndex(int referenceIndex)
		{
			int a_ = 5;
			for (;;)
			{
				IL_21:
				sprᦖ.ᜀ[] array = this.\u173E.ᜃ();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_B1;
					case 2:
						if (referenceIndex >= 0)
						{
							num = 0;
							continue;
						}
						goto IL_4D;
					case 3:
						if (referenceIndex > array.Length - 1)
						{
							num = 1;
							continue;
						}
						goto IL_B3;
					}
					goto IL_21;
				}
				IL_4D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_63;
				}
				IL_B1:
				goto IL_4D;
			}
			IL_63:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤺堼夾⑀ㅂ⁄⥆⩈⹊ьⅎ㕐㙒ⵔ", a_), RecordTableEnumerator.b("洺尼匾㑀♂敄⑆⡈╊⍌⁎═獒㝔㉖祘㝚㡜ⱞበ䍢ᅤསࡨժ䵬彮兰ቲ᭴፶奸ᱺོ᩾ꦈﾊﾐ뎒즚咽튠趢좦\udca8얪\ud9ac辮鲰鎲蒴", a_));
			IL_B3:
			return (int)this.\u173E.ᜃ()[referenceIndex].ᜁ();
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x003976B8 File Offset: 0x003966B8
		internal XlsExternWorksheet ᜄ(int A_0)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				XlsExternWorksheet result;
				for (;;)
				{
					if (true)
					{
					}
					sprᦖ.ᜀ[] array = this.\u173E.ᜃ();
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_71;
						case 1:
							goto IL_118;
						case 2:
						{
							if (A_0 > array.Length - 1)
							{
								num = 1;
								continue;
							}
							result = null;
							sprᦖ.ᜀ ᜀ = this.\u173E.ᜃ()[A_0];
							num = 7;
							continue;
						}
						case 3:
						{
							sprᦖ.ᜀ ᜀ;
							int index = (int)ᜀ.ᜁ();
							int num2;
							result = this.ᝢ[index].Worksheets.Values[num2];
							num = 8;
							continue;
						}
						case 4:
						{
							int num2;
							if (num2 != 65535)
							{
								num = 3;
								continue;
							}
							return result;
						}
						case 5:
							if (A_0 >= 0)
							{
								num = 0;
								continue;
							}
							goto IL_14E;
						case 6:
							num = 4;
							continue;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_71;
							default:
							{
								if (false)
								{
								}
								sprᦖ.ᜀ ᜀ;
								int num2;
								if ((num2 = (int)ᜀ.ᜀ()) == (int)ᜀ.ᜂ())
								{
									num = 6;
									continue;
								}
								return result;
							}
							}
							break;
						case 8:
							goto IL_14C;
						}
						break;
						IL_71:
						num = 2;
					}
				}
				IL_118:
				goto IL_14E;
				IL_14C:
				return result;
				IL_14E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㭈⹊⭌⩎⍐㙒㭔㑖㱘ቚ㍜㭞Ѡ᭢", a_), RecordTableEnumerator.b("Ὀ⩊⅌㩎㑐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ը๪Ṭᱮ兰ݲᵴᙶ᝸孺䵼彾Ꞇ力랖ﲜ膠슢힤햦ﮨ캪쮬\udcae龰\udab4슶ힸ쾺鶼銾", a_));
			}
			}
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x00397838 File Offset: 0x00396838
		public string DecodeName(string name)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 29;
				StringBuilder stringBuilder;
				for (;;)
				{
					char c;
					int num2;
					char value;
					char value2;
					char c2;
					char c3;
					switch (num)
					{
					case 0:
						switch (c)
						{
						case '\u0001':
							num2++;
							num = 10;
							continue;
						case '\u0002':
							stringBuilder.Append(value);
							stringBuilder.Append(Path.VolumeSeparatorChar);
							stringBuilder.Append(value2);
							num = 11;
							continue;
						case '\u0003':
							stringBuilder.Append(value2);
							num = 1;
							continue;
						case '\u0004':
							goto IL_1B5;
						case '\u0005':
						{
							int num3 = (int)name[num2 + 1];
							stringBuilder.Append(name.Substring(num2 + 2, num3));
							num2 += num3;
							num = 25;
							continue;
						}
						case '\u0006':
							stringBuilder.Append('\\');
							num = 27;
							continue;
						case '\a':
							stringBuilder.Append(name[num2]);
							num = 3;
							continue;
						case '\b':
							stringBuilder.Append(name[num2]);
							num = 5;
							continue;
						default:
							num = 31;
							continue;
						}
						break;
					case 1:
						goto IL_446;
					case 2:
						goto IL_D2;
					case 3:
						if (true)
						{
						}
						goto IL_446;
					case 4:
						goto IL_1B0;
					case 5:
						goto IL_446;
					case 6:
						c2 = XlsWorkbook.\u1712();
						goto IL_317;
					case 7:
						if (c3 == '\u0002')
						{
							num = 34;
							continue;
						}
						num = 19;
						continue;
					case 8:
						goto IL_446;
					case 9:
						num = 6;
						continue;
					case 10:
						if (name[num2] != '@')
						{
							num = 15;
							continue;
						}
						stringBuilder.Append(RecordTableEnumerator.b("ᑇᙉ", a_));
						num = 8;
						continue;
					case 11:
						goto IL_446;
					case 12:
						if (stringBuilder.Length == 5)
						{
							num = 24;
							continue;
						}
						goto IL_2CE;
					case 13:
						goto IL_446;
					case 14:
						goto IL_462;
					case 15:
						stringBuilder.Append(name[num2]);
						stringBuilder.Append(Path.VolumeSeparatorChar);
						stringBuilder.Append(value2);
						num = 13;
						continue;
					case 16:
					{
						if (c3 != '\u0001')
						{
							num = 35;
							continue;
						}
						stringBuilder = new StringBuilder();
						int length = name.Length;
						num2 = 1;
						num = 32;
						continue;
					}
					case 17:
					{
						int length;
						if (num2 >= length)
						{
							num = 30;
							continue;
						}
						goto IL_3A6;
					}
					case 18:
						if (name.Length == 0)
						{
							num = 20;
							continue;
						}
						c3 = name[0];
						num = 7;
						continue;
					case 19:
						if (c3 == '\0')
						{
							num = 4;
							continue;
						}
						num = 16;
						continue;
					case 20:
						goto IL_4A6;
					case 21:
						value2 = '/';
						num = 23;
						continue;
					case 22:
						c2 = this.ᝀ[0];
						goto IL_317;
					case 23:
						goto IL_2CE;
					case 24:
						num = 28;
						continue;
					case 25:
						goto IL_446;
					case 26:
						stringBuilder.Append(name[num2]);
						num = 36;
						continue;
					case 27:
						goto IL_446;
					case 28:
						if (stringBuilder.ToString() == RecordTableEnumerator.b("⁇㹉㡋㹍橏", a_))
						{
							num = 21;
							continue;
						}
						goto IL_2CE;
					case 30:
						goto IL_481;
					case 31:
						num = 26;
						continue;
					case 32:
						if (this.ᝀ == null)
						{
							num = 9;
							continue;
						}
						num = 22;
						continue;
					case 33:
						goto IL_462;
					case 34:
						return name;
					case 35:
						goto IL_193;
					case 36:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3A6;
						default:
							if (false)
							{
							}
							goto IL_446;
						}
						break;
					}
					if (name == null)
					{
						num = 2;
						continue;
					}
					num = 18;
					continue;
					IL_2CE:
					c = c3;
					num = 0;
					continue;
					IL_317:
					value = c2;
					value2 = Path.DirectorySeparatorChar;
					num = 33;
					continue;
					IL_3A6:
					c3 = name[num2];
					num = 12;
					continue;
					IL_446:
					num2++;
					num = 14;
					continue;
					IL_462:
					num = 17;
				}
				IL_D2:
				throw new ArgumentNullException(RecordTableEnumerator.b("♇⭉⅋⭍", a_));
				IL_193:
				return name.Replace('\u0003', '|');
				IL_1B0:
				return string.Empty;
				IL_1B5:
				throw new NotImplementedException();
				IL_481:
				return stringBuilder.ToString();
				IL_4A6:
				throw new ArgumentException(RecordTableEnumerator.b("ه⭉⅋⭍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣ͥէᩩᡫ᝭", a_));
			}
			}
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x00397D2C File Offset: 0x00396D2C
		private static char \u1712()
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
			return Environment.CurrentDirectory[0];
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x00397D74 File Offset: 0x00396D74
		public string EncodeName(string strName)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 1;
				StringBuilder stringBuilder;
				for (;;)
				{
					IL_25:
					int num2;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						num2 = strName.Length;
						goto IL_286;
					case 2:
						num = 11;
						continue;
					case 3:
					{
						int num3 = strName.Length;
						int startIndex;
						strName = strName.Substring(startIndex);
						string[] array = strName.Split(new char[]
						{
							Path.AltDirectorySeparatorChar,
							Path.DirectorySeparatorChar
						});
						num3 = array.Length;
						num4 = 0;
						num = 30;
						continue;
					}
					case 4:
						goto IL_35A;
					case 5:
					{
						stringBuilder.Append('\u0001');
						char value = strName[0];
						stringBuilder.Append(value);
						int startIndex = 3;
						num = 4;
						continue;
					}
					case 6:
						if (true)
						{
						}
						num = 8;
						continue;
					case 7:
						num = 34;
						continue;
					case 8:
						num2 = 0;
						goto IL_286;
					case 9:
						goto IL_2BB;
					case 10:
						num = 19;
						continue;
					case 11:
						if (strName.Length == 0)
						{
							num = 24;
							continue;
						}
						num = 26;
						continue;
					case 12:
						if (strName.StartsWith(RecordTableEnumerator.b("ᡃᩅ", a_)))
						{
							num = 14;
							continue;
						}
						num = 16;
						continue;
					case 13:
						goto IL_303;
					case 14:
					{
						stringBuilder.Append('\u0001');
						stringBuilder.Append('@');
						int startIndex = RecordTableEnumerator.b("ᡃᩅ", a_).Length;
						num = 15;
						continue;
					}
					case 15:
						goto IL_35A;
					case 16:
					{
						bool flag;
						if (flag)
						{
							num = 31;
							continue;
						}
						num = 21;
						continue;
					}
					case 17:
						goto IL_44A;
					case 18:
						stringBuilder.Append('\u0003');
						num = 9;
						continue;
					case 19:
						if (strName[2] == '\\')
						{
							num = 5;
							continue;
						}
						goto IL_44C;
					case 20:
					{
						int num3;
						if (num4 >= num3)
						{
							num = 17;
							continue;
						}
						string[] array;
						stringBuilder.Append(array[num4]);
						num = 22;
						continue;
					}
					case 21:
						if (num5 > 2)
						{
							num = 10;
							continue;
						}
						goto IL_44C;
					case 22:
					{
						int num3;
						if (num4 != num3 - 1)
						{
							num = 18;
							continue;
						}
						goto IL_2BB;
					}
					case 23:
						if (strName == null)
						{
							num = 6;
							continue;
						}
						num = 0;
						continue;
					case 24:
						goto IL_1EB;
					case 25:
						goto IL_35A;
					case 26:
						if (strName.IndexOfAny(XlsWorkbook.ᜮ) == -1)
						{
							num = 7;
							continue;
						}
						goto IL_199;
					case 27:
						stringBuilder.Append('\u0006');
						strName = UtilityMethods.ᜀ(strName);
						num = 29;
						continue;
					case 28:
						if (strName[0] == '\\')
						{
							num = 27;
							continue;
						}
						goto IL_35A;
					case 29:
						goto IL_35A;
					case 30:
						goto IL_429;
					case 31:
						stringBuilder.Append('\u0005');
						stringBuilder.Append((char)strName.Length);
						stringBuilder.Append(strName);
						num = 25;
						continue;
					case 32:
						goto IL_429;
					case 33:
					{
						bool flag;
						if (!flag)
						{
							num = 3;
							continue;
						}
						goto IL_477;
					}
					case 34:
						while (!(strName == RecordTableEnumerator.b("摃", a_)))
						{
							stringBuilder = new StringBuilder();
							stringBuilder.Append('\u0001');
							bool flag = strName.StartsWith(RecordTableEnumerator.b("ⱃ㉅㱇㩉癋", a_));
							int startIndex = 0;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 23;
								goto IL_25;
							}
						}
						num = 13;
						continue;
					}
					if (strName != null)
					{
						num = 2;
						continue;
					}
					return strName;
					IL_286:
					num5 = num2;
					num = 12;
					continue;
					IL_2BB:
					num4++;
					num = 32;
					continue;
					IL_35A:
					num = 33;
					continue;
					IL_429:
					num = 20;
					continue;
					IL_44C:
					num = 28;
				}
				IL_199:
				return strName.Replace('|', '\u0003');
				IL_1EB:
				return strName;
				IL_303:
				goto IL_199;
				IL_44A:
				IL_477:
				return stringBuilder.ToString();
			}
			}
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x00398200 File Offset: 0x00397200
		internal bool ᜀ(BiffRecordRaw A_0)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 3;
				spr\u2267 spr_u;
				for (;;)
				{
					TBIFFRecord typeCode;
					switch (num)
					{
					case 0:
						if (typeCode != TBIFFRecord.ChartFontx)
						{
							num = 11;
							continue;
						}
						if (true)
						{
						}
						((spr\u2241)A_0).ᜀ(0);
						num = 9;
						continue;
					case 1:
						goto IL_8E;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_83;
						}
						break;
					case 4:
						goto IL_1AF;
					case 5:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 13;
							continue;
						}
						sprᜰ.ᜀ[] array;
						array[num2].ᜁ(0);
						num2++;
						num = 8;
						continue;
					}
					case 6:
						goto IL_127;
					case 7:
					{
						if (typeCode != TBIFFRecord.ChartAlruns)
						{
							num = 10;
							continue;
						}
						sprᜰ sprᜰ = (sprᜰ)A_0;
						sprᜰ.ᜀ[] array = sprᜰ.ᜀ();
						int num2 = 0;
						int num3 = array.Length;
						num = 1;
						continue;
					}
					case 8:
						goto IL_8E;
					case 9:
						goto IL_18C;
					case 10:
						num = 12;
						continue;
					case 11:
						num = 7;
						continue;
					case 12:
						if (typeCode == TBIFFRecord.ChartFbi)
						{
							num = 4;
							continue;
						}
						return true;
					case 13:
						num = 6;
						continue;
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					XlsFont xlsFont = (XlsFont)this.InnerFonts[0];
					spr_u = xlsFont.Record;
					typeCode = A_0.TypeCode;
					num = 0;
					continue;
					IL_8E:
					num = 5;
				}
				IL_83:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹弻儽㈿♁", a_));
				IL_127:
				IL_18C:
				return true;
				IL_1AF:
				spr\u1F17 spr_u1F = (spr\u1F17)A_0;
				spr_u1F.ᜃ(0);
				spr_u1F.ᜄ(spr_u.ᜏ());
				return false;
			}
			}
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x00398404 File Offset: 0x00397404
		internal void ᜀ(BiffRecordRaw[] A_0)
		{
			int a_ = 1;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_17:
					int num = 10;
					for (;;)
					{
						spr\u2267 spr_u;
						int num4;
						int num5;
						switch (num)
						{
						case 0:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ChartFontx)
							{
								num = 17;
								continue;
							}
							BiffRecordRaw biffRecordRaw;
							((spr\u2241)biffRecordRaw).ᜀ(0);
							num = 5;
							continue;
						}
						case 1:
							num = 14;
							continue;
						case 2:
						{
							BiffRecordRaw biffRecordRaw;
							spr\u1F17 spr_u1F = (spr\u1F17)biffRecordRaw;
							spr_u1F.ᜃ(0);
							spr_u1F.ᜄ(spr_u.ᜏ());
							num = 16;
							continue;
						}
						case 3:
						{
							TBIFFRecord typeCode;
							if (typeCode != TBIFFRecord.ChartAlruns)
							{
								num = 1;
								continue;
							}
							BiffRecordRaw biffRecordRaw;
							sprᜰ sprᜰ = (sprᜰ)biffRecordRaw;
							sprᜰ.ᜀ[] array = sprᜰ.ᜀ();
							int num2 = 0;
							int num3 = array.Length;
							num = 6;
							continue;
						}
						case 4:
							goto IL_15C;
						case 5:
							goto IL_F4;
						case 6:
							goto IL_136;
						case 7:
							goto IL_84;
						case 8:
							num = 9;
							continue;
						case 9:
							goto IL_F4;
						case 11:
						{
							if (num4 >= num5)
							{
								num = 18;
								continue;
							}
							BiffRecordRaw biffRecordRaw = A_0[num4];
							TBIFFRecord typeCode = biffRecordRaw.TypeCode;
							num = 0;
							continue;
						}
						case 12:
						{
							int num2;
							int num3;
							if (num2 >= num3)
							{
								num = 8;
								continue;
							}
							sprᜰ.ᜀ[] array;
							array[num2].ᜁ(0);
							num2++;
							num = 13;
							continue;
						}
						case 13:
							goto IL_136;
						case 14:
						{
							TBIFFRecord typeCode;
							if (typeCode == TBIFFRecord.ChartFbi)
							{
								num = 2;
								continue;
							}
							goto IL_F4;
						}
						case 15:
							goto IL_15C;
						case 16:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								goto IL_F4;
							}
							break;
						case 17:
							if (true)
							{
							}
							num = 3;
							continue;
						case 18:
							return;
						}
						if (A_0 == null)
						{
							num = 7;
							continue;
						}
						XlsFont xlsFont = (XlsFont)this.\u1737[0];
						spr_u = xlsFont.Record;
						num4 = 0;
						num5 = A_0.Length;
						num = 4;
						continue;
						IL_F4:
						num4++;
						num = 15;
						continue;
						IL_136:
						num = 12;
						continue;
						IL_15C:
						num = 11;
					}
				}
				IL_84:
				throw new ArgumentNullException(RecordTableEnumerator.b("䔶尸堺刼䴾╀あ", a_));
			}
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x00398694 File Offset: 0x00397694
		private bool ᜀ(Color A_0, Color A_1)
		{
			int num = 3;
			for (;;)
			{
				IL_0A:
				switch (num)
				{
				case 0:
					if (A_0.G == A_1.G)
					{
						num = 1;
						continue;
					}
					return false;
				case 1:
					goto IL_9C;
				case 2:
					if (true)
					{
					}
					num = 0;
					continue;
				}
				while (A_0.R == A_1.R)
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
				return false;
			}
			IL_9C:
			return A_0.B == A_1.B;
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x00398740 File Offset: 0x00397740
		public void RemoveExtenededFormatIndex(int xfIndex)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_27:
					Dictionary<int, int> dictFormats = this.\u1738.ᜀ(xfIndex);
					int num = 0;
					int count = this.\u1754.Count;
					for (;;)
					{
						IL_42:
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								XlsWorksheetBase xlsWorksheetBase = this.\u1754[num] as XlsWorksheetBase;
								xlsWorksheetBase.UpdateExtendedFormatIndex(dictFormats);
								num++;
								num2 = 1;
								continue;
							}
							case 1:
								goto IL_55;
							case 2:
								if (true)
								{
								}
								goto IL_55;
							case 3:
								goto IL_94;
							}
							goto IL_27;
							IL_55:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_42;
							}
							if (false)
							{
							}
							num2 = 0;
						}
					}
				}
				IL_94:
				this.\u1736.UpdateStyleRecords();
				return;
			}
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x0039881C File Offset: 0x0039781C
		private void ᜑ()
		{
			switch (0)
			{
			default:
			{
				int[] array2;
				for (;;)
				{
					int num = (int)this.\u173E.ᜅ();
					bool[] array = new bool[num];
					int num2 = 0;
					int count = this.\u1754.Count;
					int num3 = 8;
					for (;;)
					{
						if (true)
						{
						}
						int num4;
						switch (num3)
						{
						case 0:
							goto IL_DB;
						case 1:
							goto IL_12A;
						case 2:
						{
							int num5;
							array2[num4] = num4 - num5;
							num3 = 1;
							continue;
						}
						case 3:
						{
							this.\u1752.ᜀ(array);
							array2 = new int[num];
							int num5 = 0;
							num4 = 0;
							num3 = 10;
							continue;
						}
						case 4:
							goto IL_12A;
						case 5:
							if (!array[num4])
							{
								array2[num4] = -1;
								int num5;
								num5++;
								num3 = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_11C;
							default:
								if (false)
								{
								}
								num3 = 2;
								continue;
							}
							break;
						case 6:
							goto IL_BA;
						case 7:
							goto IL_F8;
						case 8:
							goto IL_BA;
						case 9:
						{
							if (num2 >= count)
							{
								num3 = 3;
								continue;
							}
							XlsWorksheetBase xlsWorksheetBase = this.\u1754[num2] as XlsWorksheetBase;
							xlsWorksheetBase.MarkUsedReferences(array);
							num2++;
							goto IL_11C;
						}
						case 10:
							goto IL_DB;
						case 11:
							if (num4 >= num)
							{
								num3 = 7;
								continue;
							}
							num3 = 5;
							continue;
						}
						break;
						IL_BA:
						num3 = 9;
						continue;
						IL_DB:
						num3 = 11;
						continue;
						IL_11C:
						num3 = 6;
						continue;
						IL_12A:
						num4++;
						num3 = 0;
					}
				}
				IL_F8:
				this.ᜁ(array2);
				return;
			}
			}
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x003989DC File Offset: 0x003979DC
		private void ᜁ(int[] A_0)
		{
			switch (0)
			{
			default:
			{
				List<sprᦖ.ᜀ> list;
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int count = this.\u1754.Count;
					int num2 = 8;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
						{
							this.\u1752.ᜂ(A_0);
							sprᦖ.ᜀ[] array = this.\u173E.ᜃ();
							list = new List<sprᦖ.ᜀ>();
							num3 = 0;
							int num4 = array.Length;
							num2 = 10;
							continue;
						}
						case 1:
							goto IL_9E;
						case 2:
						{
							sprᦖ.ᜀ[] array;
							list.Add(array[num3]);
							num2 = 5;
							continue;
						}
						case 3:
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							goto IL_FE;
						case 4:
							if (A_0[num3] >= 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_12C;
						case 5:
							goto IL_12C;
						case 6:
							goto IL_BF;
						case 7:
							goto IL_F9;
						case 8:
							goto IL_9E;
						case 9:
						{
							int num4;
							if (num3 >= num4)
							{
								num2 = 7;
								continue;
							}
							num2 = 4;
							continue;
						}
						case 10:
							goto IL_BF;
						}
						break;
						IL_9E:
						num2 = 3;
						continue;
						IL_BF:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
						{
							IL_FE:
							XlsWorksheetBase xlsWorksheetBase = this.\u1754[num] as XlsWorksheetBase;
							xlsWorksheetBase.UpdateReferenceIndexes(A_0);
							num++;
							num2 = 1;
							continue;
						}
						default:
							if (false)
							{
							}
							num2 = 9;
							continue;
						}
						IL_12C:
						num3++;
						num2 = 6;
					}
				}
				IL_F9:
				this.\u173E.ᜀ(list.ToArray());
				return;
			}
			}
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x00398B80 File Offset: 0x00397B80
		internal void ᜀ(XlsWorksheet A_0, int A_1, int A_2, bool A_3, bool A_4)
		{
			int num = 0;
			for (;;)
			{
				IEnumerator<XlsPivotCache> enumerator;
				switch (num)
				{
				case 1:
					num = 2;
					continue;
				case 2:
					if (this.ᝦ.Count > 0)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					try
					{
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 1:
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								XlsPivotCache xlsPivotCache = enumerator.Current;
								xlsPivotCache.ᜀ(A_0, A_1, A_2, A_3, A_4);
								goto IL_76;
							}
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_76;
								default:
									if (false)
									{
									}
									num = 4;
									continue;
								}
								break;
							case 4:
								goto IL_C6;
							}
							goto IL_60;
							IL_76:
							num = 0;
							continue;
							IL_80:
							if (true)
							{
							}
							num = 1;
							continue;
							IL_60:
							goto IL_80;
						}
						IL_C6:
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								enumerator.Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_102;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 0;
						}
						IL_102:;
					}
					goto IL_105;
				case 4:
					goto IL_105;
				}
				if (this.ᝦ != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_105:
				enumerator = this.ᝦ.GetEnumerator();
				num = 3;
			}
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x00398CF0 File Offset: 0x00397CF0
		static XlsWorkbook()
		{
			int a_ = 19;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			XlsWorkbook.ᜨ = new Color[]
			{
				spr\u1D39.ᜀ,
				spr\u1D39.ᜁ,
				spr\u1D39.ᜃ,
				Color.FromArgb(255, 0, 255, 0),
				spr\u1D39.ᜄ,
				spr\u1D39.ᜆ,
				spr\u1D39.ᜈ,
				spr\u1D39.ᜇ,
				Color.FromArgb(255, 0, 0, 0),
				Color.FromArgb(255, 255, 255, 255),
				Color.FromArgb(255, 255, 0, 0),
				Color.FromArgb(255, 0, 255, 0),
				Color.FromArgb(255, 0, 0, 255),
				Color.FromArgb(255, 255, 255, 0),
				Color.FromArgb(255, 255, 0, 255),
				Color.FromArgb(255, 0, 255, 255),
				Color.FromArgb(255, 128, 0, 0),
				Color.FromArgb(255, 0, 128, 0),
				Color.FromArgb(255, 0, 0, 128),
				Color.FromArgb(255, 128, 128, 0),
				Color.FromArgb(255, 128, 0, 128),
				Color.FromArgb(255, 0, 128, 128),
				Color.FromArgb(255, 192, 192, 192),
				Color.FromArgb(255, 128, 128, 128),
				Color.FromArgb(255, 153, 153, 255),
				Color.FromArgb(255, 153, 51, 102),
				Color.FromArgb(255, 255, 255, 204),
				Color.FromArgb(255, 204, 255, 255),
				Color.FromArgb(255, 102, 0, 102),
				Color.FromArgb(255, 255, 128, 128),
				Color.FromArgb(255, 0, 102, 204),
				Color.FromArgb(255, 204, 204, 255),
				Color.FromArgb(255, 0, 0, 128),
				Color.FromArgb(255, 255, 0, 255),
				Color.FromArgb(255, 255, 255, 0),
				Color.FromArgb(255, 0, 255, 255),
				Color.FromArgb(255, 128, 0, 128),
				Color.FromArgb(255, 128, 0, 0),
				Color.FromArgb(255, 0, 128, 128),
				Color.FromArgb(255, 0, 0, 255),
				Color.FromArgb(255, 0, 204, 255),
				Color.FromArgb(255, 204, 255, 255),
				Color.FromArgb(255, 204, 255, 204),
				Color.FromArgb(255, 255, 255, 153),
				Color.FromArgb(255, 153, 204, 255),
				Color.FromArgb(255, 255, 153, 204),
				Color.FromArgb(255, 204, 153, 255),
				Color.FromArgb(255, 255, 204, 153),
				Color.FromArgb(255, 51, 102, 255),
				Color.FromArgb(255, 51, 204, 204),
				Color.FromArgb(255, 153, 204, 0),
				Color.FromArgb(255, 255, 204, 0),
				Color.FromArgb(255, 255, 153, 0),
				Color.FromArgb(255, 255, 102, 0),
				Color.FromArgb(255, 102, 102, 153),
				Color.FromArgb(255, 150, 150, 150),
				Color.FromArgb(255, 0, 51, 102),
				Color.FromArgb(255, 51, 153, 102),
				Color.FromArgb(255, 0, 51, 0),
				Color.FromArgb(255, 51, 51, 0),
				Color.FromArgb(255, 153, 51, 0),
				Color.FromArgb(255, 153, 51, 102),
				Color.FromArgb(255, 51, 51, 153),
				Color.FromArgb(255, 51, 51, 51)
			};
			XlsWorkbook.ᜩ = new double[]
			{
				-0.0499893185216834,
				-0.249977111117893,
				-0.1499984740745262,
				-0.3499862666707358,
				-0.499984740745262,
				0.3499862666707358,
				0.499984740745262,
				0.249977111117893,
				0.1499984740745262,
				0.0499893185216834,
				0.7999816888943144,
				0.5999938962981048,
				0.3999755851924192,
				-0.0999786370433668,
				-0.749992370372631,
				-0.8999908444471572
			};
			XlsWorkbook.ᜪ = new Color[][]
			{
				new Color[]
				{
					Color.FromArgb(255, 242, 242, 242),
					Color.FromArgb(255, 191, 191, 191),
					Color.FromArgb(255, 217, 217, 217),
					Color.FromArgb(255, 166, 166, 166),
					Color.FromArgb(255, 128, 128, 128)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 89, 89, 89),
					Color.FromArgb(255, 128, 128, 128),
					Color.FromArgb(255, 64, 64, 64),
					Color.FromArgb(255, 38, 38, 38),
					Color.FromArgb(255, 13, 13, 13)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 196, 189, 151),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 148, 138, 84),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 221, 217, 196),
					Color.FromArgb(255, 73, 69, 41),
					Color.FromArgb(255, 29, 27, 16)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 22, 54, 92),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 15, 36, 62),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 197, 217, 241),
					Color.FromArgb(255, 141, 180, 226),
					Color.FromArgb(255, 83, 141, 213)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 54, 96, 146),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 36, 64, 98),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 220, 230, 241),
					Color.FromArgb(255, 184, 204, 228),
					Color.FromArgb(255, 149, 179, 215)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 150, 54, 52),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 99, 37, 35),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 242, 220, 219),
					Color.FromArgb(255, 230, 184, 183),
					Color.FromArgb(255, 218, 150, 148)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 118, 147, 60),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 79, 98, 40),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 235, 241, 222),
					Color.FromArgb(255, 216, 228, 188),
					Color.FromArgb(255, 196, 215, 155)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 96, 73, 122),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 64, 49, 81),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 228, 223, 236),
					Color.FromArgb(255, 204, 192, 218),
					Color.FromArgb(255, 177, 160, 199)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 49, 134, 155),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 33, 89, 103),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 218, 238, 243),
					Color.FromArgb(255, 183, 222, 232),
					Color.FromArgb(255, 146, 205, 220)
				},
				new Color[]
				{
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 226, 107, 10),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 151, 71, 6),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(0, 0, 0, 0),
					Color.FromArgb(255, 253, 233, 217),
					Color.FromArgb(255, 252, 213, 180),
					Color.FromArgb(255, 250, 191, 143)
				}
			};
			XlsWorkbook.ᜫ = new TBIFFRecord[]
			{
				TBIFFRecord.StreamId,
				TBIFFRecord.PivotViewSource,
				TBIFFRecord.DCONRef,
				TBIFFRecord.DCONBIN,
				TBIFFRecord.DCONNAME,
				TBIFFRecord.DCON,
				TBIFFRecord.PivotViewAdditionalInfo,
				TBIFFRecord.ExternalSourceInfo
			};
			XlsWorkbook.ᜬ = new Regex(RecordTableEnumerator.b("慈瑊煌ൎ㹐㱒㹔ᥖ㡘㙚㡜慞㵠㡢㹤㭦㩨㕪䵬䡮Ɒ塲⥴⩶偸䑺啼䁾부킂ﾊ쎌ﲐꮔ첖얘좚붜슞誠誢", a_), RegexOptions.Compiled);
			XlsWorkbook.ᜭ = new string[]
			{
				RecordTableEnumerator.b("䱈ᡊ㡌≎㱐㉒❔⹖ၘ㕚㭜ぞ፠๢Ѥ፦hѪͬ", a_),
				RecordTableEnumerator.b("䱈ཊ≌ⱎ⑐㹒ご㥖ⵘ࡚⡜㉞ౠɢᝤṦ⁨ժ୬nͰṲᑴͶၸᑺ፼", a_)
			};
			XlsWorkbook.ᜮ = new char[]
			{
				'\u0001',
				'\u0002',
				'\u0003',
				'\u0004',
				'\u0005',
				'\u0006',
				'\a',
				'\b',
				'|'
			};
			XlsWorkbook.ᜯ = new int[]
			{
				0,
				3,
				4,
				5,
				6,
				7
			};
			XlsWorkbook.ᜰ = new int[]
			{
				0,
				16,
				18,
				20,
				17,
				19
			};
			XlsWorkbook.ᜱ = new Color[]
			{
				SystemColors.Window,
				SystemColors.WindowText,
				spr\u1D39.ᜀ(15658209),
				spr\u1D39.ᜀ(2050429),
				spr\u1D39.ᜀ(5210557),
				spr\u1D39.ᜀ(12603469),
				spr\u1D39.ᜀ(10206041),
				spr\u1D39.ᜀ(8414370),
				spr\u1D39.ᜀ(4959430),
				spr\u1D39.ᜀ(16225862),
				spr\u1D39.ᜀ(255),
				spr\u1D39.ᜀ(8388736)
			};
			XlsWorkbook.\u1732 = new Color[]
			{
				spr\u1D39.ᜊ,
				spr\u1D39.ᜋ,
				spr\u1D39.ᜌ
			};
			XlsWorkbook.ផ = new Dictionary<ExcelSheetType, string>(5);
			XlsWorkbook.ផ.Add(ExcelSheetType.ChartSheet, RecordTableEnumerator.b("ੈ⍊ⱌ㵎═⁒", a_));
			XlsWorkbook.ផ.Add(ExcelSheetType.DialogSheet, RecordTableEnumerator.b("ൈ≊ⱌ⍎㹐㑒♔", a_));
			XlsWorkbook.ផ.Add(ExcelSheetType.Excel4IntlMacroSheet, RecordTableEnumerator.b("ై㍊⹌⩎㵐獒慔祖楘筚ᑜㅞᕠར䕤⩦ࡨᥪ๬nɰ", a_));
			XlsWorkbook.ផ.Add(ExcelSheetType.Excel4MacroSheet, RecordTableEnumerator.b("ై㍊⹌⩎㵐獒慔祖楘筚ၜ㹞ɠᅢ੤ᑦ", a_));
			XlsWorkbook.ផ.Add(ExcelSheetType.NormalWorksheet, RecordTableEnumerator.b("Ṉ⑊㽌⑎≐㭒ご㉖ⵘ⡚", a_));
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x0039A5A8 File Offset: 0x003995A8
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, ExcelVersion A_2) : this(A_0, A_1, A_0.ᜊ(), A_2)
		{
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x0039A5C4 File Offset: 0x003995C4
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, int A_2, ExcelVersion A_3)
		{
			int a_ = 15;
			this.\u173E = (sprᦖ)spr\u175E.ᜀ(TBIFFRecord.ExternSheet);
			this.ᝊ = RecordTableEnumerator.b("ᅄ⽆⁈㡊ᩌ⁎⍐㡒㝔㡖㙘ず", a_);
			this.\u1758 = true;
			this.\u175A = 8;
			this.ᝯ = true;
			this.ᝰ = -1;
			this.\u1771 = -1;
			this.\u1775 = 65536;
			this.\u1776 = 256;
			this.\u1777 = 4095;
			this.\u1778 = 15;
			this.\u177B = 15;
			this.ក = new List<Color>(XlsWorkbook.ᜱ);
			this.គ = 1;
			this.ឃ = 1;
			base..ctor(A_0, A_1);
			this.InitializeCollections();
			this.Version = A_3;
			this.ᜭ();
			this.\u171E();
			this.ᝃ = false;
			this.ឆ = true;
			this.\u1735.EnsureCapacity(A_2);
			for (int i = 0; i < A_2; i++)
			{
				this.\u1735.Add(string.Format(RecordTableEnumerator.b("ᙄ⽆ⱈ⹊㥌㑎慐⹒", a_), i + 1));
			}
			this.\u1735[0].Activate();
		}

		// Token: 0x06005C55 RID: 23637 RVA: 0x0039A6F8 File Offset: 0x003996F8
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, string A_2, ExcelVersion A_3) : this(A_0, A_1, A_2, ExcelParseOptions.Default, A_3)
		{
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x0039A711 File Offset: 0x00399711
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, string A_2, ExcelParseOptions A_3, ExcelVersion A_4) : this(A_0, A_1, A_2, A_3, false, null, A_4)
		{
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x0039A724 File Offset: 0x00399724
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, string A_2, ExcelParseOptions A_3, bool A_4, string A_5, ExcelVersion A_6)
		{
			int a_ = 13;
			this.\u173E = (sprᦖ)spr\u175E.ᜀ(TBIFFRecord.ExternSheet);
			this.ᝊ = RecordTableEnumerator.b("ᝂⵄ⹆㩈᱊≌㵎㩐ㅒ㩔㡖㉘", a_);
			this.\u1758 = true;
			this.\u175A = 8;
			this.ᝯ = true;
			this.ᝰ = -1;
			this.\u1771 = -1;
			this.\u1775 = 65536;
			this.\u1776 = 256;
			this.\u1777 = 4095;
			this.\u1778 = 15;
			this.\u177B = 15;
			this.ក = new List<Color>(XlsWorkbook.ᜱ);
			this.គ = 1;
			this.ឃ = 1;
			base..ctor(A_0, A_1);
			this.ᝠ = A_0.\u1715();
			this.InitializeCollections();
			this.Version = A_6;
			this.ᜀ(A_2, A_5, A_6, A_3);
			this.ᝀ = Path.GetFullPath(A_2);
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x0039A810 File Offset: 0x00399810
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, Stream A_2, string A_3, int A_4, int A_5, ExcelVersion A_6, string A_7, Encoding A_8)
		{
			int a_ = 7;
			this..ctor(A_0, A_1, 1, A_6);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("丼䬾㍀♂⑄⩆", a_));
			}
			if (A_3 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("丼娾ㅀ≂㝄♆㵈⑊㽌", a_));
			}
			if (A_3.Length == 0)
			{
				throw new ArgumentException(RecordTableEnumerator.b("丼娾ㅀ≂㝄♆㵈⑊㽌", a_));
			}
			if (A_8 == null)
			{
				A_8 = Encoding.Default;
			}
			this.ច = true;
			StreamReader a_2 = new StreamReader(A_2, A_8);
			this.ᝆ = true;
			if (this.\u1734 != null)
			{
				bool a_3 = this.ᜀ(A_2, A_8, A_3);
				((XlsWorksheet)this.\u1734).ᜀ(a_2, A_3, A_4, A_5, a_3);
				if (A_7 != null && A_7.Length > 0)
				{
					this.\u1734.Name = Path.GetFileNameWithoutExtension(A_7);
				}
			}
			this.ᝆ = false;
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x0039A918 File Offset: 0x00399918
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, Stream A_2, ExcelVersion A_3) : this(A_0, A_1, A_2, ExcelParseOptions.Default, A_3)
		{
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x0039A934 File Offset: 0x00399934
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, Stream A_2, ExcelParseOptions A_3, ExcelVersion A_4)
		{
			int a_ = 9;
			this.\u173E = (sprᦖ)spr\u175E.ᜀ(TBIFFRecord.ExternSheet);
			this.ᝊ = RecordTableEnumerator.b("款⥀⩂㙄၆♈㥊♌ⵎ㹐㱒㹔", a_);
			this.\u1758 = true;
			this.\u175A = 8;
			this.ᝯ = true;
			this.ᝰ = -1;
			this.\u1771 = -1;
			this.\u1775 = 65536;
			this.\u1776 = 256;
			this.\u1777 = 4095;
			this.\u1778 = 15;
			this.\u177B = 15;
			this.ក = new List<Color>(XlsWorkbook.ᜱ);
			this.គ = 1;
			this.ឃ = 1;
			base..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀ㅂ⁄♆⑈", a_));
			}
			this.ᝠ = A_0.\u1715();
			this.ប = A_3;
			this.InitializeCollections();
			this.Version = A_4;
			this.ᜀ(A_2, null, A_4, A_3);
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x0039AA34 File Offset: 0x00399A34
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, XmlReader A_2, XmlOpenType A_3)
		{
			int a_ = 3;
			this.\u173E = (sprᦖ)spr\u175E.ᜀ(TBIFFRecord.ExternSheet);
			this.ᝊ = RecordTableEnumerator.b("洸区吼䰾ᙀⱂ㝄ⱆ⭈⑊≌⑎", a_);
			this.\u1758 = true;
			this.\u175A = 8;
			this.ᝯ = true;
			this.ᝰ = -1;
			this.\u1771 = -1;
			this.\u1775 = 65536;
			this.\u1776 = 256;
			this.\u1777 = 4095;
			this.\u1778 = 15;
			this.\u177B = 15;
			this.ក = new List<Color>(XlsWorkbook.ᜱ);
			this.គ = 1;
			this.ឃ = 1;
			base..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("䬸帺尼嬾⑀ㅂ", a_));
			}
			this.InitializeCollections();
			this.Version = (A_0 as spr\u17FF).ᜋ();
			this.ᜭ();
			this.\u171E();
			this.ᝃ = false;
			if (A_3 == XmlOpenType.MSExcel)
			{
				this.ᝆ = true;
				spr\u247E spr_u247E = new spr\u247E(base.ReservedHandle, this);
				bool u = this.\u1758;
				this.\u1758 = false;
				spr_u247E.ᜄ(A_2, this);
				this.\u1758 = u;
				this.ᝆ = false;
				return;
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("甸吺尼嬾慀㭂⡄⭆楈ⵊ⑌⍎㑐獒㍔㙖じ㝚㡜㭞你", a_));
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x0039AB84 File Offset: 0x00399B84
		internal XlsWorkbook(spr\u1DF5 A_0, object A_1, Stream A_2, ExcelParseOptions A_3, bool A_4, string A_5, ExcelVersion A_6)
		{
			int a_ = 9;
			this.\u173E = (sprᦖ)spr\u175E.ᜀ(TBIFFRecord.ExternSheet);
			this.ᝊ = RecordTableEnumerator.b("款⥀⩂㙄၆♈㥊♌ⵎ㹐㱒㹔", a_);
			this.\u1758 = true;
			this.\u175A = 8;
			this.ᝯ = true;
			this.ᝰ = -1;
			this.\u1771 = -1;
			this.\u1775 = 65536;
			this.\u1776 = 256;
			this.\u1777 = 4095;
			this.\u1778 = 15;
			this.\u177B = 15;
			this.ក = new List<Color>(XlsWorkbook.ᜱ);
			this.គ = 1;
			this.ឃ = 1;
			base..ctor(A_0, A_1);
			this.ᝠ = A_0.\u1715();
			this.InitializeCollections();
			this.Version = A_6;
			this.ᜀ(A_2, A_5, A_6, A_3);
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x0039AC64 File Offset: 0x00399C64
		protected void InitializeCollections()
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
			this.ᜐ();
			this.\u1754 = new WorkbookObjectsCollection(base.ReservedHandle as spr\u2158, this);
			this.\u1735 = new WorksheetsCollection(base.ReservedHandle as spr\u2158, this);
			this.\u1736 = new StylesCollection(base.ReservedHandle as spr\u2158, this);
			this.ᝏ = new List<Color>(XlsWorkbook.ᜨ);
			this.\u1752 = new sprឦ(base.ReservedHandle, this);
			this.\u1753 = new ChartsCollection(base.ReservedHandle, this);
			this.\u173D = new SSTDictionary(this);
			this.\u1737 = new FontsCollection(base.ReservedHandle as spr\u2158, this);
			this.ᝢ = new spr\u2594(base.ReservedHandle as spr\u2158, this);
			this.ᝣ = new AddInFunctionsCollection(base.ReservedHandle as spr\u2158, this);
			this.ᝥ = new spr\u24A9(base.ReservedHandle as spr\u2158, this);
			this.\u1738 = new sprᡲ(base.ReservedHandle as spr\u2158, this);
			this.\u1759 = new XlsWorkbookShapeData(base.ReservedHandle, this, new XlsWorkbook.ᜁ(this.ᜁ));
			this.ᝬ = new spr\u1AA2(base.ReservedHandle, this);
			this.ᝫ = new BuiltInDocumentProperties((spr\u2158)base.ReservedHandle, this);
			this.ᝫ[BuiltInPropertyType.Author].Text = Environment.UserName;
			this.\u1739 = new List<sprῚ>();
			this.\u173A = new spr\u21DE(base.AppImplementation as spr\u2158, this);
			this.\u173C = new List<spr\u17C1>();
			this.\u173F = new List<sprṨ>();
			this.\u175E = new List<sprỶ>();
			this.ᝤ = new XlsWorkbookShapeData(base.AppImplementation, this, new XlsWorkbook.ᜁ(this.ᜀ));
			this.ᝩ = new spr\u233D(base.AppImplementation, this);
			this.WindowOne.ᜇ(ushort.MaxValue);
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x0039AE80 File Offset: 0x00399E80
		private void ᜐ()
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
			Image image = new Bitmap(1, 1);
			this.ᝧ = Graphics.FromImage(image);
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x0039AED0 File Offset: 0x00399ED0
		internal void \u171E()
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
			this.\u173A.ᜅ();
			this.InsertDefaultExtFormats();
			this.InsertDefaultStyles();
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x0039AF24 File Offset: 0x00399F24
		protected void InsertDefaultExtFormats()
		{
			for (;;)
			{
				int count = this.\u1738.Count;
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (count <= 18)
						{
							num = 9;
							continue;
						}
						goto IL_125;
					case 1:
						if (count <= 5)
						{
							num = 13;
							continue;
						}
						goto IL_4D8;
					case 2:
					{
						sprỶ sprỶ = this.ᜁ(19);
						this.\u1738.ᜀ(new spr\u192F(base.AppImplementation, this, sprỶ));
						num = 21;
						continue;
					}
					case 3:
						goto IL_4D8;
					case 4:
						if (count <= 20)
						{
							num = 6;
							continue;
						}
						return;
					case 5:
						if (count <= 3)
						{
							num = 10;
							continue;
						}
						goto IL_494;
					case 6:
					{
						sprỶ sprỶ = this.ᜁ(20);
						this.\u1738.ᜀ(new spr\u192F(base.AppImplementation, this, sprỶ));
						num = 15;
						continue;
					}
					case 7:
						if (count <= 19)
						{
							num = 2;
							continue;
						}
						goto IL_1CE;
					case 8:
						goto IL_125;
					case 9:
					{
						sprỶ sprỶ = this.ᜁ(18);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						num = 8;
						continue;
					}
					case 10:
					{
						sprỶ sprỶ = this.ᜁ(3);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						num = 19;
						continue;
					}
					case 11:
					{
						sprỶ sprỶ = this.ᜁ(0);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4C8;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					}
					case 12:
						if (count <= 0)
						{
							num = 11;
							continue;
						}
						goto IL_4B6;
					case 13:
					{
						sprỶ sprỶ = this.ᜁ(5);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						num = 3;
						continue;
					}
					case 14:
						if (true)
						{
						}
						goto IL_4B6;
					case 15:
						return;
					case 16:
					{
						sprỶ sprỶ = this.ᜁ(1);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, (sprỶ)sprỶ.Clone()));
						num = 26;
						continue;
					}
					case 17:
						if (count <= 16)
						{
							num = 22;
							continue;
						}
						goto IL_52A;
					case 18:
					{
						sprỶ sprỶ = this.ᜁ(17);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						num = 28;
						continue;
					}
					case 19:
						goto IL_494;
					case 20:
						if (count <= 1)
						{
							goto IL_4C8;
						}
						goto IL_103;
					case 21:
						goto IL_1CE;
					case 22:
					{
						sprỶ sprỶ = this.ᜁ(16);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						num = 24;
						continue;
					}
					case 23:
						if (count <= 15)
						{
							num = 25;
							continue;
						}
						goto IL_1FB;
					case 24:
						goto IL_52A;
					case 25:
					{
						sprỶ sprỶ = this.ᜁ(15);
						this.\u1738.ᜀ(new spr\u192F(base.ReservedHandle, this, sprỶ));
						num = 27;
						continue;
					}
					case 26:
						goto IL_103;
					case 27:
						goto IL_1FB;
					case 28:
						goto IL_B1;
					case 29:
						if (count <= 17)
						{
							num = 18;
							continue;
						}
						goto IL_B1;
					}
					break;
					IL_B1:
					num = 0;
					continue;
					IL_103:
					num = 5;
					continue;
					IL_125:
					num = 7;
					continue;
					IL_1CE:
					num = 4;
					continue;
					IL_1FB:
					num = 17;
					continue;
					IL_494:
					num = 1;
					continue;
					IL_4B6:
					num = 20;
					continue;
					IL_4C8:
					num = 16;
					continue;
					IL_4D8:
					num = 23;
					continue;
					IL_52A:
					num = 29;
				}
			}
		}

		// Token: 0x06005C61 RID: 23649 RVA: 0x0039B4B0 File Offset: 0x0039A4B0
		protected void InsertDefaultStyles()
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
			this.ᜁ(null);
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x0039B4F4 File Offset: 0x0039A4F4
		internal void ᜁ(List<sprᬐ> A_0)
		{
			int a_ = 5;
			sprᬐ sprᬐ;
			for (;;)
			{
				sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
				sprᬐ.ᜀ(0);
				sprᬐ.ᜁ(0);
				sprᬐ = this.ᜀ(A_0, sprᬐ);
				XlsStyle xlsStyle = base.AppImplementation.ᜀ(this, sprᬐ);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B3;
					case 1:
						for (;;)
						{
							this.\u1736.Add(xlsStyle, true);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_9A;
							}
						}
						IL_9A:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					case 2:
						if (!this.\u1736.ᜁ(xlsStyle.Name))
						{
							num = 1;
							continue;
						}
						goto IL_B5;
					}
					break;
				}
			}
			IL_B3:
			IL_B5:
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(16);
			sprᬐ.ᜁ(3);
			sprᬐ = this.ᜀ(A_0, sprᬐ);
			XlsStyle a_2 = base.AppImplementation.ᜀ(this, sprᬐ);
			this.ᜀ(a_2);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(17);
			sprᬐ.ᜁ(6);
			sprᬐ = this.ᜀ(A_0, sprᬐ);
			a_2 = base.AppImplementation.ᜀ(this, sprᬐ);
			this.ᜀ(a_2);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(18);
			sprᬐ.ᜁ(4);
			sprᬐ = this.ᜀ(A_0, sprᬐ);
			a_2 = base.AppImplementation.ᜀ(this, sprᬐ);
			this.ᜀ(a_2);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(19);
			sprᬐ.ᜁ(7);
			sprᬐ = this.ᜀ(A_0, sprᬐ);
			a_2 = base.AppImplementation.ᜀ(this, sprᬐ);
			this.ᜀ(a_2);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ = this.ᜀ(A_0, sprᬐ);
			a_2 = base.AppImplementation.ᜀ(this, sprᬐ);
			this.ᜀ(a_2);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(20);
			sprᬐ.ᜁ(5);
			sprᬐ = this.ᜀ(A_0, sprᬐ);
			a_2 = base.AppImplementation.ᜀ(this, sprᬐ);
			this.ᜀ(a_2);
			(this.Styles[RecordTableEnumerator.b("町刼䴾ⱀ≂⥄", a_)].Font as FontWrapper).AfterChangeEvent += this.ᜀ;
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x0039B754 File Offset: 0x0039A754
		private void ᜀ(XlsStyle A_0)
		{
			int a_ = 15;
			int num = 3;
			for (;;)
			{
				IL_13:
				int extendedFormatIndex;
				switch (num)
				{
				case 0:
					while (!this.\u1736.ᜁ(A_0.Name))
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
							num = 4;
							goto IL_13;
						}
					}
					return;
				case 1:
				{
					if (true)
					{
					}
					spr\u192F spr_u192F = this.ᜀ(this.InnerExtFormats.ᜁ(0));
					spr_u192F = this.ᜀ(spr_u192F, true);
					A_0.SetFormatIndex(spr_u192F.ᜠ());
					num = 2;
					continue;
				}
				case 2:
					goto IL_4C;
				case 4:
					this.\u1736.Add(A_0);
					num = 7;
					continue;
				case 5:
					if (this.InnerExtFormats.ᜁ(extendedFormatIndex).ᝇ())
					{
						num = 1;
						continue;
					}
					goto IL_4C;
				case 6:
					goto IL_47;
				case 7:
					return;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				extendedFormatIndex = A_0.ExtendedFormatIndex;
				num = 5;
				continue;
				IL_4C:
				num = 0;
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙄㍆♈㹊㥌", a_));
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x0039B8A4 File Offset: 0x0039A8A4
		[CLSCompliant(false)]
		internal sprỶ ᜁ(int A_0)
		{
			sprỶ sprỶ;
			for (;;)
			{
				sprỶ = null;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_23C;
					case 1:
						goto IL_1A3;
					case 2:
						switch (A_0)
						{
						case 0:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜎ(true);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜎ(65);
							sprỶ.ᜊ(64);
							num = 1;
							continue;
						case 1:
						case 2:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜉ(1);
							sprỶ.ᜎ(true);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(VerticalAlignType.Bottom);
							sprỶ.ᜁ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							num = 9;
							continue;
						case 3:
						case 4:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜉ(2);
							sprỶ.ᜎ(true);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(VerticalAlignType.Bottom);
							sprỶ.ᜁ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							num = 8;
							continue;
						case 5:
						case 6:
						case 7:
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 13:
						case 14:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜎ(true);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(VerticalAlignType.Bottom);
							sprỶ.ᜁ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							num = 11;
							continue;
						case 15:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜎ(true);
							sprỶ.ᜀ(HorizontalAlignType.General);
							sprỶ.ᜀ(VerticalAlignType.Bottom);
							sprỶ.ᜎ(65);
							sprỶ.ᜊ(64);
							num = 6;
							continue;
						case 16:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜃ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							sprỶ.ᜉ(1);
							sprỶ.ᜈ(43);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							num = 10;
							continue;
						case 17:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜃ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							sprỶ.ᜉ(1);
							sprỶ.ᜈ(41);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							num = 4;
							continue;
						case 18:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜎ(true);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜃ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							sprỶ.ᜉ(1);
							sprỶ.ᜈ(44);
							num = 5;
							continue;
						case 19:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜎ(true);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜃ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							sprỶ.ᜉ(1);
							sprỶ.ᜈ(42);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return sprỶ;
							default:
								if (false)
								{
								}
								num = 12;
								continue;
							}
							break;
						case 20:
							sprỶ = (sprỶ)spr\u175E.ᜀ(TBIFFRecord.ExtendedFormat);
							sprỶ.ᜎ(true);
							sprỶ.ᜇ((ushort)this.MaxXFCount);
							sprỶ.ᜀ(sprỶ.TXFType.XF_CELL);
							sprỶ.ᜃ(true);
							sprỶ.\u170D(true);
							sprỶ.ᜆ(true);
							sprỶ.ᜋ(true);
							sprỶ.ᜅ(true);
							sprỶ.ᜉ(1);
							sprỶ.ᜈ(9);
							num = 3;
							continue;
						default:
							num = 7;
							continue;
						}
						break;
					case 3:
						return sprỶ;
					case 4:
						goto IL_113;
					case 5:
						return sprỶ;
					case 6:
						goto IL_158;
					case 7:
						num = 0;
						continue;
					case 8:
						return sprỶ;
					case 9:
						return sprỶ;
					case 10:
						return sprỶ;
					case 11:
						return sprỶ;
					case 12:
						goto IL_22C;
					}
					break;
				}
			}
			IL_113:
			IL_158:
			IL_1A3:
			IL_22C:
			return sprỶ;
			IL_23C:
			if (true)
			{
			}
			return sprỶ;
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x0039BD84 File Offset: 0x0039AD84
		private sprᬐ ᜀ(List<sprᬐ> A_0, sprᬐ A_1)
		{
			int num = 7;
			sprᬐ sprᬐ;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4E;
					default:
						goto IL_88;
					}
					break;
				case 1:
					return sprᬐ;
				case 2:
					goto IL_90;
				case 3:
					if (this.ᜀ(sprᬐ, A_1))
					{
						num = 1;
						continue;
					}
					num2++;
					num = 5;
					continue;
				case 4:
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					goto IL_4E;
				case 5:
					goto IL_90;
				case 6:
					return A_1;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				if (true)
				{
				}
				num2 = 0;
				count = A_0.Count;
				num = 2;
				continue;
				IL_4E:
				sprᬐ = A_0[num2];
				num = 3;
				continue;
				IL_90:
				num = 4;
			}
			return sprᬐ;
			IL_88:
			if (false)
			{
			}
			return A_1;
		}

		// Token: 0x06005C66 RID: 23654 RVA: 0x0039BE68 File Offset: 0x0039AE68
		private bool ᜀ(sprᬐ A_0, sprᬐ A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					goto IL_77;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						goto IL_5A;
					}
					break;
				}
				if (A_0.ᜄ())
				{
					num = 0;
					continue;
				}
				return false;
				IL_77:
				if (!A_1.ᜄ())
				{
					return false;
				}
				num = 3;
			}
			IL_5A:
			if (false)
			{
			}
			return A_0.ᜁ() == A_1.ᜁ();
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x0039BF00 File Offset: 0x0039AF00
		internal void ᜭ()
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
			this.\u1737.InsertDefaultFonts();
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x0039BF48 File Offset: 0x0039AF48
		protected void DisposeAll()
		{
			for (;;)
			{
				this.\u1754.DisposeInternalData();
				this.ᝢ.ᜁ();
				this.ClearAll();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (this.\u173D != null)
						{
							num = 5;
							continue;
						}
						goto IL_10E;
					case 2:
						if (this.\u177E != IntPtr.Zero)
						{
							num = 4;
							continue;
						}
						goto IL_6F;
					case 3:
						if (true)
						{
						}
						goto IL_6F;
					case 4:
						Heap.HeapDestroy(this.\u177E);
						this.\u177E = IntPtr.Zero;
						num = 3;
						continue;
					case 5:
						this.\u173D.Dispose();
						this.\u173D = null;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_101;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 6:
						goto IL_10E;
					case 7:
						this.\u175F.Dispose();
						goto IL_101;
					case 8:
						if (this.\u175F != null)
						{
							num = 7;
							continue;
						}
						return;
					}
					break;
					IL_6F:
					num = 8;
					continue;
					IL_101:
					num = 0;
					continue;
					IL_10E:
					num = 2;
				}
			}
		}

		// Token: 0x06005C69 RID: 23657 RVA: 0x0039C094 File Offset: 0x0039B094
		protected void ClearAll()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.\u1736.Clear();
					IEnumerator<IWorksheet> enumerator = this.\u1735.GetEnumerator();
					int num = 15;
					for (;;)
					{
						IEnumerator<INamedRange> enumerator2;
						switch (num)
						{
						case 0:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 4;
										continue;
									case 1:
									{
										if (!enumerator2.MoveNext())
										{
											num = 0;
											continue;
										}
										XlsName xlsName = (XlsName)enumerator2.Current;
										xlsName.ᜃ();
										num = 3;
										continue;
									}
									case 4:
										goto IL_53A;
									}
									IL_515:
									num = 1;
									continue;
									goto IL_515;
								}
								IL_53A:
								goto IL_5B2;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_577;
									case 1:
										enumerator2.Dispose();
										num = 0;
										continue;
									}
									if (enumerator2 == null)
									{
										break;
									}
									num = 1;
								}
								IL_577:;
							}
							goto IL_57A;
						case 1:
							if (this.\u1737 != null)
							{
								num = 7;
								continue;
							}
							goto IL_229;
						case 2:
							if (this.\u175E != null)
							{
								num = 22;
								continue;
							}
							goto IL_367;
						case 3:
							goto IL_1E5;
						case 4:
							goto IL_3AB;
						case 5:
							goto IL_F6;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1D4;
							default:
								if (false)
								{
								}
								this.ᝬ.Clear();
								num = 11;
								continue;
							}
							break;
						case 7:
							this.\u1737.Clear();
							num = 23;
							continue;
						case 8:
							this.ᝣ.Clear();
							goto IL_1D4;
						case 9:
							if (this.ᝬ != null)
							{
								num = 6;
								continue;
							}
							goto IL_CE;
						case 10:
							if (this.ᝣ != null)
							{
								num = 8;
								continue;
							}
							goto IL_F6;
						case 11:
							goto IL_CE;
						case 12:
							this.ជ.Clear();
							num = 4;
							continue;
						case 13:
							this.ឈ.Clear();
							num = 29;
							continue;
						case 14:
							this.\u173D.Clear();
							num = 3;
							continue;
						case 15:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 1:
										goto IL_2C5;
									case 2:
										num = 1;
										continue;
									case 4:
									{
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										IWorksheet worksheet = enumerator.Current;
										(worksheet as XlsWorksheet).ᜧ();
										num = 0;
										continue;
									}
									}
									IL_283:
									num = 4;
									continue;
									goto IL_283;
								}
								IL_2C5:
								goto IL_455;
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										enumerator.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_30D;
									}
									if (true)
									{
									}
									if (enumerator == null)
									{
										break;
									}
									num = 0;
								}
								IL_30D:;
							}
							goto IL_310;
							IL_455:
							this.\u1735.Clear();
							this.\u173C.Clear();
							this.\u1739.Clear();
							this.\u173F.Clear();
							this.ᝏ.Clear();
							num = 27;
							continue;
						case 16:
							goto IL_411;
						case 17:
							goto IL_310;
						case 18:
							this.\u1754.Clear();
							num = 21;
							continue;
						case 19:
							if (this.ឈ != null)
							{
								num = 13;
								continue;
							}
							goto IL_13A;
						case 20:
							if (this.\u1754 != null)
							{
								num = 18;
								continue;
							}
							goto IL_19E;
						case 21:
							goto IL_19E;
						case 22:
							this.\u175E.Clear();
							num = 31;
							continue;
						case 23:
							goto IL_229;
						case 24:
							this.ᝢ.Clear();
							num = 26;
							continue;
						case 25:
							if (this.\u173D != null)
							{
								num = 14;
								continue;
							}
							goto IL_1E5;
						case 26:
							goto IL_3E9;
						case 27:
							if (this.\u1738 != null)
							{
								num = 32;
								continue;
							}
							goto IL_310;
						case 28:
							if (this.ᝫ != null)
							{
								num = 30;
								continue;
							}
							goto IL_411;
						case 29:
							goto IL_13A;
						case 30:
							goto IL_57A;
						case 31:
							goto IL_367;
						case 32:
							this.\u1738.Clear();
							num = 17;
							continue;
						case 33:
							if (this.ᝢ != null)
							{
								num = 24;
								continue;
							}
							goto IL_3E9;
						case 34:
							if (this.ជ != null)
							{
								num = 12;
								continue;
							}
							goto IL_3AB;
						}
						break;
						IL_CE:
						num = 34;
						continue;
						IL_F6:
						num = 28;
						continue;
						IL_13A:
						this.\u177C = null;
						num = 20;
						continue;
						IL_19E:
						this.ខ = null;
						this.ត = null;
						enumerator2 = this.\u1752.GetEnumerator();
						num = 0;
						continue;
						IL_1D4:
						num = 5;
						continue;
						IL_1E5:
						num = 2;
						continue;
						IL_229:
						num = 33;
						continue;
						IL_310:
						this.\u1737.Clear();
						this.\u173A.ᜈ();
						num = 25;
						continue;
						IL_367:
						num = 1;
						continue;
						IL_3AB:
						num = 19;
						continue;
						IL_3E9:
						num = 10;
						continue;
						IL_411:
						num = 9;
						continue;
						IL_57A:
						this.ᝫ.Clear();
						num = 16;
					}
				}
				IL_5B2:
				this.\u1752.Clear();
				return;
			}
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x0039C67C File Offset: 0x0039B67C
		internal void \u1716()
		{
			int num = 1;
			for (;;)
			{
				IEnumerator<spr\u192F> enumerator;
				switch (num)
				{
				case 0:
					goto IL_FD;
				case 2:
					try
					{
						num = 0;
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
								switch (num)
								{
								case 1:
									goto IL_C1;
								case 3:
									goto IL_A0;
								case 4:
								{
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									spr\u192F spr_u192F = enumerator.Current;
									spr_u192F.ᜥ();
									num = 2;
									continue;
								}
								}
								IL_88:
								num = 4;
								continue;
								goto IL_88;
							}
							IL_A0:
							num = 1;
						}
						IL_C1:
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_FA;
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
						IL_FA:;
					}
					goto IL_FD;
				}
				if (true)
				{
				}
				if (this.\u1738 != null)
				{
					num = 0;
					continue;
				}
				break;
				IL_FD:
				enumerator = this.\u1738.GetEnumerator();
				num = 2;
			}
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x0039C7B4 File Offset: 0x0039B7B4
		private void ᜀ(spr\u20C3 A_0, IDecryptor A_1)
		{
			int a_ = 18;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉⍋㱍ㅏ㕑ㅓ", a_));
			}
			IL_50:
			this.ᝦ = new PivotCachesCollection(base.AppImplementation, this, A_0, A_1);
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x0039C828 File Offset: 0x0039B828
		private void ᜀ(spr\u20C3 A_0, ExcelParseOptions A_1, string A_2)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				bool flag;
				for (;;)
				{
					string text = XlsWorkbook.ᜀ(A_0, RecordTableEnumerator.b("ቄ⡆㭈⁊⽌⁎㹐㡒", a_));
					if (true)
					{
					}
					int num = 3;
					for (;;)
					{
						IDecryptor a_2;
						spr\u1FDC spr_u1FDC;
						switch (num)
						{
						case 0:
							goto IL_128;
						case 1:
							goto IL_89;
						case 2:
							if (A_0.ᜇ(RecordTableEnumerator.b("ᩄᑆᅈᑊौൎ๐ၒTՖ", a_)))
							{
								num = 0;
								continue;
							}
							goto IL_1EB;
						case 3:
							if (text != null)
							{
								this.ᜃ(A_0);
								this.ᜁ(A_0);
								flag = A_0.ᜇ(RecordTableEnumerator.b("ᩄᅆୈ੊ቌ὎͐᱒ὔቖᩘཚɜᱞ㑠ㅢ", a_));
								this.ᝌ = A_0.ᜃ(RecordTableEnumerator.b("䁄ᑆ㱈♊⁌⹎⍐⩒᱔㥖㽘㑚⽜㉞`ᝢ౤ࡦݨ", a_));
								this.ᝍ = A_0.ᜃ(RecordTableEnumerator.b("䁄͆♈⡊㡌≎㑐㵒⅔іⱘ㙚ぜ㹞፠ᩢⱤ०ཨѪὬɮၰݲᱴᡶ᝸", a_));
								a_2 = null;
								num = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_204;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 4:
							goto IL_1EB;
						case 5:
							try
							{
								sprἛ sprἛ = new sprἛ(spr_u1FDC);
								try
								{
									a_2 = this.ᜀ(sprἛ, A_1, A_2);
								}
								finally
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											((IDisposable)sprἛ).Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_E2;
										}
										if (sprἛ == null)
										{
											break;
										}
										num = 0;
									}
									IL_E2:;
								}
								goto IL_204;
							}
							finally
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										goto IL_125;
									case 2:
										((IDisposable)spr_u1FDC).Dispose();
										num = 1;
										continue;
									}
									if (spr_u1FDC == null)
									{
										break;
									}
									num = 2;
								}
								IL_125:;
							}
							goto IL_128;
						}
						break;
						IL_128:
						this.ᜀ(A_0, a_2);
						num = 4;
						continue;
						IL_1EB:
						spr_u1FDC = A_0.ᜁ(text);
						num = 5;
					}
				}
				IL_89:
				throw new ApplicationException(RecordTableEnumerator.b("̈́⹆╈⹊浌⭎㹐㙒♔睖㝘㑚⥜罞ɠౢ୤፦ࡨɪͬ佮ٰᱲݴᱶ᭸ᑺቼᑾꆀ", a_));
				IL_204:
				this.ᝋ = (this.ᝋ && flag);
				return;
			}
			}
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x0039CA64 File Offset: 0x0039BA64
		private void ᜃ(spr\u20C3 A_0)
		{
			int a_ = 13;
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					if (true)
					{
					}
					spr\u1FDC spr_u1FDC = A_0.ᜁ(RecordTableEnumerator.b("Bㅄ⭆㩈", a_));
					this.ខ = new MemoryStream((int)spr_u1FDC.Length);
					UtilityMethods.ᜀ(spr_u1FDC, this.ខ);
					spr_u1FDC.Position = 0L;
					spr_u1FDC.Close();
					num = 0;
					continue;
				}
				}
				if (!A_0.ᜃ(RecordTableEnumerator.b("Bㅄ⭆㩈", a_)))
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x0039CB34 File Offset: 0x0039BB34
		private void ᜂ(spr\u20C3 A_0)
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
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x0039CB70 File Offset: 0x0039BB70
		private static string ᜀ(spr\u20C3 A_0, string A_1)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 0;
				string result;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						goto IL_7A;
					case 2:
					{
						int num2;
						int num3;
						if (num2 >= num3)
						{
							num = 7;
							continue;
						}
						string[] array;
						string text = array[num2];
						num = 9;
						continue;
					}
					case 3:
					{
						string text;
						result = text;
						num = 1;
						continue;
					}
					case 4:
						goto IL_E2;
					case 5:
						goto IL_69;
					case 6:
						goto IL_126;
					case 7:
						return result;
					case 8:
					{
						if (A_1.Length == 0)
						{
							num = 4;
							continue;
						}
						string[] array = A_0.ᜁ();
						result = null;
						int num2 = 0;
						int num3 = array.Length;
						num = 10;
						continue;
					}
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_126;
						default:
						{
							if (false)
							{
							}
							string text;
							if (string.Compare(text, A_1, StringComparison.CurrentCultureIgnoreCase) == 0)
							{
								num = 3;
								continue;
							}
							int num2;
							num2++;
							num = 6;
							continue;
						}
						}
						break;
					case 10:
						goto IL_E4;
					}
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num = 8;
					continue;
					IL_E4:
					num = 2;
					continue;
					IL_126:
					goto IL_E4;
				}
				IL_69:
				throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ㕆ᩈ㽊㽌⩎ぐ㹒᭔㙖㑘㹚", a_));
				IL_7A:
				return result;
				IL_E2:
				throw new ArgumentException(RecordTableEnumerator.b("あㅄ㕆ᩈ㽊㽌⩎ぐ㹒᭔㙖㑘㹚絜牞䅠ၢᅤᕦhժ੬佮ተቲ᭴᥶ᙸེ嵼ᵾꎂ麗ﾊꆎ", a_));
			}
			}
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x0039CCE4 File Offset: 0x0039BCE4
		private void ᜀ(Stream A_0, string A_1)
		{
			int a_ = 8;
			while (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䴽㐿ぁ⅃❅╇", a_));
				}
			}
			this.\u173A.ᜅ();
			this.\u177C = new sprវ(this, A_0, A_1);
			this.\u177C.ᜁ(ref this.ក);
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x0039CD6C File Offset: 0x0039BD6C
		private void ᜀ(string A_0, string A_1, ExcelVersion A_2, ExcelParseOptions A_3)
		{
			FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read, FileShare.Read);
			try
			{
				if (true)
				{
				}
				this.ᜀ(fileStream, A_1, A_2, A_3);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_48;
						default:
							goto IL_6E;
						}
						break;
					case 1:
						goto IL_48;
					}
					if (fileStream != null)
					{
						num = 1;
						continue;
					}
					goto IL_76;
					IL_48:
					((IDisposable)fileStream).Dispose();
					num = 0;
				}
				IL_6E:
				if (false)
				{
				}
				IL_76:;
			}
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x0039CE0C File Offset: 0x0039BE0C
		private void ᜀ(Stream A_0, string A_1, ExcelVersion A_2, ExcelParseOptions A_3)
		{
			int a_ = 14;
			for (;;)
			{
				for (;;)
				{
					this.ច = true;
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_4B;
						case 1:
							if (A_2 == ExcelVersion.Version2010)
							{
								num = 5;
								continue;
							}
							goto IL_D4;
						case 2:
							if (A_2 == ExcelVersion.Version97to2003)
							{
								num = 0;
								continue;
							}
							num = 4;
							continue;
						case 3:
							num = 1;
							continue;
						case 4:
							if (A_2 == ExcelVersion.Version2007)
							{
								goto IL_C1;
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
								num = 3;
								continue;
							}
							break;
						case 5:
							goto IL_61;
						}
						break;
					}
				}
			}
			IL_4B:
			this.\u175F = base.AppImplementation.ᜁ(A_0);
			spr\u20C3 a_2 = this.\u175F.ᜀ();
			this.ᜀ(a_2, A_3, A_1);
			return;
			IL_61:
			IL_C1:
			this.ᜀ(A_0, A_1);
			return;
			IL_D4:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㉃⍅㩇㥉╋⅍㹏", a_));
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x0039CF00 File Offset: 0x0039BF00
		~XlsWorkbook()
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
			this.Close();
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x0039CF5C File Offset: 0x0039BF5C
		private void ᜀ(sprἛ A_0)
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
			this.ᜀ(A_0, ExcelParseOptions.Default, null);
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x0039CFA4 File Offset: 0x0039BFA4
		private IDecryptor ᜀ(sprἛ A_0, ExcelParseOptions A_1, string A_2)
		{
			switch (0)
			{
			default:
			{
				IDecryptor decryptor;
				List<BiffRecordRaw> list2;
				for (;;)
				{
					this.ᝆ = true;
					bool flag = true;
					List<sprᬐ> list = new List<sprᬐ>();
					decryptor = null;
					this.\u1733 = new List<BiffRecordRaw>(128);
					this.\u173C.Clear();
					this.ᝢ.Clear();
					this.ᝤ.Clear();
					this.\u1759.Clear();
					this.\u1737.Clear();
					this.\u1738.Clear();
					A_0.ᜁ();
					bool flag2 = false;
					bool flag3 = false;
					int num = 123;
					for (;;)
					{
						TBIFFRecord typeCode;
						int num2;
						BiffRecordRaw biffRecordRaw;
						Dictionary<int, int> dictionary;
						sprᦖ sprᦖ;
						int num4;
						Dictionary<int, int> dictionary2;
						switch (num)
						{
						case 0:
							num = 29;
							continue;
						case 1:
							goto IL_45F;
						case 2:
							if (typeCode <= TBIFFRecord.ExtSST)
							{
								num = 55;
								continue;
							}
							num = 57;
							continue;
						case 3:
							goto IL_F9B;
						case 4:
							goto IL_F9B;
						case 5:
						{
							spr\u20A4.ᜀ[] array;
							num2 = array.Length;
							goto IL_982;
						}
						case 6:
							switch (typeCode)
							{
							case TBIFFRecord.FileSharing:
								this.ᝮ = (sprẋ)biffRecordRaw;
								num = 30;
								continue;
							case TBIFFRecord.WriteAccess:
								goto IL_F9B;
							default:
								num = 113;
								continue;
							}
							break;
						case 7:
							goto IL_F9B;
						case 8:
							goto IL_F9B;
						case 9:
							if (typeCode != TBIFFRecord.Compatibility)
							{
								num = 32;
								continue;
							}
							this.ណ = (sprᬡ)biffRecordRaw;
							num = 27;
							continue;
						case 10:
							goto IL_F9B;
						case 11:
							goto IL_F9B;
						case 12:
							if (typeCode != TBIFFRecord.SST)
							{
								num = 31;
								continue;
							}
							this.ᜀ(flag2, list, dictionary);
							flag3 = true;
							this.ᜀ((sprỪ)biffRecordRaw, A_1);
							num = 111;
							continue;
						case 13:
							num = 41;
							continue;
						case 14:
							num = 59;
							continue;
						case 15:
							goto IL_F9B;
						case 16:
							if (!flag2)
							{
								num = 134;
								continue;
							}
							goto IL_F9B;
						case 17:
							goto IL_F9B;
						case 18:
							if (typeCode <= TBIFFRecord.CodeName)
							{
								num = 72;
								continue;
							}
							num = 48;
							continue;
						case 19:
							goto IL_F9B;
						case 20:
							goto IL_E88;
						case 21:
							switch (typeCode)
							{
							case TBIFFRecord.FilePass:
							{
								FilePassRecord a_ = (FilePassRecord)biffRecordRaw;
								decryptor = this.ᜀ(A_2, a_);
								num = 84;
								continue;
							}
							case (TBIFFRecord)48:
								goto IL_F9B;
							case TBIFFRecord.Font:
								num = 16;
								continue;
							default:
								num = 125;
								continue;
							}
							break;
						case 22:
							if (Array.IndexOf<TBIFFRecord>(XlsWorkbook.ᜫ, biffRecordRaw.TypeCode) != -1)
							{
								num = 76;
								continue;
							}
							goto IL_DFA;
						case 23:
							goto IL_F9B;
						case 24:
							num = 58;
							continue;
						case 25:
							goto IL_F9B;
						case 26:
							if (typeCode <= TBIFFRecord.WindowProtect)
							{
								num = 95;
								continue;
							}
							num = 128;
							continue;
						case 27:
							goto IL_F9B;
						case 28:
							num = 6;
							continue;
						case 29:
						{
							if (typeCode != TBIFFRecord.Format)
							{
								num = 24;
								continue;
							}
							spr\u240D a_2 = (spr\u240D)biffRecordRaw;
							this.\u173A.ᜁ(a_2);
							num = 19;
							continue;
						}
						case 30:
							goto IL_F9B;
						case 31:
							num = 114;
							continue;
						case 32:
							num = 107;
							continue;
						case 33:
						{
							if (typeCode != TBIFFRecord.SupBook)
							{
								num = 132;
								continue;
							}
							long position;
							A_0.ᜈ().Position = position;
							this.ᝢ.ᜀ(A_0, decryptor);
							num = 88;
							continue;
						}
						case 34:
							num = 127;
							continue;
						case 35:
							num = 17;
							continue;
						case 36:
							num = 138;
							continue;
						case 37:
							num = 73;
							continue;
						case 38:
							if (typeCode != TBIFFRecord.HasBasic)
							{
								num = 36;
								continue;
							}
							this.ᝋ = true;
							num = 23;
							continue;
						case 39:
							goto IL_F9B;
						case 40:
							num = 38;
							continue;
						case 41:
						{
							if (typeCode != TBIFFRecord.Style)
							{
								num = 0;
								continue;
							}
							sprᬐ item = (sprᬐ)biffRecordRaw;
							list.Add(item);
							num = 98;
							continue;
						}
						case 42:
							goto IL_DFA;
						case 43:
							goto IL_A7D;
						case 44:
							if (this.\u173E != null)
							{
								num = 89;
								continue;
							}
							goto IL_D58;
						case 45:
							goto IL_F9B;
						case 46:
							if (this.\u173E.ᜅ() == 0)
							{
								num = 106;
								continue;
							}
							this.\u173E.ᜀ(sprᦖ.ᜀ());
							num = 10;
							continue;
						case 47:
							num = 91;
							continue;
						case 48:
							if (typeCode <= TBIFFRecord.Format)
							{
								num = 115;
								continue;
							}
							num = 112;
							continue;
						case 49:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CBF;
							default:
								if (false)
								{
								}
								goto IL_F9B;
							}
							break;
						case 50:
						{
							if (typeCode != TBIFFRecord.HeaderFooterImage)
							{
								num = 121;
								continue;
							}
							spr\u1976 a_3 = (spr\u1976)biffRecordRaw;
							this.ᝤ.ᜀ(a_3);
							num = 99;
							continue;
						}
						case 51:
							goto IL_F9B;
						case 52:
						{
							spr\u20A4.ᜀ[] array;
							if (array == null)
							{
								num = 37;
								continue;
							}
							num = 5;
							continue;
						}
						case 53:
							if (typeCode != TBIFFRecord.BOF)
							{
								num = 66;
								continue;
							}
							num = 96;
							continue;
						case 54:
							if (typeCode <= TBIFFRecord.ExtendedFormat)
							{
								num = 14;
								continue;
							}
							num = 18;
							continue;
						case 55:
							num = 131;
							continue;
						case 56:
							switch (typeCode)
							{
							case TBIFFRecord.Protect:
								this.ᝈ = ((spr\u1AE8)biffRecordRaw).ᜁ();
								num = 65;
								continue;
							case TBIFFRecord.Password:
								this.\u1755 = (spr\u24C3)biffRecordRaw;
								num = 51;
								continue;
							case TBIFFRecord.Header:
							case TBIFFRecord.Footer:
							case TBIFFRecord.ExternCount:
								goto IL_F9B;
							case TBIFFRecord.ExternSheet:
								sprᦖ = (sprᦖ)biffRecordRaw;
								num = 44;
								continue;
							case TBIFFRecord.Name:
								this.\u1739.Add((sprῚ)biffRecordRaw);
								num = 93;
								continue;
							case TBIFFRecord.WindowProtect:
								this.ᝉ = ((spr\u2520)biffRecordRaw).ᜁ();
								num = 61;
								continue;
							default:
								num = 100;
								continue;
							}
							break;
						case 57:
						{
							if (typeCode != TBIFFRecord.UseSelFS)
							{
								num = 103;
								continue;
							}
							sprồ sprồ = (sprồ)biffRecordRaw;
							this.ᝅ = sprồ.ᜁ();
							num = 137;
							continue;
						}
						case 58:
							goto IL_F9B;
						case 59:
							if (typeCode <= TBIFFRecord.WindowOne)
							{
								if (true)
								{
								}
								num = 82;
								continue;
							}
							num = 118;
							continue;
						case 60:
						{
							int num3;
							if (num3 >= num4)
							{
								num = 69;
								continue;
							}
							spr\u20A4.ᜀ[] array;
							spr\u20A4.ᜀ ᜀ = array[num3];
							int num5;
							this.SetPaletteColor(num5, Color.FromArgb((int)ᜀ.ᜃ, (int)ᜀ.ᜀ, (int)ᜀ.ᜁ, (int)ᜀ.ᜂ));
							num5++;
							num3++;
							num = 1;
							continue;
						}
						case 61:
							goto IL_F9B;
						case 62:
							if (((spr\u24AD)biffRecordRaw).ᜁ())
							{
								num = 20;
								continue;
							}
							goto IL_F9B;
						case 63:
							goto IL_F9B;
						case 64:
							num = 53;
							continue;
						case 65:
							goto IL_F9B;
						case 66:
							num = 67;
							continue;
						case 67:
							if (typeCode != TBIFFRecord.BookExt)
							{
								num = 78;
								continue;
							}
							this.\u177F = biffRecordRaw;
							num = 4;
							continue;
						case 68:
							if (typeCode != TBIFFRecord.CodeName)
							{
								num = 71;
								continue;
							}
							this.ᝊ = ((spr\u2384)biffRecordRaw).ᜀ();
							num = 25;
							continue;
						case 69:
							num = 105;
							continue;
						case 70:
							goto IL_F9B;
						case 71:
							num = 81;
							continue;
						case 72:
							num = 2;
							continue;
						case 73:
							num2 = 0;
							goto IL_982;
						case 74:
							if (typeCode != TBIFFRecord.UnkMacrosDisable)
							{
								num = 13;
								continue;
							}
							this.ᝎ = true;
							num = 75;
							continue;
						case 75:
							goto IL_F9B;
						case 76:
							list2.Add(biffRecordRaw);
							num = 42;
							continue;
						case 77:
							goto IL_F9B;
						case 78:
							num = 87;
							continue;
						case 79:
							if (typeCode != TBIFFRecord.EOF)
							{
								num = 102;
								continue;
							}
							goto IL_E88;
						case 80:
							dictionary2 = null;
							goto IL_6C1;
						case 81:
							goto IL_F9B;
						case 82:
							num = 26;
							continue;
						case 83:
							this.ᜀ(flag2, list, dictionary);
							num = 43;
							continue;
						case 84:
							goto IL_F9B;
						case 85:
							dictionary2 = new Dictionary<int, int>();
							goto IL_6C1;
						case 86:
							num = 129;
							continue;
						case 87:
							goto IL_F9B;
						case 88:
							goto IL_F9B;
						case 89:
							num = 46;
							continue;
						case 90:
							goto IL_45F;
						case 91:
						{
							if (!flag)
							{
								num = 110;
								continue;
							}
							long position = A_0.ᜈ().Position;
							biffRecordRaw = A_0.ᜀ(decryptor);
							this.\u1733.Add(biffRecordRaw);
							num = 22;
							continue;
						}
						case 92:
							num = 21;
							continue;
						case 93:
							goto IL_F9B;
						case 94:
							if (num4 > 0)
							{
								num = 117;
								continue;
							}
							goto IL_F9B;
						case 95:
							num = 79;
							continue;
						case 96:
							if (((sprḯ)biffRecordRaw).ᜉ() != sprḯ.TType.TYPE_WORKBOOK)
							{
								num = 124;
								continue;
							}
							goto IL_F9B;
						case 97:
							goto IL_F9B;
						case 98:
							goto IL_F9B;
						case 99:
							goto IL_F9B;
						case 100:
							num = 3;
							continue;
						case 101:
							num = 56;
							continue;
						case 102:
							goto IL_CBF;
						case 103:
							num = 33;
							continue;
						case 104:
							num = 80;
							continue;
						case 105:
							goto IL_F9B;
						case 106:
							goto IL_D58;
						case 107:
							goto IL_F9B;
						case 108:
							if (typeCode != TBIFFRecord.WindowOne)
							{
								num = 139;
								continue;
							}
							this.ᝑ = (spr\u17B5)biffRecordRaw;
							num = 120;
							continue;
						case 109:
						{
							if (typeCode != TBIFFRecord.Precision)
							{
								num = 101;
								continue;
							}
							sprᣰ sprᣰ = (sprᣰ)biffRecordRaw;
							this.IsDisplayPrecision = (sprᣰ.ᜀ() == 0);
							num = 8;
							continue;
						}
						case 110:
							goto IL_1012;
						case 111:
							goto IL_F9B;
						case 112:
							if (typeCode <= TBIFFRecord.BookExt)
							{
								num = 64;
								continue;
							}
							num = 50;
							continue;
						case 113:
							num = 116;
							continue;
						case 114:
							if (typeCode != TBIFFRecord.ExtSST)
							{
								num = 86;
								continue;
							}
							num = 62;
							continue;
						case 115:
							num = 74;
							continue;
						case 116:
							switch (typeCode)
							{
							case TBIFFRecord.BoundSheet:
								this.\u173C.Add((spr\u17C1)biffRecordRaw);
								num = 15;
								continue;
							case TBIFFRecord.WriteProtection:
								this.\u176D = true;
								num = 126;
								continue;
							default:
								num = 119;
								continue;
							}
							break;
						case 117:
						{
							int num5 = 8;
							int num3 = 0;
							num = 90;
							continue;
						}
						case 118:
							if (typeCode <= TBIFFRecord.Country)
							{
								num = 28;
								continue;
							}
							num = 136;
							continue;
						case 119:
							num = 130;
							continue;
						case 120:
							goto IL_F9B;
						case 121:
							num = 9;
							continue;
						case 122:
							num = 12;
							continue;
						case 123:
							if (!flag2)
							{
								num = 104;
								continue;
							}
							num = 85;
							continue;
						case 124:
							goto IL_BF4;
						case 125:
							num = 108;
							continue;
						case 126:
							goto IL_F9B;
						case 127:
							goto IL_F9B;
						case 128:
							if (typeCode != TBIFFRecord.DateWindow1904)
							{
								num = 92;
								continue;
							}
							this.ᝁ = ((spr\u17DE)biffRecordRaw).ᜃ();
							num = 49;
							continue;
						case 129:
							goto IL_F9B;
						case 130:
						{
							if (typeCode != TBIFFRecord.Country)
							{
								num = 34;
								continue;
							}
							spr\u2338 spr_u = (spr\u2338)biffRecordRaw;
							this.ឃ = (int)spr_u.ᜃ();
							this.\u173A.ᜂ(this.ឃ);
							num = 77;
							continue;
						}
						case 131:
						{
							if (typeCode != TBIFFRecord.MSODrawingGroup)
							{
								num = 122;
								continue;
							}
							spr\u23E6 a_4 = this.ឌ = (spr\u23E6)biffRecordRaw;
							this.\u1759.ᜀ(a_4);
							num = 11;
							continue;
						}
						case 132:
							num = 68;
							continue;
						case 133:
							if (!A_0.ᜂ())
							{
								num = 47;
								continue;
							}
							goto IL_1044;
						case 134:
							this.\u1737.ForceAdd(base.AppImplementation.ᜀ(this, (spr\u2267)biffRecordRaw));
							num = 70;
							continue;
						case 135:
							if (!flag3)
							{
								num = 83;
								continue;
							}
							goto IL_A7D;
						case 136:
						{
							if (typeCode != TBIFFRecord.Palette)
							{
								num = 40;
								continue;
							}
							spr\u20A4.ᜀ[] array = ((spr\u20A4)biffRecordRaw).ᜀ();
							num = 52;
							continue;
						}
						case 137:
							goto IL_F9B;
						case 138:
						{
							if (typeCode != TBIFFRecord.ExtendedFormat)
							{
								num = 35;
								continue;
							}
							sprỶ item2 = (sprỶ)biffRecordRaw;
							this.\u175E.Add(item2);
							num = 45;
							continue;
						}
						case 139:
							num = 97;
							continue;
						}
						break;
						IL_45F:
						num = 60;
						continue;
						IL_6C1:
						dictionary = dictionary2;
						list2 = new List<BiffRecordRaw>();
						num = 7;
						continue;
						IL_982:
						num4 = num2;
						num = 94;
						continue;
						IL_A7D:
						this.ᜁ(A_0, A_1, -1, -1, dictionary, decryptor);
						flag = false;
						num = 63;
						continue;
						IL_CBF:
						num = 109;
						continue;
						IL_D58:
						this.\u173E = sprᦖ;
						num = 39;
						continue;
						IL_DFA:
						typeCode = biffRecordRaw.TypeCode;
						num = 54;
						continue;
						IL_E88:
						num = 135;
						continue;
						IL_F9B:
						num = 133;
					}
				}
				IL_BF4:
				throw new spr\u2317();
				IL_1012:
				IL_1044:
				this.\u1733 = null;
				this.\u173C.Clear();
				((spr\u17FF)base.ReservedHandle).ᜀ(this);
				this.ᝆ = false;
				this.ᜊ();
				this.ᜎ();
				this.ᜁ(list2);
				return decryptor;
			}
			}
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x0039E034 File Offset: 0x0039D034
		private void ᜁ(List<BiffRecordRaw> A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					List<spr\u257E> list = this.ᜀ(A_0);
					int i = 0;
					int count = list.Count;
					int num = 0;
					for (;;)
					{
						IL_10:
						switch (num)
						{
						case 0:
							goto IL_43;
						case 1:
							return;
						case 2:
							goto IL_43;
						case 3:
							while (i < count)
							{
								if (true)
								{
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								}
								if (false)
								{
								}
								spr\u257E spr_u257E = list[i];
								int a_ = spr_u257E.ᜂ();
								XlsPivotCache xlsPivotCache = this.ᝦ[a_];
								xlsPivotCache.Info = spr_u257E;
								i++;
								num = 2;
								goto IL_10;
							}
							num = 1;
							continue;
						}
						break;
						IL_43:
						num = 3;
					}
				}
				return;
			}
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x0039E104 File Offset: 0x0039D104
		private List<spr\u257E> ᜀ(List<BiffRecordRaw> A_0)
		{
			switch (0)
			{
			default:
			{
				List<spr\u257E> list;
				for (;;)
				{
					int num = 0;
					int count = A_0.Count;
					list = new List<spr\u257E>();
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 6;
								continue;
							}
							num2 = 4;
							continue;
						case 1:
							goto IL_6E;
						case 2:
							if (true)
							{
							}
							goto IL_BC;
						case 3:
							goto IL_BC;
						case 4:
							if (A_0[num].TypeCode == TBIFFRecord.StreamId)
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 5;
							continue;
						case 5:
							goto IL_BC;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6E;
							default:
								goto IL_F0;
							}
							break;
						}
						break;
						IL_6E:
						spr\u257E spr_u257E = new spr\u257E();
						num = spr_u257E.ᜀ(A_0, num);
						list.Add(spr_u257E);
						num2 = 3;
						continue;
						IL_BC:
						num2 = 0;
					}
				}
				IL_F0:
				if (false)
				{
				}
				return list;
			}
			}
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x0039E208 File Offset: 0x0039D208
		private void ᜀ(sprỶ A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!A_0.ᜥ())
					{
						num = 5;
						continue;
					}
					goto IL_198;
				case 1:
				{
					sprỶ sprỶ;
					if (A_0.ᜐ() != sprỶ.ᜐ())
					{
						num = 13;
						continue;
					}
					goto IL_198;
				}
				case 2:
					goto IL_10F;
				case 3:
				{
					sprỶ sprỶ;
					if (A_0.ᜈ() == sprỶ.ᜈ())
					{
						num = 7;
						continue;
					}
					goto IL_E1;
				}
				case 5:
					num = 9;
					continue;
				case 6:
				{
					sprỶ sprỶ = this.\u175E[(int)A_0.\u1713()];
					num = 8;
					continue;
				}
				case 7:
					num = 11;
					continue;
				case 8:
				{
					sprỶ sprỶ;
					if (A_0.ᜋ() == sprỶ.ᜋ())
					{
						num = 10;
						continue;
					}
					goto IL_E1;
				}
				case 9:
					if ((int)A_0.\u1713() != this.MaxXFCount)
					{
						num = 6;
						continue;
					}
					goto IL_198;
				case 10:
					goto IL_92;
				case 11:
				{
					sprỶ sprỶ;
					if (A_0.ᜭ() == sprỶ.ᜭ())
					{
						num = 12;
						continue;
					}
					goto IL_E1;
				}
				case 12:
					num = 1;
					continue;
				case 13:
					goto IL_E1;
				case 14:
					num = 0;
					continue;
				}
				if (A_0.ᜎ() == sprỶ.TXFType.XF_STYLE)
				{
					num = 14;
					continue;
				}
				break;
				IL_92:
				num = 3;
				continue;
				IL_E1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_92;
				default:
					if (false)
					{
					}
					A_0.ᜆ(true);
					num = 2;
					break;
				}
			}
			IL_10F:
			IL_198:
			if (true)
			{
			}
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x0039E3B8 File Offset: 0x0039D3B8
		private IDecryptor ᜀ(string A_0, FilePassRecord A_1)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 4;
				IDecryptor decryptor;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_134;
					case 1:
						goto IL_168;
					case 2:
					{
						PasswordRequiredEventArgs passwordRequiredEventArgs;
						if (base.AppImplementation.ᜀ(this, passwordRequiredEventArgs))
						{
							num = 9;
							continue;
						}
						goto IL_EF;
					}
					case 3:
					{
						PasswordRequiredEventArgs passwordRequiredEventArgs;
						if (!passwordRequiredEventArgs.StopParsing)
						{
							num = 13;
							continue;
						}
						goto IL_EF;
					}
					case 5:
					{
						byte[] array;
						byte[] encryptedDocId;
						byte[] digest;
						if (decryptor.SetDecryptionInfo(array, encryptedDocId, digest, A_0))
						{
							num = 8;
							continue;
						}
						PasswordRequiredEventArgs passwordRequiredEventArgs = new PasswordRequiredEventArgs();
						num = 2;
						continue;
					}
					case 6:
					{
						sprṺ sprṺ;
						if (sprṺ == null)
						{
							num = 10;
							continue;
						}
						PasswordRequiredEventArgs passwordRequiredEventArgs = null;
						decryptor = null;
						this.ᜀ(ref A_0, ref decryptor, sprṺ);
						num = 7;
						continue;
					}
					case 7:
						if (decryptor == null)
						{
							num = 12;
							continue;
						}
						goto IL_254;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_163;
						default:
							goto IL_1AC;
						}
						break;
					case 9:
						goto IL_163;
					case 10:
						goto IL_24F;
					case 11:
					{
						if (A_1.IsWeakEncryption)
						{
							num = 0;
							continue;
						}
						sprṺ sprṺ = A_1.StandardBlock;
						num = 6;
						continue;
					}
					case 12:
					{
						decryptor = new spr\u22F6();
						sprṺ sprṺ;
						byte[] array = sprṺ.ᜂ();
						this.\u1774 = array;
						byte[] encryptedDocId = sprṺ.ᜁ();
						byte[] digest = sprṺ.ᜀ();
						num = 15;
						continue;
					}
					case 13:
					{
						PasswordRequiredEventArgs passwordRequiredEventArgs;
						A_0 = passwordRequiredEventArgs.NewPassword;
						decryptor = new spr\u22F6();
						num = 1;
						continue;
					}
					case 14:
						goto IL_82;
					case 15:
						goto IL_168;
					}
					if (A_1 == null)
					{
						num = 14;
						continue;
					}
					num = 11;
					continue;
					IL_163:
					num = 3;
					continue;
					IL_168:
					if (true)
					{
					}
					num = 5;
				}
				IL_82:
				throw new ArgumentNullException(RecordTableEnumerator.b("⹇⍉⁋⭍O㍑❓╕", a_));
				IL_EF:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㡇⭉㽋㵍❏㵑♓㉕", a_), RecordTableEnumerator.b("὇㡉⍋⁍㝏牑⑓㝕⭗⥙⭛ㅝ቟١䩣", a_));
				IL_134:
				throw new NotSupportedException(RecordTableEnumerator.b("὇⽉ⵋ╍灏㝑㩓㕕⩗⍙ⱛ⩝य़ൡ੣䙥१٩୫ŭɯ᭱sṵᕷ婹ᕻൽꁿꢇ黎曆ﺍ﶑ﶗﺙ늛", a_));
				IL_1AC:
				if (false)
				{
				}
				goto IL_254;
				IL_24F:
				throw new NotSupportedException(RecordTableEnumerator.b("ᭇ㹉㹋⅍㹏㕑瑓㍕㙗㥙⹛❝ၟᙡൣ॥٧䩩൫ɭᝯᵱٳή౷ቹᅻൽꁿꢇ揄낏얟욡誣", a_));
				IL_254:
				this.\u1772 = A_0;
				this.\u1773 = EncryptionType.Standard;
				return decryptor;
			}
			}
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x0039E628 File Offset: 0x0039D628
		private void ᜀ(ref string A_0, ref IDecryptor A_1, sprṺ A_2)
		{
			int a_ = 17;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_12C;
				case 1:
				{
					PasswordRequiredEventArgs passwordRequiredEventArgs;
					if (base.AppImplementation.ᜁ(this, passwordRequiredEventArgs))
					{
						num = 12;
						continue;
					}
					passwordRequiredEventArgs = null;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_164;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 3:
					goto IL_89;
				case 4:
				{
					PasswordRequiredEventArgs passwordRequiredEventArgs = new PasswordRequiredEventArgs();
					num = 1;
					continue;
				}
				case 5:
					if (A_0 == null)
					{
						num = 0;
						continue;
					}
					return;
				case 6:
					this.ᜀ(ref A_1, A_2);
					if (true)
					{
					}
					num = 13;
					continue;
				case 7:
					goto IL_89;
				case 8:
					num = 10;
					continue;
				case 9:
				{
					PasswordRequiredEventArgs passwordRequiredEventArgs;
					if (passwordRequiredEventArgs != null)
					{
						num = 8;
						continue;
					}
					goto IL_D1;
				}
				case 10:
				{
					PasswordRequiredEventArgs passwordRequiredEventArgs;
					if (!passwordRequiredEventArgs.StopParsing)
					{
						num = 11;
						continue;
					}
					goto IL_D1;
				}
				case 11:
					num = 5;
					continue;
				case 12:
				{
					PasswordRequiredEventArgs passwordRequiredEventArgs;
					A_0 = passwordRequiredEventArgs.NewPassword;
					goto IL_164;
				}
				case 13:
					if (A_1 == null)
					{
						num = 4;
						continue;
					}
					return;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				return;
				IL_89:
				num = 9;
				continue;
				IL_164:
				num = 3;
			}
			IL_D1:
			throw new ArgumentException(RecordTableEnumerator.b("၆♈㥊♌ⵎ㹐㱒㹔睖じ⡚絜⽞፠ౢᅤɦ੨Ὢ࡬୮兰ቲ᭴፶奸୺ᱼ౾ꮊ戴ﶒ는릘爵슠쪢쎤캦첨쾪莬", a_));
			IL_12C:
			goto IL_D1;
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x0039E7AC File Offset: 0x0039D7AC
		private bool ᜀ(ref IDecryptor A_0, sprṺ A_1)
		{
			int a_ = 6;
			for (;;)
			{
				A_0 = new spr\u22F6();
				byte[] array = A_1.ᜂ();
				this.\u1774 = array;
				byte[] encryptedDocId = A_1.ᜁ();
				byte[] digest = A_1.ᜀ();
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_91:
					A_0 = null;
					num = 0;
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A1;
					case 1:
						goto IL_91;
					case 2:
						if (!A_0.SetDecryptionInfo(array, encryptedDocId, digest, RecordTableEnumerator.b("樻嬽ⰿ㑁⅃㉅ᭇ㵉⥋⽍⑏⅑㱓㥕⡗", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_AD;
					}
					break;
				}
			}
			IL_A1:
			IL_AD:
			return A_0 != null;
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x0039E870 File Offset: 0x0039D870
		private IEncryptor ᜏ()
		{
			int a_ = 12;
			if (true)
			{
			}
			int num = 3;
			IEncryptor encryptor;
			for (;;)
			{
				switch (num)
				{
				case 0:
					encryptor = new spr\u22F6();
					num = 2;
					continue;
				case 1:
					this.\u1774 = Guid.NewGuid().ToByteArray();
					goto IL_72;
				case 2:
					if (this.\u1774 == null)
					{
						num = 1;
						continue;
					}
					goto IL_4B;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_72;
					default:
						goto IL_90;
					}
					break;
				}
				if (this.\u1773 == EncryptionType.Standard)
				{
					num = 0;
					continue;
				}
				goto IL_C8;
				IL_72:
				num = 4;
			}
			IL_4B:
			encryptor.SetEncryptionInfo(this.\u1774, this.\u1772);
			return encryptor;
			IL_90:
			if (false)
			{
			}
			goto IL_4B;
			IL_C8:
			throw new NotSupportedException(RecordTableEnumerator.b("ు⭃㉅桇㥉㥋㹍⁏㵑♓≕㵗㹙籛㭝๟šᙣὥᡧṩիŭṯ剱sཱུࡷό剻", a_));
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x0039E958 File Offset: 0x0039D958
		private void ᜀ(sprỪ A_0, ExcelParseOptions A_1)
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
			this.\u173D.OriginalSST = A_0;
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x0039E9A0 File Offset: 0x0039D9A0
		private void ᜀ(bool A_0, List<sprᬐ> A_1, Dictionary<int, int> A_2)
		{
			for (;;)
			{
				this.ᜀ(A_0, A_1);
				this.\u173A.ᜅ();
				if (!A_0)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_4D;
				}
			}
			this.ᜀ(A_1);
			this.InsertDefaultExtFormats();
			this.ᜁ(A_1);
			return;
			IL_4D:
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜭ();
			this.InsertDefaultExtFormats();
			this.InsertDefaultStyles();
			this.ᜀ(A_2);
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x0039EA24 File Offset: 0x0039DA24
		private void ᜀ(bool A_0, List<sprᬐ> A_1)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int num = 22;
				for (;;)
				{
					int num3;
					sprỶ sprỶ;
					sprỶ sprỶ2;
					switch (num)
					{
					case 0:
						goto IL_254;
					case 1:
						goto IL_19A;
					case 2:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 21;
							continue;
						}
						this.\u1738.ᜁ(num2).\u1735();
						num2++;
						num = 1;
						continue;
					}
					case 3:
						goto IL_AB;
					case 4:
						num = 13;
						continue;
					case 5:
					{
						int count;
						if (num3 >= count)
						{
							num = 17;
							continue;
						}
						sprỶ = this.\u175E[num3];
						int num4 = (int)sprỶ.\u1713();
						num = 6;
						continue;
					}
					case 6:
					{
						int num4;
						if (num4 != this.MaxXFCount)
						{
							num = 16;
							continue;
						}
						goto IL_AB;
					}
					case 7:
						if (!sprỶ.ᜆ())
						{
							num = 14;
							continue;
						}
						goto IL_E3;
					case 8:
						goto IL_E3;
					case 9:
						if (sprỶ.\u171D() != sprỶ2.\u171D())
						{
							num = 10;
							continue;
						}
						goto IL_254;
					case 10:
						sprỶ.ᜃ(true);
						num = 0;
						continue;
					case 11:
						if (sprỶ.ᜪ() != sprỶ2.ᜪ())
						{
							num = 18;
							continue;
						}
						goto IL_E3;
					case 12:
						sprỶ.ᜁ(true);
						num = 3;
						continue;
					case 13:
						goto IL_30E;
					case 14:
						num = 11;
						continue;
					case 15:
						if (!sprỶ.\u1715())
						{
							num = 4;
							continue;
						}
						goto IL_AB;
					case 16:
					{
						int num4;
						sprỶ2 = this.\u175E[num4];
						num = 20;
						continue;
					}
					case 17:
					{
						int num2 = 0;
						num = 26;
						continue;
					}
					case 18:
						sprỶ.\u170D(true);
						num = 8;
						continue;
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30E;
						default:
							if (false)
							{
							}
							goto IL_230;
						}
						break;
					case 20:
						if (!sprỶ.ᜃ())
						{
							num = 24;
							continue;
						}
						goto IL_254;
					case 21:
						return;
					case 23:
					{
						int count = this.\u175E.Count;
						num3 = 0;
						num = 19;
						continue;
					}
					case 24:
						num = 9;
						continue;
					case 25:
						goto IL_230;
					case 26:
						goto IL_19A;
					}
					if (!A_0)
					{
						num = 23;
						continue;
					}
					break;
					IL_AB:
					spr\u192F a_ = new spr\u192F((spr\u2158)base.ReservedHandle, this, sprỶ, true);
					this.\u1738.ᜀ(a_);
					num3++;
					num = 25;
					continue;
					IL_E3:
					num = 15;
					continue;
					IL_19A:
					num = 2;
					continue;
					IL_230:
					num = 5;
					continue;
					IL_254:
					num = 7;
					continue;
					IL_30E:
					if (sprỶ.ᜂ() == sprỶ2.ᜂ())
					{
						goto IL_AB;
					}
					num = 12;
				}
				return;
			}
			}
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x0039ED64 File Offset: 0x0039DD64
		private void ᜎ()
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				if (true)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (false)
							{
							}
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							XlsWorksheet xlsWorksheet = (XlsWorksheet)this.\u1735[num];
							xlsWorksheet.ParseAutoFilters();
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
					case 1:
						goto IL_38;
					case 2:
						goto IL_38;
					case 3:
						return;
					}
					break;
					IL_38:
					num2 = 0;
				}
			}
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x0039EE10 File Offset: 0x0039DE10
		private void ᜁ(sprἛ A_0, ExcelParseOptions A_1, int A_2, int A_3, Dictionary<int, int> A_4, IDecryptor A_5)
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
			this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5);
			this.\u170D();
			this.ᜌ();
			this.ᜋ();
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x0039EE6C File Offset: 0x0039DE6C
		private void ᜀ(sprἛ A_0, ExcelParseOptions A_1, int A_2, int A_3, Dictionary<int, int> A_4, IDecryptor A_5)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					long length = A_0.ᜈ().Length;
					int num = -1;
					int num2 = 23;
					for (;;)
					{
						sprḯ.TType ttype;
						bool flag;
						spr\u17C1 spr_u17C;
						ITabSheet tabSheet;
						bool flag2;
						sprḯ sprḯ;
						switch (num2)
						{
						case 0:
							goto IL_19B;
						case 1:
							if (ttype != sprḯ.TType.TYPE_CHART)
							{
								XlsWorksheet xlsWorksheet = base.AppImplementation.ᜀ(this, A_0, A_1, flag, A_4, A_5);
								xlsWorksheet.Type = (ExcelSheetType)spr_u17C.ᜆ();
								tabSheet = xlsWorksheet;
								num2 = 5;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17D;
							default:
								if (false)
								{
								}
								num2 = 22;
								continue;
							}
							break;
						case 2:
							flag2 = true;
							goto IL_26D;
						case 3:
							num2 = 1;
							continue;
						case 4:
							goto IL_17D;
						case 5:
						{
							XlsWorksheet xlsWorksheet;
							if (xlsWorksheet.Type == ExcelSheetType.NormalWorksheet)
							{
								num2 = 15;
								continue;
							}
							this.\u1754.ᜀ(xlsWorksheet);
							num2 = 4;
							continue;
						}
						case 6:
							num2 = 9;
							continue;
						case 7:
						{
							if (ttype != sprḯ.TType.TYPE_WORKSHEET)
							{
								num2 = 3;
								continue;
							}
							IWorksheet worksheet = this.\u1735.ᜀ(A_0, A_1, flag, A_4, A_5);
							tabSheet = worksheet;
							((XlsWorksheet)worksheet).Type = (ExcelSheetType)spr_u17C.ᜆ();
							num2 = 18;
							continue;
						}
						case 8:
							if (!A_0.ᜂ())
							{
								num2 = 10;
								continue;
							}
							goto IL_CC;
						case 9:
							if (num >= this.\u173C.Count - 1)
							{
								num2 = 21;
								continue;
							}
							goto IL_19B;
						case 10:
						{
							BiffRecordRaw biffRecordRaw = A_0.ᜊ();
							num2 = 17;
							continue;
						}
						case 11:
							goto IL_162;
						case 12:
							goto IL_CC;
						case 13:
							flag2 = (num > A_3);
							goto IL_26D;
						case 14:
							goto IL_2AB;
						case 15:
						{
							XlsWorksheet xlsWorksheet;
							this.\u1735.ᜁ(xlsWorksheet);
							num2 = 25;
							continue;
						}
						case 16:
							if (!A_0.ᜂ())
							{
								num2 = 6;
								continue;
							}
							return;
						case 17:
						{
							BiffRecordRaw biffRecordRaw;
							if (biffRecordRaw.TypeCode != TBIFFRecord.BOF)
							{
								num2 = 11;
								continue;
							}
							sprḯ = (sprḯ)biffRecordRaw;
							num2 = 12;
							continue;
						}
						case 18:
							goto IL_2AB;
						case 19:
							if (num >= A_2)
							{
								num2 = 20;
								continue;
							}
							num2 = 2;
							continue;
						case 20:
							num2 = 13;
							continue;
						case 21:
							return;
						case 22:
						{
							IChart chart = this.\u1753.ᜀ(A_0, A_1, flag, A_4, A_5);
							tabSheet = (chart as ITabSheet);
							num2 = 14;
							continue;
						}
						case 23:
							if (A_3 == -1)
							{
								num2 = 24;
								continue;
							}
							goto IL_372;
						case 24:
							A_3 = int.MaxValue;
							num2 = 26;
							continue;
						case 25:
							if (true)
							{
							}
							goto IL_2AB;
						case 26:
							goto IL_372;
						}
						break;
						IL_CC:
						num++;
						num2 = 19;
						continue;
						IL_19B:
						num2 = 8;
						continue;
						IL_26D:
						flag = flag2;
						spr_u17C = this.\u173C[num];
						tabSheet = null;
						ttype = sprḯ.ᜉ();
						num2 = 7;
						continue;
						IL_2AB:
						tabSheet.Name = spr_u17C.ᜅ();
						tabSheet.Visibility = spr_u17C.ᜄ();
						base.AppImplementation.ᜀ(A_0.ᜈ().Position, length);
						num2 = 16;
						continue;
						IL_17D:
						goto IL_2AB;
						IL_372:
						sprḯ = null;
						int count = this.\u173C.Count;
						this.\u1754.EnsureCapacity(count);
						this.\u1735.EnsureCapacity(count);
						num2 = 0;
					}
				}
				IL_162:
				throw new sprᢺ();
			}
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x0039F254 File Offset: 0x0039E254
		private void \u170D()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int count = this.\u1739.Count;
					int num2 = 3;
					for (;;)
					{
						sprῚ sprῚ;
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							sprῚ = this.\u1739[num];
							num2 = 1;
							continue;
						case 1:
							goto IL_C5;
						case 2:
							return;
						case 3:
							goto IL_DB;
						case 4:
							goto IL_5B;
						case 5:
							goto IL_5B;
						case 6:
							goto IL_DB;
						case 7:
							this.\u1752.ᜀ(sprῚ);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C5;
							}
							if (false)
							{
							}
							num2 = 5;
							continue;
						}
						break;
						IL_5B:
						num++;
						num2 = 6;
						continue;
						IL_C5:
						if (sprῚ.ᜃ() == 0)
						{
							num2 = 7;
							continue;
						}
						IWorksheet worksheet = (IWorksheet)this.\u1754[(int)(sprῚ.ᜃ() - 1)];
						sprᤗ sprᤗ = (sprᤗ)((XlsWorksheet)worksheet).Names;
						sprᤗ.ᜀ(sprῚ);
						num2 = 4;
						continue;
						IL_DB:
						num2 = 0;
					}
				}
				return;
			}
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x0039F3A0 File Offset: 0x0039E3A0
		private void ᜌ()
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
			this.\u1752.ᜈ();
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x0039F3E8 File Offset: 0x0039E3E8
		private void ᜋ()
		{
			if (true)
			{
			}
			for (;;)
			{
				int num = 0;
				int count = this.\u1754.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 5;
						continue;
					case 1:
						goto IL_71;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6F;
						default:
							if (false)
							{
							}
							goto IL_71;
						}
						break;
					case 3:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						spr\u1D46 spr_u1D = (spr\u1D46)this.\u1754[num];
						spr_u1D.ᜀ();
						XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)spr_u1D;
						num++;
						num2 = 2;
						continue;
					}
					case 4:
						goto IL_6F;
					case 5:
						if (this.ᝑ.ᜊ() != 65535)
						{
							num2 = 4;
							continue;
						}
						goto IL_F5;
					}
					break;
					IL_71:
					num2 = 3;
				}
			}
			IL_6F:
			this.\u1734 = (XlsWorksheetBase)this.\u1754[(int)this.ᝑ.ᜊ()];
			return;
			IL_F5:
			((XlsWorksheetBase)this.\u1754[0]).Activate();
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x0039F500 File Offset: 0x0039E500
		private void ᜊ()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.\u173F.Clear();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				case 2:
					goto IL_41;
				case 3:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					sprṨ sprṨ = this.\u173F[num2];
					sprṨ.ᜀ();
					num2++;
					num = 5;
					continue;
				}
				case 5:
					goto IL_76;
				case 6:
				{
					int num2 = 0;
					int count = this.\u173F.Count;
					num = 2;
					continue;
				}
				}
				if (!this.ᝆ)
				{
					num = 6;
					continue;
				}
				break;
				IL_41:
				num = 3;
				continue;
				IL_76:
				goto IL_41;
			}
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x0039F5E8 File Offset: 0x0039E5E8
		private void ᜀ(List<sprᬐ> A_0)
		{
			int a_ = 4;
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int count = A_0.Count;
					int num2 = 13;
					for (;;)
					{
						sprᬐ sprᬐ;
						switch (num2)
						{
						case 0:
							goto IL_1A8;
						case 1:
							goto IL_25A;
						case 2:
							goto IL_153;
						case 3:
							if (sprᬐ.ᜂ().Length == 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_1A8;
						case 4:
						{
							if (num >= count)
							{
								if (true)
								{
								}
								num2 = 7;
								continue;
							}
							sprᬐ = A_0[num];
							int a_2 = (int)sprᬐ.ᜅ();
							spr\u192F spr_u192F = this.InnerExtFormats.ᜁ(a_2);
							num2 = 9;
							continue;
						}
						case 5:
							if (sprᬐ.ᜂ() != null)
							{
								num2 = 16;
								continue;
							}
							goto IL_7F;
						case 6:
						{
							XlsStyle xlsStyle = base.AppImplementation.ᜀ(this, sprᬐ);
							num2 = 11;
							continue;
						}
						case 7:
							return;
						case 8:
						{
							spr\u192F spr_u192F;
							spr\u192F spr_u192F2 = (spr\u192F)spr_u192F.\u1758();
							spr_u192F2.ᜄ(this.MaxXFCount);
							spr_u192F2.ᜑ().ᜀ(sprỶ.TXFType.XF_CELL);
							spr_u192F2 = this.\u1738.ᜀ(spr_u192F2);
							spr_u192F.ᜄ(spr_u192F2.ᜠ());
							sprᬐ.ᜀ((ushort)spr_u192F2.ᜠ());
							spr_u192F = spr_u192F2;
							num2 = 17;
							continue;
						}
						case 9:
						{
							spr\u192F spr_u192F;
							if (spr_u192F.ᝇ())
							{
								num2 = 8;
								continue;
							}
							goto IL_20D;
						}
						case 10:
							if (sprᬐ.ᜄ())
							{
								num2 = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_25A;
							default:
								if (false)
								{
								}
								num2 = 5;
								continue;
							}
							break;
						case 11:
						{
							XlsStyle xlsStyle;
							if (!this.\u1736.ᜁ(xlsStyle.Name))
							{
								num2 = 15;
								continue;
							}
							goto IL_153;
						}
						case 12:
							goto IL_153;
						case 13:
							goto IL_1C2;
						case 14:
							goto IL_1C2;
						case 15:
						{
							XlsStyle xlsStyle;
							this.\u1736.Add(xlsStyle, true);
							num2 = 2;
							continue;
						}
						case 16:
							num2 = 3;
							continue;
						case 17:
							goto IL_20D;
						}
						break;
						IL_7F:
						sprᬐ.ᜀ(CollectionExtended<XlsWorksheet>.GenerateDefaultName(A_0, RecordTableEnumerator.b("漹爻甽฿ുፃࡅᭇṉᕋɍᕏ൑", a_)));
						num2 = 0;
						continue;
						IL_153:
						num++;
						num2 = 14;
						continue;
						IL_1A8:
						this.\u1736.ᜀ(sprᬐ);
						num2 = 12;
						continue;
						IL_1C2:
						num2 = 4;
						continue;
						IL_20D:
						num2 = 10;
						continue;
						IL_25A:
						goto IL_7F;
					}
				}
				return;
			}
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x0039F8A4 File Offset: 0x0039E8A4
		private sprᬐ ᜀ(List<sprᬐ> A_0, int A_1, out int A_2)
		{
			switch (0)
			{
			default:
			{
				int num;
				int num3;
				sprᬐ sprᬐ;
				sprᬐ sprᬐ2;
				for (;;)
				{
					A_2 = -1;
					num = 0;
					int count = A_0.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_120;
						case 1:
							goto IL_13E;
						case 2:
						{
							int num4;
							if (num3 >= num4)
							{
								num2 = 1;
								continue;
							}
							sprᬐ[] array;
							sprᬐ = array[num3];
							num2 = 11;
							continue;
						}
						case 3:
							goto IL_14A;
						case 4:
							if ((int)sprᬐ2.ᜅ() == A_1)
							{
								num2 = 5;
								continue;
							}
							num++;
							num2 = 8;
							continue;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								goto IL_EB;
							}
							break;
						case 6:
							goto IL_11B;
						case 7:
							goto IL_120;
						case 8:
							goto IL_14A;
						case 9:
							if (num >= count)
							{
								num2 = 10;
								continue;
							}
							sprᬐ2 = A_0[num];
							num2 = 4;
							continue;
						case 10:
						{
							if (true)
							{
							}
							sprᬐ[] array = this.ᜉ();
							num3 = 0;
							int num4 = array.Length;
							num2 = 0;
							continue;
						}
						case 11:
							if ((int)sprᬐ.ᜅ() == A_1)
							{
								num2 = 6;
								continue;
							}
							num3++;
							num2 = 7;
							continue;
						}
						break;
						IL_120:
						num2 = 2;
						continue;
						IL_14A:
						num2 = 9;
					}
				}
				IL_EB:
				if (false)
				{
				}
				A_2 = num;
				return sprᬐ2;
				IL_11B:
				A_2 = -num3 - 1;
				return sprᬐ;
				IL_13E:
				return null;
			}
			}
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x0039FA34 File Offset: 0x0039EA34
		private sprᬐ[] ᜉ()
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
			List<sprᬐ> list = new List<sprᬐ>(7);
			sprᬐ sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(16);
			sprᬐ.ᜁ(3);
			list.Add(sprᬐ);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(17);
			sprᬐ.ᜁ(6);
			list.Add(sprᬐ);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(18);
			sprᬐ.ᜁ(4);
			list.Add(sprᬐ);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(19);
			sprᬐ.ᜁ(7);
			list.Add(sprᬐ);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			list.Add(sprᬐ);
			sprᬐ = (sprᬐ)spr\u175E.ᜀ(TBIFFRecord.Style);
			sprᬐ.ᜀ(20);
			sprᬐ.ᜁ(5);
			list.Add(sprᬐ);
			return list.ToArray();
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x0039FB54 File Offset: 0x0039EB54
		private void ᜀ(Dictionary<int, int> A_0)
		{
			int a_ = 16;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_45;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				IL_45:
				IEnumerator<KeyValuePair<int, sprᤅ>> enumerator = this.\u173A.ᜁ();
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 5;
								continue;
							}
							KeyValuePair<int, sprᤅ> keyValuePair = enumerator.Current;
							int key = keyValuePair.Key;
							string text = RecordTableEnumerator.b("E❇㡉⅋⽍⑏൑", a_) + key;
							num = 2;
							continue;
						}
						case 2:
						{
							string text;
							if (!this.\u1736.ᜁ(text))
							{
								num = 3;
								continue;
							}
							break;
						}
						case 3:
						{
							string text;
							XlsStyle xlsStyle = (XlsStyle)this.\u1736.Add(text, RecordTableEnumerator.b("ࡅ❇㡉⅋⽍㱏", a_));
							KeyValuePair<int, sprᤅ> keyValuePair;
							xlsStyle.NumberFormat = keyValuePair.Value.ᜂ();
							spr\u192F spr_u192F = xlsStyle.Wrapped;
							spr_u192F = spr_u192F.ᜭ();
							int value = spr_u192F.ᜠ();
							int key;
							A_0.Add(key, value);
							num = 0;
							continue;
						}
						case 5:
							num = 6;
							continue;
						case 6:
							goto IL_165;
						}
						IL_D0:
						num = 1;
						continue;
						goto IL_D0;
					}
					IL_165:;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							enumerator.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_1A4;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 0;
					}
					IL_1A4:;
				}
				return;
			}
			}
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x0039FD24 File Offset: 0x0039ED24
		private void ᜁ(spr\u20C3 A_0)
		{
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6F:
					num = 5;
					break;
				default:
					if (false)
					{
					}
					goto IL_40;
				}
				spr\u24E8 spr_u24E;
				bool flag;
				for (;;)
				{
					IL_1E:
					switch (num)
					{
					case 0:
						goto IL_97;
					case 1:
						goto IL_95;
					case 2:
						if (spr_u24E != null)
						{
							num = 6;
							continue;
						}
						goto IL_6F;
					case 3:
						this.ᜀ(A_0);
						num = 0;
						continue;
					case 4:
						goto IL_A2;
					case 5:
						if (!flag)
						{
							num = 3;
							continue;
						}
						goto IL_97;
					case 6:
						this.ᜀ(spr_u24E);
						flag = true;
						num = 1;
						continue;
					}
					goto IL_40;
					IL_97:
					num = 4;
				}
				IL_95:
				goto IL_6F;
				IL_A2:
				goto IL_A7;
				IL_40:
				flag = false;
				spr_u24E = (A_0 as spr\u24E8);
				num = 2;
				goto IL_1E;
			}
			catch (Exception)
			{
			}
			IL_A7:
			if (true)
			{
			}
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x0039FDFC File Offset: 0x0039EDFC
		private void ᜀ(spr\u24E8 A_0)
		{
			sprᮓ sprᮓ;
			for (;;)
			{
				int num = spr\u2019.ᜁ(A_0.ᜇ(), 0U, out sprᮓ);
				if (num == 0)
				{
					goto IL_43;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_28;
				}
			}
			IL_28:
			if (false)
			{
			}
			if (true)
			{
			}
			return;
			IL_43:
			this.ᝫ.ᜁ(sprᮓ);
			this.ᝬ.ᜁ(sprᮓ);
			Marshal.FinalReleaseComObject(sprᮓ);
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x0039FE6C File Offset: 0x0039EE6C
		private void ᜀ(spr\u20C3 A_0)
		{
			int a_ = 19;
			Stream stream2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
				{
					int num = 2;
					for (;;)
					{
						Stream stream;
						switch (num)
						{
						case 0:
							try
							{
								sprណ a_2 = new sprណ(stream);
								this.ᝫ.ᜀ(a_2);
								goto IL_AC;
							}
							finally
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_136;
									case 2:
										((IDisposable)stream).Dispose();
										num = 0;
										continue;
									}
									if (stream == null)
									{
										break;
									}
									num = 2;
								}
								IL_136:;
							}
							goto IL_139;
						case 1:
							goto IL_139;
						case 3:
							goto IL_A7;
						case 4:
							stream2 = A_0.ᜁ(RecordTableEnumerator.b("䱈ཊ≌ⱎ⑐㹒ご㥖ⵘ࡚⡜㉞ౠɢᝤṦ⁨ժ୬nͰṲᑴͶၸᑺ፼", a_));
							num = 3;
							continue;
						case 5:
							if (A_0.ᜃ(RecordTableEnumerator.b("䱈ཊ≌ⱎ⑐㹒ご㥖ⵘ࡚⡜㉞ౠɢᝤṦ⁨ժ୬nͰṲᑴͶၸᑺ፼", a_)))
							{
								num = 4;
								continue;
							}
							return;
						}
						if (true)
						{
						}
						if (A_0.ᜃ(RecordTableEnumerator.b("䱈ᡊ㡌≎㱐㉒❔⹖ၘ㕚㭜ぞ፠๢Ѥ፦hѪͬ", a_)))
						{
							num = 1;
							continue;
						}
						IL_AC:
						num = 5;
						continue;
						IL_139:
						stream = A_0.ᜁ(RecordTableEnumerator.b("䱈ᡊ㡌≎㱐㉒❔⹖ၘ㕚㭜ぞ፠๢Ѥ፦hѪͬ", a_));
						num = 0;
					}
					break;
				}
				}
				break;
			}
			IL_A7:
			try
			{
				sprណ a_3 = new sprណ(stream2);
				this.ᝫ.ᜀ(a_3);
				this.ᝬ.ᜀ(a_3);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1B8;
					case 1:
						((IDisposable)stream2).Dispose();
						num = 0;
						continue;
					}
					if (stream2 == null)
					{
						break;
					}
					num = 1;
				}
				IL_1B8:;
			}
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x003A0050 File Offset: 0x0039F050
		internal DataSorter ᜰ()
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_68:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_70;
				case 2:
					goto IL_50;
				}
				if (this.ទ != null)
				{
					goto IL_72;
				}
				num = 2;
			}
			IL_50:
			this.ទ = new DataSorter(this);
			goto IL_68;
			IL_70:
			IL_72:
			return this.ទ;
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x003A00D8 File Offset: 0x0039F0D8
		public void CopyToClipboard()
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
			this.CopyToClipboard(null);
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x003A011C File Offset: 0x0039F11C
		public void Activate()
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
			((spr\u17FF)base.ReservedHandle).ᜀ(this);
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x003A0168 File Offset: 0x0039F168
		public void Close(string Filename)
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
			this.Close(Filename != null && Filename.Length > 0, Filename);
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x003A01C0 File Offset: 0x0039F1C0
		public void Close(bool SaveChanges, string Filename)
		{
			int num = 8;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_119;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 1:
					if (base.Parent is IList)
					{
						num = 5;
						continue;
					}
					goto IL_150;
				case 2:
					goto IL_119;
				case 3:
					this.SaveAs(Filename);
					num = 10;
					continue;
				case 4:
					if (Filename != null)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 5:
				{
					IList list = (IList)base.Parent;
					num2 = list.IndexOf(this);
					num = 2;
					continue;
				}
				case 6:
					if (this.ᝀ != null)
					{
						num = 9;
						continue;
					}
					goto IL_6D;
				case 7:
				{
					if (true)
					{
					}
					IList list;
					list.RemoveAt(num2);
					num = 11;
					continue;
				}
				case 9:
					this.Save();
					num = 12;
					continue;
				case 10:
					goto IL_6D;
				case 11:
					goto IL_DD;
				case 12:
					goto IL_6D;
				}
				if (SaveChanges)
				{
					num = 0;
					continue;
				}
				IL_6D:
				num = 1;
				continue;
				IL_119:
				if (num2 < 0)
				{
					break;
				}
				num = 7;
			}
			IL_DD:
			IL_150:
			this.DisposeAll();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x003A032C File Offset: 0x0039F32C
		public void Close(bool saveChanges)
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
			this.Close(saveChanges, null);
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x003A0370 File Offset: 0x0039F370
		public void Close()
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
			this.Close(false);
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x003A03B4 File Offset: 0x0039F3B4
		public IMarkersDesigner CreateTemplateMarkersProcessor()
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
			return base.AppImplementation.ᜇ(this);
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x003A03FC File Offset: 0x0039F3FC
		public void MarkAsFinal()
		{
			int a_ = 11;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			XlsDocumentProperty xlsDocumentProperty = (XlsDocumentProperty)this.ᝬ.ᜃ(RecordTableEnumerator.b("Ṁโ⑄㕆≈੊㹌ॎ㡐㵒㑔㭖", a_));
			xlsDocumentProperty.Boolean = true;
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x003A0468 File Offset: 0x0039F468
		public void Save()
		{
			int a_ = 2;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7D:
				if (true)
				{
				}
				if (this.ᝀ.Length != 0)
				{
					this.SaveAs(this.ᝀ);
					return;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9A;
				case 1:
					num = 3;
					continue;
				case 3:
					goto IL_7D;
				}
				if (this.ᝀ == null)
				{
					break;
				}
				num = 1;
			}
			IL_61:
			throw new ApplicationException(RecordTableEnumerator.b("漷唹主唽∿ⵁ⭃ⵅ桇㵉ⵋ㵍灏㱑㭓≕硗㙙㍛㽝џ䉡ɣᑥݧݩ䱫࡭᥯ṱᅳ塵塷⍹፻୽ꁿꢇﾉﾋ낏솑ﶗ캙\ud89d즟캡솣蚥잧\ud8a9貫ﶭ톯쒱톳ힷ\ud9bb춽낿귁꫃뗅귇ꇋꯍ꓏뫑믓닕", a_));
			IL_9A:
			goto IL_61;
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x003A0520 File Offset: 0x0039F520
		public void SaveAs(string FileName)
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
			this.SaveAs(FileName, ExcelSaveType.SaveAsXLS, this.Version);
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x003A056C File Offset: 0x0039F56C
		public void SaveAs(string fileName, ExcelSaveType saveType)
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
			this.SaveAs(fileName, saveType, this.Version);
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x003A05B8 File Offset: 0x0039F5B8
		public void SaveAs(string fileName, ExcelSaveType saveType, ExcelVersion version)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 20;
				string fullPath;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!(fullPath != this.ᝀ))
						{
							goto IL_33D;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_295;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 1:
						goto IL_313;
					case 2:
						if (this.Styles.Count > 0)
						{
							num = 16;
							continue;
						}
						goto IL_365;
					case 3:
						if (base.ReservedHandle.\u171E())
						{
							num = 15;
							continue;
						}
						goto IL_33D;
					case 4:
						goto IL_A0;
					case 5:
						goto IL_295;
					case 6:
					{
						if (fileName.Length == 0)
						{
							num = 23;
							continue;
						}
						fullPath = Path.GetFullPath(fileName);
						string directoryName = Path.GetDirectoryName(fullPath);
						this.ᝇ = true;
						num = 22;
						continue;
					}
					case 7:
					{
						string directoryName;
						Directory.CreateDirectory(directoryName);
						num = 10;
						continue;
					}
					case 8:
					{
						string directoryName;
						if (directoryName.Length != 0)
						{
							num = 24;
							continue;
						}
						goto IL_267;
					}
					case 9:
						goto IL_204;
					case 10:
						goto IL_267;
					case 11:
						File.Delete(fullPath);
						num = 13;
						continue;
					case 12:
					{
						string directoryName;
						if (!Directory.Exists(directoryName))
						{
							num = 7;
							continue;
						}
						goto IL_267;
					}
					case 13:
						goto IL_33D;
					case 14:
						num = 19;
						continue;
					case 15:
						num = 0;
						continue;
					case 16:
					{
						XlsFont xlsFont = (this.Styles[RecordTableEnumerator.b("ཀⱂ㝄⩆⡈❊", a_)].Font as FontWrapper).Wrapped;
						this.ᝰ = (int)Math.Round((double)xlsFont.MeasureString('0'.ToString()).Width);
						this.\u1771 = (int)Math.Round((double)xlsFont.MeasureCharacter('0').Width);
						num = 9;
						continue;
					}
					case 17:
						this.ᜀ(fullPath);
						num = 1;
						continue;
					case 18:
					{
						FileAttributes attributes = File.GetAttributes(fullPath);
						num = 25;
						continue;
					}
					case 19:
					{
						string directoryName;
						if (directoryName.Length > 0)
						{
							num = 5;
							continue;
						}
						goto IL_267;
					}
					case 21:
					{
						string directoryName;
						if (directoryName != null)
						{
							num = 14;
							continue;
						}
						goto IL_267;
					}
					case 22:
						if (File.Exists(fullPath))
						{
							num = 18;
							continue;
						}
						goto IL_33D;
					case 23:
						goto IL_15A;
					case 24:
						num = 21;
						continue;
					case 25:
					{
						FileAttributes attributes;
						if ((attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
						{
							num = 17;
							continue;
						}
						goto IL_313;
					}
					}
					if (fileName == null)
					{
						num = 4;
						continue;
					}
					num = 6;
					continue;
					IL_267:
					num = 2;
					continue;
					IL_295:
					num = 12;
					continue;
					IL_313:
					num = 3;
					continue;
					IL_33D:
					num = 8;
				}
				IL_A0:
				throw new ArgumentNullException(RecordTableEnumerator.b("݀⩂⥄≆❈⩊⁌⩎", a_));
				IL_15A:
				if (true)
				{
				}
				throw new ArgumentException(RecordTableEnumerator.b("݀⩂⥄≆݈⩊⁌⩎煐げ㑔㥖㝘㑚⥜罞͠٢䕤ɦѨ᭪ᥬ᙮彰", a_));
				IL_204:
				IL_365:
				sprᦎ a_2 = this.ᜂ(new XlsWorkbook.ᜁ(this.ᜁ));
				this.ᜂ(new XlsWorkbook.ᜁ(this.ᜀ));
				IWorkbookSerializator workbookSerializator = this.ᜀ(version, a_2);
				workbookSerializator.Serialize(fullPath, this, saveType);
				this.ᝀ = fullPath;
				this.ᝇ = false;
				this.ᝄ = true;
				this.\u1713();
				return;
			}
			}
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x003A0984 File Offset: 0x0039F984
		public void SaveAsHtml(string fileName, HTMLOptions saveOption)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					FileStream fileStream;
					string text;
					string fullPath;
					switch (num)
					{
					case 0:
						try
						{
							sprᯟ sprᯟ = new sprᯟ();
							sprᯟ.ᜀ(fileStream, this, text, saveOption);
							fileStream.Close();
							goto IL_271;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									((IDisposable)fileStream).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_1CC;
								}
								if (fileStream == null)
								{
									break;
								}
								num = 1;
							}
							IL_1CC:;
						}
						goto IL_1CF;
						IL_271:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_12F;
						default:
							goto IL_287;
						}
						break;
					case 1:
						goto IL_112;
					case 2:
						goto IL_C2;
					case 3:
						if (fileName.Length == 0)
						{
							num = 12;
							continue;
						}
						fullPath = Path.GetFullPath(fileName);
						num = 9;
						continue;
					case 4:
						goto IL_C2;
					case 5:
						goto IL_1CF;
					case 6:
					{
						FileAttributes attributes;
						if ((attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
						{
							num = 7;
							continue;
						}
						goto IL_112;
					}
					case 7:
						this.ᜀ(fullPath);
						num = 1;
						continue;
					case 8:
						File.Delete(fullPath);
						num = 5;
						continue;
					case 9:
						if (File.Exists(fullPath))
						{
							num = 15;
							continue;
						}
						goto IL_1CF;
					case 11:
						if (Directory.Exists(text))
						{
							num = 16;
							continue;
						}
						Directory.CreateDirectory(text);
						num = 2;
						continue;
					case 12:
						goto IL_FB;
					case 13:
						goto IL_79;
					case 14:
						if (fullPath != this.ᝀ)
						{
							goto IL_12F;
						}
						goto IL_1CF;
					case 15:
					{
						if (true)
						{
						}
						FileAttributes attributes = File.GetAttributes(fullPath);
						num = 6;
						continue;
					}
					case 16:
						Directory.Delete(text, true);
						Directory.CreateDirectory(text);
						num = 4;
						continue;
					}
					if (fileName == null)
					{
						num = 13;
						continue;
					}
					num = 3;
					continue;
					IL_C2:
					fileStream = new FileStream(fileName, FileMode.CreateNew);
					num = 0;
					continue;
					IL_112:
					num = 14;
					continue;
					IL_12F:
					num = 8;
					continue;
					IL_1CF:
					text = string.Format(RecordTableEnumerator.b("㵅硇㝉ፋ⡍㥏㹑ㅓ╕", a_), Path.Combine(Path.GetDirectoryName(fullPath), Path.GetFileNameWithoutExtension(fullPath)));
					num = 11;
				}
				IL_79:
				throw new ArgumentNullException(RecordTableEnumerator.b("Eⅇ♉⥋⁍ㅏ㽑ㅓ", a_));
				IL_FB:
				throw new ArgumentException(RecordTableEnumerator.b("Eⅇ♉⥋Mㅏ㽑ㅓ癕㭗㭙㉛そཟᙡ䑣ѥ൧䩩५ͭoٱ൳塵", a_));
				IL_287:
				if (false)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x003A0C38 File Offset: 0x0039FC38
		private sprᦎ ᜂ(XlsWorkbook.ᜁ A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_75:
				this.\u1759.ᜄ();
				num = 4;
				break;
			default:
				if (false)
				{
				}
				goto IL_4E;
			}
			bool flag;
			bool flag2;
			sprᦎ sprᦎ;
			for (;;)
			{
				IL_28:
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
					if (flag)
					{
						num = 3;
						continue;
					}
					goto IL_BC;
				case 2:
					if (flag2)
					{
						num = 0;
						continue;
					}
					return sprᦎ;
				case 3:
					goto IL_9D;
				case 4:
					goto IL_BC;
				case 5:
					goto IL_CF;
				case 6:
					if (this.\u1759 != null)
					{
						num = 7;
						continue;
					}
					goto IL_BC;
				case 7:
					num = 1;
					continue;
				}
				goto IL_4E;
				IL_BC:
				this.ᜂ(sprᦎ, A_0);
				num = 5;
			}
			IL_9D:
			goto IL_75;
			IL_CF:
			if (true)
			{
			}
			return sprᦎ;
			IL_4E:
			flag2 = this.ᜁ(A_0);
			sprᦎ = this.ᜀ(A_0, out flag);
			num = 2;
			goto IL_28;
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x003A0D20 File Offset: 0x0039FD20
		private void ᜂ(sprᦎ A_0, XlsWorkbook.ᜁ A_1)
		{
			int a_ = 1;
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
					if (true)
					{
					}
					this.ᜁ(A_0, A_1);
					this.ᜀ(A_0, A_1);
					return;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䐶儸娺䴼娾ࡀ❂ᝄ≆㩈⹊㽌㥎㑐⅒", a_));
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x003A0D90 File Offset: 0x0039FD90
		private void ᜁ(sprᦎ A_0, XlsWorkbook.ᜁ A_1)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				IEnumerator<ShapeCollectionBase> enumerator = this.ᜃ(A_1).GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 5;
								continue;
							}
							ShapeCollectionBase shapeCollectionBase = enumerator.Current;
							num = 2;
							continue;
						}
						case 1:
						{
							ShapeCollectionBase shapeCollectionBase;
							A_0.ᜇ(shapeCollectionBase.CollectionIndex);
							this.ᜀ(A_0, shapeCollectionBase);
							num = 4;
							continue;
						}
						case 2:
						{
							ShapeCollectionBase shapeCollectionBase;
							if (shapeCollectionBase.StartId != 0)
							{
								num = 9;
								continue;
							}
							break;
						}
						case 5:
							num = 10;
							continue;
						case 7:
						{
							ShapeCollectionBase shapeCollectionBase;
							int num2 = this.ᜁ(A_0, shapeCollectionBase);
							num = 8;
							continue;
						}
						case 8:
						{
							int num2;
							int num3;
							if (num3 > num2)
							{
								num = 1;
								continue;
							}
							ShapeCollectionBase shapeCollectionBase;
							this.ᜂ(A_0, shapeCollectionBase);
							num = 6;
							continue;
						}
						case 9:
						{
							ShapeCollectionBase shapeCollectionBase;
							int num3 = this.ᜀ(shapeCollectionBase);
							num = 11;
							continue;
						}
						case 10:
							goto IL_13F;
						case 11:
						{
							int num3;
							if (num3 > 0)
							{
								num = 7;
								continue;
							}
							break;
						}
						}
						IL_8C:
						num = 0;
						continue;
						goto IL_8C;
					}
					IL_13F:;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_198;
						case 2:
							enumerator.Dispose();
							num = 1;
							continue;
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
							if (enumerator == null)
							{
								goto IL_19A;
							}
							break;
						}
						num = 2;
					}
					IL_198:
					IL_19A:;
				}
				return;
			}
			}
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x003A0F60 File Offset: 0x0039FF60
		private void ᜂ(sprᦎ A_0, ShapeCollectionBase A_1)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_80:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
					goto IL_59;
				}
				break;
			}
			int num2;
			int count;
			int num3;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					XlsShape xlsShape = A_1[num2] as XlsShape;
					num = 2;
					continue;
				}
				case 1:
					goto IL_9F;
				case 2:
				{
					XlsShape xlsShape;
					if (xlsShape.ShapeId == 0)
					{
						num = 6;
						continue;
					}
					goto IL_7C;
				}
				case 3:
					goto IL_D0;
				case 4:
					goto IL_EC;
				case 5:
					goto IL_D0;
				case 6:
				{
					XlsShape xlsShape;
					num3 = (xlsShape.ShapeId = num3 + 1);
					num = 1;
					continue;
				}
				}
				goto IL_59;
				IL_D0:
				num = 0;
			}
			IL_7C:
			num2++;
			goto IL_80;
			IL_9F:
			goto IL_7C;
			IL_EC:
			A_1.LastId = num3;
			return;
			IL_59:
			num3 = A_1.LastId;
			num2 = 0;
			count = A_1.Count;
			if (true)
			{
			}
			num = 5;
			goto IL_36;
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x003A1064 File Offset: 0x003A0064
		private int ᜁ(sprᦎ A_0, ShapeCollectionBase A_1)
		{
			int a_ = 18;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 != null)
					{
						goto IL_A1;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_8B;
				case 3:
					goto IL_3E;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				IL_54:
				num = 0;
			}
			IL_3E:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏᭑こѕ㵗⥙㥛ⱝᙟݡᙣ", a_));
			IL_8B:
			throw new ArgumentNullException(RecordTableEnumerator.b("㭇≉ⵋ㹍㕏⅑", a_));
			IL_A1:
			int num2 = A_0.ᜃ(A_1.CollectionIndex);
			return num2 + A_1.StartId - A_1.LastId;
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x003A1130 File Offset: 0x003A0130
		private int ᜀ(ShapeCollectionBase A_0)
		{
			int a_ = 14;
			for (;;)
			{
				switch (0)
				{
				default:
				{
					int num = 3;
					for (;;)
					{
						int num2;
						int count;
						int num3;
						switch (num)
						{
						case 0:
							goto IL_8C;
						case 1:
						{
							XlsShape xlsShape;
							if (xlsShape.ShapeId <= 0)
							{
								num = 6;
								continue;
							}
							goto IL_8C;
						}
						case 2:
						{
							if (num2 >= count)
							{
								num = 4;
								continue;
							}
							XlsShape xlsShape = A_0[num2] as XlsShape;
							num = 1;
							continue;
						}
						case 4:
							return num3;
						case 5:
							goto IL_64;
						case 6:
							num3++;
							num = 0;
							continue;
						case 7:
							goto IL_DF;
						case 8:
							goto IL_DF;
						}
						if (A_0 == null)
						{
							if (true)
							{
							}
							num = 5;
							continue;
						}
						num3 = 0;
						num2 = 0;
						count = A_0.Count;
						num = 8;
						continue;
						IL_8C:
						num2++;
						num = 7;
						continue;
						IL_DF:
						num = 2;
					}
					IL_64:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_7A;
					}
					break;
				}
				}
			}
			IL_7A:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅⥇㩉⥋㵍", a_));
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x003A126C File Offset: 0x003A026C
		private void ᜀ(sprᦎ A_0, XlsWorkbook.ᜁ A_1)
		{
			int a_ = 6;
			int num = 0;
			for (;;)
			{
				IEnumerator<ShapeCollectionBase> enumerator;
				switch (num)
				{
				case 1:
					goto IL_33;
				case 2:
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 2:
								if (true)
								{
								}
								num = 4;
								continue;
							case 3:
							{
								ShapeCollectionBase shapeCollectionBase;
								if (shapeCollectionBase.StartId == 0)
								{
									num = 5;
									continue;
								}
								break;
							}
							case 4:
								goto IL_DB;
							case 5:
							{
								ShapeCollectionBase shapeCollectionBase;
								this.ᜀ(A_0, shapeCollectionBase);
								A_0.ᜁ(shapeCollectionBase.CollectionIndex, shapeCollectionBase.Count);
								num = 1;
								continue;
							}
							case 6:
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								ShapeCollectionBase shapeCollectionBase = enumerator.Current;
								num = 3;
								continue;
							}
							}
							IL_87:
							num = 6;
							continue;
							goto IL_87;
						}
						IL_DB:
						return;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_133;
							case 1:
								enumerator.Dispose();
								num = 0;
								continue;
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
								if (enumerator == null)
								{
									goto IL_135;
								}
								break;
							}
							num = 1;
						}
						IL_133:
						IL_135:;
					}
					goto IL_136;
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				IL_136:
				enumerator = this.ᜃ(A_1).GetEnumerator();
				num = 2;
			}
			IL_33:
			throw new ArgumentNullException(RecordTableEnumerator.b("伻嘽ℿ㉁⅃ཅⱇᡉ⥋㵍㕏⁑≓㍕⩗", a_));
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x003A13FC File Offset: 0x003A03FC
		private void ᜀ(sprᦎ A_0, ShapeCollectionBase A_1)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num2;
					int num3;
					int count;
					switch (num)
					{
					case 0:
						(A_1[num2] as XlsShape).ShapeId = num3 + num2;
						num = 7;
						continue;
					case 1:
						goto IL_73;
					case 2:
						if (num2 >= count)
						{
							num = 5;
							continue;
						}
						num = 8;
						continue;
					case 3:
						goto IL_FE;
					case 4:
						goto IL_103;
					case 5:
						return;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14A;
						default:
							if (false)
							{
							}
							goto IL_138;
						}
						break;
					case 8:
						if ((A_1[num2] as XlsShape).ShapeId == 0)
						{
							num = 0;
							continue;
						}
						goto IL_138;
					case 9:
						goto IL_103;
					case 10:
						if (A_1 == null)
						{
							num = 3;
							continue;
						}
						goto IL_14A;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 1;
						continue;
					}
					num = 10;
					continue;
					IL_103:
					num = 2;
					continue;
					IL_138:
					num2++;
					num = 9;
					continue;
					IL_14A:
					count = A_1.Count;
					num3 = A_0.ᜀ(count + 1, A_1.CollectionIndex);
					int a_2 = num3 + A_1.Count;
					A_1.StartId = num3;
					A_1.LastId = a_2;
					num3++;
					num2 = 0;
					num = 4;
				}
				IL_73:
				throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁╃㙅ⵇ͉⡋ᱍ㕏⅑ㅓ⑕⹗㽙⹛", a_));
				IL_FE:
				throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁╃㙅ⵇ㥉", a_));
			}
			}
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x003A15AC File Offset: 0x003A05AC
		private sprᦎ ᜀ(XlsWorkbook.ᜁ A_0, out bool A_1)
		{
			switch (0)
			{
			default:
			{
				sprᦎ sprᦎ = new sprᦎ();
				A_1 = false;
				IEnumerator<ShapeCollectionBase> enumerator = this.ᜃ(A_0).GetEnumerator();
				try
				{
					int num = 22;
					for (;;)
					{
						ShapeCollectionBase shapeCollectionBase;
						int num3;
						int num4;
						switch (num)
						{
						case 0:
							goto IL_344;
						case 1:
							num = 24;
							continue;
						case 2:
						{
							int num2 = shapeCollectionBase.StartId;
							shapeCollectionBase.LastId;
							num3 = shapeCollectionBase.CollectionIndex;
							num = 15;
							continue;
						}
						case 3:
						{
							XlsWorksheet worksheet;
							sprᦎ.ᜁ(num3, worksheet.InnerDVTable.ShapesCount + 1);
							num = 8;
							continue;
						}
						case 5:
							num = 14;
							continue;
						case 6:
							goto IL_2FC;
						case 7:
							goto IL_180;
						case 8:
							goto IL_165;
						case 9:
							num = 30;
							continue;
						case 10:
							if (shapeCollectionBase.Count > 0)
							{
								num = 20;
								continue;
							}
							break;
						case 11:
							if (shapeCollectionBase != null)
							{
								num = 21;
								continue;
							}
							break;
						case 12:
							if (shapeCollectionBase != null)
							{
								num = 2;
								continue;
							}
							break;
						case 13:
							goto IL_344;
						case 14:
						{
							XlsWorksheet worksheet;
							if (worksheet.InnerDVTable != null)
							{
								num = 3;
								continue;
							}
							goto IL_165;
						}
						case 15:
						{
							int num2;
							if (num2 > 0)
							{
								num = 26;
								continue;
							}
							goto IL_2FC;
						}
						case 16:
							num = 19;
							continue;
						case 17:
							shapeCollectionBase.StartId = 0;
							shapeCollectionBase.LastId = 0;
							num = 7;
							continue;
						case 18:
						{
							XlsWorksheet worksheet;
							if (worksheet != null)
							{
								num = 5;
								continue;
							}
							goto IL_165;
						}
						case 19:
							goto IL_373;
						case 20:
						{
							XlsWorksheet worksheet = shapeCollectionBase.Worksheet;
							num = 18;
							continue;
						}
						case 21:
							num = 10;
							continue;
						case 23:
						{
							int count;
							if (num4 >= count)
							{
								num = 6;
								continue;
							}
							int shapeId = (shapeCollectionBase[num4] as XlsShape).ShapeId;
							num = 27;
							continue;
						}
						case 24:
							if (shapeCollectionBase[num4].ShapeType != ExcelShapeType.Unknown)
							{
								num = 31;
								continue;
							}
							goto IL_180;
						case 25:
						{
							int shapeId;
							if (shapeId <= 0)
							{
								num = 1;
								continue;
							}
							goto IL_180;
						}
						case 26:
						{
							num4 = 0;
							int count = shapeCollectionBase.Count;
							num = 0;
							continue;
						}
						case 27:
						{
							int shapeId;
							if (shapeId > 0)
							{
								num = 9;
								continue;
							}
							goto IL_31F;
						}
						case 28:
							if (!enumerator.MoveNext())
							{
								num = 16;
								continue;
							}
							shapeCollectionBase = enumerator.Current;
							num = 12;
							continue;
						case 29:
							goto IL_180;
						case 30:
						{
							int shapeId;
							if (!sprᦎ.ᜁ(shapeId, shapeId, num3))
							{
								num = 17;
								continue;
							}
							goto IL_31F;
						}
						case 31:
							A_1 = true;
							num = 29;
							continue;
						}
						goto IL_BC;
						IL_165:
						sprᦎ.ᜁ(num3, shapeCollectionBase.Count);
						num = 4;
						continue;
						IL_180:
						num4++;
						num = 13;
						continue;
						IL_1EA:
						num = 28;
						continue;
						IL_BC:
						goto IL_1EA;
						IL_2FC:
						num = 11;
						continue;
						IL_31F:
						num = 25;
						continue;
						IL_344:
						num = 23;
					}
					IL_373:;
				}
				finally
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_3D6;
						case 1:
							enumerator.Dispose();
							num = 0;
							continue;
						case 2:
							if (true)
							{
							}
							break;
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
							if (enumerator == null)
							{
								goto IL_3D8;
							}
							break;
						}
						num = 1;
					}
					IL_3D6:
					IL_3D8:;
				}
				return sprᦎ;
			}
			}
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x003A19BC File Offset: 0x003A09BC
		private bool ᜁ(XlsWorkbook.ᜁ A_0)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				int num = -1;
				num = this.ᜀ(A_0);
				IEnumerator<ShapeCollectionBase> enumerator = this.ᜃ(A_0).GetEnumerator();
				try
				{
					int num2 = 6;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 5;
								continue;
							}
							ShapeCollectionBase shapeCollectionBase = enumerator.Current;
							num2 = 4;
							continue;
						}
						case 1:
							goto IL_143;
						case 2:
						{
							ShapeCollectionBase shapeCollectionBase;
							num3 = shapeCollectionBase.CollectionIndex;
							num2 = 10;
							continue;
						}
						case 3:
						{
							ShapeCollectionBase shapeCollectionBase;
							if (shapeCollectionBase.Count > 0)
							{
								num2 = 2;
								continue;
							}
							break;
						}
						case 4:
						{
							ShapeCollectionBase shapeCollectionBase;
							if (shapeCollectionBase != null)
							{
								num2 = 8;
								continue;
							}
							break;
						}
						case 5:
							num2 = 1;
							continue;
						case 8:
							num2 = 3;
							continue;
						case 9:
						{
							ShapeCollectionBase shapeCollectionBase;
							num3 = (shapeCollectionBase.CollectionIndex = ++num);
							num2 = 11;
							continue;
						}
						case 10:
							if (dictionary.ContainsKey(num3))
							{
								num2 = 9;
								continue;
							}
							goto IL_DC;
						case 11:
							goto IL_DC;
						}
						IL_97:
						num2 = 0;
						continue;
						goto IL_97;
						IL_DC:
						dictionary.Add(num3, num3);
						num2 = 7;
					}
					IL_143:;
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
							goto IL_19E;
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
							if (enumerator == null)
							{
								goto IL_1A0;
							}
							break;
						}
						num2 = 0;
					}
					IL_19E:
					IL_1A0:;
				}
				return num >= 0;
			}
			}
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x003A1B98 File Offset: 0x003A0B98
		private int ᜀ(XlsWorkbook.ᜁ A_0)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = -1;
				IEnumerator<ShapeCollectionBase> enumerator = this.ᜃ(A_0).GetEnumerator();
				try
				{
					int num2 = 6;
					for (;;)
					{
						int num3;
						int num4;
						switch (num2)
						{
						case 1:
							goto IL_122;
						case 2:
						{
							ShapeCollectionBase shapeCollectionBase;
							if (shapeCollectionBase == null)
							{
								num2 = 5;
								continue;
							}
							num2 = 4;
							continue;
						}
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 7;
								continue;
							}
							ShapeCollectionBase shapeCollectionBase = enumerator.Current;
							num2 = 2;
							continue;
						}
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B9;
							default:
							{
								if (false)
								{
								}
								ShapeCollectionBase shapeCollectionBase;
								num3 = shapeCollectionBase.CollectionIndex;
								goto IL_C7;
							}
							}
							break;
						case 5:
							num2 = 8;
							continue;
						case 7:
							num2 = 1;
							continue;
						case 8:
							num3 = -1;
							goto IL_C7;
						case 9:
							num = num4;
							goto IL_B9;
						case 10:
							if (num4 > num)
							{
								num2 = 9;
								continue;
							}
							break;
						}
						IL_7D:
						num2 = 3;
						continue;
						goto IL_7D;
						IL_B9:
						num2 = 0;
						continue;
						IL_C7:
						num4 = num3;
						num2 = 10;
					}
					IL_122:;
				}
				finally
				{
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							enumerator.Dispose();
							num2 = 2;
							continue;
						case 2:
							goto IL_15F;
						}
						if (enumerator == null)
						{
							break;
						}
						num2 = 0;
					}
					IL_15F:;
				}
				return num;
			}
			}
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x003A1D18 File Offset: 0x003A0D18
		internal IEnumerable<ShapeCollectionBase> ᜃ(XlsWorkbook.ᜁ A_0)
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
			XlsWorkbook.ᜀ ᜀ = new XlsWorkbook.ᜀ(-2);
			ᜀ.ᜃ = this;
			ᜀ.ᜅ = A_0;
			return ᜀ;
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x003A1D6C File Offset: 0x003A0D6C
		private ShapeCollectionBase ᜁ(ITabSheet A_0)
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
			return (A_0 as XlsWorksheetBase).Shapes as ShapeCollectionBase;
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x003A1DB8 File Offset: 0x003A0DB8
		private ShapeCollectionBase ᜀ(ITabSheet A_0)
		{
			for (;;)
			{
				if (true)
				{
				}
				XlsChartShape xlsChartShape = A_0 as XlsChartShape;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_0 = xlsChartShape;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6E;
					case 2:
						if (xlsChartShape != null)
						{
							num = 0;
							continue;
						}
						goto IL_70;
					}
					break;
				}
			}
			IL_6E:
			IL_70:
			return ((XlsWorksheetBase)A_0).HeaderFooterShapes;
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x003A1E40 File Offset: 0x003A0E40
		private void ᜈ()
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

		// Token: 0x06005CAB RID: 23723 RVA: 0x003A1E7C File Offset: 0x003A0E7C
		public void SaveAs(string fileName, HttpResponse response)
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
			this.SaveAs(fileName, ExcelSaveType.SaveAsXLS, response);
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x003A1EC0 File Offset: 0x003A0EC0
		public void SaveAs(string fileName, string separator)
		{
			int a_ = 13;
			int num = 2;
			XlsWorksheet xlsWorksheet;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_182;
				case 1:
					if (separator.Length == 0)
					{
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 3:
					if (true)
					{
					}
					if (xlsWorksheet != null)
					{
						num = 10;
						continue;
					}
					goto IL_191;
				case 4:
					if (this.\u1734 == null)
					{
						num = 9;
						continue;
					}
					goto IL_A1;
				case 5:
					if (fileName.Length == 0)
					{
						num = 11;
						continue;
					}
					num = 6;
					continue;
				case 6:
					if (separator != null)
					{
						num = 7;
						continue;
					}
					goto IL_6A;
				case 7:
					num = 1;
					continue;
				case 8:
					goto IL_54;
				case 9:
					goto IL_105;
				case 10:
					goto IL_D1;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A1;
					default:
						goto IL_147;
					}
					break;
				}
				if (fileName == null)
				{
					num = 8;
					continue;
				}
				num = 5;
				continue;
				IL_A1:
				xlsWorksheet = (this.\u1734 as XlsWorksheet);
				num = 3;
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("Ղⱄ⭆ⱈ╊ⱌ≎㑐", a_));
			IL_6A:
			throw new ArgumentNullException(RecordTableEnumerator.b("あ⁄㝆⡈㥊ⱌ㭎㹐⅒", a_));
			IL_D1:
			xlsWorksheet.SaveToFile(fileName, separator);
			return;
			IL_105:
			throw new ArgumentNullException(RecordTableEnumerator.b("ɂ♄㍆⁈㵊⡌㡎㹐⅒㹔⑖ㅘ㹚㡜⭞你", a_));
			IL_147:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("Ղⱄ⭆ⱈՊⱌ≎㑐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦౨٪ᵬ᭮ࡰ嵲", a_));
			IL_182:
			goto IL_6A;
			IL_191:
			throw new ArgumentNullException(RecordTableEnumerator.b("ɂ♄㍆⁈㵊⡌ᱎ㥐㙒ご⍖", a_));
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x003A2074 File Offset: 0x003A1074
		public void SaveAs(Stream stream, string separator)
		{
			int a_ = 4;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (separator != null)
					{
						num = 3;
						continue;
					}
					goto IL_103;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
					{
						if (false)
						{
						}
						if (separator.Length == 0)
						{
							num = 4;
							continue;
						}
						XlsWorksheet xlsWorksheet = this.ActiveSheet as XlsWorksheet;
						num = 2;
						continue;
					}
					}
					break;
				case 2:
				{
					XlsWorksheet xlsWorksheet;
					if (xlsWorksheet != null)
					{
						num = 6;
						continue;
					}
					return;
				}
				case 3:
					num = 1;
					continue;
				case 4:
					goto IL_B2;
				case 5:
					goto IL_4B;
				case 6:
				{
					XlsWorksheet xlsWorksheet;
					xlsWorksheet.SaveToStream(stream, separator);
					num = 8;
					continue;
				}
				case 8:
					goto IL_E3;
				}
				if (stream == null)
				{
					num = 5;
				}
				else
				{
					num = 0;
				}
			}
			IL_4B:
			goto IL_B4;
			IL_B2:
			goto IL_103;
			IL_B4:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䤹䠻䰽┿⍁⥃", a_));
			IL_E3:
			return;
			IL_103:
			throw new ArgumentNullException(RecordTableEnumerator.b("䤹夻丽ℿぁ╃㉅❇㡉", a_));
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x003A2198 File Offset: 0x003A1198
		internal void ᜀ(string A_0, HttpResponse A_1, HttpContentType A_2)
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
			this.ᜀ(A_0, ExcelSaveType.SaveAsXLS, A_1, A_2);
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x003A21E0 File Offset: 0x003A11E0
		public void SaveAs(string fileName, ExcelSaveType saveType, HttpResponse response)
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
			this.ᜀ(fileName, saveType, response, HttpDownloadType.PromptDialog);
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x003A2228 File Offset: 0x003A1228
		internal void ᜀ(string A_0, ExcelSaveType A_1, HttpResponse A_2, HttpContentType A_3)
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
			this.ᜀ(A_0, A_1, A_2, HttpDownloadType.PromptDialog, A_3);
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x003A2270 File Offset: 0x003A1270
		internal void ᜀ(string A_0, HttpResponse A_1, HttpDownloadType A_2)
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
			this.ᜀ(A_0, ExcelSaveType.SaveAsXLS, A_1, A_2);
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x003A22B8 File Offset: 0x003A12B8
		internal void ᜁ(string A_0, HttpResponse A_1, HttpDownloadType A_2, HttpContentType A_3)
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
			this.ᜀ(A_0, ExcelSaveType.SaveAsXLS, A_1, A_2, A_3);
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x003A2300 File Offset: 0x003A1300
		internal void ᜀ(string A_0, ExcelSaveType A_1, HttpResponse A_2, HttpDownloadType A_3)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_32;
					default:
						goto IL_62;
					}
					break;
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_73;
				}
				goto IL_20;
				IL_32:
				num = 1;
				continue;
				IL_20:
				if (this.Version != ExcelVersion.Version97to2003)
				{
					goto IL_32;
				}
				if (true)
				{
				}
				num = 0;
			}
			IL_62:
			if (false)
			{
			}
			HttpContentType httpContentType = HttpContentType.Excel97;
			goto IL_76;
			IL_73:
			httpContentType = HttpContentType.Excel2000;
			IL_76:
			HttpContentType a_ = httpContentType;
			this.ᜀ(A_0, A_1, A_2, A_3, a_);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x003A2390 File Offset: 0x003A1390
		internal void ᜀ(string A_0, string A_1, HttpResponse A_2, HttpDownloadType A_3, HttpContentType A_4)
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
			this.ᜀ(A_0, A_2, A_3, A_4);
			this.SaveAs(A_2.OutputStream, A_1);
			A_2.End();
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x003A23EC File Offset: 0x003A13EC
		internal void ᜀ(string A_0, ExcelSaveType A_1, HttpResponse A_2, HttpDownloadType A_3, HttpContentType A_4)
		{
			for (;;)
			{
				for (;;)
				{
					this.ᜀ(A_0, A_2, A_3, A_4);
					this.SaveAs(A_2.OutputStream, A_1);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.Version != ExcelVersion.Version2007)
							{
								num = 1;
								continue;
							}
							goto IL_56;
						case 1:
							num = 3;
							continue;
						case 2:
							goto IL_76;
						case 3:
							if (this.Version == ExcelVersion.Version2010)
							{
								num = 2;
								continue;
							}
							goto IL_9C;
						}
						break;
					}
				}
				IL_76:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_8C;
				}
			}
			IL_56:
			A_2.End();
			return;
			IL_8C:
			if (true)
			{
			}
			if (false)
			{
			}
			goto IL_56;
			IL_9C:
			A_2.Flush();
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x003A249C File Offset: 0x003A149C
		private void ᜀ(string A_0, HttpResponse A_1, HttpDownloadType A_2, HttpContentType A_3)
		{
			int a_ = 12;
			int num = 0;
			string arg;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 1:
					num = 8;
					continue;
				case 2:
					goto IL_DE;
				case 3:
					if (A_0.Length == 0)
					{
						num = 2;
						continue;
					}
					num = 10;
					continue;
				case 4:
					switch (A_2)
					{
					case HttpDownloadType.Open:
						arg = RecordTableEnumerator.b("⭁⩃⩅ⅇ⑉⥋", a_);
						num = 6;
						continue;
					case HttpDownloadType.PromptDialog:
						arg = RecordTableEnumerator.b("⍁ぃ㉅⥇⥉⑋⍍㕏㱑⁓", a_);
						num = 7;
						continue;
					default:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_152;
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
				case 5:
					goto IL_58;
				case 6:
					goto IL_74;
				case 7:
					goto IL_FA;
				case 8:
					goto IL_11E;
				case 9:
					goto IL_150;
				case 10:
					if (A_1 == null)
					{
						num = 9;
						continue;
					}
					arg = string.Empty;
					num = 4;
					continue;
				}
				if (A_0 == null)
				{
					num = 5;
				}
				else
				{
					num = 3;
				}
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("⑁ⵃ⩅ⵇщⵋ⍍㕏", a_));
			IL_74:
			goto IL_184;
			IL_DE:
			goto IL_152;
			IL_FA:
			goto IL_184;
			IL_11E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᝁ⩃ⵅ♇╉㭋⁍灏⅑㕓⁕㵗穙⡛❝ၟݡ", a_));
			IL_150:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ⅃㕅㡇╉≋㵍㕏", a_));
			IL_152:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⑁ⵃ⩅ⵇщⵋ⍍㕏", a_));
			IL_184:
			string contentType = this.ᜀ(A_3);
			A_0 = Path.GetFileName(A_0);
			A_1.Clear();
			A_1.ContentType = contentType;
			A_1.AddHeader(RecordTableEnumerator.b("Ł⭃⡅㱇⽉≋㩍絏ᙑ㵓╕⡗㕙⽛㝝ᑟୡୣࡥ", a_), string.Format(RecordTableEnumerator.b("㥁瑃㭅獇ⱉ╋≍㕏㱑㕓㭕㵗杙❛潝ᵟ奡", a_), arg, A_0));
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x003A2674 File Offset: 0x003A1674
		internal void ᜀ(XmlWriter A_0, XmlSaveType A_1)
		{
			int a_ = 0;
			if (A_0 == null)
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䄵䨷匹䠻嬽㈿", a_));
			}
			this.ᝇ = true;
			spr\u2127 spr_u = sprῶ.ᜀ(A_1);
			spr_u.ᜀ(A_0, this);
			this.ᝇ = false;
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x003A26F0 File Offset: 0x003A16F0
		internal void ᜀ(string A_0, XmlSaveType A_1)
		{
			int a_ = 13;
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_74;
					case 1:
						goto IL_46;
					case 3:
						if (A_0.Length == 0)
						{
							num = 0;
							continue;
						}
						goto IL_A6;
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 1;
					}
					else
					{
						num = 3;
					}
				}
				IL_46:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_8C;
				}
			}
			IL_74:
			throw new ArgumentException(RecordTableEnumerator.b("Ղⱄ⭆ⱈՊⱌ≎㑐獒㙔㙖㝘筚㍜ぞᕠ䍢ݤɦ䥨๪lὮհੲ孴", a_));
			IL_8C:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╂ⱄ⭆ⱈՊⱌ≎㑐", a_));
			IL_A6:
			this.ᝇ = true;
			Encoding encoding = new UTF8Encoding(false);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(A_0, encoding);
			xmlTextWriter.Formatting = Formatting.Indented;
			this.ᜀ(xmlTextWriter, A_1);
			xmlTextWriter.Close();
			this.ᝇ = false;
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x003A27D8 File Offset: 0x003A17D8
		internal void ᜀ(Stream A_0, XmlSaveType A_1)
		{
			int a_ = 6;
			if (A_0 == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3C;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				IL_3C:
				throw new ArgumentNullException(RecordTableEnumerator.b("伻䨽㈿❁╃⭅", a_));
			}
			this.ᝇ = true;
			Encoding encoding = new UTF8Encoding(false);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(A_0, encoding);
			xmlTextWriter.Formatting = Formatting.Indented;
			this.ᜀ(xmlTextWriter, A_1);
			xmlTextWriter.Flush();
			this.ᝇ = false;
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x003A2868 File Offset: 0x003A1868
		private string ᜀ(HttpContentType A_0)
		{
			int a_ = 16;
			for (;;)
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_4D:
					switch (A_0)
					{
					case HttpContentType.Excel97:
						goto IL_B3;
					case HttpContentType.Excel2000:
						goto IL_88;
					case HttpContentType.Excel2007:
					case HttpContentType.Excel2010:
						goto IL_71;
					case HttpContentType.CSV:
						goto IL_A4;
					default:
						num = 0;
						break;
					}
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_4D;
					case 2:
						goto IL_A2;
					}
					break;
				}
			}
			IL_71:
			if (true)
			{
			}
			return RecordTableEnumerator.b("݅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙獛⡝๟١䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽꾁﶑ﾙ躟톡풣풥춧쮩좫\uddad\ud8afힱ톳습햷횹銻춽ꢿꟁꇃ닅", a_);
			IL_88:
			return RecordTableEnumerator.b("݅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙獛⡝๟١䩣୥᭧䝩५᙭፯᝱ᡳ", a_);
			IL_A2:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ፅ♇ⅉ≋⅍❏㱑瑓㕕㝗㑙⡛㭝๟ᙡ䑣ብᅧᩩ५", a_));
			IL_A4:
			return RecordTableEnumerator.b("㉅ⵇ㉉㡋慍㍏⅑≓", a_);
			IL_B3:
			return RecordTableEnumerator.b("݅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙獛♝䵟ཡᝣͥၧ३५ɭ", a_);
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x003A294C File Offset: 0x003A194C
		public void SaveAs(Stream stream)
		{
			int a_ = 3;
			if (stream == null)
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
					break;
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似娾⁀⹂", a_));
			}
			this.SaveAs(stream, ExcelSaveType.SaveAsXLS);
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x003A29B4 File Offset: 0x003A19B4
		public void SaveAs(Stream stream, ExcelSaveType saveType)
		{
			int a_ = 3;
			if (stream == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				}
				if (false)
				{
				}
				IL_34:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺似娾⁀⹂", a_));
			}
			this.ᝇ = true;
			sprᦎ a_2 = this.ᜂ(new XlsWorkbook.ᜁ(this.ᜁ));
			IWorkbookSerializator workbookSerializator = this.ᜀ(this.\u177A, a_2);
			workbookSerializator.Serialize(stream, this, saveType);
			stream.Flush();
			this.ᝇ = false;
			this.ᝄ = true;
			this.\u1713();
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x003A2A5C File Offset: 0x003A1A5C
		private string[] ᜇ()
		{
			switch (0)
			{
			default:
			{
				string[] array;
				for (;;)
				{
					if (true)
					{
					}
					IWorksheets worksheets;
					int num;
					int count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						array = new string[this.Worksheets.Count];
						worksheets = this.Worksheets;
						num = 0;
						count = worksheets.Count;
						break;
					}
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_81;
						case 1:
							return array;
						case 2:
							goto IL_81;
						case 3:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							array[num] = worksheets[num].Name;
							num++;
							num2 = 2;
							continue;
						}
						break;
						IL_81:
						num2 = 3;
					}
				}
				return array;
			}
			}
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x003A2B28 File Offset: 0x003A1B28
		private object[] ᜆ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				List<object> list = new List<object>();
				Dictionary<ExcelSheetType, int> dictionary = this.ᜅ();
				Dictionary<ExcelSheetType, int>.Enumerator enumerator = dictionary.GetEnumerator();
				goto IL_37;
				try
				{
					for (;;)
					{
						IL_37:
						int num = 4;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 1;
								continue;
							case 1:
								goto IL_118;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_37;
								default:
								{
									if (false)
									{
									}
									ExcelSheetType key;
									if (XlsWorkbook.ផ.ContainsKey(key))
									{
										num = 5;
										continue;
									}
									break;
								}
								}
								break;
							case 3:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								KeyValuePair<ExcelSheetType, int> keyValuePair = enumerator.Current;
								ExcelSheetType key = keyValuePair.Key;
								int value = keyValuePair.Value;
								num = 2;
								continue;
							}
							case 5:
							{
								ExcelSheetType key;
								list.Add(XlsWorkbook.ផ[key]);
								int value;
								list.Add(value);
								num = 6;
								continue;
							}
							}
							IL_BD:
							num = 3;
							continue;
							goto IL_BD;
						}
					}
					IL_118:;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return list.ToArray();
			}
			}
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x003A2C74 File Offset: 0x003A1C74
		private Dictionary<ExcelSheetType, int> ᜅ()
		{
			switch (0)
			{
			default:
			{
				Dictionary<ExcelSheetType, int> dictionary;
				for (;;)
				{
					dictionary = new Dictionary<ExcelSheetType, int>();
					IWorksheets worksheets = this.Worksheets;
					int num = 0;
					int count = worksheets.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							XlsWorksheet xlsWorksheet;
							int num3 = dictionary[xlsWorksheet.Type];
							num3++;
							dictionary[xlsWorksheet.Type] = num3;
							num2 = 5;
							continue;
						}
						case 1:
							return dictionary;
						case 2:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D2;
							default:
							{
								if (false)
								{
								}
								XlsWorksheet xlsWorksheet = worksheets[num] as XlsWorksheet;
								num2 = 7;
								continue;
							}
							}
							break;
						case 3:
							goto IL_E0;
						case 4:
							goto IL_5B;
						case 5:
							if (true)
							{
							}
							goto IL_5B;
						case 6:
							goto IL_E0;
						case 7:
						{
							XlsWorksheet xlsWorksheet;
							if (dictionary.ContainsKey(xlsWorksheet.Type))
							{
								goto IL_D2;
							}
							dictionary.Add(xlsWorksheet.Type, 1);
							num2 = 4;
							continue;
						}
						}
						break;
						IL_5B:
						num++;
						num2 = 6;
						continue;
						IL_D2:
						num2 = 0;
						continue;
						IL_E0:
						num2 = 2;
					}
				}
				return dictionary;
			}
			}
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x003A2DC0 File Offset: 0x003A1DC0
		public void SetPaletteColor(int index, Color color)
		{
			int a_ = 7;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_128;
				case 1:
					if (index >= 8)
					{
						num = 0;
						continue;
					}
					goto IL_83;
				case 2:
					goto IL_126;
				case 3:
					goto IL_14F;
				case 4:
					if (this.ᝏ[index] != color)
					{
						num = 5;
						continue;
					}
					return;
				case 5:
					this.ᝐ = true;
					this.ᝏ[index] = Color.FromArgb((int)color.A, (int)color.R, (int)color.G, (int)color.B);
					num = 2;
					continue;
				case 7:
					if (index >= this.ᝏ.Count)
					{
						num = 3;
						continue;
					}
					num = 4;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				IL_3D:
				if (!this.ᝆ)
				{
					if (true)
					{
					}
					num = 8;
					continue;
				}
				goto IL_128;
				goto IL_3D;
				IL_128:
				num = 7;
			}
			IL_83:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吼儾╀♂㵄", a_), RecordTableEnumerator.b("琼儾╀♂㵄杆㩈⍊≌㩎㵐㝒畔㕖㱘筚㽜㩞ᕠᑢdɦݨ䭪嵬佮ၰᵲᅴ坶ॸ᩺ᅼ᩾Ꞇ愈뾐", a_));
			IL_126:
			return;
			IL_14F:
			goto IL_83;
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x003A2F24 File Offset: 0x003A1F24
		internal void ᜀ(XlsWorkbook A_0)
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
			A_0.InnerPalette.Clear();
			A_0.InnerPalette.AddRange(this.InnerPalette);
			A_0.ᝐ = this.ᝐ;
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x003A2F88 File Offset: 0x003A1F88
		public void ResetPalette()
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
			this.ᝐ = false;
			this.ᝏ = new List<Color>(XlsWorkbook.ᜨ);
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x003A2FDC File Offset: 0x003A1FDC
		public Color GetPaletteColor(ExcelColors color)
		{
			Color result;
			for (;;)
			{
				int num = (int)color;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
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
						if (num == 80)
						{
							num2 = 7;
							continue;
						}
						num %= this.ᝏ.Count;
						result = this.ᝏ[num];
						num2 = 8;
						continue;
					case 1:
						result = XlsWorkbook.\u1732[num - 77];
						num2 = 3;
						continue;
					case 2:
						if (num >= 77)
						{
							num2 = 5;
							continue;
						}
						goto IL_7C;
					case 3:
						return result;
					case 4:
						if (num <= 79)
						{
							num2 = 1;
							continue;
						}
						goto IL_7C;
					case 5:
						if (true)
						{
						}
						num2 = 4;
						continue;
					case 6:
						return result;
					case 7:
						result = XlsShapeFill.DEF_COMENT_PARSE_COLOR;
						num2 = 6;
						continue;
					case 8:
						return result;
					}
					break;
					IL_7C:
					num2 = 0;
				}
			}
			return result;
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x003A30F4 File Offset: 0x003A20F4
		public ExcelColors GetNearestColor(Color color)
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
			return this.GetNearestColor(color, 0);
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x003A3138 File Offset: 0x003A2138
		public ExcelColors GetNearestColor(Color color, int iStartIndex)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					IL_17:
					int num = 9;
					for (;;)
					{
						int num4;
						switch (num)
						{
						case 0:
							goto IL_101;
						case 1:
						{
							double num2;
							double num3;
							if (num2 < num3)
							{
								num = 2;
								continue;
							}
							goto IL_159;
						}
						case 2:
						{
							double num2;
							double num3 = num2;
							result = num4;
							num = 8;
							continue;
						}
						case 3:
							goto IL_101;
						case 4:
							goto IL_157;
						case 5:
							goto IL_159;
						case 6:
						{
							if (iStartIndex > this.ᝏ.Count)
							{
								num = 4;
								continue;
							}
							result = iStartIndex;
							double num3 = this.ColorDistance(this.ᝏ[iStartIndex], color);
							num4 = iStartIndex + 1;
							num = 0;
							continue;
						}
						case 7:
							goto IL_127;
						case 8:
						{
							double num2;
							if (num2 != 0.0)
							{
								num = 5;
								continue;
							}
							return (ExcelColors)result;
						}
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_17;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						case 10:
							if (true)
							{
							}
							num = 6;
							continue;
						case 11:
						{
							if (num4 >= this.ᝏ.Count)
							{
								num = 7;
								continue;
							}
							double num2 = this.ColorDistance(this.ᝏ[num4], color);
							num = 1;
							continue;
						}
						}
						if (iStartIndex >= 0)
						{
							num = 10;
							continue;
						}
						goto IL_ED;
						IL_101:
						num = 11;
						continue;
						IL_159:
						num4++;
						num = 3;
					}
				}
				IL_ED:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䴽㐿⍁㙃㉅Ň⑉⡋⭍⡏", a_));
				IL_127:
				return (ExcelColors)result;
				IL_157:
				goto IL_ED;
			}
			}
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x003A32DC File Offset: 0x003A22DC
		public ExcelColors GetNearestColor(int r, int g, int b)
		{
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					Color color = Color.FromArgb(255, (int)((byte)r), (int)((byte)g), (int)((byte)b));
					result = 0;
					double num = this.ColorDistance(this.ᝏ[0], color);
					int num2 = 1;
					int num3 = 6;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							for (;;)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_99;
								}
							}
							IL_99:
							if (false)
							{
							}
							double num4;
							num = num4;
							result = num2;
							num3 = 4;
							continue;
						}
						case 1:
							goto IL_ED;
						case 2:
						{
							if (num2 >= this.ᝏ.Count)
							{
								num3 = 3;
								continue;
							}
							double num4 = this.ColorDistance(this.ᝏ[num2], color);
							num3 = 5;
							continue;
						}
						case 3:
							return (ExcelColors)result;
						case 4:
							if (true)
							{
							}
							goto IL_72;
						case 5:
						{
							double num4;
							if (num4 < num)
							{
								num3 = 0;
								continue;
							}
							goto IL_72;
						}
						case 6:
							goto IL_ED;
						}
						break;
						IL_72:
						num2++;
						num3 = 1;
						continue;
						IL_ED:
						num3 = 2;
					}
				}
				return (ExcelColors)result;
			}
			}
		}

		// Token: 0x06005CC7 RID: 23751 RVA: 0x003A3400 File Offset: 0x003A2400
		public ExcelColors SetColorOrGetNearest(Color color)
		{
			switch (0)
			{
			default:
			{
				for (;;)
				{
					if (true)
					{
					}
					ExcelColors nearestColor = this.GetNearestColor(color);
					bool flag = this.ញ = this.ᜀ(this.ᝏ[(int)nearestColor], color);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E0;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return nearestColor;
							default:
								if (false)
								{
								}
								if (!flag)
								{
									num = 3;
									continue;
								}
								return nearestColor;
							}
							break;
						case 2:
							if (this.\u175A < this.ᝏ.Count)
							{
								num = 0;
								continue;
							}
							return nearestColor;
						case 3:
							num = 2;
							continue;
						}
						break;
					}
				}
				IL_E0:
				this.SetPaletteColor(this.\u175A, color);
				ExcelColors u175A = (ExcelColors)this.\u175A;
				this.\u175A++;
				return u175A;
			}
			}
		}

		// Token: 0x06005CC8 RID: 23752 RVA: 0x003A34F0 File Offset: 0x003A24F0
		public ExcelColors SetColorOrGetNearest(int r, int g, int b)
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
			Color colorOrGetNearest = Color.FromArgb(255, (int)((byte)r), (int)((byte)g), (int)((byte)b));
			return this.SetColorOrGetNearest(colorOrGetNearest);
		}

		// Token: 0x06005CC9 RID: 23753 RVA: 0x003A3544 File Offset: 0x003A2544
		public void Replace(string oldValue, string newValue)
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_30;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_8F;
						}
						break;
					case 2:
						goto IL_2E;
					case 3:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValue);
						num++;
						num2 = 0;
						continue;
					}
					break;
					IL_30:
					num2 = 3;
					continue;
					IL_2E:
					goto IL_30;
				}
			}
			IL_8F:
			if (false)
			{
			}
		}

		// Token: 0x06005CCA RID: 23754 RVA: 0x003A35E8 File Offset: 0x003A25E8
		public void Replace(string oldValue, DateTime newValue)
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_8F;
						}
						break;
					case 1:
						if (num >= count)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValue);
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_30;
					case 3:
						goto IL_2E;
					}
					break;
					IL_30:
					num2 = 1;
					continue;
					IL_2E:
					goto IL_30;
				}
			}
			IL_8F:
			if (false)
			{
			}
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x003A368C File Offset: 0x003A268C
		public void Replace(string oldValue, double newValue)
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_2E;
					case 1:
						goto IL_30;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_87;
						}
						break;
					case 3:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValue);
						num++;
						num2 = 1;
						continue;
					}
					break;
					IL_30:
					num2 = 3;
					continue;
					IL_2E:
					goto IL_30;
				}
			}
			IL_87:
			if (false)
			{
			}
			if (true)
			{
			}
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x003A3730 File Offset: 0x003A2730
		public void Replace(string oldValue, string[] newValues, bool isVertical)
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_2E;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_90;
						}
						break;
					case 2:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValues, isVertical);
						num++;
						num2 = 3;
						continue;
					case 3:
						goto IL_30;
					}
					break;
					IL_30:
					if (true)
					{
					}
					num2 = 2;
					continue;
					IL_2E:
					goto IL_30;
				}
			}
			IL_90:
			if (false)
			{
			}
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x003A37D4 File Offset: 0x003A27D4
		public void Replace(string oldValue, int[] newValues, bool isVertical)
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_36;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36;
						default:
							goto IL_90;
						}
						break;
					case 2:
						goto IL_38;
					case 3:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValues, isVertical);
						num++;
						num2 = 2;
						continue;
					}
					break;
					IL_38:
					num2 = 3;
					continue;
					IL_36:
					goto IL_38;
				}
			}
			IL_90:
			if (false)
			{
			}
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x003A3878 File Offset: 0x003A2878
		public void Replace(string oldValue, double[] newValues, bool isVertical)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36;
						default:
							goto IL_90;
						}
						break;
					case 1:
						goto IL_38;
					case 2:
						goto IL_36;
					case 3:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValues, isVertical);
						num++;
						num2 = 1;
						continue;
					}
					break;
					IL_38:
					num2 = 3;
					continue;
					IL_36:
					goto IL_38;
				}
			}
			IL_90:
			if (false)
			{
			}
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x003A391C File Offset: 0x003A291C
		public void Replace(string oldValue, DataTable newValues, bool isFieldNamesShown)
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValues, isFieldNamesShown);
						num++;
						if (true)
						{
						}
						num2 = 1;
						continue;
					case 1:
						goto IL_30;
					case 2:
						goto IL_2E;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_90;
						}
						break;
					}
					break;
					IL_30:
					num2 = 0;
					continue;
					IL_2E:
					goto IL_30;
				}
			}
			IL_90:
			if (false)
			{
			}
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x003A39C0 File Offset: 0x003A29C0
		public void Replace(string oldValue, DataColumn newValues, bool isFieldNamesShown)
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						this.\u1735[num].Replace(oldValue, newValues, isFieldNamesShown);
						num++;
						num2 = 1;
						continue;
					case 1:
						goto IL_30;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E;
						default:
							goto IL_90;
						}
						break;
					case 3:
						goto IL_2E;
					}
					break;
					IL_30:
					if (true)
					{
					}
					num2 = 0;
					continue;
					IL_2E:
					goto IL_30;
				}
			}
			IL_90:
			if (false)
			{
			}
		}

		// Token: 0x06005CD1 RID: 23761 RVA: 0x003A3A64 File Offset: 0x003A2A64
		public IFont CreateFont()
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
			XlsFont a_ = base.AppImplementation.ᜀ(this);
			return new ExcelFontWrapper(a_, false, false);
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x003A3AB4 File Offset: 0x003A2AB4
		public IFont CreateFont(Font nativeFont)
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
			XlsFont a_ = base.AppImplementation.ᜀ(this, nativeFont);
			return new ExcelFontWrapper(a_, false, false);
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x003A3B08 File Offset: 0x003A2B08
		public IFont AddFont(IFont fontToAdd)
		{
			int a_ = 3;
			FontWrapper fontWrapper;
			XlsFont xlsFont;
			for (;;)
			{
				bool flag = fontToAdd is FontWrapper;
				fontWrapper = null;
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_B1;
					case 1:
						goto IL_E7;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (flag)
							{
								num = 3;
								continue;
							}
							xlsFont = (fontToAdd as XlsFont);
							num = 6;
							continue;
						}
						break;
					case 3:
						fontWrapper = (fontToAdd as FontWrapper);
						num = 5;
						continue;
					case 4:
						goto IL_83;
					case 5:
						if (fontWrapper == null)
						{
							num = 1;
							continue;
						}
						xlsFont = fontWrapper.Wrapped;
						num = 4;
						continue;
					case 6:
						goto IL_83;
					case 7:
						if (flag)
						{
							num = 0;
							continue;
						}
						return xlsFont;
					}
					break;
					IL_83:
					xlsFont = (this.\u1737.Add(xlsFont) as XlsFont);
					num = 7;
				}
			}
			IL_B1:
			fontWrapper.Wrapped = xlsFont;
			fontWrapper.IsReadOnly = true;
			return fontWrapper;
			IL_E7:
			throw new ArgumentNullException(RecordTableEnumerator.b("弸吺匼䬾ᕀⱂф⍆ⵈ", a_));
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x003A3C3C File Offset: 0x003A2C3C
		public IFont CreateFont(IFont baseFont)
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
			return this.CreateFont(baseFont, true);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x003A3C80 File Offset: 0x003A2C80
		public IFont CreateFont(IFont baseFont, bool bAddToCollection)
		{
			int num = 4;
			IFont font;
			for (;;)
			{
				if (true)
				{
				}
				IFont font2;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					((XlsFont)font).Index = this.\u1737.Count;
					this.\u1737.Add(font);
					num = 2;
					continue;
				case 2:
					return font;
				case 3:
					font2 = base.AppImplementation.ᜀ(this);
					goto IL_94;
				case 5:
					font2 = base.AppImplementation.ᜀ(baseFont);
					goto IL_94;
				case 6:
					if (!bAddToCollection)
					{
						return font;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (baseFont == null)
				{
					num = 0;
					continue;
				}
				num = 5;
				continue;
				IL_94:
				font = font2;
				num = 6;
			}
			return font;
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x003A3D74 File Offset: 0x003A2D74
		public IXLSRange FindOne(string findValue, FindType flags)
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (!flag)
						{
							num = 2;
							continue;
						}
						goto IL_130;
					}
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
						break;
					case 2:
						goto IL_93;
					case 3:
						num = 0;
						continue;
					case 4:
					{
						bool flag2;
						if (!flag2)
						{
							num = 9;
							continue;
						}
						goto IL_130;
					}
					case 5:
						goto IL_79;
					case 6:
					{
						bool flag3;
						if (!flag3)
						{
							num = 3;
							continue;
						}
						goto IL_130;
					}
					case 7:
						num = 4;
						continue;
					case 8:
					{
						bool flag4;
						if (!flag4)
						{
							num = 7;
							continue;
						}
						goto IL_130;
					}
					case 9:
						if (true)
						{
						}
						num = 6;
						continue;
					}
					if (findValue == null)
					{
						num = 5;
					}
					else
					{
						bool flag4 = (flags & FindType.Formula) == FindType.Formula;
						bool flag2 = (flags & FindType.Text) == FindType.Text;
						bool flag3 = (flags & FindType.FormulaStringValue) == FindType.FormulaStringValue;
						bool flag = (flags & FindType.Error) == FindType.Error;
						num = 8;
					}
				}
				IL_79:
				return null;
				IL_93:
				throw new ArgumentException(RecordTableEnumerator.b("漾⁀ㅂ⑄⩆ⱈ㽊⡌㵎煐㩒♔睖㝘㑚⥜罞ᝠɢ।๦൨䕪", a_));
				IL_130:
				return ((XlsWorksheetsCollection)this.Worksheets).FindFirst(findValue, flags);
			}
			}
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x003A3EC4 File Offset: 0x003A2EC4
		public IXLSRange FindOne(double findValue, FindType flags)
		{
			int a_ = 12;
			for (;;)
			{
				bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
				bool flag2 = (flags & FindType.Number) == FindType.Number;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (!flag2)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_83;
					case 2:
						goto IL_81;
					case 3:
						IL_3B:
						if (!flag)
						{
							num = 0;
							continue;
						}
						goto IL_83;
					}
					break;
					IL_83:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						goto IL_99;
					}
				}
			}
			IL_81:
			throw new ArgumentException(RecordTableEnumerator.b("ቁ╃㑅⥇❉⥋㩍㕏⁑瑓㽕⭗穙㉛ㅝᑟ䉡ባݥѧͩ࡫䁭", a_));
			IL_99:
			if (false)
			{
			}
			return ((XlsWorksheetsCollection)this.Worksheets).FindFirst(findValue, flags);
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x003A3F84 File Offset: 0x003A2F84
		public IXLSRange FindOne(bool findValue)
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
			return ((XlsWorksheetsCollection)this.Worksheets).FindFirst(findValue);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x003A3FD0 File Offset: 0x003A2FD0
		public IXLSRange FindOne(DateTime findValue)
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
			return ((XlsWorksheetsCollection)this.Worksheets).FindFirst(findValue);
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x003A401C File Offset: 0x003A301C
		public IXLSRange FindOne(TimeSpan findValue)
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
			return ((XlsWorksheetsCollection)this.Worksheets).FindFirst(findValue);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x003A4068 File Offset: 0x003A3068
		public CellRange[] FindAll(string findValue, FindType flags)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (!flag)
						{
							num = 8;
							continue;
						}
						goto IL_12D;
					}
					case 1:
						num = 6;
						continue;
					case 2:
					{
						bool flag2;
						if (!flag2)
						{
							num = 1;
							continue;
						}
						goto IL_12D;
					}
					case 3:
						num = 5;
						continue;
					case 4:
						goto IL_79;
					case 5:
					{
						bool flag3;
						if (!flag3)
						{
							if (true)
							{
							}
							num = 7;
							continue;
						}
						goto IL_12D;
					}
					case 6:
					{
						bool flag4;
						if (!flag4)
						{
							num = 3;
							continue;
						}
						goto IL_12D;
					}
					case 7:
						goto IL_A5;
					case 8:
						num = 2;
						continue;
					case 9:
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
						break;
					}
					if (findValue == null)
					{
						num = 4;
					}
					else
					{
						bool flag = (flags & FindType.Formula) == FindType.Formula;
						bool flag2 = (flags & FindType.Text) == FindType.Text;
						bool flag4 = (flags & FindType.FormulaStringValue) == FindType.FormulaStringValue;
						bool flag3 = (flags & FindType.Error) == FindType.Error;
						num = 0;
					}
				}
				IL_79:
				return null;
				IL_A5:
				throw new ArgumentException(RecordTableEnumerator.b("ᝆ⡈㥊ⱌ≎㑐❒ご╖祘㉚⹜罞འౢᅤ䝦Ὠ੪Ŭٮᕰ嵲", a_));
				IL_12D:
				return ((XlsWorksheetsCollection)this.Worksheets).FindAll(findValue, flags);
			}
			}
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x003A41B4 File Offset: 0x003A31B4
		public CellRange[] FindAll(double findValue, FindType flags)
		{
			int a_ = 14;
			for (;;)
			{
				bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
				bool flag2 = (flags & FindType.Number) == FindType.Number;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_3B:
						if (!flag)
						{
							num = 1;
							continue;
						}
						goto IL_83;
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_81;
					case 3:
						if (!flag2)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_83;
					}
					break;
					IL_83:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						goto IL_99;
					}
				}
			}
			IL_81:
			throw new ArgumentException(RecordTableEnumerator.b("ᑃ❅㩇⭉⅋⭍⑏㝑♓癕㹗㙙㵛㥝፟䉡ൣᕥ䡧ѩͫᩭ偯ѱᕳ᩵ᅷṹ剻", a_));
			IL_99:
			if (false)
			{
			}
			return ((XlsWorksheetsCollection)this.Worksheets).FindAll(findValue, flags);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x003A4274 File Offset: 0x003A3274
		public CellRange[] FindAll(bool findValue)
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
			return ((XlsWorksheetsCollection)this.Worksheets).FindAll(findValue);
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x003A42C0 File Offset: 0x003A32C0
		public CellRange[] FindAll(DateTime findValue)
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
			return ((XlsWorksheetsCollection)this.Worksheets).FindAll(findValue);
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x003A430C File Offset: 0x003A330C
		public CellRange[] FindAll(TimeSpan findValue)
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
			return ((XlsWorksheetsCollection)this.Worksheets).FindAll(findValue);
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x003A4358 File Offset: 0x003A3358
		public void SetSeparators(char argumentsSeparator, char arrayRowsSeparator)
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
			spr\u22F2.ᜁ(argumentsSeparator);
			this.FormulaUtil.SetSeparators(argumentsSeparator, arrayRowsSeparator);
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x003A43A8 File Offset: 0x003A33A8
		public IHFEngine CreateHFEngine()
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
			return new HFEngine(base.ReservedHandle, this);
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x003A43F0 File Offset: 0x003A33F0
		public void Protect(bool bIsProtectWindow, bool bIsProtectContent)
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
			this.Protect(bIsProtectWindow, bIsProtectContent, null);
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x003A4434 File Offset: 0x003A3434
		public void Protect(bool bIsProtectWindow, bool bIsProtectContent, string password)
		{
			int a_ = 14;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
				{
					spr\u24C3 spr_u24C = this.Password;
					num = 9;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_13D;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 4:
					if (this.ᝉ)
					{
						num = 8;
						continue;
					}
					goto IL_13D;
				case 5:
					if (!bIsProtectContent)
					{
						num = 12;
						continue;
					}
					goto IL_7B;
				case 6:
					goto IL_BC;
				case 7:
					return;
				case 8:
					goto IL_103;
				case 9:
				{
					spr\u24C3 spr_u24C;
					spr_u24C.ᜀ((password.Length > 0) ? XlsWorksheetBase.ᜀ(password) : 0);
					num = 7;
					continue;
				}
				case 10:
					if (password != null)
					{
						num = 1;
						continue;
					}
					return;
				case 11:
					if (!this.ᝈ)
					{
						num = 3;
						continue;
					}
					goto IL_105;
				case 12:
					num = 6;
					continue;
				}
				if (!bIsProtectWindow)
				{
					num = 0;
					continue;
				}
				IL_7B:
				num = 11;
				continue;
				IL_13D:
				if (true)
				{
				}
				this.ᝈ = bIsProtectContent;
				this.ᝉ = bIsProtectWindow;
				this.\u1773 = EncryptionType.Standard;
				num = 10;
			}
			IL_BC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ୃ⡅ⵇ橉⍋⡍灏≑㕓⑕㥗㝙⽛繝ൟᝡᝣብ䡧ࡩ५乭⑯ⁱⅳ㍵噷", a_));
			IL_103:
			IL_105:
			throw new NotSupportedException(RecordTableEnumerator.b("ፃ⥅㩇ⅉ⹋⅍㽏㥑瑓㽕⭗穙㵛㉝቟ݡգɥᅧ䩩ᱫᱭὯٱᅳᕵ౷ό᡻偽ꁿ삁꺍뚕춗쾟횡솣얥\udca7誩솫쮭쒯\udab1\udbb3튵隷", a_));
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x003A45CC File Offset: 0x003A35CC
		public void Unprotect()
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
			this.Unprotect(null);
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x003A4610 File Offset: 0x003A3610
		public void Unprotect(string password)
		{
			int a_ = 7;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_A6;
				case 1:
					if (this.\u1755.ᜀ() == 0)
					{
						num = 0;
						continue;
					}
					goto IL_141;
				case 2:
					if (this.\u1755 != null)
					{
						num = 3;
						continue;
					}
					goto IL_A6;
				case 3:
					num = 1;
					continue;
				case 4:
					if (this.\u1772 == null)
					{
						num = 5;
						continue;
					}
					return;
				case 5:
					this.\u1755.ᜀ(0);
					this.\u1773 = EncryptionType.None;
					num = 7;
					continue;
				case 7:
					goto IL_10F;
				case 8:
					goto IL_CF;
				case 9:
					num = 2;
					continue;
				case 10:
					if (XlsWorksheetBase.ᜀ(password) != this.\u1755.ᜀ())
					{
						num = 8;
						continue;
					}
					this.ᝈ = false;
					this.ᝉ = false;
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_141;
				default:
					if (false)
					{
					}
					if (password == null)
					{
						num = 9;
						continue;
					}
					break;
				}
				IL_A6:
				num = 10;
			}
			IL_CF:
			goto IL_141;
			IL_10F:
			return;
			IL_141:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("洼匾⑀≂㙄≆效歊㵌㵎㹐╒㱔㍖㱘筚㹜ぞ፠ᅢdѦᵨ䭪ᵬ๮ɰrɴᡶ୸ὺ嵼୾ꎂ麗力ﮎ랖膠풢쪤햦슨즪슬삮\udab0鶲", a_));
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x003A4774 File Offset: 0x003A3774
		public IWorkbook Clone()
		{
			XlsWorkbook xlsWorkbook;
			for (;;)
			{
				IL_48:
				xlsWorkbook = (XlsWorkbook)base.MemberwiseClone();
				int num = 4;
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
						switch (num)
						{
						case 0:
						{
							this.ខ.Position = 0L;
							byte[] array = new byte[this.ខ.Length];
							this.ខ.Read(array, 0, array.Length);
							this.ខ.Position = 0L;
							xlsWorkbook.ខ = new MemoryStream(array);
							num = 5;
							continue;
						}
						case 1:
						{
							this.ង.Position = 0L;
							byte[] array2 = new byte[this.ង.Length];
							this.ង.Read(array2, 0, array2.Length);
							this.ង.Position = 0L;
							xlsWorkbook.ង = new MemoryStream(array2);
							num = 6;
							continue;
						}
						case 2:
							goto IL_6F;
						case 3:
							if (this.ង != null)
							{
								num = 1;
								continue;
							}
							goto IL_71;
						case 4:
							if (this.\u177C != null)
							{
								num = 2;
								continue;
							}
							goto IL_169;
						case 5:
							goto IL_15A;
						case 6:
							goto IL_71;
						case 7:
							if (this.ខ != null)
							{
								num = 0;
								continue;
							}
							goto IL_200;
						case 8:
							goto IL_169;
						}
						goto IL_48;
						IL_71:
						num = 7;
						continue;
						IL_169:
						if (true)
						{
						}
						xlsWorkbook.\u177E = IntPtr.Zero;
						xlsWorkbook.\u1737 = this.\u1737.Clone(xlsWorkbook);
						xlsWorkbook.\u173A = this.\u173A.ᜀ(xlsWorkbook);
						xlsWorkbook.\u1738 = (sprᡲ)this.\u1738.Clone(xlsWorkbook);
						xlsWorkbook.\u1736 = (StylesCollection)this.\u1736.Clone(xlsWorkbook);
						xlsWorkbook.ᝏ = this.ᜄ();
						num = 3;
						continue;
					}
					IL_6F:
					xlsWorkbook.\u177C = this.\u177C.ᜀ(xlsWorkbook);
					num = 8;
				}
			}
			IL_15A:
			IL_200:
			xlsWorkbook.\u1735 = new WorksheetsCollection((spr\u2158)base.ReservedHandle, xlsWorkbook);
			xlsWorkbook.\u1753 = new ChartsCollection((spr\u2158)base.ReservedHandle, xlsWorkbook);
			xlsWorkbook.ᝣ = new AddInFunctionsCollection((spr\u2158)base.ReservedHandle, xlsWorkbook);
			xlsWorkbook.\u173E = (sprᦖ)spr\u1CD3.ᜀ(this.\u173E);
			xlsWorkbook.ᝢ = (spr\u2594)this.ᝢ.Clone(xlsWorkbook);
			xlsWorkbook.\u1754 = (WorkbookObjectsCollection)this.\u1754.Clone(xlsWorkbook);
			xlsWorkbook.\u173D = (SSTDictionary)this.\u173D.Clone(xlsWorkbook);
			xlsWorkbook.ᝥ = (sprỆ)this.ᝥ.ᜀ(xlsWorkbook);
			xlsWorkbook.ᝫ = (BuiltInDocumentProperties)this.ᝫ.Clone(xlsWorkbook);
			xlsWorkbook.ᝬ = (spr\u1AA2)this.ᝬ.Clone(xlsWorkbook);
			xlsWorkbook.\u1752 = (sprឦ)this.\u1752.Clone(xlsWorkbook);
			xlsWorkbook.\u1759 = (XlsWorkbookShapeData)this.\u1759.Clone(xlsWorkbook);
			xlsWorkbook.ᝤ = (XlsWorkbookShapeData)this.ᝤ.Clone(xlsWorkbook);
			xlsWorkbook.ᝦ = (XlsPivotCachesCollection)spr\u1CD3.ᜀ(this.ᝦ, xlsWorkbook);
			xlsWorkbook.ᝩ = (spr\u233D)this.ᝩ.Clone(xlsWorkbook);
			xlsWorkbook.ᝣ.CopyFrom(this.ᝣ);
			xlsWorkbook.\u173E = (sprᦖ)spr\u1CD3.ᜀ(this.\u173E);
			xlsWorkbook.ᝑ = (spr\u17B5)spr\u1CD3.ᜀ(this.ᝑ);
			xlsWorkbook.\u1755 = (spr\u24C3)spr\u1CD3.ᜀ(this.\u1755);
			xlsWorkbook.\u1756 = (spr\u1938)spr\u1CD3.ᜀ(this.\u1756);
			xlsWorkbook.\u1757 = (spr\u237D)spr\u1CD3.ᜀ(this.\u1757);
			xlsWorkbook.ᝮ = (sprẋ)spr\u1CD3.ᜀ(this.ᝮ);
			xlsWorkbook.\u1739 = spr\u1CD3.ᜀ<sprῚ>(this.\u1739);
			xlsWorkbook.\u173C = spr\u1CD3.ᜀ<spr\u17C1>(this.\u173C);
			xlsWorkbook.\u173F = new List<sprṨ>();
			xlsWorkbook.\u175E = spr\u1CD3.ᜀ<sprỶ>(this.\u175E);
			xlsWorkbook.\u1734 = null;
			xlsWorkbook.ActiveSheetIndex = this.ActiveSheetIndex;
			xlsWorkbook.ᜐ();
			xlsWorkbook.ᝨ = null;
			return xlsWorkbook;
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x003A4BD0 File Offset: 0x003A3BD0
		public void SetWriteProtectionPassword(string password)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᝮ != null)
					{
						num = 9;
						continue;
					}
					return;
				case 1:
					if (this.ᝮ == null)
					{
						num = 3;
						continue;
					}
					goto IL_C1;
				case 2:
					if (true)
					{
					}
					num = 8;
					continue;
				case 3:
					goto IL_64;
				case 4:
					goto IL_EE;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_64;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 6:
					goto IL_C1;
				case 7:
					goto IL_F0;
				case 8:
					if (password.Length == 0)
					{
						num = 7;
						continue;
					}
					num = 1;
					continue;
				case 9:
					goto IL_118;
				}
				if (password != null)
				{
					num = 2;
					continue;
				}
				goto IL_F0;
				IL_64:
				this.ᝮ = (sprẋ)spr\u175E.ᜀ(TBIFFRecord.FileSharing);
				num = 6;
				continue;
				IL_C1:
				this.ᝮ.ᜁ(XlsWorksheetBase.ᜀ(password));
				this.ᝮ.ᜀ(this.Author);
				num = 4;
				continue;
				IL_F0:
				num = 0;
			}
			IL_EE:
			return;
			IL_118:
			this.ᝮ.ᜁ(0);
			this.ᝮ.ᜀ(null);
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x003A4D20 File Offset: 0x003A3D20
		private List<Color> ᜄ()
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
			return new List<Color>(this.ᝏ);
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x003A4D68 File Offset: 0x003A3D68
		private void ᜀ(Stream A_0, ExcelSaveType A_1)
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					IL_12:
					break;
				case 1:
					goto IL_56;
				case 2:
					this.\u177C = new sprវ(this);
					num = 1;
					continue;
				}
				if (this.\u177C == null)
				{
					num = 2;
					continue;
				}
				IL_56:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12;
				default:
					goto IL_6C;
				}
			}
			IL_6C:
			if (false)
			{
			}
			this.\u177C.ᜀ(A_0, A_1);
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x003A4DF4 File Offset: 0x003A3DF4
		private IWorkbookSerializator ᜀ(ExcelVersion A_0, sprᦎ A_1)
		{
			int a_ = 10;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_103;
				case 1:
					if (this.\u177C == null)
					{
						num = 5;
						continue;
					}
					goto IL_C7;
				case 2:
					this.ᜑ();
					num = 8;
					continue;
				case 3:
					switch (A_0)
					{
					case ExcelVersion.Version97to2003:
						goto IL_8F;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						goto IL_75;
					default:
						num = 4;
						continue;
					}
					break;
				case 4:
					num = 6;
					continue;
				case 5:
					this.\u177C = new sprវ(this, A_0);
					num = 0;
					continue;
				case 6:
					goto IL_110;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 8:
					goto IL_96;
				}
				if (this.\u173E.ᜅ() > 1370)
				{
					num = 2;
					continue;
				}
				goto IL_96;
				IL_75:
				num = 1;
				continue;
				IL_96:
				if (true)
				{
				}
				num = 3;
			}
			IL_8F:
			return new XlsWorkbook.WorkbookExcel97Serializator(A_1);
			IL_C7:
			return this.\u177C;
			IL_103:
			goto IL_C7;
			IL_110:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㘿❁㙃㕅ⅇ╉≋", a_));
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x003A4F28 File Offset: 0x003A3F28
		private void ᜃ()
		{
			for (;;)
			{
				int num;
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
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						List<int> a_ = this.ᜂ();
						int[] a_2 = this.ᜀ(a_);
						this.ᜀ(a_2);
						if (true)
						{
						}
						num = 0;
						continue;
					}
					case 2:
						if (this.ᜁ())
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x003A4FB4 File Offset: 0x003A3FB4
		private void ᜀ(int[] A_0)
		{
			int a_ = 16;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C2;
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)this.\u1754[num2];
					xlsWorksheetBase.UpdateStyleIndexes(A_0);
					num2++;
					num = 0;
					continue;
				}
				case 2:
					return;
				case 3:
					goto IL_C2;
				case 5:
					goto IL_6A;
				}
				IL_3B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3B;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					int num2 = 0;
					int count = this.\u1754.Count;
					num = 3;
					continue;
				}
				}
				IL_C2:
				num = 1;
			}
			IL_6A:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕅㱇㍉⁋⭍᥏㱑こ㍕⁗㽙⽛", a_));
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x003A50A0 File Offset: 0x003A40A0
		private List<int> ᜂ()
		{
			switch (0)
			{
			default:
			{
				List<int> list;
				for (;;)
				{
					list = new List<int>();
					int num = 0;
					int num2 = XlsWorkbook.ᜯ.Length;
					int num3 = 9;
					for (;;)
					{
						if (true)
						{
						}
						XlsStyle xlsStyle;
						string name;
						int num5;
						XlsStyle xlsStyle2;
						switch (num3)
						{
						case 0:
							num3 = 10;
							continue;
						case 1:
							num3 = 5;
							continue;
						case 2:
							if (xlsStyle == null)
							{
								num3 = 1;
								continue;
							}
							num3 = 11;
							continue;
						case 3:
							if (!this.\u1736.Contains(name))
							{
								num3 = 0;
								continue;
							}
							num3 = 7;
							continue;
						case 4:
						{
							if (num >= num2)
							{
								num3 = 6;
								continue;
							}
							int num4 = XlsWorkbook.ᜯ[num];
							name = XlsStyle.DEF_DEFAULT_STYLES[num4];
							num3 = 3;
							continue;
						}
						case 5:
							num5 = -1;
							goto IL_F5;
						case 6:
							return list;
						case 7:
							goto IL_162;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_162;
							default:
								if (false)
								{
								}
								goto IL_9A;
							}
							break;
						case 9:
							goto IL_9A;
						case 10:
							xlsStyle2 = null;
							goto IL_130;
						case 11:
							num5 = xlsStyle.ExtendedFormatIndex;
							goto IL_F5;
						}
						break;
						IL_9A:
						num3 = 4;
						continue;
						IL_F5:
						int item = num5;
						list.Add(item);
						num++;
						num3 = 8;
						continue;
						IL_130:
						xlsStyle = xlsStyle2;
						num3 = 2;
						continue;
						IL_162:
						xlsStyle2 = (XlsStyle)this.\u1736[name];
						goto IL_130;
					}
				}
				return list;
			}
			}
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x003A5230 File Offset: 0x003A4230
		private bool ᜁ()
		{
			bool flag;
			for (;;)
			{
				flag = false;
				int num;
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
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!flag)
						{
							num = 1;
							continue;
						}
						return flag;
					case 1:
						flag = (this.DefaultXFIndex != 15);
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						return flag;
					}
					break;
				}
			}
			return flag;
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x003A52B4 File Offset: 0x003A42B4
		private int[] ᜀ(List<int> A_0)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 13;
				sprᢖ u;
				int[] array;
				for (;;)
				{
					int num2;
					int num3;
					spr\u192F spr_u192F2;
					int num6;
					int num7;
					int count4;
					switch (num)
					{
					case 0:
						goto IL_244;
					case 1:
						goto IL_C7;
					case 2:
					{
						spr\u192F spr_u192F;
						if (spr_u192F.ᝇ())
						{
							num = 12;
							continue;
						}
						goto IL_1DC;
					}
					case 3:
					{
						List<int> a_2 = this.ᜀ();
						u = this.\u1738;
						this.\u1738 = new sprᢖ(base.AppImplementation, this);
						this.InsertDefaultExtFormats();
						num2 = 0;
						int count = A_0.Count;
						num = 27;
						continue;
					}
					case 4:
					{
						num3 = 1;
						int count2 = u.Count;
						num = 1;
						continue;
					}
					case 5:
					{
						int num4 = XlsWorkbook.ᜰ[num2];
						int num5;
						array[num5] = num4;
						this.\u1738.ᜀ(num4, u.ᜁ(num5));
						num = 19;
						continue;
					}
					case 6:
					{
						int count2;
						if (num3 >= count2)
						{
							num = 15;
							continue;
						}
						spr_u192F2 = u.ᜁ(num3);
						List<int> a_2;
						this.ᜀ(spr_u192F2, a_2);
						spr_u192F2.ᜁ(Math.Min(spr_u192F2.\u171A(), this.\u1778));
						num = 24;
						continue;
					}
					case 7:
						goto IL_1AB;
					case 8:
					{
						spr\u192F spr_u192F;
						spr_u192F.ᜄ(array[spr_u192F.ᜯ()]);
						spr_u192F = this.\u1738.ᜁ(spr_u192F);
						array[num6] = spr_u192F.ᜠ();
						num = 26;
						continue;
					}
					case 9:
					{
						int count;
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						int num5 = A_0[num2];
						num = 20;
						continue;
					}
					case 10:
						goto IL_C2;
					case 11:
					{
						int count3;
						if (num6 < count3)
						{
							spr\u192F spr_u192F = u.ᜁ(num6);
							num = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_244;
						default:
							if (false)
							{
							}
							num = 17;
							continue;
						}
						break;
					}
					case 12:
						num = 23;
						continue;
					case 14:
						goto IL_32F;
					case 15:
					{
						num6 = 1;
						int count3 = u.Count;
						num = 25;
						continue;
					}
					case 16:
						goto IL_2B3;
					case 17:
						goto IL_2F0;
					case 18:
						if (true)
						{
						}
						if (spr_u192F2.ᜤ() == ExcelPatternType.Gradient)
						{
							num = 29;
							continue;
						}
						goto IL_1AB;
					case 19:
						goto IL_387;
					case 20:
					{
						int num5;
						if (num5 >= 0)
						{
							num = 5;
							continue;
						}
						goto IL_387;
					}
					case 21:
						num = 18;
						continue;
					case 22:
						if (array[num3] < 0)
						{
							num = 21;
							continue;
						}
						goto IL_1AB;
					case 23:
					{
						spr\u192F spr_u192F;
						if (spr_u192F.ᜠ() != this.\u177B)
						{
							num = 8;
							continue;
						}
						goto IL_1DC;
					}
					case 24:
						if (!spr_u192F2.ᝇ())
						{
							num = 0;
							continue;
						}
						goto IL_1AB;
					case 25:
						goto IL_2B3;
					case 26:
						goto IL_1DC;
					case 27:
						goto IL_2F5;
					case 28:
						goto IL_C7;
					case 29:
					{
						spr_u192F2.ᜀ(ExcelPatternType.Solid);
						ExcelColors knownColor = spr_u192F2.ᝐ().BackColorObject.ᜂ(this);
						spr_u192F2.ᝄ().SetKnownColor(knownColor);
						num = 7;
						continue;
					}
					case 30:
						goto IL_2F5;
					case 31:
						goto IL_32F;
					case 32:
						if (num7 >= count4)
						{
							num = 3;
							continue;
						}
						array[num7] = -1;
						num7++;
						num = 14;
						continue;
					}
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					count4 = this.\u1738.Count;
					array = new int[count4];
					num7 = 0;
					num = 31;
					continue;
					IL_C7:
					num = 6;
					continue;
					IL_1AB:
					spr_u192F2 = this.\u1738.ᜀ(spr_u192F2);
					array[num3] = spr_u192F2.ᜠ();
					num3++;
					num = 28;
					continue;
					IL_1DC:
					num6++;
					num = 16;
					continue;
					IL_244:
					num = 22;
					continue;
					IL_2B3:
					num = 11;
					continue;
					IL_2F5:
					num = 9;
					continue;
					IL_32F:
					num = 32;
					continue;
					IL_387:
					num2++;
					num = 30;
				}
				IL_C2:
				throw new ArgumentNullException(RecordTableEnumerator.b("♁⅃⁅⥇㽉⁋㩍͏♑ⵓ㩕㵗ፙ㉛㩝՟ᩡţᕥ", a_));
				IL_2F0:
				this.\u1738.ᜀ(15, u.ᜁ(this.DefaultXFIndex));
				array[this.\u177B] = 15;
				this.\u177B = 15;
				return array;
			}
			}
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x003A578C File Offset: 0x003A478C
		private void ᜀ(spr\u192F A_0, List<int> A_1)
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
			A_0.ᝄ().ᜀ(this);
			A_0.\u1754().ᜀ(this);
			A_0.\u173F().ᜀ(this);
			A_0.ᜡ().ᜀ(this);
			A_0.ᝅ().ᜀ(this);
			A_0.\u1756().ᜀ(this);
			A_0.\u171F().ᜀ(this);
			int index = A_0.\u173B();
			A_0.ᜂ(A_1[index]);
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x003A5830 File Offset: 0x003A4830
		private List<int> ᜀ()
		{
			switch (0)
			{
			default:
			{
				XlsFontsCollection xlsFontsCollection;
				List<int> list;
				for (;;)
				{
					IL_3F:
					int count = this.\u1737.Count;
					xlsFontsCollection = new XlsFontsCollection((spr\u2158)base.AppImplementation, this);
					list = new List<int>(count);
					int num = 0;
					for (;;)
					{
						IL_66:
						int num2 = 6;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								int num3;
								if (num3 > 5)
								{
									num2 = 1;
									continue;
								}
								XlsFont xlsFont = (XlsFont)xlsFont.Clone();
								xlsFontsCollection.ForceAdd(xlsFont);
								num3++;
								num2 = 9;
								continue;
							}
							case 1:
								goto IL_164;
							case 2:
								if (count < 5)
								{
									num2 = 8;
									continue;
								}
								goto IL_1B1;
							case 3:
								count = xlsFontsCollection.Count;
								num2 = 2;
								continue;
							case 4:
								goto IL_166;
							case 5:
								if (true)
								{
								}
								goto IL_144;
							case 6:
								goto IL_166;
							case 7:
							{
								if (num >= count)
								{
									num2 = 3;
									continue;
								}
								XlsFont xlsFont2 = (XlsFont)this.\u1737[num];
								OColor ocolor = xlsFont2.OColor;
								ocolor.ᜀ(this);
								xlsFont2 = (XlsFont)xlsFontsCollection.Add(xlsFont2);
								list.Add(xlsFont2.Index);
								num++;
								num2 = 4;
								continue;
							}
							case 8:
							{
								XlsFont xlsFont = (XlsFont)xlsFontsCollection[0];
								int num3 = count;
								num2 = 5;
								continue;
							}
							case 9:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_66;
								default:
									if (false)
									{
									}
									goto IL_144;
								}
								break;
							}
							goto IL_3F;
							IL_144:
							num2 = 0;
							continue;
							IL_166:
							num2 = 7;
						}
					}
				}
				IL_164:
				IL_1B1:
				this.\u1737 = xlsFontsCollection;
				return list;
			}
			}
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x003A59F8 File Offset: 0x003A49F8
		private bool ᜀ(Stream A_0, Encoding A_1, string A_2)
		{
			switch (0)
			{
			default:
			{
				bool flag;
				for (;;)
				{
					StreamReader streamReader = new StreamReader(A_0, A_1);
					string text = streamReader.ReadToEnd();
					int num = 0;
					int num2 = 0;
					int num3 = 1;
					flag = true;
					int length = A_2.Length;
					double num4 = (double)text.Length;
					int num5 = 13;
					for (;;)
					{
						switch (num5)
						{
						case 0:
							if (flag)
							{
								num5 = 7;
								continue;
							}
							goto IL_1B9;
						case 1:
							goto IL_84;
						case 2:
							num5 = 12;
							continue;
						case 3:
							goto IL_11E;
						case 4:
							if ((double)num >= num4)
							{
								num5 = 3;
								continue;
							}
							num = text.IndexOf('"', num);
							num++;
							num3 = num;
							num2++;
							num5 = 11;
							continue;
						case 5:
							num5 = 8;
							continue;
						case 6:
							if (num3 != 0)
							{
								if (true)
								{
								}
								num5 = 10;
								continue;
							}
							goto IL_1B9;
						case 7:
							num5 = 6;
							continue;
						case 8:
							if (num2 % 2 != 0)
							{
								num5 = 9;
								continue;
							}
							goto IL_84;
						case 9:
							flag = false;
							num5 = 1;
							continue;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_84;
							default:
								if (false)
								{
								}
								num5 = 4;
								continue;
							}
							break;
						case 11:
							if ((double)(num + length) <= num4)
							{
								num5 = 2;
								continue;
							}
							goto IL_84;
						case 12:
							if (text.Substring(num, length) == A_2)
							{
								num5 = 5;
								continue;
							}
							goto IL_84;
						case 13:
							goto IL_84;
						}
						break;
						IL_84:
						num5 = 0;
					}
				}
				IL_11E:
				IL_1B9:
				A_0.Position = 0L;
				return flag;
			}
			}
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x003A5BC8 File Offset: 0x003A4BC8
		[CLSCompliant(false)]
		internal void ᜀ(RecordArrayList A_0, XlsWorksheet A_1)
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
			XlsWorkbook.WorkbookExcel97Serializator workbookExcel97Serializator = new XlsWorkbook.WorkbookExcel97Serializator(null);
			workbookExcel97Serializator.ᜀ(A_0, ExcelSaveType.SaveAsXLS, null, this, A_1, true);
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x003A5C18 File Offset: 0x003A4C18
		public void SetActiveWorksheet(XlsWorksheetBase sheet)
		{
			for (;;)
			{
				this.\u1734 = sheet;
				int realIndex = sheet.RealIndex;
				spr\u17B5 spr_u17B = this.WindowOne;
				spr_u17B.ᜇ((ushort)realIndex);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_71;
					case 1:
						if (spr_u17B.ᜈ() > (ushort)realIndex)
						{
							num = 2;
							continue;
						}
						goto IL_71;
					case 2:
						if (true)
						{
						}
						spr_u17B.ᜆ((ushort)realIndex);
						num = 0;
						continue;
					}
					break;
					IL_71:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_87;
					}
				}
			}
			IL_87:
			if (false)
			{
			}
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x003A5CB4 File Offset: 0x003A4CB4
		public bool ContainsFont(XlsFont font)
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
			return this.\u1737.Contains(font);
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x003A5CFC File Offset: 0x003A4CFC
		public void UpdateNamedRangeIndexes(int[] newIndex)
		{
			int a_ = 6;
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_60;
				default:
				{
					if (false)
					{
					}
					int num2;
					int count;
					switch (num)
					{
					case 0:
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						((XlsWorksheet)this.\u1735[num2]).UpdateNamedRangeIndexes(newIndex);
						num2++;
						num = 3;
						continue;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						goto IL_B6;
					case 4:
						goto IL_60;
					case 5:
						goto IL_B6;
					}
					if (newIndex == null)
					{
						num = 4;
						break;
					}
					num2 = 0;
					count = this.\u1735.Count;
					num = 5;
					break;
					IL_B6:
					num = 0;
					break;
				}
				}
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("刻嬽㜿ୁ⩃≅ⵇ㉉", a_));
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x003A5DE8 File Offset: 0x003A4DE8
		public void UpdateNamedRangeIndexes(IDictionary<int, int> dicNewIndex)
		{
			int a_ = 3;
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_60;
				default:
				{
					if (false)
					{
					}
					int num2;
					int count;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						goto IL_60;
					case 2:
						return;
					case 3:
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						((XlsWorksheet)this.\u1735[num2]).UpdateNamedRangeIndexes(dicNewIndex);
						num2++;
						num = 5;
						continue;
					case 4:
						goto IL_B6;
					case 5:
						goto IL_B6;
					}
					if (dicNewIndex == null)
					{
						num = 1;
						break;
					}
					num2 = 0;
					count = this.\u1735.Count;
					num = 4;
					break;
					IL_B6:
					num = 3;
					break;
				}
				}
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("崸刺帼焾⑀㑂ౄ⥆ⵈ⹊㕌", a_));
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x003A5ED4 File Offset: 0x003A4ED4
		public void SetChanged()
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
			this.Saved = false;
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x003A5F18 File Offset: 0x003A4F18
		public void UpdateStringIndexes(List<int> arrNewIndexes)
		{
			int a_ = 15;
			for (;;)
			{
				if (true)
				{
				}
				if (arrNewIndexes != null)
				{
					goto IL_50;
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
			throw new ArgumentNullException(RecordTableEnumerator.b("⑄㕆㭈Պ⡌㡎ᡐ㵒ㅔ㉖⅘㹚⹜", a_));
			IL_50:
			this.\u1735.UpdateStringIndexes(arrNewIndexes);
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x003A5F84 File Offset: 0x003A4F84
		[CLSCompliant(false)]
		internal Dictionary<int, int> ᜀ(sprᦖ A_0, Dictionary<int, int> A_1)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 9;
				for (;;)
				{
					int num2;
					sprᦖ.ᜀ ᜀ;
					int num4;
					Dictionary<int, int> dictionary;
					sprᦖ sprᦖ;
					switch (num)
					{
					case 0:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 6;
							continue;
						}
						ᜀ = A_0.ᜃ()[num2];
						num4 = (int)ᜀ.ᜁ();
						num = 7;
						continue;
					}
					case 1:
						goto IL_106;
					case 2:
						goto IL_13B;
					case 3:
						num4 = A_1[num4];
						num = 2;
						continue;
					case 4:
						goto IL_101;
					case 5:
						goto IL_8A;
					case 6:
						return dictionary;
					case 7:
						if (A_1.ContainsKey(num4))
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_13B;
					case 8:
					{
						if (A_1 == null)
						{
							num = 4;
							continue;
						}
						sprᦖ = this.ExternSheet;
						dictionary = new Dictionary<int, int>();
						num2 = 0;
						int num3 = (int)A_0.ᜅ();
						goto IL_184;
					}
					case 10:
						goto IL_106;
					}
					if (A_0 != null)
					{
						num = 8;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_184;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					IL_106:
					num = 0;
					continue;
					IL_13B:
					int value = sprᦖ.ᜀ(num4, (int)ᜀ.ᜀ(), (int)ᜀ.ᜂ());
					dictionary.Add(num2, value);
					num2++;
					num = 1;
					continue;
					IL_184:
					num = 10;
				}
				IL_8A:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⱈ㍊㥌⩎⍐㵒ٔ㽖㱘㹚⥜", a_));
				IL_101:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⅈ⩊㹌❎ɐ♒㝔ᕖ㙘㑚㙜ⱞ", a_));
			}
			}
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x003A613C File Offset: 0x003A513C
		internal void ᜦ()
		{
			for (;;)
			{
				int num = 0;
				int count = this.\u1735.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						goto IL_4E;
					case 1:
						goto IL_30;
					case 2:
						goto IL_30;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4E;
						}
						goto Block_2;
					}
					break;
					IL_30:
					if (true)
					{
					}
					num2 = 0;
					continue;
					IL_4E:
					XlsWorksheet xlsWorksheet = (XlsWorksheet)this.\u1735[num];
					xlsWorksheet.ᜭ();
					num++;
					num2 = 2;
				}
			}
			Block_2:
			if (false)
			{
			}
		}

		// Token: 0x06005CFC RID: 23804 RVA: 0x003A61E4 File Offset: 0x003A51E4
		public void UpdateXFIndexes(int maxCount)
		{
			int a_ = 10;
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_61;
				default:
				{
					if (false)
					{
					}
					int num2;
					int count;
					switch (num)
					{
					case 0:
						return;
					case 2:
						goto IL_61;
					case 3:
						goto IL_B9;
					case 4:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						XlsWorksheet xlsWorksheet = (XlsWorksheet)this.\u1735[num2];
						xlsWorksheet.UpdateExtendedFormatIndex(maxCount);
						num2++;
						num = 5;
						continue;
					}
					case 5:
						goto IL_B9;
					}
					if (true)
					{
					}
					if (maxCount <= 0)
					{
						num = 2;
						break;
					}
					num2 = 0;
					count = this.\u1735.Count;
					num = 3;
					break;
					IL_B9:
					num = 4;
					break;
				}
				}
			}
			IL_61:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⴿ⍁㱃Յ❇㽉≋㩍", a_));
		}

		// Token: 0x06005CFD RID: 23805 RVA: 0x003A62D0 File Offset: 0x003A52D0
		public bool IsFormatted(int xfIndex)
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
			return xfIndex != this.DefaultXFIndex;
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x003A6318 File Offset: 0x003A5318
		public double GetMaxDigitWidth()
		{
			int a_ = 11;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Font font = this.\u1736[RecordTableEnumerator.b("ཀⱂ㝄⩆⡈❊", a_)].Font.GenerateNativeFont();
			return this.GetMaxDigitWidth(font);
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x003A6388 File Offset: 0x003A5388
		public double GetMaxDigitHeight()
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			Font font = this.\u1736[RecordTableEnumerator.b("ه╉㹋⍍ㅏ㹑", a_)].Font.GenerateNativeFont();
			return (double)((int)Math.Ceiling((double)this.ᝧ.MeasureString(RecordTableEnumerator.b("㡇ᩉ", a_), font).Height));
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x003A641C File Offset: 0x003A541C
		public double GetMaxDigitWidth(Font font)
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
			return this.ᜀ(font, new XlsWorkbook.DigitSizeCallback(this.ᜁ));
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x003A646C File Offset: 0x003A546C
		public double GetMaxDigitHeight(Font font)
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
			return this.ᜀ(font, new XlsWorkbook.DigitSizeCallback(this.ᜀ), new char[]
			{
				'p',
				'P'
			});
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x003A64CC File Offset: 0x003A54CC
		private void ᜁ(RectangleF A_0, ref double A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_50;
				case 1:
					A_1 = (double)A_0.Width;
					num = 0;
					continue;
				}
				IL_1C:
				if ((double)A_0.Width > A_1)
				{
					num = 1;
					continue;
				}
				IL_50:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1C;
				default:
					goto IL_66;
				}
			}
			IL_66:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x003A6550 File Offset: 0x003A5550
		private void ᜀ(RectangleF A_0, ref double A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					A_1 = (double)A_0.Height;
					num = 2;
					continue;
				case 2:
					goto IL_50;
				}
				IL_1C:
				if ((double)A_0.Height > A_1)
				{
					num = 1;
					continue;
				}
				IL_50:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1C;
				default:
					goto IL_66;
				}
			}
			IL_66:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x003A65D4 File Offset: 0x003A55D4
		private double ᜀ(Font A_0, XlsWorkbook.DigitSizeCallback A_1)
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
			return this.ᜀ(A_0, A_1, new char[]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9'
			});
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x003A662C File Offset: 0x003A562C
		private double ᜀ(Font A_0, XlsWorkbook.DigitSizeCallback A_1, char[] A_2)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				double result;
				for (;;)
				{
					IL_56:
					if (true)
					{
					}
					StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
					stringFormat.Alignment = StringAlignment.Near;
					stringFormat.SetMeasurableCharacterRanges(new CharacterRange[]
					{
						new CharacterRange(1, 1)
					});
					RectangleF bounds = new RectangleF(0f, 0f, 1000f, 1000f);
					result = 0.0;
					int num = 0;
					int num2 = A_2.Length;
					int num3 = 0;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							switch (num3)
							{
							case 0:
								goto IL_CE;
							case 1:
								return result;
							case 2:
							{
								if (num >= num2)
								{
									num3 = 1;
									continue;
								}
								char c = A_2[num];
								Region[] array = this.ᝧ.MeasureCharacterRanges(RecordTableEnumerator.b("獂", a_) + c, A_0, bounds, stringFormat);
								Region region = array[0];
								bounds = region.GetBounds(this.ᝧ);
								A_1(bounds, ref result);
								num++;
								num3 = 3;
								continue;
							}
							case 3:
								goto IL_CE;
							}
							goto IL_56;
							IL_CE:
							num3 = 2;
							break;
						}
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x003A6788 File Offset: 0x003A5788
		public double WidthToFileWidth(double width)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_67;
			}
			if (false)
			{
			}
			double maxDigitWidth = this.MaxDigitWidth;
			if (width <= 1.0)
			{
				if (true)
				{
				}
				return width * (maxDigitWidth + 5.0) / maxDigitWidth * 256.0 / 256.0;
			}
			IL_67:
			return (width * maxDigitWidth + 5.0) / maxDigitWidth * 256.0 / 256.0;
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x003A6820 File Offset: 0x003A5820
		public double FileWidthToPixels(double fileWidth)
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
			double maxDigitWidth = this.MaxDigitWidth;
			return spr\u1DBE.ᜀ((256.0 * fileWidth + spr\u1DBE.ᜀ(128.0 / maxDigitWidth)) / 256.0 * maxDigitWidth);
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x003A6890 File Offset: 0x003A5890
		public double PixelsToWidth(double pixels)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4B;
			}
			if (false)
			{
			}
			double maxDigitWidth = this.MaxDigitWidth;
			if (pixels <= maxDigitWidth + 5.0)
			{
				return pixels / (maxDigitWidth + 5.0);
			}
			IL_4B:
			if (true)
			{
			}
			return (pixels - 5.0) / maxDigitWidth;
		}

		// Token: 0x06005D09 RID: 23817 RVA: 0x003A6900 File Offset: 0x003A5900
		internal void ᜬ()
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_72;
					case 1:
						goto IL_72;
					case 2:
						return;
					case 3:
					{
						int num2;
						int[] array;
						if (num2 >= array.Length)
						{
							num = 2;
							continue;
						}
						int a_ = array[num2];
						this.ᜃ(a_);
						num2++;
						num = 0;
						continue;
					}
					case 4:
					{
						int[] indexes = this.ᝦ.GetIndexes();
						int[] array = indexes;
						int num2 = 0;
						num = 1;
						continue;
					}
					}
					if (this.ᝦ == null)
					{
						break;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					IL_72:
					num = 3;
				}
				return;
			}
			}
		}

		// Token: 0x06005D0A RID: 23818 RVA: 0x003A69E0 File Offset: 0x003A59E0
		internal void ᜃ(int A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_4D:
					if (true)
					{
					}
					bool flag = false;
					IWorksheets worksheets = this.Worksheets;
					IEnumerator enumerator = worksheets.GetEnumerator();
					int num = 0;
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
							switch (num)
							{
							case 0:
								try
								{
									num = 6;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (!flag)
											{
												num = 8;
												continue;
											}
											goto IL_1B4;
										case 1:
										{
											if (!enumerator.MoveNext())
											{
												num = 10;
												continue;
											}
											IWorksheet worksheet = (IWorksheet)enumerator.Current;
											PivotTablesCollection pivotTables = worksheet.PivotTables;
											int num2 = 0;
											num = 9;
											continue;
										}
										case 2:
										{
											IPivotTable pivotTable;
											if (pivotTable.CacheIndex == A_0)
											{
												num = 5;
												continue;
											}
											int num2;
											num2++;
											num = 3;
											continue;
										}
										case 3:
											goto IL_17A;
										case 4:
											goto IL_1C0;
										case 5:
											flag = true;
											num = 11;
											continue;
										case 7:
										{
											PivotTablesCollection pivotTables;
											int num2;
											if (num2 >= pivotTables.Count)
											{
												num = 12;
												continue;
											}
											IPivotTable pivotTable = pivotTables[num2];
											num = 2;
											continue;
										}
										case 9:
											goto IL_17A;
										case 10:
											goto IL_1B4;
										case 11:
											goto IL_FB;
										case 12:
											goto IL_FB;
										}
										goto IL_D4;
										IL_FB:
										num = 0;
										continue;
										IL_115:
										num = 1;
										continue;
										IL_D4:
										goto IL_115;
										IL_17A:
										num = 7;
										continue;
										IL_1B4:
										num = 4;
									}
									IL_1C0:
									goto IL_71;
								}
								finally
								{
									for (;;)
									{
										IDisposable disposable = enumerator as IDisposable;
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_20B;
											case 1:
												if (disposable != null)
												{
													num = 2;
													continue;
												}
												goto IL_20D;
											case 2:
												disposable.Dispose();
												num = 0;
												continue;
											}
											break;
										}
									}
									IL_20B:
									IL_20D:;
								}
								goto IL_20E;
								IL_71:
								num = 2;
								continue;
							case 1:
								return;
							case 2:
								if (!flag)
								{
									num = 3;
									continue;
								}
								return;
							case 3:
								goto IL_89;
							}
							goto IL_4D;
						}
						IL_20E:
						this.ᝦ.RemoveAt(A_0);
						num = 1;
						continue;
						IL_89:
						goto IL_20E;
					}
				}
				return;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06005D0B RID: 23819 RVA: 0x003A6C34 File Offset: 0x003A5C34
		// (remove) Token: 0x06005D0C RID: 23820 RVA: 0x003A6CCC File Offset: 0x003A5CCC
		public event EventHandler OnFileSaved
		{
			add
			{
				for (;;)
				{
					EventHandler eventHandler = this.ព;
					int num = 0;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						}
						if (false)
						{
						}
						EventHandler eventHandler2;
						switch (num)
						{
						case 0:
							goto IL_4B;
						case 1:
							if (eventHandler == eventHandler2)
							{
								goto IL_7C;
							}
							goto IL_4B;
						case 2:
							return;
						}
						break;
						IL_4B:
						eventHandler2 = eventHandler;
						EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
						eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ព, value2, eventHandler2);
						if (true)
						{
						}
						num = 1;
						continue;
						IL_7C:
						num = 2;
					}
				}
			}
			remove
			{
				for (;;)
				{
					EventHandler eventHandler = this.ព;
					int num = 2;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						}
						if (false)
						{
						}
						EventHandler eventHandler2;
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (eventHandler == eventHandler2)
							{
								goto IL_7C;
							}
							goto IL_53;
						case 2:
							if (true)
							{
							}
							goto IL_53;
						}
						break;
						IL_53:
						eventHandler2 = eventHandler;
						EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
						eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ព, value2, eventHandler2);
						num = 1;
						continue;
						IL_7C:
						num = 0;
					}
				}
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06005D0D RID: 23821 RVA: 0x003A6D64 File Offset: 0x003A5D64
		// (remove) Token: 0x06005D0E RID: 23822 RVA: 0x003A6DFC File Offset: 0x003A5DFC
		public event ReadOnlyFileEventHandler OnReadOnlyFile
		{
			add
			{
				for (;;)
				{
					ReadOnlyFileEventHandler readOnlyFileEventHandler = this.ភ;
					int num = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						}
						if (false)
						{
						}
						ReadOnlyFileEventHandler readOnlyFileEventHandler2;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (readOnlyFileEventHandler == readOnlyFileEventHandler2)
							{
								goto IL_7C;
							}
							goto IL_4B;
						case 1:
							goto IL_4B;
						case 2:
							return;
						}
						break;
						IL_4B:
						readOnlyFileEventHandler2 = readOnlyFileEventHandler;
						ReadOnlyFileEventHandler value2 = (ReadOnlyFileEventHandler)Delegate.Combine(readOnlyFileEventHandler2, value);
						readOnlyFileEventHandler = Interlocked.CompareExchange<ReadOnlyFileEventHandler>(ref this.ភ, value2, readOnlyFileEventHandler2);
						num = 0;
						continue;
						IL_7C:
						num = 2;
					}
				}
			}
			remove
			{
				for (;;)
				{
					if (true)
					{
					}
					ReadOnlyFileEventHandler readOnlyFileEventHandler = this.ភ;
					int num = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7C;
						}
						if (false)
						{
						}
						ReadOnlyFileEventHandler readOnlyFileEventHandler2;
						switch (num)
						{
						case 0:
							if (readOnlyFileEventHandler == readOnlyFileEventHandler2)
							{
								goto IL_7C;
							}
							goto IL_53;
						case 1:
							goto IL_53;
						case 2:
							return;
						}
						break;
						IL_53:
						readOnlyFileEventHandler2 = readOnlyFileEventHandler;
						ReadOnlyFileEventHandler value2 = (ReadOnlyFileEventHandler)Delegate.Remove(readOnlyFileEventHandler2, value);
						readOnlyFileEventHandler = Interlocked.CompareExchange<ReadOnlyFileEventHandler>(ref this.ភ, value2, readOnlyFileEventHandler2);
						num = 0;
						continue;
						IL_7C:
						num = 2;
					}
				}
			}
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x003A6E94 File Offset: 0x003A5E94
		private void ᜀ(object A_0, EventArgs A_1)
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
			this.ᝰ = -1;
			this.\u1771 = -1;
			this.\u177D = this.GetMaxDigitWidth();
			this.StandardRowHeightInPixels = (int)this.GetMaxDigitHeight();
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06005D10 RID: 23824 RVA: 0x003A6EF8 File Offset: 0x003A5EF8
		// (set) Token: 0x06005D11 RID: 23825 RVA: 0x003A6FA4 File Offset: 0x003A5FA4
		public double StandardRowHeight
		{
			get
			{
				int num = 1;
				double num2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return num2;
					case 2:
						return num2;
					case 3:
						num2 = this.\u1735[0].DefaultRowHeight;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return num2;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (this.\u1735.Count > 0)
					{
						num = 3;
					}
					else
					{
						num2 = this.GetMaxDigitHeight();
						num2 = spr\u17FF.ᜁ(num2, MeasureUnits.Point);
						num = 2;
					}
				}
				return num2;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						goto IL_84;
					case 3:
					{
						int num2 = 0;
						int count = this.\u1735.Count;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_84;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					}
					case 4:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						this.\u1735[num2].DefaultRowHeight = value;
						num2++;
						num = 2;
						continue;
					}
					case 5:
						goto IL_46;
					}
					if (true)
					{
					}
					if (value != this.StandardRowHeight)
					{
						num = 3;
						continue;
					}
					break;
					IL_46:
					num = 4;
					continue;
					IL_84:
					goto IL_46;
				}
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x003A7070 File Offset: 0x003A6070
		// (set) Token: 0x06005D13 RID: 23827 RVA: 0x003A70B8 File Offset: 0x003A60B8
		public int StandardRowHeightInPixels
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
				return (int)spr\u17FF.ᜁ(this.StandardRowHeight, MeasureUnits.Point);
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
				double standardRowHeight = spr\u17FF.ᜀ((double)value, MeasureUnits.Point);
				this.StandardRowHeight = standardRowHeight;
			}
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x003A7104 File Offset: 0x003A6104
		internal void ᜤ()
		{
			int a_ = 11;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new Exception(RecordTableEnumerator.b("ᕀ⭂⁄杆⑈⹊㥌❎㹐㝒畔㡖⭘筚㉜⽞ѠᅢѤ፦hѪͬ佮ᡰr啴᥶ᙸེ嵼ᙾﮎ뮔", a_));
		}

		// Token: 0x04002CA1 RID: 11425
		private const string ᜀ = "\u0005SummaryInformation";

		// Token: 0x04002CA2 RID: 11426
		private const string ᜁ = "\u0005DocumentSummaryInformation";

		// Token: 0x04002CA3 RID: 11427
		internal const string ᜂ = "Workbook";

		// Token: 0x04002CA4 RID: 11428
		private const string ᜃ = "Book";

		// Token: 0x04002CA5 RID: 11429
		private const string ᜄ = "_VBA_PROJECT_CUR";

		// Token: 0x04002CA6 RID: 11430
		private const string ᜅ = "VBA";

		// Token: 0x04002CA7 RID: 11431
		private const char ᜆ = '\u0002';

		// Token: 0x04002CA8 RID: 11432
		private const char ᜇ = '\u0001';

		// Token: 0x04002CA9 RID: 11433
		private const char ᜈ = '\0';

		// Token: 0x04002CAA RID: 11434
		private const char ᜉ = '\u0001';

		// Token: 0x04002CAB RID: 11435
		private const char ᜊ = '\u0002';

		// Token: 0x04002CAC RID: 11436
		private const char ᜋ = '\u0003';

		// Token: 0x04002CAD RID: 11437
		private const char ᜌ = '\u0004';

		// Token: 0x04002CAE RID: 11438
		private const char \u170D = '\u0005';

		// Token: 0x04002CAF RID: 11439
		private const char ᜎ = '\u0006';

		// Token: 0x04002CB0 RID: 11440
		private const char ᜏ = '\a';

		// Token: 0x04002CB1 RID: 11441
		private const char ᜐ = '\b';

		// Token: 0x04002CB2 RID: 11442
		private const char ᜑ = '@';

		// Token: 0x04002CB3 RID: 11443
		private const string \u1712 = "\\\\";

		// Token: 0x04002CB4 RID: 11444
		private const int \u1713 = 0;

		// Token: 0x04002CB5 RID: 11445
		internal const int \u1714 = 65535;

		// Token: 0x04002CB6 RID: 11446
		private const string \u1715 = "http:";

		// Token: 0x04002CB7 RID: 11447
		public const int DEF_FIRST_USER_COLOR = 8;

		// Token: 0x04002CB8 RID: 11448
		public const string DEF_BAD_SHEET_NAME = "#REF";

		// Token: 0x04002CB9 RID: 11449
		private const int \u1716 = 10;

		// Token: 0x04002CBA RID: 11450
		private const string \u1717 = "inline";

		// Token: 0x04002CBB RID: 11451
		private const string \u1718 = "attachment";

		// Token: 0x04002CBC RID: 11452
		private const ushort \u1719 = 65535;

		// Token: 0x04002CBD RID: 11453
		private const string \u171A = "Application/x-msexcel";

		// Token: 0x04002CBE RID: 11454
		private const string \u171B = "Application/vnd.ms-excel";

		// Token: 0x04002CBF RID: 11455
		private const string \u171C = "Application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

		// Token: 0x04002CC0 RID: 11456
		private const string \u171D = "text/csv";

		// Token: 0x04002CC1 RID: 11457
		internal const string \u171E = "VelvetSweatshop";

		// Token: 0x04002CC2 RID: 11458
		internal const char \u171F = '"';

		// Token: 0x04002CC3 RID: 11459
		private const RegexOptions ᜠ = RegexOptions.Compiled;

		// Token: 0x04002CC4 RID: 11460
		private const string ᜡ = "BookName";

		// Token: 0x04002CC5 RID: 11461
		private const string ᜢ = "SheetName";

		// Token: 0x04002CC6 RID: 11462
		internal const int ᜣ = 65534;

		// Token: 0x04002CC7 RID: 11463
		private const string ᜤ = "Format_";

		// Token: 0x04002CC8 RID: 11464
		private const int ᜥ = 77;

		// Token: 0x04002CC9 RID: 11465
		private const int ᜦ = 79;

		// Token: 0x04002CCA RID: 11466
		private const char ᜧ = ':';

		// Token: 0x04002CCB RID: 11467
		internal static readonly Color[] ᜨ;

		// Token: 0x04002CCC RID: 11468
		internal static readonly double[] ᜩ;

		// Token: 0x04002CCD RID: 11469
		internal static readonly Color[][] ᜪ;

		// Token: 0x04002CCE RID: 11470
		private static readonly TBIFFRecord[] ᜫ;

		// Token: 0x04002CCF RID: 11471
		private static readonly Regex ᜬ;

		// Token: 0x04002CD0 RID: 11472
		private static readonly string[] ᜭ;

		// Token: 0x04002CD1 RID: 11473
		private static readonly char[] ᜮ;

		// Token: 0x04002CD2 RID: 11474
		private static readonly int[] ᜯ;

		// Token: 0x04002CD3 RID: 11475
		private static readonly int[] ᜰ;

		// Token: 0x04002CD4 RID: 11476
		internal static readonly Color[] ᜱ;

		// Token: 0x04002CD5 RID: 11477
		private static readonly Color[] \u1732;

		// Token: 0x04002CD6 RID: 11478
		private List<BiffRecordRaw> \u1733;

		// Token: 0x04002CD7 RID: 11479
		private XlsWorksheetBase \u1734;

		// Token: 0x04002CD8 RID: 11480
		private XlsWorksheetsCollection \u1735;

		// Token: 0x04002CD9 RID: 11481
		private XlsStylesCollection \u1736;

		// Token: 0x04002CDA RID: 11482
		private XlsFontsCollection \u1737;

		// Token: 0x04002CDB RID: 11483
		private sprᢖ \u1738;

		// Token: 0x04002CDC RID: 11484
		private List<sprῚ> \u1739;

		// Token: 0x04002CDD RID: 11485
		private spr\u21FF \u173A;

		// Token: 0x04002CDE RID: 11486
		private Workbook \u173B;

		// Token: 0x04002CDF RID: 11487
		private List<spr\u17C1> \u173C;

		// Token: 0x04002CE0 RID: 11488
		private SSTDictionary \u173D;

		// Token: 0x04002CE1 RID: 11489
		private sprᦖ \u173E;

		// Token: 0x04002CE2 RID: 11490
		private List<sprṨ> \u173F;

		// Token: 0x04002CE3 RID: 11491
		private string ᝀ;

		// Token: 0x04002CE4 RID: 11492
		private bool ᝁ;

		// Token: 0x04002CE5 RID: 11493
		private bool ᝂ;

		// Token: 0x04002CE6 RID: 11494
		private bool ᝃ;

		// Token: 0x04002CE7 RID: 11495
		private bool ᝄ;

		// Token: 0x04002CE8 RID: 11496
		private bool ᝅ;

		// Token: 0x04002CE9 RID: 11497
		private bool ᝆ;

		// Token: 0x04002CEA RID: 11498
		private bool ᝇ;

		// Token: 0x04002CEB RID: 11499
		private bool ᝈ;

		// Token: 0x04002CEC RID: 11500
		private bool ᝉ;

		// Token: 0x04002CED RID: 11501
		private string ᝊ;

		// Token: 0x04002CEE RID: 11502
		private bool ᝋ;

		// Token: 0x04002CEF RID: 11503
		private bool ᝌ;

		// Token: 0x04002CF0 RID: 11504
		private bool ᝍ;

		// Token: 0x04002CF1 RID: 11505
		private bool ᝎ;

		// Token: 0x04002CF2 RID: 11506
		private List<Color> ᝏ;

		// Token: 0x04002CF3 RID: 11507
		internal bool ᝐ;

		// Token: 0x04002CF4 RID: 11508
		private spr\u17B5 ᝑ;

		// Token: 0x04002CF5 RID: 11509
		private sprឦ \u1752;

		// Token: 0x04002CF6 RID: 11510
		private XlsChartsCollection \u1753;

		// Token: 0x04002CF7 RID: 11511
		private XlsWorkbookObjectsCollection \u1754;

		// Token: 0x04002CF8 RID: 11512
		private spr\u24C3 \u1755;

		// Token: 0x04002CF9 RID: 11513
		private spr\u1938 \u1756;

		// Token: 0x04002CFA RID: 11514
		private spr\u237D \u1757;

		// Token: 0x04002CFB RID: 11515
		private bool \u1758;

		// Token: 0x04002CFC RID: 11516
		private XlsWorkbookShapeData \u1759;

		// Token: 0x04002CFD RID: 11517
		private int \u175A;

		// Token: 0x04002CFE RID: 11518
		private int \u175B;

		// Token: 0x04002CFF RID: 11519
		private int \u175C;

		// Token: 0x04002D00 RID: 11520
		private int \u175D;

		// Token: 0x04002D01 RID: 11521
		private List<sprỶ> \u175E;

		// Token: 0x04002D02 RID: 11522
		private spr\u2496 \u175F;

		// Token: 0x04002D03 RID: 11523
		private bool ᝠ;

		// Token: 0x04002D04 RID: 11524
		private bool ᝡ;

		// Token: 0x04002D05 RID: 11525
		private XlsExternBookCollection ᝢ;

		// Token: 0x04002D06 RID: 11526
		private XlsAddInFunctionsCollection ᝣ;

		// Token: 0x04002D07 RID: 11527
		private XlsWorkbookShapeData ᝤ;

		// Token: 0x04002D08 RID: 11528
		private sprỆ ᝥ;

		// Token: 0x04002D09 RID: 11529
		private XlsPivotCachesCollection ᝦ;

		// Token: 0x04002D0A RID: 11530
		private Graphics ᝧ;

		// Token: 0x04002D0B RID: 11531
		private FormulaUtil ᝨ;

		// Token: 0x04002D0C RID: 11532
		private spr\u233D ᝩ;

		// Token: 0x04002D0D RID: 11533
		private bool ᝪ;

		// Token: 0x04002D0E RID: 11534
		private XlsBuiltInDocumentProperties ᝫ;

		// Token: 0x04002D0F RID: 11535
		private spr\u1AA2 ᝬ;

		// Token: 0x04002D10 RID: 11536
		internal bool \u176D;

		// Token: 0x04002D11 RID: 11537
		private sprẋ ᝮ;

		// Token: 0x04002D12 RID: 11538
		private bool ᝯ;

		// Token: 0x04002D13 RID: 11539
		private int ᝰ;

		// Token: 0x04002D14 RID: 11540
		private int \u1771;

		// Token: 0x04002D15 RID: 11541
		private string \u1772;

		// Token: 0x04002D16 RID: 11542
		internal EncryptionType \u1773;

		// Token: 0x04002D17 RID: 11543
		private byte[] \u1774;

		// Token: 0x04002D18 RID: 11544
		private int \u1775;

		// Token: 0x04002D19 RID: 11545
		private int \u1776;

		// Token: 0x04002D1A RID: 11546
		private int \u1777;

		// Token: 0x04002D1B RID: 11547
		private int \u1778;

		// Token: 0x04002D1C RID: 11548
		private int \u1779;

		// Token: 0x04002D1D RID: 11549
		private ExcelVersion \u177A;

		// Token: 0x04002D1E RID: 11550
		private int \u177B;

		// Token: 0x04002D1F RID: 11551
		private sprវ \u177C;

		// Token: 0x04002D20 RID: 11552
		private double \u177D;

		// Token: 0x04002D21 RID: 11553
		private IntPtr \u177E;

		// Token: 0x04002D22 RID: 11554
		private BiffRecordRaw \u177F;

		// Token: 0x04002D23 RID: 11555
		private List<Color> ក;

		// Token: 0x04002D24 RID: 11556
		private Stream ខ;

		// Token: 0x04002D25 RID: 11557
		private int គ;

		// Token: 0x04002D26 RID: 11558
		private int ឃ;

		// Token: 0x04002D27 RID: 11559
		private Stream ង;

		// Token: 0x04002D28 RID: 11560
		private bool ច;

		// Token: 0x04002D29 RID: 11561
		private bool ឆ;

		// Token: 0x04002D2A RID: 11562
		private Dictionary<string, XlsFont> ជ;

		// Token: 0x04002D2B RID: 11563
		private Dictionary<string, XlsFont> ឈ;

		// Token: 0x04002D2C RID: 11564
		private bool ញ;

		// Token: 0x04002D2D RID: 11565
		internal bool ដ;

		// Token: 0x04002D2E RID: 11566
		private bool? ឋ;

		// Token: 0x04002D2F RID: 11567
		private spr\u23E6 ឌ;

		// Token: 0x04002D30 RID: 11568
		private bool ឍ;

		// Token: 0x04002D31 RID: 11569
		private sprᬡ ណ;

		// Token: 0x04002D32 RID: 11570
		private Stream ត;

		// Token: 0x04002D33 RID: 11571
		private bool ថ;

		// Token: 0x04002D34 RID: 11572
		private DataSorter ទ;

		// Token: 0x04002D35 RID: 11573
		private bool ធ;

		// Token: 0x04002D36 RID: 11574
		private List<Stream> ន;

		// Token: 0x04002D37 RID: 11575
		private ExcelParseOptions ប;

		// Token: 0x04002D38 RID: 11576
		private static Dictionary<ExcelSheetType, string> ផ;

		// Token: 0x04002D39 RID: 11577
		private EventHandler ព;

		// Token: 0x04002D3A RID: 11578
		private ReadOnlyFileEventHandler ភ;

		// Token: 0x0200060C RID: 1548
		public class WorkbookExcel97Serializator : IWorkbookSerializator
		{
			// Token: 0x06005D15 RID: 23829 RVA: 0x003A715C File Offset: 0x003A615C
			internal WorkbookExcel97Serializator(sprᦎ A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x06005D16 RID: 23830 RVA: 0x003A7178 File Offset: 0x003A6178
			public void Serialize(string fullName, XlsWorkbook book, ExcelSaveType saveType)
			{
				int a_ = 0;
				int num = 6;
				for (;;)
				{
					IEncryptor encryptor;
					spr\u2496 spr_u;
					RecordArrayList recordArrayList;
					switch (num)
					{
					case 0:
						encryptor = this.ᜀ(book.\u1773, book);
						num = 3;
						continue;
					case 1:
						goto IL_155;
					case 2:
						if (true)
						{
						}
						try
						{
							this.ᜀ(spr_u.ᜀ(), false, recordArrayList, encryptor, book);
							spr_u.ᜁ();
							return;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
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
									spr_u.Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_134;
								}
								if (spr_u == null)
								{
									break;
								}
								num = 0;
							}
							IL_134:;
						}
						goto IL_137;
					case 3:
						goto IL_51;
					case 4:
						if (book == null)
						{
							num = 5;
							continue;
						}
						encryptor = null;
						num = 7;
						continue;
					case 5:
						goto IL_1B2;
					case 7:
						if (book.\u1773 != EncryptionType.None)
						{
							num = 0;
							continue;
						}
						goto IL_51;
					case 8:
						if (fullName.Length == 0)
						{
							num = 1;
							continue;
						}
						num = 4;
						continue;
					case 9:
						goto IL_137;
					}
					if (fullName != null)
					{
						num = 9;
						continue;
					}
					break;
					IL_51:
					recordArrayList = new RecordArrayList();
					this.ᜀ(recordArrayList, saveType, encryptor, book, null, false);
					recordArrayList.UpdateBiffRecordsOffsets();
					spr_u = book.AppImplementation.ᜀ(fullName, STGM.STGM_READWRITE | STGM.STGM_SHARE_EXCLUSIVE | STGM.STGM_CREATE);
					num = 2;
					continue;
					IL_137:
					num = 8;
				}
				IL_B2:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倵䴷嘹倻瀽ℿ⽁⅃", a_));
				IL_155:
				goto IL_B2;
				IL_1B2:
				throw new ArgumentNullException(RecordTableEnumerator.b("吵圷唹圻", a_));
			}

			// Token: 0x06005D17 RID: 23831 RVA: 0x003A734C File Offset: 0x003A634C
			public void Serialize(Stream stream, XlsWorkbook book, ExcelSaveType saveType)
			{
				int a_ = 18;
				int num = 0;
				for (;;)
				{
					IEncryptor encryptor;
					spr\u2496 spr_u;
					RecordArrayList recordArrayList;
					switch (num)
					{
					case 1:
						goto IL_46;
					case 2:
						goto IL_44;
					case 3:
						goto IL_BD;
					case 4:
						if (book.\u1773 != EncryptionType.None)
						{
							num = 5;
							continue;
						}
						goto IL_46;
					case 5:
						encryptor = this.ᜀ(book.\u1773, book);
						num = 1;
						continue;
					case 6:
						if (book == null)
						{
							num = 3;
							continue;
						}
						goto IL_148;
					case 7:
						try
						{
							this.ᜀ(spr_u.ᜀ(), false, recordArrayList, encryptor, book);
							spr_u.ᜀ(stream);
							return;
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_145;
								case 2:
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
									spr_u.Dispose();
									num = 0;
									continue;
								}
								if (spr_u == null)
								{
									break;
								}
								num = 2;
							}
							IL_145:;
						}
						goto IL_148;
					}
					if (stream == null)
					{
						num = 2;
						continue;
					}
					num = 6;
					continue;
					IL_46:
					recordArrayList = new RecordArrayList();
					this.ᜀ(recordArrayList, saveType, encryptor, book, null, false);
					recordArrayList.UpdateBiffRecordsOffsets();
					spr_u = book.AppImplementation.ᜄ();
					if (true)
					{
					}
					num = 7;
					continue;
					IL_148:
					encryptor = null;
					num = 4;
				}
				IL_44:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋⭍ㅏ㽑", a_));
				IL_BD:
				throw new ArgumentNullException(RecordTableEnumerator.b("⩇╉⍋╍", a_));
			}

			// Token: 0x06005D18 RID: 23832 RVA: 0x003A74F0 File Offset: 0x003A64F0
			[CLSCompliant(false)]
			internal void ᜀ(RecordArrayList A_0, ExcelSaveType A_1, IEncryptor A_2, XlsWorkbook A_3, XlsWorksheet A_4, bool A_5)
			{
				int a_ = 17;
				switch (0)
				{
				default:
					for (;;)
					{
						this.ᜀ(A_3);
						this.ᜁ();
						this.ᜀ();
						A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.BOF));
						int num = 8;
						for (;;)
						{
							int num2;
							int count;
							int num4;
							int count2;
							int num9;
							int count5;
							switch (num)
							{
							case 0:
								A_0.ᜀ(A_3.\u173E);
								num = 89;
								continue;
							case 1:
								goto IL_D51;
							case 2:
								if (A_5)
								{
									num = 81;
									continue;
								}
								goto IL_B04;
							case 3:
								if (A_3.ណ == null)
								{
									num = 35;
									continue;
								}
								goto IL_804;
							case 4:
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.UnkMacrosDisable));
								num = 23;
								continue;
							case 5:
								goto IL_726;
							case 6:
								num = 45;
								continue;
							case 7:
								num = 17;
								continue;
							case 8:
								if (!A_5)
								{
									num = 53;
									continue;
								}
								goto IL_84E;
							case 9:
								goto IL_B04;
							case 10:
								if (!A_5)
								{
									num = 48;
									continue;
								}
								goto IL_EE8;
							case 11:
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Template));
								num = 20;
								continue;
							case 12:
								if (A_4 == null)
								{
									num = 9;
									continue;
								}
								goto IL_F88;
							case 13:
								if (!A_5)
								{
									num = 22;
									continue;
								}
								num = 24;
								continue;
							case 14:
								goto IL_7E0;
							case 15:
								goto IL_702;
							case 16:
							{
								sprồ sprồ = (sprồ)spr\u175E.ᜀ(TBIFFRecord.UseSelFS);
								sprồ.ᜀ(A_3.ᝅ);
								A_0.ᜀ(sprồ);
								num = 47;
								continue;
							}
							case 17:
								goto IL_C04;
							case 18:
								if (!A_5)
								{
									num = 83;
									continue;
								}
								goto IL_30F;
							case 19:
								goto IL_E9C;
							case 20:
								goto IL_E21;
							case 21:
							{
								if (num2 >= count)
								{
									num = 41;
									continue;
								}
								spr\u192F spr_u192F = A_3.\u1738.ᜁ(num2);
								spr_u192F.ᜀ(A_0);
								num2++;
								num = 62;
								continue;
							}
							case 22:
							{
								spr\u2520 spr_u = (spr\u2520)spr\u175E.ᜀ(TBIFFRecord.WindowProtect);
								spr_u.ᜀ(A_3.IsWindowProtection);
								A_0.ᜀ(spr_u);
								spr\u1AE8 spr_u1AE = (spr\u1AE8)spr\u175E.ᜀ(TBIFFRecord.Protect);
								spr_u1AE.ᜀ(A_3.IsCellProtection);
								A_0.ᜀ(spr_u1AE);
								A_0.ᜀ(A_3.Password);
								A_0.ᜀ(A_3.ProtectionRev4);
								A_0.ᜀ(A_3.PasswordRev4);
								num = 99;
								continue;
							}
							case 23:
								goto IL_414;
							case 24:
								if (A_4 != null)
								{
									num = 66;
									continue;
								}
								goto IL_702;
							case 25:
								num = 36;
								continue;
							case 26:
								goto IL_E9C;
							case 27:
								goto IL_C04;
							case 28:
							{
								int num3;
								spr\u20A4.ᜀ[] array;
								if (num3 >= array.Length)
								{
									num = 91;
									continue;
								}
								array[num3].ᜃ = A_3.ᝏ[num3 + 8].A;
								array[num3].ᜀ = A_3.ᝏ[num3 + 8].R;
								array[num3].ᜁ = A_3.ᝏ[num3 + 8].G;
								array[num3].ᜂ = A_3.ᝏ[num3 + 8].B;
								num3++;
								num = 84;
								continue;
							}
							case 29:
							{
								spr\u20A4 spr_u20A = (spr\u20A4)spr\u175E.ᜀ(TBIFFRecord.Palette);
								spr\u20A4.ᜀ[] array = new spr\u20A4.ᜀ[A_3.ᝏ.Count - 8];
								int num3 = 0;
								num = 100;
								continue;
							}
							case 30:
								if (!A_5)
								{
									num = 65;
									continue;
								}
								goto IL_38D;
							case 31:
								goto IL_BCE;
							case 32:
							{
								if (num4 >= count2)
								{
									num = 97;
									continue;
								}
								XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)A_3.\u1754[num4];
								xlsWorksheetBase.SerializeDataToList(A_0);
								num4++;
								num = 42;
								continue;
							}
							case 33:
								if (!A_5)
								{
									num = 16;
									continue;
								}
								goto IL_333;
							case 34:
							{
								int num5;
								int count3;
								if (num5 >= count3)
								{
									num = 39;
									continue;
								}
								spr᧒ spr᧒;
								spr᧒.ᜁ()[num5] = (ushort)(A_3.\u1735[num5].Index + 1);
								num5++;
								num = 14;
								continue;
							}
							case 35:
								A_3.ណ = new sprᬡ();
								num = 68;
								continue;
							case 36:
								if (A_3.ᝋ)
								{
									num = 60;
									continue;
								}
								goto IL_EE8;
							case 37:
								if (A_3.ᝐ)
								{
									num = 29;
									continue;
								}
								goto IL_D51;
							case 38:
								goto IL_5EF;
							case 39:
							{
								int num6 = A_3.\u1735.Count;
								spr᧒ spr᧒;
								int num7 = spr᧒.ᜁ().Length;
								num = 26;
								continue;
							}
							case 40:
							{
								spr᧒ spr᧒;
								A_0.ᜀ(spr᧒);
								num = 46;
								continue;
							}
							case 41:
							{
								int num8 = 0;
								int count4 = A_3.\u1736.Count;
								num = 73;
								continue;
							}
							case 42:
								goto IL_726;
							case 43:
								goto IL_AD5;
							case 44:
								if (A_3.ᝎ)
								{
									num = 4;
									continue;
								}
								goto IL_414;
							case 45:
							{
								if (A_4 == null)
								{
									num = 71;
									continue;
								}
								spr\u17C1 spr_u17C = this.ᜀ(A_4);
								spr_u17C.ᜀ(0);
								A_0.ᜀ(spr_u17C);
								num = 27;
								continue;
							}
							case 46:
								goto IL_30F;
							case 47:
								goto IL_333;
							case 48:
								num = 93;
								continue;
							case 49:
								goto IL_B51;
							case 50:
							{
								if (A_3.\u1735.Count == 0)
								{
									num = 51;
									continue;
								}
								spr᧒ spr᧒ = (spr᧒)spr\u175E.ᜀ(TBIFFRecord.TabId);
								spr᧒.ᜀ(new ushort[A_3.\u1735.Count + A_3.\u1753.Count]);
								int num5 = 0;
								int count3 = A_3.\u1735.Count;
								num = 90;
								continue;
							}
							case 51:
								goto IL_696;
							case 52:
							{
								sprᣰ sprᣰ;
								sprᣰ.ᜀ(A_3.IsDisplayPrecision ? 0 : 1);
								A_0.ᜀ(sprᣰ);
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.RefreshAll));
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.BookBool));
								num = 94;
								continue;
							}
							case 53:
								num = 61;
								continue;
							case 54:
								goto IL_84E;
							case 55:
							{
								int num8;
								int count4;
								if (num8 >= count4)
								{
									num = 92;
									continue;
								}
								XlsStyle xlsStyle = A_3.\u1736[num8] as XlsStyle;
								xlsStyle.ᜀ(A_0);
								num8++;
								num = 80;
								continue;
							}
							case 56:
								if (A_3.\u173E.ᜀ().Count != 0)
								{
									num = 0;
									continue;
								}
								goto IL_6C4;
							case 57:
								A_0.ᜀ(A_3.ᝮ);
								num = 54;
								continue;
							case 58:
								goto IL_7BC;
							case 59:
								goto IL_200;
							case 60:
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.HasBasic));
								num = 44;
								continue;
							case 61:
								if (A_1 == ExcelSaveType.SaveAsTemplate)
								{
									num = 11;
									continue;
								}
								goto IL_E21;
							case 62:
								goto IL_CB7;
							case 63:
								if (A_3.\u173E.ᜀ() != null)
								{
									num = 49;
									continue;
								}
								goto IL_6C4;
							case 64:
								A_0.ᜀ(A_3.WindowOne);
								num = 87;
								continue;
							case 65:
							{
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Backup));
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.HideObj));
								spr\u17DE spr_u17DE = (spr\u17DE)spr\u175E.ᜀ(TBIFFRecord.DateWindow1904);
								spr_u17DE.ᜀ(A_3.Date1904);
								A_0.ᜀ(spr_u17DE);
								sprᣰ sprᣰ = (sprᣰ)spr\u175E.ᜀ(TBIFFRecord.Precision);
								num = 52;
								continue;
							}
							case 66:
							{
								spr\u21CC spr_u21CC = (spr\u21CC)spr\u175E.ᜀ(TBIFFRecord.OleSize);
								spr_u21CC.ᜁ((ushort)(A_4.FirstRow - 1));
								spr_u21CC.ᜁ((byte)(A_4.FirstColumn - 1));
								spr_u21CC.ᜀ((ushort)(A_4.LastRow - 1));
								spr_u21CC.ᜀ((byte)(A_4.LastColumn - 1));
								A_0.ᜀ(spr_u21CC);
								num = 15;
								continue;
							}
							case 67:
								if (!A_5)
								{
									num = 72;
									continue;
								}
								goto IL_BCE;
							case 68:
								goto IL_804;
							case 69:
								if (A_3.ᝮ != null)
								{
									num = 57;
									continue;
								}
								goto IL_84E;
							case 70:
								A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.WriteProtection));
								num = 95;
								continue;
							case 71:
								goto IL_C95;
							case 72:
								this.ᜁ(A_0, A_3);
								num = 31;
								continue;
							case 73:
								goto IL_B84;
							case 74:
								if (A_3.\u177F != null)
								{
									num = 82;
									continue;
								}
								goto IL_AD5;
							case 75:
								goto IL_200;
							case 76:
								if (A_2 != null)
								{
									num = 86;
									continue;
								}
								goto IL_5EF;
							case 77:
								if (A_5)
								{
									num = 6;
									continue;
								}
								goto IL_C95;
							case 78:
							{
								if (!A_5)
								{
									num = 64;
									continue;
								}
								spr\u17B5 spr_u17B = (spr\u17B5)A_3.WindowOne.Clone();
								spr_u17B.ᜇ(0);
								spr_u17B.ᜆ(0);
								A_0.ᜀ(spr_u17B);
								num = 58;
								continue;
							}
							case 79:
								goto IL_CB7;
							case 80:
								goto IL_B84;
							case 81:
								num = 12;
								continue;
							case 82:
								A_0.ᜀ(A_3.\u177F);
								num = 43;
								continue;
							case 83:
								num = 50;
								continue;
							case 84:
								goto IL_F60;
							case 85:
								if (A_3.\u176D)
								{
									num = 70;
									continue;
								}
								goto IL_A89;
							case 86:
								A_0.ᜀ(A_2.GetFilePassRecord());
								num = 38;
								continue;
							case 87:
								goto IL_7BC;
							case 88:
							{
								int num6;
								int num7;
								if (num6 >= num7)
								{
									num = 40;
									continue;
								}
								spr᧒ spr᧒;
								spr᧒.ᜁ()[num6] = (ushort)(num6 + 1);
								num6++;
								num = 19;
								continue;
							}
							case 89:
								goto IL_6C4;
							case 90:
								goto IL_7E0;
							case 91:
							{
								spr\u20A4.ᜀ[] array;
								spr\u20A4 spr_u20A;
								spr_u20A.ᜀ(array);
								A_0.ᜀ(spr_u20A);
								num = 1;
								continue;
							}
							case 92:
								num = 37;
								continue;
							case 93:
								if ((A_3.AppImplementation.\u171B() & SkipExtRecordsType.Macros) != SkipExtRecordsType.Macros)
								{
									num = 25;
									continue;
								}
								goto IL_EE8;
							case 94:
								goto IL_38D;
							case 95:
								goto IL_A89;
							case 96:
							{
								if (num9 >= count5)
								{
									num = 7;
									continue;
								}
								INamedObject a_2 = A_3.\u1754[num9];
								A_0.ᜀ(this.ᜀ(a_2));
								num9++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_B51;
								default:
									if (false)
									{
									}
									num = 75;
									continue;
								}
								break;
							}
							case 97:
								return;
							case 98:
								goto IL_EE8;
							case 99:
								goto IL_702;
							case 100:
								goto IL_F60;
							}
							break;
							IL_200:
							num = 96;
							continue;
							IL_30F:
							num = 10;
							continue;
							IL_333:
							num = 77;
							continue;
							IL_38D:
							num = 3;
							continue;
							IL_414:
							spr\u2384 spr_u2 = (spr\u2384)spr\u175E.ᜀ(TBIFFRecord.CodeName);
							spr_u2.ᜀ(A_3.CodeName);
							A_0.ᜀ(spr_u2);
							num = 98;
							continue;
							IL_5EF:
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.InterfaceHdr));
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.MMS));
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.InterfaceEnd));
							spr\u1802 spr_u3 = (spr\u1802)spr\u175E.ᜀ(TBIFFRecord.WriteAccess);
							spr_u3.ᜀ(A_3.Author);
							A_0.ᜀ(spr_u3);
							num = 69;
							continue;
							IL_6C4:
							this.ᜀ(A_0, A_3);
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.UnkEnd));
							num = 67;
							continue;
							IL_702:
							num = 78;
							continue;
							IL_726:
							num = 32;
							continue;
							IL_7BC:
							num = 30;
							continue;
							IL_7E0:
							num = 34;
							continue;
							IL_804:
							A_0.ᜀ(A_3.ណ);
							A_3.\u1737.SerializeDataToList(A_0);
							A_3.\u173A.ᜀ(A_0);
							num2 = 0;
							count = A_3.\u1738.Count;
							num = 79;
							continue;
							IL_84E:
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.Codepage));
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.DSF));
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.UnkBegin));
							num = 18;
							continue;
							IL_A89:
							num = 76;
							continue;
							IL_AD5:
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.EOF));
							num = 2;
							continue;
							IL_B04:
							num4 = 0;
							count2 = A_3.\u1754.Count;
							num = 5;
							continue;
							IL_B51:
							num = 56;
							continue;
							IL_B84:
							num = 55;
							continue;
							IL_BCE:
							A_3.\u173D.SerializeDataToList(A_0);
							num = 74;
							continue;
							IL_C04:
							spr\u2338 spr_u4 = (spr\u2338)spr\u175E.ᜀ(TBIFFRecord.Country);
							spr_u4.ᜁ((ushort)A_3.ឃ);
							spr_u4.ᜀ((ushort)A_3.ឃ);
							A_0.ᜀ(spr_u4);
							A_3.ᝢ.SerializeDataToList(A_0);
							num = 63;
							continue;
							IL_C95:
							num9 = 0;
							count5 = A_3.\u1754.Count;
							num = 59;
							continue;
							IL_CB7:
							num = 21;
							continue;
							IL_D51:
							this.ᜂ(A_0, A_3);
							if (true)
							{
							}
							num = 33;
							continue;
							IL_E21:
							num = 85;
							continue;
							IL_E9C:
							num = 88;
							continue;
							IL_EE8:
							A_0.ᜀ(spr\u175E.ᜀ(TBIFFRecord.FnGroupCount));
							num = 13;
							continue;
							IL_F60:
							num = 28;
						}
					}
					IL_696:
					throw new ApplicationException(RecordTableEnumerator.b("၆♈㥊♌ⵎ㹐㱒㹔睖㑘⹚⹜⭞䅠b੤०ᵨ੪ѬŮɰ卲ᑴͶ奸᝺᡼Ṿꖄ권ﺐﺔﺚ", a_));
					IL_F88:
					A_4.ᜆ(A_0);
					return;
				}
			}

			// Token: 0x06005D19 RID: 23833 RVA: 0x003A8490 File Offset: 0x003A7490
			private void ᜂ(RecordArrayList A_0, XlsWorkbook A_1)
			{
				int a_ = 19;
				switch (0)
				{
				default:
				{
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (A_1.ᝦ == null)
							{
								if (true)
								{
								}
								num = 5;
								continue;
							}
							List<int>.Enumerator enumerator = A_1.ᝦ.Order.GetEnumerator();
							num = 3;
							continue;
						}
						case 1:
							goto IL_92;
						case 2:
							goto IL_5B;
						case 3:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 2:
									{
										spr\u257E spr_u257E;
										spr_u257E.ᜀ(A_0);
										num = 0;
										continue;
									}
									case 3:
										goto IL_16B;
									case 4:
									{
										List<int>.Enumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 5;
											continue;
										}
										int a_2 = enumerator.Current;
										XlsPivotCache xlsPivotCache = A_1.ᝦ[a_2];
										spr\u257E spr_u257E = xlsPivotCache.Info;
										num = 6;
										continue;
									}
									case 5:
										num = 3;
										continue;
									case 6:
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
											spr\u257E spr_u257E;
											if (spr_u257E != null)
											{
												num = 2;
												continue;
											}
											break;
										}
										}
										break;
									}
									IL_12A:
									num = 4;
									continue;
									IL_D9:
									goto IL_12A;
									goto IL_D9;
								}
								IL_16B:
								return;
							}
							finally
							{
								List<int>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							goto IL_17B;
						case 4:
							if (A_1 == null)
							{
								num = 1;
								continue;
							}
							goto IL_17B;
						case 5:
							return;
						}
						if (A_0 == null)
						{
							num = 2;
							continue;
						}
						num = 4;
						continue;
						IL_17B:
						num = 0;
					}
					IL_5B:
					throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊⹌⁎⍐㝒♔", a_));
					IL_92:
					throw new ArgumentNullException(RecordTableEnumerator.b("⭈⑊≌⑎", a_));
				}
				}
			}

			// Token: 0x06005D1A RID: 23834 RVA: 0x003A866C File Offset: 0x003A766C
			private void ᜁ(RecordArrayList A_0, XlsWorkbook A_1)
			{
				int a_ = 5;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1.\u1759 != null)
						{
							num = 8;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						if ((A_1.AppImplementation.\u171B() & SkipExtRecordsType.Drawings) != SkipExtRecordsType.Drawings)
						{
							num = 5;
							continue;
						}
						return;
					case 4:
						goto IL_90;
					case 5:
						A_1.\u1759.ᜀ(A_0, TBIFFRecord.MSODrawingGroup, this.ᜁ);
						num = 1;
						continue;
					case 6:
						goto IL_4F;
					case 7:
						if (A_1.ᝤ != null)
						{
							if (true)
							{
							}
							num = 9;
							continue;
						}
						goto IL_11E;
					case 8:
						num = 2;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_90;
						default:
							if (false)
							{
							}
							A_1.ᝤ.ᜀ(A_0, TBIFFRecord.HeaderFooterImage, null);
							num = 4;
							continue;
						}
						break;
					}
					if (A_0 == null)
					{
						num = 6;
						continue;
					}
					num = 7;
					continue;
					IL_11E:
					num = 0;
					continue;
					IL_90:
					goto IL_11E;
				}
				IL_4F:
				throw new ArgumentNullException(RecordTableEnumerator.b("䤺堼尾⹀ㅂ⅄㑆", a_));
			}

			// Token: 0x06005D1B RID: 23835 RVA: 0x003A87BC File Offset: 0x003A77BC
			private IEncryptor ᜀ(EncryptionType A_0, XlsWorkbook A_1)
			{
				int a_ = 1;
				string text;
				IEncryptor encryptor;
				for (;;)
				{
					IL_09:
					int num = 12;
					for (;;)
					{
						string text2;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							}
							if (false)
							{
							}
							num = 6;
							continue;
						case 1:
							goto IL_7B;
						case 2:
							if (A_1.\u1774 == null)
							{
								num = 5;
								continue;
							}
							goto IL_16F;
						case 3:
							if (text.Length > 15)
							{
								num = 1;
								continue;
							}
							goto IL_11F;
						case 4:
							num = 3;
							continue;
						case 5:
							A_1.\u1774 = Guid.NewGuid().ToByteArray();
							num = 10;
							continue;
						case 6:
							text2 = RecordTableEnumerator.b("愶尸场䬼娾㕀၂㉄≆⡈㽊㹌❎㹐⍒", a_);
							goto IL_100;
						case 7:
							if (text != null)
							{
								num = 4;
								continue;
							}
							goto IL_11F;
						case 8:
							if (true)
							{
							}
							text2 = A_1.\u1772;
							goto IL_100;
						case 9:
							encryptor = new spr\u22F6();
							num = 2;
							continue;
						case 10:
							goto IL_16F;
						case 11:
							if (A_1.\u1772 == null)
							{
								num = 0;
								continue;
							}
							num = 8;
							continue;
						}
						if (A_0 == EncryptionType.Standard)
						{
							num = 9;
							continue;
						}
						goto IL_192;
						IL_100:
						text = text2;
						num = 7;
						continue;
						IL_16F:
						num = 11;
					}
				}
				IL_7B:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("朶堸䠺丼䠾⹀ㅂ⅄ፆ♈ъ㵌⩎㽐", a_), RecordTableEnumerator.b("朶堸䠺丼䠾⹀ㅂ⅄杆㵈⑊≌潎㵐㱒㭔ざ睘筚ၜ㹞ᥠ੢ࡤቦѨ䭪ᵬ๮ɰrɴᡶ୸ὺ嵼፾ꮊﲎ놐ꊒꂔ랖滛ﲜ삠삢톤슦\udba8\ud8aa莬", a_));
				IL_11F:
				encryptor.SetEncryptionInfo(A_1.\u1774, text);
				return encryptor;
				IL_192:
				throw new NotSupportedException(RecordTableEnumerator.b("礶嘸伺ᴼ䰾㑀㍂㕄⡆㭈㽊⡌⭎煐㙒㭔㑖⭘≚ⵜ⭞ࡠౢ୤䝦ᵨቪᵬ੮彰", a_));
			}

			// Token: 0x06005D1C RID: 23836 RVA: 0x003A8970 File Offset: 0x003A7970
			private void ᜁ()
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

			// Token: 0x06005D1D RID: 23837 RVA: 0x003A89AC File Offset: 0x003A79AC
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
			}

			// Token: 0x06005D1E RID: 23838 RVA: 0x003A89E8 File Offset: 0x003A79E8
			private void ᜀ(XlsWorkbook A_0)
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

			// Token: 0x06005D1F RID: 23839 RVA: 0x003A8A24 File Offset: 0x003A7A24
			private spr\u17C1 ᜀ(INamedObject A_0)
			{
				int a_ = 1;
				spr\u17C1 spr_u17C;
				for (;;)
				{
					for (;;)
					{
						spr_u17C = (spr\u17C1)spr\u175E.ᜀ(TBIFFRecord.BoundSheet);
						spr_u17C.ᜀ(A_0.Name);
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (A_0 is IWorksheet)
								{
									num = 1;
									continue;
								}
								num = 4;
								continue;
							case 1:
								if (true)
								{
								}
								this.ᜀ(spr_u17C, (XlsWorksheet)A_0);
								num = 2;
								continue;
							case 2:
								goto IL_DF;
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
									this.ᜀ(spr_u17C, (XlsChart)A_0);
									num = 5;
									continue;
								}
								break;
							case 4:
								if (A_0 is IChart)
								{
									num = 3;
									continue;
								}
								goto IL_E1;
							case 5:
								goto IL_9D;
							}
							break;
						}
					}
				}
				IL_9D:
				IL_DF:
				return spr_u17C;
				IL_E1:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夶堸嘺堼嬾เ⅂⽄≆⩈㽊浌❎ぐ⁒畔⁖⭘㑚㍜㡞䅠ᝢᱤᝦ౨", a_));
			}

			// Token: 0x06005D20 RID: 23840 RVA: 0x003A8B28 File Offset: 0x003A7B28
			private void ᜀ(spr\u20C3 A_0, bool A_1, RecordArrayList A_2, IEncryptor A_3, XlsWorkbook A_4)
			{
				int a_ = 11;
				switch (0)
				{
				default:
				{
					int num = 10;
					for (;;)
					{
						int num2;
						int count;
						switch (num)
						{
						case 0:
							goto IL_338;
						case 1:
							goto IL_314;
						case 2:
							if (A_4.CalculationOptions.CalculationMode == ExcelCalculationMode.Automatic)
							{
								goto IL_338;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_338;
							default:
								if (false)
								{
								}
								num = 8;
								continue;
							}
							break;
						case 3:
							goto IL_6E;
						case 4:
							goto IL_314;
						case 5:
						{
							if (num2 >= count)
							{
								num = 3;
								continue;
							}
							XlsWorksheet xlsWorksheet = A_4.Worksheets[num2] as XlsWorksheet;
							xlsWorksheet.CellRecords.Table.ᜃ();
							num2++;
							num = 1;
							continue;
						}
						case 6:
							if (A_4.CalculationOptions.CalculationMode == ExcelCalculationMode.AutomaticExceptTables)
							{
								num = 0;
								continue;
							}
							goto IL_6E;
						case 7:
							goto IL_69;
						case 8:
							num = 6;
							continue;
						case 9:
							goto IL_79;
						case 10:
							if (true)
							{
							}
							break;
						}
						if (A_0 == null)
						{
							num = 7;
							continue;
						}
						num = 2;
						continue;
						IL_6E:
						num = 9;
						continue;
						IL_314:
						num = 5;
						continue;
						IL_338:
						num2 = 0;
						count = A_4.Worksheets.Count;
						num = 4;
					}
					IL_69:
					goto IL_300;
					IL_79:
					try
					{
						try
						{
							for (;;)
							{
								spr\u1FDC a_2 = A_0.ᜀ(RecordTableEnumerator.b("ᙀⱂ㝄ⱆ⭈⑊≌⑎", a_));
								sprᡄ sprᡄ = new sprᡄ(a_2, true);
								num = 2;
								for (;;)
								{
									spr\u24E8 spr_u24E;
									bool flag;
									switch (num)
									{
									case 0:
										if (spr_u24E != null)
										{
											num = 1;
											continue;
										}
										goto IL_172;
									case 1:
										goto IL_219;
									case 2:
										try
										{
											sprᡄ.ᜀ(A_2, A_3);
											goto IL_190;
										}
										finally
										{
											num = 2;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_216;
												case 1:
													((IDisposable)sprᡄ).Dispose();
													num = 0;
													continue;
												}
												if (sprᡄ == null)
												{
													break;
												}
												num = 1;
											}
											IL_216:;
										}
										goto IL_219;
										IL_190:
										this.ᜀ(A_0, A_4, A_3);
										this.ᜀ(A_0, A_4);
										flag = false;
										spr_u24E = (A_0 as spr\u24E8);
										num = 0;
										continue;
									case 3:
										if (A_4.ខ != null)
										{
											num = 8;
											continue;
										}
										goto IL_172;
									case 4:
										if (!flag)
										{
											num = 9;
											continue;
										}
										goto IL_2A6;
									case 5:
										goto IL_172;
									case 6:
										goto IL_2B2;
									case 7:
										goto IL_2A6;
									case 8:
									{
										spr\u1FDC spr_u1FDC = A_0.ᜀ(RecordTableEnumerator.b("ɀ㝂⥄㑆", a_));
										A_4.ខ.Position = 0L;
										spr_u1FDC.Position = 0L;
										UtilityMethods.ᜀ(A_4.ខ, spr_u1FDC);
										spr_u1FDC.Flush();
										spr_u1FDC.Close();
										num = 5;
										continue;
									}
									case 9:
										this.ᜀ(A_4, A_0);
										num = 7;
										continue;
									}
									break;
									IL_172:
									num = 4;
									continue;
									IL_219:
									this.ᜀ(A_4, spr_u24E);
									flag = true;
									num = 3;
									continue;
									IL_2A6:
									num = 6;
								}
							}
							IL_2B2:;
						}
						catch (Exception)
						{
							throw;
						}
						return;
					}
					finally
					{
						for (;;)
						{
							A_0.ᜀ();
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (A_1)
									{
										num = 2;
										continue;
									}
									goto IL_2FF;
								case 1:
									goto IL_2FD;
								case 2:
									A_0.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_2FD:
						IL_2FF:;
					}
					IL_300:
					throw new ArgumentNullException(RecordTableEnumerator.b("㉀㝂⩄㕆⡈ⱊ⡌", a_));
				}
				}
			}

			// Token: 0x06005D21 RID: 23841 RVA: 0x003A8F0C File Offset: 0x003A7F0C
			private void ᜀ(XlsWorkbook A_0, spr\u24E8 A_1)
			{
				int a_ = 12;
				int num = 4;
				int num2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A2;
					case 1:
						goto IL_FA;
					case 2:
					{
						if (true)
						{
						}
						sprᮓ sprᮓ = null;
						new Guid(RecordTableEnumerator.b("牁瑃癅硇穉絋絍ㅏ网摓晕桗橙煛湝偟剡呣䭥୧婩屫幭嵯䉱䑳䙵䡷䩹䱻乽끿늁뒃늅뺇", a_));
						num2 = spr\u2019.ᜁ(A_1.ᜇ(), 0U, out sprᮓ);
						num = 3;
						continue;
					}
					case 3:
						if (num2 != 0)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A2;
						default:
						{
							if (false)
							{
							}
							sprᮓ sprᮓ;
							A_0.ᝫ.ᜀ(sprᮓ);
							A_0.ᝬ.ᜀ(sprᮓ);
							Marshal.FinalReleaseComObject(sprᮓ);
							num = 0;
							continue;
						}
						}
						break;
					}
					if ((A_0.AppImplementation.\u171B() & SkipExtRecordsType.SummaryInfo) == SkipExtRecordsType.SummaryInfo)
					{
						break;
					}
					num = 2;
				}
				IL_A2:
				return;
				IL_FA:
				throw new ExternalException(RecordTableEnumerator.b("Ł╃⡅♇╉㡋湍㍏⁑ㅓ㝕ⱗ㽙籛൝ᑟൡᙣݥཧཀྵ䱫ṭɯᵱѳ፵੷๹ᕻ᭽ꊁ慎", a_), num2);
			}

			// Token: 0x06005D22 RID: 23842 RVA: 0x003A9018 File Offset: 0x003A8018
			private void ᜀ(XlsWorkbook A_0, spr\u20C3 A_1)
			{
				int a_ = 15;
				sprណ sprណ2;
				Stream stream;
				for (;;)
				{
					IL_09:
					switch (0)
					{
					default:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
							{
								if (true)
								{
								}
								if (false)
								{
								}
								sprណ sprណ = new sprណ();
								spr\u22A9 spr_u22A = new spr\u22A9(XlsBuiltInDocumentProperties.ᜁ, -1);
								sprណ.ᜀ().Add(spr_u22A);
								sprណ2 = new sprណ();
								spr\u22A9 spr_u22A2 = new spr\u22A9(XlsBuiltInDocumentProperties.ᜂ, -1);
								spr\u22A9 spr_u22A3 = new spr\u22A9(spr\u1AA2.ᜁ, -1);
								sprណ2.ᜀ().Add(spr_u22A2);
								sprណ2.ᜀ().Add(spr_u22A3);
								A_0.ᝫ.ᜀ(spr_u22A, spr_u22A2);
								A_0.ᝬ.ᜀ(spr_u22A3);
								int num = 0;
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (A_1.ᜃ(RecordTableEnumerator.b("䁄ᑆ㱈♊⁌⹎⍐⩒᱔㥖㽘㑚⽜㉞`ᝢ౤ࡦݨ", a_)))
										{
											num = 3;
											continue;
										}
										goto IL_12E;
									case 1:
										goto IL_129;
									case 2:
										goto IL_12E;
									case 3:
										A_1.ᜂ(RecordTableEnumerator.b("䁄ᑆ㱈♊⁌⹎⍐⩒᱔㥖㽘㑚⽜㉞`ᝢ౤ࡦݨ", a_));
										num = 2;
										continue;
									case 4:
										if (A_1.ᜃ(RecordTableEnumerator.b("䁄͆♈⡊㡌≎㑐㵒⅔іⱘ㙚ぜ㹞፠ᩢⱤ०ཨѪὬɮၰݲᱴᡶ᝸", a_)))
										{
											num = 5;
											continue;
										}
										goto IL_1B4;
									case 5:
										A_1.ᜂ(RecordTableEnumerator.b("䁄͆♈⡊㡌≎㑐㵒⅔іⱘ㙚ぜ㹞፠ᩢⱤ०ཨѪὬɮၰݲᱴᡶ᝸", a_));
										num = 1;
										continue;
									}
									break;
									IL_12E:
									stream = A_1.ᜀ(RecordTableEnumerator.b("䁄ᑆ㱈♊⁌⹎⍐⩒᱔㥖㽘㑚⽜㉞`ᝢ౤ࡦݨ", a_));
									stream.Position = 0L;
									sprណ.ᜄ(stream);
									stream.Close();
									num = 4;
								}
								break;
							}
							}
						}
						break;
					}
				}
				IL_129:
				IL_1B4:
				stream = A_1.ᜀ(RecordTableEnumerator.b("䁄͆♈⡊㡌≎㑐㵒⅔іⱘ㙚ぜ㹞፠ᩢⱤ०ཨѪὬɮၰݲᱴᡶ᝸", a_));
				stream.Position = 0L;
				sprណ2.ᜄ(stream);
				stream.Close();
			}

			// Token: 0x06005D23 RID: 23843 RVA: 0x003A9208 File Offset: 0x003A8208
			private void ᜁ(spr\u20C3 A_0, XlsWorkbook A_1)
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

			// Token: 0x06005D24 RID: 23844 RVA: 0x003A9244 File Offset: 0x003A8244
			private void ᜀ(spr\u20C3 A_0, XlsWorkbook A_1, IEncryptor A_2)
			{
				int a_ = 11;
				switch (0)
				{
				default:
					for (;;)
					{
						XlsPivotCachesCollection xlsPivotCachesCollection = A_1.PivotCaches;
						int num = 0;
						for (;;)
						{
							spr\u20C3 spr_u20C;
							switch (num)
							{
							case 0:
								if (xlsPivotCachesCollection != null)
								{
									num = 1;
									continue;
								}
								return;
							case 1:
								if (true)
								{
								}
								num = 4;
								continue;
							case 2:
								try
								{
									IEnumerator<XlsPivotCache> enumerator = xlsPivotCachesCollection.GetEnumerator();
									try
									{
										num = 3;
										for (;;)
										{
											XlsPivotCache xlsPivotCache;
											spr\u1FDC spr_u1FDC;
											switch (num)
											{
											case 0:
												num = 1;
												continue;
											case 1:
												goto IL_153;
											case 2:
												try
												{
													xlsPivotCache.ᜀ(spr_u1FDC, A_2);
													break;
												}
												finally
												{
													num = 0;
													for (;;)
													{
														switch (num)
														{
														case 1:
															((IDisposable)spr_u1FDC).Dispose();
															num = 2;
															continue;
														case 2:
															goto IL_104;
														}
														if (spr_u1FDC == null)
														{
															break;
														}
														num = 1;
													}
													IL_104:;
												}
												goto IL_107;
											case 4:
												if (!enumerator.MoveNext())
												{
													num = 0;
													continue;
												}
												goto IL_107;
											}
											IL_9C:
											num = 4;
											continue;
											goto IL_9C;
											IL_107:
											xlsPivotCache = enumerator.Current;
											string a_2 = xlsPivotCache.StreamId.ToString(RecordTableEnumerator.b("᥀睂", a_));
											spr_u1FDC = spr_u20C.ᜀ(a_2);
											num = 2;
										}
										IL_153:;
									}
									finally
									{
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												enumerator.Dispose();
												num = 2;
												continue;
											case 2:
												switch ((1 == 1) ? 1 : 0)
												{
												case 0:
												case 2:
													goto IL_177;
												default:
													goto IL_1A8;
												}
												break;
											}
											goto IL_173;
											IL_177:
											num = 0;
											continue;
											IL_173:
											if (enumerator != null)
											{
												goto IL_177;
											}
											goto IL_1B0;
										}
										IL_1A8:
										if (false)
										{
										}
										IL_1B0:;
									}
									return;
								}
								finally
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_1F1;
										case 1:
											spr_u20C.Dispose();
											num = 0;
											continue;
										}
										if (spr_u20C == null)
										{
											break;
										}
										num = 1;
									}
									IL_1F1:;
								}
								goto IL_1F4;
							case 3:
								goto IL_1F4;
							case 4:
								if (xlsPivotCachesCollection.Count > 0)
								{
									num = 3;
									continue;
								}
								return;
							}
							break;
							IL_1F4:
							spr_u20C = A_0.ᜄ(RecordTableEnumerator.b("Ṁ၂ᵄᡆൈॊቌ౎ѐŒ", a_));
							num = 2;
						}
					}
					return;
				}
			}

			// Token: 0x06005D25 RID: 23845 RVA: 0x003A94DC File Offset: 0x003A84DC
			private void ᜀ(spr\u20C3 A_0, XlsWorkbook A_1)
			{
				int a_ = 5;
				for (;;)
				{
					IL_09:
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_F4;
						case 1:
							goto IL_6E;
						case 2:
							if (A_1.\u175F != null)
							{
								num = 3;
								continue;
							}
							return;
						case 3:
						{
							spr\u20C3 a_2 = A_1.\u175F.ᜀ();
							this.ᜁ(A_0, a_2);
							this.ᜀ(A_0, a_2);
							num = 6;
							continue;
						}
						case 4:
							if (A_1 == null)
							{
								num = 0;
								continue;
							}
							num = 2;
							continue;
						case 5:
							if (true)
							{
							}
							break;
						case 6:
							goto IL_D9;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
							if (false)
							{
							}
							if (A_0 == null)
							{
								num = 1;
							}
							else
							{
								num = 4;
							}
							break;
						}
					}
				}
				IL_6E:
				throw new ArgumentNullException(RecordTableEnumerator.b("䠺䤼倾㍀≂≄≆", a_));
				IL_D9:
				return;
				IL_F4:
				throw new ArgumentNullException(RecordTableEnumerator.b("夺刼倾⩀", a_));
			}

			// Token: 0x06005D26 RID: 23846 RVA: 0x003A95E0 File Offset: 0x003A85E0
			private void ᜁ(spr\u20C3 A_0, spr\u20C3 A_1)
			{
				int a_ = 13;
				switch (0)
				{
				default:
				{
					int num = 6;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
						{
							string[] array;
							if (array[num2] != RecordTableEnumerator.b("᱂ᙄ὆ᙈཊཌ၎ቐْݔ", a_))
							{
								num = 5;
								continue;
							}
							goto IL_95;
						}
						case 1:
							goto IL_61;
						case 2:
							if (true)
							{
							}
							goto IL_11B;
						case 3:
							return;
						case 4:
							goto IL_11B;
						case 5:
						{
							string[] array;
							spr\u20C3 spr_u20C = A_1.ᜅ(array[num2]);
							num = 9;
							continue;
						}
						case 7:
							goto IL_90;
						case 8:
						{
							if (A_1 == null)
							{
								num = 7;
								continue;
							}
							string[] array = A_1.ᜂ();
							num2 = 0;
							int num3 = array.Length;
							num = 2;
							continue;
						}
						case 9:
							try
							{
								spr\u20C3 spr_u20C;
								A_0.ᜀ(spr_u20C);
								goto IL_95;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									spr\u20C3 spr_u20C;
									switch (num)
									{
									case 0:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_E2;
										default:
											goto IL_112;
										}
										break;
									case 1:
										spr_u20C.Dispose();
										num = 0;
										continue;
									}
									goto IL_DF;
									IL_E2:
									num = 1;
									continue;
									IL_DF:
									if (spr_u20C != null)
									{
										goto IL_E2;
									}
									goto IL_11A;
								}
								IL_112:
								if (false)
								{
								}
								IL_11A:;
							}
							goto IL_11B;
						case 10:
						{
							int num3;
							if (num2 >= num3)
							{
								num = 3;
								continue;
							}
							num = 0;
							continue;
						}
						}
						if (A_0 == null)
						{
							num = 1;
							continue;
						}
						num = 8;
						continue;
						IL_95:
						num2++;
						num = 4;
						continue;
						IL_11B:
						num = 10;
					}
					IL_61:
					throw new ArgumentNullException(RecordTableEnumerator.b("あㅄ⡆㭈⩊⩌⩎", a_));
					IL_90:
					throw new ArgumentNullException(RecordTableEnumerator.b("あ⩄㉆㭈⡊⡌ᱎ═㱒❔㙖㹘㹚", a_));
				}
				}
			}

			// Token: 0x06005D27 RID: 23847 RVA: 0x003A97B0 File Offset: 0x003A87B0
			private void ᜀ(spr\u20C3 A_0, spr\u20C3 A_1)
			{
				int a_ = 3;
				switch (0)
				{
				default:
				{
					int num = 0;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 1:
							goto IL_132;
						case 2:
						{
							if (true)
							{
							}
							if (A_1 == null)
							{
								num = 9;
								continue;
							}
							string[] array = A_1.ᜁ();
							num2 = 0;
							int num3 = array.Length;
							num = 1;
							continue;
						}
						case 3:
							goto IL_6B;
						case 4:
						{
							string text;
							if (XlsWorkbook.ᜀ(A_0, text) == null)
							{
								num = 7;
								continue;
							}
							goto IL_A6;
						}
						case 5:
							goto IL_132;
						case 6:
						{
							int num3;
							if (num2 >= num3)
							{
								num = 10;
								continue;
							}
							string[] array;
							string text = array[num2];
							num = 4;
							continue;
						}
						case 7:
						{
							string text;
							spr\u1FDC spr_u1FDC = A_1.ᜁ(text);
							num = 8;
							continue;
						}
						case 8:
							try
							{
								spr\u1FDC spr_u1FDC;
								A_0.ᜀ(spr_u1FDC);
								goto IL_A6;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									spr\u1FDC spr_u1FDC;
									switch (num)
									{
									case 0:
										((IDisposable)spr_u1FDC).Dispose();
										num = 1;
										continue;
									case 1:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_F8;
										default:
											goto IL_129;
										}
										break;
									}
									goto IL_F4;
									IL_F8:
									num = 0;
									continue;
									IL_F4:
									if (spr_u1FDC != null)
									{
										goto IL_F8;
									}
									goto IL_131;
								}
								IL_129:
								if (false)
								{
								}
								IL_131:;
							}
							goto IL_132;
						case 9:
							goto IL_A1;
						case 10:
							return;
						}
						if (A_0 == null)
						{
							num = 3;
							continue;
						}
						num = 2;
						continue;
						IL_A6:
						num2++;
						num = 5;
						continue;
						IL_132:
						num = 6;
					}
					IL_6B:
					throw new ArgumentNullException(RecordTableEnumerator.b("䨸伺刼䴾⁀⑂⁄", a_));
					IL_A1:
					throw new ArgumentNullException(RecordTableEnumerator.b("䨸吺䠼䴾≀♂ᙄ㍆♈㥊ⱌ⡎㑐", a_));
				}
				}
			}

			// Token: 0x06005D28 RID: 23848 RVA: 0x003A997C File Offset: 0x003A897C
			private void ᜀ(RecordArrayList A_0, XlsWorkbook A_1)
			{
				int a_ = 9;
				if (A_0 == null)
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
						throw new ArgumentNullException(RecordTableEnumerator.b("䴾⑀⁂⩄㕆ⵈ㡊", a_));
					}
				}
				if (true)
				{
				}
				A_1.\u1752.ᜀ(A_0);
			}

			// Token: 0x06005D29 RID: 23849 RVA: 0x003A99E8 File Offset: 0x003A89E8
			private void ᜀ(spr\u17C1 A_0, XlsWorksheet A_1)
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
				A_0.ᜀ(A_1.RealIndex);
				A_0.ᜀ(A_1.Visibility);
				A_0.ᜀ((spr\u17C1.SheetType)A_1.Type);
				A_0.ᜀ(A_1.BOF);
			}

			// Token: 0x06005D2A RID: 23850 RVA: 0x003A9A54 File Offset: 0x003A8A54
			private void ᜀ(spr\u17C1 A_0, XlsChart A_1)
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
				A_0.ᜀ(A_1.RealIndex);
				A_0.ᜀ(A_1.Visibility);
				A_0.ᜀ(spr\u17C1.SheetType.Chart);
				A_0.ᜀ(A_1.BOF);
			}

			// Token: 0x04002D3B RID: 11579
			private byte \u2609\u0098\u0083\u0081;

			// Token: 0x04002D3C RID: 11580
			private const int ᜀ = 15;

			// Token: 0x04002D3D RID: 11581
			private long \u2460\u008C\u0080\u0085;

			// Token: 0x04002D3E RID: 11582
			private byte \u25D9\u0082\u00AF\u00A9;

			// Token: 0x04002D3F RID: 11583
			private float \u25D9\u0084ª\u00A3;

			// Token: 0x04002D40 RID: 11584
			private string[] \u2609\u00AB\u0097\u00AE;

			// Token: 0x04002D41 RID: 11585
			private sprᦎ ᜁ;
		}

		// Token: 0x0200060D RID: 1549
		// (Invoke) Token: 0x06005D2C RID: 23852
		internal delegate ShapeCollectionBase ᜁ(ITabSheet A_0);

		// Token: 0x0200060E RID: 1550
		// (Invoke) Token: 0x06005D30 RID: 23856
		public delegate void DigitSizeCallback(RectangleF rect, ref double currentMax);

		// Token: 0x0200060F RID: 1551
		[CompilerGenerated]
		private sealed class ᜀ : IEnumerable<ShapeCollectionBase>, IEnumerator<ShapeCollectionBase>
		{
			// Token: 0x06005D33 RID: 23859 RVA: 0x003A9ABC File Offset: 0x003A8ABC
			[DebuggerHidden]
			IEnumerator<ShapeCollectionBase> IEnumerable<ShapeCollectionBase>.ᜂ()
			{
				int num = 5;
				XlsWorkbook.ᜀ ᜀ;
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
						switch (num)
						{
						case 0:
							num = 2;
							continue;
						case 1:
							goto IL_99;
						case 2:
							if (this.ᜁ == -2)
							{
								goto IL_B0;
							}
							goto IL_68;
						case 3:
							goto IL_83;
						case 4:
							this.ᜁ = 0;
							ᜀ = this;
							num = 1;
							continue;
						}
						if (true)
						{
						}
						if (Thread.CurrentThread.ManagedThreadId == this.ᜂ)
						{
							num = 0;
							continue;
						}
						IL_68:
						ᜀ = new XlsWorkbook.ᜀ(0);
						ᜀ.ᜃ = this.ᜃ;
						num = 3;
						continue;
					}
					IL_B0:
					num = 4;
				}
				IL_83:
				IL_99:
				ᜀ.ᜄ = this.ᜅ;
				return ᜀ;
			}

			// Token: 0x06005D34 RID: 23860 RVA: 0x003A9BA0 File Offset: 0x003A8BA0
			[DebuggerHidden]
			IEnumerator IEnumerable.ᜄ()
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
				return this.ᜂ();
			}

			// Token: 0x06005D35 RID: 23861 RVA: 0x003A9BE4 File Offset: 0x003A8BE4
			bool IEnumerator.ᜆ()
			{
				for (;;)
				{
					for (;;)
					{
						int num = this.ᜁ;
						int num2 = 13;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								num2 = 10;
								continue;
							case 1:
								goto IL_18A;
							case 2:
								this.ᜆ++;
								num2 = 1;
								continue;
							case 3:
								this.ᜌ = this.ᜄ(this.ᜈ);
								num2 = 7;
								continue;
							case 4:
								if (this.ᜊ >= this.ᜋ)
								{
									num2 = 2;
									continue;
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
									this.ᜈ = (this.ᜉ[this.ᜊ] as ITabSheet);
									num2 = 14;
									continue;
								}
								break;
							case 5:
								goto IL_363;
							case 6:
								goto IL_110;
							case 7:
								if (this.ᜌ != null)
								{
									num2 = 0;
									continue;
								}
								goto IL_29A;
							case 8:
								goto IL_115;
							case 9:
								num2 = 12;
								continue;
							case 10:
								if (this.ᜌ.Count > 0)
								{
									num2 = 5;
									continue;
								}
								goto IL_29A;
							case 11:
								goto IL_18A;
							case 12:
								goto IL_2DF;
							case 13:
								switch (num)
								{
								case 0:
									this.ᜁ = -1;
									this.ᜆ = 0;
									this.ᜇ = this.ᜃ.\u1754.Count;
									num2 = 11;
									continue;
								case 1:
									this.ᜁ = -1;
									num2 = 15;
									continue;
								case 2:
									this.ᜁ = -1;
									num2 = 19;
									continue;
								default:
									num2 = 9;
									continue;
								}
								break;
							case 14:
								if (this.ᜈ != null)
								{
									num2 = 3;
									continue;
								}
								goto IL_29A;
							case 15:
								if (true)
								{
								}
								goto IL_2F9;
							case 16:
								goto IL_1B1;
							case 17:
								if (this.ᜉ.Count > 0)
								{
									num2 = 6;
									continue;
								}
								goto IL_2F9;
							case 18:
								if (this.ᜉ != null)
								{
									num2 = 20;
									continue;
								}
								goto IL_2F9;
							case 19:
								goto IL_29A;
							case 20:
								num2 = 17;
								continue;
							case 21:
								if (this.ᜆ >= this.ᜇ)
								{
									num2 = 16;
									continue;
								}
								this.ᜈ = (this.ᜃ.\u1754[this.ᜆ] as ITabSheet);
								this.ᜉ = this.ᜄ(this.ᜈ);
								num2 = 18;
								continue;
							case 22:
								goto IL_115;
							}
							break;
							IL_115:
							num2 = 4;
							continue;
							IL_18A:
							num2 = 21;
							continue;
							IL_29A:
							this.ᜊ++;
							num2 = 22;
							continue;
							IL_2F9:
							this.ᜉ = ((this.ᜈ as XlsWorksheetBase).Shapes as ShapeCollectionBase);
							this.ᜊ = 0;
							this.ᜋ = this.ᜉ.Count;
							num2 = 8;
						}
					}
				}
				IL_110:
				this.ᜀ = this.ᜉ;
				this.ᜁ = 1;
				return true;
				IL_1B1:
				IL_2DF:
				return false;
				IL_363:
				this.ᜀ = this.ᜌ;
				this.ᜁ = 2;
				return true;
			}

			// Token: 0x06005D36 RID: 23862 RVA: 0x003A9F5C File Offset: 0x003A8F5C
			[DebuggerHidden]
			ShapeCollectionBase IEnumerator<ShapeCollectionBase>.ᜃ()
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

			// Token: 0x06005D37 RID: 23863 RVA: 0x003A9FA0 File Offset: 0x003A8FA0
			[DebuggerHidden]
			void IEnumerator.ᜁ()
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

			// Token: 0x06005D38 RID: 23864 RVA: 0x003A9FE0 File Offset: 0x003A8FE0
			void IDisposable.ᜀ()
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

			// Token: 0x06005D39 RID: 23865 RVA: 0x003AA01C File Offset: 0x003A901C
			[DebuggerHidden]
			object IEnumerator.ᜅ()
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

			// Token: 0x06005D3A RID: 23866 RVA: 0x003AA060 File Offset: 0x003A9060
			[DebuggerHidden]
			public ᜀ(int A_0)
			{
				this.ᜁ = A_0;
				this.ᜂ = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x04002D42 RID: 11586
			private ShapeCollectionBase ᜀ;

			// Token: 0x04002D43 RID: 11587
			private int ᜁ;

			// Token: 0x04002D44 RID: 11588
			private int ᜂ;

			// Token: 0x04002D45 RID: 11589
			public XlsWorkbook ᜃ;

			// Token: 0x04002D46 RID: 11590
			public XlsWorkbook.ᜁ ᜄ;

			// Token: 0x04002D47 RID: 11591
			public XlsWorkbook.ᜁ ᜅ;

			// Token: 0x04002D48 RID: 11592
			public int ᜆ;

			// Token: 0x04002D49 RID: 11593
			public int ᜇ;

			// Token: 0x04002D4A RID: 11594
			public ITabSheet ᜈ;

			// Token: 0x04002D4B RID: 11595
			public ShapeCollectionBase ᜉ;

			// Token: 0x04002D4C RID: 11596
			public int ᜊ;

			// Token: 0x04002D4D RID: 11597
			public int ᜋ;

			// Token: 0x04002D4E RID: 11598
			public ShapeCollectionBase ᜌ;
		}
	}
}
