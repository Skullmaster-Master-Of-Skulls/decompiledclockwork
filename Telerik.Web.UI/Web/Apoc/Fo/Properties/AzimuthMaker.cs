using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200144E RID: 5198
	internal class AzimuthMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3CB RID: 54219 RVA: 0x002EFBA8 File Offset: 0x002EDDA8
		public new static PropertyMaker Maker(string propName)
		{
			return new AzimuthMaker(propName);
		}

		// Token: 0x0600D3CC RID: 54220 RVA: 0x002EFBB0 File Offset: 0x002EDDB0
		protected AzimuthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3CD RID: 54221 RVA: 0x002EFBB9 File Offset: 0x002EDDB9
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D3CE RID: 54222 RVA: 0x002EFBBC File Offset: 0x002EDDBC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "center", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003983 RID: 14723
		private Property m_defaultProp;
	}
}
