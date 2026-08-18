using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002E5 RID: 741
	public interface IAssignment : IAssignmentBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x060019AD RID: 6573
		IOrderedDictionary GetData();

		// Token: 0x060019AE RID: 6574
		void LoadFromDictionary(IDictionary values);
	}
}
