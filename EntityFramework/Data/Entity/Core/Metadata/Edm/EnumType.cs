using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CF RID: 1231
	public class EnumType : SimpleType
	{
		// Token: 0x06002D77 RID: 11639 RVA: 0x000DC0F3 File Offset: 0x000DA2F3
		internal EnumType()
		{
			this._underlyingType = PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32);
			this._isFlags = false;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000DC11F File Offset: 0x000DA31F
		internal EnumType(string name, string namespaceName, PrimitiveType underlyingType, bool isFlags, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._isFlags = isFlags;
			this._underlyingType = underlyingType;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000DC14C File Offset: 0x000DA34C
		internal EnumType(Type clrType) : base(clrType.Name, clrType.NestingNamespace() ?? string.Empty, DataSpace.OSpace)
		{
			ClrProviderManifest.Instance.TryGetPrimitiveType(clrType.GetEnumUnderlyingType(), out this._underlyingType);
			this._isFlags = clrType.GetCustomAttributes(false).Any<FlagsAttribute>();
			foreach (string text in Enum.GetNames(clrType))
			{
				this.AddMember(new EnumMember(text, Convert.ChangeType(Enum.Parse(clrType, text), clrType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000DC1EA File Offset: 0x000DA3EA
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumType;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000DC1EE File Offset: 0x000DA3EE
		[MetadataProperty(BuiltInTypeKind.EnumMember, true)]
		public ReadOnlyMetadataCollection<EnumMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000DC1F6 File Offset: 0x000DA3F6
		// (set) Token: 0x06002D7D RID: 11645 RVA: 0x000DC1FE File Offset: 0x000DA3FE
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
		public bool IsFlags
		{
			get
			{
				return this._isFlags;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._isFlags = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000DC20D File Offset: 0x000DA40D
		// (set) Token: 0x06002D7F RID: 11647 RVA: 0x000DC215 File Offset: 0x000DA415
		[MetadataProperty(BuiltInTypeKind.PrimitiveType, false)]
		public PrimitiveType UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._underlyingType = value;
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000DC224 File Offset: 0x000DA424
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Members.Source.SetReadOnly();
			}
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000DC245 File Offset: 0x000DA445
		internal void AddMember(EnumMember enumMember)
		{
			this.Members.Source.Add(enumMember);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000DC258 File Offset: 0x000DA458
		[SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags")]
		public static EnumType Create(string name, string namespaceName, PrimitiveType underlyingType, bool isFlags, IEnumerable<EnumMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotNull<PrimitiveType>(underlyingType, "underlyingType");
			if (!Helper.IsSupportedEnumUnderlyingType(underlyingType.PrimitiveTypeKind))
			{
				throw new ArgumentException(Strings.InvalidEnumUnderlyingType, "underlyingType");
			}
			EnumType enumType = new EnumType(name, namespaceName, underlyingType, isFlags, DataSpace.CSpace);
			if (members != null)
			{
				foreach (EnumMember enumMember in members)
				{
					if (!Helper.IsEnumMemberValueInRange(underlyingType.PrimitiveTypeKind, Convert.ToInt64(enumMember.Value, CultureInfo.InvariantCulture)))
					{
						throw new ArgumentException(Strings.EnumMemberValueOutOfItsUnderylingTypeRange(enumMember.Value, enumMember.Name, underlyingType.Name), "members");
					}
					enumType.AddMember(enumMember);
				}
			}
			if (metadataProperties != null)
			{
				enumType.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			enumType.SetReadOnly();
			return enumType;
		}

		// Token: 0x040010AC RID: 4268
		private readonly ReadOnlyMetadataCollection<EnumMember> _members = new ReadOnlyMetadataCollection<EnumMember>(new MetadataCollection<EnumMember>());

		// Token: 0x040010AD RID: 4269
		private PrimitiveType _underlyingType;

		// Token: 0x040010AE RID: 4270
		private bool _isFlags;
	}
}
