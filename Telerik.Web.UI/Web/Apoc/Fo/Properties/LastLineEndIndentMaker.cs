using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001514 RID: 5396
	internal class LastLineEndIndentMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D6AD RID: 54957 RVA: 0x002F6C7B File Offset: 0x002F4E7B
		public new static PropertyMaker Maker(string propName)
		{
			return new LastLineEndIndentMaker(propName);
		}

		// Token: 0x0600D6AE RID: 54958 RVA: 0x002F6C83 File Offset: 0x002F4E83
		protected LastLineEndIndentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6AF RID: 54959 RVA: 0x002F6C8C File Offset: 0x002F4E8C
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6B0 RID: 54960 RVA: 0x002F6C8F File Offset: 0x002F4E8F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AD3 RID: 15059
		private Property m_defaultProp;
	}
}
