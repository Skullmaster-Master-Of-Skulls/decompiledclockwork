using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200024C RID: 588
	internal class ErrorContext
	{
		// Token: 0x04000708 RID: 1800
		internal int InputPosition = -1;

		// Token: 0x04000709 RID: 1801
		internal string ErrorContextInfo;

		// Token: 0x0400070A RID: 1802
		internal bool UseContextInfoAsResourceIdentifier = true;

		// Token: 0x0400070B RID: 1803
		internal string CommandText;
	}
}
