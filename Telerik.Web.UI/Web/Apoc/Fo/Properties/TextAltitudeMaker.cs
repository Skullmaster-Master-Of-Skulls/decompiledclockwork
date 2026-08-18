using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A1 RID: 5537
	internal class TextAltitudeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8A6 RID: 55462 RVA: 0x002F94C8 File Offset: 0x002F76C8
		public new static PropertyMaker Maker(string propName)
		{
			return new TextAltitudeMaker(propName);
		}

		// Token: 0x0600D8A7 RID: 55463 RVA: 0x002F94D0 File Offset: 0x002F76D0
		protected TextAltitudeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8A8 RID: 55464 RVA: 0x002F94D9 File Offset: 0x002F76D9
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8A9 RID: 55465 RVA: 0x002F94DC File Offset: 0x002F76DC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "use-font-metrics", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BA4 RID: 15268
		private Property m_defaultProp;
	}
}
