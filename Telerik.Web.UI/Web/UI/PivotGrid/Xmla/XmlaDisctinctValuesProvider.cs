using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000741 RID: 1857
	internal class XmlaDisctinctValuesProvider : DistinctValuesProvider
	{
		// Token: 0x060041F6 RID: 16886 RVA: 0x000CEF98 File Offset: 0x000CD198
		internal XmlaDisctinctValuesProvider(IXmlaClient client, XmlaConnectionSettings connectionSettings, OlapHierarchyFieldInfo fieldInfo)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (connectionSettings == null)
			{
				throw new ArgumentNullException("connectionSettings");
			}
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			this.client = client;
			this.connectionSettings = connectionSettings;
			this.fieldInfo = fieldInfo;
			this.disctinctValues = new List<object>();
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000CEFFB File Offset: 0x000CD1FB
		public XmlaDisctinctValuesProvider(XmlaConnectionSettings connectionSettings, OlapHierarchyFieldInfo fieldInfo, int setConditionListCapacity) : this(new XmlaWebClient(), connectionSettings, fieldInfo)
		{
			this.SetConditionListCapacity = setConditionListCapacity;
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x000CF011 File Offset: 0x000CD211
		public override IEnumerable<object> DisctinctValues
		{
			get
			{
				return this.disctinctValues;
			}
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000CF01C File Offset: 0x000CD21C
		public override void Refresh()
		{
			OlapPivotConfiguration pivotconfiguration = new OlapPivotConfiguration();
			string xmlaRequest = this.GetXmlaRequest(this.fieldInfo.Name);
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, pivotconfiguration);
			this.client.SendRequestCompleted += this.ClientSendRequestCompleted;
			this.client.SendRequestAsync(requestInfo);
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000CF074 File Offset: 0x000CD274
		private string GetXmlaRequest(string name)
		{
			string text = string.Empty;
			if (this.fieldInfo.SupportsMembersFunction)
			{
				text = ".Children";
			}
			XmlaTextBodyCommand commandToExecute = (XmlaTextBodyCommand)XmlaCommands.Statement(string.Concat(new string[]
			{
				"SELECT {TOPCOUNT(AddCalculatedMembers({",
				name,
				text,
				"}), ",
				this.SetConditionListCapacity.ToString(),
				")} ON COLUMNS FROM [",
				this.connectionSettings.Cube,
				"]"
			}));
			XmlaMethodExecute xmlaMethodExecute = new XmlaMethodExecute(commandToExecute);
			xmlaMethodExecute.AddProperty(XmlaProperties.Catalog(this.connectionSettings.Database));
			xmlaMethodExecute.AddProperty(XmlaProperties.Format(XmlaFormatTypes.Multidimensional));
			xmlaMethodExecute.AddProperty(XmlaProperties.Content(XmlaContentTypes.Data));
			xmlaMethodExecute.MergeProperties(this.connectionSettings.QueryProperties);
			return xmlaMethodExecute.ToXml();
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000CF154 File Offset: 0x000CD354
		private void ClientSendRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.client.SendRequestCompleted -= this.ClientSendRequestCompleted;
			if (e.Error != null)
			{
				return;
			}
			OlapCommunicationException soapError = XmlaWebClient.GetSoapError(e);
			if (soapError != null)
			{
				return;
			}
			this.CreateDistinctValuesForValidResponse(e);
			base.OnUpdated();
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000CF19C File Offset: 0x000CD39C
		private void CreateDistinctValuesForValidResponse(XmlaClientRequestCompletedEventArgs e)
		{
			XmlaResponseData xmlaResponseData = new XmlaResponseData(e.RequestInfo.PivotConfiguration, e.Result);
			List<object> list = new List<object>();
			if (xmlaResponseData.ColumnAxisTuples.Count == 0)
			{
				return;
			}
			foreach (IOlapTuple tuple in xmlaResponseData.ColumnAxisTuples)
			{
				MemberDistinctValue distinctValueFromTuple = XmlaDisctinctValuesProvider.GetDistinctValueFromTuple(tuple);
				if (distinctValueFromTuple != null)
				{
					list.Add(distinctValueFromTuple);
				}
			}
			this.disctinctValues = list;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000CF22C File Offset: 0x000CD42C
		private static MemberDistinctValue GetDistinctValueFromTuple(IOlapTuple tuple)
		{
			MemberDistinctValue memberDistinctValue = null;
			using (IEnumerator enumerator = tuple.Members.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IOlapMember olapMember = (IOlapMember)enumerator.Current;
					memberDistinctValue = new MemberDistinctValue(olapMember.UniqueName);
					memberDistinctValue.Caption = olapMember.Caption;
				}
			}
			return memberDistinctValue;
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x060041FE RID: 16894 RVA: 0x000CF29C File Offset: 0x000CD49C
		// (set) Token: 0x060041FF RID: 16895 RVA: 0x000CF2A4 File Offset: 0x000CD4A4
		public int SetConditionListCapacity { get; set; }

		// Token: 0x0400116D RID: 4461
		private readonly XmlaConnectionSettings connectionSettings;

		// Token: 0x0400116E RID: 4462
		private readonly OlapHierarchyFieldInfo fieldInfo;

		// Token: 0x0400116F RID: 4463
		private readonly IXmlaClient client;

		// Token: 0x04001170 RID: 4464
		private IEnumerable<object> disctinctValues;
	}
}
