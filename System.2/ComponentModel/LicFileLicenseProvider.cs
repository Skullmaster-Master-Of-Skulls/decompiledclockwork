using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000583 RID: 1411
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class LicFileLicenseProvider : LicenseProvider
	{
		// Token: 0x06003429 RID: 13353 RVA: 0x000E4B06 File Offset: 0x000E2D06
		protected virtual bool IsKeyValid(string key, Type type)
		{
			return key != null && key.StartsWith(this.GetKey(type));
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000E4B1A File Offset: 0x000E2D1A
		protected virtual string GetKey(Type type)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} is a licensed component.", new object[]
			{
				type.FullName
			});
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000E4B3C File Offset: 0x000E2D3C
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			LicFileLicenseProvider.LicFileLicense licFileLicense = null;
			if (context != null)
			{
				if (context.UsageMode == LicenseUsageMode.Runtime)
				{
					string savedLicenseKey = context.GetSavedLicenseKey(type, null);
					if (savedLicenseKey != null && this.IsKeyValid(savedLicenseKey, type))
					{
						licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, savedLicenseKey);
					}
				}
				if (licFileLicense == null)
				{
					string text = null;
					if (context != null)
					{
						ITypeResolutionService typeResolutionService = (ITypeResolutionService)context.GetService(typeof(ITypeResolutionService));
						if (typeResolutionService != null)
						{
							text = typeResolutionService.GetPathOfAssembly(type.Assembly.GetName());
						}
					}
					if (text == null)
					{
						text = type.Module.FullyQualifiedName;
					}
					string directoryName = Path.GetDirectoryName(text);
					string path = directoryName + "\\" + type.FullName + ".lic";
					if (File.Exists(path))
					{
						Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
						StreamReader streamReader = new StreamReader(stream);
						string key = streamReader.ReadLine();
						streamReader.Close();
						if (this.IsKeyValid(key, type))
						{
							licFileLicense = new LicFileLicenseProvider.LicFileLicense(this, this.GetKey(type));
						}
					}
					if (licFileLicense != null)
					{
						context.SetSavedLicenseKey(type, licFileLicense.LicenseKey);
					}
				}
			}
			return licFileLicense;
		}

		// Token: 0x02000896 RID: 2198
		private class LicFileLicense : License
		{
			// Token: 0x060045A5 RID: 17829 RVA: 0x001235D7 File Offset: 0x001217D7
			public LicFileLicense(LicFileLicenseProvider owner, string key)
			{
				this.owner = owner;
				this.key = key;
			}

			// Token: 0x17000FBF RID: 4031
			// (get) Token: 0x060045A6 RID: 17830 RVA: 0x001235ED File Offset: 0x001217ED
			public override string LicenseKey
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x060045A7 RID: 17831 RVA: 0x001235F5 File Offset: 0x001217F5
			public override void Dispose()
			{
				GC.SuppressFinalize(this);
			}

			// Token: 0x040037D7 RID: 14295
			private LicFileLicenseProvider owner;

			// Token: 0x040037D8 RID: 14296
			private string key;
		}
	}
}
