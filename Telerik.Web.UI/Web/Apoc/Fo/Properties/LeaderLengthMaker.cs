using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001517 RID: 5399
	internal class LeaderLengthMaker : LengthRangeProperty.Maker
	{
		// Token: 0x0600D6B8 RID: 54968 RVA: 0x002F6D6F File Offset: 0x002F4F6F
		public new static PropertyMaker Maker(string propName)
		{
			return new LeaderLengthMaker(propName);
		}

		// Token: 0x0600D6B9 RID: 54969 RVA: 0x002F6D77 File Offset: 0x002F4F77
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected LeaderLengthMaker(string name) : base(name)
		{
			this.m_shorthandMaker = this.GetSubpropMaker("minimum");
		}

		// Token: 0x0600D6BA RID: 54970 RVA: 0x002F6D91 File Offset: 0x002F4F91
		public override Property CheckEnumValues(string value)
		{
			return this.m_shorthandMaker.CheckEnumValues(value);
		}

		// Token: 0x0600D6BB RID: 54971 RVA: 0x002F6D9F File Offset: 0x002F4F9F
		protected override bool IsCompoundMaker()
		{
			return true;
		}

		// Token: 0x0600D6BC RID: 54972 RVA: 0x002F6DA4 File Offset: 0x002F4FA4
		protected override PropertyMaker GetSubpropMaker(string subprop)
		{
			if (subprop.Equals("minimum"))
			{
				return LeaderLengthMaker.s_MinimumMaker;
			}
			if (subprop.Equals("optimum"))
			{
				return LeaderLengthMaker.s_OptimumMaker;
			}
			if (subprop.Equals("maximum"))
			{
				return LeaderLengthMaker.s_MaximumMaker;
			}
			return base.GetSubpropMaker(subprop);
		}

		// Token: 0x0600D6BD RID: 54973 RVA: 0x002F6DF4 File Offset: 0x002F4FF4
		protected override Property SetSubprop(Property baseProp, string subpropName, Property subProp)
		{
			LengthRange lengthRange = baseProp.GetLengthRange();
			lengthRange.SetComponent(subpropName, subProp, false);
			return baseProp;
		}

		// Token: 0x0600D6BE RID: 54974 RVA: 0x002F6E14 File Offset: 0x002F5014
		public override Property GetSubpropValue(Property baseProp, string subpropName)
		{
			LengthRange lengthRange = baseProp.GetLengthRange();
			return lengthRange.GetComponent(subpropName);
		}

		// Token: 0x0600D6BF RID: 54975 RVA: 0x002F6E2F File Offset: 0x002F502F
		public override Property Make(PropertyList propertyList)
		{
			return this.MakeCompound(propertyList, propertyList.getParentFObj());
		}

		// Token: 0x0600D6C0 RID: 54976 RVA: 0x002F6E40 File Offset: 0x002F5040
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

		// Token: 0x0600D6C1 RID: 54977 RVA: 0x002F6ECB File Offset: 0x002F50CB
		protected virtual string GetDefaultForMinimum()
		{
			return "0pt";
		}

		// Token: 0x0600D6C2 RID: 54978 RVA: 0x002F6ED2 File Offset: 0x002F50D2
		protected virtual string GetDefaultForOptimum()
		{
			return "12.0pt";
		}

		// Token: 0x0600D6C3 RID: 54979 RVA: 0x002F6ED9 File Offset: 0x002F50D9
		protected virtual string GetDefaultForMaximum()
		{
			return "100%";
		}

		// Token: 0x0600D6C4 RID: 54980 RVA: 0x002F6EE0 File Offset: 0x002F50E0
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

		// Token: 0x0600D6C5 RID: 54981 RVA: 0x002F6F4C File Offset: 0x002F514C
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6C6 RID: 54982 RVA: 0x002F6F4F File Offset: 0x002F514F
		public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
		{
			return new LengthBase(fo, propertyList, 3);
		}

		// Token: 0x04003ADB RID: 15067
		private static readonly PropertyMaker s_MinimumMaker = new LeaderLengthMaker.SP_MinimumMaker("leader-length.minimum");

		// Token: 0x04003ADC RID: 15068
		private static readonly PropertyMaker s_OptimumMaker = new LeaderLengthMaker.SP_OptimumMaker("leader-length.optimum");

		// Token: 0x04003ADD RID: 15069
		private static readonly PropertyMaker s_MaximumMaker = new LeaderLengthMaker.SP_MaximumMaker("leader-length.maximum");

		// Token: 0x04003ADE RID: 15070
		private PropertyMaker m_shorthandMaker;

		// Token: 0x02001518 RID: 5400
		private class SP_MinimumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D6C8 RID: 54984 RVA: 0x002F6F88 File Offset: 0x002F5188
			protected internal SP_MinimumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D6C9 RID: 54985 RVA: 0x002F6F91 File Offset: 0x002F5191
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}

		// Token: 0x02001519 RID: 5401
		private class SP_OptimumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D6CA RID: 54986 RVA: 0x002F6F9B File Offset: 0x002F519B
			protected internal SP_OptimumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D6CB RID: 54987 RVA: 0x002F6FA4 File Offset: 0x002F51A4
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}

		// Token: 0x0200151A RID: 5402
		private class SP_MaximumMaker : LengthProperty.Maker
		{
			// Token: 0x0600D6CC RID: 54988 RVA: 0x002F6FAE File Offset: 0x002F51AE
			protected internal SP_MaximumMaker(string sPropName) : base(sPropName)
			{
			}

			// Token: 0x0600D6CD RID: 54989 RVA: 0x002F6FB7 File Offset: 0x002F51B7
			public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
			{
				return new LengthBase(fo, propertyList, 3);
			}
		}
	}
}
