using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc
{
	// Token: 0x020000DC RID: 220
	public class Table : BodyRegion, ITable, spr\u1AE4
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0001B8DC File Offset: 0x0001A8DC
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0001B920 File Offset: 0x0001A920
		internal bool IsHasCaculatedCellWidth
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
				return this.ᜥ;
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
				this.ᜥ = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0001B964 File Offset: 0x0001A964
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0001B9A8 File Offset: 0x0001A9A8
		internal bool IsSDTTable
		{
			[CompilerGenerated]
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
				return this.ᜦ;
			}
			[CompilerGenerated]
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
				this.ᜦ = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0001B9EC File Offset: 0x0001A9EC
		internal TextBoxFormat TextBoxFormat
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
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0001BA30 File Offset: 0x0001AA30
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0001BA74 File Offset: 0x0001AA74
		internal RectangleF TableBounds
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0001BAB8 File Offset: 0x0001AAB8
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0001BAFC File Offset: 0x0001AAFC
		internal spr\u1F89 LastLayoutPage
		{
			[CompilerGenerated]
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
				return this.ᜧ;
			}
			[CompilerGenerated]
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
				this.ᜧ = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0001BB40 File Offset: 0x0001AB40
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0001BB84 File Offset: 0x0001AB84
		internal ArrayList Offsets
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

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001BBC8 File Offset: 0x0001ABC8
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0001BC24 File Offset: 0x0001AC24
		public float DefaultRowHeight
		{
			get
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
					if (this.\u170D == null)
					{
						return 0f;
					}
					break;
				}
				return this.\u170D.Value;
			}
			set
			{
				int a_ = 3;
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
					if (value < 0f)
					{
						throw new ArgumentOutOfRangeException(ClipboardData.b("⵨๪୬๮ѰὲŴ╶ᙸ౺㕼᩾", a_), ClipboardData.b("ᵨͪ࡬佮ᥰᙲᱴၶᅸེ嵼ၾꎂﺈꮊﾐ뎒ﮔ뮚ﾜ爵膠쾢삤풦\udaa8讪\ud9ac잮킰\uddb2閴螶鞸", a_));
					}
					break;
				}
				this.\u170D = new float?(value);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0001BCA0 File Offset: 0x0001ACA0
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0001BCF8 File Offset: 0x0001ACF8
		public int DefaultColumnsNumber
		{
			get
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
					if (this.ᜇ == null)
					{
						return 0;
					}
					break;
				}
				return this.ᜇ.Value;
			}
			set
			{
				int a_ = 19;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_36;
					case 1:
						goto IL_76;
					case 3:
						if (value < 0)
						{
							num = 1;
							continue;
						}
						goto IL_C0;
					}
					if (value > 63)
					{
						num = 0;
					}
					else
					{
						num = 3;
					}
				}
				for (;;)
				{
					IL_36:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("㵸Ṻ᭼Ṿ쒆ﾐ\udb94連", a_), ClipboardData.b("㝸ᑺॼ彾力歷뎒ﺚ붜즠슢쮤螦龨颪趬첮\udeb0\udfb2살\udab6ힸ좺鎼", a_));
				IL_76:
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("㵸Ṻ᭼Ṿ쒆ﾐ\udb94連", a_), ClipboardData.b("൸፺᡼彾力권뎒膠삢쒤즦覨얪슬\udbae醰톲킴鞶햸\udeba캼첾럂귄ꛆꟈ﷌", a_));
				IL_C0:
				this.ᜇ = new int?(value);
				this.ᜀ();
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0001BDD8 File Offset: 0x0001ADD8
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0001BE34 File Offset: 0x0001AE34
		public float DefaultColumnWidth
		{
			get
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
					if (this.ᜉ == null)
					{
						return 0f;
					}
					break;
				}
				return this.ᜉ.Value;
			}
			set
			{
				int a_ = 11;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (value < 0f)
					{
						if (true)
						{
						}
						throw new ArgumentOutOfRangeException(ClipboardData.b("㕰ᙲ፴ᙶ౸᝺ॼ㱾\udc8aﮒ", a_), ClipboardData.b("հ᭲ၴ坶๸ቺ᥼୾ꎂꦈﺒﮔ랖滛漢뾞쾠첢톤螦쮨캪趬쎮풰삲운鞶춸펺\udcbc톾", a_));
					}
					break;
				}
				this.ᜉ = new float?(value);
				this.ᜈ = true;
				this.ᜀ();
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001BEC0 File Offset: 0x0001AEC0
		internal List<float> _ColumnWidths
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
						this.\u1717 = new List<float>();
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (this.\u1717 != null)
					{
						break;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6F;
					}
					if (false)
					{
					}
					num = 2;
				}
				IL_6F:
				return this.\u1717;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0001BF44 File Offset: 0x0001AF44
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0001BF88 File Offset: 0x0001AF88
		public float[] ColumnWidth
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
				return this.ᜋ;
			}
			set
			{
				int a_ = 19;
				int num = 7;
				int num2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value[num2] >= 0f)
						{
							num2++;
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BF;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 1:
						goto IL_AE;
					case 2:
						goto IL_4F;
					case 3:
						goto IL_AE;
					case 4:
						goto IL_98;
					case 5:
						if (num2 >= value.Length)
						{
							goto IL_BF;
						}
						num = 0;
						continue;
					case 6:
						goto IL_CA;
					}
					if (true)
					{
					}
					if (value == null)
					{
						num = 2;
						continue;
					}
					num2 = 0;
					num = 3;
					continue;
					IL_AE:
					num = 5;
					continue;
					IL_BF:
					num = 6;
				}
				IL_4F:
				throw new ArgumentNullException(ClipboardData.b("㩸ᑺᅼ੾튄ﾊ", a_));
				IL_98:
				string message = string.Format(ClipboardData.b("㩸ᑺᅼ੾튄ﾊ풎ꎒ쪖릘Ꞛ붜꾞趠莢톤쾦첨讪\udaac욮햰잲\uddb4鞶횸\uddba鶼\udcbe껀꿂냄꫆ꟈ껌껎뿐믔룖귘ﯚ뿜뫞쇠迢胤铦髨쯪駬蟮郰鷲헴쟶ퟸ", a_), num2);
				throw new ArgumentOutOfRangeException(ClipboardData.b("ླྀ᩺ᅼ੾", a_), message);
				IL_CA:
				this.ᜋ = value;
				this.ᜊ = true;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001C0B8 File Offset: 0x0001B0B8
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0001C0FC File Offset: 0x0001B0FC
		private float[] ColumnWidthPercent
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
				return this.ᜌ;
			}
			set
			{
				int a_ = 9;
				int num = 5;
				int num2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A3;
					case 1:
						goto IL_A3;
					case 2:
						if (num2 >= value.Length)
						{
							num = 6;
							continue;
						}
						num = 7;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C1;
						default:
							goto IL_5D;
						}
						break;
					case 4:
						goto IL_8D;
					case 6:
						return;
					case 7:
						if (value[num2] < 0f)
						{
							num = 4;
							continue;
						}
						num2++;
						num = 1;
						continue;
					}
					if (value == null)
					{
						num = 3;
						continue;
					}
					goto IL_C1;
					IL_A3:
					num = 2;
					continue;
					IL_C1:
					if (true)
					{
					}
					num2 = 0;
					num = 0;
				}
				IL_5D:
				if (false)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("ⱮṰὲt᩶᝸ⱺᑼ᭾햄ﮈ", a_));
				IL_8D:
				string message = string.Format(ClipboardData.b("᥮ၰὲtቶ≸z䵼ɾ\udc80ꎂ름Ꞇ릈꞊권ﮎ戀떔ﾚ膠첢쎤螦쪨쒪솬\udaae\udcb0\uddb2閴풶\ud8b8햺鶼톾껀럂ꗆ곈ꇌ꫎ꋐꃒꏖ뇘뫚돜￞퇠췢", a_), num2);
				throw new ArgumentOutOfRangeException(ClipboardData.b("᥮ၰὲtቶ", a_), message);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0001C21C File Offset: 0x0001B21C
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.Table;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0001C25C File Offset: 0x0001B25C
		public RowCollection Rows
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
				return this.ᜃ;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0001C2A0 File Offset: 0x0001B2A0
		public RowFormat TableFormat
		{
			get
			{
				int num = 4;
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
							goto IL_5B;
						default:
							if (false)
							{
							}
							goto IL_65;
						}
						break;
					case 2:
						this.ᜄ.ImportContainer(this.FirstRow.RowFormat);
						num = 7;
						continue;
					case 3:
						if (this.Rows.Count > 0)
						{
							goto IL_5B;
						}
						goto IL_DB;
					case 5:
						if (this.ᜄ.IsDefault)
						{
							num = 0;
							continue;
						}
						goto IL_DB;
					case 6:
						this.ᜄ = new RowFormat();
						num = 1;
						continue;
					case 7:
						goto IL_D9;
					}
					if (this.ᜄ == null)
					{
						num = 6;
						continue;
					}
					goto IL_65;
					IL_5B:
					num = 2;
					continue;
					IL_65:
					num = 5;
				}
				IL_D9:
				IL_DB:
				if (true)
				{
				}
				this.ᜄ.ᜀ(this);
				return this.ᜄ;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0001C3AC File Offset: 0x0001B3AC
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0001C4A4 File Offset: 0x0001B4A4
		public PreferredWidth PreferredWidth
		{
			get
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
					{
						FtsWidth ftsWidth = this.\u171F.ᜀ();
						goto IL_60;
					}
					case 2:
						goto IL_CF;
					case 4:
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
							FtsWidth ftsWidth;
							switch (ftsWidth)
							{
							case FtsWidth.None:
								goto IL_4C;
							case FtsWidth.Auto:
								goto IL_D9;
							case FtsWidth.Percentage:
								goto IL_39;
							case FtsWidth.Point:
								goto IL_A7;
							default:
								num = 0;
								continue;
							}
							break;
						}
						}
						break;
					}
					if (this.\u171F != null)
					{
						num = 1;
						continue;
					}
					goto IL_E1;
					IL_60:
					num = 4;
				}
				IL_39:
				return new PreferredWidth(WidthType.Percentage, (short)this.\u171F.ᜁ());
				IL_4C:
				return new PreferredWidth(WidthType.None, 0);
				IL_A7:
				return new PreferredWidth(WidthType.Twip, (short)this.\u171F.ᜁ());
				IL_CF:
				if (true)
				{
				}
				goto IL_E1;
				IL_D9:
				return new PreferredWidth(WidthType.Auto, 0);
				IL_E1:
				return new PreferredWidth(WidthType.None, 0);
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6C;
					case 1:
						num = 2;
						continue;
					case 2:
						if (value.Type == WidthType.Auto)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_56;
							}
							if (false)
							{
							}
							num = 5;
							continue;
						}
						this.TableFormat.IsAutoResized = false;
						num = 4;
						continue;
					case 4:
						goto IL_56;
					case 5:
						goto IL_58;
					}
					if (value.Type != WidthType.None)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					IL_58:
					this.TableFormat.IsAutoResized = true;
					num = 0;
				}
				IL_56:
				IL_6C:
				this.PreferredTableWidth.ᜀ((FtsWidth)value.Type);
				this.PreferredTableWidth.ᜀ((int)value.Value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0001C588 File Offset: 0x0001B588
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0001C60C File Offset: 0x0001B60C
		internal Table.ᜀ PreferredTableWidth
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u171F = new Table.ᜀ();
						goto IL_43;
					case 1:
						goto IL_4D;
					}
					if (this.\u171F == null)
					{
						num = 0;
						continue;
					}
					goto IL_4D;
					IL_43:
					num = 1;
					continue;
					IL_4D:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_43;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.\u171F;
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
				this.\u171F = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0001C650 File Offset: 0x0001B650
		public string TableStyleName
		{
			get
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
					if (this.\u1718 != null)
					{
						return this.\u1718.Name;
					}
					break;
				}
				return null;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0001C6A4 File Offset: 0x0001B6A4
		internal spr\u2179 TableStyle
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
				return this.\u1718;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0001C6E8 File Offset: 0x0001B6E8
		public TableCell LastCell
		{
			get
			{
				TableRow lastRow;
				int count;
				for (;;)
				{
					lastRow = this.LastRow;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
					default:
					{
						if (false)
						{
						}
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								count = lastRow.Cells.Count;
								num = 1;
								continue;
							case 1:
								if (count <= 0)
								{
									num = 3;
									continue;
								}
								goto IL_50;
							case 2:
								if (lastRow != null)
								{
									num = 0;
									continue;
								}
								goto IL_9B;
							case 3:
								goto IL_99;
							}
							break;
						}
						break;
					}
					}
				}
				IL_50:
				return lastRow.Cells[count - 1];
				IL_99:
				if (true)
				{
				}
				return null;
				IL_9B:
				return null;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0001C794 File Offset: 0x0001B794
		public TableRow FirstRow
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
				return this.Rows.FirstItem as TableRow;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001C7E0 File Offset: 0x0001B7E0
		public TableRow LastRow
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
				return this.Rows.LastItem as TableRow;
			}
		}

		// Token: 0x170000FB RID: 251
		public TableCell this[int row, int column]
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
				return this.Rows[row].Cells[column];
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0001C880 File Offset: 0x0001B880
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0001C90C File Offset: 0x0001B90C
		public float Width
		{
			get
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						this.ᜅ = this.ᜑ();
						goto IL_51;
					case 2:
						goto IL_5B;
					}
					if (this.ᜅ == -3.4028235E+38f)
					{
						num = 1;
						continue;
					}
					goto IL_5B;
					IL_51:
					num = 2;
					continue;
					IL_5B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_51;
					default:
						goto IL_71;
					}
				}
				IL_71:
				if (false)
				{
				}
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0001C950 File Offset: 0x0001B950
		public DocumentObjectCollection ChildObjects
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
				return this.ᜃ;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0001C994 File Offset: 0x0001B994
		internal List<float> TableGrid
		{
			get
			{
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜃ.Count <= 0)
						{
							goto IL_F4;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E0;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 1:
						goto IL_DE;
					case 2:
						goto IL_CC;
					case 3:
						goto IL_CC;
					case 4:
						if (this.ᜃ != null)
						{
							num = 11;
							continue;
						}
						goto IL_F4;
					case 5:
						this.ᜂ();
						num = 2;
						continue;
					case 6:
						goto IL_AC;
					case 7:
						if (this.ᜆ == null)
						{
							num = 8;
							continue;
						}
						goto IL_12F;
					case 8:
						num = 4;
						continue;
					case 10:
						goto IL_E0;
					case 11:
						num = 0;
						continue;
					}
					if (!this.ᜢ)
					{
						num = 10;
						continue;
					}
					IL_AC:
					num = 7;
					continue;
					IL_CC:
					this.ᜢ = true;
					num = 1;
					continue;
					IL_E0:
					this.ᜆ = null;
					num = 6;
					continue;
					IL_F4:
					this.ᜆ = new List<float>();
					num = 3;
				}
				IL_DE:
				IL_12F:
				return this.ᜆ;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0001CAE0 File Offset: 0x0001BAE0
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0001CBC4 File Offset: 0x0001BBC4
		public float IndentFromLeft
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 2:
						if (this.TableFormat.RowIndent != -3.4028235E+38f)
						{
							num = 4;
							continue;
						}
						goto IL_CC;
					case 3:
						goto IL_50;
					case 4:
						goto IL_91;
					case 5:
						if (this.TableFormat.HasValue(107))
						{
							goto IL_B7;
						}
						goto IL_CC;
					}
					if (this.TableFormat.RowIndent == 0f)
					{
						num = 0;
						continue;
					}
					IL_50:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_B7:
						if (true)
						{
						}
						num = 3;
						break;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
				}
				IL_91:
				return this.TableFormat.RowIndent;
				IL_CC:
				return this.TableFormat.LeftIndent;
			}
			set
			{
				int a_ = 3;
				switch (0)
				{
				default:
				{
					int num = 6;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_205;
						case 1:
							num = 4;
							continue;
						case 2:
						{
							if (value < -3.4028235E+38f)
							{
								num = 5;
								continue;
							}
							IEnumerator enumerator = this.Rows.GetEnumerator();
							num = 0;
							continue;
						}
						case 3:
							num = 2;
							continue;
						case 4:
							if (value <= 3.4028235E+38f)
							{
								num = 3;
								continue;
							}
							goto IL_13F;
						case 5:
							goto IL_1DE;
						}
						if (value == 0f)
						{
							return;
						}
						num = 1;
					}
					IL_13F:
					throw new ArgumentOutOfRangeException(ClipboardData.b("⁨ժ६੮ὰݲ㍴նᙸᙺㅼ᩾", a_), string.Concat(new object[]
					{
						ClipboardData.b("⁨ժ६੮ὰݲ㍴նᙸᙺㅼ᩾ꖄ뎒릘쒠톢薤펦솨쪪쎬辮", a_),
						float.MaxValue,
						ClipboardData.b("䥨੪ͬ୮兰ὲᑴնṸṺོ彾ꦈ", a_),
						float.MinValue
					}));
					IL_1DE:
					goto IL_13F;
					IL_205:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									goto IL_9F;
								}
								TableRow tableRow = (TableRow)enumerator.Current;
								tableRow.RowFormat.RowIndent = value;
								num = 4;
								continue;
							}
							case 2:
								goto IL_F5;
							case 3:
								num = 2;
								continue;
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_9F;
								default:
									if (false)
									{
									}
									break;
								}
								break;
							}
							IL_8E:
							num = 0;
							continue;
							goto IL_8E;
							IL_9F:
							num = 3;
						}
						IL_F5:
						return;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable = enumerator as IDisposable;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_13C;
								case 1:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_13E;
								case 2:
									disposable.Dispose();
									num = 0;
									continue;
								}
								break;
							}
						}
						IL_13C:
						IL_13E:;
					}
					goto IL_13F;
				}
				}
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0001CDEC File Offset: 0x0001BDEC
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0001CE30 File Offset: 0x0001BE30
		internal bool IsTextBoxInTable
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
				return this.\u1715;
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
				this.\u1715 = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0001CE74 File Offset: 0x0001BE74
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0001CEB8 File Offset: 0x0001BEB8
		internal bool IsTextBox
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
				return this.ᜑ;
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
				this.ᜑ = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0001CEFC File Offset: 0x0001BEFC
		internal Section OwnerSection
		{
			get
			{
				DocumentObject owner;
				for (;;)
				{
					owner = base.Owner;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_9D;
						case 1:
							if (true)
							{
							}
							num = 3;
							continue;
						case 2:
							if (owner is Section)
							{
								num = 6;
								continue;
							}
							goto IL_DA;
						case 3:
							if (owner is Section)
							{
								num = 5;
								continue;
							}
							owner = owner.Owner;
							num = 7;
							continue;
						case 4:
							if (owner != null)
							{
								num = 1;
								continue;
							}
							goto IL_39;
						case 5:
							IL_76:
							goto IL_39;
						case 6:
							goto IL_54;
						case 7:
							goto IL_9D;
						}
						break;
						IL_39:
						num = 2;
						continue;
						IL_9D:
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
							break;
						}
					}
				}
				IL_54:
				return owner as Section;
				IL_DA:
				return null;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0001CFE4 File Offset: 0x0001BFE4
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0001D06C File Offset: 0x0001C06C
		internal XmlTableFormat DocxTableFormat
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜏ = new XmlTableFormat(this);
						goto IL_4C;
					case 2:
						goto IL_56;
					}
					if (this.ᜏ == null)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_56;
					IL_4C:
					num = 2;
					continue;
					IL_56:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						goto IL_6C;
					}
				}
				IL_6C:
				if (false)
				{
				}
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0001D0B0 File Offset: 0x0001C0B0
		internal XmlTableFormat TrackTblFormat
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_56;
					case 2:
						this.ᜐ = new XmlTableFormat(this);
						goto IL_4C;
					}
					if (this.ᜐ == null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_56;
					IL_4C:
					num = 1;
					continue;
					IL_56:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						goto IL_6C;
					}
				}
				IL_6C:
				if (false)
				{
				}
				return this.ᜐ;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0001D138 File Offset: 0x0001C138
		internal List<float> TrackTableGrid
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u1716 = new List<float>();
						goto IL_43;
					case 2:
						goto IL_4D;
					}
					if (this.\u1716 == null)
					{
						num = 0;
						continue;
					}
					goto IL_4D;
					IL_43:
					num = 2;
					continue;
					IL_4D:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_43;
					default:
						goto IL_6B;
					}
				}
				IL_6B:
				if (false)
				{
				}
				return this.\u1716;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0001D1BC File Offset: 0x0001C1BC
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0001D200 File Offset: 0x0001C200
		internal DocumentObject ClonedOwner
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
				return this.ᜣ;
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
				this.ᜣ = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001D244 File Offset: 0x0001C244
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0001D288 File Offset: 0x0001C288
		internal bool ApplyStyleForHeaderRow
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
				return this.\u1719;
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
				this.\u1719 = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0001D2CC File Offset: 0x0001C2CC
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0001D310 File Offset: 0x0001C310
		internal bool ApplyStyleForLastRow
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
				return this.\u171A;
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
				this.\u171A = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0001D354 File Offset: 0x0001C354
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0001D398 File Offset: 0x0001C398
		internal bool ApplyStyleForFirstColumn
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
				return this.\u171B;
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
				this.\u171B = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0001D3DC File Offset: 0x0001C3DC
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0001D420 File Offset: 0x0001C420
		internal bool ApplyStyleForLastColumn
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
				return this.\u171C;
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
				this.\u171C = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0001D464 File Offset: 0x0001C464
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0001D4A8 File Offset: 0x0001C4A8
		internal bool ApplyStyleForBandedRows
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
				return this.\u171D;
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
				this.\u171D = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001D4EC File Offset: 0x0001C4EC
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0001D530 File Offset: 0x0001C530
		internal bool ApplyStyleForBandedColumns
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
				return this.\u171E;
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
				this.\u171E = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0001D574 File Offset: 0x0001C574
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0001D5B8 File Offset: 0x0001C5B8
		public string Title
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0001D5FC File Offset: 0x0001C5FC
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0001D640 File Offset: 0x0001C640
		public string TableDescription
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
				return this.ᜡ;
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
				this.ᜡ = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0001D684 File Offset: 0x0001C684
		internal bool IsFrame
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if ((float)this.Rows[0].Cells[0].Paragraphs[0].Format.FrameXEx != this.TableFormat.Positioning.HorizPosition * 20f)
						{
							num = 6;
							continue;
						}
						return false;
					case 2:
						if (this.Rows[0].Cells[0].Paragraphs[0].Format.IsFrame)
						{
							num = 3;
							continue;
						}
						return false;
					case 3:
						num = 8;
						continue;
					case 4:
						num = 11;
						continue;
					case 5:
						num = 9;
						continue;
					case 6:
						return true;
					case 7:
						num = 2;
						continue;
					case 8:
						if ((float)this.Rows[0].Cells[0].Paragraphs[0].Format.FrameYEx == this.TableFormat.Positioning.VertPosition * 20f)
						{
							num = 10;
							continue;
						}
						return true;
					case 9:
						if (this.Rows[0].Cells.Count > 0)
						{
							num = 4;
							continue;
						}
						return false;
					case 10:
						num = 0;
						continue;
					case 11:
						if (true)
						{
						}
						if (this.Rows[0].Cells[0].Paragraphs.Count > 0)
						{
							num = 7;
							continue;
						}
						return false;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						if (this.Rows.Count <= 0)
						{
							return false;
						}
						num = 5;
						break;
					}
				}
				return true;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0001D8AC File Offset: 0x0001C8AC
		internal ParagraphFormat FrameFormat
		{
			get
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
					if (this.IsFrame)
					{
						return this.Rows[0].Cells[0].Paragraphs[0].Format;
					}
					break;
				}
				return null;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0001D91C File Offset: 0x0001C91C
		internal short FrameX
		{
			get
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
					if (this.IsFrame)
					{
						return this.FrameFormat.FrameX;
					}
					break;
				}
				return 0;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0001D970 File Offset: 0x0001C970
		internal short FrameY
		{
			get
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
					if (this.IsFrame)
					{
						if (true)
						{
						}
						return this.FrameFormat.FrameY;
					}
					break;
				}
				return 0;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001D9C4 File Offset: 0x0001C9C4
		public Table(IDocument doc) : this(doc, false)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001D9DC File Offset: 0x0001C9DC
		public Table(IDocument doc, bool showBorder)
		{
			this.ᜅ = float.MinValue;
			this.ᜎ = default(RectangleF);
			this.\u1714 = new ArrayList();
			this.\u1719 = true;
			this.\u171B = true;
			this.\u171D = true;
			base..ctor((Document)doc);
			this.ᜃ = new RowCollection(this);
			if (showBorder)
			{
				this.TableFormat.Borders.BorderType = BorderStyle.Single;
				this.TableFormat.Borders.Color = Color.Black;
				this.TableFormat.Borders.LineWidth = 0.5f;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001DA7C File Offset: 0x0001CA7C
		public Table(IDocument doc, bool showBorder, float lineWidth)
		{
			this.ᜅ = float.MinValue;
			this.ᜎ = default(RectangleF);
			this.\u1714 = new ArrayList();
			this.\u1719 = true;
			this.\u171B = true;
			this.\u171D = true;
			base..ctor((Document)doc);
			this.ᜃ = new RowCollection(this);
			if (showBorder)
			{
				this.TableFormat.Borders.BorderType = BorderStyle.Single;
				this.TableFormat.Borders.Color = Color.Black;
				this.TableFormat.Borders.LineWidth = lineWidth;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001DB18 File Offset: 0x0001CB18
		internal Table(bool A_0, IDocument A_1)
		{
			this.ᜅ = float.MinValue;
			this.ᜎ = default(RectangleF);
			this.\u1714 = new ArrayList();
			this.\u1719 = true;
			this.\u171B = true;
			this.\u171D = true;
			base..ctor((Document)A_1);
			this.ᜃ = new RowCollection(this);
			if (A_0)
			{
				this.TableFormat.Borders.BorderType = BorderStyle.Single;
				this.TableFormat.Borders.Color = Color.Black;
				this.TableFormat.Borders.LineWidth = 0.5f;
				return;
			}
			this.TableFormat.Borders.BorderType = BorderStyle.None;
			this.TableFormat.Borders.Color = Color.White;
			this.TableFormat.Borders.LineWidth = 0f;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001DBF0 File Offset: 0x0001CBF0
		public new Table Clone()
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
			return (Table)this.CloneImpl();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001DC38 File Offset: 0x0001CC38
		public void ResetCells(int rowsNum, int columnsNum)
		{
			int a_ = 2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8F;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_8D;
				case 2:
					if (columnsNum <= 0)
					{
						num = 1;
						continue;
					}
					goto IL_8F;
				case 3:
					num = 2;
					continue;
				}
				if (rowsNum <= 0)
				{
					break;
				}
				num = 3;
			}
			IL_65:
			throw new ArgumentException(ClipboardData.b("㱧୩๫ɭᕯ剱ᥳ͵୷๹屻ᙽꚅﺉ겋뢗ﮝ肟킡쮣톥袧쮩슫쪭邯\uddb1\udab3펵颷\ud9b9펻튽떿꿁꫃", a_));
			IL_8D:
			goto IL_65;
			IL_8F:
			this.ᜀ(rowsNum, columnsNum, null, null);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001DCE8 File Offset: 0x0001CCE8
		public void ResetCells(int rowsNum, int columnsNum, RowFormat format, float cellWidth)
		{
			int a_ = 3;
			int num = 3;
			float? a_2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DF;
				case 1:
				{
					Section section;
					if (section != null)
					{
						num = 6;
						continue;
					}
					num = 8;
					continue;
				}
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
					cellWidth = base.Document.LastSection.PageSetup.ClientWidth / (float)columnsNum;
					num = 5;
					continue;
				case 4:
				{
					if (columnsNum <= 0)
					{
						num = 0;
						continue;
					}
					a_2 = new float?(cellWidth);
					Section section = this.ᜁ();
					num = 1;
					continue;
				}
				case 5:
					goto IL_106;
				case 6:
				{
					Section section;
					cellWidth = section.PageSetup.ClientWidth / (float)columnsNum;
					num = 9;
					continue;
				}
				case 7:
					num = 4;
					continue;
				case 8:
					if (base.Document.LastSection != null)
					{
						num = 2;
						continue;
					}
					goto IL_139;
				case 9:
					goto IL_137;
				}
				if (rowsNum <= 0)
				{
					break;
				}
				num = 7;
			}
			IL_DF:
			goto IL_108;
			IL_106:
			goto IL_139;
			IL_108:
			throw new ArgumentException(ClipboardData.b("㵨੪ཬͮᑰ卲ᡴɶ੸ེ嵼᝾Ꞇﾊ권릘爵膠톢쪤킦覨쪪쎬쮮醰\udcb2\udbb4튶馸\ud8ba튼펾듀껂ꯄ", a_));
			IL_137:
			IL_139:
			if (true)
			{
			}
			this.ᜀ(rowsNum, columnsNum, format, a_2);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001DE40 File Offset: 0x0001CE40
		private new void ᜀ(int A_0, int A_1, RowFormat A_2, float? A_3)
		{
			int a_ = 4;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(A_1, A_3);
					A_0--;
					num = 4;
					continue;
				case 2:
					goto IL_18C;
				case 3:
					goto IL_A2;
				case 4:
					goto IL_168;
				case 5:
					goto IL_7D;
				case 6:
					if (A_3 != null)
					{
						num = 13;
						continue;
					}
					goto IL_7D;
				case 7:
					this.TableFormat.ClearFormatting();
					this.TableFormat.ImportContainer(A_2);
					num = 3;
					continue;
				case 8:
					if (A_2 != null)
					{
						num = 7;
						continue;
					}
					goto IL_A2;
				case 9:
					goto IL_1CC;
				case 10:
					goto IL_168;
				case 11:
					if (A_0 <= 0)
					{
						num = 2;
						continue;
					}
					this.AddRow();
					A_0--;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 12:
					goto IL_F8;
				case 13:
					this.ᜉ = new float?(A_3.Value);
					num = 5;
					continue;
				case 14:
					if (A_0 > 0)
					{
						num = 0;
						continue;
					}
					return;
				case 15:
					num = 17;
					continue;
				case 16:
					if (A_1 > 63)
					{
						num = 12;
						continue;
					}
					num = 8;
					continue;
				case 17:
					if (A_1 <= 0)
					{
						num = 9;
						continue;
					}
					num = 16;
					continue;
				}
				if (true)
				{
				}
				if (A_0 > 0)
				{
					num = 15;
					continue;
				}
				goto IL_132;
				IL_7D:
				this.ᜀ();
				num = 14;
				continue;
				IL_A2:
				this.ᜃ.Clear();
				this.ᜇ = new int?(A_1);
				num = 6;
				continue;
				IL_168:
				num = 11;
			}
			IL_F8:
			throw new ArgumentException(ClipboardData.b("❩ͫᱭᕯ剱sṵ᥷ᑹ屻䡽덿ꊁﾋ꺍望뚕뺝슟잡蒣향\udda7\udaa9\udcab솭슯욱톳튵隷", a_));
			IL_132:
			throw new ArgumentException(ClipboardData.b("㹩൫౭ᱯ᝱味᭵൷ॹࡻ幽ꢇ꺍ﲏ몙얟芡횣즥\udfa7誩춫삭풯銱\udbb3\ud8b5\uddb7骹\udfbb톽겿럁꧃ꣅ", a_));
			IL_18C:
			return;
			IL_1CC:
			goto IL_132;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001E078 File Offset: 0x0001D078
		private new TableRow ᜀ(int A_0, float? A_1)
		{
			switch (0)
			{
			default:
			{
				TableRow tableRow;
				for (;;)
				{
					tableRow = new TableRow(base.Document);
					int num = 12;
					for (;;)
					{
						TableCell tableCell;
						int num3;
						switch (num)
						{
						case 0:
						{
							IL_AD:
							float? num2;
							if (num2 != null)
							{
								num = 1;
								continue;
							}
							goto IL_133;
						}
						case 1:
						{
							float? num2;
							tableCell.Width = num2.Value;
							num = 18;
							continue;
						}
						case 2:
							goto IL_A1;
						case 3:
							tableRow.Height = this.\u170D.Value;
							tableRow.HeightType = TableRowHeightType.Exactly;
							num = 10;
							continue;
						case 4:
							num = 14;
							continue;
						case 5:
						{
							float? num2 = new float?(this.ᜉ.Value);
							num = 13;
							continue;
						}
						case 6:
						{
							float? num2 = new float?(A_1.Value);
							num = 7;
							continue;
						}
						case 7:
							goto IL_A1;
						case 8:
							if (A_1 != null)
							{
								num = 6;
								continue;
							}
							num = 20;
							continue;
						case 9:
						{
							if (num3 >= A_0)
							{
								num = 16;
								continue;
							}
							tableCell = new TableCell(base.Document);
							float? num2 = null;
							num = 8;
							continue;
						}
						case 10:
							goto IL_152;
						case 11:
						{
							float? num2 = new float?(this.ᜋ[num3]);
							if (true)
							{
							}
							num = 2;
							continue;
						}
						case 12:
							if (this.\u170D != null)
							{
								num = 3;
								continue;
							}
							goto IL_152;
						case 13:
							goto IL_A1;
						case 14:
							if (this.ᜋ.Length > num3)
							{
								num = 11;
								continue;
							}
							goto IL_22D;
						case 15:
							goto IL_188;
						case 16:
							goto IL_1C3;
						case 17:
							goto IL_188;
						case 18:
							goto IL_133;
						case 19:
							if (this.ᜉ != null)
							{
								num = 5;
								continue;
							}
							goto IL_A1;
						case 20:
							if (this.ᜋ != null)
							{
								num = 4;
								continue;
							}
							goto IL_22D;
						}
						break;
						IL_A1:
						num = 0;
						continue;
						IL_188:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AD;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						IL_133:
						tableRow.Cells.Add(tableCell);
						num3++;
						num = 15;
						continue;
						IL_152:
						num3 = 0;
						num = 17;
						continue;
						IL_22D:
						num = 19;
					}
				}
				IL_1C3:
				this.Rows.Add(tableRow);
				return tableRow;
			}
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001E33C File Offset: 0x0001D33C
		public void ApplyStyle(DefaultTableStyle builtinTableStyle)
		{
			int a_ = 3;
			IStyle style;
			for (;;)
			{
				this.ᜄ();
				string name = Style.ᜀ(builtinTableStyle);
				style = (base.Document.Styles.FindByName(name, StyleType.TableStyle) as spr\u2179);
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							style = (spr\u2179)Style.ᜀ(builtinTableStyle, base.Document);
							num = 3;
							continue;
						}
						break;
					case 1:
						goto IL_73;
					case 2:
						(style as spr\u173A).StyleId = 4094;
						num = 1;
						continue;
					case 3:
						if ((style as spr\u173A).StyleId > 10)
						{
							num = 2;
							continue;
						}
						goto IL_73;
					case 4:
						goto IL_100;
					case 5:
						if (style == null)
						{
							num = 0;
							continue;
						}
						goto IL_182;
					}
					break;
					IL_73:
					base.Document.Styles.Add(style);
					string text = style.Name.Replace(ClipboardData.b("⡨ࡪ๬੮ὰݲ", a_), ClipboardData.b("䑨⩪๬౮ᑰᵲŴ", a_));
					base.Document.StyleNameIds.Add(text.Replace(ClipboardData.b("䥨", a_), ""), style.Name);
					(style as spr\u173A).ApplyBaseStyle(ClipboardData.b("❨ѪὬɮၰὲ啴⍶ᡸ᥺ᅼ᩾", a_));
					num = 4;
				}
			}
			IL_100:
			if (true)
			{
			}
			IL_182:
			this.ᜀ(style as spr\u2179);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001E4D8 File Offset: 0x0001D4D8
		public TableRow AddRow()
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
			return this.AddRow(true, true);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001E51C File Offset: 0x0001D51C
		public TableRow AddRow(int columnsNum)
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
			return this.ᜀ(true, true, new int?(columnsNum));
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001E568 File Offset: 0x0001D568
		public TableRow AddRow(bool isCopyFormat)
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
			return this.AddRow(isCopyFormat, true);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001E5AC File Offset: 0x0001D5AC
		public TableRow AddRow(bool isCopyFormat, bool autoPopulateCells)
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
			return this.ᜀ(isCopyFormat, autoPopulateCells, null);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001E5F8 File Offset: 0x0001D5F8
		public TableRow AddRow(bool isCopyFormat, int columnsNum)
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
			return this.ᜀ(isCopyFormat, true, new int?(columnsNum));
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001E644 File Offset: 0x0001D644
		private new TableRow ᜀ(bool A_0, bool A_1, int? A_2)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				TableRow tableRow;
				for (;;)
				{
					TableRow lastRow = this.LastRow;
					int num = 49;
					for (;;)
					{
						int num3;
						TableCell tableCell;
						float? num4;
						int num6;
						switch (num)
						{
						case 0:
						{
							float? num2 = new float?(this.ᜋ[num3]);
							num = 15;
							continue;
						}
						case 1:
						{
							float? num2;
							tableCell.Width = num2.Value;
							num = 4;
							continue;
						}
						case 2:
							if (A_2.Value > 63)
							{
								num = 53;
								continue;
							}
							num = 14;
							continue;
						case 3:
							goto IL_308;
						case 4:
							goto IL_205;
						case 5:
							if (num4 != null)
							{
								num = 26;
								continue;
							}
							goto IL_70F;
						case 6:
							goto IL_49D;
						case 7:
						{
							if (num3 >= A_2.Value)
							{
								num = 21;
								continue;
							}
							tableCell = new TableCell(base.Document);
							float? num2 = null;
							num = 47;
							continue;
						}
						case 8:
							num = 28;
							continue;
						case 9:
							num4 = new float?(this.\u170D.Value);
							num = 16;
							continue;
						case 10:
							if (lastRow != null)
							{
								num = 37;
								continue;
							}
							tableRow.RowFormat.ImportContainer(this.TableFormat);
							num = 30;
							continue;
						case 11:
							num = 13;
							continue;
						case 12:
							goto IL_42A;
						case 13:
						{
							int num5;
							if (A_2.Value > num5)
							{
								num = 31;
								continue;
							}
							goto IL_3ED;
						}
						case 14:
							if (A_2.Value < 0)
							{
								num = 48;
								continue;
							}
							goto IL_49D;
						case 15:
							goto IL_6BB;
						case 16:
							goto IL_668;
						case 17:
						{
							int num5;
							if (num6 >= num5)
							{
								num = 11;
								continue;
							}
							TableCell tableCell2 = lastRow.Cells[num6];
							TableCell tableCell3 = new TableCell(base.Document);
							tableRow.Cells.Add(tableCell3);
							tableCell3.Width = tableCell2.Width;
							num = 44;
							continue;
						}
						case 18:
							goto IL_42A;
						case 19:
							goto IL_6BB;
						case 20:
							goto IL_418;
						case 21:
							goto IL_3ED;
						case 22:
							num = 10;
							continue;
						case 23:
							num = 2;
							continue;
						case 24:
							if (A_1)
							{
								num = 35;
								continue;
							}
							goto IL_3ED;
						case 25:
							tableRow = this.ᜀ(A_2.Value, null);
							num = 55;
							continue;
						case 26:
							tableRow.Height = num4.Value;
							tableRow.HeightType = TableRowHeightType.Exactly;
							num = 3;
							continue;
						case 27:
							goto IL_6E4;
						case 28:
							if (this.\u170D != null)
							{
								num = 9;
								continue;
							}
							goto IL_668;
						case 29:
							goto IL_6E4;
						case 30:
							goto IL_2A3;
						case 31:
						{
							int num5;
							num3 = num5;
							num = 29;
							continue;
						}
						case 32:
							if (this.ᜋ.Length > num3)
							{
								num = 0;
								continue;
							}
							goto IL_589;
						case 33:
							if (lastRow == null)
							{
								num = 25;
								continue;
							}
							tableRow = new TableRow(base.Document);
							num = 24;
							continue;
						case 34:
							IL_1D3:
							goto IL_2A3;
						case 35:
						{
							int num5 = Math.Min(A_2.Value, lastRow.Cells.Count);
							num6 = 0;
							num = 18;
							continue;
						}
						case 36:
							if (this.ᜇ != null)
							{
								num = 42;
								continue;
							}
							num = 43;
							continue;
						case 37:
							tableRow.RowFormat.ImportContainer(lastRow.RowFormat);
							num4 = new float?(lastRow.Height);
							num = 34;
							continue;
						case 38:
							if (A_0)
							{
								num = 22;
								continue;
							}
							goto IL_2A3;
						case 39:
							A_2 = new int?(lastRow.Cells.Count);
							num = 6;
							continue;
						case 40:
							goto IL_49D;
						case 41:
							if (this.ᜉ != null)
							{
								num = 50;
								continue;
							}
							goto IL_6BB;
						case 42:
							A_2 = new int?(this.ᜇ.Value);
							num = 40;
							continue;
						case 43:
							if (lastRow != null)
							{
								num = 39;
								continue;
							}
							A_2 = new int?(0);
							num = 54;
							continue;
						case 44:
							if (A_0)
							{
								num = 45;
								continue;
							}
							goto IL_418;
						case 45:
						{
							TableCell tableCell2;
							TableCell tableCell3;
							tableCell3.CellFormat.ImportContainer(tableCell2.CellFormat);
							num = 20;
							continue;
						}
						case 46:
							num = 32;
							continue;
						case 47:
							if (this.ᜋ != null)
							{
								num = 46;
								continue;
							}
							goto IL_589;
						case 48:
							goto IL_6B6;
						case 49:
							if (true)
							{
							}
							if (A_2 != null)
							{
								num = 23;
								continue;
							}
							num = 36;
							continue;
						case 50:
						{
							float? num2 = new float?(this.ᜉ.Value);
							num = 19;
							continue;
						}
						case 51:
						{
							float? num2;
							if (num2 != null)
							{
								num = 1;
								continue;
							}
							goto IL_205;
						}
						case 52:
							if (num4 == null)
							{
								num = 8;
								continue;
							}
							goto IL_668;
						case 53:
							goto IL_357;
						case 54:
							goto IL_49D;
						case 55:
							goto IL_3C6;
						}
						break;
						IL_205:
						tableRow.Cells.Add(tableCell);
						num3++;
						num = 27;
						continue;
						IL_2A3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1D3;
						default:
							if (false)
							{
							}
							num = 52;
							continue;
						}
						IL_3ED:
						num4 = null;
						num = 38;
						continue;
						IL_418:
						num6++;
						num = 12;
						continue;
						IL_42A:
						num = 17;
						continue;
						IL_49D:
						tableRow = null;
						num = 33;
						continue;
						IL_589:
						num = 41;
						continue;
						IL_668:
						num = 5;
						continue;
						IL_6BB:
						num = 51;
						continue;
						IL_6E4:
						num = 7;
					}
				}
				IL_308:
				goto IL_70F;
				IL_357:
				throw new ArgumentOutOfRangeException(ClipboardData.b("ᝳ᥵ᑷཹᅻၽ첁", a_), ClipboardData.b("⁳᝵᩷ᙹ᥻幽ꚅ꺍뺝춟춡횣쎥袧\udea9쒫쾭\udeaf銱芳薵颷\ud9b9펻튽떿꿁꫃뗅", a_));
				IL_3C6:
				goto IL_70F;
				IL_6B6:
				throw new ArgumentOutOfRangeException(ClipboardData.b("ᝳ᥵ᑷཹᅻၽ첁", a_), ClipboardData.b("⁳ṵᵷ婹ቻ୽ꢇ꺍﶑몙肟횡첣쎥袧\ud8a9쎫\ud9ad邯톱햳\ud8b5颷풹펻쪽ꃁꇃ꓇꿉뿋뷍", a_));
				IL_70F:
				this.Rows.Add(tableRow);
				return tableRow;
			}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001ED70 File Offset: 0x0001DD70
		public override int Replace(Regex pattern, string replace)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 0;
				IEnumerator enumerator = this.Rows.GetEnumerator();
				int result;
				try
				{
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_293;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 2;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num2 = 4;
							continue;
						}
						case 2:
							goto IL_287;
						case 4:
							try
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_239;
									case 1:
										try
										{
											num2 = 6;
											for (;;)
											{
												switch (num2)
												{
												case 0:
													goto IL_1DF;
												case 1:
													goto IL_1AB;
												case 2:
												{
													IEnumerator enumerator3;
													if (!enumerator3.MoveNext())
													{
														num2 = 3;
														continue;
													}
													BodyRegion bodyRegion = (BodyRegion)enumerator3.Current;
													num += bodyRegion.Replace(pattern, replace);
													num2 = 4;
													continue;
												}
												case 3:
													num2 = 0;
													continue;
												case 4:
													if (base.Document.ReplaceFirst)
													{
														num2 = 5;
														continue;
													}
													break;
												case 5:
													num2 = 7;
													continue;
												case 7:
													if (num > 0)
													{
														num2 = 8;
														continue;
													}
													break;
												case 8:
													result = num;
													num2 = 1;
													continue;
												}
												IL_1B0:
												num2 = 2;
												continue;
												goto IL_1B0;
											}
											IL_1AB:
											return result;
											IL_1DF:
											break;
										}
										finally
										{
											for (;;)
											{
												IEnumerator enumerator3;
												IDisposable disposable = enumerator3 as IDisposable;
												num2 = 1;
												for (;;)
												{
													switch (num2)
													{
													case 0:
														disposable.Dispose();
														num2 = 2;
														continue;
													case 1:
														if (disposable != null)
														{
															num2 = 0;
															continue;
														}
														goto IL_22C;
													case 2:
														goto IL_22A;
													}
													break;
												}
											}
											IL_22A:
											IL_22C:;
										}
										goto IL_22D;
									case 3:
										goto IL_22D;
									case 4:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 3;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										IEnumerator enumerator3 = tableCell.ChildObjects.GetEnumerator();
										num2 = 1;
										continue;
									}
									}
									IL_F3:
									num2 = 4;
									continue;
									goto IL_F3;
									IL_22D:
									num2 = 0;
								}
								IL_239:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable2 = enumerator2 as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											disposable2.Dispose();
											num2 = 1;
											continue;
										case 1:
											goto IL_284;
										case 2:
											if (disposable2 != null)
											{
												num2 = 0;
												continue;
											}
											goto IL_286;
										}
										break;
									}
								}
								IL_284:
								IL_286:;
							}
							goto IL_287;
						}
						IL_5E:
						num2 = 1;
						continue;
						goto IL_5E;
						IL_287:
						num2 = 0;
					}
					IL_293:
					return num;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable3 = enumerator as IDisposable;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_2FA;
							case 1:
								goto IL_2BF;
							case 2:
								disposable3.Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_2BF;
								default:
									if (false)
									{
									}
									num2 = 0;
									continue;
								}
								break;
							}
							break;
							IL_2BF:
							if (disposable3 == null)
							{
								goto IL_2FC;
							}
							num2 = 2;
						}
					}
					IL_2FA:
					IL_2FC:;
				}
				return result;
			}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001F0C8 File Offset: 0x0001E0C8
		public override int Replace(string given, string replace, bool caseSensitive, bool wholeWord)
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
			Regex pattern = spr\u1AB5.ᜀ(given, caseSensitive, wholeWord);
			return this.Replace(pattern, replace);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001F118 File Offset: 0x0001E118
		public override int Replace(Regex pattern, TextSelection textSelection)
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
			return this.Replace(pattern, textSelection, false);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001F15C File Offset: 0x0001E15C
		public override int Replace(Regex pattern, TextSelection textSelection, bool saveFormatting)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				textSelection.ᜂ();
				int num = 0;
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				int result;
				try
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 2;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num2 = 4;
							continue;
						}
						case 2:
							goto IL_1DF;
						case 3:
							goto IL_1EB;
						case 4:
							try
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										num2 = 7;
										continue;
									case 1:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 0;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										num += tableCell.ᜀ(pattern, textSelection, saveFormatting);
										num2 = 4;
										continue;
									}
									case 3:
										num2 = 6;
										continue;
									case 4:
										if (base.Document.ReplaceFirst)
										{
											num2 = 3;
											continue;
										}
										break;
									case 5:
										result = num;
										num2 = 8;
										continue;
									case 6:
										if (num > 0)
										{
											num2 = 5;
											continue;
										}
										break;
									case 7:
										goto IL_175;
									case 8:
										goto IL_E9;
									}
									IL_128:
									num2 = 1;
									continue;
									goto IL_128;
								}
								IL_E9:
								return result;
								IL_175:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											disposable.Dispose();
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_1A1;
											default:
												if (false)
												{
												}
												num2 = 1;
												continue;
											}
											break;
										case 1:
											goto IL_1DC;
										case 2:
											goto IL_1A1;
										}
										break;
										IL_1A1:
										if (disposable == null)
										{
											goto IL_1DE;
										}
										num2 = 0;
									}
								}
								IL_1DC:
								IL_1DE:;
							}
							goto IL_1DF;
						}
						IL_61:
						num2 = 1;
						continue;
						goto IL_61;
						IL_1DF:
						num2 = 3;
					}
					IL_1EB:
					return num;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_236;
							case 1:
								if (disposable2 != null)
								{
									num2 = 2;
									continue;
								}
								goto IL_238;
							case 2:
								disposable2.Dispose();
								num2 = 0;
								continue;
							}
							break;
						}
					}
					IL_236:
					IL_238:;
				}
				return result;
			}
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001F3D8 File Offset: 0x0001E3D8
		public override TextSelection Find(Regex pattern)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				TextSelection result;
				try
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_1C5;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 3;
							continue;
						}
						case 3:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 6;
										continue;
									case 1:
										goto IL_CF;
									case 2:
									{
										TextSelection textSelection;
										if (textSelection != null)
										{
											num = 0;
											continue;
										}
										break;
									}
									case 4:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 5;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										TextSelection textSelection = tableCell.ᜀ(pattern);
										num = 2;
										continue;
									}
									case 5:
										num = 7;
										continue;
									case 6:
									{
										TextSelection textSelection;
										if (textSelection.Count > 0)
										{
											num = 8;
											continue;
										}
										break;
									}
									case 7:
										goto IL_14F;
									case 8:
									{
										TextSelection textSelection;
										result = textSelection;
										num = 1;
										continue;
									}
									}
									IL_100:
									num = 4;
									continue;
									goto IL_100;
								}
								IL_CF:
								goto IL_213;
								IL_14F:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable.Dispose();
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_17B;
											default:
												if (false)
												{
												}
												num = 1;
												continue;
											}
											break;
										case 1:
											goto IL_1B6;
										case 2:
											goto IL_17B;
										}
										break;
										IL_17B:
										if (disposable == null)
										{
											goto IL_1B8;
										}
										num = 0;
									}
								}
								IL_1B6:
								IL_1B8:;
							}
							goto IL_1B9;
						case 4:
							goto IL_1B9;
						}
						IL_47:
						num = 2;
						continue;
						goto IL_47;
						IL_1B9:
						num = 1;
					}
					IL_1C5:
					goto IL_1D;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable2 != null)
								{
									num = 2;
									continue;
								}
								goto IL_212;
							case 1:
								goto IL_210;
							case 2:
								disposable2.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_210:
					IL_212:;
				}
				goto IL_213;
				IL_1D:
				return null;
				IL_213:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001F640 File Offset: 0x0001E640
		public void ApplyVerticalMerge(int columnIndex, int startRowIndex, int endRowIndex)
		{
			int a_ = 9;
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
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2;
						if (columnIndex >= this.ᜃ[num2].Cells.Count)
						{
							num = 24;
							continue;
						}
						num2++;
						num = 16;
						continue;
					}
					case 1:
						goto IL_304;
					case 2:
					{
						if (columnIndex < 0)
						{
							num = 8;
							continue;
						}
						int num2 = startRowIndex;
						num = 22;
						continue;
					}
					case 3:
						goto IL_2B5;
					case 4:
						if (endRowIndex >= this.ᜃ.Count)
						{
							num = 9;
							continue;
						}
						num = 18;
						continue;
					case 5:
						if (this.ᜃ.Count == 0)
						{
							num = 3;
							continue;
						}
						num = 17;
						continue;
					case 6:
						if (endRowIndex >= 0)
						{
							num = 15;
							continue;
						}
						goto IL_26D;
					case 7:
						return;
					case 8:
						goto IL_FF;
					case 9:
						goto IL_353;
					case 10:
						if (startRowIndex >= this.ᜃ.Count)
						{
							num = 14;
							continue;
						}
						num = 6;
						continue;
					case 12:
						goto IL_24B;
					case 13:
						goto IL_24B;
					case 14:
						goto IL_12B;
					case 15:
						num = 4;
						continue;
					case 16:
						goto IL_130;
					case 17:
						if (startRowIndex >= 0)
						{
							num = 19;
							continue;
						}
						goto IL_1F6;
					case 18:
						if (startRowIndex > endRowIndex)
						{
							num = 1;
							continue;
						}
						num = 2;
						continue;
					case 19:
						num = 10;
						continue;
					case 20:
					{
						int num2;
						if (num2 > endRowIndex)
						{
							num = 21;
							continue;
						}
						num = 0;
						continue;
					}
					case 21:
					{
						this.ᜃ[startRowIndex].Cells[columnIndex].CellFormat.VerticalMerge = CellMerge.Start;
						int num3 = startRowIndex + 1;
						num = 12;
						continue;
					}
					case 22:
						goto IL_130;
					case 23:
					{
						int num3;
						if (num3 > endRowIndex)
						{
							num = 7;
							continue;
						}
						this.ᜃ[num3].Cells[columnIndex].CellFormat.VerticalMerge = CellMerge.Continue;
						num3++;
						num = 13;
						continue;
					}
					case 24:
						goto IL_1A0;
					case 25:
						num = 5;
						continue;
					}
					if (true)
					{
					}
					if (this.ᜃ != null)
					{
						num = 25;
						continue;
					}
					goto IL_2D3;
					IL_130:
					num = 20;
					continue;
					IL_24B:
					num = 23;
				}
				IL_FF:
				throw new ArgumentOutOfRangeException(ClipboardData.b("౮Ṱὲt᩶᝸㉺፼᭾ﮂ", a_), ClipboardData.b("ⱮṰὲt᩶᝸孺੼ᙾꖄ麗朗릘풠캢쮤螦삨얪즬쪮즰鎲톴\ud8b6\udcb8좺펼颾뗀ꃄ뿆ꃈ룊만", a_));
				IL_1A0:
				throw new ArgumentOutOfRangeException(ClipboardData.b("౮Ṱὲt᩶᝸㉺፼᭾ﮂ", a_), ClipboardData.b("ⱮṰὲt᩶᝸孺੼ᙾꖄ麗朗릘풠캢쮤螦삨얪즬쪮즰鎲톴\ud8b6\udcb8좺펼颾뗀ꃄ뿆ꃈ룊만", a_));
				IL_26D:
				throw new ArgumentOutOfRangeException(ClipboardData.b("੮ὰᝲ❴ᡶ๸㉺፼᭾ﮂ", a_), ClipboardData.b("㵮ṰѲ啴vၸེᕼ彾뎒뮚얠욢\udda4螦춨쒪좬\udcae\udfb0钲솴鞶\udcb8쎺풼첾뗀", a_));
				IL_2B5:
				IL_2D3:
				throw new Exception(ClipboardData.b("㭮ၰᅲᥴቶ奸ॺቼࡾꎂꮊ뎒ﲔ練ﺞ춠쪢\udfa4슦춨薪", a_));
				IL_304:
				throw new Exception(ClipboardData.b("㱮հቲݴͶ奸ॺቼࡾꆀ권뎒ﲘ漢爵펠莢톤쾦좨얪趬쪮\udfb0ힲ閴얶횸첺鶼횾꿀Ꟃꃄ뿆", a_));
				IL_353:
				goto IL_26D;
			}
			}
			IL_12B:
			IL_1F6:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᱮհቲݴͶ⭸ᑺ੼㙾ﾆ", a_), ClipboardData.b("㵮ṰѲ啴vၸེᕼ彾뎒뮚얠욢\udda4螦춨쒪좬\udcae\udfb0钲솴鞶\udcb8쎺풼첾뗀", a_));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001F9C8 File Offset: 0x0001E9C8
		public void ApplyHorizontalMerge(int rowIndex, int startCellIndex, int endCellIndex)
		{
			int a_ = 11;
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_262;
					case 1:
					{
						CellCollection cells;
						if (startCellIndex > cells.Count - 1)
						{
							num = 10;
							continue;
						}
						num = 19;
						continue;
					}
					case 3:
						num = 4;
						continue;
					case 4:
						if (this.ᜃ.Count == 0)
						{
							num = 13;
							continue;
						}
						num = 15;
						continue;
					case 5:
						if (startCellIndex >= 0)
						{
							num = 11;
							continue;
						}
						goto IL_DA;
					case 6:
						num = 8;
						continue;
					case 7:
						goto IL_21F;
					case 8:
					{
						CellCollection cells;
						if (cells.Count == 0)
						{
							num = 0;
							continue;
						}
						if (true)
						{
						}
						num = 5;
						continue;
					}
					case 9:
						num = 18;
						continue;
					case 10:
						goto IL_1D5;
					case 11:
						num = 1;
						continue;
					case 12:
						goto IL_123;
					case 13:
						goto IL_28A;
					case 14:
						return;
					case 15:
						if (rowIndex >= 0)
						{
							num = 25;
							continue;
						}
						goto IL_1DA;
					case 16:
					{
						CellCollection cells;
						if (cells != null)
						{
							num = 6;
							continue;
						}
						goto IL_2CD;
					}
					case 17:
					{
						if (rowIndex >= this.ᜃ.Count)
						{
							num = 12;
							continue;
						}
						CellCollection cells = this.ᜃ[rowIndex].Cells;
						num = 16;
						continue;
					}
					case 18:
					{
						CellCollection cells;
						if (endCellIndex > cells.Count - 1)
						{
							num = 22;
							continue;
						}
						num = 20;
						continue;
					}
					case 19:
						if (endCellIndex >= 0)
						{
							num = 9;
							continue;
						}
						goto IL_B8;
					case 20:
					{
						if (startCellIndex > endCellIndex)
						{
							num = 21;
							continue;
						}
						CellCollection cells;
						cells[startCellIndex].CellFormat.HorizontalMerge = CellMerge.Start;
						int num2 = startCellIndex + 1;
						num = 23;
						continue;
					}
					case 21:
						goto IL_2FE;
					case 22:
						goto IL_189;
					case 23:
						goto IL_21F;
					case 24:
					{
						int num2;
						if (num2 > endCellIndex)
						{
							num = 14;
							continue;
						}
						CellCollection cells;
						cells[num2].CellFormat.HorizontalMerge = CellMerge.Continue;
						num2++;
						num = 7;
						continue;
					}
					case 25:
						num = 17;
						continue;
					}
					if (this.ᜃ != null)
					{
						num = 3;
						continue;
					}
					goto IL_2B9;
					IL_21F:
					num = 24;
				}
				IL_B8:
				throw new ArgumentOutOfRangeException(ClipboardData.b("ᑰᵲᅴ㑶ᱸ᝺ᅼ㙾ﾆ", a_), ClipboardData.b("㉰ᙲᥴ᭶奸౺ᑼ୾ꎂ랖ﲘ列뾞슠욢즤쮦覨슪쎬쮮풰쮲閴펶횸\udeba캼톾럂ꋆ뇈ꋊ뻌믎", a_));
				IL_DA:
				throw new ArgumentOutOfRangeException(ClipboardData.b("ɰݲᑴն൸㡺᡼፾쪂", a_), ClipboardData.b("㉰ᙲᥴ᭶奸౺ᑼ୾ꎂ랖ﲜ햠莢욤슦얨잪趬욮\udfb0ힲ킴쾶馸\udfba튼\udabe닀귂돆껊뗌ꛎꋐ꟒", a_));
				IL_123:
				break;
				IL_189:
				goto IL_B8;
				IL_1D5:
				goto IL_DA;
				IL_262:
				goto IL_2CD;
				IL_28A:
				IL_2B9:
				throw new Exception(ClipboardData.b("╰ቲ᝴᭶ᱸ孺ོၾꖄﮈ권ﺐ떔ﺖ삠쾢첤\udda6첨쾪莬", a_));
				IL_2CD:
				throw new Exception(ClipboardData.b("╰ቲ᝴᭶ᱸ孺ོၾꎂﺌ꾎랖뾞좠춢첤펦삨쪪솬욮쮰횲톴馶", a_));
				IL_2FE:
				throw new Exception(ClipboardData.b("≰ݲᑴն൸孺Ṽ᩾ꖄ놐朗랖ﺘﺞ햠욢힤螦\udda8쎪첬솮醰횲\udbb4펶馸\ud8ba\ud8bc펾귀계꧆귈껊뗌", a_));
			}
			}
			IL_1DA:
			throw new ArgumentOutOfRangeException(ClipboardData.b("Ͱᱲɴ㹶᝸ὺ᡼ݾ", a_), ClipboardData.b("⍰ᱲɴ坶๸ቺॼ᝾ꆀ떔붜쾠잢삤\udfa6覨쾪슬쪮슰\uddb2銴쎶馸\udeba얼횾닀럂", a_));
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001FD24 File Offset: 0x0001ED24
		public void RemoveAbsPosition()
		{
			switch (0)
			{
			default:
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
					IEnumerator enumerator = this.ᜃ.GetEnumerator();
					try
					{
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 6;
								continue;
							case 1:
							{
								TableRow tableRow;
								tableRow.RowFormat.RemovePositioning();
								num = 4;
								continue;
							}
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								TableRow tableRow = (TableRow)enumerator.Current;
								IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
								num = 5;
								continue;
							}
							case 3:
							{
								TableRow tableRow;
								if (tableRow.RowFormat != null)
								{
									num = 1;
									continue;
								}
								break;
							}
							case 5:
								try
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											try
											{
												num = 3;
												for (;;)
												{
													switch (num)
													{
													case 0:
													{
														IEnumerator enumerator3;
														if (!enumerator3.MoveNext())
														{
															num = 8;
															continue;
														}
														BodyRegion bodyRegion = (BodyRegion)enumerator3.Current;
														num = 9;
														continue;
													}
													case 1:
													{
														BodyRegion bodyRegion;
														(bodyRegion as Paragraph).RemoveAbsPosition();
														num = 2;
														continue;
													}
													case 4:
														goto IL_1D3;
													case 6:
													{
														BodyRegion bodyRegion;
														if (bodyRegion is Table)
														{
															num = 7;
															continue;
														}
														break;
													}
													case 7:
													{
														BodyRegion bodyRegion;
														(bodyRegion as Table).RemoveAbsPosition();
														num = 5;
														continue;
													}
													case 8:
														num = 4;
														continue;
													case 9:
													{
														BodyRegion bodyRegion;
														if (bodyRegion is Paragraph)
														{
															num = 1;
															continue;
														}
														num = 6;
														continue;
													}
													}
													IL_1A1:
													num = 0;
													continue;
													goto IL_1A1;
												}
												IL_1D3:
												break;
											}
											finally
											{
												for (;;)
												{
													IEnumerator enumerator3;
													IDisposable disposable = enumerator3 as IDisposable;
													num = 1;
													for (;;)
													{
														switch (num)
														{
														case 0:
															goto IL_21E;
														case 1:
															if (disposable != null)
															{
																num = 2;
																continue;
															}
															goto IL_220;
														case 2:
															disposable.Dispose();
															num = 0;
															continue;
														}
														break;
													}
												}
												IL_21E:
												IL_220:;
											}
											goto IL_221;
										case 1:
											goto IL_221;
										case 3:
										{
											IEnumerator enumerator2;
											if (!enumerator2.MoveNext())
											{
												num = 1;
												continue;
											}
											TableCell tableCell = (TableCell)enumerator2.Current;
											IEnumerator enumerator3 = tableCell.Items.GetEnumerator();
											num = 0;
											continue;
										}
										case 4:
											goto IL_22D;
										}
										IL_CE:
										num = 3;
										continue;
										goto IL_CE;
										IL_221:
										num = 4;
									}
									IL_22D:
									goto IL_2E3;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator2;
										IDisposable disposable2 = enumerator2 as IDisposable;
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												disposable2.Dispose();
												num = 2;
												continue;
											case 1:
												if (disposable2 != null)
												{
													num = 0;
													continue;
												}
												goto IL_27A;
											case 2:
												goto IL_278;
											}
											break;
										}
									}
									IL_278:
									IL_27A:;
								}
								break;
								IL_2E3:
								num = 3;
								continue;
							case 6:
								goto IL_311;
							}
							IL_27B:
							num = 2;
							continue;
							goto IL_27B;
						}
						IL_311:;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable3 = enumerator as IDisposable;
							int num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_358;
								case 1:
									disposable3.Dispose();
									num = 0;
									continue;
								case 2:
									if (disposable3 != null)
									{
										num = 1;
										continue;
									}
									goto IL_35A;
								}
								break;
							}
						}
						IL_358:
						IL_35A:;
					}
					break;
				}
				}
				return;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000200D8 File Offset: 0x0001F0D8
		internal string ᜐ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				string text = string.Empty;
				IEnumerator enumerator = this.Rows.GetEnumerator();
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							try
							{
								num = 14;
								for (;;)
								{
									int num2;
									switch (num)
									{
									case 0:
										num = 10;
										continue;
									case 1:
										goto IL_11F;
									case 2:
									{
										TableCell tableCell;
										if (num2 >= tableCell.Items.Count)
										{
											num = 21;
											continue;
										}
										num = 22;
										continue;
									}
									case 3:
										if (base.Document.ᜅ != null)
										{
											num = 0;
											continue;
										}
										goto IL_350;
									case 4:
										goto IL_11F;
									case 5:
									{
										TableCell tableCell;
										text += (tableCell.Items[num2] as Table).ᜐ();
										num = 8;
										continue;
									}
									case 6:
										num = 23;
										continue;
									case 7:
										goto IL_38C;
									case 8:
										goto IL_1DE;
									case 9:
									{
										TableCell tableCell;
										if (tableCell.Items[num2] is Table)
										{
											num = 5;
											continue;
										}
										goto IL_1DE;
									}
									case 10:
									{
										TableCell tableCell;
										if (base.Document.ᜅ.OwnerTextBody == tableCell)
										{
											num = 13;
											continue;
										}
										goto IL_350;
									}
									case 11:
										num = 7;
										continue;
									case 12:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 11;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										num2 = 0;
										num = 4;
										continue;
									}
									case 13:
										num2 = base.Document.ᜅ.ឯ();
										base.Document.ᜅ = null;
										num = 16;
										continue;
									case 15:
										text = text.Substring(0, text.Length - 1);
										text += spr\u20E8.\u1714;
										num = 19;
										continue;
									case 16:
										goto IL_350;
									case 17:
										goto IL_1DE;
									case 18:
									{
										TableCell tableCell;
										if (num2 == tableCell.Items.Count - 1)
										{
											num = 6;
											continue;
										}
										goto IL_1A0;
									}
									case 19:
										goto IL_1A0;
									case 20:
									{
										TableCell tableCell;
										text += (tableCell.Items[num2] as Paragraph).ᜈ();
										num = 17;
										continue;
									}
									case 22:
									{
										TableCell tableCell;
										if (tableCell.Items[num2] is Paragraph)
										{
											num = 20;
											continue;
										}
										num = 9;
										continue;
									}
									case 23:
									{
										TableCell tableCell;
										TableRow tableRow;
										if (tableCell.CellFormat.CurCellIndex < tableRow.Cells.Count - 1)
										{
											num = 15;
											continue;
										}
										goto IL_1A0;
									}
									}
									goto IL_11A;
									IL_11F:
									num = 2;
									continue;
									IL_1A0:
									num2++;
									num = 1;
									continue;
									IL_1B5:
									num = 12;
									continue;
									IL_11A:
									goto IL_1B5;
									IL_1DE:
									num = 3;
									continue;
									IL_350:
									num = 18;
								}
								IL_38C:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_3D9;
										case 1:
											disposable.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_3D7;
										}
										break;
									}
								}
								IL_3D7:
								IL_3D9:;
							}
							goto IL_3DA;
						case 1:
							goto IL_3E6;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 0;
							continue;
						}
						case 3:
							goto IL_3DA;
						}
						IL_85:
						num = 2;
						continue;
						goto IL_85;
						IL_3DA:
						num = 1;
					}
					IL_3E6:;
				}
				finally
				{
					for (;;)
					{
						for (;;)
						{
							IDisposable disposable2 = enumerator as IDisposable;
							int num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_42E;
								case 1:
									if (disposable2 != null)
									{
										num = 2;
										continue;
									}
									goto IL_430;
								case 2:
									disposable2.Dispose();
									num = 0;
									continue;
								}
								break;
							}
						}
						IL_430:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_446;
						}
						IL_42E:
						goto IL_430;
					}
					IL_446:
					if (false)
					{
					}
				}
				return text;
			}
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00020568 File Offset: 0x0001F568
		private void ᜄ()
		{
			int a_ = 7;
			for (;;)
			{
				spr\u173A spr_u173A = base.Document.Styles.FindByName(ClipboardData.b("⍬nͰṲᑴ᭶奸⽺ᱼᵾ", a_), StyleType.TableStyle) as spr\u173A;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4A;
						default:
							if (false)
							{
							}
							spr_u173A = (spr\u173A)Style.ᜀ(DefaultTableStyle.TableNormal, base.Document);
							base.Document.Styles.Add(spr_u173A);
							base.Document.StyleNameIds.Add(ClipboardData.b("㥬๮፰ὲၴ㥶ᙸॺၼṾ", a_), spr_u173A.Name);
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_4A;
					}
					break;
					IL_4A:
					if (spr_u173A != null)
					{
						return;
					}
					num = 1;
				}
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00020654 File Offset: 0x0001F654
		private spr\u2179 ᜃ()
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
			return this.\u1718;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00020698 File Offset: 0x0001F698
		private new void ᜀ(spr\u2179 A_0)
		{
			int a_ = 8;
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
					if (true)
					{
					}
					throw new ArgumentNullException(ClipboardData.b("mᕯձ❳ɵŷᙹ᥻", a_));
				}
			}
			this.\u1718 = A_0;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000206FC File Offset: 0x0001F6FC
		internal new void ᜀ(string A_0)
		{
			int a_ = 9;
			spr\u2179 spr_u = base.Document.Styles.FindByName(A_0, StyleType.TableStyle) as spr\u2179;
			if (spr_u == null)
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
					throw new ArgumentNullException(ClipboardData.b("ŮᑰѲ♴Ͷx᝺᡼", a_));
				}
			}
			this.ᜀ(spr_u);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00020778 File Offset: 0x0001F778
		internal void ᜊ()
		{
			switch (0)
			{
			default:
			{
				int num = 33;
				for (;;)
				{
					sprῊ sprῊ;
					int num2;
					int num3;
					bool flag;
					bool flag2;
					Dictionary<ConditionalFormattingCode, sprῊ>.Enumerator enumerator;
					bool flag3;
					sprῊ sprῊ2;
					bool flag4;
					ParagraphFormat paragraphFormat;
					CharacterFormat characterFormat;
					CellFormat cellFormat;
					Dictionary<ConditionalFormattingCode, sprῊ>.Enumerator enumerator2;
					switch (num)
					{
					case 0:
						goto IL_D3;
					case 1:
						return;
					case 2:
						num = 11;
						continue;
					case 3:
						if (sprῊ != null)
						{
							num = 32;
							continue;
						}
						this.Rows[num2].RowFormat.ApplyBase(this.\u1718.ᜂ().ᜃ());
						num = 0;
						continue;
					case 4:
						if (num2 >= this.Rows.Count)
						{
							num = 1;
							continue;
						}
						sprῊ = null;
						num = 18;
						continue;
					case 5:
						num = 23;
						continue;
					case 6:
						flag = (((long)(num3 - 1) / this.\u1718.ᜃ().ᜅ() + 1L) % 2L == 1L);
						goto IL_6C3;
					case 7:
						flag2 = (((long)(num2 - 1) / this.\u1718.ᜃ().ᜃ() + 1L) % 2L == 1L);
						goto IL_651;
					case 8:
						flag2 = ((long)num2 / this.\u1718.ᜃ().ᜃ() % 2L == 1L);
						goto IL_651;
					case 9:
						try
						{
							num = 20;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair;
									sprῊ = keyValuePair.Value;
									num = 32;
									continue;
								}
								case 1:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair;
									sprῊ = keyValuePair.Value;
									num = 19;
									continue;
								}
								case 2:
									goto IL_380;
								case 3:
									num = 6;
									continue;
								case 4:
									if (this.ApplyStyleForLastRow)
									{
										num = 0;
										continue;
									}
									break;
								case 5:
									num = 17;
									continue;
								case 6:
									if (!this.ApplyStyleForLastRow)
									{
										num = 2;
										continue;
									}
									break;
								case 7:
									num = 11;
									continue;
								case 8:
									num = 10;
									continue;
								case 10:
									if (this.ApplyStyleForBandedRows)
									{
										num = 1;
										continue;
									}
									break;
								case 11:
									if (num2 != this.Rows.Count - 1)
									{
										num = 30;
										continue;
									}
									break;
								case 12:
									num = 14;
									continue;
								case 13:
									num = 4;
									continue;
								case 15:
								{
									ConditionalFormattingCode key;
									switch (key)
									{
									case ConditionalFormattingCode.FirstRow:
										num = 21;
										continue;
									case ConditionalFormattingCode.LastRow:
										num = 22;
										continue;
									case ConditionalFormattingCode.OddRowBanding:
										num = 34;
										continue;
									case ConditionalFormattingCode.EvenRowBanding:
										num = 23;
										continue;
									default:
										num = 12;
										continue;
									}
									break;
								}
								case 17:
									goto IL_46D;
								case 18:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair;
									sprῊ = keyValuePair.Value;
									num = 9;
									continue;
								}
								case 21:
									if (num2 == 0)
									{
										num = 24;
										continue;
									}
									break;
								case 22:
									if (num2 != 0)
									{
										num = 27;
										continue;
									}
									break;
								case 23:
									if (num2 != 0)
									{
										num = 7;
										continue;
									}
									break;
								case 24:
									num = 31;
									continue;
								case 25:
								{
									if (!enumerator.MoveNext())
									{
										num = 5;
										continue;
									}
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair = enumerator.Current;
									ConditionalFormattingCode key = keyValuePair.Key;
									num = 15;
									continue;
								}
								case 26:
									if (flag3)
									{
										num = 28;
										continue;
									}
									break;
								case 27:
									num = 36;
									continue;
								case 28:
									num = 29;
									continue;
								case 29:
									if (this.ApplyStyleForBandedRows)
									{
										num = 18;
										continue;
									}
									break;
								case 30:
									num = 35;
									continue;
								case 31:
									if (this.ApplyStyleForHeaderRow)
									{
										num = 33;
										continue;
									}
									break;
								case 33:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair;
									sprῊ = keyValuePair.Value;
									num = 16;
									continue;
								}
								case 34:
									if (num2 == this.Rows.Count - 1)
									{
										num = 3;
										continue;
									}
									goto IL_380;
								case 35:
									if (!flag3)
									{
										num = 8;
										continue;
									}
									break;
								case 36:
									if (num2 == this.Rows.Count - 1)
									{
										num = 13;
										continue;
									}
									break;
								}
								goto IL_190;
								IL_380:
								num = 26;
								continue;
								IL_425:
								num = 25;
								continue;
								IL_190:
								goto IL_425;
							}
							IL_46D:
							goto IL_13B1;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_480;
						IL_13B1:
						num = 3;
						continue;
					case 10:
						try
						{
							num = 72;
							for (;;)
							{
								ConditionalFormattingCode conditionalFormattingCode;
								switch (num)
								{
								case 0:
								{
									ConditionalFormattingCode key2;
									switch (key2)
									{
									case ConditionalFormattingCode.FirstColumn:
										num = 84;
										continue;
									case ConditionalFormattingCode.LastColumn:
										num = 12;
										continue;
									case ConditionalFormattingCode.OddColumnBanding:
										num = 32;
										continue;
									case ConditionalFormattingCode.EvenColumnBanding:
										num = 11;
										continue;
									case ConditionalFormattingCode.FirstRowLastCell:
										num = 87;
										continue;
									case ConditionalFormattingCode.FirstRowFirstCell:
										num = 19;
										continue;
									case ConditionalFormattingCode.LastRowLastCell:
										num = 40;
										continue;
									case ConditionalFormattingCode.LastRowFirstCell:
										num = 56;
										continue;
									default:
										num = 1;
										continue;
									}
									break;
								}
								case 1:
									num = 78;
									continue;
								case 2:
									if (num3 == this.Rows[num2].Cells.Count - 1)
									{
										num = 31;
										continue;
									}
									goto IL_FA0;
								case 3:
									num = 25;
									continue;
								case 4:
									num = 74;
									continue;
								case 5:
									goto IL_1218;
								case 6:
									if (this.ApplyStyleForFirstColumn)
									{
										num = 83;
										continue;
									}
									goto IL_FA0;
								case 7:
									goto IL_DCE;
								case 8:
									goto IL_C7B;
								case 9:
									num = 89;
									continue;
								case 10:
									if (sprῊ != null)
									{
										num = 51;
										continue;
									}
									goto IL_C7B;
								case 11:
									if (num3 != 0)
									{
										num = 73;
										continue;
									}
									goto IL_FA0;
								case 12:
									if (num3 != 0)
									{
										num = 58;
										continue;
									}
									goto IL_FA0;
								case 13:
									num = 60;
									continue;
								case 15:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
									sprῊ2 = keyValuePair2.Value;
									num = 65;
									continue;
								}
								case 16:
									num = 6;
									continue;
								case 17:
									if (flag4)
									{
										num = 90;
										continue;
									}
									goto IL_FA0;
								case 18:
									num = 5;
									continue;
								case 19:
									if (num2 == 0)
									{
										num = 61;
										continue;
									}
									goto IL_FA0;
								case 20:
									paragraphFormat.ᜂ(sprῊ.ᜀ());
									characterFormat.ᜂ(sprῊ.CharacterFormat);
									num = 53;
									continue;
								case 21:
									num = 86;
									continue;
								case 22:
									if (this.ApplyStyleForLastColumn)
									{
										num = 15;
										continue;
									}
									goto IL_FA0;
								case 23:
									if (this.ApplyStyleForLastRow)
									{
										num = 9;
										continue;
									}
									goto IL_FA0;
								case 24:
									if (this.ApplyStyleForHeaderRow)
									{
										num = 26;
										continue;
									}
									goto IL_FA0;
								case 25:
									if (num2 == this.Rows.Count - 1)
									{
										num = 30;
										continue;
									}
									goto IL_FA0;
								case 26:
									num = 22;
									continue;
								case 27:
									if (this.ApplyStyleForFirstColumn)
									{
										num = 62;
										continue;
									}
									goto IL_FA0;
								case 28:
									if (this.ApplyStyleForBandedColumns)
									{
										num = 88;
										continue;
									}
									goto IL_FA0;
								case 29:
									if (this.ApplyStyleForLastColumn)
									{
										num = 94;
										continue;
									}
									goto IL_FA0;
								case 30:
									num = 95;
									continue;
								case 31:
									num = 24;
									continue;
								case 32:
									if (num3 != this.Rows[num2].Cells.Count - 1)
									{
										num = 91;
										continue;
									}
									goto IL_FA0;
								case 33:
									goto IL_FA0;
								case 34:
									if (this.ApplyStyleForLastColumn)
									{
										num = 66;
										continue;
									}
									goto IL_FA0;
								case 35:
									if (sprῊ2 != null)
									{
										num = 54;
										continue;
									}
									break;
								case 37:
									goto IL_C4B;
								case 38:
									num = 71;
									continue;
								case 39:
									num = 23;
									continue;
								case 40:
									if (num2 != 0)
									{
										num = 3;
										continue;
									}
									goto IL_FA0;
								case 41:
									if (num3 != this.Rows[num2].Cells.Count - 1)
									{
										num = 46;
										continue;
									}
									goto IL_FA0;
								case 42:
									if (num3 == this.Rows[num2].Cells.Count - 1)
									{
										num = 49;
										continue;
									}
									goto IL_FA0;
								case 43:
									num = 29;
									continue;
								case 44:
									num = 27;
									continue;
								case 45:
									goto IL_FA0;
								case 46:
									num = 50;
									continue;
								case 47:
									num = 2;
									continue;
								case 48:
									num = 82;
									continue;
								case 49:
									num = 34;
									continue;
								case 50:
									if (!flag4)
									{
										num = 77;
										continue;
									}
									goto IL_FA0;
								case 51:
									num = 69;
									continue;
								case 52:
									if (sprῊ != null)
									{
										num = 4;
										continue;
									}
									goto IL_DCE;
								case 53:
									goto IL_10A7;
								case 54:
									cellFormat.ᜀ(sprῊ2.ᜈ());
									this.ᜀ(cellFormat.Borders, sprῊ2.ᜈ().ᜁ(), this.TableFormat.Borders, num2, this.Rows.Count);
									paragraphFormat.ᜂ(sprῊ2.ᜀ());
									characterFormat.ᜂ(sprῊ2.CharacterFormat);
									num = 10;
									continue;
								case 55:
									switch (conditionalFormattingCode)
									{
									case ConditionalFormattingCode.FirstColumn:
										num = 52;
										continue;
									case ConditionalFormattingCode.LastColumn:
									case ConditionalFormattingCode.OddColumnBanding:
									case ConditionalFormattingCode.EvenColumnBanding:
										break;
									case ConditionalFormattingCode.FirstRowLastCell:
									case ConditionalFormattingCode.FirstRowFirstCell:
									case ConditionalFormattingCode.LastRowLastCell:
									case ConditionalFormattingCode.LastRowFirstCell:
										cellFormat.ᜂ(sprῊ2.ᜈ());
										paragraphFormat.ᜂ(sprῊ2.ᜀ());
										characterFormat.ᜂ(sprῊ2.CharacterFormat);
										num = 36;
										continue;
									default:
										num = 92;
										continue;
									}
									break;
								case 56:
									if (num2 != 0)
									{
										num = 13;
										continue;
									}
									goto IL_FA0;
								case 57:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
									sprῊ2 = keyValuePair2.Value;
									num = 45;
									continue;
								}
								case 58:
									num = 42;
									continue;
								case 59:
									goto IL_FA0;
								case 60:
									if (num2 == this.Rows.Count - 1)
									{
										num = 38;
										continue;
									}
									goto IL_FA0;
								case 61:
									num = 93;
									continue;
								case 62:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
									sprῊ2 = keyValuePair2.Value;
									num = 33;
									continue;
								}
								case 63:
									if (this.ApplyStyleForBandedColumns)
									{
										num = 57;
										continue;
									}
									goto IL_FA0;
								case 64:
									goto IL_FA0;
								case 65:
									goto IL_FA0;
								case 66:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
									sprῊ2 = keyValuePair2.Value;
									num = 64;
									continue;
								}
								case 67:
									goto IL_FA0;
								case 68:
									num = 37;
									continue;
								case 69:
									if (sprῊ.ᜇ() == ConditionalFormattingCode.FirstRow)
									{
										num = 20;
										continue;
									}
									goto IL_10A7;
								case 70:
								{
									if (!enumerator2.MoveNext())
									{
										num = 18;
										continue;
									}
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2 = enumerator2.Current;
									ConditionalFormattingCode key2 = keyValuePair2.Key;
									num = 0;
									continue;
								}
								case 71:
									if (num3 == 0)
									{
										num = 39;
										continue;
									}
									goto IL_FA0;
								case 73:
									num = 41;
									continue;
								case 74:
									if (sprῊ.ᜇ() != ConditionalFormattingCode.FirstRow)
									{
										num = 7;
										continue;
									}
									break;
								case 75:
									goto IL_FA0;
								case 76:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
									sprῊ2 = keyValuePair2.Value;
									num = 67;
									continue;
								}
								case 77:
									num = 28;
									continue;
								case 78:
									goto IL_FA0;
								case 79:
									if (this.ApplyStyleForHeaderRow)
									{
										num = 44;
										continue;
									}
									goto IL_FA0;
								case 81:
									goto IL_FA0;
								case 82:
									if (this.ApplyStyleForLastRow)
									{
										num = 43;
										continue;
									}
									goto IL_FA0;
								case 83:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
									sprῊ2 = keyValuePair2.Value;
									num = 75;
									continue;
								}
								case 84:
									if (num3 == 0)
									{
										num = 16;
										continue;
									}
									goto IL_FA0;
								case 85:
									num = 79;
									continue;
								case 86:
									if (num3 != 0)
									{
										num = 47;
										continue;
									}
									goto IL_FA0;
								case 87:
									if (num2 == 0)
									{
										num = 21;
										continue;
									}
									goto IL_FA0;
								case 88:
								{
									KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
									sprῊ2 = keyValuePair2.Value;
									num = 59;
									continue;
								}
								case 89:
									if (this.ApplyStyleForFirstColumn)
									{
										num = 76;
										continue;
									}
									goto IL_FA0;
								case 90:
									num = 63;
									continue;
								case 91:
									num = 17;
									continue;
								case 92:
									num = 14;
									continue;
								case 93:
									if (num3 == 0)
									{
										num = 85;
										continue;
									}
									goto IL_FA0;
								case 94:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_C4B;
									default:
									{
										if (false)
										{
										}
										KeyValuePair<ConditionalFormattingCode, sprῊ> keyValuePair2;
										sprῊ2 = keyValuePair2.Value;
										num = 81;
										continue;
									}
									}
									break;
								case 95:
									if (num3 != 0)
									{
										num = 68;
										continue;
									}
									goto IL_FA0;
								}
								goto IL_8D4;
								IL_C4B:
								if (num3 == this.Rows[num2].Cells.Count - 1)
								{
									num = 48;
									continue;
								}
								goto IL_FA0;
								IL_C7B:
								conditionalFormattingCode = sprῊ2.ᜇ();
								num = 55;
								continue;
								IL_DCE:
								cellFormat.ᜀ(sprῊ2.ᜈ());
								this.ᜀ(cellFormat.Borders, sprῊ2.ᜈ().ᜁ(), this.TableFormat.Borders, num2, this.Rows.Count);
								num = 80;
								continue;
								IL_EF2:
								num = 70;
								continue;
								IL_8D4:
								goto IL_EF2;
								IL_FA0:
								num = 35;
								continue;
								IL_10A7:
								cellFormat.ᜀ(sprῊ.ᜈ());
								this.ᜁ(cellFormat.Borders, sprῊ.ᜈ().ᜁ(), this.TableFormat.Borders, num3, this.Rows[num2].Cells.Count);
								num = 8;
							}
							IL_1218:
							goto IL_4F3;
						}
						finally
						{
							((IDisposable)enumerator2).Dispose();
						}
						goto IL_122B;
						IL_4F3:
						this.Rows[num2].Cells[num3].ᜀ(cellFormat, paragraphFormat, characterFormat);
						num3++;
						num = 16;
						continue;
					case 11:
						if (!(this.\u1718 as spr\u173A).ᜀ().ContainsKey(ConditionalFormattingCode.FirstRow))
						{
							num = 19;
							continue;
						}
						num = 21;
						continue;
					case 12:
						flag2 = false;
						goto IL_651;
					case 13:
						if (this.ApplyStyleForFirstColumn)
						{
							num = 5;
							continue;
						}
						goto IL_126B;
					case 14:
						goto IL_5EE;
					case 15:
						num = 12;
						continue;
					case 16:
						goto IL_578;
					case 17:
						goto IL_126B;
					case 18:
						if (this.ApplyStyleForHeaderRow)
						{
							num = 2;
							continue;
						}
						goto IL_1240;
					case 19:
						goto IL_1240;
					case 20:
						this.TableFormat.ApplyBase(this.\u1718.ᜃ().ᜌ());
						num2 = 0;
						num = 24;
						continue;
					case 21:
						if (num2 == 0)
						{
							num = 15;
							continue;
						}
						num = 7;
						continue;
					case 22:
						if (sprῊ != null)
						{
							num = 36;
							continue;
						}
						goto IL_5EE;
					case 23:
						if (!(this.\u1718 as spr\u173A).ᜀ().ContainsKey(ConditionalFormattingCode.FirstColumn))
						{
							num = 17;
							continue;
						}
						num = 28;
						continue;
					case 24:
						goto IL_13D4;
					case 25:
						flag = ((long)num3 / this.\u1718.ᜃ().ᜅ() % 2L == 1L);
						goto IL_6C3;
					case 26:
						goto IL_122B;
					case 27:
						if (true)
						{
						}
						flag = false;
						goto IL_6C3;
					case 28:
						if (num3 == 0)
						{
							num = 30;
							continue;
						}
						num = 6;
						continue;
					case 29:
						goto IL_13D4;
					case 30:
						num = 27;
						continue;
					case 31:
						goto IL_D3;
					case 32:
						this.Rows[num2].RowFormat.ApplyBase(sprῊ.ᜅ().ᜃ());
						num = 31;
						continue;
					case 34:
						goto IL_578;
					case 35:
						if (num3 >= this.Rows[num2].Cells.Count)
						{
							num = 26;
							continue;
						}
						num = 13;
						continue;
					case 36:
						goto IL_480;
					}
					if (this.\u1718 != null)
					{
						num = 20;
						continue;
					}
					break;
					IL_D3:
					num3 = 0;
					num = 34;
					continue;
					IL_480:
					cellFormat.ᜀ(sprῊ.ᜈ());
					this.ᜁ(cellFormat.Borders, sprῊ.ᜈ().ᜁ(), this.TableFormat.Borders, num3, this.Rows[num2].Cells.Count);
					paragraphFormat.ᜂ(sprῊ.ᜀ());
					characterFormat.ᜂ(sprῊ.CharacterFormat);
					num = 14;
					continue;
					IL_578:
					num = 35;
					continue;
					IL_5EE:
					sprῊ2 = null;
					enumerator2 = (this.\u1718 as spr\u173A).ᜀ().GetEnumerator();
					num = 10;
					continue;
					IL_651:
					flag3 = flag2;
					enumerator = (this.\u1718 as spr\u173A).ᜀ().GetEnumerator();
					num = 9;
					continue;
					IL_6C3:
					flag4 = flag;
					paragraphFormat = new ParagraphFormat(base.Document);
					characterFormat = new CharacterFormat(base.Document);
					cellFormat = new CellFormat();
					paragraphFormat.ᜂ(this.\u1718.ᜀ());
					characterFormat.ᜂ(this.\u1718.get_CharacterFormat());
					cellFormat.ᜀ(this.\u1718.ᜁ());
					num = 22;
					continue;
					IL_122B:
					num2++;
					num = 29;
					continue;
					IL_1240:
					num = 8;
					continue;
					IL_126B:
					num = 25;
					continue;
					IL_13D4:
					num = 4;
				}
				return;
			}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00021BB8 File Offset: 0x00020BB8
		private void ᜁ(Borders A_0, Borders A_1, Borders A_2, int A_3, int A_4)
		{
			int num = 12;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_3 == A_4 - 1)
					{
						num = 1;
						continue;
					}
					goto IL_260;
				case 1:
					A_0.Right.ᜀ(A_1.Right);
					num = 16;
					continue;
				case 2:
					A_0.Right.ᜀ(A_1.Vertical);
					num = 3;
					continue;
				case 3:
					goto IL_1C7;
				case 4:
					A_0.Left.ᜀ(A_1.Vertical);
					num = 23;
					continue;
				case 5:
					A_0.Left.ᜀ(A_2.Vertical);
					num = 22;
					continue;
				case 6:
					if (true)
					{
					}
					if (A_3 == 0)
					{
						num = 19;
						continue;
					}
					goto IL_DE;
				case 7:
					A_0.Top.ᜀ(A_1.Top);
					A_0.Bottom.ᜀ(A_1.Bottom);
					num = 6;
					continue;
				case 8:
					if (A_3 > 0)
					{
						num = 11;
						continue;
					}
					return;
				case 9:
					goto IL_93;
				case 10:
					if (A_2.Vertical.BorderType != BorderStyle.None)
					{
						num = 5;
						continue;
					}
					return;
				case 11:
					num = 15;
					continue;
				case 13:
					num = 24;
					continue;
				case 14:
					if (A_3 < A_4 - 1)
					{
						num = 2;
						continue;
					}
					goto IL_93;
				case 15:
					if (A_3 < A_4)
					{
						num = 4;
						continue;
					}
					return;
				case 16:
					goto IL_260;
				case 17:
					A_0.Right.ᜀ(A_2.Vertical);
					num = 9;
					continue;
				case 18:
					if (A_1.Vertical.HasValue(2))
					{
						num = 21;
						continue;
					}
					return;
				case 19:
					A_0.Left.ᜀ(A_1.Left);
					num = 20;
					continue;
				case 20:
					goto IL_DE;
				case 21:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C7;
					default:
						if (false)
						{
						}
						num = 14;
						continue;
					}
					break;
				case 22:
					return;
				case 23:
					if (A_1.Vertical.BorderType == BorderStyle.Cleared)
					{
						num = 25;
						continue;
					}
					return;
				case 24:
					if (A_2.Vertical.BorderType != BorderStyle.None)
					{
						num = 17;
						continue;
					}
					goto IL_93;
				case 25:
					num = 10;
					continue;
				}
				if (!A_1.NoBorder)
				{
					num = 7;
					continue;
				}
				break;
				IL_93:
				num = 8;
				continue;
				IL_1C7:
				if (A_1.Vertical.BorderType == BorderStyle.Cleared)
				{
					num = 13;
					continue;
				}
				goto IL_93;
				IL_DE:
				num = 0;
				continue;
				IL_260:
				num = 18;
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00021EDC File Offset: 0x00020EDC
		private new void ᜀ(Borders A_0, Borders A_1, Borders A_2, int A_3, int A_4)
		{
			int num = 23;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_3 < A_4 - 1)
					{
						num = 7;
						continue;
					}
					goto IL_9B;
				case 1:
					num = 6;
					continue;
				case 2:
					goto IL_1CF;
				case 3:
					return;
				case 4:
					if (A_2.Horizontal.BorderType != BorderStyle.None)
					{
						num = 21;
						continue;
					}
					goto IL_9B;
				case 5:
					goto IL_9B;
				case 6:
					if (A_3 < A_4)
					{
						num = 25;
						continue;
					}
					return;
				case 7:
					A_0.Bottom.ᜀ(A_1.Horizontal);
					num = 2;
					continue;
				case 8:
					goto IL_E6;
				case 9:
					A_0.Left.ᜀ(A_1.Left);
					A_0.Right.ᜀ(A_1.Right);
					num = 22;
					continue;
				case 10:
					if (A_3 > 0)
					{
						num = 1;
						continue;
					}
					return;
				case 11:
					if (A_1.Horizontal.BorderType == BorderStyle.Cleared)
					{
						num = 14;
						continue;
					}
					return;
				case 12:
					if (A_2.Horizontal.BorderType != BorderStyle.None)
					{
						num = 18;
						continue;
					}
					return;
				case 13:
					goto IL_268;
				case 14:
					num = 12;
					continue;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1CF;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 16:
					A_0.Bottom.ᜀ(A_1.Bottom);
					num = 13;
					continue;
				case 17:
					A_0.Top.ᜀ(A_1.Top);
					num = 8;
					continue;
				case 18:
					A_0.Top.ᜀ(A_2.Vertical);
					num = 3;
					continue;
				case 19:
					if (A_3 == A_4 - 1)
					{
						num = 16;
						continue;
					}
					goto IL_268;
				case 20:
					num = 4;
					continue;
				case 21:
					A_0.Bottom.ᜀ(A_2.Vertical);
					num = 5;
					continue;
				case 22:
					if (A_3 == 0)
					{
						num = 17;
						continue;
					}
					goto IL_E6;
				case 24:
					if (A_1.Horizontal.HasValue(2))
					{
						num = 15;
						continue;
					}
					return;
				case 25:
					A_0.Top.ᜀ(A_1.Horizontal);
					num = 11;
					continue;
				}
				if (!A_1.NoBorder)
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				break;
				IL_9B:
				num = 10;
				continue;
				IL_1CF:
				if (A_1.Horizontal.BorderType == BorderStyle.Cleared)
				{
					num = 20;
					continue;
				}
				goto IL_9B;
				IL_E6:
				num = 19;
				continue;
				IL_268:
				num = 24;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00022200 File Offset: 0x00021200
		internal override spr\u226E FindAll(Regex pattern)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				spr\u226E spr_u226E = null;
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				try
				{
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
								try
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
										{
											spr\u226E spr_u226E2;
											if (spr_u226E2 != null)
											{
												num = 8;
												continue;
											}
											break;
										}
										case 1:
										{
											spr\u226E spr_u226E2;
											if (spr_u226E2.Count > 0)
											{
												num = 5;
												continue;
											}
											break;
										}
										case 3:
										{
											spr\u226E spr_u226E2;
											spr_u226E = spr_u226E2;
											num = 6;
											continue;
										}
										case 4:
											goto IL_1C4;
										case 5:
											num = 11;
											continue;
										case 7:
											num = 4;
											continue;
										case 8:
											num = 1;
											continue;
										case 10:
										{
											IEnumerator enumerator2;
											if (!enumerator2.MoveNext())
											{
												num = 7;
												continue;
											}
											TableCell tableCell = (TableCell)enumerator2.Current;
											spr\u226E spr_u226E2 = tableCell.ᜁ(pattern);
											num = 0;
											continue;
										}
										case 11:
										{
											if (spr_u226E == null)
											{
												num = 3;
												continue;
											}
											spr\u226E spr_u226E2;
											spr_u226E.AddRange(spr_u226E2);
											num = 9;
											continue;
										}
										}
										IL_FE:
										num = 10;
										continue;
										goto IL_FE;
									}
									IL_1C4:
									break;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator2;
										IDisposable disposable = enumerator2 as IDisposable;
										num = 0;
										for (;;)
										{
											switch (num)
											{
											case 0:
												if (disposable != null)
												{
													num = 2;
													continue;
												}
												goto IL_211;
											case 1:
												goto IL_20F;
											case 2:
												disposable.Dispose();
												num = 1;
												continue;
											}
											break;
										}
									}
									IL_20F:
									IL_211:;
								}
								goto IL_212;
							case 1:
								goto IL_212;
							case 2:
								goto IL_21E;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								TableRow tableRow = (TableRow)enumerator.Current;
								IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
								num = 0;
								continue;
							}
							}
							break;
							IL_212:
							num = 2;
							continue;
						}
						num = 4;
					}
					IL_21E:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable2.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable2 != null)
								{
									num = 0;
									continue;
								}
								goto IL_268;
							case 2:
								goto IL_266;
							}
							break;
						}
					}
					IL_266:
					IL_268:;
				}
				return spr_u226E;
			}
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000224AC File Offset: 0x000214AC
		protected override object CloneImpl()
		{
			Table table;
			for (;;)
			{
				table = (Table)base.CloneImpl();
				table.ᜃ = new RowCollection(table);
				table.ᜄ = null;
				table.TableFormat.ImportContainer(this.TableFormat);
				int num = 0;
				for (;;)
				{
					spr\u2179 spr_u;
					switch (num)
					{
					case 0:
						if (this.TableFormat.Scaling != 100f)
						{
							num = 7;
							continue;
						}
						goto IL_12D;
					case 1:
						goto IL_C1;
					case 2:
						if (spr_u != null)
						{
							num = 1;
							continue;
						}
						return table;
					case 3:
						goto IL_12D;
					case 4:
						return table;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C1;
						default:
							if (false)
							{
							}
							table.ᜏ = this.ᜏ.ᜀ(table);
							num = 6;
							continue;
						}
						break;
					case 6:
						goto IL_83;
					case 7:
						table.TableFormat.Scaling = this.TableFormat.Scaling;
						num = 3;
						continue;
					case 8:
						if (this.ᜏ != null)
						{
							num = 5;
							continue;
						}
						goto IL_83;
					}
					break;
					IL_83:
					this.Rows.ᜀ(table.ᜃ);
					spr_u = this.ᜃ();
					num = 2;
					continue;
					IL_C1:
					if (true)
					{
					}
					spr\u173A a_ = spr_u.Clone() as spr\u173A;
					table.ᜀ(a_);
					num = 4;
					continue;
					IL_12D:
					table.TableFormat.ᜀ(table);
					num = 8;
				}
			}
			return table;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00022638 File Offset: 0x00021638
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			int num = 2;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					DocumentObject documentObject = this.ChildObjects[num2];
					documentObject.CloneRelationsTo(doc, nextOwner);
					num2++;
					num = 6;
					continue;
				}
				case 1:
					return;
				case 3:
					goto IL_C0;
				case 4:
					if (true)
					{
					}
					this.ᜀ(doc);
					num = 5;
					continue;
				case 5:
					goto IL_BE;
				case 6:
					goto IL_C0;
				}
				if (doc.ImportStyles)
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
						continue;
					}
				}
				IL_85:
				num2 = 0;
				count = this.ChildObjects.Count;
				num = 3;
				continue;
				IL_BE:
				goto IL_85;
				IL_C0:
				num = 0;
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00022724 File Offset: 0x00021724
		internal object ᜋ()
		{
			Table table;
			for (;;)
			{
				IL_44:
				table = (Table)base.CloneImpl();
				table.ᜃ = new RowCollection(table);
				table.ᜄ = null;
				table.TableFormat.ImportContainer(this.TableFormat);
				table.TableFormat.Scaling = this.TableFormat.Scaling;
				table.TableFormat.ᜀ(table);
				int num = 3;
				for (;;)
				{
					spr\u2179 spr_u;
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
						switch (num)
						{
						case 0:
							return table;
						case 1:
							if (spr_u != null)
							{
								num = 5;
								continue;
							}
							return table;
						case 2:
							goto IL_E0;
						case 3:
							if (this.ᜏ != null)
							{
								num = 4;
								continue;
							}
							goto IL_E0;
						case 4:
							table.ᜏ = this.ᜏ.ᜀ(table);
							num = 2;
							continue;
						case 5:
							goto IL_C0;
						}
						goto IL_44;
						IL_E0:
						spr_u = this.ᜃ();
						num = 1;
						continue;
					}
					IL_C0:
					spr\u173A a_ = spr_u.Clone() as spr\u173A;
					table.ᜀ(a_);
					num = 0;
				}
			}
			return table;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00022854 File Offset: 0x00021854
		private new void ᜀ(Document A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.CurClonedSection != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
				{
					IStyle style;
					this.\u1718 = (spr\u173A)(this.\u1718 as Style).ᜀ(A_0, style);
					this.ᜀ(this.\u1718);
					num = 5;
					continue;
				}
				case 3:
					goto IL_CF;
				case 4:
				{
					IStyle style = A_0.Styles.FindByName(this.\u1718.Name, StyleType.TableStyle);
					num = 6;
					continue;
				}
				case 5:
					return;
				case 6:
				{
					IStyle style;
					if (style == null)
					{
						goto IL_C4;
					}
					num = 0;
					continue;
				}
				}
				if (this.\u1718 == null)
				{
					return;
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
					num = 4;
					continue;
				}
				IL_C4:
				num = 3;
			}
			IL_CF:
			(this.\u1718 as Style).ᜁ(A_0);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00022968 File Offset: 0x00021968
		private void ᜂ()
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					this.ᜆ = new List<float>();
					int num = 2;
					for (;;)
					{
						List<float>.Enumerator enumerator;
						float num3;
						IEnumerator enumerator2;
						switch (num)
						{
						case 0:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 4;
										continue;
									case 2:
									{
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										int num2 = (int)enumerator.Current;
										num3 += (float)num2;
										this.ᜀ(num3);
										num = 1;
										continue;
									}
									case 4:
										goto IL_35A;
									}
									IL_334:
									num = 2;
									continue;
									goto IL_334;
								}
								IL_35A:
								return;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_36D;
						case 1:
							try
							{
								num = 2;
								for (;;)
								{
									float num4;
									IEnumerator enumerator3;
									float num5;
									switch (num)
									{
									case 0:
										if (!enumerator2.MoveNext())
										{
											num = 1;
											continue;
										}
										goto IL_213;
									case 1:
										num = 3;
										continue;
									case 3:
										goto IL_276;
									case 4:
										try
										{
											num = 5;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_19C;
												case 1:
													goto IL_10D;
												case 2:
													switch ((1 == 1) ? 1 : 0)
													{
													case 0:
													case 2:
														goto IL_10D;
													default:
													{
														if (false)
														{
														}
														TableCell tableCell;
														num4 = (float)Math.Round((double)tableCell.Width);
														num = 4;
														continue;
													}
													}
													break;
												case 3:
												{
													TableCell tableCell;
													if (tableCell.WidthType == FtsWidth.Percentage)
													{
														num = 2;
														continue;
													}
													num4 = (float)Math.Round((double)(tableCell.Width * 20f));
													num = 0;
													continue;
												}
												case 4:
													goto IL_19C;
												case 6:
												{
													if (!enumerator3.MoveNext())
													{
														num = 1;
														continue;
													}
													TableCell tableCell = (TableCell)enumerator3.Current;
													num = 3;
													continue;
												}
												case 8:
													goto IL_1C5;
												}
												IL_F2:
												num = 6;
												continue;
												goto IL_F2;
												IL_10D:
												num = 8;
												continue;
												IL_19C:
												num5 += num4;
												this.ᜀ(num5);
												num = 7;
											}
											IL_1C5:
											break;
										}
										finally
										{
											for (;;)
											{
												IDisposable disposable = enumerator3 as IDisposable;
												num = 0;
												for (;;)
												{
													switch (num)
													{
													case 0:
														if (disposable != null)
														{
															num = 2;
															continue;
														}
														goto IL_212;
													case 1:
														goto IL_210;
													case 2:
														disposable.Dispose();
														num = 1;
														continue;
													}
													break;
												}
											}
											IL_210:
											IL_212:;
										}
										goto IL_213;
									}
									IL_97:
									num = 0;
									continue;
									goto IL_97;
									IL_213:
									TableRow tableRow = (TableRow)enumerator2.Current;
									num5 = 0f;
									num4 = 0f;
									num4 = tableRow.RowFormat.LeftIndent * 20f;
									num5 += num4;
									this.ᜀ(num5);
									enumerator3 = tableRow.Cells.GetEnumerator();
									num = 4;
								}
								IL_276:
								return;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator2 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable2.Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_2C1;
										case 2:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_2C3;
										}
										break;
									}
								}
								IL_2C1:
								IL_2C3:;
							}
							goto IL_2C4;
						case 2:
							if (this._ColumnWidths.Count > 0)
							{
								num = 3;
								continue;
							}
							goto IL_36D;
						case 3:
							goto IL_2C4;
						}
						break;
						IL_2C4:
						num3 = 0f;
						this.ᜀ(num3);
						enumerator = this._ColumnWidths.GetEnumerator();
						num = 0;
						continue;
						IL_36D:
						enumerator2 = this.Rows.GetEnumerator();
						num = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00022D4C File Offset: 0x00021D4C
		private new void ᜀ(float A_0)
		{
			int num = 7;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜆ.Add(A_0);
					num = 8;
					continue;
				case 1:
					goto IL_131;
				case 2:
					if (this.ᜆ.Count > 0)
					{
						num = 4;
						continue;
					}
					this.ᜆ.Add(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BC;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 3:
					goto IL_64;
				case 4:
				{
					if (true)
					{
					}
					num2 = 0;
					int count = this.ᜆ.Count;
					num = 3;
					continue;
				}
				case 5:
					return;
				case 6:
					goto IL_168;
				case 8:
					goto IL_EA;
				case 9:
				{
					int count;
					if (count == num2 + 1)
					{
						num = 0;
						continue;
					}
					goto IL_EA;
				}
				case 10:
					goto IL_64;
				case 11:
				{
					int count;
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					float num3 = this.ᜆ[num2];
					num = 12;
					continue;
				}
				case 12:
				{
					float num3;
					if (num3 > A_0)
					{
						num = 6;
						continue;
					}
					num = 9;
					continue;
				}
				case 13:
					goto IL_BC;
				}
				if (this.ᜆ.IndexOf(A_0) < 0)
				{
					num = 13;
					continue;
				}
				return;
				IL_64:
				num = 11;
				continue;
				IL_BC:
				num = 2;
				continue;
				IL_EA:
				num2++;
				num = 10;
			}
			return;
			IL_131:
			return;
			IL_168:
			this.ᜆ.Insert(num2, A_0);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00022EE8 File Offset: 0x00021EE8
		internal override BodyRegion GetNextTextBodyItem()
		{
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					base.GetNextInSection(base.OwnerTextBody.Owner as Section);
					num = 1;
					continue;
				case 1:
					goto IL_76;
				case 2:
					if (base.OwnerTextBody != null)
					{
						num = 0;
						continue;
					}
					goto IL_D9;
				case 4:
					goto IL_F7;
				case 5:
					if (base.OwnerTextBody is TableCell)
					{
						num = 7;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_55;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 6:
					goto IL_55;
				case 7:
					(base.OwnerTextBody as TableCell).ᜋ();
					num = 4;
					continue;
				}
				if (base.NextSibling == null)
				{
					num = 6;
					continue;
				}
				goto IL_F9;
				IL_55:
				num = 5;
			}
			IL_76:
			IL_D9:
			return null;
			IL_F7:
			goto IL_D9;
			IL_F9:
			return base.NextSibling as BodyRegion;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00022FFC File Offset: 0x00021FFC
		internal new void ᜀ(FormatBase A_0, int A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						try
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 2:
									goto IL_127;
								case 3:
								{
									if (!enumerator.MoveNext())
									{
										num = 4;
										continue;
									}
									TableRow tableRow = (TableRow)enumerator.Current;
									tableRow.RowFormat.Borders.ImportContainer(this.TableFormat.Borders);
									num = 1;
									continue;
								}
								case 4:
									num = 2;
									continue;
								}
								IL_101:
								num = 3;
								continue;
								goto IL_101;
							}
							IL_127:
							goto IL_3BA;
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
										goto IL_172;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_174;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_172:
							IL_174:;
						}
						goto Block_4;
					case 1:
						if (A_0 is Borders)
						{
							num = 10;
							continue;
						}
						num = 3;
						continue;
					case 2:
						goto IL_244;
					case 3:
						if (A_0 is Paddings)
						{
							num = 4;
							continue;
						}
						goto IL_3BA;
					case 4:
						goto IL_34F;
					case 5:
						if (!(A_0 is Border))
						{
							num = 8;
							continue;
						}
						goto IL_377;
					case 6:
					{
						IEnumerator enumerator2 = this.Rows.GetEnumerator();
						num = 2;
						continue;
					}
					case 8:
						goto IL_30D;
					case 9:
						goto IL_175;
					case 10:
						goto IL_377;
					}
					if (!(A_0 is RowFormat))
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34F;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					IL_30D:
					num = 1;
					continue;
					Block_5:
					try
					{
						IL_244:
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_2C0;
							case 3:
								num = 1;
								continue;
							case 4:
							{
								IEnumerator enumerator2;
								if (!enumerator2.MoveNext())
								{
									num = 3;
									continue;
								}
								TableRow tableRow2 = (TableRow)enumerator2.Current;
								tableRow2.RowFormat.ᜀ(A_1, this.TableFormat.ᜁ(A_1));
								num = 0;
								continue;
							}
							}
							IL_26C:
							num = 4;
							continue;
							goto IL_26C;
						}
						IL_2C0:
						break;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator2;
							IDisposable disposable2 = enumerator2 as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable2 != null)
									{
										num = 2;
										continue;
									}
									goto IL_30C;
								case 1:
									goto IL_30A;
								case 2:
									disposable2.Dispose();
									num = 1;
									continue;
								}
								break;
							}
						}
						IL_30A:
						IL_30C:;
					}
					goto IL_30D;
					Block_4:
					IEnumerator enumerator3;
					try
					{
						IL_175:
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 1;
								continue;
							case 1:
								goto IL_1F6;
							case 2:
							{
								if (!enumerator3.MoveNext())
								{
									num = 0;
									continue;
								}
								TableRow tableRow3 = (TableRow)enumerator3.Current;
								tableRow3.RowFormat.Paddings.ImportContainer(this.TableFormat.Paddings);
								num = 4;
								continue;
							}
							}
							IL_1D0:
							num = 2;
							continue;
							goto IL_1D0;
						}
						IL_1F6:
						break;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable3 = enumerator3 as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									disposable3.Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_241;
								case 2:
									if (disposable3 != null)
									{
										num = 0;
										continue;
									}
									goto IL_243;
								}
								break;
							}
						}
						IL_241:
						IL_243:;
					}
					goto Block_5;
					IL_34F:
					enumerator3 = this.Rows.GetEnumerator();
					num = 9;
					continue;
					IL_377:
					enumerator = this.Rows.GetEnumerator();
					num = 0;
				}
				IL_3BA:
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000233F4 File Offset: 0x000223F4
		internal override void Close()
		{
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜆ != null)
					{
						num = 12;
						continue;
					}
					goto IL_CD;
				case 1:
					goto IL_CD;
				case 2:
					if (this.ᜄ != null)
					{
						num = 17;
						continue;
					}
					goto IL_6D;
				case 3:
					goto IL_6D;
				case 4:
					if (this.ᜃ.Count > 0)
					{
						num = 7;
						continue;
					}
					goto IL_8A;
				case 5:
					goto IL_1E8;
				case 6:
					goto IL_1E8;
				case 7:
				{
					int count = this.ᜃ.Count;
					int num2 = 0;
					num = 5;
					continue;
				}
				case 8:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 13;
						continue;
					}
					TableRow tableRow = this.ᜃ[num2];
					tableRow.ᜂ();
					num2++;
					num = 6;
					continue;
				}
				case 9:
					return;
				case 10:
					this.ᜏ.ᜂ();
					this.ᜏ = null;
					num = 9;
					continue;
				case 11:
					goto IL_8A;
				case 12:
					if (true)
					{
					}
					this.ᜆ.Clear();
					this.ᜆ = null;
					num = 1;
					continue;
				case 13:
					goto IL_169;
				case 14:
					num = 4;
					continue;
				case 15:
					if (this.ᜏ != null)
					{
						num = 10;
						continue;
					}
					return;
				case 17:
					this.ᜄ.Close();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_169;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				if (this.ᜃ != null)
				{
					num = 14;
					continue;
				}
				goto IL_8A;
				IL_6D:
				num = 0;
				continue;
				IL_8A:
				num = 2;
				continue;
				IL_CD:
				num = 15;
				continue;
				IL_169:
				this.ᜃ.Clear();
				this.ᜃ = null;
				num = 11;
				continue;
				IL_1E8:
				num = 8;
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00023608 File Offset: 0x00022608
		internal float ᜑ()
		{
			switch (0)
			{
			default:
			{
				float num6;
				for (;;)
				{
					float num = 0f;
					int num2 = 17;
					for (;;)
					{
						int num4;
						switch (num2)
						{
						case 0:
							if (base.Document != null)
							{
								num2 = 15;
								continue;
							}
							goto IL_D5;
						case 1:
							goto IL_101;
						case 2:
							num2 = 11;
							continue;
						case 3:
							if (this.PreferredTableWidth.ᜀ() == FtsWidth.Point)
							{
								num2 = 2;
								continue;
							}
							return num;
						case 4:
						{
							if (true)
							{
							}
							float num3;
							if (num3 > num)
							{
								num2 = 21;
								continue;
							}
							goto IL_1A3;
						}
						case 5:
							num2 = 0;
							continue;
						case 6:
						{
							int count;
							if (num4 >= count)
							{
								num2 = 25;
								continue;
							}
							float num3 = 0f;
							int num5 = 0;
							int count2 = this.Rows[num4].Cells.Count;
							num2 = 1;
							continue;
						}
						case 7:
							if (num6 > num)
							{
								num2 = 8;
								continue;
							}
							return num;
						case 8:
							return num6;
						case 9:
							if (base.Document.GrammarSpellingData == null)
							{
								num2 = 16;
								continue;
							}
							goto IL_D5;
						case 10:
							goto IL_B0;
						case 11:
							if (this.PreferredWidth.Type == WidthType.Twip)
							{
								num2 = 18;
								continue;
							}
							return num;
						case 12:
							if (this.Rows[num4].RowFormat.HorizontalAlignment == RowAlignment.Left)
							{
								num2 = 22;
								continue;
							}
							goto IL_D5;
						case 13:
						{
							float num3 = 0f;
							num4 = 0;
							int count = this.Rows.Count;
							num2 = 23;
							continue;
						}
						case 14:
							goto IL_D5;
						case 15:
							num2 = 9;
							continue;
						case 16:
							num2 = 12;
							continue;
						case 17:
							if (this.Rows.Count > 0)
							{
								num2 = 13;
								continue;
							}
							goto IL_266;
						case 18:
							num6 = (float)this.PreferredWidth.Value / 20f;
							num2 = 7;
							continue;
						case 19:
							goto IL_1A3;
						case 20:
						{
							int num5;
							int count2;
							if (num5 >= count2)
							{
								num2 = 5;
								continue;
							}
							TableCell tableCell = this.Rows[num4].Cells[num5];
							float num3;
							num3 += tableCell.Width;
							num5++;
							num2 = 24;
							continue;
						}
						case 21:
						{
							float num3;
							num = num3;
							num2 = 19;
							continue;
						}
						case 22:
						{
							float num3;
							num3 += Math.Abs(this.Rows[num4].RowFormat.LeftIndent);
							num2 = 14;
							continue;
						}
						case 23:
							goto IL_B0;
						case 24:
							goto IL_101;
						case 25:
							goto IL_266;
						}
						break;
						IL_B0:
						num2 = 6;
						continue;
						IL_D5:
						num2 = 4;
						continue;
						IL_101:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num2 = 20;
							continue;
						}
						IL_1A3:
						num4++;
						num2 = 10;
						continue;
						IL_266:
						num2 = 3;
					}
				}
				return num6;
			}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0002396C File Offset: 0x0002296C
		private Section ᜁ()
		{
			IDocumentObject documentObject;
			for (;;)
			{
				documentObject = this;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_61;
					case 1:
						if (documentObject == null)
						{
							num = 5;
							continue;
						}
						num = 2;
						continue;
					case 2:
						if (documentObject.DocumentObjectType == DocumentObjectType.Section)
						{
							num = 4;
							continue;
						}
						documentObject = documentObject.Owner;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 3:
						goto IL_88;
					case 4:
						goto IL_86;
					case 5:
						goto IL_A9;
					}
					break;
					IL_88:
					if (true)
					{
					}
					num = 1;
					continue;
					IL_61:
					goto IL_88;
				}
			}
			IL_86:
			return documentObject as Section;
			IL_A9:
			return null;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00023A28 File Offset: 0x00022A28
		internal override void MakeChanges(bool acceptChanges)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 2;
					for (;;)
					{
						TableRow tableRow;
						switch (num2)
						{
						case 0:
							goto IL_2D0;
						case 1:
							return;
						case 2:
							goto IL_2D0;
						case 3:
						{
							if (num >= this.ᜃ.Count)
							{
								num2 = 1;
								continue;
							}
							tableRow = this.ᜃ[num];
							IEnumerator enumerator = tableRow.Cells.GetEnumerator();
							num2 = 16;
							continue;
						}
						case 4:
							goto IL_2AD;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2FE;
							default:
								if (false)
								{
								}
								goto IL_9C;
							}
							break;
						case 6:
							goto IL_77;
						case 7:
							if (tableRow.ᜆ != null)
							{
								num2 = 19;
								continue;
							}
							goto IL_9C;
						case 8:
							num2 = 17;
							continue;
						case 9:
							if (tableRow.IsDeleteRevision)
							{
								num2 = 8;
								continue;
							}
							goto IL_77;
						case 10:
							if (acceptChanges)
							{
								num2 = 12;
								continue;
							}
							num2 = 7;
							continue;
						case 11:
							goto IL_36E;
						case 12:
							tableRow.ᜆ = null;
							num2 = 18;
							continue;
						case 13:
							goto IL_C1;
						case 14:
							if (!acceptChanges)
							{
								num2 = 13;
								continue;
							}
							goto IL_36E;
						case 15:
							if (tableRow.IsInsertRevision)
							{
								num2 = 4;
								continue;
							}
							goto IL_36E;
						case 16:
							try
							{
								num2 = 12;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_25F;
									case 1:
										num2 = 0;
										continue;
									case 2:
									{
										TableCell tableCell;
										if (tableCell.ᜉ != null)
										{
											num2 = 13;
											continue;
										}
										break;
									}
									case 4:
									{
										TableCell tableCell;
										tableCell.ᜉ = null;
										num2 = 3;
										continue;
									}
									case 5:
										goto IL_14F;
									case 6:
										goto IL_14F;
									case 8:
										if (acceptChanges)
										{
											num2 = 4;
											continue;
										}
										num2 = 2;
										continue;
									case 9:
									{
										TableCell tableCell;
										tableCell.ᜂ(acceptChanges);
										num2 = 8;
										continue;
									}
									case 10:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num2 = 1;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator.Current;
										int num3 = 0;
										int count = tableCell.Items.Count;
										num2 = 6;
										continue;
									}
									case 11:
									{
										int num3;
										int count;
										if (num3 >= count)
										{
											num2 = 9;
											continue;
										}
										TableCell tableCell;
										tableCell.Items[num3].MakeChanges(acceptChanges);
										num3++;
										num2 = 5;
										continue;
									}
									case 13:
									{
										TableCell tableCell;
										tableCell.CellFormat.ClearFormatting();
										tableCell.CellFormat.ImportContainer(tableCell.TrackCellFormat);
										tableCell.ᜉ = null;
										num2 = 7;
										continue;
									}
									}
									goto IL_12C;
									IL_14F:
									num2 = 11;
									continue;
									IL_168:
									num2 = 10;
									continue;
									IL_12C:
									goto IL_168;
								}
								IL_25F:
								goto IL_34E;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											disposable.Dispose();
											num2 = 1;
											continue;
										case 1:
											goto IL_2AA;
										case 2:
											if (disposable != null)
											{
												num2 = 0;
												continue;
											}
											goto IL_2AC;
										}
										break;
									}
								}
								IL_2AA:
								IL_2AC:;
							}
							goto IL_2AD;
							IL_34E:
							num2 = 10;
							continue;
						case 17:
							if (true)
							{
							}
							if (!acceptChanges)
							{
								num2 = 6;
								continue;
							}
							goto IL_C1;
						case 18:
							goto IL_9C;
						case 19:
							goto IL_2FE;
						}
						break;
						IL_77:
						num2 = 15;
						continue;
						IL_9C:
						num2 = 9;
						continue;
						IL_C1:
						this.ᜃ.RemoveAt(num);
						num--;
						num2 = 11;
						continue;
						IL_2AD:
						num2 = 14;
						continue;
						IL_2D0:
						num2 = 3;
						continue;
						IL_2FE:
						tableRow.RowFormat.ClearFormatting();
						tableRow.RowFormat.ImportContainer(tableRow.TrackRowFormat);
						tableRow.ᜆ = null;
						num2 = 5;
						continue;
						IL_36E:
						num++;
						num2 = 0;
					}
				}
				return;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00023E74 File Offset: 0x00022E74
		internal override void RemoveCFormatChanges()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
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
				IL_3C:
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										goto IL_12E;
									case 2:
										num = 1;
										continue;
									case 4:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 2;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										tableCell.CharacterFormat.RemoveChanges();
										num = 3;
										continue;
									}
									}
									IL_E7:
									num = 4;
									continue;
									goto IL_E7;
								}
								IL_12E:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable != null)
											{
												num = 1;
												continue;
											}
											goto IL_17A;
										case 1:
											disposable.Dispose();
											num = 2;
											continue;
										case 2:
											goto IL_178;
										}
										break;
									}
								}
								IL_178:
								IL_17A:;
							}
							goto IL_17B;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							tableRow.CharacterFormat.RemoveChanges();
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 0;
							continue;
						}
						case 3:
							goto IL_187;
						case 4:
							goto IL_17B;
						}
						IL_A0:
						num = 2;
						continue;
						goto IL_A0;
						IL_17B:
						num = 3;
					}
					IL_187:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1CE;
							case 1:
								disposable2.Dispose();
								num = 0;
								continue;
							case 2:
								if (disposable2 != null)
								{
									num = 1;
									continue;
								}
								goto IL_1D0;
							}
							break;
						}
					}
					IL_1CE:
					IL_1D0:;
				}
				return;
			}
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00024088 File Offset: 0x00023088
		internal override void RemovePFormatChanges()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IEnumerator enumerator = this.ᜃ.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_A5;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						TableRow tableRow = (TableRow)enumerator.Current;
						tableRow.RowFormat.RemoveChanges();
						num = 0;
						continue;
					}
					}
					IL_83:
					num = 3;
					continue;
					goto IL_83;
				}
				IL_A5:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 2;
							continue;
						case 1:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_E7;
						case 2:
							goto IL_E5;
						}
						break;
					}
				}
				IL_E5:
				IL_E7:;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00024190 File Offset: 0x00023190
		internal override void AcceptCChanges()
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_175;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							tableRow.CharacterFormat.AcceptChanges();
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 3;
							continue;
						}
						case 3:
							try
							{
								num = 4;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 2;
										continue;
									case 2:
										goto IL_100;
									case 3:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 1;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										tableCell.CharacterFormat.AcceptChanges();
										num = 0;
										continue;
									}
									}
									IL_B9:
									num = 3;
									continue;
									goto IL_B9;
								}
								IL_100:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												continue;
											default:
												if (false)
												{
												}
												disposable.Dispose();
												num = 2;
												continue;
											}
											break;
										case 1:
											if (disposable != null)
											{
												num = 0;
												continue;
											}
											goto IL_168;
										case 2:
											goto IL_166;
										}
										break;
									}
								}
								IL_166:
								IL_168:;
							}
							goto IL_169;
						case 4:
							goto IL_169;
						}
						IL_72:
						num = 2;
						continue;
						goto IL_72;
						IL_169:
						num = 0;
					}
					IL_175:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable2.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable2 != null)
								{
									num = 0;
									continue;
								}
								goto IL_1BE;
							case 2:
								goto IL_1BC;
							}
							break;
						}
					}
					IL_1BC:
					IL_1BE:;
				}
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000243A4 File Offset: 0x000233A4
		internal override void AcceptPChanges()
		{
			if (true)
			{
			}
			IEnumerator enumerator = this.ᜃ.GetEnumerator();
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_62:
					TableRow tableRow = (TableRow)enumerator.Current;
					tableRow.RowFormat.AcceptChanges();
					num = 2;
					break;
				}
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
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						goto IL_62;
					case 3:
						num = 4;
						continue;
					case 4:
						goto IL_A5;
					}
					IL_83:
					num = 1;
					continue;
					goto IL_83;
				}
				IL_A5:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_E7;
						case 1:
							disposable.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_E5;
						}
						break;
					}
				}
				IL_E5:
				IL_E7:;
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000244AC File Offset: 0x000234AC
		internal override bool CheckChangedPFormat()
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				bool result;
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_D9;
						case 1:
						{
							TableRow tableRow;
							if (tableRow.RowFormat.IsChangedFormat)
							{
								num = 4;
								continue;
							}
							break;
						}
						case 3:
							num = 0;
							continue;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								result = true;
								num = 5;
								continue;
							}
							break;
						case 5:
							goto IL_CB;
						case 6:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							num = 1;
							continue;
						}
						}
						IL_58:
						num = 6;
						continue;
						goto IL_58;
					}
					IL_CB:
					return result;
					IL_D9:
					return false;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable.Dispose();
								if (true)
								{
								}
								num = 1;
								continue;
							case 1:
								goto IL_128;
							case 2:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_12A;
							}
							break;
						}
					}
					IL_128:
					IL_12A:;
				}
				return result;
			}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000245F8 File Offset: 0x000235F8
		internal override bool CheckDeleteRev()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				bool result;
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							TableRow tableRow;
							if (tableRow.CharacterFormat.IsDeleteRevision)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 1:
							goto IL_E1;
						case 2:
							num = 1;
							continue;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							num = 0;
							continue;
						}
						case 5:
							goto IL_D3;
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
								result = true;
								num = 5;
								continue;
							}
							break;
						}
						IL_60:
						num = 3;
						continue;
						goto IL_60;
					}
					IL_D3:
					return result;
					IL_E1:
					return false;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_12A;
							case 2:
								goto IL_128;
							}
							break;
						}
					}
					IL_128:
					IL_12A:;
				}
				return result;
			}
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00024744 File Offset: 0x00023744
		internal override bool CheckInsertRev()
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				bool result;
				try
				{
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							num = 3;
							continue;
						}
						case 1:
							goto IL_CF;
						case 2:
							goto IL_C1;
						case 3:
						{
							TableRow tableRow;
							if (tableRow.CharacterFormat.IsInsertRevision)
							{
								num = 5;
								continue;
							}
							break;
						}
						case 4:
							num = 1;
							continue;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								result = true;
								num = 2;
								continue;
							}
							break;
						}
						IL_4E:
						num = 0;
						continue;
						goto IL_4E;
					}
					IL_C1:
					goto IL_119;
					IL_CF:
					return false;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_118;
							case 2:
								goto IL_116;
							}
							break;
						}
					}
					IL_116:
					IL_118:;
				}
				IL_119:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00024890 File Offset: 0x00023890
		internal override bool CheckChangedCFormat()
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.ᜃ.GetEnumerator();
				bool result;
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							TableRow tableRow;
							if (tableRow.CharacterFormat.IsChangedFormat)
							{
								num = 5;
								continue;
							}
							IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
							num = 3;
							continue;
						}
						case 1:
							goto IL_1EF;
						case 3:
							try
							{
								num = 6;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 4;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator2.Current;
										num = 2;
										continue;
									}
									case 1:
										result = true;
										num = 3;
										continue;
									case 2:
									{
										TableCell tableCell;
										if (tableCell.CharacterFormat.IsChangedFormat)
										{
											num = 1;
											continue;
										}
										break;
									}
									case 3:
										goto IL_EA;
									case 4:
										num = 5;
										continue;
									case 5:
										goto IL_129;
									}
									IL_C2:
									num = 0;
									continue;
									goto IL_C2;
								}
								IL_EA:
								return result;
								IL_129:;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_18D;
										case 1:
											if (disposable != null)
											{
												num = 2;
												continue;
											}
											goto IL_18F;
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
												disposable.Dispose();
												num = 0;
												continue;
											}
											break;
										}
										break;
									}
								}
								IL_18D:
								IL_18F:;
							}
							break;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							num = 0;
							continue;
						}
						case 5:
							result = true;
							num = 7;
							continue;
						case 6:
							num = 1;
							continue;
						case 7:
							goto IL_1E1;
						}
						IL_190:
						num = 4;
						continue;
						goto IL_190;
					}
					IL_1E1:
					return result;
					IL_1EF:
					return false;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_239;
							case 1:
								disposable2.Dispose();
								num = 0;
								continue;
							case 2:
								if (disposable2 != null)
								{
									num = 1;
									continue;
								}
								goto IL_243;
							}
							break;
						}
					}
					IL_239:
					if (true)
					{
					}
					IL_243:;
				}
				return result;
			}
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00024B18 File Offset: 0x00023B18
		internal override bool HasTrackedChanges()
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						try
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_299;
								case 2:
								{
									if (!enumerator.MoveNext())
									{
										num = 1;
										continue;
									}
									TableRow tableRow = (TableRow)enumerator.Current;
									IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
									num = 3;
									continue;
								}
								case 3:
									try
									{
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												try
												{
													num = 4;
													bool result;
													for (;;)
													{
														switch (num)
														{
														case 0:
														{
															BodyRegion bodyRegion;
															if (bodyRegion.HasTrackedChanges())
															{
																num = 1;
																continue;
															}
															break;
														}
														case 1:
															result = true;
															num = 2;
															continue;
														case 2:
															goto IL_194;
														case 3:
															goto IL_1CE;
														case 5:
															num = 3;
															continue;
														case 6:
														{
															IEnumerator enumerator3;
															if (!enumerator3.MoveNext())
															{
																num = 5;
																continue;
															}
															BodyRegion bodyRegion = (BodyRegion)enumerator3.Current;
															num = 0;
															continue;
														}
														}
														IL_16C:
														num = 6;
														continue;
														goto IL_16C;
													}
													IL_194:
													return result;
													IL_1CE:;
												}
												finally
												{
													for (;;)
													{
														IEnumerator enumerator3;
														IDisposable disposable = enumerator3 as IDisposable;
														num = 0;
														for (;;)
														{
															switch (num)
															{
															case 0:
																if (disposable != null)
																{
																	num = 1;
																	continue;
																}
																goto IL_218;
															case 1:
																disposable.Dispose();
																num = 2;
																continue;
															case 2:
																goto IL_216;
															}
															break;
														}
													}
													IL_216:
													IL_218:;
												}
												break;
											case 2:
												num = 4;
												continue;
											case 3:
											{
												IEnumerator enumerator2;
												if (!enumerator2.MoveNext())
												{
													num = 2;
													continue;
												}
												TableCell tableCell = (TableCell)enumerator2.Current;
												IEnumerator enumerator3 = tableCell.Items.GetEnumerator();
												num = 0;
												continue;
											}
											case 4:
												goto IL_24B;
											}
											IL_219:
											num = 3;
											continue;
											goto IL_219;
										}
										IL_24B:
										break;
									}
									finally
									{
										for (;;)
										{
											IEnumerator enumerator2;
											IDisposable disposable2 = enumerator2 as IDisposable;
											num = 1;
											for (;;)
											{
												switch (num)
												{
												case 0:
													goto IL_296;
												case 1:
													if (disposable2 != null)
													{
														num = 2;
														continue;
													}
													goto IL_298;
												case 2:
													disposable2.Dispose();
													num = 0;
													continue;
												}
												break;
											}
										}
										IL_296:
										IL_298:;
									}
									goto IL_299;
								case 4:
									goto IL_2A5;
								}
								IL_C9:
								num = 2;
								continue;
								goto IL_C9;
								IL_299:
								num = 4;
							}
							IL_2A5:
							goto IL_33D;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable3 = enumerator as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable3.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_2F0;
									case 2:
										if (disposable3 != null)
										{
											num = 0;
											continue;
										}
										goto IL_2F2;
									}
									break;
								}
							}
							IL_2F0:
							IL_2F2:;
						}
						goto IL_2F3;
					case 1:
						if (!base.IsInsertRevision)
						{
							num = 3;
							continue;
						}
						return true;
					case 3:
						num = 5;
						continue;
					case 4:
						if (base.IsChangedPFormat)
						{
							num = 7;
							continue;
						}
						goto IL_2F3;
					case 5:
						goto IL_31D;
					case 6:
						num = 4;
						continue;
					case 7:
						goto IL_38E;
					case 8:
						num = 1;
						continue;
					}
					if (base.IsDeleteRevision)
					{
						return true;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31D;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					IL_2F3:
					enumerator = this.ᜃ.GetEnumerator();
					num = 0;
					continue;
					IL_31D:
					if (base.IsChangedCFormat)
					{
						return true;
					}
					num = 6;
				}
				IL_33D:
				if (true)
				{
				}
				return false;
				IL_38E:
				return true;
			}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00024F04 File Offset: 0x00023F04
		internal bool ᜆ()
		{
			for (;;)
			{
				TableRow tableRow = null;
				int num = 0;
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_126;
					case 1:
						if (true)
						{
						}
						if (tableRow.CharacterFormat.IsInsertRevision)
						{
							num2 = 0;
							continue;
						}
						goto IL_112;
					case 2:
						this.ᜃ.Remove(tableRow);
						num--;
						num2 = 9;
						continue;
					case 3:
						goto IL_9B;
					case 4:
						num2 = 1;
						continue;
					case 5:
						if (num >= this.ᜃ.Count)
						{
							goto IL_B4;
						}
						tableRow = this.ᜃ[num];
						num2 = 6;
						continue;
					case 6:
						if (!tableRow.CharacterFormat.IsDeleteRevision)
						{
							num2 = 4;
							continue;
						}
						goto IL_126;
					case 7:
						goto IL_9B;
					case 8:
						if (this.ᜃ.Count > 1)
						{
							num2 = 2;
							continue;
						}
						return true;
					case 9:
						goto IL_112;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B4;
						default:
							goto IL_D5;
						}
						break;
					}
					break;
					IL_9B:
					num2 = 5;
					continue;
					IL_B4:
					num2 = 10;
					continue;
					IL_112:
					num++;
					num2 = 3;
					continue;
					IL_126:
					num2 = 8;
				}
			}
			return true;
			IL_D5:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00025064 File Offset: 0x00024064
		internal override void SetDeleteRev(bool check)
		{
			if (true)
			{
			}
			IEnumerator enumerator = this.ᜃ.GetEnumerator();
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_62:
					TableRow tableRow = (TableRow)enumerator.Current;
					tableRow.CharacterFormat.IsDeleteRevision = check;
					num = 3;
					break;
				}
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
						if (!enumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						goto IL_62;
					case 1:
						goto IL_A6;
					case 4:
						num = 1;
						continue;
					}
					IL_84:
					num = 0;
					continue;
					goto IL_84;
				}
				IL_A6:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_E8;
						case 1:
							disposable.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_E6;
						}
						break;
					}
				}
				IL_E6:
				IL_E8:;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0002516C File Offset: 0x0002416C
		internal override void SetInsertRev(bool check)
		{
			IEnumerator enumerator = this.ᜃ.GetEnumerator();
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_5A:
					TableRow tableRow = (TableRow)enumerator.Current;
					tableRow.CharacterFormat.IsInsertRevision = check;
					num = 3;
					break;
				}
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
					case 2:
						goto IL_9E;
					case 4:
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						goto IL_5A;
					}
					IL_7C:
					num = 4;
					continue;
					goto IL_7C;
				}
				IL_9E:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_E8;
						case 1:
							goto IL_E6;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_E6:
				IL_E8:;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00025274 File Offset: 0x00024274
		internal override void SetChangedCFormat(bool check)
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
				switch (0)
				{
				}
				break;
			}
			IEnumerator enumerator = this.ᜃ.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						TableRow tableRow = (TableRow)enumerator.Current;
						tableRow.CharacterFormat.IsChangedFormat = check;
						IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
						num = 2;
						continue;
					}
					case 1:
						goto IL_189;
					case 2:
						try
						{
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_130;
								case 2:
								{
									IEnumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num = 3;
										continue;
									}
									TableCell tableCell = (TableCell)enumerator2.Current;
									tableCell.CharacterFormat.IsChangedFormat = check;
									num = 0;
									continue;
								}
								case 3:
									num = 1;
									continue;
								}
								IL_E8:
								num = 2;
								continue;
								goto IL_E8;
							}
							IL_130:
							break;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator2;
								IDisposable disposable = enumerator2 as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_17C;
									case 2:
										goto IL_17A;
									}
									break;
								}
							}
							IL_17A:
							IL_17C:;
						}
						goto IL_17D;
					case 3:
						goto IL_17D;
					}
					IL_A1:
					num = 0;
					continue;
					goto IL_A1;
					IL_17D:
					num = 1;
				}
				IL_189:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable2 = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1D0;
						case 1:
							disposable2.Dispose();
							num = 0;
							continue;
						case 2:
							if (disposable2 != null)
							{
								num = 1;
								continue;
							}
							goto IL_1D2;
						}
						break;
					}
				}
				IL_1D0:
				IL_1D2:;
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00025488 File Offset: 0x00024488
		internal override void SetChangedPFormat(bool check)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IEnumerator enumerator = this.ᜃ.GetEnumerator();
			try
			{
				int num = 0;
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
						TableRow tableRow = (TableRow)enumerator.Current;
						tableRow.RowFormat.IsChangedFormat = check;
						num = 4;
						continue;
					}
					case 2:
						num = 3;
						continue;
					case 3:
						goto IL_A6;
					}
					IL_84:
					num = 1;
					continue;
					goto IL_84;
				}
				IL_A6:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E6;
						case 1:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_E8;
						case 2:
							disposable.Dispose();
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_E6:
				IL_E8:;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00025590 File Offset: 0x00024590
		protected override void InitXDLSHolder()
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("ᑥݧᵩὫ", a_), this.Rows);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000255F4 File Offset: 0x000245F4
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("ὪᑬὮᑰ", a_), ClipboardData.b("㽪౬൮ᵰᙲ", a_));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00025664 File Offset: 0x00024664
		protected override void CreateLayoutInfo()
		{
			for (;;)
			{
				this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Horizontal);
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ.ᜅ((base.NextSibling as Table).Rows[0].Cells[0].Paragraphs[0].Format.PageBreakBefore);
						num = 5;
						continue;
					case 1:
						goto IL_21A;
					case 2:
						return;
					case 3:
						goto IL_24A;
					case 4:
						if (this.\u1712.TextWrappingStyle != TextWrappingStyle.Inline)
						{
							num = 11;
							continue;
						}
						goto IL_277;
					case 5:
						goto IL_21A;
					case 6:
						if (base.NextSibling is Paragraph)
						{
							num = 3;
							continue;
						}
						num = 10;
						continue;
					case 7:
						goto IL_277;
					case 8:
						if (this.TableFormat.CellSpacing > 0f)
						{
							num = 9;
							continue;
						}
						return;
					case 9:
						this.ᜀ.ᜋ().ᜂ((double)(this.TableFormat.Borders.Left.LineWidth / 2f));
						this.ᜀ.ᜋ().ᜃ((double)(this.TableFormat.Borders.Right.LineWidth / 2f));
						this.ᜀ.ᜋ().ᜁ((double)(this.TableFormat.Borders.Top.LineWidth / 2f));
						this.ᜀ.ᜋ().ᜀ((double)(this.TableFormat.Borders.Bottom.LineWidth / 2f));
						num = 2;
						continue;
					case 10:
						if (base.NextSibling is Table)
						{
							num = 0;
							continue;
						}
						goto IL_21A;
					case 11:
						this.ᜀ.ᜂ(true);
						num = 7;
						continue;
					case 12:
						if (true)
						{
						}
						num = 4;
						continue;
					case 13:
						if (!this.ᜑ)
						{
							goto IL_277;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24A;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					}
					break;
					IL_21A:
					num = 8;
					continue;
					IL_24A:
					this.ᜀ.ᜅ((base.NextSibling as Paragraph).Format.PageBreakBefore);
					num = 1;
					continue;
					IL_277:
					num = 6;
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00025910 File Offset: 0x00024910
		spr\u2441 spr\u1AE4.TableLayoutInfo
		{
			get
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						goto IL_70;
					case 2:
						this.\u1713 = new Table.TableLayoutInfo(this);
						num = 1;
						continue;
					}
					IL_2E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						if (this.\u1713 != null)
						{
							goto IL_72;
						}
						num = 2;
						break;
					}
				}
				IL_70:
				IL_72:
				return this.\u1713;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00025998 File Offset: 0x00024998
		int spr\u1AE4.MaxRowIndex
		{
			get
			{
				int result;
				for (;;)
				{
					IL_24:
					int num = 0;
					result = 0;
					int num2 = 0;
					if (true)
					{
					}
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_60:
						goto IL_C7;
					default:
						if (false)
						{
						}
						num3 = 6;
						break;
					}
					for (;;)
					{
						IL_02:
						switch (num3)
						{
						case 0:
							if (num < this.ᜃ[num2].Cells.Count)
							{
								num3 = 5;
								continue;
							}
							goto IL_62;
						case 1:
							goto IL_6E;
						case 2:
							return result;
						case 3:
							goto IL_62;
						case 4:
							if (num2 >= this.ᜃ.Count)
							{
								num3 = 2;
								continue;
							}
							num3 = 0;
							continue;
						case 5:
							num = this.ᜃ[num2].Cells.Count;
							result = num2;
							num3 = 3;
							continue;
						case 6:
							goto IL_60;
						}
						goto IL_24;
						IL_62:
						num2++;
						num3 = 1;
					}
					IL_6E:
					IL_C7:
					num3 = 4;
					goto IL_02;
				}
				return result;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00025A94 File Offset: 0x00024A94
		int spr\u1AE4.ColumnsCount
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num;
					for (;;)
					{
						num = 0;
						int num2 = 0;
						int num3 = 10;
						for (;;)
						{
							switch (num3)
							{
							case 0:
							{
								int num4;
								if (num < num4)
								{
									num3 = 2;
									continue;
								}
								goto IL_F5;
							}
							case 1:
							{
								if (num2 >= this.ᜃ.Count)
								{
									num3 = 5;
									continue;
								}
								int num4 = 0;
								int num5 = 0;
								num3 = 4;
								continue;
							}
							case 2:
							{
								int num4;
								num = num4;
								num3 = 8;
								continue;
							}
							case 3:
								num3 = 0;
								continue;
							case 4:
								goto IL_123;
							case 5:
								return num;
							case 6:
								goto IL_123;
							case 7:
							{
								int num5;
								if (num5 >= this.ᜃ[num2].Cells.Count)
								{
									num3 = 3;
									continue;
								}
								int num4;
								num4 += this.ᜃ[num2].Cells[num5].Colspan;
								num5++;
								num3 = 6;
								continue;
							}
							case 8:
								goto IL_F5;
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
									goto IL_AE;
								}
								break;
							case 10:
								goto IL_AE;
							}
							break;
							IL_AE:
							num3 = 1;
							continue;
							IL_F5:
							num2++;
							num3 = 9;
							continue;
							IL_123:
							if (true)
							{
							}
							num3 = 7;
						}
					}
					return num;
				}
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00025C08 File Offset: 0x00024C08
		int spr\u1AE4.RowsCount
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
				return this.ᜃ.Count;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00025C50 File Offset: 0x00024C50
		spr\u17C8 spr\u1AE4.GetCellWidget(int row, int column)
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
			return this.ᜃ[row].Cells[column];
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00025CA4 File Offset: 0x00024CA4
		spr\u1AB8 spr\u1AE4.GetRowWidget(int row)
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
			return this.ᜃ[row];
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00025CEC File Offset: 0x00024CEC
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
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
			dc.ᜀ(this, ltWidget);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00025D30 File Offset: 0x00024D30
		private new void ᜀ()
		{
			int num = 11;
			for (;;)
			{
				float? num2;
				switch (num)
				{
				case 0:
					if (!this.ᜈ)
					{
						num = 2;
						continue;
					}
					goto IL_81;
				case 1:
					num2 = new float?(this.ᜉ.Value);
					num = 9;
					continue;
				case 2:
					this.ᜉ = new float?(num2.Value);
					num = 10;
					continue;
				case 3:
					goto IL_179;
				case 4:
					if (this.ᜈ)
					{
						num = 1;
						continue;
					}
					num = 15;
					continue;
				case 5:
					goto IL_179;
				case 6:
					num2 = new float?(base.Document.LastSection.PageSetup.ClientWidth / (float)this.ᜇ.Value);
					num = 7;
					continue;
				case 7:
					goto IL_201;
				case 8:
					num = 0;
					continue;
				case 9:
					goto IL_201;
				case 10:
					goto IL_81;
				case 11:
					if (true)
					{
					}
					break;
				case 12:
					return;
				case 13:
				{
					int num3;
					if (num3 >= this.ᜋ.Length)
					{
						num = 12;
						continue;
					}
					this.ᜋ[num3] = num2.Value;
					num3++;
					num = 3;
					continue;
				}
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_215;
					default:
					{
						if (false)
						{
						}
						this.ᜋ = new float[this.ᜇ.Value];
						int num3 = 0;
						num = 5;
						continue;
					}
					}
					break;
				case 15:
					if (base.Document.LastSection != null)
					{
						num = 6;
						continue;
					}
					goto IL_201;
				case 16:
					if (num2 != null)
					{
						goto IL_215;
					}
					return;
				case 17:
					return;
				case 18:
					if (!this.ᜊ)
					{
						num = 14;
						continue;
					}
					return;
				}
				if (this.ᜇ == null)
				{
					num = 17;
					continue;
				}
				num2 = null;
				num = 4;
				continue;
				IL_81:
				num = 18;
				continue;
				IL_179:
				num = 13;
				continue;
				IL_201:
				num = 16;
				continue;
				IL_215:
				num = 8;
			}
		}

		// Token: 0x04000C83 RID: 3203
		internal new const int ᜀ = 20;

		// Token: 0x04000C84 RID: 3204
		private const string ᜁ = "Normal Table";

		// Token: 0x04000C85 RID: 3205
		private const int ᜂ = 4094;

		// Token: 0x04000C86 RID: 3206
		private RowCollection ᜃ;

		// Token: 0x04000C87 RID: 3207
		private new RowFormat ᜄ;

		// Token: 0x04000C88 RID: 3208
		private float ᜅ;

		// Token: 0x04000C89 RID: 3209
		private List<float> ᜆ;

		// Token: 0x04000C8A RID: 3210
		private int? ᜇ;

		// Token: 0x04000C8B RID: 3211
		private bool ᜈ;

		// Token: 0x04000C8C RID: 3212
		private float? ᜉ;

		// Token: 0x04000C8D RID: 3213
		private bool ᜊ;

		// Token: 0x04000C8E RID: 3214
		private float[] ᜋ;

		// Token: 0x04000C8F RID: 3215
		private float[] ᜌ;

		// Token: 0x04000C90 RID: 3216
		private float? \u170D;

		// Token: 0x04000C91 RID: 3217
		private RectangleF ᜎ;

		// Token: 0x04000C92 RID: 3218
		private XmlTableFormat ᜏ;

		// Token: 0x04000C93 RID: 3219
		internal XmlTableFormat ᜐ;

		// Token: 0x04000C94 RID: 3220
		private bool ᜑ;

		// Token: 0x04000C95 RID: 3221
		internal TextBoxFormat \u1712;

		// Token: 0x04000C96 RID: 3222
		private spr\u2441 \u1713;

		// Token: 0x04000C97 RID: 3223
		private ArrayList \u1714;

		// Token: 0x04000C98 RID: 3224
		private bool \u1715;

		// Token: 0x04000C99 RID: 3225
		internal List<float> \u1716;

		// Token: 0x04000C9A RID: 3226
		internal List<float> \u1717;

		// Token: 0x04000C9B RID: 3227
		private spr\u2179 \u1718;

		// Token: 0x04000C9C RID: 3228
		private bool \u1719;

		// Token: 0x04000C9D RID: 3229
		private bool \u171A;

		// Token: 0x04000C9E RID: 3230
		private bool \u171B;

		// Token: 0x04000C9F RID: 3231
		private bool \u171C;

		// Token: 0x04000CA0 RID: 3232
		private bool \u171D;

		// Token: 0x04000CA1 RID: 3233
		private bool \u171E;

		// Token: 0x04000CA2 RID: 3234
		private Table.ᜀ \u171F;

		// Token: 0x04000CA3 RID: 3235
		private string ᜠ;

		// Token: 0x04000CA4 RID: 3236
		private string ᜡ;

		// Token: 0x04000CA5 RID: 3237
		internal bool ᜢ;

		// Token: 0x04000CA6 RID: 3238
		internal DocumentObject ᜣ;

		// Token: 0x04000CA7 RID: 3239
		internal RectangleF ᜤ;

		// Token: 0x04000CA8 RID: 3240
		private bool ᜥ;

		// Token: 0x04000CA9 RID: 3241
		[CompilerGenerated]
		private bool ᜦ;

		// Token: 0x04000CAA RID: 3242
		[CompilerGenerated]
		private spr\u1F89 ᜧ;

		// Token: 0x020000DF RID: 223
		protected class TableLayoutInfo : spr\u2441
		{
			// Token: 0x1700011F RID: 287
			// (get) Token: 0x0600033A RID: 826 RVA: 0x00025F8C File Offset: 0x00024F8C
			// (set) Token: 0x0600033B RID: 827 RVA: 0x00025FD0 File Offset: 0x00024FD0
			public float Width
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
					switch (1 == 1)
					{
					}
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜄ = value;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x0600033C RID: 828 RVA: 0x00026014 File Offset: 0x00025014
			public float Height
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
					return 0f;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x0600033D RID: 829 RVA: 0x00026054 File Offset: 0x00025054
			// (set) Token: 0x0600033E RID: 830 RVA: 0x00026098 File Offset: 0x00025098
			public float[] CellsWidth
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
					return this.ᜁ;
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
					this.ᜁ = value;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x0600033F RID: 831 RVA: 0x000260DC File Offset: 0x000250DC
			public int HeadersRowCount
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
					return this.ᜂ;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000340 RID: 832 RVA: 0x00026120 File Offset: 0x00025120
			public bool[] IsDefaultCells
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

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000341 RID: 833 RVA: 0x00026164 File Offset: 0x00025164
			public bool UseAbsolutePosition
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

			// Token: 0x06000342 RID: 834 RVA: 0x000261A8 File Offset: 0x000251A8
			public TableLayoutInfo(Table table)
			{
				this.ᜀ = table;
				int num = 0;
				int index = 0;
				for (int i = 0; i < this.ᜀ.Rows.Count; i++)
				{
					if (num < this.ᜀ.Rows[i].Cells.Count)
					{
						num = this.ᜀ.Rows[i].Cells.Count;
						index = i;
					}
				}
				this.ᜄ = ((this.ᜀ.Rows[0].Cells[0].WidthType == FtsWidth.Percentage) ? (this.ᜀ.Width / 20f) : this.ᜀ.Width);
				this.ᜁ = new float[num];
				this.ᜃ = new bool[num];
				int j = 0;
				int num2 = num;
				while (j < num2)
				{
					TableCell tableCell = this.ᜀ.Rows[index].Cells[j];
					this.ᜁ[j] = tableCell.Width;
					this.ᜃ[j] = tableCell.IsFixedWidth;
					j++;
				}
				this.ᜂ = this.ᜀ();
				this.ᜅ = this.ᜁ();
				if (!this.ᜅ && this.ᜀ.IsFrame)
				{
					Paddings paddings = this.ᜀ.TableFormat.Paddings;
					Paragraph paragraph = this.ᜀ.Rows[0].Cells[0].Paragraphs[0];
					this.ᜀ.TableFormat.Positioning.HorizRelationTo = (HorizontalRelation)paragraph.Format.FrameHorizontalPos;
					this.ᜀ.TableFormat.Positioning.HorizPositionAbs = HorizontalPosition.Left;
					this.ᜀ.TableFormat.Positioning.HorizPosition = (float)paragraph.Format.FrameX / 20f - paddings.Left;
					this.ᜀ.TableFormat.Positioning.VertRelationTo = (VerticalRelation)paragraph.Format.FrameVerticalPos;
					this.ᜀ.TableFormat.Positioning.VertPositionAbs = VerticalPosition.Top;
					this.ᜀ.TableFormat.Positioning.VertPosition = (float)paragraph.Format.FrameY / 20f - paddings.Top;
				}
			}

			// Token: 0x06000343 RID: 835 RVA: 0x0002643C File Offset: 0x0002543C
			private bool ᜁ()
			{
				for (;;)
				{
					RowFormat tableFormat = this.ᜀ.TableFormat;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return true;
						case 1:
							if (tableFormat.Sprms.ᜈ() > 0)
							{
								num = 2;
								continue;
							}
							goto IL_148;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7E;
							default:
								if (false)
								{
								}
								num = 14;
								continue;
							}
							break;
						case 3:
							num = 1;
							continue;
						case 4:
							if (!tableFormat.Sprms.ᜂ(37902))
							{
								num = 10;
								continue;
							}
							return true;
						case 5:
							if (true)
							{
							}
							goto IL_E0;
						case 6:
							if (tableFormat.Sprms != null)
							{
								num = 3;
								continue;
							}
							goto IL_148;
						case 7:
							return true;
						case 8:
							if (tableFormat.Sprms != null)
							{
								num = 13;
								continue;
							}
							goto IL_E0;
						case 9:
							if (tableFormat.WrapTextAround)
							{
								num = 0;
								continue;
							}
							return false;
						case 10:
							num = 15;
							continue;
						case 11:
							if (tableFormat.Sprms.ᜈ() == 0)
							{
								num = 5;
								continue;
							}
							return false;
						case 12:
							num = 4;
							continue;
						case 13:
							goto IL_7E;
						case 14:
							if (!tableFormat.Sprms.ᜂ(13837))
							{
								num = 12;
								continue;
							}
							return true;
						case 15:
							if (tableFormat.Sprms.ᜂ(37903))
							{
								num = 7;
								continue;
							}
							return false;
						}
						break;
						IL_7E:
						num = 11;
						continue;
						IL_E0:
						num = 9;
						continue;
						IL_148:
						num = 8;
					}
				}
				return false;
			}

			// Token: 0x06000344 RID: 836 RVA: 0x00026610 File Offset: 0x00025610
			private int ᜀ()
			{
				int num;
				for (;;)
				{
					for (;;)
					{
						num = 0;
						int num2 = 0;
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
							int num3 = 1;
							for (;;)
							{
								switch (num3)
								{
								case 0:
									return num;
								case 1:
									goto IL_54;
								case 2:
								{
									if (num2 >= this.ᜀ.Rows.Count)
									{
										num3 = 0;
										continue;
									}
									TableRow tableRow = this.ᜀ.Rows[num2];
									if (true)
									{
									}
									num3 = 4;
									continue;
								}
								case 3:
									goto IL_54;
								case 4:
								{
									TableRow tableRow;
									if (tableRow.IsHeader)
									{
										num3 = 5;
										continue;
									}
									return num;
								}
								case 5:
									num++;
									num2++;
									num3 = 3;
									continue;
								}
								break;
								IL_54:
								num3 = 2;
							}
							break;
						}
						}
					}
				}
				return num;
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000345 RID: 837 RVA: 0x000266E8 File Offset: 0x000256E8
			public double CellSpacings
			{
				get
				{
					for (;;)
					{
						if (true)
						{
						}
						if (!(this.ᜀ.Owner.Owner is TableCell))
						{
							goto IL_83;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_3F;
						}
					}
					IL_3F:
					if (false)
					{
					}
					double val = (double)((this.ᜀ.Owner.Owner as TableCell).OwnerRow.RowFormat.CellSpacing * 2f);
					return Math.Max(0.0, val);
					IL_83:
					return 0.0;
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000346 RID: 838 RVA: 0x00026784 File Offset: 0x00025784
			public double CellPaddings
			{
				get
				{
					while (this.ᜀ.Owner.Owner is TableCell)
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
							return (double)((this.ᜀ.Owner.Owner as TableCell).CellFormat.Paddings.Left + (this.ᜀ.Owner.Owner as TableCell).CellFormat.Paddings.Right);
						}
					}
					return 0.0;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000347 RID: 839 RVA: 0x0002682C File Offset: 0x0002582C
			// (set) Token: 0x06000348 RID: 840 RVA: 0x00026870 File Offset: 0x00025870
			public bool IsSplitted
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
					return this.ᜆ;
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
					this.ᜆ = value;
				}
			}

			// Token: 0x04000CAB RID: 3243
			private Table ᜀ;

			// Token: 0x04000CAC RID: 3244
			private float[] ᜁ;

			// Token: 0x04000CAD RID: 3245
			private int ᜂ;

			// Token: 0x04000CAE RID: 3246
			private bool[] ᜃ;

			// Token: 0x04000CAF RID: 3247
			private float ᜄ;

			// Token: 0x04000CB0 RID: 3248
			private bool ᜅ;

			// Token: 0x04000CB1 RID: 3249
			private bool ᜆ;
		}

		// Token: 0x020000E1 RID: 225
		internal new class ᜀ
		{
			// Token: 0x06000355 RID: 853 RVA: 0x000268B4 File Offset: 0x000258B4
			internal int ᜁ()
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

			// Token: 0x06000356 RID: 854 RVA: 0x000268F8 File Offset: 0x000258F8
			internal void ᜀ(int A_0)
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
				this.ᜀ = A_0;
			}

			// Token: 0x06000357 RID: 855 RVA: 0x0002693C File Offset: 0x0002593C
			internal FtsWidth ᜀ()
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
				return this.ᜁ;
			}

			// Token: 0x06000358 RID: 856 RVA: 0x00026980 File Offset: 0x00025980
			internal void ᜀ(FtsWidth A_0)
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
				this.ᜁ = A_0;
			}

			// Token: 0x04000CB2 RID: 3250
			private int ᜀ;

			// Token: 0x04000CB3 RID: 3251
			private FtsWidth ᜁ = FtsWidth.Auto;
		}
	}
}
