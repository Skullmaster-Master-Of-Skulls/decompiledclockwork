using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015AF RID: 5551
	internal class VolumeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8DD RID: 55517 RVA: 0x002F99FC File Offset: 0x002F7BFC
		public new static PropertyMaker Maker(string propName)
		{
			return new VolumeMaker(propName);
		}

		// Token: 0x0600D8DE RID: 55518 RVA: 0x002F9A04 File Offset: 0x002F7C04
		protected VolumeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8DF RID: 55519 RVA: 0x002F9A0D File Offset: 0x002F7C0D
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8E0 RID: 55520 RVA: 0x002F9A10 File Offset: 0x002F7C10
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "medium", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD2 RID: 15314
		private Property m_defaultProp;
	}
}
