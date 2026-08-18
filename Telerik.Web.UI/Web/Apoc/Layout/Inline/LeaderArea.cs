using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout.Inline
{
	// Token: 0x020015EF RID: 5615
	internal class LeaderArea : InlineArea
	{
		// Token: 0x0600DAD7 RID: 56023 RVA: 0x002FDFE8 File Offset: 0x002FC1E8
		public LeaderArea(FontState fontState, float red, float green, float blue, string text, int leaderLengthOptimum, int leaderPattern, int ruleThickness, int ruleStyle) : base(fontState, leaderLengthOptimum, red, green, blue)
		{
			this.leaderPattern = leaderPattern;
			this.leaderLengthOptimum = leaderLengthOptimum;
			this.ruleStyle = ruleStyle;
			if (ruleStyle == 51)
			{
				ruleThickness = 0;
			}
			this.ruleThickness = ruleThickness;
		}

		// Token: 0x0600DAD8 RID: 56024 RVA: 0x002FE020 File Offset: 0x002FC220
		public override void render(IRenderer renderer)
		{
			renderer.RenderLeaderArea(this);
		}

		// Token: 0x0600DAD9 RID: 56025 RVA: 0x002FE029 File Offset: 0x002FC229
		public int getRuleThickness()
		{
			return this.ruleThickness;
		}

		// Token: 0x0600DADA RID: 56026 RVA: 0x002FE031 File Offset: 0x002FC231
		public int getRuleStyle()
		{
			return this.ruleStyle;
		}

		// Token: 0x0600DADB RID: 56027 RVA: 0x002FE039 File Offset: 0x002FC239
		public int getLeaderPattern()
		{
			return this.leaderPattern;
		}

		// Token: 0x0600DADC RID: 56028 RVA: 0x002FE041 File Offset: 0x002FC241
		public int getLeaderLength()
		{
			return this.contentRectangleWidth;
		}

		// Token: 0x04003CF7 RID: 15607
		private int ruleThickness;

		// Token: 0x04003CF8 RID: 15608
		private int leaderLengthOptimum;

		// Token: 0x04003CF9 RID: 15609
		private int leaderPattern;

		// Token: 0x04003CFA RID: 15610
		private int ruleStyle;
	}
}
