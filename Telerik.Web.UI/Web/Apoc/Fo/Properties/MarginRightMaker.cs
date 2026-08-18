using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001529 RID: 5417
	internal class MarginRightMaker : LengthProperty.Maker
	{
		// Token: 0x0600D70D RID: 55053 RVA: 0x002F7487 File Offset: 0x002F5687
		public new static PropertyMaker Maker(string propName)
		{
			return new MarginRightMaker(propName);
		}

		// Token: 0x0600D70E RID: 55054 RVA: 0x002F748F File Offset: 0x002F568F
		protected MarginRightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D70F RID: 55055 RVA: 0x002F7498 File Offset: 0x002F5698
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D710 RID: 55056 RVA: 0x002F749B File Offset: 0x002F569B
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AF9 RID: 15097
		private Property m_defaultProp;
	}
}
