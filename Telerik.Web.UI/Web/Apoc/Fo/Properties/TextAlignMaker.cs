using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A0 RID: 5536
	internal class TextAlignMaker : EnumProperty.Maker
	{
		// Token: 0x0600D8A0 RID: 55456 RVA: 0x002F93D2 File Offset: 0x002F75D2
		public new static PropertyMaker Maker(string propName)
		{
			return new TextAlignMaker(propName);
		}

		// Token: 0x0600D8A1 RID: 55457 RVA: 0x002F93DA File Offset: 0x002F75DA
		protected TextAlignMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8A2 RID: 55458 RVA: 0x002F93E3 File Offset: 0x002F75E3
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8A3 RID: 55459 RVA: 0x002F93E8 File Offset: 0x002F75E8
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("center"))
			{
				return TextAlignMaker.s_propCENTER;
			}
			if (value.Equals("end"))
			{
				return TextAlignMaker.s_propEND;
			}
			if (value.Equals("right"))
			{
				return TextAlignMaker.s_propEND;
			}
			if (value.Equals("start"))
			{
				return TextAlignMaker.s_propSTART;
			}
			if (value.Equals("left"))
			{
				return TextAlignMaker.s_propSTART;
			}
			if (value.Equals("justify"))
			{
				return TextAlignMaker.s_propJUSTIFY;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D8A4 RID: 55460 RVA: 0x002F946E File Offset: 0x002F766E
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "start", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B9F RID: 15263
		protected static readonly EnumProperty s_propCENTER = new EnumProperty(13);

		// Token: 0x04003BA0 RID: 15264
		protected static readonly EnumProperty s_propEND = new EnumProperty(22);

		// Token: 0x04003BA1 RID: 15265
		protected static readonly EnumProperty s_propSTART = new EnumProperty(72);

		// Token: 0x04003BA2 RID: 15266
		protected static readonly EnumProperty s_propJUSTIFY = new EnumProperty(37);

		// Token: 0x04003BA3 RID: 15267
		private Property m_defaultProp;
	}
}
