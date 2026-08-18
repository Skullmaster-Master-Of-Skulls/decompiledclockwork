using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001472 RID: 5234
	internal class BorderBeforePrecedenceMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D460 RID: 54368 RVA: 0x002F1915 File Offset: 0x002EFB15
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderBeforePrecedenceMaker(propName);
		}

		// Token: 0x0600D461 RID: 54369 RVA: 0x002F191D File Offset: 0x002EFB1D
		protected BorderBeforePrecedenceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D462 RID: 54370 RVA: 0x002F1926 File Offset: 0x002EFB26
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D463 RID: 54371 RVA: 0x002F1929 File Offset: 0x002EFB29
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039CD RID: 14797
		private Property m_defaultProp;
	}
}
