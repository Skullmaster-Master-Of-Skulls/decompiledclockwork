using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x0200182F RID: 6191
	internal class SimpleCompressionLogger : IRadCompressionLogger
	{
		// Token: 0x0600F0C1 RID: 61633 RVA: 0x0036B9F4 File Offset: 0x00369BF4
		public SimpleCompressionLogger(bool isEnabled)
		{
			this._isEnabled = isEnabled;
			this._trace = HttpContext.Current.Trace;
		}

		// Token: 0x170048C1 RID: 18625
		// (get) Token: 0x0600F0C2 RID: 61634 RVA: 0x0036BA13 File Offset: 0x00369C13
		protected bool IsEnabled
		{
			get
			{
				return this._trace.IsEnabled && this._isEnabled;
			}
		}

		// Token: 0x0600F0C3 RID: 61635 RVA: 0x0036BA2A File Offset: 0x00369C2A
		public void Write(string message)
		{
			this._trace.Write("RadCompression", message);
		}

		// Token: 0x0600F0C4 RID: 61636 RVA: 0x0036BA3D File Offset: 0x00369C3D
		public void Write(TFunc<string> info)
		{
			if (this.IsEnabled)
			{
				this.Write(info());
			}
		}

		// Token: 0x04004551 RID: 17745
		private const string categoryName = "RadCompression";

		// Token: 0x04004552 RID: 17746
		private TraceContext _trace;

		// Token: 0x04004553 RID: 17747
		private bool _isEnabled;
	}
}
