using System;
using System.ComponentModel;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003E8 RID: 1000
	public class SeriesAppearance : HtmlChartAppearance, IJsConvertable, IDefaultCheck
	{
		// Token: 0x06002489 RID: 9353 RVA: 0x0007949F File Offset: 0x0007769F
		public SeriesAppearance(StateBag OwnerStateBag) : base("sa", OwnerStateBag, true)
		{
			this.serializer = new AdvancedJavaScriptSerializer();
			this.RegisterConverters();
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000794BF File Offset: 0x000776BF
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		public OverlayAppearance Overlay
		{
			get
			{
				if (this._overlay == null)
				{
					this._overlay = new OverlayAppearance();
				}
				return this._overlay;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x000794DA File Offset: 0x000776DA
		// (set) Token: 0x0600248C RID: 9356 RVA: 0x000794FA File Offset: 0x000776FA
		[DefaultValue("")]
		public string Visual
		{
			get
			{
				return (string)(base.ViewState["Visual"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Visual"] = value;
			}
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00079510 File Offset: 0x00077710
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Serialize());
			string text = this.serializer.Serialize(this);
			string value = text.Substring(1, text.Length - 2);
			if (!string.IsNullOrEmpty(value))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(value);
			}
			if (this.Visual != string.Empty)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.AppendFormat("visual:{0}", this.Visual);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000795AC File Offset: 0x000777AC
		protected void RegisterConverters()
		{
			this.serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new SeriesAppearanceConverter(),
				new OverlayAppearanceConverter()
			});
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000795DC File Offset: 0x000777DC
		public void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new SeriesAppearanceConverter(),
				new OverlayAppearanceConverter()
			});
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x00079607 File Offset: 0x00077807
		public bool IsDefault
		{
			get
			{
				return this.Overlay.IsDefault && this.Visual == string.Empty;
			}
		}

		// Token: 0x04000964 RID: 2404
		private OverlayAppearance _overlay;

		// Token: 0x04000965 RID: 2405
		private readonly AdvancedJavaScriptSerializer serializer;
	}
}
