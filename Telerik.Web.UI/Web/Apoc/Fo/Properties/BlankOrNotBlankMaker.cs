using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200145E RID: 5214
	internal class BlankOrNotBlankMaker : EnumProperty.Maker
	{
		// Token: 0x0600D404 RID: 54276 RVA: 0x002F0BC6 File Offset: 0x002EEDC6
		public new static PropertyMaker Maker(string propName)
		{
			return new BlankOrNotBlankMaker(propName);
		}

		// Token: 0x0600D405 RID: 54277 RVA: 0x002F0BCE File Offset: 0x002EEDCE
		protected BlankOrNotBlankMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D406 RID: 54278 RVA: 0x002F0BD7 File Offset: 0x002EEDD7
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D407 RID: 54279 RVA: 0x002F0BDC File Offset: 0x002EEDDC
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("blank"))
			{
				return BlankOrNotBlankMaker.s_propBLANK;
			}
			if (value.Equals("not-blank"))
			{
				return BlankOrNotBlankMaker.s_propNOT_BLANK;
			}
			if (value.Equals("any"))
			{
				return BlankOrNotBlankMaker.s_propANY;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D408 RID: 54280 RVA: 0x002F0C29 File Offset: 0x002EEE29
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "any", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039A2 RID: 14754
		protected static readonly EnumProperty s_propBLANK = new EnumProperty(10);

		// Token: 0x040039A3 RID: 14755
		protected static readonly EnumProperty s_propNOT_BLANK = new EnumProperty(53);

		// Token: 0x040039A4 RID: 14756
		protected static readonly EnumProperty s_propANY = new EnumProperty(6);

		// Token: 0x040039A5 RID: 14757
		private Property m_defaultProp;
	}
}
