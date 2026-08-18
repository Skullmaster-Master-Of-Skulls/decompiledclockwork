using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001465 RID: 5221
	internal class GenericBorderStyle : EnumProperty.Maker
	{
		// Token: 0x0600D42E RID: 54318 RVA: 0x002F1171 File Offset: 0x002EF371
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericBorderStyle(propName);
		}

		// Token: 0x0600D42F RID: 54319 RVA: 0x002F1179 File Offset: 0x002EF379
		protected GenericBorderStyle(string name) : base(name)
		{
		}

		// Token: 0x0600D430 RID: 54320 RVA: 0x002F1182 File Offset: 0x002EF382
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D431 RID: 54321 RVA: 0x002F1188 File Offset: 0x002EF388
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-style");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser = new BoxPropShorthandParser(listProperty);
					property = shorthandParser.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			return property;
		}

		// Token: 0x0600D432 RID: 54322 RVA: 0x002F11C8 File Offset: 0x002EF3C8
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("none"))
			{
				return GenericBorderStyle.s_propNONE;
			}
			if (value.Equals("hidden"))
			{
				return GenericBorderStyle.s_propHIDDEN;
			}
			if (value.Equals("dotted"))
			{
				return GenericBorderStyle.s_propDOTTED;
			}
			if (value.Equals("dashed"))
			{
				return GenericBorderStyle.s_propDASHED;
			}
			if (value.Equals("solid"))
			{
				return GenericBorderStyle.s_propSOLID;
			}
			if (value.Equals("double"))
			{
				return GenericBorderStyle.s_propDOUBLE;
			}
			if (value.Equals("groove"))
			{
				return GenericBorderStyle.s_propGROOVE;
			}
			if (value.Equals("ridge"))
			{
				return GenericBorderStyle.s_propRIDGE;
			}
			if (value.Equals("inset"))
			{
				return GenericBorderStyle.s_propINSET;
			}
			if (value.Equals("outset"))
			{
				return GenericBorderStyle.s_propOUTSET;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D433 RID: 54323 RVA: 0x002F129A File Offset: 0x002EF49A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039AD RID: 14765
		protected static readonly EnumProperty s_propNONE = new EnumProperty(51);

		// Token: 0x040039AE RID: 14766
		protected static readonly EnumProperty s_propHIDDEN = new EnumProperty(34);

		// Token: 0x040039AF RID: 14767
		protected static readonly EnumProperty s_propDOTTED = new EnumProperty(20);

		// Token: 0x040039B0 RID: 14768
		protected static readonly EnumProperty s_propDASHED = new EnumProperty(16);

		// Token: 0x040039B1 RID: 14769
		protected static readonly EnumProperty s_propSOLID = new EnumProperty(70);

		// Token: 0x040039B2 RID: 14770
		protected static readonly EnumProperty s_propDOUBLE = new EnumProperty(21);

		// Token: 0x040039B3 RID: 14771
		protected static readonly EnumProperty s_propGROOVE = new EnumProperty(33);

		// Token: 0x040039B4 RID: 14772
		protected static readonly EnumProperty s_propRIDGE = new EnumProperty(64);

		// Token: 0x040039B5 RID: 14773
		protected static readonly EnumProperty s_propINSET = new EnumProperty(36);

		// Token: 0x040039B6 RID: 14774
		protected static readonly EnumProperty s_propOUTSET = new EnumProperty(56);

		// Token: 0x040039B7 RID: 14775
		private Property m_defaultProp;

		// Token: 0x02001466 RID: 5222
		internal class Enums
		{
			// Token: 0x040039B8 RID: 14776
			public const int NONE = 51;

			// Token: 0x040039B9 RID: 14777
			public const int HIDDEN = 34;

			// Token: 0x040039BA RID: 14778
			public const int DOTTED = 20;

			// Token: 0x040039BB RID: 14779
			public const int DASHED = 16;

			// Token: 0x040039BC RID: 14780
			public const int SOLID = 70;

			// Token: 0x040039BD RID: 14781
			public const int DOUBLE = 21;

			// Token: 0x040039BE RID: 14782
			public const int GROOVE = 33;

			// Token: 0x040039BF RID: 14783
			public const int RIDGE = 64;

			// Token: 0x040039C0 RID: 14784
			public const int INSET = 36;

			// Token: 0x040039C1 RID: 14785
			public const int OUTSET = 56;
		}
	}
}
