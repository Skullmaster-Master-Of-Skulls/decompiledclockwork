using System;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000110 RID: 272
	public sealed class DbDeleteCommandTree : DbModificationCommandTree
	{
		// Token: 0x06000722 RID: 1826 RVA: 0x00026DE4 File Offset: 0x00024FE4
		internal DbDeleteCommandTree()
		{
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00026DEC File Offset: 0x00024FEC
		public DbDeleteCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, DbExpression predicate) : base(metadata, dataSpace, target)
		{
			this._predicate = predicate;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00026DFF File Offset: 0x00024FFF
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00026E07 File Offset: 0x00025007
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Delete;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x00026E0A File Offset: 0x0002500A
		internal override bool HasReader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00026E0D File Offset: 0x0002500D
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			base.DumpStructure(dumper);
			if (this.Predicate != null)
			{
				dumper.Dump(this.Predicate, "Predicate");
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00026E2F File Offset: 0x0002502F
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x04000208 RID: 520
		private readonly DbExpression _predicate;
	}
}
