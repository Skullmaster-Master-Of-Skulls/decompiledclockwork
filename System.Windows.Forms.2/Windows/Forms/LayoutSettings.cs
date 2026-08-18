using System;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000448 RID: 1096
	public abstract class LayoutSettings
	{
		// Token: 0x06004C09 RID: 19465 RVA: 0x00002843 File Offset: 0x00000A43
		protected LayoutSettings()
		{
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0013BC59 File Offset: 0x00139E59
		internal LayoutSettings(IArrangedElement owner)
		{
			this._owner = owner;
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06004C0B RID: 19467 RVA: 0x00015ECC File Offset: 0x000140CC
		public virtual LayoutEngine LayoutEngine
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06004C0C RID: 19468 RVA: 0x0013BC68 File Offset: 0x00139E68
		internal IArrangedElement Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x04002871 RID: 10353
		private IArrangedElement _owner;
	}
}
