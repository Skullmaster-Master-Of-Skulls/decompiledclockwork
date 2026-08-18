using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B77 RID: 7031
	internal class FieldHeaderInfo
	{
		// Token: 0x0601108C RID: 69772 RVA: 0x003C291D File Offset: 0x003C0B1D
		public FieldHeaderInfo(byte[] headerContent, Encoding encoding)
		{
			this._content = headerContent;
			this._encoding = encoding;
		}

		// Token: 0x17005325 RID: 21285
		// (get) Token: 0x0601108D RID: 69773 RVA: 0x003C2933 File Offset: 0x003C0B33
		public string ContentAsString
		{
			get
			{
				if (this._contentAsString == null)
				{
					this._contentAsString = this._encoding.GetString(this.Content);
				}
				return this._contentAsString;
			}
		}

		// Token: 0x17005326 RID: 21286
		// (get) Token: 0x0601108E RID: 69774 RVA: 0x003C295C File Offset: 0x003C0B5C
		public string FieldName
		{
			get
			{
				if (this._fieldName == null)
				{
					Regex regex = new Regex("\\bname=(\"?)([^;\\r\\n]*)\\1", RegexOptions.IgnoreCase | RegexOptions.Compiled);
					this._fieldName = regex.Match(this.ContentAsString).Groups[2].Value;
				}
				return this._fieldName;
			}
		}

		// Token: 0x17005327 RID: 21287
		// (get) Token: 0x0601108F RID: 69775 RVA: 0x003C29A6 File Offset: 0x003C0BA6
		public byte[] Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x04004C31 RID: 19505
		private Encoding _encoding;

		// Token: 0x04004C32 RID: 19506
		private string _contentAsString;

		// Token: 0x04004C33 RID: 19507
		private string _fieldName;

		// Token: 0x04004C34 RID: 19508
		private byte[] _content;
	}
}
