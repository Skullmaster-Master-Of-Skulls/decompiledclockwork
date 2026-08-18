using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014CF RID: 5327
	internal class FontStretchMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D5BC RID: 54716 RVA: 0x002F3ED5 File Offset: 0x002F20D5
		public new static PropertyMaker Maker(string propName)
		{
			return new FontStretchMaker(propName);
		}

		// Token: 0x0600D5BD RID: 54717 RVA: 0x002F3EDD File Offset: 0x002F20DD
		protected FontStretchMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5BE RID: 54718 RVA: 0x002F3EE6 File Offset: 0x002F20E6
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5BF RID: 54719 RVA: 0x002F3EE9 File Offset: 0x002F20E9
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A74 RID: 14964
		private Property m_defaultProp;
	}
}
