using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.Enums;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000B89 RID: 2953
	public class ExtendedLineAppearance : LineAppearance
	{
		// Token: 0x06006F8D RID: 28557 RVA: 0x001A0DD5 File Offset: 0x0019EFD5
		public ExtendedLineAppearance(string prefix, StateBag OwnerStateBag) : base("extendedLineAppearance" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17002487 RID: 9351
		// (get) Token: 0x06006F8E RID: 28558 RVA: 0x001A0DE9 File Offset: 0x0019EFE9
		// (set) Token: 0x06006F8F RID: 28559 RVA: 0x001A0E0A File Offset: 0x0019F00A
		[DefaultValue(ExtendedLineStyle.Normal)]
		public new ExtendedLineStyle LineStyle
		{
			get
			{
				return (ExtendedLineStyle)(base.ViewState["LineStyle"] ?? ExtendedLineStyle.Normal);
			}
			set
			{
				base.ViewState["LineStyle"] = value;
			}
		}

		// Token: 0x06006F90 RID: 28560 RVA: 0x001A0E22 File Offset: 0x0019F022
		internal override void SerializeLineStyle(StringBuilder sb)
		{
			if (this.LineStyle != ExtendedLineStyle.Normal)
			{
				sb.AppendFormat("style:'{0}'", this.LineStyle.ToString().ToLower());
			}
		}
	}
}
