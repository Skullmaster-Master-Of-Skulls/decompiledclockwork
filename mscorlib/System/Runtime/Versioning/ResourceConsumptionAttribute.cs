using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x0200094A RID: 2378
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		// Token: 0x060055D1 RID: 21969 RVA: 0x00137644 File Offset: 0x00136644
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = this._resourceScope;
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x0013765F File Offset: 0x0013665F
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this._resourceScope = resourceScope;
			this._consumptionScope = consumptionScope;
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x060055D3 RID: 21971 RVA: 0x00137675 File Offset: 0x00136675
		public ResourceScope ResourceScope
		{
			get
			{
				return this._resourceScope;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x060055D4 RID: 21972 RVA: 0x0013767D File Offset: 0x0013667D
		public ResourceScope ConsumptionScope
		{
			get
			{
				return this._consumptionScope;
			}
		}

		// Token: 0x04002CDB RID: 11483
		private ResourceScope _consumptionScope;

		// Token: 0x04002CDC RID: 11484
		private ResourceScope _resourceScope;
	}
}
