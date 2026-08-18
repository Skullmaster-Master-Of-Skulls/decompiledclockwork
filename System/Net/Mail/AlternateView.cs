using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000677 RID: 1655
	public class AlternateView : AttachmentBase
	{
		// Token: 0x06003325 RID: 13093 RVA: 0x000D852C File Offset: 0x000D752C
		internal AlternateView()
		{
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000D8534 File Offset: 0x000D7534
		public AlternateView(string fileName) : base(fileName)
		{
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000D853D File Offset: 0x000D753D
		public AlternateView(string fileName, string mediaType) : base(fileName, mediaType)
		{
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000D8547 File Offset: 0x000D7547
		public AlternateView(string fileName, ContentType contentType) : base(fileName, contentType)
		{
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000D8551 File Offset: 0x000D7551
		public AlternateView(Stream contentStream) : base(contentStream)
		{
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000D855A File Offset: 0x000D755A
		public AlternateView(Stream contentStream, string mediaType) : base(contentStream, mediaType)
		{
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000D8564 File Offset: 0x000D7564
		public AlternateView(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000D856E File Offset: 0x000D756E
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

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000D85A2 File Offset: 0x000D75A2
		// (set) Token: 0x0600332E RID: 13102 RVA: 0x000D85AA File Offset: 0x000D75AA
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

		// Token: 0x0600332F RID: 13103 RVA: 0x000D85B4 File Offset: 0x000D75B4
		public static AlternateView CreateAlternateViewFromString(string content)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, null, string.Empty);
			return alternateView;
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000D85D8 File Offset: 0x000D75D8
		public static AlternateView CreateAlternateViewFromString(string content, Encoding contentEncoding, string mediaType)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, contentEncoding, mediaType);
			return alternateView;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000D85F8 File Offset: 0x000D75F8
		public static AlternateView CreateAlternateViewFromString(string content, ContentType contentType)
		{
			AlternateView alternateView = new AlternateView();
			alternateView.SetContentFromString(content, contentType);
			return alternateView;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000D8614 File Offset: 0x000D7614
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

		// Token: 0x04002F8C RID: 12172
		private LinkedResourceCollection linkedResources;
	}
}
