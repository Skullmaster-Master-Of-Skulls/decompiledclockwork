using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.PlotArea;

namespace Telerik.Web.UI.HtmlChart.Appearance
{
	// Token: 0x020003E3 RID: 995
	public class MarkersAppearanceBase : AppearanceBase
	{
		// Token: 0x06002469 RID: 9321 RVA: 0x00078EA8 File Offset: 0x000770A8
		public MarkersAppearanceBase(string key, StateBag OwnerStateBag) : base("mab" + key, OwnerStateBag)
		{
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x00078EBC File Offset: 0x000770BC
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x00078EDD File Offset: 0x000770DD
		[Bindable(false)]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int RotationAngle
		{
			get
			{
				return (int)(base.ViewState["RotationAngle"] ?? 0);
			}
			set
			{
				base.ViewState["RotationAngle"] = value;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x00078EF5 File Offset: 0x000770F5
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x00078F16 File Offset: 0x00077116
		[DefaultValue(MarkersType.Circle)]
		public virtual MarkersType MarkersType
		{
			get
			{
				return (MarkersType)(base.ViewState["MarkersType"] ?? MarkersType.Circle);
			}
			set
			{
				base.ViewState["MarkersType"] = value;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x00078F2E File Offset: 0x0007712E
		// (set) Token: 0x0600246F RID: 9327 RVA: 0x00078F53 File Offset: 0x00077153
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "")]
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

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x00078F6B File Offset: 0x0007716B
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x00078F82 File Offset: 0x00077182
		[DefaultValue(null)]
		public decimal? Size
		{
			get
			{
				return (decimal?)base.ViewState["Size"];
			}
			set
			{
				base.ViewState["Size"] = value;
			}
		}
	}
}
