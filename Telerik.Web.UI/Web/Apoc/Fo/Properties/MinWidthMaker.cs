using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001533 RID: 5427
	internal class MinWidthMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D739 RID: 55097 RVA: 0x002F7733 File Offset: 0x002F5933
		public new static PropertyMaker Maker(string propName)
		{
			return new MinWidthMaker(propName);
		}

		// Token: 0x0600D73A RID: 55098 RVA: 0x002F773B File Offset: 0x002F593B
		protected MinWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D73B RID: 55099 RVA: 0x002F7744 File Offset: 0x002F5944
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D73C RID: 55100 RVA: 0x002F7747 File Offset: 0x002F5947
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B05 RID: 15109
		private Property m_defaultProp;
	}
}
