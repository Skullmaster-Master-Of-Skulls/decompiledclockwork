using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000568 RID: 1384
	[Serializable]
	public sealed class VariantWrapper
	{
		// Token: 0x060033BD RID: 13245 RVA: 0x000ADD33 File Offset: 0x000ACD33
		public VariantWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000ADD42 File Offset: 0x000ACD42
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04001B0F RID: 6927
		private object m_WrappedObject;
	}
}
