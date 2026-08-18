using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001524 RID: 5412
	internal class LineHeightShiftAdjustmentMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D6F9 RID: 55033 RVA: 0x002F735B File Offset: 0x002F555B
		public new static PropertyMaker Maker(string propName)
		{
			return new LineHeightShiftAdjustmentMaker(propName);
		}

		// Token: 0x0600D6FA RID: 55034 RVA: 0x002F7363 File Offset: 0x002F5563
		protected LineHeightShiftAdjustmentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6FB RID: 55035 RVA: 0x002F736C File Offset: 0x002F556C
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6FC RID: 55036 RVA: 0x002F736F File Offset: 0x002F556F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "consider-shifts", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AF4 RID: 15092
		private Property m_defaultProp;
	}
}
