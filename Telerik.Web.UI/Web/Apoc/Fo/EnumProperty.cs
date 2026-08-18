using System;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020013A9 RID: 5033
	internal class EnumProperty : Property
	{
		// Token: 0x0600D122 RID: 53538 RVA: 0x002E4449 File Offset: 0x002E2649
		public EnumProperty(int explicitValue)
		{
			this.value = explicitValue;
		}

		// Token: 0x0600D123 RID: 53539 RVA: 0x002E4458 File Offset: 0x002E2658
		public override int GetEnum()
		{
			return this.value;
		}

		// Token: 0x0600D124 RID: 53540 RVA: 0x002E4460 File Offset: 0x002E2660
		public override object GetObject()
		{
			return this.value;
		}

		// Token: 0x0400381F RID: 14367
		private int value;

		// Token: 0x020013AA RID: 5034
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D125 RID: 53541 RVA: 0x002E446D File Offset: 0x002E266D
			protected Maker(string propName) : base(propName)
			{
			}

			// Token: 0x0600D126 RID: 53542 RVA: 0x002E4476 File Offset: 0x002E2676
			public override Property CheckEnumValues(string value)
			{
				return null;
			}

			// Token: 0x0600D127 RID: 53543 RVA: 0x002E4479 File Offset: 0x002E2679
			protected Property findConstant(string value)
			{
				return null;
			}

			// Token: 0x0600D128 RID: 53544 RVA: 0x002E447C File Offset: 0x002E267C
			public override Property ConvertProperty(Property p, PropertyList propertyList, FObj fo)
			{
				if (p is EnumProperty)
				{
					return p;
				}
				return null;
			}
		}
	}
}
