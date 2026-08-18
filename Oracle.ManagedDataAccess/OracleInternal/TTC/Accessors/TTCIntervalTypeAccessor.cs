using System;
using OracleInternal.Common;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x02000203 RID: 515
	internal class TTCIntervalTypeAccessor : Accessor
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x000CCA1C File Offset: 0x000CAC1C
		internal TTCIntervalTypeAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind) : base(colMetaData, marshallingEngine, bForBind)
		{
			if (!bForBind)
			{
				this.InitForDataAccess(colMetaData.m_maxLength);
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000CCA38 File Offset: 0x000CAC38
		internal override void InitForDataAccess(int max_len)
		{
			this.m_internalTypeMaxLength = this.m_colMetaData.m_maxLength;
			this.m_byteLength = this.m_internalTypeMaxLength;
		}

		// Token: 0x04001477 RID: 5239
		internal static int INTERVALTYPE_MAX_LENGTH = 11;
	}
}
