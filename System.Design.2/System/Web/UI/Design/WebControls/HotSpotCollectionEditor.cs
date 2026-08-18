using System;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000D2 RID: 210
	public class HotSpotCollectionEditor : CollectionEditor
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x00023ABB File Offset: 0x00021CBB
		public HotSpotCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000445B File Offset: 0x0000265B
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0002796B File Offset: 0x00025B6B
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(CircleHotSpot),
				typeof(RectangleHotSpot),
				typeof(PolygonHotSpot)
			};
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0002799A File Offset: 0x00025B9A
		protected override string HelpTopic
		{
			get
			{
				return "net.Asp.HotSpot.CollectionEditor";
			}
		}
	}
}
