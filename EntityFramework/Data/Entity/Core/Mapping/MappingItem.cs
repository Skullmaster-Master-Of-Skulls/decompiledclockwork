using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200000D RID: 13
	public abstract class MappingItem
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004957 File Offset: 0x00002B57
		internal bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000495F File Offset: 0x00002B5F
		internal IList<MetadataProperty> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004967 File Offset: 0x00002B67
		internal virtual void SetReadOnly()
		{
			this._annotations.TrimExcess();
			this._readOnly = true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000497B File Offset: 0x00002B7B
		internal void ThrowIfReadOnly()
		{
			if (this.IsReadOnly)
			{
				throw new InvalidOperationException(Strings.OperationOnReadOnlyItem);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004990 File Offset: 0x00002B90
		internal static void SetReadOnly(MappingItem item)
		{
			if (item != null)
			{
				item.SetReadOnly();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000499C File Offset: 0x00002B9C
		internal static void SetReadOnly(IEnumerable<MappingItem> items)
		{
			if (items == null)
			{
				return;
			}
			foreach (MappingItem readOnly in items)
			{
				MappingItem.SetReadOnly(readOnly);
			}
		}

		// Token: 0x0400001A RID: 26
		private bool _readOnly;

		// Token: 0x0400001B RID: 27
		private readonly List<MetadataProperty> _annotations = new List<MetadataProperty>();
	}
}
