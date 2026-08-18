using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001564 RID: 5476
	internal class RelativeAlignMaker : EnumProperty.Maker
	{
		// Token: 0x0600D7DE RID: 55262 RVA: 0x002F84A6 File Offset: 0x002F66A6
		public new static PropertyMaker Maker(string propName)
		{
			return new RelativeAlignMaker(propName);
		}

		// Token: 0x0600D7DF RID: 55263 RVA: 0x002F84AE File Offset: 0x002F66AE
		protected RelativeAlignMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7E0 RID: 55264 RVA: 0x002F84B7 File Offset: 0x002F66B7
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D7E1 RID: 55265 RVA: 0x002F84BA File Offset: 0x002F66BA
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("before"))
			{
				return RelativeAlignMaker.s_propBEFORE;
			}
			if (value.Equals("after"))
			{
				return RelativeAlignMaker.s_propBASELINE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D7E2 RID: 55266 RVA: 0x002F84E9 File Offset: 0x002F66E9
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "before", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B43 RID: 15171
		protected static readonly EnumProperty s_propBEFORE = new EnumProperty(9);

		// Token: 0x04003B44 RID: 15172
		protected static readonly EnumProperty s_propBASELINE = new EnumProperty(8);

		// Token: 0x04003B45 RID: 15173
		private Property m_defaultProp;
	}
}
