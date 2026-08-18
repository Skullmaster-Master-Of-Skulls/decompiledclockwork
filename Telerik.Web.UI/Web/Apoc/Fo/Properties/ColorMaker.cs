using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014AE RID: 5294
	internal class ColorMaker : GenericColor
	{
		// Token: 0x0600D536 RID: 54582 RVA: 0x002F34C8 File Offset: 0x002F16C8
		public new static PropertyMaker Maker(string propName)
		{
			return new ColorMaker(propName);
		}

		// Token: 0x0600D537 RID: 54583 RVA: 0x002F34D0 File Offset: 0x002F16D0
		protected ColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D538 RID: 54584 RVA: 0x002F34D9 File Offset: 0x002F16D9
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D539 RID: 54585 RVA: 0x002F34DC File Offset: 0x002F16DC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F4 RID: 14836
		private Property m_defaultProp;
	}
}
