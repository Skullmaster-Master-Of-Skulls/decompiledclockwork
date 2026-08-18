using System;
using System.Collections.Generic;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x020000F9 RID: 249
	internal sealed class NameNode : ExpressionNode
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x000801CC File Offset: 0x0007F5CC
		internal NameNode(DataTable table, char[] text, int start, int pos) : base(table)
		{
			this.name = NameNode.ParseName(text, start, pos);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000801F0 File Offset: 0x0007F5F0
		internal NameNode(DataTable table, string name) : base(table)
		{
			this.name = name;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0008020C File Offset: 0x0007F60C
		internal override bool IsSqlColumn
		{
			get
			{
				return this.column.IsSqlType;
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00080224 File Offset: 0x0007F624
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

		// Token: 0x06001004 RID: 4100 RVA: 0x00080300 File Offset: 0x0007F700
		internal override object Eval()
		{
			throw ExprException.EvalNoContext();
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00080314 File Offset: 0x0007F714
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

		// Token: 0x06001006 RID: 4102 RVA: 0x00080370 File Offset: 0x0007F770
		internal override object Eval(int[] records)
		{
			throw ExprException.ComputeNotAggregate(this.ToString());
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00080388 File Offset: 0x0007F788
		internal override bool IsConstant()
		{
			return false;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00080398 File Offset: 0x0007F798
		internal override bool IsTableConstant()
		{
			return this.column != null && this.column.Computed && this.column.DataExpression.IsTableAggregate();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x000803CC File Offset: 0x0007F7CC
		internal override bool HasLocalAggregate()
		{
			return this.column != null && this.column.Computed && this.column.DataExpression.HasLocalAggregate();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00080400 File Offset: 0x0007F800
		internal override bool HasRemoteAggregate()
		{
			return this.column != null && this.column.Computed && this.column.DataExpression.HasRemoteAggregate();
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00080434 File Offset: 0x0007F834
		internal override bool DependsOn(DataColumn column)
		{
			return this.column == column || (this.column.Computed && this.column.DataExpression.DependsOn(column));
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0008046C File Offset: 0x0007F86C
		internal override ExpressionNode Optimize()
		{
			return this;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0008047C File Offset: 0x0007F87C
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

		// Token: 0x0400051B RID: 1307
		internal char open;

		// Token: 0x0400051C RID: 1308
		internal char close;

		// Token: 0x0400051D RID: 1309
		internal string name;

		// Token: 0x0400051E RID: 1310
		internal bool found;

		// Token: 0x0400051F RID: 1311
		internal bool type;

		// Token: 0x04000520 RID: 1312
		internal DataColumn column;
	}
}
