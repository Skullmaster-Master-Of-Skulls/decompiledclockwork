using System;

namespace System.Web.UI
{
	// Token: 0x02000244 RID: 580
	internal class UserControlRegisterEntry : RegisterDirectiveEntry
	{
		// Token: 0x06001AEE RID: 6894 RVA: 0x0005485F File Offset: 0x00052A5F
		internal UserControlRegisterEntry(string tagPrefix, string tagName) : base(tagPrefix)
		{
			this._tagName = tagName;
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0005486F File Offset: 0x00052A6F
		internal string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00054877 File Offset: 0x00052A77
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x0005487F File Offset: 0x00052A7F
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

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001AF2 RID: 6898 RVA: 0x00054888 File Offset: 0x00052A88
		// (set) Token: 0x06001AF3 RID: 6899 RVA: 0x00054890 File Offset: 0x00052A90
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

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001AF4 RID: 6900 RVA: 0x00054899 File Offset: 0x00052A99
		internal string Key
		{
			get
			{
				return base.TagPrefix + ":" + this._tagName;
			}
		}

		// Token: 0x04001879 RID: 6265
		private string _tagName;

		// Token: 0x0400187A RID: 6266
		private VirtualPath _source;

		// Token: 0x0400187B RID: 6267
		private bool _comesFromConfig;
	}
}
