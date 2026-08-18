using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Threading;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.XmlReaders;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000166 RID: 358
	public class XlsFont : XlsObject, ICloneParent, IComparable, IInternalFont, IDisposable
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x000A6580 File Offset: 0x000A5580
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x000A65D0 File Offset: 0x000A55D0
		public bool IsBold
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
				return this.ᜈ.ᜌ() >= 700;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_69;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_69;
					}
					if (value != this.IsBold)
					{
						num = 2;
						continue;
					}
					break;
					IL_69:
					this.ᜈ.ᜁ(value ? 700 : 400);
					this.SetChanged();
					num = 0;
				}
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x000A6678 File Offset: 0x000A5678
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x000A66C4 File Offset: 0x000A56C4
		public ExcelColors KnownColor
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
				return this.\u170D.ᜂ(this.ᜉ);
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
				this.\u170D.SetKnownColor(value);
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x000A670C File Offset: 0x000A570C
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x000A6758 File Offset: 0x000A5758
		public Color Color
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
				return this.\u170D.ᜁ(this.ᜉ);
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
				this.\u170D.ᜀ(value, this.ᜉ);
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x000A67A8 File Offset: 0x000A57A8
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x000A67F0 File Offset: 0x000A57F0
		public bool IsItalic
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
				return this.ᜈ.ᜃ();
			}
			set
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
							goto IL_3E;
						default:
							goto IL_76;
						}
						break;
					case 2:
						goto IL_3E;
					}
					if (this.ᜈ.ᜃ() != value)
					{
						num = 2;
						continue;
					}
					return;
					IL_3E:
					this.ᜈ.ᜃ(value);
					this.SetChanged();
					if (true)
					{
					}
					num = 0;
				}
				IL_76:
				if (false)
				{
				}
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x000A687C File Offset: 0x000A587C
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x000A68C4 File Offset: 0x000A58C4
		protected internal bool MacOSOutlineFont
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
				return this.ᜈ.ᜎ();
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
				this.ᜈ.ᜂ(value);
				this.SetChanged();
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x000A6914 File Offset: 0x000A5914
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x000A695C File Offset: 0x000A595C
		protected internal bool MacOSShadow
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
				return this.ᜈ.ᜋ();
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
				this.ᜈ.ᜀ(value);
				this.SetChanged();
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x000A69AC File Offset: 0x000A59AC
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x000A6A00 File Offset: 0x000A5A00
		public double Size
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
				return (double)this.ᜈ.ᜏ() / 20.0;
			}
			set
			{
				int a_ = 18;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_E2:
					num = 3;
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
					case 1:
					{
						if (value > 409.0)
						{
							goto IL_E2;
						}
						ushort num2 = (ushort)(value * 20.0);
						num = 4;
						continue;
					}
					case 2:
						return;
					case 3:
						goto IL_ED;
					case 4:
					{
						ushort num2;
						if (this.ᜈ.ᜏ() != num2)
						{
							num = 5;
							continue;
						}
						return;
					}
					case 5:
						this.ᜈ.ᜀ((ushort)(value * 20.0));
						this.SetChanged();
						num = 2;
						continue;
					case 6:
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (value < 1.0)
					{
						break;
					}
					num = 6;
				}
				IL_A9:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᭇ⍉㙋⭍", a_), RecordTableEnumerator.b("ᭇ⍉㙋⭍灏㵑㉓癕㹗㕙㉛⩝䁟ᅡౣ॥ᵧ٩࡫乭ቯ᝱味᩵ᵷॹཻ幽ꢇ뺉벋랍낏望뢗ﶙﮝ솟횡솣풥袧\udea9쒫쾭\udeaf銱薳颵", a_));
				IL_ED:
				goto IL_A9;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x000A6B28 File Offset: 0x000A5B28
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x000A6B70 File Offset: 0x000A5B70
		public bool IsStrikethrough
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
				return this.ᜈ.\u170D();
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
				this.ᜈ.ᜁ(value);
				this.SetChanged();
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000A6BC0 File Offset: 0x000A5BC0
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x000A6C0C File Offset: 0x000A5C0C
		public bool IsSubscript
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
				return this.ᜈ.ᜆ() == FontVertialAlignmentType.Subscript;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 2:
							if (value)
							{
								num = 8;
								continue;
							}
							num = 6;
							continue;
						case 4:
							goto IL_C9;
						case 5:
							if (true)
							{
							}
							goto IL_C9;
						case 6:
							if (this.ᜈ.ᜆ() == FontVertialAlignmentType.Subscript)
							{
								num = 7;
								continue;
							}
							goto IL_C9;
						case 7:
							this.ᜈ.ᜀ(FontVertialAlignmentType.Baseline);
							num = 5;
							continue;
						case 8:
							this.ᜈ.ᜀ(FontVertialAlignmentType.Subscript);
							num = 4;
							continue;
						}
						if (value != this.IsSubscript)
						{
							num = 1;
							continue;
						}
						return;
						IL_C9:
						this.SetChanged();
						num = 0;
					}
				}
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x000A6D10 File Offset: 0x000A5D10
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x000A6D5C File Offset: 0x000A5D5C
		public bool IsSuperscript
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
				return this.ᜈ.ᜆ() == FontVertialAlignmentType.Superscript;
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_C9;
						case 1:
							if (this.ᜈ.ᜆ() == FontVertialAlignmentType.Superscript)
							{
								num = 8;
								continue;
							}
							goto IL_C9;
						case 2:
							this.ᜈ.ᜀ(FontVertialAlignmentType.Superscript);
							num = 5;
							continue;
						case 3:
							if (value)
							{
								num = 2;
								continue;
							}
							num = 1;
							continue;
						case 5:
							goto IL_C9;
						case 6:
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
						case 7:
							return;
						case 8:
							this.ᜈ.ᜀ(FontVertialAlignmentType.Baseline);
							num = 0;
							continue;
						}
						if (true)
						{
						}
						if (value != this.IsSuperscript)
						{
							num = 6;
							continue;
						}
						return;
						IL_C9:
						this.SetChanged();
						num = 7;
					}
				}
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x000A6E60 File Offset: 0x000A5E60
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x000A6EA8 File Offset: 0x000A5EA8
		public FontUnderlineType Underline
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
				return this.ᜈ.ᜈ();
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
				this.ᜈ.ᜀ(value);
				this.SetChanged();
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x000A6EF8 File Offset: 0x000A5EF8
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x000A6F40 File Offset: 0x000A5F40
		public string FontName
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
				return this.ᜈ.ᜀ();
			}
			set
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
							goto IL_43;
						default:
							goto IL_73;
						}
						break;
					case 2:
						goto IL_43;
					}
					if (value != this.ᜈ.ᜀ())
					{
						num = 2;
						continue;
					}
					return;
					IL_43:
					this.ᜈ.ᜀ(value);
					this.SetChanged();
					num = 0;
				}
				IL_73:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x000A6FD0 File Offset: 0x000A5FD0
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x000A7018 File Offset: 0x000A6018
		public FontVertialAlignmentType VerticalAlignment
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
				return this.ᜈ.ᜆ();
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
				this.ᜈ.ᜀ(value);
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x000A7060 File Offset: 0x000A6060
		public bool IsAutoColor
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

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x000A709C File Offset: 0x000A609C
		internal spr\u2267 Record
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
				return this.ᜈ;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x000A70E0 File Offset: 0x000A60E0
		internal XlsWorkbook ParentWorkbook
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_38;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38;
						default:
							goto IL_71;
						}
						break;
					}
					if (this.ᜉ == null)
					{
						num = 0;
						continue;
					}
					goto IL_81;
					IL_38:
					this.ᜉ = (XlsWorkbook)base.FindParent(typeof(XlsWorkbook));
					num = 2;
				}
				IL_71:
				if (true)
				{
				}
				if (false)
				{
				}
				IL_81:
				return this.ᜉ;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x000A7174 File Offset: 0x000A6174
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x000A71B8 File Offset: 0x000A61B8
		internal int Index
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
				return this.ᜊ;
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
						this.ᜊ = value;
						goto IL_64;
					case 1:
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_64:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						if (this.ᜊ == value)
						{
							return;
						}
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x000A7234 File Offset: 0x000A6234
		protected Graphics BookGraphics
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
				return this.ParentWorkbook.InnerGraphics;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x000A727C File Offset: 0x000A627C
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x000A72C4 File Offset: 0x000A62C4
		public byte CharSet
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
				return this.ᜈ.ᜇ();
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
				this.ᜈ.ᜁ(value);
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x000A730C File Offset: 0x000A630C
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x000A7354 File Offset: 0x000A6354
		internal byte Family
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
				return this.ᜈ.ᜅ();
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
				this.ᜈ.ᜀ(value);
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x000A739C File Offset: 0x000A639C
		public OColor OColor
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
				return this.\u170D;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x000A73E0 File Offset: 0x000A63E0
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x000A7424 File Offset: 0x000A6424
		public string Language
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x000A7468 File Offset: 0x000A6468
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x000A74AC File Offset: 0x000A64AC
		internal bool HasLatin
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
				return this.ᜑ;
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
				this.ᜑ = value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x000A74F0 File Offset: 0x000A64F0
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x000A7534 File Offset: 0x000A6534
		internal bool HasComplexScripts
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
				return this.\u1712;
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
				this.\u1712 = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x000A7578 File Offset: 0x000A6578
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x000A75BC File Offset: 0x000A65BC
		internal bool HasEastAsianFont
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
				return this.\u1713;
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
				this.\u1713 = value;
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000A7600 File Offset: 0x000A6600
		internal XlsFont(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 2;
			this.ᜊ = -1;
			this.ᜋ = 1;
			this.\u1715 = new string[]
			{
				RecordTableEnumerator.b("稷䠹䤻䴽⠿扁ᝃ╅㩇⍉㱋㩍灏ὑS", a_)
			};
			base..ctor(A_0, A_1);
			this.ᜈ = (spr\u2267)spr\u175E.ᜀ(TBIFFRecord.Font);
			this.ᜈ.ᜀ(base.AppImplementation.\u1715());
			this.ᜈ.ᜀ((ushort)XlsFont.SizeInTwips(base.AppImplementation.\u171A()));
			this.ᜃ();
			this.ᜀ();
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000A769C File Offset: 0x000A669C
		internal XlsFont(spr\u1DF5 A_0, object A_1, sprἛ A_2) : this(A_0, A_1)
		{
			this.ᜀ(A_2);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000A76B8 File Offset: 0x000A66B8
		internal XlsFont(spr\u1DF5 A_0, object A_1, spr\u2267 A_2) : this(A_0, A_1)
		{
			this.ᜈ = A_2;
			this.ᜁ();
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000A76DC File Offset: 0x000A66DC
		public XlsFont(IFont baseFont)
		{
			int a_ = 2;
			this..ctor(((XlsFont)baseFont).ReservedHandle, baseFont.Parent);
			if (baseFont is XlsFont)
			{
				this.ᜈ = (spr\u2267)((XlsFont)baseFont).Record.Clone();
			}
			else
			{
				if (!(baseFont is FontWrapper))
				{
					throw new ArgumentException(RecordTableEnumerator.b("焷吹䨻弽ⰿ⭁⁃晅⹇╉≋㩍繏", a_));
				}
				this.ᜈ = (spr\u2267)((FontWrapper)baseFont).Wrapped.Record.Clone();
			}
			this.ᜁ();
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000A7778 File Offset: 0x000A6778
		internal XlsFont(spr\u1DF5 A_0, object A_1, Font A_2) : this(A_0, A_1)
		{
			this.ᜀ(A_2);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000A7794 File Offset: 0x000A6794
		private void ᜃ()
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
			this.\u170D = new OColor((ExcelColors)this.ᜈ.ᜂ());
			this.\u170D.AfterChange += this.ᜂ;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000A77FC File Offset: 0x000A67FC
		private void ᜂ()
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
			this.ᜈ.ᜂ((ushort)this.\u170D.ᜂ(this.ᜉ));
			this.SetChanged();
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000A785C File Offset: 0x000A685C
		private void ᜁ()
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
			this.\u170D.SetKnownColor((ExcelColors)this.ᜈ.ᜂ());
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x000A78B0 File Offset: 0x000A68B0
		private void ᜀ()
		{
			int a_ = 5;
			this.ᜉ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜉ == null)
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
					throw new ArgumentException(RecordTableEnumerator.b("砺尼儾⽀ⱂㅄ杆⽈≊⍌⭎煐⍒㑔╖㱘㕚⥜罞ᙠౢᝤ౦୨ѪɬѮ彰", a_));
				}
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000A7930 File Offset: 0x000A6930
		private void ᜀ(sprἛ A_0)
		{
			int a_ = 16;
			int num = 1;
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5F;
				case 2:
					if (biffRecordRaw.TypeCode != TBIFFRecord.Font)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_B7;
				case 3:
					goto IL_A1;
				}
				IL_33:
				if (!A_0.ᜂ())
				{
					biffRecordRaw = A_0.ᜃ();
					num = 2;
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
					num = 0;
					continue;
				}
				goto IL_33;
			}
			IL_5F:
			throw new ApplicationException(RecordTableEnumerator.b("ፅ♇⽉㑋㹍㕏ㅑ⁓㍕㱗穙㥛そџ䉡ୣe䡧ᥩᡫᱭᕯ፱ᥳ塵", a_));
			IL_A1:
			throw new ApplicationException(RecordTableEnumerator.b("ፅ♇ⅉ≋⅍❏㱑瑓さ㝗㑙⡛繝㉟ݡݣ॥ᩧ๩", a_));
			IL_B7:
			this.ᜈ = (spr\u2267)biffRecordRaw;
			this.ᜁ();
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000A7A08 File Offset: 0x000A6A08
		public void SerializeDataToList(RecordArrayList records)
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
			records.ᜀ(this.ᜈ);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000A7A50 File Offset: 0x000A6A50
		public void CopyTo(XlsFont twin)
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
			this.ᜈ.CopyTo(twin.ᜈ);
			twin.CharSet = this.CharSet;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000A7AA8 File Offset: 0x000A6AA8
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
			this.ParentWorkbook.Saved = false;
			this.ᜌ = null;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000A7AF8 File Offset: 0x000A6AF8
		public Font GenerateNativeFont()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 2:
					this.ᜌ = this.GenerateNativeFont((float)this.Size);
					goto IL_67;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_67:
					num = 0;
					continue;
				}
				if (false)
				{
				}
				if (this.ᜌ != null)
				{
					break;
				}
				num = 2;
			}
			IL_6F:
			if (true)
			{
			}
			return this.ᜌ;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000A7B84 File Offset: 0x000A6B84
		public Font GenerateNativeFont(float size)
		{
			FontStyle fontStyle;
			for (;;)
			{
				fontStyle = FontStyle.Regular;
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						fontStyle |= FontStyle.Underline;
						num = 1;
						continue;
					case 1:
						goto IL_63;
					case 2:
						if (this.IsStrikethrough)
						{
							num = 14;
							continue;
						}
						goto IL_8F;
					case 3:
						goto IL_243;
					case 4:
						if (this.Underline != FontUnderlineType.None)
						{
							num = 0;
							continue;
						}
						goto IL_63;
					case 5:
						fontStyle |= FontStyle.Italic;
						num = 13;
						continue;
					case 6:
						if (this.IsItalic)
						{
							num = 10;
							continue;
						}
						goto IL_243;
					case 7:
						goto IL_8F;
					case 8:
						goto IL_B2;
					case 9:
						goto IL_5E;
					case 10:
						fontStyle |= FontStyle.Italic;
						num = 3;
						continue;
					case 11:
						if (Array.IndexOf<string>(this.\u1715, this.FontName) >= 0)
						{
							num = 5;
							continue;
						}
						goto IL_D8;
					case 12:
						if (this.IsBold)
						{
							num = 9;
							continue;
						}
						goto IL_B2;
					case 13:
						goto IL_D8;
					case 14:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5E;
						default:
							if (false)
							{
							}
							fontStyle |= FontStyle.Strikeout;
							num = 7;
							continue;
						}
						break;
					}
					break;
					IL_63:
					num = 11;
					continue;
					IL_8F:
					num = 4;
					continue;
					IL_B2:
					num = 6;
					continue;
					IL_1BE:
					fontStyle |= FontStyle.Bold;
					num = 8;
					continue;
					try
					{
						IL_D8:
						for (;;)
						{
							FontFamily fontFamily = new FontFamily(this.FontName);
							num = 9;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 3;
									continue;
								case 1:
									goto IL_1B3;
								case 2:
									num = 4;
									continue;
								case 3:
									if (!fontFamily.IsStyleAvailable(FontStyle.Regular))
									{
										num = 2;
										continue;
									}
									goto IL_1A8;
								case 4:
									if (fontFamily.IsStyleAvailable(FontStyle.Bold))
									{
										num = 8;
										continue;
									}
									num = 7;
									continue;
								case 5:
									fontStyle = FontStyle.Italic;
									num = 10;
									continue;
								case 6:
									goto IL_1A8;
								case 7:
									if (fontFamily.IsStyleAvailable(FontStyle.Italic))
									{
										num = 5;
										continue;
									}
									goto IL_1A8;
								case 8:
									fontStyle = FontStyle.Bold;
									num = 6;
									continue;
								case 9:
									if (fontStyle == FontStyle.Regular)
									{
										num = 0;
										continue;
									}
									goto IL_1A8;
								case 10:
									goto IL_1A8;
								}
								break;
								IL_1A8:
								num = 1;
							}
						}
						IL_1B3:
						goto IL_273;
					}
					catch (Exception)
					{
						goto IL_273;
					}
					IL_5E:
					goto IL_1BE;
					IL_243:
					num = 2;
				}
			}
			IL_273:
			return new Font(this.FontName, size, fontStyle, GraphicsUnit.Point, this.ᜋ);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000A7E2C File Offset: 0x000A6E2C
		internal void ᜀ(Font A_0)
		{
			int a_ = 10;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_BD;
				case 2:
					goto IL_5E;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_74;
				}
				if (false)
				{
				}
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				IL_74:
				this.FontName = A_0.Name;
				this.Size = (double)((int)A_0.Size);
				this.IsStrikethrough = A_0.Strikeout;
				this.IsBold = A_0.Bold;
				this.IsItalic = A_0.Italic;
				num = 0;
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("⸿⍁ぃ⽅㹇⽉ੋ⅍㹏♑", a_));
			IL_BD:
			this.Underline = (A_0.Underline ? FontUnderlineType.Single : FontUnderlineType.None);
			this.ᜁ();
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000A7F14 File Offset: 0x000A6F14
		public SizeF MeasureString(string strValue)
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
			Size size = base.AppImplementation.ᜀ(strValue, this, new SizeF(2.1474836E+09f, 2.1474836E+09f)).ToSize();
			return new SizeF((float)size.Width, (float)(size.Height - 1));
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000A7F8C File Offset: 0x000A6F8C
		public SizeF MeasureStringSpecial(string strValue)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				StringFormat stringFormat;
				Graphics bookGraphics;
				TextRenderingHint textRenderingHint;
				double num;
				for (;;)
				{
					stringFormat = new StringFormat(StringFormat.GenericTypographic);
					stringFormat.Alignment = StringAlignment.Near;
					stringFormat.FormatFlags = (StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.NoClip);
					stringFormat.SetMeasurableCharacterRanges(XlsFont.ᜇ);
					bookGraphics = this.BookGraphics;
					textRenderingHint = bookGraphics.TextRenderingHint;
					bookGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
					num = this.Size;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (this.IsBold)
							{
								num2 = 5;
								continue;
							}
							num2 = 6;
							continue;
						case 1:
							goto IL_FA;
						case 2:
							num *= (double)((this.Size >= 10.0) ? 1.07f : 1.15f);
							num2 = 1;
							continue;
						case 3:
							num *= 1.0700000524520874;
							num2 = 4;
							continue;
						case 4:
							goto IL_11E;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_11E;
							default:
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						case 6:
							if (this.Size <= 10.0)
							{
								num2 = 3;
								continue;
							}
							goto IL_14B;
						}
						break;
					}
				}
				IL_FA:
				IL_11E:
				IL_14B:
				SizeF result = bookGraphics.MeasureString(strValue, this.GenerateNativeFont((float)Math.Ceiling(num)), int.MaxValue, stringFormat);
				bookGraphics.TextRenderingHint = textRenderingHint;
				return result;
			}
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000A810C File Offset: 0x000A710C
		public SizeF[] MeasureCharacterRanges(string strValue, CharacterRange[] ranges)
		{
			switch (0)
			{
			default:
			{
				SizeF[] array2;
				for (;;)
				{
					for (;;)
					{
						Font font = this.GenerateNativeFont();
						StringFormat stringFormat = new StringFormat();
						stringFormat.SetMeasurableCharacterRanges(ranges);
						stringFormat.FormatFlags = StringFormatFlags.NoClip;
						Region[] array = this.BookGraphics.MeasureCharacterRanges(strValue, font, new RectangleF(0f, 0f, 1000f, 1000f), stringFormat);
						array2 = new SizeF[array.Length];
						int num = 0;
						int num2 = array.Length;
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
							int num3 = 0;
							for (;;)
							{
								switch (num3)
								{
								case 0:
									goto IL_B3;
								case 1:
								{
									if (num >= num2)
									{
										num3 = 2;
										continue;
									}
									RectangleF bounds = array[num].GetBounds(this.BookGraphics);
									array2[num] = new SizeF(bounds.Width, bounds.Height);
									num++;
									if (true)
									{
									}
									num3 = 3;
									continue;
								}
								case 2:
									return array2;
								case 3:
									goto IL_B3;
								}
								break;
								IL_B3:
								num3 = 1;
							}
							break;
						}
						}
					}
				}
				return array2;
			}
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000A823C File Offset: 0x000A723C
		public SizeF MeasureCharacter(char value)
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
			return this.MeasureCharacterRanges(new string(value, 3), XlsFont.ᜇ)[1];
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000A8294 File Offset: 0x000A7294
		private void ᜀ(XlsEventArgs A_0)
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
					this.\u1717(this, A_0);
					goto IL_69;
				case 1:
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_69:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (this.\u1717 == null)
					{
						return;
					}
					num = 0;
					break;
				}
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000A8314 File Offset: 0x000A7314
		public XlsFont TypedClone()
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
			XlsFont xlsFont = base.MemberwiseClone() as XlsFont;
			xlsFont.ᜈ = (this.ᜈ.Clone() as spr\u2267);
			return xlsFont;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000A8374 File Offset: 0x000A7374
		public object Clone()
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
			return this.TypedClone();
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000A83B8 File Offset: 0x000A73B8
		public XlsFont Clone(object parent)
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
			return new XlsFont(base.ReservedHandle, parent)
			{
				m_bIsDisposed = this.m_bIsDisposed,
				ᜊ = -1,
				ᜈ = (spr\u2267)this.ᜈ.Clone()
			};
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000A842C File Offset: 0x000A742C
		void IDisposable.Dispose()
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
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000A8470 File Offset: 0x000A7470
		public static int SizeInTwips(double fontSize)
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
			return (int)(fontSize * 20.0);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000A84B8 File Offset: 0x000A74B8
		public static double SizeInPoints(int twipsSize)
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
			return (double)((float)twipsSize / 20f);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000A84FC File Offset: 0x000A74FC
		internal static int ᜀ(int A_0, Dictionary<int, int> A_1, ExcelParseOptions A_2)
		{
			int result;
			for (;;)
			{
				result = A_0;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_44:
					if (A_1 == null)
					{
						return result;
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
						if (true)
						{
						}
						A_1.TryGetValue(A_0, out result);
						num = 1;
						continue;
					case 1:
						return result;
					case 2:
						goto IL_44;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001110 RID: 4368 RVA: 0x000A8578 File Offset: 0x000A7578
		// (remove) Token: 0x06001111 RID: 4369 RVA: 0x000A8610 File Offset: 0x000A7610
		internal event XlsEventHandler IndexChanged
		{
			add
			{
				for (;;)
				{
					IL_14:
					XlsEventHandler xlsEventHandler = this.\u1717;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_49:
						goto IL_4B;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
					XlsEventHandler xlsEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (xlsEventHandler == xlsEventHandler2)
							{
								num = 2;
								continue;
							}
							goto IL_4B;
						case 1:
							goto IL_49;
						case 2:
							return;
						}
						goto IL_14;
					}
					IL_4B:
					xlsEventHandler2 = xlsEventHandler;
					XlsEventHandler value2 = (XlsEventHandler)Delegate.Combine(xlsEventHandler2, value);
					xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.\u1717, value2, xlsEventHandler2);
					num = 0;
					goto IL_02;
				}
			}
			remove
			{
				for (;;)
				{
					IL_14:
					XlsEventHandler xlsEventHandler = this.\u1717;
					if (true)
					{
					}
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_51:
						goto IL_53;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
					XlsEventHandler xlsEventHandler2;
					for (;;)
					{
						IL_02:
						switch (num)
						{
						case 0:
							return;
						case 1:
							goto IL_51;
						case 2:
							if (xlsEventHandler == xlsEventHandler2)
							{
								num = 0;
								continue;
							}
							goto IL_53;
						}
						goto IL_14;
					}
					IL_53:
					xlsEventHandler2 = xlsEventHandler;
					XlsEventHandler value2 = (XlsEventHandler)Delegate.Remove(xlsEventHandler2, value);
					xlsEventHandler = Interlocked.CompareExchange<XlsEventHandler>(ref this.\u1717, value2, xlsEventHandler2);
					num = 2;
					goto IL_02;
				}
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000A86A8 File Offset: 0x000A76A8
		public override bool Equals(object obj)
		{
			XlsFont xlsFont;
			for (;;)
			{
				xlsFont = (obj as XlsFont);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (xlsFont == null)
						{
							num = 5;
							continue;
						}
						num = 1;
						continue;
					case 1:
						if (this.GetHashCode() != xlsFont.GetHashCode())
						{
							num = 7;
							continue;
						}
						num = 2;
						continue;
					case 2:
						goto IL_D2;
					case 3:
						if (this.ᜋ == xlsFont.ᜋ)
						{
							num = 6;
							continue;
						}
						return false;
					case 4:
						num = 3;
						continue;
					case 5:
						return false;
					case 6:
						goto IL_65;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D2;
						default:
							goto IL_7D;
						}
						break;
					}
					break;
					IL_D2:
					if (!xlsFont.ᜈ.Equals(this.ᜈ))
					{
						return false;
					}
					num = 4;
				}
			}
			return false;
			IL_65:
			return this.\u170D == xlsFont.\u170D;
			IL_7D:
			if (false)
			{
			}
			if (true)
			{
			}
			return false;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000A87B8 File Offset: 0x000A77B8
		public override int GetHashCode()
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
			return this.ᜈ.GetHashCode();
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000A8800 File Offset: 0x000A7800
		public int CompareTo(object obj)
		{
			int a_ = 10;
			if (true)
			{
			}
			for (;;)
			{
				XlsFont xlsFont = obj as XlsFont;
				int num = 7;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						return num2;
					case 1:
						goto IL_114;
					case 2:
						num = 8;
						continue;
					case 3:
						if (num2 == 0)
						{
							num = 9;
							continue;
						}
						goto IL_114;
					case 4:
						num = 10;
						continue;
					case 5:
						if (num2 == 0)
						{
							goto IL_12C;
						}
						return num2;
					case 6:
						goto IL_63;
					case 7:
						if (xlsFont == null)
						{
							num = 6;
							continue;
						}
						num2 = this.ᜈ.ᜀ(xlsFont.ᜈ);
						num = 3;
						continue;
					case 8:
						if (!(this.\u170D == xlsFont.\u170D))
						{
							num = 4;
							continue;
						}
						num = 11;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_12C;
						default:
							if (false)
							{
							}
							num2 = (int)(this.ᜋ - xlsFont.ᜋ);
							num = 1;
							continue;
						}
						break;
					case 10:
						num3 = 1;
						goto IL_E4;
					case 11:
						num3 = 0;
						goto IL_E4;
					}
					break;
					IL_E4:
					num2 = num3;
					num = 0;
					continue;
					IL_114:
					num = 5;
					continue;
					IL_12C:
					num = 2;
				}
			}
			IL_63:
			throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ⩃㉅", a_));
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x000A8968 File Offset: 0x000A7968
		int IInternalFont.Index
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
				return this.Index;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x000A89AC File Offset: 0x000A79AC
		public XlsFont Font
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
				return this;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x000A89E8 File Offset: 0x000A79E8
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x000A8A2C File Offset: 0x000A7A2C
		internal string ActualFontName
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
				return this.\u1714;
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
				this.\u1714 = value;
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000A8A70 File Offset: 0x000A7A70
		public void BeginUpdate()
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

		// Token: 0x0600111A RID: 4378 RVA: 0x000A8AAC File Offset: 0x000A7AAC
		public void EndUpdate()
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

		// Token: 0x0600111B RID: 4379 RVA: 0x000A8AE8 File Offset: 0x000A7AE8
		object ICloneParent.Clone(object parent)
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
			return this.Clone(parent);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000A8B2C File Offset: 0x000A7B2C
		// Note: this type is marked as 'beforefieldinit'.
		static XlsFont()
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
			XlsFont.ᜇ = new CharacterRange[]
			{
				new CharacterRange(0, 2),
				new CharacterRange(1, 1)
			};
		}

		// Token: 0x04000DF0 RID: 3568
		internal const ushort ᜀ = 700;

		// Token: 0x04000DF1 RID: 3569
		internal const ushort ᜁ = 400;

		// Token: 0x04000DF2 RID: 3570
		private const int ᜂ = -1;

		// Token: 0x04000DF3 RID: 3571
		private byte[] \u25D8\u0095\u0084\u009A;

		// Token: 0x04000DF4 RID: 3572
		internal const int ᜃ = 4;

		// Token: 0x04000DF5 RID: 3573
		private const float ᜄ = 1.15f;

		// Token: 0x04000DF6 RID: 3574
		private float[] \u25D9\u0094\u00AFª;

		// Token: 0x04000DF7 RID: 3575
		private const float ᜅ = 1.07f;

		// Token: 0x04000DF8 RID: 3576
		private const int ᜆ = 64;

		// Token: 0x04000DF9 RID: 3577
		private static readonly CharacterRange[] ᜇ;

		// Token: 0x04000DFA RID: 3578
		private spr\u2267 ᜈ;

		// Token: 0x04000DFB RID: 3579
		private XlsWorkbook ᜉ;

		// Token: 0x04000DFC RID: 3580
		private int ᜊ;

		// Token: 0x04000DFD RID: 3581
		private byte ᜋ;

		// Token: 0x04000DFE RID: 3582
		private Font ᜌ;

		// Token: 0x04000DFF RID: 3583
		private OColor \u170D;

		// Token: 0x04000E00 RID: 3584
		private string ᜎ;

		// Token: 0x04000E01 RID: 3585
		private string ᜏ;

		// Token: 0x04000E02 RID: 3586
		private int ᜐ;

		// Token: 0x04000E03 RID: 3587
		private string[] \u2609\u00A6\u0094\u0085;

		// Token: 0x04000E04 RID: 3588
		private bool ᜑ;

		// Token: 0x04000E05 RID: 3589
		private bool \u1712;

		// Token: 0x04000E06 RID: 3590
		private bool \u1713;

		// Token: 0x04000E07 RID: 3591
		private string \u1714;

		// Token: 0x04000E08 RID: 3592
		private string[] \u1715;

		// Token: 0x04000E09 RID: 3593
		internal TextSettings \u1716;

		// Token: 0x04000E0A RID: 3594
		private XlsEventHandler \u1717;
	}
}
