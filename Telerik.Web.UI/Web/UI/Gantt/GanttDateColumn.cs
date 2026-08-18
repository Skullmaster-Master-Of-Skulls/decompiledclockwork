using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002F8 RID: 760
	public class GanttDateColumn : GanttBoundColumn
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x00055148 File Offset: 0x00053348
		public GanttDateColumn()
		{
			base.DataFormatString = "MM/dd/yyyy HH:mm";
			base.DataType = DataType.DateTime;
		}
	}
}
