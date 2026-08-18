using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x02000097 RID: 151
	public class PageSetup : DocumentSerializable
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000BD38 File Offset: 0x0000AD38
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000BD7C File Offset: 0x0000AD7C
		public float DefaultTabWidth
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
				for (;;)
				{
					IL_00:
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
								goto IL_00;
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
						case 1:
							this.ᜑ = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						if (value == this.ᜑ)
						{
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000BDF8 File Offset: 0x0000ADF8
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x0000BE3C File Offset: 0x0000AE3C
		public SizeF PageSize
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000BE80 File Offset: 0x0000AE80
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000BEC4 File Offset: 0x0000AEC4
		public PageOrientation Orientation
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
				return this.ᜋ;
			}
			set
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_C1:
					this.PageSize = new SizeF(this.PageSize.Height, this.PageSize.Width);
					num = 6;
					break;
				default:
					if (false)
					{
					}
					switch (0)
					{
					default:
						num = 8;
						break;
					}
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
						this.ᜋ = value;
						num = 2;
						continue;
					case 1:
						return;
					case 2:
						switch (value)
						{
						case PageOrientation.Portrait:
						{
							SizeF pageSize = this.PageSize;
							num = 5;
							continue;
						}
						case (PageOrientation)1:
							return;
						case PageOrientation.Landscape:
						{
							SizeF pageSize2 = this.PageSize;
							num = 7;
							continue;
						}
						default:
							num = 1;
							continue;
						}
						break;
					case 3:
						goto IL_C1;
					case 4:
						goto IL_FC;
					case 5:
					{
						SizeF pageSize;
						if (pageSize.Width > this.PageSize.Height)
						{
							num = 4;
							continue;
						}
						return;
					}
					case 6:
						return;
					case 7:
					{
						SizeF pageSize2;
						if (pageSize2.Height > this.PageSize.Width)
						{
							num = 3;
							continue;
						}
						return;
					}
					}
					if (this.ᜋ == value)
					{
						return;
					}
					num = 0;
				}
				IL_FC:
				this.PageSize = new SizeF(this.PageSize.Height, this.PageSize.Width);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000C068 File Offset: 0x0000B068
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000C0AC File Offset: 0x0000B0AC
		public PageAlignment VerticalAlignment
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
				this.\u170D = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000C0F0 File Offset: 0x0000B0F0
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000C174 File Offset: 0x0000B174
		public MarginsF Margins
		{
			get
			{
				for (;;)
				{
					IL_00:
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
								goto IL_00;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								break;
							}
							break;
						case 1:
							this.ᜌ = new MarginsF();
							num = 2;
							continue;
						case 2:
							goto IL_6F;
						}
						if (this.ᜌ != null)
						{
							goto IL_71;
						}
						num = 1;
					}
				}
				IL_6F:
				IL_71:
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000C1B8 File Offset: 0x0000B1B8
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000C1FC File Offset: 0x0000B1FC
		public float HeaderDistance
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
				return this.m_fHeaderDistance;
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
				this.m_fHeaderDistance = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000C240 File Offset: 0x0000B240
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000C284 File Offset: 0x0000B284
		public float FooterDistance
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
				return this.m_fFooterDistance;
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
				this.m_fFooterDistance = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000C2C8 File Offset: 0x0000B2C8
		public float ClientWidth
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
				return this.PageSize.Width - this.Margins.Left - this.Margins.Right;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000C32C File Offset: 0x0000B32C
		public float ClientHeight
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
				return this.PageSize.Height - this.Margins.Top - this.Margins.Bottom;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000C390 File Offset: 0x0000B390
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000C3D4 File Offset: 0x0000B3D4
		public bool DifferentFirstPageHeaderFooter
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000C418 File Offset: 0x0000B418
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000C45C File Offset: 0x0000B45C
		public bool DifferentOddAndEvenPagesHeaderFooter
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
				return this.ᜐ;
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
				this.ᜐ = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000C4A0 File Offset: 0x0000B4A0
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000C4E4 File Offset: 0x0000B4E4
		public LineNumberingRestartMode LineNumberingRestartMode
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u1712 = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000C528 File Offset: 0x0000B528
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000C56C File Offset: 0x0000B56C
		public int LineNumberingStep
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
				return this.\u1713;
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
				this.\u1713 = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000C5B0 File Offset: 0x0000B5B0
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000C5F4 File Offset: 0x0000B5F4
		public int LineNumberingStartValue
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u1714 = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000C638 File Offset: 0x0000B638
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000C67C File Offset: 0x0000B67C
		public float LineNumberingDistanceFromText
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000C6C0 File Offset: 0x0000B6C0
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000C704 File Offset: 0x0000B704
		public PageBordersApplyType PageBordersApplyType
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
				return this.\u1716;
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
				this.\u1716 = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000C748 File Offset: 0x0000B748
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000C78C File Offset: 0x0000B78C
		public PageBorderOffsetFrom PageBorderOffsetFrom
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
				return this.\u1717;
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
				this.\u1717 = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000C7D0 File Offset: 0x0000B7D0
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000C814 File Offset: 0x0000B814
		public bool IsFrontPageBorder
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
				return this.\u1718;
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
				this.\u1718 = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000C858 File Offset: 0x0000B858
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000C8A8 File Offset: 0x0000B8A8
		public bool PageBorderIncludeHeader
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
				return base.Document.DOP.ᜤ().\u171A();
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
				base.Document.DOP.ᜤ().ᜀ(value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000C8FC File Offset: 0x0000B8FC
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000C94C File Offset: 0x0000B94C
		public bool PageBorderIncludeFooter
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
				return base.Document.DOP.ᜤ().ᜃ();
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
				base.Document.DOP.ᜤ().ᜅ(value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000C9A0 File Offset: 0x0000B9A0
		public Borders Borders
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
				return this.\u1719;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000C9E4 File Offset: 0x0000B9E4
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000CA28 File Offset: 0x0000BA28
		public bool Bidi
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
				return this.\u171A;
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
				this.\u171A = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000CA6C File Offset: 0x0000BA6C
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000CAB0 File Offset: 0x0000BAB0
		internal bool EqualColumnWidth
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u171B = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000CAF4 File Offset: 0x0000BAF4
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000CB38 File Offset: 0x0000BB38
		public PageNumberStyle PageNumberStyle
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
				return this.\u171C;
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
				this.\u171C = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000CB7C File Offset: 0x0000BB7C
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000CBC0 File Offset: 0x0000BBC0
		public int PageStartingNumber
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000CC04 File Offset: 0x0000BC04
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000CC48 File Offset: 0x0000BC48
		public bool RestartPageNumbering
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
				return this.\u171E;
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
				this.\u171E = value;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000CC8C File Offset: 0x0000BC8C
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000CCD0 File Offset: 0x0000BCD0
		internal float LinePitch
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000CD14 File Offset: 0x0000BD14
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000CD58 File Offset: 0x0000BD58
		internal GridPitchType PitchType
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
				return this.ᜠ;
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
				this.ᜠ = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000CD9C File Offset: 0x0000BD9C
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000CDE0 File Offset: 0x0000BDE0
		internal bool DrawLinesBetweenCols
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
				return this.ᜡ;
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
				this.ᜡ = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000CE24 File Offset: 0x0000BE24
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000CE68 File Offset: 0x0000BE68
		internal bool HasLineNumbering
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
				return this.ᜢ;
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
				this.ᜢ = true;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000CEAC File Offset: 0x0000BEAC
		// (set) Token: 0x06000118 RID: 280 RVA: 0x0000CEF0 File Offset: 0x0000BEF0
		public CharacterSpacing CharacterSpacingControl
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜎ = value;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000CF34 File Offset: 0x0000BF34
		internal PageSetup(Section A_0) : base(A_0.Document, A_0)
		{
			this.ᜊ = new SizeF(595.3f, 841.9f);
			this.ᜌ = new MarginsF();
			this.ᜌ.All = 20f;
			this.ᜌ.Left = 50f;
			this.ᜌ.Right = 50f;
			this.m_fFooterDistance = (this.m_fHeaderDistance = -0.05f);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000CFE4 File Offset: 0x0000BFE4
		public void InsertPageNumbers(bool fromTopPage, PageNumberAlignment horizontalAlignment)
		{
			switch (0)
			{
			default:
			{
				int num = 18;
				IParagraph paragraph;
				for (;;)
				{
					int num2;
					HeaderFooter headerFooter;
					int num3;
					int count;
					HeaderFooter headerFooter2;
					IField field2;
					switch (num)
					{
					case 0:
						goto IL_234;
					case 1:
					{
						Field field;
						if (field.Type == FieldType.FieldPage)
						{
							num = 11;
							continue;
						}
						goto IL_AF;
					}
					case 2:
						num = 12;
						continue;
					case 3:
						goto IL_14C;
					case 4:
						if (paragraph.Items[num2].DocumentObjectType != DocumentObjectType.Field)
						{
							goto IL_AF;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_234;
						default:
							if (false)
							{
							}
							num = 13;
							continue;
						}
						break;
					case 5:
						goto IL_250;
					case 6:
						num = 0;
						continue;
					case 7:
						headerFooter = (base.OwnerBase as Section).HeadersFooters.Header;
						goto IL_203;
					case 8:
						goto IL_14C;
					case 9:
						goto IL_250;
					case 10:
					{
						if (num3 >= count)
						{
							num = 6;
							continue;
						}
						paragraph = headerFooter2.Paragraphs[num3];
						num2 = 0;
						int count2 = paragraph.Items.Count;
						num = 5;
						continue;
					}
					case 11:
					{
						Field field;
						field2 = field;
						num = 16;
						continue;
					}
					case 12:
						headerFooter = (base.OwnerBase as Section).HeadersFooters.Footer;
						goto IL_203;
					case 13:
					{
						Field field = (Field)paragraph.Items[num2];
						num = 1;
						continue;
					}
					case 14:
						goto IL_273;
					case 15:
						goto IL_1DB;
					case 16:
						goto IL_273;
					case 17:
					{
						int count2;
						if (num2 >= count2)
						{
							num = 14;
							continue;
						}
						num = 4;
						continue;
					}
					case 19:
						paragraph = headerFooter2.AddParagraph();
						field2 = paragraph.AppendField("", FieldType.FieldPage);
						num = 15;
						continue;
					}
					if (!fromTopPage)
					{
						num = 2;
						continue;
					}
					num = 7;
					continue;
					IL_AF:
					num2++;
					num = 9;
					continue;
					IL_14C:
					num = 10;
					continue;
					IL_203:
					headerFooter2 = headerFooter;
					paragraph = null;
					field2 = null;
					num3 = 0;
					count = headerFooter2.Paragraphs.Count;
					num = 8;
					continue;
					IL_234:
					if (field2 == null)
					{
						if (true)
						{
						}
						num = 19;
						continue;
					}
					break;
					IL_250:
					num = 17;
					continue;
					IL_273:
					num3++;
					num = 3;
				}
				IL_1DB:
				paragraph.Format.WrapFrameAround = true;
				paragraph.Format.FrameX = (short)horizontalAlignment;
				return;
			}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000D294 File Offset: 0x0000C294
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 11;
			switch (0)
			{
			default:
				for (;;)
				{
					base.WriteXmlAttributes(writer);
					int num = 50;
					for (;;)
					{
						SizeF pageSize;
						SizeF pageSize2;
						switch (num)
						{
						case 0:
							goto IL_42F;
						case 1:
							goto IL_813;
						case 2:
							if (!this.IsFrontPageBorder)
							{
								num = 40;
								continue;
							}
							goto IL_279;
						case 3:
							if (this.VerticalAlignment != PageAlignment.Top)
							{
								num = 42;
								continue;
							}
							goto IL_1A2;
						case 4:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⩸Ṻॼ੾쾂얊슐", a_), this.LineNumberingStep);
							num = 6;
							continue;
						case 5:
							if (this.DifferentFirstPageHeaderFooter)
							{
								num = 15;
								continue;
							}
							goto IL_619;
						case 6:
							goto IL_840;
						case 7:
							if (pageSize.Height != 0f)
							{
								num = 55;
								continue;
							}
							goto IL_792;
						case 8:
							if (this.FooterDistance >= 0f)
							{
								num = 45;
								continue;
							}
							goto IL_2CC;
						case 9:
							if (this.Margins.Left >= 0f)
							{
								num = 36;
								continue;
							}
							goto IL_6FE;
						case 10:
							goto IL_56E;
						case 11:
							goto IL_53A;
						case 12:
							if (this.PageBorderOffsetFrom != PageBorderOffsetFrom.Text)
							{
								num = 52;
								continue;
							}
							goto IL_73A;
						case 13:
							writer.WriteValue(ClipboardData.b("㥰ᙲᑴ፶ᱸॺ㥼ᙾ", a_), this.HeaderDistance);
							num = 29;
							continue;
						case 14:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⩸Ṻॼ੾쾂얊\udc90ﲒ", a_), this.LineNumberingRestartMode);
							num = 58;
							continue;
						case 15:
							writer.WriteValue(ClipboardData.b("㕰ᩲ፴ᅶᱸॺ᡼ᅾ얂愈ﾊ\udd8c", a_), this.DifferentFirstPageHeaderFooter);
							num = 17;
							continue;
						case 16:
							return;
						case 17:
							goto IL_619;
						case 18:
							if (this.HeaderDistance >= 0f)
							{
								num = 13;
								continue;
							}
							goto IL_407;
						case 19:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_56E;
							default:
								if (false)
								{
								}
								goto IL_73A;
							}
							break;
						case 20:
							goto IL_641;
						case 21:
							writer.WriteValue(ClipboardData.b("⍰ᩲቴὶ൸㙺ᱼൾ", a_), this.Margins.Right);
							num = 24;
							continue;
						case 22:
							if (this.LineNumberingRestartMode != LineNumberingRestartMode.None)
							{
								num = 14;
								continue;
							}
							goto IL_562;
						case 23:
							if (this.DifferentOddAndEvenPagesHeaderFooter)
							{
								num = 25;
								continue;
							}
							goto IL_813;
						case 24:
							goto IL_17A;
						case 25:
							writer.WriteValue(ClipboardData.b("㕰ᩲ፴ᅶᱸॺ᡼ᅾ첂첈ﶊ손", a_), this.DifferentOddAndEvenPagesHeaderFooter);
							num = 1;
							continue;
						case 26:
							goto IL_792;
						case 27:
							writer.WriteValue(ClipboardData.b("ばٲŴᡶ⵸᩺ὼ⡾", a_), this.DefaultTabWidth);
							num = 20;
							continue;
						case 28:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⩸Ṻॼ੾솂ﾌ캎璉", a_), this.PageBordersApplyType);
							num = 0;
							continue;
						case 29:
							goto IL_407;
						case 30:
							if (this.\u171B)
							{
								num = 43;
								continue;
							}
							return;
						case 31:
							goto IL_5E7;
						case 32:
							if (this.LineNumberingDistanceFromText != 0f)
							{
								num = 33;
								continue;
							}
							goto IL_6D3;
						case 33:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⩸Ṻॼ੾쾂얊햐朗ﺜ爵", a_), this.LineNumberingDistanceFromText);
							num = 57;
							continue;
						case 34:
							goto IL_279;
						case 35:
							writer.WriteValue(ClipboardData.b("╰ᱲմ㩶ᡸॺ᩼ᙾ", a_), this.Margins.Top);
							num = 31;
							continue;
						case 36:
							writer.WriteValue(ClipboardData.b("㵰ᙲ፴Ͷ㑸ོ᩺᡾", a_), this.Margins.Left);
							num = 39;
							continue;
						case 37:
							if (this.Orientation != PageOrientation.Portrait)
							{
								num = 53;
								continue;
							}
							goto IL_58A;
						case 38:
							if (pageSize2.Width != 0f)
							{
								num = 59;
								continue;
							}
							goto IL_53A;
						case 39:
							goto IL_6FE;
						case 40:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⩸Ṻॼ੾솂ﾌ욎\uda92ﮔ톖", a_), this.IsFrontPageBorder);
							num = 34;
							continue;
						case 41:
							goto IL_2CC;
						case 42:
							writer.WriteValue(ClipboardData.b("ばὲᱴၶ᝸ᙺ᡼ᅾ", a_), this.VerticalAlignment);
							num = 44;
							continue;
						case 43:
							writer.WriteValue(ClipboardData.b("㑰ɲtᙶᕸ㡺ቼ፾횀", a_), this.\u171B);
							num = 16;
							continue;
						case 44:
							goto IL_1A2;
						case 45:
							writer.WriteValue(ClipboardData.b("㝰ᱲᩴͶᱸॺ㥼ᙾ", a_), this.FooterDistance);
							num = 41;
							continue;
						case 46:
							if (this.Margins.Top >= 0f)
							{
								num = 35;
								continue;
							}
							goto IL_5E7;
						case 47:
							goto IL_58A;
						case 48:
							goto IL_34F;
						case 49:
							writer.WriteValue(ClipboardData.b("㍰ᱲŴͶᙸᙺぼṾ", a_), this.Margins.Bottom);
							num = 48;
							continue;
						case 50:
							if (this.DefaultTabWidth != 36f)
							{
								if (true)
								{
								}
								num = 27;
								continue;
							}
							goto IL_641;
						case 51:
							if (this.Margins.Right >= 0f)
							{
								num = 21;
								continue;
							}
							goto IL_17A;
						case 52:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⩸Ṻॼ੾솂ﾌ삎\udd9a철", a_), this.PageBorderOffsetFrom);
							num = 19;
							continue;
						case 53:
							writer.WriteValue(ClipboardData.b("㹰Ųᱴቶ᝸ེᱼ୾", a_), this.Orientation);
							num = 47;
							continue;
						case 54:
							goto IL_562;
						case 55:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶㅸṺᑼ᡾", a_), this.PageSize.Height);
							num = 26;
							continue;
						case 56:
							if (this.Margins.Bottom >= 0f)
							{
								num = 49;
								continue;
							}
							goto IL_34F;
						case 57:
							goto IL_6D3;
						case 58:
							if (this.LineNumberingStep != 0)
							{
								num = 4;
								continue;
							}
							goto IL_840;
						case 59:
							writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⹸ቺ᥼୾", a_), this.PageSize.Width);
							num = 11;
							continue;
						}
						break;
						IL_17A:
						num = 5;
						continue;
						IL_1A2:
						num = 8;
						continue;
						IL_279:
						num = 12;
						continue;
						IL_2CC:
						num = 18;
						continue;
						IL_34F:
						num = 46;
						continue;
						IL_407:
						num = 37;
						continue;
						IL_42F:
						num = 2;
						continue;
						IL_56E:
						if (this.PageBordersApplyType != PageBordersApplyType.AllPages)
						{
							num = 28;
							continue;
						}
						goto IL_42F;
						IL_53A:
						num = 3;
						continue;
						IL_562:
						num = 10;
						continue;
						IL_58A:
						num = 56;
						continue;
						IL_5E7:
						num = 9;
						continue;
						IL_619:
						num = 23;
						continue;
						IL_641:
						pageSize = this.PageSize;
						num = 7;
						continue;
						IL_6D3:
						writer.WriteValue(ClipboardData.b("ⅰቲቴቶ⩸Ṻॼ੾쾂얊슐춚ﲜ풠욢", a_), this.LineNumberingStartValue);
						num = 54;
						continue;
						IL_6FE:
						num = 51;
						continue;
						IL_73A:
						num = 30;
						continue;
						IL_792:
						pageSize2 = this.PageSize;
						num = 38;
						continue;
						IL_813:
						num = 22;
						continue;
						IL_840:
						num = 32;
					}
				}
				return;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000DB10 File Offset: 0x0000CB10
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 14;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 31;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ킓ﾕﶛ쎟잡", a_)))
						{
							num = 42;
							continue;
						}
						goto IL_93A;
					case 1:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⽻᭽쒅펑", a_)))
						{
							num = 55;
							continue;
						}
						goto IL_30A;
					case 2:
						goto IL_23A;
					case 3:
						this.LineNumberingStartValue = reader.ReadInt(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ잓聯좝솟캡톣쎥", a_));
						num = 59;
						continue;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ잓ﶗ", a_)))
						{
							num = 26;
							continue;
						}
						goto IL_23A;
					case 5:
						goto IL_806;
					case 6:
						this.LineNumberingRestartMode = (LineNumberingRestartMode)reader.ReadEnum(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ\ud993秊ﲗﾙ", a_), typeof(LineNumberingRestartMode));
						num = 30;
						continue;
					case 7:
						goto IL_3C1;
					case 8:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⽻᭽쒅\udd91ﾙ\ud89d튟춡즣", a_)))
						{
							num = 61;
							continue;
						}
						goto IL_874;
					case 9:
						goto IL_93A;
					case 10:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⽻᭽쒅\udb91\udf95\udc99캟횡", a_)))
						{
							num = 47;
							continue;
						}
						goto IL_26E;
					case 11:
						goto IL_2A2;
					case 12:
						if (true)
						{
						}
						this.Margins.Bottom = reader.ReadFloat(ClipboardData.b("㙳᥵౷๹፻፽쵿", a_));
						num = 58;
						continue;
					case 13:
						goto IL_52C;
					case 14:
						this.DifferentFirstPageHeaderFooter = reader.ReadBoolean(ClipboardData.b("びήṷᱹ᥻౽삅ﾋ揄삏", a_));
						num = 11;
						continue;
					case 15:
						this.Margins.Right = reader.ReadFloat(ClipboardData.b("♳ήίቹࡻ㍽", a_));
						num = 32;
						continue;
					case 16:
						goto IL_26E;
					case 17:
						if (reader.HasAttribute(ClipboardData.b("㙳᥵౷๹፻፽쵿", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_4FB;
					case 18:
						goto IL_30A;
					case 19:
						this.FooterDistance = reader.ReadFloat(ClipboardData.b("㉳᥵᝷๹᥻౽쑿", a_));
						num = 24;
						continue;
					case 20:
						if (reader.HasAttribute(ClipboardData.b("㉳᥵᝷๹᥻౽쑿", a_)))
						{
							num = 19;
							continue;
						}
						goto IL_2D6;
					case 21:
						if (reader.HasAttribute(ClipboardData.b("㱳፵᥷ṹ᥻౽쑿", a_)))
						{
							num = 50;
							continue;
						}
						goto IL_435;
					case 22:
						goto IL_6D2;
					case 23:
						if (reader.HasAttribute(ClipboardData.b("ㅳݵ൷᭹ၻ㵽펃ﺉ", a_)))
						{
							num = 39;
							continue;
						}
						return;
					case 24:
						goto IL_2D6;
					case 25:
						this.DifferentOddAndEvenPagesHeaderFooter = reader.ReadBoolean(ClipboardData.b("びήṷᱹ᥻౽즅즋ﲑ쒓ﾗﾙ", a_));
						num = 13;
						continue;
					case 26:
						this.LineNumberingStep = reader.ReadInt(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ잓ﶗ", a_));
						goto IL_8F1;
					case 27:
						goto IL_435;
					case 28:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό㑻᭽", a_)))
						{
							num = 51;
							continue;
						}
						goto IL_806;
					case 29:
						if (reader.HasAttribute(ClipboardData.b("㕳᩵ᅷᵹቻ፽", a_)))
						{
							num = 53;
							continue;
						}
						goto IL_3C1;
					case 30:
						goto IL_493;
					case 31:
						if (reader.HasAttribute(ClipboardData.b("㕳͵౷ᕹ⡻ώ햁ﲇ", a_)))
						{
							num = 49;
							continue;
						}
						goto IL_6D2;
					case 32:
						goto IL_706;
					case 33:
						return;
					case 34:
						goto IL_874;
					case 35:
						goto IL_674;
					case 36:
						if (reader.HasAttribute(ClipboardData.b("㭳ѵᅷόቻ੽", a_)))
						{
							num = 45;
							continue;
						}
						goto IL_560;
					case 37:
						this.Margins.Left = reader.ReadFloat(ClipboardData.b("㡳፵ṷ๹ㅻώ", a_));
						num = 40;
						continue;
					case 38:
						if (reader.HasAttribute(ClipboardData.b("びήṷᱹ᥻౽즅즋ﲑ쒓ﾗﾙ", a_)))
						{
							num = 25;
							continue;
						}
						goto IL_52C;
					case 39:
						this.\u171B = reader.ReadBoolean(ClipboardData.b("ㅳݵ൷᭹ၻ㵽펃ﺉ", a_));
						num = 33;
						continue;
					case 40:
						goto IL_73A;
					case 41:
						if (reader.HasAttribute(ClipboardData.b("♳ήίቹࡻ㍽", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_706;
					case 42:
						this.LineNumberingDistanceFromText = reader.ReadFloat(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ킓ﾕﶛ쎟잡", a_));
						num = 9;
						continue;
					case 43:
						if (reader.HasAttribute(ClipboardData.b("びήṷᱹ᥻౽삅ﾋ揄삏", a_)))
						{
							num = 14;
							continue;
						}
						goto IL_2A2;
					case 44:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ\ud993秊ﲗﾙ", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_493;
					case 45:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8F1;
						default:
							if (false)
							{
							}
							this.Orientation = (PageOrientation)reader.ReadEnum(ClipboardData.b("㭳ѵᅷόቻ੽", a_), typeof(PageOrientation));
							num = 46;
							continue;
						}
						break;
					case 46:
						goto IL_560;
					case 47:
						this.IsFrontPageBorder = reader.ReadBoolean(ClipboardData.b("⑳᝵ίό⽻᭽쒅\udb91\udf95\udc99캟횡", a_));
						num = 16;
						continue;
					case 48:
						goto IL_4C7;
					case 49:
						this.DefaultTabWidth = reader.ReadFloat(ClipboardData.b("㕳͵౷ᕹ⡻ώ햁ﲇ", a_));
						num = 22;
						continue;
					case 50:
						this.HeaderDistance = reader.ReadFloat(ClipboardData.b("㱳፵᥷ṹ᥻౽쑿", a_));
						num = 27;
						continue;
					case 51:
						this.PageSize = new SizeF(this.PageSize.Width, reader.ReadFloat(ClipboardData.b("⑳᝵ίό㑻᭽", a_)));
						num = 5;
						continue;
					case 52:
						this.PageSize = new SizeF(reader.ReadFloat(ClipboardData.b("⑳᝵ίό⭻᝽", a_)), this.PageSize.Height);
						num = 35;
						continue;
					case 53:
						this.VerticalAlignment = (PageAlignment)reader.ReadEnum(ClipboardData.b("㕳᩵ᅷᵹቻ፽", a_), typeof(PageAlignment));
						num = 7;
						continue;
					case 54:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⽻᭽쪅삍ﾑ잓聯좝솟캡톣쎥", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_1C9;
					case 55:
						this.PageBordersApplyType = (PageBordersApplyType)reader.ReadEnum(ClipboardData.b("⑳᝵ίό⽻᭽쒅펑", a_), typeof(PageBordersApplyType));
						num = 18;
						continue;
					case 56:
						if (reader.HasAttribute(ClipboardData.b("⁳᥵ࡷ㝹ᵻ౽", a_)))
						{
							num = 57;
							continue;
						}
						goto IL_4C7;
					case 57:
						this.Margins.Top = reader.ReadFloat(ClipboardData.b("⁳᥵ࡷ㝹ᵻ౽", a_));
						num = 48;
						continue;
					case 58:
						goto IL_4FB;
					case 59:
						goto IL_1C9;
					case 60:
						if (reader.HasAttribute(ClipboardData.b("㡳፵ṷ๹ㅻώ", a_)))
						{
							num = 37;
							continue;
						}
						goto IL_73A;
					case 61:
						this.PageBorderOffsetFrom = (PageBorderOffsetFrom)reader.ReadEnum(ClipboardData.b("⑳᝵ίό⽻᭽쒅\udd91ﾙ\ud89d튟춡즣", a_), typeof(PageBorderOffsetFrom));
						num = 34;
						continue;
					case 62:
						if (reader.HasAttribute(ClipboardData.b("⑳᝵ίό⭻᝽", a_)))
						{
							num = 52;
							continue;
						}
						goto IL_674;
					}
					break;
					IL_1C9:
					num = 1;
					continue;
					IL_23A:
					num = 0;
					continue;
					IL_26E:
					num = 8;
					continue;
					IL_2A2:
					num = 38;
					continue;
					IL_2D6:
					num = 21;
					continue;
					IL_30A:
					num = 10;
					continue;
					IL_3C1:
					num = 20;
					continue;
					IL_435:
					num = 36;
					continue;
					IL_493:
					num = 54;
					continue;
					IL_4C7:
					num = 60;
					continue;
					IL_4FB:
					num = 56;
					continue;
					IL_52C:
					num = 4;
					continue;
					IL_560:
					num = 17;
					continue;
					IL_674:
					num = 29;
					continue;
					IL_6D2:
					num = 28;
					continue;
					IL_706:
					num = 43;
					continue;
					IL_73A:
					num = 41;
					continue;
					IL_806:
					num = 62;
					continue;
					IL_874:
					num = 23;
					continue;
					IL_8F1:
					num = 2;
					continue;
					IL_93A:
					num = 44;
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000E488 File Offset: 0x0000D488
		protected override void InitXDLSHolder()
		{
			int a_ = 14;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("ᙳ᥵੷ṹ᥻౽", a_), this.Borders);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000E4F4 File Offset: 0x0000D4F4
		public override string ToString()
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
			return base.ToString();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000E538 File Offset: 0x0000D538
		internal PageSetup ᜄ()
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
			PageSetup pageSetup = (PageSetup)base.CloneImpl();
			pageSetup.\u1719 = this.Borders.Clone();
			pageSetup.Margins = this.Margins.Clone();
			return pageSetup;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000E5A4 File Offset: 0x0000D5A4
		internal SizeF ᜂ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new SizeF(this.PageSize);
		}

		// Token: 0x0400094B RID: 2379
		private new const float ᜀ = 595.3f;

		// Token: 0x0400094C RID: 2380
		private const float ᜁ = 841.9f;

		// Token: 0x0400094D RID: 2381
		private const float ᜂ = 20f;

		// Token: 0x0400094E RID: 2382
		private const float ᜃ = 50f;

		// Token: 0x0400094F RID: 2383
		private const float ᜄ = 90f;

		// Token: 0x04000950 RID: 2384
		private const float ᜅ = 90f;

		// Token: 0x04000951 RID: 2385
		private const float ᜆ = 50f;

		// Token: 0x04000952 RID: 2386
		private const float ᜇ = 42.55f;

		// Token: 0x04000953 RID: 2387
		private const float ᜈ = 49.6f;

		// Token: 0x04000954 RID: 2388
		internal const float ᜉ = 36f;

		// Token: 0x04000955 RID: 2389
		private SizeF ᜊ;

		// Token: 0x04000956 RID: 2390
		private PageOrientation ᜋ;

		// Token: 0x04000957 RID: 2391
		private MarginsF ᜌ;

		// Token: 0x04000958 RID: 2392
		protected float m_fHeaderDistance;

		// Token: 0x04000959 RID: 2393
		protected float m_fFooterDistance;

		// Token: 0x0400095A RID: 2394
		private int[] \u25D8\u0085ª\u0096;

		// Token: 0x0400095B RID: 2395
		private PageAlignment \u170D;

		// Token: 0x0400095C RID: 2396
		private CharacterSpacing ᜎ;

		// Token: 0x0400095D RID: 2397
		private bool ᜏ;

		// Token: 0x0400095E RID: 2398
		private bool ᜐ;

		// Token: 0x0400095F RID: 2399
		private float ᜑ = 36f;

		// Token: 0x04000960 RID: 2400
		private long[] \u2460\u0080\u0085\u008B;

		// Token: 0x04000961 RID: 2401
		private LineNumberingRestartMode \u1712 = LineNumberingRestartMode.None;

		// Token: 0x04000962 RID: 2402
		private int \u1713 = 1;

		// Token: 0x04000963 RID: 2403
		private int \u1714;

		// Token: 0x04000964 RID: 2404
		private float \u1715;

		// Token: 0x04000965 RID: 2405
		private PageBordersApplyType \u1716;

		// Token: 0x04000966 RID: 2406
		private PageBorderOffsetFrom \u1717;

		// Token: 0x04000967 RID: 2407
		private bool \u1718 = true;

		// Token: 0x04000968 RID: 2408
		private Borders \u1719 = new Borders();

		// Token: 0x04000969 RID: 2409
		private bool \u171A;

		// Token: 0x0400096A RID: 2410
		private bool \u171B;

		// Token: 0x0400096B RID: 2411
		private float[] \u2593\u00A0\u00A8\u0083;

		// Token: 0x0400096C RID: 2412
		private PageNumberStyle \u171C;

		// Token: 0x0400096D RID: 2413
		private int \u171D;

		// Token: 0x0400096E RID: 2414
		private bool \u171E;

		// Token: 0x0400096F RID: 2415
		private float \u171F;

		// Token: 0x04000970 RID: 2416
		private GridPitchType ᜠ;

		// Token: 0x04000971 RID: 2417
		private string[] \u25D8\u008A\u009A\u00A2;

		// Token: 0x04000972 RID: 2418
		private bool ᜡ;

		// Token: 0x04000973 RID: 2419
		private bool ᜢ;

		// Token: 0x04000974 RID: 2420
		internal bool ᜣ;
	}
}
