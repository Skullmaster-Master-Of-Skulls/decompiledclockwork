using System;
using System.Collections;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x020004A5 RID: 1189
	internal class GetAttributesEvent : EventArgs
	{
		// Token: 0x06004F2C RID: 20268 RVA: 0x001463D5 File Offset: 0x001445D5
		public GetAttributesEvent(ArrayList attrList)
		{
			this.attrList = attrList;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x001463E4 File Offset: 0x001445E4
		public void Add(Attribute attribute)
		{
			this.attrList.Add(attribute);
		}

		// Token: 0x0400344C RID: 13388
		private ArrayList attrList;
	}
}
