using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001528 RID: 5416
	internal class MarginMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D709 RID: 55049 RVA: 0x002F744B File Offset: 0x002F564B
		public new static PropertyMaker Maker(string propName)
		{
			return new MarginMaker(propName);
		}

		// Token: 0x0600D70A RID: 55050 RVA: 0x002F7453 File Offset: 0x002F5653
		protected MarginMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D70B RID: 55051 RVA: 0x002F745C File Offset: 0x002F565C
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D70C RID: 55052 RVA: 0x002F745F File Offset: 0x002F565F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AF8 RID: 15096
		private Property m_defaultProp;
	}
}
