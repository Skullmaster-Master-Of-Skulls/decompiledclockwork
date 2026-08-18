using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000114 RID: 276
	public sealed class DbInsertCommandTree : DbModificationCommandTree
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00027064 File Offset: 0x00025264
		internal DbInsertCommandTree()
		{
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0002706C File Offset: 0x0002526C
		public DbInsertCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, ReadOnlyCollection<DbModificationClause> setClauses, DbExpression returning) : base(metadata, dataSpace, target)
		{
			this._setClauses = setClauses;
			this._returning = returning;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00027087 File Offset: 0x00025287
		public IList<DbModificationClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0002708F File Offset: 0x0002528F
		public DbExpression Returning
		{
			get
			{
				return this._returning;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00027097 File Offset: 0x00025297
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Insert;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0002709A File Offset: 0x0002529A
		internal override bool HasReader
		{
			get
			{
				return null != this.Returning;
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000270A8 File Offset: 0x000252A8
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			base.DumpStructure(dumper);
			dumper.Begin("SetClauses");
			foreach (DbModificationClause dbModificationClause in this.SetClauses)
			{
				if (dbModificationClause != null)
				{
					dbModificationClause.DumpStructure(dumper);
				}
			}
			dumper.End("SetClauses");
			if (this.Returning != null)
			{
				dumper.Dump(this.Returning, "Returning");
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00027130 File Offset: 0x00025330
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x0400024A RID: 586
		private readonly ReadOnlyCollection<DbModificationClause> _setClauses;

		// Token: 0x0400024B RID: 587
		private readonly DbExpression _returning;
	}
}
