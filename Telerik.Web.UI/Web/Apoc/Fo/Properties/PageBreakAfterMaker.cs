using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200154D RID: 5453
	internal class PageBreakAfterMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D786 RID: 55174 RVA: 0x002F7EA1 File Offset: 0x002F60A1
		public new static PropertyMaker Maker(string propName)
		{
			return new PageBreakAfterMaker(propName);
		}

		// Token: 0x0600D787 RID: 55175 RVA: 0x002F7EA9 File Offset: 0x002F60A9
		protected PageBreakAfterMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D788 RID: 55176 RVA: 0x002F7EB2 File Offset: 0x002F60B2
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D789 RID: 55177 RVA: 0x002F7EB5 File Offset: 0x002F60B5
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B1A RID: 15130
		private Property m_defaultProp;
	}
}
