using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart
{
	// Token: 0x02000B9D RID: 2973
	public class HtmlChartTitle : ObjectWithState
	{
		// Token: 0x0600704B RID: 28747 RVA: 0x001A375B File Offset: 0x001A195B
		public HtmlChartTitle(StateBag OwnerStateBag) : base("cht", OwnerStateBag)
		{
		}

		// Token: 0x170024BB RID: 9403
		// (get) Token: 0x0600704C RID: 28748 RVA: 0x001A3769 File Offset: 0x001A1969
		// (set) Token: 0x0600704D RID: 28749 RVA: 0x001A3789 File Offset: 0x001A1989
		[ClientControlProperty]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x170024BC RID: 9404
		// (get) Token: 0x0600704E RID: 28750 RVA: 0x001A379C File Offset: 0x001A199C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Chart title visual settings")]
		[DefaultValue("Appearance")]
		[Category("Appearance")]
		public TitleAppearance Appearance
		{
			get
			{
				if (this._appearance == null)
				{
					this._appearance = new TitleAppearance(base.OwnerViewState);
				}
				return this._appearance;
			}
		}

		// Token: 0x0600704F RID: 28751 RVA: 0x001A37C0 File Offset: 0x001A19C0
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			stringBuilder.Append(this.Appearance.Serialize());
			if (this.Text != string.Empty)
			{
				stringBuilder.Append(", text: '").Append(this.Text).Append("'");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x04001E22 RID: 7714
		private TitleAppearance _appearance;
	}
}
