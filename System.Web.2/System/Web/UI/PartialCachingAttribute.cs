using System;

namespace System.Web.UI
{
	// Token: 0x020002E7 RID: 743
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PartialCachingAttribute : Attribute
	{
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x00070C3C File Offset: 0x0006EE3C
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x00070C44 File Offset: 0x0006EE44
		public int Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x00070C4D File Offset: 0x0006EE4D
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x00070C55 File Offset: 0x0006EE55
		public string VaryByParams
		{
			get
			{
				return this._varyByParams;
			}
			set
			{
				this._varyByParams = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x00070C5E File Offset: 0x0006EE5E
		// (set) Token: 0x06002298 RID: 8856 RVA: 0x00070C66 File Offset: 0x0006EE66
		public string VaryByControls
		{
			get
			{
				return this._varyByControls;
			}
			set
			{
				this._varyByControls = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x00070C6F File Offset: 0x0006EE6F
		// (set) Token: 0x0600229A RID: 8858 RVA: 0x00070C77 File Offset: 0x0006EE77
		public string VaryByCustom
		{
			get
			{
				return this._varyByCustom;
			}
			set
			{
				this._varyByCustom = value;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x00070C80 File Offset: 0x0006EE80
		// (set) Token: 0x0600229C RID: 8860 RVA: 0x00070C88 File Offset: 0x0006EE88
		public string SqlDependency
		{
			get
			{
				return this._sqlDependency;
			}
			set
			{
				this._sqlDependency = value;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x00070C91 File Offset: 0x0006EE91
		// (set) Token: 0x0600229E RID: 8862 RVA: 0x00070C99 File Offset: 0x0006EE99
		public bool Shared
		{
			get
			{
				return this._shared;
			}
			set
			{
				this._shared = value;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x00070CA2 File Offset: 0x0006EEA2
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x00070CB8 File Offset: 0x0006EEB8
		public string ProviderName
		{
			get
			{
				if (this._providerName == null)
				{
					return "AspNetInternalProvider";
				}
				return this._providerName;
			}
			set
			{
				if (value == "AspNetInternalProvider")
				{
					value = null;
				}
				this._providerName = value;
			}
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00070CD1 File Offset: 0x0006EED1
		public PartialCachingAttribute(int duration)
		{
			this._duration = duration;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00070CE0 File Offset: 0x0006EEE0
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom) : this(duration, varyByParams, varyByControls, varyByCustom, null, false)
		{
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00070CEF File Offset: 0x0006EEEF
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, bool shared) : this(duration, varyByParams, varyByControls, varyByCustom, null, shared)
		{
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x00070CFF File Offset: 0x0006EEFF
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, bool shared)
		{
			this._duration = duration;
			this._varyByParams = varyByParams;
			this._varyByControls = varyByControls;
			this._varyByCustom = varyByCustom;
			this._shared = shared;
			this._sqlDependency = sqlDependency;
		}

		// Token: 0x04001C48 RID: 7240
		private int _duration;

		// Token: 0x04001C49 RID: 7241
		private string _varyByParams;

		// Token: 0x04001C4A RID: 7242
		private string _varyByControls;

		// Token: 0x04001C4B RID: 7243
		private string _varyByCustom;

		// Token: 0x04001C4C RID: 7244
		private string _sqlDependency;

		// Token: 0x04001C4D RID: 7245
		private bool _shared;

		// Token: 0x04001C4E RID: 7246
		private string _providerName;
	}
}
