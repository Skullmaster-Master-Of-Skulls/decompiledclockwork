using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Security.Permissions;

namespace EmailClassLibrary
{
	// Token: 0x02000006 RID: 6
	[RunInstaller(true)]
	public class Installer1 : Installer
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002795 File Offset: 0x00001795
		public Installer1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027A3 File Offset: 0x000017A3
		[SecurityPermission(SecurityAction.Demand)]
		public override void Install(IDictionary stateSaver)
		{
			base.Install(stateSaver);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027AC File Offset: 0x000017AC
		[SecurityPermission(SecurityAction.Demand)]
		public override void Commit(IDictionary savedState)
		{
			base.Commit(savedState);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027B5 File Offset: 0x000017B5
		[SecurityPermission(SecurityAction.Demand)]
		public override void Rollback(IDictionary savedState)
		{
			base.Rollback(savedState);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027BE File Offset: 0x000017BE
		[SecurityPermission(SecurityAction.Demand)]
		public override void Uninstall(IDictionary savedState)
		{
			base.Uninstall(savedState);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027C7 File Offset: 0x000017C7
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027E6 File Offset: 0x000017E6
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x04000017 RID: 23
		private IContainer components;
	}
}
