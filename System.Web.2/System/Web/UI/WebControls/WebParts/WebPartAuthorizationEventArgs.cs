using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200057B RID: 1403
	public class WebPartAuthorizationEventArgs : EventArgs
	{
		// Token: 0x0600473E RID: 18238 RVA: 0x000EA7DB File Offset: 0x000E89DB
		public WebPartAuthorizationEventArgs(Type type, string path, string authorizationFilter, bool isShared)
		{
			this._type = type;
			this._path = path;
			this._authorizationFilter = authorizationFilter;
			this._isShared = isShared;
			this._isAuthorized = true;
		}

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x000EA807 File Offset: 0x000E8A07
		public string AuthorizationFilter
		{
			get
			{
				return this._authorizationFilter;
			}
		}

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x000EA80F File Offset: 0x000E8A0F
		// (set) Token: 0x06004741 RID: 18241 RVA: 0x000EA817 File Offset: 0x000E8A17
		public bool IsAuthorized
		{
			get
			{
				return this._isAuthorized;
			}
			set
			{
				this._isAuthorized = value;
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x000EA820 File Offset: 0x000E8A20
		public bool IsShared
		{
			get
			{
				return this._isShared;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x000EA828 File Offset: 0x000E8A28
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x000EA830 File Offset: 0x000E8A30
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040026E6 RID: 9958
		private Type _type;

		// Token: 0x040026E7 RID: 9959
		private string _path;

		// Token: 0x040026E8 RID: 9960
		private string _authorizationFilter;

		// Token: 0x040026E9 RID: 9961
		private bool _isShared;

		// Token: 0x040026EA RID: 9962
		private bool _isAuthorized;
	}
}
