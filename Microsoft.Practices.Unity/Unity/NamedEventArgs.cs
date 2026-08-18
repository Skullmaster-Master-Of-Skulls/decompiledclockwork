using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200007D RID: 125
	public abstract class NamedEventArgs : EventArgs
	{
		// Token: 0x06000236 RID: 566 RVA: 0x000079AE File Offset: 0x00005BAE
		protected NamedEventArgs()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000079B6 File Offset: 0x00005BB6
		protected NamedEventArgs(string name)
		{
			this.name = name;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000079C5 File Offset: 0x00005BC5
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000079CD File Offset: 0x00005BCD
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000077 RID: 119
		private string name;
	}
}
