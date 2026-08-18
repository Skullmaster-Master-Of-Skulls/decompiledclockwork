using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005B4 RID: 1460
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class TypeDescriptionProvider
	{
		// Token: 0x06003672 RID: 13938 RVA: 0x000ED00F File Offset: 0x000EB20F
		protected TypeDescriptionProvider()
		{
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000ED017 File Offset: 0x000EB217
		protected TypeDescriptionProvider(TypeDescriptionProvider parent)
		{
			this._parent = parent;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000ED026 File Offset: 0x000EB226
		public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (this._parent != null)
			{
				return this._parent.CreateInstance(provider, objectType, argTypes, args);
			}
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			return SecurityUtils.SecureCreateInstance(objectType, args);
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000ED05D File Offset: 0x000EB25D
		public virtual IDictionary GetCache(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetCache(instance);
			}
			return null;
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000ED075 File Offset: 0x000EB275
		public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtendedTypeDescriptor(instance);
			}
			if (this._emptyDescriptor == null)
			{
				this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyDescriptor;
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000ED0A5 File Offset: 0x000EB2A5
		protected internal virtual IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtenderProviders(instance);
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return new IExtenderProvider[0];
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000ED0D0 File Offset: 0x000EB2D0
		public virtual string GetFullComponentName(object component)
		{
			if (this._parent != null)
			{
				return this._parent.GetFullComponentName(component);
			}
			return this.GetTypeDescriptor(component).GetComponentName();
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000ED0F3 File Offset: 0x000EB2F3
		public Type GetReflectionType(Type objectType)
		{
			return this.GetReflectionType(objectType, null);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000ED0FD File Offset: 0x000EB2FD
		public Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetReflectionType(instance.GetType(), instance);
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000ED11A File Offset: 0x000EB31A
		public virtual Type GetReflectionType(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetReflectionType(objectType, instance);
			}
			return objectType;
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000ED134 File Offset: 0x000EB334
		public virtual Type GetRuntimeType(Type reflectionType)
		{
			if (this._parent != null)
			{
				return this._parent.GetRuntimeType(reflectionType);
			}
			if (reflectionType == null)
			{
				throw new ArgumentNullException("reflectionType");
			}
			if (reflectionType.GetType().Assembly == typeof(object).Assembly)
			{
				return reflectionType;
			}
			return reflectionType.UnderlyingSystemType;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000ED193 File Offset: 0x000EB393
		public ICustomTypeDescriptor GetTypeDescriptor(Type objectType)
		{
			return this.GetTypeDescriptor(objectType, null);
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000ED19D File Offset: 0x000EB39D
		public ICustomTypeDescriptor GetTypeDescriptor(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetTypeDescriptor(instance.GetType(), instance);
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000ED1BA File Offset: 0x000EB3BA
		public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetTypeDescriptor(objectType, instance);
			}
			if (this._emptyDescriptor == null)
			{
				this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyDescriptor;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000ED1EB File Offset: 0x000EB3EB
		public virtual bool IsSupportedType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._parent == null || this._parent.IsSupportedType(type);
		}

		// Token: 0x04002AAC RID: 10924
		private TypeDescriptionProvider _parent;

		// Token: 0x04002AAD RID: 10925
		private TypeDescriptionProvider.EmptyCustomTypeDescriptor _emptyDescriptor;

		// Token: 0x020008A0 RID: 2208
		private sealed class EmptyCustomTypeDescriptor : CustomTypeDescriptor
		{
		}
	}
}
