using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015C2 RID: 5570
	internal class Title : ToBeImplementedElement
	{
		// Token: 0x0600D949 RID: 55625 RVA: 0x002FB315 File Offset: 0x002F9515
		public new static FObj.Maker GetMaker()
		{
			return new Title.Maker();
		}

		// Token: 0x0600D94A RID: 55626 RVA: 0x002FB31C File Offset: 0x002F951C
		protected Title(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:title";
		}

		// Token: 0x0600D94B RID: 55627 RVA: 0x002FB334 File Offset: 0x002F9534
		public override Status Layout(Area area)
		{
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetFontState(area.getFontInfo());
			this.propMgr.GetMarginInlineProps();
			Property property = this.properties.GetProperty("baseline-shift");
			if (property is LengthProperty)
			{
				property.GetLength();
			}
			else if (property is EnumProperty)
			{
				property.GetEnum();
			}
			this.properties.GetProperty("color").GetColorType();
			this.properties.GetProperty("line-height").GetLength();
			this.properties.GetProperty("line-height-shift-adjustment").GetEnum();
			this.properties.GetProperty("visibility").GetEnum();
			this.properties.GetProperty("z-index").GetLength();
			return base.Layout(area);
		}

		// Token: 0x020015C3 RID: 5571
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D94C RID: 55628 RVA: 0x002FB435 File Offset: 0x002F9635
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Title(parent, propertyList);
			}
		}
	}
}
