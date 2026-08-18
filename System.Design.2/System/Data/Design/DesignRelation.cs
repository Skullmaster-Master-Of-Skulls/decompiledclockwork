using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Data.Design
{
	// Token: 0x0200023C RID: 572
	internal class DesignRelation : DataSourceComponent, IDataSourceNamedObject, INamedObject
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x00079928 File Offset: 0x00077B28
		public DesignRelation(DataRelation dataRelation)
		{
			this.DataRelation = dataRelation;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00079937 File Offset: 0x00077B37
		public DesignRelation(ForeignKeyConstraint foreignKeyConstraint)
		{
			this.DataRelation = null;
			this.dataForeignKeyConstraint = foreignKeyConstraint;
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0007994D File Offset: 0x00077B4D
		internal DataColumn[] ChildDataColumns
		{
			get
			{
				if (this.dataRelation != null)
				{
					return this.dataRelation.ChildColumns;
				}
				if (this.dataForeignKeyConstraint != null)
				{
					return this.dataForeignKeyConstraint.Columns;
				}
				return new DataColumn[0];
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00079980 File Offset: 0x00077B80
		internal DesignTable ChildDesignTable
		{
			get
			{
				DataTable dataTable = null;
				if (this.dataRelation != null)
				{
					dataTable = this.dataRelation.ChildTable;
				}
				else if (this.dataForeignKeyConstraint != null)
				{
					dataTable = this.dataForeignKeyConstraint.Table;
				}
				if (dataTable != null && this.Owner != null)
				{
					return this.Owner.DesignTables[dataTable];
				}
				return null;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x000799D7 File Offset: 0x00077BD7
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x000799DF File Offset: 0x00077BDF
		internal DataRelation DataRelation
		{
			get
			{
				return this.dataRelation;
			}
			set
			{
				this.dataRelation = value;
				if (this.dataRelation != null)
				{
					this.dataForeignKeyConstraint = null;
				}
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000799F7 File Offset: 0x00077BF7
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x00079A20 File Offset: 0x00077C20
		internal ForeignKeyConstraint ForeignKeyConstraint
		{
			get
			{
				if (this.dataRelation != null && this.dataRelation.ChildKeyConstraint != null)
				{
					return this.dataRelation.ChildKeyConstraint;
				}
				return this.dataForeignKeyConstraint;
			}
			set
			{
				this.dataForeignKeyConstraint = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00079A29 File Offset: 0x00077C29
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x00079A58 File Offset: 0x00077C58
		[MergableProperty(false)]
		[DefaultValue("")]
		public string Name
		{
			get
			{
				if (this.dataRelation != null)
				{
					return this.dataRelation.RelationName;
				}
				if (this.dataForeignKeyConstraint != null)
				{
					return this.dataForeignKeyConstraint.ConstraintName;
				}
				return string.Empty;
			}
			set
			{
				if (!StringUtil.EqualValue(this.Name, value))
				{
					if (this.CollectionParent != null)
					{
						this.CollectionParent.ValidateUniqueName(this, value);
					}
					if (this.dataRelation != null)
					{
						this.dataRelation.RelationName = value;
					}
					if (this.dataForeignKeyConstraint != null)
					{
						this.dataForeignKeyConstraint.ConstraintName = value;
					}
				}
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00079AB0 File Offset: 0x00077CB0
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x00079AB8 File Offset: 0x00077CB8
		internal DesignDataSource Owner
		{
			get
			{
				return this.owner;
			}
			set
			{
				this.owner = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00079AC1 File Offset: 0x00077CC1
		internal DataColumn[] ParentDataColumns
		{
			get
			{
				if (this.dataRelation != null)
				{
					return this.dataRelation.ParentColumns;
				}
				if (this.dataForeignKeyConstraint != null)
				{
					return this.dataForeignKeyConstraint.RelatedColumns;
				}
				return new DataColumn[0];
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00079AF4 File Offset: 0x00077CF4
		internal DesignTable ParentDesignTable
		{
			get
			{
				DataTable dataTable = null;
				if (this.dataRelation != null)
				{
					dataTable = this.dataRelation.ParentTable;
				}
				else if (this.dataForeignKeyConstraint != null)
				{
					dataTable = this.dataForeignKeyConstraint.RelatedTable;
				}
				if (dataTable != null && this.Owner != null)
				{
					return this.Owner.DesignTables[dataTable];
				}
				return null;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x00079B4B File Offset: 0x00077D4B
		[Browsable(false)]
		public string PublicTypeName
		{
			get
			{
				return "Relation";
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00079B52 File Offset: 0x00077D52
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x00079B6E File Offset: 0x00077D6E
		internal string UserRelationName
		{
			get
			{
				return this.dataRelation.ExtendedProperties["Generator_UserRelationName"] as string;
			}
			set
			{
				this.dataRelation.ExtendedProperties["Generator_UserRelationName"] = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00079B86 File Offset: 0x00077D86
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x00079BA2 File Offset: 0x00077DA2
		internal string UserParentTable
		{
			get
			{
				return this.dataRelation.ExtendedProperties["Generator_UserParentTable"] as string;
			}
			set
			{
				this.dataRelation.ExtendedProperties["Generator_UserParentTable"] = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00079BBA File Offset: 0x00077DBA
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00079BD6 File Offset: 0x00077DD6
		internal string UserChildTable
		{
			get
			{
				return this.dataRelation.ExtendedProperties["Generator_UserChildTable"] as string;
			}
			set
			{
				this.dataRelation.ExtendedProperties["Generator_UserChildTable"] = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00079BEE File Offset: 0x00077DEE
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x00079C0A File Offset: 0x00077E0A
		internal string GeneratorRelationVarName
		{
			get
			{
				return this.dataRelation.ExtendedProperties["Generator_RelationVarName"] as string;
			}
			set
			{
				this.dataRelation.ExtendedProperties["Generator_RelationVarName"] = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00079C22 File Offset: 0x00077E22
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x00079C3E File Offset: 0x00077E3E
		internal string GeneratorChildPropName
		{
			get
			{
				return this.dataRelation.ExtendedProperties["Generator_ChildPropName"] as string;
			}
			set
			{
				this.dataRelation.ExtendedProperties["Generator_ChildPropName"] = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00079C56 File Offset: 0x00077E56
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x00079C72 File Offset: 0x00077E72
		internal string GeneratorParentPropName
		{
			get
			{
				return this.dataRelation.ExtendedProperties["Generator_ParentPropName"] as string;
			}
			set
			{
				this.dataRelation.ExtendedProperties["Generator_ParentPropName"] = value;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00079C8C File Offset: 0x00077E8C
		internal override StringCollection NamingPropertyNames
		{
			get
			{
				StringCollection stringCollection = new StringCollection();
				stringCollection.AddRange(new string[]
				{
					"typedParent",
					"typedChildren"
				});
				return stringCollection;
			}
		}

		// Token: 0x04000B52 RID: 2898
		internal const string NAMEROOT = "Relation";

		// Token: 0x04000B53 RID: 2899
		private DesignDataSource owner;

		// Token: 0x04000B54 RID: 2900
		private DataRelation dataRelation;

		// Token: 0x04000B55 RID: 2901
		private ForeignKeyConstraint dataForeignKeyConstraint;

		// Token: 0x04000B56 RID: 2902
		private const string EXTPROPNAME_USER_RELATIONNAME = "Generator_UserRelationName";

		// Token: 0x04000B57 RID: 2903
		private const string EXTPROPNAME_USER_PARENTTABLE = "Generator_UserParentTable";

		// Token: 0x04000B58 RID: 2904
		private const string EXTPROPNAME_USER_CHILDTABLE = "Generator_UserChildTable";

		// Token: 0x04000B59 RID: 2905
		private const string EXTPROPNAME_GENERATOR_RELATIONVARNAME = "Generator_RelationVarName";

		// Token: 0x04000B5A RID: 2906
		private const string EXTPROPNAME_GENERATOR_PARENTPROPNAME = "Generator_ParentPropName";

		// Token: 0x04000B5B RID: 2907
		private const string EXTPROPNAME_GENERATOR_CHILDPROPNAME = "Generator_ChildPropName";

		// Token: 0x020004BD RID: 1213
		[Flags]
		public enum CompareOption
		{
			// Token: 0x04001E9E RID: 7838
			Columns = 0,
			// Token: 0x04001E9F RID: 7839
			Tables = 1,
			// Token: 0x04001EA0 RID: 7840
			ForeignKeyConstraints = 2
		}
	}
}
