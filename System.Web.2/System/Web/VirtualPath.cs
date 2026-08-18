using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	internal sealed class VirtualPath : IComparable
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x000030B5 File Offset: 0x000012B5
		private VirtualPath()
		{
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0002D7B4 File Offset: 0x0002B9B4
		private VirtualPath(string virtualPath)
		{
			if (UrlPath.IsAppRelativePath(virtualPath))
			{
				this._appRelativeVirtualPath = virtualPath;
				return;
			}
			this._virtualPath = virtualPath;
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		int IComparable.CompareTo(object obj)
		{
			VirtualPath virtualPath = obj as VirtualPath;
			if (virtualPath == null)
			{
				throw new ArgumentException();
			}
			if (virtualPath == this)
			{
				return 0;
			}
			return StringComparer.InvariantCultureIgnoreCase.Compare(this.VirtualPathString, virtualPath.VirtualPathString);
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002D818 File Offset: 0x0002BA18
		public string VirtualPathString
		{
			get
			{
				if (this._virtualPath == null)
				{
					if (HttpRuntime.AppDomainAppVirtualPathObject == null)
					{
						throw new HttpException(SR.GetString("VirtualPath_CantMakeAppAbsolute", new object[]
						{
							this._appRelativeVirtualPath
						}));
					}
					if (this._appRelativeVirtualPath.Length == 1)
					{
						this._virtualPath = HttpRuntime.AppDomainAppVirtualPath;
					}
					else
					{
						this._virtualPath = HttpRuntime.AppDomainAppVirtualPathString + this._appRelativeVirtualPath.Substring(2);
					}
				}
				return this._virtualPath;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0002D896 File Offset: 0x0002BA96
		internal string VirtualPathStringNoTrailingSlash
		{
			get
			{
				return UrlPath.RemoveSlashFromPathIfNeeded(this.VirtualPathString);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0002D8A3 File Offset: 0x0002BAA3
		internal string VirtualPathStringIfAvailable
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0002D8AC File Offset: 0x0002BAAC
		internal string AppRelativeVirtualPathStringOrNull
		{
			get
			{
				if (this._appRelativeVirtualPath == null)
				{
					if (this.flags[4])
					{
						return null;
					}
					if (HttpRuntime.AppDomainAppVirtualPathObject == null)
					{
						throw new HttpException(SR.GetString("VirtualPath_CantMakeAppRelative", new object[]
						{
							this._virtualPath
						}));
					}
					this._appRelativeVirtualPath = UrlPath.MakeVirtualPathAppRelativeOrNull(this._virtualPath);
					this.flags[4] = true;
					if (this._appRelativeVirtualPath == null)
					{
						return null;
					}
				}
				return this._appRelativeVirtualPath;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002D92C File Offset: 0x0002BB2C
		public string AppRelativeVirtualPathString
		{
			get
			{
				string appRelativeVirtualPathStringOrNull = this.AppRelativeVirtualPathStringOrNull;
				if (appRelativeVirtualPathStringOrNull == null)
				{
					return this._virtualPath;
				}
				return appRelativeVirtualPathStringOrNull;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0002D94B File Offset: 0x0002BB4B
		internal string AppRelativeVirtualPathStringIfAvailable
		{
			get
			{
				return this._appRelativeVirtualPath;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0002D953 File Offset: 0x0002BB53
		internal string VirtualPathStringWhicheverAvailable
		{
			get
			{
				if (this._virtualPath == null)
				{
					return this._appRelativeVirtualPath;
				}
				return this._virtualPath;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0002D96A File Offset: 0x0002BB6A
		public string Extension
		{
			get
			{
				return UrlPath.GetExtension(this.VirtualPathString);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0002D977 File Offset: 0x0002BB77
		public string FileName
		{
			get
			{
				return UrlPath.GetFileName(this.VirtualPathStringNoTrailingSlash);
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0002D984 File Offset: 0x0002BB84
		public VirtualPath CombineWithAppRoot()
		{
			return HttpRuntime.AppDomainAppVirtualPathObject.Combine(this);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002D994 File Offset: 0x0002BB94
		public VirtualPath Combine(VirtualPath relativePath)
		{
			if (relativePath == null)
			{
				throw new ArgumentNullException("relativePath");
			}
			if (!relativePath.IsRelative)
			{
				return relativePath;
			}
			this.FailIfRelativePath();
			string text = this.VirtualPathStringWhicheverAvailable;
			text = UrlPath.Combine(text, relativePath.VirtualPathString);
			return new VirtualPath(text);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0002D9DF File Offset: 0x0002BBDF
		internal VirtualPath SimpleCombine(string relativePath)
		{
			return this.SimpleCombine(relativePath, false);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002D9E9 File Offset: 0x0002BBE9
		internal VirtualPath SimpleCombineWithDir(string directoryName)
		{
			return this.SimpleCombine(directoryName, true);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0002D9F4 File Offset: 0x0002BBF4
		private VirtualPath SimpleCombine(string filename, bool addTrailingSlash)
		{
			string text = this.VirtualPathStringWhicheverAvailable + filename;
			if (addTrailingSlash)
			{
				text += "/";
			}
			VirtualPath virtualPath = new VirtualPath(text);
			virtualPath.CopyFlagsFrom(this, 7);
			return virtualPath;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002DA30 File Offset: 0x0002BC30
		public VirtualPath MakeRelative(VirtualPath toVirtualPath)
		{
			VirtualPath virtualPath = new VirtualPath();
			this.FailIfRelativePath();
			toVirtualPath.FailIfRelativePath();
			virtualPath._virtualPath = UrlPath.MakeRelative(this.VirtualPathString, toVirtualPath.VirtualPathString);
			return virtualPath;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002DA67 File Offset: 0x0002BC67
		public string MapPath()
		{
			return HostingEnvironment.MapPath(this);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002DA6F File Offset: 0x0002BC6F
		internal string MapPathInternal()
		{
			return HostingEnvironment.MapPathInternal(this);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002DA77 File Offset: 0x0002BC77
		internal string MapPathInternal(bool permitNull)
		{
			return HostingEnvironment.MapPathInternal(this, permitNull);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0002DA80 File Offset: 0x0002BC80
		internal string MapPathInternal(VirtualPath baseVirtualDir, bool allowCrossAppMapping)
		{
			return HostingEnvironment.MapPathInternal(this, baseVirtualDir, allowCrossAppMapping);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0002DA8A File Offset: 0x0002BC8A
		public string GetFileHash(IEnumerable virtualPathDependencies)
		{
			return HostingEnvironment.VirtualPathProvider.GetFileHash(this, virtualPathDependencies);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0002DA98 File Offset: 0x0002BC98
		public CacheDependency GetCacheDependency(IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			return HostingEnvironment.VirtualPathProvider.GetCacheDependency(this, virtualPathDependencies, utcStart);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0002DAA7 File Offset: 0x0002BCA7
		public bool FileExists()
		{
			return HostingEnvironment.VirtualPathProvider.FileExists(this);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0002DAB4 File Offset: 0x0002BCB4
		public bool DirectoryExists()
		{
			return HostingEnvironment.VirtualPathProvider.DirectoryExists(this);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0002DAC1 File Offset: 0x0002BCC1
		public VirtualFile GetFile()
		{
			return HostingEnvironment.VirtualPathProvider.GetFile(this);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0002DACE File Offset: 0x0002BCCE
		public VirtualDirectory GetDirectory()
		{
			return HostingEnvironment.VirtualPathProvider.GetDirectory(this);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002DADB File Offset: 0x0002BCDB
		public string GetCacheKey()
		{
			return HostingEnvironment.VirtualPathProvider.GetCacheKey(this);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0002DAE8 File Offset: 0x0002BCE8
		public Stream OpenFile()
		{
			return VirtualPathProvider.OpenFile(this);
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0002DAF0 File Offset: 0x0002BCF0
		internal bool HasTrailingSlash
		{
			get
			{
				if (this._virtualPath != null)
				{
					return UrlPath.HasTrailingSlash(this._virtualPath);
				}
				return UrlPath.HasTrailingSlash(this._appRelativeVirtualPath);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0002DB14 File Offset: 0x0002BD14
		public bool IsWithinAppRoot
		{
			get
			{
				if (!this.flags[1])
				{
					if (HttpRuntime.AppDomainIdInternal == null)
					{
						return true;
					}
					if (this.flags[4])
					{
						this.flags[2] = (this._appRelativeVirtualPath != null);
					}
					else
					{
						this.flags[2] = UrlPath.IsEqualOrSubpath(HttpRuntime.AppDomainAppVirtualPathString, this.VirtualPathString);
					}
					this.flags[1] = true;
				}
				return this.flags[2];
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0002DB92 File Offset: 0x0002BD92
		internal void FailIfNotWithinAppRoot()
		{
			if (!this.IsWithinAppRoot)
			{
				throw new ArgumentException(SR.GetString("Cross_app_not_allowed", new object[]
				{
					this.VirtualPathString
				}));
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002DBBB File Offset: 0x0002BDBB
		internal void FailIfRelativePath()
		{
			if (this.IsRelative)
			{
				throw new ArgumentException(SR.GetString("VirtualPath_AllowRelativePath", new object[]
				{
					this._virtualPath
				}));
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
		public bool IsRelative
		{
			get
			{
				return this._virtualPath != null && this._virtualPath[0] != '/';
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0002DC03 File Offset: 0x0002BE03
		public bool IsRoot
		{
			get
			{
				return this._virtualPath == "/";
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0002DC18 File Offset: 0x0002BE18
		public VirtualPath Parent
		{
			get
			{
				this.FailIfRelativePath();
				if (this.IsRoot)
				{
					return null;
				}
				string text = this.VirtualPathStringWhicheverAvailable;
				text = UrlPath.RemoveSlashFromPathIfNeeded(text);
				if (text == "~")
				{
					text = this.VirtualPathStringNoTrailingSlash;
				}
				int num = text.LastIndexOf('/');
				if (num == 0)
				{
					return VirtualPath.RootVirtualPath;
				}
				text = text.Substring(0, num + 1);
				return new VirtualPath(text);
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0002DC7A File Offset: 0x0002BE7A
		internal static VirtualPath Combine(VirtualPath v1, VirtualPath v2)
		{
			if (v1 == null)
			{
				v1 = HttpRuntime.AppDomainAppVirtualPathObject;
			}
			if (v1 == null)
			{
				v2.FailIfRelativePath();
				return v2;
			}
			return v1.Combine(v2);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0002DCA4 File Offset: 0x0002BEA4
		public static bool operator ==(VirtualPath v1, VirtualPath v2)
		{
			return VirtualPath.Equals(v1, v2);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0002DCAD File Offset: 0x0002BEAD
		public static bool operator !=(VirtualPath v1, VirtualPath v2)
		{
			return !VirtualPath.Equals(v1, v2);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0002DCB9 File Offset: 0x0002BEB9
		public static bool Equals(VirtualPath v1, VirtualPath v2)
		{
			return v1 == v2 || (v1 != null && v2 != null && VirtualPath.EqualsHelper(v1, v2));
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0002DCD0 File Offset: 0x0002BED0
		public override bool Equals(object value)
		{
			if (value == null)
			{
				return false;
			}
			VirtualPath virtualPath = value as VirtualPath;
			return virtualPath != null && VirtualPath.EqualsHelper(virtualPath, this);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0002DCF5 File Offset: 0x0002BEF5
		private static bool EqualsHelper(VirtualPath v1, VirtualPath v2)
		{
			return StringComparer.InvariantCultureIgnoreCase.Compare(v1.VirtualPathString, v2.VirtualPathString) == 0;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0002DD10 File Offset: 0x0002BF10
		public override int GetHashCode()
		{
			return StringComparer.InvariantCultureIgnoreCase.GetHashCode(this.VirtualPathString);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0002DD22 File Offset: 0x0002BF22
		public override string ToString()
		{
			if (this._virtualPath == null && HttpRuntime.AppDomainAppVirtualPathObject == null)
			{
				return this._appRelativeVirtualPath;
			}
			return this.VirtualPathString;
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0002DD46 File Offset: 0x0002BF46
		private void CopyFlagsFrom(VirtualPath virtualPath, int mask)
		{
			this.flags.IntegerValue = (this.flags.IntegerValue | (virtualPath.flags.IntegerValue & mask));
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002DD67 File Offset: 0x0002BF67
		internal static string GetVirtualPathString(VirtualPath virtualPath)
		{
			if (!(virtualPath == null))
			{
				return virtualPath.VirtualPathString;
			}
			return null;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0002DD7A File Offset: 0x0002BF7A
		internal static string GetVirtualPathStringNoTrailingSlash(VirtualPath virtualPath)
		{
			if (!(virtualPath == null))
			{
				return virtualPath.VirtualPathStringNoTrailingSlash;
			}
			return null;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0002DD8D File Offset: 0x0002BF8D
		internal static string GetAppRelativeVirtualPathString(VirtualPath virtualPath)
		{
			if (!(virtualPath == null))
			{
				return virtualPath.AppRelativeVirtualPathString;
			}
			return null;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0002DDA0 File Offset: 0x0002BFA0
		internal static string GetAppRelativeVirtualPathStringOrEmpty(VirtualPath virtualPath)
		{
			if (!(virtualPath == null))
			{
				return virtualPath.AppRelativeVirtualPathString;
			}
			return string.Empty;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0002DDB7 File Offset: 0x0002BFB7
		public static VirtualPath Create(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.AllowAllPath);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0002DDC1 File Offset: 0x0002BFC1
		public static VirtualPath CreateTrailingSlash(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.EnsureTrailingSlash | VirtualPathOptions.AllowAbsolutePath | VirtualPathOptions.AllowAppRelativePath | VirtualPathOptions.AllowRelativePath);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0002DDCB File Offset: 0x0002BFCB
		public static VirtualPath CreateAllowNull(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.AllowNull | VirtualPathOptions.AllowAbsolutePath | VirtualPathOptions.AllowAppRelativePath | VirtualPathOptions.AllowRelativePath);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0002DDD5 File Offset: 0x0002BFD5
		public static VirtualPath CreateAbsolute(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.AllowAbsolutePath);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0002DDDE File Offset: 0x0002BFDE
		public static VirtualPath CreateNonRelative(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.AllowAbsolutePath | VirtualPathOptions.AllowAppRelativePath);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002DDE8 File Offset: 0x0002BFE8
		public static VirtualPath CreateAbsoluteTrailingSlash(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.EnsureTrailingSlash | VirtualPathOptions.AllowAbsolutePath);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0002DDF1 File Offset: 0x0002BFF1
		public static VirtualPath CreateNonRelativeTrailingSlash(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.EnsureTrailingSlash | VirtualPathOptions.AllowAbsolutePath | VirtualPathOptions.AllowAppRelativePath);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0002DDFB File Offset: 0x0002BFFB
		public static VirtualPath CreateAbsoluteAllowNull(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.AllowNull | VirtualPathOptions.AllowAbsolutePath);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0002DE04 File Offset: 0x0002C004
		public static VirtualPath CreateNonRelativeAllowNull(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.AllowNull | VirtualPathOptions.AllowAbsolutePath | VirtualPathOptions.AllowAppRelativePath);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x0002DE0E File Offset: 0x0002C00E
		public static VirtualPath CreateNonRelativeTrailingSlashAllowNull(string virtualPath)
		{
			return VirtualPath.Create(virtualPath, VirtualPathOptions.AllowNull | VirtualPathOptions.EnsureTrailingSlash | VirtualPathOptions.AllowAbsolutePath | VirtualPathOptions.AllowAppRelativePath);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0002DE18 File Offset: 0x0002C018
		public unsafe static VirtualPath Create(string virtualPath, VirtualPathOptions options)
		{
			if (virtualPath != null)
			{
				virtualPath = virtualPath.Trim();
			}
			if (!string.IsNullOrEmpty(virtualPath))
			{
				bool flag = false;
				bool flag2 = false;
				int length = virtualPath.Length;
				fixed (string text = virtualPath)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					for (int i = 0; i < length; i++)
					{
						char c = ptr[i];
						if (c <= '.')
						{
							if (c == '\0')
							{
								throw new HttpException(SR.GetString("Invalid_vpath", new object[]
								{
									virtualPath
								}));
							}
							if (c == '.')
							{
								flag2 = true;
							}
						}
						else if (c != '/')
						{
							if (c == '\\')
							{
								flag = true;
							}
						}
						else if (i > 0 && ptr[i - 1] == '/')
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					if ((options & VirtualPathOptions.FailIfMalformed) != (VirtualPathOptions)0)
					{
						throw new HttpException(SR.GetString("Invalid_vpath", new object[]
						{
							virtualPath
						}));
					}
					virtualPath = UrlPath.FixVirtualPathSlashes(virtualPath);
				}
				if ((options & VirtualPathOptions.EnsureTrailingSlash) != (VirtualPathOptions)0)
				{
					virtualPath = UrlPath.AppendSlashToPathIfNeeded(virtualPath);
				}
				VirtualPath virtualPath2 = new VirtualPath();
				if (UrlPath.IsAppRelativePath(virtualPath))
				{
					if (flag2)
					{
						virtualPath = UrlPath.ReduceVirtualPath(virtualPath);
					}
					if (virtualPath[0] == '~')
					{
						if ((options & VirtualPathOptions.AllowAppRelativePath) == (VirtualPathOptions)0)
						{
							throw new ArgumentException(SR.GetString("VirtualPath_AllowAppRelativePath", new object[]
							{
								virtualPath
							}));
						}
						virtualPath2._appRelativeVirtualPath = virtualPath;
					}
					else
					{
						if ((options & VirtualPathOptions.AllowAbsolutePath) == (VirtualPathOptions)0)
						{
							throw new ArgumentException(SR.GetString("VirtualPath_AllowAbsolutePath", new object[]
							{
								virtualPath
							}));
						}
						virtualPath2._virtualPath = virtualPath;
					}
				}
				else if (virtualPath[0] != '/')
				{
					if ((options & VirtualPathOptions.AllowRelativePath) == (VirtualPathOptions)0)
					{
						throw new ArgumentException(SR.GetString("VirtualPath_AllowRelativePath", new object[]
						{
							virtualPath
						}));
					}
					virtualPath2._virtualPath = virtualPath;
				}
				else
				{
					if ((options & VirtualPathOptions.AllowAbsolutePath) == (VirtualPathOptions)0)
					{
						throw new ArgumentException(SR.GetString("VirtualPath_AllowAbsolutePath", new object[]
						{
							virtualPath
						}));
					}
					if (flag2)
					{
						virtualPath = UrlPath.ReduceVirtualPath(virtualPath);
					}
					virtualPath2._virtualPath = virtualPath;
				}
				return virtualPath2;
			}
			if ((options & VirtualPathOptions.AllowNull) != (VirtualPathOptions)0)
			{
				return null;
			}
			throw new ArgumentNullException("virtualPath");
		}

		// Token: 0x04000639 RID: 1593
		private string _appRelativeVirtualPath;

		// Token: 0x0400063A RID: 1594
		private string _virtualPath;

		// Token: 0x0400063B RID: 1595
		private const int isWithinAppRootComputed = 1;

		// Token: 0x0400063C RID: 1596
		private const int isWithinAppRoot = 2;

		// Token: 0x0400063D RID: 1597
		private const int appRelativeAttempted = 4;

		// Token: 0x0400063E RID: 1598
		private SimpleBitVector32 flags;

		// Token: 0x0400063F RID: 1599
		internal static VirtualPath RootVirtualPath = VirtualPath.Create("/");
	}
}
