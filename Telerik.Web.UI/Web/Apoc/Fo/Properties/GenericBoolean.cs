using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014D8 RID: 5336
	internal class GenericBoolean : EnumProperty.Maker
	{
		// Token: 0x0600D5DD RID: 54749 RVA: 0x002F59BC File Offset: 0x002F3BBC
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericBoolean(propName);
		}

		// Token: 0x0600D5DE RID: 54750 RVA: 0x002F59C4 File Offset: 0x002F3BC4
		protected GenericBoolean(string name) : base(name)
		{
		}

		// Token: 0x0600D5DF RID: 54751 RVA: 0x002F59CD File Offset: 0x002F3BCD
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("true"))
			{
				return GenericBoolean.s_propTRUE;
			}
			if (value.Equals("false"))
			{
				return GenericBoolean.s_propFALSE;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x04003A8B RID: 14987
		protected static readonly EnumProperty s_propTRUE = new EnumProperty(81);

		// Token: 0x04003A8C RID: 14988
		protected static readonly EnumProperty s_propFALSE = new EnumProperty(27);

		// Token: 0x020014D9 RID: 5337
		internal class Enums
		{
			// Token: 0x04003A8D RID: 14989
			public const int TRUE = 81;

			// Token: 0x04003A8E RID: 14990
			public const int FALSE = 27;
		}
	}
}
