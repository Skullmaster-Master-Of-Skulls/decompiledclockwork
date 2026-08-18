using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015B4 RID: 5556
	internal class WidowsMaker : NumberProperty.Maker
	{
		// Token: 0x0600D8EE RID: 55534 RVA: 0x002F9AF4 File Offset: 0x002F7CF4
		public new static PropertyMaker Maker(string propName)
		{
			return new WidowsMaker(propName);
		}

		// Token: 0x0600D8EF RID: 55535 RVA: 0x002F9AFC File Offset: 0x002F7CFC
		protected WidowsMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8F0 RID: 55536 RVA: 0x002F9B05 File Offset: 0x002F7D05
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8F1 RID: 55537 RVA: 0x002F9B08 File Offset: 0x002F7D08
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "2", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD6 RID: 15318
		private Property m_defaultProp;
	}
}
