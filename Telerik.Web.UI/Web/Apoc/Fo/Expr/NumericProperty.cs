using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013BB RID: 5051
	internal class NumericProperty : Property
	{
		// Token: 0x0600D168 RID: 53608 RVA: 0x002E4F4A File Offset: 0x002E314A
		internal NumericProperty(Numeric value)
		{
			this.numeric = value;
		}

		// Token: 0x0600D169 RID: 53609 RVA: 0x002E4F59 File Offset: 0x002E3159
		public override Numeric GetNumeric()
		{
			return this.numeric;
		}

		// Token: 0x0600D16A RID: 53610 RVA: 0x002E4F61 File Offset: 0x002E3161
		public override Number GetNumber()
		{
			return this.numeric.asNumber();
		}

		// Token: 0x0600D16B RID: 53611 RVA: 0x002E4F6E File Offset: 0x002E316E
		public override Length GetLength()
		{
			return this.numeric.asLength();
		}

		// Token: 0x0600D16C RID: 53612 RVA: 0x002E4F7B File Offset: 0x002E317B
		public override ColorType GetColorType()
		{
			return null;
		}

		// Token: 0x0600D16D RID: 53613 RVA: 0x002E4F7E File Offset: 0x002E317E
		public override object GetObject()
		{
			return this.numeric;
		}

		// Token: 0x0400382A RID: 14378
		private Numeric numeric;
	}
}
