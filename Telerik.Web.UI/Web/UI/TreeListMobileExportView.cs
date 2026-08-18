using System;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000969 RID: 2409
	internal class TreeListMobileExportView : TreeListMobileView
	{
		// Token: 0x06005BBE RID: 23486 RVA: 0x00117930 File Offset: 0x00115B30
		public TreeListMobileExportView(RadTreeList treelist) : base(treelist)
		{
			base.Title = base.Localization.MobileExportViewTitle;
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x0011794A File Offset: 0x00115B4A
		public override TreeListMobileViewType Type
		{
			get
			{
				return TreeListMobileViewType.Export;
			}
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x00117950 File Offset: 0x00115B50
		protected HtmlGenericControl CreateSpanButton(string title, string cssClass = "")
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.Attributes.Add("class", string.Format("rtlButton {0}", cssClass).Trim());
			htmlGenericControl.InnerText = title;
			return htmlGenericControl;
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x00117990 File Offset: 0x00115B90
		protected override void CreateContent(HtmlGenericControl container)
		{
			container.Controls.Add(base.CreateTitle(base.Localization.MobileExportViewDescription));
			TreeListCommandItemSettings commandItemSettings = base.TreeList.CommandItemSettings;
			if (commandItemSettings.ShowExportToExcelButton)
			{
				container.Controls.Add(this.CreateSpanButton(base.Localization.ExportToExcelText, "rtlColumnItem rtlExcelExport"));
			}
			if (commandItemSettings.ShowExportToWordButton)
			{
				container.Controls.Add(this.CreateSpanButton(base.Localization.ExportToWordText, "rtlColumnItem rtlWordExport"));
			}
			if (commandItemSettings.ShowExportToPdfButton)
			{
				container.Controls.Add(this.CreateSpanButton(base.Localization.ExportToPdfText, "rtlColumnItem rtlPdfExport"));
			}
		}
	}
}
