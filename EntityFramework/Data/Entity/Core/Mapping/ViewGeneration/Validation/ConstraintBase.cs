using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000493 RID: 1171
	internal abstract class ConstraintBase : InternalBase
	{
		// Token: 0x06002B32 RID: 11058
		internal abstract ErrorLog.Record GetErrorRecord();
	}
}
