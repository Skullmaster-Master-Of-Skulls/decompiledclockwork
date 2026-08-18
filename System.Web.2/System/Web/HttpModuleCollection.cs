using System;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000A4 RID: 164
	public sealed class HttpModuleCollection : NameObjectCollectionBase
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x00016A1C File Offset: 0x00014C1C
		internal HttpModuleCollection() : base(Misc.CaseInsensitiveInvariantKeyComparer)
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00017CA8 File Offset: 0x00015EA8
		public void CopyTo(Array dest, int index)
		{
			if (this._all == null)
			{
				int count = this.Count;
				this._all = new IHttpModule[count];
				for (int i = 0; i < count; i++)
				{
					this._all[i] = this.Get(i);
				}
			}
			if (this._all != null)
			{
				this._all.CopyTo(dest, index);
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00017D00 File Offset: 0x00015F00
		internal void AddModule(string name, IHttpModule m)
		{
			this._all = null;
			this._allKeys = null;
			base.BaseAdd(name, m);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00017D18 File Offset: 0x00015F18
		internal void AppendCollection(HttpModuleCollection other)
		{
			for (int i = 0; i < other.Count; i++)
			{
				this.AddModule(other.BaseGetKey(i), other.Get(i));
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00017D4A File Offset: 0x00015F4A
		public IHttpModule Get(string name)
		{
			return (IHttpModule)base.BaseGet(name);
		}

		// Token: 0x170003F9 RID: 1017
		public IHttpModule this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00017D61 File Offset: 0x00015F61
		public IHttpModule Get(int index)
		{
			return (IHttpModule)base.BaseGet(index);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000166A9 File Offset: 0x000148A9
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x170003FA RID: 1018
		public IHttpModule this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00017D78 File Offset: 0x00015F78
		public string[] AllKeys
		{
			get
			{
				if (this._allKeys == null)
				{
					this._allKeys = base.BaseGetAllKeys();
				}
				return this._allKeys;
			}
		}

		// Token: 0x040003C6 RID: 966
		private IHttpModule[] _all;

		// Token: 0x040003C7 RID: 967
		private string[] _allKeys;
	}
}
