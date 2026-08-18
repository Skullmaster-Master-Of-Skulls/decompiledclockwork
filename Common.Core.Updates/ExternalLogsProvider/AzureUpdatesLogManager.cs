using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage;
using TechnoPro.Common.ClientManager.Core.Azure.Storage;
using TechnoPro.Common.ClientManager.ICore.Azure.Storage;
using TechnoPro.Common.Core.Azure.Storage;
using TechnoPro.Common.ICore.Azure.Storage;
using TechnoPro.Common.ICore.Updates;

namespace TechnoPro.Common.Core.Updates.ExternalLogsProvider
{
	// Token: 0x0200000F RID: 15
	public class AzureUpdatesLogManager : IExternalLogManager
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004F66 File Offset: 0x00003166
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00004F6E File Offset: 0x0000316E
		private Uri LogsBlobUri { get; set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00004F78 File Offset: 0x00003178
		public AzureUpdatesLogManager(string clientId)
		{
			IClockWorkSasTokenProviderClientManager clockWorkSasTokenProviderClientManager = new ClockWorkSasTokenProviderClientManager();
			GetUpdatingSystemClientPrivateContainerSasUriResp updatingSystemClientPrivateContainerSasUri = clockWorkSasTokenProviderClientManager.GetUpdatingSystemClientPrivateContainerSasUri(TokenBasedClientCredentialsFactory.GenerateToken(clientId, null));
			this.LogsBlobUri = updatingSystemClientPrivateContainerSasUri.LogsBlobSasUri;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004FAE File Offset: 0x000031AE
		public AzureUpdatesLogManager(Uri clientLogsBlobUri)
		{
			this.LogsBlobUri = clientLogsBlobUri;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004FC0 File Offset: 0x000031C0
		public void Log(string text)
		{
			IAzureStorageManager azureStorageManager = new AzureStorageManager();
			string text2 = DateTime.Now.ToString("MMM dd, yyyy hh:mm:ss tt") + ": " + text + Environment.NewLine;
			azureStorageManager.WriteToAppendBlob(this.LogsBlobUri, text2);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005008 File Offset: 0x00003208
		[DebuggerStepThrough]
		public Task LogAsync(string text)
		{
			AzureUpdatesLogManager.<LogAsync>d__7 <LogAsync>d__ = new AzureUpdatesLogManager.<LogAsync>d__7();
			<LogAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LogAsync>d__.<>4__this = this;
			<LogAsync>d__.text = text;
			<LogAsync>d__.<>1__state = -1;
			<LogAsync>d__.<>t__builder.Start<AzureUpdatesLogManager.<LogAsync>d__7>(ref <LogAsync>d__);
			return <LogAsync>d__.<>t__builder.Task;
		}
	}
}
