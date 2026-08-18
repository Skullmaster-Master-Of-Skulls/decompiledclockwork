using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001494 RID: 5268
	internal class BorderSpacingMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D4DE RID: 54494 RVA: 0x002F2B7E File Offset: 0x002F0D7E
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderSpacingMaker(propName);
		}

		// Token: 0x0600D4DF RID: 54495 RVA: 0x002F2B86 File Offset: 0x002F0D86
		protected BorderSpacingMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4E0 RID: 54496 RVA: 0x002F2B8F File Offset: 0x002F0D8F
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D4E1 RID: 54497 RVA: 0x002F2B92 File Offset: 0x002F0D92
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039DE RID: 14814
		private Property m_defaultProp;
	}
}
