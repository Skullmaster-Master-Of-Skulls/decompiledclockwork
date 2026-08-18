using System;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013C7 RID: 5063
	internal class Inline : FObjMixed
	{
		// Token: 0x0600D1B1 RID: 53681 RVA: 0x002E67C4 File Offset: 0x002E49C4
		public new static FObj.Maker GetMaker()
		{
			return new Inline.Maker();
		}

		// Token: 0x0600D1B2 RID: 53682 RVA: 0x002E67CC File Offset: 0x002E49CC
		public Inline(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:inline";
			if (parent.GetName().Equals("fo:flow"))
			{
				throw new ApocException("inline formatting objects cannot be directly under flow");
			}
			this.propMgr.GetAccessibilityProps();
			this.propMgr.GetAuralProps();
			this.propMgr.GetBorderAndPadding();
			this.propMgr.GetBackgroundProps();
			this.propMgr.GetMarginInlineProps();
			this.propMgr.GetRelativePositionProps();
			this.ts = this.propMgr.getTextDecoration(parent);
		}

		// Token: 0x0600D1B3 RID: 53683 RVA: 0x002E6864 File Offset: 0x002E4A64
		protected internal override void AddCharacters(char[] data, int start, int length)
		{
			FOText fotext = new FOText(data, start, length, this);
			fotext.setUnderlined(this.ts.getUnderlined());
			fotext.setOverlined(this.ts.getOverlined());
			fotext.setLineThrough(this.ts.getLineThrough());
			this.children.Add(fotext);
		}

		// Token: 0x020013C8 RID: 5064
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1B4 RID: 53684 RVA: 0x002E68BB File Offset: 0x002E4ABB
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new Inline(parent, propertyList);
			}
		}
	}
}
