using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015B8 RID: 5560
	internal class WrapOptionMaker : EnumProperty.Maker
	{
		// Token: 0x0600D8FD RID: 55549 RVA: 0x002F9BBD File Offset: 0x002F7DBD
		public new static PropertyMaker Maker(string propName)
		{
			return new WrapOptionMaker(propName);
		}

		// Token: 0x0600D8FE RID: 55550 RVA: 0x002F9BC5 File Offset: 0x002F7DC5
		protected WrapOptionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8FF RID: 55551 RVA: 0x002F9BCE File Offset: 0x002F7DCE
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D900 RID: 55552 RVA: 0x002F9BD1 File Offset: 0x002F7DD1
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("wrap"))
			{
				return WrapOptionMaker.s_propWRAP;
			}
			if (value.Equals("no-wrap"))
			{
				return WrapOptionMaker.s_propNO_WRAP;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D901 RID: 55553 RVA: 0x002F9C00 File Offset: 0x002F7E00
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "wrap", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BDB RID: 15323
		protected static readonly EnumProperty s_propWRAP = new EnumProperty(86);

		// Token: 0x04003BDC RID: 15324
		protected static readonly EnumProperty s_propNO_WRAP = new EnumProperty(49);

		// Token: 0x04003BDD RID: 15325
		private Property m_defaultProp;
	}
}
