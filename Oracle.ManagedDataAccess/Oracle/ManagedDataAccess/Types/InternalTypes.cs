using System;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200025E RID: 606
	internal static class InternalTypes
	{
		// Token: 0x06001871 RID: 6257 RVA: 0x00102480 File Offset: 0x00100680
		internal static CompareNullEnum CompareNull(bool b1Null, bool b2Null)
		{
			if (b1Null)
			{
				if (b2Null)
				{
					return CompareNullEnum.BothNull;
				}
				return CompareNullEnum.FirstNullOnly;
			}
			else
			{
				if (b2Null)
				{
					return CompareNullEnum.SecondNullOnly;
				}
				return CompareNullEnum.BothNotNull;
			}
		}

		// Token: 0x04001AB6 RID: 6838
		internal const string NullStr = "null";
	}
}
