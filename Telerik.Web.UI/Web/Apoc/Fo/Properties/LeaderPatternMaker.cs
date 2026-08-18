using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200151C RID: 5404
	internal class LeaderPatternMaker : EnumProperty.Maker
	{
		// Token: 0x0600D6CF RID: 54991 RVA: 0x002F6FC9 File Offset: 0x002F51C9
		public new static PropertyMaker Maker(string propName)
		{
			return new LeaderPatternMaker(propName);
		}

		// Token: 0x0600D6D0 RID: 54992 RVA: 0x002F6FD1 File Offset: 0x002F51D1
		protected LeaderPatternMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6D1 RID: 54993 RVA: 0x002F6FDA File Offset: 0x002F51DA
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6D2 RID: 54994 RVA: 0x002F6FE0 File Offset: 0x002F51E0
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("space"))
			{
				return LeaderPatternMaker.s_propSPACE;
			}
			if (value.Equals("rule"))
			{
				return LeaderPatternMaker.s_propRULE;
			}
			if (value.Equals("dots"))
			{
				return LeaderPatternMaker.s_propDOTS;
			}
			if (value.Equals("use-content"))
			{
				return LeaderPatternMaker.s_propUSECONTENT;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D6D3 RID: 54995 RVA: 0x002F7040 File Offset: 0x002F5240
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "space", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AE3 RID: 15075
		protected static readonly EnumProperty s_propSPACE = new EnumProperty(71);

		// Token: 0x04003AE4 RID: 15076
		protected static readonly EnumProperty s_propRULE = new EnumProperty(66);

		// Token: 0x04003AE5 RID: 15077
		protected static readonly EnumProperty s_propDOTS = new EnumProperty(19);

		// Token: 0x04003AE6 RID: 15078
		protected static readonly EnumProperty s_propUSECONTENT = new EnumProperty(84);

		// Token: 0x04003AE7 RID: 15079
		private Property m_defaultProp;
	}
}
