using System;
using System.Collections.Generic;
using System.IO;
using WebGrease.Configuration;

namespace WebGrease
{
	// Token: 0x02000106 RID: 262
	public class ContentItem
	{
		// Token: 0x06001098 RID: 4248 RVA: 0x0004A194 File Offset: 0x00048394
		private ContentItem()
		{
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0004A19C File Offset: 0x0004839C
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x0004A1A4 File Offset: 0x000483A4
		public string RelativeContentPath { get; private set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0004A1AD File Offset: 0x000483AD
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x0004A1B5 File Offset: 0x000483B5
		public IEnumerable<ResourcePivotKey> ResourcePivotKeys { get; private set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0004A1BE File Offset: 0x000483BE
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x0004A1C6 File Offset: 0x000483C6
		public string RelativeHashedContentPath { get; private set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x0004A1CF File Offset: 0x000483CF
		public string Content
		{
			get
			{
				if (this.ContentItemType != ContentItemType.Path)
				{
					return this.ContentValue;
				}
				return this.ContentFromDisk();
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0004A1E6 File Offset: 0x000483E6
		public bool IsFromDisk
		{
			get
			{
				return this.ContentItemType == ContentItemType.Path;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0004A1F1 File Offset: 0x000483F1
		public string AbsoluteDiskPath
		{
			get
			{
				if (!this.IsFromDisk)
				{
					return null;
				}
				return this.AbsoluteContentPath;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0004A203 File Offset: 0x00048403
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x0004A20B File Offset: 0x0004840B
		private string ContentValue { get; set; }

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0004A214 File Offset: 0x00048414
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x0004A21C File Offset: 0x0004841C
		private string AbsoluteContentPath { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x0004A225 File Offset: 0x00048425
		// (set) Token: 0x060010A7 RID: 4263 RVA: 0x0004A22D File Offset: 0x0004842D
		private ContentItemType ContentItemType { get; set; }

		// Token: 0x060010A8 RID: 4264 RVA: 0x0004A238 File Offset: 0x00048438
		public static ContentItem FromCacheResult(CacheResult cacheResult, params ResourcePivotKey[] resourcePivotKeys)
		{
			return new ContentItem
			{
				ContentItemType = ContentItemType.Path,
				AbsoluteContentPath = cacheResult.CachedFilePath,
				RelativeContentPath = cacheResult.RelativeContentPath,
				RelativeHashedContentPath = cacheResult.RelativeHashedContentPath,
				ResourcePivotKeys = resourcePivotKeys
			};
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x0004A280 File Offset: 0x00048480
		public static ContentItem FromCacheResult(CacheResult cacheResult, string relativeContentPath = null, string relativeHashedContentPath = null, params ResourcePivotKey[] resourcePivotKeys)
		{
			return new ContentItem
			{
				ContentItemType = ContentItemType.Path,
				AbsoluteContentPath = cacheResult.CachedFilePath,
				RelativeContentPath = (relativeContentPath ?? cacheResult.RelativeContentPath),
				RelativeHashedContentPath = (relativeHashedContentPath ?? cacheResult.RelativeHashedContentPath),
				ResourcePivotKeys = resourcePivotKeys
			};
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004A2D0 File Offset: 0x000484D0
		public static ContentItem FromFile(string absoluteContentPath, string relativeContentPath = null, string relativeHashedContentPath = null, params ResourcePivotKey[] resourcePivotKeys)
		{
			return new ContentItem
			{
				ContentItemType = ContentItemType.Path,
				AbsoluteContentPath = absoluteContentPath,
				RelativeContentPath = (relativeContentPath ?? absoluteContentPath),
				RelativeHashedContentPath = relativeHashedContentPath,
				ResourcePivotKeys = resourcePivotKeys
			};
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004A30C File Offset: 0x0004850C
		public static ContentItem FromContentItem(ContentItem contentItem, string relativeContentPath = null, string relativeHashedContentPath = null)
		{
			return new ContentItem
			{
				RelativeHashedContentPath = (relativeHashedContentPath ?? contentItem.RelativeHashedContentPath),
				RelativeContentPath = (relativeContentPath ?? contentItem.RelativeContentPath),
				AbsoluteContentPath = contentItem.AbsoluteContentPath,
				ContentItemType = contentItem.ContentItemType,
				ContentValue = contentItem.ContentValue,
				ResourcePivotKeys = contentItem.ResourcePivotKeys,
				contentHash = contentItem.contentHash
			};
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004A380 File Offset: 0x00048580
		public static ContentItem FromContent(string content, params ResourcePivotKey[] resourcePivotKeys)
		{
			return new ContentItem
			{
				ContentItemType = ContentItemType.Value,
				ContentValue = content,
				ResourcePivotKeys = resourcePivotKeys
			};
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x0004A3AC File Offset: 0x000485AC
		public static ContentItem FromContent(string content, string relativeContentPath, string relativeHashedContentPath = null, params ResourcePivotKey[] resourcePivotKeys)
		{
			return new ContentItem
			{
				ContentItemType = ContentItemType.Value,
				ContentValue = content,
				ResourcePivotKeys = resourcePivotKeys,
				RelativeContentPath = relativeContentPath,
				RelativeHashedContentPath = relativeHashedContentPath
			};
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0004A3E4 File Offset: 0x000485E4
		public static ContentItem FromContent(string content, ContentItem contentItem, params ResourcePivotKey[] resourcePivotKeys)
		{
			return new ContentItem
			{
				ContentItemType = ContentItemType.Value,
				ContentValue = content,
				RelativeContentPath = contentItem.RelativeContentPath,
				RelativeHashedContentPath = contentItem.RelativeHashedContentPath,
				ResourcePivotKeys = ((resourcePivotKeys != null) ? ((IEnumerable<ResourcePivotKey>)resourcePivotKeys) : contentItem.ResourcePivotKeys)
			};
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004A438 File Offset: 0x00048638
		internal string GetContentHash(IWebGreaseContext context)
		{
			string result;
			if ((result = this.contentHash) == null)
			{
				result = (this.contentHash = ((this.ContentItemType == ContentItemType.Value) ? context.GetValueHash(this.Content) : context.GetFileHash(this.AbsoluteContentPath)));
			}
			return result;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0004A47B File Offset: 0x0004867B
		internal void WriteToRelativeHashedPath(string destinationDirectory, bool overwrite = false)
		{
			this.WriteTo(Path.Combine(destinationDirectory ?? string.Empty, this.RelativeHashedContentPath), overwrite);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0004A499 File Offset: 0x00048699
		internal void WriteToContentPath(string destinationDirectory, bool overwrite = false)
		{
			this.WriteTo(Path.Combine(destinationDirectory ?? string.Empty, this.RelativeContentPath), overwrite);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004A55C File Offset: 0x0004875C
		internal void WriteTo(string fullPath, bool overwrite = false)
		{
			FileInfo absolutePath = new FileInfo(fullPath);
			Safe.FileLock(absolutePath, delegate()
			{
				if (!absolutePath.Exists || overwrite)
				{
					if (absolutePath.Directory != null && !absolutePath.Directory.Exists)
					{
						absolutePath.Directory.Create();
					}
					if (this.ContentItemType == ContentItemType.Path)
					{
						File.Copy(this.AbsoluteContentPath, absolutePath.FullName, overwrite);
						return;
					}
					File.WriteAllText(absolutePath.FullName, this.Content);
				}
			});
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004A5A0 File Offset: 0x000487A0
		private string ContentFromDisk()
		{
			string result;
			if ((result = this.content) == null)
			{
				result = (this.content = File.ReadAllText(this.AbsoluteContentPath));
			}
			return result;
		}

		// Token: 0x04000678 RID: 1656
		private string contentHash;

		// Token: 0x04000679 RID: 1657
		private string content;
	}
}
