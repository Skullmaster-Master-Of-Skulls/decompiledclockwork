using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200158A RID: 5514
	internal class SpeakNumeralMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D850 RID: 55376 RVA: 0x002F8CFE File Offset: 0x002F6EFE
		public new static PropertyMaker Maker(string propName)
		{
			return new SpeakNumeralMaker(propName);
		}

		// Token: 0x0600D851 RID: 55377 RVA: 0x002F8D06 File Offset: 0x002F6F06
		protected SpeakNumeralMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D852 RID: 55378 RVA: 0x002F8D0F File Offset: 0x002F6F0F
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D853 RID: 55379 RVA: 0x002F8D12 File Offset: 0x002F6F12
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "continuous", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B7E RID: 15230
		private Property m_defaultProp;
	}
}
