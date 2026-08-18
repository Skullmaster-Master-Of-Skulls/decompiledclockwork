using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001AD RID: 429
	public class CloudUploadProviderException : Exception
	{
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00039E7E File Offset: 0x0003807E
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x00039E86 File Offset: 0x00038086
		public string FileKeyName { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00039E8F File Offset: 0x0003808F
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00039E97 File Offset: 0x00038097
		public string UploadId { get; set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00039EA0 File Offset: 0x000380A0
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x00039EA8 File Offset: 0x000380A8
		public string ContainerName { get; set; }

		// Token: 0x06000F83 RID: 3971 RVA: 0x00039EB1 File Offset: 0x000380B1
		public CloudUploadProviderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x00039EBB File Offset: 0x000380BB
		public CloudUploadProviderException(string message, Exception innerException, string fileKeyName, string containerName) : this(message, innerException)
		{
			this.FileKeyName = fileKeyName;
			this.ContainerName = containerName;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x00039ED4 File Offset: 0x000380D4
		public CloudUploadProviderException(string message, Exception innerException, string fileKeyName, string containerName, string uploadId) : this(message, innerException, fileKeyName, containerName)
		{
			this.UploadId = uploadId;
		}
	}
}
