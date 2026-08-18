using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014A4 RID: 5284
	internal class GenericBreak : EnumProperty.Maker
	{
		// Token: 0x0600D515 RID: 54549 RVA: 0x002F325C File Offset: 0x002F145C
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericBreak(propName);
		}

		// Token: 0x0600D516 RID: 54550 RVA: 0x002F3264 File Offset: 0x002F1464
		protected GenericBreak(string name) : base(name)
		{
		}

		// Token: 0x0600D517 RID: 54551 RVA: 0x002F326D File Offset: 0x002F146D
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D518 RID: 54552 RVA: 0x002F3270 File Offset: 0x002F1470
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("auto"))
			{
				return GenericBreak.s_propAUTO;
			}
			if (value.Equals("column"))
			{
				return GenericBreak.s_propCOLUMN;
			}
			if (value.Equals("page"))
			{
				return GenericBreak.s_propPAGE;
			}
			if (value.Equals("even-page"))
			{
				return GenericBreak.s_propEVEN_PAGE;
			}
			if (value.Equals("odd-page"))
			{
				return GenericBreak.s_propODD_PAGE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D519 RID: 54553 RVA: 0x002F32E3 File Offset: 0x002F14E3
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039E3 RID: 14819
		protected static readonly EnumProperty s_propAUTO = new EnumProperty(7);

		// Token: 0x040039E4 RID: 14820
		protected static readonly EnumProperty s_propCOLUMN = new EnumProperty(15);

		// Token: 0x040039E5 RID: 14821
		protected static readonly EnumProperty s_propPAGE = new EnumProperty(58);

		// Token: 0x040039E6 RID: 14822
		protected static readonly EnumProperty s_propEVEN_PAGE = new EnumProperty(26);

		// Token: 0x040039E7 RID: 14823
		protected static readonly EnumProperty s_propODD_PAGE = new EnumProperty(55);

		// Token: 0x040039E8 RID: 14824
		private Property m_defaultProp;

		// Token: 0x020014A5 RID: 5285
		internal class Enums
		{
			// Token: 0x040039E9 RID: 14825
			public const int AUTO = 7;

			// Token: 0x040039EA RID: 14826
			public const int COLUMN = 15;

			// Token: 0x040039EB RID: 14827
			public const int PAGE = 58;

			// Token: 0x040039EC RID: 14828
			public const int EVEN_PAGE = 26;

			// Token: 0x040039ED RID: 14829
			public const int ODD_PAGE = 55;
		}
	}
}
