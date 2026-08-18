using System;

namespace System.ServiceModel
{
	// Token: 0x02000106 RID: 262
	internal static class OperationFormatStyleHelper
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x0001AB18 File Offset: 0x00018D18
		public static bool IsDefined(OperationFormatStyle x)
		{
			return x == OperationFormatStyle.Document || x == OperationFormatStyle.Rpc;
		}
	}
}
