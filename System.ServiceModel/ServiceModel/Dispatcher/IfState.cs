using System;
using System.Reflection.Emit;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A7 RID: 1447
	internal class IfState
	{
		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000D9B34 File Offset: 0x000D7D34
		// (set) Token: 0x06003881 RID: 14465 RVA: 0x000D9B3C File Offset: 0x000D7D3C
		internal Label EndIf
		{
			get
			{
				return this.endIf;
			}
			set
			{
				this.endIf = value;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x000D9B45 File Offset: 0x000D7D45
		// (set) Token: 0x06003883 RID: 14467 RVA: 0x000D9B4D File Offset: 0x000D7D4D
		internal Label ElseBegin
		{
			get
			{
				return this.elseBegin;
			}
			set
			{
				this.elseBegin = value;
			}
		}

		// Token: 0x04002994 RID: 10644
		private Label elseBegin;

		// Token: 0x04002995 RID: 10645
		private Label endIf;
	}
}
