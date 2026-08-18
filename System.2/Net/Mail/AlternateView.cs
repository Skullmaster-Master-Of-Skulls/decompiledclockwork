using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000254 RID: 596
	public class AlternateView : AttachmentBase
	{
		// Token: 0x060016B0 RID: 5808 RVA: 0x00075689 File Offset: 0x00073889
		internal AlternateView()
		{
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00075691 File Offset: 0x00073891
		public AlternateView(string fileName) : base(fileName)
		{
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0007569A File Offset: 0x0007389A
		public AlternateView(string fileName, string mediaType) : base(fileName, mediaType)
		{
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000756A4 File Offset: 0x000738A4
		public AlternateView(string fileName, ContentType contentType) : base(fileName, contentType)
		{
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000756AE File Offset: 0x000738AE
		public AlternateView(Stream contentStream) : base(contentStream)
		{
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x000756B7 File Offset: 0x000738B7
		public AlternateView(Stream contentStream, string mediaType) : base(contentStream, mediaType)
		{
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x000756C1 File Offset: 0x000738C1
		public AlternateView(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000756CB File Offset: 0x000738CB
		public LinkedResourceCollection LinkedResources
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.linkedResources == null)
				{
					this.linkedResources = new LinkedResourceCollection();
				}
				return this.linkedResources;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000756FF File Offset: 0x000738FF
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x00075707 File Offset: 0x00073907
		public Uri BaseUri
		{
			get
			{
				return base.ContentLocation;
			}
			set
			{
				base.ContentLocation = value;
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00075710 File Offset: 0x00073910
		public static AlternateView CreateAlternateViewFromString(string content)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, null, string.Empty);
			return alternateView;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00075734 File Offset: 0x00073934
		public static AlternateView CreateAlternateViewFromString(string content, Encoding contentEncoding, string mediaType)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, contentEncoding, mediaType);
			return alternateView;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00075754 File Offset: 0x00073954
		public static AlternateView CreateAlternateViewFromString(string content, ContentType contentType)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, contentType);
			return alternateView;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00075770 File Offset: 0x00073970
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.linkedResources != null)
			{
				this.linkedResources.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04001770 RID: 6000
		private LinkedResourceCollection linkedResources;
	}
}
