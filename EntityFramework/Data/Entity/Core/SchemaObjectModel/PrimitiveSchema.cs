using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000379 RID: 889
	internal class PrimitiveSchema : Schema
	{
		// Token: 0x06002011 RID: 8209 RVA: 0x000980F0 File Offset: 0x000962F0
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

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x000981C0 File Offset: 0x000963C0
		internal override string Alias
		{
			get
			{
				return base.ProviderManifest.NamespaceName;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x000981CD File Offset: 0x000963CD
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

		// Token: 0x06002014 RID: 8212 RVA: 0x000981E8 File Offset: 0x000963E8
		protected override bool HandleAttribute(XmlReader reader)
		{
			return false;
		}
	}
}
