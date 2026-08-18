using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001516 RID: 5398
	internal class LeaderAlignmentMaker : EnumProperty.Maker
	{
		// Token: 0x0600D6B2 RID: 54962 RVA: 0x002F6CBF File Offset: 0x002F4EBF
		public new static PropertyMaker Maker(string propName)
		{
			return new LeaderAlignmentMaker(propName);
		}

		// Token: 0x0600D6B3 RID: 54963 RVA: 0x002F6CC7 File Offset: 0x002F4EC7
		protected LeaderAlignmentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6B4 RID: 54964 RVA: 0x002F6CD0 File Offset: 0x002F4ED0
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6B5 RID: 54965 RVA: 0x002F6CD4 File Offset: 0x002F4ED4
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("none"))
			{
				return LeaderAlignmentMaker.s_propNONE;
			}
			if (value.Equals("reference-area"))
			{
				return LeaderAlignmentMaker.s_propREFERENCE_AREA;
			}
			if (value.Equals("page"))
			{
				return LeaderAlignmentMaker.s_propPAGE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D6B6 RID: 54966 RVA: 0x002F6D21 File Offset: 0x002F4F21
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AD7 RID: 15063
		protected static readonly EnumProperty s_propNONE = new EnumProperty(51);

		// Token: 0x04003AD8 RID: 15064
		protected static readonly EnumProperty s_propREFERENCE_AREA = new EnumProperty(60);

		// Token: 0x04003AD9 RID: 15065
		protected static readonly EnumProperty s_propPAGE = new EnumProperty(58);

		// Token: 0x04003ADA RID: 15066
		private Property m_defaultProp;
	}
}
