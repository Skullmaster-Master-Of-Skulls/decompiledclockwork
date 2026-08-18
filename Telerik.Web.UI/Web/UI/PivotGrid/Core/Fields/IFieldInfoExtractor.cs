using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CAB RID: 3243
	public interface IFieldInfoExtractor
	{
		// Token: 0x06007987 RID: 31111
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice.")]
		IEnumerable<IPivotFieldInfo> GetDescriptions();
	}
}
