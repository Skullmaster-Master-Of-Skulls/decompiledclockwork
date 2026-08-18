using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views.Month;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001A81 RID: 6785
	internal class SchedulerContentPanel : WebControl
	{
		// Token: 0x17004FC7 RID: 20423
		// (get) Token: 0x060106DC RID: 67292 RVA: 0x003ABCF9 File Offset: 0x003A9EF9
		// (set) Token: 0x060106DD RID: 67293 RVA: 0x003ABD01 File Offset: 0x003A9F01
		private RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x17004FC8 RID: 20424
		// (get) Token: 0x060106DE RID: 67294 RVA: 0x003ABD0A File Offset: 0x003A9F0A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004FC9 RID: 20425
		// (get) Token: 0x060106DF RID: 67295 RVA: 0x003ABD0E File Offset: 0x003A9F0E
		// (set) Token: 0x060106E0 RID: 67296 RVA: 0x003ABD16 File Offset: 0x003A9F16
		public SchedulerTable ContentTable
		{
			get
			{
				return this._contentTable;
			}
			private set
			{
				this._contentTable = value;
			}
		}

		// Token: 0x17004FCA RID: 20426
		// (get) Token: 0x060106E1 RID: 67297 RVA: 0x003ABD1F File Offset: 0x003A9F1F
		// (set) Token: 0x060106E2 RID: 67298 RVA: 0x003ABD27 File Offset: 0x003A9F27
		public TableRow ContentRow
		{
			get
			{
				return this._contentRow;
			}
			private set
			{
				this._contentRow = value;
			}
		}

		// Token: 0x060106E3 RID: 67299 RVA: 0x003ABD30 File Offset: 0x003A9F30
		public SchedulerContentPanel(RadScheduler owner, string additionalCssClass)
		{
			this.CreateSchedulerContentPanel(owner, additionalCssClass);
		}

		// Token: 0x060106E4 RID: 67300 RVA: 0x003ABD40 File Offset: 0x003A9F40
		private void CreateSchedulerContentPanel(RadScheduler owner, string additionalCssClass)
		{
			this.Owner = owner;
			this.CssClass = "rsContent " + additionalCssClass;
			if (this.Owner.DesignMode)
			{
				if (this.Owner.OverflowBehavior == OverflowBehavior.Expand)
				{
					base.Style["overflow-y"] = "visible";
				}
				else
				{
					this.Height = Unit.Percentage(100.0);
				}
			}
			this.CreateContentTable();
		}

		// Token: 0x060106E5 RID: 67301 RVA: 0x003ABDB4 File Offset: 0x003A9FB4
		private void CreateContentTable()
		{
			this.ContentTable = new ContentTable();
			this.ContentTable.CssClass = "";
			this.ContentRow = new TableRow();
			this.ContentTable.Controls.Add(this.ContentRow);
			this.Controls.Add(this.ContentTable);
		}

		// Token: 0x060106E6 RID: 67302 RVA: 0x003ABE10 File Offset: 0x003AA010
		protected override void OnPreRender(EventArgs e)
		{
			if (HttpContext.Current != null)
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				if (!browser.IsBrowser("IE") || browser.MajorVersion >= 8)
				{
					this.ContentTable.Width = Unit.Percentage(100.0);
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x040049AB RID: 18859
		private RadScheduler _owner;

		// Token: 0x040049AC RID: 18860
		private TableRow _contentRow;

		// Token: 0x040049AD RID: 18861
		private SchedulerTable _contentTable;
	}
}
