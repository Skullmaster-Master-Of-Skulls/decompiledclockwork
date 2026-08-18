using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001449 RID: 5193
	internal class AbsolutePositionMaker : EnumProperty.Maker
	{
		// Token: 0x0600D3B5 RID: 54197 RVA: 0x002EF9EB File Offset: 0x002EDBEB
		public new static PropertyMaker Maker(string propName)
		{
			return new AbsolutePositionMaker(propName);
		}

		// Token: 0x0600D3B6 RID: 54198 RVA: 0x002EF9F3 File Offset: 0x002EDBF3
		protected AbsolutePositionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3B7 RID: 54199 RVA: 0x002EF9FC File Offset: 0x002EDBFC
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3B8 RID: 54200 RVA: 0x002EFA00 File Offset: 0x002EDC00
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("auto"))
			{
				return AbsolutePositionMaker.s_propAUTO;
			}
			if (value.Equals("fixed"))
			{
				return AbsolutePositionMaker.s_propFIXED;
			}
			if (value.Equals("absolute"))
			{
				return AbsolutePositionMaker.s_propABSOLUTE;
			}
			if (value.Equals("inherit"))
			{
				return AbsolutePositionMaker.s_propINHERIT;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D3B9 RID: 54201 RVA: 0x002EFA60 File Offset: 0x002EDC60
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0400397A RID: 14714
		protected static readonly EnumProperty s_propAUTO = new EnumProperty(7);

		// Token: 0x0400397B RID: 14715
		protected static readonly EnumProperty s_propFIXED = new EnumProperty(30);

		// Token: 0x0400397C RID: 14716
		protected static readonly EnumProperty s_propABSOLUTE = new EnumProperty(1);

		// Token: 0x0400397D RID: 14717
		protected static readonly EnumProperty s_propINHERIT = new EnumProperty(35);

		// Token: 0x0400397E RID: 14718
		private Property m_defaultProp;
	}
}
