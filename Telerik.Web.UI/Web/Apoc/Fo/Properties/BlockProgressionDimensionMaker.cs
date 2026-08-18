using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200145F RID: 5215
	internal class BlockProgressionDimensionMaker : LengthRangeProperty.Maker
	{
		// Token: 0x0600D40A RID: 54282 RVA: 0x002F0C76 File Offset: 0x002EEE76
		public new static PropertyMaker Maker(string propName)
		{
			return new BlockProgressionDimensionMaker(propName);
		}

		// Token: 0x0600D40B RID: 54283 RVA: 0x002F0C7E File Offset: 0x002EEE7E
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected BlockProgressionDimensionMaker(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("minimum");
		}

		// Token: 0x0600D40C RID: 54284 RVA: 0x002F0C98 File Offset: 0x002EEE98
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D40D RID: 54285 RVA: 0x002F0CA6 File Offset: 0x002EEEA6
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D40E RID: 54286 RVA: 0x002F0CAC File Offset: 0x002EEEAC
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("minimum"))
			{
				return BlockProgressionDimensionMaker.s_MinimumMaker;
			}
			if (subprop.Equals("optimum"))
			{
				return BlockProgressionDimensionMaker.s_OptimumMaker;
			}
			if (subprop.Equals("maximum"))
			{
				return BlockProgressionDimensionMaker.s_MaximumMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D40F RID: 54287 RVA: 0x002F0CFC File Offset: 0x002EEEFC
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			LengthRange lengthRange = baseProp.GetLengthRange();
			lengthRange.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D410 RID: 54288 RVA: 0x002F0D1C File Offset: 0x002EEF1C
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			LengthRange lengthRange = baseProp.GetLengthRange();
			return lengthRange.GetComponent(subpropName);
		}

		// Token: 0x0600D411 RID: 54289 RVA: 0x002F0D37 File Offset: 0x002EEF37
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.MakeCompound(propertyList, propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D412 RID: 54290 RVA: 0x002F0D5C File Offset: 0x002EEF5C
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

		// Token: 0x0600D413 RID: 54291 RVA: 0x002F0DE7 File Offset: 0x002EEFE7
		protected virtual string GetDefaultForMinimum()
		{
			return "auto";
		}

		// Token: 0x0600D414 RID: 54292 RVA: 0x002F0DEE File Offset: 0x002EEFEE
		protected virtual string GetDefaultForOptimum()
		{
			return "auto";
		}

		// Token: 0x0600D415 RID: 54293 RVA: 0x002F0DF5 File Offset: 0x002EEFF5
		protected virtual string GetDefaultForMaximum()
		{
			return "auto";
		}

		// Token: 0x0600D416 RID: 54294 RVA: 0x002F0DFC File Offset: 0x002EEFFC
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

		// Token: 0x0600D417 RID: 54295 RVA: 0x002F0E68 File Offset: 0x002EF068
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D418 RID: 54296 RVA: 0x002F0E6C File Offset: 0x002EF06C
		public override bool IsCorrespondingForced(PropertyList propertyList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append(propertyList.wmRelToAbs(4));
			if (propertyList.GetExplicitProperty(stringBuilder.ToString()) != null)
			{
				return true;
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("min-");
			stringBuilder.Append(propertyList.wmRelToAbs(4));
			if (propertyList.GetExplicitProperty(stringBuilder.ToString()) != null)
			{
				return true;
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("max-");
			stringBuilder.Append(propertyList.wmRelToAbs(4));
			return propertyList.GetExplicitProperty(stringBuilder.ToString()) != null;
		}

		// Token: 0x0600D419 RID: 54297 RVA: 0x002F0F08 File Offset: 0x002EF108
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(propertyList.wmRelToAbs(4));
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
			stringBuilder.Append(propertyList.wmRelToAbs(4));
			Property explicitOrShorthandProperty = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (explicitOrShorthandProperty != null)
			{
				this.SetSubprop(property, "minimum", explicitOrShorthandProperty);
			}
			stringBuilder.Length = 0;
			stringBuilder.Append("max-");
			stringBuilder.Append(propertyList.wmRelToAbs(4));
			explicitOrShorthandProperty = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (explicitOrShorthandProperty != null)
			{
				this.SetSubprop(property, "maximum", explicitOrShorthandProperty);
			}
			return property;
		}

		// Token: 0x040039A6 RID: 14758
		private static readonly PropertyMaker s_MinimumMaker = new BlockProgressionDimensionMaker.SP_MinimumMaker("block-progression-dimension.minimum");

		// Token: 0x040039A7 RID: 14759
		private static readonly PropertyMaker s_OptimumMaker = new BlockProgressionDimensionMaker.SP_OptimumMaker("block-progression-dimension.optimum");

		// Token: 0x040039A8 RID: 14760
		private static readonly PropertyMaker s_MaximumMaker = new BlockProgressionDimensionMaker.SP_MaximumMaker("block-progression-dimension.maximum");

		// Token: 0x040039A9 RID: 14761
		private PropertyMaker m_shorthandMaker;

		// Token: 0x040039AA RID: 14762
		private Property m_defaultProp;

		// Token: 0x02001460 RID: 5216
		private class SP_MinimumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D41B RID: 54299 RVA: 0x002F1005 File Offset: 0x002EF205
			protected internal SP_MinimumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D41C RID: 54300 RVA: 0x002F100E File Offset: 0x002EF20E
			protected override bool IsAutoLengthAllowed()
			{
				return true;
			}

			// Token: 0x0600D41D RID: 54301 RVA: 0x002F1011 File Offset: 0x002EF211
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}

		// Token: 0x02001461 RID: 5217
		private class SP_OptimumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D41E RID: 54302 RVA: 0x002F101B File Offset: 0x002EF21B
			protected internal SP_OptimumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D41F RID: 54303 RVA: 0x002F1024 File Offset: 0x002EF224
			protected override bool IsAutoLengthAllowed()
			{
				return true;
			}

			// Token: 0x0600D420 RID: 54304 RVA: 0x002F1027 File Offset: 0x002EF227
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}

		// Token: 0x02001462 RID: 5218
		private class SP_MaximumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D421 RID: 54305 RVA: 0x002F1031 File Offset: 0x002EF231
			protected internal SP_MaximumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D422 RID: 54306 RVA: 0x002F103A File Offset: 0x002EF23A
			protected override bool IsAutoLengthAllowed()
			{
				return true;
			}

			// Token: 0x0600D423 RID: 54307 RVA: 0x002F103D File Offset: 0x002EF23D
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}
	}
}
