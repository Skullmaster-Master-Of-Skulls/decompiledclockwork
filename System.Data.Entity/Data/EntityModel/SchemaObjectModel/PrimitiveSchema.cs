using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x020002FD RID: 765
	internal class PrimitiveSchema : Schema
	{
		// Token: 0x06002D6D RID: 11629 RVA: 0x000AC1E4 File Offset: 0x000AA3E4
		public PrimitiveSchema(SchemaManager schemaManager) : base(schemaManager)
		{
			base.Schema = this;
			DbProviderManifest providerManifest = base.ProviderManifest;
			if (providerManifest == null)
			{
				base.AddError(new EdmSchemaError(Strings.FailedToRetrieveProviderManifest, 168, EdmSchemaErrorSeverity.Error));
				return;
			}
			IList<PrimitiveType> list = providerManifest.GetStoreTypes();
			if (schemaManager.DataModel == SchemaDataModelOption.EntityDataModel && schemaManager.SchemaVersion < 3.0)
			{
				list = (from t in list
				where !Helper.IsSpatialType(t)
				select t).ToList<PrimitiveType>();
			}
			foreach (PrimitiveType primitiveType in list)
			{
				base.TryAddType(new ScalarType(this, primitiveType.Name, primitiveType), false);
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x000AC2B4 File Offset: 0x000AA4B4
		internal override string Alias
		{
			get
			{
				return base.ProviderManifest.NamespaceName;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x000AC2C1 File Offset: 0x000AA4C1
		internal override string Namespace
		{
			get
			{
				if (base.ProviderManifest != null)
				{
					return base.ProviderManifest.NamespaceName;
				}
				return string.Empty;
			}
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000173E2 File Offset: 0x000155E2
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}
	}
}
