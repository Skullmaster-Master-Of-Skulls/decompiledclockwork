using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B0 RID: 5296
	internal class ColumnCountMaker : StringProperty.Maker
	{
		// Token: 0x0600D53E RID: 54590 RVA: 0x002F3540 File Offset: 0x002F1740
		public new static PropertyMaker Maker(string propName)
		{
			return new ColumnCountMaker(propName);
		}

		// Token: 0x0600D53F RID: 54591 RVA: 0x002F3548 File Offset: 0x002F1748
		protected ColumnCountMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D540 RID: 54592 RVA: 0x002F3551 File Offset: 0x002F1751
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D541 RID: 54593 RVA: 0x002F3554 File Offset: 0x002F1754
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "1", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F6 RID: 14838
		private Property m_defaultProp;
	}
}
