using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x02000224 RID: 548
	public class XlsShapeFill : XlsObject, spr\u1C26, IGradient
	{
		// Token: 0x06002115 RID: 8469 RVA: 0x0012915C File Offset: 0x0012815C
		static XlsShapeFill()
		{
			int a_ = 1;
			for (;;)
			{
				IL_09:
				switch (0)
				{
				default:
					for (;;)
					{
						byte[] array = new byte[4];
						array[0] = 100;
						XlsShapeFill.\u1713 = array;
						XlsShapeFill.\u1714 = new byte[]
						{
							206,
							byte.MaxValue,
							byte.MaxValue,
							byte.MaxValue
						};
						byte[] array2 = new byte[4];
						array2[0] = 50;
						XlsShapeFill.\u1715 = array2;
						byte[] array3 = new byte[4];
						array3[1] = 128;
						XlsShapeFill.\u1716 = array3;
						byte[] array4 = new byte[4];
						array4[2] = 1;
						XlsShapeFill.\u1717 = array4;
						XlsShapeFill.\u1718 = new byte[]
						{
							128,
							122,
							31,
							240
						};
						XlsShapeFill.DEF_COMENT_PARSE_COLOR = Color.FromArgb(255, 255, 255, 222);
						XlsShapeFill.\u1719 = Rectangle.FromLTRB(50000, 50000, 50000, 50000);
						XlsShapeFill.\u171A = new Rectangle[]
						{
							Rectangle.FromLTRB(0, 0, 100000, 100000),
							Rectangle.FromLTRB(100000, 0, 0, 100000),
							Rectangle.FromLTRB(0, 100000, 100000, 0),
							Rectangle.FromLTRB(100000, 100000, 0, 0)
						};
						XlsShapeFill.\u171B = new Dictionary<string, byte[]>();
						XlsShapeFill.ᜯ = typeof(XlsShapeFill).Assembly;
						XlsShapeFill.ᜰ = new byte[1320];
						int num = 0;
						int num2 = 1;
						if (true)
						{
						}
						int num3 = 1;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_1C6;
							case 1:
								goto IL_1C6;
							case 2:
							{
								if (num2 > 24)
								{
									num3 = 3;
									continue;
								}
								byte[] resData = XlsShapeFill.GetResData(RecordTableEnumerator.b("瀶䬸娺夼", a_) + num2.ToString());
								int num4 = resData.Length;
								XlsShapeFill.ᜰ[num] = (byte)num4;
								num++;
								resData.CopyTo(XlsShapeFill.ᜰ, num);
								num += num4;
								num2++;
								num3 = 0;
								continue;
							}
							case 3:
								return;
							}
							break;
							IL_1C6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								num3 = 2;
								break;
							}
						}
					}
					break;
				}
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x001293C0 File Offset: 0x001283C0
		public static byte[] GetResData(string strID)
		{
			int a_ = 4;
			int num = 1;
			byte[] array;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					ResourceManager resourceManager = new ResourceManager(RecordTableEnumerator.b("椹䰻圽㈿❁橃ṅ⑇㥉手്㽏⁑ㅓ硕W㙙⽛ᥝ቟͡cཥ൧ѩᡫ", a_), XlsShapeFill.ᜯ);
					array = (byte[])resourceManager.GetObject(strID);
					XlsShapeFill.\u171B[strID] = array;
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
				case 2:
					return array;
				}
				IL_25:
				if (true)
				{
				}
				if (!XlsShapeFill.\u171B.TryGetValue(strID, out array))
				{
					num = 0;
					continue;
				}
				break;
				goto IL_25;
			}
			return array;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0012947C File Offset: 0x0012847C
		internal static Color ᜀ(XlsWorkbook A_0, byte[] A_1)
		{
			int a_ = 4;
			int num = 3;
			Color result;
			for (;;)
			{
				Color color;
				switch (num)
				{
				case 0:
					goto IL_11F;
				case 1:
					goto IL_5E;
				case 2:
					if (A_0 == null)
					{
						num = 10;
						continue;
					}
					num = 9;
					continue;
				case 4:
					color = A_0.GetPaletteColor((ExcelColors)A_1[2]);
					goto IL_C5;
				case 5:
					num = 6;
					continue;
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
						if (A_1[2] != 80)
						{
							num = 8;
							continue;
						}
						num = 11;
						continue;
					}
					break;
				case 7:
					goto IL_D1;
				case 8:
					num = 4;
					continue;
				case 9:
					if (A_1[5] == 8)
					{
						num = 5;
						continue;
					}
					result = Color.FromArgb(255, (int)A_1[2], (int)A_1[3], (int)A_1[4]);
					num = 0;
					continue;
				case 10:
					goto IL_C0;
				case 11:
					color = XlsShapeFill.DEF_COMENT_PARSE_COLOR;
					goto IL_C5;
				}
				IL_49:
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num = 2;
				continue;
				goto IL_49;
				IL_C5:
				result = color;
				num = 7;
			}
			IL_5E:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰹崻刽㔿❁", a_));
			IL_C0:
			throw new ArgumentNullException(RecordTableEnumerator.b("堹医儽⬿", a_));
			IL_D1:
			return result;
			IL_11F:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x001295E8 File Offset: 0x001285E8
		internal static void ᜀ(XlsWorkbook A_0, byte[] A_1, OColor A_2)
		{
			int a_ = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 == null)
					{
						num = 7;
						continue;
					}
					num = 6;
					continue;
				case 2:
					goto IL_BC;
				case 3:
					num = 10;
					continue;
				case 4:
					goto IL_54;
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
						if (A_0 == null)
						{
							num = 2;
							continue;
						}
						num = 0;
						continue;
					}
					break;
				case 6:
					if (A_1[5] == 8)
					{
						num = 3;
						continue;
					}
					num = 8;
					continue;
				case 7:
					goto IL_182;
				case 8:
					if (A_1[5] == 16)
					{
						num = 11;
						continue;
					}
					goto IL_184;
				case 9:
					goto IL_153;
				case 10:
					if (A_1[2] == 80)
					{
						num = 9;
						continue;
					}
					goto IL_F4;
				case 11:
					goto IL_DE;
				}
				IL_49:
				if (A_1 == null)
				{
					num = 4;
					continue;
				}
				num = 5;
				continue;
				goto IL_49;
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("䀵夷嘹䤻嬽", a_));
			IL_BC:
			throw new ArgumentNullException(RecordTableEnumerator.b("吵圷唹圻", a_));
			IL_DE:
			A_2.ᜀ(Color.FromArgb(0, 0, 0, 0));
			return;
			IL_F4:
			A_2.SetKnownColor((ExcelColors)A_1[2]);
			return;
			IL_153:
			if (true)
			{
			}
			A_2.SetKnownColor((ExcelColors)80);
			return;
			IL_182:
			throw new ArgumentNullException(RecordTableEnumerator.b("唵圷嘹医䰽", a_));
			IL_184:
			A_2.ᜀ(Color.FromArgb(255, (int)A_1[2], (int)A_1[3], (int)A_1[4]), A_0);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00129794 File Offset: 0x00128794
		internal static GradientStops ᜀ(GradientPresetType A_0)
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
			byte[] presetGradientStopsData = XlsShapeFill.GetPresetGradientStopsData(A_0);
			return new GradientStops(presetGradientStopsData);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x001297E0 File Offset: 0x001287E0
		public static byte[] GetPresetGradientStopsData(GradientPresetType preset)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_68;
				case 2:
					XlsShapeFill.ᜀ();
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
				IL_1C:
				if (true)
				{
				}
				if (XlsShapeFill.ᜱ == null)
				{
					num = 2;
					continue;
				}
				break;
				goto IL_1C;
			}
			IL_68:
			return XlsShapeFill.ᜱ[preset];
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00129864 File Offset: 0x00128864
		internal XlsShapeFill(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜆ();
			this.ᜥ = new OColor(spr\u1D39.ᜂ);
			this.ᜤ = new OColor(spr\u1D39.ᜂ);
			this.ᜤ.AfterChange += this.ChangeVisible;
			this.ᜥ.AfterChange += this.ChangeVisible;
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00129940 File Offset: 0x00128940
		internal XlsShapeFill(spr\u1DF5 A_0, object A_1, ShapeFillType A_2) : this(A_0, A_1)
		{
			this.m_fillType = A_2;
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0012995C File Offset: 0x0012895C
		private void ᜆ()
		{
			int a_ = 19;
			this.ᜣ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜣ != null)
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
					return;
				}
			}
			if (true)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("ੈ⩊⍌ⅎ癐❒畔ㅖじ㕚㥜罞ᅠɢᝤɦݨὪ䵬൮ṰᱲṴ", a_));
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600211E RID: 8478 RVA: 0x001299DC File Offset: 0x001289DC
		internal GradientStops GradientStops
		{
			get
			{
				switch (0)
				{
				default:
				{
					GradientStops gradientStops;
					for (;;)
					{
						gradientStops = null;
						int num = 19;
						for (;;)
						{
							int num2;
							int num3;
							XlsGradientStop item;
							switch (num)
							{
							case 0:
								num2 = -1;
								goto IL_34A;
							case 1:
								gradientStops.InvertGradientStops();
								num = 22;
								continue;
							case 2:
								if (XlsShapeFill.IsDoubled(this.\u171C, this.\u171D))
								{
									num = 8;
									continue;
								}
								goto IL_291;
							case 3:
								num3 = -1;
								goto IL_3A3;
							case 4:
								if (this.ᜠ == GradientColorType.OneColor)
								{
									num = 12;
									continue;
								}
								num = 26;
								continue;
							case 5:
								num = 0;
								continue;
							case 6:
								num = 3;
								continue;
							case 7:
								if (this.ᜮ)
								{
									num = 9;
									continue;
								}
								return gradientStops;
							case 8:
								gradientStops.DoubleGradientStops();
								num = 17;
								continue;
							case 9:
								num = 24;
								continue;
							case 10:
								if (XlsShapeFill.IsInverted(this.\u171C, this.\u171D))
								{
									num = 1;
									continue;
								}
								goto IL_14F;
							case 11:
							{
								byte b;
								num2 = (int)b * 100000 / 255;
								goto IL_34A;
							}
							case 12:
							{
								gradientStops = new GradientStops();
								item = new XlsGradientStop(this.ForeColorObject, 0, 100000);
								gradientStops.Add(item);
								byte b = (byte)(this.ᜪ * 255.0);
								int num4 = 127;
								num = 20;
								continue;
							}
							case 13:
								goto IL_370;
							case 14:
							{
								byte b;
								num3 = (int)(byte.MaxValue - b) * 100000 / 255;
								goto IL_3A3;
							}
							case 15:
								return gradientStops;
							case 16:
							{
								byte b;
								int num4;
								if ((int)b <= num4)
								{
									num = 6;
									continue;
								}
								num = 14;
								continue;
							}
							case 17:
								goto IL_291;
							case 18:
								goto IL_370;
							case 19:
								goto IL_95;
							case 20:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_95;
								default:
								{
									if (false)
									{
									}
									byte b;
									int num4;
									if ((int)b > num4)
									{
										num = 5;
										continue;
									}
									num = 11;
									continue;
								}
								}
								break;
							case 21:
								goto IL_370;
							case 22:
								if (true)
								{
								}
								goto IL_14F;
							case 23:
								gradientStops = XlsShapeFill.ᜀ(this.ᜦ);
								num = 21;
								continue;
							case 24:
								if (this.ᜠ == GradientColorType.Preset)
								{
									num = 23;
									continue;
								}
								num = 4;
								continue;
							case 25:
							{
								gradientStops = new GradientStops();
								XlsGradientStop item2 = new XlsGradientStop(this.ForeColorObject, 0, 100000);
								gradientStops.Add(item2);
								item2 = new XlsGradientStop(this.BackColorObject, 100000, 100000);
								gradientStops.Add(item2);
								num = 18;
								continue;
							}
							case 26:
								if (this.ᜠ == GradientColorType.TwoColor)
								{
									num = 25;
									continue;
								}
								goto IL_370;
							case 27:
								num = 7;
								continue;
							}
							break;
							IL_95:
							if (this.m_fillType == ShapeFillType.Gradient)
							{
								num = 27;
								continue;
							}
							return gradientStops;
							IL_14F:
							num = 2;
							continue;
							IL_291:
							gradientStops.Angle = XlsShapeFill.ᜁ(this.\u171C);
							gradientStops.FillToRect = XlsShapeFill.ᜀ(this.\u171C, this.\u171D);
							gradientStops.GradientType = XlsShapeFill.ᜀ(this.\u171C);
							num = 15;
							continue;
							IL_34A:
							int shade = num2;
							num = 16;
							continue;
							IL_370:
							num = 10;
							continue;
							IL_3A3:
							int tint = num3;
							item = new XlsGradientStop(this.ForeColorObject, 100000, 100000, tint, shade);
							gradientStops.Add(item);
							num = 13;
						}
					}
					return gradientStops;
				}
				}
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x00129DC0 File Offset: 0x00128DC0
		// (set) Token: 0x06002120 RID: 8480 RVA: 0x00129E04 File Offset: 0x00128E04
		public bool Tile
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
				return this.ᜬ;
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
				this.ᜬ = value;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x00129E48 File Offset: 0x00128E48
		// (set) Token: 0x06002122 RID: 8482 RVA: 0x00129E8C File Offset: 0x00128E8C
		public GradientStops PreservedGradient
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
				return this.ᜭ;
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
				this.ᜭ = value;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x00129ED0 File Offset: 0x00128ED0
		internal spr\u23E7.ᜀ ParsePictureData
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
				return this.ᜫ;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x00129F14 File Offset: 0x00128F14
		// (set) Token: 0x06002125 RID: 8485 RVA: 0x00129F58 File Offset: 0x00128F58
		public bool IsGradientSupported
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
				return this.ᜮ;
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
				this.ᜮ = value;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x00129F9C File Offset: 0x00128F9C
		// (set) Token: 0x06002127 RID: 8487 RVA: 0x00129FE0 File Offset: 0x00128FE0
		public ShapeFillType FillType
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
				return this.m_fillType;
			}
			set
			{
				int a_ = 12;
				int num = 10;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_112;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_112;
						case 1:
							goto IL_77;
						case 2:
							if (true)
							{
							}
							goto IL_B5;
						case 3:
							this.\u171D = GradientVariantsType.ShadingVariants1;
							num = 2;
							continue;
						case 4:
							if (value == ShapeFillType.Gradient)
							{
								num = 3;
								continue;
							}
							goto IL_B5;
						case 5:
							num = 7;
							continue;
						case 6:
							goto IL_E9;
						case 7:
							if (value == ShapeFillType.Picture)
							{
								num = 6;
								continue;
							}
							num = 0;
							continue;
						case 8:
							this.ᜢ = GradientTextureType.Papyrus;
							num = 1;
							continue;
						case 9:
							goto IL_CD;
						}
						if (this.FillType != value)
						{
							num = 5;
							continue;
						}
						return;
						IL_B5:
						this.m_fillType = value;
						this.ChangeVisible();
						num = 9;
						continue;
					}
					IL_77:
					num = 4;
					continue;
					IL_112:
					if (value != ShapeFillType.Texture)
					{
						goto IL_77;
					}
					num = 8;
				}
				IL_CD:
				return;
				IL_E9:
				throw new ArgumentException(RecordTableEnumerator.b("с⭃㑅桇㥉⥋㩍灏≑㵓㕕ⱗ⽙⹛㭝䁟ᙡᵣᙥ൧䩩ᥫᵭᕯ剱ⅳյᵷࡹⱻ᝽ꪉ晴ﮓ뚗", a_));
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x0012A120 File Offset: 0x00129120
		// (set) Token: 0x06002129 RID: 8489 RVA: 0x0012A168 File Offset: 0x00129168
		public GradientStyleType GradientStyle
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
				this.ᜅ();
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
				this.ᜅ();
				this.\u171C = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600212A RID: 8490 RVA: 0x0012A1B8 File Offset: 0x001291B8
		// (set) Token: 0x0600212B RID: 8491 RVA: 0x0012A200 File Offset: 0x00129200
		public GradientVariantsType GradientVariant
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
				this.ᜅ();
				return this.\u171D;
			}
			set
			{
				int a_ = 12;
				for (;;)
				{
					IL_4D:
					this.ᜅ();
					int num = 4;
					for (;;)
					{
						bool flag;
						bool flag2;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								if (this.\u171C == GradientStyleType.From_Center)
								{
									num = 1;
									continue;
								}
								goto IL_E5;
							case 1:
								num = 7;
								continue;
							case 2:
								goto IL_7C;
							case 3:
								goto IL_BD;
							case 4:
								if (value != GradientVariantsType.ShadingVariants3)
								{
									num = 6;
									continue;
								}
								if (true)
								{
								}
								num = 5;
								continue;
							case 5:
								flag = true;
								goto IL_C3;
							case 6:
								num = 3;
								continue;
							case 7:
								if (flag2)
								{
									num = 2;
									continue;
								}
								goto IL_E5;
							}
							goto IL_4D;
						}
						IL_C3:
						flag2 = flag;
						num = 0;
						continue;
						IL_BD:
						flag = (value == GradientVariantsType.ShadingVariants4);
						goto IL_C3;
					}
				}
				IL_7C:
				throw new NotSupportedException(RecordTableEnumerator.b("ᙁⱃ⽅㭇橉㩋⽍≏㭑㕓㡕ⱗ穙㡛ㅝ՟ᅡ੣䅥ᱧ䩩Ὣ᭭oɱ᭳ѵ౷婹ύ᭽ꢇ黎ﮑ望뢗첟잡誣", a_));
				IL_E5:
				this.\u171D = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x0012A300 File Offset: 0x00129300
		// (set) Token: 0x0600212D RID: 8493 RVA: 0x0012A348 File Offset: 0x00129348
		public virtual double TransparencyTo
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
				this.ᜅ();
				return this.\u171E;
			}
			set
			{
				int a_ = 4;
				for (;;)
				{
					IL_29:
					this.ᜅ();
					for (;;)
					{
						IL_2F:
						int num = 3;
						for (;;)
						{
							if (true)
							{
							}
							switch (num)
							{
							case 0:
								goto IL_A6;
							case 1:
								if (value <= 1.0)
								{
									goto IL_A8;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_2F;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 2:
								num = 1;
								continue;
							case 3:
								if (value >= 0.0)
								{
									num = 2;
									continue;
								}
								goto IL_4D;
							}
							goto IL_29;
						}
					}
				}
				IL_4D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("渹主弽⸿ㅁ㑃❅㩇⽉≋ⵍ⥏ّ㭓", a_));
				IL_A6:
				goto IL_4D;
				IL_A8:
				this.\u171E = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0012A40C File Offset: 0x0012940C
		// (set) Token: 0x0600212F RID: 8495 RVA: 0x0012A450 File Offset: 0x00129450
		public virtual double TransparencyFrom
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
				return this.\u171F;
			}
			set
			{
				int a_ = 4;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value > 1.0)
						{
							goto IL_83;
						}
						goto IL_8D;
					case 2:
						goto IL_8B;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_83;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (value >= 0.0)
					{
						num = 3;
						continue;
					}
					break;
					IL_83:
					num = 2;
				}
				IL_3F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("渹主弽⸿ㅁ㑃❅㩇⽉≋ⵍ⥏ᑑ♓㥕㕗", a_));
				IL_8B:
				goto IL_3F;
				IL_8D:
				if (true)
				{
				}
				this.\u171F = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x0012A50C File Offset: 0x0012950C
		// (set) Token: 0x06002131 RID: 8497 RVA: 0x0012A554 File Offset: 0x00129554
		public double Transparency
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
				this.ᜁ();
				return this.\u171F;
			}
			set
			{
				int a_ = 19;
				for (;;)
				{
					IL_21:
					this.ᜁ();
					for (;;)
					{
						IL_27:
						if (true)
						{
						}
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_A6;
							case 1:
								if (value <= 1.0)
								{
									goto IL_A8;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_27;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 2:
								num = 1;
								continue;
							case 3:
								if (value >= 0.0)
								{
									num = 2;
									continue;
								}
								goto IL_4D;
							}
							goto IL_21;
						}
					}
				}
				IL_4D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ᵈ㥊ⱌⅎ≐⍒㑔╖㱘㕚㹜♞䅠੢ᙤ䝦٨Ṫᥬ佮Ṱᕲ啴նᡸᕺ᩼᩾", a_));
				IL_A6:
				goto IL_4D;
				IL_A8:
				this.\u171F = value;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0012A610 File Offset: 0x00129610
		// (set) Token: 0x06002133 RID: 8499 RVA: 0x0012A658 File Offset: 0x00129658
		public GradientColorType GradientColorType
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
				this.ᜅ();
				return this.ᜠ;
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
				this.ᜅ();
				this.ᜠ = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0012A6A8 File Offset: 0x001296A8
		// (set) Token: 0x06002135 RID: 8501 RVA: 0x0012A6F0 File Offset: 0x001296F0
		public GradientPatternType Pattern
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
				this.ᜃ();
				return this.ᜡ;
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
				this.m_fillType = ShapeFillType.Pattern;
				this.ᜡ = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x0012A740 File Offset: 0x00129740
		// (set) Token: 0x06002137 RID: 8503 RVA: 0x0012A788 File Offset: 0x00129788
		public GradientTextureType Texture
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
				this.ᜂ();
				return this.ᜢ;
			}
			set
			{
				int a_ = 19;
				if (this.ᜢ == GradientTextureType.UserDefined)
				{
					if (true)
					{
					}
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
						this.m_fillType = ShapeFillType.Texture;
						this.ᜢ = value;
						this.ChangeVisible();
						return;
					}
				}
				throw new ArgumentException(RecordTableEnumerator.b("ᵈ⍊⑌㱎煐㹒ご⍖ㅘ㑚㥜罞በᙢᕤᝦ٨ᥪᥬ佮Ṱᵲᥴ๶奸୺ོ᩾Ꞇﶈﮎ", a_));
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x0012A800 File Offset: 0x00129800
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x0012A84C File Offset: 0x0012984C
		public ExcelColors BackKnownColor
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
				return this.BackColorObject.ᜂ(this.ᜣ);
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
				this.BackColorObject.SetKnownColor(value);
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x0012A894 File Offset: 0x00129894
		// (set) Token: 0x0600213B RID: 8507 RVA: 0x0012A8E0 File Offset: 0x001298E0
		public ExcelColors ForeKnownColor
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
				return this.ForeColorObject.ᜂ(this.ᜣ);
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
				this.ForeColorObject.SetKnownColor(value);
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0012A928 File Offset: 0x00129928
		// (set) Token: 0x0600213D RID: 8509 RVA: 0x0012A974 File Offset: 0x00129974
		public virtual Color BackColor
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
				return this.BackColorObject.ᜁ(this.ᜣ);
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
				this.BackColorObject.ᜀ(value, this.ᜣ);
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0012A9C4 File Offset: 0x001299C4
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x0012AA10 File Offset: 0x00129A10
		public virtual Color ForeColor
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
				return this.ForeColorObject.ᜁ(this.ᜣ);
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
				this.ForeColorObject.ᜀ(value, this.ᜣ);
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0012AA60 File Offset: 0x00129A60
		public virtual OColor BackColorObject
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
				return this.ᜤ;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0012AAA4 File Offset: 0x00129AA4
		public virtual OColor ForeColorObject
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
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0012AAE8 File Offset: 0x00129AE8
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x0012AB58 File Offset: 0x00129B58
		public GradientPresetType PresetGradientType
		{
			get
			{
				int a_ = 19;
				this.ᜅ();
				if (this.ᜠ == GradientColorType.Preset)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1A;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					return this.ᜦ;
				}
				IL_1A:
				throw new NotSupportedException(RecordTableEnumerator.b("ᵈ⍊⑌㱎煐⍒❔㡖⥘㹚⽜⭞ᡠ䍢ᙤቦᥨ᭪ɬᵮհᙲᅴ坶ᙸᕺᅼپꆀꞆ敖랖쒠힢薤쒦욨잪슬\uddae醰잲체잶\udcb8閺", a_));
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
				this.ᜅ();
				this.ᜠ = GradientColorType.Preset;
				this.ᜦ = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0012ABB0 File Offset: 0x00129BB0
		public Image Picture
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
				this.ᜄ();
				return this.m_picture;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x0012ABF8 File Offset: 0x00129BF8
		public string PictureName
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
				this.ᜄ();
				return this.ᜧ;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0012AC40 File Offset: 0x00129C40
		// (set) Token: 0x06002147 RID: 8519 RVA: 0x0012AC84 File Offset: 0x00129C84
		public virtual bool Visible
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
				return this.ᜨ;
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
						this.ᜨ = value;
						num = 1;
						continue;
					}
					if (this.Visible == value)
					{
						break;
					}
					if (true)
					{
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0012AD00 File Offset: 0x00129D00
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x0012AD70 File Offset: 0x00129D70
		public double GradientDegree
		{
			get
			{
				int a_ = 9;
				this.ᜅ();
				if (this.ᜠ == GradientColorType.OneColor)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_19;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					return this.ᜪ;
				}
				IL_19:
				throw new NotSupportedException(RecordTableEnumerator.b("款⥀⩂㙄杆㥈㥊≌㽎㑐⅒⅔⹖祘⡚⡜⽞ᅠౢᝤ፦ᩨ䭪ɬŮᵰੲ啴Ṷὸ孺Ṽ᝾ꮊ뎒뾞욠톢쒤쎦삨캪쎬\udbae", a_));
			}
			set
			{
				int a_ = 16;
				for (;;)
				{
					this.ᜅ();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜠ != GradientColorType.OneColor)
							{
								num = 5;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_CD;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 1:
							num = 2;
							continue;
						case 2:
							if (value > 1.0)
							{
								num = 3;
								continue;
							}
							goto IL_E1;
						case 3:
							goto IL_6F;
						case 4:
							if (value >= 0.0)
							{
								num = 1;
								continue;
							}
							goto IL_CD;
						case 5:
							goto IL_51;
						}
						break;
					}
				}
				IL_51:
				if (true)
				{
				}
				throw new NotSupportedException(RecordTableEnumerator.b("ቅ⁇⍉㽋湍⁏⁑㭓♕㵗⡙⡛❝䁟ᅡᅣᙥᡧթṫᩭͯ剱᭳ᡵᑷ͹屻᝽ꊁ늑ﮓﶗ몙ﾛ첟춡횣蚥쾧\ud8a9춫쪭\ud9afힱ\udab3습", a_));
				IL_6F:
				IL_CD:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("Ņ㩇⭉⡋❍㕏㱑⁓癕㱗㽙㭛ⱝ՟ݡ䑣ཥ᭧䩩ͫ᭭ѯ剱᭳ၵ塷ࡹᵻၽꪃ", a_));
				IL_E1:
				this.ᜪ = value;
				this.ChangeVisible();
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0012AE6C File Offset: 0x00129E6C
		public void CustomPicture(string path)
		{
			int a_ = 14;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					num = 5;
					continue;
				case 2:
					if (!File.Exists(path))
					{
						num = 0;
						continue;
					}
					goto IL_CE;
				case 4:
					goto IL_B8;
				case 5:
					if (path.Length == 0)
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				if (path == null)
				{
					goto IL_86;
				}
				num = 1;
			}
			IL_84:
			throw new FileNotFoundException(RecordTableEnumerator.b("Ƀ⽅⑇⽉汋⩍㽏㝑❓㡕罗⹙籛㭝ᡟୡᝣብ䙧", a_));
			IL_86:
			throw new ArgumentException(RecordTableEnumerator.b("ᑃ❅㱇≉汋ⵍㅏ㱑㭓≕硗㡙㥛繝๟ᝡࡣ੥䡧թṫ乭ᕯάѳɵŷ呹", a_));
			IL_B8:
			goto IL_86;
			IL_CE:
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			this.CustomPicture(Image.FromFile(path), fileNameWithoutExtension);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0012AF5C File Offset: 0x00129F5C
		public void CustomPicture(Image im, string name)
		{
			int a_ = 15;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (name.Length == 0)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_7F;
				case 2:
					if (im == null)
					{
						num = 1;
						continue;
					}
					goto IL_C6;
				case 3:
					goto IL_B0;
				case 5:
					num = 0;
					continue;
				}
				if (true)
				{
				}
				if (name == null)
				{
					goto IL_81;
				}
				num = 5;
			}
			IL_7F:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱄ⩆", a_));
			IL_81:
			throw new ArgumentException(RecordTableEnumerator.b("⭄♆⑈⹊浌ⱎぐ㵒㩔⍖祘㥚㡜罞འᙢ।୦䥨ѪὬ佮ᑰṲմͶx啺", a_));
			IL_B0:
			goto IL_81;
			IL_C6:
			this.m_fillType = ShapeFillType.Picture;
			this.m_picture = im;
			this.ᜧ = name;
			this.ChangeVisible();
			this.ᜩ = this.SetPictureToBse(im, name);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0012B058 File Offset: 0x0012A058
		public void CustomTexture(string path)
		{
			int a_ = 3;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_AD;
				case 2:
					num = 5;
					continue;
				case 3:
					goto IL_7C;
				case 4:
					if (!File.Exists(path))
					{
						num = 3;
						continue;
					}
					goto IL_C3;
				case 5:
					if (path.Length == 0)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				if (path == null)
				{
					goto IL_7E;
				}
				num = 2;
			}
			IL_7C:
			throw new FileNotFoundException(RecordTableEnumerator.b("缸刺儼娾慀❂⩄≆㩈╊橌㭎煐㙒ⵔ㹖⩘⽚獜", a_));
			IL_7E:
			throw new ArgumentException(RecordTableEnumerator.b("䤸娺䤼圾慀⁂⑄⥆♈㽊浌ⵎ㑐獒㭔≖㕘㝚絜ぞ፠䍢d੦ᥨὪᑬ䅮", a_));
			IL_AD:
			goto IL_7E;
			IL_C3:
			if (true)
			{
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
			this.CustomTexture(Image.FromFile(path), fileNameWithoutExtension);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0012B144 File Offset: 0x0012A144
		public void CustomTexture(Image im, string name)
		{
			int a_ = 12;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B0;
				case 2:
					goto IL_77;
				case 3:
					num = 4;
					continue;
				case 4:
					if (name.Length == 0)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 5:
					if (im == null)
					{
						num = 2;
						continue;
					}
					goto IL_C6;
				}
				if (name == null)
				{
					goto IL_81;
				}
				num = 3;
			}
			IL_77:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⭁⥃", a_));
			IL_81:
			throw new ArgumentException(RecordTableEnumerator.b("ⱁ╃⭅ⵇ橉⽋⽍㹏㵑⁓癕㩗㽙籛そᕟ๡ࡣ䙥ݧᡩ䱫୭ᵯɱsཱུ噷", a_));
			IL_B0:
			goto IL_81;
			IL_C6:
			this.m_fillType = ShapeFillType.Texture;
			this.ᜢ = GradientTextureType.UserDefined;
			this.m_picture = im;
			this.ᜧ = name;
			this.ChangeVisible();
			this.ᜩ = this.SetPictureToBse(im, name);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0012B248 File Offset: 0x0012A248
		public void Patterned(GradientPatternType pattern)
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
			this.Pattern = pattern;
			this.ChangeVisible();
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0012B290 File Offset: 0x0012A290
		public void PresetGradient(GradientPresetType grad)
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
			this.PresetGradient(grad, GradientStyleType.Horizontal);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0012B2D4 File Offset: 0x0012A2D4
		public void PresetGradient(GradientPresetType grad, GradientStyleType shadStyle)
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
			this.PresetGradient(grad, shadStyle, GradientVariantsType.ShadingVariants1);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0012B318 File Offset: 0x0012A318
		public void PresetGradient(GradientPresetType grad, GradientStyleType shadStyle, GradientVariantsType shadVar)
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_69;
				case 2:
					if (shadVar > GradientVariantsType.ShadingVariants2)
					{
						num = 1;
						continue;
					}
					goto IL_6B;
				case 3:
					num = 2;
					continue;
				}
				goto IL_29;
				IL_2D:
				num = 3;
				continue;
				IL_29:
				if (shadStyle == GradientStyleType.From_Center)
				{
					goto IL_2D;
				}
				IL_6B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					goto IL_81;
				}
			}
			IL_69:
			throw new ArgumentException(RecordTableEnumerator.b("簹主儽ⴿ扁❃⍅♇㹉㹋湍⍏♑ⵓ㩕㵗穙⽛⭝ၟቡୣᑥᱧ䩩ͫmᱯୱ味u᥷ࡹ⍻佽ꁿꚅﺇﺋ톍ꊏ", a_));
			IL_81:
			if (true)
			{
			}
			if (false)
			{
			}
			this.m_fillType = ShapeFillType.Gradient;
			this.ᜠ = GradientColorType.Preset;
			this.ᜦ = grad;
			this.\u171C = shadStyle;
			this.\u171D = shadVar;
			this.ᜮ = true;
			this.ChangeVisible();
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0012B3E4 File Offset: 0x0012A3E4
		public void PresetTextured(GradientTextureType texture)
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
			this.Texture = texture;
			this.ChangeVisible();
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0012B42C File Offset: 0x0012A42C
		public void TwoColorGradient()
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
			this.TwoColorGradient(GradientStyleType.Horizontal);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0012B470 File Offset: 0x0012A470
		public void TwoColorGradient(GradientStyleType style)
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
			this.TwoColorGradient(style, GradientVariantsType.ShadingVariants1);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0012B4B4 File Offset: 0x0012A4B4
		public void TwoColorGradient(GradientStyleType style, GradientVariantsType variant)
		{
			int a_ = 8;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 2:
					if (variant > GradientVariantsType.ShadingVariants2)
					{
						num = 3;
						continue;
					}
					goto IL_6B;
				case 3:
					goto IL_69;
				}
				goto IL_29;
				IL_2D:
				num = 0;
				continue;
				IL_29:
				if (style == GradientStyleType.From_Center)
				{
					goto IL_2D;
				}
				IL_6B:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					goto IL_89;
				}
			}
			IL_69:
			throw new ArgumentException(RecordTableEnumerator.b("砽㈿ⵁ⥃晅⭇⽉≋㩍≏牑❓≕⅗㙙㥛繝፟ᝡᑣᙥݧᡩᡫ乭Ὧᱱᡳཱུ塷౹ᵻ౽\udf7f뎁ꒃ慎ꪉ懲춑ꚓ", a_));
			IL_89:
			if (false)
			{
			}
			this.m_fillType = ShapeFillType.Gradient;
			this.ᜠ = GradientColorType.TwoColor;
			this.\u171C = style;
			this.\u171D = variant;
			this.ᜮ = true;
			this.ChangeVisible();
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0012B57C File Offset: 0x0012A57C
		public void OneColorGradient()
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
			this.OneColorGradient(GradientStyleType.Horizontal);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0012B5C0 File Offset: 0x0012A5C0
		public void OneColorGradient(GradientStyleType style)
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
			this.OneColorGradient(style, GradientVariantsType.ShadingVariants1);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0012B604 File Offset: 0x0012A604
		public void OneColorGradient(GradientStyleType style, GradientVariantsType variant)
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					if (true)
					{
					}
					num = 3;
					continue;
				case 3:
					if (variant > GradientVariantsType.ShadingVariants2)
					{
						num = 0;
						continue;
					}
					goto IL_73;
				}
				goto IL_29;
				IL_2D:
				num = 1;
				continue;
				IL_29:
				if (style == GradientStyleType.From_Center)
				{
					goto IL_2D;
				}
				IL_73:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					goto IL_89;
				}
			}
			IL_71:
			throw new ArgumentException(RecordTableEnumerator.b("縷䠹医匽怿⅁⅃⡅㱇㡉汋㵍⑏⭑㡓㍕硗⥙⥛⹝ၟൡᙣብ䡧թɫɭ९剱ɳ᝵੷╹䵻幽ꒃ펋벍", a_));
			IL_89:
			if (false)
			{
			}
			this.m_fillType = ShapeFillType.Gradient;
			this.ᜠ = GradientColorType.OneColor;
			this.\u171C = style;
			this.\u171D = variant;
			this.ᜮ = true;
			this.ChangeVisible();
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0012B6CC File Offset: 0x0012A6CC
		public void Solid()
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
			this.m_fillType = ShapeFillType.SolidColor;
			this.ChangeVisible();
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0012B714 File Offset: 0x0012A714
		public int CompareTo(IGradient twin)
		{
			int num = 13;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				switch (num)
				{
				case 0:
					num2 = 1;
					goto IL_167;
				case 1:
					num = 4;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_268;
					default:
						if (false)
						{
						}
						if (num3 != 0)
						{
							num = 6;
							continue;
						}
						num = 23;
						continue;
					}
					break;
				case 3:
					return 1;
				case 4:
					num4 = 1;
					goto IL_199;
				case 5:
					return num3;
				case 6:
					return num3;
				case 7:
					num5 = 1;
					goto IL_23E;
				case 8:
					goto IL_268;
				case 9:
					if (true)
					{
					}
					num = 24;
					continue;
				case 10:
					return num3;
				case 11:
					if (num3 != 0)
					{
						num = 19;
						continue;
					}
					return num3;
				case 12:
					if (!(this.ᜤ == twin.BackColorObject))
					{
						num = 9;
						continue;
					}
					num = 15;
					continue;
				case 14:
					if (num3 != 0)
					{
						num = 5;
						continue;
					}
					num = 21;
					continue;
				case 15:
					num6 = 0;
					goto IL_E6;
				case 16:
					if (num3 != 0)
					{
						num = 10;
						continue;
					}
					num = 12;
					continue;
				case 17:
					if (this.\u171C != twin.GradientStyle)
					{
						num = 1;
						continue;
					}
					num = 8;
					continue;
				case 18:
					num = 0;
					continue;
				case 19:
					return num3;
				case 20:
					num = 7;
					continue;
				case 21:
					if (this.\u171D != twin.GradientVariant)
					{
						num = 18;
						continue;
					}
					num = 22;
					continue;
				case 22:
					num2 = 0;
					goto IL_167;
				case 23:
					if (!(this.ᜥ == twin.ForeColorObject))
					{
						num = 20;
						continue;
					}
					num = 25;
					continue;
				case 24:
					num6 = 1;
					goto IL_E6;
				case 25:
					num5 = 0;
					goto IL_23E;
				}
				if (twin == null)
				{
					num = 3;
					continue;
				}
				num = 17;
				continue;
				IL_E6:
				num3 = num6;
				num = 2;
				continue;
				IL_167:
				num3 = num2;
				num = 16;
				continue;
				IL_199:
				num3 = num4;
				num = 14;
				continue;
				IL_268:
				num4 = 0;
				goto IL_199;
				IL_23E:
				num3 = num5;
				num = 11;
			}
			return 1;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0012B994 File Offset: 0x0012A994
		[CLSCompliant(false)]
		internal bool ᜀ(spr\u23E7.ᜀ A_0)
		{
			for (;;)
			{
				MsoOptions msoOptions = A_0.ᜈ();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 15;
						continue;
					case 1:
						if (msoOptions <= MsoOptions.PresetGradientData)
						{
							num = 12;
							continue;
						}
						num = 4;
						continue;
					case 2:
						goto IL_355;
					case 3:
					{
						byte[] array;
						if (array != null)
						{
							num = 10;
							continue;
						}
						return true;
					}
					case 4:
						if (msoOptions != MsoOptions.GradientColorType)
						{
							num = 28;
							continue;
						}
						goto IL_130;
					case 5:
						goto IL_21D;
					case 6:
						if (!A_0.ᜁ())
						{
							num = 17;
							continue;
						}
						return true;
					case 7:
						if (this.m_fillType == ShapeFillType.Texture)
						{
							num = 24;
							continue;
						}
						return true;
					case 8:
						if (this.m_fillType != ShapeFillType.Picture)
						{
							num = 16;
							continue;
						}
						goto IL_2D4;
					case 9:
						switch (msoOptions)
						{
						case MsoOptions.FillType:
							goto IL_E5;
						case MsoOptions.ForeColor:
							return true;
						case MsoOptions.Transparency:
							goto IL_172;
						case MsoOptions.BackColor:
							goto IL_14E;
						case MsoOptions.GradientTransparency:
							goto IL_3A6;
						case (MsoOptions)389:
						case (MsoOptions)392:
						case (MsoOptions)393:
						case (MsoOptions)394:
							return false;
						case MsoOptions.PatternTexture:
							num = 8;
							continue;
						case MsoOptions.PattTextName:
						{
							byte[] array = A_0.ᜄ();
							num = 6;
							continue;
						}
						case MsoOptions.ShadStyle:
							goto IL_245;
						case MsoOptions.ShadVariant:
							goto IL_32E;
						case MsoOptions.ShadingStyleCorner_1:
							num = 14;
							continue;
						case MsoOptions.ShadingStyleCorner_2:
							goto IL_162;
						default:
							num = 0;
							continue;
						}
						break;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A1;
						default:
							if (false)
							{
							}
							num = 21;
							continue;
						}
						break;
					case 11:
						if (A_0.ᜃ()[4] == 1)
						{
							num = 20;
							continue;
						}
						return true;
					case 12:
						goto IL_A1;
					case 13:
						num = 23;
						continue;
					case 14:
						if (this.\u171C == GradientStyleType.From_Corner)
						{
							num = 25;
							continue;
						}
						return true;
					case 15:
						if (msoOptions != MsoOptions.PresetGradientData)
						{
							num = 13;
							continue;
						}
						goto IL_A6;
					case 16:
						num = 7;
						continue;
					case 17:
						num = 3;
						continue;
					case 18:
						this.ᜃ(A_0.ᜄ());
						num = 2;
						continue;
					case 19:
						if (msoOptions != MsoOptions.NoFillHitTest)
						{
							num = 27;
							continue;
						}
						goto IL_13E;
					case 20:
						this.\u171D = GradientVariantsType.ShadingVariants2;
						num = 22;
						continue;
					case 21:
					{
						byte[] array;
						if (array.Length > 0)
						{
							num = 18;
							continue;
						}
						return true;
					}
					case 22:
						goto IL_105;
					case 23:
						goto IL_2F8;
					case 24:
						goto IL_2D4;
					case 25:
						num = 11;
						continue;
					case 26:
						goto IL_2E6;
					case 27:
						num = 5;
						continue;
					case 28:
						num = 19;
						continue;
					}
					break;
					IL_A1:
					num = 9;
					continue;
					IL_2D4:
					this.ᜫ = A_0;
					num = 26;
				}
			}
			IL_A6:
			this.ᜂ(A_0.ᜄ());
			return true;
			IL_E5:
			this.ᜁ(A_0.ᜆ());
			return true;
			IL_105:
			return true;
			IL_130:
			this.ᜀ(A_0.ᜆ());
			return true;
			IL_13E:
			this.ᜁ(A_0.ᜃ());
			return true;
			IL_14E:
			this.ᜪ = this.ᜀ(A_0.ᜃ());
			return true;
			IL_162:
			this.ᜀ(A_0.ᜃ()[4]);
			return true;
			IL_172:
			this.\u171F = XlsShapeLineFormat.ᜀ(A_0.ᜆ());
			return true;
			IL_21D:
			return false;
			IL_245:
			this.ᜄ(A_0.ᜃ());
			return true;
			IL_2E6:
			return true;
			IL_2F8:
			if (true)
			{
			}
			return false;
			IL_32E:
			this.ᜁ(A_0.ᜃ()[2]);
			return true;
			IL_355:
			return true;
			IL_3A6:
			this.\u171E = XlsShapeLineFormat.ᜀ(A_0.ᜆ());
			return true;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0012BD5C File Offset: 0x0012AD5C
		private void ᜁ(uint A_0)
		{
			for (;;)
			{
				this.m_fillType = (ShapeFillType)A_0;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_fillType = ShapeFillType.Gradient;
						this.\u171C = GradientStyleType.From_Center;
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BC;
						default:
							if (false)
							{
							}
							if (A_0 == 6U)
							{
								num = 0;
								continue;
							}
							return;
						}
						break;
					case 2:
						return;
					case 3:
						this.m_fillType = ShapeFillType.Gradient;
						this.\u171C = GradientStyleType.From_Corner;
						this.\u171D = GradientVariantsType.ShadingVariants1;
						num = 5;
						continue;
					case 4:
						if (A_0 == 5U)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_67;
					case 5:
						goto IL_BC;
					}
					break;
					IL_67:
					num = 1;
					continue;
					IL_BC:
					goto IL_67;
				}
			}
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0012BE28 File Offset: 0x0012AE28
		private void ᜄ(byte[] A_0)
		{
			int a_ = 2;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					byte b;
					if (b != 90)
					{
						num = 6;
						continue;
					}
					goto IL_E0;
				}
				case 2:
					goto IL_DB;
				case 3:
				{
					if (this.\u171C == GradientStyleType.From_Corner)
					{
						num = 2;
						continue;
					}
					byte b2 = A_0[4];
					byte b = b2;
					num = 12;
					continue;
				}
				case 4:
					num = 0;
					continue;
				case 5:
				{
					byte b;
					if (b != 166)
					{
						num = 11;
						continue;
					}
					goto IL_E0;
				}
				case 6:
					num = 13;
					continue;
				case 7:
					return;
				case 8:
					num = 3;
					continue;
				case 9:
				{
					byte b;
					if (b != 211)
					{
						num = 15;
						continue;
					}
					goto IL_1B5;
				}
				case 10:
					goto IL_67;
				case 11:
					num = 9;
					continue;
				case 12:
				{
					byte b;
					if (b <= 121)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				}
				case 13:
				{
					byte b;
					if (b != 121)
					{
						num = 7;
						continue;
					}
					goto IL_150;
				}
				case 14:
					if (this.\u171C != GradientStyleType.From_Center)
					{
						if (true)
						{
						}
						num = 8;
						continue;
					}
					return;
				case 15:
					return;
				}
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
						num = 14;
						continue;
					}
				}
				num = 10;
			}
			IL_67:
			throw new ArgumentNullException(RecordTableEnumerator.b("夷䠹主", a_));
			IL_DB:
			return;
			IL_E0:
			this.\u171C = GradientStyleType.Vertical;
			return;
			IL_150:
			this.\u171C = GradientStyleType.Diagonl_Up;
			return;
			IL_1B5:
			this.\u171C = GradientStyleType.Diagonl_Down;
			this.\u171D = GradientVariantsType.ShadingVariants1;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0012BFF8 File Offset: 0x0012AFF8
		private void ᜁ(byte A_0)
		{
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_10B;
				case 1:
					if (A_0 != 100)
					{
						num = 12;
						continue;
					}
					num = 3;
					continue;
				case 2:
					goto IL_1A7;
				case 3:
					goto IL_138;
				case 4:
					if (this.\u171C == GradientStyleType.From_Center)
					{
						num = 8;
						continue;
					}
					num = 14;
					continue;
				case 5:
					goto IL_15B;
				case 6:
					num = 1;
					continue;
				case 7:
					goto IL_185;
				case 8:
					num = 10;
					continue;
				case 9:
					return;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10B;
					default:
						goto IL_AA;
					}
					break;
				case 12:
					num = 0;
					continue;
				case 13:
					num = 2;
					continue;
				case 14:
					if (A_0 != 50)
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				case 15:
					num = 13;
					continue;
				}
				if (this.\u171C == GradientStyleType.From_Corner)
				{
					num = 9;
					continue;
				}
				num = 4;
				continue;
				IL_10B:
				if (A_0 != 206)
				{
					num = 15;
				}
				else
				{
					if (true)
					{
					}
					num = 7;
				}
			}
			return;
			IL_AA:
			if (false)
			{
			}
			this.\u171D = ((A_0 == 100) ? GradientVariantsType.ShadingVariants1 : GradientVariantsType.ShadingVariants2);
			return;
			IL_138:
			this.\u171D = ((this.\u171C == GradientStyleType.Diagonl_Down) ? GradientVariantsType.ShadingVariants2 : GradientVariantsType.ShadingVariants1);
			return;
			IL_15B:
			this.\u171D = ((this.\u171C == GradientStyleType.Horizontal) ? GradientVariantsType.ShadingVariants3 : GradientVariantsType.ShadingVariants4);
			return;
			IL_185:
			this.\u171D = ((this.\u171C == GradientStyleType.Horizontal) ? GradientVariantsType.ShadingVariants4 : GradientVariantsType.ShadingVariants3);
			return;
			IL_1A7:
			this.\u171D = ((this.\u171C == GradientStyleType.Diagonl_Down) ? GradientVariantsType.ShadingVariants1 : GradientVariantsType.ShadingVariants2);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0012C1C4 File Offset: 0x0012B1C4
		private void ᜃ(byte[] A_0)
		{
			int a_ = 1;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				string text;
				string text2;
				for (;;)
				{
					for (;;)
					{
						bool flag = this.m_fillType == ShapeFillType.Pattern;
						bool flag2 = this.m_fillType == ShapeFillType.Picture;
						bool flag3 = this.m_fillType == ShapeFillType.Texture;
						int num = 17;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_23C;
							case 1:
								if (flag2)
								{
									num = 3;
									continue;
								}
								goto IL_183;
							case 2:
								if (!flag3)
								{
									num = 14;
									continue;
								}
								goto IL_221;
							case 3:
								goto IL_290;
							case 4:
								if (text[0] <= '9')
								{
									num = 16;
									continue;
								}
								goto IL_C5;
							case 5:
								if (!flag2)
								{
									num = 15;
									continue;
								}
								goto IL_221;
							case 6:
								goto IL_1E1;
							case 7:
								if (text[0] >= '0')
								{
									num = 8;
									continue;
								}
								goto IL_C5;
							case 8:
								num = 4;
								continue;
							case 9:
							{
								if (A_0 == null)
								{
									num = 0;
									continue;
								}
								text = "";
								int num2 = 0;
								int num3 = A_0.Length - 2;
								num = 6;
								continue;
							}
							case 10:
								goto IL_14E;
							case 11:
								num = 2;
								continue;
							case 12:
								text2 = RecordTableEnumerator.b("朶堸伺戼", a_) + text2;
								num = 10;
								continue;
							case 13:
								num = 1;
								continue;
							case 14:
								num = 5;
								continue;
							case 15:
								return;
							case 16:
								goto IL_31C;
							case 17:
								if (!flag)
								{
									num = 11;
									continue;
								}
								goto IL_221;
							case 18:
								if (!flag3)
								{
									goto IL_C5;
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
									num = 22;
									continue;
								}
								break;
							case 19:
								if (flag)
								{
									num = 12;
									continue;
								}
								try
								{
									this.ᜢ = (GradientTextureType)Enum.Parse(typeof(GradientTextureType), text2, true);
									return;
								}
								catch
								{
									this.ᜀ(text, false);
									return;
								}
								goto IL_183;
							case 20:
							{
								int num2;
								int num3;
								if (num2 >= num3)
								{
									num = 13;
									continue;
								}
								text += (char)A_0[num2];
								num2 += 2;
								num = 21;
								continue;
							}
							case 21:
								goto IL_1E1;
							case 22:
								num = 7;
								continue;
							}
							break;
							IL_C5:
							text2 = text.Replace(' ', '_');
							num = 19;
							continue;
							IL_183:
							num = 18;
							continue;
							IL_1E1:
							num = 20;
							continue;
							IL_221:
							num = 9;
						}
					}
				}
				IL_14E:
				try
				{
					this.ᜡ = (GradientPatternType)Enum.Parse(typeof(GradientPatternType), text2, true);
					return;
				}
				catch
				{
					this.ᜡ = GradientPatternType.Pat5Percent;
					return;
				}
				goto IL_2C1;
				IL_23C:
				throw new ArgumentNullException(RecordTableEnumerator.b("嘶崸强礼帾㕀≂", a_));
				IL_290:
				this.ᜀ(text, true);
				return;
				IL_2C1:
				this.ᜀ(text, false);
				return;
				IL_31C:
				goto IL_2C1;
			}
			}
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0012C50C File Offset: 0x0012B50C
		private void ᜀ(uint A_0)
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					}
					if (false)
					{
					}
					break;
				case 1:
					goto IL_59;
				case 2:
					if (A_0 == 1073741835U)
					{
						num = 3;
						continue;
					}
					goto IL_85;
				case 3:
					goto IL_7B;
				}
				if (A_0 == 0U)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			IL_59:
			this.ᜠ = GradientColorType.Preset;
			return;
			IL_7B:
			this.ᜠ = GradientColorType.OneColor;
			return;
			IL_85:
			this.ᜠ = GradientColorType.TwoColor;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0012C5A8 File Offset: 0x0012B5A8
		private void ᜂ(byte[] A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					bool flag;
					int num2;
					int num3;
					int num4;
					int num6;
					switch (num)
					{
					case 0:
						flag = true;
						num = 10;
						continue;
					case 1:
						goto IL_91;
					case 2:
						num = 5;
						continue;
					case 3:
						num2 = 0;
						num = 12;
						continue;
					case 4:
						num = 21;
						continue;
					case 5:
					{
						if (this.m_fillType != ShapeFillType.Gradient)
						{
							num = 11;
							continue;
						}
						num3 = 1;
						num4 = 0;
						int num5 = A_0.Length;
						flag = false;
						num = 20;
						continue;
					}
					case 6:
						this.ᜦ = (GradientPresetType)(num3 - 1);
						num = 7;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1BD;
						default:
							goto IL_14F;
						}
						break;
					case 8:
					{
						int num5;
						if (num6 == num5)
						{
							num = 3;
							continue;
						}
						goto IL_91;
					}
					case 9:
						num = 22;
						continue;
					case 10:
						goto IL_AB;
					case 11:
						goto IL_238;
					case 12:
						goto IL_FE;
					case 14:
						if (num2 >= num6)
						{
							num = 1;
							continue;
						}
						num = 23;
						continue;
					case 15:
						goto IL_FE;
					case 16:
						goto IL_1BD;
					case 17:
						goto IL_1B1;
					case 18:
						goto IL_1DE;
					case 19:
						if (num3 < 25)
						{
							num = 4;
							continue;
						}
						goto IL_1B1;
					case 20:
						goto IL_1DE;
					case 21:
						if (flag)
						{
							num = 17;
							continue;
						}
						num6 = (int)XlsShapeFill.ᜰ[num4];
						num4++;
						num = 8;
						continue;
					case 22:
						if (num6 - num2 == 1)
						{
							num = 0;
							continue;
						}
						goto IL_AB;
					case 23:
						if (A_0[num2] == XlsShapeFill.ᜰ[num4 + num2])
						{
							num = 9;
							continue;
						}
						goto IL_91;
					}
					if (A_0 != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_91:
					num4 += num6;
					num3++;
					num = 18;
					continue;
					IL_AB:
					num2++;
					num = 15;
					continue;
					IL_FE:
					num = 14;
					continue;
					IL_1B1:
					num = 16;
					continue;
					IL_1BD:
					if (flag)
					{
						num = 6;
						continue;
					}
					goto IL_280;
					IL_1DE:
					num = 19;
				}
				return;
				IL_14F:
				if (false)
				{
				}
				goto IL_280;
				IL_238:
				return;
				IL_280:
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0012C840 File Offset: 0x0012B840
		private void ᜀ(string A_0, bool A_1)
		{
			int a_ = 6;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Length == 0)
					{
						num = 1;
						continue;
					}
					goto IL_76;
				case 1:
					goto IL_74;
				case 2:
					num = 0;
					continue;
				case 3:
					if (true)
					{
					}
					break;
				}
				if (A_0 == null)
				{
					break;
				}
				num = 2;
			}
			IL_3E:
			throw new ArgumentException(RecordTableEnumerator.b("伻䨽㈿ు╃⭅ⵇ橉⽋⽍㹏㵑⁓癕㩗㽙籛そᕟ๡ࡣ䙥ݧᡩ䱫୭ᵯɱsཱུ噷", a_));
			IL_74:
			goto IL_3E;
			IL_76:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3E;
			default:
				if (false)
				{
				}
				this.ᜧ = A_0;
				this.ParsePictureOrUserDefinedTexture(A_1);
				return;
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0012C8F0 File Offset: 0x0012B8F0
		protected void ParsePictureOrUserDefinedTexture(bool bIsPicture)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					byte[] array;
					switch (num)
					{
					case 0:
						goto IL_130;
					case 1:
						goto IL_5D;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13C;
						default:
							if (false)
							{
							}
							if (array != null)
							{
								num = 4;
								continue;
							}
							goto IL_5D;
						}
						break;
					case 3:
					{
						if (array.Length == 0)
						{
							num = 1;
							continue;
						}
						byte[] array2 = new byte[array.Length - 25];
						Array.Copy(array, 25, array2, 0, array2.Length);
						MemoryStream memoryStream = new MemoryStream();
						XlsShapeFill.ᜀ(memoryStream, array);
						memoryStream.Write(array2, 0, array2.Length);
						this.m_picture = spr\u17FF.ᜀ(memoryStream);
						num = 8;
						continue;
					}
					case 4:
						num = 3;
						continue;
					case 6:
						if (true)
						{
						}
						this.ᜢ = GradientTextureType.UserDefined;
						num = 0;
						continue;
					case 7:
						goto IL_A2;
					case 8:
						goto IL_12E;
					}
					if (!bIsPicture)
					{
						num = 6;
						continue;
					}
					goto IL_130;
					IL_5D:
					this.ᜩ = (int)this.ᜫ.ᜆ();
					sprᜪ sprᜪ = this.ᜣ.ShapesData.ᜀ(this.ᜩ);
					this.m_picture = sprᜪ.ᜄ().ᜀ();
					num = 7;
					continue;
					IL_13C:
					num = 2;
					continue;
					IL_130:
					array = this.ᜫ.ᜄ();
					goto IL_13C;
				}
				IL_A2:
				IL_12E:
				this.ᜫ = null;
				return;
			}
			}
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0012CA80 File Offset: 0x0012BA80
		internal static void ᜀ(MemoryStream A_0, byte[] A_1)
		{
			int a_ = 5;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!BiffRecordRaw.CompareArrays(A_1, 0, XlsShapeFill.\u1718, 0, XlsShapeFill.\u1718.Length))
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_D2;
					}
					break;
				case 2:
					goto IL_3C;
				case 3:
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 4:
					goto IL_64;
				case 5:
					goto IL_BA;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("嘺丼", a_));
			IL_64:
			if (true)
			{
			}
			return;
			IL_BA:
			throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾", a_));
			IL_D2:
			if (false)
			{
			}
			int a_2 = A_1.Length + 14 - 25;
			uint a_3 = BitConverter.ToUInt32(A_1, 25);
			uint a_4 = BitConverter.ToUInt32(A_1, 57);
			spr៣.ᜀ(A_0, a_2, a_3, a_4);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0012CB8C File Offset: 0x0012BB8C
		private void ᜁ(byte[] A_0)
		{
			int a_ = 3;
			int num = 3;
			for (;;)
			{
				byte b;
				switch (num)
				{
				case 0:
					if (A_0[3] == 0)
					{
						num = 6;
						continue;
					}
					goto IL_122;
				case 1:
					if (b > 0)
					{
						num = 2;
						continue;
					}
					b = (A_0[4] & 16);
					num = 0;
					continue;
				case 2:
					goto IL_D6;
				case 4:
					num = 7;
					continue;
				case 5:
					if (A_0[5] == 0)
					{
						num = 4;
						continue;
					}
					goto IL_122;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_109;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 7:
					goto IL_109;
				case 8:
					goto IL_4C;
				case 9:
					goto IL_118;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				b = (A_0[2] & 16);
				num = 1;
				continue;
				IL_109:
				if (b <= 0)
				{
					goto IL_122;
				}
				num = 9;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("崸娺䤼帾", a_));
			IL_D6:
			this.ᜨ = true;
			return;
			IL_118:
			this.ᜨ = false;
			return;
			IL_122:
			this.ᜨ = true;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0012CCC4 File Offset: 0x0012BCC4
		[CLSCompliant(false)]
		internal sprᡍ \u170D(sprᡍ A_0)
		{
			int a_ = 3;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					ShapeFillType fillType;
					switch (fillType)
					{
					case ShapeFillType.SolidColor:
						goto IL_115;
					case ShapeFillType.Pattern:
					case ShapeFillType.Texture:
						goto IL_61;
					case ShapeFillType.Picture:
						goto IL_3D;
					case ShapeFillType.UnknownGradient:
						return A_0;
					case (ShapeFillType)5:
					case (ShapeFillType)6:
						goto IL_13C;
					case ShapeFillType.Gradient:
						goto IL_127;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_13A;
				case 3:
					goto IL_38;
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					A_0 = this.ᜈ(A_0);
					A_0 = this.SerializeTransparency(A_0);
					A_0 = this.ᜁ(A_0);
					XlsShapeLineFormat.ᜀ(A_0, this.ForeColorObject, this.ᜣ, MsoOptions.ForeColor);
					XlsShapeLineFormat.ᜀ(A_0, this.BackColorObject, this.ᜣ, MsoOptions.BackColor);
					ShapeFillType fillType = this.m_fillType;
					if (true)
					{
					}
					num = 0;
				}
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("嘸䬺䤼", a_));
			IL_3D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_127:
				return this.ᜌ(A_0);
			default:
				if (false)
				{
				}
				return this.ᜊ(A_0);
			}
			IL_61:
			return this.ᜋ(A_0);
			IL_115:
			return this.ᜉ(A_0);
			IL_13A:
			IL_13C:
			throw new ApplicationException(RecordTableEnumerator.b("永唺嘼儾⹀㑂⭄杆⽈≊⅌⍎煐❒ⱔ❖㱘", a_));
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0012CE20 File Offset: 0x0012BE20
		private sprᡍ ᜌ(sprᡍ A_0)
		{
			int a_ = 12;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0 = this.ᜇ(A_0);
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_51;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_49;
				case 2:
					if (this.\u171C != GradientStyleType.Horizontal)
					{
						num = 0;
						continue;
					}
					goto IL_49;
				case 4:
					A_0 = this.ᜂ(A_0);
					num = 6;
					continue;
				case 5:
					goto IL_51;
				case 6:
					return A_0;
				case 7:
					goto IL_47;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				A_0 = this.ᜆ(A_0);
				A_0 = this.ᜃ(A_0);
				num = 2;
				continue;
				IL_49:
				num = 5;
				continue;
				IL_51:
				if (this.ᜠ != GradientColorType.Preset)
				{
					return A_0;
				}
				num = 4;
			}
			IL_47:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⵁ㑃㉅", a_));
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0012CF3C File Offset: 0x0012BF3C
		private sprᡍ ᜋ(sprᡍ A_0)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 0;
				string text;
				for (;;)
				{
					if (true)
					{
					}
					bool flag;
					int num2;
					string str;
					switch (num)
					{
					case 1:
						if (flag)
						{
							num = 4;
							continue;
						}
						goto IL_223;
					case 2:
						goto IL_221;
					case 3:
						if (!flag)
						{
							num = 7;
							continue;
						}
						goto IL_AB;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1F5;
						default:
							if (false)
							{
							}
							text = text.Substring(RecordTableEnumerator.b("᥈⩊㥌၎", a_).Length);
							text = text.Replace(RecordTableEnumerator.b("ᙈᭊ⡌㵎㉐㙒㭔⍖", a_), RecordTableEnumerator.b("汈", a_));
							num = 11;
							continue;
						}
						break;
					case 5:
						if (flag)
						{
							num = 9;
							continue;
						}
						num2 = (int)this.ᜢ;
						text = this.ᜢ.ToString();
						str = RecordTableEnumerator.b("ᵈ⹊㕌㭎", a_);
						num = 8;
						continue;
					case 6:
						if (this.ᜢ == GradientTextureType.UserDefined)
						{
							num = 2;
							continue;
						}
						goto IL_AB;
					case 7:
						num = 6;
						continue;
					case 8:
						goto IL_F5;
					case 9:
						num2 = (int)this.ᜡ;
						text = this.ᜡ.ToString();
						str = RecordTableEnumerator.b("᥈⩊㥌㭎", a_);
						num = 10;
						continue;
					case 10:
						goto IL_F5;
					case 11:
						goto IL_1E9;
					case 12:
						goto IL_71;
					}
					if (A_0 == null)
					{
						num = 12;
						continue;
					}
					flag = (this.m_fillType == ShapeFillType.Pattern);
					num = 3;
					continue;
					IL_AB:
					num = 5;
					continue;
					IL_F5:
					byte[] resData = XlsShapeFill.GetResData(str + num2.ToString());
					XlsShape.ᜀ(A_0, MsoOptions.PatternTexture, 0, resData, true);
					num = 1;
				}
				IL_71:
				throw new ArgumentNullException(RecordTableEnumerator.b("♈㭊㥌", a_));
				IL_1E9:
				goto IL_223;
				IL_1F5:
				return this.ᜊ(A_0);
				IL_221:
				goto IL_1F5;
				IL_223:
				text = text.Replace('_', ' ');
				byte[] a_2 = this.ᜀ(text);
				XlsShape.ᜀ(A_0, MsoOptions.PattTextName, 0, a_2, true);
				return A_0;
			}
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0012D190 File Offset: 0x0012C190
		private sprᡍ ᜊ(sprᡍ A_0)
		{
			int a_ = 13;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					byte[] a_2 = this.ᜀ(this.ᜧ);
					XlsShape.ᜀ(A_0, MsoOptions.PattTextName, 0, a_2, true);
					num = 4;
					continue;
				}
				case 2:
					goto IL_38;
				case 3:
					if (!string.IsNullOrEmpty(this.ᜧ))
					{
						num = 0;
						continue;
					}
					return A_0;
				case 4:
					return A_0;
				}
				if (A_0 == null)
				{
					num = 2;
				}
				else
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return A_0;
					default:
						if (false)
						{
						}
						A_0 = this.SetPicture(A_0);
						num = 3;
						break;
					}
				}
			}
			IL_38:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱂ㕄㍆", a_));
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0012D270 File Offset: 0x0012C270
		private sprᡍ ᜉ(sprᡍ A_0)
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
			return A_0;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0012D2AC File Offset: 0x0012C2AC
		private sprᡍ ᜈ(sprᡍ A_0)
		{
			int a_ = 0;
			int num = 8;
			int a_2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_70;
				case 1:
					num = 9;
					continue;
				case 2:
					if (this.\u171C == GradientStyleType.From_Center)
					{
						goto IL_10D;
					}
					goto IL_141;
				case 3:
					num = 2;
					continue;
				case 4:
					a_2 = 5;
					num = 5;
					continue;
				case 5:
					goto IL_72;
				case 6:
					if (this.m_fillType == ShapeFillType.Gradient)
					{
						num = 1;
						continue;
					}
					goto IL_72;
				case 7:
					if (this.m_fillType == ShapeFillType.Gradient)
					{
						num = 3;
						continue;
					}
					goto IL_141;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10D;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 9:
					if (this.\u171C == GradientStyleType.From_Corner)
					{
						num = 4;
						continue;
					}
					goto IL_72;
				case 10:
					a_2 = 6;
					num = 11;
					continue;
				case 11:
					goto IL_E3;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				a_2 = (int)this.m_fillType;
				num = 6;
				continue;
				IL_72:
				if (true)
				{
				}
				num = 7;
				continue;
				IL_10D:
				num = 10;
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("夵䠷丹", a_));
			IL_E3:
			IL_141:
			XlsShape.ᜀ(A_0, MsoOptions.FillType, a_2);
			return A_0;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0012D408 File Offset: 0x0012C408
		private sprᡍ ᜇ(sprᡍ A_0)
		{
			int a_ = 6;
			byte[] array;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_65:
				array[2] = 121;
				num = 7;
				break;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					GradientStyleType u171C;
					switch (u171C)
					{
					case GradientStyleType.Vertical:
						array[2] = 166;
						num = 2;
						continue;
					case GradientStyleType.Diagonl_Up:
						goto IL_65;
					case GradientStyleType.Diagonl_Down:
						array[2] = 211;
						num = 6;
						continue;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_87;
				case 4:
					return A_0;
				case 5:
					goto IL_60;
				case 6:
					goto IL_10B;
				case 7:
					goto IL_72;
				}
				if (A_0 == null)
				{
					num = 5;
				}
				else
				{
					array = new byte[]
					{
						0,
						0,
						0,
						byte.MaxValue
					};
					GradientStyleType u171C = this.\u171C;
					num = 0;
				}
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("医丽㐿", a_));
			IL_72:
			goto IL_11A;
			IL_87:
			if (true)
			{
			}
			IL_10B:
			IL_11A:
			XlsShape.ᜀ(A_0, MsoOptions.ShadStyle, array);
			return A_0;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0012D53C File Offset: 0x0012C53C
		private sprᡍ ᜆ(sprᡍ A_0)
		{
			int a_ = 8;
			int num = 7;
			for (;;)
			{
				bool flag;
				bool flag2;
				bool flag3;
				switch (num)
				{
				case 0:
					goto IL_83;
				case 1:
					if (this.\u171D < GradientVariantsType.ShadingVariants3)
					{
						num = 14;
						continue;
					}
					num = 15;
					continue;
				case 2:
					if (true)
					{
					}
					XlsShape.ᜀ(A_0, MsoOptions.ShadVariant, XlsShapeFill.\u1713);
					num = 5;
					continue;
				case 3:
					if (flag)
					{
						num = 19;
						continue;
					}
					XlsShape.ᜀ(A_0, MsoOptions.ShadVariant, XlsShapeFill.\u1715);
					num = 17;
					continue;
				case 4:
					flag2 = (this.\u171D != GradientVariantsType.ShadingVariants3);
					goto IL_1F4;
				case 5:
					goto IL_A2;
				case 6:
					num = 10;
					continue;
				case 7:
					IL_11:
					break;
				case 8:
					flag3 = (this.\u171D != GradientVariantsType.ShadingVariants2);
					goto IL_13D;
				case 9:
					goto IL_274;
				case 10:
					flag2 = (this.\u171D == GradientVariantsType.ShadingVariants3);
					goto IL_1F4;
				case 11:
					if (this.\u171C == GradientStyleType.From_Corner)
					{
						num = 21;
						continue;
					}
					num = 16;
					continue;
				case 12:
					if (!flag)
					{
						num = 2;
						continue;
					}
					return A_0;
				case 13:
					num = 18;
					continue;
				case 14:
					num = 20;
					continue;
				case 15:
					if (this.\u171C != GradientStyleType.Horizontal)
					{
						num = 6;
						continue;
					}
					num = 4;
					continue;
				case 16:
					if (this.\u171C == GradientStyleType.From_Center)
					{
						num = 22;
						continue;
					}
					flag = false;
					num = 1;
					continue;
				case 17:
					goto IL_274;
				case 18:
					flag3 = (this.\u171D == GradientVariantsType.ShadingVariants2);
					goto IL_13D;
				case 19:
					XlsShape.ᜀ(A_0, MsoOptions.ShadVariant, XlsShapeFill.\u1714);
					num = 9;
					continue;
				case 20:
					if (this.\u171C != GradientStyleType.Diagonl_Down)
					{
						num = 13;
						continue;
					}
					num = 8;
					continue;
				case 21:
					goto IL_138;
				case 22:
					goto IL_252;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				num = 11;
				continue;
				IL_274:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_11;
				default:
					goto IL_28A;
				}
				IL_13D:
				flag = flag3;
				num = 12;
				continue;
				IL_1F4:
				flag = flag2;
				num = 3;
			}
			IL_83:
			throw new ArgumentNullException(RecordTableEnumerator.b("儽〿㙁", a_));
			IL_A2:
			return A_0;
			IL_138:
			return this.ᜄ(A_0);
			IL_252:
			return this.ᜅ(A_0);
			IL_28A:
			if (false)
			{
			}
			return A_0;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0012D7DC File Offset: 0x0012C7DC
		private sprᡍ ᜅ(sprᡍ A_0)
		{
			int a_ = 15;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5E:
				XlsShape.ᜀ(A_0, MsoOptions.ShadVariant, XlsShapeFill.\u1713);
				num = 4;
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
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_9B;
				case 1:
					goto IL_5C;
				case 3:
					if (this.\u171D == GradientVariantsType.ShadingVariants1)
					{
						num = 0;
						continue;
					}
					goto IL_B1;
				case 4:
					goto IL_76;
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
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩄㝆㵈", a_));
			IL_76:
			goto IL_B1;
			IL_9B:
			goto IL_5E;
			IL_B1:
			XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_1, XlsShapeFill.\u1716);
			XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_2, XlsShapeFill.\u1716);
			XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_3, XlsShapeFill.\u1716);
			XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_4, XlsShapeFill.\u1716);
			return A_0;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0012D8DC File Offset: 0x0012C8DC
		private sprᡍ ᜄ(sprᡍ A_0)
		{
			int a_ = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 8;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return A_0;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					goto IL_25B;
				case 3:
					if (this.\u171D == GradientVariantsType.ShadingVariants4)
					{
						num = 2;
						continue;
					}
					goto IL_191;
				case 4:
					return A_0;
				case 5:
					if (this.\u171D == GradientVariantsType.ShadingVariants4)
					{
						num = 19;
						continue;
					}
					goto IL_14A;
				case 6:
					if (this.\u171D == GradientVariantsType.ShadingVariants1)
					{
						num = 9;
						continue;
					}
					num = 10;
					continue;
				case 7:
					num = 3;
					continue;
				case 8:
					if (this.\u171D == GradientVariantsType.ShadingVariants4)
					{
						num = 13;
						continue;
					}
					goto IL_F5;
				case 9:
					return A_0;
				case 10:
					if (this.\u171D != GradientVariantsType.ShadingVariants2)
					{
						num = 7;
						continue;
					}
					goto IL_25B;
				case 11:
					if (this.\u171D != GradientVariantsType.ShadingVariants2)
					{
						num = 0;
						continue;
					}
					goto IL_203;
				case 12:
					goto IL_F5;
				case 13:
					goto IL_203;
				case 14:
					goto IL_AD;
				case 15:
					num = 5;
					continue;
				case 16:
					goto IL_14A;
				case 17:
					goto IL_171;
				case 18:
					goto IL_191;
				case 19:
					goto IL_D6;
				case 20:
					num = 22;
					continue;
				case 21:
					if (this.\u171D != GradientVariantsType.ShadingVariants3)
					{
						num = 15;
						continue;
					}
					goto IL_D6;
				case 22:
					if (this.\u171D == GradientVariantsType.ShadingVariants4)
					{
						num = 17;
						continue;
					}
					return A_0;
				case 23:
					if (this.\u171D != GradientVariantsType.ShadingVariants3)
					{
						num = 20;
						continue;
					}
					goto IL_171;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				XlsShape.ᜀ(A_0, MsoOptions.ShadVariant, XlsShapeFill.\u1713);
				num = 6;
				continue;
				IL_D6:
				XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_2, XlsShapeFill.\u1717);
				num = 16;
				continue;
				IL_F5:
				num = 23;
				continue;
				IL_14A:
				num = 11;
				continue;
				IL_171:
				XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_4, XlsShapeFill.\u1717);
				num = 4;
				continue;
				IL_191:
				num = 21;
				continue;
				IL_203:
				XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_3, XlsShapeFill.\u1717);
				num = 12;
				continue;
				IL_25B:
				if (true)
				{
				}
				XlsShape.ᜀ(A_0, MsoOptions.ShadingStyleCorner_1, XlsShapeFill.\u1717);
				num = 18;
			}
			IL_AD:
			throw new ArgumentNullException(RecordTableEnumerator.b("夵䠷丹", a_));
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0012DB94 File Offset: 0x0012CB94
		private sprᡍ ᜃ(sprᡍ A_0)
		{
			int a_ = 12;
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
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_62;
					case 1:
						if (this.ᜠ != GradientColorType.OneColor)
						{
							num = 2;
							continue;
						}
						goto IL_94;
					case 2:
						return A_0;
					}
					if (A_0 == null)
					{
						num = 0;
					}
					else
					{
						num = 1;
					}
				}
				IL_62:
				break;
				IL_94:
				XlsShape.ᜀ(A_0, MsoOptions.GradientColorType, 1073741835);
				this.ᜀ(A_0);
				return A_0;
			}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ⵁ㑃㉅", a_));
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0012DC50 File Offset: 0x0012CC50
		private sprᡍ ᜂ(sprᡍ A_0)
		{
			int a_ = 6;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("医丽㐿", a_));
				}
				break;
			}
			if (true)
			{
			}
			int num = (int)this.ᜦ;
			byte[] resData = XlsShapeFill.GetResData(RecordTableEnumerator.b("笻䰽ℿ♁", a_) + num.ToString());
			XlsShape.ᜀ(A_0, MsoOptions.PresetGradientData, 0, resData, true);
			XlsShape.ᜀ(A_0, MsoOptions.GradientColorType, 0);
			return A_0;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0012DCF0 File Offset: 0x0012CCF0
		private sprᡍ ᜁ(sprᡍ A_0)
		{
			int a_ = 14;
			byte[] array;
			for (;;)
			{
				IL_09:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_51;
					case 2:
						array[0] = 16;
						num = 1;
						continue;
					case 3:
						goto IL_42;
					case 4:
						if (!this.ᜨ)
						{
							goto IL_B6;
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
							num = 2;
							continue;
						}
						break;
					}
					if (A_0 == null)
					{
						num = 3;
					}
					else
					{
						byte[] array2 = new byte[4];
						array2[2] = 16;
						array = array2;
						num = 4;
					}
				}
			}
			IL_42:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭃㙅㱇", a_));
			IL_51:
			if (true)
			{
			}
			IL_B6:
			XlsShape.ᜀ(A_0, MsoOptions.NoFillHitTest, array);
			return A_0;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0012DDC0 File Offset: 0x0012CDC0
		private sprᡍ ᜀ(sprᡍ A_0)
		{
			int a_ = 16;
			byte[] array;
			double num2;
			for (;;)
			{
				IL_09:
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						array[1] = 2;
						num2 = 1.0 - num2;
						num = 2;
						continue;
					case 2:
						goto IL_64;
					case 3:
						if (num2 < 0.5)
						{
							goto IL_D0;
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
							num = 0;
							continue;
						}
						break;
					case 4:
						goto IL_4A;
					}
					if (A_0 == null)
					{
						num = 4;
					}
					else
					{
						array = new byte[]
						{
							240,
							1,
							0,
							16
						};
						num2 = this.ᜪ;
						num = 3;
					}
				}
			}
			IL_4A:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥅㡇㹉", a_));
			IL_64:
			IL_D0:
			array[2] = (byte)(num2 * 255.0 * 2.0);
			XlsShape.ᜀ(A_0, MsoOptions.BackColor, array);
			return A_0;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0012DEC4 File Offset: 0x0012CEC4
		private void ᜅ()
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
				if (true)
				{
				}
				if (this.m_fillType != ShapeFillType.Gradient)
				{
					throw new NotSupportedException(RecordTableEnumerator.b("ᕀ⭂ⱄ㑆楈㭊㽌⁎⅐㙒❔⍖⁘筚㹜㹞འ䍢ݤɦ䥨ᡪ࡬᭮兰ᱲ᭴᭶x孺੼᝾ꖄ삆ﮈﶒ랖쪘쒠莢첤풦覨\ud8aa좬쎮풰킲솴튶\uddb8閺", a_));
				}
				break;
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0012DF28 File Offset: 0x0012CF28
		private void ᜄ()
		{
			int a_ = 19;
			for (;;)
			{
				IL_09:
				int num = 2;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						num = 5;
						continue;
					case 1:
						goto IL_7B;
					case 3:
						if (true)
						{
						}
						if (this.m_fillType != ShapeFillType.Picture)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						flag = false;
						goto IL_B5;
					case 5:
						if (!flag2)
						{
							num = 1;
							continue;
						}
						return;
					case 6:
						num = 7;
						continue;
					case 7:
						flag = (this.ᜢ == GradientTextureType.UserDefined);
						goto IL_B5;
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
						if (this.m_fillType == ShapeFillType.Texture)
						{
							num = 6;
							continue;
						}
						num = 4;
						continue;
					}
					IL_B5:
					flag2 = flag;
					num = 3;
				}
			}
			IL_7B:
			throw new NotSupportedException(RecordTableEnumerator.b("ᵈ⍊⑌㱎煐⍒❔㡖⥘㹚⽜⭞ᡠ䍢ᙤቦᥨ᭪ɬᵮհ卲ᩴ᥶ᕸɺ嵼ᙾꎂ뎒ﲘ붜쒠\udba2톤튦\udba8캪趬삮ힰ鎲어\udeb6\udab8쾺좼춾꓀", a_));
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0012E024 File Offset: 0x0012D024
		private void ᜃ()
		{
			int a_ = 16;
			while (this.m_fillType != ShapeFillType.Pattern)
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
					throw new NotSupportedException(RecordTableEnumerator.b("ቅ⁇⍉㽋湍⁏⁑㭓♕㵗⡙⡛❝䁟ᅡᅣᙥݧᡩᡫᵭ偯ᵱᩳ᩵ŷ婹ᕻ᡽ꁿ낏ﾙ肟톡킣\udfa5쒧쾩芫", a_));
				}
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0012E088 File Offset: 0x0012D088
		private void ᜂ()
		{
			int a_ = 17;
			while (this.m_fillType != ShapeFillType.Texture)
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
					throw new NotSupportedException(RecordTableEnumerator.b("ፆⅈ≊㹌潎⅐⅒㩔❖㱘⥚⥜♞䅠ၢၤᝦ٨ᥪᥬᱮ兰ᱲ᭴᭶x孺ᑼ᥾ꆀ놐爵膠킢톤\udea6얨캪莬", a_));
				}
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0012E0EC File Offset: 0x0012D0EC
		private void ᜁ()
		{
			int a_ = 16;
			while (this.m_fillType != ShapeFillType.SolidColor)
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
					throw new NotSupportedException(RecordTableEnumerator.b("ቅ⁇⍉㽋湍⁏⁑㭓♕㵗⡙⡛❝䁟ᅡᅣᙥᡧթṫᩭͯ剱᭳ᡵᑷ͹屻᝽ꊁ잃늑잓秊뺝펟횡\udda3쪥춧蒩", a_));
				}
			}
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0012E150 File Offset: 0x0012D150
		private byte[] ᜀ(string A_0)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 5;
				int length;
				byte[] array;
				for (;;)
				{
					char c;
					switch (num)
					{
					case 0:
					{
						if (A_0.Length == 0)
						{
							num = 7;
							continue;
						}
						length = A_0.Length;
						array = new byte[length * 2 + 2];
						int num2 = 0;
						num = 1;
						continue;
					}
					case 1:
						goto IL_B3;
					case 2:
						goto IL_F9;
					case 3:
						goto IL_CF;
					case 4:
						goto IL_75;
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_F7;
					case 8:
						goto IL_B3;
					case 9:
						if (true)
						{
						}
						if (char.IsUpper(c))
						{
							num = 12;
							continue;
						}
						goto IL_F9;
					case 10:
					{
						int num2;
						if (num2 >= length)
						{
							num = 3;
							continue;
						}
						c = A_0[num2];
						num = 9;
						continue;
					}
					case 11:
					{
						int num2;
						if (num2 > 0)
						{
							num = 4;
							continue;
						}
						goto IL_F9;
					}
					case 12:
						num = 11;
						continue;
					}
					if (A_0 != null)
					{
						num = 6;
						continue;
					}
					break;
					IL_75:
					c = char.ToLower(c);
					num = 2;
					continue;
					IL_F9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_75;
					default:
					{
						if (false)
						{
						}
						int num2;
						array[2 * num2] = (byte)c;
						array[2 * num2 + 1] = 0;
						num2++;
						num = 8;
						continue;
					}
					}
					IL_B3:
					num = 10;
				}
				IL_9F:
				throw new ArgumentException(RecordTableEnumerator.b("䤹䠻䰽฿⍁⥃⍅桇⥉ⵋ⁍㽏♑瑓㑕㵗穙㉛⭝౟๡䑣॥ᩧ䩩५ͭoٱ൳塵", a_));
				IL_CF:
				array[2 * length] = 0;
				array[2 * length + 1] = 0;
				return array;
				IL_F7:
				goto IL_9F;
			}
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0012E300 File Offset: 0x0012D300
		private double ᜀ(byte[] A_0)
		{
			int a_ = 16;
			int num = 5;
			double num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num2 = (double)A_0[4] * 0.5 / 255.0;
					num = 2;
					continue;
				case 1:
					if (A_0[5] == 16)
					{
						num = 0;
						continue;
					}
					return num2;
				case 2:
					if (A_0[3] == 2)
					{
						num = 3;
						continue;
					}
					return num2;
				case 3:
					num2 = 1.0 - num2;
					num = 4;
					continue;
				case 4:
					goto IL_C6;
				case 5:
					if (true)
					{
					}
					break;
				case 6:
					goto IL_48;
				}
				if (A_0 == null)
				{
					num = 6;
				}
				else
				{
					num2 = this.ᜪ;
					num = 1;
				}
			}
			IL_48:
			IL_9B:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍", a_));
			IL_C6:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_9B;
			default:
				if (false)
				{
				}
				break;
			}
			return num2;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0012E3FC File Offset: 0x0012D3FC
		private void ᜀ(byte A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					goto IL_84;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5B;
					}
					if (false)
					{
					}
					break;
				case 3:
					return;
				case 4:
					if (A_0 != 1)
					{
						num = 3;
						continue;
					}
					num = 1;
					continue;
				}
				goto IL_4A;
				IL_5B:
				num = 0;
				continue;
				IL_4A:
				if (true)
				{
				}
				if (this.\u171C == GradientStyleType.From_Corner)
				{
					goto IL_5B;
				}
				break;
			}
			return;
			IL_84:
			this.\u171D = ((this.\u171D == GradientVariantsType.ShadingVariants1) ? GradientVariantsType.ShadingVariants3 : GradientVariantsType.ShadingVariants4);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0012E4A4 File Offset: 0x0012D4A4
		private static void ᜀ()
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_126:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					goto IL_4C;
				}
				int num2;
				int num3;
				GradientPresetType[] array;
				ResourceManager resourceManager;
				for (;;)
				{
					IL_35:
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_192;
					case 2:
						goto IL_124;
					case 3:
					{
						if (true)
						{
						}
						if (num2 >= num3)
						{
							num = 0;
							continue;
						}
						GradientPresetType gradientPresetType = array[num2];
						byte[] value = (byte[])resourceManager.GetObject(gradientPresetType.ToString());
						XlsShapeFill.ᜱ.Add(gradientPresetType, value);
						num2++;
						num = 1;
						continue;
					}
					}
					goto IL_4C;
				}
				IL_124:
				IL_192:
				goto IL_126;
				IL_4C:
				XlsShapeFill.ᜱ = new Dictionary<GradientPresetType, byte[]>();
				array = new GradientPresetType[]
				{
					GradientPresetType.GradEarlySunset,
					GradientPresetType.GradLateSunset,
					GradientPresetType.GradNightfall,
					GradientPresetType.GradDaybreak,
					GradientPresetType.GradHorizon,
					GradientPresetType.GradDesert,
					GradientPresetType.GradOcean,
					GradientPresetType.GradCalmWater,
					GradientPresetType.GradFire,
					GradientPresetType.GradFog,
					GradientPresetType.GradMoss,
					GradientPresetType.GradPeacock,
					GradientPresetType.GradWheat,
					GradientPresetType.GradParchment,
					GradientPresetType.GradMahogany,
					GradientPresetType.GradRainbow,
					GradientPresetType.GradRainbow2,
					GradientPresetType.GradGold,
					GradientPresetType.GradGold2,
					GradientPresetType.GradBrass,
					GradientPresetType.GradChrome,
					GradientPresetType.GradChrome2,
					GradientPresetType.GradSilver,
					GradientPresetType.GradSapphire
				};
				resourceManager = new ResourceManager(RecordTableEnumerator.b("洽〿⭁㙃⍅晇቉⁋㵍繏ᅑ㭓⑕㵗瑙౛ⱝ՟ᅡţብ⽧ᡩ൫੭᥯᝱ᩳɵ୷", a_), XlsShapeFill.ᜯ);
				num2 = 0;
				num3 = array.Length;
				num = 2;
				goto IL_35;
			}
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0012E648 File Offset: 0x0012D648
		[CLSCompliant(false)]
		internal virtual sprᡍ SetPicture(sprᡍ opt)
		{
			int a_ = 18;
			while (opt == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("❇㩉㡋", a_));
				}
			}
			XlsShape.ᜀ(opt, MsoOptions.PatternTexture, this.ᜩ, null, true);
			return opt;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0012E6BC File Offset: 0x0012D6BC
		protected virtual int SetPictureToBse(Image im, string strName)
		{
			int a_ = 18;
			int num = 1;
			XlsWorkbookShapeData shapesData;
			for (;;)
			{
				IL_1D:
				switch (num)
				{
				case 0:
					goto IL_5A;
				case 2:
					shapesData.RemovePicture((uint)(this.ᜩ - 1), true);
					num = 8;
					continue;
				case 3:
				{
					sprᜪ sprᜪ;
					if (sprᜪ != null)
					{
						num = 5;
						continue;
					}
					goto IL_11B;
				}
				case 4:
				{
					sprᜪ sprᜪ;
					if (sprᜪ.\u170D() <= 1U)
					{
						num = 2;
						continue;
					}
					goto IL_11B;
				}
				case 5:
					num = 4;
					continue;
				case 6:
					while (this.ᜩ >= 0)
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
							num = 7;
							goto IL_1D;
						}
					}
					goto IL_11B;
				case 7:
				{
					sprᜪ sprᜪ = shapesData.ᜀ(this.ᜩ);
					num = 3;
					continue;
				}
				case 8:
					goto IL_CD;
				}
				if (true)
				{
				}
				if (im == null)
				{
					num = 0;
				}
				else
				{
					shapesData = this.ᜣ.ShapesData;
					num = 6;
				}
			}
			IL_5A:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⅇ❉", a_));
			IL_CD:
			IL_11B:
			return shapesData.AddPicture(im, ImageFormatType.Original, strName);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0012E7F0 File Offset: 0x0012D7F0
		[CLSCompliant(false)]
		internal virtual sprᡍ SerializeTransparency(sprᡍ opt)
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
			XlsShapeLineFormat.ᜀ(opt, MsoOptions.GradientTransparency, this.\u171E);
			XlsShapeLineFormat.ᜀ(opt, MsoOptions.Transparency, this.\u171F);
			return opt;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0012E850 File Offset: 0x0012D850
		protected virtual void ChangeVisible()
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
			this.Visible = true;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0012E894 File Offset: 0x0012D894
		public virtual XlsShapeFill Clone(object parent)
		{
			int a_ = 13;
			while (parent == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("㍂⑄㕆ⱈ╊㥌", a_));
				}
			}
			XlsShapeFill xlsShapeFill = (XlsShapeFill)base.MemberwiseClone();
			xlsShapeFill.SetParent(parent);
			xlsShapeFill.ᜆ();
			xlsShapeFill.m_picture = (Image)spr\u1CD3.ᜀ(this.m_picture);
			return xlsShapeFill;
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0012E924 File Offset: 0x0012D924
		internal void ᜀ(XlsShapeFill A_0)
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
			this.m_fillType = A_0.m_fillType;
			this.\u171C = A_0.\u171C;
			this.\u171D = A_0.\u171D;
			this.\u171E = A_0.\u171E;
			this.\u171F = A_0.\u171F;
			this.ᜠ = A_0.ᜠ;
			this.ᜡ = A_0.ᜡ;
			this.ᜢ = A_0.ᜢ;
			this.ᜣ = A_0.ᜣ;
			this.ᜤ.ᜀ(A_0.ᜤ, false);
			this.ᜥ.ᜀ(A_0.ᜥ, false);
			this.ᜦ = A_0.ᜦ;
			this.m_picture = (Image)A_0.m_picture.Clone();
			this.ᜧ = A_0.ᜧ;
			this.ᜨ = A_0.ᜨ;
			this.ᜩ = A_0.ᜩ;
			this.ᜪ = A_0.ᜪ;
			this.ᜫ = (spr\u23E7.ᜀ)A_0.ᜫ.ᜇ();
			this.m_bIsShapeFill = A_0.m_bIsShapeFill;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0012EA64 File Offset: 0x0012DA64
		public static bool IsInverted(GradientStyleType gradientStyle, GradientVariantsType variant)
		{
			bool result;
			for (;;)
			{
				result = false;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_63;
					case 1:
						num = 3;
						continue;
					case 2:
						switch (gradientStyle)
						{
						case GradientStyleType.Horizontal:
						case GradientStyleType.Vertical:
						case GradientStyleType.Diagonl_Up:
						case GradientStyleType.From_Center:
							result = XlsShapeFill.ᜁ(variant);
							num = 4;
							continue;
						case GradientStyleType.Diagonl_Down:
							result = XlsShapeFill.ᜂ(variant);
							num = 0;
							continue;
						case GradientStyleType.From_Corner:
							result = false;
							goto IL_9E;
						default:
							num = 1;
							continue;
						}
						break;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							goto IL_94;
						}
						break;
					case 4:
						goto IL_74;
					case 5:
						goto IL_A9;
					}
					break;
					IL_9E:
					num = 5;
				}
			}
			IL_63:
			IL_74:
			goto IL_B5;
			IL_94:
			if (false)
			{
			}
			IL_A9:
			IL_B5:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0012EB30 File Offset: 0x0012DB30
		private static bool ᜂ(GradientVariantsType A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6D;
				case 1:
					num = 0;
					continue;
				case 2:
					num = 3;
					continue;
				case 3:
					if (A_0 != GradientVariantsType.ShadingVariants4)
					{
						num = 1;
						continue;
					}
					goto IL_52;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 5:
					goto IL_62;
				}
				if (A_0 != GradientVariantsType.ShadingVariants1)
				{
					num = 2;
					continue;
				}
				IL_52:
				if (true)
				{
				}
				num = 5;
			}
			IL_62:
			return true;
			IL_6D:
			return false;
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0012EBD0 File Offset: 0x0012DBD0
		private static bool ᜁ(GradientVariantsType A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6D;
				case 1:
					num = 2;
					continue;
				case 2:
					if (A_0 != GradientVariantsType.ShadingVariants4)
					{
						num = 5;
						continue;
					}
					goto IL_5A;
				case 3:
					goto IL_62;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5A;
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
				case 5:
					num = 0;
					continue;
				}
				if (A_0 != GradientVariantsType.ShadingVariants2)
				{
					num = 1;
					continue;
				}
				IL_5A:
				num = 3;
			}
			IL_62:
			return true;
			IL_6D:
			return false;
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0012EC70 File Offset: 0x0012DC70
		public static bool IsDoubled(GradientStyleType gradientStyle, GradientVariantsType variant)
		{
			bool result;
			for (;;)
			{
				result = false;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						num = 4;
						continue;
					case 2:
						return result;
					case 3:
						switch (gradientStyle)
						{
						case GradientStyleType.Horizontal:
						case GradientStyleType.Vertical:
						case GradientStyleType.Diagonl_Up:
						case GradientStyleType.Diagonl_Down:
						case GradientStyleType.From_Center:
							result = XlsShapeFill.ᜀ(variant);
							if (true)
							{
							}
							num = 2;
							continue;
						case GradientStyleType.From_Corner:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								result = false;
								num = 0;
								continue;
							}
							break;
						default:
							num = 1;
							continue;
						}
						break;
					case 4:
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0012ED2C File Offset: 0x0012DD2C
		private static bool ᜀ(GradientVariantsType A_0)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_65;
				case 1:
					goto IL_5A;
				case 2:
					num = 0;
					continue;
				case 3:
					if (A_0 != GradientVariantsType.ShadingVariants4)
					{
						num = 2;
						continue;
					}
					goto IL_52;
				case 4:
					if (true)
					{
					}
					num = 3;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (A_0 != GradientVariantsType.ShadingVariants3)
				{
					num = 4;
					continue;
				}
				IL_52:
				num = 1;
			}
			IL_5A:
			return true;
			IL_65:
			return false;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0012EDCC File Offset: 0x0012DDCC
		private static int ᜁ(GradientStyleType A_0)
		{
			int result;
			for (;;)
			{
				result = -1;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						return result;
					case 2:
						return result;
					case 3:
						switch (A_0)
						{
						case GradientStyleType.Horizontal:
							result = 5400000;
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
							break;
						case GradientStyleType.Vertical:
							result = 0;
							num = 6;
							continue;
						case GradientStyleType.Diagonl_Up:
							break;
						case GradientStyleType.Diagonl_Down:
							result = 18900000;
							num = 0;
							continue;
						default:
							num = 5;
							continue;
						}
						if (true)
						{
						}
						result = 2700000;
						num = 2;
						continue;
					case 4:
						return result;
					case 5:
						num = 1;
						continue;
					case 6:
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0012EEA8 File Offset: 0x0012DEA8
		private static Rectangle ᜀ(GradientStyleType A_0, GradientVariantsType A_1)
		{
			Rectangle result;
			for (;;)
			{
				result = Rectangle.Empty;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (A_0 == GradientStyleType.From_Center)
						{
							num = 2;
							continue;
						}
						return result;
					case 2:
						result = XlsShapeFill.\u1719;
						num = 4;
						continue;
					case 3:
						if (true)
						{
						}
						result = XlsShapeFill.\u171A[(int)A_1];
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
					case 4:
						return result;
					case 5:
						if (A_0 == GradientStyleType.From_Corner)
						{
							num = 3;
							continue;
						}
						num = 1;
						continue;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0012EF64 File Offset: 0x0012DF64
		private static GradientType ᜀ(GradientStyleType A_0)
		{
			for (;;)
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
							goto IL_1E;
						default:
							goto IL_72;
						}
						break;
					case 1:
						goto IL_1E;
					case 2:
						num = 0;
						continue;
					}
					break;
					IL_1E:
					switch (A_0)
					{
					case GradientStyleType.Horizontal:
					case GradientStyleType.Vertical:
					case GradientStyleType.Diagonl_Up:
					case GradientStyleType.Diagonl_Down:
						return GradientType.Liniar;
					case GradientStyleType.From_Corner:
					case GradientStyleType.From_Center:
						return GradientType.Rect;
					default:
						num = 2;
						break;
					}
				}
			}
			return GradientType.Rect;
			IL_72:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x04001173 RID: 4467
		private const int ᜀ = 166;

		// Token: 0x04001174 RID: 4468
		private const int ᜁ = 90;

		// Token: 0x04001175 RID: 4469
		private const int ᜂ = 121;

		// Token: 0x04001176 RID: 4470
		private const int ᜃ = 211;

		// Token: 0x04001177 RID: 4471
		internal const int ᜄ = 8;

		// Token: 0x04001178 RID: 4472
		private const int ᜅ = 1073741835;

		// Token: 0x04001179 RID: 4473
		public const string DEF_PATTERN_PREFIX = "Patt";

		// Token: 0x0400117A RID: 4474
		internal const string ᜆ = "Text";

		// Token: 0x0400117B RID: 4475
		private const string ᜇ = "Grad";

		// Token: 0x0400117C RID: 4476
		private const string ᜈ = "Pat_";

		// Token: 0x0400117D RID: 4477
		private const byte ᜉ = 16;

		// Token: 0x0400117E RID: 4478
		internal const int ᜊ = 80;

		// Token: 0x0400117F RID: 4479
		private const int ᜋ = 5;

		// Token: 0x04001180 RID: 4480
		private const int ᜌ = 6;

		// Token: 0x04001181 RID: 4481
		private const int \u170D = 25;

		// Token: 0x04001182 RID: 4482
		internal const int ᜎ = 100000;

		// Token: 0x04001183 RID: 4483
		internal const int ᜏ = 5400000;

		// Token: 0x04001184 RID: 4484
		internal const int ᜐ = 0;

		// Token: 0x04001185 RID: 4485
		internal const int ᜑ = 2700000;

		// Token: 0x04001186 RID: 4486
		internal const int \u1712 = 18900000;

		// Token: 0x04001187 RID: 4487
		private static readonly byte[] \u1713;

		// Token: 0x04001188 RID: 4488
		private static readonly byte[] \u1714;

		// Token: 0x04001189 RID: 4489
		private static readonly byte[] \u1715;

		// Token: 0x0400118A RID: 4490
		private static readonly byte[] \u1716;

		// Token: 0x0400118B RID: 4491
		private static readonly byte[] \u1717;

		// Token: 0x0400118C RID: 4492
		private static readonly byte[] \u1718;

		// Token: 0x0400118D RID: 4493
		public static readonly Color DEF_COMENT_PARSE_COLOR;

		// Token: 0x0400118E RID: 4494
		internal static Rectangle \u1719;

		// Token: 0x0400118F RID: 4495
		internal static Rectangle[] \u171A;

		// Token: 0x04001190 RID: 4496
		private static Dictionary<string, byte[]> \u171B;

		// Token: 0x04001191 RID: 4497
		protected ShapeFillType m_fillType;

		// Token: 0x04001192 RID: 4498
		private GradientStyleType \u171C;

		// Token: 0x04001193 RID: 4499
		private GradientVariantsType \u171D = GradientVariantsType.ShadingVariants2;

		// Token: 0x04001194 RID: 4500
		private double \u171E;

		// Token: 0x04001195 RID: 4501
		private double \u171F;

		// Token: 0x04001196 RID: 4502
		private GradientColorType ᜠ = GradientColorType.TwoColor;

		// Token: 0x04001197 RID: 4503
		private GradientPatternType ᜡ = GradientPatternType.Pat5Percent;

		// Token: 0x04001198 RID: 4504
		private GradientTextureType ᜢ = GradientTextureType.Papyrus;

		// Token: 0x04001199 RID: 4505
		private XlsWorkbook ᜣ;

		// Token: 0x0400119A RID: 4506
		private OColor ᜤ = spr\u1D39.ᜀ;

		// Token: 0x0400119B RID: 4507
		private OColor ᜥ = spr\u1D39.ᜉ;

		// Token: 0x0400119C RID: 4508
		private GradientPresetType ᜦ = GradientPresetType.GradEarlySunset;

		// Token: 0x0400119D RID: 4509
		protected Image m_picture;

		// Token: 0x0400119E RID: 4510
		private string ᜧ;

		// Token: 0x0400119F RID: 4511
		private bool ᜨ = true;

		// Token: 0x040011A0 RID: 4512
		private int ᜩ = -1;

		// Token: 0x040011A1 RID: 4513
		private double ᜪ = 0.2;

		// Token: 0x040011A2 RID: 4514
		private spr\u23E7.ᜀ ᜫ;

		// Token: 0x040011A3 RID: 4515
		protected bool m_bIsShapeFill = true;

		// Token: 0x040011A4 RID: 4516
		private bool ᜬ;

		// Token: 0x040011A5 RID: 4517
		private GradientStops ᜭ;

		// Token: 0x040011A6 RID: 4518
		private bool ᜮ = true;

		// Token: 0x040011A7 RID: 4519
		private static Assembly ᜯ;

		// Token: 0x040011A8 RID: 4520
		private static byte[] ᜰ;

		// Token: 0x040011A9 RID: 4521
		private bool \u2609\u00B0\u0093\u00A7;

		// Token: 0x040011AA RID: 4522
		private static Dictionary<GradientPresetType, byte[]> ᜱ;
	}
}
