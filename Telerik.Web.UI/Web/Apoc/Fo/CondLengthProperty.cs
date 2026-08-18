using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020013A5 RID: 5029
	internal class CondLengthProperty : Property
	{
		// Token: 0x0600D119 RID: 53529 RVA: 0x002E43E2 File Offset: 0x002E25E2
		public CondLengthProperty(CondLength condLength)
		{
			this.condLength = condLength;
		}

		// Token: 0x0600D11A RID: 53530 RVA: 0x002E43F1 File Offset: 0x002E25F1
		public override CondLength GetCondLength()
		{
			return this.condLength;
		}

		// Token: 0x0600D11B RID: 53531 RVA: 0x002E43F9 File Offset: 0x002E25F9
		public override Length GetLength()
		{
			return this.condLength.GetLength().GetLength();
		}

		// Token: 0x0600D11C RID: 53532 RVA: 0x002E440B File Offset: 0x002E260B
		public override object GetObject()
		{
			return this.condLength;
		}

		// Token: 0x0400381E RID: 14366
		private CondLength condLength;

		// Token: 0x020013A6 RID: 5030
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D11D RID: 53533 RVA: 0x002E4413 File Offset: 0x002E2613
			public Maker(string name) : base(name)
			{
			}
		}
	}
}
