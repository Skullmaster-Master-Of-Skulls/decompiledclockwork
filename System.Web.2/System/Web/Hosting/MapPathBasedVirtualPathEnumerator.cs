using System;
using System.Collections;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007D4 RID: 2004
	internal class MapPathBasedVirtualPathEnumerator : MarshalByRefObject, IEnumerator, IDisposable
	{
		// Token: 0x0600601E RID: 24606 RVA: 0x0014C0F8 File Offset: 0x0014A2F8
		internal MapPathBasedVirtualPathEnumerator(VirtualPath virtualPath, RequestedEntryType requestedEntryType)
		{
			if (virtualPath.IsRelative)
			{
				throw new ArgumentException(SR.GetString("Invalid_app_VirtualPath"), "virtualPath");
			}
			this._virtualPath = virtualPath;
			this._requestedEntryType = requestedEntryType;
			string path;
			if (!ServerConfig.UseServerConfig)
			{
				path = this._virtualPath.MapPathInternal();
			}
			else
			{
				IServerConfig instance = ServerConfig.GetInstance();
				this._serverConfig2 = (instance as IServerConfig2);
				path = instance.MapPath(null, this._virtualPath);
				if (this._requestedEntryType != RequestedEntryType.Files)
				{
					if (this._serverConfig2 == null)
					{
						string[] virtualSubdirs = instance.GetVirtualSubdirs(this._virtualPath, false);
						if (virtualSubdirs != null)
						{
							this._exclude = new Hashtable(StringComparer.OrdinalIgnoreCase);
							foreach (string text in virtualSubdirs)
							{
								this._exclude[text] = text;
							}
						}
					}
					string[] virtualSubdirs2 = instance.GetVirtualSubdirs(this._virtualPath, true);
					if (virtualSubdirs2 != null)
					{
						this._virtualPaths = new Hashtable(StringComparer.OrdinalIgnoreCase);
						foreach (string text2 in virtualSubdirs2)
						{
							VirtualPath virtualPath2 = this._virtualPath.SimpleCombineWithDir(text2);
							string dirname = instance.MapPath(null, virtualPath2);
							if (FileUtil.DirectoryExists(dirname))
							{
								this._virtualPaths[text2] = new MapPathBasedVirtualDirectory(virtualPath2.VirtualPathString);
							}
						}
						this._virtualEnumerator = this._virtualPaths.Values.GetEnumerator();
					}
				}
			}
			this._fileEnumerator = FileEnumerator.Create(path);
			this._useFileEnumerator = false;
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x0014C271 File Offset: 0x0014A471
		void IDisposable.Dispose()
		{
			if (this._fileEnumerator != null)
			{
				((IDisposable)this._fileEnumerator).Dispose();
				this._fileEnumerator = null;
			}
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x0014C294 File Offset: 0x0014A494
		bool IEnumerator.MoveNext()
		{
			bool flag = false;
			if (this._virtualEnumerator != null)
			{
				flag = this._virtualEnumerator.MoveNext();
			}
			if (!flag)
			{
				this._useFileEnumerator = true;
				for (;;)
				{
					flag = this._fileEnumerator.MoveNext();
					if (!flag)
					{
						break;
					}
					FileData fileData = (FileData)this._fileEnumerator.Current;
					if (!fileData.IsHidden)
					{
						if (fileData.IsDirectory)
						{
							if (this._requestedEntryType != RequestedEntryType.Files)
							{
								string name = fileData.Name;
								if ((this._virtualPaths == null || !this._virtualPaths.Contains(name)) && (this._exclude == null || !this._exclude.Contains(name)))
								{
									if (this._serverConfig2 == null || this._serverConfig2.IsWithinApp(UrlPath.SimpleCombine(this._virtualPath.VirtualPathString, name)))
									{
										break;
									}
								}
							}
						}
						else if (this._requestedEntryType != RequestedEntryType.Directories)
						{
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x17001B81 RID: 7041
		// (get) Token: 0x06006022 RID: 24610 RVA: 0x0014C36C File Offset: 0x0014A56C
		internal VirtualFileBase Current
		{
			get
			{
				if (!this._useFileEnumerator)
				{
					return (VirtualFileBase)this._virtualEnumerator.Current;
				}
				FileData fileData = (FileData)this._fileEnumerator.Current;
				VirtualPath virtualPath;
				if (fileData.IsDirectory)
				{
					virtualPath = this._virtualPath.SimpleCombineWithDir(fileData.Name);
					return new MapPathBasedVirtualDirectory(virtualPath.VirtualPathString);
				}
				virtualPath = this._virtualPath.SimpleCombine(fileData.Name);
				FindFileData findFileData = fileData.GetFindFileData();
				return new MapPathBasedVirtualFile(virtualPath.VirtualPathString, fileData.FullName, findFileData);
			}
		}

		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x0014C3F4 File Offset: 0x0014A5F4
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		void IEnumerator.Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0400323F RID: 12863
		private VirtualPath _virtualPath;

		// Token: 0x04003240 RID: 12864
		private Hashtable _exclude;

		// Token: 0x04003241 RID: 12865
		private Hashtable _virtualPaths;

		// Token: 0x04003242 RID: 12866
		private IEnumerator _fileEnumerator;

		// Token: 0x04003243 RID: 12867
		private IEnumerator _virtualEnumerator;

		// Token: 0x04003244 RID: 12868
		private bool _useFileEnumerator;

		// Token: 0x04003245 RID: 12869
		private RequestedEntryType _requestedEntryType;

		// Token: 0x04003246 RID: 12870
		private IServerConfig2 _serverConfig2;
	}
}
