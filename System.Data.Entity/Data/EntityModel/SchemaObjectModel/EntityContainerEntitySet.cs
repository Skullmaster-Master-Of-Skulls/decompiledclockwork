using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002E6 RID: 742
	internal sealed class EntityContainerEntitySet : SchemaElement
	{
		// Token: 0x06002C88 RID: 11400 RVA: 0x000A9632 File Offset: 0x000A7832
		public EntityContainerEntitySet(EntityContainer parentElement) : base(parentElement)
		{
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000A963B File Offset: 0x000A783B
		public override string FQName
		{
			get
			{
				return base.ParentElement.Name + "." + this.Name;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000A9658 File Offset: 0x000A7858
		public SchemaEntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000A9660 File Offset: 0x000A7860
		public string DbSchema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000A9668 File Offset: 0x000A7868
		public string Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000A9670 File Offset: 0x000A7870
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

		// Token: 0x06002C8E RID: 11406 RVA: 0x000A9688 File Offset: 0x000A7888
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
					base.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					base.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000A9704 File Offset: 0x000A7904
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

		// Token: 0x06002C90 RID: 11408 RVA: 0x000A9770 File Offset: 0x000A7970
		private void HandleDefiningQueryElement(XmlReader reader)
		{
			EntityContainerEntitySetDefiningQuery entityContainerEntitySetDefiningQuery = new EntityContainerEntitySetDefiningQuery(this);
			entityContainerEntitySetDefiningQuery.Parse(reader);
			this._definingQueryElement = entityContainerEntitySetDefiningQuery;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000A9792 File Offset: 0x000A7992
		protected override void HandleNameAttribute(XmlReader reader)
		{
			if (base.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
			{
				this.Name = reader.Value;
				return;
			}
			base.HandleNameAttribute(reader);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000A97B8 File Offset: 0x000A79B8
		private void HandleEntityTypeAttribute(XmlReader reader)
		{
			ReturnValue<string> returnValue = base.HandleDottedNameAttribute(reader, this._unresolvedEntityTypeName, new Func<object, string>(Strings.PropertyTypeAlreadyDefined));
			if (returnValue.Succeeded)
			{
				this._unresolvedEntityTypeName = returnValue.Value;
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000A97F3 File Offset: 0x000A79F3
		private void HandleDbSchemaAttribute(XmlReader reader)
		{
			this._schema = reader.Value;
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000A9801 File Offset: 0x000A7A01
		private void HandleTableAttribute(XmlReader reader)
		{
			this._table = reader.Value;
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000A9810 File Offset: 0x000A7A10
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
					return;
				}
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000A9870 File Offset: 0x000A7A70
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

		// Token: 0x06002C97 RID: 11415 RVA: 0x000A98F4 File Offset: 0x000A7AF4
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

		// Token: 0x0400130D RID: 4877
		private SchemaEntityType _entityType;

		// Token: 0x0400130E RID: 4878
		private string _unresolvedEntityTypeName;

		// Token: 0x0400130F RID: 4879
		private string _schema;

		// Token: 0x04001310 RID: 4880
		private string _table;

		// Token: 0x04001311 RID: 4881
		private EntityContainerEntitySetDefiningQuery _definingQueryElement;
	}
}
