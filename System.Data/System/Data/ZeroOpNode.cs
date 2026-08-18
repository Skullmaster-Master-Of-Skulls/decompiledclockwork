using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020001B9 RID: 441
	internal sealed class ZeroOpNode : ExpressionNode
	{
		// Token: 0x06001936 RID: 6454 RVA: 0x00258BE8 File Offset: 0x00257FE8
		internal ZeroOpNode(int op) : base(null)
		{
			this.op = op;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00258C08 File Offset: 0x00258008
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00258C18 File Offset: 0x00258018
		internal override object Eval()
		{
			switch (this.op)
			{
			case 32:
				return DBNull.Value;
			case 33:
				return true;
			case 34:
				return false;
			default:
				return DBNull.Value;
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00258C68 File Offset: 0x00258068
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00258C88 File Offset: 0x00258088
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00258CA8 File Offset: 0x002580A8
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00258CB8 File Offset: 0x002580B8
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00258CC8 File Offset: 0x002580C8
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00258CD8 File Offset: 0x002580D8
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00258CE8 File Offset: 0x002580E8
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000E35 RID: 3637
		internal const int zop_True = 1;

		// Token: 0x04000E36 RID: 3638
		internal const int zop_False = 0;

		// Token: 0x04000E37 RID: 3639
		internal const int zop_Null = -1;

		// Token: 0x04000E38 RID: 3640
		internal readonly int op;
	}
}
