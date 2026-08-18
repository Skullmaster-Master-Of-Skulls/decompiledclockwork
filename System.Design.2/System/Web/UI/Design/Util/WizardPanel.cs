using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x0200016D RID: 365
	internal class WizardPanel : UserControl
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00053427 File Offset: 0x00051627
		// (set) Token: 0x06000D06 RID: 3334 RVA: 0x0005343D File Offset: 0x0005163D
		public string Caption
		{
			get
			{
				if (this._caption == null)
				{
					return string.Empty;
				}
				return this._caption;
			}
			set
			{
				this._caption = value;
				if (this._parentWizard != null)
				{
					this._parentWizard.Invalidate();
					return;
				}
				this._needsToInvalidate = true;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00053461 File Offset: 0x00051661
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x00053469 File Offset: 0x00051669
		public WizardPanel NextPanel
		{
			get
			{
				return this._nextPanel;
			}
			set
			{
				this._nextPanel = value;
				if (this._parentWizard != null)
				{
					this._parentWizard.RegisterPanel(this._nextPanel);
				}
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0005348B File Offset: 0x0005168B
		[Browsable(false)]
		public WizardForm ParentWizard
		{
			get
			{
				return this._parentWizard;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00053493 File Offset: 0x00051693
		protected IServiceProvider ServiceProvider
		{
			get
			{
				return this.ParentWizard.ServiceProvider;
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00003937 File Offset: 0x00001B37
		protected internal virtual void OnComplete()
		{
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00003B0F File Offset: 0x00001D0F
		public virtual bool OnNext()
		{
			return true;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00003937 File Offset: 0x00001B37
		public virtual void OnPrevious()
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000534A0 File Offset: 0x000516A0
		internal void SetParentWizard(WizardForm parent)
		{
			this._parentWizard = parent;
			if (this._parentWizard != null && this._needsToInvalidate)
			{
				this._parentWizard.Invalidate();
				this._needsToInvalidate = false;
			}
		}

		// Token: 0x040007DD RID: 2013
		private WizardForm _parentWizard;

		// Token: 0x040007DE RID: 2014
		private string _caption;

		// Token: 0x040007DF RID: 2015
		private WizardPanel _nextPanel;

		// Token: 0x040007E0 RID: 2016
		private bool _needsToInvalidate;
	}
}
