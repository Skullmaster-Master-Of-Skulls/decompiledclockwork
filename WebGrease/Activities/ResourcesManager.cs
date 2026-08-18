using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace WebGrease.Activities
{
	// Token: 0x02000043 RID: 67
	internal static class ResourcesManager
	{
		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C88C File Offset: 0x0000AA8C
		internal static void TryGetResources(string resourcesDirectoryPath, string localeOrThemeName, out Dictionary<string, string> resources)
		{
			resources = new Dictionary<string, string>();
			string fileName;
			if (ResourcesManager.HasResources(resourcesDirectoryPath, localeOrThemeName, out fileName))
			{
				using (ResXResourceReader resXResourceReader = new ResXResourceReader(fileName))
				{
					foreach (object obj in resXResourceReader)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						string text = dictionaryEntry.Key as string;
						string value = dictionaryEntry.Value as string;
						if (text != null)
						{
							resources.Add(text, value);
						}
					}
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000C93C File Offset: 0x0000AB3C
		private static bool HasResources(string resourcesDirectoryPath, string localeOrThemeName, out string resourcePath)
		{
			resourcePath = Path.Combine(resourcesDirectoryPath, localeOrThemeName + ".resx");
			return File.Exists(resourcePath);
		}
	}
}
