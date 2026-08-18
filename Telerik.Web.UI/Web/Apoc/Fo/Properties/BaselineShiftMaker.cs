using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200145C RID: 5212
	internal class BaselineShiftMaker : LengthProperty.Maker
	{
		// Token: 0x0600D3FD RID: 54269 RVA: 0x002F0B0D File Offset: 0x002EED0D
		public new static PropertyMaker Maker(string propName)
		{
			return new BaselineShiftMaker(propName);
		}

		// Token: 0x0600D3FE RID: 54270 RVA: 0x002F0B15 File Offset: 0x002EED15
		protected BaselineShiftMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3FF RID: 54271 RVA: 0x002F0B1E File Offset: 0x002EED1E
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D400 RID: 54272 RVA: 0x002F0B24 File Offset: 0x002EED24
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("baseline"))
			{
				return BaselineShiftMaker.s_propBASELINE;
			}
			if (value.Equals("sub"))
			{
				return BaselineShiftMaker.s_propSUB;
			}
			if (value.Equals("super"))
			{
				return BaselineShiftMaker.s_propSUPER;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D401 RID: 54273 RVA: 0x002F0B71 File Offset: 0x002EED71
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "baseline", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0400399B RID: 14747
		protected static readonly EnumProperty s_propBASELINE = new EnumProperty(8);

		// Token: 0x0400399C RID: 14748
		protected static readonly EnumProperty s_propSUB = new EnumProperty(74);

		// Token: 0x0400399D RID: 14749
		protected static readonly EnumProperty s_propSUPER = new EnumProperty(75);

		// Token: 0x0400399E RID: 14750
		private Property m_defaultProp;
	}
}
