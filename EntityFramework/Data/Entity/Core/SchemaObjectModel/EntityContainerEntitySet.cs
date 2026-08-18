using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000361 RID: 865
	internal sealed class EntityContainerEntitySet : SchemaElement
	{
		// Token: 0x06001EFA RID: 7930 RVA: 0x00094176 File Offset: 0x00092376
		public EntityContainerEntitySet(EntityContainer parentElement) : base(parentElement, null)
		{
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00094180 File Offset: 0x00092380
		public override string FQName
		{
			get
			{
				return base.ParentElement.Name + "." + this.Name;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0009419D File Offset: 0x0009239D
		public SchemaEntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x000941A5 File Offset: 0x000923A5
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x000941AD File Offset: 0x000923AD
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x000941B5 File Offset: 0x000923B5
		public string DefiningQuery
		{
			get
			{
				if (this._definingQueryElement != null)
				{
					return this._definingQueryElement.Query;
				}
				return null;
			}
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000941CC File Offset: 0x000923CC
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				if (base.CanHandleElement(reader, "DefiningQuery"))
				{
					this.HandleDefiningQueryElement(reader);
					return true;
				}
			}
			else if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "ValueAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00094248 File Offset: 0x00092448
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntityType"))
			{
				this.HandleEntityTypeAttribute(reader);
				return true;
			}
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				if (SchemaElement.CanHandleAttribute(reader, "Schema"))
				{
					this.HandleDbSchemaAttribute(reader);
					return true;
				}
				if (SchemaElement.CanHandleAttribute(reader, "Table"))
				{
					this.HandleTableAttribute(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000942B4 File Offset: 0x000924B4
		private void HandleDefiningQueryElement(XmlReader reader)
		{
			EntityContainerEntitySetDefiningQuery entityContainerEntitySetDefiningQuery = new EntityContainerEntitySetDefiningQuery(this);
			entityContainerEntitySetDefiningQuery.Parse(reader);
			this._definingQueryElement = entityContainerEntitySetDefiningQuery;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000942D6 File Offset: 0x000924D6
		protected override void HandleNameAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this.Name = reader.Value;
				return;
			}
			base.HandleNameAttribute(reader);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000942FC File Offset: 0x000924FC
		private void HandleEntityTypeAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this._unresolvedEntityTypeName);
			if (returnValue.Succeeded)
			{
				this._unresolvedEntityTypeName = returnValue.Value;
			}
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0009432B File Offset: 0x0009252B
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00094339 File Offset: 0x00092539
		private void HandleTableAttribute(XmlReader reader)
		{
			this._table = reader.Value;
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00094348 File Offset: 0x00092548
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._entityType == null)
			{
				SchemaType schemaType = null;
				if (!base.Schema.ResolveTypeName(this, this._unresolvedEntityTypeName, out schemaType))
				{
					return;
				}
				this._entityType = (schemaType as SchemaEntityType);
				if (this._entityType == null)
				{
					base.AddError(ErrorCode.InvalidPropertyType, EdmSchemaErrorSeverity.Error, Strings.InvalidEntitySetType(this._unresolvedEntityTypeName));
				}
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000943A4 File Offset: 0x000925A4
		internal override void Validate()
		{
			base.Validate();
			if (this._entityType.KeyProperties.Count == 0)
			{
				base.AddError(ErrorCode.EntitySetTypeHasNoKeys, EdmSchemaErrorSeverity.Error, Strings.EntitySetTypeHasNoKeys(this.Name, this._entityType.FQName));
			}
			if (this._definingQueryElement != null)
			{
				this._definingQueryElement.Validate();
				if (this.DbSchema != null || this.Table != null)
				{
					base.AddError(ErrorCode.TableAndSchemaAreMutuallyExclusiveWithDefiningQuery, EdmSchemaErrorSeverity.Error, Strings.TableAndSchemaAreMutuallyExclusiveWithDefiningQuery(this.FQName));
				}
			}
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00094428 File Offset: 0x00092628
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new EntityContainerEntitySet((EntityContainer)parentElement)
			{
				_definingQueryElement = this._definingQueryElement,
				_entityType = this._entityType,
				_schema = this._schema,
				_table = this._table,
				Name = this.Name
			};
		}

		// Token: 0x04000A89 RID: 2697
		private SchemaEntityType _entityType;

		// Token: 0x04000A8A RID: 2698
		private string _unresolvedEntityTypeName;

		// Token: 0x04000A8B RID: 2699
		private string _schema;

		// Token: 0x04000A8C RID: 2700
		private string _table;

		// Token: 0x04000A8D RID: 2701
		private EntityContainerEntitySetDefiningQuery _definingQueryElement;
	}
}
