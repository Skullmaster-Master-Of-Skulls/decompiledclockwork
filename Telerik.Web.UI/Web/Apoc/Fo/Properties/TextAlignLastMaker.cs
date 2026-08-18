using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200159F RID: 5535
	internal class TextAlignLastMaker : EnumProperty.Maker
	{
		// Token: 0x0600D899 RID: 55449 RVA: 0x002F92A1 File Offset: 0x002F74A1
		public new static PropertyMaker Maker(string propName)
		{
			return new TextAlignLastMaker(propName);
		}

		// Token: 0x0600D89A RID: 55450 RVA: 0x002F92A9 File Offset: 0x002F74A9
		protected TextAlignLastMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D89B RID: 55451 RVA: 0x002F92B2 File Offset: 0x002F74B2
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D89C RID: 55452 RVA: 0x002F92B8 File Offset: 0x002F74B8
		public override Property Compute(PropertyList propertyList)
		{
			Property result = null;
			Property property = propertyList.GetProperty("text-align");
			if (property != null)
			{
				int @enum = property.GetEnum();
				if (@enum == 37)
				{
					result = new EnumProperty(72);
				}
				else if (@enum == 22)
				{
					result = new EnumProperty(22);
				}
				else if (@enum == 72)
				{
					result = new EnumProperty(72);
				}
				else if (@enum == 13)
				{
					result = new EnumProperty(13);
				}
			}
			return result;
		}

		// Token: 0x0600D89D RID: 55453 RVA: 0x002F9318 File Offset: 0x002F7518
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("center"))
			{
				return TextAlignLastMaker.s_propCENTER;
			}
			if (value.Equals("end"))
			{
				return TextAlignLastMaker.s_propEND;
			}
			if (value.Equals("start"))
			{
				return TextAlignLastMaker.s_propSTART;
			}
			if (value.Equals("justify"))
			{
				return TextAlignLastMaker.s_propJUSTIFY;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D89E RID: 55454 RVA: 0x002F9378 File Offset: 0x002F7578
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "start", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B9A RID: 15258
		protected static readonly EnumProperty s_propCENTER = new EnumProperty(13);

		// Token: 0x04003B9B RID: 15259
		protected static readonly EnumProperty s_propEND = new EnumProperty(22);

		// Token: 0x04003B9C RID: 15260
		protected static readonly EnumProperty s_propSTART = new EnumProperty(72);

		// Token: 0x04003B9D RID: 15261
		protected static readonly EnumProperty s_propJUSTIFY = new EnumProperty(37);

		// Token: 0x04003B9E RID: 15262
		private Property m_defaultProp;
	}
}
