using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Resources
{
	// Token: 0x020000EF RID: 239
	[TypeConverter(typeof(ResXFileRef.Converter))]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[Serializable]
	public class ResXFileRef
	{
		// Token: 0x06000362 RID: 866 RVA: 0x0000A569 File Offset: 0x00008769
		public ResXFileRef(string fileName, string typeName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.fileName = fileName;
			this.typeName = typeName;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A59B File Offset: 0x0000879B
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.textFileEncoding = null;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000072B6 File Offset: 0x000054B6
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public ResXFileRef(string fileName, string typeName, Encoding textFileEncoding) : this(fileName, typeName)
		{
			this.textFileEncoding = textFileEncoding;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A5B5 File Offset: 0x000087B5
		internal ResXFileRef Clone()
		{
			return new ResXFileRef(this.fileName, this.typeName, this.textFileEncoding);
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000A5CE File Offset: 0x000087CE
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000A5D6 File Offset: 0x000087D6
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000A5DE File Offset: 0x000087DE
		public Encoding TextFileEncoding
		{
			get
			{
				return this.textFileEncoding;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A5E8 File Offset: 0x000087E8
		private static string PathDifference(string path1, string path2, bool compareCase)
		{
			int num = -1;
			int i = 0;
			while (i < path1.Length && i < path2.Length && (path1[i] == path2[i] || (!compareCase && char.ToLower(path1[i], CultureInfo.InvariantCulture) == char.ToLower(path2[i], CultureInfo.InvariantCulture))))
			{
				if (path1[i] == Path.DirectorySeparatorChar)
				{
					num = i;
				}
				i++;
			}
			if (i == 0)
			{
				return path2;
			}
			if (i == path1.Length && i == path2.Length)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (i < path1.Length)
			{
				if (path1[i] == Path.DirectorySeparatorChar)
				{
					stringBuilder.Append(".." + Path.DirectorySeparatorChar.ToString());
				}
				i++;
			}
			return stringBuilder.ToString() + path2.Substring(num + 1);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A6CA File Offset: 0x000088CA
		internal void MakeFilePathRelative(string basePath)
		{
			if (basePath == null || basePath.Length == 0)
			{
				return;
			}
			this.fileName = ResXFileRef.PathDifference(basePath, this.fileName, false);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A6EC File Offset: 0x000088EC
		public override string ToString()
		{
			string text = "";
			if (this.fileName.IndexOf(";") != -1 || this.fileName.IndexOf("\"") != -1)
			{
				text = text + "\"" + this.fileName + "\";";
			}
			else
			{
				text = text + this.fileName + ";";
			}
			text += this.typeName;
			if (this.textFileEncoding != null)
			{
				text = text + ";" + this.textFileEncoding.WebName;
			}
			return text;
		}

		// Token: 0x040003D2 RID: 978
		private string fileName;

		// Token: 0x040003D3 RID: 979
		private string typeName;

		// Token: 0x040003D4 RID: 980
		[OptionalField(VersionAdded = 2)]
		private Encoding textFileEncoding;

		// Token: 0x02000543 RID: 1347
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public class Converter : TypeConverter
		{
			// Token: 0x06005560 RID: 21856 RVA: 0x00166126 File Offset: 0x00164326
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string);
			}

			// Token: 0x06005561 RID: 21857 RVA: 0x00166126 File Offset: 0x00164326
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return destinationType == typeof(string);
			}

			// Token: 0x06005562 RID: 21858 RVA: 0x00166140 File Offset: 0x00164340
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				object result = null;
				if (destinationType == typeof(string))
				{
					result = ((ResXFileRef)value).ToString();
				}
				return result;
			}

			// Token: 0x06005563 RID: 21859 RVA: 0x00166170 File Offset: 0x00164370
			internal static string[] ParseResxFileRefString(string stringValue)
			{
				string[] result = null;
				if (stringValue != null)
				{
					stringValue = stringValue.Trim();
					string text;
					string text2;
					if (stringValue.StartsWith("\""))
					{
						int num = stringValue.LastIndexOf("\"");
						if (num - 1 < 0)
						{
							throw new ArgumentException("value");
						}
						text = stringValue.Substring(1, num - 1);
						if (num + 2 > stringValue.Length)
						{
							throw new ArgumentException("value");
						}
						text2 = stringValue.Substring(num + 2);
					}
					else
					{
						int num2 = stringValue.IndexOf(";");
						if (num2 == -1)
						{
							throw new ArgumentException("value");
						}
						text = stringValue.Substring(0, num2);
						if (num2 + 1 > stringValue.Length)
						{
							throw new ArgumentException("value");
						}
						text2 = stringValue.Substring(num2 + 1);
					}
					string[] array = text2.Split(new char[]
					{
						';'
					});
					if (array.Length > 1)
					{
						result = new string[]
						{
							text,
							array[0],
							array[1]
						};
					}
					else if (array.Length != 0)
					{
						result = new string[]
						{
							text,
							array[0]
						};
					}
					else
					{
						result = new string[]
						{
							text
						};
					}
				}
				return result;
			}

			// Token: 0x06005564 RID: 21860 RVA: 0x00166288 File Offset: 0x00164488
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				object result = null;
				string text = value as string;
				if (text != null)
				{
					string[] array = ResXFileRef.Converter.ParseResxFileRefString(text);
					string text2 = array[0];
					Type type = Type.GetType(array[1], true);
					if (type.Equals(typeof(string)))
					{
						Encoding encoding = Encoding.Default;
						if (array.Length > 2)
						{
							encoding = Encoding.GetEncoding(array[2]);
						}
						using (StreamReader streamReader = new StreamReader(text2, encoding))
						{
							return streamReader.ReadToEnd();
						}
					}
					byte[] array2 = null;
					using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						array2 = new byte[fileStream.Length];
						fileStream.Read(array2, 0, (int)fileStream.Length);
					}
					if (type.Equals(typeof(byte[])))
					{
						result = array2;
					}
					else
					{
						MemoryStream memoryStream = new MemoryStream(array2);
						if (type.Equals(typeof(MemoryStream)))
						{
							return memoryStream;
						}
						if (type.Equals(typeof(Bitmap)) && text2.EndsWith(".ico"))
						{
							Icon icon = new Icon(memoryStream);
							result = icon.ToBitmap();
						}
						else
						{
							result = Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, new object[]
							{
								memoryStream
							}, null);
						}
					}
				}
				return result;
			}
		}
	}
}
