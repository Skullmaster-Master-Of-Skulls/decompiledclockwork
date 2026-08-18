using System;
using System.IO;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02001346 RID: 4934
	internal sealed class PostedFile : UploadedFile
	{
		// Token: 0x17004218 RID: 16920
		// (get) Token: 0x0600CDAC RID: 52652 RVA: 0x002DC942 File Offset: 0x002DAB42
		// (set) Token: 0x0600CDAD RID: 52653 RVA: 0x002DC94F File Offset: 0x002DAB4F
		public override string FileName
		{
			get
			{
				return this._file.FileName;
			}
			internal set
			{
			}
		}

		// Token: 0x17004219 RID: 16921
		// (get) Token: 0x0600CDAE RID: 52654 RVA: 0x002DC951 File Offset: 0x002DAB51
		public override string ContentType
		{
			get
			{
				return this._file.ContentType;
			}
		}

		// Token: 0x1700421A RID: 16922
		// (get) Token: 0x0600CDAF RID: 52655 RVA: 0x002DC95E File Offset: 0x002DAB5E
		public override long ContentLength
		{
			get
			{
				return (long)this._file.ContentLength;
			}
		}

		// Token: 0x0600CDB0 RID: 52656 RVA: 0x002DC96C File Offset: 0x002DAB6C
		public override void SaveAs(string fileName, bool overwrite)
		{
			if (overwrite || (!overwrite && !File.Exists(fileName)))
			{
				this._file.SaveAs(fileName);
			}
		}

		// Token: 0x1700421B RID: 16923
		// (get) Token: 0x0600CDB1 RID: 52657 RVA: 0x002DC988 File Offset: 0x002DAB88
		public override Stream InputStream
		{
			get
			{
				return this._file.InputStream;
			}
		}

		// Token: 0x1700421C RID: 16924
		// (get) Token: 0x0600CDB2 RID: 52658 RVA: 0x002DC995 File Offset: 0x002DAB95
		protected internal override string InputFieldName
		{
			get
			{
				return this._inputFieldName;
			}
		}

		// Token: 0x0600CDB3 RID: 52659 RVA: 0x002DC99D File Offset: 0x002DAB9D
		internal PostedFile(string inputFieldName, HttpPostedFile file)
		{
			this._inputFieldName = inputFieldName;
			this._file = file;
		}

		// Token: 0x040036FC RID: 14076
		private string _inputFieldName;

		// Token: 0x040036FD RID: 14077
		private HttpPostedFile _file;
	}
}
