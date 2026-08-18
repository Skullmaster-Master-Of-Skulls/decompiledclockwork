using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000959 RID: 2393
	public interface ITreeListColumnEditor
	{
		// Token: 0x06005B1E RID: 23326
		void Initialize(TreeListEditableItem editItem, Control container);

		// Token: 0x06005B1F RID: 23327
		void SetValues(IEnumerable values);

		// Token: 0x06005B20 RID: 23328
		IEnumerable GetValues();
	}
}
