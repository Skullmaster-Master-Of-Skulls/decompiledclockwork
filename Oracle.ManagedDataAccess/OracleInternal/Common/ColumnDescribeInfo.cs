using System;

namespace OracleInternal.Common
{
	// Token: 0x020000B5 RID: 181
	internal class ColumnDescribeInfo
	{
		// Token: 0x04000968 RID: 2408
		internal bool m_isNullAllowed;

		// Token: 0x04000969 RID: 2409
		internal string pColAlias;

		// Token: 0x0400096A RID: 2410
		internal short m_dataType;

		// Token: 0x0400096B RID: 2411
		internal bool bIsXmlType;

		// Token: 0x0400096C RID: 2412
		internal short m_flag;

		// Token: 0x0400096D RID: 2413
		internal short m_precision;

		// Token: 0x0400096E RID: 2414
		internal short m_scale;

		// Token: 0x0400096F RID: 2415
		internal int m_maxLength;

		// Token: 0x04000970 RID: 2416
		internal int m_maxLengthOfChars;

		// Token: 0x04000971 RID: 2417
		internal int m_maxNoOfArrayElements;

		// Token: 0x04000972 RID: 2418
		internal int m_contFlag;

		// Token: 0x04000973 RID: 2419
		internal byte[] m_toid;

		// Token: 0x04000974 RID: 2420
		internal int m_version;

		// Token: 0x04000975 RID: 2421
		internal int m_characterSetId;

		// Token: 0x04000976 RID: 2422
		internal short m_characterSetForm;

		// Token: 0x04000977 RID: 2423
		internal int m_oaccollid;
	}
}
