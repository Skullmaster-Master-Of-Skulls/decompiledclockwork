using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x020000E3 RID: 227
	public class CacheManager : ICacheManager
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x00045574 File Offset: 0x00043774
		public CacheManager(WebGreaseConfiguration configuration, LogManager logManager, ICacheSection parentCacheSection)
		{
			CacheManager <>4__this = this;
			this.currentCacheSection = parentCacheSection;
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (logManager == null)
			{
				throw new ArgumentNullException("logManager");
			}
			string text = configuration.CacheRootPath.AsNullIfWhiteSpace() ?? "_webgrease.cache";
			if (!Path.IsPathRooted(text))
			{
				text = Path.Combine(configuration.SourceDirectory, text);
			}
			this.cacheRootPath = CacheManager.GetCacheUniquePath(text, configuration.CacheUniqueKey);
			if (!Directory.Exists(this.cacheRootPath))
			{
				Directory.CreateDirectory(this.cacheRootPath);
			}
			Safe.Lock(CacheManager.First, delegate()
			{
				if (CacheManager.First.Contains(<>4__this.cacheRootPath))
				{
					CacheManager.First.Add(<>4__this.cacheRootPath);
					logManager.Information("Cache enabled using cache path: {0}".InvariantFormat(new object[]
					{
						<>4__this.cacheRootPath
					}), MessageImportance.Normal);
				}
			});
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00045694 File Offset: 0x00043894
		public void LockedFileCacheAction(string lockFileContent, Action action)
		{
			string text = Path.Combine(this.RootPath, "webgrease.caching.lock");
			if (!Safe.WriteToFileStream(text, delegate(FileStream fs)
			{
				StreamWriter streamWriter = new StreamWriter(fs);
				streamWriter.Write(lockFileContent);
				streamWriter.Flush();
				action();
				streamWriter.Write("\r\nDone");
				streamWriter.Flush();
			}))
			{
				throw new BuildWorkflowException("Could not get a unique lock on cache lock file: {0}".InvariantFormat(new object[]
				{
					text
				}));
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000457B4 File Offset: 0x000439B4
		private static string GetCacheUniquePath(string cacheRoot, string cacheUniqueKey)
		{
			string cachePath = null;
			string uniqueKey = cacheUniqueKey ?? string.Empty;
			string text = Path.Combine(cacheRoot, "cachefoldermap.txt");
			if (!Safe.WriteToFileStream(text, delegate(FileStream fs)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				StreamReader streamReader = new StreamReader(fs);
				dictionary = (streamReader.ReadToEnd().FromJson(false) ?? dictionary);
				string text2;
				if (!dictionary.TryGetValue(uniqueKey, out text2))
				{
					int num = 0;
					do
					{
						text2 = "CF{0}".InvariantFormat(new object[]
						{
							++num
						});
					}
					while (dictionary.ContainsValue(text2));
					dictionary.Add(uniqueKey, text2);
					fs.Seek(0L, SeekOrigin.Begin);
					StreamWriter streamWriter = new StreamWriter(fs);
					string value = dictionary.ToJson(false);
					streamWriter.Write(value);
					streamWriter.Flush();
				}
				cachePath = Path.Combine(cacheRoot, text2);
			}))
			{
				throw new BuildWorkflowException("Could not get a unique lock on: {0}".InvariantFormat(new object[]
				{
					text
				}));
			}
			if (string.IsNullOrWhiteSpace(cachePath) || cachePath == cacheRoot)
			{
				throw new BuildWorkflowException("Could not find a valid cache folder in: {0}".InvariantFormat(new object[]
				{
					cacheRoot
				}));
			}
			return cachePath;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00045872 File Offset: 0x00043A72
		public ICacheSection CurrentCacheSection
		{
			get
			{
				return this.currentCacheSection;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0004587A File Offset: 0x00043A7A
		public IDictionary<string, ReadOnlyCacheSection> LoadedCacheSections
		{
			get
			{
				return this.loadedCacheSections;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00045882 File Offset: 0x00043A82
		public string RootPath
		{
			get
			{
				return this.cacheRootPath;
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0004588C File Offset: 0x00043A8C
		public ICacheSection BeginSection(WebGreaseSectionKey webGreaseSectionKey, bool autoLoad = true)
		{
			return this.currentCacheSection = CacheSection.Begin(this.context, webGreaseSectionKey.Category, webGreaseSectionKey.Value, this.CurrentCacheSection, autoLoad);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x000458DC File Offset: 0x00043ADC
		public void CleanUp()
		{
			DateTime utcDateTime = this.context.SessionStartTime.UtcDateTime;
			if (this.context.Configuration.CacheTimeout.TotalSeconds > 0.0)
			{
				DateTime expireTime = utcDateTime - this.context.Configuration.CacheTimeout;
				string[] files = Directory.GetFiles(this.cacheRootPath, "*.*", SearchOption.AllDirectories);
				IEnumerable<string> enumerable = from f in files
				where File.GetLastWriteTimeUtc(f) < expireTime
				select f;
				foreach (string path in enumerable)
				{
					try
					{
						File.Delete(path);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x000459BC File Offset: 0x00043BBC
		public void EndSection(ICacheSection cacheSection)
		{
			if (this.CurrentCacheSection == cacheSection)
			{
				this.currentCacheSection = cacheSection.Parent;
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000459D3 File Offset: 0x00043BD3
		public string GetAbsoluteCacheFilePath(string category, string fileName)
		{
			return Path.Combine(this.cacheRootPath, category, fileName);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000459E2 File Offset: 0x00043BE2
		public void SetContext(IWebGreaseContext newContext)
		{
			this.context = newContext;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000459EC File Offset: 0x00043BEC
		public string StoreInCache(string cacheCategory, ContentItem contentItem)
		{
			string contentHash = contentItem.GetContentHash(this.context);
			string str = Path.GetExtension(contentItem.RelativeContentPath) ?? ".txt";
			string absoluteCacheFilePath = this.GetAbsoluteCacheFilePath(cacheCategory, contentHash + str);
			contentItem.WriteTo(absoluteCacheFilePath, false);
			return absoluteCacheFilePath;
		}

		// Token: 0x040005B7 RID: 1463
		internal const string LockFileName = "webgrease.caching.lock";

		// Token: 0x040005B8 RID: 1464
		private static readonly IList<string> First = new List<string>();

		// Token: 0x040005B9 RID: 1465
		private readonly IDictionary<string, ReadOnlyCacheSection> loadedCacheSections = new Dictionary<string, ReadOnlyCacheSection>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040005BA RID: 1466
		private readonly string cacheRootPath;

		// Token: 0x040005BB RID: 1467
		private IWebGreaseContext context;

		// Token: 0x040005BC RID: 1468
		private ICacheSection currentCacheSection;
	}
}
