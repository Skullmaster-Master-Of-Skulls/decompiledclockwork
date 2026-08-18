using System;

namespace System.Web.Razor.Text
{
	// Token: 0x02000060 RID: 96
	public class LookaheadToken : IDisposable
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00011F9B File Offset: 0x0001019B
		public LookaheadToken(Action cancelAction)
		{
			this._cancelAction = cancelAction;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011FAA File Offset: 0x000101AA
		public void Accept()
		{
			this._accepted = true;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011FB3 File Offset: 0x000101B3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00011FC2 File Offset: 0x000101C2
		protected virtual void Dispose(bool disposing)
		{
			if (!this._accepted)
			{
				this._cancelAction();
			}
		}

		// Token: 0x04000143 RID: 323
		private Action _cancelAction;

		// Token: 0x04000144 RID: 324
		private bool _accepted;
	}
}
