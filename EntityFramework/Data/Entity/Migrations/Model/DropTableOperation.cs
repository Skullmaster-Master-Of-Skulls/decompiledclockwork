using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x0200070E RID: 1806
	public class DropTableOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06004939 RID: 18745 RVA: 0x0015F1BB File Offset: 0x0015D3BB
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropTableOperation(string name, object anonymousArguments = null) : this(name, null, null, null, anonymousArguments)
		{
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x0015F1C8 File Offset: 0x0015D3C8
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DropTableOperation(string name, IDictionary<string, object> removedAnnotations, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, object anonymousArguments = null) : this(name, removedAnnotations, removedColumnAnnotations, null, anonymousArguments)
		{
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x0015F1D6 File Offset: 0x0015D3D6
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public DropTableOperation(string name, CreateTableOperation inverse, object anonymousArguments = null) : this(name, null, null, inverse, anonymousArguments)
		{
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0015F1E4 File Offset: 0x0015D3E4
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DropTableOperation(string name, IDictionary<string, object> removedAnnotations, IDictionary<string, IDictionary<string, object>> removedColumnAnnotations, CreateTableOperation inverse, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._removedAnnotations = (removedAnnotations ?? new Dictionary<string, object>());
			this._removedColumnAnnotations = (removedColumnAnnotations ?? new Dictionary<string, IDictionary<string, object>>());
			this._inverse = inverse;
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x0015F234 File Offset: 0x0015D434
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x0015F23C File Offset: 0x0015D43C
		public virtual IDictionary<string, object> RemovedAnnotations
		{
			get
			{
				return this._removedAnnotations;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600493F RID: 18751 RVA: 0x0015F244 File Offset: 0x0015D444
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public IDictionary<string, IDictionary<string, object>> RemovedColumnAnnotations
		{
			get
			{
				return this._removedColumnAnnotations;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06004940 RID: 18752 RVA: 0x0015F24C File Offset: 0x0015D44C
		public override MigrationOperation Inverse
		{
			get
			{
				return this._inverse;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x0015F254 File Offset: 0x0015D454
		public override bool IsDestructiveChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x0015F258 File Offset: 0x0015D458
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				CreateTableOperation createTableOperation = this.Inverse as CreateTableOperation;
				return this.RemovedAnnotations.Any<KeyValuePair<string, object>>() || this.RemovedColumnAnnotations.Any<KeyValuePair<string, IDictionary<string, object>>>() || (createTableOperation != null && ((IAnnotationTarget)createTableOperation).HasAnnotations);
			}
		}

		// Token: 0x04001B36 RID: 6966
		private readonly string _name;

		// Token: 0x04001B37 RID: 6967
		private readonly CreateTableOperation _inverse;

		// Token: 0x04001B38 RID: 6968
		private readonly IDictionary<string, IDictionary<string, object>> _removedColumnAnnotations;

		// Token: 0x04001B39 RID: 6969
		private readonly IDictionary<string, object> _removedAnnotations;
	}
}
