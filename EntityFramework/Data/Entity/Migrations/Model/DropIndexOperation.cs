using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x0200070C RID: 1804
	public class DropIndexOperation : IndexOperation
	{
		// Token: 0x06004931 RID: 18737 RVA: 0x0015F100 File Offset: 0x0015D300
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropIndexOperation(object anonymousArguments = null) : base(anonymousArguments)
		{
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x0015F109 File Offset: 0x0015D309
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropIndexOperation(CreateIndexOperation inverse, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotNull<CreateIndexOperation>(inverse, "inverse");
			this._inverse = inverse;
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x0015F125 File Offset: 0x0015D325
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x0015F12D File Offset: 0x0015D32D
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B34 RID: 6964
		private readonly CreateIndexOperation _inverse;
	}
}
