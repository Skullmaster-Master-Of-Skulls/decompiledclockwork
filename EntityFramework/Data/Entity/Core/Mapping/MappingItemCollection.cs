using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003AA RID: 938
	public abstract class MappingItemCollection : ItemCollection
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x0009F8A2 File Offset: 0x0009DAA2
		internal MappingItemCollection(DataSpace dataSpace) : base(dataSpace)
		{
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0009F8AB File Offset: 0x0009DAAB
		internal virtual bool TryGetMap(string identity, DataSpace typeSpace, out MappingBase map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0009F8B2 File Offset: 0x0009DAB2
		internal virtual MappingBase GetMap(GlobalItem item)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0009F8B9 File Offset: 0x0009DAB9
		internal virtual bool TryGetMap(GlobalItem item, out MappingBase map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0009F8C0 File Offset: 0x0009DAC0
		internal virtual MappingBase GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			throw Error.NotSupported();
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0009F8C7 File Offset: 0x0009DAC7
		internal virtual bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out MappingBase map)
		{
			throw Error.NotSupported();
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0009F8CE File Offset: 0x0009DACE
		internal virtual MappingBase GetMap(string identity, DataSpace typeSpace)
		{
			throw Error.NotSupported();
		}
	}
}
