using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Xml.Linq;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D96 RID: 3478
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Should not be a problem")]
	internal class XmlaWebClient : XmlaClientBase
	{
		// Token: 0x0600814F RID: 33103 RVA: 0x001D82F0 File Offset: 0x001D64F0
		protected override void BeginNewRequestCore(XmlaClientRequestInfo requestInfo)
		{
			this.SubmitRequestToWebClient(requestInfo);
		}

		// Token: 0x06008150 RID: 33104 RVA: 0x001D82FC File Offset: 0x001D64FC
		private void InitializeWebClient(XmlaClientRequestInfo requestInfo)
		{
			if (this.webClient != null)
			{
				this.webClient.UploadStringCompleted -= this.WebClient_UploadStringCompleted;
			}
			this.webClient = new WebClient();
			this.InitializeHeaders();
			XmlaWebClient.InitializeWebRequestHandling(requestInfo.ConnectionSettings);
			this.InitializeEncoding(requestInfo.ConnectionSettings);
			this.webClient.UploadStringCompleted += this.WebClient_UploadStringCompleted;
			this.SetUpServiceCallCredentials(requestInfo.ConnectionSettings);
		}

		// Token: 0x06008151 RID: 33105 RVA: 0x001D8373 File Offset: 0x001D6573
		private void InitializeHeaders()
		{
			this.webClient.Headers["Content-Type"] = "text/xml";
		}

		// Token: 0x06008152 RID: 33106 RVA: 0x001D838F File Offset: 0x001D658F
		private static void InitializeWebRequestHandling(XmlaConnectionSettings connectionSettings)
		{
			if (connectionSettings == null)
			{
				return;
			}
			string serverAddress = connectionSettings.ServerAddress;
		}

		// Token: 0x06008153 RID: 33107 RVA: 0x001D83A2 File Offset: 0x001D65A2
		private void InitializeEncoding(XmlaConnectionSettings settings)
		{
			if (settings.Encoding == null)
			{
				this.webClient.Encoding = XmlaConnectionSettings.DefaultEncoding;
				return;
			}
			this.webClient.Encoding = settings.Encoding;
		}

		// Token: 0x06008154 RID: 33108 RVA: 0x001D83CE File Offset: 0x001D65CE
		private void SetUpServiceCallCredentials(XmlaConnectionSettings connectionSettings)
		{
			if (connectionSettings.Credentials == null)
			{
				this.webClient.Credentials = null;
				this.webClient.UseDefaultCredentials = true;
				return;
			}
			this.webClient.UseDefaultCredentials = false;
			this.webClient.Credentials = connectionSettings.Credentials;
		}

		// Token: 0x06008155 RID: 33109 RVA: 0x001D840E File Offset: 0x001D660E
		private void SubmitRequestToWebClient(XmlaClientRequestInfo requestInfo)
		{
			if (GlobalOptions.PreferredExecutionStrategy == OperationExecutionStrategy.Asynchronous)
			{
				this.SubmitRequestAsync(requestInfo);
				return;
			}
			this.SubmitRequestBlocking(requestInfo);
		}

		// Token: 0x06008156 RID: 33110 RVA: 0x001D8428 File Offset: 0x001D6628
		private void SubmitRequestAsync(XmlaClientRequestInfo requestInfo)
		{
			string soapRequestString = XmlaWebClient.GetSoapRequestString(requestInfo);
			Uri address = new Uri(requestInfo.ConnectionSettings.ServerAddress);
			this.webClient.UploadStringAsync(address, null, soapRequestString, requestInfo);
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x001D845C File Offset: 0x001D665C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Design choice.")]
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "requestInfo", Justification = "Design choice.")]
		private void SubmitRequestBlocking(XmlaClientRequestInfo requestInfo)
		{
			string soapRequestString = XmlaWebClient.GetSoapRequestString(requestInfo);
			Uri address = new Uri(requestInfo.ConnectionSettings.ServerAddress);
			try
			{
				string result = this.webClient.UploadString(address, soapRequestString);
				XmlaClientRequestCompletedEventArgs e = new XmlaClientRequestCompletedEventArgs(result, requestInfo, null);
				base.OnSendRequestCompleted(e);
			}
			catch (Exception error)
			{
				this.HandleClientWithError(error, requestInfo);
			}
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x001D84C0 File Offset: 0x001D66C0
		private void WebClient_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
		{
			XmlaClientRequestInfo requestInfo = (XmlaClientRequestInfo)e.UserState;
			if (e.Error != null)
			{
				this.HandleClientWithError(e);
				return;
			}
			XmlaClientRequestCompletedEventArgs e2 = new XmlaClientRequestCompletedEventArgs(e.Result, requestInfo, null);
			base.OnSendRequestCompleted(e2);
		}

		// Token: 0x06008159 RID: 33113 RVA: 0x001D8500 File Offset: 0x001D6700
		private static string GetSoapRequestString(XmlaClientRequestInfo requestInfo)
		{
			XElement xelement = XmlaSoapSerializer.Serialize(new SoapEnvelope
			{
				Body = 
				{
					Content = requestInfo.XmlaRequest
				}
			});
			return xelement.ToString();
		}

		// Token: 0x0600815A RID: 33114 RVA: 0x001D8534 File Offset: 0x001D6734
		private void HandleClientWithError(UploadStringCompletedEventArgs argsWithError)
		{
			OlapCommunicationException error = new OlapCommunicationException("Problem with service call", argsWithError.Error);
			XmlaClientRequestInfo request = (XmlaClientRequestInfo)argsWithError.UserState;
			this.HandleClientWithError(error, request);
		}

		// Token: 0x0600815B RID: 33115 RVA: 0x001D8568 File Offset: 0x001D6768
		private void HandleClientWithError(Exception error, XmlaClientRequestInfo request)
		{
			OlapCommunicationException error2 = new OlapCommunicationException("Problem with service call", error);
			base.HandleRequestError(request, error2);
		}

		// Token: 0x0600815C RID: 33116 RVA: 0x001D858C File Offset: 0x001D678C
		internal static OlapCommunicationException GetSoapError(XmlaClientRequestCompletedEventArgs completedArgs)
		{
			if (completedArgs.Error != null)
			{
				return completedArgs.Error;
			}
			XmlaFaultReader xmlaFaultReader = new XmlaFaultReader(completedArgs.Result);
			if (string.IsNullOrEmpty(xmlaFaultReader.FaultString))
			{
				return null;
			}
			return new OlapCommunicationException(xmlaFaultReader.FaultString);
		}

		// Token: 0x0600815D RID: 33117 RVA: 0x001D85D0 File Offset: 0x001D67D0
		protected override void InitializeLocalStateCore(XmlaClientRequestInfo request)
		{
			this.InitializeWebClient(request);
		}

		// Token: 0x040023AF RID: 9135
		private WebClient webClient;
	}
}
