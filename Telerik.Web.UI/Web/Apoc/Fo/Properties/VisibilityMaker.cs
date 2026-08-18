using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015AD RID: 5549
	internal class VisibilityMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8D5 RID: 55509 RVA: 0x002F9984 File Offset: 0x002F7B84
		public new static PropertyMaker Maker(string propName)
		{
			return new VisibilityMaker(propName);
		}

		// Token: 0x0600D8D6 RID: 55510 RVA: 0x002F998C File Offset: 0x002F7B8C
		protected VisibilityMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8D7 RID: 55511 RVA: 0x002F9995 File Offset: 0x002F7B95
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8D8 RID: 55512 RVA: 0x002F9998 File Offset: 0x002F7B98
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "visible", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD0 RID: 15312
		private Property m_defaultProp;
	}
}
