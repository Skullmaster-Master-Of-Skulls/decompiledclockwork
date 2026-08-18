using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015BC RID: 5564
	internal class ZIndexMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D90E RID: 55566 RVA: 0x002F9D37 File Offset: 0x002F7F37
		public new static PropertyMaker Maker(string propName)
		{
			return new ZIndexMaker(propName);
		}

		// Token: 0x0600D90F RID: 55567 RVA: 0x002F9D3F File Offset: 0x002F7F3F
		protected ZIndexMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D910 RID: 55568 RVA: 0x002F9D48 File Offset: 0x002F7F48
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D911 RID: 55569 RVA: 0x002F9D4B File Offset: 0x002F7F4B
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BE6 RID: 15334
		private Property m_defaultProp;
	}
}
