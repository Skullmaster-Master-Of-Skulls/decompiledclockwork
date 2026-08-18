using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000009 RID: 9
	public class WindowsNameTransform : INameTransform
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00002CC4 File Offset: 0x00001CC4
		public WindowsNameTransform(string baseDirectory)
		{
			if (baseDirectory == null)
			{
				throw new ArgumentNullException("baseDirectory", "Directory name is invalid");
			}
			this.BaseDirectory = baseDirectory;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002CEE File Offset: 0x00001CEE
		public WindowsNameTransform()
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002CFE File Offset: 0x00001CFE
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002D06 File Offset: 0x00001D06
		public string BaseDirectory
		{
			get
			{
				return this._baseDirectory;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._baseDirectory = Path.GetFullPath(value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002D22 File Offset: 0x00001D22
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002D2A File Offset: 0x00001D2A
		public bool TrimIncomingPaths
		{
			get
			{
				return this._trimIncomingPaths;
			}
			set
			{
				this._trimIncomingPaths = value;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D34 File Offset: 0x00001D34
		public string TransformDirectory(string name)
		{
			name = this.TransformFile(name);
			if (name.Length > 0)
			{
				while (name.EndsWith(Path.DirectorySeparatorChar.ToString()))
				{
					name = name.Remove(name.Length - 1, 1);
				}
				return name;
			}
			throw new ZipException("Cannot have an empty directory name");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D8C File Offset: 0x00001D8C
		public string TransformFile(string name)
		{
			if (name != null)
			{
				name = WindowsNameTransform.MakeValidName(name, this._replacementChar);
				if (this._trimIncomingPaths)
				{
					name = Path.GetFileName(name);
				}
				if (this._baseDirectory != null)
				{
					name = Path.Combine(this._baseDirectory, name);
				}
			}
			else
			{
				name = string.Empty;
			}
			return name;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002DDC File Offset: 0x00001DDC
		public static bool IsValidName(string name)
		{
			return name != null && name.Length <= 260 && string.Compare(name, WindowsNameTransform.MakeValidName(name, '_')) == 0;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002E10 File Offset: 0x00001E10
		static WindowsNameTransform()
		{
			char[] invalidPathChars = Path.GetInvalidPathChars();
			int num = invalidPathChars.Length + 3;
			WindowsNameTransform.InvalidEntryChars = new char[num];
			Array.Copy(invalidPathChars, 0, WindowsNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
			WindowsNameTransform.InvalidEntryChars[num - 1] = '*';
			WindowsNameTransform.InvalidEntryChars[num - 2] = '?';
			WindowsNameTransform.InvalidEntryChars[num - 3] = ':';
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002E68 File Offset: 0x00001E68
		public static string MakeValidName(string name, char replacement)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			name = WindowsPathUtils.DropPathRoot(name.Replace("/", Path.DirectorySeparatorChar.ToString()));
			while (name.Length > 0)
			{
				if (name[0] != Path.DirectorySeparatorChar)
				{
					break;
				}
				name = name.Remove(0, 1);
			}
			while (name.Length > 0 && name[name.Length - 1] == Path.DirectorySeparatorChar)
			{
				name = name.Remove(name.Length - 1, 1);
			}
			int i;
			for (i = name.IndexOf(string.Format("{0}{0}", Path.DirectorySeparatorChar)); i >= 0; i = name.IndexOf(Path.DirectorySeparatorChar))
			{
				name = name.Remove(i, 1);
			}
			i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars);
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
						i = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars, i + 1);
					}
				}
				name = stringBuilder.ToString();
			}
			if (name.Length > 260)
			{
				throw new PathTooLongException();
			}
			return name;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002F8A File Offset: 0x00001F8A
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002F94 File Offset: 0x00001F94
		public char Replacement
		{
			get
			{
				return this._replacementChar;
			}
			set
			{
				for (int i = 0; i < WindowsNameTransform.InvalidEntryChars.Length; i++)
				{
					if (WindowsNameTransform.InvalidEntryChars[i] == value)
					{
						throw new ArgumentException("invalid path character");
					}
				}
				if (value == Path.DirectorySeparatorChar || value == Path.AltDirectorySeparatorChar)
				{
					throw new ArgumentException("invalid replacement character");
				}
				this._replacementChar = value;
			}
		}

		// Token: 0x0400001A RID: 26
		private const int MaxPath = 260;

		// Token: 0x0400001B RID: 27
		private string _baseDirectory;

		// Token: 0x0400001C RID: 28
		private bool _trimIncomingPaths;

		// Token: 0x0400001D RID: 29
		private char _replacementChar = '_';

		// Token: 0x0400001E RID: 30
		private static readonly char[] InvalidEntryChars;
	}
}
