using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000132 RID: 306
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class CloudUploadLocalization : LocalizationStrings
	{
		// Token: 0x06000CAC RID: 3244 RVA: 0x0002D961 File Offset: 0x0002BB61
		internal CloudUploadLocalization(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0002D96A File Offset: 0x0002BB6A
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0002D977 File Offset: 0x0002BB77
		[NotifyParentProperty(true)]
		[DefaultValue("Select")]
		[Localizable(true)]
		public string SelectButtonText
		{
			get
			{
				return this.GetString("SelectButtonText");
			}
			set
			{
				this.SetString("SelectButtonText", value);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002D985 File Offset: 0x0002BB85
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x0002D992 File Offset: 0x0002BB92
		[DefaultValue("Size validation failed")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string SizeValidationFailedMessage
		{
			get
			{
				return this.GetString("SizeValidationFailedMessage");
			}
			set
			{
				this.SetString("SizeValidationFailedMessage", value);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x0002D9AD File Offset: 0x0002BBAD
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Extension validation failed")]
		public string ExtensionValidationFailedMessage
		{
			get
			{
				return this.GetString("ExtensionValidationFailedMessage");
			}
			set
			{
				this.SetString("ExtensionValidationFailedMessage", value);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002D9BB File Offset: 0x0002BBBB
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0002D9C8 File Offset: 0x0002BBC8
		[Localizable(true)]
		[DefaultValue("Error occured during file upload")]
		[NotifyParentProperty(true)]
		public string ServerErrorMessage
		{
			get
			{
				return this.GetString("ServerErrorMessage");
			}
			set
			{
				this.SetString("ServerErrorMessage", value);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002D9D6 File Offset: 0x0002BBD6
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0002D9E3 File Offset: 0x0002BBE3
		[DefaultValue("Error")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public string Error
		{
			get
			{
				return this.GetString("Error");
			}
			set
			{
				this.SetString("Error", value);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002D9F1 File Offset: 0x0002BBF1
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0002D9FE File Offset: 0x0002BBFE
		[Localizable(true)]
		[DefaultValue("Remove")]
		[NotifyParentProperty(true)]
		public string Remove
		{
			get
			{
				return this.GetString("Remove");
			}
			set
			{
				this.SetString("Remove", value);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002DA0C File Offset: 0x0002BC0C
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0002DA19 File Offset: 0x0002BC19
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		public string Cancel
		{
			get
			{
				return this.GetString("Cancel");
			}
			set
			{
				this.SetString("Cancel", value);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002DA27 File Offset: 0x0002BC27
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0002DA34 File Offset: 0x0002BC34
		[DefaultValue("Uploading Files")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string UploadingFilesMessage
		{
			get
			{
				return this.GetString("UploadingFilesMessage");
			}
			set
			{
				this.SetString("UploadingFilesMessage", value);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002DA42 File Offset: 0x0002BC42
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0002DA4F File Offset: 0x0002BC4F
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Uploaded Files")]
		public string UploadedFilesMessage
		{
			get
			{
				return this.GetString("UploadedFilesMessage");
			}
			set
			{
				this.SetString("UploadedFilesMessage", value);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002DA5D File Offset: 0x0002BC5D
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0002DA6A File Offset: 0x0002BC6A
		[Localizable(true)]
		[DefaultValue("Collapse Button")]
		[NotifyParentProperty(true)]
		public string CollapseButton
		{
			get
			{
				return this.GetString("CollapseButton");
			}
			set
			{
				this.SetString("CollapseButton", value);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002DA78 File Offset: 0x0002BC78
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x0002DA85 File Offset: 0x0002BC85
		[Localizable(true)]
		[DefaultValue("Expand Button")]
		[NotifyParentProperty(true)]
		public string ExpandButton
		{
			get
			{
				return this.GetString("ExpandButton");
			}
			set
			{
				this.SetString("ExpandButton", value);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002DA93 File Offset: 0x0002BC93
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
		[Localizable(true)]
		[DefaultValue("Drop files here")]
		[NotifyParentProperty(true)]
		public string DropZone
		{
			get
			{
				return this.GetString("DropZone");
			}
			set
			{
				this.SetString("DropZone", value);
			}
		}
	}
}
