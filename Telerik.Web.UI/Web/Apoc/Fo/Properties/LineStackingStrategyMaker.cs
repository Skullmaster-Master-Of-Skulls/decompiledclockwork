using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001525 RID: 5413
	internal class LineStackingStrategyMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D6FD RID: 55037 RVA: 0x002F7397 File Offset: 0x002F5597
		public new static PropertyMaker Maker(string propName)
		{
			return new LineStackingStrategyMaker(propName);
		}

		// Token: 0x0600D6FE RID: 55038 RVA: 0x002F739F File Offset: 0x002F559F
		protected LineStackingStrategyMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6FF RID: 55039 RVA: 0x002F73A8 File Offset: 0x002F55A8
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D700 RID: 55040 RVA: 0x002F73AB File Offset: 0x002F55AB
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "line-height", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AF5 RID: 15093
		private Property m_defaultProp;
	}
}
