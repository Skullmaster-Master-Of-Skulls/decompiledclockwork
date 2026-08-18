using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;

namespace Spire.DataExport.RTF
{
	// Token: 0x02000177 RID: 375
	public class RTFOptions : ICloneable
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x00065510 File Offset: 0x00064510
		public RTFOptions(object Holder)
		{
			int a_ = 11;
			base..ctor();
			this.ᜀ = Holder;
			this.ᜁ = new StringListCollection();
			this.ᜃ = new RTFStyle();
			this.ᜂ = (this.ᜃ.Clone() as RTFStyle);
			this.ᜂ.Font = new Font(HyperlinksCollectionEditor.b("昦嬨䈪䰬䌮", a_), 10f, FontStyle.Bold);
			this.ᜂ.Alignment = RtfTextAlignment.Center;
			this.ᜄ = new RTFStyles(this);
			this.ᜅ = new RTFStyle();
			this.ᜆ = new RTFStyle();
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000655B4 File Offset: 0x000645B4
		public object Clone()
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new RTFOptions(this.ᜀ)
			{
				TitleAligns = this.TitleAligns,
				TitleStyle = this.TitleStyle,
				DataStyle = this.DataStyle,
				PageOrientation = this.PageOrientation,
				ItemStyles = this.ItemStyles,
				ItemType = this.ItemType,
				HeaderStyle = this.HeaderStyle,
				FooterStyle = this.FooterStyle
			};
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0006565C File Offset: 0x0006465C
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x000656A0 File Offset: 0x000646A0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", typeof(UITypeEditor))]
		public StringListCollection TitleAligns
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
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						return;
					case 2:
						this.ᜁ = value;
						num = 1;
						continue;
					case 3:
						if (true)
						{
						}
						if (value != this.ᜁ)
						{
							num = 2;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00065738 File Offset: 0x00064738
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0006577C File Offset: 0x0006477C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public RTFStyle TitleStyle
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
				return this.ᜂ;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜂ = value;
						num = 0;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 4:
						if (value != this.ᜂ)
						{
							num = 1;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00065814 File Offset: 0x00064814
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00065858 File Offset: 0x00064858
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public RTFStyle DataStyle
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜃ = value;
						num = 2;
						continue;
					case 2:
						return;
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
							num = 4;
							continue;
						}
						break;
					case 4:
						if (value != this.ᜃ)
						{
							num = 0;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					if (true)
					{
					}
					num = 3;
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000658F0 File Offset: 0x000648F0
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x00065934 File Offset: 0x00064934
		[DefaultValue(PageOrientation.Portrait)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public PageOrientation PageOrientation
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜇ = value;
						num = 1;
						continue;
					case 1:
						goto IL_52;
					case 2:
						IL_08:
						break;
					}
					if (true)
					{
					}
					if (value != this.ᜇ)
					{
						num = 0;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000659B0 File Offset: 0x000649B0
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x000659F4 File Offset: 0x000649F4
		[Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RTFStyles ItemStyles
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
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != this.ᜄ)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						if (true)
						{
						}
						break;
					case 2:
						return;
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
							num = 0;
							continue;
						}
						break;
					case 4:
						this.ᜄ = value;
						num = 2;
						continue;
					}
					if (value == null)
					{
						break;
					}
					num = 3;
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00065A8C File Offset: 0x00064A8C
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00065AD0 File Offset: 0x00064AD0
		[DefaultValue(RtfItemType.None)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public RtfItemType ItemType
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
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_08:
						break;
					case 1:
						goto IL_52;
					case 2:
						this.ᜈ = value;
						num = 1;
						continue;
					}
					if (true)
					{
					}
					if (value != this.ᜈ)
					{
						num = 2;
						continue;
					}
					IL_52:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_08;
					default:
						goto IL_68;
					}
				}
				IL_68:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00065B4C File Offset: 0x00064B4C
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00065B90 File Offset: 0x00064B90
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public RTFStyle HeaderStyle
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
						if (value != this.ᜅ)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						this.ᜅ = value;
						if (true)
						{
						}
						num = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					if (value == null)
					{
						break;
					}
					num = 4;
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00065C28 File Offset: 0x00064C28
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00065C6C File Offset: 0x00064C6C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public RTFStyle FooterStyle
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
				return this.ᜆ;
			}
			set
			{
				int num = 4;
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
							continue;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 2:
						this.ᜆ = value;
						num = 0;
						continue;
					case 3:
						if (true)
						{
						}
						if (value != this.ᜆ)
						{
							num = 2;
							continue;
						}
						return;
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x0400077C RID: 1916
		private object ᜀ;

		// Token: 0x0400077D RID: 1917
		private StringListCollection ᜁ;

		// Token: 0x0400077E RID: 1918
		private byte \u25D8\u0086\u00AD\u009C;

		// Token: 0x0400077F RID: 1919
		private byte[] \u2593\u0097\u0098\u0081;

		// Token: 0x04000780 RID: 1920
		private RTFStyle ᜂ;

		// Token: 0x04000781 RID: 1921
		private RTFStyle ᜃ;

		// Token: 0x04000782 RID: 1922
		private bool \u2460\u00A4\u008A\u009E;

		// Token: 0x04000783 RID: 1923
		private RTFStyles ᜄ;

		// Token: 0x04000784 RID: 1924
		private RTFStyle ᜅ;

		// Token: 0x04000785 RID: 1925
		private string \u2609\u0089\u00AC\u0082;

		// Token: 0x04000786 RID: 1926
		private RTFStyle ᜆ;

		// Token: 0x04000787 RID: 1927
		private PageOrientation ᜇ;

		// Token: 0x04000788 RID: 1928
		private RtfItemType ᜈ;
	}
}
