using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014AD RID: 5293
	internal class ClipMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D532 RID: 54578 RVA: 0x002F348C File Offset: 0x002F168C
		public new static PropertyMaker Maker(string propName)
		{
			return new ClipMaker(propName);
		}

		// Token: 0x0600D533 RID: 54579 RVA: 0x002F3494 File Offset: 0x002F1694
		protected ClipMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D534 RID: 54580 RVA: 0x002F349D File Offset: 0x002F169D
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D535 RID: 54581 RVA: 0x002F34A0 File Offset: 0x002F16A0
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F3 RID: 14835
		private Property m_defaultProp;
	}
}
