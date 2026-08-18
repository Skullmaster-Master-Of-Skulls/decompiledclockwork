using System;
using System.Collections;
using System.IO;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Extensions;
using Telerik.Web.Apoc.Fo.Pagination;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015DA RID: 5594
	internal class AreaTree
	{
		// Token: 0x0600DA17 RID: 55831 RVA: 0x002FCAF6 File Offset: 0x002FACF6
		public AreaTree(StreamRenderer streamRenderer)
		{
			this.streamRenderer = streamRenderer;
		}

		// Token: 0x0600DA18 RID: 55832 RVA: 0x002FCB05 File Offset: 0x002FAD05
		public void setFontInfo(FontInfo fontInfo)
		{
			this.fontInfo = fontInfo;
		}

		// Token: 0x0600DA19 RID: 55833 RVA: 0x002FCB0E File Offset: 0x002FAD0E
		public FontInfo getFontInfo()
		{
			return this.fontInfo;
		}

		// Token: 0x0600DA1A RID: 55834 RVA: 0x002FCB18 File Offset: 0x002FAD18
		public void addPage(Page page)
		{
			try
			{
				page.setExtensions(this.rootExtensions);
				this.rootExtensions = null;
				this.streamRenderer.QueuePage(page);
			}
			catch (IOException innerException)
			{
				throw new ApocException("", innerException);
			}
		}

		// Token: 0x0600DA1B RID: 55835 RVA: 0x002FCB64 File Offset: 0x002FAD64
		public IDReferences getIDReferences()
		{
			return this.streamRenderer.GetIDReferences();
		}

		// Token: 0x0600DA1C RID: 55836 RVA: 0x002FCB71 File Offset: 0x002FAD71
		public void addExtension(ExtensionObj obj)
		{
			if (this.rootExtensions == null)
			{
				this.rootExtensions = new ArrayList();
			}
			this.rootExtensions.Add(obj);
		}

		// Token: 0x0600DA1D RID: 55837 RVA: 0x002FCB93 File Offset: 0x002FAD93
		public ArrayList GetDocumentMarkers()
		{
			return this.streamRenderer.GetDocumentMarkers();
		}

		// Token: 0x0600DA1E RID: 55838 RVA: 0x002FCBA0 File Offset: 0x002FADA0
		public PageSequence GetCurrentPageSequence()
		{
			return this.streamRenderer.GetCurrentPageSequence();
		}

		// Token: 0x0600DA1F RID: 55839 RVA: 0x002FCBAD File Offset: 0x002FADAD
		public ArrayList GetCurrentPageSequenceMarkers()
		{
			return this.streamRenderer.GetCurrentPageSequenceMarkers();
		}

		// Token: 0x04003C84 RID: 15492
		private FontInfo fontInfo;

		// Token: 0x04003C85 RID: 15493
		private ArrayList rootExtensions;

		// Token: 0x04003C86 RID: 15494
		private StreamRenderer streamRenderer;
	}
}
