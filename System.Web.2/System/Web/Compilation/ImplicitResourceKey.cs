using System;

namespace System.Web.Compilation
{
	// Token: 0x02000844 RID: 2116
	public sealed class ImplicitResourceKey
	{
		// Token: 0x0600649D RID: 25757 RVA: 0x000030B5 File Offset: 0x000012B5
		public ImplicitResourceKey()
		{
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x00160952 File Offset: 0x0015EB52
		public ImplicitResourceKey(string filter, string keyPrefix, string property)
		{
			this._filter = filter;
			this._keyPrefix = keyPrefix;
			this._property = property;
		}

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x0600649F RID: 25759 RVA: 0x0016096F File Offset: 0x0015EB6F
		// (set) Token: 0x060064A0 RID: 25760 RVA: 0x00160977 File Offset: 0x0015EB77
		public string Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x060064A1 RID: 25761 RVA: 0x00160980 File Offset: 0x0015EB80
		// (set) Token: 0x060064A2 RID: 25762 RVA: 0x00160988 File Offset: 0x0015EB88
		public string KeyPrefix
		{
			get
			{
				return this._keyPrefix;
			}
			set
			{
				this._keyPrefix = value;
			}
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x060064A3 RID: 25763 RVA: 0x00160991 File Offset: 0x0015EB91
		// (set) Token: 0x060064A4 RID: 25764 RVA: 0x00160999 File Offset: 0x0015EB99
		public string Property
		{
			get
			{
				return this._property;
			}
			set
			{
				this._property = value;
			}
		}

		// Token: 0x040033EF RID: 13295
		private string _filter;

		// Token: 0x040033F0 RID: 13296
		private string _keyPrefix;

		// Token: 0x040033F1 RID: 13297
		private string _property;
	}
}
