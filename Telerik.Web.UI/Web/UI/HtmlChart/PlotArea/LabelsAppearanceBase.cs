using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003D8 RID: 984
	public class LabelsAppearanceBase : AppearanceBase
	{
		// Token: 0x06002410 RID: 9232 RVA: 0x00077ED1 File Offset: 0x000760D1
		public LabelsAppearanceBase(string prefix, StateBag OwnerStateBag) : base("lab" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x00077EE5 File Offset: 0x000760E5
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x00077F05 File Offset: 0x00076105
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

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x00077F18 File Offset: 0x00076118
		[DefaultValue("Appearance")]
		[Category("Appearance")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Text visual settings")]
		public TextStyle TextStyle
		{
			get
			{
				if (this._textStyle == null)
				{
					this._textStyle = new TextStyle(HtmlChartConstants.DEFAULT_AXIS_LABELS_FONT_SIZE, "Arial,Helvetica,sans-serif");
				}
				return this._textStyle;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x00077F3D File Offset: 0x0007613D
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x00077F62 File Offset: 0x00076162
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

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x00077F7A File Offset: 0x0007617A
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x00077F9A File Offset: 0x0007619A
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

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00077FAD File Offset: 0x000761AD
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x00077FCD File Offset: 0x000761CD
		[Browsable(true)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(true)]
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

		// Token: 0x0600241A RID: 9242 RVA: 0x00077FE0 File Offset: 0x000761E0
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("labels: {");
			if (this.Visible == false || this.Visible == null)
			{
				stringBuilder.Append("visible: false");
			}
			else
			{
				stringBuilder.Append(base.Serialize());
				string text = this.TextStyle.Serialize();
				if (text != string.Empty)
				{
					stringBuilder.Append(",").Append(text);
				}
				if (stringBuilder.Length - 1 >= 0 && stringBuilder[stringBuilder.Length - 1] == ',')
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
				this.SerializeDataFormats(stringBuilder);
				if (this.Color != Color.Empty)
				{
					stringBuilder.AppendFormat(",color:{0}", HtmlChartHelper.SerializeColor(this.Color));
				}
				if (this.Visual != string.Empty)
				{
					stringBuilder.AppendFormat(",visual:{0}", this.Visual);
				}
				if (!string.IsNullOrEmpty(this.ClientTemplate))
				{
					stringBuilder.AppendFormat(",template: '{0}'", HtmlChartHelper.GetTemplateWithoutNewLinesAndTabs(this.ClientTemplate));
				}
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x00078124 File Offset: 0x00076324
		internal virtual void SerializeDataFormats(StringBuilder sb)
		{
			if (this.DataFormatString != string.Empty)
			{
				sb.Append(", format: '").Append(this.DataFormatString).Append("'");
			}
		}

		// Token: 0x0400095A RID: 2394
		private TextStyle _textStyle;
	}
}
