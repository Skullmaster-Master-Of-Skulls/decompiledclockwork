using System;
using System.Collections;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002B1 RID: 689
	internal class MapPathBasedVirtualPathEnumerator : MarshalByRefObject, IEnumerator, IDisposable
	{
		// Token: 0x060023EE RID: 9198 RVA: 0x0009A11C File Offset: 0x0009911C
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
				this._processHostServerConfig = (instance as ProcessHostServerConfig);
				path = instance.MapPath(null, this._virtualPath);
				if (this._requestedEntryType != RequestedEntryType.Files)
				{
					if (this._processHostServerConfig == null)
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

		// Token: 0x060023EF RID: 9199 RVA: 0x0009A298 File Offset: 0x00099298
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x0009A29B File Offset: 0x0009929B
		void IDisposable.Dispose()
		{
			if (this._fileEnumerator != null)
			{
				((IDisposable)this._fileEnumerator).Dispose();
				this._fileEnumerator = null;
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x0009A2BC File Offset: 0x000992BC
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
									if (this._processHostServerConfig == null || this._processHostServerConfig.IsWithinApp(UrlPath.SimpleCombine(this._virtualPath.VirtualPathString, name)))
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

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x0009A394 File Offset: 0x00099394
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

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x0009A41C File Offset: 0x0009941C
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x0009A424 File Offset: 0x00099424
		void IEnumerator.Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04001C25 RID: 7205
		private VirtualPath _virtualPath;

		// Token: 0x04001C26 RID: 7206
		private Hashtable _exclude;

		// Token: 0x04001C27 RID: 7207
		private Hashtable _virtualPaths;

		// Token: 0x04001C28 RID: 7208
		private IEnumerator _fileEnumerator;

		// Token: 0x04001C29 RID: 7209
		private IEnumerator _virtualEnumerator;

		// Token: 0x04001C2A RID: 7210
		private bool _useFileEnumerator;

		// Token: 0x04001C2B RID: 7211
		private RequestedEntryType _requestedEntryType;

		// Token: 0x04001C2C RID: 7212
		private ProcessHostServerConfig _processHostServerConfig;
	}
}
