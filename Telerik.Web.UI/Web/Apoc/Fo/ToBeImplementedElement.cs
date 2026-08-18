using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020013A0 RID: 5024
	internal class ToBeImplementedElement : FObj
	{
		// Token: 0x0600D10E RID: 53518 RVA: 0x002E432B File Offset: 0x002E252B
		protected ToBeImplementedElement(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
		}

		// Token: 0x0600D10F RID: 53519 RVA: 0x002E4335 File Offset: 0x002E2535
		public override Status Layout(Area area)
		{
			return new Status(1);
		}
	}
}
