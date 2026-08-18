using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	// Token: 0x0200029D RID: 669
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class UnescapedXmlDiagnosticData
	{
		// Token: 0x0600185B RID: 6235 RVA: 0x0005841C File Offset: 0x0005661C
		public UnescapedXmlDiagnosticData(string xmlPayload)
		{
			this._xmlString = xmlPayload;
			if (this._xmlString == null)
			{
				this._xmlString = string.Empty;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0005843E File Offset: 0x0005663E
		// (set) Token: 0x0600185D RID: 6237 RVA: 0x00058446 File Offset: 0x00056646
		public string UnescapedXml
		{
			get
			{
				return this._xmlString;
			}
			set
			{
				this._xmlString = value;
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0005844F File Offset: 0x0005664F
		public override string ToString()
		{
			return this._xmlString;
		}

		// Token: 0x04000BAC RID: 2988
		private string _xmlString;
	}
}
