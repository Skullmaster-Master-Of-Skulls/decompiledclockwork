using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.UI
{
	// Token: 0x0200023A RID: 570
	internal interface IPageAsyncTask
	{
		// Token: 0x06001AC1 RID: 6849
		Task ExecuteAsync(object sender, EventArgs e, CancellationToken cancellationToken);
	}
}
