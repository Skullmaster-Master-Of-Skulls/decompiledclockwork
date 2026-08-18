using System;
using System.Collections.Generic;
using OracleInternal.Common;

namespace OracleInternal.TTC.Accessors
{
	// Token: 0x0200020E RID: 526
	internal class TTCRefCursorAccessor : Accessor
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x000CEAD8 File Offset: 0x000CCCD8
		internal TTCRefCursorAccessor(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine) : base(colMetaData, marshallingEngine, true)
		{
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000CEAE4 File Offset: 0x000CCCE4
		internal override void Initialize(ColumnDescribeInfo colMetaData, MarshallingEngine marshallingEngine, bool bForBind)
		{
			if (this.m_TTCResultSetList != null)
			{
				this.m_TTCResultSetList.Clear();
			}
			base.Initialize(colMetaData, marshallingEngine, bForBind);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000CEB04 File Offset: 0x000CCD04
		internal override byte[] GetByteRepresentation(int currentRow)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000CEB0C File Offset: 0x000CCD0C
		internal override bool IsNullIndicatorSet(int currentRow)
		{
			bool result = true;
			if (this.m_TTCResultSetList[currentRow].CursorId != 0)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000CEB34 File Offset: 0x000CCD34
		internal TTCResultSet GetResultSet(int currentRow)
		{
			return this.m_TTCResultSetList[currentRow];
		}

		// Token: 0x17000333 RID: 819
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x000CEB44 File Offset: 0x000CCD44
		internal Accessor[] DefineAccessorForCurrentRow
		{
			set
			{
				this.m_TTCResultSetList[this.m_lastRowProcessed - 1].DefineAccessors = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x000CEB60 File Offset: 0x000CCD60
		internal SQLMetaData SqlMetaDataForCurrentRow
		{
			get
			{
				return this.m_TTCResultSetList[this.m_lastRowProcessed - 1].SqlMetaData;
			}
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000CEB7C File Offset: 0x000CCD7C
		internal override bool UnmarshalOneRow()
		{
			if (!this.m_bNullByDescribe)
			{
				if (this.m_TTCResultSetList == null)
				{
					this.m_TTCResultSetList = new List<TTCResultSet>();
				}
				TTCResultSet ttcresultSet = new TTCResultSet();
				TTCDescribeInfo.ReadMessage(false, true, this.m_marshallingEngine, ttcresultSet.SqlMetaData, false);
				ttcresultSet.CursorId = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				if (this.m_statementType == SqlStatementType.PLSQL)
				{
					this.m_marshallingEngine.UnmarshalSB2();
				}
				this.m_TTCResultSetList.Add(ttcresultSet);
				this.m_lastRowProcessed++;
			}
			return false;
		}

		// Token: 0x040014A1 RID: 5281
		internal List<TTCResultSet> m_TTCResultSetList;
	}
}
