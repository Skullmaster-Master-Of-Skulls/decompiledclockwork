using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004D7 RID: 1239
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class FileVersionInfo
	{
		// Token: 0x06002EBE RID: 11966 RVA: 0x000D2520 File Offset: 0x000D0720
		private FileVersionInfo(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000D252F File Offset: 0x000D072F
		public string Comments
		{
			get
			{
				return this.comments;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x000D2537 File Offset: 0x000D0737
		public string CompanyName
		{
			get
			{
				return this.companyName;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000D253F File Offset: 0x000D073F
		public int FileBuildPart
		{
			get
			{
				return this.fileBuild;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000D2547 File Offset: 0x000D0747
		public string FileDescription
		{
			get
			{
				return this.fileDescription;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000D254F File Offset: 0x000D074F
		public int FileMajorPart
		{
			get
			{
				return this.fileMajor;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000D2557 File Offset: 0x000D0757
		public int FileMinorPart
		{
			get
			{
				return this.fileMinor;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000D255F File Offset: 0x000D075F
		public string FileName
		{
			get
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.fileName).Demand();
				return this.fileName;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002EC6 RID: 11974 RVA: 0x000D2578 File Offset: 0x000D0778
		public int FilePrivatePart
		{
			get
			{
				return this.filePrivate;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000D2580 File Offset: 0x000D0780
		public string FileVersion
		{
			get
			{
				return this.fileVersion;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x000D2588 File Offset: 0x000D0788
		public string InternalName
		{
			get
			{
				return this.internalName;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x000D2590 File Offset: 0x000D0790
		public bool IsDebug
		{
			get
			{
				return (this.fileFlags & 1) != 0;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000D259D File Offset: 0x000D079D
		public bool IsPatched
		{
			get
			{
				return (this.fileFlags & 4) != 0;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000D25AA File Offset: 0x000D07AA
		public bool IsPrivateBuild
		{
			get
			{
				return (this.fileFlags & 8) != 0;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002ECC RID: 11980 RVA: 0x000D25B7 File Offset: 0x000D07B7
		public bool IsPreRelease
		{
			get
			{
				return (this.fileFlags & 2) != 0;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000D25C4 File Offset: 0x000D07C4
		public bool IsSpecialBuild
		{
			get
			{
				return (this.fileFlags & 32) != 0;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x000D25D2 File Offset: 0x000D07D2
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000D25DA File Offset: 0x000D07DA
		public string LegalCopyright
		{
			get
			{
				return this.legalCopyright;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x000D25E2 File Offset: 0x000D07E2
		public string LegalTrademarks
		{
			get
			{
				return this.legalTrademarks;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000D25EA File Offset: 0x000D07EA
		public string OriginalFilename
		{
			get
			{
				return this.originalFilename;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000D25F2 File Offset: 0x000D07F2
		public string PrivateBuild
		{
			get
			{
				return this.privateBuild;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000D25FA File Offset: 0x000D07FA
		public int ProductBuildPart
		{
			get
			{
				return this.productBuild;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x000D2602 File Offset: 0x000D0802
		public int ProductMajorPart
		{
			get
			{
				return this.productMajor;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000D260A File Offset: 0x000D080A
		public int ProductMinorPart
		{
			get
			{
				return this.productMinor;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x000D2612 File Offset: 0x000D0812
		public string ProductName
		{
			get
			{
				return this.productName;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000D261A File Offset: 0x000D081A
		public int ProductPrivatePart
		{
			get
			{
				return this.productPrivate;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x000D2622 File Offset: 0x000D0822
		public string ProductVersion
		{
			get
			{
				return this.productVersion;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000D262A File Offset: 0x000D082A
		public string SpecialBuild
		{
			get
			{
				return this.specialBuild;
			}
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000D2634 File Offset: 0x000D0834
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

		// Token: 0x06002EDB RID: 11995 RVA: 0x000D2694 File Offset: 0x000D0894
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

		// Token: 0x06002EDC RID: 11996 RVA: 0x000D26D4 File Offset: 0x000D08D4
		private static string GetFileVersionLanguage(IntPtr memPtr)
		{
			int langID = FileVersionInfo.GetVarEntry(memPtr) >> 16;
			StringBuilder stringBuilder = new StringBuilder(256);
			UnsafeNativeMethods.VerLanguageName(langID, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000D270C File Offset: 0x000D090C
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

		// Token: 0x06002EDE RID: 11998 RVA: 0x000D2750 File Offset: 0x000D0950
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

		// Token: 0x06002EDF RID: 11999 RVA: 0x000D27A0 File Offset: 0x000D09A0
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

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000D2A56 File Offset: 0x000D0C56
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string GetFullPathWithAssert(string fileName)
		{
			return Path.GetFullPath(fileName);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000D2A60 File Offset: 0x000D0C60
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
				byte[] array2;
				byte* value;
				if ((array2 = array) == null || array2.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array2[0];
				}
				IntPtr intPtr = new IntPtr((void*)value);
				if (UnsafeNativeMethods.GetFileVersionInfo(fileName, 0, fileVersionInfoSize, new HandleRef(null, intPtr)))
				{
					int varEntry = FileVersionInfo.GetVarEntry(intPtr);
					if (!fileVersionInfo.GetVersionInfoForCodePage(intPtr, FileVersionInfo.ConvertTo8DigitHex(varEntry)))
					{
						int[] array3 = new int[]
						{
							67699888,
							67699940,
							67698688
						};
						foreach (int num2 in array3)
						{
							if (num2 != varEntry && fileVersionInfo.GetVersionInfoForCodePage(intPtr, FileVersionInfo.ConvertTo8DigitHex(num2)))
							{
								break;
							}
						}
					}
				}
				array2 = null;
			}
			return fileVersionInfo;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000D2B4C File Offset: 0x000D0D4C
		private static int HIWORD(int dword)
		{
			return NativeMethods.Util.HIWORD(dword);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000D2B54 File Offset: 0x000D0D54
		private static int LOWORD(int dword)
		{
			return NativeMethods.Util.LOWORD(dword);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000D2B5C File Offset: 0x000D0D5C
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

		// Token: 0x0400278D RID: 10125
		private string fileName;

		// Token: 0x0400278E RID: 10126
		private string companyName;

		// Token: 0x0400278F RID: 10127
		private string fileDescription;

		// Token: 0x04002790 RID: 10128
		private string fileVersion;

		// Token: 0x04002791 RID: 10129
		private string internalName;

		// Token: 0x04002792 RID: 10130
		private string legalCopyright;

		// Token: 0x04002793 RID: 10131
		private string originalFilename;

		// Token: 0x04002794 RID: 10132
		private string productName;

		// Token: 0x04002795 RID: 10133
		private string productVersion;

		// Token: 0x04002796 RID: 10134
		private string comments;

		// Token: 0x04002797 RID: 10135
		private string legalTrademarks;

		// Token: 0x04002798 RID: 10136
		private string privateBuild;

		// Token: 0x04002799 RID: 10137
		private string specialBuild;

		// Token: 0x0400279A RID: 10138
		private string language;

		// Token: 0x0400279B RID: 10139
		private int fileMajor;

		// Token: 0x0400279C RID: 10140
		private int fileMinor;

		// Token: 0x0400279D RID: 10141
		private int fileBuild;

		// Token: 0x0400279E RID: 10142
		private int filePrivate;

		// Token: 0x0400279F RID: 10143
		private int productMajor;

		// Token: 0x040027A0 RID: 10144
		private int productMinor;

		// Token: 0x040027A1 RID: 10145
		private int productBuild;

		// Token: 0x040027A2 RID: 10146
		private int productPrivate;

		// Token: 0x040027A3 RID: 10147
		private int fileFlags;
	}
}
