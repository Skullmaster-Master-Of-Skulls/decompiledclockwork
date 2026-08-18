using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using Microsoft.Win32;

namespace System.Configuration
{
	// Token: 0x02000015 RID: 21
	internal class ClientConfigPaths
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00007608 File Offset: 0x00005808
		[FileIOPermission(SecurityAction.Assert, AllFiles = (FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery))]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private ClientConfigPaths(string exePath, bool includeUserConfig)
		{
			this._includesUserConfig = includeUserConfig;
			Assembly assembly = null;
			string applicationFilename = null;
			string text;
			if (exePath == null)
			{
				AppDomain currentDomain = AppDomain.CurrentDomain;
				AppDomainSetup setupInformation = currentDomain.SetupInformation;
				this._applicationConfigUri = setupInformation.ConfigurationFile;
				assembly = Assembly.GetEntryAssembly();
				if (assembly != null)
				{
					this._hasEntryAssembly = true;
					text = assembly.CodeBase;
					bool flag = false;
					if (StringUtil.StartsWithIgnoreCase(text, "file:///"))
					{
						flag = true;
						text = text.Substring("file:///".Length);
					}
					else if (StringUtil.StartsWithIgnoreCase(text, "file://"))
					{
						flag = true;
						text = text.Substring("file:".Length);
					}
					if (flag)
					{
						text = text.Replace('/', '\\');
						applicationFilename = text;
					}
					else
					{
						text = assembly.EscapedCodeBase;
					}
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(260);
					int num = 1;
					int moduleFileName;
					while ((moduleFileName = UnsafeNativeMethods.GetModuleFileName(new HandleRef(null, IntPtr.Zero), stringBuilder, stringBuilder.Capacity)) == stringBuilder.Capacity && Marshal.GetLastWin32Error() == 122 && stringBuilder.Capacity < 32767)
					{
						num += 2;
						int capacity = (num * 260 < 32767) ? (num * 260) : 32767;
						stringBuilder.EnsureCapacity(capacity);
					}
					stringBuilder.Length = moduleFileName;
					text = Path.GetFullPath(stringBuilder.ToString());
					applicationFilename = text;
				}
			}
			else
			{
				text = Path.GetFullPath(exePath);
				if (!FileUtil.FileExists(text, false))
				{
					throw ExceptionUtil.ParameterInvalid("exePath");
				}
				applicationFilename = text;
			}
			if (this._applicationConfigUri == null)
			{
				this._applicationConfigUri = text + ".config";
			}
			this._applicationUri = text;
			if (exePath != null)
			{
				return;
			}
			if (!this._includesUserConfig)
			{
				return;
			}
			bool flag2 = StringUtil.StartsWithIgnoreCase(this._applicationConfigUri, "http://");
			this.SetNamesAndVersion(applicationFilename, assembly, flag2);
			if (this.IsClickOnceDeployed(AppDomain.CurrentDomain))
			{
				string text2 = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
				string path = this.Validate(this._productVersion, false);
				if (Path.IsPathRooted(text2))
				{
					this._localConfigDirectory = this.CombineIfValid(text2, path);
					this._localConfigFilename = this.CombineIfValid(this._localConfigDirectory, "user.config");
					return;
				}
			}
			else if (!flag2)
			{
				string path2 = this.Validate(this._companyName, true);
				string text3 = this.Validate(AppDomain.CurrentDomain.FriendlyName, true);
				string exePath2 = (!string.IsNullOrEmpty(this._applicationUri)) ? this._applicationUri.ToLower(CultureInfo.InvariantCulture) : null;
				string text4 = (!string.IsNullOrEmpty(text3)) ? text3 : this.Validate(this._productName, true);
				string typeAndHashSuffix = this.GetTypeAndHashSuffix(AppDomain.CurrentDomain, exePath2);
				string path3 = (!string.IsNullOrEmpty(text4) && !string.IsNullOrEmpty(typeAndHashSuffix)) ? (text4 + typeAndHashSuffix) : null;
				string path4 = this.Validate(this._productVersion, false);
				string path5 = this.CombineIfValid(this.CombineIfValid(path2, path3), path4);
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				if (Path.IsPathRooted(folderPath))
				{
					this._roamingConfigDirectory = this.CombineIfValid(folderPath, path5);
					this._roamingConfigFilename = this.CombineIfValid(this._roamingConfigDirectory, "user.config");
				}
				string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				if (Path.IsPathRooted(folderPath2))
				{
					this._localConfigDirectory = this.CombineIfValid(folderPath2, path5);
					this._localConfigFilename = this.CombineIfValid(this._localConfigDirectory, "user.config");
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007960 File Offset: 0x00005B60
		internal static ClientConfigPaths GetPaths(string exePath, bool includeUserConfig)
		{
			ClientConfigPaths result;
			if (exePath == null)
			{
				if (ClientConfigPaths.s_current == null || (includeUserConfig && !ClientConfigPaths.s_currentIncludesUserConfig))
				{
					ClientConfigPaths.s_current = new ClientConfigPaths(null, includeUserConfig);
					ClientConfigPaths.s_currentIncludesUserConfig = includeUserConfig;
				}
				result = ClientConfigPaths.s_current;
			}
			else
			{
				result = new ClientConfigPaths(exePath, includeUserConfig);
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000079B0 File Offset: 0x00005BB0
		internal static void RefreshCurrent()
		{
			ClientConfigPaths.s_currentIncludesUserConfig = false;
			ClientConfigPaths.s_current = null;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000079C2 File Offset: 0x00005BC2
		internal static ClientConfigPaths Current
		{
			get
			{
				return ClientConfigPaths.GetPaths(null, true);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000079CB File Offset: 0x00005BCB
		internal bool HasEntryAssembly
		{
			get
			{
				return this._hasEntryAssembly;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000079D3 File Offset: 0x00005BD3
		internal string ApplicationUri
		{
			get
			{
				return this._applicationUri;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000079DB File Offset: 0x00005BDB
		internal string ApplicationConfigUri
		{
			get
			{
				return this._applicationConfigUri;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000079E3 File Offset: 0x00005BE3
		internal string RoamingConfigFilename
		{
			get
			{
				return this._roamingConfigFilename;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000079EB File Offset: 0x00005BEB
		internal string RoamingConfigDirectory
		{
			get
			{
				return this._roamingConfigDirectory;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000079F3 File Offset: 0x00005BF3
		internal bool HasRoamingConfig
		{
			get
			{
				return this.RoamingConfigFilename != null || !this._includesUserConfig;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00007A08 File Offset: 0x00005C08
		internal string LocalConfigFilename
		{
			get
			{
				return this._localConfigFilename;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00007A10 File Offset: 0x00005C10
		internal string LocalConfigDirectory
		{
			get
			{
				return this._localConfigDirectory;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00007A18 File Offset: 0x00005C18
		internal bool HasLocalConfig
		{
			get
			{
				return this.LocalConfigFilename != null || !this._includesUserConfig;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00007A2D File Offset: 0x00005C2D
		internal string ProductName
		{
			get
			{
				return this._productName;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00007A35 File Offset: 0x00005C35
		internal string ProductVersion
		{
			get
			{
				return this._productVersion;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00007A3D File Offset: 0x00005C3D
		private static SecurityPermission ControlEvidencePermission
		{
			get
			{
				if (ClientConfigPaths.s_controlEvidencePerm == null)
				{
					ClientConfigPaths.s_controlEvidencePerm = new SecurityPermission(SecurityPermissionFlag.ControlEvidence);
				}
				return ClientConfigPaths.s_controlEvidencePerm;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00007A5D File Offset: 0x00005C5D
		private static SecurityPermission SerializationFormatterPermission
		{
			get
			{
				if (ClientConfigPaths.s_serializationPerm == null)
				{
					ClientConfigPaths.s_serializationPerm = new SecurityPermission(SecurityPermissionFlag.SerializationFormatter);
				}
				return ClientConfigPaths.s_serializationPerm;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007A80 File Offset: 0x00005C80
		private string CombineIfValid(string path1, string path2)
		{
			string result = null;
			if (path1 != null && path2 != null)
			{
				try
				{
					string text = Path.Combine(path1, path2);
					if (text.Length < 260)
					{
						result = text;
					}
				}
				catch
				{
				}
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007AC4 File Offset: 0x00005CC4
		private string GetTypeAndHashSuffix(AppDomain appDomain, string exePath)
		{
			string result = null;
			string text = null;
			object evidenceInfo = ClientConfigPaths.GetEvidenceInfo(appDomain, exePath, out text);
			if (evidenceInfo != null && !string.IsNullOrEmpty(text))
			{
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				ClientConfigPaths.SerializationFormatterPermission.Assert();
				binaryFormatter.Serialize(memoryStream, evidenceInfo);
				memoryStream.Position = 0L;
				string hash = ClientConfigPaths.GetHash(memoryStream);
				if (!string.IsNullOrEmpty(hash))
				{
					result = "_" + text + "_" + hash;
				}
			}
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007B3C File Offset: 0x00005D3C
		private static object GetEvidenceInfo(AppDomain appDomain, string exePath, out string typeName)
		{
			ClientConfigPaths.ControlEvidencePermission.Assert();
			Evidence evidence = appDomain.Evidence;
			StrongName strongName = null;
			Url url = null;
			if (evidence != null)
			{
				IEnumerator hostEnumerator = evidence.GetHostEnumerator();
				while (hostEnumerator.MoveNext())
				{
					object obj = hostEnumerator.Current;
					if (obj is StrongName)
					{
						strongName = (StrongName)obj;
						break;
					}
					if (obj is Url)
					{
						url = (Url)obj;
					}
				}
			}
			object result = null;
			if (strongName != null)
			{
				result = ClientConfigPaths.MakeVersionIndependent(strongName);
				typeName = "StrongName";
			}
			else if (url != null)
			{
				result = url.Value.ToUpperInvariant();
				typeName = "Url";
			}
			else if (exePath != null)
			{
				result = exePath;
				typeName = "Path";
			}
			else
			{
				typeName = null;
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007BE4 File Offset: 0x00005DE4
		private static string GetHash(Stream s)
		{
			byte[] buff;
			using (SHA1 sha = new SHA1CryptoServiceProvider())
			{
				buff = sha.ComputeHash(s);
			}
			return ClientConfigPaths.ToBase32StringSuitableForDirName(buff);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007C24 File Offset: 0x00005E24
		private bool IsClickOnceDeployed(AppDomain appDomain)
		{
			ActivationContext activationContext = appDomain.ActivationContext;
			if (activationContext != null && activationContext.Form == ActivationContext.ContextForm.StoreBounded)
			{
				string fullName = activationContext.Identity.FullName;
				if (!string.IsNullOrEmpty(fullName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00007C5B File Offset: 0x00005E5B
		private static StrongName MakeVersionIndependent(StrongName sn)
		{
			return new StrongName(sn.PublicKey, sn.Name, new Version(0, 0, 0, 0));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007C78 File Offset: 0x00005E78
		private void SetNamesAndVersion(string applicationFilename, Assembly exeAssembly, bool isHttp)
		{
			Type type = null;
			if (exeAssembly != null)
			{
				object[] customAttributes = exeAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					this._companyName = ((AssemblyCompanyAttribute)customAttributes[0]).Company;
					if (this._companyName != null)
					{
						this._companyName = this._companyName.Trim();
					}
				}
				customAttributes = exeAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					this._productName = ((AssemblyProductAttribute)customAttributes[0]).Product;
					if (this._productName != null)
					{
						this._productName = this._productName.Trim();
					}
				}
				this._productVersion = exeAssembly.GetName().Version.ToString();
				if (this._productVersion != null)
				{
					this._productVersion = this._productVersion.Trim();
				}
			}
			if (!isHttp && (string.IsNullOrEmpty(this._companyName) || string.IsNullOrEmpty(this._productName) || string.IsNullOrEmpty(this._productVersion)))
			{
				string text = null;
				if (exeAssembly != null)
				{
					MethodInfo entryPoint = exeAssembly.EntryPoint;
					if (entryPoint != null)
					{
						type = entryPoint.ReflectedType;
						if (type != null)
						{
							text = type.Module.FullyQualifiedName;
						}
					}
				}
				if (text == null)
				{
					text = applicationFilename;
				}
				if (text != null)
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(text);
					if (versionInfo != null)
					{
						if (string.IsNullOrEmpty(this._companyName))
						{
							this._companyName = versionInfo.CompanyName;
							if (this._companyName != null)
							{
								this._companyName = this._companyName.Trim();
							}
						}
						if (string.IsNullOrEmpty(this._productName))
						{
							this._productName = versionInfo.ProductName;
							if (this._productName != null)
							{
								this._productName = this._productName.Trim();
							}
						}
						if (string.IsNullOrEmpty(this._productVersion))
						{
							this._productVersion = versionInfo.ProductVersion;
							if (this._productVersion != null)
							{
								this._productVersion = this._productVersion.Trim();
							}
						}
					}
				}
			}
			if (string.IsNullOrEmpty(this._companyName) || string.IsNullOrEmpty(this._productName))
			{
				string text2 = null;
				if (type != null)
				{
					text2 = type.Namespace;
				}
				if (string.IsNullOrEmpty(this._productName))
				{
					if (text2 != null)
					{
						int num = text2.LastIndexOf(".", StringComparison.Ordinal);
						if (num != -1 && num < text2.Length - 1)
						{
							this._productName = text2.Substring(num + 1);
						}
						else
						{
							this._productName = text2;
						}
						this._productName = this._productName.Trim();
					}
					if (string.IsNullOrEmpty(this._productName) && type != null)
					{
						this._productName = type.Name.Trim();
					}
					if (this._productName == null)
					{
						this._productName = string.Empty;
					}
				}
				if (string.IsNullOrEmpty(this._companyName))
				{
					if (text2 != null)
					{
						int num2 = text2.IndexOf(".", StringComparison.Ordinal);
						if (num2 != -1)
						{
							this._companyName = text2.Substring(0, num2);
						}
						else
						{
							this._companyName = text2;
						}
						this._companyName = this._companyName.Trim();
					}
					if (string.IsNullOrEmpty(this._companyName))
					{
						this._companyName = this._productName;
					}
				}
			}
			if (string.IsNullOrEmpty(this._productVersion))
			{
				this._productVersion = "1.0.0.0";
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007FB4 File Offset: 0x000061B4
		private static string ToBase32StringSuitableForDirName(byte[] buff)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = buff.Length;
			int num2 = 0;
			do
			{
				byte b = (num2 < num) ? buff[num2++] : 0;
				byte b2 = (num2 < num) ? buff[num2++] : 0;
				byte b3 = (num2 < num) ? buff[num2++] : 0;
				byte b4 = (num2 < num) ? buff[num2++] : 0;
				byte b5 = (num2 < num) ? buff[num2++] : 0;
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(int)(b & 31)]);
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(int)(b2 & 31)]);
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(int)(b3 & 31)]);
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(int)(b4 & 31)]);
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(int)(b5 & 31)]);
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(b & 224) >> 5 | (b4 & 96) >> 2]);
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(b2 & 224) >> 5 | (b5 & 96) >> 2]);
				b3 = (byte)(b3 >> 5);
				if ((b4 & 128) != 0)
				{
					b3 |= 8;
				}
				if ((b5 & 128) != 0)
				{
					b3 |= 16;
				}
				stringBuilder.Append(ClientConfigPaths.s_Base32Char[(int)b3]);
			}
			while (num2 < num);
			return stringBuilder.ToString();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008104 File Offset: 0x00006304
		private string Validate(string str, bool limitSize)
		{
			string text = str;
			if (!string.IsNullOrEmpty(text))
			{
				foreach (char oldChar in Path.GetInvalidFileNameChars())
				{
					text = text.Replace(oldChar, '_');
				}
				text = text.Replace(' ', '_');
				if (limitSize)
				{
					text = ((text.Length > 25) ? text.Substring(0, 25) : text);
				}
			}
			return text;
		}

		// Token: 0x04000131 RID: 305
		internal const string UserConfigFilename = "user.config";

		// Token: 0x04000132 RID: 306
		private const string ClickOnceDataDirectory = "DataDirectory";

		// Token: 0x04000133 RID: 307
		private const string ConfigExtension = ".config";

		// Token: 0x04000134 RID: 308
		private const int MAX_PATH = 260;

		// Token: 0x04000135 RID: 309
		private const int MAX_UNICODESTRING_LEN = 32767;

		// Token: 0x04000136 RID: 310
		private const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x04000137 RID: 311
		private const int MAX_LENGTH_TO_USE = 25;

		// Token: 0x04000138 RID: 312
		private const string FILE_URI_LOCAL = "file:///";

		// Token: 0x04000139 RID: 313
		private const string FILE_URI_UNC = "file://";

		// Token: 0x0400013A RID: 314
		private const string FILE_URI = "file:";

		// Token: 0x0400013B RID: 315
		private const string HTTP_URI = "http://";

		// Token: 0x0400013C RID: 316
		private const string StrongNameDesc = "StrongName";

		// Token: 0x0400013D RID: 317
		private const string UrlDesc = "Url";

		// Token: 0x0400013E RID: 318
		private const string PathDesc = "Path";

		// Token: 0x0400013F RID: 319
		private static char[] s_Base32Char = new char[]
		{
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5'
		};

		// Token: 0x04000140 RID: 320
		private static volatile ClientConfigPaths s_current;

		// Token: 0x04000141 RID: 321
		private static volatile bool s_currentIncludesUserConfig;

		// Token: 0x04000142 RID: 322
		private static volatile SecurityPermission s_serializationPerm;

		// Token: 0x04000143 RID: 323
		private static volatile SecurityPermission s_controlEvidencePerm;

		// Token: 0x04000144 RID: 324
		private bool _hasEntryAssembly;

		// Token: 0x04000145 RID: 325
		private bool _includesUserConfig;

		// Token: 0x04000146 RID: 326
		private string _applicationUri;

		// Token: 0x04000147 RID: 327
		private string _applicationConfigUri;

		// Token: 0x04000148 RID: 328
		private string _roamingConfigDirectory;

		// Token: 0x04000149 RID: 329
		private string _roamingConfigFilename;

		// Token: 0x0400014A RID: 330
		private string _localConfigDirectory;

		// Token: 0x0400014B RID: 331
		private string _localConfigFilename;

		// Token: 0x0400014C RID: 332
		private string _companyName;

		// Token: 0x0400014D RID: 333
		private string _productName;

		// Token: 0x0400014E RID: 334
		private string _productVersion;
	}
}
