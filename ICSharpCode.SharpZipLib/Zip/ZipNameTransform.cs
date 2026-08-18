using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200006A RID: 106
	public class ZipNameTransform : INameTransform
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x000168B8 File Offset: 0x000158B8
		public ZipNameTransform()
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000168C0 File Offset: 0x000158C0
		public ZipNameTransform(string trimPrefix)
		{
			this.TrimPrefix = trimPrefix;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000168D0 File Offset: 0x000158D0
		static ZipNameTransform()
		{
			char[] invalidPathChars = Path.GetInvalidPathChars();
			int num = invalidPathChars.Length + 2;
			ZipNameTransform.InvalidEntryCharsRelaxed = new char[num];
			Array.Copy(invalidPathChars, 0, ZipNameTransform.InvalidEntryCharsRelaxed, 0, invalidPathChars.Length);
			ZipNameTransform.InvalidEntryCharsRelaxed[num - 1] = '*';
			ZipNameTransform.InvalidEntryCharsRelaxed[num - 2] = '?';
			num = invalidPathChars.Length + 4;
			ZipNameTransform.InvalidEntryChars = new char[num];
			Array.Copy(invalidPathChars, 0, ZipNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
			ZipNameTransform.InvalidEntryChars[num - 1] = ':';
			ZipNameTransform.InvalidEntryChars[num - 2] = '\\';
			ZipNameTransform.InvalidEntryChars[num - 3] = '*';
			ZipNameTransform.InvalidEntryChars[num - 4] = '?';
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00016967 File Offset: 0x00015967
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			if (name.Length > 0)
			{
				if (!name.EndsWith("/"))
				{
					name += "/";
				}
				return name;
			}
			throw new ZipException("Cannot have an empty directory name");
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000169A4 File Offset: 0x000159A4
		public string TransformFile(string name)
		{
			if (name != null)
			{
				string text = name.ToLower();
				if (this.trimPrefix_ != null && text.IndexOf(this.trimPrefix_) == 0)
				{
					name = name.Substring(this.trimPrefix_.Length);
				}
				name = name.Replace("\\", "/");
				name = WindowsPathUtils.DropPathRoot(name);
				while (name.Length > 0)
				{
					if (name[0] != '/')
					{
						break;
					}
					name = name.Remove(0, 1);
				}
				while (name.Length > 0 && name[name.Length - 1] == '/')
				{
					name = name.Remove(name.Length - 1, 1);
				}
				for (int i = name.IndexOf("//"); i >= 0; i = name.IndexOf("//"))
				{
					name = name.Remove(i, 1);
				}
				name = ZipNameTransform.MakeValidName(name, '_');
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00016A8B File Offset: 0x00015A8B
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00016A93 File Offset: 0x00015A93
		public string TrimPrefix
		{
			get
			{
				return this.trimPrefix_;
			}
			set
			{
				this.trimPrefix_ = value;
				if (this.trimPrefix_ != null)
				{
					this.trimPrefix_ = this.trimPrefix_.ToLower();
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00016AB8 File Offset: 0x00015AB8
		private static string MakeValidName(string name, char replacement)
		{
			int i = name.IndexOfAny(ZipNameTransform.InvalidEntryChars);
			if (i >= 0)
			{
				StringBuilder stringBuilder = new StringBuilder(name);
				while (i >= 0)
				{
					stringBuilder[i] = replacement;
					if (i >= name.Length)
					{
						i = -1;
					}
					else
					{
						i = name.IndexOfAny(ZipNameTransform.InvalidEntryChars, i + 1);
					}
				}
				name = stringBuilder.ToString();
			}
			if (name.Length > 65535)
			{
				throw new PathTooLongException();
			}
			return name;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00016B24 File Offset: 0x00015B24
		public static bool IsValidName(string name, bool relaxed)
		{
			bool flag = name != null;
			if (flag)
			{
				if (relaxed)
				{
					flag = (name.IndexOfAny(ZipNameTransform.InvalidEntryCharsRelaxed) < 0);
				}
				else
				{
					flag = (name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0);
				}
			}
			return flag;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00016B74 File Offset: 0x00015B74
		public static bool IsValidName(string name)
		{
			return name != null && name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
		}

		// Token: 0x040002DF RID: 735
		private string trimPrefix_;

		// Token: 0x040002E0 RID: 736
		private static readonly char[] InvalidEntryChars;

		// Token: 0x040002E1 RID: 737
		private static readonly char[] InvalidEntryCharsRelaxed;
	}
}
