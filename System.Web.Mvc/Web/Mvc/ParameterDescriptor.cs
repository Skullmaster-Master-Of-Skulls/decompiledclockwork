using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x020001B1 RID: 433
	public abstract class ParameterDescriptor : ICustomAttributeProvider
	{
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000C3A RID: 3130
		public abstract ActionDescriptor ActionDescriptor { get; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000208CB File Offset: 0x0001EACB
		public virtual ParameterBindingInfo BindingInfo
		{
			get
			{
				return ParameterDescriptor._emptyBindingInfo;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000208D2 File Offset: 0x0001EAD2
		public virtual object DefaultValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000C3D RID: 3133
		public abstract string ParameterName { get; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C3E RID: 3134
		public abstract Type ParameterType { get; }

		// Token: 0x06000C3F RID: 3135 RVA: 0x000208D5 File Offset: 0x0001EAD5
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return this.GetCustomAttributes(typeof(object), inherit);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000208E8 File Offset: 0x0001EAE8
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return (object[])Array.CreateInstance(attributeType, 0);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002090A File Offset: 0x0001EB0A
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		// Token: 0x0400034A RID: 842
		private static readonly ParameterDescriptor.EmptyParameterBindingInfo _emptyBindingInfo = new ParameterDescriptor.EmptyParameterBindingInfo();

		// Token: 0x020001B2 RID: 434
		private sealed class EmptyParameterBindingInfo : ParameterBindingInfo
		{
		}
	}
}
