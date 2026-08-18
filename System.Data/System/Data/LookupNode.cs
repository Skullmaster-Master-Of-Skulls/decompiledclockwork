using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020001B5 RID: 437
	internal sealed class LookupNode : ExpressionNode
	{
		// Token: 0x0600190A RID: 6410 RVA: 0x00257F28 File Offset: 0x00257328
		internal LookupNode(DataTable table, string columnName, string relationName) : base(table)
		{
			this.relationName = relationName;
			this.columnName = columnName;
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00257F58 File Offset: 0x00257358
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

		// Token: 0x0600190C RID: 6412 RVA: 0x00258068 File Offset: 0x00257468
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00258088 File Offset: 0x00257488
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

		// Token: 0x0600190E RID: 6414 RVA: 0x002580E8 File Offset: 0x002574E8
		internal override object Eval(int[] recordNos)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00258108 File Offset: 0x00257508
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00258118 File Offset: 0x00257518
		internal override bool IsTableConstant()
		{
			return false;
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00258128 File Offset: 0x00257528
		internal override bool HasLocalAggregate()
		{
			return false;
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00258138 File Offset: 0x00257538
		internal override bool HasRemoteAggregate()
		{
			return false;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00258148 File Offset: 0x00257548
		internal override bool DependsOn(DataColumn column)
		{
			return this.column == column;
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00258168 File Offset: 0x00257568
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000DE8 RID: 3560
		private readonly string relationName;

		// Token: 0x04000DE9 RID: 3561
		private readonly string columnName;

		// Token: 0x04000DEA RID: 3562
		private DataColumn column;

		// Token: 0x04000DEB RID: 3563
		private DataRelation relation;
	}
}
