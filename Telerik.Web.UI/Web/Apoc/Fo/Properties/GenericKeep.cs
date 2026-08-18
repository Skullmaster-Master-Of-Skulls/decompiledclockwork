using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014DF RID: 5343
	internal class GenericKeep : KeepProperty.Maker
	{
		// Token: 0x0600D5F8 RID: 54776 RVA: 0x002F5C43 File Offset: 0x002F3E43
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericKeep(propName);
		}

		// Token: 0x0600D5F9 RID: 54777 RVA: 0x002F5C4B File Offset: 0x002F3E4B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected GenericKeep(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("within-page");
		}

		// Token: 0x0600D5FA RID: 54778 RVA: 0x002F5C65 File Offset: 0x002F3E65
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D5FB RID: 54779 RVA: 0x002F5C73 File Offset: 0x002F3E73
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D5FC RID: 54780 RVA: 0x002F5C78 File Offset: 0x002F3E78
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("within-page"))
			{
				return GenericKeep.s_WithinPageMaker;
			}
			if (subprop.Equals("within-line"))
			{
				return GenericKeep.s_WithinLineMaker;
			}
			if (subprop.Equals("within-column"))
			{
				return GenericKeep.s_WithinColumnMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D5FD RID: 54781 RVA: 0x002F5CC8 File Offset: 0x002F3EC8
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			Keep keep = baseProp.GetKeep();
			keep.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D5FE RID: 54782 RVA: 0x002F5CE8 File Offset: 0x002F3EE8
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			Keep keep = baseProp.GetKeep();
			return keep.GetComponent(subpropName);
		}

		// Token: 0x0600D5FF RID: 54783 RVA: 0x002F5D03 File Offset: 0x002F3F03
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.MakeCompound(propertyList, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D600 RID: 54784 RVA: 0x002F5D28 File Offset: 0x002F3F28
		protected override Property MakeCompound(PropertyList pList, FObj fo)
		{
			Keep keep = new Keep();
			Property cmpnValue = this.GetSubpropMaker("within-page").Make(pList, this.getDefaultForWithinPage(), fo);
			keep.SetComponent("within-page", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("within-line").Make(pList, this.getDefaultForWithinLine(), fo);
			keep.SetComponent("within-line", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("within-column").Make(pList, this.getDefaultForWithinColumn(), fo);
			keep.SetComponent("within-column", cmpnValue, true);
			return new KeepProperty(keep);
		}

		// Token: 0x0600D601 RID: 54785 RVA: 0x002F5DB3 File Offset: 0x002F3FB3
		protected virtual string getDefaultForWithinPage()
		{
			return "auto";
		}

		// Token: 0x0600D602 RID: 54786 RVA: 0x002F5DBA File Offset: 0x002F3FBA
		protected virtual string getDefaultForWithinLine()
		{
			return "auto";
		}

		// Token: 0x0600D603 RID: 54787 RVA: 0x002F5DC1 File Offset: 0x002F3FC1
		protected virtual string getDefaultForWithinColumn()
		{
			return "auto";
		}

		// Token: 0x0600D604 RID: 54788 RVA: 0x002F5DC8 File Offset: 0x002F3FC8
		public override Property ConvertProperty(Property p, PropertyList pList, FObj fo)
		{
			if (p is KeepProperty)
			{
				return p;
			}
			if (!(p is EnumProperty))
			{
				p = this.m_shorthandMaker.ConvertProperty(p, pList, fo);
			}
			if (p != null)
			{
				Property property = this.MakeCompound(pList, fo);
				Keep keep = property.GetKeep();
				keep.SetComponent("within-page", p, false);
				keep.SetComponent("within-line", p, false);
				keep.SetComponent("within-column", p, false);
				return property;
			}
			return null;
		}

		// Token: 0x04003A97 RID: 14999
		private static readonly PropertyMaker s_WithinPageMaker = new GenericKeep.SP_WithinPageMaker("generic-keep.within-page");

		// Token: 0x04003A98 RID: 15000
		private static readonly PropertyMaker s_WithinLineMaker = new GenericKeep.SP_WithinLineMaker("generic-keep.within-line");

		// Token: 0x04003A99 RID: 15001
		private static readonly PropertyMaker s_WithinColumnMaker = new GenericKeep.SP_WithinColumnMaker("generic-keep.within-column");

		// Token: 0x04003A9A RID: 15002
		private PropertyMaker m_shorthandMaker;

		// Token: 0x04003A9B RID: 15003
		private Property m_defaultProp;

		// Token: 0x020014E0 RID: 5344
		internal class Enums
		{
			// Token: 0x020014E1 RID: 5345
			internal class WithinPage
			{
				// Token: 0x04003A9C RID: 15004
				public const int AUTO = 7;

				// Token: 0x04003A9D RID: 15005
				public const int ALWAYS = 5;
			}

			// Token: 0x020014E2 RID: 5346
			internal class WithinLine
			{
				// Token: 0x04003A9E RID: 15006
				public const int AUTO = 7;

				// Token: 0x04003A9F RID: 15007
				public const int ALWAYS = 5;
			}

			// Token: 0x020014E3 RID: 5347
			internal class WithinColumn
			{
				// Token: 0x04003AA0 RID: 15008
				public const int AUTO = 7;

				// Token: 0x04003AA1 RID: 15009
				public const int ALWAYS = 5;
			}
		}

		// Token: 0x020014E4 RID: 5348
		private class SP_WithinPageMaker : NumberProperty.Maker
		{
			// Token: 0x0600D60A RID: 54794 RVA: 0x002F5E83 File Offset: 0x002F4083
			protected internal SP_WithinPageMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D60B RID: 54795 RVA: 0x002F5E8C File Offset: 0x002F408C
			public override Property CheckEnumValues(string value)
			{
				if (value.Equals("auto"))
				{
					return GenericKeep.SP_WithinPageMaker.s_propAUTO;
				}
				if (value.Equals("always"))
				{
					return GenericKeep.SP_WithinPageMaker.s_propALWAYS;
				}
				return base.CheckEnumValues(value);
			}

			// Token: 0x04003AA2 RID: 15010
			protected internal static readonly EnumProperty s_propAUTO = new EnumProperty(7);

			// Token: 0x04003AA3 RID: 15011
			protected internal static readonly EnumProperty s_propALWAYS = new EnumProperty(5);
		}

		// Token: 0x020014E5 RID: 5349
		private class SP_WithinLineMaker : NumberProperty.Maker
		{
			// Token: 0x0600D60D RID: 54797 RVA: 0x002F5ED3 File Offset: 0x002F40D3
			protected internal SP_WithinLineMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D60E RID: 54798 RVA: 0x002F5EDC File Offset: 0x002F40DC
			public override Property CheckEnumValues(string value)
			{
				if (value.Equals("auto"))
				{
					return GenericKeep.SP_WithinLineMaker.s_propAUTO;
				}
				if (value.Equals("always"))
				{
					return GenericKeep.SP_WithinLineMaker.s_propALWAYS;
				}
				return base.CheckEnumValues(value);
			}

			// Token: 0x04003AA4 RID: 15012
			protected internal static readonly EnumProperty s_propAUTO = new EnumProperty(7);

			// Token: 0x04003AA5 RID: 15013
			protected internal static readonly EnumProperty s_propALWAYS = new EnumProperty(5);
		}

		// Token: 0x020014E6 RID: 5350
		private class SP_WithinColumnMaker : NumberProperty.Maker
		{
			// Token: 0x0600D610 RID: 54800 RVA: 0x002F5F23 File Offset: 0x002F4123
			protected internal SP_WithinColumnMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D611 RID: 54801 RVA: 0x002F5F2C File Offset: 0x002F412C
			public override Property CheckEnumValues(string value)
			{
				if (value.Equals("auto"))
				{
					return GenericKeep.SP_WithinColumnMaker.s_propAUTO;
				}
				if (value.Equals("always"))
				{
					return GenericKeep.SP_WithinColumnMaker.s_propALWAYS;
				}
				return base.CheckEnumValues(value);
			}

			// Token: 0x04003AA6 RID: 15014
			protected internal static readonly EnumProperty s_propAUTO = new EnumProperty(7);

			// Token: 0x04003AA7 RID: 15015
			protected internal static readonly EnumProperty s_propALWAYS = new EnumProperty(5);
		}
	}
}
