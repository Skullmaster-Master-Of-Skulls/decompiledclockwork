using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018F6 RID: 6390
	[TelerikToolboxCategory("Data")]
	public class GridAttachmentColumnEditor : GridUploadColumnEditor
	{
		// Token: 0x0600F627 RID: 63015 RVA: 0x0037D976 File Offset: 0x0037BB76
		public GridAttachmentColumnEditor(GridAttachmentColumn owner) : base(owner)
		{
		}

		// Token: 0x17004A16 RID: 18966
		// (get) Token: 0x0600F628 RID: 63016 RVA: 0x0037D97F File Offset: 0x0037BB7F
		private GridAttachmentColumn Owner
		{
			get
			{
				return this.owner as GridAttachmentColumn;
			}
		}

		// Token: 0x17004A17 RID: 18967
		// (get) Token: 0x0600F629 RID: 63017 RVA: 0x0037D98C File Offset: 0x0037BB8C
		protected override GridUploadControlType UploadControlType
		{
			get
			{
				return this.Owner.UploadControlType;
			}
		}

		// Token: 0x0600F62A RID: 63018 RVA: 0x0037D99C File Offset: 0x0037BB9C
		protected override void CreateControls()
		{
			base.CreateControls();
			base.ControlsCreated = true;
			if (this.UploadControlType == GridUploadControlType.RadUpload)
			{
				RadUpload radUploadControl = base.RadUploadControl;
				radUploadControl.AllowedFileExtensions = this.Owner.AllowedFileExtensions;
				radUploadControl.MaxFileSize = this.Owner.MaxFileSize;
				return;
			}
			RadAsyncUpload radAsyncUploadControl = base.RadAsyncUploadControl;
			radAsyncUploadControl.AllowedFileExtensions = this.Owner.AllowedFileExtensions;
			radAsyncUploadControl.MaxFileSize = this.Owner.MaxFileSize;
		}
	}
}
