using System;
using System.ComponentModel;
using Spire.License;

namespace Spire.DataExport.License
{
	// Token: 0x02000194 RID: 404
	public class DataExportLicenseProvider : System.ComponentModel.LicenseProvider
	{
		// Token: 0x06000B23 RID: 2851 RVA: 0x000732E0 File Offset: 0x000722E0
		public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
		{
			License license;
			for (;;)
			{
				license = new Spire.License.LicenseProvider().GetLicense(context, type, instance, allowExceptions);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_40;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						if (((LicenseInfo)license).IsUpdateRightExpired)
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					case 3:
						goto IL_EF;
					case 4:
						goto IL_76;
					case 5:
						if (((LicenseInfo)license).Type != LicenseType.Runtime)
						{
							num = 4;
							continue;
						}
						goto IL_F1;
					case 6:
						if (true)
						{
						}
						num = 7;
						continue;
					case 7:
						if (license.GetType() == typeof(LicenseInfo))
						{
							num = 1;
							continue;
						}
						goto IL_78;
					}
					break;
					IL_40:
					if (license == null)
					{
						goto IL_78;
					}
					num = 6;
				}
			}
			IL_76:
			return null;
			IL_78:
			return null;
			IL_EF:
			goto IL_78;
			IL_F1:
			return new spr\u21E5(license.LicenseKey);
		}
	}
}
