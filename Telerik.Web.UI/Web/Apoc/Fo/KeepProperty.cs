using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200141C RID: 5148
	internal class KeepProperty : Property
	{
		// Token: 0x0600D2E1 RID: 53985 RVA: 0x002ED2D6 File Offset: 0x002EB4D6
		public KeepProperty(Keep keep)
		{
			this.keep = keep;
		}

		// Token: 0x0600D2E2 RID: 53986 RVA: 0x002ED2E5 File Offset: 0x002EB4E5
		public override Keep GetKeep()
		{
			return this.keep;
		}

		// Token: 0x0600D2E3 RID: 53987 RVA: 0x002ED2ED File Offset: 0x002EB4ED
		public override object GetObject()
		{
			return this.keep;
		}

		// Token: 0x04003922 RID: 14626
		private Keep keep;

		// Token: 0x0200141D RID: 5149
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D2E4 RID: 53988 RVA: 0x002ED2F5 File Offset: 0x002EB4F5
			protected Maker(string name) : base(name)
			{
			}
		}
	}
}
