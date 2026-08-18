using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014AC RID: 5292
	internal class ClearMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D52E RID: 54574 RVA: 0x002F3450 File Offset: 0x002F1650
		public new static PropertyMaker Maker(string propName)
		{
			return new ClearMaker(propName);
		}

		// Token: 0x0600D52F RID: 54575 RVA: 0x002F3458 File Offset: 0x002F1658
		protected ClearMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D530 RID: 54576 RVA: 0x002F3461 File Offset: 0x002F1661
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D531 RID: 54577 RVA: 0x002F3464 File Offset: 0x002F1664
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F2 RID: 14834
		private Property m_defaultProp;
	}
}
