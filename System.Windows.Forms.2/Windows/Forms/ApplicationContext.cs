using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000123 RID: 291
	public class ApplicationContext : IDisposable
	{
		// Token: 0x06000952 RID: 2386 RVA: 0x000199D7 File Offset: 0x00017BD7
		public ApplicationContext() : this(null)
		{
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000199E0 File Offset: 0x00017BE0
		public ApplicationContext(Form mainForm)
		{
			this.MainForm = mainForm;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000199F0 File Offset: 0x00017BF0
		~ApplicationContext()
		{
			this.Dispose(false);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x00019A20 File Offset: 0x00017C20
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x00019A28 File Offset: 0x00017C28
		public Form MainForm
		{
			get
			{
				return this.mainForm;
			}
			set
			{
				EventHandler value2 = new EventHandler(this.OnMainFormDestroy);
				if (this.mainForm != null)
				{
					this.mainForm.HandleDestroyed -= value2;
				}
				this.mainForm = value;
				if (this.mainForm != null)
				{
					this.mainForm.HandleDestroyed += value2;
				}
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x00019A71 File Offset: 0x00017C71
		// (set) Token: 0x06000958 RID: 2392 RVA: 0x00019A79 File Offset: 0x00017C79
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000959 RID: 2393 RVA: 0x00019A84 File Offset: 0x00017C84
		// (remove) Token: 0x0600095A RID: 2394 RVA: 0x00019ABC File Offset: 0x00017CBC
		public event EventHandler ThreadExit;

		// Token: 0x0600095B RID: 2395 RVA: 0x00019AF1 File Offset: 0x00017CF1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00019B00 File Offset: 0x00017D00
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.mainForm != null)
			{
				if (!this.mainForm.IsDisposed)
				{
					this.mainForm.Dispose();
				}
				this.mainForm = null;
			}
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00019B2C File Offset: 0x00017D2C
		public void ExitThread()
		{
			this.ExitThreadCore();
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00019B34 File Offset: 0x00017D34
		protected virtual void ExitThreadCore()
		{
			if (this.ThreadExit != null)
			{
				this.ThreadExit(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00019B2C File Offset: 0x00017D2C
		protected virtual void OnMainFormClosed(object sender, EventArgs e)
		{
			this.ExitThreadCore();
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00019B50 File Offset: 0x00017D50
		private void OnMainFormDestroy(object sender, EventArgs e)
		{
			Form form = (Form)sender;
			if (!form.RecreatingHandle)
			{
				form.HandleDestroyed -= this.OnMainFormDestroy;
				this.OnMainFormClosed(sender, e);
			}
		}

		// Token: 0x040005F2 RID: 1522
		private Form mainForm;

		// Token: 0x040005F3 RID: 1523
		private object userData;
	}
}
