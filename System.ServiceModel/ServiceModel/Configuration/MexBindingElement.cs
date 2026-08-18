using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000628 RID: 1576
	public abstract class MexBindingElement<TStandardBinding> : StandardBindingElement where TStandardBinding : Binding
	{
		// Token: 0x06003C75 RID: 15477 RVA: 0x000E6D91 File Offset: 0x000E4F91
		protected MexBindingElement(string name) : base(name)
		{
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x000E6D9A File Offset: 0x000E4F9A
		protected override Type BindingElementType
		{
			get
			{
				return typeof(TStandardBinding);
			}
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000E6DA6 File Offset: 0x000E4FA6
		protected override void OnApplyConfiguration(Binding binding)
		{
		}
	}
}
