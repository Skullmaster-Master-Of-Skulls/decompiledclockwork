using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A6 RID: 5542
	internal class TextShadowMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8B9 RID: 55481 RVA: 0x002F96F9 File Offset: 0x002F78F9
		public new static PropertyMaker Maker(string propName)
		{
			return new TextShadowMaker(propName);
		}

		// Token: 0x0600D8BA RID: 55482 RVA: 0x002F9701 File Offset: 0x002F7901
		protected TextShadowMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8BB RID: 55483 RVA: 0x002F970A File Offset: 0x002F790A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8BC RID: 55484 RVA: 0x002F970D File Offset: 0x002F790D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BBA RID: 15290
		private Property m_defaultProp;
	}
}
