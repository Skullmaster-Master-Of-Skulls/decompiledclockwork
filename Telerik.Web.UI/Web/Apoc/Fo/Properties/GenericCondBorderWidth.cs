using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200146A RID: 5226
	internal class GenericCondBorderWidth : CondLengthProperty.Maker
	{
		// Token: 0x0600D43C RID: 54332 RVA: 0x002F1425 File Offset: 0x002EF625
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericCondBorderWidth(propName);
		}

		// Token: 0x0600D43D RID: 54333 RVA: 0x002F142D File Offset: 0x002EF62D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected GenericCondBorderWidth(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("length");
		}

		// Token: 0x0600D43E RID: 54334 RVA: 0x002F1447 File Offset: 0x002EF647
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D43F RID: 54335 RVA: 0x002F1455 File Offset: 0x002EF655
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D440 RID: 54336 RVA: 0x002F1458 File Offset: 0x002EF658
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("length"))
			{
				return GenericCondBorderWidth.s_LengthMaker;
			}
			if (subprop.Equals("conditionality"))
			{
				return GenericCondBorderWidth.s_ConditionalityMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D441 RID: 54337 RVA: 0x002F1488 File Offset: 0x002EF688
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			CondLength condLength = baseProp.GetCondLength();
			condLength.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D442 RID: 54338 RVA: 0x002F14A8 File Offset: 0x002EF6A8
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			CondLength condLength = baseProp.GetCondLength();
			return condLength.GetComponent(subpropName);
		}

		// Token: 0x0600D443 RID: 54339 RVA: 0x002F14C3 File Offset: 0x002EF6C3
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.MakeCompound(propertyList, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D444 RID: 54340 RVA: 0x002F14E8 File Offset: 0x002EF6E8
		protected override Property MakeCompound(PropertyList pList, FObj fo)
		{
			CondLength condLength = new CondLength();
			Property cmpnValue = this.GetSubpropMaker("length").Make(pList, this.getDefaultForLength(), fo);
			condLength.SetComponent("length", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("conditionality").Make(pList, this.getDefaultForConditionality(), fo);
			condLength.SetComponent("conditionality", cmpnValue, true);
			return new CondLengthProperty(condLength);
		}

		// Token: 0x0600D445 RID: 54341 RVA: 0x002F154D File Offset: 0x002EF74D
		protected virtual string getDefaultForLength()
		{
			return "medium";
		}

		// Token: 0x0600D446 RID: 54342 RVA: 0x002F1554 File Offset: 0x002EF754
		protected virtual string getDefaultForConditionality()
		{
			return "";
		}

		// Token: 0x0600D447 RID: 54343 RVA: 0x002F155C File Offset: 0x002EF75C
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

		// Token: 0x0600D448 RID: 54344 RVA: 0x002F15AE File Offset: 0x002EF7AE
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D449 RID: 54345 RVA: 0x002F15B4 File Offset: 0x002EF7B4
		private static void initKeywords()
		{
			GenericCondBorderWidth.s_htKeywords = new Hashtable(3);
			GenericCondBorderWidth.s_htKeywords.Add("thin", "0.5pt");
			GenericCondBorderWidth.s_htKeywords.Add("medium", "1pt");
			GenericCondBorderWidth.s_htKeywords.Add("thick", "2pt");
		}

		// Token: 0x0600D44A RID: 54346 RVA: 0x002F1608 File Offset: 0x002EF808
		protected override string CheckValueKeywords(string keyword)
		{
			if (GenericCondBorderWidth.s_htKeywords == null)
			{
				GenericCondBorderWidth.initKeywords();
			}
			string text = (string)GenericCondBorderWidth.s_htKeywords[keyword];
			if (text == null)
			{
				return base.CheckValueKeywords(keyword);
			}
			return text;
		}

		// Token: 0x040039C2 RID: 14786
		private static readonly PropertyMaker s_LengthMaker = new GenericCondBorderWidth.SP_LengthMaker("border-cond-width-template.length");

		// Token: 0x040039C3 RID: 14787
		private static readonly PropertyMaker s_ConditionalityMaker = new GenericCondBorderWidth.SP_ConditionalityMaker("border-cond-width-template.conditionality");

		// Token: 0x040039C4 RID: 14788
		private PropertyMaker m_shorthandMaker;

		// Token: 0x040039C5 RID: 14789
		private Property m_defaultProp;

		// Token: 0x040039C6 RID: 14790
		private static Hashtable s_htKeywords;

		// Token: 0x0200146B RID: 5227
		internal class Enums
		{
			// Token: 0x0200146C RID: 5228
			internal class Conditionality
			{
				// Token: 0x040039C7 RID: 14791
				public const int DISCARD = 17;

				// Token: 0x040039C8 RID: 14792
				public const int RETAIN = 63;
			}
		}

		// Token: 0x0200146D RID: 5229
		private class SP_LengthMaker : LengthProperty.Maker
		{
			// Token: 0x0600D44E RID: 54350 RVA: 0x002F166E File Offset: 0x002EF86E
			protected internal SP_LengthMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D44F RID: 54351 RVA: 0x002F1678 File Offset: 0x002EF878
			private static void initKeywords()
			{
				GenericCondBorderWidth.SP_LengthMaker.s_htKeywords = new Hashtable(3);
				GenericCondBorderWidth.SP_LengthMaker.s_htKeywords.Add("thin", "0.5pt");
				GenericCondBorderWidth.SP_LengthMaker.s_htKeywords.Add("medium", "1pt");
				GenericCondBorderWidth.SP_LengthMaker.s_htKeywords.Add("thick", "2pt");
			}

			// Token: 0x0600D450 RID: 54352 RVA: 0x002F16CC File Offset: 0x002EF8CC
			protected override string CheckValueKeywords(string keyword)
			{
				if (GenericCondBorderWidth.SP_LengthMaker.s_htKeywords == null)
				{
					GenericCondBorderWidth.SP_LengthMaker.initKeywords();
				}
				string text = (string)GenericCondBorderWidth.SP_LengthMaker.s_htKeywords[keyword];
				if (text == null)
				{
					return base.CheckValueKeywords(keyword);
				}
				return text;
			}

			// Token: 0x040039C9 RID: 14793
			private static Hashtable s_htKeywords;
		}

		// Token: 0x0200146E RID: 5230
		private class SP_ConditionalityMaker : EnumProperty.Maker
		{
			// Token: 0x0600D451 RID: 54353 RVA: 0x002F1702 File Offset: 0x002EF902
			protected internal SP_ConditionalityMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D452 RID: 54354 RVA: 0x002F170B File Offset: 0x002EF90B
			public override Property CheckEnumValues(string value)
			{
				if (value.Equals("discard"))
				{
					return GenericCondBorderWidth.SP_ConditionalityMaker.s_propDISCARD;
				}
				if (value.Equals("retain"))
				{
					return GenericCondBorderWidth.SP_ConditionalityMaker.s_propRETAIN;
				}
				return base.CheckEnumValues(value);
			}

			// Token: 0x040039CA RID: 14794
			protected internal static readonly EnumProperty s_propDISCARD = new EnumProperty(17);

			// Token: 0x040039CB RID: 14795
			protected internal static readonly EnumProperty s_propRETAIN = new EnumProperty(63);
		}
	}
}
