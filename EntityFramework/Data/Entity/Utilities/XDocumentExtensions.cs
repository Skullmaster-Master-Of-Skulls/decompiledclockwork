using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations.Edm;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006F1 RID: 1777
	internal static class XDocumentExtensions
	{
		// Token: 0x0600472F RID: 18223 RVA: 0x00150F9C File Offset: 0x0014F19C
		public static StorageMappingItemCollection GetStorageMappingItemCollection(this XDocument model, out DbProviderInfo providerInfo)
		{
			EdmItemCollection edmCollection = new EdmItemCollection(new XmlReader[]
			{
				model.Descendants(EdmXNames.Csdl.SchemaNames).Single<XElement>().CreateReader()
			});
			XElement xelement = model.Descendants(EdmXNames.Ssdl.SchemaNames).Single<XElement>();
			providerInfo = new DbProviderInfo(xelement.ProviderAttribute(), xelement.ProviderManifestTokenAttribute());
			StoreItemCollection storeCollection = new StoreItemCollection(new XmlReader[]
			{
				xelement.CreateReader()
			});
			return new StorageMappingItemCollection(edmCollection, storeCollection, new XmlReader[]
			{
				new XElement(model.Descendants(EdmXNames.Msl.MappingNames).Single<XElement>()).CreateReader()
			});
		}
	}
}
