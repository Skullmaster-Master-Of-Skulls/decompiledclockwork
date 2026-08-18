using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020016C5 RID: 5829
	internal class MinimumSubmissionTime : StateManager, IAutoBotDiscoveryStrategy, ISpamProtector
	{
		// Token: 0x170044F3 RID: 17651
		// (get) Token: 0x0600E0F6 RID: 57590 RVA: 0x0031FB51 File Offset: 0x0031DD51
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x170044F4 RID: 17652
		// (get) Token: 0x0600E0F7 RID: 57591 RVA: 0x0031FB59 File Offset: 0x0031DD59
		// (set) Token: 0x0600E0F8 RID: 57592 RVA: 0x0031FB7A File Offset: 0x0031DD7A
		public int MinTimeout
		{
			get
			{
				return (int)(base.ViewState["MinTimeout"] ?? 3);
			}
			set
			{
				if (value > 15)
				{
					throw new ArgumentOutOfRangeException("MinTimeout", "Timeout must be less than 15 seconds. Humans aren't that slow!");
				}
				base.ViewState["MinTimeout"] = value;
			}
		}

		// Token: 0x170044F5 RID: 17653
		// (get) Token: 0x0600E0F9 RID: 57593 RVA: 0x0031FBA7 File Offset: 0x0031DDA7
		// (set) Token: 0x0600E0FA RID: 57594 RVA: 0x0031FBAF File Offset: 0x0031DDAF
		public DateTime RenderedAt
		{
			get
			{
				return this.renderedAt;
			}
			set
			{
				this.renderedAt = value;
			}
		}

		// Token: 0x0600E0FB RID: 57595 RVA: 0x0031FBB8 File Offset: 0x0031DDB8
		public void AddChildControls(Control container)
		{
		}

		// Token: 0x170044F6 RID: 17654
		// (get) Token: 0x0600E0FC RID: 57596 RVA: 0x0031FBBA File Offset: 0x0031DDBA
		// (set) Token: 0x0600E0FD RID: 57597 RVA: 0x0031FBDB File Offset: 0x0031DDDB
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x0600E0FE RID: 57598 RVA: 0x0031FBF3 File Offset: 0x0031DDF3
		public void LoadPostBackData(Control container)
		{
		}

		// Token: 0x0600E0FF RID: 57599 RVA: 0x0031FBF5 File Offset: 0x0031DDF5
		public void ValidatePostBackData()
		{
			this.isValid = (this.renderedAt.AddSeconds((double)this.MinTimeout) < DateTime.Now);
		}

		// Token: 0x0600E100 RID: 57600 RVA: 0x0031FC19 File Offset: 0x0031DE19
		public void PreRenderHandler()
		{
			this.renderedAt = DateTime.Now;
		}

		// Token: 0x04004110 RID: 16656
		private bool isValid = true;

		// Token: 0x04004111 RID: 16657
		private DateTime renderedAt;
	}
}
