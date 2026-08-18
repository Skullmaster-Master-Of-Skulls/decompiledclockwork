using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013EB RID: 5099
	internal class ListItemLabel : FObj
	{
		// Token: 0x0600D21D RID: 53789 RVA: 0x002E96E9 File Offset: 0x002E78E9
		public new static FObj.Maker GetMaker()
		{
			return new ListItemLabel.Maker();
		}

		// Token: 0x0600D21E RID: 53790 RVA: 0x002E96F0 File Offset: 0x002E78F0
		public ListItemLabel(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:list-item-label";
		}

		// Token: 0x0600D21F RID: 53791 RVA: 0x002E9708 File Offset: 0x002E7908
		public override Status Layout(Area area)
		{
			int count = this.children.Count;
			if (count != 1)
			{
				throw new ApocException("list-item-label must have exactly one block in this version of FOP");
			}
			this.propMgr.GetAccessibilityProps();
			string @string = this.properties.GetProperty("id").GetString();
			area.getIDReferences().InitializeID(@string, area);
			Block block = (Block)this.children[0];
			Status result = block.Layout(area);
			area.addDisplaySpace(-block.GetAreaHeight());
			return result;
		}

		// Token: 0x020013EC RID: 5100
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D220 RID: 53792 RVA: 0x002E9787 File Offset: 0x002E7987
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new ListItemLabel(parent, propertyList);
			}
		}
	}
}
