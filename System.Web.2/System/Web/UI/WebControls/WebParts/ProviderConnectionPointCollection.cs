using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000564 RID: 1380
	public sealed class ProviderConnectionPointCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600460D RID: 17933 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public ProviderConnectionPointCollection()
		{
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x000E70D0 File Offset: 0x000E52D0
		public ProviderConnectionPointCollection(ICollection connectionPoints)
		{
			if (connectionPoints == null)
			{
				throw new ArgumentNullException("connectionPoints");
			}
			this._ids = new HybridDictionary(connectionPoints.Count, true);
			foreach (object obj in connectionPoints)
			{
				if (obj == null)
				{
					throw new ArgumentException(SR.GetString("Collection_CantAddNull"), "connectionPoints");
				}
				ProviderConnectionPoint providerConnectionPoint = obj as ProviderConnectionPoint;
				if (providerConnectionPoint == null)
				{
					throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
					{
						"ProviderConnectionPoint"
					}), "connectionPoints");
				}
				string id = providerConnectionPoint.ID;
				if (this._ids.Contains(id))
				{
					throw new ArgumentException(SR.GetString("WebPart_Collection_DuplicateID", new object[]
					{
						"ProviderConnectionPoint",
						id
					}), "connectionPoints");
				}
				base.InnerList.Add(providerConnectionPoint);
				this._ids.Add(id, providerConnectionPoint);
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x0600460F RID: 17935 RVA: 0x000E71E4 File Offset: 0x000E53E4
		public ProviderConnectionPoint Default
		{
			get
			{
				return this[ConnectionPoint.DefaultID];
			}
		}

		// Token: 0x170014A4 RID: 5284
		public ProviderConnectionPoint this[int index]
		{
			get
			{
				return (ProviderConnectionPoint)base.InnerList[index];
			}
		}

		// Token: 0x170014A5 RID: 5285
		public ProviderConnectionPoint this[string id]
		{
			get
			{
				if (this._ids == null)
				{
					return null;
				}
				return (ProviderConnectionPoint)this._ids[id];
			}
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(ProviderConnectionPoint connectionPoint)
		{
			return base.InnerList.Contains(connectionPoint);
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(ProviderConnectionPoint connectionPoint)
		{
			return base.InnerList.IndexOf(connectionPoint);
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(ProviderConnectionPoint[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0400268F RID: 9871
		private HybridDictionary _ids;
	}
}
