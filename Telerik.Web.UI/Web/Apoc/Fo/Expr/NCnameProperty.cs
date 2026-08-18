using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013B8 RID: 5048
	internal class NCnameProperty : Property
	{
		// Token: 0x0600D151 RID: 53585 RVA: 0x002E484C File Offset: 0x002E2A4C
		public NCnameProperty(string ncName)
		{
			this.ncName = ncName;
		}

		// Token: 0x0600D152 RID: 53586 RVA: 0x002E485B File Offset: 0x002E2A5B
		public ColorType getColor()
		{
			throw new PropertyException("Not a Color");
		}

		// Token: 0x0600D153 RID: 53587 RVA: 0x002E4867 File Offset: 0x002E2A67
		public override string GetString()
		{
			return this.ncName;
		}

		// Token: 0x0600D154 RID: 53588 RVA: 0x002E486F File Offset: 0x002E2A6F
		public override string GetNCname()
		{
			return this.ncName;
		}

		// Token: 0x04003820 RID: 14368
		private string ncName;
	}
}
