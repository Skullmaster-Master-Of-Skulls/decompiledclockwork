using System;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.Utils;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003EC RID: 1004
	public sealed class DbSetClause : DbModificationClause
	{
		// Token: 0x060035D8 RID: 13784 RVA: 0x000CFEF9 File Offset: 0x000CE0F9
		internal DbSetClause(DbExpression targetProperty, DbExpression sourceValue)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(targetProperty, "targetProperty");
			EntityUtil.CheckArgumentNull<DbExpression>(sourceValue, "sourceValue");
			this._prop = targetProperty;
			this._val = sourceValue;
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000CFF27 File Offset: 0x000CE127
		public DbExpression Property
		{
			get
			{
				return this._prop;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060035DA RID: 13786 RVA: 0x000CFF2F File Offset: 0x000CE12F
		public DbExpression Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000CFF38 File Offset: 0x000CE138
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			dumper.Begin("DbSetClause");
			if (this.Property != null)
			{
				dumper.Dump(this.Property, "Property");
			}
			if (this.Value != null)
			{
				dumper.Dump(this.Value, "Value");
			}
			dumper.End("DbSetClause");
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000CFF90 File Offset: 0x000CE190
		internal override TreeNode Print(DbExpressionVisitor<TreeNode> visitor)
		{
			TreeNode treeNode = new TreeNode("DbSetClause", new TreeNode[0]);
			if (this.Property != null)
			{
				treeNode.Children.Add(new TreeNode("Property", new TreeNode[]
				{
					this.Property.Accept<TreeNode>(visitor)
				}));
			}
			if (this.Value != null)
			{
				treeNode.Children.Add(new TreeNode("Value", new TreeNode[]
				{
					this.Value.Accept<TreeNode>(visitor)
				}));
			}
			return treeNode;
		}

		// Token: 0x040017B1 RID: 6065
		private DbExpression _prop;

		// Token: 0x040017B2 RID: 6066
		private DbExpression _val;
	}
}
