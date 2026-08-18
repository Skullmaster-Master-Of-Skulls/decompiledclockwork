using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using WebGrease.Css.Extensions;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x020000EB RID: 235
	public class ReadOnlyCacheSection
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x00046658 File Offset: 0x00044858
		private ReadOnlyCacheSection(string jsonString, IWebGreaseContext context)
		{
			this.context = context;
			JObject jobject = JObject.Parse(jsonString);
			this.sourceDependencies = jobject["sourceDependencies"].ToString().FromJson(true);
			this.cacheResults = jobject["cacheResults"].ToString().FromJson(true);
			this.childCacheSectionFiles = from f in jobject["children"].AsEnumerable<JToken>()
			select (string)f;
			this.absolutePath = (string)jobject["absolutePath"];
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00046710 File Offset: 0x00044910
		private IEnumerable<ReadOnlyCacheSection> ChildCacheSections
		{
			get
			{
				IEnumerable<ReadOnlyCacheSection> result;
				if ((result = this.childCacheSections) == null)
				{
					result = (this.childCacheSections = (from childCacheSectionFile in this.childCacheSectionFiles
					select ReadOnlyCacheSection.Load(childCacheSectionFile, this.context)).ToArray<ReadOnlyCacheSection>());
				}
				return result;
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000467C4 File Offset: 0x000449C4
		internal static ReadOnlyCacheSection Load(string fullPath, IWebGreaseContext context)
		{
			if (!File.Exists(fullPath))
			{
				return null;
			}
			return Safe.Lock<ReadOnlyCacheSection>(ReadOnlyCacheSection.LoadLock, delegate()
			{
				ReadOnlyCacheSection readOnlyCacheSection;
				if (!context.Cache.LoadedCacheSections.TryGetValue(fullPath, out readOnlyCacheSection))
				{
					readOnlyCacheSection = new ReadOnlyCacheSection(File.ReadAllText(fullPath), context);
					context.Cache.LoadedCacheSections.Add(fullPath, readOnlyCacheSection);
				}
				readOnlyCacheSection.referenceCount++;
				return readOnlyCacheSection;
			});
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00046858 File Offset: 0x00044A58
		internal IEnumerable<CacheResult> GetCacheResults(string fileCategory = null, bool endResultOnly = false)
		{
			return (from cr in this.cacheResults
			where (!endResultOnly || cr.EndResult) && (fileCategory == null || cr.FileCategory == fileCategory)
			select cr).Concat(this.ChildCacheSections.SelectMany((ReadOnlyCacheSection css) => css.GetCacheResults(fileCategory, endResultOnly)));
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000468C0 File Offset: 0x00044AC0
		internal void Dispose()
		{
			if (this.disposed)
			{
				throw new BuildWorkflowException("Cannot dispose an object twice.");
			}
			if (ReadOnlyCacheSection.Unload(this.context, this.absolutePath))
			{
				this.disposed = true;
				if (this.childCacheSections != null)
				{
					(from ccs in this.childCacheSections
					where ccs != null
					select ccs).ForEach(delegate(ReadOnlyCacheSection ccs)
					{
						ccs.Dispose();
					});
				}
				this.context = null;
				this.cacheResults = null;
				this.sourceDependencies = null;
				this.childCacheSections = null;
				this.childCacheSectionFiles = null;
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00046984 File Offset: 0x00044B84
		internal bool CanBeRestoredFromCache()
		{
			IEnumerable<ReadOnlyCacheSection> enumerable = new ReadOnlyCacheSection[]
			{
				this
			}.Concat(this.SafeAllRecursiveChildSections());
			foreach (ReadOnlyCacheSection readOnlyCacheSection in enumerable)
			{
				if (readOnlyCacheSection == null)
				{
					return false;
				}
				if (readOnlyCacheSection.cacheResults.Any((CacheResult cr) => cr == null || !File.Exists(cr.CachedFilePath)))
				{
					return false;
				}
				if (readOnlyCacheSection.HasChangedSourceDependencies())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00046A5C File Offset: 0x00044C5C
		internal bool CanBeSkipped()
		{
			IEnumerable<ReadOnlyCacheSection> enumerable = new ReadOnlyCacheSection[]
			{
				this
			}.Concat(this.SafeAllRecursiveChildSections());
			List<ReadOnlyCacheSection> list = new List<ReadOnlyCacheSection>();
			bool flag = false;
			foreach (ReadOnlyCacheSection readOnlyCacheSection in enumerable)
			{
				if (readOnlyCacheSection == null)
				{
					return false;
				}
				CacheResult[] source = (from cr in readOnlyCacheSection.cacheResults
				where cr.EndResult
				select cr).ToArray<CacheResult>();
				if (source.Any<CacheResult>())
				{
					flag = true;
					if (source.Any((CacheResult cr) => ReadOnlyCacheSection.HasCachedEndResultThatChanged(this.context, cr)))
					{
						return false;
					}
				}
				if (readOnlyCacheSection.sourceDependencies.Any((CacheSourceDependency sd) => sd == null || sd.HasChanged(this.context)))
				{
					return false;
				}
				list.Add(readOnlyCacheSection);
			}
			if (flag)
			{
				list.ForEach(delegate(ReadOnlyCacheSection scc)
				{
					scc.Touch();
				});
				this.Touch();
			}
			return flag;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00046C04 File Offset: 0x00044E04
		private static bool Unload(IWebGreaseContext context, string fullPath)
		{
			return Safe.Lock<bool>(ReadOnlyCacheSection.LoadLock, delegate()
			{
				ReadOnlyCacheSection readOnlyCacheSection;
				if (context.Cache.LoadedCacheSections.TryGetValue(fullPath, out readOnlyCacheSection))
				{
					readOnlyCacheSection.referenceCount--;
					if (readOnlyCacheSection.referenceCount != 0)
					{
						return false;
					}
					context.Cache.LoadedCacheSections.Remove(fullPath);
				}
				return true;
			});
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00046C3C File Offset: 0x00044E3C
		private static bool HasCachedEndResultThatChanged(IWebGreaseContext context, CacheResult r)
		{
			if (r == null)
			{
				return true;
			}
			string text = Path.Combine(context.Configuration.DestinationDirectory, r.RelativeHashedContentPath ?? r.RelativeContentPath);
			return !File.Exists(text) || !r.ContentHash.Equals(context.GetFileHash(text));
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00046CA1 File Offset: 0x00044EA1
		private void Touch()
		{
			this.context.Touch(this.absolutePath);
			this.cacheResults.ForEach(delegate(CacheResult cr)
			{
				this.context.Touch(cr.CachedFilePath);
			});
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00046CDE File Offset: 0x00044EDE
		private bool HasChangedSourceDependencies()
		{
			return this.sourceDependencies.Any((CacheSourceDependency sd) => sd == null || sd.HasChanged(this.context));
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00046D04 File Offset: 0x00044F04
		private IEnumerable<ReadOnlyCacheSection> SafeAllRecursiveChildSections()
		{
			return this.ChildCacheSections.Concat(this.ChildCacheSections.SelectMany(delegate(ReadOnlyCacheSection css)
			{
				if (css == null)
				{
					return null;
				}
				return css.SafeAllRecursiveChildSections();
			}));
		}

		// Token: 0x040005D8 RID: 1496
		private static readonly object LoadLock = new object();

		// Token: 0x040005D9 RID: 1497
		private readonly string absolutePath;

		// Token: 0x040005DA RID: 1498
		private IWebGreaseContext context;

		// Token: 0x040005DB RID: 1499
		private IEnumerable<CacheSourceDependency> sourceDependencies;

		// Token: 0x040005DC RID: 1500
		private IEnumerable<CacheResult> cacheResults;

		// Token: 0x040005DD RID: 1501
		private IEnumerable<string> childCacheSectionFiles;

		// Token: 0x040005DE RID: 1502
		private IEnumerable<ReadOnlyCacheSection> childCacheSections;

		// Token: 0x040005DF RID: 1503
		private bool disposed;

		// Token: 0x040005E0 RID: 1504
		private int referenceCount;
	}
}
