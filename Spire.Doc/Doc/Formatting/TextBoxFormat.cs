using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x0200046F RID: 1135
	public class TextBoxFormat : FormatBase
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06003E3A RID: 15930 RVA: 0x00399B24 File Offset: 0x00398B24
		// (set) Token: 0x06003E3B RID: 15931 RVA: 0x00399B68 File Offset: 0x00398B68
		internal TextDirection LayoutFlowAlt
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x00399BAC File Offset: 0x00398BAC
		// (set) Token: 0x06003E3D RID: 15933 RVA: 0x00399BF0 File Offset: 0x00398BF0
		internal ShapeVerticalAlignment TextAnchor
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
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							num = 7;
							continue;
						case 2:
							num = 4;
							continue;
						case 3:
							if (value != ShapeVerticalAlignment.Outside)
							{
								num = 1;
								continue;
							}
							return;
						case 4:
							if (value != ShapeVerticalAlignment.Inside)
							{
								num = 6;
								continue;
							}
							return;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.ᜡ = value;
								num = 8;
								continue;
							}
							break;
						case 6:
							if (true)
							{
							}
							num = 3;
							continue;
						case 7:
							if (value != ShapeVerticalAlignment.None)
							{
								num = 5;
								continue;
							}
							return;
						case 8:
							return;
						}
						if (value == ShapeVerticalAlignment.Inline)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x00399CD0 File Offset: 0x00398CD0
		// (set) Token: 0x06003E3F RID: 15935 RVA: 0x00399D14 File Offset: 0x00398D14
		public HorizontalOrigin HorizontalOrigin
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

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x00399D58 File Offset: 0x00398D58
		// (set) Token: 0x06003E41 RID: 15937 RVA: 0x00399D9C File Offset: 0x00398D9C
		public VerticalOrigin VerticalOrigin
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
				this.ᜂ = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x00399DE0 File Offset: 0x00398DE0
		// (set) Token: 0x06003E43 RID: 15939 RVA: 0x00399E24 File Offset: 0x00398E24
		public TextWrappingStyle TextWrappingStyle
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
			set
			{
				this.ᜈ = value;
				if (value != TextWrappingStyle.Behind)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_0B;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					this.IsBelowText = false;
					return;
				}
				IL_0B:
				this.IsBelowText = true;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06003E44 RID: 15940 RVA: 0x00399E7C File Offset: 0x00398E7C
		// (set) Token: 0x06003E45 RID: 15941 RVA: 0x00399ED4 File Offset: 0x00398ED4
		public Color FillColor
		{
			get
			{
				if (true)
				{
				}
				if (this.\u1717 == null)
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
						return Color.White;
					}
				}
				return this.\u1717.Color;
			}
			set
			{
				for (;;)
				{
					this.FillEfects.Color = value;
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
								return;
							default:
								if (false)
								{
								}
								this.FillEfects.Type = BackgroundType.Color;
								num = 2;
								continue;
							}
							break;
						case 1:
							if (true)
							{
							}
							if (!value.IsEmpty)
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

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06003E46 RID: 15942 RVA: 0x00399F60 File Offset: 0x00398F60
		// (set) Token: 0x06003E47 RID: 15943 RVA: 0x00399FA4 File Offset: 0x00398FA4
		public TextBoxLineStyle LineStyle
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
				return this.ᜇ;
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
				this.ᜇ = value;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x00399FE8 File Offset: 0x00398FE8
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x0039A02C File Offset: 0x0039902C
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x0039A070 File Offset: 0x00399070
		// (set) Token: 0x06003E4B RID: 15947 RVA: 0x0039A0B4 File Offset: 0x003990B4
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x0039A0F8 File Offset: 0x003990F8
		// (set) Token: 0x06003E4D RID: 15949 RVA: 0x0039A13C File Offset: 0x0039913C
		public Color LineColor
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
				return this.ᜆ;
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
				this.ᜆ = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x0039A180 File Offset: 0x00399180
		// (set) Token: 0x06003E4F RID: 15951 RVA: 0x0039A1C4 File Offset: 0x003991C4
		public bool NoLine
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

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x0039A208 File Offset: 0x00399208
		// (set) Token: 0x06003E51 RID: 15953 RVA: 0x0039A24C File Offset: 0x0039924C
		internal WrapMode WrappingMode
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
				return this.ᜏ;
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
				this.ᜏ = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x0039A290 File Offset: 0x00399290
		// (set) Token: 0x06003E53 RID: 15955 RVA: 0x0039A2D4 File Offset: 0x003992D4
		public float HorizontalPosition
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06003E54 RID: 15956 RVA: 0x0039A318 File Offset: 0x00399318
		// (set) Token: 0x06003E55 RID: 15957 RVA: 0x0039A35C File Offset: 0x0039935C
		internal bool IsBelowText
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

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06003E56 RID: 15958 RVA: 0x0039A3A0 File Offset: 0x003993A0
		// (set) Token: 0x06003E57 RID: 15959 RVA: 0x0039A3E4 File Offset: 0x003993E4
		public float VerticalPosition
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜊ = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x0039A428 File Offset: 0x00399428
		// (set) Token: 0x06003E59 RID: 15961 RVA: 0x0039A46C File Offset: 0x0039946C
		public TextWrappingType TextWrappingType
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

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06003E5A RID: 15962 RVA: 0x0039A4B0 File Offset: 0x003994B0
		// (set) Token: 0x06003E5B RID: 15963 RVA: 0x0039A4F4 File Offset: 0x003994F4
		internal int TextBoxShapeID
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
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜋ = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06003E5C RID: 15964 RVA: 0x0039A538 File Offset: 0x00399538
		// (set) Token: 0x06003E5D RID: 15965 RVA: 0x0039A57C File Offset: 0x0039957C
		public float LineWidth
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜌ = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x0039A5C0 File Offset: 0x003995C0
		// (set) Token: 0x06003E5F RID: 15967 RVA: 0x0039A604 File Offset: 0x00399604
		public LineDashing LineDashing
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u170D = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06003E60 RID: 15968 RVA: 0x0039A648 File Offset: 0x00399648
		// (set) Token: 0x06003E61 RID: 15969 RVA: 0x0039A68C File Offset: 0x0039968C
		public ShapeHorizontalAlignment HorizontalAlignment
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06003E62 RID: 15970 RVA: 0x0039A6D0 File Offset: 0x003996D0
		// (set) Token: 0x06003E63 RID: 15971 RVA: 0x0039A714 File Offset: 0x00399714
		public ShapeVerticalAlignment VerticalAlignment
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

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06003E64 RID: 15972 RVA: 0x0039A758 File Offset: 0x00399758
		// (set) Token: 0x06003E65 RID: 15973 RVA: 0x0039A79C File Offset: 0x0039979C
		internal float TextBoxIdentificator
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
				return this.ᜐ;
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
				this.ᜐ = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x0039A7E0 File Offset: 0x003997E0
		// (set) Token: 0x06003E67 RID: 15975 RVA: 0x0039A824 File Offset: 0x00399824
		internal bool IsHeaderTextBox
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

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x0039A868 File Offset: 0x00399868
		internal spr\u203E InternalMargin
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6F;
					case 1:
						this.\u1716 = new spr\u203E();
						num = 0;
						continue;
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
						if (this.\u1716 != null)
						{
							goto IL_71;
						}
						num = 1;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.\u1716;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x0039A8EC File Offset: 0x003998EC
		public Background FillEfects
		{
			get
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_70;
					case 2:
						this.\u1717 = new Background(BackgroundType.NoBackground);
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
						if (this.\u1717 != null)
						{
							goto IL_72;
						}
						num = 2;
						break;
					}
				}
				IL_70:
				IL_72:
				return this.\u1717;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06003E6A RID: 15978 RVA: 0x0039A974 File Offset: 0x00399974
		// (set) Token: 0x06003E6B RID: 15979 RVA: 0x0039A9B8 File Offset: 0x003999B8
		internal bool IsAllowInCell
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u1718 = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06003E6C RID: 15980 RVA: 0x0039A9FC File Offset: 0x003999FC
		// (set) Token: 0x06003E6D RID: 15981 RVA: 0x0039AA40 File Offset: 0x00399A40
		internal int OrderIndex
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
				return this.\u1719;
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
				this.\u1719 = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x0039AA84 File Offset: 0x00399A84
		internal List<string> DocxStyleProps
		{
			get
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
						this.\u171A = new List<string>();
						num = 1;
						continue;
					case 1:
						goto IL_6F;
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
						if (this.\u171A != null)
						{
							goto IL_71;
						}
						num = 0;
						break;
					}
				}
				IL_6F:
				IL_71:
				return this.\u171A;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x0039AB08 File Offset: 0x00399B08
		internal bool HasDocxProps
		{
			get
			{
				if (this.\u171A == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					}
					if (false)
					{
					}
					if (true)
					{
					}
					return false;
				}
				return true;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x0039AB50 File Offset: 0x00399B50
		// (set) Token: 0x06003E71 RID: 15985 RVA: 0x0039AB94 File Offset: 0x00399B94
		internal bool IsFitTextToShape
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

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x0039ABD8 File Offset: 0x00399BD8
		// (set) Token: 0x06003E73 RID: 15987 RVA: 0x0039AC1C File Offset: 0x00399C1C
		internal bool IsFitShapeToText
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u171C = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x0039AC60 File Offset: 0x00399C60
		// (set) Token: 0x06003E75 RID: 15989 RVA: 0x0039ACA4 File Offset: 0x00399CA4
		internal float HorizontalRelativePercent
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
				return this.\u171D;
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
				this.\u171D = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06003E76 RID: 15990 RVA: 0x0039ACE8 File Offset: 0x00399CE8
		// (set) Token: 0x06003E77 RID: 15991 RVA: 0x0039AD2C File Offset: 0x00399D2C
		internal float VerticalRelativePercent
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

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x0039AD70 File Offset: 0x00399D70
		// (set) Token: 0x06003E79 RID: 15993 RVA: 0x0039ADB4 File Offset: 0x00399DB4
		internal TextDirection TextDirection
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
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.\u171F = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06003E7A RID: 15994 RVA: 0x0039ADF8 File Offset: 0x00399DF8
		// (set) Token: 0x06003E7B RID: 15995 RVA: 0x0039AE3C File Offset: 0x00399E3C
		internal bool IsInShape
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
				return this.ᜢ;
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
				this.ᜢ = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x0039AE80 File Offset: 0x00399E80
		// (set) Token: 0x06003E7D RID: 15997 RVA: 0x0039AEC4 File Offset: 0x00399EC4
		internal PointF StartPoint
		{
			[CompilerGenerated]
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
				return this.ᜣ;
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
				this.ᜣ = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06003E7E RID: 15998 RVA: 0x0039AF08 File Offset: 0x00399F08
		// (set) Token: 0x06003E7F RID: 15999 RVA: 0x0039AF4C File Offset: 0x00399F4C
		internal bool IsInGroupShape
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
				return this.ᜤ;
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
				this.ᜤ = value;
			}
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x0039AF90 File Offset: 0x00399F90
		public TextBoxFormat()
		{
			this.ᜈ = TextWrappingStyle.InFrontOfText;
			this.ᜅ = Color.White;
			this.ᜆ = Color.Black;
			this.ᜇ = TextBoxLineStyle.Simple;
			this.ᜁ = HorizontalOrigin.Column;
			this.ᜂ = VerticalOrigin.Paragraph;
			this.ᜌ = 0.75f;
			this.\u170D = LineDashing.Solid;
			this.ᜏ = WrapMode.None;
			this.\u1714 = ShapeHorizontalAlignment.None;
			this.\u1715 = ShapeVerticalAlignment.None;
			this.\u1717 = new Background(BackgroundType.NoBackground);
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x0039B028 File Offset: 0x0039A028
		protected override object GetDefValue(int key)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return null;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x0039B064 File Offset: 0x0039A064
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 14;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 20;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.TextWrappingType = (TextWrappingType)reader.ReadEnum(ClipboardData.b("⍳ѵ᥷੹౻᝽킃ﾅ", a_), typeof(TextWrappingType));
						num = 33;
						continue;
					case 1:
						this.VerticalOrigin = (VerticalOrigin)reader.ReadEnum(ClipboardData.b("≳፵੷๹ᕻᵽ쮃", a_), typeof(VerticalOrigin));
						num = 24;
						continue;
					case 2:
						goto IL_4D2;
					case 3:
						if (reader.HasAttribute(ClipboardData.b("㡳ήᙷό⭻᝽", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_6E8;
					case 4:
						goto IL_71C;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("㱳᥵੷፹ٻᅽ잇憐ﲑ", a_)))
						{
							num = 32;
							continue;
						}
						goto IL_647;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("㱳᥵੷፹ٻᅽ\ud887ﾋﮑﮓ", a_)))
						{
							num = 56;
							continue;
						}
						goto IL_4D2;
					case 7:
						this.LineWidth = reader.ReadFloat(ClipboardData.b("㡳ήᙷό⭻᝽", a_));
						num = 13;
						continue;
					case 8:
						this.VerticalPosition = reader.ReadFloat(ClipboardData.b("≳፵੷๹ᕻᵽ풃ﮇﾏﲑ", a_));
						num = 62;
						continue;
					case 9:
						if (reader.HasAttribute(ClipboardData.b("㡳ήᙷό㽻ᅽ", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_506;
					case 10:
						this.IsHeaderTextBox = reader.ReadBoolean(ClipboardData.b("㵳յぷόᵻ᩽", a_));
						num = 36;
						continue;
					case 11:
						this.FillColor = reader.ReadColor(ClipboardData.b("㉳ήᑷᙹ㽻ᅽ", a_));
						num = 41;
						continue;
					case 12:
						goto IL_7DC;
					case 13:
						goto IL_6E8;
					case 14:
						goto IL_49E;
					case 15:
						if (reader.HasAttribute(ClipboardData.b("㵳յぷόᵻ᩽", a_)))
						{
							num = 10;
							continue;
						}
						return;
					case 16:
						this.NoLine = reader.ReadBoolean(ClipboardData.b("㩳᥵㑷፹ቻ᭽", a_));
						num = 14;
						continue;
					case 17:
						this.LineColor = reader.ReadColor(ClipboardData.b("㡳ήᙷό㽻ᅽ", a_));
						num = 19;
						continue;
					case 18:
						if (reader.HasAttribute(ClipboardData.b("⍳ήᱷ๹ᑻ", a_)))
						{
							num = 44;
							continue;
						}
						goto IL_56B;
					case 19:
						goto IL_506;
					case 20:
						if (reader.HasAttribute(ClipboardData.b("㉳ήᑷᙹ㽻ᅽ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_6B4;
					case 21:
						if (reader.HasAttribute(ClipboardData.b("㱳᥵੷፹ٻᅽ즇ﺏﾑ", a_)))
						{
							num = 58;
							continue;
						}
						goto IL_315;
					case 22:
						if (reader.HasAttribute(ClipboardData.b("㡳ήᙷό⽻੽勵", a_)))
						{
							num = 31;
							continue;
						}
						goto IL_3C7;
					case 23:
						if (reader.HasAttribute(ClipboardData.b("⍳ѵ᥷੹౻᝽킃ﾅ", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_245;
					case 24:
						goto IL_431;
					case 25:
						this.Height = reader.ReadFloat(ClipboardData.b("㱳፵ᅷᵹᑻ੽", a_));
						num = 12;
						continue;
					case 26:
						goto IL_56B;
					case 27:
						if (reader.HasAttribute(ClipboardData.b("㩳᥵㑷፹ቻ᭽", a_)))
						{
							num = 16;
							continue;
						}
						goto IL_49E;
					case 28:
						if (reader.HasAttribute(ClipboardData.b("❳ṵ᥷੹᥻㝽쑿", a_)))
						{
							num = 55;
							continue;
						}
						goto IL_841;
					case 29:
						goto IL_841;
					case 30:
						goto IL_279;
					case 31:
						this.LineStyle = (TextBoxLineStyle)reader.ReadEnum(ClipboardData.b("㡳ήᙷό⽻੽勵", a_), typeof(TextBoxLineStyle));
						num = 45;
						continue;
					case 32:
						this.HorizontalOrigin = (HorizontalOrigin)reader.ReadEnum(ClipboardData.b("㱳᥵੷፹ٻᅽ잇憐ﲑ", a_), typeof(HorizontalOrigin));
						num = 50;
						continue;
					case 33:
						goto IL_245;
					case 34:
						if (reader.HasAttribute(ClipboardData.b("⍳ѵ᥷੹౻᝽힃", a_)))
						{
							num = 53;
							continue;
						}
						goto IL_2E1;
					case 35:
						if (reader.HasAttribute(ClipboardData.b("≳፵੷๹ᕻᵽ풃ﮇﾏﲑ", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_2AD;
					case 36:
						return;
					case 37:
						goto IL_911;
					case 38:
						goto IL_315;
					case 39:
						goto IL_537;
					case 40:
						this.FillColor = Color.Empty;
						num = 43;
						continue;
					case 41:
						goto IL_6B4;
					case 42:
						if (reader.HasAttribute(ClipboardData.b("≳፵੷๹ᕻᵽ쮃", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_431;
					case 43:
						goto IL_1D8;
					case 44:
						this.Width = reader.ReadFloat(ClipboardData.b("⍳ήᱷ๹ᑻ", a_));
						num = 26;
						continue;
					case 45:
						goto IL_3C7;
					case 46:
						if (reader.HasAttribute(ClipboardData.b("≳፵੷๹ᕻᵽ얃ﲑ", a_)))
						{
							num = 59;
							continue;
						}
						goto IL_279;
					case 47:
						this.IsBelowText = reader.ReadBoolean(ClipboardData.b("㵳յ㩷όၻᅽ횁ﺅﲇ", a_));
						num = 37;
						continue;
					case 48:
						this.LineDashing = (LineDashing)reader.ReadEnum(ClipboardData.b("㡳ήᙷό㡻ώ", a_), typeof(LineDashing));
						num = 4;
						continue;
					case 49:
						if (reader.HasAttribute(ClipboardData.b("⍳ѵ᥷੹౻᝽즃", a_)))
						{
							num = 51;
							continue;
						}
						goto IL_537;
					case 50:
						goto IL_647;
					case 51:
						this.WrappingMode = (WrapMode)reader.ReadEnum(ClipboardData.b("⍳ѵ᥷੹౻᝽즃", a_), typeof(WrapMode));
						goto IL_6A4;
					case 52:
						if (reader.HasAttribute(ClipboardData.b("㵳յ㩷όၻᅽ횁ﺅﲇ", a_)))
						{
							num = 47;
							continue;
						}
						goto IL_911;
					case 53:
						this.TextWrappingStyle = (TextWrappingStyle)reader.ReadEnum(ClipboardData.b("⍳ѵ᥷੹౻᝽힃", a_), typeof(TextWrappingStyle));
						num = 60;
						continue;
					case 54:
						if (reader.HasAttribute(ClipboardData.b("㡳ήᙷό㡻ώ", a_)))
						{
							num = 48;
							continue;
						}
						goto IL_71C;
					case 55:
						if (true)
						{
						}
						this.TextBoxShapeID = reader.ReadInt(ClipboardData.b("❳ṵ᥷੹᥻㝽쑿", a_));
						num = 29;
						continue;
					case 56:
						this.HorizontalPosition = reader.ReadFloat(ClipboardData.b("㱳᥵੷፹ٻᅽ\ud887ﾋﮑﮓ", a_));
						num = 2;
						continue;
					case 57:
						if (reader.HasAttribute(ClipboardData.b("㱳፵ᅷᵹᑻ੽", a_)))
						{
							num = 25;
							continue;
						}
						goto IL_7DC;
					case 58:
						this.HorizontalAlignment = (ShapeHorizontalAlignment)reader.ReadEnum(ClipboardData.b("㱳᥵੷፹ٻᅽ즇ﺏﾑ", a_), typeof(ShapeHorizontalAlignment));
						num = 38;
						continue;
					case 59:
						this.VerticalAlignment = (ShapeVerticalAlignment)reader.ReadEnum(ClipboardData.b("≳፵੷๹ᕻᵽ얃ﲑ", a_), typeof(ShapeVerticalAlignment));
						num = 30;
						continue;
					case 60:
						goto IL_2E1;
					case 61:
						if (reader.HasAttribute(ClipboardData.b("㩳᥵㹷፹ၻች", a_)))
						{
							num = 40;
							continue;
						}
						goto IL_1D8;
					case 62:
						goto IL_2AD;
					}
					break;
					IL_1D8:
					num = 21;
					continue;
					IL_245:
					num = 52;
					continue;
					IL_279:
					num = 28;
					continue;
					IL_2AD:
					num = 49;
					continue;
					IL_2E1:
					num = 42;
					continue;
					IL_315:
					num = 46;
					continue;
					IL_3C7:
					num = 34;
					continue;
					IL_431:
					num = 18;
					continue;
					IL_49E:
					num = 61;
					continue;
					IL_4D2:
					num = 54;
					continue;
					IL_506:
					num = 6;
					continue;
					IL_537:
					num = 23;
					continue;
					IL_56B:
					num = 9;
					continue;
					IL_647:
					num = 22;
					continue;
					IL_6A4:
					num = 39;
					continue;
					IL_911:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6A4;
					default:
						if (false)
						{
						}
						num = 27;
						continue;
					}
					IL_6B4:
					num = 57;
					continue;
					IL_6E8:
					num = 35;
					continue;
					IL_71C:
					num = 3;
					continue;
					IL_7DC:
					num = 5;
					continue;
					IL_841:
					num = 15;
				}
			}
		}

		// Token: 0x06003E83 RID: 16003 RVA: 0x0039B9D0 File Offset: 0x0039A9D0
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 13;
			if (true)
			{
			}
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.LineStyle != TextBoxLineStyle.Simple)
						{
							num = 20;
							continue;
						}
						goto IL_29E;
					case 1:
						if (this.VerticalPosition != 0f)
						{
							num = 53;
							continue;
						}
						goto IL_48A;
					case 2:
						if (this.TextWrappingType != TextWrappingType.Both)
						{
							num = 22;
							continue;
						}
						goto IL_7E1;
					case 3:
						writer.WriteValue(ClipboardData.b("╲ၴն൸ቺṼṾ슂ﾐ", a_), this.VerticalAlignment);
						num = 16;
						continue;
					case 4:
						if (this.LineColor != Color.Black)
						{
							num = 43;
							continue;
						}
						goto IL_432;
					case 5:
						writer.WriteValue(ClipboardData.b("㭲ᩴնၸźቼᅾ힆ﮎﲒﮔ", a_), this.HorizontalPosition);
						num = 57;
						continue;
					case 6:
						return;
					case 7:
						writer.WriteValue(ClipboardData.b("㽲ᱴ᥶ᱸ㽺ᱼ౾", a_), this.LineDashing);
						num = 62;
						continue;
					case 8:
						writer.WriteValue(ClipboardData.b("⑲ݴᙶॸ୺ᑼᅾ킂ﺆ", a_), this.TextWrappingStyle);
						num = 9;
						continue;
					case 9:
						goto IL_3B6;
					case 10:
						if (this.HorizontalAlignment != ShapeHorizontalAlignment.None)
						{
							num = 24;
							continue;
						}
						goto IL_2C5;
					case 11:
						writer.WriteValue(ClipboardData.b("⑲ᱴ፶൸፺", a_), this.Width);
						num = 27;
						continue;
					case 12:
						goto IL_58B;
					case 13:
						goto IL_227;
					case 14:
						if (this.HorizontalOrigin != HorizontalOrigin.Column)
						{
							num = 31;
							continue;
						}
						goto IL_364;
					case 15:
						if (this.FillColor == Color.Empty)
						{
							num = 39;
							continue;
						}
						num = 29;
						continue;
					case 16:
						goto IL_24D;
					case 17:
						if (this.Height != 0f)
						{
							num = 42;
							continue;
						}
						goto IL_58B;
					case 18:
						if (this.WrappingMode != WrapMode.None)
						{
							num = 36;
							continue;
						}
						goto IL_227;
					case 19:
						if (this.NoLine)
						{
							num = 52;
							continue;
						}
						goto IL_1D7;
					case 20:
						writer.WriteValue(ClipboardData.b("㽲ᱴ᥶ᱸ⡺ॼپ", a_), this.LineStyle);
						num = 28;
						continue;
					case 21:
						goto IL_4B1;
					case 22:
						writer.WriteValue(ClipboardData.b("⑲ݴᙶॸ୺ᑼᅾ힂ﲄ", a_), this.TextWrappingType);
						num = 34;
						continue;
					case 23:
						if (this.HorizontalPosition != 0f)
						{
							num = 5;
							continue;
						}
						goto IL_637;
					case 24:
						writer.WriteValue(ClipboardData.b("㭲ᩴնၸźቼᅾ욆ﲐﮔ", a_), this.HorizontalAlignment);
						num = 33;
						continue;
					case 25:
						if (this.VerticalAlignment != ShapeVerticalAlignment.None)
						{
							num = 3;
							continue;
						}
						goto IL_24D;
					case 26:
						if (this.LineWidth != 0.75f)
						{
							num = 55;
							continue;
						}
						goto IL_273;
					case 27:
						goto IL_45D;
					case 28:
						goto IL_29E;
					case 29:
						if (this.FillColor != Color.White)
						{
							num = 49;
							continue;
						}
						goto IL_6DC;
					case 30:
						goto IL_48A;
					case 31:
						writer.WriteValue(ClipboardData.b("㭲ᩴնၸźቼᅾ좆ﮈﾐ", a_), this.HorizontalOrigin);
						num = 56;
						continue;
					case 32:
						writer.WriteValue(ClipboardData.b("╲ၴն൸ቺṼṾ첂", a_), this.VerticalOrigin);
						num = 21;
						continue;
					case 33:
						goto IL_2C5;
					case 34:
						goto IL_7E1;
					case 35:
						if (this.IsBelowText)
						{
							num = 60;
							continue;
						}
						goto IL_40C;
					case 36:
						writer.WriteValue(ClipboardData.b("⑲ݴᙶॸ୺ᑼᅾ캂", a_), this.WrappingMode);
						num = 13;
						continue;
					case 37:
						goto IL_6DC;
					case 38:
						writer.WriteValue(ClipboardData.b("⁲ᵴᙶॸṺ㑼㭾", a_), this.TextBoxShapeID);
						num = 44;
						continue;
					case 39:
						writer.WriteValue(ClipboardData.b("㵲ᩴㅶၸ᝺ᅼ", a_), true);
						num = 37;
						continue;
					case 40:
						goto IL_273;
					case 41:
						goto IL_40C;
					case 42:
						writer.WriteValue(ClipboardData.b("㭲ၴṶṸ፺ॼ", a_), this.Height);
						num = 12;
						continue;
					case 43:
						writer.WriteValue(ClipboardData.b("㽲ᱴ᥶ᱸ㡺ቼ፾", a_), this.LineColor);
						num = 46;
						continue;
					case 44:
						goto IL_738;
					case 45:
						if (this.LineDashing != LineDashing.Solid)
						{
							num = 7;
							continue;
						}
						goto IL_60C;
					case 46:
						goto IL_432;
					case 47:
						if (this.VerticalOrigin != VerticalOrigin.Paragraph)
						{
							num = 32;
							continue;
						}
						goto IL_4B1;
					case 48:
						writer.WriteValue(ClipboardData.b("㩲ٴ㽶ᱸ᩺᥼᩾", a_), this.IsHeaderTextBox);
						num = 6;
						continue;
					case 49:
						writer.WriteValue(ClipboardData.b("㕲ᱴ᭶ᕸ㡺ቼ፾", a_), this.FillColor);
						num = 54;
						continue;
					case 50:
						if (this.TextWrappingStyle != TextWrappingStyle.Square)
						{
							num = 8;
							continue;
						}
						goto IL_3B6;
					case 51:
						goto IL_1D7;
					case 52:
						writer.WriteValue(ClipboardData.b("㵲ᩴ㭶ၸᕺ᡼", a_), this.NoLine);
						num = 51;
						continue;
					case 53:
						writer.WriteValue(ClipboardData.b("╲ၴն൸ቺṼṾ펂ﾊﾐ", a_), this.VerticalPosition);
						goto IL_5CC;
					case 54:
						goto IL_6DC;
					case 55:
						writer.WriteValue(ClipboardData.b("㽲ᱴ᥶ᱸⱺᑼ᭾", a_), this.LineWidth);
						num = 40;
						continue;
					case 56:
						goto IL_364;
					case 57:
						goto IL_637;
					case 58:
						if (this.Width != 0f)
						{
							num = 11;
							continue;
						}
						goto IL_45D;
					case 59:
						if (this.TextBoxShapeID != 0)
						{
							num = 38;
							continue;
						}
						goto IL_738;
					case 60:
						writer.WriteValue(ClipboardData.b("㩲ٴ㕶ᱸ᝺ቼࡾ햀ﶄ", a_), this.IsBelowText);
						num = 41;
						continue;
					case 61:
						if (this.IsHeaderTextBox)
						{
							num = 48;
							continue;
						}
						return;
					case 62:
						goto IL_60C;
					}
					break;
					IL_1D7:
					num = 10;
					continue;
					IL_227:
					num = 2;
					continue;
					IL_24D:
					num = 59;
					continue;
					IL_273:
					num = 1;
					continue;
					IL_29E:
					num = 50;
					continue;
					IL_2C5:
					num = 25;
					continue;
					IL_364:
					num = 0;
					continue;
					IL_3B6:
					num = 47;
					continue;
					IL_40C:
					num = 19;
					continue;
					IL_432:
					num = 23;
					continue;
					IL_45D:
					num = 4;
					continue;
					IL_48A:
					num = 18;
					continue;
					IL_4B1:
					num = 58;
					continue;
					IL_58B:
					num = 14;
					continue;
					IL_5CC:
					num = 30;
					continue;
					IL_7E1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5CC;
					default:
						if (false)
						{
						}
						num = 35;
						continue;
					}
					IL_60C:
					num = 26;
					continue;
					IL_637:
					num = 45;
					continue;
					IL_6DC:
					num = 17;
					continue;
					IL_738:
					num = 61;
				}
			}
		}

		// Token: 0x06003E84 RID: 16004 RVA: 0x0039C200 File Offset: 0x0039B200
		public TextBoxFormat Clone()
		{
			TextBoxFormat textBoxFormat;
			for (;;)
			{
				textBoxFormat = new TextBoxFormat();
				textBoxFormat.FillColor = this.FillColor;
				textBoxFormat.LineColor = this.LineColor;
				textBoxFormat.Height = this.Height;
				textBoxFormat.HorizontalOrigin = this.HorizontalOrigin;
				textBoxFormat.LineStyle = this.LineStyle;
				textBoxFormat.TextWrappingStyle = this.TextWrappingStyle;
				textBoxFormat.VerticalOrigin = this.VerticalOrigin;
				textBoxFormat.Width = this.Width;
				textBoxFormat.HorizontalPosition = this.HorizontalPosition;
				textBoxFormat.VerticalPosition = this.VerticalPosition;
				textBoxFormat.TextBoxShapeID = this.TextBoxShapeID;
				textBoxFormat.LineWidth = this.LineWidth;
				textBoxFormat.LineDashing = this.LineDashing;
				textBoxFormat.TextWrappingType = this.TextWrappingType;
				textBoxFormat.WrappingMode = this.WrappingMode;
				textBoxFormat.TextBoxIdentificator = this.TextBoxIdentificator;
				textBoxFormat.IsBelowText = this.IsBelowText;
				textBoxFormat.NoLine = this.NoLine;
				textBoxFormat.HorizontalAlignment = this.HorizontalAlignment;
				textBoxFormat.VerticalAlignment = this.VerticalAlignment;
				textBoxFormat.IsHeaderTextBox = this.IsHeaderTextBox;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1AC:
					textBoxFormat.\u1717 = this.\u1717.ᜇ();
					num = 4;
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_18E;
					case 1:
						textBoxFormat.\u1716 = this.\u1716.ᜁ();
						num = 0;
						continue;
					case 2:
						goto IL_1AC;
					case 3:
						if (this.\u1716 != null)
						{
							num = 1;
							continue;
						}
						goto IL_18E;
					case 4:
						return textBoxFormat;
					case 5:
						if (this.\u1717 != null)
						{
							num = 2;
							continue;
						}
						return textBoxFormat;
					}
					break;
					IL_18E:
					num = 5;
				}
			}
			return textBoxFormat;
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x0039C3DC File Offset: 0x0039B3DC
		internal void ᜀ(spr\u2459 A_0, Document A_1)
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
			this.\u1717 = new Background(A_1, A_0);
		}

		// Token: 0x04002D7B RID: 11643
		private new const float ᜀ = 0.75f;

		// Token: 0x04002D7C RID: 11644
		private HorizontalOrigin ᜁ;

		// Token: 0x04002D7D RID: 11645
		private new VerticalOrigin ᜂ;

		// Token: 0x04002D7E RID: 11646
		private new float ᜃ;

		// Token: 0x04002D7F RID: 11647
		private bool \u2460\u008E\u0095\u00A2;

		// Token: 0x04002D80 RID: 11648
		private new float ᜄ;

		// Token: 0x04002D81 RID: 11649
		private Color ᜅ;

		// Token: 0x04002D82 RID: 11650
		private Color ᜆ;

		// Token: 0x04002D83 RID: 11651
		private TextBoxLineStyle ᜇ;

		// Token: 0x04002D84 RID: 11652
		private TextWrappingStyle ᜈ;

		// Token: 0x04002D85 RID: 11653
		private new float ᜉ;

		// Token: 0x04002D86 RID: 11654
		private new float ᜊ;

		// Token: 0x04002D87 RID: 11655
		private int ᜋ;

		// Token: 0x04002D88 RID: 11656
		private int \u25D8\u0085\u00A8\u0084;

		// Token: 0x04002D89 RID: 11657
		private float ᜌ;

		// Token: 0x04002D8A RID: 11658
		private LineDashing \u170D;

		// Token: 0x04002D8B RID: 11659
		private TextWrappingType ᜎ;

		// Token: 0x04002D8C RID: 11660
		private WrapMode ᜏ;

		// Token: 0x04002D8D RID: 11661
		private float ᜐ;

		// Token: 0x04002D8E RID: 11662
		private bool ᜑ;

		// Token: 0x04002D8F RID: 11663
		private bool \u1712;

		// Token: 0x04002D90 RID: 11664
		private bool \u1713;

		// Token: 0x04002D91 RID: 11665
		private ShapeHorizontalAlignment \u1714;

		// Token: 0x04002D92 RID: 11666
		private ShapeVerticalAlignment \u1715;

		// Token: 0x04002D93 RID: 11667
		private spr\u203E \u1716;

		// Token: 0x04002D94 RID: 11668
		private Background \u1717;

		// Token: 0x04002D95 RID: 11669
		private bool \u1718 = true;

		// Token: 0x04002D96 RID: 11670
		private int \u1719;

		// Token: 0x04002D97 RID: 11671
		private List<string> \u171A;

		// Token: 0x04002D98 RID: 11672
		private bool \u171B;

		// Token: 0x04002D99 RID: 11673
		private bool \u171C;

		// Token: 0x04002D9A RID: 11674
		private float \u171D = float.MinValue;

		// Token: 0x04002D9B RID: 11675
		private float \u171E = float.MinValue;

		// Token: 0x04002D9C RID: 11676
		private TextDirection \u171F;

		// Token: 0x04002D9D RID: 11677
		private TextDirection ᜠ;

		// Token: 0x04002D9E RID: 11678
		private ShapeVerticalAlignment ᜡ;

		// Token: 0x04002D9F RID: 11679
		[CompilerGenerated]
		private bool ᜢ;

		// Token: 0x04002DA0 RID: 11680
		[CompilerGenerated]
		private PointF ᜣ;

		// Token: 0x04002DA1 RID: 11681
		[CompilerGenerated]
		private bool ᜤ;
	}
}
