using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource
{
	// Token: 0x02000834 RID: 2100
	internal class Renderer : RendererBase
	{
		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06004DDD RID: 19933 RVA: 0x000F4518 File Offset: 0x000F2718
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x000F452A File Offset: 0x000F272A
		public Renderer(ISchedulerView view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x000F4540 File Offset: 0x000F2740
		protected override void CreateHorizontalContent(SchedulerTopTable topTable)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			schedulerTable.CssClass = base.ContentTableCssClass;
			if (this.Owner.UsingWebServiceBinding)
			{
				base.AddEmptyCell(schedulerTable);
			}
			else
			{
				for (int i = 0; i < this.Model.AgendaModels.Count; i++)
				{
					Model model = this.Model.AgendaModels[i];
					List<TableRow> list = base.CreateViewRows(model.DaySlots);
					if (list.Count > 0)
					{
						if (this.Owner.AgendaView.ShowResourceHeadersResolved)
						{
							TableHeaderCell tableHeaderCell = new TableHeaderCell
							{
								CssClass = "rsResourceHeader"
							};
							this.CreateResourceHeader(tableHeaderCell, this.Model.Resources[i]);
							tableHeaderCell.RowSpan = list.Count;
							list[0].Controls.AddAt(0, tableHeaderCell);
						}
						foreach (TableRow child in list)
						{
							schedulerTable.Controls.Add(child);
						}
					}
				}
			}
			topTable.ContentScrollArea.Controls.Add(schedulerTable);
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x000F4684 File Offset: 0x000F2884
		protected override void CreateVerticalContent(SchedulerTopTable topTable)
		{
			for (int i = 0; i < this.Model.AgendaModels.Count; i++)
			{
				Model model = this.Model.AgendaModels[i];
				if (model.Appointments.Count > 0 || this.Owner.UsingWebServiceBinding)
				{
					if (this.Owner.AgendaView.ShowResourceHeadersResolved)
					{
						this.CreateVerticalResourceHeader(topTable.ContentScrollArea, this.Model.Resources[i]);
					}
					base.CreateInnerContentTable(topTable.ContentScrollArea, model.DaySlots);
				}
			}
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x000F4720 File Offset: 0x000F2920
		protected void CreateVerticalResourceHeader(Control container, Resource resource)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsSubHeader";
			if (this.Owner.UsingWebServiceBinding)
			{
				webControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			this.CreateResourceHeader(webControl, resource);
			container.Controls.Add(webControl);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x000F4774 File Offset: 0x000F2974
		protected void CreateResourceHeader(Control container, Resource resource)
		{
			SchedulerResourceContainer schedulerResourceContainer = new SchedulerResourceContainer(this.Owner);
			schedulerResourceContainer.Resource = resource;
			resource.HeaderControls.Add(schedulerResourceContainer);
			container.Controls.Add(schedulerResourceContainer);
			this.Owner.ResourceHeaderTemplate.InstantiateIn(schedulerResourceContainer);
		}
	}
}
