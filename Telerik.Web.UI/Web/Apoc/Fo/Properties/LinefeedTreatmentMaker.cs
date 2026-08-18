using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001522 RID: 5410
	internal class LinefeedTreatmentMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D6EC RID: 55020 RVA: 0x002F7255 File Offset: 0x002F5455
		public new static PropertyMaker Maker(string propName)
		{
			return new LinefeedTreatmentMaker(propName);
		}

		// Token: 0x0600D6ED RID: 55021 RVA: 0x002F725D File Offset: 0x002F545D
		protected LinefeedTreatmentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6EE RID: 55022 RVA: 0x002F7266 File Offset: 0x002F5466
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D6EF RID: 55023 RVA: 0x002F7269 File Offset: 0x002F5469
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "treat-as-space", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AF2 RID: 15090
		private Property m_defaultProp;
	}
}
