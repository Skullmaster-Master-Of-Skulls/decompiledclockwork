using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013E3 RID: 5091
	internal class Leader : FObjMixed
	{
		// Token: 0x0600D207 RID: 53767 RVA: 0x002E8BE6 File Offset: 0x002E6DE6
		public new static FObj.Maker GetMaker()
		{
			return new Leader.Maker();
		}

		// Token: 0x0600D208 RID: 53768 RVA: 0x002E8BED File Offset: 0x002E6DED
		public Leader(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:leader";
		}

		// Token: 0x0600D209 RID: 53769 RVA: 0x002E8C04 File Offset: 0x002E6E04
		public override Status Layout(Area area)
		{
			BlockArea blockArea = area as BlockArea;
			if (blockArea == null)
			{
				ApocDriver.ActiveDriver.FireApocWarning("In this version of Apoc fo:leader must be a direct child of fo:block ");
				return new Status(1);
			}
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetMarginInlineProps();
			this.propMgr.GetRelativePositionProps();
			ColorType colorType = this.properties.GetProperty("color").GetColorType();
			float red = colorType.Red;
			float green = colorType.Green;
			float blue = colorType.Blue;
			int @enum = this.properties.GetProperty("leader-pattern").GetEnum();
			int leaderLengthOptimum = this.properties.GetProperty("leader-length.optimum").GetLength().MValue();
			int leaderLengthMinimum = this.properties.GetProperty("leader-length.minimum").GetLength().MValue();
			Length length = this.properties.GetProperty("leader-length.maximum").GetLength();
			PercentLength percentLength = length as PercentLength;
			int leaderLengthMaximum;
			if (percentLength != null)
			{
				leaderLengthMaximum = (int)(percentLength.value() * (double)area.getAllocationWidth());
			}
			else
			{
				leaderLengthMaximum = length.MValue();
			}
			int ruleThickness = this.properties.GetProperty("rule-thickness").GetLength().MValue();
			int enum2 = this.properties.GetProperty("rule-style").GetEnum();
			int leaderPatternWidth = this.properties.GetProperty("leader-pattern-width").GetLength().MValue();
			int enum3 = this.properties.GetProperty("leader-alignment").GetEnum();
			string @string = this.properties.GetProperty("id").GetString();
			blockArea.getIDReferences().InitializeID(@string, blockArea);
			int num = this.AddLeader(blockArea, this.propMgr.GetFontState(area.getFontInfo()), red, green, blue, @enum, leaderLengthMinimum, leaderLengthOptimum, leaderLengthMaximum, ruleThickness, enum2, leaderPatternWidth, enum3);
			if (num == 1)
			{
				return new Status(1);
			}
			return new Status(3);
		}

		// Token: 0x0600D20A RID: 53770 RVA: 0x002E8E00 File Offset: 0x002E7000
		public int AddLeader(BlockArea ba, FontState fontState, float red, float green, float blue, int leaderPattern, int leaderLengthMinimum, int leaderLengthOptimum, int leaderLengthMaximum, int ruleThickness, int ruleStyle, int leaderPatternWidth, int leaderAlignment)
		{
			LineArea lineArea = ba.getCurrentLineArea();
			if (lineArea == null)
			{
				return -1;
			}
			lineArea.changeFont(fontState);
			lineArea.changeColor(red, green, blue);
			if (leaderLengthOptimum <= lineArea.getRemainingWidth())
			{
				lineArea.AddLeader(leaderPattern, leaderLengthMinimum, leaderLengthOptimum, leaderLengthMaximum, ruleStyle, ruleThickness, leaderPatternWidth, leaderAlignment);
			}
			else
			{
				lineArea = ba.createNextLineArea();
				if (lineArea == null)
				{
					return -1;
				}
				lineArea.changeFont(fontState);
				lineArea.changeColor(red, green, blue);
				if (leaderLengthMinimum <= lineArea.getContentWidth())
				{
					lineArea.AddLeader(leaderPattern, leaderLengthMinimum, leaderLengthOptimum, leaderLengthMaximum, ruleStyle, ruleThickness, leaderPatternWidth, leaderAlignment);
				}
				else
				{
					ApocDriver.ActiveDriver.FireApocWarning("Leader doesn't fit into line, it will be clipped to fit.");
					lineArea.AddLeader(leaderPattern, lineArea.getRemainingWidth(), leaderLengthOptimum, leaderLengthMaximum, ruleStyle, ruleThickness, leaderPatternWidth, leaderAlignment);
				}
			}
			return 1;
		}

		// Token: 0x020013E4 RID: 5092
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D20B RID: 53771 RVA: 0x002E8EB7 File Offset: 0x002E70B7
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Leader(parent, propertyList);
			}
		}
	}
}
