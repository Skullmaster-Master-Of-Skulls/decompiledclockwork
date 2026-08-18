using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200154E RID: 5454
	internal class PageBreakBeforeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D78A RID: 55178 RVA: 0x002F7EDD File Offset: 0x002F60DD
		public new static PropertyMaker Maker(string propName)
		{
			return new PageBreakBeforeMaker(propName);
		}

		// Token: 0x0600D78B RID: 55179 RVA: 0x002F7EE5 File Offset: 0x002F60E5
		protected PageBreakBeforeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D78C RID: 55180 RVA: 0x002F7EEE File Offset: 0x002F60EE
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D78D RID: 55181 RVA: 0x002F7EF1 File Offset: 0x002F60F1
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B1B RID: 15131
		private Property m_defaultProp;
	}
}
