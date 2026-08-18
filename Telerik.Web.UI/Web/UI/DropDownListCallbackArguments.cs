using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000B25 RID: 2853
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class DropDownListCallbackArguments
	{
		// Token: 0x06006AF7 RID: 27383 RVA: 0x00190211 File Offset: 0x0018E411
		public DropDownListCallbackArguments()
		{
			this.Count = -1;
			this.StartIndex = -1;
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x06006AF8 RID: 27384 RVA: 0x00190227 File Offset: 0x0018E427
		// (set) Token: 0x06006AF9 RID: 27385 RVA: 0x0019022F File Offset: 0x0018E42F
		public int Count { get; set; }

		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x06006AFA RID: 27386 RVA: 0x00190238 File Offset: 0x0018E438
		// (set) Token: 0x06006AFB RID: 27387 RVA: 0x00190240 File Offset: 0x0018E440
		public int StartIndex { get; set; }
	}
}
