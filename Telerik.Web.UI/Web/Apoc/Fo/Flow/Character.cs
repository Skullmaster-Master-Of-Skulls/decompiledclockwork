using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013D1 RID: 5073
	internal class Character : FObj
	{
		// Token: 0x0600D1D1 RID: 53713 RVA: 0x002E7604 File Offset: 0x002E5804
		public Character(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:character";
		}

		// Token: 0x0600D1D2 RID: 53714 RVA: 0x002E7619 File Offset: 0x002E5819
		public new static FObj.Maker GetMaker()
		{
			return new Character.Maker();
		}

		// Token: 0x0600D1D3 RID: 53715 RVA: 0x002E7620 File Offset: 0x002E5820
		public override Status Layout(Area area)
		{
			BlockArea blockArea = area as BlockArea;
			if (blockArea == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning("Currently Character can only be in a BlockArea");
				return new Status(1);
			}
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetHyphenationProps();
			this.propMgr.GetMarginInlineProps();
			this.propMgr.GetRelativePositionProps();
			ColorType colorType = this.properties.GetProperty("color").GetColorType();
			float red = colorType.Red;
			float green = colorType.Green;
			float blue = colorType.Blue;
			int @enum = this.properties.GetProperty("white-space-collapse").GetEnum();
			int enum2 = this.parent.properties.GetProperty("wrap-option").GetEnum();
			int enum3 = this.properties.GetProperty("text-decoration").GetEnum();
			bool ul = enum3 == 82;
			char character = this.properties.GetProperty("character").GetCharacter();
			string @string = this.properties.GetProperty("id").GetString();
			blockArea.getIDReferences().InitializeID(@string, blockArea);
			LineArea lineArea = blockArea.getCurrentLineArea();
			if (lineArea == null)
			{
				return new Status(2);
			}
			lineArea.changeFont(this.propMgr.GetFontState(area.getFontInfo()));
			lineArea.changeColor(red, green, blue);
			lineArea.changeWrapOption(enum2);
			lineArea.changeWhiteSpaceCollapse(@enum);
			blockArea.setupLinkSet(this.GetLinkSet());
			int num = lineArea.addCharacter(character, this.GetLinkSet(), ul);
			if (num == 1)
			{
				lineArea = blockArea.createNextLineArea();
				if (lineArea == null)
				{
					return new Status(2);
				}
				lineArea.changeFont(this.propMgr.GetFontState(area.getFontInfo()));
				lineArea.changeColor(red, green, blue);
				lineArea.changeWrapOption(enum2);
				lineArea.changeWhiteSpaceCollapse(@enum);
				blockArea.setupLinkSet(this.GetLinkSet());
				lineArea.addCharacter(character, this.GetLinkSet(), ul);
			}
			return new Status(1);
		}

		// Token: 0x04003872 RID: 14450
		public const int OK = 0;

		// Token: 0x04003873 RID: 14451
		public const int DOESNOT_FIT = 1;

		// Token: 0x020013D2 RID: 5074
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1D4 RID: 53716 RVA: 0x002E782C File Offset: 0x002E5A2C
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Character(parent, propertyList);
			}
		}
	}
}
