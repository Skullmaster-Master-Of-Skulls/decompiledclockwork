using System;
using System.Collections;

// Token: 0x0200001F RID: 31
public class ExecuteScriptCache : Queue, IDisposable
{
	// Token: 0x06000256 RID: 598 RVA: 0x00038E70 File Offset: 0x00037E70
	~ExecuteScriptCache()
	{
		this.Dispose(false);
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00038EA4 File Offset: 0x00037EA4
	public void Dispose()
	{
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00038EB8 File Offset: 0x00037EB8
	protected virtual void Dispose(bool disposeManagedResources)
	{
		if (!this.disposed)
		{
			if (disposeManagedResources)
			{
				this.EndExecuteScriptCaching();
			}
			this.disposed = true;
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00038F0B File Offset: 0x00037F0B
	public void StartExecuteScriptCaching()
	{
		this.executeScript_CachingEnabled = true;
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600025B RID: 603 RVA: 0x00038F18 File Offset: 0x00037F18
	// (set) Token: 0x0600025C RID: 604 RVA: 0x00038F30 File Offset: 0x00037F30
	public int Max
	{
		get
		{
			return this.max;
		}
		set
		{
			this.max = value;
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00038F3C File Offset: 0x00037F3C
	public void EndExecuteScriptCaching()
	{
		this.executeScript_CachingEnabled = false;
		while (base.Count > 0)
		{
			ExecuteScriptCacheItem executeScriptCacheItem = (ExecuteScriptCacheItem)this.Dequeue();
			if (executeScriptCacheItem != null)
			{
				executeScriptCacheItem.Dispose();
			}
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00038F80 File Offset: 0x00037F80
	public void Add(ExecuteScriptCacheItem itemNull)
	{
		if (base.Count >= this.max)
		{
			ExecuteScriptCacheItem executeScriptCacheItem = (ExecuteScriptCacheItem)base.Dequeue();
			executeScriptCacheItem.ShutDown();
		}
		base.Enqueue(itemNull);
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600025F RID: 607 RVA: 0x00038FC0 File Offset: 0x00037FC0
	public bool ExecuteScript_CachingEnabled
	{
		get
		{
			return this.executeScript_CachingEnabled;
		}
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00038FD8 File Offset: 0x00037FD8
	public ExecuteScriptCacheItem FindItem(string cachedCode)
	{
		object[] array = base.ToArray();
		ExecuteScriptCacheItem result = null;
		foreach (ExecuteScriptCacheItem executeScriptCacheItem in array)
		{
			if (executeScriptCacheItem.ExecuteScript_cachedCode.Equals(cachedCode))
			{
				result = executeScriptCacheItem;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0003903C File Offset: 0x0003803C
	public void Remove(string cachedCode)
	{
		Stack stack = new Stack();
		foreach (ExecuteScriptCacheItem executeScriptCacheItem in base.ToArray())
		{
			if (!executeScriptCacheItem.ExecuteScript_cachedCode.Equals(cachedCode))
			{
				stack.Push(executeScriptCacheItem);
			}
		}
		base.Clear();
		while (stack.Count > 0)
		{
			base.Enqueue(stack.Pop());
		}
	}

	// Token: 0x0400011C RID: 284
	private bool disposed = false;

	// Token: 0x0400011D RID: 285
	private bool executeScript_CachingEnabled = false;

	// Token: 0x0400011E RID: 286
	private int max = 10;
}
