using System;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200001F RID: 31
	public sealed class ConfigurationCollectionSchema
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00005B2B File Offset: 0x00004B2B
		internal ConfigurationCollectionSchema(IAppHostCollectionSchema schema)
		{
			this._schema = schema;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00005B3A File Offset: 0x00004B3A
		public string AddElementNames
		{
			get
			{
				return this._schema.AddElementNames;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005B47 File Offset: 0x00004B47
		public bool AllowDuplicates
		{
			get
			{
				return this._schema.DoesAllowDuplicates;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005B54 File Offset: 0x00004B54
		public string ClearElementName
		{
			get
			{
				ConfigurationElementSchema clearElementSchema = this.GetClearElementSchema();
				if (clearElementSchema == null)
				{
					return null;
				}
				return clearElementSchema.Name;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005B73 File Offset: 0x00004B73
		public bool IsMergeAppend
		{
			get
			{
				return this._schema.IsMergeAppend;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005B80 File Offset: 0x00004B80
		public string RemoveElementName
		{
			get
			{
				ConfigurationElementSchema removeElementSchema = this.GetRemoveElementSchema();
				if (removeElementSchema == null)
				{
					return null;
				}
				return removeElementSchema.Name;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005BA0 File Offset: 0x00004BA0
		public ConfigurationElementSchema GetAddElementSchema(string elementName)
		{
			IAppHostElementSchema addElementSchema = this._schema.GetAddElementSchema(elementName);
			if (addElementSchema != null)
			{
				return new ConfigurationElementSchema(addElementSchema);
			}
			return null;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005BC8 File Offset: 0x00004BC8
		public ConfigurationElementSchema GetClearElementSchema()
		{
			if (this._clearElementSchema == null)
			{
				IAppHostElementSchema clearElementSchema = this._schema.ClearElementSchema;
				if (clearElementSchema != null)
				{
					this._clearElementSchema = new ConfigurationElementSchema(clearElementSchema);
				}
			}
			return this._clearElementSchema;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005C00 File Offset: 0x00004C00
		public ConfigurationElementSchema GetRemoveElementSchema()
		{
			if (this._removeElementSchema == null)
			{
				IAppHostElementSchema removeElementSchema = this._schema.RemoveElementSchema;
				if (removeElementSchema != null)
				{
					this._removeElementSchema = new ConfigurationElementSchema(removeElementSchema);
				}
			}
			return this._removeElementSchema;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005C36 File Offset: 0x00004C36
		public object GetMetadata(string metadataType)
		{
			return this._schema.GetMetadata(metadataType);
		}

		// Token: 0x0400005C RID: 92
		private IAppHostCollectionSchema _schema;

		// Token: 0x0400005D RID: 93
		private ConfigurationElementSchema _clearElementSchema;

		// Token: 0x0400005E RID: 94
		private ConfigurationElementSchema _removeElementSchema;
	}
}
