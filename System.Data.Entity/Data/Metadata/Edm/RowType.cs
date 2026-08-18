using System;
using System.Collections.Generic;
using System.Data.Objects.ELinq;
using System.Text;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F6 RID: 502
	public sealed class RowType : StructuralType
	{
		// Token: 0x0600211D RID: 8477 RVA: 0x000748AD File Offset: 0x00072AAD
		internal RowType(IEnumerable<EdmProperty> properties) : this(properties, null)
		{
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000748B8 File Offset: 0x00072AB8
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

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x00074928 File Offset: 0x00072B28
		internal InitializerMetadata InitializerMetadata
		{
			get
			{
				return this._initializerMetadata;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x00074930 File Offset: 0x00072B30
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RowType;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x00074934 File Offset: 0x00072B34
		public ReadOnlyMetadataCollection<EdmProperty> Properties
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

		// Token: 0x06002122 RID: 8482 RVA: 0x00074968 File Offset: 0x00072B68
		private void AddProperty(EdmProperty property)
		{
			EntityUtil.GenericCheckArgumentNull<EdmProperty>(property, "property");
			base.AddMember(property);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00074980 File Offset: 0x00072B80
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

		// Token: 0x06002125 RID: 8485 RVA: 0x00074A50 File Offset: 0x00072C50
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
							throw EntityUtil.CollectionParameterElementIsNull("properties");
						}
						num++;
					}
				}
			}
			return properties;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00074AAC File Offset: 0x00072CAC
		internal override bool EdmEquals(MetadataItem item)
		{
			if (this == item)
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

		// Token: 0x04000EA9 RID: 3753
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x04000EAA RID: 3754
		private readonly InitializerMetadata _initializerMetadata;
	}
}
