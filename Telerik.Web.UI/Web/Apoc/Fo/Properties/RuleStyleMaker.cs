using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001570 RID: 5488
	internal class RuleStyleMaker : EnumProperty.Maker
	{
		// Token: 0x0600D80C RID: 55308 RVA: 0x002F882D File Offset: 0x002F6A2D
		public new static PropertyMaker Maker(string propName)
		{
			return new RuleStyleMaker(propName);
		}

		// Token: 0x0600D80D RID: 55309 RVA: 0x002F8835 File Offset: 0x002F6A35
		protected RuleStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D80E RID: 55310 RVA: 0x002F883E File Offset: 0x002F6A3E
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D80F RID: 55311 RVA: 0x002F8844 File Offset: 0x002F6A44
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("none"))
			{
				return RuleStyleMaker.s_propNONE;
			}
			if (value.Equals("dotted"))
			{
				return RuleStyleMaker.s_propDOTTED;
			}
			if (value.Equals("dashed"))
			{
				return RuleStyleMaker.s_propDASHED;
			}
			if (value.Equals("solid"))
			{
				return RuleStyleMaker.s_propSOLID;
			}
			if (value.Equals("double"))
			{
				return RuleStyleMaker.s_propDOUBLE;
			}
			if (value.Equals("groove"))
			{
				return RuleStyleMaker.s_propGROOVE;
			}
			if (value.Equals("ridge"))
			{
				return RuleStyleMaker.s_propRIDGE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D810 RID: 55312 RVA: 0x002F88DD File Offset: 0x002F6ADD
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "solid", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B63 RID: 15203
		protected static readonly EnumProperty s_propNONE = new EnumProperty(51);

		// Token: 0x04003B64 RID: 15204
		protected static readonly EnumProperty s_propDOTTED = new EnumProperty(20);

		// Token: 0x04003B65 RID: 15205
		protected static readonly EnumProperty s_propDASHED = new EnumProperty(16);

		// Token: 0x04003B66 RID: 15206
		protected static readonly EnumProperty s_propSOLID = new EnumProperty(70);

		// Token: 0x04003B67 RID: 15207
		protected static readonly EnumProperty s_propDOUBLE = new EnumProperty(21);

		// Token: 0x04003B68 RID: 15208
		protected static readonly EnumProperty s_propGROOVE = new EnumProperty(33);

		// Token: 0x04003B69 RID: 15209
		protected static readonly EnumProperty s_propRIDGE = new EnumProperty(64);

		// Token: 0x04003B6A RID: 15210
		private Property m_defaultProp;
	}
}
