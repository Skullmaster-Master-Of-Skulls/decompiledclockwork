using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000021 RID: 33
	[DebuggerDisplay("Name = {Name}")]
	public sealed class ConfigurationElementSchema
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00005D9F File Offset: 0x00004D9F
		internal ConfigurationElementSchema(IAppHostElementSchema schema)
		{
			this._schema = schema;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005DAE File Offset: 0x00004DAE
		public bool AllowUnrecognizedAttributes
		{
			get
			{
				return this._schema.DoesAllowUnschematizedProperties;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005DBC File Offset: 0x00004DBC
		public ConfigurationAttributeSchemaCollection AttributeSchemas
		{
			get
			{
				if (this._attributeSchemas == null)
				{
					IAppHostPropertySchemaCollection propertySchemas = this._schema.PropertySchemas;
					if (propertySchemas != null)
					{
						this._attributeSchemas = new ConfigurationAttributeSchemaCollection(propertySchemas);
					}
				}
				return this._attributeSchemas;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005DF4 File Offset: 0x00004DF4
		public ConfigurationElementSchemaCollection ChildElementSchemas
		{
			get
			{
				if (this._elementSchemas == null)
				{
					IAppHostElementSchemaCollection childElementSchemas = this._schema.ChildElementSchemas;
					if (childElementSchemas != null)
					{
						this._elementSchemas = new ConfigurationElementSchemaCollection(childElementSchemas);
					}
				}
				return this._elementSchemas;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005E2C File Offset: 0x00004E2C
		public ConfigurationCollectionSchema CollectionSchema
		{
			get
			{
				if (this._collectionSchema == null)
				{
					IAppHostCollectionSchema collectionSchema = this._schema.CollectionSchema;
					if (collectionSchema != null)
					{
						this._collectionSchema = new ConfigurationCollectionSchema(collectionSchema);
					}
				}
				return this._collectionSchema;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005E62 File Offset: 0x00004E62
		public bool IsCollectionDefault
		{
			get
			{
				return this._schema.IsCollectionDefault;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005E6F File Offset: 0x00004E6F
		public string Name
		{
			get
			{
				return this._schema.Name;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005E7C File Offset: 0x00004E7C
		public object GetMetadata(string metadataType)
		{
			return this._schema.GetMetadata(metadataType);
		}

		// Token: 0x04000061 RID: 97
		private IAppHostElementSchema _schema;

		// Token: 0x04000062 RID: 98
		private ConfigurationAttributeSchemaCollection _attributeSchemas;

		// Token: 0x04000063 RID: 99
		private ConfigurationElementSchemaCollection _elementSchemas;

		// Token: 0x04000064 RID: 100
		private ConfigurationCollectionSchema _collectionSchema;
	}
}
