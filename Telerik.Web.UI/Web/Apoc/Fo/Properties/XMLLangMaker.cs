using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015BB RID: 5563
	internal class XMLLangMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D90A RID: 55562 RVA: 0x002F9CFB File Offset: 0x002F7EFB
		public new static PropertyMaker Maker(string propName)
		{
			return new XMLLangMaker(propName);
		}

		// Token: 0x0600D90B RID: 55563 RVA: 0x002F9D03 File Offset: 0x002F7F03
		protected XMLLangMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D90C RID: 55564 RVA: 0x002F9D0C File Offset: 0x002F7F0C
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D90D RID: 55565 RVA: 0x002F9D0F File Offset: 0x002F7F0F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BE5 RID: 15333
		private Property m_defaultProp;
	}
}
