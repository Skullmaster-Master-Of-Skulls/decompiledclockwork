using System;
using System.Reflection;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001EC RID: 492
	public sealed class EdmProperty : EdmMember
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x000728DD File Offset: 0x00070ADD
		internal EdmProperty(string name, TypeUsage typeUsage) : base(name, typeUsage)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00072900 File Offset: 0x00070B00
		internal EdmProperty(string name, TypeUsage typeUsage, PropertyInfo propertyInfo, RuntimeTypeHandle entityDeclaringType) : this(name, typeUsage)
		{
			if (null != propertyInfo)
			{
				MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
				this.PropertyGetterHandle = ((null != methodInfo) ? methodInfo.MethodHandle : default(RuntimeMethodHandle));
				methodInfo = propertyInfo.GetSetMethod(true);
				this.PropertySetterHandle = ((null != methodInfo) ? methodInfo.MethodHandle : default(RuntimeMethodHandle));
				this.EntityDeclaringType = entityDeclaringType;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00072976 File Offset: 0x00070B76
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EdmProperty;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0007297A File Offset: 0x00070B7A
		public bool Nullable
		{
			get
			{
				return (bool)base.TypeUsage.Facets["Nullable"].Value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0007299B File Offset: 0x00070B9B
		public object DefaultValue
		{
			get
			{
				return base.TypeUsage.Facets["DefaultValue"].Value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x000729B7 File Offset: 0x00070BB7
		// (set) Token: 0x060020D5 RID: 8405 RVA: 0x000729BF File Offset: 0x00070BBF
		internal Func<object, object> ValueGetter
		{
			get
			{
				return this._memberGetter;
			}
			set
			{
				Interlocked.CompareExchange<Func<object, object>>(ref this._memberGetter, value, null);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x000729CF File Offset: 0x00070BCF
		// (set) Token: 0x060020D7 RID: 8407 RVA: 0x000729D7 File Offset: 0x00070BD7
		internal Action<object, object> ValueSetter
		{
			get
			{
				return this._memberSetter;
			}
			set
			{
				Interlocked.CompareExchange<Action<object, object>>(ref this._memberSetter, value, null);
			}
		}

		// Token: 0x04000E8B RID: 3723
		internal readonly RuntimeMethodHandle PropertyGetterHandle;

		// Token: 0x04000E8C RID: 3724
		internal readonly RuntimeMethodHandle PropertySetterHandle;

		// Token: 0x04000E8D RID: 3725
		internal readonly RuntimeTypeHandle EntityDeclaringType;

		// Token: 0x04000E8E RID: 3726
		private Func<object, object> _memberGetter;

		// Token: 0x04000E8F RID: 3727
		private Action<object, object> _memberSetter;
	}
}
