using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005AD RID: 1453
	public sealed class WebPartTracker : IDisposable
	{
		// Token: 0x06004995 RID: 18837 RVA: 0x000F4AD8 File Offset: 0x000F2CD8
		public WebPartTracker(WebPart webPart, ProviderConnectionPoint providerConnectionPoint)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (providerConnectionPoint == null)
			{
				throw new ArgumentNullException("providerConnectionPoint");
			}
			if (providerConnectionPoint.ControlType != webPart.GetType())
			{
				throw new ArgumentException(SR.GetString("WebPartManager_InvalidConnectionPoint"), "providerConnectionPoint");
			}
			this._webPart = webPart;
			this._providerConnectionPoint = providerConnectionPoint;
			int num = this.Count + 1;
			this.Count = num;
			if (num > 1)
			{
				webPart.SetConnectErrorMessage(SR.GetString("WebPartTracker_CircularConnection", new object[]
				{
					this._providerConnectionPoint.DisplayName
				}));
			}
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x000F4B75 File Offset: 0x000F2D75
		public bool IsCircularConnection
		{
			get
			{
				return this.Count > 1;
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06004997 RID: 18839 RVA: 0x000F4B80 File Offset: 0x000F2D80
		// (set) Token: 0x06004998 RID: 18840 RVA: 0x000F4BA7 File Offset: 0x000F2DA7
		private int Count
		{
			get
			{
				int result;
				this._webPart.TrackerCounter.TryGetValue(this._providerConnectionPoint, out result);
				return result;
			}
			set
			{
				this._webPart.TrackerCounter[this._providerConnectionPoint] = value;
			}
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x000F4BC0 File Offset: 0x000F2DC0
		void IDisposable.Dispose()
		{
			if (!this._disposed)
			{
				int count = this.Count;
				this.Count = count - 1;
				this._disposed = true;
			}
		}

		// Token: 0x040027AF RID: 10159
		private bool _disposed;

		// Token: 0x040027B0 RID: 10160
		private WebPart _webPart;

		// Token: 0x040027B1 RID: 10161
		private ProviderConnectionPoint _providerConnectionPoint;
	}
}
