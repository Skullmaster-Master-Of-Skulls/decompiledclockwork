using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003E1 RID: 993
	public class LineAndScatterLabelsAppearance : SeriesLabelsAppearanceBase
	{
		// Token: 0x0600245A RID: 9306 RVA: 0x00078C54 File Offset: 0x00076E54
		public LineAndScatterLabelsAppearance(string prefix, StateBag OwnerStateBag) : base("lsla" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x00078C68 File Offset: 0x00076E68
		// (set) Token: 0x0600245C RID: 9308 RVA: 0x00078C89 File Offset: 0x00076E89
		[DefaultValue(LineAndScatterLabelsPosition.Above)]
		public LineAndScatterLabelsPosition Position
		{
			get
			{
				return (LineAndScatterLabelsPosition)(base.ViewState["Position"] ?? LineAndScatterLabelsPosition.Above);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00078CA4 File Offset: 0x00076EA4
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder(base.Serialize());
			if (this.Visible == true && this.Position != LineAndScatterLabelsPosition.Above)
			{
				stringBuilder.Insert(stringBuilder.Length - 1, ", position: '" + this.Position.ToString().ToLower() + "'");
			}
			return stringBuilder.ToString();
		}
	}
}
