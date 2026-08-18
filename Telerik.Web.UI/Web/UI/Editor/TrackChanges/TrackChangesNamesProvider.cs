using System;

namespace Telerik.Web.UI.Editor.TrackChanges
{
	// Token: 0x02000B57 RID: 2903
	internal class TrackChangesNamesProvider : ITrackChangesNamesProvider
	{
		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x06006D64 RID: 28004 RVA: 0x001965A6 File Offset: 0x001947A6
		// (set) Token: 0x06006D65 RID: 28005 RVA: 0x001965AE File Offset: 0x001947AE
		public virtual string DeleteTagName
		{
			get
			{
				return this._deleteTagName;
			}
			set
			{
				this._deleteTagName = value;
			}
		}

		// Token: 0x170023E0 RID: 9184
		// (get) Token: 0x06006D66 RID: 28006 RVA: 0x001965B7 File Offset: 0x001947B7
		// (set) Token: 0x06006D67 RID: 28007 RVA: 0x001965BF File Offset: 0x001947BF
		public virtual string InsertTagName
		{
			get
			{
				return this._insertTagName;
			}
			set
			{
				this._insertTagName = value;
			}
		}

		// Token: 0x170023E1 RID: 9185
		// (get) Token: 0x06006D68 RID: 28008 RVA: 0x001965C8 File Offset: 0x001947C8
		// (set) Token: 0x06006D69 RID: 28009 RVA: 0x001965D0 File Offset: 0x001947D0
		public virtual string TitleAttribute
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		// Token: 0x170023E2 RID: 9186
		// (get) Token: 0x06006D6A RID: 28010 RVA: 0x001965D9 File Offset: 0x001947D9
		// (set) Token: 0x06006D6B RID: 28011 RVA: 0x001965E1 File Offset: 0x001947E1
		public virtual string TimestampAttribute
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				this._timestamp = value;
			}
		}

		// Token: 0x170023E3 RID: 9187
		// (get) Token: 0x06006D6C RID: 28012 RVA: 0x001965EA File Offset: 0x001947EA
		// (set) Token: 0x06006D6D RID: 28013 RVA: 0x001965F2 File Offset: 0x001947F2
		public virtual string CiteAttribute
		{
			get
			{
				return this._cite;
			}
			set
			{
				this._cite = value;
			}
		}

		// Token: 0x170023E4 RID: 9188
		// (get) Token: 0x06006D6E RID: 28014 RVA: 0x001965FB File Offset: 0x001947FB
		// (set) Token: 0x06006D6F RID: 28015 RVA: 0x00196603 File Offset: 0x00194803
		public virtual string AuthorAttribute
		{
			get
			{
				return this._author;
			}
			set
			{
				this._author = value;
			}
		}

		// Token: 0x170023E5 RID: 9189
		// (get) Token: 0x06006D70 RID: 28016 RVA: 0x0019660C File Offset: 0x0019480C
		// (set) Token: 0x06006D71 RID: 28017 RVA: 0x00196614 File Offset: 0x00194814
		public virtual string BrowserCommandAttribute
		{
			get
			{
				return this._browserCommand;
			}
			set
			{
				this._browserCommand = value;
			}
		}

		// Token: 0x170023E6 RID: 9190
		// (get) Token: 0x06006D72 RID: 28018 RVA: 0x0019661D File Offset: 0x0019481D
		// (set) Token: 0x06006D73 RID: 28019 RVA: 0x00196625 File Offset: 0x00194825
		public virtual string AlignOrigAttribute
		{
			get
			{
				return this._alignOriginal;
			}
			set
			{
				this._alignOriginal = value;
			}
		}

		// Token: 0x04001D94 RID: 7572
		private string _deleteTagName = "del[@command='Delete']";

		// Token: 0x04001D95 RID: 7573
		private string _insertTagName = "ins[@command='Insert']";

		// Token: 0x04001D96 RID: 7574
		private string _title = "title";

		// Token: 0x04001D97 RID: 7575
		private string _timestamp = "timestamp";

		// Token: 0x04001D98 RID: 7576
		private string _cite = "cite";

		// Token: 0x04001D99 RID: 7577
		private string _author = "author";

		// Token: 0x04001D9A RID: 7578
		private string _browserCommand = "command";

		// Token: 0x04001D9B RID: 7579
		private string _alignOriginal = "alignorig";
	}
}
