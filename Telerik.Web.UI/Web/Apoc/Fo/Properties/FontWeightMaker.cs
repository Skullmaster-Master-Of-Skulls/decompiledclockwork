using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014D3 RID: 5331
	internal class FontWeightMaker : StringProperty.Maker
	{
		// Token: 0x0600D5CB RID: 54731 RVA: 0x002F3FDA File Offset: 0x002F21DA
		public new static PropertyMaker Maker(string propName)
		{
			return new FontWeightMaker(propName);
		}

		// Token: 0x0600D5CC RID: 54732 RVA: 0x002F3FE2 File Offset: 0x002F21E2
		protected FontWeightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5CD RID: 54733 RVA: 0x002F3FEB File Offset: 0x002F21EB
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5CE RID: 54734 RVA: 0x002F3FEE File Offset: 0x002F21EE
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A7B RID: 14971
		private Property m_defaultProp;
	}
}
