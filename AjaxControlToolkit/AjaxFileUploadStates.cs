using System;
using System.Collections.Generic;
using System.Web;

namespace AjaxControlToolkit
{
	// Token: 0x02000027 RID: 39
	public class AjaxFileUploadStates
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00005EF6 File Offset: 0x000040F6
		public AjaxFileUploadStates(HttpContext context, string id)
		{
			this._httpContext = context;
			this._id = id;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005F0C File Offset: 0x0000410C
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005F3C File Offset: 0x0000413C
		public decimal FileLength
		{
			get
			{
				return decimal.Parse(((string)this._httpContext.Cache[this.GetSessionName("fileLength")]) ?? "0");
			}
			set
			{
				this._httpContext.Cache[this.GetSessionName("fileLength")] = value.ToString();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005F60 File Offset: 0x00004160
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005F90 File Offset: 0x00004190
		public decimal Uploaded
		{
			get
			{
				return decimal.Parse(((string)this._httpContext.Cache[this.GetSessionName("uploaded")]) ?? "0");
			}
			set
			{
				this._httpContext.Cache[this.GetSessionName("uploaded")] = value.ToString();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005FB4 File Offset: 0x000041B4
		public decimal Percent
		{
			get
			{
				decimal fileLength = this.FileLength;
				decimal uploaded = this.Uploaded;
				if (fileLength == 0m || uploaded == 0m)
				{
					return 0m;
				}
				return uploaded / fileLength * 100m;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006005 File Offset: 0x00004205
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00006035 File Offset: 0x00004235
		public bool Abort
		{
			get
			{
				return bool.Parse(((string)this._httpContext.Cache[this.GetSessionName("abort")]) ?? "false");
			}
			set
			{
				this._httpContext.Cache[this.GetSessionName("abort")] = value.ToString();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000605C File Offset: 0x0000425C
		// (set) Token: 0x0600018B RID: 395 RVA: 0x000060C6 File Offset: 0x000042C6
		public List<string> BlockList
		{
			get
			{
				if (this._httpContext.Cache[this.GetSessionName("blockList")] == null)
				{
					this._httpContext.Cache[this.GetSessionName("blockList")] = new List<string>();
				}
				return (List<string>)this._httpContext.Cache[this.GetSessionName("blockList")];
			}
			set
			{
				this._httpContext.Cache[this.GetSessionName("blockList")] = value;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000060E4 File Offset: 0x000042E4
		private string GetSessionName(string name)
		{
			return "AjaxFileUpload_" + name + "_" + this._id;
		}

		// Token: 0x04000070 RID: 112
		private readonly HttpContext _httpContext;

		// Token: 0x04000071 RID: 113
		private readonly string _id;
	}
}
