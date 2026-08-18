using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200147F RID: 5247
	internal class BorderCollapseMaker : EnumProperty.Maker
	{
		// Token: 0x0600D48A RID: 54410 RVA: 0x002F1F71 File Offset: 0x002F0171
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderCollapseMaker(propName);
		}

		// Token: 0x0600D48B RID: 54411 RVA: 0x002F1F79 File Offset: 0x002F0179
		protected BorderCollapseMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D48C RID: 54412 RVA: 0x002F1F82 File Offset: 0x002F0182
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D48D RID: 54413 RVA: 0x002F1F85 File Offset: 0x002F0185
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "collapse", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D48E RID: 54414 RVA: 0x002F1FAD File Offset: 0x002F01AD
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("separate"))
			{
				return BorderCollapseMaker.s_propSEPARATE;
			}
			if (value.Equals("collapse"))
			{
				return BorderCollapseMaker.s_propCOLLAPSE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x040039D3 RID: 14803
		protected static readonly EnumProperty s_propSEPARATE = new EnumProperty(68);

		// Token: 0x040039D4 RID: 14804
		protected static readonly EnumProperty s_propCOLLAPSE = new EnumProperty(14);

		// Token: 0x040039D5 RID: 14805
		private Property m_defaultProp;
	}
}
