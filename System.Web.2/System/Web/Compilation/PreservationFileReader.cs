using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x02000855 RID: 2133
	internal class PreservationFileReader
	{
		// Token: 0x0600651C RID: 25884 RVA: 0x0016389A File Offset: 0x00161A9A
		internal PreservationFileReader(DiskBuildResultCache diskCache, bool precompilationMode)
		{
			this._diskCache = diskCache;
			this._precompilationMode = precompilationMode;
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x001638B0 File Offset: 0x00161AB0
		internal BuildResult ReadBuildResultFromFile(VirtualPath virtualPath, string preservationFile, long hashCode, bool ensureIsUpToDate)
		{
			if (!FileUtil.FileExists(preservationFile))
			{
				return null;
			}
			BuildResult result = null;
			try
			{
				result = this.ReadFileInternal(virtualPath, preservationFile, hashCode, ensureIsUpToDate);
			}
			catch (SecurityException)
			{
				throw;
			}
			catch
			{
				if (!this._precompilationMode)
				{
					Util.RemoveOrRenameFile(preservationFile);
				}
			}
			return result;
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x00163908 File Offset: 0x00161B08
		private BuildResult ReadFileInternal(VirtualPath virtualPath, string preservationFile, long hashCode, bool ensureIsUpToDate)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(preservationFile);
			this._root = xmlDocument.DocumentElement;
			if (this._root == null || this._root.Name != "preserve")
			{
				return null;
			}
			string attribute = this.GetAttribute("resultType");
			BuildResultTypeCode code = (BuildResultTypeCode)int.Parse(attribute, CultureInfo.InvariantCulture);
			if (virtualPath == null || AppSettings.VerifyVirtualPathFromDiskCache)
			{
				virtualPath = VirtualPath.Create(this.GetAttribute("virtualPath"));
			}
			long num = 0L;
			string virtualPathDependenciesHash = null;
			if (!this._precompilationMode)
			{
				string attribute2 = this.GetAttribute("hash");
				if (attribute2 == null)
				{
					return null;
				}
				num = long.Parse(attribute2, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
				virtualPathDependenciesHash = this.GetAttribute("filehash");
			}
			BuildResult buildResult = BuildResult.CreateBuildResultFromCode(code, virtualPath);
			if (!this._precompilationMode)
			{
				this.ReadDependencies();
				if (this._sourceDependencies != null)
				{
					buildResult.SetVirtualPathDependencies(this._sourceDependencies);
				}
				buildResult.VirtualPathDependenciesHash = virtualPathDependenciesHash;
				bool flag = false;
				if (!buildResult.IsUpToDate(virtualPath, ensureIsUpToDate))
				{
					flag = true;
				}
				else
				{
					long num2 = buildResult.ComputeHashCode(hashCode);
					if (num2 == 0L || num2 != num)
					{
						flag = true;
					}
				}
				if (flag)
				{
					bool flag2 = false;
					try
					{
						CompilationLock.GetLock(ref flag2);
						buildResult.RemoveOutOfDateResources(this);
						File.Delete(preservationFile);
					}
					finally
					{
						if (flag2)
						{
							CompilationLock.ReleaseLock();
						}
					}
					return null;
				}
			}
			buildResult.GetPreservedAttributes(this);
			return buildResult;
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x00163A6C File Offset: 0x00161C6C
		private void ReadDependencies()
		{
			foreach (object obj in this._root.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					string name = xmlNode.Name;
					if (name == "filedeps")
					{
						this._sourceDependencies = this.ReadDependencies(xmlNode, "filedep");
					}
				}
			}
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x00163AD0 File Offset: 0x00161CD0
		private ArrayList ReadDependencies(XmlNode parent, string tagName)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in parent.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (!xmlNode.Name.Equals(tagName))
					{
						break;
					}
					string text = HandlerBase.RemoveAttribute(xmlNode, "name");
					if (text == null)
					{
						return null;
					}
					arrayList.Add(text);
				}
			}
			return arrayList;
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x00163B36 File Offset: 0x00161D36
		internal string GetAttribute(string name)
		{
			return HandlerBase.RemoveAttribute(this._root, name);
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x06006522 RID: 25890 RVA: 0x00163B44 File Offset: 0x00161D44
		internal DiskBuildResultCache DiskCache
		{
			get
			{
				return this._diskCache;
			}
		}

		// Token: 0x04003427 RID: 13351
		private XmlNode _root;

		// Token: 0x04003428 RID: 13352
		private bool _precompilationMode;

		// Token: 0x04003429 RID: 13353
		private DiskBuildResultCache _diskCache;

		// Token: 0x0400342A RID: 13354
		private ArrayList _sourceDependencies;
	}
}
