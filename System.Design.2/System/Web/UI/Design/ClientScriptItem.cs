using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000010 RID: 16
	public sealed class ClientScriptItem
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000035A3 File Offset: 0x000017A3
		public ClientScriptItem(string text, string source, string language, string type, string id)
		{
			this._text = text;
			this._source = source;
			this._language = language;
			this._type = type;
			this._id = id;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000035D0 File Offset: 0x000017D0
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000035D8 File Offset: 0x000017D8
		public string Language
		{
			get
			{
				return this._language;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000035E0 File Offset: 0x000017E0
		public string Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000035E8 File Offset: 0x000017E8
		public string Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000035F0 File Offset: 0x000017F0
		public string Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040000B8 RID: 184
		private string _text;

		// Token: 0x040000B9 RID: 185
		private string _source;

		// Token: 0x040000BA RID: 186
		private string _language;

		// Token: 0x040000BB RID: 187
		private string _type;

		// Token: 0x040000BC RID: 188
		private string _id;
	}
}
