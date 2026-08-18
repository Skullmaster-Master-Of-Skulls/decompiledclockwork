using System;
using System.Collections;
using OracleInternal.Common;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001B0 RID: 432
	internal class CachedStatement
	{
		// Token: 0x040012FE RID: 4862
		internal int m_cursorId;

		// Token: 0x040012FF RID: 4863
		internal Accessor[] m_accessors;

		// Token: 0x04001300 RID: 4864
		internal DataUnmarshaller m_dataUnmarshaller;

		// Token: 0x04001301 RID: 4865
		internal long[] m_scnFromExecution;

		// Token: 0x04001302 RID: 4866
		internal ArrayList m_placeHolderCollection;

		// Token: 0x04001303 RID: 4867
		internal bool m_bBindParamPresent;

		// Token: 0x04001304 RID: 4868
		internal long m_numRowsFetchArrayCanAccomodate;

		// Token: 0x04001305 RID: 4869
		internal Accessor[] m_bindAccessors;

		// Token: 0x04001306 RID: 4870
		internal ColumnDescribeInfo[] m_bindParamMetadata;

		// Token: 0x04001307 RID: 4871
		internal bool m_bDisableCompressedFetch;

		// Token: 0x04001308 RID: 4872
		internal long m_internalInitialLOBFS;

		// Token: 0x04001309 RID: 4873
		internal bool m_bDefinesDone;

		// Token: 0x0400130A RID: 4874
		internal int m_longFetchSize;

		// Token: 0x0400130B RID: 4875
		internal BindDirection[] m_bindDirections;

		// Token: 0x0400130C RID: 4876
		internal bool m_bAllInBinds;

		// Token: 0x0400130D RID: 4877
		internal bool m_bAllOutBinds;

		// Token: 0x0400130E RID: 4878
		internal SQLMetaData statementdata;

		// Token: 0x0400130F RID: 4879
		internal SQLInfo sqlInfo;

		// Token: 0x04001310 RID: 4880
		internal ulong m_lastUsedCount;

		// Token: 0x04001311 RID: 4881
		internal bool m_hasExclusiveOwnershipOfCursorInfo;

		// Token: 0x04001312 RID: 4882
		internal bool m_bIsPooled;
	}
}
