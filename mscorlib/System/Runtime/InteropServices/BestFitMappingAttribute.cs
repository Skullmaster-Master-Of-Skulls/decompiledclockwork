using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000506 RID: 1286
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public sealed class BestFitMappingAttribute : Attribute
	{
		// Token: 0x060031A7 RID: 12711 RVA: 0x000A9972 File Offset: 0x000A8972
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this._bestFitMapping = BestFitMapping;
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x000A9981 File Offset: 0x000A8981
		public bool BestFitMapping
		{
			get
			{
				return this._bestFitMapping;
			}
		}

		// Token: 0x040019AE RID: 6574
		internal bool _bestFitMapping;

		// Token: 0x040019AF RID: 6575
		public bool ThrowOnUnmappableChar;
	}
}
