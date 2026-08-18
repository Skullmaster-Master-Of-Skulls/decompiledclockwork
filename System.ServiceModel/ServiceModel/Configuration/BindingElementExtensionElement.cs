using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F6 RID: 1526
	public abstract class BindingElementExtensionElement : ServiceModelExtensionElement
	{
		// Token: 0x06003AC3 RID: 15043 RVA: 0x000E1A96 File Offset: 0x000DFC96
		public virtual void ApplyConfiguration(BindingElement bindingElement)
		{
			if (bindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElement");
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003AC4 RID: 15044
		public abstract Type BindingElementType { get; }

		// Token: 0x06003AC5 RID: 15045
		protected internal abstract BindingElement CreateBindingElement();

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000E1AAC File Offset: 0x000DFCAC
		protected internal virtual void InitializeFrom(BindingElement bindingElement)
		{
			if (bindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("bindingElement");
			}
			if (bindingElement.GetType() != this.BindingElementType)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("bindingElement", SR.GetString("ConfigInvalidTypeForBindingElement", new object[]
				{
					this.BindingElementType.ToString(),
					bindingElement.GetType().ToString()
				}));
			}
		}
	}
}
