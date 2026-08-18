using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x0200070A RID: 1802
	public class DropColumnOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06004922 RID: 18722 RVA: 0x0015EF8B File Offset: 0x0015D18B
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropColumnOperation(string table, string name, object anonymousArguments = null) : this(table, name, null, null, anonymousArguments)
		{
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x0015EF98 File Offset: 0x0015D198
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropColumnOperation(string table, string name, IDictionary<string, object> removedAnnotations, object anonymousArguments = null) : this(table, name, removedAnnotations, null, anonymousArguments)
		{
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x0015EFA6 File Offset: 0x0015D1A6
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropColumnOperation(string table, string name, AddColumnOperation inverse, object anonymousArguments = null) : this(table, name, null, inverse, anonymousArguments)
		{
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x0015EFB4 File Offset: 0x0015D1B4
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropColumnOperation(string table, string name, IDictionary<string, object> removedAnnotations, AddColumnOperation inverse, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(table, "table");
			Check.NotEmpty(name, "name");
			this._table = table;
			this._name = name;
			this._removedAnnotations = (removedAnnotations ?? new Dictionary<string, object>());
			this._inverse = inverse;
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06004926 RID: 18726 RVA: 0x0015F007 File Offset: 0x0015D207
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x0015F00F File Offset: 0x0015D20F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06004928 RID: 18728 RVA: 0x0015F017 File Offset: 0x0015D217
		public IDictionary<string, object> RemovedAnnotations
		{
			get
			{
				return this._removedAnnotations;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x0015F01F File Offset: 0x0015D21F
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600492A RID: 18730 RVA: 0x0015F027 File Offset: 0x0015D227
		public override bool IsDestructiveChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x0015F02C File Offset: 0x0015D22C
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				AddColumnOperation addColumnOperation = this.Inverse as AddColumnOperation;
				return this.RemovedAnnotations.Any<KeyValuePair<string, object>>() || (addColumnOperation != null && ((IAnnotationTarget)addColumnOperation).HasAnnotations);
			}
		}

		// Token: 0x04001B2F RID: 6959
		private readonly string _table;

		// Token: 0x04001B30 RID: 6960
		private readonly string _name;

		// Token: 0x04001B31 RID: 6961
		private readonly AddColumnOperation _inverse;

		// Token: 0x04001B32 RID: 6962
		private readonly IDictionary<string, object> _removedAnnotations;
	}
}
