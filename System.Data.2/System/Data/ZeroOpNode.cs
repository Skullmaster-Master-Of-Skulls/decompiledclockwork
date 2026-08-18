using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000FC RID: 252
	internal sealed class ZeroOpNode : ExpressionNode
	{
		// Token: 0x06001021 RID: 4129 RVA: 0x00080B40 File Offset: 0x0007FF40
		internal ZeroOpNode(int op) : base(null)
		{
			this.op = op;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00080B5C File Offset: 0x0007FF5C
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00080B6C File Offset: 0x0007FF6C
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

		// Token: 0x06001024 RID: 4132 RVA: 0x00080BB0 File Offset: 0x0007FFB0
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			return this.Eval();
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00080BC4 File Offset: 0x0007FFC4
		internal override object Eval(int[] recordNos)
		{
			return this.Eval();
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00080BD8 File Offset: 0x0007FFD8
		internal override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00080BE8 File Offset: 0x0007FFE8
		internal override bool IsTableConstant()
		{
			return true;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00080BF8 File Offset: 0x0007FFF8
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00080C08 File Offset: 0x00080008
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00080C18 File Offset: 0x00080018
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000564 RID: 1380
		internal readonly int op;

		// Token: 0x04000565 RID: 1381
		internal const int zop_True = 1;

		// Token: 0x04000566 RID: 1382
		internal const int zop_False = 0;

		// Token: 0x04000567 RID: 1383
		internal const int zop_Null = -1;
	}
}
