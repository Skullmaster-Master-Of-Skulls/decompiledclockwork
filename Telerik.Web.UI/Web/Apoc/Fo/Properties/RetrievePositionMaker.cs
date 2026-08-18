using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200156B RID: 5483
	internal class RetrievePositionMaker : EnumProperty.Maker
	{
		// Token: 0x0600D7F8 RID: 55288 RVA: 0x002F869F File Offset: 0x002F689F
		public new static PropertyMaker Maker(string propName)
		{
			return new RetrievePositionMaker(propName);
		}

		// Token: 0x0600D7F9 RID: 55289 RVA: 0x002F86A7 File Offset: 0x002F68A7
		protected RetrievePositionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7FA RID: 55290 RVA: 0x002F86B0 File Offset: 0x002F68B0
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7FB RID: 55291 RVA: 0x002F86B4 File Offset: 0x002F68B4
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("first-starting-within-page"))
			{
				return RetrievePositionMaker.s_propFSWP;
			}
			if (value.Equals("first-including-carryover"))
			{
				return RetrievePositionMaker.s_propFIC;
			}
			if (value.Equals("last-starting-within-page"))
			{
				return RetrievePositionMaker.s_propLSWP;
			}
			if (value.Equals("last-ending-within-page"))
			{
				return RetrievePositionMaker.s_propLEWP;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D7FC RID: 55292 RVA: 0x002F8714 File Offset: 0x002F6914
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "first-starting-within-page", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B54 RID: 15188
		protected static readonly EnumProperty s_propFSWP = new EnumProperty(32);

		// Token: 0x04003B55 RID: 15189
		protected static readonly EnumProperty s_propFIC = new EnumProperty(28);

		// Token: 0x04003B56 RID: 15190
		protected static readonly EnumProperty s_propLSWP = new EnumProperty(42);

		// Token: 0x04003B57 RID: 15191
		protected static readonly EnumProperty s_propLEWP = new EnumProperty(39);

		// Token: 0x04003B58 RID: 15192
		private Property m_defaultProp;
	}
}
