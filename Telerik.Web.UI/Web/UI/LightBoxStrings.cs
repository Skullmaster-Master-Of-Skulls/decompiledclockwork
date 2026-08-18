using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200043A RID: 1082
	internal class LightBoxStrings : LocalizationStrings
	{
		// Token: 0x060026CA RID: 9930 RVA: 0x0007E8F3 File Offset: 0x0007CAF3
		public LightBoxStrings(LocalizationProvider localizationProvider) : base(localizationProvider)
		{
			this._localizationProvider = localizationProvider;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0007E903 File Offset: 0x0007CB03
		public override string GetString(string key)
		{
			return this._localizationProvider.GetString(key) ?? base.GetString(key);
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x0007E91C File Offset: 0x0007CB1C
		// (set) Token: 0x060026CD RID: 9933 RVA: 0x0007E929 File Offset: 0x0007CB29
		[NotifyParentProperty(true)]
		[DefaultValue("Maximize image")]
		public string MaximizeButtonText
		{
			get
			{
				return this.GetString("MaximizeButtonText");
			}
			set
			{
				this.SetString("MaximizeButtonText", value);
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0007E937 File Offset: 0x0007CB37
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x0007E944 File Offset: 0x0007CB44
		[NotifyParentProperty(true)]
		[DefaultValue("Restore")]
		public string RestoreButtonText
		{
			get
			{
				return this.GetString("RestoreButtonText");
			}
			set
			{
				this.SetString("RestoreButtonText", value);
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0007E952 File Offset: 0x0007CB52
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x0007E95F File Offset: 0x0007CB5F
		[NotifyParentProperty(true)]
		[DefaultValue("Close")]
		public string CloseButtonText
		{
			get
			{
				return this.GetString("CloseButtonText");
			}
			set
			{
				this.SetString("CloseButtonText", value);
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x0007E96D File Offset: 0x0007CB6D
		// (set) Token: 0x060026D3 RID: 9939 RVA: 0x0007E97A File Offset: 0x0007CB7A
		[DefaultValue("Next")]
		[NotifyParentProperty(true)]
		public string NextButtonText
		{
			get
			{
				return this.GetString("NextButtonText");
			}
			set
			{
				this.SetString("NextButtonText", value);
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x0007E988 File Offset: 0x0007CB88
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x0007E995 File Offset: 0x0007CB95
		[NotifyParentProperty(true)]
		[DefaultValue("Prev")]
		public string PrevButtonText
		{
			get
			{
				return this.GetString("PrevButtonText");
			}
			set
			{
				this.SetString("PrevButtonText", value);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x0007E9A3 File Offset: 0x0007CBA3
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x0007E9B0 File Offset: 0x0007CBB0
		[NotifyParentProperty(true)]
		[DefaultValue("Image {0} of {1}")]
		public string PagerFormatString
		{
			get
			{
				return this.GetString("PagerFormatString");
			}
			set
			{
				this.SetString("PagerFormatString", value);
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x0007E9BE File Offset: 0x0007CBBE
		// (set) Token: 0x060026D9 RID: 9945 RVA: 0x0007E9CB File Offset: 0x0007CBCB
		[DefaultValue("LightBox Active Image")]
		[NotifyParentProperty(true)]
		public string ActiveImageAltText
		{
			get
			{
				return this.GetString("ActiveImageAltText");
			}
			set
			{
				this.SetString("ActiveImageAltText", value);
			}
		}

		// Token: 0x040009FA RID: 2554
		private readonly LocalizationProvider _localizationProvider;
	}
}
