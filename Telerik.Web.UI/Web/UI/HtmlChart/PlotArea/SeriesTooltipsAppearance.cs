using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.Appearance;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003DD RID: 989
	public class SeriesTooltipsAppearance : AppearanceBase, IJsConvertable, IDefaultCheck
	{
		// Token: 0x06002438 RID: 9272 RVA: 0x0007871F File Offset: 0x0007691F
		public SeriesTooltipsAppearance(string prefix, StateBag OwnerStateBag) : base("sta" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x00078733 File Offset: 0x00076933
		// (set) Token: 0x0600243A RID: 9274 RVA: 0x00078754 File Offset: 0x00076954
		[DefaultValue(true)]
		public override bool? Visible
		{
			get
			{
				return (bool?)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x0007876C File Offset: 0x0007696C
		// (set) Token: 0x0600243C RID: 9276 RVA: 0x00078791 File Offset: 0x00076991
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

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000787A9 File Offset: 0x000769A9
		// (set) Token: 0x0600243E RID: 9278 RVA: 0x000787CE File Offset: 0x000769CE
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000787E6 File Offset: 0x000769E6
		// (set) Token: 0x06002440 RID: 9280 RVA: 0x00078806 File Offset: 0x00076A06
		[DefaultValue("")]
		public string DataFormatString
		{
			get
			{
				return (string)(base.ViewState["DataFormatString"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x00078819 File Offset: 0x00076A19
		// (set) Token: 0x06002442 RID: 9282 RVA: 0x0007883A File Offset: 0x00076A3A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		[DefaultValue(0)]
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

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00078852 File Offset: 0x00076A52
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x00078872 File Offset: 0x00076A72
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue("")]
		public string ClientTemplate
		{
			get
			{
				return ((string)base.ViewState["ClientTemplate"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x00078885 File Offset: 0x00076A85
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Appearance")]
		public BorderAppearance BorderAppearance
		{
			get
			{
				if (this._borderAppearance == null)
				{
					this._borderAppearance = new BorderAppearance();
				}
				return this._borderAppearance;
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000788A0 File Offset: 0x00076AA0
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("tooltip: {");
			if (this.Visible == true)
			{
				stringBuilder.Append(base.Serialize());
				if (!string.IsNullOrEmpty(this.DataFormatString))
				{
					stringBuilder.AppendFormat(", format: '{0}'", this.DataFormatString);
				}
				Color backgroundColor = this.BackgroundColor;
				if (backgroundColor != Color.Empty)
				{
					stringBuilder.Append(", background: '").Append(HtmlChartHelper.ColorToHex(backgroundColor)).Append("'");
				}
				if (!string.IsNullOrEmpty(this.ClientTemplate))
				{
					stringBuilder.AppendFormat(", template: '{0}'", HtmlChartHelper.GetTemplateWithoutNewLinesAndTabs(this.ClientTemplate));
				}
				string text = this.BorderAppearance.Serialize();
				if (text != "{}")
				{
					HtmlChartHelper.AddComma(stringBuilder);
					stringBuilder.Append("border:").Append(text);
				}
				if (this.Color != Color.Empty)
				{
					stringBuilder.Append(", color: '").Append(HtmlChartHelper.ColorToHex(this.Color)).Append("'");
				}
				this.SerializeSharedProperties(stringBuilder);
			}
			else
			{
				stringBuilder.Append("visible: false");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000789EC File Offset: 0x00076BEC
		protected virtual void SerializeSharedProperties(StringBuilder sb)
		{
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000789F0 File Offset: 0x00076BF0
		public override void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new SeriesTooltipsConverter[]
			{
				new SeriesTooltipsConverter()
			});
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x00078A14 File Offset: 0x00076C14
		public override bool IsDefault
		{
			get
			{
				return this.Visible == null && this.RotationAngle == 0;
			}
		}

		// Token: 0x0400095D RID: 2397
		private BorderAppearance _borderAppearance;
	}
}
