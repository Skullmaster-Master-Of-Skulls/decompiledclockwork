using System;
using System.Linq;
using System.Reflection;

namespace Telerik.Licensing
{
	// Token: 0x0200041D RID: 1053
	internal class LicenseContextManager : ILicenseManager
	{
		// Token: 0x060025F8 RID: 9720 RVA: 0x0007D042 File Offset: 0x0007B242
		public LicenseContextManager(ILicenseContextData data)
		{
			this._contextData = data;
			this._licensingEnabled = false;
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0007D058 File Offset: 0x0007B258
		public bool LicensingEnabled
		{
			get
			{
				return this._licensingEnabled;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x0007D060 File Offset: 0x0007B260
		public ILicenseContextData ContextData
		{
			get
			{
				return this._contextData;
			}
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x0007D068 File Offset: 0x0007B268
		public void SaveLicenseKey(Type type, ILicenseKey key)
		{
			if (this.LicensingEnabled)
			{
				this.ContextData.Context.SetSavedLicenseKey(type, key.Key);
			}
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0007D089 File Offset: 0x0007B289
		public ILicenseKey ExtractLicenseKey(Type type)
		{
			if (this.LicensingEnabled)
			{
				return new RuntimeKey(this.ContextData.Context.GetSavedLicenseKey(type, this.FindLicenseAssembly()));
			}
			return new DefaultKey();
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0007D0C7 File Offset: 0x0007B2C7
		private Assembly FindLicenseAssembly()
		{
			return (from p in AppDomain.CurrentDomain.GetAssemblies()
			where p.FullName.Contains("App_Licenses")
			select p).FirstOrDefault<Assembly>();
		}

		// Token: 0x040009AC RID: 2476
		private readonly ILicenseContextData _contextData;

		// Token: 0x040009AD RID: 2477
		private readonly bool _licensingEnabled;
	}
}
