using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020016BA RID: 5818
	public class ImageHttpResponseWrapper
	{
		// Token: 0x170044D8 RID: 17624
		// (get) Token: 0x0600E09F RID: 57503 RVA: 0x0031F083 File Offset: 0x0031D283
		protected virtual HttpResponse HttpResponse
		{
			get
			{
				return HttpContext.Current.Response;
			}
		}

		// Token: 0x0600E0A0 RID: 57504 RVA: 0x0031F08F File Offset: 0x0031D28F
		public virtual void Clear()
		{
			this.HttpResponse.Clear();
		}

		// Token: 0x0600E0A1 RID: 57505 RVA: 0x0031F09C File Offset: 0x0031D29C
		public virtual void BinaryWrite(byte[] data)
		{
			this.HttpResponse.BinaryWrite(data);
		}

		// Token: 0x170044D9 RID: 17625
		// (get) Token: 0x0600E0A2 RID: 57506 RVA: 0x0031F0AA File Offset: 0x0031D2AA
		// (set) Token: 0x0600E0A3 RID: 57507 RVA: 0x0031F0B7 File Offset: 0x0031D2B7
		public virtual string ContentType
		{
			get
			{
				return this.HttpResponse.ContentType;
			}
			set
			{
				this.HttpResponse.ContentType = value;
			}
		}

		// Token: 0x0600E0A4 RID: 57508 RVA: 0x0031F0C5 File Offset: 0x0031D2C5
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date")]
		public virtual void SetCacheExpires(DateTime date)
		{
			this.Cache.SetExpires(date);
		}

		// Token: 0x170044DA RID: 17626
		// (get) Token: 0x0600E0A5 RID: 57509 RVA: 0x0031F0D3 File Offset: 0x0031D2D3
		public virtual HttpCachePolicy Cache
		{
			get
			{
				return this.HttpResponse.Cache;
			}
		}

		// Token: 0x0600E0A6 RID: 57510 RVA: 0x0031F0E0 File Offset: 0x0031D2E0
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "End")]
		public virtual void End()
		{
			HttpContext.Current.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x0600E0A7 RID: 57511 RVA: 0x0031F0F1 File Offset: 0x0031D2F1
		public virtual void ContentLength(long length)
		{
			this.HttpResponse.AddHeader("Content-Length", length.ToString());
		}

		// Token: 0x0600E0A8 RID: 57512 RVA: 0x0031F10C File Offset: 0x0031D30C
		public virtual void FileName(string fileName)
		{
			string arg = fileName.Replace('\n', ' ').Replace('\r', ' ');
			this.HttpResponse.AddHeader("content-disposition", string.Format("inline; filename={0}", arg));
		}
	}
}
