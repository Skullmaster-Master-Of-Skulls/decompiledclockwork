using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000536 RID: 1334
	public sealed class ConsumerConnectionPointCollection : ReadOnlyCollectionBase
	{
		// Token: 0x060043F6 RID: 17398 RVA: 0x000DCEF2 File Offset: 0x000DB0F2
		public ConsumerConnectionPointCollection()
		{
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000E1A0C File Offset: 0x000DFC0C
		public ConsumerConnectionPointCollection(ICollection connectionPoints)
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
				ConsumerConnectionPoint consumerConnectionPoint = obj as ConsumerConnectionPoint;
				if (consumerConnectionPoint == null)
				{
					throw new ArgumentException(SR.GetString("Collection_InvalidType", new object[]
					{
						"ConsumerConnectionPoint"
					}), "connectionPoints");
				}
				string id = consumerConnectionPoint.ID;
				if (this._ids.Contains(id))
				{
					throw new ArgumentException(SR.GetString("WebPart_Collection_DuplicateID", new object[]
					{
						"ConsumerConnectionPoint",
						id
					}), "connectionPoints");
				}
				base.InnerList.Add(consumerConnectionPoint);
				this._ids.Add(id, consumerConnectionPoint);
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x000E1B20 File Offset: 0x000DFD20
		public ConsumerConnectionPoint Default
		{
			get
			{
				return this[ConnectionPoint.DefaultID];
			}
		}

		// Token: 0x170013F2 RID: 5106
		public ConsumerConnectionPoint this[int index]
		{
			get
			{
				return (ConsumerConnectionPoint)base.InnerList[index];
			}
		}

		// Token: 0x170013F3 RID: 5107
		public ConsumerConnectionPoint this[string id]
		{
			get
			{
				if (this._ids == null)
				{
					return null;
				}
				return (ConsumerConnectionPoint)this._ids[id];
			}
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x00043ADC File Offset: 0x00041CDC
		public bool Contains(ConsumerConnectionPoint connectionPoint)
		{
			return base.InnerList.Contains(connectionPoint);
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x00043ACE File Offset: 0x00041CCE
		public int IndexOf(ConsumerConnectionPoint connectionPoint)
		{
			return base.InnerList.IndexOf(connectionPoint);
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x000DCFA6 File Offset: 0x000DB1A6
		public void CopyTo(ConsumerConnectionPoint[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0400261D RID: 9757
		private HybridDictionary _ids;
	}
}
