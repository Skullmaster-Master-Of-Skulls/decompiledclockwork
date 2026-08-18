using System;
using System.Data.Common.CommandTrees;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;

namespace System.Data.Objects
{
	// Token: 0x02000145 RID: 325
	public sealed class ObjectParameter
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x0004F4C7 File Offset: 0x0004D6C7
		internal static bool ValidateParameterName(string name)
		{
			return DbCommandTree.IsValidParameterName(name);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0004F4D0 File Offset: 0x0004D6D0
		public ObjectParameter(string name, Type type)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			EntityUtil.CheckArgumentNull<Type>(type, "type");
			if (!ObjectParameter.ValidateParameterName(name))
			{
				throw EntityUtil.Argument(Strings.ObjectParameter_InvalidParameterName(name), "name");
			}
			this._name = name;
			this._type = type;
			this._mappableType = TypeSystem.GetNonNullableType(this._type);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0004F534 File Offset: 0x0004D734
		public ObjectParameter(string name, object value)
		{
			EntityUtil.CheckArgumentNull<string>(name, "name");
			EntityUtil.CheckArgumentNull<object>(value, "value");
			if (!ObjectParameter.ValidateParameterName(name))
			{
				throw EntityUtil.Argument(Strings.ObjectParameter_InvalidParameterName(name), "name");
			}
			this._name = name;
			this._type = value.GetType();
			this._value = value;
			this._mappableType = TypeSystem.GetNonNullableType(this._type);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0004F5A4 File Offset: 0x0004D7A4
		private ObjectParameter(ObjectParameter template)
		{
			this._name = template._name;
			this._type = template._type;
			this._mappableType = template._mappableType;
			this._effectiveType = template._effectiveType;
			this._value = template._value;
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0004F5F3 File Offset: 0x0004D7F3
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0004F5FB File Offset: 0x0004D7FB
		public Type ParameterType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0004F603 File Offset: 0x0004D803
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x0004F60B File Offset: 0x0004D80B
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0004F614 File Offset: 0x0004D814
		// (set) Token: 0x06001790 RID: 6032 RVA: 0x0004F61C File Offset: 0x0004D81C
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

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0004F625 File Offset: 0x0004D825
		internal Type MappableType
		{
			get
			{
				return this._mappableType;
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0004F62D File Offset: 0x0004D82D
		internal ObjectParameter ShallowCopy()
		{
			return new ObjectParameter(this);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0004F638 File Offset: 0x0004D838
		internal bool ValidateParameterType(ClrPerspective perspective)
		{
			TypeUsage type = null;
			return perspective.TryGetType(this._mappableType, out type) && TypeSemantics.IsScalarType(type);
		}

		// Token: 0x04000A8E RID: 2702
		private string _name;

		// Token: 0x04000A8F RID: 2703
		private Type _type;

		// Token: 0x04000A90 RID: 2704
		private Type _mappableType;

		// Token: 0x04000A91 RID: 2705
		private TypeUsage _effectiveType;

		// Token: 0x04000A92 RID: 2706
		private object _value;
	}
}
