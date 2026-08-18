using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x0200094B RID: 2379
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceExposureAttribute : Attribute
	{
		// Token: 0x060055D5 RID: 21973 RVA: 0x00137685 File Offset: 0x00136685
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this._resourceExposureLevel = exposureLevel;
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x00137694 File Offset: 0x00136694
		public ResourceScope ResourceExposureLevel
		{
			get
			{
				return this._resourceExposureLevel;
			}
		}

		// Token: 0x04002CDD RID: 11485
		private ResourceScope _resourceExposureLevel;
	}
}
