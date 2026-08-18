using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003ED RID: 1005
	public sealed class DbUpdateCommandTree : DbModificationCommandTree
	{
		// Token: 0x060035DD RID: 13789 RVA: 0x000D0013 File Offset: 0x000CE213
		internal DbUpdateCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, DbExpression predicate, ReadOnlyCollection<DbModificationClause> setClauses, DbExpression returning) : base(metadata, dataSpace, target)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(predicate, "predicate");
			EntityUtil.CheckArgumentNull<ReadOnlyCollection<DbModificationClause>>(setClauses, "setClauses");
			this._predicate = predicate;
			this._setClauses = setClauses;
			this._returning = returning;
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060035DE RID: 13790 RVA: 0x000D0050 File Offset: 0x000CE250
		public IList<DbModificationClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x000D0058 File Offset: 0x000CE258
		public DbExpression Returning
		{
			get
			{
				return this._returning;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x000D0060 File Offset: 0x000CE260
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x00017938 File Offset: 0x00015B38
		internal override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Update;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x000D0068 File Offset: 0x000CE268
		internal override bool HasReader
		{
			get
			{
				return this.Returning != null;
			}
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000D0074 File Offset: 0x000CE274
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			base.DumpStructure(dumper);
			if (this.Predicate != null)
			{
				dumper.Dump(this.Predicate, "Predicate");
			}
			dumper.Begin("SetClauses", null);
			foreach (DbModificationClause dbModificationClause in this.SetClauses)
			{
				if (dbModificationClause != null)
				{
					dbModificationClause.DumpStructure(dumper);
				}
			}
			dumper.End("SetClauses");
			dumper.Dump(this.Returning, "Returning");
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000D010C File Offset: 0x000CE30C
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x040017B3 RID: 6067
		private readonly DbExpression _predicate;

		// Token: 0x040017B4 RID: 6068
		private readonly DbExpression _returning;

		// Token: 0x040017B5 RID: 6069
		private readonly ReadOnlyCollection<DbModificationClause> _setClauses;
	}
}
