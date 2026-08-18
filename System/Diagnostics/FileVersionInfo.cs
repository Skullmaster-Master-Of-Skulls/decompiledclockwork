using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200075D RID: 1885
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class FileVersionInfo
	{
		// Token: 0x060039DE RID: 14814 RVA: 0x000F5030 File Offset: 0x000F4030
		private FileVersionInfo(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000F503F File Offset: 0x000F403F
		public string Comments
		{
			get
			{
				return this.comments;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x060039E0 RID: 14816 RVA: 0x000F5047 File Offset: 0x000F4047
		public string CompanyName
		{
			get
			{
				return this.companyName;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000F504F File Offset: 0x000F404F
		public int FileBuildPart
		{
			get
			{
				return this.fileBuild;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000F5057 File Offset: 0x000F4057
		public string FileDescription
		{
			get
			{
				return this.fileDescription;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000F505F File Offset: 0x000F405F
		public int FileMajorPart
		{
			get
			{
				return this.fileMajor;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000F5067 File Offset: 0x000F4067
		public int FileMinorPart
		{
			get
			{
				return this.fileMinor;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000F506F File Offset: 0x000F406F
		public string FileName
		{
			get
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
				return this.fileName;
			}
		}

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060039E6 RID: 14822 RVA: 0x000F5088 File Offset: 0x000F4088
		public int FilePrivatePart
		{
			get
			{
				return this.filePrivate;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000F5090 File Offset: 0x000F4090
		public string FileVersion
		{
			get
			{
				return this.fileVersion;
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000F5098 File Offset: 0x000F4098
		public string InternalName
		{
			get
			{
				return this.internalName;
			}
		}

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000F50A0 File Offset: 0x000F40A0
		public bool IsDebug
		{
			get
			{
				return (this.fileFlags & 1) != 0;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x000F50B0 File Offset: 0x000F40B0
		public bool IsPatched
		{
			get
			{
				return (this.fileFlags & 4) != 0;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000F50C0 File Offset: 0x000F40C0
		public bool IsPrivateBuild
		{
			get
			{
				return (this.fileFlags & 8) != 0;
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000F50D0 File Offset: 0x000F40D0
		public bool IsPreRelease
		{
			get
			{
				return (this.fileFlags & 2) != 0;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000F50E0 File Offset: 0x000F40E0
		public bool IsSpecialBuild
		{
			get
			{
				return (this.fileFlags & 32) != 0;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000F50F1 File Offset: 0x000F40F1
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000F50F9 File Offset: 0x000F40F9
		public string LegalCopyright
		{
			get
			{
				return this.legalCopyright;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000F5101 File Offset: 0x000F4101
		public string LegalTrademarks
		{
			get
			{
				return this.legalTrademarks;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000F5109 File Offset: 0x000F4109
		public string OriginalFilename
		{
			get
			{
				return this.originalFilename;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000F5111 File Offset: 0x000F4111
		public string PrivateBuild
		{
			get
			{
				return this.privateBuild;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000F5119 File Offset: 0x000F4119
		public int ProductBuildPart
		{
			get
			{
				return this.productBuild;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000F5121 File Offset: 0x000F4121
		public int ProductMajorPart
		{
			get
			{
				return this.productMajor;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000F5129 File Offset: 0x000F4129
		public int ProductMinorPart
		{
			get
			{
				return this.productMinor;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060039F6 RID: 14838 RVA: 0x000F5131 File Offset: 0x000F4131
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000F5139 File Offset: 0x000F4139
		public int ProductPrivatePart
		{
			get
			{
				return this.productPrivate;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x000F5141 File Offset: 0x000F4141
		public string ProductVersion
		{
			get
			{
				return this.productVersion;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x000F5149 File Offset: 0x000F4149
		public string SpecialBuild
		{
			get
			{
				return this.specialBuild;
			}
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000F5154 File Offset: 0x000F4154
		private static string ConvertTo8DigitHex(int value)
		{
			string text = Convert.ToString(value, 16);
			text = text.ToUpper(CultureInfo.InvariantCulture);
			if (text.Length == 8)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(8);
			for (int i = text.Length; i < 8; i++)
			{
				stringBuilder.Append("0");
			}
			stringBuilder.Append(text);
			return stringBuilder.ToString();
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000F51B4 File Offset: 0x000F41B4
		private static NativeMethods.VS_FIXEDFILEINFO GetFixedFileInfo(IntPtr memPtr)
		{
			IntPtr zero = IntPtr.Zero;
			int num;
			if (UnsafeNativeMethods.VerQueryValue(new HandleRef(null, memPtr), "\\", ref zero, out num))
			{
				NativeMethods.VS_FIXEDFILEINFO vs_FIXEDFILEINFO = new NativeMethods.VS_FIXEDFILEINFO();
				Marshal.PtrToStructure(zero, vs_FIXEDFILEINFO);
				return vs_FIXEDFILEINFO;
			}
			return new NativeMethods.VS_FIXEDFILEINFO();
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000F51F4 File Offset: 0x000F41F4
		private static string GetFileVersionLanguage(IntPtr memPtr)
		{
			int langID = FileVersionInfo.GetVarEntry(memPtr) >> 16;
			StringBuilder stringBuilder = new StringBuilder(256);
			UnsafeNativeMethods.VerLanguageName(langID, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x000F522C File Offset: 0x000F422C
		private static string GetFileVersionString(IntPtr memPtr, string name)
		{
			string result = "";
			IntPtr zero = IntPtr.Zero;
			int num;
			if (UnsafeNativeMethods.VerQueryValue(new HandleRef(null, memPtr), name, ref zero, out num) && zero != IntPtr.Zero)
			{
				result = Marshal.PtrToStringAuto(zero);
			}
			return result;
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x000F5270 File Offset: 0x000F4270
		private static int GetVarEntry(IntPtr memPtr)
		{
			IntPtr zero = IntPtr.Zero;
			int num;
			if (UnsafeNativeMethods.VerQueryValue(new HandleRef(null, memPtr), "\\VarFileInfo\\Translation", ref zero, out num))
			{
				return ((int)Marshal.ReadInt16(zero) << 16) + (int)Marshal.ReadInt16((IntPtr)((long)zero + 2L));
			}
			return 67699940;
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000F52C0 File Offset: 0x000F42C0
		private bool GetVersionInfoForCodePage(IntPtr memIntPtr, string codepage)
		{
			string format = "\\\\StringFileInfo\\\\{0}\\\\{1}";
			this.companyName = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"CompanyName"
			}));
			this.fileDescription = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"FileDescription"
			}));
			this.fileVersion = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"FileVersion"
			}));
			this.internalName = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"InternalName"
			}));
			this.legalCopyright = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"LegalCopyright"
			}));
			this.originalFilename = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"OriginalFilename"
			}));
			this.productName = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"ProductName"
			}));
			this.productVersion = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"ProductVersion"
			}));
			this.comments = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"Comments"
			}));
			this.legalTrademarks = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"LegalTrademarks"
			}));
			this.privateBuild = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"PrivateBuild"
			}));
			this.specialBuild = FileVersionInfo.GetFileVersionString(memIntPtr, string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				codepage,
				"SpecialBuild"
			}));
			this.language = FileVersionInfo.GetFileVersionLanguage(memIntPtr);
			NativeMethods.VS_FIXEDFILEINFO fixedFileInfo = FileVersionInfo.GetFixedFileInfo(memIntPtr);
			this.fileMajor = FileVersionInfo.HIWORD(fixedFileInfo.dwFileVersionMS);
			this.fileMinor = FileVersionInfo.LOWORD(fixedFileInfo.dwFileVersionMS);
			this.fileBuild = FileVersionInfo.HIWORD(fixedFileInfo.dwFileVersionLS);
			this.filePrivate = FileVersionInfo.LOWORD(fixedFileInfo.dwFileVersionLS);
			this.productMajor = FileVersionInfo.HIWORD(fixedFileInfo.dwProductVersionMS);
			this.productMinor = FileVersionInfo.LOWORD(fixedFileInfo.dwProductVersionMS);
			this.productBuild = FileVersionInfo.HIWORD(fixedFileInfo.dwProductVersionLS);
			this.productPrivate = FileVersionInfo.LOWORD(fixedFileInfo.dwProductVersionLS);
			this.fileFlags = fixedFileInfo.dwFileFlags;
			return this.fileVersion != string.Empty;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000F55B6 File Offset: 0x000F45B6
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string GetFullPathWithAssert(string fileName)
		{
			return Path.GetFullPath(fileName);
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000F55CC File Offset: 0x000F45CC
		public unsafe static FileVersionInfo GetVersionInfo(string fileName)
		{
			if (!File.Exists(fileName))
			{
				string fullPathWithAssert = FileVersionInfo.GetFullPathWithAssert(fileName);
				new FileIOPermission(FileIOPermissionAccess.Read, fullPathWithAssert).Demand();
				throw new FileNotFoundException(fileName);
			}
			int num;
			int fileVersionInfoSize = UnsafeNativeMethods.GetFileVersionInfoSize(fileName, out num);
			FileVersionInfo fileVersionInfo = new FileVersionInfo(fileName);
			if (fileVersionInfoSize != 0)
			{
				byte[] array = new byte[fileVersionInfoSize];
				fixed (byte* ptr = array)
				{
					IntPtr intPtr = new IntPtr((void*)ptr);
					if (UnsafeNativeMethods.GetFileVersionInfo(fileName, 0, fileVersionInfoSize, new HandleRef(null, intPtr)))
					{
						int varEntry = FileVersionInfo.GetVarEntry(intPtr);
						if (!fileVersionInfo.GetVersionInfoForCodePage(intPtr, FileVersionInfo.ConvertTo8DigitHex(varEntry)))
						{
							int[] array2 = new int[]
							{
								67699888,
								67699940,
								67698688
							};
							foreach (int num2 in array2)
							{
								if (num2 != varEntry && fileVersionInfo.GetVersionInfoForCodePage(intPtr, FileVersionInfo.ConvertTo8DigitHex(num2)))
								{
									break;
								}
							}
						}
					}
				}
			}
			return fileVersionInfo;
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000F56B9 File Offset: 0x000F46B9
		private static int HIWORD(int dword)
		{
			return NativeMethods.Util.HIWORD(dword);
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000F56C1 File Offset: 0x000F46C1
		private static int LOWORD(int dword)
		{
			return NativeMethods.Util.LOWORD(dword);
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x000F56CC File Offset: 0x000F46CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			string value = "\r\n";
			stringBuilder.Append("File:             ");
			stringBuilder.Append(this.FileName);
			stringBuilder.Append(value);
			stringBuilder.Append("InternalName:     ");
			stringBuilder.Append(this.InternalName);
			stringBuilder.Append(value);
			stringBuilder.Append("OriginalFilename: ");
			stringBuilder.Append(this.OriginalFilename);
			stringBuilder.Append(value);
			stringBuilder.Append("FileVersion:      ");
			stringBuilder.Append(this.FileVersion);
			stringBuilder.Append(value);
			stringBuilder.Append("FileDescription:  ");
			stringBuilder.Append(this.FileDescription);
			stringBuilder.Append(value);
			stringBuilder.Append("Product:          ");
			stringBuilder.Append(this.ProductName);
			stringBuilder.Append(value);
			stringBuilder.Append("ProductVersion:   ");
			stringBuilder.Append(this.ProductVersion);
			stringBuilder.Append(value);
			stringBuilder.Append("Debug:            ");
			stringBuilder.Append(this.IsDebug.ToString());
			stringBuilder.Append(value);
			stringBuilder.Append("Patched:          ");
			stringBuilder.Append(this.IsPatched.ToString());
			stringBuilder.Append(value);
			stringBuilder.Append("PreRelease:       ");
			stringBuilder.Append(this.IsPreRelease.ToString());
			stringBuilder.Append(value);
			stringBuilder.Append("PrivateBuild:     ");
			stringBuilder.Append(this.IsPrivateBuild.ToString());
			stringBuilder.Append(value);
			stringBuilder.Append("SpecialBuild:     ");
			stringBuilder.Append(this.IsSpecialBuild.ToString());
			stringBuilder.Append(value);
			stringBuilder.Append("Language:         ");
			stringBuilder.Append(this.Language);
			stringBuilder.Append(value);
			return stringBuilder.ToString();
		}

		// Token: 0x040032E5 RID: 13029
		private string fileName;

		// Token: 0x040032E6 RID: 13030
		private string companyName;

		// Token: 0x040032E7 RID: 13031
		private string fileDescription;

		// Token: 0x040032E8 RID: 13032
		private string fileVersion;

		// Token: 0x040032E9 RID: 13033
		private string internalName;

		// Token: 0x040032EA RID: 13034
		private string legalCopyright;

		// Token: 0x040032EB RID: 13035
		private string originalFilename;

		// Token: 0x040032EC RID: 13036
		private string productName;

		// Token: 0x040032ED RID: 13037
		private string productVersion;

		// Token: 0x040032EE RID: 13038
		private string comments;

		// Token: 0x040032EF RID: 13039
		private string legalTrademarks;

		// Token: 0x040032F0 RID: 13040
		private string privateBuild;

		// Token: 0x040032F1 RID: 13041
		private string specialBuild;

		// Token: 0x040032F2 RID: 13042
		private string language;

		// Token: 0x040032F3 RID: 13043
		private int fileMajor;

		// Token: 0x040032F4 RID: 13044
		private int fileMinor;

		// Token: 0x040032F5 RID: 13045
		private int fileBuild;

		// Token: 0x040032F6 RID: 13046
		private int filePrivate;

		// Token: 0x040032F7 RID: 13047
		private int productMajor;

		// Token: 0x040032F8 RID: 13048
		private int productMinor;

		// Token: 0x040032F9 RID: 13049
		private int productBuild;

		// Token: 0x040032FA RID: 13050
		private int productPrivate;

		// Token: 0x040032FB RID: 13051
		private int fileFlags;
	}
}
