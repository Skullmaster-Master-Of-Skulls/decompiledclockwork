using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000476 RID: 1142
	public class CharacterFormat : FormatBase
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06003F28 RID: 16168 RVA: 0x003A30E8 File Offset: 0x003A20E8
		// (set) Token: 0x06003F29 RID: 16169 RVA: 0x003A329C File Offset: 0x003A229C
		public Font Font
		{
			get
			{
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						float fontSize = this.FontSize;
						num = 10;
						continue;
					}
					case 1:
						if (this.FontSize != 0f)
						{
							num = 0;
							continue;
						}
						goto IL_111;
					case 2:
						goto IL_139;
					case 3:
						goto IL_79;
					case 4:
						num = 5;
						continue;
					case 5:
						if (this.ᝐ.Style != this.FontStyle)
						{
							num = 3;
							continue;
						}
						goto IL_1A0;
					case 6:
						num = 13;
						continue;
					case 8:
						num = 12;
						continue;
					case 9:
						goto IL_E9;
					case 10:
						goto IL_111;
					case 11:
						num = 9;
						continue;
					case 12:
						if (this.ᝐ != null)
						{
							num = 11;
							continue;
						}
						goto IL_1A0;
					case 13:
						if (this.ᝐ.Size == this.FontSize)
						{
							num = 4;
							continue;
						}
						goto IL_79;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_E9:
						if (!(this.ᝐ.Name != this.FontName))
						{
							num = 6;
							continue;
						}
						break;
					default:
						if (false)
						{
						}
						if (this.ᝐ != null)
						{
							num = 8;
							continue;
						}
						break;
					}
					IL_79:
					if (true)
					{
					}
					num = 1;
					continue;
					IL_111:
					this.ᝐ = spr\u215C.ᜀ(this.FontName, this.FontSize, this.FontStyle);
					num = 2;
				}
				IL_139:
				IL_1A0:
				return this.ᝐ;
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
				this.FontName = value.Name;
				this.FontSize = value.SizeInPoints;
				this.Bold = value.Bold;
				this.Italic = value.Italic;
				this.IsStrikeout = value.Strikeout;
				this.UnderlineStyle = (value.Underline ? UnderlineStyle.Single : UnderlineStyle.None);
				this.ᝐ = value;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x003A3330 File Offset: 0x003A2330
		internal FontStyle FontStyle
		{
			get
			{
				FontStyle fontStyle;
				for (;;)
				{
					for (;;)
					{
						fontStyle = FontStyle.Regular;
						int num = 7;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_A3;
							case 1:
								if (!this.IsStrikeout)
								{
									num = 6;
									continue;
								}
								goto IL_C3;
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
									fontStyle |= FontStyle.Underline;
									num = 5;
									continue;
								}
								break;
							case 3:
								fontStyle |= FontStyle.Italic;
								num = 9;
								continue;
							case 4:
								goto IL_C3;
							case 5:
								goto IL_5C;
							case 6:
								num = 12;
								continue;
							case 7:
								if (this.Bold)
								{
									num = 13;
									continue;
								}
								goto IL_A3;
							case 8:
								if (this.Italic)
								{
									num = 3;
									continue;
								}
								goto IL_105;
							case 9:
								goto IL_105;
							case 10:
								if (this.UnderlineStyle != UnderlineStyle.None)
								{
									num = 2;
									continue;
								}
								goto IL_5C;
							case 11:
								return fontStyle;
							case 12:
								if (this.DoubleStrike)
								{
									num = 4;
									continue;
								}
								return fontStyle;
							case 13:
								fontStyle |= FontStyle.Bold;
								num = 0;
								continue;
							}
							break;
							IL_5C:
							num = 1;
							continue;
							IL_A3:
							num = 8;
							continue;
							IL_C3:
							fontStyle |= FontStyle.Strikeout;
							num = 11;
							continue;
							IL_105:
							if (true)
							{
							}
							num = 10;
						}
					}
				}
				return fontStyle;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x003A348C File Offset: 0x003A248C
		// (set) Token: 0x06003F2C RID: 16172 RVA: 0x003A34D0 File Offset: 0x003A24D0
		public string FontName
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
				return this.ᜇ(2);
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
				base[2] = value;
				this.FontNameAscii = value;
				this.FontNameBidi = value;
				this.FontNameFarEast = value;
				this.FontNameNonFarEast = value;
				this.\u1718();
				base.SetPropUpdateFlag(2);
				this.ᜀ();
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x003A3544 File Offset: 0x003A2544
		// (set) Token: 0x06003F2E RID: 16174 RVA: 0x003A358C File Offset: 0x003A258C
		public float FontSize
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
				return (float)this.ᜃ(3);
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
				this.ᜁ(3, value);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06003F2F RID: 16175 RVA: 0x003A35D4 File Offset: 0x003A25D4
		// (set) Token: 0x06003F30 RID: 16176 RVA: 0x003A3618 File Offset: 0x003A2618
		internal bool ComplexScript
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
				return this.ᜄ(99);
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
				this.ᜁ(99, value);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x003A3664 File Offset: 0x003A2664
		// (set) Token: 0x06003F32 RID: 16178 RVA: 0x003A36A8 File Offset: 0x003A26A8
		public bool Bold
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
				return this.ᜄ(4);
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
				this.ᜁ(4, value);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x003A36F0 File Offset: 0x003A26F0
		// (set) Token: 0x06003F34 RID: 16180 RVA: 0x003A3734 File Offset: 0x003A2734
		public bool Italic
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
				return this.ᜄ(5);
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
				this.ᜁ(5, value);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x003A377C File Offset: 0x003A277C
		// (set) Token: 0x06003F36 RID: 16182 RVA: 0x003A37C0 File Offset: 0x003A27C0
		public bool IsStrikeout
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
				return this.ᜄ(6);
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
				this.ᜁ(6, value);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x003A3808 File Offset: 0x003A2808
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x003A384C File Offset: 0x003A284C
		public bool DoubleStrike
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
				return this.ᜄ(14);
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
				this.ᜁ(14, value);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x003A3898 File Offset: 0x003A2898
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x003A38E0 File Offset: 0x003A28E0
		public UnderlineStyle UnderlineStyle
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
				return (UnderlineStyle)this.ᜃ(7);
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
							break;
						default:
							if (false)
							{
							}
							this.ᜁ(7, value);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_74;
					}
					if (value.ToString().Length <= 3)
					{
						break;
					}
					num = 0;
				}
				IL_74:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x003A396C File Offset: 0x003A296C
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x003A39B4 File Offset: 0x003A29B4
		public Color TextColor
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
				return (Color)this.ᜃ(1);
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
				this.ᜁ(1, value);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x003A39FC File Offset: 0x003A29FC
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x003A3A44 File Offset: 0x003A2A44
		public Color TextBackgroundColor
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
				return (Color)this.ᜃ(9);
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
				this.ᜁ(9, value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06003F3F RID: 16191 RVA: 0x003A3A90 File Offset: 0x003A2A90
		// (set) Token: 0x06003F40 RID: 16192 RVA: 0x003A3AD8 File Offset: 0x003A2AD8
		public SubSuperScript SubSuperScript
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
				return (SubSuperScript)this.ᜃ(10);
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
				this.ᜁ(10, value);
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06003F41 RID: 16193 RVA: 0x003A3B24 File Offset: 0x003A2B24
		// (set) Token: 0x06003F42 RID: 16194 RVA: 0x003A3B6C File Offset: 0x003A2B6C
		public float CharacterSpacing
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
				return (float)this.ᜃ(18);
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
				this.ᜁ(18, value);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06003F43 RID: 16195 RVA: 0x003A3BB8 File Offset: 0x003A2BB8
		// (set) Token: 0x06003F44 RID: 16196 RVA: 0x003A3C00 File Offset: 0x003A2C00
		public float Position
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
				return (float)this.ᜃ(17);
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
				this.ᜁ(17, value);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06003F45 RID: 16197 RVA: 0x003A3C4C File Offset: 0x003A2C4C
		// (set) Token: 0x06003F46 RID: 16198 RVA: 0x003A3C90 File Offset: 0x003A2C90
		internal bool LineBreak
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
				return this.ᜆ();
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
				this.ᜅ();
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06003F47 RID: 16199 RVA: 0x003A3CD4 File Offset: 0x003A2CD4
		// (set) Token: 0x06003F48 RID: 16200 RVA: 0x003A3D18 File Offset: 0x003A2D18
		public bool IsShadow
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
				return this.ᜄ(50);
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
				this.ᜁ(50, value);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06003F49 RID: 16201 RVA: 0x003A3D64 File Offset: 0x003A2D64
		// (set) Token: 0x06003F4A RID: 16202 RVA: 0x003A3DA8 File Offset: 0x003A2DA8
		public bool Emboss
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
				return this.ᜄ(51);
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
				this.ᜁ(51, value);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x003A3DF4 File Offset: 0x003A2DF4
		// (set) Token: 0x06003F4C RID: 16204 RVA: 0x003A3E38 File Offset: 0x003A2E38
		public bool Engrave
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
				return this.ᜄ(52);
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
				this.ᜁ(52, value);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x003A3E84 File Offset: 0x003A2E84
		// (set) Token: 0x06003F4E RID: 16206 RVA: 0x003A3EC8 File Offset: 0x003A2EC8
		public bool Hidden
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
				return this.ᜄ(53);
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
				this.ᜁ(53, value);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x003A3F14 File Offset: 0x003A2F14
		// (set) Token: 0x06003F50 RID: 16208 RVA: 0x003A3F58 File Offset: 0x003A2F58
		public bool AllCaps
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
				return this.ᜄ(54);
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
				this.ᜁ(54, value);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x003A3FA4 File Offset: 0x003A2FA4
		// (set) Token: 0x06003F52 RID: 16210 RVA: 0x003A3FE8 File Offset: 0x003A2FE8
		public bool IsSmallCaps
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
				return this.ᜄ(55);
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
				this.ᜁ(55, value);
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06003F53 RID: 16211 RVA: 0x003A4034 File Offset: 0x003A3034
		// (set) Token: 0x06003F54 RID: 16212 RVA: 0x003A4078 File Offset: 0x003A3078
		public bool Bidi
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
				return this.ᜄ(58);
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
				this.ᜁ(58, value);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x003A40C4 File Offset: 0x003A30C4
		// (set) Token: 0x06003F56 RID: 16214 RVA: 0x003A4108 File Offset: 0x003A3108
		public bool BoldBidi
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
				return this.ᜄ(59);
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
				this.ᜁ(59, value);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06003F57 RID: 16215 RVA: 0x003A4154 File Offset: 0x003A3154
		// (set) Token: 0x06003F58 RID: 16216 RVA: 0x003A4198 File Offset: 0x003A3198
		public bool ItalicBidi
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
				return this.ᜄ(60);
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
				this.ᜁ(60, value);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06003F59 RID: 16217 RVA: 0x003A41E4 File Offset: 0x003A31E4
		// (set) Token: 0x06003F5A RID: 16218 RVA: 0x003A422C File Offset: 0x003A322C
		public float FontSizeBidi
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
				return (float)this.ᜃ(62);
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
				this.ᜁ(62, value);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06003F5B RID: 16219 RVA: 0x003A4278 File Offset: 0x003A3278
		// (set) Token: 0x06003F5C RID: 16220 RVA: 0x003A42DC File Offset: 0x003A32DC
		public string FontNameBidi
		{
			get
			{
				if (this.HasValue(61))
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
						break;
					}
					return (string)base[61];
				}
				return (string)base[2];
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
				base[61] = value;
				this.\u1718();
				base.SetPropUpdateFlag(61);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x003A4330 File Offset: 0x003A3330
		// (set) Token: 0x06003F5E RID: 16222 RVA: 0x003A4378 File Offset: 0x003A3378
		public Color HighlightColor
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
				return (Color)this.ᜃ(63);
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
				this.ᜁ(63, value);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06003F5F RID: 16223 RVA: 0x003A43C4 File Offset: 0x003A33C4
		public Border Border
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
				return this.ᜃ(67) as Border;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x003A440C File Offset: 0x003A340C
		// (set) Token: 0x06003F61 RID: 16225 RVA: 0x003A4470 File Offset: 0x003A3470
		internal string FontNameAscii
		{
			get
			{
				if (this.HasValue(68))
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
					return (string)base[68];
				}
				return (string)base[2];
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
				base[68] = value;
				base.SetPropUpdateFlag(68);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06003F62 RID: 16226 RVA: 0x003A44BC File Offset: 0x003A34BC
		// (set) Token: 0x06003F63 RID: 16227 RVA: 0x003A4520 File Offset: 0x003A3520
		internal string FontNameFarEast
		{
			get
			{
				if (this.HasValue(69))
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
						break;
					}
					return (string)base[69];
				}
				if (true)
				{
				}
				return (string)base[2];
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
				base[69] = value;
				base.SetPropUpdateFlag(69);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06003F64 RID: 16228 RVA: 0x003A456C File Offset: 0x003A356C
		// (set) Token: 0x06003F65 RID: 16229 RVA: 0x003A45D0 File Offset: 0x003A35D0
		internal string FontNameNonFarEast
		{
			get
			{
				if (true)
				{
				}
				if (this.HasValue(70))
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
						break;
					}
					return (string)base[70];
				}
				return (string)base[2];
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
				base[70] = value;
				base.SetPropUpdateFlag(70);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06003F66 RID: 16230 RVA: 0x003A461C File Offset: 0x003A361C
		// (set) Token: 0x06003F67 RID: 16231 RVA: 0x003A4664 File Offset: 0x003A3664
		internal bool IdctHint
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
				return this.IdctHintValue != Spire.Doc.Documents.IdctHint.Default;
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
				this.IdctHintValue = (value ? Spire.Doc.Documents.IdctHint.EastAsia : Spire.Doc.Documents.IdctHint.Default);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06003F68 RID: 16232 RVA: 0x003A46B0 File Offset: 0x003A36B0
		// (set) Token: 0x06003F69 RID: 16233 RVA: 0x003A4718 File Offset: 0x003A3718
		internal IdctHint IdctHintValue
		{
			get
			{
				if (this.HasValue(72))
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
						break;
					}
					if (true)
					{
					}
					return (IdctHint)Enum.ToObject(typeof(IdctHint), this.ᜃ(72));
				}
				return Spire.Doc.Documents.IdctHint.Default;
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
				this.ᜁ(72, Convert.ToByte(value));
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x003A476C File Offset: 0x003A376C
		// (set) Token: 0x06003F6B RID: 16235 RVA: 0x003A47B0 File Offset: 0x003A37B0
		public short LocaleIdASCII
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
				return this.ᜀ(73);
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
				this.ᜁ(73, value);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x003A47FC File Offset: 0x003A37FC
		// (set) Token: 0x06003F6D RID: 16237 RVA: 0x003A4840 File Offset: 0x003A3840
		public short LocaleIdFarEast
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
				return this.ᜀ(74);
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
				this.ᜁ(74, value);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x003A488C File Offset: 0x003A388C
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x003A48D4 File Offset: 0x003A38D4
		internal short RgLid3
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
				return (short)this.ᜃ(75);
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
				this.ᜁ(75, value);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x003A4920 File Offset: 0x003A3920
		// (set) Token: 0x06003F71 RID: 16241 RVA: 0x003A4968 File Offset: 0x003A3968
		internal short RgLid3_2
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
				return (short)this.ᜃ(76);
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
				this.ᜁ(76, value);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06003F72 RID: 16242 RVA: 0x003A49B4 File Offset: 0x003A39B4
		// (set) Token: 0x06003F73 RID: 16243 RVA: 0x003A49FC File Offset: 0x003A39FC
		internal short Lid
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
				return (short)this.ᜃ(77);
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
				this.ᜁ(77, value);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x003A4A48 File Offset: 0x003A3A48
		// (set) Token: 0x06003F75 RID: 16245 RVA: 0x003A4A90 File Offset: 0x003A3A90
		internal short LidBi
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
				return (short)this.ᜃ(78);
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
				this.ᜁ(78, value);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x003A4ADC File Offset: 0x003A3ADC
		// (set) Token: 0x06003F77 RID: 16247 RVA: 0x003A4B24 File Offset: 0x003A3B24
		internal bool IsWebHidden
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
				return (bool)this.ᜃ(125);
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
				this.ᜁ(125, value);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x003A4B70 File Offset: 0x003A3B70
		// (set) Token: 0x06003F79 RID: 16249 RVA: 0x003A4BB8 File Offset: 0x003A3BB8
		internal bool IsNoProof
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
				return (bool)this.ᜃ(79);
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
				this.ᜁ(79, value);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06003F7A RID: 16250 RVA: 0x003A4C04 File Offset: 0x003A3C04
		// (set) Token: 0x06003F7B RID: 16251 RVA: 0x003A4C4C File Offset: 0x003A3C4C
		internal Color ForeColor
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
				return (Color)this.ᜃ(80);
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
				this.ᜁ(80, value);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x003A4C98 File Offset: 0x003A3C98
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x003A4CE0 File Offset: 0x003A3CE0
		internal TextureStyle TextureStyle
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
				return (TextureStyle)this.ᜃ(81);
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
				this.ᜁ(81, value);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06003F7E RID: 16254 RVA: 0x003A4D2C File Offset: 0x003A3D2C
		// (set) Token: 0x06003F7F RID: 16255 RVA: 0x003A4D70 File Offset: 0x003A3D70
		public bool IsOutLine
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
				return this.ᜄ(71);
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
				this.ᜁ(71, value);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06003F80 RID: 16256 RVA: 0x003A4DBC File Offset: 0x003A3DBC
		// (set) Token: 0x06003F81 RID: 16257 RVA: 0x003A4E00 File Offset: 0x003A3E00
		internal bool IsSpecial
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
				return this.ᜄ(106);
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
				this.ᜁ(106, value);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x003A4E4C File Offset: 0x003A3E4C
		// (set) Token: 0x06003F83 RID: 16259 RVA: 0x003A4E90 File Offset: 0x003A3E90
		internal string CharStyleName
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
				return this.m_charStyleName;
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
				this.m_charStyleName = value;
				base.IsDefault = false;
				this.OnStateChange(this);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x003A4EE0 File Offset: 0x003A3EE0
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x003A4F24 File Offset: 0x003A3F24
		internal string NewCharStyleName
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

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x003A4F68 File Offset: 0x003A3F68
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x003A4FB0 File Offset: 0x003A3FB0
		internal bool IsInsertRevision
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
				return (bool)this.ᜃ(103);
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
				this.ᜁ(103, value);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x003A4FFC File Offset: 0x003A3FFC
		// (set) Token: 0x06003F89 RID: 16265 RVA: 0x003A5044 File Offset: 0x003A4044
		internal bool IsDeleteRevision
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
				return (bool)this.ᜃ(104);
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
				this.ᜁ(104, value);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x003A5090 File Offset: 0x003A4090
		// (set) Token: 0x06003F8B RID: 16267 RVA: 0x003A50D8 File Offset: 0x003A40D8
		internal bool IsChangedFormat
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
				return (bool)this.ᜃ(105);
			}
			set
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
							goto IL_67;
						}
						break;
					case 1:
						this.ᜁ(105, value);
						num = 0;
						continue;
					case 2:
						if (true)
						{
						}
						break;
					}
					if (!value)
					{
						return;
					}
					num = 1;
				}
				IL_67:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x003A5154 File Offset: 0x003A4154
		internal sprℵ CharacterProps
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
				return this.ᝌ;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x003A519C File Offset: 0x003A419C
		// (set) Token: 0x06003F8E RID: 16270 RVA: 0x003A51E0 File Offset: 0x003A41E0
		internal sprḍ Sprms
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
				return this.ᜊ;
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
				this.ᝌ = null;
				this.ᜊ = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06003F8F RID: 16271 RVA: 0x003A522C File Offset: 0x003A422C
		// (set) Token: 0x06003F90 RID: 16272 RVA: 0x003A5274 File Offset: 0x003A4274
		internal int ListPictureIndex
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
				return (int)this.ᜃ(107);
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
				this.ᜁ(107, value);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06003F91 RID: 16273 RVA: 0x003A52C0 File Offset: 0x003A42C0
		// (set) Token: 0x06003F92 RID: 16274 RVA: 0x003A5308 File Offset: 0x003A4308
		internal bool ListHasPicture
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
				return (bool)this.ᜃ(108);
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
				this.ᜁ(108, value);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06003F93 RID: 16275 RVA: 0x003A5354 File Offset: 0x003A4354
		internal sprᯉ CharStyle
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
				return this.ᜁ();
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06003F94 RID: 16276 RVA: 0x003A5398 File Offset: 0x003A4398
		// (set) Token: 0x06003F95 RID: 16277 RVA: 0x003A53DC File Offset: 0x003A43DC
		internal bool FieldVanish
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
				return this.ᜄ(109);
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
				this.ᜁ(109, value);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06003F96 RID: 16278 RVA: 0x003A5428 File Offset: 0x003A4428
		// (set) Token: 0x06003F97 RID: 16279 RVA: 0x003A5470 File Offset: 0x003A4470
		internal byte FieldVanishComplex
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
				return (byte)this.ᜃ(110);
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
				this.ᜁ(110, value);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06003F98 RID: 16280 RVA: 0x003A54BC File Offset: 0x003A44BC
		// (set) Token: 0x06003F99 RID: 16281 RVA: 0x003A5504 File Offset: 0x003A4504
		internal int PicLocation
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
				return (int)this.ᜃ(111);
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
				this.ᜁ(111, value);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06003F9A RID: 16282 RVA: 0x003A5550 File Offset: 0x003A4550
		// (set) Token: 0x06003F9B RID: 16283 RVA: 0x003A5594 File Offset: 0x003A4594
		internal CharacterFormat TableStyleCharacterFormat
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
				return this.ᝋ;
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
				this.ᝋ = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06003F9C RID: 16284 RVA: 0x003A55D8 File Offset: 0x003A45D8
		// (set) Token: 0x06003F9D RID: 16285 RVA: 0x003A5620 File Offset: 0x003A4620
		public bool AllowContextualAlternates
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
				return (bool)this.ᜃ(120);
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
				this.ᜁ(120, value);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06003F9E RID: 16286 RVA: 0x003A566C File Offset: 0x003A466C
		// (set) Token: 0x06003F9F RID: 16287 RVA: 0x003A56B4 File Offset: 0x003A46B4
		public LigatureType LigaturesType
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
				return (LigatureType)this.ᜃ(121);
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
				this.ᜁ(121, value);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06003FA0 RID: 16288 RVA: 0x003A5700 File Offset: 0x003A4700
		// (set) Token: 0x06003FA1 RID: 16289 RVA: 0x003A5748 File Offset: 0x003A4748
		public NumberFormType NumberFormType
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
				return (NumberFormType)this.ᜃ(122);
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
				this.ᜁ(122, value);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06003FA2 RID: 16290 RVA: 0x003A5794 File Offset: 0x003A4794
		// (set) Token: 0x06003FA3 RID: 16291 RVA: 0x003A57DC File Offset: 0x003A47DC
		public NumberSpaceType NumberSpaceType
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
				return (NumberSpaceType)this.ᜃ(123);
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
				this.ᜁ(123, value);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06003FA4 RID: 16292 RVA: 0x003A5828 File Offset: 0x003A4828
		// (set) Token: 0x06003FA5 RID: 16293 RVA: 0x003A5870 File Offset: 0x003A4870
		public StylisticSetType StylisticSetType
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
				return (StylisticSetType)this.ᜃ(124);
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
				this.ᜁ(124, value);
			}
		}

		// Token: 0x06003FA6 RID: 16294 RVA: 0x003A58BC File Offset: 0x003A48BC
		private CharacterFormat()
		{
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x003A58D0 File Offset: 0x003A48D0
		public CharacterFormat(IDocument doc) : base(doc)
		{
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x003A58E4 File Offset: 0x003A48E4
		private bool ᜆ()
		{
			switch (0)
			{
			default:
			{
				bool result;
				for (;;)
				{
					for (;;)
					{
						result = false;
						OwnerHolder ownerHolder = base.OwnerBase;
						int num = 1;
						for (;;)
						{
							bool flag;
							switch (num)
							{
							case 0:
								flag = true;
								goto IL_111;
							case 1:
								if (ownerHolder != null)
								{
									num = 11;
									continue;
								}
								return result;
							case 2:
							{
								Paragraph paragraph;
								int num2;
								if (paragraph.Items[num2 + 1] is Break)
								{
									num = 5;
									continue;
								}
								return result;
							}
							case 3:
								num = 10;
								continue;
							case 4:
								num = 2;
								continue;
							case 5:
							{
								Paragraph paragraph;
								int num2;
								Break @break = paragraph.Items[num2 + 1] as Break;
								num = 7;
								continue;
							}
							case 6:
							{
								Paragraph paragraph;
								int num2;
								if (num2 < paragraph.Items.Count - 1)
								{
									num = 4;
									continue;
								}
								return result;
							}
							case 7:
							{
								Break @break;
								if (@break.BreakType != BreakType.LineBreak)
								{
									num = 3;
									continue;
								}
								num = 0;
								continue;
							}
							case 8:
							{
								Paragraph paragraph = ownerHolder.OwnerBase as Paragraph;
								int num2 = paragraph.Items.IndexOf(ownerHolder as IDocumentObject);
								num = 6;
								continue;
							}
							case 9:
								return result;
							case 10:
								flag = false;
								goto IL_111;
							case 11:
								num = 12;
								continue;
							case 12:
								if (true)
								{
								}
								if (!(ownerHolder.OwnerBase is Paragraph))
								{
									return result;
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
									num = 8;
									continue;
								}
								break;
							}
							break;
							IL_111:
							result = flag;
							num = 9;
						}
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x003A5AA4 File Offset: 0x003A4AA4
		private void ᜅ()
		{
			for (;;)
			{
				IL_1C:
				OwnerHolder ownerHolder = base.OwnerBase;
				for (;;)
				{
					IL_23:
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_23;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						case 1:
						{
							if (true)
							{
							}
							Paragraph paragraph = ownerHolder.OwnerBase as Paragraph;
							int index = paragraph.Items.IndexOf(ownerHolder as IDocumentObject) + 1;
							paragraph.Items.Insert(index, new Break(paragraph.Document, BreakType.LineBreak));
							num = 2;
							continue;
						}
						case 2:
							return;
						case 3:
							if (ownerHolder.OwnerBase is Paragraph)
							{
								num = 1;
								continue;
							}
							return;
						case 4:
							if (ownerHolder != null)
							{
								num = 0;
								continue;
							}
							return;
						}
						goto IL_1C;
					}
				}
			}
		}

		// Token: 0x06003FAA RID: 16298 RVA: 0x003A5B88 File Offset: 0x003A4B88
		private void ᜄ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_51;
				case 2:
					goto IL_51;
				case 3:
					this.ᝌ = new sprℵ(null);
					this.ᝌ.ᜐ().ᜀ(this.ᜊ);
					num = 2;
					continue;
				case 4:
					if (base.Document.ᜇ)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 5:
					if (this.ᜊ != null)
					{
						num = 3;
						continue;
					}
					this.ᝌ = new sprℵ(null);
					this.ᜊ = this.ᝌ.ᜢ();
					num = 1;
					continue;
				case 6:
					return;
				case 7:
					if (true)
					{
					}
					num = 5;
					continue;
				case 8:
					this.ᝏ = true;
					goto IL_E3;
				}
				if (this.ᝌ == null)
				{
					num = 7;
					continue;
				}
				break;
				IL_51:
				num = 4;
				continue;
				IL_E3:
				num = 6;
			}
		}

		// Token: 0x06003FAB RID: 16299 RVA: 0x003A5CC4 File Offset: 0x003A4CC4
		internal object ᜃ(int A_0)
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
			this.ᜂ(A_0);
			return base[A_0];
		}

		// Token: 0x06003FAC RID: 16300 RVA: 0x003A5D10 File Offset: 0x003A4D10
		internal bool ᜄ(short A_0)
		{
			int num = 11;
			bool flag;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					base[(int)A_0] = this.ᜂ(A_0);
					num = 4;
					continue;
				case 1:
					if (this.ᜁ(A_0))
					{
						num = 0;
						continue;
					}
					goto IL_5A;
				case 2:
					if (this.ᜁ(A_0))
					{
						num = 3;
						continue;
					}
					goto IL_5A;
				case 3:
					goto IL_144;
				case 4:
					goto IL_5A;
				case 5:
					num = 7;
					continue;
				case 6:
					goto IL_142;
				case 7:
					if (base.IsPropertyUpdated((int)A_0))
					{
						num = 8;
						continue;
					}
					goto IL_144;
				case 8:
					goto IL_AC;
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
						if ((bool)this.ᝋ[(int)A_0])
						{
							num = 6;
							continue;
						}
						return flag;
					}
					break;
				case 10:
					if (this.ᝋ != null)
					{
						num = 12;
						continue;
					}
					return flag;
				case 12:
					num = 9;
					continue;
				}
				IL_44:
				if (!base.HasKey((int)A_0))
				{
					num = 5;
					continue;
				}
				goto IL_AC;
				goto IL_44;
				IL_5A:
				flag = (bool)base[(int)A_0];
				num = 10;
				continue;
				IL_AC:
				num = 2;
				continue;
				IL_144:
				this.ᜄ();
				base.SetPropUpdateFlag((int)A_0);
				num = 1;
			}
			IL_142:
			return !flag;
		}

		// Token: 0x06003FAD RID: 16301 RVA: 0x003A5E98 File Offset: 0x003A4E98
		private bool ᜁ(short A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
				{
					int sprmOption;
					if (this.ᜊ.ᜇ(sprmOption) != null)
					{
						num = 0;
						continue;
					}
					return false;
				}
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
						break;
					}
					break;
				case 3:
				{
					int sprmOption = this.GetSprmOption((int)A_0);
					if (true)
					{
					}
					num = 1;
					continue;
				}
				}
				if (this.ᜊ == null)
				{
					return false;
				}
				num = 3;
			}
			return true;
		}

		// Token: 0x06003FAE RID: 16302 RVA: 0x003A5F30 File Offset: 0x003A4F30
		internal void ᜀ(sprℵ A_0)
		{
			for (;;)
			{
				IL_30:
				spr\u1CC1 spr_u1CC = A_0.ᜢ().ᜇ(2178);
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
						switch (num)
						{
						case 0:
							return;
						case 1:
							base[99] = A_0.\u173E();
							num = 0;
							continue;
						case 2:
							if (spr_u1CC != null)
							{
								goto IL_4C;
							}
							return;
						}
						goto IL_30;
					}
					IL_4C:
					if (true)
					{
					}
					num = 1;
				}
			}
		}

		// Token: 0x06003FAF RID: 16303 RVA: 0x003A5FC8 File Offset: 0x003A4FC8
		private void ᜁ(int A_0, object A_1)
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
			base[A_0] = A_1;
			this.ᜀ(A_0, A_1);
			this.OnStateChange(this);
		}

		// Token: 0x06003FB0 RID: 16304 RVA: 0x003A601C File Offset: 0x003A501C
		private void ᜀ(int A_0, object A_1)
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
			this.\u1718();
			this.ᜄ();
			base.SetPropUpdateFlag(A_0);
			spr\u1AFF.ᜀ(A_0, A_1, this.ᝌ, this);
		}

		// Token: 0x06003FB1 RID: 16305 RVA: 0x003A6078 File Offset: 0x003A5078
		private void ᜂ(int A_0)
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
					return;
				case 2:
					if (base.IsPropertyUpdated(A_0))
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_7D;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (!base.HasKey(A_0))
				{
					goto IL_7D;
				}
				num = 0;
			}
			return;
			IL_7D:
			this.ᜄ();
			base.SetPropUpdateFlag(A_0);
			this.ᜀ(A_0);
		}

		// Token: 0x06003FB2 RID: 16306 RVA: 0x003A6118 File Offset: 0x003A5118
		internal void ᜇ(int A_0)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 6;
					continue;
				case 1:
					if (this.ᜁ((short)A_0))
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					return;
				case 3:
					this.ᜄ();
					base.SetPropUpdateFlag(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					goto IL_A8;
				case 5:
					num = 7;
					continue;
				case 6:
					if (!base.IsPropertyUpdated(A_0))
					{
						num = 3;
						continue;
					}
					return;
				case 7:
					if (!base.HasKey(A_0))
					{
						num = 0;
						continue;
					}
					return;
				}
				if (this.ᜁ(A_0))
				{
					num = 5;
				}
				else
				{
					this.ᜂ(A_0);
					num = 2;
				}
			}
			IL_A8:
			base[A_0] = this.ᜂ((short)A_0);
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x003A623C File Offset: 0x003A523C
		private void ᜃ()
		{
			for (;;)
			{
				spr\u1CC1 spr_u1CC = this.ᝌ.ᜢ().ᜇ(51799);
				if (true)
				{
				}
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
							goto IL_49;
						default:
							if (false)
							{
							}
							base[105] = this.ᝌ.ᜣ();
							num = 4;
							continue;
						}
						break;
					case 1:
						if (spr_u1CC != null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						if (spr_u1CC == null)
						{
							goto IL_49;
						}
						goto IL_75;
					case 3:
						spr_u1CC = this.ᝌ.ᜢ().ᜇ(51849);
						num = 5;
						continue;
					case 4:
						return;
					case 5:
						goto IL_75;
					}
					break;
					IL_49:
					num = 3;
					continue;
					IL_75:
					num = 1;
				}
			}
		}

		// Token: 0x06003FB4 RID: 16308 RVA: 0x003A6320 File Offset: 0x003A5320
		private bool ᜁ(int A_0)
		{
			for (;;)
			{
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 != 14)
						{
							num = 4;
							continue;
						}
						return true;
					case 1:
						num = 13;
						continue;
					case 2:
						num = 5;
						continue;
					case 3:
						goto IL_153;
					case 4:
						num = 6;
						continue;
					case 5:
						if (A_0 != 99)
						{
							num = 14;
							continue;
						}
						return true;
					case 6:
						switch (A_0)
						{
						case 50:
						case 51:
						case 52:
						case 53:
						case 54:
						case 55:
						case 58:
						case 59:
						case 60:
							return true;
						case 56:
						case 57:
							return false;
						default:
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 7:
						switch (A_0)
						{
						case 4:
						case 5:
						case 6:
							return true;
						default:
							num = 15;
							continue;
						}
						break;
					case 8:
						if (A_0 == 109)
						{
							num = 10;
							continue;
						}
						return false;
					case 9:
						num = 7;
						continue;
					case 10:
						return true;
					case 11:
						if (A_0 <= 60)
						{
							num = 9;
							continue;
						}
						num = 18;
						continue;
					case 12:
						switch (A_0)
						{
						case 71:
						case 72:
							return true;
						default:
							num = 2;
							continue;
						}
						break;
					case 13:
						return false;
					case 14:
						num = 17;
						continue;
					case 15:
						num = 0;
						continue;
					case 16:
						if (A_0 != 106)
						{
							num = 3;
							continue;
						}
						return true;
					case 17:
						goto IL_7C;
					case 18:
						if (A_0 <= 99)
						{
							num = 19;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_153;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					case 19:
						num = 12;
						continue;
					}
					break;
					IL_153:
					num = 8;
				}
			}
			IL_7C:
			return false;
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x003A6538 File Offset: 0x003A5538
		internal void ᜁ(sprℵ A_0)
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
			this.ᜀ(18527, 78, A_0);
			this.ᜀ(19009, 77, A_0);
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x003A6590 File Offset: 0x003A5590
		private void ᜀ(int A_0, short A_1, sprℵ A_2)
		{
			if (true)
			{
			}
			for (;;)
			{
				spr\u1CC1 spr_u1CC = A_2.ᜢ().ᜇ(A_0);
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
							goto IL_5A;
						default:
							goto IL_78;
						}
						break;
					case 1:
						base[(int)A_1] = spr_u1CC.ᜐ();
						goto IL_5A;
					case 2:
						if (spr_u1CC != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
					IL_5A:
					num = 0;
				}
			}
			IL_78:
			if (false)
			{
			}
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x003A6620 File Offset: 0x003A5620
		internal void ᜂ(sprℵ A_0)
		{
			for (;;)
			{
				this.ᜀ(19023, 68, A_0);
				this.ᜀ(19024, 69, A_0);
				this.ᜀ(19025, 70, A_0);
				this.ᜀ(19038, 61, A_0);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (base.HasKey(68))
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_89;
						default:
							goto IL_AA;
						}
						break;
					case 2:
						base[2] = base[68];
						goto IL_89;
					}
					break;
					IL_89:
					num = 1;
				}
			}
			IL_AA:
			if (false)
			{
			}
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x003A66E0 File Offset: 0x003A56E0
		private void ᜀ(int A_0, int A_1, sprℵ A_2)
		{
			for (;;)
			{
				spr\u1CC1 spr_u1CC = A_2.ᜢ().ᜇ(A_0);
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						base[A_1] = A_2.ᜄ(spr_u1CC);
						goto IL_56;
					case 1:
						if (spr_u1CC != null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_56;
						default:
							goto IL_74;
						}
						break;
					}
					break;
					IL_56:
					num = 2;
				}
			}
			IL_74:
			if (false)
			{
			}
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x003A676C File Offset: 0x003A576C
		internal void ᜀ(spr\u2305 A_0)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(68, A_0);
					num = 11;
					continue;
				case 1:
					goto IL_98;
				case 2:
					this.ᜀ(2, A_0);
					num = 1;
					continue;
				case 4:
					goto IL_E4;
				case 5:
					if (true)
					{
					}
					if (!base.HasKey(68))
					{
						goto IL_15C;
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
						num = 0;
						continue;
					}
					break;
				case 6:
					return;
				case 7:
					if (base.HasKey(69))
					{
						num = 13;
						continue;
					}
					goto IL_121;
				case 8:
					goto IL_121;
				case 9:
					if (base.HasKey(61))
					{
						num = 14;
						continue;
					}
					return;
				case 10:
					if (base.HasKey(70))
					{
						num = 12;
						continue;
					}
					goto IL_E4;
				case 11:
					goto IL_15C;
				case 12:
					goto IL_141;
				case 13:
					this.ᜀ(69, A_0);
					num = 8;
					continue;
				case 14:
					this.ᜀ(61, A_0);
					num = 6;
					continue;
				}
				if (base.HasKey(2))
				{
					num = 2;
					continue;
				}
				IL_98:
				num = 5;
				continue;
				IL_E4:
				num = 9;
				continue;
				IL_121:
				num = 10;
				continue;
				IL_141:
				this.ᜀ(70, A_0);
				num = 4;
				continue;
				IL_15C:
				num = 7;
			}
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x003A6910 File Offset: 0x003A5910
		private void ᜀ(int A_0, spr\u2305 A_1)
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
			this.CharacterProps.ᜀ(A_1);
			spr\u1AFF.ᜀ(A_0, base[A_0], this.ᝌ, this);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x003A696C File Offset: 0x003A596C
		internal bool \u1719()
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
					goto IL_85;
				case 1:
					num = 2;
					continue;
				case 2:
					if (this.ᝌ.ᜢ().ᜈ() <= 0)
					{
						num = 0;
						continue;
					}
					return true;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				}
				if (false)
				{
				}
				if (this.ᝌ == null)
				{
					break;
				}
				num = 1;
			}
			return false;
			IL_85:
			return false;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x003A6A04 File Offset: 0x003A5A04
		private bool ᜂ()
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						goto IL_74;
					}
					break;
				case 2:
					if (this.ᜊ != null)
					{
						num = 0;
						continue;
					}
					return true;
				case 3:
					return false;
				}
				if (base.Document == null)
				{
					num = 3;
					continue;
				}
				IL_34:
				num = 2;
			}
			return false;
			IL_74:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x003A6A90 File Offset: 0x003A5A90
		internal new byte ᜊ(short A_0)
		{
			int sprmOption = this.GetSprmOption((int)A_0);
			if (this.ᜊ != null)
			{
				byte result;
				try
				{
					for (;;)
					{
						spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(sprmOption);
						int num;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_7F:
							num = 2;
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
								if (spr_u1CC != null)
								{
									num = 1;
									continue;
								}
								goto IL_7F;
							case 1:
								result = spr_u1CC.\u1714();
								num = 3;
								continue;
							case 2:
								goto IL_87;
							case 3:
								goto IL_7D;
							}
							break;
						}
					}
					IL_7D:
					goto IL_92;
					IL_87:
					return byte.MaxValue;
				}
				catch
				{
					result = byte.MaxValue;
				}
				IL_92:
				if (true)
				{
				}
				return result;
			}
			return byte.MaxValue;
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x003A6B54 File Offset: 0x003A5B54
		internal bool ᜂ(short A_0)
		{
			for (;;)
			{
				byte b = this.ᜊ(A_0);
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_166;
						default:
							goto IL_134;
						}
						break;
					case 1:
						return true;
					case 2:
						if (b == 1)
						{
							num = 17;
							continue;
						}
						num = 9;
						continue;
					case 3:
						num = 16;
						continue;
					case 4:
						if (b == 129)
						{
							goto IL_166;
						}
						goto IL_FC;
					case 5:
						num = 6;
						continue;
					case 6:
						if (base.BaseFormat != null)
						{
							num = 10;
							continue;
						}
						goto IL_153;
					case 7:
						if (b == 129)
						{
							num = 12;
							continue;
						}
						goto IL_97;
					case 8:
						goto IL_1EE;
					case 9:
						if (b == 255)
						{
							num = 11;
							continue;
						}
						return false;
					case 10:
						goto IL_194;
					case 11:
						num = 13;
						continue;
					case 12:
						if (true)
						{
						}
						num = 15;
						continue;
					case 13:
						if (base.BaseFormat != null)
						{
							num = 0;
							continue;
						}
						return false;
					case 14:
						if (b == 128)
						{
							num = 5;
							continue;
						}
						goto IL_153;
					case 15:
						if (base.BaseFormat != null)
						{
							num = 8;
							continue;
						}
						goto IL_97;
					case 16:
						if (base.BaseFormat == null)
						{
							num = 1;
							continue;
						}
						goto IL_FC;
					case 17:
						return true;
					}
					break;
					IL_97:
					num = 14;
					continue;
					IL_FC:
					num = 2;
					continue;
					IL_153:
					num = 4;
					continue;
					IL_166:
					num = 3;
				}
			}
			return true;
			IL_134:
			if (false)
			{
			}
			return this.ᜀ(this.m_charStyleName, base.BaseFormat as CharacterFormat, A_0);
			IL_194:
			return this.ᜀ(this.m_charStyleName, base.BaseFormat as CharacterFormat, A_0);
			IL_1EE:
			return !this.ᜀ(this.m_charStyleName, base.BaseFormat as CharacterFormat, A_0);
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x003A6D70 File Offset: 0x003A5D70
		private byte ᜀ(CharacterFormat A_0, short A_1)
		{
			byte result;
			for (;;)
			{
				result = 0;
				int num = 0;
				for (;;)
				{
					byte b;
					switch (num)
					{
					case 0:
						goto IL_36;
					case 1:
						return result;
					case 2:
						return result;
					case 3:
					{
						if (true)
						{
						}
						bool flag = A_0.ᜂ(A_1);
						num = 8;
						continue;
					}
					case 4:
						b = 1;
						goto IL_9B;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 6:
						if (A_0.HasValue((int)A_1))
						{
							num = 3;
							continue;
						}
						return result;
					case 7:
						b = 0;
						goto IL_9B;
					case 8:
					{
						bool flag;
						if (!flag)
						{
							num = 5;
							continue;
						}
						num = 4;
						continue;
					}
					}
					break;
					IL_36:
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num = 6;
					continue;
					IL_9B:
					result = b;
					num = 1;
				}
			}
			return result;
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x003A6E58 File Offset: 0x003A5E58
		private bool ᜀ(string A_0, CharacterFormat A_1, short A_2)
		{
			if (true)
			{
			}
			byte b;
			for (;;)
			{
				b = 0;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						sprᯉ sprᯉ;
						if (sprᯉ != null)
						{
							num = 1;
							continue;
						}
						goto IL_4F;
					}
					case 1:
					{
						sprᯉ sprᯉ;
						b = this.ᜀ(sprᯉ.CharacterFormat, A_2);
						num = 8;
						continue;
					}
					case 2:
					{
						sprᯉ sprᯉ = base.Document.Styles.FindByName(A_0, StyleType.CharacterStyle) as sprᯉ;
						num = 0;
						continue;
					}
					case 3:
						if (b == 1)
						{
							num = 4;
							continue;
						}
						goto IL_4F;
					case 4:
						return true;
					case 5:
						if (A_0 != null)
						{
							num = 2;
							continue;
						}
						goto IL_4F;
					case 6:
						if (b != 129)
						{
							num = 7;
							continue;
						}
						return true;
					case 7:
						goto IL_92;
					case 8:
						if (b != 129)
						{
							num = 9;
							continue;
						}
						return true;
					case 9:
						goto IL_94;
					}
					break;
					IL_4F:
					b = this.ᜀ(A_1, A_2);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_94:
						num = 3;
						break;
					default:
						if (false)
						{
						}
						num = 6;
						break;
					}
				}
			}
			IL_92:
			return b == 1;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x003A6F90 File Offset: 0x003A5F90
		private CharacterFormat ᜁ(CharacterFormat A_0)
		{
			int num = 4;
			sprᯉ sprᯉ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (sprᯉ != null)
					{
						num = 2;
						continue;
					}
					goto IL_C7;
				case 1:
					sprᯉ = (base.Document.Styles.FindByName(A_0.CharStyleName, StyleType.CharacterStyle) as sprᯉ);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_91;
				case 3:
					goto IL_33;
				case 5:
					if (A_0.CharStyleName != null)
					{
						num = 1;
						continue;
					}
					goto IL_C7;
				}
				goto IL_28;
				IL_2B:
				num = 3;
				continue;
				IL_28:
				if (A_0 == null)
				{
					goto IL_2B;
				}
				num = 5;
			}
			IL_33:
			return null;
			IL_91:
			if (true)
			{
			}
			return sprᯉ.CharacterFormat;
			IL_C7:
			return A_0.BaseFormat as CharacterFormat;
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x003A7070 File Offset: 0x003A6070
		internal override void RemoveChanges()
		{
			for (;;)
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_4C:
					if (this.ᜊ == null)
					{
						return;
					}
					num = 3;
					break;
				default:
					if (false)
					{
					}
					this.\u1718();
					base.RemoveChanges();
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4C;
					case 1:
						return;
					case 2:
						if (this.ᜊ.ᜇ(18992) == null)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						num = 2;
						continue;
					case 4:
						if (true)
						{
						}
						this.m_charStyleName = null;
						this.ᝊ = null;
						num = 1;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x003A7134 File Offset: 0x003A6134
		internal override void AcceptChanges()
		{
			for (;;)
			{
				base[104] = false;
				base[103] = false;
				base[105] = false;
				int num = 33;
				for (;;)
				{
					Dictionary<int, object> dictionary;
					switch (num)
					{
					case 0:
						goto IL_41E;
					case 1:
						if (this.ᜊ.ᜂ(19025))
						{
							num = 34;
							continue;
						}
						goto IL_291;
					case 2:
						num = 36;
						continue;
					case 3:
						dictionary.Add(69, base[69]);
						num = 25;
						continue;
					case 4:
						goto IL_291;
					case 5:
						dictionary.Add(68, base[68]);
						num = 23;
						continue;
					case 6:
						base[61] = dictionary[61];
						num = 20;
						continue;
					case 7:
						if (this.ᜊ.ᜂ(19023))
						{
							num = 26;
							continue;
						}
						goto IL_41E;
					case 8:
						base[68] = dictionary[68];
						base[2] = dictionary[68];
						num = 0;
						continue;
					case 9:
						dictionary.Add(61, base[61]);
						num = 19;
						continue;
					case 10:
						if (base.HasKey(69))
						{
							num = 3;
							continue;
						}
						goto IL_3D9;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1BF;
						default:
							if (false)
							{
							}
							if (dictionary.ContainsKey(69))
							{
								num = 41;
								continue;
							}
							goto IL_3A9;
						}
						break;
					case 12:
						base[70] = dictionary[70];
						num = 4;
						continue;
					case 13:
						num = 32;
						continue;
					case 14:
						if (dictionary.ContainsKey(70))
						{
							num = 12;
							continue;
						}
						goto IL_291;
					case 15:
						this.m_charStyleName = this.ᝊ;
						num = 17;
						continue;
					case 16:
						if (this.ᜊ.ᜂ(19024))
						{
							num = 27;
							continue;
						}
						goto IL_3A9;
					case 17:
						goto IL_153;
					case 18:
						if (this.ᝊ != null)
						{
							num = 2;
							continue;
						}
						goto IL_153;
					case 19:
						goto IL_35C;
					case 20:
						goto IL_2BE;
					case 21:
						this.\u1718();
						this.ᜊ.ᜆ(2049);
						this.ᜊ.ᜆ(2048);
						this.ᜊ.ᜆ(18436);
						this.ᜊ.ᜆ(18531);
						this.ᜊ.ᜆ(26629);
						this.ᜊ.ᜆ(26724);
						this.ᜊ.ᜆ(51799);
						this.ᜊ.ᜆ(51849);
						num = 18;
						continue;
					case 22:
						if (this.ᜊ.ᜂ(19038))
						{
							num = 13;
							continue;
						}
						goto IL_2BE;
					case 23:
						goto IL_269;
					case 24:
						num = 29;
						continue;
					case 25:
						goto IL_3D9;
					case 26:
						num = 31;
						continue;
					case 27:
						num = 11;
						continue;
					case 28:
						goto IL_233;
					case 29:
						if (this.ᜊ.ᜇ() > 0)
						{
							num = 21;
							continue;
						}
						return;
					case 30:
						goto IL_3A9;
					case 31:
						if (true)
						{
						}
						if (dictionary.ContainsKey(68))
						{
							num = 8;
							continue;
						}
						goto IL_41E;
					case 32:
						if (dictionary.ContainsKey(61))
						{
							num = 6;
							continue;
						}
						goto IL_2BE;
					case 33:
						if (this.ᜊ != null)
						{
							num = 24;
							continue;
						}
						return;
					case 34:
						num = 14;
						continue;
					case 35:
						return;
					case 36:
						if (this.m_charStyleName != this.ᝊ)
						{
							goto IL_1BF;
						}
						goto IL_153;
					case 37:
						if (base.HasKey(70))
						{
							num = 39;
							continue;
						}
						goto IL_233;
					case 38:
						if (base.HasKey(61))
						{
							num = 9;
							continue;
						}
						goto IL_35C;
					case 39:
						dictionary.Add(70, base[70]);
						num = 28;
						continue;
					case 40:
						if (base.HasKey(68))
						{
							num = 5;
							continue;
						}
						goto IL_269;
					case 41:
						base[69] = dictionary[69];
						num = 30;
						continue;
					}
					break;
					IL_153:
					dictionary = new Dictionary<int, object>();
					num = 40;
					continue;
					IL_1BF:
					num = 15;
					continue;
					IL_233:
					base.AcceptChanges();
					num = 7;
					continue;
					IL_269:
					num = 38;
					continue;
					IL_291:
					num = 22;
					continue;
					IL_2BE:
					dictionary.Clear();
					num = 35;
					continue;
					IL_35C:
					num = 10;
					continue;
					IL_3A9:
					num = 1;
					continue;
					IL_3D9:
					num = 37;
					continue;
					IL_41E:
					num = 16;
				}
			}
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x003A76BC File Offset: 0x003A66BC
		internal void \u1718()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					if (this.ᜊ == null)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
				case 3:
					return;
				case 4:
					if (this.ᝎ)
					{
						num = 5;
						continue;
					}
					goto IL_E4;
				case 5:
					if (true)
					{
					}
					this.ᝎ = false;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 6:
					return;
				case 7:
					if (this.ᝏ)
					{
						num = 6;
						continue;
					}
					num = 4;
					continue;
				case 8:
					goto IL_C0;
				}
				IL_34:
				if (base.Document.ᜇ)
				{
					num = 3;
					continue;
				}
				num = 2;
				continue;
				goto IL_34;
			}
			return;
			IL_C0:
			IL_E4:
			this.ᝏ = true;
			this.ᜊ = this.ᜊ.ᜀ();
			this.ᝌ = null;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x003A77CC File Offset: 0x003A67CC
		private bool ᜀ(IDocumentObject A_0)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
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
					num = 6;
					continue;
				case 2:
					num = 5;
					continue;
				case 3:
				{
					CharacterFormat characterFormat = (A_0 as ParagraphBase).ឬ();
					num = 9;
					continue;
				}
				case 4:
					if (A_0 is ParagraphBase)
					{
						num = 3;
						continue;
					}
					return false;
				case 5:
				{
					CharacterFormat characterFormat;
					if (characterFormat.Sprms == this.ᜊ)
					{
						num = 0;
						continue;
					}
					return false;
				}
				case 6:
					if ((A_0 as Paragraph).BreakCharacterFormat.Sprms == this.ᜊ)
					{
						num = 8;
						continue;
					}
					return false;
				case 8:
					goto IL_D7;
				case 9:
				{
					CharacterFormat characterFormat;
					if (characterFormat != null)
					{
						num = 2;
						continue;
					}
					return false;
				}
				}
				if (A_0 is Paragraph)
				{
					num = 1;
				}
				else
				{
					num = 4;
				}
			}
			return true;
			IL_D7:
			if (true)
			{
			}
			return true;
		}

		// Token: 0x06003FC6 RID: 16326 RVA: 0x003A78F4 File Offset: 0x003A68F4
		internal bool ᜆ(short A_0)
		{
			int num = 4;
			for (;;)
			{
				spr\u1CC1 spr_u1CC;
				switch (num)
				{
				case 0:
					if (spr_u1CC.\u1714() < 128)
					{
						num = 2;
						continue;
					}
					return true;
				case 1:
					goto IL_AC;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AC;
					default:
						goto IL_53;
					}
					break;
				case 3:
				{
					int sprmOption = this.GetSprmOption((int)A_0);
					spr_u1CC = this.ᜊ.ᜇ(sprmOption);
					if (true)
					{
					}
					num = 1;
					continue;
				}
				case 5:
					num = 0;
					continue;
				}
				if (this.ᜊ != null)
				{
					num = 3;
					continue;
				}
				return false;
				IL_AC:
				if (spr_u1CC == null)
				{
					return false;
				}
				num = 5;
			}
			IL_53:
			if (false)
			{
			}
			return false;
		}

		// Token: 0x06003FC7 RID: 16327 RVA: 0x003A79C0 File Offset: 0x003A69C0
		private sprᯉ ᜁ()
		{
			sprᯉ result;
			for (;;)
			{
				result = null;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3D;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3D;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (base.Document != null)
							{
								num = 0;
								continue;
							}
							return result;
						}
						break;
					case 2:
						if (!string.IsNullOrEmpty(this.m_charStyleName))
						{
							num = 3;
							continue;
						}
						return result;
					case 3:
						num = 1;
						continue;
					case 4:
						return result;
					}
					break;
					IL_3D:
					result = (base.Document.Styles.FindByName(this.m_charStyleName, StyleType.CharacterStyle) as sprᯉ);
					num = 4;
				}
			}
			return result;
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x003A7A80 File Offset: 0x003A6A80
		internal string ᜇ(short A_0)
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
			return (string)base[(int)A_0];
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x003A7AC8 File Offset: 0x003A6AC8
		private short ᜀ(short A_0)
		{
			int num = 13;
			for (;;)
			{
				CharacterFormat characterFormat;
				short num2;
				CharacterFormat characterFormat2;
				switch (num)
				{
				case 0:
					if (characterFormat.HasValue(74))
					{
						num = 21;
						continue;
					}
					goto IL_10C;
				case 1:
					goto IL_10C;
				case 2:
					num2 = characterFormat.Sprms.ᜇ(18527).ᜐ();
					num = 15;
					continue;
				case 3:
					switch (A_0)
					{
					case 73:
						num = 25;
						continue;
					case 74:
						num = 0;
						continue;
					default:
						num = 22;
						continue;
					}
					break;
				case 4:
					goto IL_10C;
				case 5:
					if (true)
					{
					}
					num = 20;
					continue;
				case 6:
					if (num2 == 1033)
					{
						num = 26;
						continue;
					}
					goto IL_216;
				case 7:
					goto IL_28E;
				case 8:
					goto IL_10C;
				case 9:
					if (this.CharStyle != null)
					{
						num = 17;
						continue;
					}
					goto IL_28E;
				case 10:
					if (!this.CharStyle.CharacterFormat.HasValue((int)A_0))
					{
						num = 7;
						continue;
					}
					num = 16;
					continue;
				case 11:
					base[(int)A_0] = characterFormat.ᜃ(73);
					num = 1;
					continue;
				case 12:
					if (characterFormat != null)
					{
						num = 23;
						continue;
					}
					goto IL_10C;
				case 14:
					characterFormat2 = (base.BaseFormat as CharacterFormat);
					goto IL_1F7;
				case 15:
					goto IL_216;
				case 16:
					characterFormat2 = this.CharStyle.CharacterFormat;
					goto IL_1F7;
				case 17:
					num = 10;
					continue;
				case 18:
					if (this.HasValue((int)A_0))
					{
						num = 5;
						continue;
					}
					goto IL_316;
				case 19:
					goto IL_1AE;
				case 20:
					if (base.HasKey((int)A_0))
					{
						num = 19;
						continue;
					}
					goto IL_316;
				case 21:
					num2 = (short)characterFormat.ᜃ(74);
					goto IL_2E6;
				case 22:
					num = 8;
					continue;
				case 23:
					num = 3;
					continue;
				case 24:
					if (characterFormat.Sprms.ᜂ(18527))
					{
						num = 2;
						continue;
					}
					goto IL_216;
				case 25:
					if (!characterFormat.HasValue(73))
					{
						goto IL_10C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E6;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 26:
					num = 24;
					continue;
				case 27:
					num = 9;
					continue;
				}
				if (!this.HasValue((int)A_0))
				{
					num = 27;
					continue;
				}
				IL_10C:
				num = 18;
				continue;
				IL_1F7:
				characterFormat = characterFormat2;
				num = 12;
				continue;
				IL_216:
				base[(int)A_0] = num2;
				num = 4;
				continue;
				IL_28E:
				num = 14;
				continue;
				IL_2E6:
				num = 6;
			}
			IL_1AE:
			return (short)base[(int)A_0];
			IL_316:
			return (short)this.ᜃ((int)A_0);
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x003A7DF8 File Offset: 0x003A6DF8
		internal void ᜃ(CharacterFormat A_0)
		{
			if (true)
			{
			}
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
						return;
					case 2:
						goto IL_27A;
					}
					if (this.m_doc.DefCharFormat == null)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27A;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
					}
					IL_24E:
					Dictionary<int, object>.Enumerator enumerator = this.m_doc.DefCharFormat.PropertiesHash.GetEnumerator();
					num = 2;
					continue;
					IL_27A:
					try
					{
						num = 6;
						for (;;)
						{
							spr\u1CC1 spr_u1CC;
							switch (num)
							{
							case 0:
								goto IL_23E;
							case 1:
								if (spr_u1CC != null)
								{
									num = 14;
									continue;
								}
								break;
							case 2:
								goto IL_1A5;
							case 3:
								this.ᜊ = new sprḍ();
								num = 2;
								continue;
							case 4:
							{
								KeyValuePair<int, object> keyValuePair;
								if (keyValuePair.Key != 67)
								{
									num = 15;
									continue;
								}
								break;
							}
							case 5:
								if (this.ᜊ == null)
								{
									num = 3;
									continue;
								}
								goto IL_1A5;
							case 7:
								num = 0;
								continue;
							case 8:
								if (this.m_doc.DefCharFormat.Sprms != null)
								{
									num = 11;
									continue;
								}
								break;
							case 10:
							{
								KeyValuePair<int, object> keyValuePair;
								base.PropertiesHash.Add(keyValuePair.Key, keyValuePair.Value);
								num = 8;
								continue;
							}
							case 11:
							{
								KeyValuePair<int, object> keyValuePair;
								int sprmOption = this.GetSprmOption(keyValuePair.Key);
								spr_u1CC = this.m_doc.DefCharFormat.Sprms.ᜇ(sprmOption);
								num = 1;
								continue;
							}
							case 12:
							{
								if (!enumerator.MoveNext())
								{
									num = 7;
									continue;
								}
								KeyValuePair<int, object> keyValuePair = enumerator.Current;
								num = 4;
								continue;
							}
							case 13:
							{
								KeyValuePair<int, object> keyValuePair;
								if (!A_0.ᜅ(keyValuePair.Key))
								{
									num = 10;
									continue;
								}
								break;
							}
							case 14:
								num = 5;
								continue;
							case 15:
								num = 13;
								continue;
							}
							IL_107:
							num = 12;
							continue;
							goto IL_107;
							IL_1A5:
							this.ᜊ.ᜆ(spr_u1CC);
							num = 9;
						}
						IL_23E:
						return;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_24E;
				}
				return;
			}
			}
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x003A80A4 File Offset: 0x003A70A4
		internal bool ᜅ(int A_0)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8C;
				case 1:
					num = 5;
					continue;
				case 2:
					num = 4;
					continue;
				case 3:
					goto IL_74;
				case 4:
					if (this.CharStyle != null)
					{
						num = 1;
						continue;
					}
					goto IL_74;
				case 5:
					if (true)
					{
					}
					if (!this.CharStyle.CharacterFormat.HasValue(A_0))
					{
						num = 3;
						continue;
					}
					return true;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 7:
					if (base.BaseFormat != null)
					{
						num = 0;
						continue;
					}
					return false;
				}
				if (!this.HasValue(A_0))
				{
					num = 2;
					continue;
				}
				return true;
				IL_74:
				num = 7;
			}
			IL_62:
			return (base.BaseFormat as CharacterFormat).ᜅ(A_0);
			IL_8C:
			goto IL_62;
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x003A819C File Offset: 0x003A719C
		protected override void InitXDLSHolder()
		{
			int a_ = 18;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
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
						base.XDLSHolder.AddElement(ClipboardData.b("౷όѻ੽굿ﺋ", a_), this.Border);
						break;
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				if (this.ᜊ != null)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x003A8238 File Offset: 0x003A7238
		protected override object GetDefValue(int key)
		{
			int a_ = 9;
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (key)
					{
					case 99:
					case 103:
					case 104:
					case 105:
					case 106:
					case 108:
					case 109:
					case 120:
					case 125:
						goto IL_23A;
					case 100:
					case 101:
					case 102:
					case 112:
					case 113:
					case 114:
					case 115:
					case 116:
					case 117:
					case 118:
					case 119:
						goto IL_4D2;
					case 107:
						goto IL_E5;
					case 110:
						goto IL_1C4;
					case 111:
						goto IL_124;
					case 121:
						goto IL_315;
					case 122:
						goto IL_107;
					case 123:
						goto IL_349;
					case 124:
						goto IL_12B;
					default:
						num = 6;
						continue;
					}
					break;
				case 1:
					if (!string.IsNullOrEmpty(this.m_doc.StandardAsciiFont))
					{
						num = 9;
						continue;
					}
					goto IL_1E2;
				case 2:
					if (!string.IsNullOrEmpty(this.m_doc.StandardFarEastFont))
					{
						num = 8;
						continue;
					}
					goto IL_10E;
				case 3:
					num = 0;
					continue;
				case 4:
					if (!string.IsNullOrEmpty(this.m_doc.StandardNonFarEastFont))
					{
						num = 5;
						continue;
					}
					goto IL_2DC;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4AB;
					default:
						goto IL_C7;
					}
					break;
				case 6:
					num = 12;
					continue;
				case 7:
					num = 14;
					continue;
				case 8:
					goto IL_347;
				case 9:
					goto IL_15D;
				case 10:
					if (!string.IsNullOrEmpty(this.m_doc.StandardBidiFont))
					{
						num = 15;
						continue;
					}
					goto IL_7A;
				case 11:
					goto IL_186;
				case 12:
					goto IL_FB;
				case 13:
					switch (key)
					{
					case 0:
						goto IL_1A0;
					case 1:
						goto IL_195;
					case 2:
					case 68:
						num = 1;
						continue;
					case 3:
					case 62:
						goto IL_DA;
					case 4:
					case 5:
					case 6:
					case 14:
					case 20:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 58:
					case 59:
					case 60:
					case 71:
					case 72:
					case 79:
						goto IL_23A;
					case 7:
						goto IL_11D;
					case 8:
					case 11:
					case 12:
					case 13:
					case 15:
					case 16:
					case 19:
					case 21:
					case 22:
					case 23:
					case 24:
					case 25:
					case 26:
					case 27:
					case 28:
					case 29:
					case 30:
					case 31:
					case 32:
					case 33:
					case 34:
					case 35:
					case 36:
					case 37:
					case 38:
					case 39:
					case 40:
					case 41:
					case 42:
					case 43:
					case 44:
					case 45:
					case 46:
					case 47:
					case 48:
					case 49:
					case 56:
					case 57:
					case 67:
						goto IL_4D2;
					case 9:
						goto IL_1B9;
					case 10:
						goto IL_22C;
					case 17:
					case 18:
						goto IL_221;
					case 61:
						num = 10;
						continue;
					case 63:
					case 80:
						goto IL_241;
					case 64:
					case 65:
					case 66:
						goto IL_100;
					case 69:
						num = 2;
						continue;
					case 70:
						num = 4;
						continue;
					case 73:
					case 74:
						goto IL_4C7;
					case 75:
					case 76:
					case 77:
					case 78:
						goto IL_1CB;
					case 81:
						goto IL_233;
					}
					goto IL_4AB;
				case 14:
					if (this.m_doc.DefCharFormat != this)
					{
						num = 11;
						continue;
					}
					goto IL_350;
				case 15:
					goto IL_21C;
				}
				if (this.m_doc.DefCharFormat != null)
				{
					num = 7;
					continue;
				}
				IL_350:
				num = 13;
				continue;
				IL_4AB:
				num = 3;
			}
			IL_7A:
			return ClipboardData.b("㭮ᡰṲၴѶ奸㕺᡼ࡾꆀ톂", a_);
			IL_C7:
			if (true)
			{
			}
			if (false)
			{
			}
			return this.m_doc.StandardNonFarEastFont;
			IL_DA:
			return 10f;
			IL_E5:
			return int.MaxValue;
			IL_FB:
			goto IL_4D2;
			IL_100:
			return 0;
			IL_107:
			return NumberFormType.Default;
			IL_10E:
			return ClipboardData.b("㭮ᡰṲၴѶ奸㕺᡼ࡾꆀ톂", a_);
			IL_11D:
			return UnderlineStyle.None;
			IL_124:
			return 0;
			IL_12B:
			return StylisticSetType.Default;
			IL_15D:
			return this.m_doc.StandardAsciiFont;
			IL_186:
			return this.m_doc.DefCharFormat[key];
			IL_195:
			return Color.Empty;
			IL_1A0:
			return new Font(ClipboardData.b("㭮ᡰṲၴѶ奸㕺᡼ࡾꆀ톂", a_), 10f);
			IL_1B9:
			return Color.White;
			IL_1C4:
			return 0;
			IL_1CB:
			return short.MaxValue;
			IL_1E2:
			return ClipboardData.b("㭮ᡰṲၴѶ奸㕺᡼ࡾꆀ톂", a_);
			IL_21C:
			return this.m_doc.StandardBidiFont;
			IL_221:
			return 0f;
			IL_22C:
			return SubSuperScript.None;
			IL_233:
			return TextureStyle.TextureNone;
			IL_23A:
			return false;
			IL_241:
			return Color.White;
			IL_2DC:
			return ClipboardData.b("㭮ᡰṲၴѶ奸㕺᡼ࡾꆀ톂", a_);
			IL_315:
			return LigatureType.None;
			IL_347:
			return this.m_doc.StandardFarEastFont;
			IL_349:
			return NumberSpaceType.Default;
			IL_4C7:
			return 1033;
			IL_4D2:
			throw new ArgumentException(ClipboardData.b("Ѯᑰੲ啴ὶᡸࡺ嵼ᙾ권年ﾒ", a_));
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x003A872C File Offset: 0x003A772C
		protected override FormatBase GetDefComposite(int key)
		{
			if (key == 67)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_1F;
					}
				}
				IL_1F:
				if (true)
				{
				}
				if (false)
				{
				}
				return base.GetDefComposite(67, new Border(this, 67));
			}
			return null;
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x003A8784 File Offset: 0x003A7784
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 19;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 43;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4A2;
					case 1:
						this.Lid = reader.ReadShort(ClipboardData.b("㕸ቺ᥼", a_));
						num = 46;
						continue;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("⵸Ṻռ୾", a_)))
						{
							num = 28;
							continue;
						}
						goto IL_411;
					case 3:
						this.IdctHint = reader.ReadBoolean(ClipboardData.b("へὺṼ୾즀", a_));
						num = 19;
						continue;
					case 4:
						this.IsStrikeout = reader.ReadBoolean(ClipboardData.b("⩸ེོᙾ", a_));
						num = 89;
						continue;
					case 5:
						goto IL_D4F;
					case 6:
						goto IL_A63;
					case 7:
						goto IL_732;
					case 8:
						goto IL_F65;
					case 9:
						if (reader.HasAttribute(ClipboardData.b("⭸ᱺㅼᙾ낂\uda84떆", a_)))
						{
							num = 64;
							continue;
						}
						goto IL_E1F;
					case 10:
						goto IL_5F2;
					case 11:
						base[69] = reader.ReadString(ClipboardData.b("㽸ᑺ፼୾쾀쾈ﾌ쪎", a_));
						num = 63;
						continue;
					case 12:
						this.LidBi = reader.ReadShort(ClipboardData.b("㕸ቺ᥼㵾", a_));
						num = 70;
						continue;
					case 13:
						if (reader.HasAttribute(ClipboardData.b("へࡺ⩼᩾쮂", a_)))
						{
							num = 98;
							continue;
						}
						goto IL_91D;
					case 14:
						goto IL_BC2;
					case 15:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺ፼୾쾀있즎킔", a_)))
						{
							num = 100;
							continue;
						}
						goto IL_8B5;
					case 16:
						if (reader.HasAttribute(ClipboardData.b("⥸ᑺ๼ᙾ", a_)))
						{
							num = 58;
							continue;
						}
						goto IL_7ED;
					case 17:
						this.Hidden = reader.ReadBoolean(ClipboardData.b("ㅸቺ᥼᭾", a_));
						num = 8;
						continue;
					case 18:
						if (reader.HasAttribute(ClipboardData.b("㙸๺ॼ፾", a_)))
						{
							num = 42;
							continue;
						}
						return;
					case 19:
						goto IL_AC8;
					case 20:
						if (reader.HasAttribute(ClipboardData.b("㡸᝺ᅼ㱾", a_)))
						{
							num = 32;
							continue;
						}
						goto IL_265;
					case 21:
						this.BoldBidi = reader.ReadBoolean(ClipboardData.b("㭸ᑺᅼ᭾쎀", a_));
						num = 10;
						continue;
					case 22:
						if (reader.HasAttribute(ClipboardData.b("㱸ᙺὼၾ", a_)))
						{
							num = 51;
							continue;
						}
						goto IL_7B9;
					case 23:
						goto IL_91D;
					case 24:
						this.HighlightColor = reader.ReadColor(ClipboardData.b("ㅸቺ᩼᝾ﶈ좊ﺐ", a_));
						num = 109;
						continue;
					case 25:
						if (reader.HasAttribute(ClipboardData.b("⵸Ṻռ୾쎀力搜ﾐ횔", a_)))
						{
							num = 119;
							continue;
						}
						goto IL_101E;
					case 26:
						this.LocaleIdFarEast = reader.ReadShort(ClipboardData.b("⭸ᱺㅼᙾ늂", a_));
						num = 117;
						continue;
					case 27:
						goto IL_7ED;
					case 28:
						this.TextureStyle = (TextureStyle)reader.ReadEnum(ClipboardData.b("⵸Ṻռ୾", a_), typeof(TextureStyle));
						num = 97;
						continue;
					case 29:
						if (reader.HasAttribute(ClipboardData.b("⩸๺ὼⱾ\uda88ﾌ", a_)))
						{
							num = 67;
							continue;
						}
						goto IL_5C1;
					case 30:
						return;
					case 31:
						goto IL_B8E;
					case 32:
						this.AllCaps = reader.ReadBoolean(ClipboardData.b("㡸᝺ᅼ㱾", a_));
						num = 94;
						continue;
					case 33:
						if (reader.HasAttribute(ClipboardData.b("⭸ᱺㅼᙾ늂", a_)))
						{
							num = 26;
							continue;
						}
						goto IL_563;
					case 34:
						this.IsShadow = reader.ReadBoolean(ClipboardData.b("へࡺ⹼᝾", a_));
						num = 107;
						continue;
					case 35:
						if (reader.HasAttribute(ClipboardData.b("㱸ᕺ᩼ൾ", a_)))
						{
							num = 103;
							continue;
						}
						goto IL_D83;
					case 36:
						this.Bold = reader.ReadBoolean(ClipboardData.b("㭸ᑺᅼ᭾", a_));
						num = 106;
						continue;
					case 37:
						if (reader.HasAttribute(ClipboardData.b("⵸Ṻռ୾슀ﮈ", a_)))
						{
							num = 38;
							continue;
						}
						goto IL_4A2;
					case 38:
						this.TextColor = reader.ReadColor(ClipboardData.b("⵸Ṻռ୾슀ﮈ", a_));
						num = 0;
						continue;
					case 39:
						goto IL_DEB;
					case 40:
						this.Bidi = reader.ReadBoolean(ClipboardData.b("へࡺ㽼ᙾ", a_));
						num = 31;
						continue;
					case 41:
						if (reader.HasAttribute(ClipboardData.b("へࡺ⹼᝾", a_)))
						{
							num = 34;
							continue;
						}
						goto IL_DB7;
					case 42:
						this.IsOutLine = reader.ReadBoolean(ClipboardData.b("㙸๺ॼ፾", a_));
						num = 30;
						continue;
					case 43:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺ፼୾쾀", a_)))
						{
							num = 69;
							continue;
						}
						goto IL_8E9;
					case 44:
						goto IL_68E;
					case 45:
						if (reader.HasAttribute(ClipboardData.b("㩸፺ᱼൾ튀ﲄ얊", a_)))
						{
							num = 86;
							continue;
						}
						goto IL_985;
					case 46:
						goto IL_AFC;
					case 47:
						if (reader.HasAttribute(ClipboardData.b("へὺṼ୾즀", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_AC8;
					case 48:
						goto IL_C4C;
					case 49:
						goto IL_E1F;
					case 50:
						goto IL_65A;
					case 51:
						this.Emboss = reader.ReadBoolean(ClipboardData.b("㱸ᙺὼၾ", a_));
						num = 74;
						continue;
					case 52:
						goto IL_37F;
					case 53:
						this.CharacterSpacing = reader.ReadFloat(ClipboardData.b("㕸ቺ፼᩾튀", a_));
						num = 6;
						continue;
					case 54:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺོ᩾슀ﮈ", a_)))
						{
							num = 111;
							continue;
						}
						goto IL_D4F;
					case 55:
						this.DoubleStrike = reader.ReadBoolean(ClipboardData.b("㵸ᑺࡼᵾ횄ﮈ", a_));
						num = 102;
						continue;
					case 56:
						this.IsSmallCaps = reader.ReadBoolean(ClipboardData.b("へࡺ⹼ቾ쒆ﮊﺌ", a_));
						num = 52;
						continue;
					case 57:
						if (reader.HasAttribute(ClipboardData.b("へེᱼ፾임", a_)))
						{
							num = 112;
							continue;
						}
						goto IL_DEB;
					case 58:
						this.Position = reader.ReadFloat(ClipboardData.b("⥸ᑺ๼ᙾ", a_));
						num = 27;
						continue;
					case 59:
						goto IL_D83;
					case 60:
						if (reader.HasAttribute(ClipboardData.b("へࡺ㽼ᙾ", a_)))
						{
							num = 40;
							continue;
						}
						goto IL_B8E;
					case 61:
						if (reader.HasAttribute(ClipboardData.b("㕸ቺ፼᩾튀", a_)))
						{
							num = 53;
							continue;
						}
						goto IL_A63;
					case 62:
						if (reader.HasAttribute(ClipboardData.b("⭸ᱺㅼᙾ뎂", a_)))
						{
							num = 80;
							continue;
						}
						goto IL_C4C;
					case 63:
						goto IL_E7D;
					case 64:
						this.RgLid3_2 = reader.ReadShort(ClipboardData.b("⭸ᱺㅼᙾ낂\uda84떆", a_));
						num = 49;
						continue;
					case 65:
						this.FontSize = reader.ReadFloat(ClipboardData.b("㽸ᑺ፼୾튀ﾄ", a_));
						num = 50;
						continue;
					case 66:
						goto IL_10B5;
					case 67:
						this.SubSuperScript = (SubSuperScript)reader.ReadEnum(ClipboardData.b("⩸๺ὼⱾ\uda88ﾌ", a_), typeof(SubSuperScript));
						num = 122;
						continue;
					case 68:
						if (true)
						{
						}
						this.LineBreak = reader.ReadBoolean(ClipboardData.b("㕸ቺ፼᩾쎀", a_));
						num = 14;
						continue;
					case 69:
						base[2] = reader.ReadString(ClipboardData.b("㽸ᑺ፼୾쾀", a_));
						num = 116;
						continue;
					case 70:
						goto IL_881;
					case 71:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺ፼୾튀ﾄ쮈", a_)))
						{
							num = 75;
							continue;
						}
						goto IL_317;
					case 72:
						if (reader.HasAttribute(ClipboardData.b("ㅸቺ᩼᝾ﶈ좊ﺐ", a_)))
						{
							num = 24;
							continue;
						}
						goto IL_FED;
					case 73:
						this.Italic = reader.ReadBoolean(ClipboardData.b("へེᱼ፾", a_));
						num = 7;
						continue;
					case 74:
						goto IL_7B9;
					case 75:
						this.FontSizeBidi = reader.ReadFloat(ClipboardData.b("㽸ᑺ፼୾튀ﾄ쮈", a_));
						num = 104;
						continue;
					case 76:
						if (reader.HasAttribute(ClipboardData.b("㕸ቺ᥼", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_AFC;
					case 77:
						goto IL_985;
					case 78:
						goto IL_F07;
					case 79:
						if (reader.HasAttribute(ClipboardData.b("㭸ᑺᅼ᭾", a_)))
						{
							num = 36;
							continue;
						}
						goto IL_3B3;
					case 80:
						this.LocaleIdASCII = reader.ReadShort(ClipboardData.b("⭸ᱺㅼᙾ뎂", a_));
						num = 48;
						continue;
					case 81:
						if (reader.HasAttribute(ClipboardData.b("へࡺ㍼ၾ톀", a_)))
						{
							num = 110;
							continue;
						}
						goto IL_34B;
					case 82:
						base[68] = reader.ReadString(ClipboardData.b("㽸ᑺ፼୾쾀좈", a_));
						num = 66;
						continue;
					case 83:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺ፼୾튀ﾄ", a_)))
						{
							num = 65;
							continue;
						}
						goto IL_65A;
					case 84:
						base[61] = reader.ReadString(ClipboardData.b("㽸ᑺ፼୾쾀쮈", a_));
						num = 78;
						continue;
					case 85:
						if (reader.HasAttribute(ClipboardData.b("へེᱼ፾", a_)))
						{
							num = 73;
							continue;
						}
						goto IL_732;
					case 86:
						this.m_charStyleName = reader.ReadString(ClipboardData.b("㩸፺ᱼൾ튀ﲄ얊", a_));
						num = 77;
						continue;
					case 87:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺ፼୾쾀쮈", a_)))
						{
							num = 84;
							continue;
						}
						goto IL_F07;
					case 88:
						if (reader.HasAttribute(ClipboardData.b("へࡺ⹼ቾ쒆ﮊﺌ", a_)))
						{
							num = 56;
							continue;
						}
						goto IL_37F;
					case 89:
						goto IL_951;
					case 90:
						if (reader.HasAttribute(ClipboardData.b("ㅸቺ᥼᭾", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_F65;
					case 91:
						if (reader.HasAttribute(ClipboardData.b("㕸ቺ᥼㵾", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_881;
					case 92:
						if (reader.HasAttribute(ClipboardData.b("㵸ᑺࡼᵾ횄ﮈ", a_)))
						{
							num = 55;
							continue;
						}
						goto IL_626;
					case 93:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺ፼୾쾀쾈ﾌ쪎", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_E7D;
					case 94:
						goto IL_265;
					case 95:
						if (reader.HasAttribute(ClipboardData.b("㭸ᑺᅼ᭾쎀", a_)))
						{
							num = 21;
							continue;
						}
						goto IL_5F2;
					case 96:
						if (reader.HasAttribute(ClipboardData.b("㽸ᑺ፼୾쾀좈", a_)))
						{
							num = 82;
							continue;
						}
						goto IL_10B5;
					case 97:
						goto IL_411;
					case 98:
						this.IsWebHidden = reader.ReadBoolean(ClipboardData.b("へࡺ⩼᩾쮂", a_));
						num = 23;
						continue;
					case 99:
						this.RgLid3 = reader.ReadShort(ClipboardData.b("⭸ᱺㅼᙾ낂", a_));
						num = 44;
						continue;
					case 100:
						base[70] = reader.ReadString(ClipboardData.b("㽸ᑺ፼୾쾀있즎킔", a_));
						num = 108;
						continue;
					case 101:
						if (reader.HasAttribute(ClipboardData.b("⭸ᱺㅼᙾ낂", a_)))
						{
							num = 99;
							continue;
						}
						goto IL_68E;
					case 102:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_113A;
						default:
							if (false)
							{
							}
							goto IL_626;
						}
						break;
					case 103:
						this.Engrave = reader.ReadBoolean(ClipboardData.b("㱸ᕺ᩼ൾ", a_));
						num = 59;
						continue;
					case 104:
						goto IL_317;
					case 105:
						goto IL_34B;
					case 106:
						goto IL_3B3;
					case 107:
						goto IL_DB7;
					case 108:
						goto IL_8B5;
					case 109:
						goto IL_FED;
					case 110:
						this.IsNoProof = reader.ReadBoolean(ClipboardData.b("へࡺ㍼ၾ톀", a_));
						num = 105;
						continue;
					case 111:
						goto IL_113A;
					case 112:
						this.ItalicBidi = reader.ReadBoolean(ClipboardData.b("へེᱼ፾임", a_));
						num = 39;
						continue;
					case 113:
						this.UnderlineStyle = (UnderlineStyle)reader.ReadEnum(ClipboardData.b("ⱸᕺ᥼᩾", a_), typeof(UnderlineStyle));
						num = 118;
						continue;
					case 114:
						if (reader.HasAttribute(ClipboardData.b("㕸ቺ፼᩾쎀", a_)))
						{
							num = 68;
							continue;
						}
						goto IL_BC2;
					case 115:
						if (reader.HasAttribute(ClipboardData.b("⩸ེོᙾ", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_951;
					case 116:
						goto IL_8E9;
					case 117:
						goto IL_563;
					case 118:
						goto IL_B5A;
					case 119:
						this.TextBackgroundColor = reader.ReadColor(ClipboardData.b("⵸Ṻռ୾쎀力搜ﾐ횔", a_));
						num = 121;
						continue;
					case 120:
						if (reader.HasAttribute(ClipboardData.b("ⱸᕺ᥼᩾", a_)))
						{
							num = 113;
							continue;
						}
						goto IL_B5A;
					case 121:
						goto IL_101E;
					case 122:
						goto IL_5C1;
					}
					break;
					IL_265:
					num = 88;
					continue;
					IL_317:
					num = 72;
					continue;
					IL_34B:
					num = 13;
					continue;
					IL_37F:
					num = 60;
					continue;
					IL_3B3:
					num = 85;
					continue;
					IL_411:
					num = 18;
					continue;
					IL_4A2:
					num = 83;
					continue;
					IL_563:
					num = 101;
					continue;
					IL_5C1:
					num = 25;
					continue;
					IL_5F2:
					num = 57;
					continue;
					IL_626:
					num = 61;
					continue;
					IL_65A:
					num = 79;
					continue;
					IL_68E:
					num = 9;
					continue;
					IL_732:
					num = 115;
					continue;
					IL_7B9:
					num = 35;
					continue;
					IL_7ED:
					num = 29;
					continue;
					IL_881:
					num = 81;
					continue;
					IL_8B5:
					num = 45;
					continue;
					IL_8E9:
					num = 87;
					continue;
					IL_91D:
					num = 54;
					continue;
					IL_951:
					num = 92;
					continue;
					IL_985:
					num = 120;
					continue;
					IL_A63:
					num = 16;
					continue;
					IL_AC8:
					num = 62;
					continue;
					IL_AFC:
					num = 91;
					continue;
					IL_B5A:
					num = 37;
					continue;
					IL_B8E:
					num = 95;
					continue;
					IL_BC2:
					num = 41;
					continue;
					IL_C4C:
					num = 33;
					continue;
					IL_D4F:
					num = 2;
					continue;
					IL_D83:
					num = 90;
					continue;
					IL_DB7:
					num = 22;
					continue;
					IL_DEB:
					num = 71;
					continue;
					IL_E1F:
					num = 76;
					continue;
					IL_E7D:
					num = 15;
					continue;
					IL_F07:
					num = 96;
					continue;
					IL_F65:
					num = 20;
					continue;
					IL_FED:
					num = 47;
					continue;
					IL_101E:
					num = 114;
					continue;
					IL_10B5:
					num = 93;
					continue;
					IL_113A:
					this.ForeColor = reader.ReadColor(ClipboardData.b("㽸ᑺོ᩾슀ﮈ", a_));
					num = 5;
				}
			}
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x003A98F8 File Offset: 0x003A88F8
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 12;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 10;
				for (;;)
				{
					Color textColor;
					switch (num)
					{
					case 0:
						goto IL_EDC;
					case 1:
						if (this.HasValue(54))
						{
							num = 39;
							continue;
						}
						goto IL_1191;
					case 2:
						goto IL_4BD;
					case 3:
						if (this.HasValue(3))
						{
							num = 79;
							continue;
						}
						goto IL_4BD;
					case 4:
						writer.WriteValue(ClipboardData.b("㹱ᵳት", a_), (int)this.Lid);
						num = 23;
						continue;
					case 5:
						writer.WriteValue(ClipboardData.b("㹱ᵳት㩷፹", a_), (int)this.LidBi);
						num = 101;
						continue;
					case 6:
						if (this.HasValue(62))
						{
							num = 59;
							continue;
						}
						goto IL_AB7;
					case 7:
						if (this.HasValue(88))
						{
							num = 114;
							continue;
						}
						goto IL_9EF;
					case 8:
						goto IL_85F;
					case 9:
						goto IL_76D;
					case 10:
						if (base.HasKey(2))
						{
							num = 110;
							continue;
						}
						goto IL_D73;
					case 11:
						goto IL_1169;
					case 12:
						if (!this.ᜂ())
						{
							num = 104;
							continue;
						}
						num = 63;
						continue;
					case 13:
						writer.WriteValue(ClipboardData.b("♱ᅳ๵౷㥹፻ች", a_), this.TextColor);
						num = 55;
						continue;
					case 14:
						writer.WriteValue(ClipboardData.b("㝱ᩳᅵ੷᭹੻᭽", a_), this.Engrave);
						num = 44;
						continue;
					case 15:
						goto IL_B2A;
					case 16:
						writer.WriteValue(ClipboardData.b("㹱ᵳᡵᵷ㡹๻᭽", a_), this.LineBreak);
						num = 131;
						continue;
					case 17:
						goto IL_E3D;
					case 18:
						if (this.HasValue(80))
						{
							num = 36;
							continue;
						}
						goto IL_8DC;
					case 19:
						writer.WriteValue(ClipboardData.b("㑱᭳ᡵ౷㑹ᵻ፽쎁", a_), this.FontNameAscii);
						num = 38;
						continue;
					case 20:
						if (this.HasValue(51))
						{
							num = 33;
							continue;
						}
						goto IL_5AC;
					case 21:
						if (this.HasValue(83))
						{
							num = 40;
							continue;
						}
						goto IL_41C;
					case 22:
						goto IL_D73;
					case 23:
						goto IL_11DE;
					case 24:
						goto IL_F04;
					case 25:
						if (this.HasValue(63))
						{
							num = 78;
							continue;
						}
						goto IL_B2A;
					case 26:
						goto IL_1255;
					case 27:
						writer.WriteValue(ClipboardData.b("ⅱsѵᅷᅹ᥻", a_), this.IsStrikeout);
						num = 37;
						continue;
					case 28:
						if (this.HasValue(6))
						{
							num = 27;
							continue;
						}
						goto IL_50E;
					case 29:
						writer.WriteValue(ClipboardData.b("㙱᭳͵᩷ᙹ᥻⵽", a_), this.DoubleStrike);
						num = 147;
						continue;
					case 30:
						goto IL_5D4;
					case 31:
						if (this.HasValue(73))
						{
							num = 146;
							continue;
						}
						goto IL_FD0;
					case 32:
						writer.WriteValue(ClipboardData.b("❱ᩳትᵷࡹၻ᝽", a_), (int)this.UnderlineStyle);
						num = 99;
						continue;
					case 33:
						writer.WriteValue(ClipboardData.b("㝱ᥳᑵ᝷ॹཻ", a_), this.Emboss);
						num = 50;
						continue;
					case 34:
						writer.WriteValue(ClipboardData.b("ぱ᭳᩵ᱷ㡹ᕻ᩽", a_), this.BoldBidi);
						num = 35;
						continue;
					case 35:
						goto IL_6F5;
					case 36:
						writer.WriteValue(ClipboardData.b("㑱᭳ѵᵷ㥹፻ች", a_), this.ForeColor);
						num = 47;
						continue;
					case 37:
						goto IL_50E;
					case 38:
						goto IL_127D;
					case 39:
						writer.WriteValue(ClipboardData.b("㍱ᡳ᩵㭷᭹౻ൽ", a_), this.AllCaps);
						num = 90;
						continue;
					case 40:
						this.ᜀ(writer, 83, ClipboardData.b("㝱ᥳᑵ᝷ॹཻ㵽", a_));
						num = 149;
						continue;
					case 41:
						if (base.HasKey(68))
						{
							num = 19;
							continue;
						}
						goto IL_127D;
					case 42:
						return;
					case 43:
						if (this.HasValue(85))
						{
							num = 115;
							continue;
						}
						return;
					case 44:
						goto IL_B7C;
					case 45:
						goto IL_E65;
					case 46:
						writer.WriteValue(ClipboardData.b("㭱ݳ㑵ᅷṹᕻ", a_), this.Bidi);
						num = 125;
						continue;
					case 47:
						goto IL_8DC;
					case 48:
						goto IL_FF8;
					case 49:
						writer.WriteValue(ClipboardData.b("ⁱ፳㩵ᅷṹ佻", a_), (int)this.RgLid3);
						num = 24;
						continue;
					case 50:
						goto IL_5AC;
					case 51:
						if (this.HasValue(50))
						{
							num = 144;
							continue;
						}
						goto IL_A8F;
					case 52:
						if (this.HasValue(58))
						{
							num = 46;
							continue;
						}
						goto IL_C27;
					case 53:
						goto IL_ADC;
					case 54:
						writer.WriteValue(ClipboardData.b("ㅱᱳ᝵੷⥹ࡻݽ쪃", a_), this.m_charStyleName);
						num = 53;
						continue;
					case 55:
						goto IL_92C;
					case 56:
						if (this.HasValue(81))
						{
							num = 128;
							continue;
						}
						goto IL_55D;
					case 57:
						this.ᜀ(writer, 86, ClipboardData.b("ⅱᥳ᝵ᑷᙹ㽻ώ잃憎", a_));
						num = 142;
						continue;
					case 58:
						if (this.HasValue(52))
						{
							num = 14;
							continue;
						}
						goto IL_B7C;
					case 59:
						writer.WriteValue(ClipboardData.b("㑱᭳ᡵ౷⥹ᕻѽ삁", a_), this.FontSizeBidi);
						num = 74;
						continue;
					case 60:
						if (this.m_charStyleName != null)
						{
							num = 54;
							continue;
						}
						goto IL_ADC;
					case 61:
						if (this.HasValue(84))
						{
							num = 70;
							continue;
						}
						goto IL_3A0;
					case 62:
						goto IL_55D;
					case 63:
						if (this.LineBreak)
						{
							num = 16;
							continue;
						}
						goto IL_8B1;
					case 64:
						if (this.HasValue(9))
						{
							num = 141;
							continue;
						}
						goto IL_E3D;
					case 65:
						writer.WriteValue(ClipboardData.b("≱᭳յᅷ๹ᕻᅽ", a_), this.Position);
						num = 9;
						continue;
					case 66:
						writer.WriteValue(ClipboardData.b("ⁱ፳㩵ᅷṹ佻ⅽ뉿", a_), (int)this.RgLid3_2);
						num = 136;
						continue;
					case 67:
						goto IL_795;
					case 68:
						if (this.HasValue(4))
						{
							num = 148;
							continue;
						}
						goto IL_302;
					case 69:
						if (true)
						{
						}
						if (this.HasValue(53))
						{
							num = 98;
							continue;
						}
						goto IL_46B;
					case 70:
						this.ᜀ(writer, 84, ClipboardData.b("ⅱᱳ᝵ᱷᕹ୻㵽", a_));
						num = 72;
						continue;
					case 71:
						if (this.HasValue(18))
						{
							num = 92;
							continue;
						}
						goto IL_1255;
					case 72:
						goto IL_3A0;
					case 73:
						writer.WriteValue(ClipboardData.b("㭱ݳⅵᵷ᡹㑻᝽", a_), this.IsWebHidden);
						num = 67;
						continue;
					case 74:
						goto IL_AB7;
					case 75:
						if (this.HasValue(79))
						{
							num = 122;
							continue;
						}
						goto IL_B02;
					case 76:
						writer.WriteValue(ClipboardData.b("ⁱ፳㩵ᅷṹ䵻", a_), 74);
						num = 30;
						continue;
					case 77:
						goto IL_CCD;
					case 78:
						writer.WriteValue(ClipboardData.b("㩱ᵳᅵၷᙹᕻ᥽잃ﺋ", a_), this.HighlightColor);
						num = 15;
						continue;
					case 79:
						writer.WriteValue(ClipboardData.b("㑱᭳ᡵ౷⥹ᕻѽ", a_), this.FontSize);
						num = 2;
						continue;
					case 80:
						if (this.HasValue(72))
						{
							num = 113;
							continue;
						}
						goto IL_CCD;
					case 81:
						if (base.HasKey(70))
						{
							num = 84;
							continue;
						}
						goto IL_904;
					case 82:
						writer.WriteValue(ClipboardData.b("ⅱųᑵ⭷ཹ౻᭽톁憎", a_), this.SubSuperScript);
						num = 11;
						continue;
					case 83:
						writer.WriteValue(ClipboardData.b("㭱s᝵ᑷ፹ύ㱽", a_), this.ItalicBidi);
						num = 45;
						continue;
					case 84:
						writer.WriteValue(ClipboardData.b("㑱᭳ᡵ౷㑹ᵻ፽첁캇ﺋ쮍", a_), this.FontNameNonFarEast);
						num = 121;
						continue;
					case 85:
						if (this.HasValue(10))
						{
							num = 82;
							continue;
						}
						goto IL_1169;
					case 86:
						goto IL_A17;
					case 87:
						writer.WriteValue(ClipboardData.b("㭱ݳ╵ᕷ᭹ၻች썿", a_), this.IsSmallCaps);
						num = 94;
						continue;
					case 88:
						if (this.HasValue(60))
						{
							num = 83;
							continue;
						}
						goto IL_E65;
					case 89:
						goto IL_9EF;
					case 90:
						goto IL_1191;
					case 91:
						if (this.HasValue(17))
						{
							num = 65;
							continue;
						}
						goto IL_76D;
					case 92:
						goto IL_118C;
					case 93:
						goto IL_B02;
					case 94:
						goto IL_745;
					case 95:
						if (!textColor.IsEmpty)
						{
							num = 13;
							continue;
						}
						goto IL_92C;
					case 96:
						goto IL_11B9;
					case 97:
						if (this.HasValue(76))
						{
							num = 66;
							continue;
						}
						goto IL_E15;
					case 98:
						writer.WriteValue(ClipboardData.b("㩱ᵳትᱷόቻ", a_), this.Hidden);
						num = 135;
						continue;
					case 99:
						goto IL_979;
					case 100:
						if (this.HasValue(75))
						{
							num = 49;
							continue;
						}
						goto IL_F04;
					case 101:
						goto IL_DF0;
					case 102:
						goto IL_101F;
					case 103:
						if (this.HasValue(55))
						{
							num = 87;
							continue;
						}
						goto IL_745;
					case 104:
						return;
					case 105:
						writer.WriteValue(ClipboardData.b("㑱᭳ᡵ౷㑹ᵻ፽삁", a_), this.FontNameBidi);
						num = 143;
						continue;
					case 106:
						this.ᜀ(writer, 65, ClipboardData.b("㭱s᝵ᑷ፹ύ㵽", a_));
						num = 116;
						continue;
					case 107:
						if (this.HasValue(59))
						{
							num = 34;
							continue;
						}
						goto IL_6F5;
					case 108:
						if (this.HasValue(14))
						{
							num = 29;
							continue;
						}
						goto IL_6A7;
					case 109:
						if (this.HasValue(87))
						{
							num = 150;
							continue;
						}
						goto IL_11B9;
					case 110:
						writer.WriteValue(ClipboardData.b("㑱᭳ᡵ౷㑹ᵻ፽", a_), this.FontName);
						num = 22;
						continue;
					case 111:
						goto IL_A8F;
					case 112:
						writer.WriteValue(ClipboardData.b("㵱ųɵᑷ፹ቻ᭽", a_), this.IsOutLine);
						num = 151;
						continue;
					case 113:
						writer.WriteValue(ClipboardData.b("㭱ၳᕵ౷㉹ᕻၽ", a_), this.IdctHint);
						num = 77;
						continue;
					case 114:
						this.ᜀ(writer, 88, ClipboardData.b("㍱ᡳ᩵㭷᭹౻ൽ썿", a_));
						num = 89;
						continue;
					case 115:
						this.ᜀ(writer, 85, ClipboardData.b("ⅱsѵᅷᅹ᥻㵽", a_));
						num = 42;
						continue;
					case 116:
						goto IL_E8D;
					case 117:
						if (this.HasValue(71))
						{
							num = 112;
							continue;
						}
						goto IL_650;
					case 118:
						this.ᜀ(writer, 66, ClipboardData.b("㩱ᵳትᱷόቻ㵽", a_));
						num = 102;
						continue;
					case 119:
						if (this.HasValue(74))
						{
							num = 76;
							continue;
						}
						goto IL_5D4;
					case 120:
						writer.WriteValue(ClipboardData.b("㭱s᝵ᑷ፹ύ", a_), this.Italic);
						num = 48;
						continue;
					case 121:
						goto IL_904;
					case 122:
						writer.WriteValue(ClipboardData.b("㭱ݳ㡵᝷⩹๻ᅽ", a_), this.IsNoProof);
						num = 93;
						continue;
					case 123:
						if (this.HasValue(78))
						{
							num = 5;
							continue;
						}
						goto IL_DF0;
					case 124:
						if (this.HasValue(86))
						{
							num = 57;
							continue;
						}
						goto IL_1206;
					case 125:
						goto IL_C27;
					case 126:
						if (this.HasValue(5))
						{
							num = 120;
							continue;
						}
						goto IL_FF8;
					case 127:
						if (base.HasKey(61))
						{
							num = 105;
							continue;
						}
						goto IL_71D;
					case 128:
						writer.WriteValue(ClipboardData.b("♱ᅳ๵౷ཹ๻᭽", a_), this.TextureStyle);
						num = 62;
						continue;
					case 129:
						writer.WriteValue(ClipboardData.b("㑱᭳ᡵ౷㑹ᵻ፽쒁춇ﾋ揄", a_), this.FontNameFarEast);
						num = 0;
						continue;
					case 130:
						this.ᜀ(writer, 64, ClipboardData.b("ぱ᭳᩵ᱷ㥹፻፽ﺅ", a_));
						num = 8;
						continue;
					case 131:
						goto IL_8B1;
					case 132:
						if (this.HasValue(77))
						{
							num = 4;
							continue;
						}
						goto IL_11DE;
					case 133:
						if (base.HasKey(69))
						{
							num = 129;
							continue;
						}
						goto IL_EDC;
					case 134:
						if (this.HasValue(7))
						{
							num = 32;
							continue;
						}
						goto IL_979;
					case 135:
						goto IL_46B;
					case 136:
						goto IL_E15;
					case 137:
						if (this.HasValue(82))
						{
							num = 140;
							continue;
						}
						goto IL_A17;
					case 138:
						if (this.HasValue(65))
						{
							num = 106;
							continue;
						}
						goto IL_E8D;
					case 139:
						if (this.HasValue(125))
						{
							num = 73;
							continue;
						}
						goto IL_795;
					case 140:
						this.ᜀ(writer, 82, ClipboardData.b("㝱ᩳᅵ੷᭹੻᭽썿", a_));
						num = 86;
						continue;
					case 141:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_118C;
						default:
							if (false)
							{
							}
							writer.WriteValue(ClipboardData.b("♱ᅳ๵౷㡹ᵻᵽﶇ춍ﾏﺑﮓ", a_), this.TextBackgroundColor);
							num = 17;
							continue;
						}
						break;
					case 142:
						goto IL_1206;
					case 143:
						goto IL_71D;
					case 144:
						writer.WriteValue(ClipboardData.b("㭱ݳ╵ၷ᭹᡻ᅽ", a_), this.IsShadow);
						num = 111;
						continue;
					case 145:
						if (this.HasValue(64))
						{
							num = 130;
							continue;
						}
						goto IL_85F;
					case 146:
						writer.WriteValue(ClipboardData.b("ⁱ፳㩵ᅷṹ䱻", a_), 73);
						num = 152;
						continue;
					case 147:
						goto IL_6A7;
					case 148:
						writer.WriteValue(ClipboardData.b("ぱ᭳᩵ᱷ", a_), this.Bold);
						num = 153;
						continue;
					case 149:
						goto IL_41C;
					case 150:
						this.ᜀ(writer, 87, ClipboardData.b("㙱❳ɵ੷፹᝻᭽썿", a_));
						num = 96;
						continue;
					case 151:
						goto IL_650;
					case 152:
						goto IL_FD0;
					case 153:
						goto IL_302;
					case 154:
						if (this.HasValue(66))
						{
							num = 118;
							continue;
						}
						goto IL_101F;
					}
					break;
					IL_302:
					num = 126;
					continue;
					IL_3A0:
					num = 145;
					continue;
					IL_41C:
					num = 137;
					continue;
					IL_46B:
					num = 1;
					continue;
					IL_4BD:
					num = 68;
					continue;
					IL_50E:
					num = 108;
					continue;
					IL_55D:
					num = 117;
					continue;
					IL_5AC:
					num = 58;
					continue;
					IL_5D4:
					num = 100;
					continue;
					IL_650:
					num = 138;
					continue;
					IL_6A7:
					num = 134;
					continue;
					IL_6F5:
					num = 88;
					continue;
					IL_71D:
					num = 133;
					continue;
					IL_745:
					num = 52;
					continue;
					IL_76D:
					num = 64;
					continue;
					IL_795:
					num = 18;
					continue;
					IL_85F:
					num = 154;
					continue;
					IL_8B1:
					textColor = this.TextColor;
					num = 95;
					continue;
					IL_8DC:
					num = 56;
					continue;
					IL_904:
					num = 41;
					continue;
					IL_92C:
					num = 3;
					continue;
					IL_979:
					num = 85;
					continue;
					IL_9EF:
					num = 21;
					continue;
					IL_A17:
					num = 61;
					continue;
					IL_A8F:
					num = 20;
					continue;
					IL_AB7:
					num = 25;
					continue;
					IL_ADC:
					num = 12;
					continue;
					IL_B02:
					num = 139;
					continue;
					IL_B2A:
					num = 80;
					continue;
					IL_B7C:
					num = 69;
					continue;
					IL_C27:
					num = 107;
					continue;
					IL_CCD:
					num = 31;
					continue;
					IL_D73:
					num = 127;
					continue;
					IL_DF0:
					num = 75;
					continue;
					IL_E15:
					num = 132;
					continue;
					IL_E3D:
					num = 51;
					continue;
					IL_E65:
					num = 6;
					continue;
					IL_E8D:
					num = 7;
					continue;
					IL_EDC:
					num = 81;
					continue;
					IL_F04:
					num = 97;
					continue;
					IL_FD0:
					num = 119;
					continue;
					IL_FF8:
					num = 28;
					continue;
					IL_101F:
					num = 109;
					continue;
					IL_1169:
					num = 71;
					continue;
					IL_118C:
					writer.WriteValue(ClipboardData.b("㹱ᵳᡵᵷ⥹౻ώ", a_), this.CharacterSpacing);
					num = 26;
					continue;
					IL_1191:
					num = 103;
					continue;
					IL_11B9:
					num = 124;
					continue;
					IL_11DE:
					num = 123;
					continue;
					IL_1206:
					num = 43;
					continue;
					IL_1255:
					num = 91;
					continue;
					IL_127D:
					num = 60;
				}
			}
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x003AABF8 File Offset: 0x003A9BF8
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 19;
			for (;;)
			{
				base.WriteXmlContent(writer);
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_8F;
					case 1:
						if (this.ᜊ != null)
						{
							num = 2;
							continue;
						}
						goto IL_91;
					case 2:
					{
						byte[] array = new byte[this.ᜊ.ᜇ()];
						this.ᜊ.ᜀ(array, 0);
						writer.WriteChildBinaryElement(ClipboardData.b("ၸᕺॼ᩾ꒈﮎ", a_), array);
						num = 0;
						continue;
					}
					}
					break;
				}
			}
			IL_8F:
			IL_91:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8F;
			default:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x003AACB4 File Offset: 0x003A9CB4
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 4;
			bool result;
			for (;;)
			{
				result = base.ReadXmlContent(reader);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᝌ != null)
						{
							num = 3;
							continue;
						}
						goto IL_B7;
					case 1:
						if (reader.TagName == ClipboardData.b("ͩɫᩭᕯqᩳ᝵ᑷ坹᡻ώ", a_))
						{
							num = 2;
							continue;
						}
						goto IL_B7;
					case 2:
					{
						IL_58:
						byte[] a_2 = reader.ReadChildBinaryElement();
						this.ᜊ = new sprḍ(a_2);
						result = true;
						num = 0;
						continue;
					}
					case 3:
						this.ᝌ.ᜐ().ᜀ(this.ᜊ);
						if (true)
						{
						}
						num = 4;
						continue;
					case 4:
						goto IL_B7;
					}
					break;
					IL_B7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_58;
					default:
						goto IL_CD;
					}
				}
			}
			IL_CD:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x003AADA0 File Offset: 0x003A9DA0
		protected internal new void ImportContainer(FormatBase format)
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
			base.ImportContainer(format);
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x003AADE4 File Offset: 0x003A9DE4
		protected override void ImportMembers(FormatBase format)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					base.ImportMembers(format);
					CharacterFormat characterFormat = format as CharacterFormat;
					int num = 8;
					for (;;)
					{
						string text;
						switch (num)
						{
						case 0:
							goto IL_3F9;
						case 1:
						{
							IStyle style = characterFormat.Document.Styles.FindByName(text);
							num = 25;
							continue;
						}
						case 2:
							num = 18;
							continue;
						case 3:
							goto IL_5DF;
						case 4:
							if (characterFormat.Sprms != null)
							{
								num = 7;
								continue;
							}
							goto IL_170;
						case 5:
							base[61] = characterFormat[61];
							num = 38;
							continue;
						case 6:
							if (characterFormat.Sprms != null)
							{
								num = 49;
								continue;
							}
							goto IL_170;
						case 7:
							goto IL_1D1;
						case 8:
							if (characterFormat != null)
							{
								num = 10;
								continue;
							}
							goto IL_649;
						case 9:
							if (characterFormat.Sprms != null)
							{
								num = 2;
								continue;
							}
							goto IL_3C5;
						case 10:
							this.ᝌ = null;
							num = 14;
							continue;
						case 11:
							this.Position = (float)characterFormat[17];
							num = 45;
							continue;
						case 12:
							base[70] = characterFormat[70];
							num = 19;
							continue;
						case 13:
							goto IL_170;
						case 14:
							if (!base.Document.ᜈ)
							{
								num = 48;
								continue;
							}
							goto IL_4AB;
						case 15:
							if (text != null)
							{
								num = 34;
								continue;
							}
							goto IL_649;
						case 16:
							if (characterFormat.HasKey(18))
							{
								num = 46;
								continue;
							}
							goto IL_5DF;
						case 17:
							if (characterFormat.HasKey(70))
							{
								num = 12;
								continue;
							}
							goto IL_265;
						case 18:
							if (characterFormat.Sprms.ᜇ(this.GetSprmOption(17)) != null)
							{
								num = 43;
								continue;
							}
							goto IL_3C5;
						case 19:
							goto IL_265;
						case 20:
							goto IL_4AB;
						case 21:
							base[2] = characterFormat[2];
							num = 24;
							continue;
						case 22:
							goto IL_532;
						case 23:
							if (characterFormat.HasKey(68))
							{
								num = 47;
								continue;
							}
							goto IL_484;
						case 24:
							goto IL_508;
						case 25:
						{
							IStyle style;
							if (style != null)
							{
								num = 37;
								continue;
							}
							goto IL_2E7;
						}
						case 26:
							base[69] = characterFormat[69];
							num = 22;
							continue;
						case 27:
							if (characterFormat.HasKey(69))
							{
								num = 26;
								continue;
							}
							goto IL_532;
						case 28:
							num = 42;
							continue;
						case 29:
						{
							sprᯉ sprᯉ;
							if (sprᯉ == null)
							{
								num = 1;
								continue;
							}
							goto IL_2E7;
						}
						case 30:
							if (characterFormat.HasKey(2))
							{
								num = 21;
								continue;
							}
							goto IL_508;
						case 31:
							if (characterFormat.HasKey(17))
							{
								num = 11;
								continue;
							}
							goto IL_206;
						case 32:
							if (base.Document.ᜉ)
							{
								num = 20;
								continue;
							}
							num = 6;
							continue;
						case 33:
							goto IL_3C5;
						case 34:
						{
							Document document = base.Document;
							sprᯉ sprᯉ = document.Styles.FindByName(text) as sprᯉ;
							num = 29;
							continue;
						}
						case 35:
							goto IL_2FA;
						case 36:
							if (characterFormat.Sprms != null)
							{
								num = 28;
								continue;
							}
							goto IL_3F9;
						case 37:
						{
							IStyle style;
							Document document;
							document.Styles.Add(style.Clone());
							num = 39;
							continue;
						}
						case 38:
							goto IL_2FF;
						case 39:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1D1;
							default:
								if (false)
								{
								}
								goto IL_2E7;
							}
							break;
						case 40:
							this.Sprms.ᜁ(this.GetSprmOption(18), (int)((float)characterFormat[17] * 20f));
							num = 0;
							continue;
						case 41:
							if (characterFormat.HasKey(61))
							{
								num = 5;
								continue;
							}
							goto IL_2FF;
						case 42:
							if (characterFormat.Sprms.ᜇ(this.GetSprmOption(18)) != null)
							{
								num = 40;
								continue;
							}
							goto IL_3F9;
						case 43:
							this.Sprms.ᜁ(this.GetSprmOption(17), (int)((float)characterFormat[17] * 20f));
							num = 33;
							continue;
						case 44:
							goto IL_170;
						case 45:
							goto IL_206;
						case 46:
							this.CharacterSpacing = (float)characterFormat[18];
							num = 3;
							continue;
						case 47:
							base[68] = characterFormat[68];
							num = 50;
							continue;
						case 48:
							num = 32;
							continue;
						case 49:
							this.ᜊ = characterFormat.Sprms.ᜀ();
							num = 44;
							continue;
						case 50:
							goto IL_484;
						}
						break;
						IL_170:
						num = 30;
						continue;
						IL_1D1:
						this.ᜊ = characterFormat.Sprms.ᜀ();
						this.ᝎ = true;
						(format as CharacterFormat).ᝎ = true;
						num = 13;
						continue;
						IL_206:
						num = 36;
						continue;
						IL_265:
						num = 16;
						continue;
						IL_2E7:
						this.m_charStyleName = text;
						num = 35;
						continue;
						IL_2FF:
						num = 27;
						continue;
						IL_3C5:
						text = characterFormat.CharStyleName;
						num = 15;
						continue;
						IL_3F9:
						num = 9;
						continue;
						IL_484:
						num = 41;
						continue;
						IL_4AB:
						num = 4;
						continue;
						IL_508:
						num = 23;
						continue;
						IL_532:
						num = 17;
						continue;
						IL_5DF:
						num = 31;
					}
				}
				IL_2FA:
				IL_649:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x003AB444 File Offset: 0x003AA444
		protected override void OnChange(FormatBase format, int propKey)
		{
			int num = 1;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					if (base.OwnerBase.Document.ᜇ)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					goto IL_6A;
				case 2:
					goto IL_136;
				case 3:
					num = 4;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_68;
					default:
						if (false)
						{
						}
						if (format is Borders)
						{
							num = 10;
							continue;
						}
						goto IL_BA;
					}
					break;
				case 5:
					if (base.OwnerBase != null)
					{
						num = 12;
						continue;
					}
					goto IL_6A;
				case 6:
					return;
				case 7:
					if (num2 != -2147483648)
					{
						num = 8;
						continue;
					}
					return;
				case 8:
					this.ᜀ(num2, base[num2]);
					num = 2;
					continue;
				case 9:
					if (!(format is Border))
					{
						num = 3;
						continue;
					}
					goto IL_5D;
				case 10:
					goto IL_5D;
				case 11:
					return;
				case 12:
					num = 0;
					continue;
				case 13:
					goto IL_68;
				}
				if (this.ᝍ)
				{
					num = 11;
					continue;
				}
				num = 5;
				continue;
				IL_5D:
				num2 = 67;
				num = 13;
				continue;
				IL_6A:
				num2 = int.MinValue;
				num = 9;
				continue;
				IL_BA:
				num = 7;
				continue;
				IL_68:
				goto IL_BA;
			}
			return;
			IL_136:;
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x003AB5C0 File Offset: 0x003AA5C0
		internal override void ApplyBase(FormatBase baseFormat)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Dictionary<int, bool> dictionary = new Dictionary<int, bool>();
					int num = 11;
					for (;;)
					{
						sprℵ sprℵ;
						Dictionary<int, bool>.Enumerator enumerator;
						switch (num)
						{
						case 0:
							goto IL_14A;
						case 1:
							if (sprℵ == null)
							{
								num = 9;
								continue;
							}
							this.CharacterProps.ᜁ(sprℵ);
							if (true)
							{
							}
							num = 0;
							continue;
						case 2:
							if (!baseFormat.Document.ImportStyles)
							{
								num = 8;
								continue;
							}
							goto IL_A9;
						case 3:
							try
							{
								num = 0;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 7;
										continue;
									case 3:
										num = 4;
										continue;
									case 4:
									{
										spr\u1CC1 spr_u1CC;
										spr_u1CC.ᜀ((spr_u1CC.\u1714() == 129) ? 128 : 129);
										num = 2;
										continue;
									}
									case 5:
									{
										if (!enumerator.MoveNext())
										{
											num = 1;
											continue;
										}
										KeyValuePair<int, bool> keyValuePair = enumerator.Current;
										num = 8;
										continue;
									}
									case 6:
									{
										KeyValuePair<int, bool> keyValuePair;
										spr\u1CC1 spr_u1CC = this.Sprms.ᜇ(this.GetSprmOption(keyValuePair.Key));
										num = 9;
										continue;
									}
									case 7:
										goto IL_379;
									case 8:
									{
										KeyValuePair<int, bool> keyValuePair;
										if (keyValuePair.Value != this.ᜂ((short)keyValuePair.Key))
										{
											num = 6;
											continue;
										}
										break;
									}
									case 9:
									{
										spr\u1CC1 spr_u1CC;
										if (spr_u1CC != null)
										{
											num = 3;
											continue;
										}
										break;
									}
									}
									IL_347:
									num = 5;
									continue;
									goto IL_347;
								}
								IL_379:
								goto IL_79;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_38C;
							IL_79:
							dictionary.Clear();
							num = 12;
							continue;
						case 4:
						{
							try
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									IL_20E:
									num = 3;
									break;
								default:
									if (false)
									{
									}
									num = 6;
									break;
								}
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										int num2;
										if (this.HasValue(num2))
										{
											num = 1;
											continue;
										}
										break;
									}
									case 1:
									{
										int num2;
										dictionary.Add(num2, this.ᜂ((short)num2));
										num = 5;
										continue;
									}
									case 2:
									{
										List<int>.Enumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 4;
											continue;
										}
										int num2 = enumerator2.Current;
										num = 0;
										continue;
									}
									case 3:
										goto IL_21A;
									case 4:
										goto IL_20C;
									}
									IL_1F1:
									num = 2;
									continue;
									goto IL_1F1;
								}
								IL_20C:
								goto IL_20E;
								IL_21A:
								goto IL_14F;
							}
							finally
							{
								List<int>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							goto IL_22D;
							IL_14F:
							List<int> list;
							list.Clear();
							num = 7;
							continue;
						}
						case 5:
							goto IL_38C;
						case 6:
							if (base.Document != baseFormat.Document)
							{
								num = 13;
								continue;
							}
							goto IL_A9;
						case 7:
							goto IL_A9;
						case 8:
						{
							List<int> list = new List<int>(new int[]
							{
								109,
								99,
								4,
								5,
								6,
								14,
								50,
								51,
								52,
								53,
								54,
								55,
								58,
								59,
								60,
								72,
								71,
								106
							});
							List<int>.Enumerator enumerator2 = list.GetEnumerator();
							num = 4;
							continue;
						}
						case 9:
							return;
						case 10:
							goto IL_22D;
						case 11:
							if (base.Document.ᜉ)
							{
								num = 5;
								continue;
							}
							goto IL_A9;
						case 12:
							if (base.Document.ᜇ)
							{
								num = 10;
								continue;
							}
							goto IL_3BA;
						case 13:
							num = 2;
							continue;
						}
						break;
						IL_A9:
						base.ApplyBase(baseFormat);
						enumerator = dictionary.GetEnumerator();
						num = 3;
						continue;
						IL_22D:
						sprℵ = (baseFormat as CharacterFormat).CharacterProps;
						num = 1;
						continue;
						IL_38C:
						num = 6;
					}
				}
				IL_14A:
				IL_3BA:
				this.ᜀ();
				return;
			}
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x003AB9C4 File Offset: 0x003AA9C4
		private void ᜀ()
		{
			int num = 4;
			for (;;)
			{
				Font item;
				FontStyle fontStyle;
				string text;
				string text2;
				switch (num)
				{
				case 0:
					goto IL_2C3;
				case 1:
					goto IL_249;
				case 2:
					if (!this.m_doc.UsedFontNames.Contains(item))
					{
						num = 15;
						continue;
					}
					return;
				case 3:
					goto IL_203;
				case 5:
					if (this.Italic)
					{
						num = 6;
						continue;
					}
					goto IL_2C3;
				case 6:
					fontStyle |= FontStyle.Italic;
					goto IL_B6;
				case 7:
					num = 25;
					continue;
				case 8:
					if (this.HasValue(5))
					{
						num = 17;
						continue;
					}
					goto IL_2C3;
				case 9:
					text = this.ᜇ(68);
					goto IL_227;
				case 10:
					fontStyle |= FontStyle.Bold;
					num = 16;
					continue;
				case 11:
					if (this.UnderlineStyle != UnderlineStyle.None)
					{
						num = 21;
						continue;
					}
					goto IL_29C;
				case 12:
					text = null;
					goto IL_227;
				case 13:
					if (this.HasValue(6))
					{
						num = 20;
						continue;
					}
					goto IL_11B;
				case 14:
					if (this.HasValue(7))
					{
						num = 26;
						continue;
					}
					goto IL_29C;
				case 15:
					this.m_doc.UsedFontNames.Add(item);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B6;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 16:
					goto IL_278;
				case 17:
					num = 5;
					continue;
				case 18:
					goto IL_29C;
				case 19:
					if (string.IsNullOrEmpty(text2))
					{
						num = 1;
						continue;
					}
					fontStyle = FontStyle.Regular;
					num = 23;
					continue;
				case 20:
					num = 27;
					continue;
				case 21:
					fontStyle |= FontStyle.Underline;
					num = 18;
					continue;
				case 22:
					num = 12;
					continue;
				case 23:
					if (this.HasValue(4))
					{
						num = 7;
						continue;
					}
					goto IL_278;
				case 24:
					goto IL_11B;
				case 25:
					if (this.Bold)
					{
						num = 10;
						continue;
					}
					goto IL_278;
				case 26:
					num = 11;
					continue;
				case 27:
					if (this.IsStrikeout)
					{
						num = 28;
						continue;
					}
					goto IL_11B;
				case 28:
					fontStyle |= FontStyle.Strikeout;
					num = 24;
					continue;
				}
				if (!this.HasValue(68))
				{
					num = 22;
					continue;
				}
				num = 9;
				continue;
				IL_B6:
				num = 0;
				continue;
				IL_11B:
				item = spr\u215C.ᜀ(text2, 11f, fontStyle);
				num = 2;
				continue;
				IL_227:
				text2 = text;
				num = 19;
				continue;
				IL_278:
				num = 8;
				continue;
				IL_29C:
				num = 13;
				continue;
				IL_2C3:
				num = 14;
			}
			IL_203:
			return;
			IL_249:
			if (true)
			{
			}
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x003ABCB8 File Offset: 0x003AACB8
		internal override bool HasValue(int propertyKey)
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
				{
					spr\u1CC1 spr_u1CC;
					if (spr_u1CC == null)
					{
						num = 15;
						continue;
					}
					return true;
				}
				case 2:
					goto IL_DA;
				case 3:
				{
					int sprmOption;
					if (sprmOption == 2147483647)
					{
						num = 9;
						continue;
					}
					spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(sprmOption);
					num = 12;
					continue;
				}
				case 4:
				{
					if (true)
					{
					}
					spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(18527);
					goto IL_133;
				}
				case 5:
					num = 14;
					continue;
				case 6:
					if (this.ᜊ != null)
					{
						num = 8;
						continue;
					}
					return false;
				case 8:
					num = 10;
					continue;
				case 9:
					return false;
				case 10:
				{
					if (this.ᜊ.ᜈ() == 0)
					{
						num = 2;
						continue;
					}
					int sprmOption = this.GetSprmOption(propertyKey);
					num = 3;
					continue;
				}
				case 11:
					return false;
				case 12:
					if (propertyKey == 74)
					{
						num = 5;
						continue;
					}
					goto IL_DC;
				case 13:
				{
					spr\u1CC1 spr_u1CC;
					if (spr_u1CC == null)
					{
						num = 11;
						continue;
					}
					return true;
				}
				case 14:
				{
					spr\u1CC1 spr_u1CC;
					if (spr_u1CC == null)
					{
						num = 4;
						continue;
					}
					goto IL_DC;
				}
				case 15:
					return false;
				}
				if (!base.HasKey(propertyKey))
				{
					num = 6;
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
					num = 0;
					continue;
				}
				IL_DC:
				num = 1;
				continue;
				IL_133:
				num = 13;
			}
			return true;
			IL_DA:
			return false;
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x003ABE74 File Offset: 0x003AAE74
		protected override int GetSprmOption(int propertyKey)
		{
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_28B;
					case 1:
						goto IL_2A0;
					case 2:
						switch (propertyKey)
						{
						case 1:
							return 10818;
						case 2:
						case 68:
							return 19023;
						case 3:
							return 19011;
						case 4:
						case 64:
							return 2101;
						case 5:
						case 65:
							return 2102;
						case 6:
						case 85:
							return 2103;
						case 7:
							return 10814;
						case 8:
						case 11:
						case 12:
						case 13:
						case 15:
						case 16:
						case 19:
						case 20:
						case 21:
						case 22:
						case 23:
						case 24:
						case 25:
						case 26:
						case 27:
						case 28:
						case 29:
						case 30:
						case 31:
						case 32:
						case 33:
						case 34:
						case 35:
						case 36:
						case 37:
						case 38:
						case 39:
						case 40:
						case 41:
						case 42:
						case 43:
						case 44:
						case 45:
						case 46:
						case 47:
						case 48:
						case 49:
						case 56:
						case 57:
						case 90:
						case 91:
						case 92:
						case 93:
						case 94:
						case 95:
						case 96:
						case 97:
						case 98:
						case 106:
						case 112:
						case 113:
						case 114:
						case 115:
						case 116:
						case 117:
						case 118:
						case 119:
						case 120:
						case 121:
						case 122:
						case 123:
						case 124:
							return int.MaxValue;
						case 9:
						case 80:
						case 81:
							return 18534;
						case 10:
							return 10824;
						case 14:
						case 87:
							return 10835;
						case 17:
							return 18501;
						case 18:
							return 34880;
						case 50:
						case 84:
							return 2105;
						case 51:
						case 83:
							return 2136;
						case 52:
						case 82:
							return 2132;
						case 53:
						case 66:
							return 2108;
						case 54:
						case 88:
							return 2107;
						case 55:
						case 86:
							return 2106;
						case 58:
							return 2138;
						case 59:
							return 2140;
						case 60:
							return 2141;
						case 61:
							return 19038;
						case 62:
							return 19041;
						case 63:
							return 10764;
						case 67:
							return 26725;
						case 69:
							return 19024;
						case 70:
							return 19025;
						case 71:
							return 2104;
						case 72:
							return 10351;
						case 73:
							return 18541;
						case 74:
							return 18542;
						case 75:
							return 18547;
						case 76:
							return 18548;
						case 77:
							return 19009;
						case 78:
							return 18527;
						case 79:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_28B;
							default:
								goto IL_279;
							}
							break;
						case 89:
							goto IL_2BD;
						case 99:
							return 2178;
						case 100:
						case 101:
						case 102:
							return 51825;
						case 103:
							return 2049;
						case 104:
							return 2048;
						case 105:
							return 51799;
						case 107:
							return 26759;
						case 108:
							return 18568;
						case 109:
						case 110:
							return 2050;
						case 111:
							return 27139;
						case 125:
							return 2166;
						default:
							num = 0;
							continue;
						}
						break;
					}
					break;
					IL_28B:
					num = 1;
				}
			}
			return 18527;
			IL_279:
			if (false)
			{
			}
			return 2165;
			IL_2A0:
			return int.MaxValue;
			IL_2BD:
			if (true)
			{
			}
			return 26736;
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x003AC208 File Offset: 0x003AB208
		private void ᜀ(IXDLSAttributeWriter A_0, int A_1, string A_2)
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
			int sprmOption = this.GetSprmOption(A_1);
			spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(sprmOption);
			A_0.WriteValue(A_2, (int)spr_u1CC.\u1714());
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x003AC268 File Offset: 0x003AB268
		private void ᜀ(int A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u1CC1 spr_u1CC = null;
					int num = 88;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.Sprms.ᜇ(10818) != null)
							{
								num = 19;
								continue;
							}
							goto IL_15F0;
						case 1:
						{
							spr\u24DB spr_u24DB = this.ᝌ.ᜁ(spr_u1CC);
							base[9] = spr_u24DB.ᜂ();
							base[80] = spr_u24DB.ᜃ();
							base[81] = spr_u24DB.ᜁ();
							num = 56;
							continue;
						}
						case 2:
							base[78] = this.ᝌ.\u1738();
							num = 63;
							continue;
						case 3:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 29;
								continue;
							}
							goto IL_15F0;
						case 4:
							goto IL_BAF;
						case 5:
							if (this.Sprms.ᜇ(26736) != null)
							{
								num = 102;
								continue;
							}
							num = 0;
							continue;
						case 6:
							goto IL_B4D;
						case 7:
							if (spr_u1CC != null)
							{
								num = 1;
								continue;
							}
							goto IL_15F0;
						case 8:
							num = 12;
							continue;
						case 9:
							base[103] = this.ᝌ.ᝃ();
							num = 111;
							continue;
						case 10:
							goto IL_EB2;
						case 11:
							goto IL_29E;
						case 12:
							if (this.m_doc.DefCharFormat != null)
							{
								num = 66;
								continue;
							}
							goto IL_B52;
						case 13:
							base[107] = this.ᝌ.ᝁ();
							num = 4;
							continue;
						case 14:
							goto IL_14B7;
						case 15:
							base[104] = this.ᝌ.ᝂ();
							num = 115;
							continue;
						case 16:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 9;
								continue;
							}
							goto IL_15F0;
						case 17:
							base[5] = this.ᝌ.\u171F();
							num = 117;
							continue;
						case 18:
							goto IL_EE0;
						case 19:
							base[1] = sprṡ.ᜀ((int)this.ᝌ.\u1716());
							num = 11;
							continue;
						case 20:
							goto IL_9E0;
						case 21:
							base[99] = this.ᝌ.\u173E();
							num = 26;
							continue;
						case 22:
							goto IL_37A;
						case 23:
							base[76] = this.ᝌ.ᝎ();
							num = 77;
							continue;
						case 24:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 134;
								continue;
							}
							goto IL_15F0;
						case 25:
							base[109] = this.ᝌ.ᝇ();
							num = 51;
							continue;
						case 26:
							goto IL_643;
						case 27:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 31;
								continue;
							}
							goto IL_15F0;
						case 28:
							goto IL_5B0;
						case 29:
							base[58] = this.ᝌ.ᜤ();
							num = 130;
							continue;
						case 30:
							if (spr_u1CC != null)
							{
								num = 123;
								continue;
							}
							goto IL_A76;
						case 31:
							base[111] = this.ᝌ.\u173F();
							num = 79;
							continue;
						case 32:
							if (this.ᝌ == null)
							{
								num = 8;
								continue;
							}
							goto IL_B52;
						case 33:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 112;
								continue;
							}
							goto IL_15F0;
						case 34:
							goto IL_1135;
						case 35:
							base[53] = this.ᝌ.ᜅ();
							num = 10;
							continue;
						case 36:
							num = 20;
							continue;
						case 37:
							base[3] = this.ᝌ.ᜱ();
							num = 138;
							continue;
						case 38:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 108;
								continue;
							}
							goto IL_15F0;
						case 39:
							base[108] = this.ᝌ.ᝉ();
							num = 55;
							continue;
						case 40:
							goto IL_A76;
						case 41:
							base[77] = this.ᝌ.ᜨ();
							num = 86;
							continue;
						case 42:
							base[62] = (float)this.ᝌ.ᝌ();
							num = 48;
							continue;
						case 43:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 41;
								continue;
							}
							goto IL_15F0;
						case 44:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 15;
								continue;
							}
							goto IL_15F0;
						case 45:
							goto IL_84F;
						case 46:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 81;
								continue;
							}
							goto IL_15F0;
						case 47:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 126;
								continue;
							}
							goto IL_15F0;
						case 48:
							goto IL_12E6;
						case 49:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 58;
								continue;
							}
							goto IL_15F0;
						case 50:
							base[6] = this.ᝌ.\u171B();
							num = 18;
							continue;
						case 51:
							goto IL_826;
						case 52:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 72;
								continue;
							}
							goto IL_15F0;
						case 53:
							spr_u1CC = this.ᝌ.ᜢ().ᜇ(18534);
							num = 14;
							continue;
						case 54:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 129;
								continue;
							}
							goto IL_15F0;
						case 55:
							goto IL_6C7;
						case 56:
							goto IL_FA9;
						case 57:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 127;
								continue;
							}
							goto IL_15F0;
						case 58:
							base[75] = this.ᝌ.\u173C();
							num = 122;
							continue;
						case 59:
							goto IL_A89;
						case 60:
							goto IL_110C;
						case 61:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 35;
								continue;
							}
							goto IL_15F0;
						case 62:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 13;
								continue;
							}
							goto IL_15F0;
						case 63:
							goto IL_AEB;
						case 64:
							goto IL_335;
						case 65:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 23;
								continue;
							}
							goto IL_15F0;
						case 66:
							base[3] = this.m_doc.DefCharFormat.FontSize;
							num = 95;
							continue;
						case 67:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 137;
								continue;
							}
							goto IL_15F0;
						case 68:
							goto IL_866;
						case 69:
							goto IL_14B2;
						case 70:
							goto IL_E50;
						case 71:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 107;
								continue;
							}
							goto IL_15F0;
						case 72:
							base[59] = this.ᝌ.ᜰ();
							num = 34;
							continue;
						case 73:
							goto IL_1217;
						case 74:
							goto IL_719;
						case 75:
							base[74] = this.ᝌ.\u1734();
							num = 141;
							continue;
						case 76:
							goto IL_54E;
						case 77:
							goto IL_1034;
						case 78:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 25;
								continue;
							}
							goto IL_15F0;
						case 79:
							goto IL_996;
						case 80:
							goto IL_11C6;
						case 81:
							base[106] = this.ᝌ.\u171C();
							num = 92;
							continue;
						case 82:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 120;
								continue;
							}
							goto IL_15F0;
						case 83:
							goto IL_118E;
						case 84:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 75;
								continue;
							}
							goto IL_15F0;
						case 85:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 87;
								continue;
							}
							goto IL_15F0;
						case 86:
							goto IL_115E;
						case 87:
							base[18] = (float)this.ᝌ.ᝍ() / 20f;
							num = 94;
							continue;
						case 88:
						{
							sprℵ sprℵ = this.ᝌ;
							if (this.CharStyle == null)
							{
								goto IL_8E5;
							}
							if (!this.CharStyle.CharacterFormat.HasValue(A_0))
							{
								goto IL_8E5;
							}
							sprℵ a_ = this.CharStyle.CharacterFormat.CharacterProps;
							IL_124D:
							sprℵ.ᜀ(a_);
							num = 105;
							continue;
							IL_8E5:
							a_ = ((base.BaseFormat != null) ? (base.BaseFormat as CharacterFormat).CharacterProps : null);
							goto IL_124D;
						}
						case 89:
							base[72] = this.ᝌ.ᜁ();
							num = 98;
							continue;
						case 90:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 50;
								continue;
							}
							goto IL_15F0;
						case 91:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 140;
								continue;
							}
							goto IL_15F0;
						case 92:
							goto IL_15D6;
						case 93:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 131;
								continue;
							}
							goto IL_15F0;
						case 94:
							goto IL_A49;
						case 95:
							goto IL_96D;
						case 96:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 2;
								continue;
							}
							goto IL_15F0;
						case 97:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 39;
								continue;
							}
							goto IL_15F0;
						case 98:
							goto IL_C11;
						case 99:
							goto IL_525;
						case 100:
							switch (A_0)
							{
							case 50:
								num = 38;
								continue;
							case 51:
								num = 124;
								continue;
							case 52:
								num = 47;
								continue;
							case 53:
								num = 61;
								continue;
							case 54:
								num = 82;
								continue;
							case 55:
								num = 93;
								continue;
							case 56:
							case 57:
							case 61:
							case 64:
							case 65:
							case 66:
							case 68:
							case 69:
							case 70:
							case 82:
							case 83:
							case 84:
							case 85:
							case 86:
							case 87:
							case 88:
							case 89:
							case 90:
							case 91:
							case 92:
							case 93:
							case 94:
							case 95:
							case 96:
							case 97:
							case 98:
							case 100:
							case 101:
							case 102:
								goto IL_15F0;
							case 58:
								num = 3;
								continue;
							case 59:
								num = 52;
								continue;
							case 60:
								num = 57;
								continue;
							case 62:
								num = 136;
								continue;
							case 63:
								num = 71;
								continue;
							case 67:
								this.ᝍ = true;
								spr_u1CC = this.ᝌ.ᜢ().ᜇ(26725);
								num = 30;
								continue;
							case 71:
								num = 119;
								continue;
							case 72:
								num = 109;
								continue;
							case 73:
								num = 91;
								continue;
							case 74:
								num = 84;
								continue;
							case 75:
								num = 49;
								continue;
							case 76:
								num = 65;
								continue;
							case 77:
								num = 43;
								continue;
							case 78:
								num = 96;
								continue;
							case 79:
								num = 133;
								continue;
							case 80:
							case 81:
								goto IL_1459;
							case 99:
								num = 101;
								continue;
							case 103:
								num = 16;
								continue;
							case 104:
								num = 44;
								continue;
							case 105:
								this.ᜃ();
								num = 68;
								continue;
							case 106:
								num = 46;
								continue;
							case 107:
								num = 62;
								continue;
							case 108:
								num = 97;
								continue;
							case 109:
								num = 78;
								continue;
							case 110:
								num = 24;
								continue;
							case 111:
								num = 27;
								continue;
							default:
								num = 36;
								continue;
							}
							break;
						case 101:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 21;
								continue;
							}
							goto IL_15F0;
						case 102:
							base[1] = this.ᝌ.ᜧ();
							num = 70;
							continue;
						case 103:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 17;
								continue;
							}
							goto IL_15F0;
						case 104:
							base[51] = this.ᝌ.ᜦ();
							num = 114;
							continue;
						case 105:
							switch (A_0)
							{
							case 1:
								num = 5;
								continue;
							case 2:
							case 8:
							case 11:
							case 12:
							case 13:
							case 15:
							case 16:
								goto IL_15F0;
							case 3:
								num = 32;
								continue;
							case 4:
								num = 128;
								continue;
							case 5:
								num = 103;
								continue;
							case 6:
								num = 90;
								continue;
							case 7:
								num = 67;
								continue;
							case 9:
								goto IL_1459;
							case 10:
								num = 135;
								continue;
							case 14:
								num = 33;
								continue;
							case 17:
								num = 54;
								continue;
							case 18:
								num = 85;
								continue;
							default:
								num = 118;
								continue;
							}
							break;
						case 106:
							goto IL_1248;
						case 107:
							base[63] = sprṡ.ᜂ[(int)this.ᝌ.\u171E()];
							num = 80;
							continue;
						case 108:
							base[50] = this.ᝌ.ᜬ();
							num = 110;
							continue;
						case 109:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 89;
								continue;
							}
							goto IL_15F0;
						case 110:
							goto IL_1348;
						case 111:
							goto IL_6F0;
						case 112:
							base[14] = this.ᝌ.\u1739();
							num = 99;
							continue;
						case 113:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 37;
								continue;
							}
							goto IL_15F0;
						case 114:
							goto IL_4B9;
						case 115:
							goto IL_66C;
						case 116:
							base[4] = this.ᝌ.ᜏ();
							num = 64;
							continue;
						case 117:
							goto IL_13E2;
						case 118:
							num = 100;
							continue;
						case 119:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 121;
								continue;
							}
							goto IL_15F0;
						case 120:
							base[54] = this.ᝌ.ᜫ();
							num = 76;
							continue;
						case 121:
							base[71] = this.ᝌ.ᜯ();
							num = 60;
							continue;
						case 122:
							goto IL_940;
						case 123:
						{
							spr\u224E a_2 = this.ᝌ.ᜃ(spr_u1CC);
							spr\u192A.ᜁ(a_2, (Border)base[67]);
							num = 40;
							continue;
						}
						case 124:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 104;
								continue;
							}
							goto IL_15F0;
						case 125:
							if (spr_u1CC == null)
							{
								num = 53;
								continue;
							}
							goto IL_14B7;
						case 126:
							base[52] = this.ᝌ.\u1736();
							num = 45;
							continue;
						case 127:
							base[60] = this.ᝌ.ᝋ();
							num = 74;
							continue;
						case 128:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 116;
								continue;
							}
							goto IL_15F0;
						case 129:
							base[17] = (float)this.ᝌ.\u1718() / 20f;
							num = 83;
							continue;
						case 130:
							goto IL_100B;
						case 131:
							base[55] = this.ᝌ.ᝊ();
							num = 73;
							continue;
						case 132:
							base[10] = (SubSuperScript)this.ᝌ.ᝈ();
							num = 28;
							continue;
						case 133:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 139;
								continue;
							}
							goto IL_15F0;
						case 134:
							base[110] = this.ᝌ.ᜎ();
							num = 106;
							continue;
						case 135:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 132;
								continue;
							}
							goto IL_15F0;
						case 136:
							if (this.ᝌ.ᜢ().ᜇ(this.GetSprmOption(A_0)) != null)
							{
								num = 42;
								continue;
							}
							goto IL_15F0;
						case 137:
							base[7] = (UnderlineStyle)this.ᝌ.\u1712();
							num = 69;
							continue;
						case 138:
							goto IL_A71;
						case 139:
							base[79] = this.ᝌ.\u1713();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B4D;
							default:
								if (false)
								{
								}
								num = 22;
								continue;
							}
							break;
						case 140:
							base[73] = this.ᝌ.ᜠ();
							num = 6;
							continue;
						case 141:
							goto IL_8A7;
						}
						break;
						IL_A76:
						this.ᝍ = false;
						num = 59;
						continue;
						IL_B52:
						num = 113;
						continue;
						IL_1459:
						spr_u1CC = this.ᝌ.ᜢ().ᜇ(51825);
						num = 125;
						continue;
						IL_14B7:
						num = 7;
					}
				}
				IL_29E:
				IL_335:
				IL_37A:
				IL_4B9:
				IL_525:
				IL_54E:
				IL_5B0:
				IL_643:
				IL_66C:
				IL_6C7:
				IL_6F0:
				IL_719:
				IL_826:
				IL_84F:
				IL_866:
				IL_8A7:
				IL_940:
				IL_96D:
				IL_996:
				IL_9E0:
				IL_A49:
				IL_A71:
				IL_A89:
				IL_AEB:
				IL_B4D:
				IL_BAF:
				IL_C11:
				IL_E50:
				IL_EB2:
				IL_EE0:
				IL_FA9:
				IL_100B:
				IL_1034:
				IL_110C:
				IL_1135:
				IL_115E:
				IL_118E:
				IL_11C6:
				goto IL_15F0;
				IL_1217:
				if (true)
				{
				}
				IL_1248:
				IL_12E6:
				IL_1348:
				IL_13E2:
				IL_14B2:
				IL_15D6:
				IL_15F0:
				this.ᝌ.ᜀ(null);
				return;
			}
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x003AD874 File Offset: 0x003AC874
		internal void ᜂ(CharacterFormat A_0)
		{
			Dictionary<int, object> dictionary;
			for (;;)
			{
				dictionary = new Dictionary<int, object>();
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_260;
					case 1:
						dictionary.Add(59, true);
						num = 20;
						continue;
					case 2:
						dictionary.Add(10, this.SubSuperScript);
						num = 14;
						continue;
					case 3:
						goto IL_19E;
					case 4:
						if (this.ComplexScript)
						{
							num = 16;
							continue;
						}
						goto IL_2E1;
					case 5:
						dictionary.Add(58, true);
						num = 26;
						continue;
					case 6:
						goto IL_AF;
					case 7:
						dictionary.Add(5, true);
						num = 17;
						continue;
					case 8:
						if (this.Bidi)
						{
							num = 5;
							continue;
						}
						goto IL_2BB;
					case 9:
						if (this.UnderlineStyle != UnderlineStyle.None)
						{
							num = 25;
							continue;
						}
						goto IL_19E;
					case 10:
						goto IL_EF;
					case 11:
						if (this.Hidden)
						{
							num = 24;
							continue;
						}
						goto IL_1E7;
					case 12:
						if (this.BoldBidi)
						{
							num = 1;
							continue;
						}
						goto IL_29B;
					case 13:
						if (this.ItalicBidi)
						{
							num = 22;
							continue;
						}
						goto IL_AF;
					case 14:
						goto IL_11D;
					case 15:
						if (this.ᜊ != null)
						{
							num = 29;
							continue;
						}
						goto IL_36B;
					case 16:
						dictionary.Add(99, true);
						num = 18;
						continue;
					case 17:
						goto IL_327;
					case 18:
						goto IL_2E1;
					case 19:
						goto IL_1E7;
					case 20:
						goto IL_29B;
					case 21:
						if (this.Italic)
						{
							num = 7;
							continue;
						}
						goto IL_327;
					case 22:
						for (;;)
						{
							dictionary.Add(60, true);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_188;
							}
						}
						IL_188:
						if (false)
						{
						}
						num = 6;
						continue;
					case 23:
						dictionary.Add(4, true);
						num = 10;
						continue;
					case 24:
						dictionary.Add(53, true);
						num = 19;
						continue;
					case 25:
						dictionary.Add(7, this.UnderlineStyle);
						num = 3;
						continue;
					case 26:
						goto IL_2BB;
					case 27:
						if (this.SubSuperScript != SubSuperScript.None)
						{
							num = 2;
							continue;
						}
						goto IL_11D;
					case 28:
						if (this.Bold)
						{
							num = 23;
							continue;
						}
						goto IL_EF;
					case 29:
						this.ᜊ.ᜄ();
						num = 0;
						continue;
					}
					break;
					IL_AF:
					num = 27;
					continue;
					IL_EF:
					if (true)
					{
					}
					num = 12;
					continue;
					IL_11D:
					num = 9;
					continue;
					IL_19E:
					dictionary.Add(73, this.LocaleIdASCII);
					dictionary.Add(74, this.LocaleIdFarEast);
					num = 15;
					continue;
					IL_1E7:
					num = 21;
					continue;
					IL_29B:
					num = 4;
					continue;
					IL_2BB:
					num = 28;
					continue;
					IL_2E1:
					num = 11;
					continue;
					IL_327:
					num = 13;
				}
			}
			IL_260:
			IL_36B:
			this.CharStyleName = null;
			this.ImportContainer(A_0);
			base.ᜃ(A_0);
			this.ApplyBase(A_0.BaseFormat);
			this.ᜀ(dictionary);
			dictionary.Clear();
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x003ADC1C File Offset: 0x003ACC1C
		private void ᜀ(Dictionary<int, object> A_0)
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
				using (Dictionary<int, object>.Enumerator enumerator = A_0.GetEnumerator())
				{
					int num = 9;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							KeyValuePair<int, object> keyValuePair;
							this.ItalicBidi = (bool)keyValuePair.Value;
							num = 37;
							continue;
						}
						case 2:
						{
							KeyValuePair<int, object> keyValuePair;
							this.UnderlineStyle = (UnderlineStyle)keyValuePair.Value;
							num = 23;
							continue;
						}
						case 3:
							num = 21;
							continue;
						case 5:
						{
							int key;
							if (key <= 53)
							{
								num = 3;
								continue;
							}
							num = 18;
							continue;
						}
						case 6:
						{
							KeyValuePair<int, object> keyValuePair;
							this.Italic = (bool)keyValuePair.Value;
							num = 15;
							continue;
						}
						case 7:
						{
							KeyValuePair<int, object> keyValuePair;
							this.ComplexScript = (bool)keyValuePair.Value;
							num = 28;
							continue;
						}
						case 8:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.SubSuperScript != (SubSuperScript)keyValuePair.Value)
							{
								num = 16;
								continue;
							}
							break;
						}
						case 11:
						{
							int key;
							if (key != 53)
							{
								num = 36;
								continue;
							}
							num = 35;
							continue;
						}
						case 12:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.ComplexScript != (bool)keyValuePair.Value)
							{
								num = 7;
								continue;
							}
							break;
						}
						case 14:
						{
							KeyValuePair<int, object> keyValuePair;
							this.BoldBidi = (bool)keyValuePair.Value;
							num = 19;
							continue;
						}
						case 16:
						{
							KeyValuePair<int, object> keyValuePair;
							this.SubSuperScript = (SubSuperScript)keyValuePair.Value;
							num = 47;
							continue;
						}
						case 18:
						{
							int key;
							switch (key)
							{
							case 58:
								num = 24;
								continue;
							case 59:
								num = 49;
								continue;
							case 60:
								num = 27;
								continue;
							default:
								num = 25;
								continue;
							}
							break;
						}
						case 20:
						{
							KeyValuePair<int, object> keyValuePair;
							this.LocaleIdFarEast = (short)keyValuePair.Value;
							num = 17;
							continue;
						}
						case 21:
						{
							int key;
							switch (key)
							{
							case 4:
								num = 48;
								continue;
							case 5:
								num = 41;
								continue;
							case 6:
							case 8:
							case 9:
								break;
							case 7:
								num = 30;
								continue;
							case 10:
								num = 8;
								continue;
							default:
								num = 42;
								continue;
							}
							break;
						}
						case 22:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.LocaleIdFarEast != (short)keyValuePair.Value)
							{
								num = 20;
								continue;
							}
							break;
						}
						case 24:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.Bidi != (bool)keyValuePair.Value)
							{
								num = 46;
								continue;
							}
							break;
						}
						case 25:
							num = 32;
							continue;
						case 27:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.ItalicBidi != (bool)keyValuePair.Value)
							{
								num = 1;
								continue;
							}
							break;
						}
						case 29:
							num = 26;
							continue;
						case 30:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.UnderlineStyle != (UnderlineStyle)keyValuePair.Value)
							{
								num = 2;
								continue;
							}
							break;
						}
						case 32:
						{
							int key;
							switch (key)
							{
							case 73:
								num = 34;
								continue;
							case 74:
								num = 22;
								continue;
							default:
								num = 33;
								continue;
							}
							break;
						}
						case 33:
							num = 40;
							continue;
						case 34:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.LocaleIdASCII != (short)keyValuePair.Value)
							{
								num = 38;
								continue;
							}
							break;
						}
						case 35:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.Hidden != (bool)keyValuePair.Value)
							{
								num = 43;
								continue;
							}
							break;
						}
						case 36:
							num = 31;
							continue;
						case 38:
						{
							KeyValuePair<int, object> keyValuePair;
							this.LocaleIdASCII = (short)keyValuePair.Value;
							num = 13;
							continue;
						}
						case 39:
							num = 50;
							continue;
						case 40:
						{
							int key;
							if (key != 99)
							{
								num = 29;
								continue;
							}
							num = 12;
							continue;
						}
						case 41:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.Italic != (bool)keyValuePair.Value)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 42:
							num = 11;
							continue;
						case 43:
						{
							KeyValuePair<int, object> keyValuePair;
							this.Hidden = (bool)keyValuePair.Value;
							num = 10;
							continue;
						}
						case 44:
						{
							KeyValuePair<int, object> keyValuePair;
							this.Bold = (bool)keyValuePair.Value;
							num = 4;
							continue;
						}
						case 45:
						{
							if (!enumerator.MoveNext())
							{
								num = 39;
								continue;
							}
							KeyValuePair<int, object> keyValuePair = enumerator.Current;
							int key = keyValuePair.Key;
							num = 5;
							continue;
						}
						case 46:
						{
							KeyValuePair<int, object> keyValuePair;
							this.Bidi = (bool)keyValuePair.Value;
							num = 0;
							continue;
						}
						case 48:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.Bold != (bool)keyValuePair.Value)
							{
								num = 44;
								continue;
							}
							break;
						}
						case 49:
						{
							KeyValuePair<int, object> keyValuePair;
							if (this.BoldBidi != (bool)keyValuePair.Value)
							{
								num = 14;
								continue;
							}
							break;
						}
						case 50:
							goto IL_617;
						}
						IL_247:
						num = 45;
						continue;
						goto IL_247;
					}
					IL_617:;
				}
				break;
			}
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x003AE26C File Offset: 0x003AD26C
		internal void ᜅ(CharacterFormat A_0)
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
			CharacterFormat characterFormat = new CharacterFormat(A_0.Document);
			characterFormat.ImportContainer(this);
			characterFormat.ᜃ(this);
			characterFormat.ApplyBase(A_0);
			characterFormat.CharStyleName = null;
			this.ᜀ(characterFormat);
			this.ImportContainer(characterFormat);
			base.ᜃ(characterFormat);
			characterFormat.Close();
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x003AE2EC File Offset: 0x003AD2EC
		private void ᜀ(CharacterFormat A_0)
		{
			int num = 66;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5DB;
				case 1:
					A_0.DoubleStrike = this.DoubleStrike;
					num = 64;
					continue;
				case 2:
					if (A_0.PicLocation != this.PicLocation)
					{
						num = 87;
						continue;
					}
					goto IL_CC5;
				case 3:
					goto IL_1038;
				case 4:
					goto IL_C0C;
				case 5:
					A_0.FontSizeBidi = this.FontSizeBidi;
					num = 92;
					continue;
				case 6:
					A_0.Bold = this.Bold;
					num = 101;
					continue;
				case 7:
					if (A_0.Bidi != this.Bidi)
					{
						num = 126;
						continue;
					}
					goto IL_62D;
				case 8:
					if (A_0.IsOutLine != this.IsOutLine)
					{
						num = 143;
						continue;
					}
					goto IL_F91;
				case 9:
					goto IL_971;
				case 10:
					A_0.CharacterSpacing = this.CharacterSpacing;
					num = 4;
					continue;
				case 11:
					A_0.BoldBidi = this.BoldBidi;
					num = 51;
					continue;
				case 12:
					if (A_0.HighlightColor != this.HighlightColor)
					{
						num = 78;
						continue;
					}
					goto IL_A5B;
				case 13:
					if (A_0.FontSize != this.FontSize)
					{
						num = 49;
						continue;
					}
					goto IL_E58;
				case 14:
					if (A_0.RgLid3_2 != this.RgLid3_2)
					{
						num = 40;
						continue;
					}
					goto IL_7CB;
				case 15:
					if (A_0.Lid != this.Lid)
					{
						num = 129;
						continue;
					}
					goto IL_E2F;
				case 16:
					if (A_0.NumberFormType != this.NumberFormType)
					{
						num = 81;
						continue;
					}
					goto IL_E84;
				case 17:
					goto IL_4F5;
				case 18:
					goto IL_B43;
				case 19:
					goto IL_659;
				case 20:
					A_0.FontName = this.FontName;
					num = 33;
					continue;
				case 21:
					goto IL_E2F;
				case 22:
					goto IL_459;
				case 23:
					if (A_0.IsShadow != this.IsShadow)
					{
						num = 50;
						continue;
					}
					goto IL_B6F;
				case 24:
					goto IL_E53;
				case 25:
					if (A_0.IsSpecial != this.IsSpecial)
					{
						num = 46;
						continue;
					}
					goto IL_89C;
				case 26:
					goto IL_77E;
				case 27:
					A_0.FontNameBidi = this.FontNameBidi;
					num = 26;
					continue;
				case 28:
					A_0.LocaleIdFarEast = this.LocaleIdFarEast;
					num = 75;
					continue;
				case 29:
					A_0.TextBackgroundColor = this.TextBackgroundColor;
					num = 35;
					continue;
				case 30:
					A_0.SubSuperScript = this.SubSuperScript;
					num = 63;
					continue;
				case 31:
					if (A_0.FontSizeBidi != this.FontSizeBidi)
					{
						num = 5;
						continue;
					}
					goto IL_D1A;
				case 32:
					goto IL_B6F;
				case 33:
					goto IL_380;
				case 34:
					if (A_0.ComplexScript != this.ComplexScript)
					{
						num = 109;
						continue;
					}
					goto IL_C61;
				case 35:
					goto IL_74D;
				case 36:
					goto IL_F91;
				case 37:
					A_0.LigaturesType = this.LigaturesType;
					num = 132;
					continue;
				case 38:
					if (A_0.SubSuperScript != this.SubSuperScript)
					{
						num = 30;
						continue;
					}
					goto IL_D67;
				case 39:
					goto IL_ECC;
				case 40:
					A_0.RgLid3_2 = this.RgLid3_2;
					num = 48;
					continue;
				case 41:
					A_0.FieldVanishComplex = this.FieldVanishComplex;
					num = 91;
					continue;
				case 42:
					goto IL_B17;
				case 43:
					A_0.FontNameAscii = this.FontNameAscii;
					num = 0;
					continue;
				case 44:
					A_0.ItalicBidi = this.ItalicBidi;
					num = 19;
					continue;
				case 45:
					if (A_0.FontName != this.FontName)
					{
						num = 20;
						continue;
					}
					goto IL_380;
				case 46:
					A_0.IsSpecial = this.IsSpecial;
					num = 120;
					continue;
				case 47:
					goto IL_2E4;
				case 48:
					goto IL_7CB;
				case 49:
					A_0.FontSize = this.FontSize;
					num = 140;
					continue;
				case 50:
					A_0.IsShadow = this.IsShadow;
					num = 32;
					continue;
				case 51:
					goto IL_7F7;
				case 52:
					goto IL_BC4;
				case 53:
					if (A_0.LineBreak != this.LineBreak)
					{
						num = 74;
						continue;
					}
					goto IL_29C;
				case 54:
					A_0.TextureStyle = this.TextureStyle;
					num = 39;
					continue;
				case 55:
					if (A_0.FontNameFarEast != this.FontNameFarEast)
					{
						num = 106;
						continue;
					}
					goto IL_86B;
				case 56:
					if (A_0.LidBi != this.LidBi)
					{
						num = 24;
						continue;
					}
					goto IL_E03;
				case 57:
					goto IL_C61;
				case 58:
					A_0.Hidden = this.Hidden;
					num = 9;
					continue;
				case 59:
					if (A_0.FieldVanish != this.FieldVanish)
					{
						num = 98;
						continue;
					}
					goto IL_BC4;
				case 60:
					if (A_0.Hidden != this.Hidden)
					{
						num = 58;
						continue;
					}
					goto IL_971;
				case 61:
					goto IL_6D9;
				case 62:
					A_0.FontNameNonFarEast = this.FontNameNonFarEast;
					num = 113;
					continue;
				case 63:
					goto IL_D67;
				case 64:
					goto IL_705;
				case 65:
					if (A_0.TextColor != this.TextColor)
					{
						num = 146;
						continue;
					}
					goto IL_ACF;
				case 67:
					A_0.IsNoProof = this.IsNoProof;
					num = 18;
					continue;
				case 68:
					goto IL_823;
				case 69:
					goto IL_F49;
				case 70:
					if (A_0.Italic != this.Italic)
					{
						num = 107;
						continue;
					}
					goto IL_A87;
				case 71:
					goto IL_E03;
				case 72:
					goto IL_CEE;
				case 73:
					A_0.AllowContextualAlternates = this.AllowContextualAlternates;
					num = 3;
					continue;
				case 74:
					A_0.LineBreak = this.LineBreak;
					num = 134;
					continue;
				case 75:
					goto IL_B9B;
				case 76:
					if (A_0.IsSmallCaps != this.IsSmallCaps)
					{
						num = 97;
						continue;
					}
					goto IL_2E4;
				case 77:
					goto IL_CC5;
				case 78:
					A_0.HighlightColor = this.HighlightColor;
					num = 86;
					continue;
				case 79:
					A_0.IsStrikeout = this.IsStrikeout;
					num = 61;
					continue;
				case 80:
					goto IL_8C5;
				case 81:
					A_0.NumberFormType = this.NumberFormType;
					num = 100;
					continue;
				case 82:
					if (A_0.IsNoProof != this.IsNoProof)
					{
						num = 67;
						continue;
					}
					goto IL_B43;
				case 83:
					if (A_0.Engrave != this.Engrave)
					{
						num = 125;
						continue;
					}
					goto IL_823;
				case 84:
					goto IL_90D;
				case 85:
					if (A_0.ForeColor != this.ForeColor)
					{
						num = 89;
						continue;
					}
					goto IL_4F5;
				case 86:
					goto IL_A5B;
				case 87:
					A_0.PicLocation = this.PicLocation;
					num = 77;
					continue;
				case 88:
					if (A_0.NumberSpaceType != this.NumberSpaceType)
					{
						num = 142;
						continue;
					}
					goto IL_CEE;
				case 89:
					A_0.ForeColor = this.ForeColor;
					num = 17;
					continue;
				case 90:
					if (A_0.LocaleIdFarEast != this.LocaleIdFarEast)
					{
						num = 28;
						continue;
					}
					goto IL_B9B;
				case 91:
					goto IL_556;
				case 92:
					goto IL_D1A;
				case 93:
					if (A_0.DoubleStrike != this.DoubleStrike)
					{
						num = 1;
						continue;
					}
					goto IL_705;
				case 94:
					goto IL_A16;
				case 95:
					goto IL_A87;
				case 96:
					A_0.Emboss = this.Emboss;
					num = 22;
					continue;
				case 97:
					A_0.IsSmallCaps = this.IsSmallCaps;
					num = 47;
					continue;
				case 98:
					A_0.FieldVanish = this.FieldVanish;
					num = 52;
					continue;
				case 99:
					A_0.UnderlineStyle = this.UnderlineStyle;
					num = 69;
					continue;
				case 100:
					goto IL_E84;
				case 101:
					goto IL_DB1;
				case 102:
					goto IL_86B;
				case 103:
					if (A_0.TextureStyle != this.TextureStyle)
					{
						num = 54;
						continue;
					}
					goto IL_ECC;
				case 104:
					A_0.IdctHint = this.IdctHint;
					num = 42;
					continue;
				case 105:
					if (A_0.AllowContextualAlternates != this.AllowContextualAlternates)
					{
						num = 73;
						continue;
					}
					goto IL_1056;
				case 106:
					A_0.FontNameFarEast = this.FontNameFarEast;
					num = 102;
					continue;
				case 107:
					A_0.Italic = this.Italic;
					num = 95;
					continue;
				case 108:
					if (A_0.UnderlineStyle != this.UnderlineStyle)
					{
						num = 99;
						continue;
					}
					goto IL_F49;
				case 109:
					A_0.ComplexScript = this.ComplexScript;
					num = 57;
					continue;
				case 110:
					A_0.AllCaps = this.AllCaps;
					num = 122;
					continue;
				case 111:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E53;
					default:
						if (false)
						{
						}
						if (A_0.FontNameAscii != this.FontNameAscii)
						{
							num = 43;
							continue;
						}
						goto IL_5DB;
					}
					break;
				case 112:
					if (A_0.Position != this.Position)
					{
						num = 116;
						continue;
					}
					goto IL_8C5;
				case 113:
					goto IL_FD9;
				case 114:
					if (A_0.CharacterSpacing != this.CharacterSpacing)
					{
						num = 10;
						continue;
					}
					goto IL_C0C;
				case 115:
					if (A_0.Emboss != this.Emboss)
					{
						num = 96;
						continue;
					}
					goto IL_459;
				case 116:
					A_0.Position = this.Position;
					num = 80;
					continue;
				case 117:
					if (A_0.Bold != this.Bold)
					{
						num = 6;
						continue;
					}
					goto IL_DB1;
				case 118:
					if (A_0.StylisticSetType != this.StylisticSetType)
					{
						num = 128;
						continue;
					}
					goto IL_90D;
				case 119:
					if (A_0.FontNameNonFarEast != this.FontNameNonFarEast)
					{
						num = 62;
						continue;
					}
					goto IL_FD9;
				case 120:
					goto IL_89C;
				case 121:
					goto IL_62D;
				case 122:
					goto IL_C35;
				case 123:
					if (A_0.ItalicBidi != this.ItalicBidi)
					{
						num = 44;
						continue;
					}
					goto IL_659;
				case 124:
					if (A_0.FontNameBidi != this.FontNameBidi)
					{
						num = 27;
						continue;
					}
					goto IL_77E;
				case 125:
					A_0.Engrave = this.Engrave;
					num = 68;
					continue;
				case 126:
					A_0.Bidi = this.Bidi;
					num = 121;
					continue;
				case 127:
					A_0.LocaleIdASCII = this.LocaleIdASCII;
					num = 94;
					continue;
				case 128:
					A_0.StylisticSetType = this.StylisticSetType;
					num = 84;
					continue;
				case 129:
					A_0.Lid = this.Lid;
					num = 21;
					continue;
				case 130:
					if (A_0.LocaleIdASCII != this.LocaleIdASCII)
					{
						num = 127;
						continue;
					}
					goto IL_A16;
				case 131:
					if (A_0.FieldVanishComplex != this.FieldVanishComplex)
					{
						num = 41;
						continue;
					}
					goto IL_556;
				case 132:
					goto IL_9A2;
				case 133:
					if (A_0.LigaturesType != this.LigaturesType)
					{
						num = 37;
						continue;
					}
					goto IL_9A2;
				case 134:
					goto IL_29C;
				case 135:
					goto IL_9EA;
				case 136:
					if (A_0.RgLid3 != this.RgLid3)
					{
						num = 139;
						continue;
					}
					goto IL_9EA;
				case 137:
					if (A_0.IsStrikeout != this.IsStrikeout)
					{
						num = 79;
						continue;
					}
					goto IL_6D9;
				case 138:
					if (A_0.IdctHint != this.IdctHint)
					{
						num = 104;
						continue;
					}
					goto IL_B17;
				case 139:
					A_0.RgLid3 = this.RgLid3;
					num = 135;
					continue;
				case 140:
					goto IL_E58;
				case 141:
					goto IL_ACF;
				case 142:
					A_0.NumberSpaceType = this.NumberSpaceType;
					num = 72;
					continue;
				case 143:
					A_0.IsOutLine = this.IsOutLine;
					num = 36;
					continue;
				case 144:
					if (A_0.TextBackgroundColor != this.TextBackgroundColor)
					{
						num = 29;
						continue;
					}
					goto IL_74D;
				case 145:
					if (A_0.BoldBidi != this.BoldBidi)
					{
						num = 11;
						continue;
					}
					goto IL_7F7;
				case 146:
					A_0.TextColor = this.TextColor;
					num = 141;
					continue;
				}
				if (A_0.AllCaps != this.AllCaps)
				{
					num = 110;
					continue;
				}
				goto IL_C35;
				IL_29C:
				num = 130;
				continue;
				IL_2E4:
				num = 25;
				continue;
				IL_380:
				num = 111;
				continue;
				IL_459:
				num = 83;
				continue;
				IL_4F5:
				num = 60;
				continue;
				IL_556:
				num = 45;
				continue;
				IL_5DB:
				num = 124;
				continue;
				IL_E53:
				if (true)
				{
				}
				A_0.LidBi = this.LidBi;
				num = 71;
				continue;
				IL_62D:
				num = 117;
				continue;
				IL_659:
				num = 15;
				continue;
				IL_6D9:
				num = 118;
				continue;
				IL_705:
				num = 115;
				continue;
				IL_74D:
				num = 65;
				continue;
				IL_77E:
				num = 55;
				continue;
				IL_7CB:
				num = 23;
				continue;
				IL_7F7:
				num = 114;
				continue;
				IL_823:
				num = 59;
				continue;
				IL_86B:
				num = 119;
				continue;
				IL_89C:
				num = 137;
				continue;
				IL_8C5:
				num = 136;
				continue;
				IL_90D:
				num = 38;
				continue;
				IL_971:
				num = 12;
				continue;
				IL_9A2:
				num = 53;
				continue;
				IL_9EA:
				num = 14;
				continue;
				IL_A16:
				num = 90;
				continue;
				IL_A5B:
				num = 138;
				continue;
				IL_A87:
				num = 123;
				continue;
				IL_ACF:
				num = 103;
				continue;
				IL_B17:
				num = 70;
				continue;
				IL_B43:
				num = 16;
				continue;
				IL_B6F:
				num = 76;
				continue;
				IL_B9B:
				num = 82;
				continue;
				IL_BC4:
				num = 131;
				continue;
				IL_C0C:
				num = 34;
				continue;
				IL_C35:
				num = 7;
				continue;
				IL_C61:
				num = 93;
				continue;
				IL_CC5:
				num = 112;
				continue;
				IL_CEE:
				num = 8;
				continue;
				IL_D1A:
				num = 85;
				continue;
				IL_D67:
				num = 144;
				continue;
				IL_DB1:
				num = 145;
				continue;
				IL_E03:
				num = 133;
				continue;
				IL_E2F:
				num = 56;
				continue;
				IL_E58:
				num = 31;
				continue;
				IL_E84:
				num = 88;
				continue;
				IL_ECC:
				num = 108;
				continue;
				IL_F49:
				num = 105;
				continue;
				IL_F91:
				num = 2;
				continue;
				IL_FD9:
				num = 13;
			}
			IL_1038:
			IL_1056:
			this.Border.ᜁ(A_0.Border);
		}

		// Token: 0x04002DF0 RID: 11760
		internal new const string ᜀ = "Times New Roman";

		// Token: 0x04002DF1 RID: 11761
		protected const float DEF_FONTSIZE = 10f;

		// Token: 0x04002DF2 RID: 11762
		internal const byte ᜁ = 129;

		// Token: 0x04002DF3 RID: 11763
		internal new const byte ᜂ = 128;

		// Token: 0x04002DF4 RID: 11764
		internal new const short ᜃ = 0;

		// Token: 0x04002DF5 RID: 11765
		internal new const short ᜄ = 1;

		// Token: 0x04002DF6 RID: 11766
		internal const short ᜅ = 2;

		// Token: 0x04002DF7 RID: 11767
		internal const short ᜆ = 3;

		// Token: 0x04002DF8 RID: 11768
		internal const short ᜇ = 4;

		// Token: 0x04002DF9 RID: 11769
		internal const short ᜈ = 5;

		// Token: 0x04002DFA RID: 11770
		internal new const short ᜉ = 6;

		// Token: 0x04002DFB RID: 11771
		internal new const short ᜊ = 7;

		// Token: 0x04002DFC RID: 11772
		internal const short ᜋ = 9;

		// Token: 0x04002DFD RID: 11773
		internal const short ᜌ = 10;

		// Token: 0x04002DFE RID: 11774
		internal const short \u170D = 14;

		// Token: 0x04002DFF RID: 11775
		internal const short ᜎ = 17;

		// Token: 0x04002E00 RID: 11776
		internal const short ᜏ = 18;

		// Token: 0x04002E01 RID: 11777
		internal const short ᜐ = 20;

		// Token: 0x04002E02 RID: 11778
		internal const short ᜑ = 50;

		// Token: 0x04002E03 RID: 11779
		internal const short \u1712 = 51;

		// Token: 0x04002E04 RID: 11780
		internal const short \u1713 = 52;

		// Token: 0x04002E05 RID: 11781
		internal const short \u1714 = 53;

		// Token: 0x04002E06 RID: 11782
		internal const short \u1715 = 54;

		// Token: 0x04002E07 RID: 11783
		internal const short \u1716 = 55;

		// Token: 0x04002E08 RID: 11784
		private byte \u25D8\u00AD\u00A1\u009E;

		// Token: 0x04002E09 RID: 11785
		internal const short \u1717 = 58;

		// Token: 0x04002E0A RID: 11786
		internal const short \u1718 = 59;

		// Token: 0x04002E0B RID: 11787
		internal const short \u1719 = 60;

		// Token: 0x04002E0C RID: 11788
		internal const short \u171A = 61;

		// Token: 0x04002E0D RID: 11789
		internal const short \u171B = 62;

		// Token: 0x04002E0E RID: 11790
		internal const short \u171C = 63;

		// Token: 0x04002E0F RID: 11791
		internal const short \u171D = 67;

		// Token: 0x04002E10 RID: 11792
		internal const short \u171E = 68;

		// Token: 0x04002E11 RID: 11793
		internal const short \u171F = 69;

		// Token: 0x04002E12 RID: 11794
		internal const short ᜠ = 70;

		// Token: 0x04002E13 RID: 11795
		internal const short ᜡ = 71;

		// Token: 0x04002E14 RID: 11796
		internal const short ᜢ = 72;

		// Token: 0x04002E15 RID: 11797
		internal const short ᜣ = 73;

		// Token: 0x04002E16 RID: 11798
		internal const short ᜤ = 74;

		// Token: 0x04002E17 RID: 11799
		internal const short ᜥ = 75;

		// Token: 0x04002E18 RID: 11800
		internal const short ᜦ = 76;

		// Token: 0x04002E19 RID: 11801
		internal const short ᜧ = 77;

		// Token: 0x04002E1A RID: 11802
		internal const short ᜨ = 78;

		// Token: 0x04002E1B RID: 11803
		internal const short ᜩ = 79;

		// Token: 0x04002E1C RID: 11804
		internal const short ᜪ = 80;

		// Token: 0x04002E1D RID: 11805
		internal const short ᜫ = 81;

		// Token: 0x04002E1E RID: 11806
		internal const short ᜬ = 125;

		// Token: 0x04002E1F RID: 11807
		internal const short ᜭ = 109;

		// Token: 0x04002E20 RID: 11808
		internal const short ᜮ = 110;

		// Token: 0x04002E21 RID: 11809
		internal const short ᜯ = 111;

		// Token: 0x04002E22 RID: 11810
		internal const short ᜰ = 99;

		// Token: 0x04002E23 RID: 11811
		internal const short ᜱ = 65;

		// Token: 0x04002E24 RID: 11812
		internal const short \u1732 = 64;

		// Token: 0x04002E25 RID: 11813
		internal const short \u1733 = 66;

		// Token: 0x04002E26 RID: 11814
		internal const short \u1734 = 82;

		// Token: 0x04002E27 RID: 11815
		internal const short \u1735 = 83;

		// Token: 0x04002E28 RID: 11816
		internal const short \u1736 = 84;

		// Token: 0x04002E29 RID: 11817
		internal const short \u1737 = 85;

		// Token: 0x04002E2A RID: 11818
		internal const short \u1738 = 86;

		// Token: 0x04002E2B RID: 11819
		internal new const short \u1739 = 87;

		// Token: 0x04002E2C RID: 11820
		internal const short \u173A = 88;

		// Token: 0x04002E2D RID: 11821
		internal const short \u173B = 89;

		// Token: 0x04002E2E RID: 11822
		internal const short \u173C = 100;

		// Token: 0x04002E2F RID: 11823
		internal const short \u173D = 101;

		// Token: 0x04002E30 RID: 11824
		internal const short \u173E = 102;

		// Token: 0x04002E31 RID: 11825
		internal const short \u173F = 103;

		// Token: 0x04002E32 RID: 11826
		internal const short ᝀ = 104;

		// Token: 0x04002E33 RID: 11827
		internal const short ᝁ = 105;

		// Token: 0x04002E34 RID: 11828
		internal const short ᝂ = 106;

		// Token: 0x04002E35 RID: 11829
		internal const short ᝃ = 107;

		// Token: 0x04002E36 RID: 11830
		internal const short ᝄ = 108;

		// Token: 0x04002E37 RID: 11831
		internal const short ᝅ = 120;

		// Token: 0x04002E38 RID: 11832
		internal const short ᝆ = 121;

		// Token: 0x04002E39 RID: 11833
		internal const short ᝇ = 122;

		// Token: 0x04002E3A RID: 11834
		internal const short ᝈ = 123;

		// Token: 0x04002E3B RID: 11835
		internal const short ᝉ = 124;

		// Token: 0x04002E3C RID: 11836
		protected string m_charStyleName;

		// Token: 0x04002E3D RID: 11837
		private string ᝊ;

		// Token: 0x04002E3E RID: 11838
		private CharacterFormat ᝋ;

		// Token: 0x04002E3F RID: 11839
		private sprℵ ᝌ;

		// Token: 0x04002E40 RID: 11840
		private bool ᝍ;

		// Token: 0x04002E41 RID: 11841
		internal bool ᝎ;

		// Token: 0x04002E42 RID: 11842
		internal bool ᝏ;

		// Token: 0x04002E43 RID: 11843
		private Font ᝐ;
	}
}
