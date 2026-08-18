using System;

namespace System.Data.OleDb
{
	// Token: 0x0200025E RID: 606
	internal enum OleDbPropertyStatus
	{
		// Token: 0x04001787 RID: 6023
		Ok,
		// Token: 0x04001788 RID: 6024
		NotSupported,
		// Token: 0x04001789 RID: 6025
		BadValue,
		// Token: 0x0400178A RID: 6026
		BadOption,
		// Token: 0x0400178B RID: 6027
		BadColumn,
		// Token: 0x0400178C RID: 6028
		NotAllSettable,
		// Token: 0x0400178D RID: 6029
		NotSettable,
		// Token: 0x0400178E RID: 6030
		NotSet,
		// Token: 0x0400178F RID: 6031
		Conflicting,
		// Token: 0x04001790 RID: 6032
		NotAvailable
	}
}
