using System;

namespace System.Windows.Forms
{
	// Token: 0x02000274 RID: 628
	internal class HelpInfo
	{
		// Token: 0x06002817 RID: 10263 RVA: 0x000BA9BC File Offset: 0x000B8BBC
		public HelpInfo(string helpfilepath)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = "";
			this.navigator = HelpNavigator.TableOfContents;
			this.param = null;
			this.option = 1;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000BA9EF File Offset: 0x000B8BEF
		public HelpInfo(string helpfilepath, string keyword)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = keyword;
			this.navigator = HelpNavigator.TableOfContents;
			this.param = null;
			this.option = 2;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000BAA1E File Offset: 0x000B8C1E
		public HelpInfo(string helpfilepath, HelpNavigator navigator)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = "";
			this.navigator = navigator;
			this.param = null;
			this.option = 3;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000BAA4D File Offset: 0x000B8C4D
		public HelpInfo(string helpfilepath, HelpNavigator navigator, object param)
		{
			this.helpFilePath = helpfilepath;
			this.keyword = "";
			this.navigator = navigator;
			this.param = param;
			this.option = 4;
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x000BAA7C File Offset: 0x000B8C7C
		public int Option
		{
			get
			{
				return this.option;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x000BAA84 File Offset: 0x000B8C84
		public string HelpFilePath
		{
			get
			{
				return this.helpFilePath;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x000BAA8C File Offset: 0x000B8C8C
		public string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x000BAA94 File Offset: 0x000B8C94
		public HelpNavigator Navigator
		{
			get
			{
				return this.navigator;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x000BAA9C File Offset: 0x000B8C9C
		public object Param
		{
			get
			{
				return this.param;
			}
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000BAAA4 File Offset: 0x000B8CA4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{HelpFilePath=",
				this.helpFilePath,
				", keyword =",
				this.keyword,
				", navigator=",
				this.navigator.ToString(),
				"}"
			});
		}

		// Token: 0x04001090 RID: 4240
		private string helpFilePath;

		// Token: 0x04001091 RID: 4241
		private string keyword;

		// Token: 0x04001092 RID: 4242
		private HelpNavigator navigator;

		// Token: 0x04001093 RID: 4243
		private object param;

		// Token: 0x04001094 RID: 4244
		private int option;
	}
}
