using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001568 RID: 5480
	internal class RetrieveBoundaryMaker : EnumProperty.Maker
	{
		// Token: 0x0600D7ED RID: 55277 RVA: 0x002F85AA File Offset: 0x002F67AA
		public new static PropertyMaker Maker(string propName)
		{
			return new RetrieveBoundaryMaker(propName);
		}

		// Token: 0x0600D7EE RID: 55278 RVA: 0x002F85B2 File Offset: 0x002F67B2
		protected RetrieveBoundaryMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7EF RID: 55279 RVA: 0x002F85BB File Offset: 0x002F67BB
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7F0 RID: 55280 RVA: 0x002F85C0 File Offset: 0x002F67C0
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("page"))
			{
				return RetrieveBoundaryMaker.s_propPAGE;
			}
			if (value.Equals("page-sequence"))
			{
				return RetrieveBoundaryMaker.s_propPAGE_SEQUENCE;
			}
			if (value.Equals("document"))
			{
				return RetrieveBoundaryMaker.s_propDOCUMENT;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D7F1 RID: 55281 RVA: 0x002F860D File Offset: 0x002F680D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "page-sequence", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B4B RID: 15179
		protected static readonly EnumProperty s_propPAGE = new EnumProperty(58);

		// Token: 0x04003B4C RID: 15180
		protected static readonly EnumProperty s_propPAGE_SEQUENCE = new EnumProperty(59);

		// Token: 0x04003B4D RID: 15181
		protected static readonly EnumProperty s_propDOCUMENT = new EnumProperty(18);

		// Token: 0x04003B4E RID: 15182
		private Property m_defaultProp;
	}
}
