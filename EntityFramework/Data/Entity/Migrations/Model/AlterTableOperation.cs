using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001A9 RID: 425
	public class AlterTableOperation : MigrationOperation, IAnnotationTarget
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x0003EF4C File Offset: 0x0003D14C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public AlterTableOperation(string name, IDictionary<string, AnnotationValues> annotations, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._annotations = (annotations ?? new Dictionary<string, AnnotationValues>());
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0003EF83 File Offset: 0x0003D183
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0003EF8B File Offset: 0x0003D18B
		public virtual IList<ColumnModel> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0003EF93 File Offset: 0x0003D193
		public virtual IDictionary<string, AnnotationValues> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0003EFC4 File Offset: 0x0003D1C4
		public override MigrationOperation Inverse
		{
			get
			{
				AlterTableOperation alterTableOperation = new AlterTableOperation(this.Name, this.Annotations.ToDictionary((KeyValuePair<string, AnnotationValues> a) => a.Key, (KeyValuePair<string, AnnotationValues> a) => new AnnotationValues(a.Value.NewValue, a.Value.OldValue)), null);
				alterTableOperation._columns.AddRange(this._columns);
				return alterTableOperation;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x0003F035 File Offset: 0x0003D235
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0003F040 File Offset: 0x0003D240
		bool IAnnotationTarget.HasAnnotations
		{
			get
			{
				if (!this.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
				{
					return this.Columns.SelectMany((ColumnModel c) => c.Annotations).Any<KeyValuePair<string, AnnotationValues>>();
				}
				return true;
			}
		}

		// Token: 0x040003D7 RID: 983
		private readonly string _name;

		// Token: 0x040003D8 RID: 984
		private readonly List<ColumnModel> _columns = new List<ColumnModel>();

		// Token: 0x040003D9 RID: 985
		private readonly IDictionary<string, AnnotationValues> _annotations;
	}
}
