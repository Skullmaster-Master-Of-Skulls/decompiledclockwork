using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x0200002D RID: 45
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpPostedFileWrapper : HttpPostedFileBase
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00005111 File Offset: 0x00003311
		public HttpPostedFileWrapper(HttpPostedFile httpPostedFile)
		{
			if (httpPostedFile == null)
			{
				throw new ArgumentNullException("httpPostedFile");
			}
			this._file = httpPostedFile;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000512E File Offset: 0x0000332E
		public override int ContentLength
		{
			get
			{
				return this._file.ContentLength;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000513B File Offset: 0x0000333B
		public override string ContentType
		{
			get
			{
				return this._file.ContentType;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00005148 File Offset: 0x00003348
		public override string FileName
		{
			get
			{
				return this._file.FileName;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00005155 File Offset: 0x00003355
		public override Stream InputStream
		{
			get
			{
				return this._file.InputStream;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00005162 File Offset: 0x00003362
		public override void SaveAs(string filename)
		{
			this._file.SaveAs(filename);
		}

		// Token: 0x0400010C RID: 268
		private HttpPostedFile _file;
	}
}
