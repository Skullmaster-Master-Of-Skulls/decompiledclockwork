using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014FF RID: 5375
	internal class InlineProgressionDimensionMaker : LengthRangeProperty.Maker
	{
		// Token: 0x0600D673 RID: 54899 RVA: 0x002F6721 File Offset: 0x002F4921
		public new static PropertyMaker Maker(string propName)
		{
			return new InlineProgressionDimensionMaker(propName);
		}

		// Token: 0x0600D674 RID: 54900 RVA: 0x002F6729 File Offset: 0x002F4929
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected InlineProgressionDimensionMaker(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("minimum");
		}

		// Token: 0x0600D675 RID: 54901 RVA: 0x002F6743 File Offset: 0x002F4943
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D676 RID: 54902 RVA: 0x002F6751 File Offset: 0x002F4951
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D677 RID: 54903 RVA: 0x002F6754 File Offset: 0x002F4954
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("minimum"))
			{
				return InlineProgressionDimensionMaker.s_MinimumMaker;
			}
			if (subprop.Equals("optimum"))
			{
				return InlineProgressionDimensionMaker.s_OptimumMaker;
			}
			if (subprop.Equals("maximum"))
			{
				return InlineProgressionDimensionMaker.s_MaximumMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D678 RID: 54904 RVA: 0x002F67A4 File Offset: 0x002F49A4
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			LengthRange lengthRange = baseProp.GetLengthRange();
			lengthRange.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D679 RID: 54905 RVA: 0x002F67C4 File Offset: 0x002F49C4
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			LengthRange lengthRange = baseProp.GetLengthRange();
			return lengthRange.GetComponent(subpropName);
		}

		// Token: 0x0600D67A RID: 54906 RVA: 0x002F67DF File Offset: 0x002F49DF
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.MakeCompound(propertyList, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D67B RID: 54907 RVA: 0x002F6804 File Offset: 0x002F4A04
		protected override Property MakeCompound(PropertyList pList, FObj fo)
		{
			LengthRange lengthRange = new LengthRange();
			Property cmpnValue = this.GetSubpropMaker("minimum").Make(pList, this.GetDefaultForMinimum(), fo);
			lengthRange.SetComponent("minimum", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("optimum").Make(pList, this.GetDefaultForOptimum(), fo);
			lengthRange.SetComponent("optimum", cmpnValue, true);
			cmpnValue = this.GetSubpropMaker("maximum").Make(pList, this.GetDefaultForMaximum(), fo);
			lengthRange.SetComponent("maximum", cmpnValue, true);
			return new LengthRangeProperty(lengthRange);
		}

		// Token: 0x0600D67C RID: 54908 RVA: 0x002F688F File Offset: 0x002F4A8F
		protected virtual string GetDefaultForMinimum()
		{
			return "auto";
		}

		// Token: 0x0600D67D RID: 54909 RVA: 0x002F6896 File Offset: 0x002F4A96
		protected virtual string GetDefaultForOptimum()
		{
			return "auto";
		}

		// Token: 0x0600D67E RID: 54910 RVA: 0x002F689D File Offset: 0x002F4A9D
		protected virtual string GetDefaultForMaximum()
		{
			return "auto";
		}

		// Token: 0x0600D67F RID: 54911 RVA: 0x002F68A4 File Offset: 0x002F4AA4
		public override Property ConvertProperty(Property p, PropertyList pList, FObj fo)
		{
			if (p is LengthRangeProperty)
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
				LengthRange lengthRange = property.GetLengthRange();
				lengthRange.SetComponent("minimum", p, false);
				lengthRange.SetComponent("optimum", p, false);
				lengthRange.SetComponent("maximum", p, false);
				return property;
			}
			return null;
		}

		// Token: 0x0600D680 RID: 54912 RVA: 0x002F6910 File Offset: 0x002F4B10
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D681 RID: 54913 RVA: 0x002F6914 File Offset: 0x002F4B14
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append(propertyList.wmRelToAbs(5));
			if (propertyList.GetExplicitProperty(stringBuilder.ToString()) != null)
			{
				return true;
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("min-");
			stringBuilder.Append(propertyList.wmRelToAbs(5));
			if (propertyList.GetExplicitProperty(stringBuilder.ToString()) != null)
			{
				return true;
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("max-");
			stringBuilder.Append(propertyList.wmRelToAbs(5));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D682 RID: 54914 RVA: 0x002F69B0 File Offset: 0x002F4BB0
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(propertyList.wmRelToAbs(5));
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			else
			{
				property = this.MakeCompound(propertyList, parentFObj);
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("min-");
			stringBuilder.Append(propertyList.wmRelToAbs(5));
			Property explicitOrShorthandProperty = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (explicitOrShorthandProperty != null)
			{
				this.SetSubprop(property, "minimum", explicitOrShorthandProperty);
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("max-");
			stringBuilder.Append(propertyList.wmRelToAbs(5));
			explicitOrShorthandProperty = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (explicitOrShorthandProperty != null)
			{
				this.SetSubprop(property, "maximum", explicitOrShorthandProperty);
			}
			return property;
		}

		// Token: 0x04003AC9 RID: 15049
		private static readonly PropertyMaker s_MinimumMaker = new InlineProgressionDimensionMaker.SP_MinimumMaker("inline-progression-dimension.minimum");

		// Token: 0x04003ACA RID: 15050
		private static readonly PropertyMaker s_OptimumMaker = new InlineProgressionDimensionMaker.SP_OptimumMaker("inline-progression-dimension.optimum");

		// Token: 0x04003ACB RID: 15051
		private static readonly PropertyMaker s_MaximumMaker = new InlineProgressionDimensionMaker.SP_MaximumMaker("inline-progression-dimension.maximum");

		// Token: 0x04003ACC RID: 15052
		private PropertyMaker m_shorthandMaker;

		// Token: 0x04003ACD RID: 15053
		private Property m_defaultProp;

		// Token: 0x02001500 RID: 5376
		private class SP_MinimumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D684 RID: 54916 RVA: 0x002F6AAD File Offset: 0x002F4CAD
			protected internal SP_MinimumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D685 RID: 54917 RVA: 0x002F6AB6 File Offset: 0x002F4CB6
			protected override bool IsAutoLengthAllowed()
			{
				return true;
			}

			// Token: 0x0600D686 RID: 54918 RVA: 0x002F6AB9 File Offset: 0x002F4CB9
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}

		// Token: 0x02001501 RID: 5377
		private class SP_OptimumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D687 RID: 54919 RVA: 0x002F6AC3 File Offset: 0x002F4CC3
			protected internal SP_OptimumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D688 RID: 54920 RVA: 0x002F6ACC File Offset: 0x002F4CCC
			protected override bool IsAutoLengthAllowed()
			{
				return true;
			}

			// Token: 0x0600D689 RID: 54921 RVA: 0x002F6ACF File Offset: 0x002F4CCF
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}

		// Token: 0x02001502 RID: 5378
		private class SP_MaximumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D68A RID: 54922 RVA: 0x002F6AD9 File Offset: 0x002F4CD9
			protected internal SP_MaximumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D68B RID: 54923 RVA: 0x002F6AE2 File Offset: 0x002F4CE2
			protected override bool IsAutoLengthAllowed()
			{
				return true;
			}

			// Token: 0x0600D68C RID: 54924 RVA: 0x002F6AE5 File Offset: 0x002F4CE5
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}
	}
}
