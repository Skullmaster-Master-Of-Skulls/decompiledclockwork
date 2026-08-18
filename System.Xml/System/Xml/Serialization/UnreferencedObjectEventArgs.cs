using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000347 RID: 839
	public class UnreferencedObjectEventArgs : EventArgs
	{
		// Token: 0x060028D1 RID: 10449 RVA: 0x000D1F22 File Offset: 0x000D0F22
		public UnreferencedObjectEventArgs(object o, string id)
		{
			this.o = o;
			this.id = id;
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x000D1F38 File Offset: 0x000D0F38
		public object UnreferencedObject
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x000D1F40 File Offset: 0x000D0F40
		public string UnreferencedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x0400169B RID: 5787
		private object o;

		// Token: 0x0400169C RID: 5788
		private string id;
	}
}
