using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x02000450 RID: 1104
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PartialCachingAttribute : Attribute
	{
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x000E3BAC File Offset: 0x000E2BAC
		public int Duration
		{
			get
			{
				return this._duration;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x000E3BB4 File Offset: 0x000E2BB4
		public string VaryByParams
		{
			get
			{
				return this._varyByParams;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x000E3BBC File Offset: 0x000E2BBC
		public string VaryByControls
		{
			get
			{
				return this._varyByControls;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x000E3BC4 File Offset: 0x000E2BC4
		public string VaryByCustom
		{
			get
			{
				return this._varyByCustom;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x000E3BCC File Offset: 0x000E2BCC
		public string SqlDependency
		{
			get
			{
				return this._sqlDependency;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x000E3BD4 File Offset: 0x000E2BD4
		public bool Shared
		{
			get
			{
				return this._shared;
			}
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000E3BDC File Offset: 0x000E2BDC
		public PartialCachingAttribute(int duration)
		{
			this._duration = duration;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000E3BEB File Offset: 0x000E2BEB
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom)
		{
			this._duration = duration;
			this._varyByParams = varyByParams;
			this._varyByControls = varyByControls;
			this._varyByCustom = varyByCustom;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000E3C10 File Offset: 0x000E2C10
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, bool shared)
		{
			this._duration = duration;
			this._varyByParams = varyByParams;
			this._varyByControls = varyByControls;
			this._varyByCustom = varyByCustom;
			this._shared = shared;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000E3C3D File Offset: 0x000E2C3D
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, bool shared)
		{
			this._duration = duration;
			this._varyByParams = varyByParams;
			this._varyByControls = varyByControls;
			this._varyByCustom = varyByCustom;
			this._shared = shared;
			this._sqlDependency = sqlDependency;
		}

		// Token: 0x040024C2 RID: 9410
		private int _duration;

		// Token: 0x040024C3 RID: 9411
		private string _varyByParams;

		// Token: 0x040024C4 RID: 9412
		private string _varyByControls;

		// Token: 0x040024C5 RID: 9413
		private string _varyByCustom;

		// Token: 0x040024C6 RID: 9414
		private string _sqlDependency;

		// Token: 0x040024C7 RID: 9415
		private bool _shared;
	}
}
