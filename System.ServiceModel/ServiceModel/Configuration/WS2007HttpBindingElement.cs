using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E6 RID: 1766
	public class WS2007HttpBindingElement : WSHttpBindingElement
	{
		// Token: 0x06004402 RID: 17410 RVA: 0x00100E6A File Offset: 0x000FF06A
		public WS2007HttpBindingElement(string name) : base(name)
		{
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x00100E73 File Offset: 0x000FF073
		public WS2007HttpBindingElement() : this(null)
		{
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x00100E7C File Offset: 0x000FF07C
		protected override Type BindingElementType
		{
			get
			{
				return typeof(WS2007HttpBinding);
			}
		}
	}
}
