using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000392 RID: 914
	internal class GridMobileFilterView : GridMobileView
	{
		// Token: 0x06001F77 RID: 8055 RVA: 0x00063AFB File Offset: 0x00061CFB
		public GridMobileFilterView(GridTableView tableView) : base(tableView)
		{
			this.CssClass = "rgMobileFilterForm";
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x00063B0F File Offset: 0x00061D0F
		public override GridMobileViewType Type
		{
			get
			{
				return GridMobileViewType.Filter;
			}
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00063B14 File Offset: 0x00061D14
		protected override void CreateContent(HtmlGenericControl container)
		{
			container.Controls.Add(base.CreateTitle(base.Localization.HeaderContextMenuRowsLabel));
			this.AddFilterControls(container);
			container.Controls.Add(base.CreateTitle(base.Localization.HeaderContextMenuAndLabel));
			this.AddFilterControls(container);
			container.Controls.Add(base.CreateButton("Clear", "rgClear"));
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00063B84 File Offset: 0x00061D84
		private void AddFilterControls(HtmlGenericControl container)
		{
			container.Controls.Add(base.CreateLabel(base.Localization.MobileFilterViewOptionsText, ""));
			container.Controls.Add(this.CreateFilterOptions());
			container.Controls.Add(base.CreateLabel(base.Localization.MobileFilterViewValueText, ""));
			container.Controls.Add(this.CreateInput());
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00063BF8 File Offset: 0x00061DF8
		private HtmlSelect CreateFilterOptions()
		{
			HtmlSelect htmlSelect = new HtmlSelect();
			htmlSelect.Attributes.Add("class", "rgValue");
			foreach (string text in Enum.GetNames(typeof(GridKnownFunction)))
			{
				htmlSelect.Items.Add(new ListItem
				{
					Text = base.Localization.GetStringFromViewState(string.Format("{0}Text", text)),
					Value = text
				});
			}
			return htmlSelect;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00063C80 File Offset: 0x00061E80
		private HtmlGenericControl CreateInput()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("input");
			htmlGenericControl.Attributes.Add("class", "rgValue");
			return htmlGenericControl;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00063CAE File Offset: 0x00061EAE
		protected override void DescribeProperties(ScriptControlDescriptor descriptor)
		{
			base.DescribeProperties(descriptor);
			descriptor.AddProperty("_titleFormat", base.Localization.MobileFilterViewTitleFormat);
		}
	}
}
