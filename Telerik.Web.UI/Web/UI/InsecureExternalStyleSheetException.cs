using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F9E RID: 3998
	internal class InsecureExternalStyleSheetException : Exception
	{
		// Token: 0x0600990D RID: 39181 RVA: 0x0022235B File Offset: 0x0022055B
		public InsecureExternalStyleSheetException(string externalStyleSheetPath)
		{
			this._externalStyleSheetPath = externalStyleSheetPath;
		}

		// Token: 0x17003073 RID: 12403
		// (get) Token: 0x0600990E RID: 39182 RVA: 0x0022236A File Offset: 0x0022056A
		public override string Message
		{
			get
			{
				return string.Format("The style sheet '{0}' is not located in any of the 'Style Sheet' folders designated in the web.config.", this._externalStyleSheetPath);
			}
		}

		// Token: 0x04002B92 RID: 11154
		private const string MessageTemplate = "The style sheet '{0}' is not located in any of the 'Style Sheet' folders designated in the web.config.";

		// Token: 0x04002B93 RID: 11155
		private string _externalStyleSheetPath;
	}
}
