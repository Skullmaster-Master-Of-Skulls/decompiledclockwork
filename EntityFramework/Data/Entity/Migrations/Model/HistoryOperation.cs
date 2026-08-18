using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x0200070F RID: 1807
	public class HistoryOperation : MigrationOperation
	{
		// Token: 0x06004943 RID: 18755 RVA: 0x0015F298 File Offset: 0x0015D498
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public HistoryOperation(IList<DbModificationCommandTree> commandTrees, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotNull<IList<DbModificationCommandTree>>(commandTrees, "commandTrees");
			if (!commandTrees.Any<DbModificationCommandTree>())
			{
				throw new ArgumentException(Strings.CollectionEmpty("commandTrees", "HistoryOperation"));
			}
			this._commandTrees = commandTrees;
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x0015F2D1 File Offset: 0x0015D4D1
		public IList<DbModificationCommandTree> CommandTrees
		{
			get
			{
				return this._commandTrees;
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x0015F2D9 File Offset: 0x0015D4D9
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B3A RID: 6970
		private readonly IList<DbModificationCommandTree> _commandTrees;
	}
}
