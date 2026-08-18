using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020014E8 RID: 5352
	internal class SpaceProperty : Property
	{
		// Token: 0x0600D618 RID: 54808 RVA: 0x002F5FED File Offset: 0x002F41ED
		public SpaceProperty(Space space)
		{
			this.space = space;
		}

		// Token: 0x0600D619 RID: 54809 RVA: 0x002F5FFC File Offset: 0x002F41FC
		public override Space GetSpace()
		{
			return this.space;
		}

		// Token: 0x0600D61A RID: 54810 RVA: 0x002F6004 File Offset: 0x002F4204
		public override LengthRange GetLengthRange()
		{
			return this.space;
		}

		// Token: 0x0600D61B RID: 54811 RVA: 0x002F600C File Offset: 0x002F420C
		public override object GetObject()
		{
			return this.space;
		}

		// Token: 0x04003AA9 RID: 15017
		private Space space;

		// Token: 0x020014E9 RID: 5353
		internal class Maker : LengthRangeProperty.Maker
		{
			// Token: 0x0600D61C RID: 54812 RVA: 0x002F6014 File Offset: 0x002F4214
			protected Maker(string name) : base(name)
			{
			}
		}
	}
}
