using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000593 RID: 1427
	public abstract class WebPartDisplayMode
	{
		// Token: 0x06004803 RID: 18435 RVA: 0x000ECC8C File Offset: 0x000EAE8C
		protected WebPartDisplayMode(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this._name = name;
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool AllowPageDesign
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool AssociatedWithToolZone
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x000ECCAE File Offset: 0x000EAEAE
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool RequiresPersonalization
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06004808 RID: 18440 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool ShowHiddenWebParts
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x000ECCB6 File Offset: 0x000EAEB6
		public virtual bool IsEnabled(WebPartManager webPartManager)
		{
			return !this.RequiresPersonalization || webPartManager.Personalization.IsModifiable;
		}

		// Token: 0x0400271E RID: 10014
		private string _name;
	}
}
