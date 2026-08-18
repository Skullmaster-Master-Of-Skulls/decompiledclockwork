using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x02000505 RID: 1285
	internal abstract class ProxyChain : IEnumerable<Uri>, IEnumerable, IDisposable
	{
		// Token: 0x060027F2 RID: 10226 RVA: 0x000A4D5B File Offset: 0x000A3D5B
		protected ProxyChain(Uri destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000A4D78 File Offset: 0x000A3D78
		public IEnumerator<Uri> GetEnumerator()
		{
			ProxyChain.ProxyEnumerator proxyEnumerator = new ProxyChain.ProxyEnumerator(this);
			if (this.m_MainEnumerator == null)
			{
				this.m_MainEnumerator = proxyEnumerator;
			}
			return proxyEnumerator;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000A4D9C File Offset: 0x000A3D9C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000A4DA4 File Offset: 0x000A3DA4
		public virtual void Dispose()
		{
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000A4DA6 File Offset: 0x000A3DA6
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

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x000A4DBD File Offset: 0x000A3DBD
		internal Uri Destination
		{
			get
			{
				return this.m_Destination;
			}
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000A4DC5 File Offset: 0x000A3DC5
		internal virtual void Abort()
		{
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000A4DC7 File Offset: 0x000A3DC7
		internal bool HttpAbort(HttpWebRequest request, WebException webException)
		{
			this.Abort();
			return true;
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x000A4DD0 File Offset: 0x000A3DD0
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

		// Token: 0x060027FB RID: 10235
		protected abstract bool GetNextProxy(out Uri proxy);

		// Token: 0x04002748 RID: 10056
		private List<Uri> m_Cache = new List<Uri>();

		// Token: 0x04002749 RID: 10057
		private bool m_CacheComplete;

		// Token: 0x0400274A RID: 10058
		private ProxyChain.ProxyEnumerator m_MainEnumerator;

		// Token: 0x0400274B RID: 10059
		private Uri m_Destination;

		// Token: 0x0400274C RID: 10060
		private HttpAbortDelegate m_HttpAbortDelegate;

		// Token: 0x02000506 RID: 1286
		private class ProxyEnumerator : IEnumerator<Uri>, IDisposable, IEnumerator
		{
			// Token: 0x060027FC RID: 10236 RVA: 0x000A4DF2 File Offset: 0x000A3DF2
			internal ProxyEnumerator(ProxyChain chain)
			{
				this.m_Chain = chain;
			}

			// Token: 0x17000842 RID: 2114
			// (get) Token: 0x060027FD RID: 10237 RVA: 0x000A4E08 File Offset: 0x000A3E08
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

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x060027FE RID: 10238 RVA: 0x000A4E41 File Offset: 0x000A3E41
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060027FF RID: 10239 RVA: 0x000A4E4C File Offset: 0x000A3E4C
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
					bool result;
					lock (this.m_Chain.m_Cache)
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

			// Token: 0x06002800 RID: 10240 RVA: 0x000A4F54 File Offset: 0x000A3F54
			public void Reset()
			{
				this.m_Finished = false;
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06002801 RID: 10241 RVA: 0x000A4F64 File Offset: 0x000A3F64
			public void Dispose()
			{
			}

			// Token: 0x0400274D RID: 10061
			private ProxyChain m_Chain;

			// Token: 0x0400274E RID: 10062
			private bool m_Finished;

			// Token: 0x0400274F RID: 10063
			private int m_CurrentIndex = -1;

			// Token: 0x04002750 RID: 10064
			private bool m_TriedDirect;
		}
	}
}
