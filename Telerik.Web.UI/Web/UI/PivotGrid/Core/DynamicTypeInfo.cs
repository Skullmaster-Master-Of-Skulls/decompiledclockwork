using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CDE RID: 3294
	internal class DynamicTypeInfo
	{
		// Token: 0x06007B1E RID: 31518 RVA: 0x001C4472 File Offset: 0x001C2672
		public DynamicTypeInfo(Type groupingType, IEnumerable<PropertyInfo> properties)
		{
			this.Type = groupingType;
			this.PropertyInfos = properties;
		}

		// Token: 0x17002766 RID: 10086
		// (get) Token: 0x06007B1F RID: 31519 RVA: 0x001C4488 File Offset: 0x001C2688
		// (set) Token: 0x06007B20 RID: 31520 RVA: 0x001C4490 File Offset: 0x001C2690
		public Type Type { get; private set; }

		// Token: 0x17002767 RID: 10087
		// (get) Token: 0x06007B21 RID: 31521 RVA: 0x001C4499 File Offset: 0x001C2699
		// (set) Token: 0x06007B22 RID: 31522 RVA: 0x001C44A1 File Offset: 0x001C26A1
		public IEnumerable<PropertyInfo> PropertyInfos { get; private set; }

		// Token: 0x06007B23 RID: 31523 RVA: 0x001C44AC File Offset: 0x001C26AC
		public static DynamicTypeInfo CreateTypeWithProperties(IEnumerable<PivotDynamicProperty> properties)
		{
			Type dynamicClass = PivotClassFactory.Instance.GetDynamicClass(properties);
			List<PropertyInfo> properties2 = dynamicClass.GetProperties().ToList<PropertyInfo>();
			return new DynamicTypeInfo(dynamicClass, properties2);
		}
	}
}
