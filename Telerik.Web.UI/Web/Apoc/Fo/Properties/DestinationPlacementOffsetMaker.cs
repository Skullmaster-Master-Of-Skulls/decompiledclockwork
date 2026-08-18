using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014BC RID: 5308
	internal class DestinationPlacementOffsetMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D56E RID: 54638 RVA: 0x002F37D1 File Offset: 0x002F19D1
		public new static PropertyMaker Maker(string propName)
		{
			return new DestinationPlacementOffsetMaker(propName);
		}

		// Token: 0x0600D56F RID: 54639 RVA: 0x002F37D9 File Offset: 0x002F19D9
		protected DestinationPlacementOffsetMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D570 RID: 54640 RVA: 0x002F37E2 File Offset: 0x002F19E2
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D571 RID: 54641 RVA: 0x002F37E5 File Offset: 0x002F19E5
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A5A RID: 14938
		private Property m_defaultProp;
	}
}
