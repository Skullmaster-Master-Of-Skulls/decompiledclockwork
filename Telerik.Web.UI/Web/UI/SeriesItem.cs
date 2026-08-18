using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI
{
	// Token: 0x02000B96 RID: 2966
	public class SeriesItem : StateManager
	{
		// Token: 0x0600700A RID: 28682 RVA: 0x001A2C0C File Offset: 0x001A0E0C
		public SeriesItem()
		{
		}

		// Token: 0x0600700B RID: 28683 RVA: 0x001A2C14 File Offset: 0x001A0E14
		public SeriesItem(decimal? yValue)
		{
			this.YValue = yValue;
		}

		// Token: 0x0600700C RID: 28684 RVA: 0x001A2C23 File Offset: 0x001A0E23
		public SeriesItem(decimal? xValue, decimal? yValue)
		{
			this.XValue = xValue;
			this.YValue = yValue;
		}

		// Token: 0x170024A3 RID: 9379
		// (get) Token: 0x0600700D RID: 28685 RVA: 0x001A2C39 File Offset: 0x001A0E39
		// (set) Token: 0x0600700E RID: 28686 RVA: 0x001A2C50 File Offset: 0x001A0E50
		[DefaultValue(null)]
		public decimal? XValue
		{
			get
			{
				return (decimal?)base.ViewState["XValue"];
			}
			set
			{
				base.ViewState["XValue"] = value;
			}
		}

		// Token: 0x170024A4 RID: 9380
		// (get) Token: 0x0600700F RID: 28687 RVA: 0x001A2C68 File Offset: 0x001A0E68
		// (set) Token: 0x06007010 RID: 28688 RVA: 0x001A2C7F File Offset: 0x001A0E7F
		[DefaultValue(null)]
		public decimal? YValue
		{
			get
			{
				return (decimal?)base.ViewState["YValue"];
			}
			set
			{
				base.ViewState["YValue"] = value;
			}
		}

		// Token: 0x170024A5 RID: 9381
		// (get) Token: 0x06007011 RID: 28689 RVA: 0x001A2C97 File Offset: 0x001A0E97
		// (set) Token: 0x06007012 RID: 28690 RVA: 0x001A2CAE File Offset: 0x001A0EAE
		[DefaultValue(null)]
		public decimal? SizeValue
		{
			get
			{
				return (decimal?)base.ViewState["SizeValue"];
			}
			set
			{
				base.ViewState["SizeValue"] = value;
			}
		}

		// Token: 0x170024A6 RID: 9382
		// (get) Token: 0x06007013 RID: 28691 RVA: 0x001A2CC6 File Offset: 0x001A0EC6
		// (set) Token: 0x06007014 RID: 28692 RVA: 0x001A2CE7 File Offset: 0x001A0EE7
		[DefaultValue(false)]
		public bool Exploded
		{
			get
			{
				return (bool)(base.ViewState["Exploded"] ?? false);
			}
			set
			{
				base.ViewState["Exploded"] = value;
			}
		}

		// Token: 0x170024A7 RID: 9383
		// (get) Token: 0x06007015 RID: 28693 RVA: 0x001A2CFF File Offset: 0x001A0EFF
		// (set) Token: 0x06007016 RID: 28694 RVA: 0x001A2D1F File Offset: 0x001A0F1F
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x170024A8 RID: 9384
		// (get) Token: 0x06007017 RID: 28695 RVA: 0x001A2D32 File Offset: 0x001A0F32
		// (set) Token: 0x06007018 RID: 28696 RVA: 0x001A2D57 File Offset: 0x001A0F57
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color BackgroundColor
		{
			get
			{
				return (Color)(base.ViewState["BackgroundColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["BackgroundColor"] = value;
			}
		}

		// Token: 0x170024A9 RID: 9385
		// (get) Token: 0x06007019 RID: 28697 RVA: 0x001A2D6F File Offset: 0x001A0F6F
		// (set) Token: 0x0600701A RID: 28698 RVA: 0x001A2D8F File Offset: 0x001A0F8F
		[DefaultValue("")]
		public string TooltipValue
		{
			get
			{
				return (string)(base.ViewState["TooltipValue"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TooltipValue"] = value;
			}
		}
	}
}
