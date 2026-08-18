using System;
using System.Collections;
using Telerik.Web.Apoc.Apps;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Extensions;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Fo.Pagination;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc
{
	// Token: 0x020016A5 RID: 5797
	internal class StreamRenderer
	{
		// Token: 0x0600DFE4 RID: 57316 RVA: 0x0031D234 File Offset: 0x0031B434
		public StreamRenderer(IRenderer renderer)
		{
			this.renderer = renderer;
		}

		// Token: 0x0600DFE5 RID: 57317 RVA: 0x0031D285 File Offset: 0x0031B485
		public IDReferences GetIDReferences()
		{
			return this.idReferences;
		}

		// Token: 0x0600DFE6 RID: 57318 RVA: 0x0031D28D File Offset: 0x0031B48D
		public FormattingResults getResults()
		{
			return this.results;
		}

		// Token: 0x0600DFE7 RID: 57319 RVA: 0x0031D295 File Offset: 0x0031B495
		public void AddExtension(ExtensionObj ext)
		{
			this.extensions.Add(ext);
		}

		// Token: 0x0600DFE8 RID: 57320 RVA: 0x0031D2A4 File Offset: 0x0031B4A4
		public void StartRenderer()
		{
			this.pageCount = 0;
			this.renderer.SetupFontInfo(this.fontInfo);
			this.renderer.StartRenderer();
		}

		// Token: 0x0600DFE9 RID: 57321 RVA: 0x0031D2C9 File Offset: 0x0031B4C9
		public void StopRenderer()
		{
			this.ProcessQueue(true);
			this.renderer.StopRenderer();
		}

		// Token: 0x0600DFEA RID: 57322 RVA: 0x0031D2E0 File Offset: 0x0031B4E0
		public void Render(PageSequence pageSequence)
		{
			AreaTree areaTree = new AreaTree(this);
			areaTree.setFontInfo(this.fontInfo);
			foreach (object obj in this.extensions)
			{
				ExtensionObj extensionObj = (ExtensionObj)obj;
				extensionObj.Format(areaTree);
			}
			pageSequence.Format(areaTree);
			this.results.HaveFormattedPageSequence(pageSequence);
			ApocDriver.ActiveDriver.FireApocInfo("Last page-sequence produced " + pageSequence.PageCount + " page(s).");
		}

		// Token: 0x0600DFEB RID: 57323 RVA: 0x0031D384 File Offset: 0x0031B584
		public void QueuePage(Page page)
		{
			PageSequence pageSequence = page.getPageSequence();
			if (pageSequence != this.currentPageSequence)
			{
				this.currentPageSequence = pageSequence;
				this.currentPageSequenceMarkers = null;
			}
			ArrayList markers = page.getMarkers();
			if (markers != null)
			{
				if (this.documentMarkers == null)
				{
					this.documentMarkers = new ArrayList();
				}
				if (this.currentPageSequenceMarkers == null)
				{
					this.currentPageSequenceMarkers = new ArrayList();
				}
				for (int i = 0; i < markers.Count; i++)
				{
					Marker marker = (Marker)markers[i];
					marker.releaseRegistryArea();
					this.currentPageSequenceMarkers.Add(marker);
					this.documentMarkers.Add(marker);
				}
			}
			if (this.renderQueue.Count == 0 && this.idReferences.IsEveryIdValid())
			{
				this.renderer.Render(page);
			}
			else
			{
				this.AddToRenderQueue(page);
			}
			this.pageCount++;
		}

		// Token: 0x0600DFEC RID: 57324 RVA: 0x0031D45C File Offset: 0x0031B65C
		private void AddToRenderQueue(Page page)
		{
			StreamRenderer.RenderQueueEntry value = new StreamRenderer.RenderQueueEntry(this, page);
			this.renderQueue.Add(value);
			this.ProcessQueue(false);
		}

		// Token: 0x0600DFED RID: 57325 RVA: 0x0031D488 File Offset: 0x0031B688
		private void ProcessQueue(bool force)
		{
			while (this.renderQueue.Count > 0)
			{
				StreamRenderer.RenderQueueEntry renderQueueEntry = (StreamRenderer.RenderQueueEntry)this.renderQueue[0];
				if (!force && !renderQueueEntry.isResolved())
				{
					return;
				}
				this.renderer.Render(renderQueueEntry.getPage());
				this.renderQueue.RemoveAt(0);
			}
		}

		// Token: 0x0600DFEE RID: 57326 RVA: 0x0031D4E0 File Offset: 0x0031B6E0
		public ArrayList GetDocumentMarkers()
		{
			return this.documentMarkers;
		}

		// Token: 0x0600DFEF RID: 57327 RVA: 0x0031D4E8 File Offset: 0x0031B6E8
		public PageSequence GetCurrentPageSequence()
		{
			return this.currentPageSequence;
		}

		// Token: 0x0600DFF0 RID: 57328 RVA: 0x0031D4F0 File Offset: 0x0031B6F0
		public ArrayList GetCurrentPageSequenceMarkers()
		{
			return this.currentPageSequenceMarkers;
		}

		// Token: 0x040040BA RID: 16570
		private int pageCount;

		// Token: 0x040040BB RID: 16571
		private IRenderer renderer;

		// Token: 0x040040BC RID: 16572
		private FormattingResults results = new FormattingResults();

		// Token: 0x040040BD RID: 16573
		private FontInfo fontInfo = new FontInfo();

		// Token: 0x040040BE RID: 16574
		private ArrayList renderQueue = new ArrayList();

		// Token: 0x040040BF RID: 16575
		private IDReferences idReferences = new IDReferences();

		// Token: 0x040040C0 RID: 16576
		private ArrayList extensions = new ArrayList();

		// Token: 0x040040C1 RID: 16577
		private ArrayList documentMarkers;

		// Token: 0x040040C2 RID: 16578
		private ArrayList currentPageSequenceMarkers;

		// Token: 0x040040C3 RID: 16579
		private PageSequence currentPageSequence;

		// Token: 0x020016A6 RID: 5798
		private class RenderQueueEntry
		{
			// Token: 0x0600DFF1 RID: 57329 RVA: 0x0031D4F8 File Offset: 0x0031B6F8
			public RenderQueueEntry(StreamRenderer outer, Page page)
			{
				this.outer = outer;
				this.page = page;
				foreach (object value in outer.idReferences.getInvalidElements())
				{
					this.unresolvedIdReferences.Add(value);
				}
			}

			// Token: 0x0600DFF2 RID: 57330 RVA: 0x0031D578 File Offset: 0x0031B778
			public Page getPage()
			{
				return this.page;
			}

			// Token: 0x0600DFF3 RID: 57331 RVA: 0x0031D580 File Offset: 0x0031B780
			public bool isResolved()
			{
				if (this.unresolvedIdReferences.Count == 0 || this.outer.idReferences.IsEveryIdValid())
				{
					return true;
				}
				foreach (object obj in this.unresolvedIdReferences)
				{
					string id = (string)obj;
					if (!this.outer.idReferences.doesIDExist(id))
					{
						return false;
					}
				}
				this.unresolvedIdReferences.RemoveRange(0, this.unresolvedIdReferences.Count);
				return true;
			}

			// Token: 0x040040C4 RID: 16580
			private Page page;

			// Token: 0x040040C5 RID: 16581
			private StreamRenderer outer;

			// Token: 0x040040C6 RID: 16582
			private ArrayList unresolvedIdReferences = new ArrayList();
		}
	}
}
