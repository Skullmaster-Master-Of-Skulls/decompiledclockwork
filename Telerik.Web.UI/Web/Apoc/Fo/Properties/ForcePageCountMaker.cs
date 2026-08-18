using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014D6 RID: 5334
	internal class ForcePageCountMaker : EnumProperty.Maker
	{
		// Token: 0x0600D5D3 RID: 54739 RVA: 0x002F5865 File Offset: 0x002F3A65
		public new static PropertyMaker Maker(string propName)
		{
			return new ForcePageCountMaker(propName);
		}

		// Token: 0x0600D5D4 RID: 54740 RVA: 0x002F586D File Offset: 0x002F3A6D
		protected ForcePageCountMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5D5 RID: 54741 RVA: 0x002F5876 File Offset: 0x002F3A76
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D5D6 RID: 54742 RVA: 0x002F587C File Offset: 0x002F3A7C
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("even"))
			{
				return ForcePageCountMaker.s_propEVEN;
			}
			if (value.Equals("odd"))
			{
				return ForcePageCountMaker.s_propODD;
			}
			if (value.Equals("end-on-even"))
			{
				return ForcePageCountMaker.s_propEND_ON_EVEN;
			}
			if (value.Equals("end-on-odd"))
			{
				return ForcePageCountMaker.s_propEND_ON_ODD;
			}
			if (value.Equals("no-force"))
			{
				return ForcePageCountMaker.s_propNO_FORCE;
			}
			if (value.Equals("auto"))
			{
				return ForcePageCountMaker.s_propAUTO;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D5D7 RID: 54743 RVA: 0x002F5902 File Offset: 0x002F3B02
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A83 RID: 14979
		protected static readonly EnumProperty s_propEVEN = new EnumProperty(25);

		// Token: 0x04003A84 RID: 14980
		protected static readonly EnumProperty s_propODD = new EnumProperty(54);

		// Token: 0x04003A85 RID: 14981
		protected static readonly EnumProperty s_propEND_ON_EVEN = new EnumProperty(23);

		// Token: 0x04003A86 RID: 14982
		protected static readonly EnumProperty s_propEND_ON_ODD = new EnumProperty(24);

		// Token: 0x04003A87 RID: 14983
		protected static readonly EnumProperty s_propNO_FORCE = new EnumProperty(45);

		// Token: 0x04003A88 RID: 14984
		protected static readonly EnumProperty s_propAUTO = new EnumProperty(7);

		// Token: 0x04003A89 RID: 14985
		private Property m_defaultProp;
	}
}
