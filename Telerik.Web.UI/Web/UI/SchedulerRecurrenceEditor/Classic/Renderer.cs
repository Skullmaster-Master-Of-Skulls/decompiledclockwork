using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor.Classic
{
	// Token: 0x020007F8 RID: 2040
	internal class Renderer : RendererBase
	{
		// Token: 0x0600495E RID: 18782 RVA: 0x000E6F22 File Offset: 0x000E5122
		public Renderer(IRecurrenceEditorView view) : base(view)
		{
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x000E6F2B File Offset: 0x000E512B
		protected override void AddAppointmentRecurrenceWeeklyControls()
		{
			base.AddAppointmentRecurrenceWeeklyControls();
			((WebControl)base.View.WeeklyWeekDayThursday.Parent).Style["clear"] = "left";
		}
	}
}
