using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000118 RID: 280
	public sealed class DbSetClause : DbModificationClause
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x000281C5 File Offset: 0x000263C5
		internal DbSetClause(DbExpression targetProperty, DbExpression sourceValue)
		{
			this._prop = targetProperty;
			this._val = sourceValue;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x000281DB File Offset: 0x000263DB
		public DbExpression Property
		{
			get
			{
				return this._prop;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x000281E3 File Offset: 0x000263E3
		public DbExpression Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000281EC File Offset: 0x000263EC
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

		// Token: 0x06000761 RID: 1889 RVA: 0x00028244 File Offset: 0x00026444
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Common.Utils.TreeNode.#ctor(System.String,System.Data.Entity.Core.Common.Utils.TreeNode[])")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "DbSetClause")]
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

		// Token: 0x04000252 RID: 594
		private readonly DbExpression _prop;

		// Token: 0x04000253 RID: 595
		private readonly DbExpression _val;
	}
}
