using System;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B75 RID: 7029
	public class ValidateFileEventArgs : UploadedFileEventArgs
	{
		// Token: 0x17005323 RID: 21283
		// (get) Token: 0x06011082 RID: 69762 RVA: 0x003C27B4 File Offset: 0x003C09B4
		// (set) Token: 0x06011083 RID: 69763 RVA: 0x003C27BC File Offset: 0x003C09BC
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			set
			{
				this._isValid = value;
			}
		}

		// Token: 0x17005324 RID: 21284
		// (get) Token: 0x06011084 RID: 69764 RVA: 0x003C27C5 File Offset: 0x003C09C5
		// (set) Token: 0x06011085 RID: 69765 RVA: 0x003C27CD File Offset: 0x003C09CD
		public bool SkipInternalValidation
		{
			get
			{
				return this._skipInternalValidation;
			}
			set
			{
				this._skipInternalValidation = value;
			}
		}

		// Token: 0x06011086 RID: 69766 RVA: 0x003C27D6 File Offset: 0x003C09D6
		internal ValidateFileEventArgs(UploadedFile uploadedFile) : base(uploadedFile)
		{
			this._isValid = true;
			this._skipInternalValidation = false;
		}

		// Token: 0x04004C2F RID: 19503
		private bool _isValid;

		// Token: 0x04004C30 RID: 19504
		private bool _skipInternalValidation;
	}
}
