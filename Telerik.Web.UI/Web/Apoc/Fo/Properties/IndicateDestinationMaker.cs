using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014FD RID: 5373
	internal class IndicateDestinationMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D66B RID: 54891 RVA: 0x002F66A9 File Offset: 0x002F48A9
		public new static PropertyMaker Maker(string propName)
		{
			return new IndicateDestinationMaker(propName);
		}

		// Token: 0x0600D66C RID: 54892 RVA: 0x002F66B1 File Offset: 0x002F48B1
		protected IndicateDestinationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D66D RID: 54893 RVA: 0x002F66BA File Offset: 0x002F48BA
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D66E RID: 54894 RVA: 0x002F66BD File Offset: 0x002F48BD
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC7 RID: 15047
		private Property m_defaultProp;
	}
}
