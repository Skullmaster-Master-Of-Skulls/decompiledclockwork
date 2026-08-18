using System;

namespace System.Data.Design
{
	// Token: 0x0200023D RID: 573
	internal class DesignRelationCollection : DataSourceCollectionBase
	{
		// Token: 0x060015E7 RID: 5607 RVA: 0x00079CBC File Offset: 0x00077EBC
		public DesignRelationCollection(DesignDataSource dataSource) : base(dataSource)
		{
			this.dataSource = dataSource;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00079CCC File Offset: 0x00077ECC
		private DataSet DataSet
		{
			get
			{
				if (this.dataSource != null)
				{
					return this.dataSource.DataSet;
				}
				return null;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00079CE3 File Offset: 0x00077EE3
		protected override Type ItemType
		{
			get
			{
				return typeof(DesignRelation);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00078388 File Offset: 0x00076588
		protected override INameService NameService
		{
			get
			{
				return DataSetNameService.DefaultInstance;
			}
		}

		// Token: 0x170004FC RID: 1276
		internal DesignRelation this[ForeignKeyConstraint constraint]
		{
			get
			{
				if (constraint == null)
				{
					return null;
				}
				foreach (object obj in this)
				{
					DesignRelation designRelation = (DesignRelation)obj;
					if (designRelation.ForeignKeyConstraint == constraint)
					{
						return designRelation;
					}
				}
				return null;
			}
		}

		// Token: 0x170004FD RID: 1277
		internal DesignRelation this[string name]
		{
			get
			{
				return (DesignRelation)this.FindObject(name);
			}
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(DesignRelation rel)
		{
			base.List.Remove(rel);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(DesignRelation rel)
		{
			return base.List.Add(rel);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(DesignRelation rel)
		{
			return base.List.Contains(rel);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00079D64 File Offset: 0x00077F64
		protected override void OnInsert(int index, object value)
		{
			base.ValidateType(value);
			DesignRelation designRelation = (DesignRelation)value;
			if (this.dataSource != null && designRelation.Owner == this.dataSource)
			{
				return;
			}
			if (this.dataSource != null && designRelation.Owner != null)
			{
				throw new InternalException("This relation belongs to another DataSource already", 20010);
			}
			if (designRelation.Name == null || designRelation.Name.Length == 0)
			{
				designRelation.Name = this.CreateUniqueName(designRelation);
			}
			this.ValidateName(designRelation);
			DataSet dataSet = this.DataSet;
			if (dataSet != null)
			{
				if (designRelation.ForeignKeyConstraint != null)
				{
					ForeignKeyConstraint foreignKeyConstraint = designRelation.ForeignKeyConstraint;
					if (foreignKeyConstraint.Columns.Length != 0)
					{
						DataTable table = foreignKeyConstraint.Columns[0].Table;
						if (table != null && !table.Constraints.Contains(foreignKeyConstraint.ConstraintName))
						{
							table.Constraints.Add(foreignKeyConstraint);
						}
					}
				}
				if (designRelation.DataRelation != null && !dataSet.Relations.Contains(designRelation.DataRelation.RelationName))
				{
					dataSet.Relations.Add(designRelation.DataRelation);
				}
			}
			base.OnInsert(index, value);
			designRelation.Owner = this.dataSource;
		}

		// Token: 0x04000B5C RID: 2908
		private DesignDataSource dataSource;
	}
}
