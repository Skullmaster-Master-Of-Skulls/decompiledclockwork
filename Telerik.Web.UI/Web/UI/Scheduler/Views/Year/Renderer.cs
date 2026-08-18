using System;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x02000858 RID: 2136
	internal class Renderer : RendererBase
	{
		// Token: 0x170019C0 RID: 6592
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x000F70EC File Offset: 0x000F52EC
		public new Model Model
		{
			get
			{
				return base.Model as Model;
			}
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x000F70F9 File Offset: 0x000F52F9
		public Renderer(ISchedulerView view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x000F7110 File Offset: 0x000F5310
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			schedulerTopTable.ShowRowHeaders = false;
			base.AddMonths(schedulerTopTable.ContentScrollArea, this.Model);
			this.SetScrollAreaOverflow(schedulerTopTable);
			return control.Controls[0];
		}
	}
}
