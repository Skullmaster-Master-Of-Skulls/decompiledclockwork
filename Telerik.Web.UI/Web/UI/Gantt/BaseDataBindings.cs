using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200030E RID: 782
	public abstract class BaseDataBindings : StateManager
	{
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x000567E6 File Offset: 0x000549E6
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x000567EE File Offset: 0x000549EE
		public List<PropertyInfo> RequiredProperties { get; set; }

		// Token: 0x06001A77 RID: 6775 RVA: 0x0005680C File Offset: 0x00054A0C
		public BaseDataBindings()
		{
			if (this.RequiredProperties == null)
			{
				this.RequiredProperties = (from p in base.GetType().GetProperties()
				where Attribute.IsDefined(p, typeof(RequiredPropertyAttribute))
				select p).ToList<PropertyInfo>();
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00056860 File Offset: 0x00054A60
		public virtual void EnsureDataFields()
		{
			foreach (PropertyInfo propertyInfo in this.RequiredProperties)
			{
				if (string.IsNullOrEmpty((string)propertyInfo.GetValue(this, null)))
				{
					this.ThrowException(propertyInfo.Name);
				}
			}
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000568D4 File Offset: 0x00054AD4
		public virtual void ThrowException(string failedField)
		{
			string arg = string.Join(", ", (from p in this.RequiredProperties
			select p.Name).Take(this.RequiredProperties.Count - 1).ToArray<string>());
			throw new ArgumentException(string.Format("{0} and {1} are required for data binding. Failing field is {2}.", arg, this.RequiredProperties.Last<PropertyInfo>().Name, failedField));
		}
	}
}
