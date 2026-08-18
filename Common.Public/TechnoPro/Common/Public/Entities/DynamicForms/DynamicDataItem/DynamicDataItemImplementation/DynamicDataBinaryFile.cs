using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x02000393 RID: 915
	public class DynamicDataBinaryFile
	{
		// Token: 0x06001C1D RID: 7197 RVA: 0x0001FF78 File Offset: 0x0001E178
		public void Deserialize(byte[] fileBytes)
		{
			try
			{
				int num = 6;
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = fileBytes[i];
				}
				string @string = new UTF8Encoding().GetString(array);
				int num2 = int.Parse(@string);
				byte[] array2 = new byte[num2];
				for (int j = 0; j < num2; j++)
				{
					array2[j] = fileBytes[j + num];
				}
				string string2 = new UTF8Encoding().GetString(array2);
				StringDictionary stringDictionary = DynamicDataBinaryFile.ParseArgs(string2, ';');
				string text = stringDictionary["filename"];
				int num3 = fileBytes.Length - num - num2;
				byte[] array3 = new byte[num3];
				for (int k = 0; k < array3.Length; k++)
				{
					array3[k] = fileBytes[k + num2 + num];
				}
				this.Data = array3;
				this.FileName = (text ?? "");
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00020080 File Offset: 0x0001E280
		public byte[] Serialize()
		{
			bool flag = this.FileName == null;
			if (flag)
			{
				this.FileName = "";
			}
			byte[] data = this.Data;
			int num = data.Length;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("filename=");
			stringBuilder.Append(Path.GetFileName(this.FileName));
			stringBuilder.Append(";");
			stringBuilder.Append("filesize=");
			stringBuilder.Append(num.ToString());
			stringBuilder.Append(";");
			string s = stringBuilder.ToString();
			byte[] bytes = new UTF8Encoding().GetBytes(s);
			string text = bytes.Length.ToString();
			int num2 = 6 - text.Length;
			bool flag2 = num2 > 0 && num2 < 7;
			if (flag2)
			{
				text = new string('0', num2) + text;
			}
			byte[] bytes2 = new UTF8Encoding().GetBytes(text);
			int num3 = bytes.Length + bytes2.Length + data.Length;
			byte[] array = new byte[num3];
			bytes2.CopyTo(array, 0);
			bytes.CopyTo(array, bytes2.Length);
			data.CopyTo(array, bytes2.Length + bytes.Length);
			return array;
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000201B6 File Offset: 0x0001E3B6
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x000201BE File Offset: 0x0001E3BE
		public byte[] Data { get; set; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000201C7 File Offset: 0x0001E3C7
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x000201CF File Offset: 0x0001E3CF
		public string FileName { get; set; }

		// Token: 0x06001C23 RID: 7203 RVA: 0x000201D8 File Offset: 0x0001E3D8
		private static StringDictionary ParseArgs(string args, char delimiter)
		{
			return DynamicDataBinaryFile.ParseArgs(args, new char[]
			{
				delimiter
			});
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000201FC File Offset: 0x0001E3FC
		private static StringDictionary ParseArgs(string args, char[] delimiter)
		{
			string[] array = args.Split(delimiter);
			StringDictionary stringDictionary = new StringDictionary();
			foreach (string text in array)
			{
				bool flag = text.Trim().Length > 0;
				if (flag)
				{
					int num = text.IndexOf('=');
					bool flag2 = num > 0;
					if (flag2)
					{
						stringDictionary.Add(text.Substring(0, num), text.Substring(num + 1));
					}
					else
					{
						stringDictionary.Add(text, "");
					}
				}
			}
			return stringDictionary;
		}
	}
}
