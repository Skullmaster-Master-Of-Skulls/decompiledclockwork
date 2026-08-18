using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE0 RID: 2784
	internal class SR
	{
		// Token: 0x17002257 RID: 8791
		// (get) Token: 0x060068C4 RID: 26820 RVA: 0x00188D20 File Offset: 0x00186F20
		public static string SheetName
		{
			get
			{
				return SR.Keys.GetString("SheetName");
			}
		}

		// Token: 0x02000AE1 RID: 2785
		private sealed class Keys
		{
			// Token: 0x17002258 RID: 8792
			// (get) Token: 0x060068C6 RID: 26822 RVA: 0x00188D44 File Offset: 0x00186F44
			private static string ResourceName
			{
				get
				{
					SR.Keys.resourceName = (from x in SR.Keys.execAsm.GetManifestResourceNames()
					where x.Contains("ExcelExportStrings")
					select x).Single<string>().Replace(".resources", string.Empty);
					return SR.Keys.resourceName;
				}
			}

			// Token: 0x17002259 RID: 8793
			// (get) Token: 0x060068C8 RID: 26824 RVA: 0x00188DA3 File Offset: 0x00186FA3
			// (set) Token: 0x060068C9 RID: 26825 RVA: 0x00188DB3 File Offset: 0x00186FB3
			public static CultureInfo Culture
			{
				get
				{
					return SR.Keys.culture ?? CultureInfo.InvariantCulture;
				}
				set
				{
					SR.Keys.culture = value;
				}
			}

			// Token: 0x060068CA RID: 26826 RVA: 0x00188DBB File Offset: 0x00186FBB
			public static string GetString(string name)
			{
				return SR.Keys.resourceManager.GetString(name, SR.Keys.execAsm.GetName().CultureInfo);
			}

			// Token: 0x04001BF9 RID: 7161
			public const string SheetName = "SheetName";

			// Token: 0x04001BFA RID: 7162
			public const string resourcesSuffix = ".resources";

			// Token: 0x04001BFB RID: 7163
			private static Assembly execAsm = Assembly.GetExecutingAssembly();

			// Token: 0x04001BFC RID: 7164
			private static string resourceName = null;

			// Token: 0x04001BFD RID: 7165
			private static readonly ResourceManager resourceManager = new ResourceManager(SR.Keys.ResourceName, SR.Keys.execAsm);

			// Token: 0x04001BFE RID: 7166
			private static CultureInfo culture = null;
		}
	}
}
