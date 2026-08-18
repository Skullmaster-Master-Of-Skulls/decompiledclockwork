using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000268 RID: 616
	[ComVisible(true)]
	public class GiveFeedbackEventArgs : EventArgs
	{
		// Token: 0x06002793 RID: 10131 RVA: 0x000B8F64 File Offset: 0x000B7164
		public GiveFeedbackEventArgs(DragDropEffects effect, bool useDefaultCursors)
		{
			this.effect = effect;
			this.useDefaultCursors = useDefaultCursors;
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x000B8F7A File Offset: 0x000B717A
		public DragDropEffects Effect
		{
			get
			{
				return this.effect;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x000B8F82 File Offset: 0x000B7182
		// (set) Token: 0x06002796 RID: 10134 RVA: 0x000B8F8A File Offset: 0x000B718A
		public bool UseDefaultCursors
		{
			get
			{
				return this.useDefaultCursors;
			}
			set
			{
				this.useDefaultCursors = value;
			}
		}

		// Token: 0x04001057 RID: 4183
		private readonly DragDropEffects effect;

		// Token: 0x04001058 RID: 4184
		private bool useDefaultCursors;
	}
}
