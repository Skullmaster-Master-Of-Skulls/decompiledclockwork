using System;
using System.ComponentModel;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.HTML
{
	// Token: 0x02000180 RID: 384
	public class HtmlExportIndexOption : ICloneable
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x0006DE4C File Offset: 0x0006CE4C
		public HtmlExportIndexOption()
		{
			int a_ = 12;
			this.ᜀ = string.Empty;
			this.ᜁ = NavigationAlign.Bottom;
			this.ᜂ = HyperlinksCollectionEditor.b("愧䐩䠫䬭䠯", a_);
			this.ᜃ = HyperlinksCollectionEditor.b("渧䌩師崭䐯", a_);
			this.ᜄ = HyperlinksCollectionEditor.b("砧堩䔫䄭䈯", a_);
			this.ᜅ = HyperlinksCollectionEditor.b("昧伩含娭", a_);
			this.ᜆ = HyperlinksCollectionEditor.b("搧䬩弫娭", a_);
			base..ctor();
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0006DEE0 File Offset: 0x0006CEE0
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
			HtmlExportIndexOption htmlExportIndexOption = new HtmlExportIndexOption();
			htmlExportIndexOption.LinkTemplate = this.LinkTemplate;
			htmlExportIndexOption.NavigationAlign = htmlExportIndexOption.NavigationAlign;
			htmlExportIndexOption.PageTitle = this.PageTitle;
			htmlExportIndexOption.FirstDisplayCaption = this.FirstDisplayCaption;
			htmlExportIndexOption.PriorDisplayCaption = this.PriorDisplayCaption;
			htmlExportIndexOption.NextDisplayCaption = this.NextDisplayCaption;
			htmlExportIndexOption.LastDisplayCaption = this.LastDisplayCaption;
			return htmlExportIndexOption;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0006DF78 File Offset: 0x0006CF78
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0006DFBC File Offset: 0x0006CFBC
		[Description("Defines the template string for generating links on the index page to other pages in the collection.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		public string LinkTemplate
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
				return this.ᜀ;
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
							goto IL_32;
						default:
							goto IL_63;
						}
						break;
					case 1:
						goto IL_32;
					}
					if (value != this.ᜀ)
					{
						num = 1;
						continue;
					}
					return;
					IL_32:
					this.ᜀ = value;
					num = 0;
				}
				IL_63:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0006E03C File Offset: 0x0006D03C
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0006E080 File Offset: 0x0006D080
		[DefaultValue(NavigationAlign.Bottom)]
		[Description("Defines if there are navigation links (First, Prior, Next, Last) on the bottom of each page in the collection.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public NavigationAlign NavigationAlign
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

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0006E0C4 File Offset: 0x0006D0C4
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0006E108 File Offset: 0x0006D108
		[Description("Defines the caption of the link navigating to the \"home\" (\"Index\") page of the collection.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("Index")]
		public string PageTitle
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
				int num = 1;
				for (;;)
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
						switch (num)
						{
						case 0:
							return;
						case 2:
							this.ᜂ = value;
							num = 0;
							continue;
						}
						if (!(value != this.ᜂ))
						{
							return;
						}
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0006E188 File Offset: 0x0006D188
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x0006E1CC File Offset: 0x0006D1CC
		[DefaultValue("First")]
		[Description("Defines the caption of the link navigating to the first document of the collection.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string FirstDisplayCaption
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
				int num = 2;
				for (;;)
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
						switch (num)
						{
						case 0:
							this.ᜃ = value;
							num = 1;
							continue;
						case 1:
							return;
						}
						if (!(value != this.ᜃ))
						{
							return;
						}
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0006E24C File Offset: 0x0006D24C
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x0006E290 File Offset: 0x0006D290
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("Prior")]
		[Description("Defines the caption of the link navigating to the previous document in the collection.")]
		public string PriorDisplayCaption
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
				if (true)
				{
				}
				int num = 1;
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
						case 2:
							this.ᜄ = value;
							num = 0;
							continue;
						}
						if (!(value != this.ᜄ))
						{
							return;
						}
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0006E310 File Offset: 0x0006D310
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x0006E354 File Offset: 0x0006D354
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Defines the caption of the link navigating to the next document in the collection.")]
		[DefaultValue("Next")]
		public string NextDisplayCaption
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
				int num = 1;
				for (;;)
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
						switch (num)
						{
						case 0:
							return;
						case 2:
							this.ᜅ = value;
							num = 0;
							continue;
						}
						if (!(value != this.ᜅ))
						{
							return;
						}
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0006E3D4 File Offset: 0x0006D3D4
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0006E418 File Offset: 0x0006D418
		[Description("Defines the caption of the link navigating to the last document of the collection.")]
		[DefaultValue("Last")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string LastDisplayCaption
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
				int num = 1;
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
							this.ᜆ = value;
							num = 2;
							continue;
						case 2:
							return;
						}
						if (true)
						{
						}
						if (!(value != this.ᜆ))
						{
							return;
						}
						break;
					}
					num = 0;
				}
			}
		}

		// Token: 0x040007E7 RID: 2023
		private bool \u2460\u00A6\u0086\u0087;

		// Token: 0x040007E8 RID: 2024
		private string ᜀ;

		// Token: 0x040007E9 RID: 2025
		private NavigationAlign ᜁ;

		// Token: 0x040007EA RID: 2026
		private string ᜂ;

		// Token: 0x040007EB RID: 2027
		private float \u25D8\u009B\u0085\u00B0;

		// Token: 0x040007EC RID: 2028
		private byte[] \u2593\u008E\u0091\u009F;

		// Token: 0x040007ED RID: 2029
		private string ᜃ;

		// Token: 0x040007EE RID: 2030
		private byte \u2609\u0085\u00AC\u00A0;

		// Token: 0x040007EF RID: 2031
		private string ᜄ;

		// Token: 0x040007F0 RID: 2032
		private int \u2460\u00A5\u0089\u009A;

		// Token: 0x040007F1 RID: 2033
		private string ᜅ;

		// Token: 0x040007F2 RID: 2034
		private string ᜆ;
	}
}
