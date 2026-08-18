using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020001B6 RID: 438
	internal sealed class NameNode : ExpressionNode
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x00258178 File Offset: 0x00257578
		internal NameNode(DataTable table, char[] text, int start, int pos) : base(table)
		{
			this.name = NameNode.ParseName(text, start, pos);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x002581A8 File Offset: 0x002575A8
		internal NameNode(DataTable table, string name) : base(table)
		{
			this.name = name;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x002581C8 File Offset: 0x002575C8
		internal override bool IsSqlColumn
		{
			get
			{
				return this.column.IsSqlType;
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x002581E8 File Offset: 0x002575E8
		internal override void Bind(DataTable table, List<DataColumn> list)
		{
			base.BindTable(table);
			if (table == null)
			{
				throw ExprException.UnboundName(this.name);
			}
			try
			{
				this.column = table.Columns[this.name];
			}
			catch (Exception e)
			{
				this.found = false;
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				throw ExprException.UnboundName(this.name);
			}
			if (this.column == null)
			{
				throw ExprException.UnboundName(this.name);
			}
			this.name = this.column.ColumnName;
			this.found = true;
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
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x002582C8 File Offset: 0x002576C8
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x002582E8 File Offset: 0x002576E8
		internal override object Eval(DataRow row, DataRowVersion version)
		{
			if (!this.found)
			{
				throw ExprException.UnboundName(this.name);
			}
			if (row != null)
			{
				return this.column[row.GetRecordFromVersion(version)];
			}
			if (this.IsTableConstant())
			{
				return this.column.DataExpression.Evaluate();
			}
			throw ExprException.UnboundName(this.name);
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00258348 File Offset: 0x00257748
		internal override object Eval(int[] records)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00258368 File Offset: 0x00257768
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00258378 File Offset: 0x00257778
		internal override bool IsTableConstant()
		{
			return this.column != null && this.column.Computed && this.column.DataExpression.IsTableAggregate();
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x002583B8 File Offset: 0x002577B8
		internal override bool HasLocalAggregate()
		{
			return this.column != null && this.column.Computed && this.column.DataExpression.HasLocalAggregate();
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x002583F8 File Offset: 0x002577F8
		internal override bool HasRemoteAggregate()
		{
			return this.column != null && this.column.Computed && this.column.DataExpression.HasRemoteAggregate();
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00258438 File Offset: 0x00257838
		internal override bool DependsOn(DataColumn column)
		{
			return this.column == column || (this.column.Computed && this.column.DataExpression.DependsOn(column));
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00258478 File Offset: 0x00257878
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00258488 File Offset: 0x00257888
		internal static string ParseName(char[] text, int start, int pos)
		{
			char c = '\0';
			string text2 = "";
			int num = start;
			int num2 = pos;
			checked
			{
				if (text[start] == '`')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "`";
				}
				else if (text[start] == '[')
				{
					start++;
					pos--;
					c = '\\';
					text2 = "]\\";
				}
			}
			if (c != '\0')
			{
				int num3 = start;
				for (int i = start; i < pos; i++)
				{
					if (text[i] == c && i + 1 < pos && text2.IndexOf(text[i + 1]) >= 0)
					{
						i++;
					}
					text[num3] = text[i];
					num3++;
				}
				pos = num3;
			}
			if (pos == start)
			{
				throw ExprException.InvalidName(new string(text, num, num2 - num));
			}
			return new string(text, start, pos - start);
		}

		// Token: 0x04000DEC RID: 3564
		internal char open;

		// Token: 0x04000DED RID: 3565
		internal char close;

		// Token: 0x04000DEE RID: 3566
		internal string name;

		// Token: 0x04000DEF RID: 3567
		internal bool found;

		// Token: 0x04000DF0 RID: 3568
		internal bool type;

		// Token: 0x04000DF1 RID: 3569
		internal DataColumn column;
	}
}
