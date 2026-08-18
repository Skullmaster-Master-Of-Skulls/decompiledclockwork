using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Telerik.Web.UI
{
	// Token: 0x020004BB RID: 1211
	[TelerikToolboxCategory("Data")]
	public abstract class GridUploadColumnEditor : GridColumnEditorBase
	{
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06002B19 RID: 11033
		protected abstract GridUploadControlType UploadControlType { get; }

		// Token: 0x06002B1A RID: 11034 RVA: 0x0008BABB File Offset: 0x00089CBB
		public GridUploadColumnEditor()
		{
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0008BAC3 File Offset: 0x00089CC3
		public GridUploadColumnEditor(GridEditableColumn owner)
		{
			this.owner = owner;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x0008BAD2 File Offset: 0x00089CD2
		protected override void AddControlsToContainer()
		{
			if (this.UploadControlType == GridUploadControlType.RadUpload)
			{
				this.ContainerControl.Controls.Add(this._radUpload);
				return;
			}
			this.ContainerControl.Controls.Add(this.radAsyncUpload);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x0008BB0C File Offset: 0x00089D0C
		protected override void LoadControlsFromContainer()
		{
			if (this.UploadControlType == GridUploadControlType.RadUpload)
			{
				this._radUpload = (this.ContainerControl.Controls[0] as RadUpload);
				return;
			}
			this.radAsyncUpload = (this.ContainerControl.Controls[0] as RadAsyncUpload);
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x0008BB5A File Offset: 0x00089D5A
		public RadUpload RadUploadControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this._radUpload;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x0008BB68 File Offset: 0x00089D68
		public RadAsyncUpload RadAsyncUploadControl
		{
			get
			{
				this.EnsureControlsCreated();
				return this.radAsyncUpload;
			}
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0008BB76 File Offset: 0x00089D76
		protected override void CreateControls()
		{
			if (this.UploadControlType == GridUploadControlType.RadUpload)
			{
				this._radUpload = this.CreateRadUpload();
				return;
			}
			this.radAsyncUpload = this.CreateRadAsyncUpload();
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0008BB9C File Offset: 0x00089D9C
		protected RadUpload CreateRadUpload()
		{
			RadUpload radUpload = new RadUpload();
			radUpload.ID = string.Format("RUC_{0}", this.owner.UniqueName);
			radUpload.RenderMode = this.owner.Owner.OwnerGrid.RenderMode;
			radUpload.EnableEmbeddedSkins = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			radUpload.PreRender += this.upload_PreRender;
			radUpload.InitialFileInputsCount = 1;
			radUpload.MaxFileInputsCount = 1;
			radUpload.ControlObjectsVisibility = ControlObjectsVisibility.None;
			radUpload.ReadOnlyFileInputs = true;
			return radUpload;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0008BC30 File Offset: 0x00089E30
		private void upload_PreRender(object sender, EventArgs e)
		{
			ISkinnableControl skinnableControl = sender as ISkinnableControl;
			skinnableControl.Skin = this.owner.Owner.OwnerGrid.RuntimeSkin;
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0008BC60 File Offset: 0x00089E60
		protected RadAsyncUpload CreateRadAsyncUpload()
		{
			RadAsyncUpload radAsyncUpload = new RadAsyncUpload();
			radAsyncUpload.ID = string.Format("RAUC_{0}", this.owner.UniqueName);
			radAsyncUpload.RenderMode = this.owner.Owner.OwnerGrid.RenderMode;
			radAsyncUpload.EnableEmbeddedSkins = this.owner.Owner.OwnerGrid.EnableEmbeddedSkins;
			radAsyncUpload.PreRender += this.upload_PreRender;
			radAsyncUpload.InitialFileInputsCount = 1;
			radAsyncUpload.MaxFileInputsCount = 1;
			return radAsyncUpload;
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0008BCE5 File Offset: 0x00089EE5
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public byte[] UploadedFileContent
		{
			get
			{
				if (this._uploadedFileContent == null)
				{
					if (this.UploadControlType == GridUploadControlType.RadUpload)
					{
						this._uploadedFileContent = this.RadUploadUploadedFileContent;
					}
					else
					{
						this._uploadedFileContent = this.RadAsyncUploadUploadedFileContent;
					}
				}
				return this._uploadedFileContent;
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x0008BD18 File Offset: 0x00089F18
		protected byte[] RadUploadUploadedFileContent
		{
			get
			{
				RadUpload radUploadControl = this.RadUploadControl;
				if (radUploadControl.UploadedFiles.Count > 0)
				{
					UploadedFile uploadedFile = radUploadControl.UploadedFiles[0];
					Stream inputStream = uploadedFile.InputStream;
					byte[] array = new byte[inputStream.Length];
					inputStream.Read(array, 0, (int)inputStream.Length);
					inputStream.Seek(0L, SeekOrigin.Begin);
					return array;
				}
				return new byte[0];
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x0008BD80 File Offset: 0x00089F80
		protected byte[] RadAsyncUploadUploadedFileContent
		{
			get
			{
				RadAsyncUpload radAsyncUploadControl = this.RadAsyncUploadControl;
				if (radAsyncUploadControl.UploadedFiles.Count > 0)
				{
					UploadedFile uploadedFile = radAsyncUploadControl.UploadedFiles[0];
					Stream inputStream = uploadedFile.InputStream;
					byte[] array = new byte[inputStream.Length];
					inputStream.Read(array, 0, (int)inputStream.Length);
					inputStream.Seek(0L, SeekOrigin.Begin);
					return array;
				}
				return new byte[0];
			}
		}

		// Token: 0x04000B4E RID: 2894
		protected GridEditableColumn owner;

		// Token: 0x04000B4F RID: 2895
		private RadUpload _radUpload;

		// Token: 0x04000B50 RID: 2896
		private RadAsyncUpload radAsyncUpload;

		// Token: 0x04000B51 RID: 2897
		private byte[] _uploadedFileContent;
	}
}
