using System;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo.Flow
{
	// Token: 0x020013CF RID: 5071
	internal class BlockContainer : FObj
	{
		// Token: 0x0600D1C9 RID: 53705 RVA: 0x002E72C3 File Offset: 0x002E54C3
		public new static FObj.Maker GetMaker()
		{
			return new BlockContainer.Maker();
		}

		// Token: 0x0600D1CA RID: 53706 RVA: 0x002E72CA File Offset: 0x002E54CA
		protected BlockContainer(FObj parent, PropertyList propertyList) : base(parent, propertyList)
		{
			this.name = "fo:block-container";
			this.span = this.properties.GetProperty("span").GetEnum();
		}

		// Token: 0x0600D1CB RID: 53707 RVA: 0x002E72FC File Offset: 0x002E54FC
		public override Status Layout(Area area)
		{
			if (this.marker == -1000)
			{
				this.propMgr.GetAbsolutePositionProps();
				this.propMgr.GetBorderAndPadding();
				this.propMgr.GetBackgroundProps();
				this.propMgr.GetMarginProps();
				this.marker = 0;
				this.position = this.properties.GetProperty("position").GetEnum();
				this.top = this.properties.GetProperty("top").GetLength().MValue();
				this.bottom = this.properties.GetProperty("bottom").GetLength().MValue();
				this.left = this.properties.GetProperty("left").GetLength().MValue();
				this.right = this.properties.GetProperty("right").GetLength().MValue();
				this.width = this.properties.GetProperty("width").GetLength().MValue();
				this.height = this.properties.GetProperty("height").GetLength().MValue();
				this.span = this.properties.GetProperty("span").GetEnum();
				string @string = this.properties.GetProperty("id").GetString();
				area.getIDReferences().InitializeID(@string, area);
			}
			AreaContainer areaContainer = (AreaContainer)area;
			if (this.width == 0 && this.height == 0)
			{
				this.width = this.right - this.left;
				this.height = this.bottom - this.top;
			}
			this.areaContainer = new AreaContainer(this.propMgr.GetFontState(areaContainer.getFontInfo()), areaContainer.getXPosition() + this.left, areaContainer.GetYPosition() - this.top, this.width, this.height, this.position);
			this.areaContainer.setPage(area.getPage());
			this.areaContainer.setBackground(this.propMgr.GetBackgroundProps());
			this.areaContainer.setBorderAndPadding(this.propMgr.GetBorderAndPadding());
			this.areaContainer.start();
			this.areaContainer.setAbsoluteHeight(0);
			this.areaContainer.setIDReferences(area.getIDReferences());
			int count = this.children.Count;
			for (int i = this.marker; i < count; i++)
			{
				FObj fobj = (FObj)this.children[i];
				fobj.Layout(this.areaContainer).isIncomplete();
			}
			this.areaContainer.end();
			if (this.position == 1)
			{
				this.areaContainer.SetHeight(this.height);
			}
			area.addChild(this.areaContainer);
			return new Status(1);
		}

		// Token: 0x0600D1CC RID: 53708 RVA: 0x002E75D1 File Offset: 0x002E57D1
		public override int GetContentWidth()
		{
			if (this.areaContainer != null)
			{
				return this.areaContainer.getContentWidth();
			}
			return 0;
		}

		// Token: 0x0600D1CD RID: 53709 RVA: 0x002E75E8 File Offset: 0x002E57E8
		public override bool GeneratesReferenceAreas()
		{
			return true;
		}

		// Token: 0x0600D1CE RID: 53710 RVA: 0x002E75EB File Offset: 0x002E57EB
		public int GetSpan()
		{
			return this.span;
		}

		// Token: 0x04003869 RID: 14441
		private int position;

		// Token: 0x0400386A RID: 14442
		private int top;

		// Token: 0x0400386B RID: 14443
		private int bottom;

		// Token: 0x0400386C RID: 14444
		private int left;

		// Token: 0x0400386D RID: 14445
		private int right;

		// Token: 0x0400386E RID: 14446
		private int width;

		// Token: 0x0400386F RID: 14447
		private int height;

		// Token: 0x04003870 RID: 14448
		private int span;

		// Token: 0x04003871 RID: 14449
		private AreaContainer areaContainer;

		// Token: 0x020013D0 RID: 5072
		internal new class Maker : FObj.Maker
		{
			// Token: 0x0600D1CF RID: 53711 RVA: 0x002E75F3 File Offset: 0x002E57F3
			public override FObj Make(FObj parent, PropertyList propertyList)
			{
				return new BlockContainer(parent, propertyList);
			}
		}
	}
}
