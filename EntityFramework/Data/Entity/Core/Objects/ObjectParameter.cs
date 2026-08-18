using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A4 RID: 1444
	public sealed class ObjectParameter
	{
		// Token: 0x06003919 RID: 14617 RVA: 0x0010FEF5 File Offset: 0x0010E0F5
		internal static bool ValidateParameterName(string name)
		{
			return DbCommandTree.IsValidParameterName(name);
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x0010FF00 File Offset: 0x0010E100
		public ObjectParameter(string name, Type type)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<Type>(type, "type");
			if (!ObjectParameter.ValidateParameterName(name))
			{
				throw new ArgumentException(Strings.ObjectParameter_InvalidParameterName(name), "name");
			}
			this._name = name;
			this._type = type;
			this._mappableType = TypeSystem.GetNonNullableType(this._type);
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x0010FF64 File Offset: 0x0010E164
		public ObjectParameter(string name, object value)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<object>(value, "value");
			if (!ObjectParameter.ValidateParameterName(name))
			{
				throw new ArgumentException(Strings.ObjectParameter_InvalidParameterName(name), "name");
			}
			this._name = name;
			this._type = value.GetType();
			this._value = value;
			this._mappableType = TypeSystem.GetNonNullableType(this._type);
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x0010FFD4 File Offset: 0x0010E1D4
		private ObjectParameter(ObjectParameter template)
		{
			this._name = template._name;
			this._type = template._type;
			this._mappableType = template._mappableType;
			this._effectiveType = template._effectiveType;
			this._value = template._value;
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x00110023 File Offset: 0x0010E223
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600391E RID: 14622 RVA: 0x0011002B File Offset: 0x0010E22B
		public Type ParameterType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x00110033 File Offset: 0x0010E233
		// (set) Token: 0x06003920 RID: 14624 RVA: 0x0011003B File Offset: 0x0010E23B
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x00110044 File Offset: 0x0010E244
		// (set) Token: 0x06003922 RID: 14626 RVA: 0x0011004C File Offset: 0x0010E24C
		internal TypeUsage TypeUsage
		{
			get
			{
				return this._effectiveType;
			}
			set
			{
				this._effectiveType = value;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x00110055 File Offset: 0x0010E255
		internal Type MappableType
		{
			get
			{
				return this._mappableType;
			}
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x0011005D File Offset: 0x0010E25D
		internal ObjectParameter ShallowCopy()
		{
			return new ObjectParameter(this);
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x00110068 File Offset: 0x0010E268
		internal bool ValidateParameterType(ClrPerspective perspective)
		{
			TypeUsage type;
			return perspective.TryGetType(this._mappableType, out type) && TypeSemantics.IsScalarType(type);
		}

		// Token: 0x040015D8 RID: 5592
		private readonly string _name;

		// Token: 0x040015D9 RID: 5593
		private readonly Type _type;

		// Token: 0x040015DA RID: 5594
		private readonly Type _mappableType;

		// Token: 0x040015DB RID: 5595
		private TypeUsage _effectiveType;

		// Token: 0x040015DC RID: 5596
		private object _value;
	}
}
