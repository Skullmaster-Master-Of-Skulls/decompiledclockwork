using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001DD RID: 477
	internal class ComCatalogCollection
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x000368C6 File Offset: 0x00034AC6
		public ComCatalogCollection(ICatalogCollection catalogCollection)
		{
			this.catalogCollection = catalogCollection;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x000368D5 File Offset: 0x00034AD5
		public int Count
		{
			get
			{
				return this.catalogCollection.Count();
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000368E4 File Offset: 0x00034AE4
		public ComCatalogObject Item(int index)
		{
			ICatalogObject catalogObject = (ICatalogObject)this.catalogCollection.Item(index);
			return new ComCatalogObject(catalogObject, this.catalogCollection);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003690F File Offset: 0x00034B0F
		public ComCatalogCollection.Enumerator GetEnumerator()
		{
			return new ComCatalogCollection.Enumerator(this);
		}

		// Token: 0x040017BE RID: 6078
		private ICatalogCollection catalogCollection;

		// Token: 0x02000B0B RID: 2827
		public struct Enumerator
		{
			// Token: 0x06006F63 RID: 28515 RVA: 0x0019DA27 File Offset: 0x0019BC27
			public Enumerator(ComCatalogCollection collection)
			{
				this.collection = collection;
				this.current = null;
				this.count = -1;
			}

			// Token: 0x170019FA RID: 6650
			// (get) Token: 0x06006F64 RID: 28516 RVA: 0x0019DA3E File Offset: 0x0019BC3E
			public ComCatalogObject Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06006F65 RID: 28517 RVA: 0x0019DA46 File Offset: 0x0019BC46
			public bool MoveNext()
			{
				this.count++;
				if (this.count >= this.collection.Count)
				{
					return false;
				}
				this.current = this.collection.Item(this.count);
				return true;
			}

			// Token: 0x06006F66 RID: 28518 RVA: 0x0019DA83 File Offset: 0x0019BC83
			public void Reset()
			{
				this.count = -1;
			}

			// Token: 0x04003F96 RID: 16278
			private ComCatalogCollection collection;

			// Token: 0x04003F97 RID: 16279
			private ComCatalogObject current;

			// Token: 0x04003F98 RID: 16280
			private int count;
		}
	}
}
