using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace System.Web.UI
{
	// Token: 0x02000282 RID: 642
	public sealed class DesignTimeParseData
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x000618D4 File Offset: 0x0005FAD4
		public DesignTimeParseData(IDesignerHost designerHost, string parseText) : this(designerHost, parseText, string.Empty)
		{
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000618E3 File Offset: 0x0005FAE3
		public DesignTimeParseData(IDesignerHost designerHost, string parseText, string filter)
		{
			if (string.IsNullOrEmpty(parseText))
			{
				throw new ArgumentNullException("parseText");
			}
			this._designerHost = designerHost;
			this._parseText = parseText;
			this._filter = filter;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x00061913 File Offset: 0x0005FB13
		// (set) Token: 0x06001E5C RID: 7772 RVA: 0x0006191B File Offset: 0x0005FB1B
		public bool ShouldApplyTheme
		{
			get
			{
				return this._shouldApplyTheme;
			}
			set
			{
				this._shouldApplyTheme = value;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00061924 File Offset: 0x0005FB24
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x0006192C File Offset: 0x0005FB2C
		public EventHandler DataBindingHandler
		{
			get
			{
				return this._dataBindingHandler;
			}
			set
			{
				this._dataBindingHandler = value;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x00061935 File Offset: 0x0005FB35
		public IDesignerHost DesignerHost
		{
			get
			{
				return this._designerHost;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0006193D File Offset: 0x0005FB3D
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x00061953 File Offset: 0x0005FB53
		public string DocumentUrl
		{
			get
			{
				if (this._documentUrl == null)
				{
					return string.Empty;
				}
				return this._documentUrl;
			}
			set
			{
				this._documentUrl = value;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0006195C File Offset: 0x0005FB5C
		public string Filter
		{
			get
			{
				if (this._filter == null)
				{
					return string.Empty;
				}
				return this._filter;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x00061972 File Offset: 0x0005FB72
		public string ParseText
		{
			get
			{
				return this._parseText;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0006197A File Offset: 0x0005FB7A
		public ICollection UserControlRegisterEntries
		{
			get
			{
				return this._userControlRegisterEntries;
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x00061984 File Offset: 0x0005FB84
		internal void SetUserControlRegisterEntries(ICollection userControlRegisterEntries, List<TagNamespaceRegisterEntry> tagRegisterEntries)
		{
			if (userControlRegisterEntries == null && tagRegisterEntries == null)
			{
				return;
			}
			List<Triplet> list = new List<Triplet>();
			if (userControlRegisterEntries != null)
			{
				foreach (object obj in userControlRegisterEntries)
				{
					UserControlRegisterEntry userControlRegisterEntry = (UserControlRegisterEntry)obj;
					list.Add(new Triplet(userControlRegisterEntry.TagPrefix, new Pair(userControlRegisterEntry.TagName, userControlRegisterEntry.UserControlSource.ToString()), null));
				}
			}
			if (tagRegisterEntries != null)
			{
				foreach (TagNamespaceRegisterEntry tagNamespaceRegisterEntry in tagRegisterEntries)
				{
					list.Add(new Triplet(tagNamespaceRegisterEntry.TagPrefix, null, new Pair(tagNamespaceRegisterEntry.Namespace, tagNamespaceRegisterEntry.AssemblyName)));
				}
			}
			this._userControlRegisterEntries = list;
		}

		// Token: 0x0400198E RID: 6542
		private IDesignerHost _designerHost;

		// Token: 0x0400198F RID: 6543
		private string _documentUrl;

		// Token: 0x04001990 RID: 6544
		private EventHandler _dataBindingHandler;

		// Token: 0x04001991 RID: 6545
		private string _parseText;

		// Token: 0x04001992 RID: 6546
		private string _filter;

		// Token: 0x04001993 RID: 6547
		private bool _shouldApplyTheme;

		// Token: 0x04001994 RID: 6548
		private ICollection _userControlRegisterEntries;
	}
}
