using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001538 RID: 5432
	internal class OddOrEvenMaker : EnumProperty.Maker
	{
		// Token: 0x0600D74A RID: 55114 RVA: 0x002F782B File Offset: 0x002F5A2B
		public new static PropertyMaker Maker(string propName)
		{
			return new OddOrEvenMaker(propName);
		}

		// Token: 0x0600D74B RID: 55115 RVA: 0x002F7833 File Offset: 0x002F5A33
		protected OddOrEvenMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D74C RID: 55116 RVA: 0x002F783C File Offset: 0x002F5A3C
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D74D RID: 55117 RVA: 0x002F7840 File Offset: 0x002F5A40
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("odd"))
			{
				return OddOrEvenMaker.s_propODD;
			}
			if (value.Equals("even"))
			{
				return OddOrEvenMaker.s_propEVEN;
			}
			if (value.Equals("any"))
			{
				return OddOrEvenMaker.s_propANY;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D74E RID: 55118 RVA: 0x002F788D File Offset: 0x002F5A8D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "any", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B0C RID: 15116
		protected static readonly EnumProperty s_propODD = new EnumProperty(54);

		// Token: 0x04003B0D RID: 15117
		protected static readonly EnumProperty s_propEVEN = new EnumProperty(25);

		// Token: 0x04003B0E RID: 15118
		protected static readonly EnumProperty s_propANY = new EnumProperty(6);

		// Token: 0x04003B0F RID: 15119
		private Property m_defaultProp;
	}
}
