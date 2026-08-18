using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200000E RID: 14
	public sealed class FunctionImportResultMapping : MappingItem
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000049FB File Offset: 0x00002BFB
		public ReadOnlyCollection<FunctionImportStructuralTypeMapping> TypeMappings
		{
			get
			{
				return new ReadOnlyCollection<FunctionImportStructuralTypeMapping>(this._typeMappings);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004A08 File Offset: 0x00002C08
		public void AddTypeMapping(FunctionImportStructuralTypeMapping typeMapping)
		{
			Check.NotNull<FunctionImportStructuralTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Add(typeMapping);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004A28 File Offset: 0x00002C28
		public void RemoveTypeMapping(FunctionImportStructuralTypeMapping typeMapping)
		{
			Check.NotNull<FunctionImportStructuralTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Remove(typeMapping);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004A49 File Offset: 0x00002C49
		internal override void SetReadOnly()
		{
			this._typeMappings.TrimExcess();
			MappingItem.SetReadOnly(this._typeMappings);
			base.SetReadOnly();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004A67 File Offset: 0x00002C67
		internal List<FunctionImportStructuralTypeMapping> SourceList
		{
			get
			{
				return this._typeMappings;
			}
		}

		// Token: 0x0400001C RID: 28
		private readonly List<FunctionImportStructuralTypeMapping> _typeMappings = new List<FunctionImportStructuralTypeMapping>();
	}
}
