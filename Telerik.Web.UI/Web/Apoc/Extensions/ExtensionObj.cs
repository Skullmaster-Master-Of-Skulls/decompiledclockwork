using System;
using Telerik.Web.Apoc.Fo;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Extensions
{
	// Token: 0x02001396 RID: 5014
	internal abstract class ExtensionObj : FObj
	{
		// Token: 0x0600D0EC RID: 53484 RVA: 0x002E3F4D File Offset: 0x002E214D
		public ExtensionObj(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
		}

		// Token: 0x0600D0ED RID: 53485 RVA: 0x002E3F58 File Offset: 0x002E2158
		public override Status Layout(Area area)
		{
			ExtensionArea child = new ExtensionArea(this);
			area.addChild(child);
			return new Status(1);
		}

		// Token: 0x0600D0EE RID: 53486 RVA: 0x002E3F79 File Offset: 0x002E2179
		public void Format(AreaTree areaTree)
		{
			new ExtensionArea(this);
			areaTree.addExtension(this);
		}
	}
}
