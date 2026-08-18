using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014AA RID: 5290
	internal class CaseTitleMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D526 RID: 54566 RVA: 0x002F33D8 File Offset: 0x002F15D8
		public new static PropertyMaker Maker(string propName)
		{
			return new CaseTitleMaker(propName);
		}

		// Token: 0x0600D527 RID: 54567 RVA: 0x002F33E0 File Offset: 0x002F15E0
		protected CaseTitleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D528 RID: 54568 RVA: 0x002F33E9 File Offset: 0x002F15E9
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D529 RID: 54569 RVA: 0x002F33EC File Offset: 0x002F15EC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F0 RID: 14832
		private Property m_defaultProp;
	}
}
