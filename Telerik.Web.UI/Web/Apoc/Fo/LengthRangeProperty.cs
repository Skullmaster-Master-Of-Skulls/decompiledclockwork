using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001422 RID: 5154
	internal class LengthRangeProperty : Property
	{
		// Token: 0x0600D2F0 RID: 54000 RVA: 0x002ED3BC File Offset: 0x002EB5BC
		public LengthRangeProperty(LengthRange lengthRange)
		{
			this.lengthRange = lengthRange;
		}

		// Token: 0x0600D2F1 RID: 54001 RVA: 0x002ED3CB File Offset: 0x002EB5CB
		public override LengthRange GetLengthRange()
		{
			return this.lengthRange;
		}

		// Token: 0x0600D2F2 RID: 54002 RVA: 0x002ED3D3 File Offset: 0x002EB5D3
		public override object GetObject()
		{
			return this.lengthRange;
		}

		// Token: 0x04003925 RID: 14629
		private LengthRange lengthRange;

		// Token: 0x02001423 RID: 5155
		internal class Maker : LengthProperty.Maker
		{
			// Token: 0x0600D2F3 RID: 54003 RVA: 0x002ED3DB File Offset: 0x002EB5DB
			protected Maker(string name) : base(name)
			{
			}
		}
	}
}
