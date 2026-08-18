using System;

namespace System.Web.UI
{
	// Token: 0x0200038B RID: 907
	internal class UserControlRegisterEntry : RegisterDirectiveEntry
	{
		// Token: 0x06002C44 RID: 11332 RVA: 0x000C5D38 File Offset: 0x000C4D38
		internal UserControlRegisterEntry(string tagPrefix, string tagName) : base(tagPrefix)
		{
			this._tagName = tagName;
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000C5D48 File Offset: 0x000C4D48
		internal string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002C46 RID: 11334 RVA: 0x000C5D50 File Offset: 0x000C4D50
		// (set) Token: 0x06002C47 RID: 11335 RVA: 0x000C5D58 File Offset: 0x000C4D58
		internal VirtualPath UserControlSource
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002C48 RID: 11336 RVA: 0x000C5D61 File Offset: 0x000C4D61
		// (set) Token: 0x06002C49 RID: 11337 RVA: 0x000C5D69 File Offset: 0x000C4D69
		internal bool ComesFromConfig
		{
			get
			{
				return this._comesFromConfig;
			}
			set
			{
				this._comesFromConfig = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000C5D72 File Offset: 0x000C4D72
		internal string Key
		{
			get
			{
				return base.TagPrefix + ":" + this._tagName;
			}
		}

		// Token: 0x04002088 RID: 8328
		private string _tagName;

		// Token: 0x04002089 RID: 8329
		private VirtualPath _source;

		// Token: 0x0400208A RID: 8330
		private bool _comesFromConfig;
	}
}
