using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003E8 RID: 1000
	public sealed class DbInsertCommandTree : DbModificationCommandTree
	{
		// Token: 0x060035C1 RID: 13761 RVA: 0x000CFCA2 File Offset: 0x000CDEA2
		internal DbInsertCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, ReadOnlyCollection<DbModificationClause> setClauses, DbExpression returning) : base(metadata, dataSpace, target)
		{
			EntityUtil.CheckArgumentNull<ReadOnlyCollection<DbModificationClause>>(setClauses, "setClauses");
			this._setClauses = setClauses;
			this._returning = returning;
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x000CFCCA File Offset: 0x000CDECA
		public IList<DbModificationClause> SetClauses
		{
			get
			{
				return this._setClauses;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000CFCD2 File Offset: 0x000CDED2
		public DbExpression Returning
		{
			get
			{
				return this._returning;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x00033532 File Offset: 0x00031732
		internal override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Insert;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000CFCDA File Offset: 0x000CDEDA
		internal override bool HasReader
		{
			get
			{
				return this.Returning != null;
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000CFCE8 File Offset: 0x000CDEE8
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

		// Token: 0x060035C7 RID: 13767 RVA: 0x000CFD70 File Offset: 0x000CDF70
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x040017AB RID: 6059
		private readonly ReadOnlyCollection<DbModificationClause> _setClauses;

		// Token: 0x040017AC RID: 6060
		private readonly DbExpression _returning;
	}
}
