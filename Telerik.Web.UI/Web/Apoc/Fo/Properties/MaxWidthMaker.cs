using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001530 RID: 5424
	internal class MaxWidthMaker : LengthProperty.Maker
	{
		// Token: 0x0600D72B RID: 55083 RVA: 0x002F7655 File Offset: 0x002F5855
		public new static PropertyMaker Maker(string propName)
		{
			return new MaxWidthMaker(propName);
		}

		// Token: 0x0600D72C RID: 55084 RVA: 0x002F765D File Offset: 0x002F585D
		protected MaxWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D72D RID: 55085 RVA: 0x002F7666 File Offset: 0x002F5866
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D72E RID: 55086 RVA: 0x002F7669 File Offset: 0x002F5869
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("none"))
			{
				return MaxWidthMaker.s_propNONE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D72F RID: 55087 RVA: 0x002F7685 File Offset: 0x002F5885
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B01 RID: 15105
		protected static readonly EnumProperty s_propNONE = new EnumProperty(51);

		// Token: 0x04003B02 RID: 15106
		private Property m_defaultProp;
	}
}
