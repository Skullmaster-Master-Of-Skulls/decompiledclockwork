using System;
using System.Collections;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200052D RID: 1325
	public sealed class CatalogPartCollection : ReadOnlyCollectionBase
	{
		// Token: 0x06004325 RID: 17189 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public CatalogPartCollection()
		{
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x000DCEFA File Offset: 0x000DB0FA
		public CatalogPartCollection(ICollection catalogParts)
		{
			this.Initialize(null, catalogParts);
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000DCF0A File Offset: 0x000DB10A
		public CatalogPartCollection(CatalogPartCollection existingCatalogParts, ICollection catalogParts)
		{
			this.Initialize(existingCatalogParts, catalogParts);
		}

		// Token: 0x170013AE RID: 5038
		public CatalogPart this[int index]
		{
			get
			{
				return (CatalogPart)base.InnerList[index];
			}
		}

		// Token: 0x170013AF RID: 5039
		public CatalogPart this[string id]
		{
			get
			{
				foreach (object obj in base.InnerList)
				{
					CatalogPart catalogPart = (CatalogPart)obj;
					if (string.Equals(catalogPart.ID, id, StringComparison.OrdinalIgnoreCase))
					{
						return catalogPart;
					}
				}
				return null;
			}
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x000DCF98 File Offset: 0x000DB198
		internal int Add(CatalogPart value)
		{
			return base.InnerList.Add(value);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(CatalogPart catalogPart)
		{
			return base.InnerList.Contains(catalogPart);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(CatalogPart[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(CatalogPart catalogPart)
		{
			return base.InnerList.IndexOf(catalogPart);
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x000DCFB8 File Offset: 0x000DB1B8
		private void Initialize(CatalogPartCollection existingCatalogParts, ICollection catalogParts)
		{
			if (existingCatalogParts != null)
			{
				foreach (object obj in existingCatalogParts)
				{
					CatalogPart value = (CatalogPart)obj;
					base.InnerList.Add(value);
				}
			}
			if (catalogParts != null)
			{
				foreach (object obj2 in catalogParts)
				{
					if (obj2 == null)
					{
						throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "catalogParts");
					}
					if (!(obj2 is CatalogPart))
					{
						throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
						{
							"CatalogPart"
						}), "catalogParts");
					}
					base.InnerList.Add(obj2);
				}
			}
		}

		// Token: 0x040025C5 RID: 9669
		public static readonly CatalogPartCollection Empty = new CatalogPartCollection();
	}
}
