using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020000E5 RID: 229
	internal sealed class AggregateNode : ExpressionNode
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x000790F0 File Offset: 0x000784F0
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName) : this(table, aggregateType, columnName, true, null)
		{
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00079108 File Offset: 0x00078508
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName, string relationName) : this(table, aggregateType, columnName, false, relationName)
		{
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00079124 File Offset: 0x00078524
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName, bool local, string relationName) : base(table)
		{
			this.aggregate = (Aggregate)aggregateType;
			if (aggregateType == FunctionId.Sum)
			{
				this.type = AggregateType.Sum;
			}
			else if (aggregateType == FunctionId.Avg)
			{
				this.type = AggregateType.Mean;
			}
			else if (aggregateType == FunctionId.Min)
			{
				this.type = AggregateType.Min;
			}
			else if (aggregateType == FunctionId.Max)
			{
				this.type = AggregateType.Max;
			}
			else if (aggregateType == FunctionId.Count)
			{
				this.type = AggregateType.Count;
			}
			else if (aggregateType == FunctionId.Var)
			{
				this.type = AggregateType.Var;
			}
			else
			{
				if (aggregateType != FunctionId.StDev)
				{
					throw ExprException.UndefinedFunction(Function.FunctionName[(int)aggregateType]);
				}
				this.type = AggregateType.StDev;
			}
			this.local = local;
			this.relationName = relationName;
			this.columnName = columnName;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000791C8 File Offset: 0x000785C8
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (this.local)
			{
				this.relation = null;
			}
			else
			{
				DataRelationCollection childRelations = table.ChildRelations;
				if (this.relationName == null)
				{
					if (childRelations.Count > 1)
					{
						throw ExprException.UnresolvedRelation(table.TableName, this.ToString());
					}
					if (childRelations.Count != 1)
					{
						throw ExprException.AggregateUnbound(this.ToString());
					}
					this.relation = childRelations[0];
				}
				else
				{
					this.relation = childRelations[this.relationName];
				}
			}
			this.childTable = ((this.relation == null) ? table : this.relation.ChildTable);
			this.column = this.childTable.Columns[this.columnName];
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

		// Token: 0x06000F27 RID: 3879 RVA: 0x000792EC File Offset: 0x000786EC
		internal static void Bind(DataRelation relation, List<DataColumn> list)
		{
			if (relation != null)
			{
				foreach (DataColumn item in relation.ChildColumnsReference)
				{
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				foreach (DataColumn item2 in relation.ParentColumnsReference)
				{
					if (!list.Contains(item2))
					{
						list.Add(item2);
					}
				}
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00079354 File Offset: 0x00078754
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00079370 File Offset: 0x00078770
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (this.childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			DataRow[] array;
			if (this.local)
			{
				array = new DataRow[this.childTable.Rows.Count];
				this.childTable.Rows.CopyTo(array, 0);
			}
			else
			{
				if (row == null)
				{
					throw ExprException.EvalNoContext();
				}
				if (this.relation == null)
				{
					throw ExprException.AggregateUnbound(this.ToString());
				}
				array = row.GetChildRows(this.relation, version);
			}
			if (version == DataRowVersion.Proposed)
			{
				version = DataRowVersion.Default;
			}
			List<int> list = new List<int>();
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].RowState == DataRowState.Deleted)
				{
					if (DataRowAction.Rollback == array[i]._action)
					{
						version = DataRowVersion.Original;
						goto IL_BF;
					}
				}
				else if (DataRowAction.Rollback != array[i]._action || array[i].RowState != DataRowState.Added)
				{
					goto IL_BF;
				}
				IL_E1:
				i++;
				continue;
				IL_BF:
				if (version != DataRowVersion.Original || array[i].oldRecord != -1)
				{
					list.Add(array[i].GetRecordFromVersion(version));
					goto IL_E1;
				}
				goto IL_E1;
			}
			int[] records = list.ToArray();
			return this.column.GetAggregateValue(records, this.type);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00079484 File Offset: 0x00078884
		internal override object Eval(int[] records)
		{
			if (this.childTable == null)
			{
				throw ExprException.AggregateUnbound(this.ToString());
			}
			if (!this.local)
			{
				throw ExprException.ComputeNotAggregate(this.ToString());
			}
			return this.column.GetAggregateValue(records, this.type);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000794CC File Offset: 0x000788CC
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x000794DC File Offset: 0x000788DC
		internal override bool IsTableConstant()
		{
			return this.local;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000794F0 File Offset: 0x000788F0
		internal override bool HasLocalAggregate()
		{
			return this.local;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00079504 File Offset: 0x00078904
		internal override bool HasRemoteAggregate()
		{
			return !this.local;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0007951C File Offset: 0x0007891C
		internal override bool DependsOn(DataColumn column)
		{
			return this.column == column || (this.column.Computed && this.column.DataExpression.DependsOn(column));
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00079554 File Offset: 0x00078954
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000481 RID: 1153
		private readonly AggregateType type;

		// Token: 0x04000482 RID: 1154
		private readonly Aggregate aggregate;

		// Token: 0x04000483 RID: 1155
		private readonly bool local;

		// Token: 0x04000484 RID: 1156
		private readonly string relationName;

		// Token: 0x04000485 RID: 1157
		private readonly string columnName;

		// Token: 0x04000486 RID: 1158
		private DataTable childTable;

		// Token: 0x04000487 RID: 1159
		private DataColumn column;

		// Token: 0x04000488 RID: 1160
		private DataRelation relation;
	}
}
