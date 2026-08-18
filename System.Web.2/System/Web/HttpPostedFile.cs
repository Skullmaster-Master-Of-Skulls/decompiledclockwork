using System;
using System.IO;
using System.Web.Configuration;

namespace System.Web
{
	// Token: 0x020000A9 RID: 169
	public sealed class HttpPostedFile
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x00017E58 File Offset: 0x00016058
		internal HttpPostedFile(string filename, string contentType, HttpInputStream stream)
		{
			this._filename = filename;
			this._contentType = contentType;
			this._stream = stream;
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00017E75 File Offset: 0x00016075
		public string FileName
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00017E7D File Offset: 0x0001607D
		public string ContentType
		{
			get
			{
				return this._contentType;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00017E85 File Offset: 0x00016085
		public int ContentLength
		{
			get
			{
				return (int)this._stream.Length;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00017E93 File Offset: 0x00016093
		public Stream InputStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00017E9C File Offset: 0x0001609C
		public void SaveAs(string filename)
		{
			if (!Path.IsPathRooted(filename))
			{
				HttpRuntimeSection httpRuntime = RuntimeConfig.GetConfig().HttpRuntime;
				if (httpRuntime.RequireRootedSaveAsPath)
				{
					throw new HttpException(SR.GetString("SaveAs_requires_rooted_path", new object[]
					{
						filename
					}));
				}
			}
			FileStream fileStream = new FileStream(filename, FileMode.Create);
			try
			{
				this._stream.WriteTo(fileStream);
				fileStream.Flush();
			}
			finally
			{
				fileStream.Close();
			}
		}

		// Token: 0x040003C8 RID: 968
		private string _filename;

		// Token: 0x040003C9 RID: 969
		private string _contentType;

		// Token: 0x040003CA RID: 970
		private HttpInputStream _stream;
	}
}
