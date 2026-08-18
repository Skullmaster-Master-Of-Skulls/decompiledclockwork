using System;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003E5 RID: 997
	public sealed class DbDeleteCommandTree : DbModificationCommandTree
	{
		// Token: 0x06003563 RID: 13667 RVA: 0x000CFC4F File Offset: 0x000CDE4F
		internal DbDeleteCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target, DbExpression predicate) : base(metadata, dataSpace, target)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(predicate, "predicate");
			this._predicate = predicate;
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000CFC6F File Offset: 0x000CDE6F
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x0003BF8C File Offset: 0x0003A18C
		internal override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Delete;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003566 RID: 13670 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override bool HasReader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000CFC77 File Offset: 0x000CDE77
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			base.DumpStructure(dumper);
			if (this.Predicate != null)
			{
				dumper.Dump(this.Predicate, "Predicate");
			}
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000CFC99 File Offset: 0x000CDE99
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x040017AA RID: 6058
		private readonly DbExpression _predicate;
	}
}
