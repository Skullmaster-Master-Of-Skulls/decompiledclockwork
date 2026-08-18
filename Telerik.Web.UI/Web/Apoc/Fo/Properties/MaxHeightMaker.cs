using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200152E RID: 5422
	internal class MaxHeightMaker : LengthProperty.Maker
	{
		// Token: 0x0600D721 RID: 55073 RVA: 0x002F75B3 File Offset: 0x002F57B3
		public new static PropertyMaker Maker(string propName)
		{
			return new MaxHeightMaker(propName);
		}

		// Token: 0x0600D722 RID: 55074 RVA: 0x002F75BB File Offset: 0x002F57BB
		protected MaxHeightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D723 RID: 55075 RVA: 0x002F75C4 File Offset: 0x002F57C4
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D724 RID: 55076 RVA: 0x002F75C7 File Offset: 0x002F57C7
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("none"))
			{
				return MaxHeightMaker.s_propNONE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D725 RID: 55077 RVA: 0x002F75E3 File Offset: 0x002F57E3
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AFE RID: 15102
		protected static readonly EnumProperty s_propNONE = new EnumProperty(51);

		// Token: 0x04003AFF RID: 15103
		private Property m_defaultProp;
	}
}
