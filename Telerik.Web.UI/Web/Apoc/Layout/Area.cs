using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Layout.Inline;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015D0 RID: 5584
	internal abstract class Area : Box
	{
		// Token: 0x0600D993 RID: 55699 RVA: 0x002FBF1E File Offset: 0x002FA11E
		public Area(FontState fontState)
		{
			this.setFontState(fontState);
			this.markers = new ArrayList();
			this.returnedBy = new Hashtable();
		}

		// Token: 0x0600D994 RID: 55700 RVA: 0x002FBF50 File Offset: 0x002FA150
		public Area(FontState fontState, int allocationWidth, int maxHeight)
		{
			this.setFontState(fontState);
			this.allocationWidth = allocationWidth;
			this.contentRectangleWidth = allocationWidth;
			this.maxHeight = maxHeight;
			this.markers = new ArrayList();
			this.returnedBy = new Hashtable();
		}

		// Token: 0x0600D995 RID: 55701 RVA: 0x002FBFA0 File Offset: 0x002FA1A0
		private void setFontState(FontState fontState)
		{
			this.fontState = fontState;
		}

		// Token: 0x0600D996 RID: 55702 RVA: 0x002FBFA9 File Offset: 0x002FA1A9
		public void addChild(Box child)
		{
			this.children.Add(child);
			child.parent = this;
		}

		// Token: 0x0600D997 RID: 55703 RVA: 0x002FBFBF File Offset: 0x002FA1BF
		public void addChildAtStart(Box child)
		{
			this.children.Insert(0, child);
			child.parent = this;
		}

		// Token: 0x0600D998 RID: 55704 RVA: 0x002FBFD5 File Offset: 0x002FA1D5
		public void addDisplaySpace(int size)
		{
			this.addChild(new DisplaySpace(size));
			this.currentHeight += size;
		}

		// Token: 0x0600D999 RID: 55705 RVA: 0x002FBFF1 File Offset: 0x002FA1F1
		public void addInlineSpace(int size)
		{
			this.addChild(new InlineSpace(size));
		}

		// Token: 0x0600D99A RID: 55706 RVA: 0x002FBFFF File Offset: 0x002FA1FF
		public FontInfo getFontInfo()
		{
			return this.page.getFontInfo();
		}

		// Token: 0x0600D99B RID: 55707 RVA: 0x002FC00C File Offset: 0x002FA20C
		public virtual void end()
		{
		}

		// Token: 0x0600D99C RID: 55708 RVA: 0x002FC00E File Offset: 0x002FA20E
		public int getAllocationWidth()
		{
			return this.allocationWidth;
		}

		// Token: 0x0600D99D RID: 55709 RVA: 0x002FC016 File Offset: 0x002FA216
		public void setAllocationWidth(int w)
		{
			this.allocationWidth = w;
			this.contentRectangleWidth = this.allocationWidth;
		}

		// Token: 0x0600D99E RID: 55710 RVA: 0x002FC02B File Offset: 0x002FA22B
		public ArrayList getChildren()
		{
			return this.children;
		}

		// Token: 0x0600D99F RID: 55711 RVA: 0x002FC033 File Offset: 0x002FA233
		public bool hasChildren()
		{
			return this.children.Count != 0;
		}

		// Token: 0x0600D9A0 RID: 55712 RVA: 0x002FC048 File Offset: 0x002FA248
		public bool hasNonSpaceChildren()
		{
			if (this.children.Count > 0)
			{
				foreach (object obj in this.children)
				{
					if (!(obj is DisplaySpace))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600D9A1 RID: 55713 RVA: 0x002FC0B4 File Offset: 0x002FA2B4
		public virtual int getContentWidth()
		{
			return this.contentRectangleWidth;
		}

		// Token: 0x0600D9A2 RID: 55714 RVA: 0x002FC0BC File Offset: 0x002FA2BC
		public FontState GetFontState()
		{
			return this.fontState;
		}

		// Token: 0x0600D9A3 RID: 55715 RVA: 0x002FC0C4 File Offset: 0x002FA2C4
		public virtual int getContentHeight()
		{
			return this.currentHeight;
		}

		// Token: 0x0600D9A4 RID: 55716 RVA: 0x002FC0CC File Offset: 0x002FA2CC
		public virtual int GetHeight()
		{
			return this.currentHeight + this.getPaddingTop() + this.getPaddingBottom() + this.getBorderTopWidth() + this.getBorderBottomWidth();
		}

		// Token: 0x0600D9A5 RID: 55717 RVA: 0x002FC0F0 File Offset: 0x002FA2F0
		public int getMaxHeight()
		{
			return this.maxHeight;
		}

		// Token: 0x0600D9A6 RID: 55718 RVA: 0x002FC0F8 File Offset: 0x002FA2F8
		public Page getPage()
		{
			return this.page;
		}

		// Token: 0x0600D9A7 RID: 55719 RVA: 0x002FC100 File Offset: 0x002FA300
		public BackgroundProps getBackground()
		{
			return this.background;
		}

		// Token: 0x0600D9A8 RID: 55720 RVA: 0x002FC108 File Offset: 0x002FA308
		public int getPaddingTop()
		{
			if (this.bp != null)
			{
				return this.bp.getPaddingTop(false);
			}
			return 0;
		}

		// Token: 0x0600D9A9 RID: 55721 RVA: 0x002FC120 File Offset: 0x002FA320
		public int getPaddingLeft()
		{
			if (this.bp != null)
			{
				return this.bp.getPaddingLeft(false);
			}
			return 0;
		}

		// Token: 0x0600D9AA RID: 55722 RVA: 0x002FC138 File Offset: 0x002FA338
		public int getPaddingBottom()
		{
			if (this.bp != null)
			{
				return this.bp.getPaddingBottom(false);
			}
			return 0;
		}

		// Token: 0x0600D9AB RID: 55723 RVA: 0x002FC150 File Offset: 0x002FA350
		public int getPaddingRight()
		{
			if (this.bp != null)
			{
				return this.bp.getPaddingRight(false);
			}
			return 0;
		}

		// Token: 0x0600D9AC RID: 55724 RVA: 0x002FC168 File Offset: 0x002FA368
		public int getBorderTopWidth()
		{
			if (this.bp != null)
			{
				return this.bp.getBorderTopWidth(false);
			}
			return 0;
		}

		// Token: 0x0600D9AD RID: 55725 RVA: 0x002FC180 File Offset: 0x002FA380
		public int getBorderRightWidth()
		{
			if (this.bp != null)
			{
				return this.bp.getBorderRightWidth(false);
			}
			return 0;
		}

		// Token: 0x0600D9AE RID: 55726 RVA: 0x002FC198 File Offset: 0x002FA398
		public int getBorderLeftWidth()
		{
			if (this.bp != null)
			{
				return this.bp.getBorderLeftWidth(false);
			}
			return 0;
		}

		// Token: 0x0600D9AF RID: 55727 RVA: 0x002FC1B0 File Offset: 0x002FA3B0
		public int getBorderBottomWidth()
		{
			if (this.bp != null)
			{
				return this.bp.getBorderBottomWidth(false);
			}
			return 0;
		}

		// Token: 0x0600D9B0 RID: 55728 RVA: 0x002FC1C8 File Offset: 0x002FA3C8
		public int getTableCellXOffset()
		{
			return this.tableCellXOffset;
		}

		// Token: 0x0600D9B1 RID: 55729 RVA: 0x002FC1D0 File Offset: 0x002FA3D0
		public void setTableCellXOffset(int offset)
		{
			this.tableCellXOffset = offset;
		}

		// Token: 0x0600D9B2 RID: 55730 RVA: 0x002FC1D9 File Offset: 0x002FA3D9
		public int getAbsoluteHeight()
		{
			return this.absoluteYTop + this.getPaddingTop() + this.getBorderTopWidth() + this.currentHeight;
		}

		// Token: 0x0600D9B3 RID: 55731 RVA: 0x002FC1F6 File Offset: 0x002FA3F6
		public void setAbsoluteHeight(int value)
		{
			this.absoluteYTop = value;
		}

		// Token: 0x0600D9B4 RID: 55732 RVA: 0x002FC1FF File Offset: 0x002FA3FF
		public void increaseHeight(int amount)
		{
			this.currentHeight += amount;
		}

		// Token: 0x0600D9B5 RID: 55733 RVA: 0x002FC20F File Offset: 0x002FA40F
		public void removeChild(Area area)
		{
			this.currentHeight -= area.GetHeight();
			this.children.Remove(area);
		}

		// Token: 0x0600D9B6 RID: 55734 RVA: 0x002FC230 File Offset: 0x002FA430
		public void removeChild(DisplaySpace spacer)
		{
			this.currentHeight -= spacer.getSize();
			this.children.Remove(spacer);
		}

		// Token: 0x0600D9B7 RID: 55735 RVA: 0x002FC251 File Offset: 0x002FA451
		public void remove()
		{
			this.parent.removeChild(this);
		}

		// Token: 0x0600D9B8 RID: 55736 RVA: 0x002FC25F File Offset: 0x002FA45F
		public virtual void setPage(Page page)
		{
			this.page = page;
		}

		// Token: 0x0600D9B9 RID: 55737 RVA: 0x002FC268 File Offset: 0x002FA468
		public void setBackground(BackgroundProps bg)
		{
			this.background = bg;
		}

		// Token: 0x0600D9BA RID: 55738 RVA: 0x002FC271 File Offset: 0x002FA471
		public void setBorderAndPadding(BorderAndPadding bp)
		{
			this.bp = bp;
		}

		// Token: 0x0600D9BB RID: 55739 RVA: 0x002FC27A File Offset: 0x002FA47A
		public virtual int spaceLeft()
		{
			return this.maxHeight - this.currentHeight;
		}

		// Token: 0x0600D9BC RID: 55740 RVA: 0x002FC289 File Offset: 0x002FA489
		public virtual void start()
		{
		}

		// Token: 0x0600D9BD RID: 55741 RVA: 0x002FC28B File Offset: 0x002FA48B
		public virtual void SetHeight(int height)
		{
			if (height > this.currentHeight)
			{
				this.currentHeight = height;
			}
			if (this.currentHeight > this.getMaxHeight())
			{
				this.currentHeight = this.getMaxHeight();
			}
		}

		// Token: 0x0600D9BE RID: 55742 RVA: 0x002FC2B7 File Offset: 0x002FA4B7
		public void setMaxHeight(int height)
		{
			this.maxHeight = height;
		}

		// Token: 0x0600D9BF RID: 55743 RVA: 0x002FC2C0 File Offset: 0x002FA4C0
		public Area getParent()
		{
			return this.parent;
		}

		// Token: 0x0600D9C0 RID: 55744 RVA: 0x002FC2C8 File Offset: 0x002FA4C8
		public void setParent(Area parent)
		{
			this.parent = parent;
		}

		// Token: 0x0600D9C1 RID: 55745 RVA: 0x002FC2D1 File Offset: 0x002FA4D1
		public virtual void setIDReferences(IDReferences idReferences)
		{
			this.idReferences = idReferences;
		}

		// Token: 0x0600D9C2 RID: 55746 RVA: 0x002FC2DA File Offset: 0x002FA4DA
		public virtual IDReferences getIDReferences()
		{
			return this.idReferences;
		}

		// Token: 0x0600D9C3 RID: 55747 RVA: 0x002FC2E2 File Offset: 0x002FA4E2
		public FObj getfoCreator()
		{
			return this.foCreator;
		}

		// Token: 0x0600D9C4 RID: 55748 RVA: 0x002FC2EC File Offset: 0x002FA4EC
		public AreaContainer getNearestAncestorAreaContainer()
		{
			Area parent = this.getParent();
			AreaContainer areaContainer = parent as AreaContainer;
			while (parent != null && areaContainer == null)
			{
				parent = parent.getParent();
				areaContainer = (parent as AreaContainer);
			}
			return areaContainer;
		}

		// Token: 0x0600D9C5 RID: 55749 RVA: 0x002FC31E File Offset: 0x002FA51E
		public BorderAndPadding GetBorderAndPadding()
		{
			return this.bp;
		}

		// Token: 0x0600D9C6 RID: 55750 RVA: 0x002FC326 File Offset: 0x002FA526
		public void addMarker(Marker marker)
		{
			this.markers.Add(marker);
		}

		// Token: 0x0600D9C7 RID: 55751 RVA: 0x002FC338 File Offset: 0x002FA538
		public void addMarkers(ArrayList markers)
		{
			foreach (object value in markers)
			{
				this.markers.Add(value);
			}
		}

		// Token: 0x0600D9C8 RID: 55752 RVA: 0x002FC390 File Offset: 0x002FA590
		public void addLineagePair(FObj fo, int areaPosition)
		{
			this.returnedBy.Add(fo, areaPosition);
		}

		// Token: 0x0600D9C9 RID: 55753 RVA: 0x002FC3A4 File Offset: 0x002FA5A4
		public ArrayList getMarkers()
		{
			return this.markers;
		}

		// Token: 0x0600D9CA RID: 55754 RVA: 0x002FC3AC File Offset: 0x002FA5AC
		public void setGeneratedBy(FObj generatedBy)
		{
			this.generatedBy = generatedBy;
		}

		// Token: 0x0600D9CB RID: 55755 RVA: 0x002FC3B5 File Offset: 0x002FA5B5
		public FObj getGeneratedBy()
		{
			return this.generatedBy;
		}

		// Token: 0x0600D9CC RID: 55756 RVA: 0x002FC3BD File Offset: 0x002FA5BD
		public void isFirst(bool isFirst)
		{
			this._isFirst = isFirst;
		}

		// Token: 0x0600D9CD RID: 55757 RVA: 0x002FC3C6 File Offset: 0x002FA5C6
		public bool isFirst()
		{
			return this._isFirst;
		}

		// Token: 0x0600D9CE RID: 55758 RVA: 0x002FC3CE File Offset: 0x002FA5CE
		public void isLast(bool isLast)
		{
			this._isLast = isLast;
		}

		// Token: 0x0600D9CF RID: 55759 RVA: 0x002FC3D7 File Offset: 0x002FA5D7
		public bool isLast()
		{
			return this._isLast;
		}

		// Token: 0x04003C2E RID: 15406
		protected FontState fontState;

		// Token: 0x04003C2F RID: 15407
		protected BorderAndPadding bp;

		// Token: 0x04003C30 RID: 15408
		protected ArrayList children = new ArrayList();

		// Token: 0x04003C31 RID: 15409
		protected int maxHeight;

		// Token: 0x04003C32 RID: 15410
		protected int currentHeight;

		// Token: 0x04003C33 RID: 15411
		protected int tableCellXOffset;

		// Token: 0x04003C34 RID: 15412
		private int absoluteYTop;

		// Token: 0x04003C35 RID: 15413
		protected int contentRectangleWidth;

		// Token: 0x04003C36 RID: 15414
		protected int allocationWidth;

		// Token: 0x04003C37 RID: 15415
		protected Page page;

		// Token: 0x04003C38 RID: 15416
		protected BackgroundProps background;

		// Token: 0x04003C39 RID: 15417
		private IDReferences idReferences;

		// Token: 0x04003C3A RID: 15418
		protected ArrayList markers;

		// Token: 0x04003C3B RID: 15419
		protected FObj generatedBy;

		// Token: 0x04003C3C RID: 15420
		protected Hashtable returnedBy;

		// Token: 0x04003C3D RID: 15421
		protected string areaClass;

		// Token: 0x04003C3E RID: 15422
		protected bool _isFirst;

		// Token: 0x04003C3F RID: 15423
		protected bool _isLast;

		// Token: 0x04003C40 RID: 15424
		public FObj foCreator;
	}
}
