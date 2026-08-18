using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000119 RID: 281
	public sealed class DbUpdateCommandTree : DbModificationCommandTree
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x000282CB File Offset: 0x000264CB
		internal DbUpdateCommandTree()
		{
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000282D3 File Offset: 0x000264D3
		public DbUpdateCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, DbExpression predicate, ReadOnlyCollection<DbModificationClause> setClauses, DbExpression returning) : base(metadata, dataSpace, target)
		{
			this._predicate = predicate;
			this._setClauses = setClauses;
			this._returning = returning;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x000282F6 File Offset: 0x000264F6
		public IList<DbModificationClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x000282FE File Offset: 0x000264FE
		public DbExpression Returning
		{
			get
			{
				return this._returning;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x00028306 File Offset: 0x00026506
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0002830E File Offset: 0x0002650E
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Update;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00028311 File Offset: 0x00026511
		internal override bool HasReader
		{
			get
			{
				return null != this.Returning;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00028320 File Offset: 0x00026520
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

		// Token: 0x0600076A RID: 1898 RVA: 0x000283B8 File Offset: 0x000265B8
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x04000254 RID: 596
		private readonly DbExpression _predicate;

		// Token: 0x04000255 RID: 597
		private readonly DbExpression _returning;

		// Token: 0x04000256 RID: 598
		private readonly ReadOnlyCollection<DbModificationClause> _setClauses;
	}
}
