using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200145A RID: 5210
	internal class BackgroundRepeatMaker : EnumProperty.Maker
	{
		// Token: 0x0600D3F6 RID: 54262 RVA: 0x002F0A18 File Offset: 0x002EEC18
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundRepeatMaker(propName);
		}

		// Token: 0x0600D3F7 RID: 54263 RVA: 0x002F0A20 File Offset: 0x002EEC20
		protected BackgroundRepeatMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3F8 RID: 54264 RVA: 0x002F0A29 File Offset: 0x002EEC29
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3F9 RID: 54265 RVA: 0x002F0A2C File Offset: 0x002EEC2C
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("repeat"))
			{
				return BackgroundRepeatMaker.s_propREPEAT;
			}
			if (value.Equals("repeat-x"))
			{
				return BackgroundRepeatMaker.s_propREPEAT_X;
			}
			if (value.Equals("repeat-y"))
			{
				return BackgroundRepeatMaker.s_propREPEAT_Y;
			}
			if (value.Equals("no-repeat"))
			{
				return BackgroundRepeatMaker.s_propNO_REPEAT;
			}
			if (value.Equals("inherit"))
			{
				return BackgroundRepeatMaker.s_propINHERIT;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D3FA RID: 54266 RVA: 0x002F0A9F File Offset: 0x002EEC9F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "repeat", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003992 RID: 14738
		protected static readonly EnumProperty s_propREPEAT = new EnumProperty(87);

		// Token: 0x04003993 RID: 14739
		protected static readonly EnumProperty s_propREPEAT_X = new EnumProperty(88);

		// Token: 0x04003994 RID: 14740
		protected static readonly EnumProperty s_propREPEAT_Y = new EnumProperty(89);

		// Token: 0x04003995 RID: 14741
		protected static readonly EnumProperty s_propNO_REPEAT = new EnumProperty(90);

		// Token: 0x04003996 RID: 14742
		protected static readonly EnumProperty s_propINHERIT = new EnumProperty(35);

		// Token: 0x04003997 RID: 14743
		private Property m_defaultProp;
	}
}
