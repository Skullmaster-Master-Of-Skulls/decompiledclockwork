using System;
using System.Globalization;
using System.Linq;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D4 RID: 468
	public class EnumType : SimpleType
	{
		// Token: 0x06001FC7 RID: 8135 RVA: 0x0006F38E File Offset: 0x0006D58E
		internal EnumType()
		{
			this._underlyingType = PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32);
			this._isFlags = false;
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0006F3BA File Offset: 0x0006D5BA
		internal EnumType(string name, string namespaceName, PrimitiveType underlyingType, bool isFlags, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._isFlags = isFlags;
			this._underlyingType = underlyingType;
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0006F3E8 File Offset: 0x0006D5E8
		internal EnumType(Type clrType) : base(clrType.Name, clrType.Namespace ?? string.Empty, DataSpace.OSpace)
		{
			ClrProviderManifest.Instance.TryGetPrimitiveType(clrType.GetEnumUnderlyingType(), out this._underlyingType);
			this._isFlags = clrType.GetCustomAttributes(typeof(FlagsAttribute), false).Any<object>();
			foreach (string text in Enum.GetNames(clrType))
			{
				this.AddMember(new EnumMember(text, Convert.ChangeType(Enum.Parse(clrType, text), clrType.GetEnumUnderlyingType(), CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x00017AC8 File Offset: 0x00015CC8
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumType;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0006F490 File Offset: 0x0006D690
		[MetadataProperty(BuiltInTypeKind.EnumMember, true)]
		public ReadOnlyMetadataCollection<EnumMember> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0006F498 File Offset: 0x0006D698
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsFlags
		{
			get
			{
				return this._isFlags;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0006F4A0 File Offset: 0x0006D6A0
		[MetadataProperty(BuiltInTypeKind.PrimitiveType, false)]
		public PrimitiveType UnderlyingType
		{
			get
			{
				return this._underlyingType;
			}
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0006F4A8 File Offset: 0x0006D6A8
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.Members.Source.SetReadOnly();
			}
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0006F4C9 File Offset: 0x0006D6C9
		internal void AddMember(EnumMember enumMember)
		{
			this.Members.Source.Add(enumMember);
		}

		// Token: 0x04000E0B RID: 3595
		private readonly ReadOnlyMetadataCollection<EnumMember> _members = new ReadOnlyMetadataCollection<EnumMember>(new MetadataCollection<EnumMember>());

		// Token: 0x04000E0C RID: 3596
		private readonly bool _isFlags;

		// Token: 0x04000E0D RID: 3597
		private readonly PrimitiveType _underlyingType;
	}
}
