using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000268 RID: 616
	public class LinkedResource : AttachmentBase
	{
		// Token: 0x06001729 RID: 5929 RVA: 0x000768F0 File Offset: 0x00074AF0
		internal LinkedResource()
		{
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000768F8 File Offset: 0x00074AF8
		public LinkedResource(string fileName) : base(fileName)
		{
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00076901 File Offset: 0x00074B01
		public LinkedResource(string fileName, string mediaType) : base(fileName, mediaType)
		{
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0007690B File Offset: 0x00074B0B
		public LinkedResource(string fileName, ContentType contentType) : base(fileName, contentType)
		{
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00076915 File Offset: 0x00074B15
		public LinkedResource(Stream contentStream) : base(contentStream)
		{
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0007691E File Offset: 0x00074B1E
		public LinkedResource(Stream contentStream, string mediaType) : base(contentStream, mediaType)
		{
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x00076928 File Offset: 0x00074B28
		public LinkedResource(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00076932 File Offset: 0x00074B32
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x0007693A File Offset: 0x00074B3A
		public Uri ContentLink
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

		// Token: 0x06001732 RID: 5938 RVA: 0x00076944 File Offset: 0x00074B44
		public static LinkedResource CreateLinkedResourceFromString(string content)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, null, string.Empty);
			return linkedResource;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00076968 File Offset: 0x00074B68
		public static LinkedResource CreateLinkedResourceFromString(string content, Encoding contentEncoding, string mediaType)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, contentEncoding, mediaType);
			return linkedResource;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00076988 File Offset: 0x00074B88
		public static LinkedResource CreateLinkedResourceFromString(string content, ContentType contentType)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, contentType);
			return linkedResource;
		}
	}
}
