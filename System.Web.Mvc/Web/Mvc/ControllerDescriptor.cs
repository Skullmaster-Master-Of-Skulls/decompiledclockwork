using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000120 RID: 288
	public abstract class ControllerDescriptor : ICustomAttributeProvider, IUniquelyIdentifiable
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x00014A0A File Offset: 0x00012C0A
		protected ControllerDescriptor()
		{
			this._uniqueId = new Lazy<string>(new Func<string>(this.CreateUniqueId));
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00014A2C File Offset: 0x00012C2C
		public virtual string ControllerName
		{
			get
			{
				string name = this.ControllerType.Name;
				if (name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
				{
					return name.Substring(0, name.Length - "Controller".Length);
				}
				return name;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000795 RID: 1941
		public abstract Type ControllerType { get; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00014A6D File Offset: 0x00012C6D
		public virtual string UniqueId
		{
			get
			{
				return this._uniqueId.Value;
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00014A7A File Offset: 0x00012C7A
		private string CreateUniqueId()
		{
			return DescriptorUtil.CreateUniqueId(base.GetType(), this.ControllerName, this.ControllerType);
		}

		// Token: 0x06000798 RID: 1944
		public abstract ActionDescriptor FindAction(ControllerContext controllerContext, string actionName);

		// Token: 0x06000799 RID: 1945
		public abstract ActionDescriptor[] GetCanonicalActions();

		// Token: 0x0600079A RID: 1946 RVA: 0x00014A93 File Offset: 0x00012C93
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return this.GetCustomAttributes(typeof(object), inherit);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00014AA6 File Offset: 0x00012CA6
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return (object[])Array.CreateInstance(attributeType, 0);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public virtual IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
		{
			return this.GetCustomAttributes(typeof(FilterAttribute), true).Cast<FilterAttribute>();
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00014AE0 File Offset: 0x00012CE0
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		// Token: 0x0400021C RID: 540
		private readonly Lazy<string> _uniqueId;
	}
}
