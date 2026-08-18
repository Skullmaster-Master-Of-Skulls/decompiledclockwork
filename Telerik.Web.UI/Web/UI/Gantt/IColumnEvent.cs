using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000333 RID: 819
	public interface IColumnEvent
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001C35 RID: 7221
		IBoundColumn Column { get; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001C36 RID: 7222
		DataType ColumnType { get; }
	}
}
