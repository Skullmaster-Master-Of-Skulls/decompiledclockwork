using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005BE RID: 1470
	public static class DynamicDataAdapter
	{
		// Token: 0x06002F6F RID: 12143 RVA: 0x00035B68 File Offset: 0x00033D68
		public static T GetDynamicFileDescriptionFromMetadata<T>(this string metaData, int dataId, int controlId) where T : DynamicFileDescription
		{
			bool flag = string.IsNullOrEmpty(metaData);
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				try
				{
					XDocument xdocument = XDocument.Parse(metaData);
					return xdocument.Descendants("file").Select(delegate(XElement g)
					{
						T t = Activator.CreateInstance<T>();
						t.DataId = dataId;
						t.ControlId = controlId;
						DynamicFileDescription dynamicFileDescription = t;
						XAttribute xattribute = g.Attribute("id");
						dynamicFileDescription.FileId = ((xattribute != null) ? xattribute.GetIntFromAttribute(0) : 0);
						DynamicFileDescription dynamicFileDescription2 = t;
						XAttribute xattribute2 = g.Attribute("fn");
						dynamicFileDescription2.Filename = ((xattribute2 != null) ? xattribute2.GetStringFromAttribute() : null);
						return t;
					}).FirstOrDefault<T>();
				}
				catch
				{
				}
				result = default(T);
			}
			return result;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x00035BF8 File Offset: 0x00033DF8
		public static byte[] ParseSingleFileBytes(this byte[] fileBytes, out string filename)
		{
			try
			{
				byte[] array = new byte[6];
				for (int i = 0; i < 6; i++)
				{
					array[i] = fileBytes[i];
				}
				string s = array.UnencryptedBytesToString();
				int num = int.Parse(s);
				byte[] array2 = new byte[num];
				for (int j = 0; j < num; j++)
				{
					array2[j] = fileBytes[j + 6];
				}
				string args = array2.UnencryptedBytesToString();
				IDictionary<string, string> dictionary = DynamicDataAdapter.ParseArgs(args, new char[]
				{
					';'
				});
				string text = dictionary["filename"];
				int num2 = fileBytes.Length - 6 - num;
				byte[] array3 = new byte[num2];
				for (int k = 0; k < array3.Length; k++)
				{
					array3[k] = fileBytes[k + num + 6];
				}
				filename = text;
				return array3;
			}
			catch (Exception ex)
			{
			}
			filename = null;
			return null;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x00035CF0 File Offset: 0x00033EF0
		private static IDictionary<string, string> ParseArgs(string args, char[] delimiter)
		{
			string[] array = args.Split(delimiter);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in array)
			{
				bool flag = text.Trim().Length > 0;
				if (flag)
				{
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						dictionary.Add(text.Substring(0, num), text.Substring(num + 1));
					}
					else
					{
						dictionary.Add(text, "");
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x00035D84 File Offset: 0x00033F84
		public static string UnencryptedBytesToString(this byte[] bytes)
		{
			bool flag = bytes == null || bytes.Length < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetString(bytes);
			}
			return result;
		}
	}
}
