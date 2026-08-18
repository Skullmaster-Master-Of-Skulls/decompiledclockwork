using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001595 RID: 5525
	internal class TableLayoutMaker : EnumProperty.Maker
	{
		// Token: 0x0600D87B RID: 55419 RVA: 0x002F90D1 File Offset: 0x002F72D1
		public new static PropertyMaker Maker(string propName)
		{
			return new TableLayoutMaker(propName);
		}

		// Token: 0x0600D87C RID: 55420 RVA: 0x002F90D9 File Offset: 0x002F72D9
		protected TableLayoutMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D87D RID: 55421 RVA: 0x002F90E2 File Offset: 0x002F72E2
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D87E RID: 55422 RVA: 0x002F90E5 File Offset: 0x002F72E5
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D87F RID: 55423 RVA: 0x002F910D File Offset: 0x002F730D
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("auto"))
			{
				return TableLayoutMaker.s_propAUTO;
			}
			if (value.Equals("fixed"))
			{
				return TableLayoutMaker.s_propFIXED;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x04003B8A RID: 15242
		protected static readonly EnumProperty s_propAUTO = new EnumProperty(7);

		// Token: 0x04003B8B RID: 15243
		protected static readonly EnumProperty s_propFIXED = new EnumProperty(30);

		// Token: 0x04003B8C RID: 15244
		private Property m_defaultProp;
	}
}
