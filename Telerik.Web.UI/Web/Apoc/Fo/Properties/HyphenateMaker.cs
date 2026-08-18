using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F6 RID: 5366
	internal class HyphenateMaker : EnumProperty.Maker
	{
		// Token: 0x0600D64D RID: 54861 RVA: 0x002F64BC File Offset: 0x002F46BC
		public new static PropertyMaker Maker(string propName)
		{
			return new HyphenateMaker(propName);
		}

		// Token: 0x0600D64E RID: 54862 RVA: 0x002F64C4 File Offset: 0x002F46C4
		protected HyphenateMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D64F RID: 54863 RVA: 0x002F64CD File Offset: 0x002F46CD
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D650 RID: 54864 RVA: 0x002F64D0 File Offset: 0x002F46D0
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("true"))
			{
				return HyphenateMaker.s_propTRUE;
			}
			if (value.Equals("false"))
			{
				return HyphenateMaker.s_propFALSE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D651 RID: 54865 RVA: 0x002F64FF File Offset: 0x002F46FF
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003ABE RID: 15038
		protected static readonly EnumProperty s_propTRUE = new EnumProperty(81);

		// Token: 0x04003ABF RID: 15039
		protected static readonly EnumProperty s_propFALSE = new EnumProperty(27);

		// Token: 0x04003AC0 RID: 15040
		private Property m_defaultProp;
	}
}
