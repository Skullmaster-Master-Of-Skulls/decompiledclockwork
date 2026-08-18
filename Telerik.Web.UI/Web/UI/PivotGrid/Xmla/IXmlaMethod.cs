using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D84 RID: 3460
	internal interface IXmlaMethod
	{
		// Token: 0x170028EA RID: 10474
		// (get) Token: 0x060080F3 RID: 33011
		IEnumerable<IXmlaMethodProperty> Properties { get; }

		// Token: 0x060080F4 RID: 33012
		void AddProperty(IXmlaMethodProperty property);

		// Token: 0x060080F5 RID: 33013
		void RemoveProperty(IXmlaMethodProperty property);
	}
}
