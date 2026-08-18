using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200155B RID: 5467
	internal class PositionMaker : EnumProperty.Maker
	{
		// Token: 0x0600D7BC RID: 55228 RVA: 0x002F8218 File Offset: 0x002F6418
		public new static PropertyMaker Maker(string propName)
		{
			return new PositionMaker(propName);
		}

		// Token: 0x0600D7BD RID: 55229 RVA: 0x002F8220 File Offset: 0x002F6420
		protected PositionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7BE RID: 55230 RVA: 0x002F8229 File Offset: 0x002F6429
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7BF RID: 55231 RVA: 0x002F822C File Offset: 0x002F642C
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("static"))
			{
				return PositionMaker.s_propSTATIC;
			}
			if (value.Equals("relative"))
			{
				return PositionMaker.s_propRELATIVE;
			}
			if (value.Equals("absolute"))
			{
				return PositionMaker.s_propABSOLUTE;
			}
			if (value.Equals("fixed"))
			{
				return PositionMaker.s_propFIXED;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D7C0 RID: 55232 RVA: 0x002F828C File Offset: 0x002F648C
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "static", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B32 RID: 15154
		protected static readonly EnumProperty s_propSTATIC = new EnumProperty(73);

		// Token: 0x04003B33 RID: 15155
		protected static readonly EnumProperty s_propRELATIVE = new EnumProperty(61);

		// Token: 0x04003B34 RID: 15156
		protected static readonly EnumProperty s_propABSOLUTE = new EnumProperty(1);

		// Token: 0x04003B35 RID: 15157
		protected static readonly EnumProperty s_propFIXED = new EnumProperty(30);

		// Token: 0x04003B36 RID: 15158
		private Property m_defaultProp;
	}
}
