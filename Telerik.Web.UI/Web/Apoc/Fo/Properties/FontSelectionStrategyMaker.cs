using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014CC RID: 5324
	internal class FontSelectionStrategyMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D5AE RID: 54702 RVA: 0x002F3DCC File Offset: 0x002F1FCC
		public new static PropertyMaker Maker(string propName)
		{
			return new FontSelectionStrategyMaker(propName);
		}

		// Token: 0x0600D5AF RID: 54703 RVA: 0x002F3DD4 File Offset: 0x002F1FD4
		protected FontSelectionStrategyMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5B0 RID: 54704 RVA: 0x002F3DDD File Offset: 0x002F1FDD
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5B1 RID: 54705 RVA: 0x002F3DE0 File Offset: 0x002F1FE0
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A71 RID: 14961
		private Property m_defaultProp;
	}
}
