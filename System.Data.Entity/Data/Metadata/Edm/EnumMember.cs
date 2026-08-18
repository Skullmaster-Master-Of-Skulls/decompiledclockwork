using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001D3 RID: 467
	public sealed class EnumMember : MetadataItem
	{
		// Token: 0x06001FC1 RID: 8129 RVA: 0x0006F354 File Offset: 0x0006D554
		internal EnumMember(string name, object value) : base(MetadataItem.MetadataFlags.Readonly)
		{
			EntityUtil.CheckStringArgument(name, "name");
			this._name = name;
			this._value = value;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x0001793B File Offset: 0x00015B3B
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumMember;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0006F376 File Offset: 0x0006D576
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0006F37E File Offset: 0x0006D57E
		[MetadataProperty(BuiltInTypeKind.PrimitiveType, false)]
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0006F386 File Offset: 0x0006D586
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0006F386 File Offset: 0x0006D586
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000E09 RID: 3593
		private readonly string _name;

		// Token: 0x04000E0A RID: 3594
		private readonly object _value;
	}
}
