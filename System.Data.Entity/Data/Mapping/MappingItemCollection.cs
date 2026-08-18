using System;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.Mapping
{
	// Token: 0x02000233 RID: 563
	[CLSCompliant(false)]
	public abstract class MappingItemCollection : ItemCollection
	{
		// Token: 0x060023FC RID: 9212 RVA: 0x00082721 File Offset: 0x00080921
		internal MappingItemCollection(DataSpace dataSpace) : base(dataSpace)
		{
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x0003BCEB File Offset: 0x00039EEB
		internal virtual bool TryGetMap(string identity, DataSpace typeSpace, out Map map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x0003BCEB File Offset: 0x00039EEB
		internal virtual Map GetMap(GlobalItem item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0003BCEB File Offset: 0x00039EEB
		internal virtual bool TryGetMap(GlobalItem item, out Map map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0003BCEB File Offset: 0x00039EEB
		internal virtual Map GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0003BCEB File Offset: 0x00039EEB
		internal virtual bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out Map map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x0003BCEB File Offset: 0x00039EEB
		internal virtual Map GetMap(string identity, DataSpace typeSpace)
		{
			throw Error.NotSupported();
		}
	}
}
