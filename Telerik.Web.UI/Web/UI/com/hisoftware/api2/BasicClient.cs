using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Telerik.Web.UI.com.hisoftware.api2
{
	// Token: 0x02001364 RID: 4964
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class BasicClient : ClientBase<Basic>, Basic
	{
		// Token: 0x0600CF5E RID: 53086 RVA: 0x002E0318 File Offset: 0x002DE518
		public BasicClient()
		{
		}

		// Token: 0x0600CF5F RID: 53087 RVA: 0x002E0320 File Offset: 0x002DE520
		public BasicClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x0600CF60 RID: 53088 RVA: 0x002E0329 File Offset: 0x002DE529
		public BasicClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600CF61 RID: 53089 RVA: 0x002E0333 File Offset: 0x002DE533
		public BasicClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x0600CF62 RID: 53090 RVA: 0x002E033D File Offset: 0x002DE53D
		public BasicClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x0600CF63 RID: 53091 RVA: 0x002E0347 File Offset: 0x002DE547
		public void DoFormSubmit(Stream input)
		{
			base.Channel.DoFormSubmit(input);
		}

		// Token: 0x0600CF64 RID: 53092 RVA: 0x002E0355 File Offset: 0x002DE555
		public ResultInformation RunOnDemandScanContent(string apiKey, string displayName, string url, List<string> checkpointGroupIds, byte[] content, string encoding, int expiryTime)
		{
			return base.Channel.RunOnDemandScanContent(apiKey, displayName, url, checkpointGroupIds, content, encoding, expiryTime);
		}

		// Token: 0x0600CF65 RID: 53093 RVA: 0x002E036D File Offset: 0x002DE56D
		public ResultInformation RunOnDemandScan(string apiKey, string displayName, string url, List<string> checkpointGroupIds, string httpUserAgent, string encoding, int expiryTime)
		{
			return base.Channel.RunOnDemandScan(apiKey, displayName, url, checkpointGroupIds, httpUserAgent, encoding, expiryTime);
		}

		// Token: 0x0600CF66 RID: 53094 RVA: 0x002E0385 File Offset: 0x002DE585
		public bool CreateAccount(string email)
		{
			return base.Channel.CreateAccount(email);
		}

		// Token: 0x0600CF67 RID: 53095 RVA: 0x002E0393 File Offset: 0x002DE593
		public string ConfirmAccount(string email, string confirmationCode)
		{
			return base.Channel.ConfirmAccount(email, confirmationCode);
		}

		// Token: 0x0600CF68 RID: 53096 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		public Account GetAccount(string apiKey)
		{
			return base.Channel.GetAccount(apiKey);
		}

		// Token: 0x0600CF69 RID: 53097 RVA: 0x002E03B0 File Offset: 0x002DE5B0
		public string ResetApiKey(string email, string confirmationCode)
		{
			return base.Channel.ResetApiKey(email, confirmationCode);
		}

		// Token: 0x0600CF6A RID: 53098 RVA: 0x002E03BF File Offset: 0x002DE5BF
		public List<Result> GetResultsSimple(string apiKey, string scanID)
		{
			return base.Channel.GetResultsSimple(apiKey, scanID);
		}

		// Token: 0x0600CF6B RID: 53099 RVA: 0x002E03CE File Offset: 0x002DE5CE
		public ResultInformation GetResultsFull(string apiKey, string scanID)
		{
			return base.Channel.GetResultsFull(apiKey, scanID);
		}

		// Token: 0x0600CF6C RID: 53100 RVA: 0x002E03DD File Offset: 0x002DE5DD
		public List<CheckpointGroup> GetCheckpointGroups(string apiKey, bool includeSubgroups)
		{
			return base.Channel.GetCheckpointGroups(apiKey, includeSubgroups);
		}
	}
}
