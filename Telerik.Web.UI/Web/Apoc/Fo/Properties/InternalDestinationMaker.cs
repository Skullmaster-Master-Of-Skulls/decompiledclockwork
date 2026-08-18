using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001503 RID: 5379
	internal class InternalDestinationMaker : StringProperty.Maker
	{
		// Token: 0x0600D68D RID: 54925 RVA: 0x002F6AEF File Offset: 0x002F4CEF
		public new static PropertyMaker Maker(string propName)
		{
			return new InternalDestinationMaker(propName);
		}

		// Token: 0x0600D68E RID: 54926 RVA: 0x002F6AF7 File Offset: 0x002F4CF7
		protected InternalDestinationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D68F RID: 54927 RVA: 0x002F6B00 File Offset: 0x002F4D00
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D690 RID: 54928 RVA: 0x002F6B03 File Offset: 0x002F4D03
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003ACE RID: 15054
		private Property m_defaultProp;
	}
}
