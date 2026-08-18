using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014D2 RID: 5330
	internal class FontVariantMaker : EnumProperty.Maker
	{
		// Token: 0x0600D5C5 RID: 54725 RVA: 0x002F3F55 File Offset: 0x002F2155
		public new static PropertyMaker Maker(string propName)
		{
			return new FontVariantMaker(propName);
		}

		// Token: 0x0600D5C6 RID: 54726 RVA: 0x002F3F5D File Offset: 0x002F215D
		protected FontVariantMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5C7 RID: 54727 RVA: 0x002F3F66 File Offset: 0x002F2166
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5C8 RID: 54728 RVA: 0x002F3F69 File Offset: 0x002F2169
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("normal"))
			{
				return FontVariantMaker.s_propNORMAL;
			}
			if (value.Equals("small-caps"))
			{
				return FontVariantMaker.s_propSMALL_CAPS;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D5C9 RID: 54729 RVA: 0x002F3F98 File Offset: 0x002F2198
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A78 RID: 14968
		protected static readonly EnumProperty s_propNORMAL = new EnumProperty(52);

		// Token: 0x04003A79 RID: 14969
		protected static readonly EnumProperty s_propSMALL_CAPS = new EnumProperty(69);

		// Token: 0x04003A7A RID: 14970
		private Property m_defaultProp;
	}
}
