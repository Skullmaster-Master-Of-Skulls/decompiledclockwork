using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200049E RID: 1182
	public interface IDependency : IDependencyBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x060029EF RID: 10735
		IOrderedDictionary GetData();

		// Token: 0x060029F0 RID: 10736
		void LoadFromDictionary(IDictionary values);
	}
}
