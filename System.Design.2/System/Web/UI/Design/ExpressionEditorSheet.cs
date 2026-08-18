using System;
using System.ComponentModel;

namespace System.Web.UI.Design
{
	// Token: 0x0200003E RID: 62
	public abstract class ExpressionEditorSheet
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000F116 File Offset: 0x0000D316
		protected ExpressionEditorSheet(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00003B0F File Offset: 0x00001D0F
		[Browsable(false)]
		public virtual bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000F125 File Offset: 0x0000D325
		[Browsable(false)]
		public IServiceProvider ServiceProvider
		{
			get
			{
				return this._serviceProvider;
			}
		}

		// Token: 0x06000233 RID: 563
		public abstract string GetExpression();

		// Token: 0x04000157 RID: 343
		private IServiceProvider _serviceProvider;
	}
}
