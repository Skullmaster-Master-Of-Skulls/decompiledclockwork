using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02001344 RID: 4932
	public class SPRadUploadHttpModule : RadUploadHttpModule
	{
		// Token: 0x0600CD97 RID: 52631 RVA: 0x002DC40F File Offset: 0x002DA60F
		public override void Init(HttpApplication app)
		{
			app.BeginRequest += this.CaptureWorkerRequest;
			app.EndRequest += this.ReleaseWorkerRequest;
		}
	}
}
