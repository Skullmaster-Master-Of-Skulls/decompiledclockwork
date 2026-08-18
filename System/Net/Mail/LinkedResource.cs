using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000697 RID: 1687
	public class LinkedResource : AttachmentBase
	{
		// Token: 0x0600340B RID: 13323 RVA: 0x000DB970 File Offset: 0x000DA970
		internal LinkedResource()
		{
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000DB978 File Offset: 0x000DA978
		public LinkedResource(string fileName) : base(fileName)
		{
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000DB981 File Offset: 0x000DA981
		public LinkedResource(string fileName, string mediaType) : base(fileName, mediaType)
		{
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000DB98B File Offset: 0x000DA98B
		public LinkedResource(string fileName, ContentType contentType) : base(fileName, contentType)
		{
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000DB995 File Offset: 0x000DA995
		public LinkedResource(Stream contentStream) : base(contentStream)
		{
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000DB99E File Offset: 0x000DA99E
		public LinkedResource(Stream contentStream, string mediaType) : base(contentStream, mediaType)
		{
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000DB9A8 File Offset: 0x000DA9A8
		public LinkedResource(Stream contentStream, ContentType contentType) : base(contentStream, contentType)
		{
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000DB9B2 File Offset: 0x000DA9B2
		// (set) Token: 0x06003413 RID: 13331 RVA: 0x000DB9BA File Offset: 0x000DA9BA
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

		// Token: 0x06003414 RID: 13332 RVA: 0x000DB9C4 File Offset: 0x000DA9C4
		public static LinkedResource CreateLinkedResourceFromString(string content)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, null, string.Empty);
			return linkedResource;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000DB9E8 File Offset: 0x000DA9E8
		public static LinkedResource CreateLinkedResourceFromString(string content, Encoding contentEncoding, string mediaType)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, contentEncoding, mediaType);
			return linkedResource;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000DBA08 File Offset: 0x000DAA08
		public static LinkedResource CreateLinkedResourceFromString(string content, ContentType contentType)
		{
			LinkedResource linkedResource = new LinkedResource();
			linkedResource.SetContentFromString(content, contentType);
			return linkedResource;
		}
	}
}
