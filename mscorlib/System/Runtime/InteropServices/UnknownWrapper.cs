using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000567 RID: 1383
	[ComVisible(true)]
	[Serializable]
	public sealed class UnknownWrapper
	{
		// Token: 0x060033BB RID: 13243 RVA: 0x000ADD1C File Offset: 0x000ACD1C
		public UnknownWrapper(object obj)
		{
			this.m_WrappedObject = obj;
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000ADD2B File Offset: 0x000ACD2B
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04001B0E RID: 6926
		private object m_WrappedObject;
	}
}
