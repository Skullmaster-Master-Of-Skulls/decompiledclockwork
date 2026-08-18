using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200055E RID: 1374
	public abstract class PersonalizationState
	{
		// Token: 0x060045CB RID: 17867 RVA: 0x000E6318 File Offset: 0x000E4518
		protected PersonalizationState(WebPartManager webPartManager)
		{
			if (webPartManager == null)
			{
				throw new ArgumentNullException("webPartManager");
			}
			this._webPartManager = webPartManager;
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x000E6335 File Offset: 0x000E4535
		public bool IsDirty
		{
			get
			{
				return this._dirty;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060045CD RID: 17869
		public abstract bool IsEmpty { get; }

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x000E633D File Offset: 0x000E453D
		public WebPartManager WebPartManager
		{
			get
			{
				return this._webPartManager;
			}
		}

		// Token: 0x060045CF RID: 17871
		public abstract void ApplyWebPartPersonalization(WebPart webPart);

		// Token: 0x060045D0 RID: 17872
		public abstract void ApplyWebPartManagerPersonalization();

		// Token: 0x060045D1 RID: 17873
		public abstract void ExtractWebPartPersonalization(WebPart webPart);

		// Token: 0x060045D2 RID: 17874
		public abstract void ExtractWebPartManagerPersonalization();

		// Token: 0x060045D3 RID: 17875
		public abstract string GetAuthorizationFilter(string webPartID);

		// Token: 0x060045D4 RID: 17876 RVA: 0x000E6345 File Offset: 0x000E4545
		protected void SetDirty()
		{
			this._dirty = true;
		}

		// Token: 0x060045D5 RID: 17877
		public abstract void SetWebPartDirty(WebPart webPart);

		// Token: 0x060045D6 RID: 17878
		public abstract void SetWebPartManagerDirty();

		// Token: 0x060045D7 RID: 17879 RVA: 0x000E634E File Offset: 0x000E454E
		protected void ValidateWebPart(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (!this._webPartManager.WebParts.Contains(webPart))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "webPart");
			}
		}

		// Token: 0x0400267E RID: 9854
		private WebPartManager _webPartManager;

		// Token: 0x0400267F RID: 9855
		private bool _dirty;
	}
}
