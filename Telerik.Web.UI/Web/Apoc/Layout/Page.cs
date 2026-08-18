using System;
using System.Collections;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Fo.Pagination;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015F7 RID: 5623
	internal class Page
	{
		// Token: 0x0600DB2C RID: 56108 RVA: 0x002FFC54 File Offset: 0x002FDE54
		internal Page(AreaTree areaTree, int height, int width)
		{
			this.areaTree = areaTree;
			this.height = height;
			this.width = width;
			this.markers = new ArrayList();
		}

		// Token: 0x0600DB2D RID: 56109 RVA: 0x002FFC92 File Offset: 0x002FDE92
		public IDReferences getIDReferences()
		{
			return this.areaTree.getIDReferences();
		}

		// Token: 0x0600DB2E RID: 56110 RVA: 0x002FFC9F File Offset: 0x002FDE9F
		public void setPageSequence(PageSequence pageSequence)
		{
			this.pageSequence = pageSequence;
		}

		// Token: 0x0600DB2F RID: 56111 RVA: 0x002FFCA8 File Offset: 0x002FDEA8
		public PageSequence getPageSequence()
		{
			return this.pageSequence;
		}

		// Token: 0x0600DB30 RID: 56112 RVA: 0x002FFCB0 File Offset: 0x002FDEB0
		public AreaTree getAreaTree()
		{
			return this.areaTree;
		}

		// Token: 0x0600DB31 RID: 56113 RVA: 0x002FFCB8 File Offset: 0x002FDEB8
		public void setNumber(int number)
		{
			this.pageNumber = number;
		}

		// Token: 0x0600DB32 RID: 56114 RVA: 0x002FFCC1 File Offset: 0x002FDEC1
		public int getNumber()
		{
			return this.pageNumber;
		}

		// Token: 0x0600DB33 RID: 56115 RVA: 0x002FFCC9 File Offset: 0x002FDEC9
		public void setFormattedNumber(string number)
		{
			this.formattedPageNumber = number;
		}

		// Token: 0x0600DB34 RID: 56116 RVA: 0x002FFCD2 File Offset: 0x002FDED2
		public string getFormattedNumber()
		{
			return this.formattedPageNumber;
		}

		// Token: 0x0600DB35 RID: 56117 RVA: 0x002FFCDA File Offset: 0x002FDEDA
		internal void addAfter(AreaContainer area)
		{
			this.after = area;
			area.setPage(this);
		}

		// Token: 0x0600DB36 RID: 56118 RVA: 0x002FFCEA File Offset: 0x002FDEEA
		internal void addBefore(AreaContainer area)
		{
			this.before = area;
			area.setPage(this);
		}

		// Token: 0x0600DB37 RID: 56119 RVA: 0x002FFCFA File Offset: 0x002FDEFA
		public void addBody(BodyAreaContainer area)
		{
			this.body = area;
			area.setPage(this);
			area.getMainReferenceArea().setPage(this);
			area.getBeforeFloatReferenceArea().setPage(this);
			area.getFootnoteReferenceArea().setPage(this);
		}

		// Token: 0x0600DB38 RID: 56120 RVA: 0x002FFD2E File Offset: 0x002FDF2E
		internal void addEnd(AreaContainer area)
		{
			this.end = area;
			area.setPage(this);
		}

		// Token: 0x0600DB39 RID: 56121 RVA: 0x002FFD3E File Offset: 0x002FDF3E
		internal void addStart(AreaContainer area)
		{
			this.start = area;
			area.setPage(this);
		}

		// Token: 0x0600DB3A RID: 56122 RVA: 0x002FFD4E File Offset: 0x002FDF4E
		public void render(IRenderer renderer)
		{
			renderer.RenderPage(this);
		}

		// Token: 0x0600DB3B RID: 56123 RVA: 0x002FFD57 File Offset: 0x002FDF57
		public AreaContainer getAfter()
		{
			return this.after;
		}

		// Token: 0x0600DB3C RID: 56124 RVA: 0x002FFD5F File Offset: 0x002FDF5F
		public AreaContainer getBefore()
		{
			return this.before;
		}

		// Token: 0x0600DB3D RID: 56125 RVA: 0x002FFD67 File Offset: 0x002FDF67
		public AreaContainer getStart()
		{
			return this.start;
		}

		// Token: 0x0600DB3E RID: 56126 RVA: 0x002FFD6F File Offset: 0x002FDF6F
		public AreaContainer getEnd()
		{
			return this.end;
		}

		// Token: 0x0600DB3F RID: 56127 RVA: 0x002FFD77 File Offset: 0x002FDF77
		public BodyAreaContainer getBody()
		{
			return this.body;
		}

		// Token: 0x0600DB40 RID: 56128 RVA: 0x002FFD7F File Offset: 0x002FDF7F
		public int GetHeight()
		{
			return this.height;
		}

		// Token: 0x0600DB41 RID: 56129 RVA: 0x002FFD87 File Offset: 0x002FDF87
		public int getWidth()
		{
			return this.width;
		}

		// Token: 0x0600DB42 RID: 56130 RVA: 0x002FFD8F File Offset: 0x002FDF8F
		public FontInfo getFontInfo()
		{
			return this.areaTree.getFontInfo();
		}

		// Token: 0x0600DB43 RID: 56131 RVA: 0x002FFD9C File Offset: 0x002FDF9C
		public void addLinkSet(LinkSet linkSet)
		{
			this.linkSets.Add(linkSet);
		}

		// Token: 0x0600DB44 RID: 56132 RVA: 0x002FFDAB File Offset: 0x002FDFAB
		public ArrayList getLinkSets()
		{
			return this.linkSets;
		}

		// Token: 0x0600DB45 RID: 56133 RVA: 0x002FFDB3 File Offset: 0x002FDFB3
		public bool hasLinks()
		{
			return this.linkSets.Count != 0;
		}

		// Token: 0x0600DB46 RID: 56134 RVA: 0x002FFDC6 File Offset: 0x002FDFC6
		public void addToIDList(string id)
		{
			this.idList.Add(id);
		}

		// Token: 0x0600DB47 RID: 56135 RVA: 0x002FFDD5 File Offset: 0x002FDFD5
		public ArrayList getIDList()
		{
			return this.idList;
		}

		// Token: 0x0600DB48 RID: 56136 RVA: 0x002FFDDD File Offset: 0x002FDFDD
		public ArrayList getPendingFootnotes()
		{
			return this.footnotes;
		}

		// Token: 0x0600DB49 RID: 56137 RVA: 0x002FFDE5 File Offset: 0x002FDFE5
		public ArrayList getExtensions()
		{
			return this.rootExtensions;
		}

		// Token: 0x0600DB4A RID: 56138 RVA: 0x002FFDED File Offset: 0x002FDFED
		public void setExtensions(ArrayList extensions)
		{
			this.rootExtensions = extensions;
		}

		// Token: 0x0600DB4B RID: 56139 RVA: 0x002FFDF8 File Offset: 0x002FDFF8
		public void setPendingFootnotes(ArrayList v)
		{
			this.footnotes = v;
			if (this.footnotes != null)
			{
				foreach (object obj in this.footnotes)
				{
					FootnoteBody fb = (FootnoteBody)obj;
					Footnote.LayoutFootnote(this, fb, null);
				}
				this.footnotes = null;
			}
		}

		// Token: 0x0600DB4C RID: 56140 RVA: 0x002FFE6C File Offset: 0x002FE06C
		public void addPendingFootnote(FootnoteBody fb)
		{
			if (this.footnotes == null)
			{
				this.footnotes = new ArrayList();
			}
			this.footnotes.Add(fb);
		}

		// Token: 0x0600DB4D RID: 56141 RVA: 0x002FFE8E File Offset: 0x002FE08E
		public void unregisterMarker(Marker marker)
		{
			this.markers.Remove(marker);
		}

		// Token: 0x0600DB4E RID: 56142 RVA: 0x002FFE9C File Offset: 0x002FE09C
		public void registerMarker(Marker marker)
		{
			this.markers.Add(marker);
		}

		// Token: 0x0600DB4F RID: 56143 RVA: 0x002FFEAB File Offset: 0x002FE0AB
		public ArrayList getMarkers()
		{
			return this.markers;
		}

		// Token: 0x04003D39 RID: 15673
		private int height;

		// Token: 0x04003D3A RID: 15674
		private int width;

		// Token: 0x04003D3B RID: 15675
		private BodyAreaContainer body;

		// Token: 0x04003D3C RID: 15676
		private AreaContainer before;

		// Token: 0x04003D3D RID: 15677
		private AreaContainer after;

		// Token: 0x04003D3E RID: 15678
		private AreaContainer start;

		// Token: 0x04003D3F RID: 15679
		private AreaContainer end;

		// Token: 0x04003D40 RID: 15680
		private AreaTree areaTree;

		// Token: 0x04003D41 RID: 15681
		private ArrayList rootExtensions;

		// Token: 0x04003D42 RID: 15682
		private PageSequence pageSequence;

		// Token: 0x04003D43 RID: 15683
		protected int pageNumber;

		// Token: 0x04003D44 RID: 15684
		protected string formattedPageNumber;

		// Token: 0x04003D45 RID: 15685
		protected ArrayList linkSets = new ArrayList();

		// Token: 0x04003D46 RID: 15686
		private ArrayList idList = new ArrayList();

		// Token: 0x04003D47 RID: 15687
		private ArrayList footnotes;

		// Token: 0x04003D48 RID: 15688
		private ArrayList markers;
	}
}
