using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015B3 RID: 5555
	internal class WhiteSpaceTreatmentMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8EA RID: 55530 RVA: 0x002F9AB8 File Offset: 0x002F7CB8
		public new static PropertyMaker Maker(string propName)
		{
			return new WhiteSpaceTreatmentMaker(propName);
		}

		// Token: 0x0600D8EB RID: 55531 RVA: 0x002F9AC0 File Offset: 0x002F7CC0
		protected WhiteSpaceTreatmentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8EC RID: 55532 RVA: 0x002F9AC9 File Offset: 0x002F7CC9
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8ED RID: 55533 RVA: 0x002F9ACC File Offset: 0x002F7CCC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "preserve", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD5 RID: 15317
		private Property m_defaultProp;
	}
}
