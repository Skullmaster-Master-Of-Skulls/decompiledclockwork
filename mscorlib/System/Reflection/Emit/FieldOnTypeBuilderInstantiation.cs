using System;
using System.Collections;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x02000853 RID: 2131
	internal sealed class FieldOnTypeBuilderInstantiation : FieldInfo
	{
		// Token: 0x06004E2D RID: 20013 RVA: 0x0010F7D8 File Offset: 0x0010E7D8
		internal static FieldInfo GetField(FieldInfo Field, TypeBuilderInstantiation type)
		{
			FieldOnTypeBuilderInstantiation.Entry entry = new FieldOnTypeBuilderInstantiation.Entry(Field, type);
			if (FieldOnTypeBuilderInstantiation.m_hashtable.Contains(entry))
			{
				return FieldOnTypeBuilderInstantiation.m_hashtable[entry] as FieldInfo;
			}
			FieldInfo fieldInfo = new FieldOnTypeBuilderInstantiation(Field, type);
			FieldOnTypeBuilderInstantiation.m_hashtable[entry] = fieldInfo;
			return fieldInfo;
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x0010F830 File Offset: 0x0010E830
		internal FieldOnTypeBuilderInstantiation(FieldInfo field, TypeBuilderInstantiation type)
		{
			this.m_field = field;
			this.m_type = type;
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06004E2F RID: 20015 RVA: 0x0010F846 File Offset: 0x0010E846
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x0010F84E File Offset: 0x0010E84E
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x0010F851 File Offset: 0x0010E851
		public override string Name
		{
			get
			{
				return this.m_field.Name;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x0010F85E File Offset: 0x0010E85E
		public override Type DeclaringType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06004E33 RID: 20019 RVA: 0x0010F866 File Offset: 0x0010E866
		public override Type ReflectedType
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x0010F86E File Offset: 0x0010E86E
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.m_field.GetCustomAttributes(inherit);
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x0010F87C File Offset: 0x0010E87C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.m_field.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0010F88B File Offset: 0x0010E88B
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.m_field.IsDefined(attributeType, inherit);
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06004E37 RID: 20023 RVA: 0x0010F89A File Offset: 0x0010E89A
		internal override int MetadataTokenInternal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x0010F8A1 File Offset: 0x0010E8A1
		public override Module Module
		{
			get
			{
				return this.m_field.Module;
			}
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x0010F8AE File Offset: 0x0010E8AE
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x0010F8B6 File Offset: 0x0010E8B6
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.m_field.GetRequiredCustomModifiers();
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x0010F8C3 File Offset: 0x0010E8C3
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.m_field.GetOptionalCustomModifiers();
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x0010F8D0 File Offset: 0x0010E8D0
		public override void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x0010F8D7 File Offset: 0x0010E8D7
		public override object GetValueDirect(TypedReference obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x0010F8DE File Offset: 0x0010E8DE
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06004E3F RID: 20031 RVA: 0x0010F8E5 File Offset: 0x0010E8E5
		public override Type FieldType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0010F8EC File Offset: 0x0010E8EC
		public override object GetValue(object obj)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x0010F8F3 File Offset: 0x0010E8F3
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06004E42 RID: 20034 RVA: 0x0010F8FA File Offset: 0x0010E8FA
		public override FieldAttributes Attributes
		{
			get
			{
				return this.m_field.Attributes;
			}
		}

		// Token: 0x04002855 RID: 10325
		private static Hashtable m_hashtable = new Hashtable();

		// Token: 0x04002856 RID: 10326
		private FieldInfo m_field;

		// Token: 0x04002857 RID: 10327
		private TypeBuilderInstantiation m_type;

		// Token: 0x02000854 RID: 2132
		private struct Entry
		{
			// Token: 0x06004E44 RID: 20036 RVA: 0x0010F913 File Offset: 0x0010E913
			public Entry(FieldInfo Field, TypeBuilderInstantiation type)
			{
				this.m_field = Field;
				this.m_type = type;
			}

			// Token: 0x06004E45 RID: 20037 RVA: 0x0010F923 File Offset: 0x0010E923
			public override int GetHashCode()
			{
				return this.m_field.GetHashCode();
			}

			// Token: 0x06004E46 RID: 20038 RVA: 0x0010F930 File Offset: 0x0010E930
			public override bool Equals(object o)
			{
				return o is FieldOnTypeBuilderInstantiation.Entry && this.Equals((FieldOnTypeBuilderInstantiation.Entry)o);
			}

			// Token: 0x06004E47 RID: 20039 RVA: 0x0010F948 File Offset: 0x0010E948
			public bool Equals(FieldOnTypeBuilderInstantiation.Entry obj)
			{
				return obj.m_field == this.m_field && obj.m_type == this.m_type;
			}

			// Token: 0x04002858 RID: 10328
			public FieldInfo m_field;

			// Token: 0x04002859 RID: 10329
			public TypeBuilderInstantiation m_type;
		}
	}
}
