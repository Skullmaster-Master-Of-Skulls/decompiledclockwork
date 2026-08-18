using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x0200046C RID: 1132
	public class ParagraphFormat : FormatBase
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x00390B48 File Offset: 0x0038FB48
		// (set) Token: 0x06003D74 RID: 15732 RVA: 0x00390B90 File Offset: 0x0038FB90
		internal float FirstLineIndentChars
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
				return (float)this.ᜃ(86);
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
				this.ᜁ(86, value);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x00390BDC File Offset: 0x0038FBDC
		// (set) Token: 0x06003D76 RID: 15734 RVA: 0x00390C24 File Offset: 0x0038FC24
		internal float LeftIndentChars
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
				return (float)this.ᜃ(85);
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
				this.ᜁ(85, value);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x00390C70 File Offset: 0x0038FC70
		// (set) Token: 0x06003D78 RID: 15736 RVA: 0x00390CB8 File Offset: 0x0038FCB8
		internal float RightIndentChars
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
				return (float)this.ᜃ(87);
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
				this.ᜁ(87, value);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x00390D04 File Offset: 0x0038FD04
		// (set) Token: 0x06003D7A RID: 15738 RVA: 0x00390D4C File Offset: 0x0038FD4C
		public bool IsBidi
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
				return (bool)this.ᜃ(31);
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
				this.ᜁ(31, value);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x00390D98 File Offset: 0x0038FD98
		public TabCollection Tabs
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
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
							goto IL_66;
						}
						break;
					}
					if (true)
					{
					}
					if (!this.HasValue(30))
					{
						num = 0;
						continue;
					}
					goto IL_6E;
					IL_36:
					this.ᜠ();
					num = 1;
				}
				IL_66:
				if (false)
				{
				}
				IL_6E:
				return (TabCollection)this.ᜃ(30);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x00390E20 File Offset: 0x0038FE20
		// (set) Token: 0x06003D7D RID: 15741 RVA: 0x00390E68 File Offset: 0x0038FE68
		public bool Keep
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
				return (bool)this.ᜃ(6);
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
				this.ᜁ(6, value);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x00390EB0 File Offset: 0x0038FEB0
		// (set) Token: 0x06003D7F RID: 15743 RVA: 0x00390EF8 File Offset: 0x0038FEF8
		public bool KeepFollow
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
				return (bool)this.ᜃ(10);
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
				this.ᜁ(10, value);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x00390F44 File Offset: 0x0038FF44
		// (set) Token: 0x06003D81 RID: 15745 RVA: 0x00390F8C File Offset: 0x0038FF8C
		public bool PageBreakBefore
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
				return (bool)this.ᜃ(12);
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
				this.ᜁ(12, value);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x00390FD8 File Offset: 0x0038FFD8
		// (set) Token: 0x06003D83 RID: 15747 RVA: 0x00391030 File Offset: 0x00390030
		public bool PageBreakAfter
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					if (base[13] != null)
					{
						goto IL_3C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_34;
					}
				}
				IL_34:
				if (false)
				{
				}
				return false;
				IL_3C:
				return (bool)base[13];
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
				base[13] = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x0039107C File Offset: 0x0039007C
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x003910C4 File Offset: 0x003900C4
		public bool IsWidowControl
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
				return (bool)this.ᜃ(11);
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
				this.ᜁ(11, value);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x00391110 File Offset: 0x00390110
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x00391158 File Offset: 0x00390158
		internal bool AutoSpaceDN
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
				return (bool)this.ᜃ(82);
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
				this.ᜁ(82, value);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x003911A4 File Offset: 0x003901A4
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x003911EC File Offset: 0x003901EC
		internal bool AutoSpaceDE
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
				return (bool)this.ᜃ(81);
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
				this.ᜁ(81, value);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x00391238 File Offset: 0x00390238
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x00391280 File Offset: 0x00390280
		internal bool AdjustRightIndent
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
				return (bool)this.ᜃ(80);
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
				this.ᜁ(80, value);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06003D8C RID: 15756 RVA: 0x003912CC File Offset: 0x003902CC
		// (set) Token: 0x06003D8D RID: 15757 RVA: 0x00391314 File Offset: 0x00390314
		public HorizontalAlignment HorizontalAlignment
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
				return (HorizontalAlignment)this.ᜃ(0);
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
				this.ᜁ(0, value);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x0039135C File Offset: 0x0039035C
		// (set) Token: 0x06003D8F RID: 15759 RVA: 0x003913A4 File Offset: 0x003903A4
		public float LeftIndent
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
				return (float)this.ᜃ(2);
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
				this.ᜁ(2, value);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06003D90 RID: 15760 RVA: 0x003913EC File Offset: 0x003903EC
		// (set) Token: 0x06003D91 RID: 15761 RVA: 0x00391434 File Offset: 0x00390434
		internal float LeftIndentBi
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
				return (float)this.ᜃ(68);
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
				this.ᜁ(68, value);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06003D92 RID: 15762 RVA: 0x00391480 File Offset: 0x00390480
		// (set) Token: 0x06003D93 RID: 15763 RVA: 0x003914C8 File Offset: 0x003904C8
		public float RightIndent
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

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06003D94 RID: 15764 RVA: 0x00391510 File Offset: 0x00390510
		internal float RightIndentEx
		{
			get
			{
				while (!this.HasValue(87))
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
						return this.FirstLineIndent;
					}
				}
				return this.RightIndentChars;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06003D95 RID: 15765 RVA: 0x00391564 File Offset: 0x00390564
		// (set) Token: 0x06003D96 RID: 15766 RVA: 0x003915AC File Offset: 0x003905AC
		internal float RightIndentBi
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
				return (float)this.ᜃ(69);
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
				this.ᜁ(69, value);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x003915F8 File Offset: 0x003905F8
		// (set) Token: 0x06003D98 RID: 15768 RVA: 0x00391640 File Offset: 0x00390640
		public float FirstLineIndent
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
				return (float)this.ᜃ(5);
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
				this.ᜁ(5, value);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x00391688 File Offset: 0x00390688
		internal float FirstLineIndentEx
		{
			get
			{
				while (!this.HasValue(86))
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
						return this.FirstLineIndent;
					}
				}
				return this.FirstLineIndentChars;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06003D9A RID: 15770 RVA: 0x003916DC File Offset: 0x003906DC
		// (set) Token: 0x06003D9B RID: 15771 RVA: 0x00391724 File Offset: 0x00390724
		internal float FirstLineIndentBi
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
				return (float)this.ᜃ(70);
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
				this.ᜁ(70, value);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x00391770 File Offset: 0x00390770
		// (set) Token: 0x06003D9D RID: 15773 RVA: 0x003917B8 File Offset: 0x003907B8
		public float BeforeSpacing
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
				return (float)this.ᜃ(8);
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
				this.ᜁ(8, value);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06003D9E RID: 15774 RVA: 0x00391800 File Offset: 0x00390800
		// (set) Token: 0x06003D9F RID: 15775 RVA: 0x00391848 File Offset: 0x00390848
		public float AfterSpacing
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
				return (float)this.ᜃ(9);
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
				this.ᜁ(9, value);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06003DA0 RID: 15776 RVA: 0x00391894 File Offset: 0x00390894
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
				return this.ᜃ(20) as Borders;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x003918DC File Offset: 0x003908DC
		// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x00391920 File Offset: 0x00390920
		public Color BackColor
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
				return this.\u173C;
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
				this.ᜁ(21, value);
				this.\u173C = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x00391970 File Offset: 0x00390970
		// (set) Token: 0x06003DA4 RID: 15780 RVA: 0x003919C8 File Offset: 0x003909C8
		public bool IsColumnBreakAfter
		{
			get
			{
				while (base[22] == null)
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
						return false;
					}
				}
				return (bool)base[22];
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
				base[22] = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06003DA5 RID: 15781 RVA: 0x00391A14 File Offset: 0x00390A14
		// (set) Token: 0x06003DA6 RID: 15782 RVA: 0x00391A58 File Offset: 0x00390A58
		internal sprḍ Sprms
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.\u1737 = null;
				this.ᜊ = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x00391AA4 File Offset: 0x00390AA4
		// (set) Token: 0x06003DA8 RID: 15784 RVA: 0x00391AEC File Offset: 0x00390AEC
		public float LineSpacing
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
				return (float)this.ᜃ(52);
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
				this.ᜁ(52, value);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x00391B38 File Offset: 0x00390B38
		// (set) Token: 0x06003DAA RID: 15786 RVA: 0x00391B80 File Offset: 0x00390B80
		public LineSpacingRule LineSpacingRule
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
				return (LineSpacingRule)this.ᜃ(53);
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06003DAB RID: 15787 RVA: 0x00391BCC File Offset: 0x00390BCC
		// (set) Token: 0x06003DAC RID: 15788 RVA: 0x00391C14 File Offset: 0x00390C14
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
				return (Color)this.ᜃ(32);
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
				this.ᜁ(32, value);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x00391C60 File Offset: 0x00390C60
		// (set) Token: 0x06003DAE RID: 15790 RVA: 0x00391CA8 File Offset: 0x00390CA8
		internal TextureStyle TextureStyle
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
				return (TextureStyle)this.ᜃ(33);
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
				this.ᜁ(33, value);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x00391CF4 File Offset: 0x00390CF4
		internal sprᨽ ParaProps
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
				this.ᜉ();
				return this.\u1737;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06003DB0 RID: 15792 RVA: 0x00391D3C File Offset: 0x00390D3C
		// (set) Token: 0x06003DB1 RID: 15793 RVA: 0x00391D84 File Offset: 0x00390D84
		internal bool IsSpacingBeforeAuto
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
				return (bool)this.ᜃ(54);
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
				this.ᜁ(54, value);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x00391DD0 File Offset: 0x00390DD0
		// (set) Token: 0x06003DB3 RID: 15795 RVA: 0x00391E18 File Offset: 0x00390E18
		internal bool IsSpacingAfterAuto
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
				return (bool)this.ᜃ(55);
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
				this.ᜁ(55, value);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06003DB4 RID: 15796 RVA: 0x00391E64 File Offset: 0x00390E64
		// (set) Token: 0x06003DB5 RID: 15797 RVA: 0x00391F38 File Offset: 0x00390F38
		public OutlineLevel OutlineLevel
		{
			get
			{
				byte b;
				for (;;)
				{
					IL_00:
					int num = 1;
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
								num = 5;
								continue;
							case 2:
								goto IL_7F;
							case 3:
								goto IL_60;
							case 4:
								if (b >= 0)
								{
									num = 0;
									continue;
								}
								return OutlineLevel.Body;
							case 5:
								if (b <= 9)
								{
									num = 2;
									continue;
								}
								return OutlineLevel.Body;
							}
							if (!this.HasValue(56))
							{
								num = 3;
							}
							else
							{
								b = (byte)this.ᜃ(56);
								num = 4;
							}
							break;
						}
					}
				}
				IL_60:
				if (true)
				{
				}
				return OutlineLevel.Body;
				IL_7F:
				return (OutlineLevel)Enum.ToObject(typeof(OutlineLevel), b);
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
				this.ᜁ(56, (byte)value);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x00391F84 File Offset: 0x00390F84
		// (set) Token: 0x06003DB7 RID: 15799 RVA: 0x00391FD0 File Offset: 0x00390FD0
		internal bool IsFrame
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
				this.ᜉ();
				return this.\u1737.ᜩ();
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
				this.ᜉ();
				this.\u1737.ᜅ(value);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x00392020 File Offset: 0x00391020
		// (set) Token: 0x06003DB9 RID: 15801 RVA: 0x0039206C File Offset: 0x0039106C
		internal byte FrameVerticalPos
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
				this.ᜉ();
				return this.\u1737.\u173A();
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
				this.ᜉ();
				this.\u1737.ᜆ(value);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x003920BC File Offset: 0x003910BC
		// (set) Token: 0x06003DBB RID: 15803 RVA: 0x00392108 File Offset: 0x00391108
		internal byte FrameHorizontalPos
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
				this.ᜉ();
				return this.\u1737.\u171A();
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
				this.ᜉ();
				this.\u1737.ᜅ(value);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x00392158 File Offset: 0x00391158
		// (set) Token: 0x06003DBD RID: 15805 RVA: 0x003921A4 File Offset: 0x003911A4
		internal short FrameX
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
				this.ᜉ();
				return this.\u1737.ᜏ();
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
				this.ᜉ();
				this.\u1737.ᜑ(value);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x003921F4 File Offset: 0x003911F4
		internal short FrameXEx
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
						if (this.FrameX != -16)
						{
							num = 6;
							continue;
						}
						return 0;
					case 2:
						num = 4;
						continue;
					case 3:
						if (this.FrameX != -8)
						{
							num = 2;
							continue;
						}
						return 0;
					case 4:
						if (this.FrameX != -12)
						{
							num = 7;
							continue;
						}
						return 0;
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
							goto IL_89;
						}
						break;
					case 7:
						num = 0;
						continue;
					}
					if (this.FrameX == -4)
					{
						return 0;
					}
					num = 5;
				}
				IL_89:
				if (false)
				{
				}
				return this.FrameX;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x003922E0 File Offset: 0x003912E0
		// (set) Token: 0x06003DC0 RID: 15808 RVA: 0x0039232C File Offset: 0x0039132C
		internal short FrameY
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
				this.ᜉ();
				return this.\u1737.ᝁ();
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
				this.ᜉ();
				this.\u1737.ᜆ(value);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x0039237C File Offset: 0x0039137C
		internal short FrameYEx
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_EC:
					if (this.FrameY == -12)
					{
						return 0;
					}
					num = 8;
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
						if (this.FrameY != -20)
						{
							num = 2;
							continue;
						}
						return 0;
					case 2:
						goto IL_97;
					case 3:
						if (this.FrameY != -8)
						{
							num = 6;
							continue;
						}
						return 0;
					case 4:
						num = 3;
						continue;
					case 5:
						if (this.FrameY != -16)
						{
							num = 9;
							continue;
						}
						return 0;
					case 6:
						num = 7;
						continue;
					case 7:
						goto IL_EC;
					case 8:
						num = 5;
						continue;
					case 9:
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (this.FrameY == -4)
					{
						return 0;
					}
					num = 4;
				}
				IL_97:
				return this.FrameY;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x00392490 File Offset: 0x00391490
		// (set) Token: 0x06003DC3 RID: 15811 RVA: 0x003924DC File Offset: 0x003914DC
		internal short FrameWidth
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
				this.ᜉ();
				return this.\u1737.ᝍ();
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
				this.ᜉ();
				this.\u1737.ᜐ(value);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x0039252C File Offset: 0x0039152C
		internal float FrameWidthEx
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					if (this.FrameWidth == 0)
					{
						goto IL_46;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_26;
					}
				}
				IL_26:
				if (false)
				{
				}
				return (float)this.FrameWidth / 20f;
				IL_46:
				return 0f;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x00392584 File Offset: 0x00391584
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x003925D0 File Offset: 0x003915D0
		internal short FrameHeight
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
				this.ᜉ();
				return this.\u1737.ᝂ();
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
				this.ᜉ();
				this.\u1737.ᜏ(value);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x00392620 File Offset: 0x00391620
		internal float FrameHeightEx
		{
			get
			{
				while (this.FrameHeight != 0)
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
						return (float)((ushort)this.FrameHeight & 32767) / 20f;
					}
				}
				return 0f;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x00392680 File Offset: 0x00391680
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x003926CC File Offset: 0x003916CC
		internal FrameSizeRule FrameWidthRule
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
				this.ᜉ();
				return this.\u1737.\u1712();
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
				this.ᜉ();
				this.\u1737.ᜀ(value);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x0039271C File Offset: 0x0039171C
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x00392768 File Offset: 0x00391768
		internal FrameSizeRule FrameHeightRule
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
				this.ᜉ();
				return this.\u1737.ᝌ();
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
				this.ᜉ();
				this.\u1737.ᜁ(value);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x003927B8 File Offset: 0x003917B8
		// (set) Token: 0x06003DCD RID: 15821 RVA: 0x00392804 File Offset: 0x00391804
		internal short FrameHorizontalDistanceFromText
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
				this.ᜉ();
				return this.\u1737.ᜐ();
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
				this.ᜉ();
				this.\u1737.ᜀ(value);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x00392854 File Offset: 0x00391854
		// (set) Token: 0x06003DCF RID: 15823 RVA: 0x003928A0 File Offset: 0x003918A0
		internal short FrameVerticalDistanceFromText
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
				this.ᜉ();
				return this.\u1737.ᜱ();
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
				this.ᜉ();
				this.\u1737.ᜎ(value);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06003DD0 RID: 15824 RVA: 0x003928F0 File Offset: 0x003918F0
		// (set) Token: 0x06003DD1 RID: 15825 RVA: 0x0039293C File Offset: 0x0039193C
		internal bool WrapFrameAround
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
				this.ᜉ();
				return this.\u1737.ᜁ();
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
				this.ᜉ();
				this.\u1737.ᜎ(value);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x0039298C File Offset: 0x0039198C
		internal bool HasListReference
		{
			get
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_56:
					num = 2;
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
						goto IL_56;
					case 1:
						return true;
					case 2:
						if (this.ᜊ.ᜇ(17931) != null)
						{
							num = 1;
							continue;
						}
						goto IL_58;
					}
					if (this.ᜊ == null)
					{
						return false;
					}
					num = 0;
				}
				IL_58:
				if (true)
				{
				}
				return false;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x00392A24 File Offset: 0x00391A24
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x00392A6C File Offset: 0x00391A6C
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
				return (bool)this.ᜃ(65);
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						for (;;)
						{
							this.ᜁ(65, value);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_55;
							}
						}
						IL_55:
						if (false)
						{
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					if (!value)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x00392AE8 File Offset: 0x00391AE8
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x00392B30 File Offset: 0x00391B30
		internal bool IsContextualSpacing
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
				return (bool)this.ᜃ(71);
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
				this.ᜁ(71, value);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x00392B7C File Offset: 0x00391B7C
		// (set) Token: 0x06003DD8 RID: 15832 RVA: 0x00392BC0 File Offset: 0x00391BC0
		internal ParagraphFormat TableStyleParagraphFormat
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
				return this.\u1736;
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
				this.\u1736 = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x00392C04 File Offset: 0x00391C04
		// (set) Token: 0x06003DDA RID: 15834 RVA: 0x00392C4C File Offset: 0x00391C4C
		public bool MirrorIndents
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
				return (bool)this.ᜃ(75);
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
				this.ᜁ(75, value);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06003DDB RID: 15835 RVA: 0x00392C98 File Offset: 0x00391C98
		// (set) Token: 0x06003DDC RID: 15836 RVA: 0x00392CE0 File Offset: 0x00391CE0
		public bool SuppressAutoHyphens
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
				return (bool)this.ᜃ(78);
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
				this.ᜁ(78, value);
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x00392D2C File Offset: 0x00391D2C
		public ParagraphFormat()
		{
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x00392D58 File Offset: 0x00391D58
		public ParagraphFormat(IDocument document) : base((Document)document)
		{
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x00392D8C File Offset: 0x00391D8C
		private object ᜃ(int A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜆ(A_0);
			return base[A_0];
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x00392DD8 File Offset: 0x00391DD8
		internal void ᜆ(int A_0)
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
					num = 3;
					continue;
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
						if (!this.ᜊ())
						{
							num = 1;
							continue;
						}
						goto IL_7C;
					}
					break;
				}
				if (!base.IsPropertyUpdated(A_0))
				{
					goto IL_7C;
				}
				num = 2;
			}
			return;
			IL_7C:
			this.ᜉ();
			base.SetPropUpdateFlag(A_0);
			this.ᜂ(A_0);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x00392E78 File Offset: 0x00391E78
		private new bool ᜊ()
		{
			switch (0)
			{
			default:
			{
				int num = 3;
				bool result;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9A;
					case 1:
						goto IL_57;
					case 2:
					{
						int a_;
						if (this.ᜊ.ᜇ(a_) != null)
						{
							num = 5;
							continue;
						}
						int num2;
						num2++;
						num = 1;
						continue;
					}
					case 4:
						goto IL_57;
					case 5:
						result = true;
						num = 8;
						continue;
					case 6:
					{
						int num2;
						int[] u173B;
						if (num2 >= u173B.Length)
						{
							num = 7;
							continue;
						}
						int a_ = u173B[num2];
						num = 2;
						continue;
					}
					case 7:
						goto IL_8B;
					case 8:
						goto IL_98;
					}
					if (this.ᜊ != null)
					{
						num = 0;
						continue;
					}
					break;
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_9A:
						int[] u173B = this.\u173B;
						int num2 = 0;
						num = 4;
						break;
					}
					default:
						if (false)
						{
						}
						num = 6;
						break;
					}
				}
				IL_8B:
				return false;
				IL_98:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x00392F8C File Offset: 0x00391F8C
		private void ᜉ()
		{
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
						goto IL_24;
					}
					if (false)
					{
					}
					if (this.ᜊ == null)
					{
						num = 4;
						continue;
					}
					this.\u1737 = new sprᨽ();
					this.\u1737.\u1739().ᜀ(this.ᜊ);
					num = 3;
					continue;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_62;
				case 4:
					goto IL_D0;
				}
				IL_24:
				if (this.\u1737 != null)
				{
					break;
				}
				num = 2;
			}
			IL_62:
			return;
			IL_D0:
			if (true)
			{
			}
			this.\u1737 = new sprᨽ();
			this.ᜊ = this.\u1737.ᜪ();
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x0039306C File Offset: 0x0039206C
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
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x003930B8 File Offset: 0x003920B8
		private void ᜀ(int A_0, object A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜊ = this.ᜊ.ᜀ();
					this.\u1739 = false;
					num = 2;
					continue;
				case 2:
					goto IL_56;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_56;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (this.ᜊ != null)
						{
							num = 0;
							continue;
						}
						goto IL_A3;
					}
					break;
				case 4:
					num = 3;
					continue;
				}
				if (!this.\u1739)
				{
					break;
				}
				num = 4;
			}
			IL_56:
			IL_A3:
			this.ᜉ();
			base.SetPropUpdateFlag(A_0);
			spr\u192A.ᜀ(A_0, A_1, this.\u1737, this);
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x00393184 File Offset: 0x00392184
		internal void ᜀ(TabCollection A_0)
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
			base[30] = A_0;
			this.ᜀ(30, A_0);
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x003931D4 File Offset: 0x003921D4
		internal void ᜠ()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base[30] = new TabCollection(this.m_doc, this);
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x00393224 File Offset: 0x00392224
		internal void ᜂ(ParagraphFormat A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					Dictionary<int, object>.Enumerator enumerator;
					switch (num)
					{
					case 0:
						return;
					case 1:
						try
						{
							num = 5;
							for (;;)
							{
								spr\u1CC1 spr_u1CC;
								switch (num)
								{
								case 0:
								{
									if (!enumerator.MoveNext())
									{
										num = 6;
										continue;
									}
									KeyValuePair<int, object> keyValuePair = enumerator.Current;
									num = 3;
									continue;
								}
								case 1:
									this.ᜊ = new sprḍ();
									num = 10;
									continue;
								case 2:
									goto IL_23E;
								case 3:
								{
									KeyValuePair<int, object> keyValuePair;
									if (keyValuePair.Key != 20)
									{
										num = 8;
										continue;
									}
									break;
								}
								case 4:
								{
									KeyValuePair<int, object> keyValuePair;
									int sprmOption = this.GetSprmOption(keyValuePair.Key);
									spr_u1CC = this.m_doc.ᜬ.Sprms.ᜇ(sprmOption);
									num = 9;
									continue;
								}
								case 6:
									num = 2;
									continue;
								case 7:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										num = 15;
										continue;
									}
									break;
								case 8:
									num = 11;
									continue;
								case 9:
									if (spr_u1CC != null)
									{
										num = 7;
										continue;
									}
									break;
								case 10:
									goto IL_189;
								case 11:
								{
									KeyValuePair<int, object> keyValuePair;
									if (!A_0.ᜅ(keyValuePair.Key))
									{
										num = 13;
										continue;
									}
									break;
								}
								case 12:
									if (this.m_doc.ᜬ.Sprms != null)
									{
										num = 4;
										continue;
									}
									break;
								case 13:
								{
									KeyValuePair<int, object> keyValuePair;
									base.PropertiesHash.Add(keyValuePair.Key, keyValuePair.Value);
									num = 12;
									continue;
								}
								case 15:
									if (this.ᜊ == null)
									{
										num = 1;
										continue;
									}
									goto IL_189;
								}
								IL_EB:
								num = 0;
								continue;
								goto IL_EB;
								IL_189:
								this.ᜊ.ᜆ(spr_u1CC);
								num = 14;
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
					if (true)
					{
					}
					if (this.m_doc.ᜬ == null)
					{
						num = 0;
						continue;
					}
					IL_24E:
					enumerator = this.m_doc.ᜬ.PropertiesHash.GetEnumerator();
					num = 1;
				}
				return;
			}
			}
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x003934D0 File Offset: 0x003924D0
		internal bool ᜅ(int A_0)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
					goto IL_8A;
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
						if (base.BaseFormat != null)
						{
							num = 2;
							continue;
						}
						return false;
					}
					break;
				}
				if (base.PropertiesHash.ContainsKey(A_0))
				{
					goto IL_8C;
				}
				num = 0;
			}
			return false;
			IL_8A:
			return (base.BaseFormat as ParagraphFormat).ᜅ(A_0);
			IL_8C:
			if (true)
			{
			}
			return true;
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x00393574 File Offset: 0x00392574
		protected internal override void EnsureComposites()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						base.EnsureComposites(new int[]
						{
							20
						});
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_61;
						}
					}
					IL_61:
					if (false)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				if (true)
				{
				}
				if (!base.HasKey(20))
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x003935FC File Offset: 0x003925FC
		protected override object GetDefValue(int key)
		{
			int a_ = 14;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 12;
					continue;
				case 1:
					goto IL_129;
				case 2:
					num = 1;
					continue;
				case 3:
					if (true)
					{
					}
					switch (key)
					{
					case 50:
						goto IL_106;
					case 51:
						goto IL_2F9;
					case 52:
						goto IL_73;
					case 53:
						goto IL_2C7;
					case 54:
					case 55:
						goto IL_12E;
					case 56:
						goto IL_108;
					default:
						num = 4;
						continue;
					}
					break;
				case 4:
					num = 8;
					continue;
				case 5:
					goto IL_173;
				case 6:
					num = 10;
					continue;
				case 7:
					switch (key)
					{
					case 30:
						goto IL_2BA;
					case 31:
						goto IL_2D5;
					case 32:
						goto IL_2DC;
					case 33:
						goto IL_B7;
					default:
						num = 2;
						continue;
					}
					break;
				case 8:
					switch (key)
					{
					case 65:
						goto IL_2D5;
					case 66:
					case 67:
					case 72:
					case 73:
					case 74:
					case 76:
					case 77:
					case 79:
					case 83:
					case 84:
						goto IL_2F9;
					case 68:
					case 69:
					case 70:
					case 85:
					case 86:
					case 87:
						goto IL_113;
					case 71:
					case 75:
					case 78:
					case 80:
					case 81:
					case 82:
						goto IL_12E;
					default:
						num = 6;
						continue;
					}
					break;
				case 10:
					goto IL_147;
				case 11:
					num = 7;
					continue;
				case 12:
					switch (key)
					{
					case 0:
						goto IL_2CE;
					case 1:
					case 4:
					case 7:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
						goto IL_2F9;
					case 2:
					case 3:
					case 5:
						goto IL_113;
					case 6:
					case 10:
					case 12:
					case 13:
					case 22:
						goto IL_2D5;
					case 8:
					case 9:
						goto IL_7E;
					case 11:
						goto IL_135;
					case 21:
						goto IL_2DC;
					default:
						num = 11;
						continue;
					}
					break;
				case 13:
					if (key <= 33)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
				case 14:
					if (this.m_doc.ᜬ != this)
					{
						num = 5;
						continue;
					}
					goto IL_BE;
				case 15:
					num = 14;
					continue;
				}
				if (this.m_doc.ᜬ != null)
				{
					num = 15;
					continue;
				}
				IL_BE:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C7;
				default:
					if (false)
					{
					}
					num = 13;
					break;
				}
			}
			IL_73:
			return 0f;
			IL_7E:
			return 0f;
			IL_B7:
			return TextureStyle.TextureNone;
			IL_106:
			return null;
			IL_108:
			return byte.MaxValue;
			IL_113:
			return 0f;
			IL_129:
			goto IL_2F9;
			IL_12E:
			return false;
			IL_135:
			return true;
			IL_147:
			goto IL_2F9;
			IL_173:
			return this.m_doc.ᜬ[key];
			IL_2BA:
			return new TabCollection(base.Document, this);
			IL_2C7:
			return LineSpacingRule.AtLeast;
			IL_2CE:
			return HorizontalAlignment.Left;
			IL_2D5:
			return false;
			IL_2DC:
			return Color.Empty;
			IL_2F9:
			throw new ArgumentException(ClipboardData.b("έ፵ŷ婹ᑻώꊁﺇ늑鍊", a_));
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x00393918 File Offset: 0x00392918
		protected override FormatBase GetDefComposite(int key)
		{
			if (key == 20)
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
				return base.GetDefComposite(20, new Borders(this, 20));
			}
			return null;
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x00393970 File Offset: 0x00392970
		protected override void ImportMembers(FormatBase format)
		{
			for (;;)
			{
				base.ImportMembers(format);
				ParagraphFormat paragraphFormat = format as ParagraphFormat;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_12D:
						if (!base.Document.ᜈ)
						{
							num = 6;
							continue;
						}
						goto IL_1AD;
					case 1:
						if (paragraphFormat.Sprms != null)
						{
							num = 13;
							continue;
						}
						goto IL_7D;
					case 2:
						goto IL_219;
					case 3:
						if (paragraphFormat != null)
						{
							num = 16;
							continue;
						}
						goto IL_219;
					case 4:
						goto IL_1AD;
					case 5:
						goto IL_1DA;
					case 6:
						num = 14;
						continue;
					case 7:
						if (paragraphFormat.Sprms != null)
						{
							num = 18;
							continue;
						}
						goto IL_1DA;
					case 8:
						base[13] = paragraphFormat.PageBreakAfter;
						num = 15;
						continue;
					case 9:
						if (paragraphFormat.HasValue(22))
						{
							num = 12;
							continue;
						}
						goto IL_219;
					case 10:
						goto IL_7D;
					case 11:
						if (paragraphFormat.HasValue(13))
						{
							if (true)
							{
							}
							num = 8;
							continue;
						}
						goto IL_FD;
					case 12:
						base[22] = paragraphFormat.IsColumnBreakAfter;
						num = 2;
						continue;
					case 13:
						this.ᜊ = paragraphFormat.Sprms.ᜀ();
						this.\u1739 = true;
						num = 17;
						continue;
					case 14:
						if (base.Document.ᜉ)
						{
							num = 4;
							continue;
						}
						num = 7;
						continue;
					case 15:
						goto IL_FD;
					case 16:
						num = 0;
						continue;
					case 17:
						goto IL_7D;
					case 18:
						this.\u1737 = null;
						this.ᜊ = paragraphFormat.Sprms.ᜀ();
						num = 5;
						continue;
					}
					break;
					IL_7D:
					num = 11;
					continue;
					IL_FD:
					num = 9;
					continue;
					IL_219:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12D;
					default:
						goto IL_22F;
					}
					IL_1AD:
					num = 1;
					continue;
					IL_1DA:
					this.m_propsUpdateFlags = null;
					num = 10;
				}
			}
			IL_22F:
			if (false)
			{
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00393BB4 File Offset: 0x00392BB4
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 1;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 19;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("⭦hժ࡬㱮ŰቲᙴṶ᝸ᱺ⽼੾", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_43A;
					case 1:
						goto IL_151;
					case 2:
						this.IsWidowControl = reader.ReadBoolean(ClipboardData.b("⹦ᩨ㱪Ѭ୮ṰѲ㙴ᡶ᝸ེོၾ", a_));
						num = 1;
						continue;
					case 3:
						goto IL_527;
					case 4:
						goto IL_558;
					case 5:
						goto IL_738;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("㝦ࡨ౪࡬⵮Ͱᙲᑴᱶ㡸ᵺॼ᩾", a_)))
						{
							num = 18;
							continue;
						}
						goto IL_229;
					case 7:
						goto IL_4BF;
					case 8:
						if (reader.HasAttribute(ClipboardData.b("㝦ࡨ౪࡬⵮Ͱᙲᑴᱶ㭸Ṻ᭼ၾ", a_)))
						{
							num = 42;
							continue;
						}
						goto IL_4F3;
					case 9:
						this.LineSpacingRule = (LineSpacingRule)reader.ReadEnum(ClipboardData.b("⭦hժ࡬㱮ŰቲᙴṶ᝸ᱺ⽼੾", a_), typeof(LineSpacingRule));
						num = 24;
						continue;
					case 10:
						if (reader.HasAttribute(ClipboardData.b("⹦ᩨ㱪Ѭ୮ṰѲ㙴ᡶ᝸ེོၾ", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_151;
					case 11:
						this.IsBidi = reader.ReadBoolean(ClipboardData.b("⹦ᩨ⥪Ѭ୮ᡰ", a_));
						num = 52;
						continue;
					case 12:
						if (reader.HasAttribute(ClipboardData.b("⹦ᩨ⡪ɬͮѰṲ᭴㕶୸Ṻᱼᑾ삀ﮈ", a_)))
						{
							num = 39;
							continue;
						}
						goto IL_58C;
					case 13:
						goto IL_5EA;
					case 14:
						if (reader.HasAttribute(ClipboardData.b("㕦h౪լ᭮㡰ᵲᅴቶ᝸ེ", a_)))
						{
							num = 49;
							continue;
						}
						goto IL_185;
					case 15:
						this.ForeColor = reader.ReadColor(ClipboardData.b("Ⅶ٨ᥪ࡬ⱮṰὲᩴն", a_));
						num = 35;
						continue;
					case 16:
						goto IL_229;
					case 17:
						goto IL_185;
					case 18:
						this.PageBreakAfter = reader.ReadBoolean(ClipboardData.b("㝦ࡨ౪࡬⵮Ͱᙲᑴᱶ㡸ᵺॼ᩾", a_));
						num = 16;
						continue;
					case 19:
						if (reader.HasAttribute(ClipboardData.b("⹦ᩨ⥪Ѭ୮ᡰ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_672;
					case 20:
						if (reader.HasAttribute(ClipboardData.b("⭦౨൪ᥬ♮ὰᝲၴ᥶൸", a_)))
						{
							num = 33;
							continue;
						}
						goto IL_3DF;
					case 21:
						if (reader.HasAttribute(ClipboardData.b("⽦᭨⩪Ŭٮᙰᵲᡴቶ᝸ེ", a_)))
						{
							num = 34;
							continue;
						}
						goto IL_558;
					case 22:
						if (reader.HasAttribute(ClipboardData.b("ⱦ౨๪ᵬ⥮Ṱὲᥴᡶ๸", a_)))
						{
							num = 31;
							continue;
						}
						goto IL_6D0;
					case 23:
						if (reader.HasAttribute(ClipboardData.b("╦ࡨࡪ٬ⱮṰὲᩴն", a_)))
						{
							num = 51;
							continue;
						}
						goto IL_738;
					case 24:
						goto IL_43A;
					case 25:
						goto IL_4F3;
					case 26:
						if (reader.HasAttribute(ClipboardData.b("ⅦhᥪṬ᭮㵰ᩲ᭴ቶへᕺ᥼᩾", a_)))
						{
							num = 36;
							continue;
						}
						goto IL_5EA;
					case 27:
						if (reader.HasAttribute(ClipboardData.b("╦౨൪ɬᵮᑰ⁲մᙶ᩸ቺ፼᡾", a_)))
						{
							num = 30;
							continue;
						}
						goto IL_34D;
					case 28:
						goto IL_34D;
					case 29:
						if (reader.HasAttribute(ClipboardData.b("Ⅶ٨ᥪ࡬ⱮṰὲᩴն", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_76C;
					case 30:
						this.BeforeSpacing = reader.ReadFloat(ClipboardData.b("╦౨൪ɬᵮᑰ⁲մᙶ᩸ቺ፼᡾", a_));
						num = 28;
						continue;
					case 31:
						this.KeepFollow = reader.ReadBoolean(ClipboardData.b("ⱦ౨๪ᵬ⥮Ṱὲᥴᡶ๸", a_));
						num = 38;
						continue;
					case 32:
						goto IL_3DF;
					case 33:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3AB;
						default:
							if (false)
							{
							}
							this.LeftIndent = reader.ReadFloat(ClipboardData.b("⭦౨൪ᥬ♮ὰᝲၴ᥶൸", a_));
							num = 32;
							continue;
						}
						break;
					case 34:
						this.HorizontalAlignment = (HorizontalAlignment)reader.ReadEnum(ClipboardData.b("⽦᭨⩪Ŭٮᙰᵲᡴቶ᝸ེ", a_), typeof(HorizontalAlignment));
						num = 4;
						continue;
					case 35:
						goto IL_76C;
					case 36:
						this.FirstLineIndent = reader.ReadFloat(ClipboardData.b("ⅦhᥪṬ᭮㵰ᩲ᭴ቶへᕺ᥼᩾", a_));
						num = 13;
						continue;
					case 37:
						this.LineSpacing = reader.ReadFloat(ClipboardData.b("⭦hժ࡬㱮ŰቲᙴṶ᝸ᱺ", a_));
						num = 47;
						continue;
					case 38:
						goto IL_6D0;
					case 39:
						this.IsColumnBreakAfter = reader.ReadBoolean(ClipboardData.b("⹦ᩨ⡪ɬͮѰṲ᭴㕶୸Ṻᱼᑾ삀ﮈ", a_));
						num = 41;
						continue;
					case 40:
						this.TextureStyle = (TextureStyle)reader.ReadEnum(ClipboardData.b("㍦౨፪ᥬᩮͰᙲ", a_), typeof(TextureStyle));
						num = 53;
						continue;
					case 41:
						goto IL_58C;
					case 42:
						this.PageBreakBefore = reader.ReadBoolean(ClipboardData.b("㝦ࡨ౪࡬⵮Ͱᙲᑴᱶ㭸Ṻ᭼ၾ", a_));
						num = 25;
						continue;
					case 43:
						if (reader.HasAttribute(ClipboardData.b("ⱦ౨๪ᵬ", a_)))
						{
							num = 48;
							continue;
						}
						goto IL_527;
					case 44:
						if (reader.HasAttribute(ClipboardData.b("⭦hժ࡬㱮ŰቲᙴṶ᝸ᱺ", a_)))
						{
							num = 37;
							continue;
						}
						goto IL_704;
					case 45:
						if (reader.HasAttribute(ClipboardData.b("㍦౨፪ᥬᩮͰᙲ", a_)))
						{
							if (true)
							{
							}
							num = 40;
							continue;
						}
						return;
					case 46:
						if (reader.HasAttribute(ClipboardData.b("♦ཨὪ࡬ᵮ≰Ͳᑴᑶၸᕺ᩼", a_)))
						{
							num = 50;
							continue;
						}
						goto IL_4BF;
					case 47:
						goto IL_704;
					case 48:
						this.Keep = reader.ReadBoolean(ClipboardData.b("ⱦ౨๪ᵬ", a_));
						num = 3;
						continue;
					case 49:
						goto IL_3AB;
					case 50:
						this.AfterSpacing = reader.ReadFloat(ClipboardData.b("♦ཨὪ࡬ᵮ≰Ͳᑴᑶၸᕺ᩼", a_));
						num = 7;
						continue;
					case 51:
						this.BackColor = reader.ReadColor(ClipboardData.b("╦ࡨࡪ٬ⱮṰὲᩴն", a_));
						num = 5;
						continue;
					case 52:
						goto IL_672;
					case 53:
						return;
					}
					break;
					IL_151:
					num = 8;
					continue;
					IL_185:
					num = 26;
					continue;
					IL_229:
					num = 23;
					continue;
					IL_34D:
					num = 46;
					continue;
					IL_3AB:
					this.RightIndent = reader.ReadFloat(ClipboardData.b("㕦h౪լ᭮㡰ᵲᅴቶ᝸ེ", a_));
					num = 17;
					continue;
					IL_3DF:
					num = 14;
					continue;
					IL_43A:
					num = 29;
					continue;
					IL_4BF:
					num = 22;
					continue;
					IL_4F3:
					num = 6;
					continue;
					IL_527:
					num = 27;
					continue;
					IL_558:
					num = 20;
					continue;
					IL_58C:
					num = 44;
					continue;
					IL_5EA:
					num = 43;
					continue;
					IL_672:
					num = 21;
					continue;
					IL_6D0:
					num = 10;
					continue;
					IL_704:
					num = 0;
					continue;
					IL_738:
					num = 12;
					continue;
					IL_76C:
					num = 45;
				}
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x003943A0 File Offset: 0x003933A0
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 8;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 40;
				for (;;)
				{
					Color backColor;
					switch (num)
					{
					case 0:
						if (this.HasValue(6))
						{
							num = 13;
							continue;
						}
						goto IL_63F;
					case 1:
						goto IL_546;
					case 2:
						writer.WriteValue(ClipboardData.b("≭᥯ᱱᅳ╵ࡷ᭹ύ᝽횃", a_), this.LineSpacingRule);
						num = 16;
						continue;
					case 3:
						goto IL_6DD;
					case 4:
						goto IL_63F;
					case 5:
						if (this.HasValue(10))
						{
							num = 34;
							continue;
						}
						goto IL_460;
					case 6:
						writer.WriteValue(ClipboardData.b("❭ͯㅱ᭳᩵൷᝹ቻ㱽즇", a_), this.IsColumnBreakAfter);
						num = 1;
						continue;
					case 7:
						writer.WriteValue(ClipboardData.b("❭ͯぱᵳትᅷ", a_), this.IsBidi);
						num = 14;
						continue;
					case 8:
						writer.WriteValue(ClipboardData.b("⡭᥯qݳɵ㑷፹ቻ᭽쥿ﺉ", a_), this.FirstLineIndent);
						num = 38;
						continue;
					case 9:
						if (this.HasValue(31))
						{
							num = 7;
							continue;
						}
						goto IL_172;
					case 10:
						goto IL_596;
					case 11:
						writer.WriteValue(ClipboardData.b("㹭ᅯᕱᅳ㑵੷όᵻᕽ쉿慎", a_), this.PageBreakBefore);
						num = 3;
						continue;
					case 12:
						writer.WriteValue(ClipboardData.b("❭ͯ╱ᵳት᝷൹㽻ᅽ", a_), this.IsWidowControl);
						num = 55;
						continue;
					case 13:
						writer.WriteValue(ClipboardData.b("╭ᕯ᝱ѳ", a_), this.Keep);
						num = 4;
						continue;
					case 14:
						goto IL_172;
					case 15:
						if (this.HasValue(5))
						{
							num = 8;
							continue;
						}
						goto IL_412;
					case 16:
						goto IL_612;
					case 17:
						goto IL_663;
					case 18:
						if (this.HasValue(52))
						{
							num = 41;
							continue;
						}
						goto IL_3C0;
					case 19:
						if (this.HasValue(3))
						{
							num = 26;
							continue;
						}
						goto IL_51F;
					case 20:
						if (this.HasValue(2))
						{
							num = 31;
							continue;
						}
						goto IL_488;
					case 21:
						goto IL_51F;
					case 22:
						writer.WriteValue(ClipboardData.b("⽭ᙯٱᅳѵ⭷੹ᵻᵽ", a_), this.AfterSpacing);
						num = 24;
						continue;
					case 23:
						if (base.HasKey(22))
						{
							if (true)
							{
							}
							num = 6;
							continue;
						}
						goto IL_546;
					case 24:
						goto IL_5C3;
					case 25:
						if (this.ForeColor != Color.Empty)
						{
							num = 37;
							continue;
						}
						goto IL_663;
					case 26:
						writer.WriteValue(ClipboardData.b("㱭᥯ᕱᱳɵㅷᑹ᡻᭽", a_), this.RightIndent);
						num = 21;
						continue;
					case 27:
						if (this.HasValue(0))
						{
							num = 29;
							continue;
						}
						goto IL_4AC;
					case 28:
						return;
					case 29:
						writer.WriteValue(ClipboardData.b("♭ɯ㍱ᡳήίᑹᅻ᭽", a_), this.HorizontalAlignment);
						num = 39;
						continue;
					case 30:
						if (this.HasValue(9))
						{
							num = 22;
							continue;
						}
						goto IL_5C3;
					case 31:
						writer.WriteValue(ClipboardData.b("≭ᕯᑱs㽵ᙷṹ᥻ၽ", a_), this.LeftIndent);
						num = 54;
						continue;
					case 32:
						if (this.ᜊ != null)
						{
							num = 28;
							continue;
						}
						num = 9;
						continue;
					case 33:
						if (this.HasValue(11))
						{
							num = 12;
							continue;
						}
						goto IL_123;
					case 34:
						writer.WriteValue(ClipboardData.b("╭ᕯ᝱ѳふ᝷ᙹၻᅽ", a_), this.KeepFollow);
						num = 50;
						continue;
					case 35:
						writer.WriteValue(ClipboardData.b("Ɑᅯᅱέ㕵᝷ᙹ፻౽", a_), this.BackColor);
						num = 36;
						continue;
					case 36:
						goto IL_4D0;
					case 37:
						writer.WriteValue(ClipboardData.b("⡭Ὧqᅳ㕵᝷ᙹ፻౽", a_), this.ForeColor);
						num = 17;
						continue;
					case 38:
						goto IL_412;
					case 39:
						goto IL_4AC;
					case 40:
						if (base.HasKey(13))
						{
							num = 51;
							continue;
						}
						goto IL_596;
					case 41:
						writer.WriteValue(ClipboardData.b("≭᥯ᱱᅳ╵ࡷ᭹ύ᝽", a_), this.LineSpacing);
						num = 52;
						continue;
					case 42:
						goto IL_218;
					case 43:
						if (this.HasValue(12))
						{
							num = 11;
							continue;
						}
						goto IL_6DD;
					case 44:
						if (this.TextureStyle != TextureStyle.TextureNone)
						{
							num = 47;
							continue;
						}
						return;
					case 45:
						if (this.HasValue(8))
						{
							num = 46;
							continue;
						}
						goto IL_218;
					case 46:
						writer.WriteValue(ClipboardData.b("Ɑᕯᑱ᭳ѵᵷ⥹౻ώ", a_), this.BeforeSpacing);
						num = 42;
						continue;
					case 47:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11E;
						default:
							if (false)
							{
							}
							writer.WriteValue(ClipboardData.b("㩭ᕯੱs͵੷ό", a_), this.TextureStyle);
							num = 48;
							continue;
						}
						break;
					case 48:
						goto IL_2EE;
					case 49:
						if (!backColor.IsEmpty)
						{
							num = 35;
							continue;
						}
						goto IL_4D0;
					case 50:
						goto IL_460;
					case 51:
						goto IL_11E;
					case 52:
						goto IL_3C0;
					case 53:
						if (this.HasValue(53))
						{
							num = 2;
							continue;
						}
						goto IL_612;
					case 54:
						goto IL_488;
					case 55:
						goto IL_123;
					}
					break;
					IL_11E:
					writer.WriteValue(ClipboardData.b("㹭ᅯᕱᅳ㑵੷όᵻᕽ셿慎", a_), this.PageBreakAfter);
					num = 10;
					continue;
					IL_123:
					num = 43;
					continue;
					IL_172:
					num = 27;
					continue;
					IL_218:
					num = 30;
					continue;
					IL_3C0:
					num = 53;
					continue;
					IL_412:
					num = 0;
					continue;
					IL_460:
					num = 33;
					continue;
					IL_488:
					num = 19;
					continue;
					IL_4AC:
					num = 20;
					continue;
					IL_4D0:
					num = 18;
					continue;
					IL_51F:
					num = 15;
					continue;
					IL_546:
					num = 32;
					continue;
					IL_596:
					num = 23;
					continue;
					IL_5C3:
					num = 5;
					continue;
					IL_612:
					num = 25;
					continue;
					IL_63F:
					num = 45;
					continue;
					IL_663:
					num = 44;
					continue;
					IL_6DD:
					backColor = this.BackColor;
					num = 49;
				}
			}
			IL_2EE:;
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x00394AB8 File Offset: 0x00393AB8
		protected override void WriteXmlContent(IXDLSContentWriter writer)
		{
			int a_ = 18;
			for (;;)
			{
				for (;;)
				{
					base.WriteXmlContent(writer);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							byte[] array = new byte[this.Sprms.ᜇ()];
							this.Sprms.ᜀ(array, 0);
							writer.WriteChildBinaryElement(ClipboardData.b("ᅷᑹࡻ᭽ꖇ揄", a_), array);
							num = 2;
							continue;
						}
						case 1:
							if (this.ᜊ != null)
							{
								num = 0;
								continue;
							}
							return;
						case 2:
							goto IL_7A;
						}
						break;
					}
				}
				IL_7A:
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
			if (true)
			{
			}
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x00394B70 File Offset: 0x00393B70
		protected override bool ReadXmlContent(IXDLSContentReader reader)
		{
			int a_ = 17;
			bool result;
			for (;;)
			{
				result = base.ReadXmlContent(reader);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						for (;;)
						{
							this.\u1737.\u1739().ᜀ(this.ᜊ);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_89;
							}
						}
						IL_89:
						if (false)
						{
						}
						num = 4;
						continue;
					case 1:
					{
						byte[] a_2 = reader.ReadChildBinaryElement();
						this.ᜊ = new sprḍ(a_2);
						result = true;
						num = 2;
						continue;
					}
					case 2:
						if (this.\u1737 != null)
						{
							num = 0;
							continue;
						}
						return result;
					case 3:
						if (reader.TagName == ClipboardData.b("Ṷ᝸ེ᡼ൾꪆ歷", a_))
						{
							num = 1;
							continue;
						}
						return result;
					case 4:
						goto IL_9A;
					}
					break;
				}
			}
			IL_9A:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x00394C64 File Offset: 0x00393C64
		protected override void InitXDLSHolder()
		{
			int a_ = 0;
			for (;;)
			{
				IL_09:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						base.XDLSHolder.AddElement(ClipboardData.b("ѥݧᡩ࡫୭ɯű", a_), this.Borders);
						base.XDLSHolder.AddElement(ClipboardData.b("㉥१ࡩὫ", a_), this.Tabs);
						num = 1;
						continue;
					case 1:
						return;
					}
					if (true)
					{
					}
					if (this.ᜊ != null)
					{
						return;
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
						break;
					}
				}
			}
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x00394D24 File Offset: 0x00393D24
		protected override void OnChange(FormatBase format, int propKey)
		{
			int num = 15;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					if (num2 != -2147483648)
					{
						num = 16;
						continue;
					}
					return;
				case 2:
					goto IL_122;
				case 3:
					if (base.OwnerBase.Document.DocxPackage == null)
					{
						num = 13;
						continue;
					}
					goto IL_180;
				case 4:
					if (base.OwnerBase != null)
					{
						num = 14;
						continue;
					}
					goto IL_180;
				case 5:
					if (!(format is Borders))
					{
						goto IL_D5;
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
						num = 6;
						continue;
					}
					break;
				case 6:
					goto IL_F9;
				case 7:
					num = 3;
					continue;
				case 8:
					if (base.OwnerBase.Document.DetectedFormatType != FileFormat.Rtf)
					{
						num = 11;
						continue;
					}
					goto IL_180;
				case 9:
					goto IL_D5;
				case 10:
					if (base.OwnerBase.Document.ᜇ)
					{
						num = 7;
						continue;
					}
					goto IL_180;
				case 11:
					return;
				case 12:
					if (!(format is Border))
					{
						num = 0;
						continue;
					}
					goto IL_F9;
				case 13:
					num = 8;
					continue;
				case 14:
					num = 10;
					continue;
				case 16:
					this.ᜀ(num2, base[num2]);
					num = 2;
					continue;
				case 17:
					return;
				}
				IL_58:
				if (this.\u1738)
				{
					if (true)
					{
					}
					num = 17;
					continue;
				}
				num = 4;
				continue;
				goto IL_58;
				IL_D5:
				num = 1;
				continue;
				IL_F9:
				num2 = 20;
				num = 9;
				continue;
				IL_180:
				num2 = int.MinValue;
				num = 12;
			}
			return;
			IL_122:;
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00394F14 File Offset: 0x00393F14
		internal override void AcceptChanges()
		{
			for (;;)
			{
				base[65] = false;
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
							return;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						if (this.ᜊ.ᜇ() > 0)
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						if (this.ᜊ != null)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						this.ᜊ.ᜆ(50751);
						this.ᜊ.ᜆ(50799);
						base.AcceptChanges();
						num = 1;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x00394FEC File Offset: 0x00393FEC
		internal override void RemovePositioning()
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
						return;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 1:
					this.ᜊ.ᜆ(9755);
					this.ᜊ.ᜆ(9251);
					this.ᜊ.ᜆ(33816);
					this.ᜊ.ᜆ(33817);
					this.ᜊ.ᜆ(33839);
					this.ᜊ.ᜆ(17954);
					this.ᜊ.ᜆ(33838);
					this.ᜊ.ᜆ(33818);
					this.ᜊ.ᜆ(17451);
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				case 4:
					if (this.ᜊ.ᜈ() > 0)
					{
						num = 1;
						continue;
					}
					return;
				}
				if (this.ᜊ == null)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x0039512C File Offset: 0x0039412C
		internal override void ApplyBase(FormatBase baseFormat)
		{
			for (;;)
			{
				IL_40:
				base.ApplyBase(baseFormat);
				this.Borders.ApplyBase((baseFormat as ParagraphFormat).Borders);
				int num = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
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
						{
							sprᨽ sprᨽ;
							if (sprᨽ == null)
							{
								num = 4;
								continue;
							}
							this.ParaProps.ᜀ(sprᨽ);
							num = 1;
							continue;
						}
						case 1:
							goto IL_9D;
						case 2:
							if (base.Document.ᜇ)
							{
								num = 3;
								continue;
							}
							return;
						case 3:
						{
							sprᨽ sprᨽ = (baseFormat as ParagraphFormat).ParaProps;
							num = 0;
							continue;
						}
						case 4:
							return;
						}
						goto IL_40;
					}
				}
			}
			IL_9D:;
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x00395200 File Offset: 0x00394200
		internal override bool HasValue(int propertyKey)
		{
			for (;;)
			{
				this.ᜆ(propertyKey);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 7;
						continue;
					case 1:
						goto IL_74;
					case 2:
					{
						spr\u1CC1 spr_u1CC;
						if (spr_u1CC == null)
						{
							num = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7E;
						default:
							goto IL_114;
						}
						break;
					}
					case 3:
						if (base.HasKey(propertyKey))
						{
							num = 6;
							continue;
						}
						goto IL_7E;
					case 4:
						goto IL_F2;
					case 5:
					{
						int sprmOption;
						if (sprmOption == 2147483647)
						{
							num = 1;
							continue;
						}
						spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(sprmOption);
						num = 2;
						continue;
					}
					case 6:
						return true;
					case 7:
					{
						if (this.ᜊ.ᜈ() == 0)
						{
							num = 4;
							continue;
						}
						int sprmOption = this.GetSprmOption(propertyKey);
						num = 5;
						continue;
					}
					case 8:
						if (this.ᜊ != null)
						{
							num = 0;
							continue;
						}
						return false;
					case 9:
						return false;
					}
					break;
					IL_7E:
					num = 8;
				}
			}
			return true;
			IL_74:
			if (true)
			{
			}
			return false;
			IL_F2:
			return false;
			IL_114:
			if (false)
			{
			}
			return true;
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x00395328 File Offset: 0x00394328
		internal bool ᜈ(int A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜊ != null)
					{
						num = 9;
						continue;
					}
					return false;
				case 1:
					return true;
				case 3:
					return false;
				case 4:
				{
					spr\u1CC1 spr_u1CC;
					if (spr_u1CC == null)
					{
						num = 3;
						continue;
					}
					return false;
				}
				case 5:
					goto IL_10E;
				case 6:
				{
					int sprmOption;
					if (sprmOption == 2147483647)
					{
						if (true)
						{
						}
						num = 7;
						continue;
					}
					spr\u1CC1 spr_u1CC = this.ᜊ.ᜇ(sprmOption);
					num = 4;
					continue;
				}
				case 7:
					return false;
				case 8:
				{
					if (this.ᜊ.ᜈ() == 0)
					{
						num = 5;
						continue;
					}
					int sprmOption = this.GetSprmOption(A_0);
					num = 6;
					continue;
				}
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				}
				if (base.ᜉ(A_0))
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return true;
			IL_10E:
			return false;
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x00395448 File Offset: 0x00394448
		internal bool ᜇ(int A_0)
		{
			for (;;)
			{
				bool flag = this.ᜈ(A_0);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (base.BaseFormat != null)
						{
							num = 4;
							continue;
						}
						return flag;
					case 2:
						if (!flag)
						{
							num = 0;
							continue;
						}
						return flag;
					case 3:
						if (this.ParaProps.ᜪ().ᜂ(A_0))
						{
							num = 6;
							continue;
						}
						return flag;
					case 4:
					{
						if (true)
						{
						}
						int sprmOption = this.GetSprmOption(A_0);
						this.ParaProps.ᜀ((base.BaseFormat as ParagraphFormat).ParaProps);
						goto IL_11E;
					}
					case 5:
					{
						bool flag2;
						if (flag2)
						{
							num = 8;
							continue;
						}
						return flag;
					}
					case 6:
					{
						int sprmOption;
						bool flag2 = this.ParaProps.ᜪ().ᜇ(sprmOption).ᜉ();
						num = 9;
						continue;
					}
					case 7:
						num = 5;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11E;
						default:
							goto IL_7F;
						}
						break;
					case 9:
					{
						int sprmOption;
						if (this.ParaProps.ᜄ(sprmOption))
						{
							num = 7;
							continue;
						}
						return flag;
					}
					}
					break;
					IL_11E:
					num = 3;
				}
			}
			IL_7F:
			if (false)
			{
			}
			return true;
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x003955A4 File Offset: 0x003945A4
		internal bool ᜄ(int A_0)
		{
			bool flag;
			for (;;)
			{
				flag = this.HasValue(A_0);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int sprmOption;
						if (this.ParaProps.ᜄ(sprmOption))
						{
							num = 3;
							continue;
						}
						goto IL_AA;
					}
					case 1:
						num = 4;
						continue;
					case 2:
						if (!flag)
						{
							num = 1;
							continue;
						}
						goto IL_AA;
					case 3:
						goto IL_80;
					case 4:
						if (base.BaseFormat != null)
						{
							num = 5;
							continue;
						}
						goto IL_AA;
					case 5:
					{
						int sprmOption = this.GetSprmOption(A_0);
						this.ParaProps.ᜀ((base.BaseFormat as ParagraphFormat).ParaProps);
						goto IL_62;
					}
					}
					break;
					IL_62:
					num = 0;
					continue;
					IL_AA:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
						goto IL_C0;
					}
				}
			}
			IL_80:
			if (true)
			{
			}
			return true;
			IL_C0:
			if (false)
			{
			}
			return flag;
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x00395684 File Offset: 0x00394684
		internal bool ᜰ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.\u1737.ᜪ().ᜇ(50765) == null)
					{
						num = 5;
						continue;
					}
					return true;
				case 2:
					num = 1;
					continue;
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
						if (this.\u1737.ᜪ().ᜇ(17453) != null)
						{
							num = 4;
							continue;
						}
						return false;
					}
					break;
				case 4:
					goto IL_97;
				case 5:
					goto IL_47;
				}
				if (true)
				{
				}
				if (this.\u1737 != null)
				{
					num = 2;
					continue;
				}
				return false;
				IL_47:
				num = 3;
			}
			return true;
			IL_97:
			return true;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00395760 File Offset: 0x00394760
		protected override int GetSprmOption(int propertyKey)
		{
			for (;;)
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (propertyKey)
						{
						case 31:
							return 9281;
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
						case 50:
						case 51:
						case 65:
						case 75:
						case 79:
							return int.MaxValue;
						case 52:
						case 53:
							return 25618;
						case 54:
							return 9307;
						case 55:
							return 9308;
						case 56:
							return 9792;
						case 57:
							return 25637;
						case 58:
							return 25639;
						case 59:
							return 25636;
						case 60:
							return 25638;
						case 61:
							return 50767;
						case 62:
							return 50769;
						case 63:
							return 50766;
						case 64:
							return 50768;
						case 66:
							return 25640;
						case 67:
							return 26153;
						case 68:
							return 33886;
						case 69:
							return 33885;
						case 70:
							return 33888;
						case 71:
							return 9325;
						case 72:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_23F;
							default:
								goto IL_A6;
							}
							break;
						case 73:
							return 33816;
						case 74:
							return 33817;
						case 76:
							return 33818;
						case 77:
							return 17451;
						case 78:
							return 9258;
						case 80:
							return 9288;
						case 81:
							return 9271;
						case 82:
							return 9272;
						case 83:
							return 33839;
						case 84:
							return 33838;
						case 85:
							return 17494;
						case 86:
							return 17495;
						case 87:
							return 17493;
						default:
							num = 3;
							continue;
						}
						break;
					case 1:
						switch (propertyKey)
						{
						case 0:
							return 9219;
						case 1:
						case 4:
						case 7:
							return int.MaxValue;
						case 2:
							return 33807;
						case 3:
							return 33806;
						case 5:
							return 33809;
						case 6:
							return 9221;
						case 8:
							return 42003;
						case 9:
							return 42004;
						case 10:
							return 9222;
						case 11:
							return 9265;
						case 12:
							goto IL_82;
						default:
							num = 4;
							continue;
						}
						break;
					case 2:
						goto IL_24A;
					case 3:
						goto IL_23F;
					case 4:
						num = 0;
						continue;
					}
					break;
					IL_23F:
					num = 2;
				}
			}
			return 33816;
			IL_82:
			if (true)
			{
			}
			return 9223;
			IL_A6:
			if (false)
			{
			}
			return 9755;
			IL_24A:
			return int.MaxValue;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00395A24 File Offset: 0x00394A24
		private void ᜂ(int A_0)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_1B8;
				case 2:
					goto IL_7C8;
				case 3:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 51;
						continue;
					}
					return;
				case 4:
					goto IL_592;
				case 5:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 14;
						continue;
					}
					return;
				case 6:
					goto IL_8AF;
				case 7:
					base[56] = this.\u1737.ᜭ();
					num = 46;
					continue;
				case 8:
					return;
				case 9:
					switch (A_0)
					{
					case 52:
					case 53:
						goto IL_322;
					case 54:
						num = 44;
						continue;
					case 55:
						num = 48;
						continue;
					case 56:
						num = 54;
						continue;
					default:
						num = 26;
						continue;
					}
					break;
				case 10:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 16;
						continue;
					}
					return;
				case 11:
					goto IL_385;
				case 12:
					goto IL_7D8;
				case 13:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 22;
						continue;
					}
					return;
				case 14:
					goto IL_53B;
				case 15:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 31;
						continue;
					}
					return;
				case 16:
					goto IL_1FE;
				case 17:
					goto IL_9F4;
				case 18:
					goto IL_986;
				case 19:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 41;
						continue;
					}
					return;
				case 20:
					goto IL_4B0;
				case 21:
					this.ParaProps.ᜀ((base.BaseFormat as ParagraphFormat).ParaProps);
					num = 20;
					continue;
				case 22:
					goto IL_9BD;
				case 23:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 35;
						continue;
					}
					return;
				case 24:
					goto IL_445;
				case 25:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 29;
						continue;
					}
					return;
				case 26:
					num = 42;
					continue;
				case 27:
					goto IL_181;
				case 28:
					goto IL_235;
				case 29:
					goto IL_507;
				case 30:
					switch (A_0)
					{
					case 0:
						goto IL_597;
					case 1:
					case 4:
					case 7:
					case 13:
					case 14:
					case 15:
					case 16:
					case 17:
					case 18:
					case 19:
						return;
					case 2:
						goto IL_1BE;
					case 3:
						goto IL_925;
					case 5:
						goto IL_329;
					case 6:
						num = 33;
						continue;
					case 8:
						num = 37;
						continue;
					case 9:
						num = 39;
						continue;
					case 10:
						num = 53;
						continue;
					case 11:
						num = 15;
						continue;
					case 12:
						num = 40;
						continue;
					case 20:
						goto IL_83F;
					case 21:
						goto IL_697;
					default:
						num = 49;
						continue;
					}
					break;
				case 31:
					goto IL_47C;
				case 32:
					goto IL_878;
				case 33:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 32;
						continue;
					}
					return;
				case 34:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 18;
						continue;
					}
					return;
				case 35:
					goto IL_73B;
				case 36:
					switch (A_0)
					{
					case 30:
						goto IL_1C5;
					case 31:
						num = 19;
						continue;
					case 32:
					case 33:
						goto IL_697;
					default:
						num = 8;
						continue;
					}
					break;
				case 37:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 0;
						continue;
					}
					return;
				case 38:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 17;
						continue;
					}
					return;
				case 39:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 2;
						continue;
					}
					return;
				case 40:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 28;
						continue;
					}
					return;
				case 41:
					goto IL_704;
				case 42:
					switch (A_0)
					{
					case 65:
						num = 34;
						continue;
					case 66:
					case 67:
					case 72:
					case 73:
					case 74:
					case 75:
					case 76:
					case 77:
					case 79:
					case 83:
					case 84:
						return;
					case 68:
						num = 38;
						continue;
					case 69:
						num = 12;
						continue;
					case 70:
						num = 23;
						continue;
					case 71:
						num = 25;
						continue;
					case 78:
						num = 13;
						continue;
					case 80:
						num = 5;
						continue;
					case 81:
						num = 47;
						continue;
					case 82:
						num = 57;
						continue;
					case 85:
						num = 56;
						continue;
					case 86:
						num = 10;
						continue;
					case 87:
						num = 3;
						continue;
					default:
						num = 43;
						continue;
					}
					break;
				case 43:
					return;
				case 44:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 11;
						continue;
					}
					return;
				case 45:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7D8;
					default:
						goto IL_815;
					}
					break;
				case 46:
					goto IL_A1C;
				case 47:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 24;
						continue;
					}
					return;
				case 48:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 4;
						continue;
					}
					return;
				case 49:
					num = 36;
					continue;
				case 50:
					num = 30;
					continue;
				case 51:
					goto IL_3BC;
				case 52:
					goto IL_6D0;
				case 53:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 27;
						continue;
					}
					return;
				case 54:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 7;
						continue;
					}
					return;
				case 55:
					if (A_0 <= 33)
					{
						num = 50;
						continue;
					}
					num = 9;
					continue;
				case 56:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 6;
						continue;
					}
					return;
				case 57:
					if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) != null)
					{
						num = 52;
						continue;
					}
					return;
				}
				if (base.BaseFormat != null)
				{
					num = 21;
					continue;
				}
				IL_4B0:
				num = 55;
				continue;
				IL_7D8:
				if (this.\u1737.ᜪ().ᜇ(this.GetSprmOption(A_0)) == null)
				{
					return;
				}
				num = 45;
			}
			IL_181:
			base[10] = this.\u1737.ᜥ();
			return;
			IL_1B8:
			base[8] = (float)this.\u1737.ᝇ() / 20f;
			return;
			IL_1BE:
			this.ᜈ();
			return;
			IL_1C5:
			this.ᜂ();
			return;
			IL_1FE:
			base[86] = (float)this.\u1737.\u175D() / 100f;
			return;
			IL_235:
			base[12] = this.\u1737.\u171E();
			return;
			IL_322:
			this.ᜃ();
			return;
			IL_329:
			this.ᜆ();
			return;
			IL_385:
			base[54] = (this.\u1737.\u1734() == 1);
			return;
			IL_3BC:
			base[87] = (float)this.\u1737.ᜦ() / 100f;
			return;
			IL_445:
			base[81] = this.\u1737.ᝑ();
			return;
			IL_47C:
			base[11] = this.\u1737.ᜬ();
			return;
			IL_507:
			base[71] = this.\u1737.\u1716();
			return;
			IL_53B:
			base[80] = this.\u1737.ᝃ();
			return;
			IL_592:
			base[55] = (this.\u1737.\u173D() == 1);
			return;
			IL_597:
			this.ᜅ();
			return;
			IL_697:
			this.ᜁ();
			return;
			IL_6D0:
			base[82] = this.\u1737.ᜮ();
			return;
			IL_704:
			base[31] = this.\u1737.\u171D();
			return;
			IL_73B:
			base[70] = (float)this.\u1737.ᜌ() / 20f;
			return;
			IL_7C8:
			base[9] = (float)this.\u1737.\u1718() / 20f;
			return;
			IL_815:
			if (false)
			{
			}
			base[69] = (float)this.\u1737.ᝋ() / 20f;
			return;
			IL_83F:
			this.ᜀ();
			return;
			IL_878:
			base[6] = this.\u1737.\u1754();
			return;
			IL_8AF:
			base[85] = (float)this.\u1737.ᝅ() / 100f;
			return;
			IL_925:
			if (true)
			{
			}
			this.ᜇ();
			return;
			IL_986:
			base[65] = this.\u1737.ᜫ();
			return;
			IL_9BD:
			base[78] = this.\u1737.ᝐ();
			return;
			IL_9F4:
			base[68] = (float)this.\u1737.\u1756() / 20f;
			return;
			IL_A1C:;
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00396484 File Offset: 0x00395484
		private void ᜈ()
		{
			for (;;)
			{
				spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(33807);
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
							continue;
						default:
							goto IL_72;
						}
						break;
					case 1:
						if (spr_u1CC != null)
						{
							num = 5;
							continue;
						}
						return;
					case 2:
						if (spr_u1CC == null)
						{
							num = 4;
							continue;
						}
						base[2] = (float)this.\u1737.ᜂ() / 20f;
						num = 6;
						continue;
					case 3:
						if (!this.IsBidi)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						spr_u1CC = this.\u1737.ᜪ().ᜇ(33886);
						if (true)
						{
						}
						num = 1;
						continue;
					case 5:
						num = 3;
						continue;
					case 6:
						return;
					}
					break;
				}
			}
			IL_72:
			if (false)
			{
			}
			base[2] = (float)this.\u1737.\u1756() / 20f;
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x003965AC File Offset: 0x003955AC
		private void ᜇ()
		{
			for (;;)
			{
				spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(33806);
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
							continue;
						default:
							goto IL_7A;
						}
						break;
					case 1:
						spr_u1CC = this.\u1737.ᜪ().ᜇ(33885);
						num = 6;
						continue;
					case 2:
						if (true)
						{
						}
						if (spr_u1CC == null)
						{
							num = 1;
							continue;
						}
						base[3] = (float)this.\u1737.ᜇ() / 20f;
						num = 5;
						continue;
					case 3:
						if (!this.IsBidi)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						num = 3;
						continue;
					case 5:
						return;
					case 6:
						if (spr_u1CC != null)
						{
							num = 4;
							continue;
						}
						return;
					}
					break;
				}
			}
			IL_7A:
			if (false)
			{
			}
			base[3] = (float)this.\u1737.ᝋ() / 20f;
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x003966D4 File Offset: 0x003956D4
		private void ᜆ()
		{
			for (;;)
			{
				IL_40:
				spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(33809);
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
							if (true)
							{
							}
							num = 3;
							continue;
						case 1:
							return;
						case 2:
							if (spr_u1CC == null)
							{
								num = 4;
								continue;
							}
							base[5] = (float)this.\u1737.\u173E() / 20f;
							num = 1;
							continue;
						case 3:
							if (!this.IsBidi)
							{
								num = 6;
								continue;
							}
							return;
						case 4:
							spr_u1CC = this.\u1737.ᜪ().ᜇ(33888);
							num = 5;
							continue;
						case 5:
							goto IL_CE;
						case 6:
							goto IL_AB;
						}
						goto IL_40;
					}
					IL_CE:
					if (spr_u1CC == null)
					{
						return;
					}
					num = 0;
				}
			}
			IL_AB:
			base[5] = (float)this.\u1737.ᜌ() / 20f;
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x003967FC File Offset: 0x003957FC
		private void ᜅ()
		{
			for (;;)
			{
				spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(9219);
				if (true)
				{
				}
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
							continue;
						default:
							goto IL_D7;
						}
						break;
					case 1:
						goto IL_78;
					case 2:
						spr_u1CC = this.\u1737.ᜪ().ᜇ(9313);
						num = 3;
						continue;
					case 3:
						if (spr_u1CC != null)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						if (spr_u1CC == null)
						{
							num = 2;
							continue;
						}
						base[0] = this.\u1737.\u173C();
						num = 1;
						continue;
					}
					break;
				}
			}
			IL_78:
			return;
			IL_D7:
			if (false)
			{
			}
			base[0] = this.\u1737.ᜠ();
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x003968E8 File Offset: 0x003958E8
		private void ᜄ()
		{
			if (true)
			{
			}
			for (;;)
			{
				spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(50751);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9B;
					case 1:
						if (spr_u1CC == null)
						{
							num = 3;
							continue;
						}
						goto IL_9B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A6;
						default:
							goto IL_93;
						}
						break;
					case 3:
						spr_u1CC = this.\u1737.ᜪ().ᜇ(50799);
						num = 0;
						continue;
					case 4:
						goto IL_A6;
					case 5:
						base[65] = this.\u1737.ᜫ();
						num = 2;
						continue;
					}
					break;
					IL_9B:
					num = 4;
					continue;
					IL_A6:
					if (spr_u1CC == null)
					{
						return;
					}
					num = 5;
				}
			}
			IL_93:
			if (false)
			{
			}
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x003969D0 File Offset: 0x003959D0
		private void ᜃ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(25618);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (spr_u1CC == null)
							{
								num = 4;
								continue;
							}
							goto IL_76;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								return;
							default:
							{
								if (false)
								{
								}
								Paragraph paragraph = base.OwnerBase as Paragraph;
								ParagraphStyle paragraphStyle = paragraph.ParaStyle;
								num = 9;
								continue;
							}
							}
							break;
						case 2:
							if (spr_u1CC != null)
							{
								if (true)
								{
								}
								num = 5;
								continue;
							}
							return;
						case 3:
							goto IL_76;
						case 4:
							num = 7;
							continue;
						case 5:
						{
							spr\u20F1 spr_u20F = this.\u1737.ᜃ(spr_u1CC);
							base[52] = (float)spr_u20F.ᜁ() / 20f;
							base[53] = spr_u20F.ᜂ();
							num = 6;
							continue;
						}
						case 6:
							return;
						case 7:
							if (base.OwnerBase is Paragraph)
							{
								num = 1;
								continue;
							}
							goto IL_76;
						case 8:
						{
							ParagraphStyle paragraphStyle;
							this.ᜀ(paragraphStyle, out spr_u1CC);
							num = 3;
							continue;
						}
						case 9:
						{
							ParagraphStyle paragraphStyle;
							if (paragraphStyle != null)
							{
								num = 8;
								continue;
							}
							goto IL_76;
						}
						}
						break;
						IL_76:
						num = 2;
					}
				}
				return;
			}
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00396B50 File Offset: 0x00395B50
		private void ᜂ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = false;
					spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(50701);
					int num = 9;
					for (;;)
					{
						sprΐ sprΐ;
						switch (num)
						{
						case 0:
							if (!flag)
							{
								num = 1;
								continue;
							}
							if (true)
							{
							}
							num = 3;
							continue;
						case 1:
							goto IL_139;
						case 2:
							num = 0;
							continue;
						case 3:
							sprΐ = this.\u1737.ᜈ();
							goto IL_DF;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_139;
							default:
								if (false)
								{
								}
								sprΐ = this.\u1737.\u1737();
								goto IL_DF;
							}
							break;
						case 5:
							spr_u1CC = this.\u1737.ᜪ().ᜇ(50709);
							flag = true;
							num = 8;
							continue;
						case 6:
							return;
						case 7:
							if (spr_u1CC != null)
							{
								num = 2;
								continue;
							}
							return;
						case 8:
							goto IL_119;
						case 9:
							if (spr_u1CC == null)
							{
								num = 5;
								continue;
							}
							goto IL_119;
						}
						break;
						IL_DF:
						sprΐ a_ = sprΐ;
						TabCollection tabCollection = new TabCollection(base.Document, this);
						tabCollection.CancelOnChangeEvent = true;
						spr\u192A.ᜀ(a_, tabCollection);
						base[30] = tabCollection;
						tabCollection.CancelOnChangeEvent = false;
						num = 6;
						continue;
						IL_119:
						num = 7;
						continue;
						IL_139:
						num = 4;
					}
				}
				return;
			}
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x00396CD0 File Offset: 0x00395CD0
		private void ᜁ()
		{
			for (;;)
			{
				spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(50765);
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						spr\u24DB spr_u24DB;
						if (spr_u24DB.ᜁ() != TextureStyle.TextureNone)
						{
							num = 5;
							continue;
						}
						goto IL_1B1;
					}
					case 1:
					{
						spr\u24DB spr_u24DB;
						base[21] = spr_u24DB.ᜂ();
						num = 3;
						continue;
					}
					case 2:
						spr_u1CC = this.\u1737.ᜪ().ᜇ(17453);
						num = 9;
						continue;
					case 3:
						goto IL_91;
					case 4:
						if (spr_u1CC == null)
						{
							goto IL_61;
						}
						goto IL_C3;
					case 5:
					{
						spr\u24DB spr_u24DB;
						base[33] = spr_u24DB.ᜁ();
						num = 8;
						continue;
					}
					case 6:
						if (spr_u1CC != null)
						{
							num = 7;
							continue;
						}
						goto IL_1B1;
					case 7:
					{
						spr\u24DB spr_u24DB = this.\u1737.ᜁ(spr_u1CC);
						num = 10;
						continue;
					}
					case 8:
						goto IL_1B1;
					case 9:
						goto IL_C3;
					case 10:
					{
						spr\u24DB spr_u24DB;
						if (spr_u24DB.ᜂ() != Color.Empty)
						{
							num = 1;
							continue;
						}
						goto IL_91;
					}
					case 11:
						goto IL_12A;
					case 12:
					{
						spr\u24DB spr_u24DB;
						base[32] = spr_u24DB.ᜃ();
						num = 11;
						continue;
					}
					case 13:
					{
						spr\u24DB spr_u24DB;
						if (spr_u24DB.ᜃ() != Color.Empty)
						{
							if (true)
							{
							}
							num = 12;
							continue;
						}
						goto IL_12A;
					}
					}
					break;
					IL_61:
					num = 2;
					continue;
					IL_91:
					num = 13;
					continue;
					IL_C3:
					num = 6;
					continue;
					IL_12A:
					num = 0;
					continue;
					IL_1B1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					default:
						goto IL_1C7;
					}
				}
			}
			IL_1C7:
			if (false)
			{
			}
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x00396EAC File Offset: 0x00395EAC
		private void ᜀ()
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
			this.\u1738 = true;
			this.ᜀ(25636, 50766, BorderSide.Top);
			this.ᜀ(25638, 50768, BorderSide.Bottom);
			this.ᜀ(25639, 50769, BorderSide.Right);
			this.ᜀ(25637, 50767, BorderSide.Left);
			this.ᜀ(25640, 17952, BorderSide.Between);
			this.ᜀ(26153, 17953, BorderSide.Bar);
			this.\u1738 = false;
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x00396F5C File Offset: 0x00395F5C
		private void ᜀ(int A_0, int A_1, BorderSide A_2)
		{
			switch (0)
			{
			default:
			{
				Borders borders;
				spr\u224E a_;
				for (;;)
				{
					borders = null;
					spr\u1CC1 spr_u1CC = this.\u1737.ᜪ().ᜇ(A_1);
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							borders = (base[20] as Borders);
							a_ = this.\u1737.ᜀ(spr_u1CC);
							num = 4;
							continue;
						case 1:
							goto IL_11C;
						case 2:
							if (spr_u1CC == null)
							{
								num = 7;
								continue;
							}
							goto IL_8E;
						case 3:
							goto IL_8E;
						case 4:
							switch (A_2)
							{
							case BorderSide.Top:
								goto IL_174;
							case BorderSide.Bottom:
								goto IL_81;
							case BorderSide.Left:
								goto IL_F7;
							case BorderSide.Right:
								goto IL_74;
							case BorderSide.Between:
								goto IL_AE;
							case BorderSide.Bar:
								spr\u192A.ᜁ(a_, borders.Vertical);
								num = 1;
								continue;
							default:
								num = 5;
								continue;
							}
							break;
						case 5:
							return;
						case 6:
							if (spr_u1CC != null)
							{
								num = 0;
								continue;
							}
							return;
						case 7:
							spr_u1CC = this.\u1737.ᜪ().ᜇ(A_0);
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
						}
						break;
						IL_8E:
						num = 6;
					}
				}
				IL_74:
				spr\u192A.ᜁ(a_, borders.Right);
				return;
				IL_81:
				spr\u192A.ᜁ(a_, borders.Bottom);
				return;
				IL_AE:
				spr\u192A.ᜁ(a_, borders.Horizontal);
				return;
				IL_F7:
				spr\u192A.ᜁ(a_, borders.Left);
				return;
				IL_11C:
				return;
				IL_174:
				spr\u192A.ᜁ(a_, borders.Top);
				return;
			}
			}
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x003970EC File Offset: 0x003960EC
		private void ᜀ(ParagraphStyle A_0, out spr\u1CC1 A_1)
		{
			for (;;)
			{
				A_1 = A_0.ParagraphFormat.ParaProps.ᜪ().ᜇ(25618);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_1 == null)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						this.ᜀ(A_0.BaseStyle, out A_1);
						goto IL_81;
					case 2:
						goto IL_8C;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_81;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 4:
						if (A_0.BaseStyle != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
					IL_81:
					num = 2;
				}
			}
			IL_8C:
			if (true)
			{
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x003971B0 File Offset: 0x003961B0
		internal void ᜃ(ParagraphFormat A_0)
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
			ParagraphFormat paragraphFormat = new ParagraphFormat(A_0.Document);
			paragraphFormat.ImportContainer(this);
			paragraphFormat.ᜃ(this);
			paragraphFormat.ApplyBase(A_0);
			this.ᜁ(paragraphFormat);
			base.ImportContainer(paragraphFormat);
			base.ᜃ(paragraphFormat);
			paragraphFormat.Close();
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00397228 File Offset: 0x00396228
		private void ᜁ(ParagraphFormat A_0)
		{
			int num = 24;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6B0;
				case 1:
					if (A_0.LeftIndentBi != this.LeftIndentBi)
					{
						num = 95;
						continue;
					}
					goto IL_999;
				case 2:
					A_0.ForeColor = this.ForeColor;
					num = 25;
					continue;
				case 3:
					A_0.FirstLineIndentChars = this.FirstLineIndentChars;
					num = 10;
					continue;
				case 4:
					if (A_0.FrameHeight != this.FrameHeight)
					{
						num = 116;
						continue;
					}
					goto IL_BC6;
				case 5:
					A_0.BeforeSpacing = this.BeforeSpacing;
					num = 104;
					continue;
				case 6:
					if (A_0.ForeColor != this.ForeColor)
					{
						num = 2;
						continue;
					}
					goto IL_5A8;
				case 7:
					goto IL_9E1;
				case 8:
					goto IL_D76;
				case 9:
					if (A_0.FrameHorizontalPos != this.FrameHorizontalPos)
					{
						num = 93;
						continue;
					}
					goto IL_635;
				case 10:
					goto IL_CE6;
				case 11:
					if (A_0.IsWidowControl != this.IsWidowControl)
					{
						num = 92;
						continue;
					}
					goto IL_304;
				case 12:
					A_0.AfterSpacing = this.AfterSpacing;
					num = 111;
					continue;
				case 13:
					goto IL_C0E;
				case 14:
					A_0.RightIndentBi = this.RightIndentBi;
					num = 46;
					continue;
				case 15:
					A_0.LineSpacing = this.LineSpacing;
					num = 79;
					continue;
				case 16:
					A_0.LeftIndentChars = this.LeftIndentChars;
					num = 56;
					continue;
				case 17:
					if (A_0.LineSpacingRule != this.LineSpacingRule)
					{
						num = 36;
						continue;
					}
					goto IL_7FC;
				case 18:
					if (A_0.AutoSpaceDN != this.AutoSpaceDN)
					{
						num = 76;
						continue;
					}
					goto IL_B6F;
				case 19:
					if (A_0.FrameHorizontalDistanceFromText != this.FrameHorizontalDistanceFromText)
					{
						num = 78;
						continue;
					}
					goto IL_828;
				case 20:
					goto IL_304;
				case 21:
					A_0.FrameVerticalDistanceFromText = this.FrameVerticalDistanceFromText;
					num = 30;
					continue;
				case 22:
					IL_9A4:
					if (A_0.LineSpacing != this.LineSpacing)
					{
						num = 15;
						continue;
					}
					goto IL_A33;
				case 23:
					goto IL_7FC;
				case 25:
					goto IL_5A8;
				case 26:
					if (A_0.AfterSpacing != this.AfterSpacing)
					{
						num = 12;
						continue;
					}
					goto IL_B9D;
				case 27:
					goto IL_DE5;
				case 28:
					goto IL_414;
				case 29:
					if (A_0.KeepFollow != this.KeepFollow)
					{
						num = 75;
						continue;
					}
					goto IL_AFB;
				case 30:
					goto IL_67A;
				case 31:
					goto IL_734;
				case 32:
					A_0.FrameVerticalPos = this.FrameVerticalPos;
					num = 34;
					continue;
				case 33:
					goto IL_854;
				case 34:
					goto IL_4D7;
				case 35:
					A_0.IsSpacingBeforeAuto = this.IsSpacingBeforeAuto;
					num = 33;
					continue;
				case 36:
					A_0.LineSpacingRule = this.LineSpacingRule;
					num = 23;
					continue;
				case 37:
					if (A_0.RightIndentBi != this.RightIndentBi)
					{
						num = 14;
						continue;
					}
					goto IL_492;
				case 38:
					A_0.FirstLineIndent = this.FirstLineIndent;
					num = 54;
					continue;
				case 39:
					if (A_0.FirstLineIndentBi != this.FirstLineIndentBi)
					{
						num = 121;
						continue;
					}
					goto IL_445;
				case 40:
					if (A_0.FirstLineIndent != this.FirstLineIndent)
					{
						num = 38;
						continue;
					}
					goto IL_760;
				case 41:
					A_0.PageBreakAfter = this.PageBreakAfter;
					num = 53;
					continue;
				case 42:
					if (A_0.FrameY != this.FrameY)
					{
						num = 55;
						continue;
					}
					goto IL_DA2;
				case 43:
					A_0.PageBreakBefore = this.PageBreakBefore;
					num = 50;
					continue;
				case 44:
					goto IL_CBA;
				case 45:
					if (A_0.IsSpacingAfterAuto != this.IsSpacingAfterAuto)
					{
						num = 114;
						continue;
					}
					goto IL_2A0;
				case 46:
					goto IL_492;
				case 47:
					A_0.TextureStyle = this.TextureStyle;
					num = 117;
					continue;
				case 48:
					goto IL_A0A;
				case 49:
					if (A_0.LeftIndentChars != this.LeftIndentChars)
					{
						num = 16;
						continue;
					}
					goto IL_A5F;
				case 50:
					goto IL_87D;
				case 51:
					if (A_0.PageBreakBefore != this.PageBreakBefore)
					{
						num = 43;
						continue;
					}
					goto IL_87D;
				case 52:
					goto IL_248;
				case 53:
					goto IL_C72;
				case 54:
					goto IL_760;
				case 55:
					A_0.FrameY = this.FrameY;
					num = 120;
					continue;
				case 56:
					goto IL_A5F;
				case 57:
					A_0.FrameX = this.FrameX;
					num = 7;
					continue;
				case 58:
					if (A_0.FrameVerticalDistanceFromText != this.FrameVerticalDistanceFromText)
					{
						num = 21;
						continue;
					}
					goto IL_67A;
				case 59:
					if (A_0.WrapFrameAround != this.WrapFrameAround)
					{
						num = 71;
						continue;
					}
					goto IL_734;
				case 60:
					A_0.OutlineLevel = this.OutlineLevel;
					num = 8;
					continue;
				case 61:
					A_0.AdjustRightIndent = this.AdjustRightIndent;
					num = 109;
					continue;
				case 62:
					A_0.LeftIndent = this.LeftIndent;
					num = 52;
					continue;
				case 63:
					goto IL_AFB;
				case 64:
					goto IL_3A0;
				case 65:
					A_0.IsColumnBreakAfter = this.IsColumnBreakAfter;
					num = 96;
					continue;
				case 66:
					if (A_0.TextureStyle != this.TextureStyle)
					{
						num = 47;
						continue;
					}
					goto IL_8C5;
				case 67:
					if (A_0.FrameWidth != this.FrameWidth)
					{
						num = 113;
						continue;
					}
					goto IL_3A0;
				case 68:
					if (A_0.IsSpacingBeforeAuto != this.IsSpacingBeforeAuto)
					{
						num = 35;
						continue;
					}
					goto IL_854;
				case 69:
					if (A_0.SuppressAutoHyphens != this.SuppressAutoHyphens)
					{
						num = 85;
						continue;
					}
					goto IL_6B0;
				case 70:
					if (A_0.IsBidi != this.IsBidi)
					{
						num = 90;
						continue;
					}
					goto IL_414;
				case 71:
					A_0.WrapFrameAround = this.WrapFrameAround;
					num = 31;
					continue;
				case 72:
					A_0.MirrorIndents = this.MirrorIndents;
					num = 44;
					continue;
				case 73:
					if (A_0.IsContextualSpacing != this.IsContextualSpacing)
					{
						num = 84;
						continue;
					}
					goto IL_274;
				case 74:
					A_0.AutoSpaceDE = this.AutoSpaceDE;
					num = 115;
					continue;
				case 75:
					A_0.KeepFollow = this.KeepFollow;
					num = 63;
					continue;
				case 76:
					A_0.AutoSpaceDN = this.AutoSpaceDN;
					num = 124;
					continue;
				case 77:
					A_0.BackColor = this.BackColor;
					num = 80;
					continue;
				case 78:
					A_0.FrameHorizontalDistanceFromText = this.FrameHorizontalDistanceFromText;
					num = 102;
					continue;
				case 79:
					goto IL_A33;
				case 80:
					goto IL_B27;
				case 81:
					if (A_0.FirstLineIndentChars != this.FirstLineIndentChars)
					{
						num = 3;
						continue;
					}
					goto IL_CE6;
				case 82:
					goto IL_274;
				case 83:
					A_0.RightIndentChars = this.RightIndentChars;
					num = 27;
					continue;
				case 84:
					A_0.IsContextualSpacing = this.IsContextualSpacing;
					num = 82;
					continue;
				case 85:
					A_0.SuppressAutoHyphens = this.SuppressAutoHyphens;
					num = 0;
					continue;
				case 86:
					goto IL_445;
				case 87:
					if (A_0.OutlineLevel != this.OutlineLevel)
					{
						num = 60;
						continue;
					}
					goto IL_D76;
				case 88:
					A_0.RightIndent = this.RightIndent;
					num = 13;
					continue;
				case 89:
					if (A_0.MirrorIndents != this.MirrorIndents)
					{
						num = 72;
						continue;
					}
					goto IL_CBA;
				case 90:
					A_0.IsBidi = this.IsBidi;
					num = 28;
					continue;
				case 91:
					if (A_0.PageBreakAfter != this.PageBreakAfter)
					{
						num = 41;
						continue;
					}
					goto IL_C72;
				case 92:
					A_0.IsWidowControl = this.IsWidowControl;
					num = 20;
					continue;
				case 93:
					A_0.FrameHorizontalPos = this.FrameHorizontalPos;
					num = 98;
					continue;
				case 94:
					if (A_0.FrameVerticalPos != this.FrameVerticalPos)
					{
						num = 32;
						continue;
					}
					goto IL_4D7;
				case 95:
					A_0.LeftIndentBi = this.LeftIndentBi;
					num = 110;
					continue;
				case 96:
					goto IL_3E8;
				case 97:
					if (A_0.IsColumnBreakAfter != this.IsColumnBreakAfter)
					{
						if (true)
						{
						}
						num = 65;
						continue;
					}
					goto IL_3E8;
				case 98:
					goto IL_635;
				case 99:
					A_0.HorizontalAlignment = this.HorizontalAlignment;
					num = 48;
					continue;
				case 100:
					if (A_0.BackColor != this.BackColor)
					{
						num = 77;
						continue;
					}
					goto IL_B27;
				case 101:
					goto IL_2A0;
				case 102:
					goto IL_828;
				case 103:
					if (A_0.HorizontalAlignment != this.HorizontalAlignment)
					{
						num = 99;
						continue;
					}
					goto IL_A0A;
				case 104:
					goto IL_6DC;
				case 105:
					if (A_0.FrameX != this.FrameX)
					{
						num = 57;
						continue;
					}
					goto IL_9E1;
				case 106:
					A_0.Keep = this.Keep;
					num = 123;
					continue;
				case 107:
					if (A_0.Keep != this.Keep)
					{
						num = 106;
						continue;
					}
					goto IL_5D1;
				case 108:
					if (A_0.RightIndent != this.RightIndent)
					{
						num = 88;
						continue;
					}
					goto IL_C0E;
				case 109:
					goto IL_708;
				case 110:
					goto IL_999;
				case 111:
					goto IL_B9D;
				case 112:
					if (A_0.LeftIndent != this.LeftIndent)
					{
						num = 62;
						continue;
					}
					goto IL_248;
				case 113:
					A_0.FrameWidth = this.FrameWidth;
					num = 64;
					continue;
				case 114:
					A_0.IsSpacingAfterAuto = this.IsSpacingAfterAuto;
					num = 101;
					continue;
				case 115:
					goto IL_D12;
				case 116:
					A_0.FrameHeight = this.FrameHeight;
					num = 119;
					continue;
				case 117:
					goto IL_8C5;
				case 118:
					if (A_0.AutoSpaceDE != this.AutoSpaceDE)
					{
						num = 74;
						continue;
					}
					goto IL_D12;
				case 119:
					goto IL_BC6;
				case 120:
					goto IL_DA2;
				case 121:
					A_0.FirstLineIndentBi = this.FirstLineIndentBi;
					num = 86;
					continue;
				case 122:
					if (A_0.BeforeSpacing != this.BeforeSpacing)
					{
						num = 5;
						continue;
					}
					goto IL_6DC;
				case 123:
					goto IL_5D1;
				case 124:
					goto IL_B6F;
				case 125:
					if (A_0.RightIndentChars != this.RightIndentChars)
					{
						num = 83;
						continue;
					}
					goto IL_DE7;
				}
				if (A_0.AdjustRightIndent != this.AdjustRightIndent)
				{
					num = 61;
					continue;
				}
				goto IL_708;
				IL_248:
				num = 1;
				continue;
				IL_274:
				num = 40;
				continue;
				IL_2A0:
				num = 68;
				continue;
				IL_304:
				num = 59;
				continue;
				IL_3A0:
				num = 105;
				continue;
				IL_3E8:
				num = 73;
				continue;
				IL_414:
				num = 97;
				continue;
				IL_445:
				num = 6;
				continue;
				IL_492:
				num = 45;
				continue;
				IL_4D7:
				num = 67;
				continue;
				IL_5A8:
				num = 4;
				continue;
				IL_5D1:
				num = 29;
				continue;
				IL_635:
				num = 58;
				continue;
				IL_67A:
				num = 94;
				continue;
				IL_6B0:
				num = 66;
				continue;
				IL_6DC:
				num = 70;
				continue;
				IL_708:
				num = 26;
				continue;
				IL_734:
				num = 49;
				continue;
				IL_760:
				num = 39;
				continue;
				IL_7FC:
				num = 89;
				continue;
				IL_828:
				num = 9;
				continue;
				IL_854:
				num = 69;
				continue;
				IL_87D:
				num = 108;
				continue;
				IL_8C5:
				num = 11;
				continue;
				IL_999:
				num = 22;
				continue;
				IL_C0E:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9A4;
				default:
					if (false)
					{
					}
					num = 37;
					continue;
				}
				IL_9E1:
				num = 42;
				continue;
				IL_A0A:
				num = 107;
				continue;
				IL_A33:
				num = 17;
				continue;
				IL_A5F:
				num = 81;
				continue;
				IL_AFB:
				num = 112;
				continue;
				IL_B27:
				num = 122;
				continue;
				IL_B6F:
				num = 100;
				continue;
				IL_B9D:
				num = 118;
				continue;
				IL_BC6:
				num = 19;
				continue;
				IL_C72:
				num = 51;
				continue;
				IL_CBA:
				num = 87;
				continue;
				IL_CE6:
				num = 125;
				continue;
				IL_D12:
				num = 18;
				continue;
				IL_D76:
				num = 91;
				continue;
				IL_DA2:
				num = 103;
			}
			IL_DE5:
			IL_DE7:
			this.Borders.ᜀ(A_0.Borders);
			this.ᜀ(A_0);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00398034 File Offset: 0x00397034
		private void ᜀ(ParagraphFormat A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 19;
				for (;;)
				{
					int num3;
					switch (num)
					{
					case 0:
						num = 17;
						continue;
					case 1:
						goto IL_34F;
					case 2:
					{
						int num2;
						if (num2 == 3)
						{
							num = 20;
							continue;
						}
						goto IL_BF;
					}
					case 3:
						goto IL_199;
					case 4:
						num = 5;
						continue;
					case 5:
					{
						int num2;
						if ((base.OwnerBase as Paragraph).ListFormat.CurrentListLevel.ParagraphFormat.ᜁ(num2))
						{
							num = 28;
							continue;
						}
						goto IL_20D;
					}
					case 6:
						A_0.ᜀ(68);
						num = 10;
						continue;
					case 7:
						A_0.ᜀ(69);
						num = 18;
						continue;
					case 8:
						return;
					case 9:
						if ((base.OwnerBase as Paragraph).ListFormat.CurrentListLevel != null)
						{
							num = 23;
							continue;
						}
						return;
					case 10:
						goto IL_20D;
					case 11:
					{
						int num2;
						if (num2 == 2)
						{
							num = 0;
							continue;
						}
						goto IL_E3;
					}
					case 12:
						goto IL_20D;
					case 13:
					{
						int num2;
						if (num2 == 5)
						{
							num = 27;
							continue;
						}
						goto IL_20D;
					}
					case 14:
						num = 25;
						continue;
					case 15:
						goto IL_199;
					case 16:
					{
						int[] array;
						if (num3 >= array.Length)
						{
							num = 8;
							continue;
						}
						int num2 = array[num3];
						num = 21;
						continue;
					}
					case 17:
						if (true)
						{
						}
						if (!base.PropertiesHash.ContainsKey(68))
						{
							num = 6;
							continue;
						}
						goto IL_E3;
					case 18:
						goto IL_20D;
					case 20:
						num = 1;
						continue;
					case 21:
					{
						int num2;
						if (!base.PropertiesHash.ContainsKey(num2))
						{
							num = 4;
							continue;
						}
						goto IL_20D;
					}
					case 22:
						A_0.ᜀ(70);
						num = 12;
						continue;
					case 23:
					{
						int[] array2 = new int[A_0.PropertiesHash.Count];
						A_0.PropertiesHash.Keys.CopyTo(array2, 0);
						int[] array = array2;
						num3 = 0;
						num = 3;
						continue;
					}
					case 24:
						num = 9;
						continue;
					case 25:
						if ((base.OwnerBase as Paragraph).ListFormat.ListType != ListType.NoList)
						{
							num = 24;
							continue;
						}
						return;
					case 26:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34F;
						default:
							if (false)
							{
							}
							if (!base.PropertiesHash.ContainsKey(70))
							{
								num = 22;
								continue;
							}
							goto IL_20D;
						}
						break;
					case 27:
						num = 26;
						continue;
					case 28:
					{
						int num2;
						A_0.ᜀ(num2);
						num = 11;
						continue;
					}
					}
					if (base.OwnerBase is Paragraph)
					{
						num = 14;
						continue;
					}
					break;
					IL_BF:
					num = 13;
					continue;
					IL_34F:
					if (!base.PropertiesHash.ContainsKey(69))
					{
						num = 7;
						continue;
					}
					goto IL_BF;
					IL_E3:
					num = 2;
					continue;
					IL_199:
					num = 16;
					continue;
					IL_20D:
					num3++;
					num = 15;
				}
				return;
			}
			}
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x003983B4 File Offset: 0x003973B4
		private bool ᜁ(int A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 > 255)
					{
						A_0 >>= 8;
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
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_68;
				}
				IL_22:
				if (true)
				{
				}
				num = 0;
				continue;
				goto IL_22;
			}
			IL_68:
			return this.HasValue(A_0);
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00398444 File Offset: 0x00397444
		private void ᜀ(int A_0)
		{
			if (true)
			{
			}
			for (;;)
			{
				base.PropertiesHash.Remove(A_0);
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
							goto IL_8E;
						default:
							if (false)
							{
							}
							if (A_0 <= 255)
							{
								num = 8;
								continue;
							}
							A_0 >>= 8;
							num = 7;
							continue;
						}
						break;
					case 1:
						this.Sprms.ᜆ(this.GetSprmOption(A_0));
						num = 6;
						continue;
					case 2:
						goto IL_CC;
					case 3:
						if (this.Sprms != null)
						{
							goto IL_8E;
						}
						return;
					case 4:
						if (this.Sprms.ᜇ(this.GetSprmOption(A_0)) != null)
						{
							num = 1;
							continue;
						}
						return;
					case 5:
						num = 4;
						continue;
					case 6:
						return;
					case 7:
						goto IL_CC;
					case 8:
						num = 3;
						continue;
					}
					break;
					IL_8E:
					num = 5;
					continue;
					IL_CC:
					num = 0;
				}
			}
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x0039855C File Offset: 0x0039755C
		internal void ᜀ(ParagraphFormat A_0, IParagraphStyle A_1)
		{
			int num = 31;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_32E;
				case 1:
					if (A_1.ParagraphFormat.HasValue(8))
					{
						num = 29;
						continue;
					}
					goto IL_26E;
				case 2:
					if (A_1 != null)
					{
						num = 23;
						continue;
					}
					goto IL_26E;
				case 3:
					goto IL_134;
				case 4:
					goto IL_183;
				case 5:
					goto IL_356;
				case 6:
					num = 14;
					continue;
				case 7:
					if (A_1 != null)
					{
						num = 32;
						continue;
					}
					goto IL_30F;
				case 8:
					num = 17;
					continue;
				case 9:
					num = 33;
					continue;
				case 10:
					num = 25;
					continue;
				case 11:
					goto IL_15B;
				case 12:
					if (!A_0.HasValue(52))
					{
						num = 10;
						continue;
					}
					goto IL_F5;
				case 13:
					goto IL_134;
				case 14:
					if (A_1.ParagraphFormat.HasValue(9))
					{
						num = 4;
						continue;
					}
					goto IL_2A7;
				case 15:
					if (!A_0.HasValue(8))
					{
						num = 19;
						continue;
					}
					goto IL_2F0;
				case 16:
					if (!A_0.HasValue(21))
					{
						num = 37;
						continue;
					}
					goto IL_3E1;
				case 17:
					goto IL_294;
				case 18:
					if (A_1.ParagraphFormat.HasValue(52))
					{
						num = 28;
						continue;
					}
					goto IL_399;
				case 19:
					num = 2;
					continue;
				case 20:
					goto IL_356;
				case 21:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_294;
					default:
						if (false)
						{
						}
						goto IL_32E;
					}
					break;
				case 22:
					goto IL_1EC;
				case 23:
					num = 1;
					continue;
				case 24:
					goto IL_15B;
				case 25:
					if (A_1 != null)
					{
						num = 36;
						continue;
					}
					goto IL_399;
				case 26:
					if (A_1.ParagraphFormat.HasValue(53))
					{
						num = 22;
						continue;
					}
					goto IL_407;
				case 27:
					num = 26;
					continue;
				case 28:
					goto IL_F5;
				case 29:
					if (true)
					{
					}
					goto IL_2F0;
				case 30:
					if (!A_0.HasValue(53))
					{
						num = 9;
						continue;
					}
					goto IL_25A;
				case 32:
					num = 35;
					continue;
				case 33:
					if (A_1 != null)
					{
						num = 27;
						continue;
					}
					goto IL_407;
				case 34:
					goto IL_3E1;
				case 35:
					if (A_1.ParagraphFormat.HasValue(21))
					{
						num = 34;
						continue;
					}
					goto IL_30F;
				case 36:
					num = 18;
					continue;
				case 37:
					num = 7;
					continue;
				}
				if (!A_0.HasValue(9))
				{
					num = 8;
					continue;
				}
				goto IL_183;
				IL_F5:
				base[52] = A_0.LineSpacing;
				num = 21;
				continue;
				IL_134:
				num = 15;
				continue;
				IL_15B:
				num = 30;
				continue;
				IL_183:
				base[9] = A_0.AfterSpacing;
				num = 13;
				continue;
				IL_26E:
				this.BeforeSpacing = 0f;
				num = 5;
				continue;
				IL_294:
				if (A_1 != null)
				{
					num = 6;
					continue;
				}
				IL_2A7:
				this.AfterSpacing = 0f;
				num = 3;
				continue;
				IL_2F0:
				base[8] = A_0.BeforeSpacing;
				num = 20;
				continue;
				IL_30F:
				this.BackColor = default(Color);
				num = 11;
				continue;
				IL_32E:
				num = 16;
				continue;
				IL_356:
				num = 12;
				continue;
				IL_399:
				this.LineSpacing = 0f;
				num = 0;
				continue;
				IL_3E1:
				this.BackColor = A_0.BackColor;
				num = 24;
			}
			IL_1EC:
			IL_25A:
			base[53] = A_0.LineSpacingRule;
			return;
			IL_407:
			this.LineSpacingRule = LineSpacingRule.Multiple;
		}

		// Token: 0x04002D23 RID: 11555
		internal new const short ᜀ = 0;

		// Token: 0x04002D24 RID: 11556
		internal const short ᜁ = 2;

		// Token: 0x04002D25 RID: 11557
		private bool \u25D8\u00A3\u0087\u00AC;

		// Token: 0x04002D26 RID: 11558
		internal new const short ᜂ = 68;

		// Token: 0x04002D27 RID: 11559
		internal new const short ᜃ = 3;

		// Token: 0x04002D28 RID: 11560
		internal new const short ᜄ = 69;

		// Token: 0x04002D29 RID: 11561
		internal const short ᜅ = 5;

		// Token: 0x04002D2A RID: 11562
		internal const short ᜆ = 70;

		// Token: 0x04002D2B RID: 11563
		internal const short ᜇ = 6;

		// Token: 0x04002D2C RID: 11564
		internal const short ᜈ = 8;

		// Token: 0x04002D2D RID: 11565
		internal new const short ᜉ = 9;

		// Token: 0x04002D2E RID: 11566
		internal new const short ᜊ = 10;

		// Token: 0x04002D2F RID: 11567
		internal const short ᜋ = 11;

		// Token: 0x04002D30 RID: 11568
		internal const short ᜌ = 12;

		// Token: 0x04002D31 RID: 11569
		internal const short \u170D = 13;

		// Token: 0x04002D32 RID: 11570
		internal const short ᜎ = 20;

		// Token: 0x04002D33 RID: 11571
		internal const short ᜏ = 21;

		// Token: 0x04002D34 RID: 11572
		internal const short ᜐ = 22;

		// Token: 0x04002D35 RID: 11573
		internal const short ᜑ = 30;

		// Token: 0x04002D36 RID: 11574
		internal const short \u1712 = 31;

		// Token: 0x04002D37 RID: 11575
		internal const short \u1713 = 32;

		// Token: 0x04002D38 RID: 11576
		internal const short \u1714 = 33;

		// Token: 0x04002D39 RID: 11577
		internal const short \u1715 = 50;

		// Token: 0x04002D3A RID: 11578
		internal const short \u1716 = 80;

		// Token: 0x04002D3B RID: 11579
		internal const short \u1717 = 81;

		// Token: 0x04002D3C RID: 11580
		internal const short \u1718 = 82;

		// Token: 0x04002D3D RID: 11581
		private long[] \u2609\u0082\u0091\u0099;

		// Token: 0x04002D3E RID: 11582
		internal const short \u1719 = 52;

		// Token: 0x04002D3F RID: 11583
		internal const short \u171A = 53;

		// Token: 0x04002D40 RID: 11584
		private int[] \u25D9\u00A2\u00AB\u009E;

		// Token: 0x04002D41 RID: 11585
		internal const short \u171B = 54;

		// Token: 0x04002D42 RID: 11586
		internal const short \u171C = 55;

		// Token: 0x04002D43 RID: 11587
		internal const short \u171D = 56;

		// Token: 0x04002D44 RID: 11588
		internal const short \u171E = 57;

		// Token: 0x04002D45 RID: 11589
		internal const short \u171F = 58;

		// Token: 0x04002D46 RID: 11590
		internal const short ᜠ = 59;

		// Token: 0x04002D47 RID: 11591
		internal const short ᜡ = 60;

		// Token: 0x04002D48 RID: 11592
		internal const short ᜢ = 61;

		// Token: 0x04002D49 RID: 11593
		internal const short ᜣ = 62;

		// Token: 0x04002D4A RID: 11594
		internal const short ᜤ = 63;

		// Token: 0x04002D4B RID: 11595
		internal const short ᜥ = 64;

		// Token: 0x04002D4C RID: 11596
		internal const short ᜦ = 65;

		// Token: 0x04002D4D RID: 11597
		internal const short ᜧ = 66;

		// Token: 0x04002D4E RID: 11598
		internal const short ᜨ = 67;

		// Token: 0x04002D4F RID: 11599
		internal const short ᜩ = 71;

		// Token: 0x04002D50 RID: 11600
		internal const short ᜪ = 72;

		// Token: 0x04002D51 RID: 11601
		internal const short ᜫ = 73;

		// Token: 0x04002D52 RID: 11602
		internal const short ᜬ = 74;

		// Token: 0x04002D53 RID: 11603
		internal const short ᜭ = 76;

		// Token: 0x04002D54 RID: 11604
		internal const short ᜮ = 77;

		// Token: 0x04002D55 RID: 11605
		internal const short ᜯ = 83;

		// Token: 0x04002D56 RID: 11606
		internal const short ᜰ = 84;

		// Token: 0x04002D57 RID: 11607
		internal const short ᜱ = 78;

		// Token: 0x04002D58 RID: 11608
		internal const short \u1732 = 75;

		// Token: 0x04002D59 RID: 11609
		internal const int \u1733 = 85;

		// Token: 0x04002D5A RID: 11610
		internal const int \u1734 = 86;

		// Token: 0x04002D5B RID: 11611
		internal const int \u1735 = 87;

		// Token: 0x04002D5C RID: 11612
		private ParagraphFormat \u1736;

		// Token: 0x04002D5D RID: 11613
		private sprᨽ \u1737;

		// Token: 0x04002D5E RID: 11614
		private bool \u1738;

		// Token: 0x04002D5F RID: 11615
		private new bool \u1739;

		// Token: 0x04002D60 RID: 11616
		private bool \u173A;

		// Token: 0x04002D61 RID: 11617
		private int[] \u173B = new int[]
		{
			25636,
			50766,
			25638,
			50768,
			25639,
			50769,
			25637,
			50767,
			25640,
			17952,
			26153,
			17953
		};

		// Token: 0x04002D62 RID: 11618
		private Color \u173C;
	}
}
