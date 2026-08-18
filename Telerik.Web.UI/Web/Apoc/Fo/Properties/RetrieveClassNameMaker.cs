using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001569 RID: 5481
	internal class RetrieveClassNameMaker : StringProperty.Maker
	{
		// Token: 0x0600D7F3 RID: 55283 RVA: 0x002F865B File Offset: 0x002F685B
		public new static PropertyMaker Maker(string propName)
		{
			return new RetrieveClassNameMaker(propName);
		}

		// Token: 0x0600D7F4 RID: 55284 RVA: 0x002F8663 File Offset: 0x002F6863
		protected RetrieveClassNameMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7F5 RID: 55285 RVA: 0x002F866C File Offset: 0x002F686C
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7F6 RID: 55286 RVA: 0x002F866F File Offset: 0x002F686F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B4F RID: 15183
		private Property m_defaultProp;
	}
}
