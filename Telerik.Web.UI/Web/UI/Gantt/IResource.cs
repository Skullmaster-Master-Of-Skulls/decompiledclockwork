using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000326 RID: 806
	public interface IResource : IResourceBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001AE8 RID: 6888
		// (set) Token: 0x06001AE9 RID: 6889
		Color Color { get; set; }

		// Token: 0x06001AEA RID: 6890
		IOrderedDictionary GetData();

		// Token: 0x06001AEB RID: 6891
		void LoadFromDictionary(IDictionary values);
	}
}
