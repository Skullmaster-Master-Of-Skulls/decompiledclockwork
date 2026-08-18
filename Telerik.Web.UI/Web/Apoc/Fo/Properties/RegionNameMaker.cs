using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001562 RID: 5474
	internal class RegionNameMaker : StringProperty.Maker
	{
		// Token: 0x0600D7D9 RID: 55257 RVA: 0x002F8462 File Offset: 0x002F6662
		public new static PropertyMaker Maker(string propName)
		{
			return new RegionNameMaker(propName);
		}

		// Token: 0x0600D7DA RID: 55258 RVA: 0x002F846A File Offset: 0x002F666A
		protected RegionNameMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7DB RID: 55259 RVA: 0x002F8473 File Offset: 0x002F6673
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7DC RID: 55260 RVA: 0x002F8476 File Offset: 0x002F6676
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B40 RID: 15168
		private Property m_defaultProp;
	}
}
