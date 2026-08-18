using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001587 RID: 5511
	internal class SpanMaker : EnumProperty.Maker
	{
		// Token: 0x0600D842 RID: 55362 RVA: 0x002F8C02 File Offset: 0x002F6E02
		public new static PropertyMaker Maker(string propName)
		{
			return new SpanMaker(propName);
		}

		// Token: 0x0600D843 RID: 55363 RVA: 0x002F8C0A File Offset: 0x002F6E0A
		protected SpanMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D844 RID: 55364 RVA: 0x002F8C13 File Offset: 0x002F6E13
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D845 RID: 55365 RVA: 0x002F8C16 File Offset: 0x002F6E16
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("none"))
			{
				return SpanMaker.s_propNONE;
			}
			if (value.Equals("all"))
			{
				return SpanMaker.s_propALL;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D846 RID: 55366 RVA: 0x002F8C45 File Offset: 0x002F6E45
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B79 RID: 15225
		protected static readonly EnumProperty s_propNONE = new EnumProperty(51);

		// Token: 0x04003B7A RID: 15226
		protected static readonly EnumProperty s_propALL = new EnumProperty(3);

		// Token: 0x04003B7B RID: 15227
		private Property m_defaultProp;
	}
}
