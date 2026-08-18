using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x020001DF RID: 479
	internal abstract class ProxyChain : IEnumerable<Uri>, IEnumerable, IDisposable
	{
		// Token: 0x060012C2 RID: 4802 RVA: 0x00063760 File Offset: 0x00061960
		protected ProxyChain(Uri destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0006377C File Offset: 0x0006197C
		public IEnumerator<Uri> GetEnumerator()
		{
			ProxyChain.ProxyEnumerator proxyEnumerator = new ProxyChain.ProxyEnumerator(this);
			if (this.m_MainEnumerator == null)
			{
				this.m_MainEnumerator = proxyEnumerator;
			}
			return proxyEnumerator;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000637A0 File Offset: 0x000619A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000637A8 File Offset: 0x000619A8
		public virtual void Dispose()
		{
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000637AC File Offset: 0x000619AC
		internal IEnumerator<Uri> Enumerator
		{
			get
			{
				if (this.m_MainEnumerator != null)
				{
					return this.m_MainEnumerator;
				}
				return this.GetEnumerator();
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x000637D0 File Offset: 0x000619D0
		internal Uri Destination
		{
			get
			{
				return this.m_Destination;
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000637D8 File Offset: 0x000619D8
		internal virtual void Abort()
		{
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000637DA File Offset: 0x000619DA
		internal bool HttpAbort(HttpWebRequest request, WebException webException)
		{
			this.Abort();
			return true;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x000637E3 File Offset: 0x000619E3
		internal HttpAbortDelegate HttpAbortDelegate
		{
			get
			{
				if (this.m_HttpAbortDelegate == null)
				{
					this.m_HttpAbortDelegate = new HttpAbortDelegate(this.HttpAbort);
				}
				return this.m_HttpAbortDelegate;
			}
		}

		// Token: 0x060012CB RID: 4811
		protected abstract bool GetNextProxy(out Uri proxy);

		// Token: 0x0400151B RID: 5403
		private List<Uri> m_Cache = new List<Uri>();

		// Token: 0x0400151C RID: 5404
		private bool m_CacheComplete;

		// Token: 0x0400151D RID: 5405
		private ProxyChain.ProxyEnumerator m_MainEnumerator;

		// Token: 0x0400151E RID: 5406
		private Uri m_Destination;

		// Token: 0x0400151F RID: 5407
		private HttpAbortDelegate m_HttpAbortDelegate;

		// Token: 0x02000755 RID: 1877
		private class ProxyEnumerator : IEnumerator<Uri>, IDisposable, IEnumerator
		{
			// Token: 0x06004206 RID: 16902 RVA: 0x00112599 File Offset: 0x00110799
			internal ProxyEnumerator(ProxyChain chain)
			{
				this.m_Chain = chain;
			}

			// Token: 0x17000F18 RID: 3864
			// (get) Token: 0x06004207 RID: 16903 RVA: 0x001125AF File Offset: 0x001107AF
			public Uri Current
			{
				get
				{
					if (this.m_Finished || this.m_CurrentIndex < 0)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.m_Chain.m_Cache[this.m_CurrentIndex];
				}
			}

			// Token: 0x17000F19 RID: 3865
			// (get) Token: 0x06004208 RID: 16904 RVA: 0x001125E8 File Offset: 0x001107E8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06004209 RID: 16905 RVA: 0x001125F0 File Offset: 0x001107F0
			public bool MoveNext()
			{
				if (this.m_Finished)
				{
					return false;
				}
				checked
				{
					this.m_CurrentIndex++;
					if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
					{
						return true;
					}
					if (this.m_Chain.m_CacheComplete)
					{
						this.m_Finished = true;
						return false;
					}
					List<Uri> cache = this.m_Chain.m_Cache;
					bool result;
					lock (cache)
					{
						if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
						{
							result = true;
						}
						else if (this.m_Chain.m_CacheComplete)
						{
							this.m_Finished = true;
							result = false;
						}
						else
						{
							Uri uri;
							while (this.m_Chain.GetNextProxy(out uri))
							{
								if (uri == null)
								{
									if (this.m_TriedDirect)
									{
										continue;
									}
									this.m_TriedDirect = true;
								}
								this.m_Chain.m_Cache.Add(uri);
								return true;
							}
							this.m_Finished = true;
							this.m_Chain.m_CacheComplete = true;
							result = false;
						}
					}
					return result;
				}
			}

			// Token: 0x0600420A RID: 16906 RVA: 0x00112700 File Offset: 0x00110900
			public void Reset()
			{
				this.m_Finished = false;
				this.m_CurrentIndex = -1;
			}

			// Token: 0x0600420B RID: 16907 RVA: 0x00112710 File Offset: 0x00110910
			public void Dispose()
			{
			}

			// Token: 0x04003218 RID: 12824
			private ProxyChain m_Chain;

			// Token: 0x04003219 RID: 12825
			private bool m_Finished;

			// Token: 0x0400321A RID: 12826
			private int m_CurrentIndex = -1;

			// Token: 0x0400321B RID: 12827
			private bool m_TriedDirect;
		}
	}
}
