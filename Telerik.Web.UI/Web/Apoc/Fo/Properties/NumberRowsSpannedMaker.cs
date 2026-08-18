using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001536 RID: 5430
	internal class NumberRowsSpannedMaker : NumberProperty.Maker
	{
		// Token: 0x0600D745 RID: 55109 RVA: 0x002F77E7 File Offset: 0x002F59E7
		public new static PropertyMaker Maker(string propName)
		{
			return new NumberRowsSpannedMaker(propName);
		}

		// Token: 0x0600D746 RID: 55110 RVA: 0x002F77EF File Offset: 0x002F59EF
		protected NumberRowsSpannedMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D747 RID: 55111 RVA: 0x002F77F8 File Offset: 0x002F59F8
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D748 RID: 55112 RVA: 0x002F77FB File Offset: 0x002F59FB
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "1", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B08 RID: 15112
		private Property m_defaultProp;
	}
}
