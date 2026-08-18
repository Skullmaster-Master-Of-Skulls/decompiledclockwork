using System;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000061 RID: 97
	public class SettingPropertyOperation : PropertyOperation
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000633B File Offset: 0x0000453B
		public SettingPropertyOperation(Type typeBeingConstructed, string propertyName) : base(typeBeingConstructed, propertyName)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006345 File Offset: 0x00004545
		protected override string GetDescriptionFormat()
		{
			return Resources.SettingPropertyOperation;
		}
	}
}
