using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020004A6 RID: 1190
	public interface ITask : ITaskBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06002A03 RID: 10755
		// (set) Token: 0x06002A04 RID: 10756
		IGantt Owner { get; set; }

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06002A05 RID: 10757
		TimeSpan Duration { get; }

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06002A06 RID: 10758
		DependencyCollection Dependencies { get; }

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06002A07 RID: 10759
		DependencyCollection Predecessors { get; }

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06002A08 RID: 10760
		DependencyCollection Successors { get; }

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06002A09 RID: 10761
		TaskCollection Tasks { get; }

		// Token: 0x06002A0A RID: 10762
		IOrderedDictionary GetData();

		// Token: 0x06002A0B RID: 10763
		void LoadFromDictionary(IDictionary values);
	}
}
