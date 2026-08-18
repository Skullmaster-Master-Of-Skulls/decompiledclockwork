using System;

namespace System.ServiceModel
{
	// Token: 0x0200009E RID: 158
	internal static class AddressFilterModeHelper
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000FFB9 File Offset: 0x0000E1B9
		public static bool IsDefined(AddressFilterMode x)
		{
			return x == AddressFilterMode.Exact || x == AddressFilterMode.Prefix || x == AddressFilterMode.Any;
		}
	}
}
