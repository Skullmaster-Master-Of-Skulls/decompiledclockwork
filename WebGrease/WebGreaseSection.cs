using System;
using System.Collections.Concurrent;
using WebGrease.Configuration;

namespace WebGrease
{
	// Token: 0x0200010B RID: 267
	public class WebGreaseSection : IWebGreaseSection, ICachableWebGreaseSection
	{
		// Token: 0x060010DA RID: 4314 RVA: 0x0004AFE5 File Offset: 0x000491E5
		private WebGreaseSection(IWebGreaseContext context, string[] idParts, bool isGroup)
		{
			this.context = context;
			this.idParts = idParts;
			this.isGroup = isGroup;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0004B002 File Offset: 0x00049202
		public static IWebGreaseSection Create(IWebGreaseContext context, string[] idParts, bool isGroup)
		{
			return new WebGreaseSection(context, idParts, isGroup);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004B00C File Offset: 0x0004920C
		public void Execute(Action action)
		{
			this.context.Measure.Start(this.isGroup, this.idParts);
			try
			{
				action();
			}
			finally
			{
				this.context.Measure.End(this.isGroup, this.idParts);
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0004B06C File Offset: 0x0004926C
		public T Execute<T>(Func<T> action)
		{
			this.context.Measure.Start(this.isGroup, this.idParts);
			T result;
			try
			{
				result = action();
			}
			finally
			{
				this.context.Measure.End(this.isGroup, this.idParts);
			}
			return result;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0004B0CC File Offset: 0x000492CC
		public ICachableWebGreaseSection MakeCachable(object varBySettings, bool isSkipable = false, bool infiniteWaitForLock = false)
		{
			this.MakeCachable(null, varBySettings, isSkipable, infiniteWaitForLock);
			return this;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0004B0DA File Offset: 0x000492DA
		public ICachableWebGreaseSection MakeCachable(ContentItem varByContentItem, object varBySettings = null, bool isSkipable = false, bool infiniteWaitForLock = false)
		{
			this.cacheVarByContentItem = varByContentItem;
			this.cacheVarBySetting = varBySettings;
			this.cacheIsSkipable = isSkipable;
			this.cacheInfiniteWaitForLock = infiniteWaitForLock;
			return this;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0004B0FA File Offset: 0x000492FA
		public ICachableWebGreaseSection MakeCachable(IFileSet varByFileSet, object varBySettings = null, bool isSkipable = false, bool infiniteWaitForLock = false)
		{
			this.cacheVarByFileSet = varByFileSet;
			this.cacheVarBySetting = varBySettings;
			this.cacheIsSkipable = isSkipable;
			this.cacheInfiniteWaitForLock = infiniteWaitForLock;
			return this;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0004B11A File Offset: 0x0004931A
		public ICachableWebGreaseSection RestoreFromCacheAction(Func<ICacheSection, bool> action)
		{
			this.restoreFromCacheAction = action;
			return this;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0004B124 File Offset: 0x00049324
		public ICachableWebGreaseSection WhenSkipped(Action<ICacheSection> action)
		{
			this.whenSkippedAction = action;
			return this;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0004B334 File Offset: 0x00049534
		public bool Execute(Func<ICacheSection, bool> cachableSectionAction)
		{
			string category = WebGreaseContext.ToStringId(this.idParts);
			WebGreaseSectionKey webGreaseSectionKey = new WebGreaseSectionKey(this.context, category, this.cacheVarByContentItem, this.cacheVarBySetting, this.cacheVarByFileSet, null);
			object orAdd = WebGreaseSection.SectionLocks.GetOrAdd(webGreaseSectionKey.Value, new object());
			return Safe.Lock<bool>(orAdd, this.cacheInfiniteWaitForLock ? int.MaxValue : 5000, delegate()
			{
				bool errorHasOccurred = false;
				EventHandler value = delegate(object param0, EventArgs param1)
				{
					errorHasOccurred = true;
				};
				this.context.Log.ErrorOccurred += value;
				ICacheSection cacheSection = this.context.Cache.BeginSection(webGreaseSectionKey, true);
				bool result;
				try
				{
					if (this.context.TemporaryIgnore(this.cacheVarByFileSet, this.cacheVarByContentItem) && !errorHasOccurred)
					{
						cacheSection.Save();
						result = true;
					}
					else
					{
						cacheSection.Load();
						if (this.cacheIsSkipable && cacheSection.CanBeSkipped())
						{
							if (this.whenSkippedAction != null)
							{
								this.whenSkippedAction(cacheSection);
							}
							if (!errorHasOccurred)
							{
								return true;
							}
						}
						if (this.restoreFromCacheAction != null && cacheSection.CanBeRestoredFromCache() && this.restoreFromCacheAction(cacheSection) && !errorHasOccurred)
						{
							result = true;
						}
						else
						{
							this.context.Measure.Start(this.isGroup, this.idParts);
							try
							{
								if (!cachableSectionAction(cacheSection) || errorHasOccurred)
								{
									result = false;
								}
								else
								{
									cacheSection.Save();
									result = true;
								}
							}
							finally
							{
								this.context.Measure.End(this.isGroup, this.idParts);
							}
						}
					}
				}
				finally
				{
					this.context.Log.ErrorOccurred -= value;
					cacheSection.EndSection();
				}
				return result;
			});
		}

		// Token: 0x04000698 RID: 1688
		private static readonly ConcurrentDictionary<string, object> SectionLocks = new ConcurrentDictionary<string, object>();

		// Token: 0x04000699 RID: 1689
		private readonly bool isGroup;

		// Token: 0x0400069A RID: 1690
		private readonly IWebGreaseContext context;

		// Token: 0x0400069B RID: 1691
		private readonly string[] idParts;

		// Token: 0x0400069C RID: 1692
		private object cacheVarBySetting;

		// Token: 0x0400069D RID: 1693
		private bool cacheIsSkipable;

		// Token: 0x0400069E RID: 1694
		private bool cacheInfiniteWaitForLock;

		// Token: 0x0400069F RID: 1695
		private ContentItem cacheVarByContentItem;

		// Token: 0x040006A0 RID: 1696
		private IFileSet cacheVarByFileSet;

		// Token: 0x040006A1 RID: 1697
		private Func<ICacheSection, bool> restoreFromCacheAction;

		// Token: 0x040006A2 RID: 1698
		private Action<ICacheSection> whenSkippedAction;
	}
}
