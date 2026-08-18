using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A9 RID: 5545
	internal class TreatAsWordSpaceMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8C6 RID: 55494 RVA: 0x002F97B0 File Offset: 0x002F79B0
		public new static PropertyMaker Maker(string propName)
		{
			return new TreatAsWordSpaceMaker(propName);
		}

		// Token: 0x0600D8C7 RID: 55495 RVA: 0x002F97B8 File Offset: 0x002F79B8
		protected TreatAsWordSpaceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8C8 RID: 55496 RVA: 0x002F97C1 File Offset: 0x002F79C1
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8C9 RID: 55497 RVA: 0x002F97C4 File Offset: 0x002F79C4
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BBD RID: 15293
		private Property m_defaultProp;
	}
}
