using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000F8 RID: 248
	internal sealed class LookupNode : ExpressionNode
	{
		// Token: 0x06000FF5 RID: 4085 RVA: 0x0007FFA4 File Offset: 0x0007F3A4
		internal LookupNode(DataTable table, string columnName, string relationName) : base(table)
		{
			this.relationName = relationName;
			this.columnName = columnName;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0007FFC8 File Offset: 0x0007F3C8
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			this.column = null;
			this.relation = null;
			if (table == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRelationCollection parentRelations = table.ParentRelations;
			if (this.relationName == null)
			{
				if (parentRelations.Count > 1)
				{
					throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
				}
				this.relation = parentRelations[0];
			}
			else
			{
				this.relation = parentRelations[this.relationName];
			}
			if (this.relation == null)
			{
				throw ExprException.BindFailure(this.relationName);
			}
			DataTable parentTable = this.relation.ParentTable;
			this.column = parentTable.Columns[this.columnName];
			if (this.column == null)
			{
				throw ExprException.UnboundName(this.columnName);
			}
			int i;
			for (i = 0; i < list.Count; i++)
			{
				DataColumn dataColumn = list[i];
				if (this.column == dataColumn)
				{
					break;
				}
			}
			if (i >= list.Count)
			{
				list.Add(this.column);
			}
			AggregateNode.Bind(this.relation, list);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000800D4 File Offset: 0x0007F4D4
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000800E8 File Offset: 0x0007F4E8
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this.column == null || this.relation == null)
			{
				throw ExprException.ExpressionUnbound(this.ToString());
			}
			DataRow parentRow = row.GetParentRow(this.relation, version);
			if (parentRow == null)
			{
				return DBNull.Value;
			}
			return parentRow[this.column, parentRow.HasVersion(version) ? version : DataRowVersion.Current];
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00080148 File Offset: 0x0007F548
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00080160 File Offset: 0x0007F560
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00080170 File Offset: 0x0007F570
		internal override bool IsTableConstant()
		{
			return false;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00080180 File Offset: 0x0007F580
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00080190 File Offset: 0x0007F590
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000801A0 File Offset: 0x0007F5A0
		internal override bool DependsOn(DataColumn column)
		{
			return this.column == column;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000801BC File Offset: 0x0007F5BC
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000517 RID: 1303
		private readonly string relationName;

		// Token: 0x04000518 RID: 1304
		private readonly string columnName;

		// Token: 0x04000519 RID: 1305
		private DataColumn column;

		// Token: 0x0400051A RID: 1306
		private DataRelation relation;
	}
}
