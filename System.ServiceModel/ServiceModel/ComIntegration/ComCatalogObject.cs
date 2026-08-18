using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001DC RID: 476
	internal class ComCatalogObject
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x00036859 File Offset: 0x00034A59
		public ComCatalogObject(ICatalogObject catalogObject, ICatalogCollection catalogCollection)
		{
			this.catalogObject = catalogObject;
			this.catalogCollection = catalogCollection;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003686F File Offset: 0x00034A6F
		public object GetValue(string key)
		{
			return this.catalogObject.GetValue(key);
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003687D File Offset: 0x00034A7D
		public string Name
		{
			get
			{
				return (string)this.catalogObject.Name();
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00036890 File Offset: 0x00034A90
		public ComCatalogCollection GetCollection(string collectionName)
		{
			ICatalogCollection catalogCollection = (ICatalogCollection)this.catalogCollection.GetCollection(collectionName, this.catalogObject.Key());
			catalogCollection.Populate();
			return new ComCatalogCollection(catalogCollection);
		}

		// Token: 0x040017BC RID: 6076
		private ICatalogObject catalogObject;

		// Token: 0x040017BD RID: 6077
		private ICatalogCollection catalogCollection;
	}
}
