using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F1 RID: 5361
	internal class GlyphOrientationVerticalMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D63B RID: 54843 RVA: 0x002F63C1 File Offset: 0x002F45C1
		public new static PropertyMaker Maker(string propName)
		{
			return new GlyphOrientationVerticalMaker(propName);
		}

		// Token: 0x0600D63C RID: 54844 RVA: 0x002F63C9 File Offset: 0x002F45C9
		protected GlyphOrientationVerticalMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D63D RID: 54845 RVA: 0x002F63D2 File Offset: 0x002F45D2
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D63E RID: 54846 RVA: 0x002F63D5 File Offset: 0x002F45D5
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AB8 RID: 15032
		private Property m_defaultProp;
	}
}
