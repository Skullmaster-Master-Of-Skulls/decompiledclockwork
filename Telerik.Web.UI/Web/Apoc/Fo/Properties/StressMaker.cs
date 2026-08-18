using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001591 RID: 5521
	internal class StressMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D86E RID: 55406 RVA: 0x002F9015 File Offset: 0x002F7215
		public new static PropertyMaker Maker(string propName)
		{
			return new StressMaker(propName);
		}

		// Token: 0x0600D86F RID: 55407 RVA: 0x002F901D File Offset: 0x002F721D
		protected StressMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D870 RID: 55408 RVA: 0x002F9026 File Offset: 0x002F7226
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D871 RID: 55409 RVA: 0x002F9029 File Offset: 0x002F7229
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "50", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B85 RID: 15237
		private Property m_defaultProp;
	}
}
