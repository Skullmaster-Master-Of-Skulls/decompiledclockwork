using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001550 RID: 5456
	internal class PageHeightMaker : LengthProperty.Maker
	{
		// Token: 0x0600D792 RID: 55186 RVA: 0x002F7F55 File Offset: 0x002F6155
		public new static PropertyMaker Maker(string propName)
		{
			return new PageHeightMaker(propName);
		}

		// Token: 0x0600D793 RID: 55187 RVA: 0x002F7F5D File Offset: 0x002F615D
		protected PageHeightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D794 RID: 55188 RVA: 0x002F7F66 File Offset: 0x002F6166
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D795 RID: 55189 RVA: 0x002F7F69 File Offset: 0x002F6169
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D796 RID: 55190 RVA: 0x002F7F6C File Offset: 0x002F616C
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "11in", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B1D RID: 15133
		private Property m_defaultProp;
	}
}
