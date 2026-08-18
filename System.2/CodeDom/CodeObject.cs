using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000648 RID: 1608
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeObject
	{
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003A87 RID: 14983 RVA: 0x000F44F2 File Offset: 0x000F26F2
		public IDictionary UserData
		{
			get
			{
				if (this.userData == null)
				{
					this.userData = new ListDictionary();
				}
				return this.userData;
			}
		}

		// Token: 0x04002C0C RID: 11276
		private IDictionary userData;
	}
}
