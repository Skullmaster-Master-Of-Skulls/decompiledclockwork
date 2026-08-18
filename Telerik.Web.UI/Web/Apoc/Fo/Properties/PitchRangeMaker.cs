using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001558 RID: 5464
	internal class PitchRangeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7B3 RID: 55219 RVA: 0x002F8198 File Offset: 0x002F6398
		public new static PropertyMaker Maker(string propName)
		{
			return new PitchRangeMaker(propName);
		}

		// Token: 0x0600D7B4 RID: 55220 RVA: 0x002F81A0 File Offset: 0x002F63A0
		protected PitchRangeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7B5 RID: 55221 RVA: 0x002F81A9 File Offset: 0x002F63A9
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D7B6 RID: 55222 RVA: 0x002F81AC File Offset: 0x002F63AC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "50", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B2C RID: 15148
		private Property m_defaultProp;
	}
}
