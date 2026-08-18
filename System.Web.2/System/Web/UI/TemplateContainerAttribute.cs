using System;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x0200030B RID: 779
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TemplateContainerAttribute : Attribute
	{
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x00075A59 File Offset: 0x00073C59
		public BindingDirection BindingDirection
		{
			get
			{
				return this._bindingDirection;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00075A61 File Offset: 0x00073C61
		public Type ContainerType
		{
			get
			{
				return this._containerType;
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x00075A69 File Offset: 0x00073C69
		public TemplateContainerAttribute(Type containerType) : this(containerType, BindingDirection.OneWay)
		{
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00075A73 File Offset: 0x00073C73
		public TemplateContainerAttribute(Type containerType, BindingDirection bindingDirection)
		{
			this._containerType = containerType;
			this._bindingDirection = bindingDirection;
		}

		// Token: 0x04001CE0 RID: 7392
		private Type _containerType;

		// Token: 0x04001CE1 RID: 7393
		private BindingDirection _bindingDirection;
	}
}
