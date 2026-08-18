using System;
using Telerik.Web.Apoc.Fo;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x0200138B RID: 5003
	internal class Space : LengthRange
	{
		// Token: 0x0600D08F RID: 53391 RVA: 0x002E35AB File Offset: 0x002E17AB
		public override void SetComponent(string componentName, Property componentValue, bool isDefault)
		{
			if (componentName.Equals("precedence"))
			{
				this.Precedence = componentValue;
				return;
			}
			if (componentName.Equals("conditionality"))
			{
				this.Conditionality = componentValue;
				return;
			}
			base.SetComponent(componentName, componentValue, isDefault);
		}

		// Token: 0x0600D090 RID: 53392 RVA: 0x002E35E0 File Offset: 0x002E17E0
		public override Property GetComponent(string componentName)
		{
			if (componentName.Equals("precedence"))
			{
				return this.Precedence;
			}
			if (componentName.Equals("conditionality"))
			{
				return this.Conditionality;
			}
			return base.GetComponent(componentName);
		}

		// Token: 0x170042D6 RID: 17110
		// (get) Token: 0x0600D091 RID: 53393 RVA: 0x002E3611 File Offset: 0x002E1811
		// (set) Token: 0x0600D092 RID: 53394 RVA: 0x002E3619 File Offset: 0x002E1819
		public Property Conditionality
		{
			get
			{
				return this.conditionality;
			}
			set
			{
				this.conditionality = value;
			}
		}

		// Token: 0x170042D7 RID: 17111
		// (get) Token: 0x0600D093 RID: 53395 RVA: 0x002E3622 File Offset: 0x002E1822
		// (set) Token: 0x0600D094 RID: 53396 RVA: 0x002E362A File Offset: 0x002E182A
		public Property Precedence
		{
			get
			{
				return this.precedence;
			}
			set
			{
				this.precedence = value;
			}
		}

		// Token: 0x040037FB RID: 14331
		private Property precedence;

		// Token: 0x040037FC RID: 14332
		private Property conditionality;
	}
}
