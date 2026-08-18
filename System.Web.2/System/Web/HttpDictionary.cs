using System;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000097 RID: 151
	internal class HttpDictionary : NameObjectCollectionBase
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x00016A1C File Offset: 0x00014C1C
		internal HttpDictionary() : base(Misc.CaseInsensitiveInvariantKeyComparer)
		{
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00016A29 File Offset: 0x00014C29
		internal int Size
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00016A31 File Offset: 0x00014C31
		internal object GetValue(string key)
		{
			return base.BaseGet(key);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00016A3A File Offset: 0x00014C3A
		internal void SetValue(string key, object value)
		{
			base.BaseSet(key, value);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00016A44 File Offset: 0x00014C44
		internal object GetValue(int index)
		{
			return base.BaseGet(index);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000166A9 File Offset: 0x000148A9
		internal string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00016A4D File Offset: 0x00014C4D
		internal string[] GetAllKeys()
		{
			return base.BaseGetAllKeys();
		}
	}
}
