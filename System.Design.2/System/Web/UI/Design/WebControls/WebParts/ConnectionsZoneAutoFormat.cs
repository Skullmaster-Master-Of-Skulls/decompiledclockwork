using System;
using System.Web.UI.WebControls.WebParts;

namespace System.Web.UI.Design.WebControls.WebParts
{
	// Token: 0x02000143 RID: 323
	internal sealed class ConnectionsZoneAutoFormat : ReflectionBasedAutoFormat
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0004AA45 File Offset: 0x00048C45
		public ConnectionsZoneAutoFormat(string schemeName, string schemes) : base(schemeName, schemes)
		{
			base.Style.Width = 225;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0004AA64 File Offset: 0x00048C64
		public override Control GetPreviewControl(Control runtimeControl)
		{
			ConnectionsZone connectionsZone = (ConnectionsZone)base.GetPreviewControl(runtimeControl);
			connectionsZone.ID = "AutoFormatPreviewControl";
			return connectionsZone;
		}

		// Token: 0x04000705 RID: 1797
		internal const string PreviewControlID = "AutoFormatPreviewControl";
	}
}
