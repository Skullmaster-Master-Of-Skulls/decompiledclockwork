using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001534 RID: 5428
	internal class NumberColumnsRepeatedMaker : NumberProperty.Maker
	{
		// Token: 0x0600D73D RID: 55101 RVA: 0x002F776F File Offset: 0x002F596F
		public new static PropertyMaker Maker(string propName)
		{
			return new NumberColumnsRepeatedMaker(propName);
		}

		// Token: 0x0600D73E RID: 55102 RVA: 0x002F7777 File Offset: 0x002F5977
		protected NumberColumnsRepeatedMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D73F RID: 55103 RVA: 0x002F7780 File Offset: 0x002F5980
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D740 RID: 55104 RVA: 0x002F7783 File Offset: 0x002F5983
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "1", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B06 RID: 15110
		private Property m_defaultProp;
	}
}
