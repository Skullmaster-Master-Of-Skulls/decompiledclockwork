using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B2 RID: 5298
	internal class ColumnNumberMaker : NumberProperty.Maker
	{
		// Token: 0x0600D547 RID: 54599 RVA: 0x002F35BB File Offset: 0x002F17BB
		public new static PropertyMaker Maker(string propName)
		{
			return new ColumnNumberMaker(propName);
		}

		// Token: 0x0600D548 RID: 54600 RVA: 0x002F35C3 File Offset: 0x002F17C3
		protected ColumnNumberMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D549 RID: 54601 RVA: 0x002F35CC File Offset: 0x002F17CC
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D54A RID: 54602 RVA: 0x002F35CF File Offset: 0x002F17CF
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F8 RID: 14840
		private Property m_defaultProp;
	}
}
