using System;
using System.Data;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000535 RID: 1333
	internal sealed class ConnectionsZoneAutoFormat : BaseAutoFormat
	{
		// Token: 0x06002F3D RID: 12093 RVA: 0x0010DC7E File Offset: 0x0010CC7E
		public ConnectionsZoneAutoFormat(DataRow schemeData) : base(schemeData)
		{
			base.Style.Width = 225;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x0010DC9C File Offset: 0x0010CC9C
		public override Control GetPreviewControl(Control runtimeControl)
		{
			ConnectionsZone connectionsZone = (ConnectionsZone)base.GetPreviewControl(runtimeControl);
			connectionsZone.ID = "AutoFormatPreviewControl";
			return connectionsZone;
		}

		// Token: 0x04002034 RID: 8244
		internal const string PreviewControlID = "AutoFormatPreviewControl";
	}
}
