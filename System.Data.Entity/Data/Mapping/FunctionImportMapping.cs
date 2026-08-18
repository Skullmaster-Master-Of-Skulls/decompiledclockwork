using System;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000223 RID: 547
	internal abstract class FunctionImportMapping
	{
		// Token: 0x060023A5 RID: 9125 RVA: 0x00080560 File Offset: 0x0007E760
		internal FunctionImportMapping(EdmFunction functionImport, EdmFunction targetFunction)
		{
			this.FunctionImport = EntityUtil.CheckArgumentNull<EdmFunction>(functionImport, "functionImport");
			this.TargetFunction = EntityUtil.CheckArgumentNull<EdmFunction>(targetFunction, "targetFunction");
		}

		// Token: 0x04000FC6 RID: 4038
		internal readonly EdmFunction FunctionImport;

		// Token: 0x04000FC7 RID: 4039
		internal readonly EdmFunction TargetFunction;
	}
}
