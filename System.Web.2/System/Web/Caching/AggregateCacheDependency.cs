using System;
using System.Collections;
using System.Text;

namespace System.Web.Caching
{
	// Token: 0x0200087E RID: 2174
	public sealed class AggregateCacheDependency : CacheDependency
	{
		// Token: 0x06006657 RID: 26199 RVA: 0x001689D7 File Offset: 0x00166BD7
		public AggregateCacheDependency()
		{
			base.FinishInit();
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x001689E8 File Offset: 0x00166BE8
		public void Add(params CacheDependency[] dependencies)
		{
			DateTime dateTime = DateTime.MinValue;
			if (dependencies == null)
			{
				throw new ArgumentNullException("dependencies");
			}
			dependencies = (CacheDependency[])dependencies.Clone();
			foreach (CacheDependency cacheDependency in dependencies)
			{
				if (cacheDependency == null)
				{
					throw new ArgumentNullException("dependencies");
				}
				if (!cacheDependency.TakeOwnership())
				{
					throw new InvalidOperationException(SR.GetString("Cache_dependency_used_more_that_once"));
				}
			}
			bool flag = false;
			lock (this)
			{
				if (!this._disposed)
				{
					if (this._dependencies == null)
					{
						this._dependencies = new ArrayList();
					}
					this._dependencies.AddRange(dependencies);
					foreach (CacheDependency cacheDependency2 in dependencies)
					{
						cacheDependency2.SetCacheDependencyChanged(delegate(object sender, EventArgs args)
						{
							this.DependencyChanged(sender, args);
						});
						if (cacheDependency2.UtcLastModified > dateTime)
						{
							dateTime = cacheDependency2.UtcLastModified;
						}
						if (cacheDependency2.HasChanged)
						{
							flag = true;
							break;
						}
					}
				}
			}
			base.SetUtcLastModified(dateTime);
			if (flag)
			{
				base.NotifyDependencyChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x00168B14 File Offset: 0x00166D14
		protected override void DependencyDispose()
		{
			CacheDependency[] array = null;
			lock (this)
			{
				this._disposed = true;
				if (this._dependencies != null)
				{
					array = (CacheDependency[])this._dependencies.ToArray(typeof(CacheDependency));
					this._dependencies = null;
				}
			}
			if (array != null)
			{
				foreach (CacheDependency cacheDependency in array)
				{
					cacheDependency.DisposeInternal();
				}
			}
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x00168907 File Offset: 0x00166B07
		private void DependencyChanged(object sender, EventArgs e)
		{
			base.NotifyDependencyChanged(sender, e);
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x00168BA0 File Offset: 0x00166DA0
		public override string GetUniqueID()
		{
			StringBuilder stringBuilder = null;
			CacheDependency[] array = null;
			if (this._dependencies == null)
			{
				return null;
			}
			lock (this)
			{
				if (this._dependencies != null)
				{
					array = (CacheDependency[])this._dependencies.ToArray(typeof(CacheDependency));
				}
			}
			if (array != null)
			{
				foreach (CacheDependency cacheDependency in array)
				{
					string uniqueID = cacheDependency.GetUniqueID();
					if (uniqueID == null)
					{
						return null;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(uniqueID);
				}
			}
			if (stringBuilder == null)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x00168C54 File Offset: 0x00166E54
		internal CacheDependency[] GetDependencyArray()
		{
			CacheDependency[] result = null;
			lock (this)
			{
				if (this._dependencies != null)
				{
					result = (CacheDependency[])this._dependencies.ToArray(typeof(CacheDependency));
				}
			}
			return result;
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x00168CB0 File Offset: 0x00166EB0
		internal override bool IsFileDependency()
		{
			CacheDependency[] dependencyArray = this.GetDependencyArray();
			if (dependencyArray == null)
			{
				return false;
			}
			foreach (CacheDependency cacheDependency in dependencyArray)
			{
				if (cacheDependency.GetType() != typeof(CacheDependency) && cacheDependency.GetType() != typeof(AggregateCacheDependency))
				{
					return false;
				}
				if (!cacheDependency.IsFileDependency())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x00168D10 File Offset: 0x00166F10
		public override string[] GetFileDependencies()
		{
			ArrayList arrayList = null;
			CacheDependency[] dependencyArray = this.GetDependencyArray();
			if (dependencyArray == null)
			{
				return null;
			}
			foreach (CacheDependency cacheDependency in dependencyArray)
			{
				if (cacheDependency.GetType() == typeof(CacheDependency) || cacheDependency.GetType() == typeof(AggregateCacheDependency))
				{
					string[] fileDependencies = cacheDependency.GetFileDependencies();
					if (fileDependencies != null)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.AddRange(fileDependencies);
					}
				}
			}
			if (arrayList != null)
			{
				return (string[])arrayList.ToArray(typeof(string));
			}
			return null;
		}

		// Token: 0x040034B8 RID: 13496
		private ArrayList _dependencies;

		// Token: 0x040034B9 RID: 13497
		private bool _disposed;
	}
}
