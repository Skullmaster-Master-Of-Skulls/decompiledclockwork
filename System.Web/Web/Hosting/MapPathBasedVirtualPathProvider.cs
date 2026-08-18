using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020002AB RID: 683
	internal class MapPathBasedVirtualPathProvider : VirtualPathProvider
	{
		// Token: 0x060023D5 RID: 9173 RVA: 0x00099EA4 File Offset: 0x00098EA4
		public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
		{
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			foreach (object obj in virtualPathDependencies)
			{
				string virtualPath2 = (string)obj;
				string fileName = HostingEnvironment.MapPathInternal(virtualPath2);
				hashCodeCombiner.AddFile(fileName);
			}
			return hashCodeCombiner.CombinedHashString;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00099F10 File Offset: 0x00098F10
		public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			if (virtualPathDependencies == null)
			{
				return null;
			}
			StringCollection stringCollection = null;
			foreach (object obj in virtualPathDependencies)
			{
				string virtualPath2 = (string)obj;
				string value = HostingEnvironment.MapPathInternal(virtualPath2);
				if (stringCollection == null)
				{
					stringCollection = new StringCollection();
				}
				stringCollection.Add(value);
			}
			if (stringCollection == null)
			{
				return null;
			}
			string[] array = new string[stringCollection.Count];
			stringCollection.CopyTo(array, 0);
			return new CacheDependency(0, array, utcStart);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00099FA4 File Offset: 0x00098FA4
		public override bool FileExists(string virtualPath)
		{
			string path = HostingEnvironment.MapPathInternal(virtualPath);
			return File.Exists(path);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x00099FC0 File Offset: 0x00098FC0
		public override bool DirectoryExists(string virtualDir)
		{
			string path = HostingEnvironment.MapPathInternal(virtualDir);
			return Directory.Exists(path);
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x00099FDA File Offset: 0x00098FDA
		public override VirtualFile GetFile(string virtualPath)
		{
			return new MapPathBasedVirtualFile(virtualPath);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x00099FE2 File Offset: 0x00098FE2
		public override VirtualDirectory GetDirectory(string virtualDir)
		{
			return new MapPathBasedVirtualDirectory(virtualDir);
		}
	}
}
