using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001C8 RID: 456
	public class UnreferencedObjectEventArgs : EventArgs
	{
		// Token: 0x06001F1C RID: 7964 RVA: 0x000A916E File Offset: 0x000A736E
		public UnreferencedObjectEventArgs(object o, string id)
		{
			this.o = o;
			this.id = id;
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000A9184 File Offset: 0x000A7384
		public object UnreferencedObject
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x000A918C File Offset: 0x000A738C
		public string UnreferencedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000D02 RID: 3330
		private object o;

		// Token: 0x04000D03 RID: 3331
		private string id;
	}
}
