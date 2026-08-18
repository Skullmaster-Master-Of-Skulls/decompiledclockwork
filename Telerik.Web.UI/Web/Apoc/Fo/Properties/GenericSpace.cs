using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014EA RID: 5354
	internal class GenericSpace : SpaceProperty.Maker
	{
		// Token: 0x0600D61D RID: 54813 RVA: 0x002F601D File Offset: 0x002F421D
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericSpace(propName);
		}

		// Token: 0x0600D61E RID: 54814 RVA: 0x002F6025 File Offset: 0x002F4225
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected GenericSpace(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("minimum");
		}

		// Token: 0x0600D61F RID: 54815 RVA: 0x002F603F File Offset: 0x002F423F
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D620 RID: 54816 RVA: 0x002F604D File Offset: 0x002F424D
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D621 RID: 54817 RVA: 0x002F6050 File Offset: 0x002F4250
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("minimum"))
			{
				return GenericSpace.s_MinimumMaker;
			}
			if (subprop.Equals("optimum"))
			{
				return GenericSpace.s_OptimumMaker;
			}
			if (subprop.Equals("maximum"))
			{
				return GenericSpace.s_MaximumMaker;
			}
			if (subprop.Equals("precedence"))
			{
				return GenericSpace.s_PrecedenceMaker;
			}
			if (subprop.Equals("conditionality"))
			{
				return GenericSpace.s_ConditionalityMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D622 RID: 54818 RVA: 0x002F60C4 File Offset: 0x002F42C4
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			Space space = baseProp.GetSpace();
			space.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D623 RID: 54819 RVA: 0x002F60E4 File Offset: 0x002F42E4
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			Space space = baseProp.GetSpace();
			return space.GetComponent(subpropName);
		}

		// Token: 0x0600D624 RID: 54820 RVA: 0x002F60FF File Offset: 0x002F42FF
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.MakeCompound(propertyList, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D625 RID: 54821 RVA: 0x002F6124 File Offset: 0x002F4324
		protected override Property MakeCompound(PropertyList pList, FObj fo)
		{
			Space space = new Space();
			Property cmpnValue = this.GetSubpropMaker("minimum").Make(pList, this.GetDefaultForMinimum(), fo);
			space.SetComponent("minimum", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("optimum").Make(pList, this.GetDefaultForOptimum(), fo);
			space.SetComponent("optimum", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("maximum").Make(pList, this.GetDefaultForMaximum(), fo);
			space.SetComponent("maximum", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("precedence").Make(pList, this.getDefaultForPrecedence(), fo);
			space.SetComponent("precedence", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("conditionality").Make(pList, this.getDefaultForConditionality(), fo);
			space.SetComponent("conditionality", cmpnValue, true);
			return new SpaceProperty(space);
		}

		// Token: 0x0600D626 RID: 54822 RVA: 0x002F61FB File Offset: 0x002F43FB
		protected virtual string GetDefaultForMinimum()
		{
			return "0pt";
		}

		// Token: 0x0600D627 RID: 54823 RVA: 0x002F6202 File Offset: 0x002F4402
		protected virtual string GetDefaultForOptimum()
		{
			return "0pt";
		}

		// Token: 0x0600D628 RID: 54824 RVA: 0x002F6209 File Offset: 0x002F4409
		protected virtual string GetDefaultForMaximum()
		{
			return "0pt";
		}

		// Token: 0x0600D629 RID: 54825 RVA: 0x002F6210 File Offset: 0x002F4410
		protected virtual string getDefaultForPrecedence()
		{
			return "0";
		}

		// Token: 0x0600D62A RID: 54826 RVA: 0x002F6217 File Offset: 0x002F4417
		protected virtual string getDefaultForConditionality()
		{
			return "discard";
		}

		// Token: 0x0600D62B RID: 54827 RVA: 0x002F6220 File Offset: 0x002F4420
		public override Property ConvertProperty(Property p, PropertyList pList, FObj fo)
		{
			if (p is SpaceProperty)
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
				Space space = property.GetSpace();
				space.SetComponent("minimum", p, false);
				space.SetComponent("optimum", p, false);
				space.SetComponent("maximum", p, false);
				return property;
			}
			return null;
		}

		// Token: 0x0600D62C RID: 54828 RVA: 0x002F628C File Offset: 0x002F448C
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x04003AAA RID: 15018
		private static readonly PropertyMaker s_MinimumMaker = new LengthProperty.Maker("generic-space.minimum");

		// Token: 0x04003AAB RID: 15019
		private static readonly PropertyMaker s_OptimumMaker = new LengthProperty.Maker("generic-space.optimum");

		// Token: 0x04003AAC RID: 15020
		private static readonly PropertyMaker s_MaximumMaker = new LengthProperty.Maker("generic-space.maximum");

		// Token: 0x04003AAD RID: 15021
		private static readonly PropertyMaker s_PrecedenceMaker = new GenericSpace.SP_PrecedenceMaker("generic-space.precedence");

		// Token: 0x04003AAE RID: 15022
		private static readonly PropertyMaker s_ConditionalityMaker = new GenericSpace.SP_ConditionalityMaker("generic-space.conditionality");

		// Token: 0x04003AAF RID: 15023
		private PropertyMaker m_shorthandMaker;

		// Token: 0x04003AB0 RID: 15024
		private Property m_defaultProp;

		// Token: 0x020014EB RID: 5355
		internal class Enums
		{
			// Token: 0x020014EC RID: 5356
			internal class Precedence
			{
				// Token: 0x04003AB1 RID: 15025
				public const int FORCE = 31;
			}

			// Token: 0x020014ED RID: 5357
			internal class Conditionality
			{
				// Token: 0x04003AB2 RID: 15026
				public const int DISCARD = 17;

				// Token: 0x04003AB3 RID: 15027
				public const int RETAIN = 63;
			}
		}

		// Token: 0x020014EE RID: 5358
		private class SP_PrecedenceMaker : NumberProperty.Maker
		{
			// Token: 0x0600D631 RID: 54833 RVA: 0x002F6300 File Offset: 0x002F4500
			protected internal SP_PrecedenceMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D632 RID: 54834 RVA: 0x002F6309 File Offset: 0x002F4509
			public override Property CheckEnumValues(string value)
			{
				if (value.Equals("force"))
				{
					return GenericSpace.SP_PrecedenceMaker.s_propFORCE;
				}
				return base.CheckEnumValues(value);
			}

			// Token: 0x04003AB4 RID: 15028
			protected internal static readonly EnumProperty s_propFORCE = new EnumProperty(31);
		}

		// Token: 0x020014EF RID: 5359
		private class SP_ConditionalityMaker : EnumProperty.Maker
		{
			// Token: 0x0600D634 RID: 54836 RVA: 0x002F6333 File Offset: 0x002F4533
			protected internal SP_ConditionalityMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D635 RID: 54837 RVA: 0x002F633C File Offset: 0x002F453C
			public override Property CheckEnumValues(string value)
			{
				if (value.Equals("discard"))
				{
					return GenericSpace.SP_ConditionalityMaker.s_propDISCARD;
				}
				if (value.Equals("retain"))
				{
					return GenericSpace.SP_ConditionalityMaker.s_propRETAIN;
				}
				return base.CheckEnumValues(value);
			}

			// Token: 0x04003AB5 RID: 15029
			protected internal static readonly EnumProperty s_propDISCARD = new EnumProperty(17);

			// Token: 0x04003AB6 RID: 15030
			protected internal static readonly EnumProperty s_propRETAIN = new EnumProperty(63);
		}
	}
}
