using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018F7 RID: 6391
	[TelerikToolboxCategory("Data")]
	public class GridBinaryImageColumnEditor : GridUploadColumnEditor
	{
		// Token: 0x0600F62B RID: 63019 RVA: 0x0037DA11 File Offset: 0x0037BC11
		public GridBinaryImageColumnEditor(GridBinaryImageColumn owner) : base(owner)
		{
		}

		// Token: 0x17004A18 RID: 18968
		// (get) Token: 0x0600F62C RID: 63020 RVA: 0x0037DA1A File Offset: 0x0037BC1A
		private GridBinaryImageColumn Owner
		{
			get
			{
				return this.owner as GridBinaryImageColumn;
			}
		}

		// Token: 0x17004A19 RID: 18969
		// (get) Token: 0x0600F62D RID: 63021 RVA: 0x0037DA27 File Offset: 0x0037BC27
		protected override GridUploadControlType UploadControlType
		{
			get
			{
				return this.Owner.UploadControlType;
			}
		}

		// Token: 0x0600F62E RID: 63022 RVA: 0x0037DA34 File Offset: 0x0037BC34
		protected override void CreateControls()
		{
			base.CreateControls();
			base.ControlsCreated = true;
			if (this.UploadControlType == GridUploadControlType.RadUpload)
			{
				RadUpload radUploadControl = base.RadUploadControl;
				radUploadControl.AllowedFileExtensions = new string[]
				{
					".gif",
					".jpg",
					".jpeg",
					".bmp",
					".png",
					".GIF",
					".JPG",
					".JPEG",
					".BMP",
					".PNG"
				};
				return;
			}
			RadAsyncUpload radAsyncUploadControl = base.RadAsyncUploadControl;
			radAsyncUploadControl.AllowedFileExtensions = new string[]
			{
				".gif",
				".jpg",
				".jpeg",
				".bmp",
				".png",
				".GIF",
				".JPG",
				".JPEG",
				".BMP",
				".PNG"
			};
		}
	}
}
