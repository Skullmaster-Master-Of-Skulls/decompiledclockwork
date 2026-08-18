using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x020000F7 RID: 247
	internal class InternalTypes
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x00058B83 File Offset: 0x00057B83
		private InternalTypes()
		{
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00058B8B File Offset: 0x00057B8B
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

		// Token: 0x04000813 RID: 2067
		internal const string NullStr = "null";
	}
}
