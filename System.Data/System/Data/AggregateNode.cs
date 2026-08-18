using System;
using System.Collections.Generic;

namespace System.Data
{
	// Token: 0x020001A2 RID: 418
	internal sealed class AggregateNode : ExpressionNode
	{
		// Token: 0x06001852 RID: 6226 RVA: 0x00250DF8 File Offset: 0x002501F8
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName) : this(table, aggregateType, columnName, true, null)
		{
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00250E18 File Offset: 0x00250218
		internal AggregateNode(DataTable table, FunctionId aggregateType, string columnName, string relationName) : this(table, aggregateType, columnName, false, relationName)
		{
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00250E38 File Offset: 0x00250238
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

		// Token: 0x06001855 RID: 6229 RVA: 0x00250EE8 File Offset: 0x002502E8
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

		// Token: 0x06001856 RID: 6230 RVA: 0x00251018 File Offset: 0x00250418
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

		// Token: 0x06001857 RID: 6231 RVA: 0x00251088 File Offset: 0x00250488
		internal override object Eval()
		{
			return this.Eval(null, DataRowVersion.Default);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x002510A8 File Offset: 0x002504A8
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

		// Token: 0x06001859 RID: 6233 RVA: 0x002511C8 File Offset: 0x002505C8
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

		// Token: 0x0600185A RID: 6234 RVA: 0x00251218 File Offset: 0x00250618
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00251228 File Offset: 0x00250628
		internal override bool IsTableConstant()
		{
			return this.local;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00251248 File Offset: 0x00250648
		internal override bool HasLocalAggregate()
		{
			return this.local;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00251268 File Offset: 0x00250668
		internal override bool HasRemoteAggregate()
		{
			return !this.local;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00251288 File Offset: 0x00250688
		internal override bool DependsOn(DataColumn column)
		{
			return this.column == column || (this.column.Computed && this.column.DataExpression.DependsOn(column));
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x002512C8 File Offset: 0x002506C8
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x04000D2D RID: 3373
		private readonly AggregateType type;

		// Token: 0x04000D2E RID: 3374
		private readonly Aggregate aggregate;

		// Token: 0x04000D2F RID: 3375
		private readonly bool local;

		// Token: 0x04000D30 RID: 3376
		private readonly string relationName;

		// Token: 0x04000D31 RID: 3377
		private readonly string columnName;

		// Token: 0x04000D32 RID: 3378
		private DataTable childTable;

		// Token: 0x04000D33 RID: 3379
		private DataColumn column;

		// Token: 0x04000D34 RID: 3380
		private DataRelation relation;
	}
}
