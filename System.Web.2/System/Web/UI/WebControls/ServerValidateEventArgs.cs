using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C4 RID: 1220
	public class ServerValidateEventArgs : EventArgs
	{
		// Token: 0x06003CB4 RID: 15540 RVA: 0x000C486B File Offset: 0x000C2A6B
		public ServerValidateEventArgs(string value, bool isValid)
		{
			this.isValid = isValid;
			this.value = value;
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000C4881 File Offset: 0x000C2A81
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000C4889 File Offset: 0x000C2A89
		// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x000C4891 File Offset: 0x000C2A91
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x04002394 RID: 9108
		private bool isValid;

		// Token: 0x04002395 RID: 9109
		private string value;
	}
}
