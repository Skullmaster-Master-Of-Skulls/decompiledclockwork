using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000741 RID: 1857
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class WebPartTracker : IDisposable
	{
		// Token: 0x06005A24 RID: 23076 RVA: 0x0016C094 File Offset: 0x0016B094
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
			if (++this.Count > 1)
			{
				webPart.SetConnectErrorMessage(SR.GetString("WebPartTracker_CircularConnection", new object[]
				{
					this._providerConnectionPoint.DisplayName
				}));
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x06005A25 RID: 23077 RVA: 0x0016C12E File Offset: 0x0016B12E
		public bool IsCircularConnection
		{
			get
			{
				return this.Count > 1;
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06005A26 RID: 23078 RVA: 0x0016C13C File Offset: 0x0016B13C
		// (set) Token: 0x06005A27 RID: 23079 RVA: 0x0016C163 File Offset: 0x0016B163
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

		// Token: 0x06005A28 RID: 23080 RVA: 0x0016C17C File Offset: 0x0016B17C
		void IDisposable.Dispose()
		{
			if (!this._disposed)
			{
				this.Count--;
				this._disposed = true;
			}
		}

		// Token: 0x0400307D RID: 12413
		private bool _disposed;

		// Token: 0x0400307E RID: 12414
		private WebPart _webPart;

		// Token: 0x0400307F RID: 12415
		private ProviderConnectionPoint _providerConnectionPoint;
	}
}
