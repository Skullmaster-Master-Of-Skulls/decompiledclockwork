using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001535 RID: 5429
	internal class NumberColumnsSpannedMaker : NumberProperty.Maker
	{
		// Token: 0x0600D741 RID: 55105 RVA: 0x002F77AB File Offset: 0x002F59AB
		public new static PropertyMaker Maker(string propName)
		{
			return new NumberColumnsSpannedMaker(propName);
		}

		// Token: 0x0600D742 RID: 55106 RVA: 0x002F77B3 File Offset: 0x002F59B3
		protected NumberColumnsSpannedMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D743 RID: 55107 RVA: 0x002F77BC File Offset: 0x002F59BC
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D744 RID: 55108 RVA: 0x002F77BF File Offset: 0x002F59BF
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "1", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B07 RID: 15111
		private Property m_defaultProp;
	}
}
