using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015AC RID: 5548
	internal class VerticalAlignMaker : EnumProperty.Maker
	{
		// Token: 0x0600D8CF RID: 55503 RVA: 0x002F9830 File Offset: 0x002F7A30
		public new static PropertyMaker Maker(string propName)
		{
			return new VerticalAlignMaker(propName);
		}

		// Token: 0x0600D8D0 RID: 55504 RVA: 0x002F9838 File Offset: 0x002F7A38
		protected VerticalAlignMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8D1 RID: 55505 RVA: 0x002F9841 File Offset: 0x002F7A41
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8D2 RID: 55506 RVA: 0x002F9844 File Offset: 0x002F7A44
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("baseline"))
			{
				return VerticalAlignMaker.s_propBASELINE;
			}
			if (value.Equals("middle"))
			{
				return VerticalAlignMaker.s_propMIDDLE;
			}
			if (value.Equals("sub"))
			{
				return VerticalAlignMaker.s_propSUB;
			}
			if (value.Equals("super"))
			{
				return VerticalAlignMaker.s_propSUPER;
			}
			if (value.Equals("text-top"))
			{
				return VerticalAlignMaker.s_propTEXT_TOP;
			}
			if (value.Equals("text-bottom"))
			{
				return VerticalAlignMaker.s_propTEXT_BOTTOM;
			}
			if (value.Equals("top"))
			{
				return VerticalAlignMaker.s_propTOP;
			}
			if (value.Equals("bottom"))
			{
				return VerticalAlignMaker.s_propBOTTOM;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D8D3 RID: 55507 RVA: 0x002F98F0 File Offset: 0x002F7AF0
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "baseline", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BC7 RID: 15303
		protected static readonly EnumProperty s_propBASELINE = new EnumProperty(8);

		// Token: 0x04003BC8 RID: 15304
		protected static readonly EnumProperty s_propMIDDLE = new EnumProperty(43);

		// Token: 0x04003BC9 RID: 15305
		protected static readonly EnumProperty s_propSUB = new EnumProperty(74);

		// Token: 0x04003BCA RID: 15306
		protected static readonly EnumProperty s_propSUPER = new EnumProperty(75);

		// Token: 0x04003BCB RID: 15307
		protected static readonly EnumProperty s_propTEXT_TOP = new EnumProperty(78);

		// Token: 0x04003BCC RID: 15308
		protected static readonly EnumProperty s_propTEXT_BOTTOM = new EnumProperty(77);

		// Token: 0x04003BCD RID: 15309
		protected static readonly EnumProperty s_propTOP = new EnumProperty(79);

		// Token: 0x04003BCE RID: 15310
		protected static readonly EnumProperty s_propBOTTOM = new EnumProperty(12);

		// Token: 0x04003BCF RID: 15311
		private Property m_defaultProp;
	}
}
