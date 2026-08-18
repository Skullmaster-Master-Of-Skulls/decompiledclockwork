using System;
using System.Text;

namespace System.Web
{
	// Token: 0x020000DB RID: 219
	internal sealed class MultipartContentElement
	{
		// Token: 0x06000E1B RID: 3611 RVA: 0x00027F1B File Offset: 0x0002611B
		internal MultipartContentElement(string name, string filename, string contentType, HttpRawUploadedContent data, int offset, int length)
		{
			this._name = name;
			this._filename = filename;
			this._contentType = contentType;
			this._data = data;
			this._offset = offset;
			this._length = length;
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00027F50 File Offset: 0x00026150
		internal bool IsFile
		{
			get
			{
				return this._filename != null;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00027F5B File Offset: 0x0002615B
		internal bool IsFormItem
		{
			get
			{
				return this._filename == null;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00027F66 File Offset: 0x00026166
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00027F6E File Offset: 0x0002616E
		internal HttpPostedFile GetAsPostedFile()
		{
			return new HttpPostedFile(this._filename, this._contentType, new HttpInputStream(this._data, this._offset, this._length));
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00027F98 File Offset: 0x00026198
		internal string GetAsString(Encoding encoding)
		{
			if (this._length > 0)
			{
				return encoding.GetString(this._data.GetAsByteArray(this._offset, this._length));
			}
			return string.Empty;
		}

		// Token: 0x04000536 RID: 1334
		private string _name;

		// Token: 0x04000537 RID: 1335
		private string _filename;

		// Token: 0x04000538 RID: 1336
		private string _contentType;

		// Token: 0x04000539 RID: 1337
		private HttpRawUploadedContent _data;

		// Token: 0x0400053A RID: 1338
		private int _offset;

		// Token: 0x0400053B RID: 1339
		private int _length;
	}
}
