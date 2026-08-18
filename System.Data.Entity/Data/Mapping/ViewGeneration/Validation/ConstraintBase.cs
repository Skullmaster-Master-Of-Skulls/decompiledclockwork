using System;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000282 RID: 642
	internal abstract class ConstraintBase : InternalBase
	{
		// Token: 0x0600269D RID: 9885
		internal abstract ErrorLog.Record GetErrorRecord();
	}
}
