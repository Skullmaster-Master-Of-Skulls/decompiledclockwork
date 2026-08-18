using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000433 RID: 1075
	[Editor("System.Web.UI.Design.WebControls.HotSpotCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public sealed class HotSpotCollection : StateManagedCollection
	{
		// Token: 0x17000F1B RID: 3867
		public HotSpot this[int index]
		{
			get
			{
				return (HotSpot)((IList)this)[index];
			}
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000A9CAD File Offset: 0x000A7EAD
		public int Add(HotSpot spot)
		{
			return ((IList)this).Add(spot);
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000A9CB6 File Offset: 0x000A7EB6
		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
			case 0:
				return new CircleHotSpot();
			case 1:
				return new RectangleHotSpot();
			case 2:
				return new PolygonHotSpot();
			default:
				throw new ArgumentOutOfRangeException(SR.GetString("HotSpotCollection_InvalidTypeIndex"));
			}
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000A9CED File Offset: 0x000A7EED
		protected override Type[] GetKnownTypes()
		{
			return HotSpotCollection.knownTypes;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x00095E5E File Offset: 0x0009405E
		public void Insert(int index, HotSpot spot)
		{
			((IList)this).Insert(index, spot);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000A9CF4 File Offset: 0x000A7EF4
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is HotSpot))
			{
				throw new ArgumentException(SR.GetString("HotSpotCollection_InvalidType"));
			}
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x00095F15 File Offset: 0x00094115
		public void Remove(HotSpot spot)
		{
			((IList)this).Remove(spot);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x00095F0C File Offset: 0x0009410C
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000A9D15 File Offset: 0x000A7F15
		protected override void SetDirtyObject(object o)
		{
			((HotSpot)o).SetDirty();
		}

		// Token: 0x04002192 RID: 8594
		private static readonly Type[] knownTypes = new Type[]
		{
			typeof(CircleHotSpot),
			typeof(RectangleHotSpot),
			typeof(PolygonHotSpot)
		};
	}
}
