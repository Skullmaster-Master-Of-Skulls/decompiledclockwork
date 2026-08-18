using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200155D RID: 5469
	internal class PrecedenceMaker : EnumProperty.Maker
	{
		// Token: 0x0600D7C3 RID: 55235 RVA: 0x002F82ED File Offset: 0x002F64ED
		public new static PropertyMaker Maker(string propName)
		{
			return new PrecedenceMaker(propName);
		}

		// Token: 0x0600D7C4 RID: 55236 RVA: 0x002F82F5 File Offset: 0x002F64F5
		protected PrecedenceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7C5 RID: 55237 RVA: 0x002F82FE File Offset: 0x002F64FE
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7C6 RID: 55238 RVA: 0x002F8301 File Offset: 0x002F6501
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("true"))
			{
				return PrecedenceMaker.s_propTRUE;
			}
			if (value.Equals("false"))
			{
				return PrecedenceMaker.s_propFALSE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D7C7 RID: 55239 RVA: 0x002F8330 File Offset: 0x002F6530
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B39 RID: 15161
		protected static readonly EnumProperty s_propTRUE = new EnumProperty(81);

		// Token: 0x04003B3A RID: 15162
		protected static readonly EnumProperty s_propFALSE = new EnumProperty(27);

		// Token: 0x04003B3B RID: 15163
		private Property m_defaultProp;
	}
}
