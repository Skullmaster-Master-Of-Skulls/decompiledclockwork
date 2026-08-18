using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001493 RID: 5267
	internal class BorderSeparationMaker : LengthPairProperty.Maker
	{
		// Token: 0x0600D4D0 RID: 54480 RVA: 0x002F29C5 File Offset: 0x002F0BC5
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderSeparationMaker(propName);
		}

		// Token: 0x0600D4D1 RID: 54481 RVA: 0x002F29CD File Offset: 0x002F0BCD
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected BorderSeparationMaker(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("block-progression-direction");
		}

		// Token: 0x0600D4D2 RID: 54482 RVA: 0x002F29E7 File Offset: 0x002F0BE7
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D4D3 RID: 54483 RVA: 0x002F29F5 File Offset: 0x002F0BF5
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D4D4 RID: 54484 RVA: 0x002F29F8 File Offset: 0x002F0BF8
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("block-progression-direction"))
			{
				return BorderSeparationMaker.s_BlockProgressionDirectionMaker;
			}
			if (subprop.Equals("inline-progression-direction"))
			{
				return BorderSeparationMaker.s_InlineProgressionDirectionMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D4D5 RID: 54485 RVA: 0x002F2A28 File Offset: 0x002F0C28
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			LengthPair lengthPair = baseProp.GetLengthPair();
			lengthPair.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D4D6 RID: 54486 RVA: 0x002F2A48 File Offset: 0x002F0C48
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			LengthPair lengthPair = baseProp.GetLengthPair();
			return lengthPair.GetComponent(subpropName);
		}

		// Token: 0x0600D4D7 RID: 54487 RVA: 0x002F2A63 File Offset: 0x002F0C63
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.MakeCompound(propertyList, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D4D8 RID: 54488 RVA: 0x002F2A88 File Offset: 0x002F0C88
		protected override Property MakeCompound(PropertyList pList, FObj fo)
		{
			LengthPair lengthPair = new LengthPair();
			Property cmpnValue = this.GetSubpropMaker("block-progression-direction").Make(pList, this.getDefaultForBlockProgressionDirection(), fo);
			lengthPair.SetComponent("block-progression-direction", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("inline-progression-direction").Make(pList, this.getDefaultForInlineProgressionDirection(), fo);
			lengthPair.SetComponent("inline-progression-direction", cmpnValue, true);
			return new LengthPairProperty(lengthPair);
		}

		// Token: 0x0600D4D9 RID: 54489 RVA: 0x002F2AED File Offset: 0x002F0CED
		protected virtual string getDefaultForBlockProgressionDirection()
		{
			return "0pt";
		}

		// Token: 0x0600D4DA RID: 54490 RVA: 0x002F2AF4 File Offset: 0x002F0CF4
		protected virtual string getDefaultForInlineProgressionDirection()
		{
			return "0pt";
		}

		// Token: 0x0600D4DB RID: 54491 RVA: 0x002F2AFC File Offset: 0x002F0CFC
		public override Property ConvertProperty(Property p, PropertyList pList, FObj fo)
		{
			if (p is LengthPairProperty)
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
				LengthPair lengthPair = property.GetLengthPair();
				lengthPair.SetComponent("block-progression-direction", p, false);
				lengthPair.SetComponent("inline-progression-direction", p, false);
				return property;
			}
			return null;
		}

		// Token: 0x0600D4DC RID: 54492 RVA: 0x002F2B5B File Offset: 0x002F0D5B
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x040039DA RID: 14810
		private static readonly PropertyMaker s_BlockProgressionDirectionMaker = new LengthProperty.Maker("border-separation.block-progression-direction");

		// Token: 0x040039DB RID: 14811
		private static readonly PropertyMaker s_InlineProgressionDirectionMaker = new LengthProperty.Maker("border-separation.inline-progression-direction");

		// Token: 0x040039DC RID: 14812
		private PropertyMaker m_shorthandMaker;

		// Token: 0x040039DD RID: 14813
		private Property m_defaultProp;
	}
}
