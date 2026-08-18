using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004FE RID: 1278
	public class RowType : StructuralType
	{
		// Token: 0x06002F85 RID: 12165 RVA: 0x000E45F6 File Offset: 0x000E27F6
		internal RowType()
		{
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000E45FE File Offset: 0x000E27FE
		internal RowType(IEnumerable<EdmProperty> properties) : this(properties, null)
		{
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000E4608 File Offset: 0x000E2808
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		internal RowType(IEnumerable<EdmProperty> properties, InitializerMetadata initializerMetadata) : base(RowType.GetRowTypeIdentityFromProperties(RowType.CheckProperties(properties), initializerMetadata), "Transient", (DataSpace)(-1))
		{
			if (properties != null)
			{
				foreach (EdmProperty property in properties)
				{
					this.AddProperty(property);
				}
			}
			this._initializerMetadata = initializerMetadata;
			this.SetReadOnly();
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000E4678 File Offset: 0x000E2878
		internal InitializerMetadata InitializerMetadata
		{
			get
			{
				return this._initializerMetadata;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000E4680 File Offset: 0x000E2880
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RowType;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x000E4684 File Offset: 0x000E2884
		public virtual ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty)), null);
				}
				return this._properties;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x000E46B8 File Offset: 0x000E28B8
		public ReadOnlyMetadataCollection<EdmProperty> DeclaredProperties
		{
			get
			{
				return base.GetDeclaredOnlyMembers<EdmProperty>();
			}
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000E46C0 File Offset: 0x000E28C0
		private void AddProperty(EdmProperty property)
		{
			Check.NotNull<EdmProperty>(property, "property");
			base.AddMember(property);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000E46D5 File Offset: 0x000E28D5
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000E46D8 File Offset: 0x000E28D8
		private static string GetRowTypeIdentityFromProperties(IEnumerable<EdmProperty> properties, InitializerMetadata initializerMetadata)
		{
			StringBuilder stringBuilder = new StringBuilder("rowtype[");
			if (properties != null)
			{
				int num = 0;
				foreach (EdmProperty edmProperty in properties)
				{
					if (num > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("(");
					stringBuilder.Append(edmProperty.Name);
					stringBuilder.Append(",");
					edmProperty.TypeUsage.BuildIdentity(stringBuilder);
					stringBuilder.Append(")");
					num++;
				}
			}
			stringBuilder.Append("]");
			if (initializerMetadata != null)
			{
				stringBuilder.Append(",").Append(initializerMetadata.Identity);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000E47A8 File Offset: 0x000E29A8
		private static IEnumerable<EdmProperty> CheckProperties(IEnumerable<EdmProperty> properties)
		{
			if (properties != null)
			{
				int num = 0;
				using (IEnumerator<EdmProperty> enumerator = properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("properties"));
						}
						num++;
					}
				}
			}
			return properties;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000E4808 File Offset: 0x000E2A08
		internal override bool EdmEquals(MetadataItem item)
		{
			if (object.ReferenceEquals(this, item))
			{
				return true;
			}
			if (item == null || BuiltInTypeKind.RowType != item.BuiltInTypeKind)
			{
				return false;
			}
			RowType rowType = (RowType)item;
			if (base.Members.Count != rowType.Members.Count)
			{
				return false;
			}
			for (int i = 0; i < base.Members.Count; i++)
			{
				EdmMember edmMember = base.Members[i];
				EdmMember edmMember2 = rowType.Members[i];
				if (!edmMember.EdmEquals(edmMember2) || !edmMember.TypeUsage.EdmEquals(edmMember2.TypeUsage))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000E48A0 File Offset: 0x000E2AA0
		public static RowType Create(IEnumerable<EdmProperty> properties, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotNull<IEnumerable<EdmProperty>>(properties, "properties");
			RowType rowType = new RowType(properties);
			if (metadataProperties != null)
			{
				rowType.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			rowType.SetReadOnly();
			return rowType;
		}

		// Token: 0x0400122A RID: 4650
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x0400122B RID: 4651
		private readonly InitializerMetadata _initializerMetadata;
	}
}
