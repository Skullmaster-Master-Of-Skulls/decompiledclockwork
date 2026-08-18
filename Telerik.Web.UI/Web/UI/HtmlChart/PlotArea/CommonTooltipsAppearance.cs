using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x020003DE RID: 990
	public class CommonTooltipsAppearance : SeriesTooltipsAppearance
	{
		// Token: 0x0600244A RID: 9290 RVA: 0x00078A3C File Offset: 0x00076C3C
		public CommonTooltipsAppearance(string prefix, StateBag OwnerStateBag) : base("cta" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00078A50 File Offset: 0x00076C50
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x00078A67 File Offset: 0x00076C67
		[DefaultValue(null)]
		public bool? Shared
		{
			get
			{
				return (bool?)base.ViewState["Shared"];
			}
			set
			{
				base.ViewState["Shared"] = value;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x00078A7F File Offset: 0x00076C7F
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x00078A9F File Offset: 0x00076C9F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Browsable(true)]
		[Bindable(true)]
		public string SharedTemplate
		{
			get
			{
				return (string)(base.ViewState["SharedTemplate"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SharedTemplate"] = value;
			}
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00078AB4 File Offset: 0x00076CB4
		protected override void SerializeSharedProperties(StringBuilder sb)
		{
			if (this.Shared != null)
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.AppendFormat(",shared: {0}", this.Shared.ToString().ToLower());
			}
			if (!string.IsNullOrEmpty(this.SharedTemplate))
			{
				HtmlChartHelper.RemoveEndingComma(sb);
				sb.AppendFormat(",sharedTemplate: '{0}'", HtmlChartHelper.GetTemplateWithoutNewLinesAndTabs(this.SharedTemplate));
			}
		}
	}
}
