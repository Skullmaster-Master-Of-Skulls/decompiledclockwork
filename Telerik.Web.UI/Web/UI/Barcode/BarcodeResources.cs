using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x02000096 RID: 150
	public static class BarcodeResources
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0000DE20 File Offset: 0x0000C020
		internal static string GetAllValues(string encodedValuesPath)
		{
			string name = string.Empty;
			string result = string.Empty;
			string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
			for (int i = 0; i < manifestResourceNames.Length; i++)
			{
				if (manifestResourceNames[i].Contains(encodedValuesPath))
				{
					name = manifestResourceNames[i];
				}
			}
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
			using (StreamReader streamReader = new StreamReader(manifestResourceStream))
			{
				result = streamReader.ReadToEnd();
			}
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
		internal static List<string> GetEncodedValues(string encodedValuesPath, int validStep)
		{
			List<string> list = new List<string>();
			string allValues = BarcodeResources.GetAllValues(encodedValuesPath);
			int i = 0;
			while (i < allValues.Length)
			{
				char c = allValues[i];
				if (c != '"' && c != ' ' && c != ',')
				{
					string text = string.Empty;
					for (int j = 0; j < validStep; j++)
					{
						text += allValues[i + j].ToString();
					}
					list.Add(text);
					i += validStep;
				}
				else
				{
					i++;
				}
			}
			return list;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000DF24 File Offset: 0x0000C124
		internal static List<int> GetCSValues(string encodedValuesPath)
		{
			List<int> list = new List<int>();
			string allValues = BarcodeResources.GetAllValues(encodedValuesPath);
			string text = string.Empty;
			foreach (char c in allValues)
			{
				if (c != ' ' && c != ',')
				{
					text += c;
				}
				else if (text.Length > 0)
				{
					int item = int.Parse(text);
					list.Add(item);
					text = string.Empty;
				}
			}
			if (text.Length > 0)
			{
				int item2 = int.Parse(text);
				list.Add(item2);
				text = string.Empty;
			}
			return list;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000DFBC File Offset: 0x0000C1BC
		internal static List<List<int>> GetBarSpaceSequence(string encodedValuesPath, int validStep)
		{
			List<int> list = new List<int>();
			List<List<int>> list2 = new List<List<int>>();
			string allValues = BarcodeResources.GetAllValues(encodedValuesPath);
			int i = 0;
			while (i < allValues.Length)
			{
				char c = allValues[i];
				if (c != '"' && c != ' ' && c != ',' && c != '\r' && c != '\n')
				{
					string text = string.Empty;
					for (int j = 0; j < validStep; j++)
					{
						text += allValues[i + j].ToString();
					}
					list.Add(int.Parse(text));
					i += validStep;
				}
				else
				{
					i++;
				}
				if (list.Count == 3)
				{
					list2.Add(new List<int>(list));
					list.Clear();
				}
			}
			return list2;
		}
	}
}
