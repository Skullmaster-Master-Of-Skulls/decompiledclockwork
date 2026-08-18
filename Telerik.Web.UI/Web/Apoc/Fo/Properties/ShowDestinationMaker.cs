using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001577 RID: 5495
	internal class ShowDestinationMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D829 RID: 55337 RVA: 0x002F8AE6 File Offset: 0x002F6CE6
		public new static PropertyMaker Maker(string propName)
		{
			return new ShowDestinationMaker(propName);
		}

		// Token: 0x0600D82A RID: 55338 RVA: 0x002F8AEE File Offset: 0x002F6CEE
		protected ShowDestinationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D82B RID: 55339 RVA: 0x002F8AF7 File Offset: 0x002F6CF7
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D82C RID: 55340 RVA: 0x002F8AFA File Offset: 0x002F6CFA
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "replace", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B74 RID: 15220
		private Property m_defaultProp;
	}
}
