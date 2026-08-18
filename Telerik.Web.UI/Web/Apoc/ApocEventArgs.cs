using System;

namespace Telerik.Web.Apoc
{
	// Token: 0x02001373 RID: 4979
	public class ApocEventArgs : EventArgs
	{
		// Token: 0x0600CFE6 RID: 53222 RVA: 0x002E129F File Offset: 0x002DF49F
		public ApocEventArgs(string message)
		{
			this.message = message;
		}

		// Token: 0x0600CFE7 RID: 53223 RVA: 0x002E12AE File Offset: 0x002DF4AE
		public string GetMessage()
		{
			return this.message;
		}

		// Token: 0x0600CFE8 RID: 53224 RVA: 0x002E12B6 File Offset: 0x002DF4B6
		public override string ToString()
		{
			return this.GetMessage();
		}

		// Token: 0x040037B9 RID: 14265
		private string message;
	}
}
