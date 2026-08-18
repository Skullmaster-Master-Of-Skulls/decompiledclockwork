using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200153B RID: 5435
	internal class OverflowMaker : EnumProperty.Maker
	{
		// Token: 0x0600D755 RID: 55125 RVA: 0x002F791E File Offset: 0x002F5B1E
		public new static PropertyMaker Maker(string propName)
		{
			return new OverflowMaker(propName);
		}

		// Token: 0x0600D756 RID: 55126 RVA: 0x002F7926 File Offset: 0x002F5B26
		protected OverflowMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D757 RID: 55127 RVA: 0x002F792F File Offset: 0x002F5B2F
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D758 RID: 55128 RVA: 0x002F7934 File Offset: 0x002F5B34
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("visible"))
			{
				return OverflowMaker.s_propVISIBLE;
			}
			if (value.Equals("hidden"))
			{
				return OverflowMaker.s_propHIDDEN;
			}
			if (value.Equals("scroll"))
			{
				return OverflowMaker.s_propSCROLL;
			}
			if (value.Equals("auto"))
			{
				return OverflowMaker.s_propAUTO;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D759 RID: 55129 RVA: 0x002F7994 File Offset: 0x002F5B94
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B15 RID: 15125
		protected static readonly EnumProperty s_propVISIBLE = new EnumProperty(85);

		// Token: 0x04003B16 RID: 15126
		protected static readonly EnumProperty s_propHIDDEN = new EnumProperty(34);

		// Token: 0x04003B17 RID: 15127
		protected static readonly EnumProperty s_propSCROLL = new EnumProperty(67);

		// Token: 0x04003B18 RID: 15128
		protected static readonly EnumProperty s_propAUTO = new EnumProperty(7);

		// Token: 0x04003B19 RID: 15129
		private Property m_defaultProp;
	}
}
