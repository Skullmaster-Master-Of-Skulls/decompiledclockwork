using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A3 RID: 5539
	internal class TextDecorationMaker : EnumProperty.Maker
	{
		// Token: 0x0600D8AB RID: 55467 RVA: 0x002F950C File Offset: 0x002F770C
		public new static PropertyMaker Maker(string propName)
		{
			return new TextDecorationMaker(propName);
		}

		// Token: 0x0600D8AC RID: 55468 RVA: 0x002F9514 File Offset: 0x002F7714
		protected TextDecorationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8AD RID: 55469 RVA: 0x002F951D File Offset: 0x002F771D
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8AE RID: 55470 RVA: 0x002F9520 File Offset: 0x002F7720
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("none"))
			{
				return TextDecorationMaker.s_propNONE;
			}
			if (value.Equals("underline"))
			{
				return TextDecorationMaker.s_propUNDERLINE;
			}
			if (value.Equals("overline"))
			{
				return TextDecorationMaker.s_propOVERLINE;
			}
			if (value.Equals("line-through"))
			{
				return TextDecorationMaker.s_propLINE_THROUGH;
			}
			if (value.Equals("blink"))
			{
				return TextDecorationMaker.s_propBLINK;
			}
			if (value.Equals("no-underline"))
			{
				return TextDecorationMaker.s_propNO_UNDERLINE;
			}
			if (value.Equals("no-overline"))
			{
				return TextDecorationMaker.s_propNO_OVERLINE;
			}
			if (value.Equals("no-line-through"))
			{
				return TextDecorationMaker.s_propNO_LINE_THROUGH;
			}
			if (value.Equals("no-blink"))
			{
				return TextDecorationMaker.s_propNO_BLINK;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D8AF RID: 55471 RVA: 0x002F95DF File Offset: 0x002F77DF
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BAE RID: 15278
		protected static readonly EnumProperty s_propNONE = new EnumProperty(51);

		// Token: 0x04003BAF RID: 15279
		protected static readonly EnumProperty s_propUNDERLINE = new EnumProperty(82);

		// Token: 0x04003BB0 RID: 15280
		protected static readonly EnumProperty s_propOVERLINE = new EnumProperty(57);

		// Token: 0x04003BB1 RID: 15281
		protected static readonly EnumProperty s_propLINE_THROUGH = new EnumProperty(40);

		// Token: 0x04003BB2 RID: 15282
		protected static readonly EnumProperty s_propBLINK = new EnumProperty(11);

		// Token: 0x04003BB3 RID: 15283
		protected static readonly EnumProperty s_propNO_UNDERLINE = new EnumProperty(48);

		// Token: 0x04003BB4 RID: 15284
		protected static readonly EnumProperty s_propNO_OVERLINE = new EnumProperty(47);

		// Token: 0x04003BB5 RID: 15285
		protected static readonly EnumProperty s_propNO_LINE_THROUGH = new EnumProperty(46);

		// Token: 0x04003BB6 RID: 15286
		protected static readonly EnumProperty s_propNO_BLINK = new EnumProperty(44);

		// Token: 0x04003BB7 RID: 15287
		private Property m_defaultProp;
	}
}
