using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000025 RID: 37
	public class AjaxFileUploadStartEventArgs : EventArgs
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00005ECE File Offset: 0x000040CE
		public AjaxFileUploadStartEventArgs(int filesInQueue)
		{
			this._filesInQueue = filesInQueue;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005EDD File Offset: 0x000040DD
		public int FilesInQueue
		{
			get
			{
				return this._filesInQueue;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005EE5 File Offset: 0x000040E5
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00005EED File Offset: 0x000040ED
		public string ServerArguments { get; set; }

		// Token: 0x0400006A RID: 106
		private readonly int _filesInQueue;
	}
}
