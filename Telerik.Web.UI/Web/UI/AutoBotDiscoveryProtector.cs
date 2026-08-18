using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020016C0 RID: 5824
	internal class AutoBotDiscoveryProtector : StateManager, ISpamProtector, IDisposable
	{
		// Token: 0x170044E5 RID: 17637
		// (get) Token: 0x0600E0C5 RID: 57541 RVA: 0x0031F365 File Offset: 0x0031D565
		// (set) Token: 0x0600E0C6 RID: 57542 RVA: 0x0031F36D File Offset: 0x0031D56D
		public List<IAutoBotDiscoveryStrategy> AutoBotFindStrats
		{
			get
			{
				return this.autoBotFindStrats;
			}
			set
			{
				this.autoBotFindStrats = value;
			}
		}

		// Token: 0x170044E6 RID: 17638
		// (get) Token: 0x0600E0C7 RID: 57543 RVA: 0x0031F376 File Offset: 0x0031D576
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x170044E7 RID: 17639
		// (get) Token: 0x0600E0C8 RID: 57544 RVA: 0x0031F37E File Offset: 0x0031D57E
		// (set) Token: 0x0600E0C9 RID: 57545 RVA: 0x0031F386 File Offset: 0x0031D586
		public InvisibleTextBox InvisibleTextBoxStrat
		{
			get
			{
				return this.invisibleTextBoxStrat;
			}
			set
			{
				this.invisibleTextBoxStrat = value;
			}
		}

		// Token: 0x170044E8 RID: 17640
		// (get) Token: 0x0600E0CA RID: 57546 RVA: 0x0031F38F File Offset: 0x0031D58F
		// (set) Token: 0x0600E0CB RID: 57547 RVA: 0x0031F397 File Offset: 0x0031D597
		public MinimumSubmissionTime MinSubmTimeStrat
		{
			get
			{
				return this.minSubmTimeStrat;
			}
			set
			{
				this.minSubmTimeStrat = value;
			}
		}

		// Token: 0x0600E0CC RID: 57548 RVA: 0x0031F3A0 File Offset: 0x0031D5A0
		public AutoBotDiscoveryProtector()
		{
			this.isValid = true;
			this.autoBotFindStrats = new List<IAutoBotDiscoveryStrategy>();
			this.invisibleTextBoxStrat = new InvisibleTextBox();
			this.minSubmTimeStrat = new MinimumSubmissionTime();
		}

		// Token: 0x0600E0CD RID: 57549 RVA: 0x0031F3D0 File Offset: 0x0031D5D0
		public void AddChildControls(Control container)
		{
			foreach (IAutoBotDiscoveryStrategy autoBotDiscoveryStrategy in this.autoBotFindStrats)
			{
				autoBotDiscoveryStrategy.AddChildControls(container);
			}
		}

		// Token: 0x170044E9 RID: 17641
		// (get) Token: 0x0600E0CE RID: 57550 RVA: 0x0031F424 File Offset: 0x0031D624
		// (set) Token: 0x0600E0CF RID: 57551 RVA: 0x0031F450 File Offset: 0x0031D650
		public bool Visible
		{
			get
			{
				object obj = base.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x0600E0D0 RID: 57552 RVA: 0x0031F478 File Offset: 0x0031D678
		public void LoadPostBackData(Control container)
		{
			foreach (IAutoBotDiscoveryStrategy autoBotDiscoveryStrategy in this.autoBotFindStrats)
			{
				autoBotDiscoveryStrategy.LoadPostBackData(container);
			}
		}

		// Token: 0x0600E0D1 RID: 57553 RVA: 0x0031F4CC File Offset: 0x0031D6CC
		public void ValidatePostBackData()
		{
			foreach (IAutoBotDiscoveryStrategy autoBotDiscoveryStrategy in this.autoBotFindStrats)
			{
				autoBotDiscoveryStrategy.ValidatePostBackData();
				if (!autoBotDiscoveryStrategy.IsValid)
				{
					this.isValid = false;
					return;
				}
			}
			this.isValid = true;
		}

		// Token: 0x0600E0D2 RID: 57554 RVA: 0x0031F538 File Offset: 0x0031D738
		public void PreRenderHandler()
		{
			foreach (IAutoBotDiscoveryStrategy autoBotDiscoveryStrategy in this.autoBotFindStrats)
			{
				autoBotDiscoveryStrategy.PreRenderHandler();
			}
		}

		// Token: 0x0600E0D3 RID: 57555 RVA: 0x0031F58C File Offset: 0x0031D78C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.invisibleTextBoxStrat).LoadViewState(array[1]);
			((IStateManager)this.minSubmTimeStrat).LoadViewState(array[2]);
		}

		// Token: 0x0600E0D4 RID: 57556 RVA: 0x0031F5C8 File Offset: 0x0031D7C8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.invisibleTextBoxStrat).SaveViewState(),
				((IStateManager)this.minSubmTimeStrat).SaveViewState()
			};
		}

		// Token: 0x0600E0D5 RID: 57557 RVA: 0x0031F604 File Offset: 0x0031D804
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.invisibleTextBoxStrat).TrackViewState();
			((IStateManager)this.minSubmTimeStrat).TrackViewState();
		}

		// Token: 0x0600E0D6 RID: 57558 RVA: 0x0031F622 File Offset: 0x0031D822
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.invisibleTextBoxStrat != null)
			{
				this.invisibleTextBoxStrat.Dispose();
			}
		}

		// Token: 0x0600E0D7 RID: 57559 RVA: 0x0031F63A File Offset: 0x0031D83A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x04004102 RID: 16642
		private bool isValid;

		// Token: 0x04004103 RID: 16643
		private List<IAutoBotDiscoveryStrategy> autoBotFindStrats;

		// Token: 0x04004104 RID: 16644
		private InvisibleTextBox invisibleTextBoxStrat;

		// Token: 0x04004105 RID: 16645
		private MinimumSubmissionTime minSubmTimeStrat;
	}
}
