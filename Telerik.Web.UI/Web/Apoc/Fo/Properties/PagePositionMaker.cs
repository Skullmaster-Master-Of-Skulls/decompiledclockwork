using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001552 RID: 5458
	internal class PagePositionMaker : EnumProperty.Maker
	{
		// Token: 0x0600D798 RID: 55192 RVA: 0x002F7F9C File Offset: 0x002F619C
		public new static PropertyMaker Maker(string propName)
		{
			return new PagePositionMaker(propName);
		}

		// Token: 0x0600D799 RID: 55193 RVA: 0x002F7FA4 File Offset: 0x002F61A4
		protected PagePositionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D79A RID: 55194 RVA: 0x002F7FAD File Offset: 0x002F61AD
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D79B RID: 55195 RVA: 0x002F7FB0 File Offset: 0x002F61B0
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("first"))
			{
				return PagePositionMaker.s_propFIRST;
			}
			if (value.Equals("last"))
			{
				return PagePositionMaker.s_propLAST;
			}
			if (value.Equals("rest"))
			{
				return PagePositionMaker.s_propREST;
			}
			if (value.Equals("any"))
			{
				return PagePositionMaker.s_propANY;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D79C RID: 55196 RVA: 0x002F8010 File Offset: 0x002F6210
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "any", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B22 RID: 15138
		protected static readonly EnumProperty s_propFIRST = new EnumProperty(29);

		// Token: 0x04003B23 RID: 15139
		protected static readonly EnumProperty s_propLAST = new EnumProperty(38);

		// Token: 0x04003B24 RID: 15140
		protected static readonly EnumProperty s_propREST = new EnumProperty(62);

		// Token: 0x04003B25 RID: 15141
		protected static readonly EnumProperty s_propANY = new EnumProperty(6);

		// Token: 0x04003B26 RID: 15142
		private Property m_defaultProp;
	}
}
