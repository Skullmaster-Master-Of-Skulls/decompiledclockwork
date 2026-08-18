using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D5 RID: 981
	public class ComplexPropertyMapping : PropertyMapping
	{
		// Token: 0x060023BD RID: 9149 RVA: 0x000A5982 File Offset: 0x000A3B82
		public ComplexPropertyMapping(EdmProperty property) : base(property)
		{
			Check.NotNull<EdmProperty>(property, "property");
			if (!TypeSemantics.IsComplexType(property.TypeUsage))
			{
				throw new ArgumentException(Strings.StorageComplexPropertyMapping_OnlyComplexPropertyAllowed, "property");
			}
			this._typeMappings = new List<ComplexTypeMapping>();
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000A59BF File Offset: 0x000A3BBF
		public ReadOnlyCollection<ComplexTypeMapping> TypeMappings
		{
			get
			{
				return new ReadOnlyCollection<ComplexTypeMapping>(this._typeMappings);
			}
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x000A59CC File Offset: 0x000A3BCC
		public void AddTypeMapping(ComplexTypeMapping typeMapping)
		{
			Check.NotNull<ComplexTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Add(typeMapping);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000A59EC File Offset: 0x000A3BEC
		public void RemoveTypeMapping(ComplexTypeMapping typeMapping)
		{
			Check.NotNull<ComplexTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Remove(typeMapping);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000A5A0D File Offset: 0x000A3C0D
		internal override void SetReadOnly()
		{
			this._typeMappings.TrimExcess();
			MappingItem.SetReadOnly(this._typeMappings);
			base.SetReadOnly();
		}

		// Token: 0x04000C8D RID: 3213
		private readonly List<ComplexTypeMapping> _typeMappings;
	}
}
