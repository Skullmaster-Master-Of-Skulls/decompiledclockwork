using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web.Caching;

namespace System.Web
{
	// Token: 0x02000006 RID: 6
	internal sealed class HttpResponseInternalWrapper : HttpResponseInternalBase
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002067 File Offset: 0x00000267
		public HttpResponseInternalWrapper(HttpResponse httpResponse)
		{
			this._httpResponse = httpResponse;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002076 File Offset: 0x00000276
		public override HttpCachePolicyBase Cache
		{
			get
			{
				return new HttpCachePolicyWrapper(this._httpResponse.Cache);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002095 File Offset: 0x00000295
		public override string ContentType
		{
			get
			{
				return this._httpResponse.ContentType;
			}
			set
			{
				this._httpResponse.ContentType = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A3 File Offset: 0x000002A3
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020B0 File Offset: 0x000002B0
		public override Stream Filter
		{
			get
			{
				return this._httpResponse.Filter;
			}
			set
			{
				this._httpResponse.Filter = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020BE File Offset: 0x000002BE
		public override TextWriter Output
		{
			get
			{
				return this._httpResponse.Output;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020CB File Offset: 0x000002CB
		public override void Clear()
		{
			this._httpResponse.Clear();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020D8 File Offset: 0x000002D8
		public override void End()
		{
			this._httpResponse.End();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020E5 File Offset: 0x000002E5
		public override void Write(string s)
		{
			this._httpResponse.Write(s);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020F3 File Offset: 0x000002F3
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002100 File Offset: 0x00000300
		public override bool Buffer
		{
			get
			{
				return this._httpResponse.Buffer;
			}
			set
			{
				this._httpResponse.Buffer = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000210E File Offset: 0x0000030E
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000211B File Offset: 0x0000031B
		public override bool BufferOutput
		{
			get
			{
				return this._httpResponse.BufferOutput;
			}
			set
			{
				this._httpResponse.BufferOutput = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002129 File Offset: 0x00000329
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002136 File Offset: 0x00000336
		public override string CacheControl
		{
			get
			{
				return this._httpResponse.CacheControl;
			}
			set
			{
				this._httpResponse.CacheControl = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002151 File Offset: 0x00000351
		public override string Charset
		{
			get
			{
				return this._httpResponse.Charset;
			}
			set
			{
				this._httpResponse.Charset = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000215F File Offset: 0x0000035F
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000216C File Offset: 0x0000036C
		public override Encoding ContentEncoding
		{
			get
			{
				return this._httpResponse.ContentEncoding;
			}
			set
			{
				this._httpResponse.ContentEncoding = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000217A File Offset: 0x0000037A
		public override HttpCookieCollection Cookies
		{
			get
			{
				return this._httpResponse.Cookies;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002187 File Offset: 0x00000387
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002194 File Offset: 0x00000394
		public override int Expires
		{
			get
			{
				return this._httpResponse.Expires;
			}
			set
			{
				this._httpResponse.Expires = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021A2 File Offset: 0x000003A2
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000021AF File Offset: 0x000003AF
		public override DateTime ExpiresAbsolute
		{
			get
			{
				return this._httpResponse.ExpiresAbsolute;
			}
			set
			{
				this._httpResponse.ExpiresAbsolute = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021BD File Offset: 0x000003BD
		public override NameValueCollection Headers
		{
			get
			{
				return this._httpResponse.Headers;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021CA File Offset: 0x000003CA
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021D7 File Offset: 0x000003D7
		public override Encoding HeaderEncoding
		{
			get
			{
				return this._httpResponse.HeaderEncoding;
			}
			set
			{
				this._httpResponse.HeaderEncoding = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021E5 File Offset: 0x000003E5
		public override bool IsClientConnected
		{
			get
			{
				return this._httpResponse.IsClientConnected;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021F2 File Offset: 0x000003F2
		public override bool IsRequestBeingRedirected
		{
			get
			{
				return this._httpResponse.IsRequestBeingRedirected;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021FF File Offset: 0x000003FF
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000220C File Offset: 0x0000040C
		public override string RedirectLocation
		{
			get
			{
				return this._httpResponse.RedirectLocation;
			}
			set
			{
				this._httpResponse.RedirectLocation = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000221A File Offset: 0x0000041A
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002227 File Offset: 0x00000427
		public override string Status
		{
			get
			{
				return this._httpResponse.Status;
			}
			set
			{
				this._httpResponse.Status = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002235 File Offset: 0x00000435
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002242 File Offset: 0x00000442
		public override int StatusCode
		{
			get
			{
				return this._httpResponse.StatusCode;
			}
			set
			{
				this._httpResponse.StatusCode = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000225D File Offset: 0x0000045D
		public override string StatusDescription
		{
			get
			{
				return this._httpResponse.StatusDescription;
			}
			set
			{
				this._httpResponse.StatusDescription = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000226B File Offset: 0x0000046B
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002278 File Offset: 0x00000478
		public override int SubStatusCode
		{
			get
			{
				return this._httpResponse.SubStatusCode;
			}
			set
			{
				this._httpResponse.SubStatusCode = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002286 File Offset: 0x00000486
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002293 File Offset: 0x00000493
		public override bool SuppressContent
		{
			get
			{
				return this._httpResponse.SuppressContent;
			}
			set
			{
				this._httpResponse.SuppressContent = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000022A1 File Offset: 0x000004A1
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000022AE File Offset: 0x000004AE
		public override bool TrySkipIisCustomErrors
		{
			get
			{
				return this._httpResponse.TrySkipIisCustomErrors;
			}
			set
			{
				this._httpResponse.TrySkipIisCustomErrors = value;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000022BC File Offset: 0x000004BC
		public override void AddCacheItemDependency(string cacheKey)
		{
			this._httpResponse.AddCacheItemDependency(cacheKey);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000022CA File Offset: 0x000004CA
		public override void AddCacheItemDependencies(ArrayList cacheKeys)
		{
			this._httpResponse.AddCacheItemDependencies(cacheKeys);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000022D8 File Offset: 0x000004D8
		public override void AddCacheItemDependencies(string[] cacheKeys)
		{
			this._httpResponse.AddCacheItemDependencies(cacheKeys);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000022E6 File Offset: 0x000004E6
		public override void AddCacheDependency(params CacheDependency[] dependencies)
		{
			this._httpResponse.AddCacheDependency(dependencies);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000022F4 File Offset: 0x000004F4
		public override void AddFileDependency(string filename)
		{
			this._httpResponse.AddFileDependency(filename);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002302 File Offset: 0x00000502
		public override void AddFileDependencies(ArrayList filenames)
		{
			this._httpResponse.AddFileDependencies(filenames);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002310 File Offset: 0x00000510
		public override void AddFileDependencies(string[] filenames)
		{
			this._httpResponse.AddFileDependencies(filenames);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000231E File Offset: 0x0000051E
		public override void AppendCookie(HttpCookie cookie)
		{
			this._httpResponse.AppendCookie(cookie);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000232C File Offset: 0x0000052C
		public override void AppendHeader(string name, string value)
		{
			this._httpResponse.AppendHeader(name, value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000233B File Offset: 0x0000053B
		public override void AppendToLog(string param)
		{
			this._httpResponse.AppendToLog(param);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002349 File Offset: 0x00000549
		public override string ApplyAppPathModifier(string virtualPath)
		{
			return this._httpResponse.ApplyAppPathModifier(virtualPath);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002357 File Offset: 0x00000557
		public override void BinaryWrite(byte[] buffer)
		{
			this._httpResponse.BinaryWrite(buffer);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002365 File Offset: 0x00000565
		public override void ClearContent()
		{
			this._httpResponse.ClearContent();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002372 File Offset: 0x00000572
		public override void ClearHeaders()
		{
			this._httpResponse.ClearHeaders();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000237F File Offset: 0x0000057F
		public override void DisableKernelCache()
		{
			this._httpResponse.DisableKernelCache();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000238C File Offset: 0x0000058C
		public override void Flush()
		{
			this._httpResponse.Flush();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002399 File Offset: 0x00000599
		public override void Pics(string value)
		{
			this._httpResponse.Pics(value);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000023A7 File Offset: 0x000005A7
		public override void Redirect(string url)
		{
			this._httpResponse.Redirect(url);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000023B5 File Offset: 0x000005B5
		public override void Redirect(string url, bool endResponse)
		{
			this._httpResponse.Redirect(url, endResponse);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000023C4 File Offset: 0x000005C4
		public override void SetCookie(HttpCookie cookie)
		{
			this._httpResponse.SetCookie(cookie);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000023D2 File Offset: 0x000005D2
		public override TextWriter SwitchWriter(TextWriter writer)
		{
			return this._httpResponse.SwitchWriter(writer);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000023E0 File Offset: 0x000005E0
		public override void TransmitFile(string filename)
		{
			this._httpResponse.TransmitFile(filename);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000023EE File Offset: 0x000005EE
		public override void TransmitFile(string filename, long offset, long length)
		{
			this._httpResponse.TransmitFile(filename, offset, length);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000023FE File Offset: 0x000005FE
		public override void Write(char[] buffer, int index, int count)
		{
			this._httpResponse.Write(buffer, index, count);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000240E File Offset: 0x0000060E
		public override void Write(object obj)
		{
			this._httpResponse.Write(obj);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000241C File Offset: 0x0000061C
		public override void WriteFile(string filename)
		{
			this._httpResponse.WriteFile(filename);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000242A File Offset: 0x0000062A
		public override void WriteFile(string filename, bool readIntoMemory)
		{
			this._httpResponse.WriteFile(filename, readIntoMemory);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002439 File Offset: 0x00000639
		public override void WriteFile(string filename, long offset, long size)
		{
			this._httpResponse.WriteFile(filename, offset, size);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002449 File Offset: 0x00000649
		public override void WriteFile(IntPtr fileHandle, long offset, long size)
		{
			this._httpResponse.WriteFile(fileHandle, offset, size);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002459 File Offset: 0x00000659
		public override void WriteSubstitution(HttpResponseSubstitutionCallback callback)
		{
			this._httpResponse.WriteSubstitution(callback);
		}

		// Token: 0x0400000B RID: 11
		private HttpResponse _httpResponse;
	}
}
