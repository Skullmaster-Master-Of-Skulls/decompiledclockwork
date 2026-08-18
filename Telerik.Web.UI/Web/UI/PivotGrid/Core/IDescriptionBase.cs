using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200067D RID: 1661
	public interface IDescriptionBase : INamed
	{
		// Token: 0x06003CA1 RID: 15521
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice")]
		string GetUniqueName();

		// Token: 0x06003CA2 RID: 15522
		IDescriptionBase Clone();
	}
}
