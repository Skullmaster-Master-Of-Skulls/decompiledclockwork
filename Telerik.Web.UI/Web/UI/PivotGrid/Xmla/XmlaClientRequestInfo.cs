using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D97 RID: 3479
	internal class XmlaClientRequestInfo : OperationRequestInfo
	{
		// Token: 0x0600815F RID: 33119 RVA: 0x001D85E1 File Offset: 0x001D67E1
		public XmlaClientRequestInfo(string xmlaRequest, XmlaConnectionSettings connectionSettings, IOlapPivotConfiguration pivotconfiguration)
		{
			this.XmlaRequest = xmlaRequest;
			this.ConnectionSettings = connectionSettings;
			this.PivotConfiguration = pivotconfiguration;
		}

		// Token: 0x170028FC RID: 10492
		// (get) Token: 0x06008160 RID: 33120 RVA: 0x001D85FE File Offset: 0x001D67FE
		// (set) Token: 0x06008161 RID: 33121 RVA: 0x001D8606 File Offset: 0x001D6806
		public string XmlaRequest { get; private set; }

		// Token: 0x170028FD RID: 10493
		// (get) Token: 0x06008162 RID: 33122 RVA: 0x001D860F File Offset: 0x001D680F
		// (set) Token: 0x06008163 RID: 33123 RVA: 0x001D8617 File Offset: 0x001D6817
		public XmlaConnectionSettings ConnectionSettings { get; private set; }

		// Token: 0x170028FE RID: 10494
		// (get) Token: 0x06008164 RID: 33124 RVA: 0x001D8620 File Offset: 0x001D6820
		// (set) Token: 0x06008165 RID: 33125 RVA: 0x001D8628 File Offset: 0x001D6828
		public IOlapPivotConfiguration PivotConfiguration { get; private set; }

		// Token: 0x06008166 RID: 33126 RVA: 0x001D8634 File Offset: 0x001D6834
		public override bool Equals(object obj)
		{
			XmlaClientRequestInfo xmlaClientRequestInfo = obj as XmlaClientRequestInfo;
			return xmlaClientRequestInfo != null && this.ConnectionSettings.Equals(xmlaClientRequestInfo.ConnectionSettings) && this.XmlaRequest.Equals(xmlaClientRequestInfo.XmlaRequest);
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x001D8673 File Offset: 0x001D6873
		public override int GetHashCode()
		{
			return this.ConnectionSettings.GetHashCode() ^ this.XmlaRequest.GetHashCode();
		}
	}
}
