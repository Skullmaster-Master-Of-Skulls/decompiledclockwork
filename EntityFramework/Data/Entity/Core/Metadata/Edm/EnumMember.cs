using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E1 RID: 1249
	public sealed class EnumMember : MetadataItem
	{
		// Token: 0x06002E64 RID: 11876 RVA: 0x000DE858 File Offset: 0x000DCA58
		internal EnumMember(string name, object value) : base(MetadataItem.MetadataFlags.Readonly)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._value = value;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000DE87B File Offset: 0x000DCA7B
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumMember;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x000DE87F File Offset: 0x000DCA7F
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000DE887 File Offset: 0x000DCA87
		[MetadataProperty(BuiltInTypeKind.PrimitiveType, false)]
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000DE88F File Offset: 0x000DCA8F
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000DE897 File Offset: 0x000DCA97
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000DE89F File Offset: 0x000DCA9F
		[CLSCompliant(false)]
		public static EnumMember Create(string name, sbyte value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000DE8BA File Offset: 0x000DCABA
		public static EnumMember Create(string name, byte value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000DE8D5 File Offset: 0x000DCAD5
		public static EnumMember Create(string name, short value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000DE8F0 File Offset: 0x000DCAF0
		public static EnumMember Create(string name, int value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000DE90B File Offset: 0x000DCB0B
		public static EnumMember Create(string name, long value, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			return EnumMember.CreateInternal(name, value, metadataProperties);
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000DE928 File Offset: 0x000DCB28
		private static EnumMember CreateInternal(string name, object value, IEnumerable<MetadataProperty> metadataProperties)
		{
			EnumMember enumMember = new EnumMember(name, value);
			if (metadataProperties != null)
			{
				enumMember.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			enumMember.SetReadOnly();
			return enumMember;
		}

		// Token: 0x040011A8 RID: 4520
		private readonly string _name;

		// Token: 0x040011A9 RID: 4521
		private readonly object _value;
	}
}
