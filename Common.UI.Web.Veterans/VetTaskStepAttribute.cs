using System;

namespace TechnoPro.Common.UI.Web.Veterans.Controls
{
	// Token: 0x02000005 RID: 5
	public class VetTaskStepAttribute : Attribute
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00003452 File Offset: 0x00001652
		public VetTaskStepAttribute()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000345A File Offset: 0x0000165A
		public VetTaskStepAttribute(string Title, string Url, string Description)
		{
			this.title = Title;
			this.url = Url;
			this.description = Description;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003477 File Offset: 0x00001677
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000347F File Offset: 0x0000167F
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003488 File Offset: 0x00001688
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00003490 File Offset: 0x00001690
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003499 File Offset: 0x00001699
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000034A1 File Offset: 0x000016A1
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x0400002E RID: 46
		protected string title;

		// Token: 0x0400002F RID: 47
		protected string url;

		// Token: 0x04000030 RID: 48
		protected string description;
	}
}
