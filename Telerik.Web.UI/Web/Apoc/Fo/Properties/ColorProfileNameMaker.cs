using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014AF RID: 5295
	internal class ColorProfileNameMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D53A RID: 54586 RVA: 0x002F3504 File Offset: 0x002F1704
		public new static PropertyMaker Maker(string propName)
		{
			return new ColorProfileNameMaker(propName);
		}

		// Token: 0x0600D53B RID: 54587 RVA: 0x002F350C File Offset: 0x002F170C
		protected ColorProfileNameMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D53C RID: 54588 RVA: 0x002F3515 File Offset: 0x002F1715
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D53D RID: 54589 RVA: 0x002F3518 File Offset: 0x002F1718
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F5 RID: 14837
		private Property m_defaultProp;
	}
}
