using System;
using System.Collections.Generic;
using System.Design;
using System.Drawing;
using System.Windows.Forms;

namespace System.Web.UI.Design.Util
{
	// Token: 0x0200016C RID: 364
	internal abstract partial class WizardForm : TaskFormBase
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x00052B0B File Offset: 0x00050D0B
		public WizardForm(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			this._panelHistory = new Stack<WizardPanel>();
			this.InitializeComponent();
			this.InitializeUI();
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00052B2B File Offset: 0x00050D2B
		public Button FinishButton
		{
			get
			{
				return this._finishButton;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00052B33 File Offset: 0x00050D33
		public Button NextButton
		{
			get
			{
				return this._nextButton;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00052B3B File Offset: 0x00050D3B
		public Button PreviousButton
		{
			get
			{
				return this._previousButton;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00053150 File Offset: 0x00051350
		private void InitializeUI()
		{
			this._cancelButton.Text = SR.GetString("Wizard_CancelButton");
			this._nextButton.Text = SR.GetString("Wizard_NextButton");
			this._previousButton.Text = SR.GetString("Wizard_PreviousButton");
			this._finishButton.Text = SR.GetString("Wizard_FinishButton");
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000531B4 File Offset: 0x000513B4
		public void NextPanel()
		{
			WizardPanel wizardPanel = this._panelHistory.Peek();
			if (wizardPanel.OnNext())
			{
				wizardPanel.Hide();
				WizardPanel nextPanel = wizardPanel.NextPanel;
				if (nextPanel != null)
				{
					this.RegisterPanel(nextPanel);
					this._panelHistory.Push(nextPanel);
					this.OnPanelChanging(new WizardPanelChangingEventArgs(wizardPanel));
					this.ShowPanel(nextPanel);
				}
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002AF61 File Offset: 0x00029161
		protected virtual void OnCancelButtonClick(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0005320B File Offset: 0x0005140B
		protected override void OnInitialActivated(EventArgs e)
		{
			base.OnInitialActivated(e);
			if (this._initialPanel != null)
			{
				this.RegisterPanel(this._initialPanel);
				this._panelHistory.Push(this._initialPanel);
				this.ShowPanel(this._initialPanel);
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00053248 File Offset: 0x00051448
		protected virtual void OnFinishButtonClick(object sender, EventArgs e)
		{
			WizardPanel wizardPanel = this._panelHistory.Peek();
			if (wizardPanel.OnNext())
			{
				WizardPanel[] array = this._panelHistory.ToArray();
				Array.Reverse(array);
				foreach (WizardPanel wizardPanel2 in array)
				{
					wizardPanel2.OnComplete();
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000532A4 File Offset: 0x000514A4
		protected virtual void OnNextButtonClick(object sender, EventArgs e)
		{
			this.NextPanel();
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void OnPanelChanging(WizardPanelChangingEventArgs e)
		{
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000532AC File Offset: 0x000514AC
		protected virtual void OnPreviousButtonClick(object sender, EventArgs e)
		{
			this.PreviousPanel();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000532B4 File Offset: 0x000514B4
		public void PreviousPanel()
		{
			if (this._panelHistory.Count > 1)
			{
				WizardPanel wizardPanel = this._panelHistory.Pop();
				WizardPanel panel = this._panelHistory.Peek();
				wizardPanel.OnPrevious();
				wizardPanel.Hide();
				this.OnPanelChanging(new WizardPanelChangingEventArgs(wizardPanel));
				this.ShowPanel(panel);
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00053306 File Offset: 0x00051506
		internal void RegisterPanel(WizardPanel panel)
		{
			if (!base.TaskPanel.Controls.Contains(panel))
			{
				panel.Dock = DockStyle.Fill;
				panel.SetParentWizard(this);
				panel.Hide();
				base.TaskPanel.Controls.Add(panel);
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00053340 File Offset: 0x00051540
		protected void SetPanels(WizardPanel[] panels)
		{
			if (panels != null && panels.Length != 0)
			{
				this.RegisterPanel(panels[0]);
				this._initialPanel = panels[0];
				for (int i = 0; i < panels.Length - 1; i++)
				{
					this.RegisterPanel(panels[i + 1]);
					panels[i].NextPanel = panels[i + 1];
				}
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00053390 File Offset: 0x00051590
		private void ShowPanel(WizardPanel panel)
		{
			if (this._panelHistory.Count == 1)
			{
				this.PreviousButton.Enabled = false;
			}
			else
			{
				this.PreviousButton.Enabled = true;
			}
			if (panel.NextPanel == null)
			{
				this.NextButton.Enabled = false;
			}
			else
			{
				this.NextButton.Enabled = true;
			}
			panel.Show();
			base.AccessibleDescription = panel.Caption;
			base.CaptionLabel.Text = panel.Caption;
			if (base.IsHandleCreated)
			{
				base.Invalidate();
			}
			panel.Focus();
		}

		// Token: 0x040007DB RID: 2011
		private Stack<WizardPanel> _panelHistory;

		// Token: 0x040007DC RID: 2012
		private WizardPanel _initialPanel;
	}
}
