using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000709 RID: 1801
	public class CreateTableOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06004912 RID: 18706 RVA: 0x0015EDAF File Offset: 0x0015CFAF
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public CreateTableOperation(string name, object anonymousArguments = null) : this(name, null, anonymousArguments)
		{
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x0015EDBA File Offset: 0x0015CFBA
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public CreateTableOperation(string name, IDictionary<string, object> annotations, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._annotations = (annotations ?? new Dictionary<string, object>());
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x0015EDF1 File Offset: 0x0015CFF1
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06004915 RID: 18709 RVA: 0x0015EDF9 File Offset: 0x0015CFF9
		public virtual IList<ColumnModel> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06004916 RID: 18710 RVA: 0x0015EE01 File Offset: 0x0015D001
		// (set) Token: 0x06004917 RID: 18711 RVA: 0x0015EE09 File Offset: 0x0015D009
		public AddPrimaryKeyOperation PrimaryKey
		{
			get
			{
				return this._primaryKey;
			}
			set
			{
				Check.NotNull<AddPrimaryKeyOperation>(value, "value");
				this._primaryKey = value;
				this._primaryKey.Table = this.Name;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x0015EE2F File Offset: 0x0015D02F
		public virtual IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004919 RID: 18713 RVA: 0x0015EEBC File Offset: 0x0015D0BC
		public override MigrationOperation Inverse
		{
			get
			{
				return new DropTableOperation(this.Name, this.Annotations, (from c in this.Columns
				where c.Annotations.Count > 0
				select c).ToDictionary((ColumnModel c) => c.Name, (ColumnModel c) => c.Annotations.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => a.Value.NewValue)), null);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x0015EF42 File Offset: 0x0015D142
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600491B RID: 18715 RVA: 0x0015EF4D File Offset: 0x0015D14D
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				if (!this.Annotations.Any<KeyValuePair<string, object>>())
				{
					return this.Columns.SelectMany((ColumnModel c) => c.Annotations).Any<KeyValuePair<string, AnnotationValues>>();
				}
				return true;
			}
		}

		// Token: 0x04001B25 RID: 6949
		private readonly string _name;

		// Token: 0x04001B26 RID: 6950
		private readonly List<ColumnModel> _columns = new List<ColumnModel>();

		// Token: 0x04001B27 RID: 6951
		private AddPrimaryKeyOperation _primaryKey;

		// Token: 0x04001B28 RID: 6952
		private readonly IDictionary<string, object> _annotations;
	}
}
