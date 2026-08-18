using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F3 RID: 5363
	internal class GroupingSizeMaker : NumberProperty.Maker
	{
		// Token: 0x0600D643 RID: 54851 RVA: 0x002F6439 File Offset: 0x002F4639
		public new static PropertyMaker Maker(string propName)
		{
			return new GroupingSizeMaker(propName);
		}

		// Token: 0x0600D644 RID: 54852 RVA: 0x002F6441 File Offset: 0x002F4641
		protected GroupingSizeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D645 RID: 54853 RVA: 0x002F644A File Offset: 0x002F464A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D646 RID: 54854 RVA: 0x002F644D File Offset: 0x002F464D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003ABA RID: 15034
		private Property m_defaultProp;
	}
}
