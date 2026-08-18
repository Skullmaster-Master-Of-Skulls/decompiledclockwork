using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B81 RID: 2945
	public class AppearanceBase : ObjectWithState
	{
		// Token: 0x06006F51 RID: 28497 RVA: 0x001A010E File Offset: 0x0019E30E
		public AppearanceBase(string key, StateBag OwnerStateBag) : base("ap" + key, OwnerStateBag)
		{
		}

		// Token: 0x17002478 RID: 9336
		// (get) Token: 0x06006F52 RID: 28498 RVA: 0x001A0122 File Offset: 0x0019E322
		// (set) Token: 0x06006F53 RID: 28499 RVA: 0x001A0139 File Offset: 0x0019E339
		[DefaultValue(false)]
		public bool? Visible
		{
			get
			{
				return (bool?)base.ViewState["Visible"];
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x17002479 RID: 9337
		// (get) Token: 0x06006F54 RID: 28500 RVA: 0x001A0151 File Offset: 0x0019E351
		// (set) Token: 0x06006F55 RID: 28501 RVA: 0x001A0176 File Offset: 0x0019E376
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

		// Token: 0x06006F56 RID: 28502 RVA: 0x001A0190 File Offset: 0x0019E390
		internal virtual string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.Visible != null)
			{
				stringBuilder.AppendFormat("visible: {0},", this.Visible.ToString().ToLower());
			}
			Color backgroundColor = this.BackgroundColor;
			if (backgroundColor != Color.Empty)
			{
				stringBuilder.Append(" background: '").Append(HtmlChartHelper.ColorToHex(backgroundColor)).Append("',");
			}
			return stringBuilder.ToString();
		}
	}
}
