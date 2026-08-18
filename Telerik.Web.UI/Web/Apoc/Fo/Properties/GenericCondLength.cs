using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014DA RID: 5338
	internal class GenericCondLength : CondLengthProperty.Maker
	{
		// Token: 0x0600D5E2 RID: 54754 RVA: 0x002F5A1E File Offset: 0x002F3C1E
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericCondLength(propName);
		}

		// Token: 0x0600D5E3 RID: 54755 RVA: 0x002F5A26 File Offset: 0x002F3C26
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected GenericCondLength(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("length");
		}

		// Token: 0x0600D5E4 RID: 54756 RVA: 0x002F5A40 File Offset: 0x002F3C40
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D5E5 RID: 54757 RVA: 0x002F5A4E File Offset: 0x002F3C4E
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D5E6 RID: 54758 RVA: 0x002F5A51 File Offset: 0x002F3C51
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("length"))
			{
				return GenericCondLength.s_LengthMaker;
			}
			if (subprop.Equals("conditionality"))
			{
				return GenericCondLength.s_ConditionalityMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D5E7 RID: 54759 RVA: 0x002F5A80 File Offset: 0x002F3C80
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			CondLength condLength = baseProp.GetCondLength();
			condLength.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D5E8 RID: 54760 RVA: 0x002F5AA0 File Offset: 0x002F3CA0
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			CondLength condLength = baseProp.GetCondLength();
			return condLength.GetComponent(subpropName);
		}

		// Token: 0x0600D5E9 RID: 54761 RVA: 0x002F5ABB File Offset: 0x002F3CBB
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.MakeCompound(propertyList, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D5EA RID: 54762 RVA: 0x002F5AE0 File Offset: 0x002F3CE0
		protected override Property MakeCompound(PropertyList pList, FObj fo)
		{
			CondLength condLength = new CondLength();
			Property cmpnValue = this.GetSubpropMaker("length").Make(pList, this.getDefaultForLength(), fo);
			condLength.SetComponent("length", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("conditionality").Make(pList, this.getDefaultForConditionality(), fo);
			condLength.SetComponent("conditionality", cmpnValue, true);
			return new CondLengthProperty(condLength);
		}

		// Token: 0x0600D5EB RID: 54763 RVA: 0x002F5B45 File Offset: 0x002F3D45
		protected virtual string getDefaultForLength()
		{
			return "";
		}

		// Token: 0x0600D5EC RID: 54764 RVA: 0x002F5B4C File Offset: 0x002F3D4C
		protected virtual string getDefaultForConditionality()
		{
			return "";
		}

		// Token: 0x0600D5ED RID: 54765 RVA: 0x002F5B54 File Offset: 0x002F3D54
		public override Property ConvertProperty(Property p, PropertyList pList, FObj fo)
		{
			if (p is CondLengthProperty)
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
				CondLength condLength = property.GetCondLength();
				condLength.SetComponent("length", p, false);
				return property;
			}
			return null;
		}

		// Token: 0x04003A8F RID: 14991
		private static readonly PropertyMaker s_LengthMaker = new LengthProperty.Maker("conditional-length-template.length");

		// Token: 0x04003A90 RID: 14992
		private static readonly PropertyMaker s_ConditionalityMaker = new GenericCondLength.SP_ConditionalityMaker("conditional-length-template.conditionality");

		// Token: 0x04003A91 RID: 14993
		private PropertyMaker m_shorthandMaker;

		// Token: 0x04003A92 RID: 14994
		private Property m_defaultProp;

		// Token: 0x020014DB RID: 5339
		internal class Enums
		{
			// Token: 0x020014DC RID: 5340
			internal class Conditionality
			{
				// Token: 0x04003A93 RID: 14995
				public const int DISCARD = 17;

				// Token: 0x04003A94 RID: 14996
				public const int RETAIN = 63;
			}
		}

		// Token: 0x020014DD RID: 5341
		private class SP_ConditionalityMaker : EnumProperty.Maker
		{
			// Token: 0x0600D5F1 RID: 54769 RVA: 0x002F5BD6 File Offset: 0x002F3DD6
			protected internal SP_ConditionalityMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D5F2 RID: 54770 RVA: 0x002F5BDF File Offset: 0x002F3DDF
			public override Property CheckEnumValues(string value)
			{
				if (value.Equals("discard"))
				{
					return GenericCondLength.SP_ConditionalityMaker.s_propDISCARD;
				}
				if (value.Equals("retain"))
				{
					return GenericCondLength.SP_ConditionalityMaker.s_propRETAIN;
				}
				return base.CheckEnumValues(value);
			}

			// Token: 0x04003A95 RID: 14997
			protected internal static readonly EnumProperty s_propDISCARD = new EnumProperty(17);

			// Token: 0x04003A96 RID: 14998
			protected internal static readonly EnumProperty s_propRETAIN = new EnumProperty(63);
		}
	}
}
