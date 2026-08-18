using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F0 RID: 5360
	internal class GlyphOrientationHorizontalMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D637 RID: 54839 RVA: 0x002F6385 File Offset: 0x002F4585
		public new static PropertyMaker Maker(string propName)
		{
			return new GlyphOrientationHorizontalMaker(propName);
		}

		// Token: 0x0600D638 RID: 54840 RVA: 0x002F638D File Offset: 0x002F458D
		protected GlyphOrientationHorizontalMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D639 RID: 54841 RVA: 0x002F6396 File Offset: 0x002F4596
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D63A RID: 54842 RVA: 0x002F6399 File Offset: 0x002F4599
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0deg", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AB7 RID: 15031
		private Property m_defaultProp;
	}
}
