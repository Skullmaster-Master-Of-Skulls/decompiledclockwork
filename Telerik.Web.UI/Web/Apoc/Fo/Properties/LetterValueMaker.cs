using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001521 RID: 5409
	internal class LetterValueMaker : EnumProperty.Maker
	{
		// Token: 0x0600D6E6 RID: 55014 RVA: 0x002F71A7 File Offset: 0x002F53A7
		public new static PropertyMaker Maker(string propName)
		{
			return new LetterValueMaker(propName);
		}

		// Token: 0x0600D6E7 RID: 55015 RVA: 0x002F71AF File Offset: 0x002F53AF
		protected LetterValueMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6E8 RID: 55016 RVA: 0x002F71B8 File Offset: 0x002F53B8
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D6E9 RID: 55017 RVA: 0x002F71BC File Offset: 0x002F53BC
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("alphabetic"))
			{
				return LetterValueMaker.s_propALPHABETIC;
			}
			if (value.Equals("traditional"))
			{
				return LetterValueMaker.s_propTRADITIONAL;
			}
			if (value.Equals("auto"))
			{
				return LetterValueMaker.s_propAUTO;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D6EA RID: 55018 RVA: 0x002F7209 File Offset: 0x002F5409
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AEE RID: 15086
		protected static readonly EnumProperty s_propALPHABETIC = new EnumProperty(4);

		// Token: 0x04003AEF RID: 15087
		protected static readonly EnumProperty s_propTRADITIONAL = new EnumProperty(80);

		// Token: 0x04003AF0 RID: 15088
		protected static readonly EnumProperty s_propAUTO = new EnumProperty(7);

		// Token: 0x04003AF1 RID: 15089
		private Property m_defaultProp;
	}
}
