using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013E9 RID: 5097
	internal class ListItemBody : FObj
	{
		// Token: 0x0600D218 RID: 53784 RVA: 0x002E95FD File Offset: 0x002E77FD
		public new static FObj.Maker GetMaker()
		{
			return new ListItemBody.Maker();
		}

		// Token: 0x0600D219 RID: 53785 RVA: 0x002E9604 File Offset: 0x002E7804
		public ListItemBody(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:list-item-body";
		}

		// Token: 0x0600D21A RID: 53786 RVA: 0x002E961C File Offset: 0x002E781C
		public override Status Layout(Area area)
		{
			if (this.marker == -1000)
			{
				this.propMgr.GetAccessibilityProps();
				this.marker = 0;
				string @string = this.properties.GetProperty("id").GetString();
				area.getIDReferences().InitializeID(@string, area);
			}
			int count = this.children.Count;
			int i = this.marker;
			while (i < count)
			{
				FObj fobj = (FObj)this.children[i];
				Status status2;
				Status status = status2 = fobj.Layout(area);
				if (status2.isIncomplete())
				{
					this.marker = i;
					if (i == 0 && status.getCode() == 2)
					{
						return new Status(2);
					}
					return new Status(3);
				}
				else
				{
					i++;
				}
			}
			return new Status(1);
		}

		// Token: 0x020013EA RID: 5098
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D21B RID: 53787 RVA: 0x002E96D8 File Offset: 0x002E78D8
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new ListItemBody(parent, propertyList);
			}
		}
	}
}
