using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000341 RID: 833
	public class DefaultColumnsFactory : IColumnFactory
	{
		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0005A38B File Offset: 0x0005858B
		// (set) Token: 0x06001C5F RID: 7263 RVA: 0x0005A393 File Offset: 0x00058593
		public bool Required { get; set; }

		// Token: 0x06001C60 RID: 7264 RVA: 0x0005A39C File Offset: 0x0005859C
		public DefaultColumnsFactory()
		{
			this.Required = true;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0005A3AC File Offset: 0x000585AC
		public GanttBoundColumn CreateColumn(DataType type)
		{
			switch (type)
			{
			case DataType.String:
				return new GanttStringColumn();
			case DataType.Number:
				return new GanttNumericColumn();
			case DataType.DateTime:
				return new GanttDateColumn();
			case DataType.Boolean:
			case DataType.Null:
			case DataType.Other:
				return new GanttBoundColumn();
			}
			return new GanttStringColumn();
		}
	}
}
