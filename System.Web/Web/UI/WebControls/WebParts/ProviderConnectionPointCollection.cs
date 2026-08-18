using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006EF RID: 1775
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ProviderConnectionPointCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060056D4 RID: 22228 RVA: 0x0015E40F File Offset: 0x0015D40F
		public ProviderConnectionPointCollection()
		{
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x0015E418 File Offset: 0x0015D418
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

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x060056D6 RID: 22230 RVA: 0x0015E538 File Offset: 0x0015D538
		public ProviderConnectionPoint Default
		{
			get
			{
				return this[ConnectionPoint.DefaultID];
			}
		}

		// Token: 0x1700166B RID: 5739
		public ProviderConnectionPoint this[int index]
		{
			get
			{
				return (ProviderConnectionPoint)base.InnerList[index];
			}
		}

		// Token: 0x1700166C RID: 5740
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

		// Token: 0x060056D9 RID: 22233 RVA: 0x0015E575 File Offset: 0x0015D575
		public bool Contains(ProviderConnectionPoint connectionPoint)
		{
			return base.InnerList.Contains(connectionPoint);
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x0015E583 File Offset: 0x0015D583
		public int IndexOf(ProviderConnectionPoint connectionPoint)
		{
			return base.InnerList.IndexOf(connectionPoint);
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x0015E591 File Offset: 0x0015D591
		public void CopyTo(ProviderConnectionPoint[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x04002F78 RID: 12152
		private HybridDictionary _ids;
	}
}
