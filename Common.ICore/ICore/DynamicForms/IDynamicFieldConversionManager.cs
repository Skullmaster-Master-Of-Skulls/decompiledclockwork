using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x02000099 RID: 153
	public interface IDynamicFieldConversionManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600046C RID: 1132
		IList<string> FindAllDataTableSuffixesWhereControlIdHasData(int ControlId);
	}
}
