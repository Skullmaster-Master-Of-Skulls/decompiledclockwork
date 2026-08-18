using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001464 RID: 5220
	internal class BorderAfterPrecedenceMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D42A RID: 54314 RVA: 0x002F1135 File Offset: 0x002EF335
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderAfterPrecedenceMaker(propName);
		}

		// Token: 0x0600D42B RID: 54315 RVA: 0x002F113D File Offset: 0x002EF33D
		protected BorderAfterPrecedenceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D42C RID: 54316 RVA: 0x002F1146 File Offset: 0x002EF346
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D42D RID: 54317 RVA: 0x002F1149 File Offset: 0x002EF349
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039AC RID: 14764
		private Property m_defaultProp;
	}
}
