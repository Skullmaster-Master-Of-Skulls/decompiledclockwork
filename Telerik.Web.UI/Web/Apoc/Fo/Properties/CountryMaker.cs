using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B8 RID: 5304
	internal class CountryMaker : StringProperty.Maker
	{
		// Token: 0x0600D55E RID: 54622 RVA: 0x002F36E1 File Offset: 0x002F18E1
		public new static PropertyMaker Maker(string propName)
		{
			return new CountryMaker(propName);
		}

		// Token: 0x0600D55F RID: 54623 RVA: 0x002F36E9 File Offset: 0x002F18E9
		protected CountryMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D560 RID: 54624 RVA: 0x002F36F2 File Offset: 0x002F18F2
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D561 RID: 54625 RVA: 0x002F36F5 File Offset: 0x002F18F5
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A56 RID: 14934
		private Property m_defaultProp;
	}
}
