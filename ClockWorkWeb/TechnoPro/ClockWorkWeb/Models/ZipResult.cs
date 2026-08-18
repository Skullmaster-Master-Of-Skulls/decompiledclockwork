using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TechnoPro.Common.Compression;
using TechnoPro.Common.Compression.Entity;

namespace TechnoPro.ClockWorkWeb.Models
{
	// Token: 0x0200010D RID: 269
	public class ZipResult : ActionResult
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0003A530 File Offset: 0x00038730
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0003A551 File Offset: 0x00038751
		public string FileName
		{
			get
			{
				return this._fileName ?? "file.zip";
			}
			set
			{
				this._fileName = value;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0003A55B File Offset: 0x0003875B
		public ZipResult(params CompressionBinaryFile[] files)
		{
			this._files = files;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0003A55B File Offset: 0x0003875B
		public ZipResult(IList<CompressionBinaryFile> files)
		{
			this._files = files;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0003A56C File Offset: 0x0003876C
		public override void ExecuteResult(ControllerContext context)
		{
			CompressionBinaryFile compressionBinaryFile = CompressDataAdapter.CompressFiles(this._files, this.FileName);
			context.HttpContext.Response.ContentType = "application/zip";
			context.HttpContext.Response.AppendHeader("content-disposition", "attachment; filename=" + this.FileName);
			context.HttpContext.Response.OutputStream.Write(compressionBinaryFile.FileBytes, 0, compressionBinaryFile.FileBytes.Length);
		}

		// Token: 0x0400061C RID: 1564
		private IList<CompressionBinaryFile> _files;

		// Token: 0x0400061D RID: 1565
		private string _fileName;
	}
}
