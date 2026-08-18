using System;

namespace System.Web.UI.Design
{
	// Token: 0x02000016 RID: 22
	public class ContentDefinition
	{
		// Token: 0x06000051 RID: 81 RVA: 0x0000437F File Offset: 0x0000257F
		public ContentDefinition(string id, string content, string designTimeHtml)
		{
			this._contentPlaceHolderID = id;
			this._defaultContent = content;
			this._defaultDesignTimeHTML = designTimeHtml;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000439C File Offset: 0x0000259C
		public string ContentPlaceHolderID
		{
			get
			{
				return this._contentPlaceHolderID;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000043A4 File Offset: 0x000025A4
		public string DefaultContent
		{
			get
			{
				return this._defaultContent;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000043AC File Offset: 0x000025AC
		public string DefaultDesignTimeHtml
		{
			get
			{
				return this._defaultDesignTimeHTML;
			}
		}

		// Token: 0x040000C2 RID: 194
		private string _contentPlaceHolderID;

		// Token: 0x040000C3 RID: 195
		private string _defaultContent;

		// Token: 0x040000C4 RID: 196
		private string _defaultDesignTimeHTML;
	}
}
