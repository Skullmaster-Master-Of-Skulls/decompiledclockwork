using System;

namespace Telerik.Web.UI.CloudUpload
{
	// Token: 0x020001AA RID: 426
	public class SetKeyNameEventArgs : EventArgs
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x00039E1D File Offset: 0x0003801D
		public SetKeyNameEventArgs(string originalName, string subFolderStructure)
		{
			this._originalName = originalName;
			this._subFolderStructure = subFolderStructure;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00039E33 File Offset: 0x00038033
		public string OriginalFileName
		{
			get
			{
				return this._originalName;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00039E3B File Offset: 0x0003803B
		public string SubFolderStructure
		{
			get
			{
				return this._subFolderStructure;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00039E43 File Offset: 0x00038043
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x00039E4B File Offset: 0x0003804B
		public string KeyName { get; set; }

		// Token: 0x04000461 RID: 1121
		private string _originalName;

		// Token: 0x04000462 RID: 1122
		private string _subFolderStructure;
	}
}
