using System;

namespace System.ServiceModel
{
	// Token: 0x02000104 RID: 260
	internal static class OperationFormatUseHelper
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x0001AB0D File Offset: 0x00018D0D
		public static bool IsDefined(OperationFormatUse x)
		{
			return x == OperationFormatUse.Literal || x == OperationFormatUse.Encoded;
		}
	}
}
