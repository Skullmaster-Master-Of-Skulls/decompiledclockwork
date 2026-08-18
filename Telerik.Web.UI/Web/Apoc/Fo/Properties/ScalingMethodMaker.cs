using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001574 RID: 5492
	internal class ScalingMethodMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D81D RID: 55325 RVA: 0x002F8A32 File Offset: 0x002F6C32
		public new static PropertyMaker Maker(string propName)
		{
			return new ScalingMethodMaker(propName);
		}

		// Token: 0x0600D81E RID: 55326 RVA: 0x002F8A3A File Offset: 0x002F6C3A
		protected ScalingMethodMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D81F RID: 55327 RVA: 0x002F8A43 File Offset: 0x002F6C43
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D820 RID: 55328 RVA: 0x002F8A46 File Offset: 0x002F6C46
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B71 RID: 15217
		private Property m_defaultProp;
	}
}
