using System;

namespace System.ComponentModel
{
	// Token: 0x020005A7 RID: 1447
	[AttributeUsage(AttributeTargets.Class)]
	public class RunInstallerAttribute : Attribute
	{
		// Token: 0x0600360F RID: 13839 RVA: 0x000EC68B File Offset: 0x000EA88B
		public RunInstallerAttribute(bool runInstaller)
		{
			this.runInstaller = runInstaller;
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003610 RID: 13840 RVA: 0x000EC69A File Offset: 0x000EA89A
		public bool RunInstaller
		{
			get
			{
				return this.runInstaller;
			}
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000EC6A4 File Offset: 0x000EA8A4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RunInstallerAttribute runInstallerAttribute = obj as RunInstallerAttribute;
			return runInstallerAttribute != null && runInstallerAttribute.RunInstaller == this.runInstaller;
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000EC6D1 File Offset: 0x000EA8D1
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000EC6D9 File Offset: 0x000EA8D9
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RunInstallerAttribute.Default);
		}

		// Token: 0x04002A96 RID: 10902
		private bool runInstaller;

		// Token: 0x04002A97 RID: 10903
		public static readonly RunInstallerAttribute Yes = new RunInstallerAttribute(true);

		// Token: 0x04002A98 RID: 10904
		public static readonly RunInstallerAttribute No = new RunInstallerAttribute(false);

		// Token: 0x04002A99 RID: 10905
		public static readonly RunInstallerAttribute Default = RunInstallerAttribute.No;
	}
}
