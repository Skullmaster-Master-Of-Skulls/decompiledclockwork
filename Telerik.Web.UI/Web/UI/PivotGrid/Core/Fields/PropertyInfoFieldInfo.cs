using System;
using System.Reflection;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CAD RID: 3245
	public class PropertyInfoFieldInfo : PropertyFieldInfo, IPropertyMetadataReader
	{
		// Token: 0x06007991 RID: 31121 RVA: 0x001BEE5C File Offset: 0x001BD05C
		public PropertyInfoFieldInfo(PropertyInfo propertyInfo, Func<object, object> propertyAccess) : this(propertyInfo)
		{
			if (propertyAccess == null)
			{
				throw new ArgumentNullException("propertyAccess");
			}
			this.PropertyAccess = propertyAccess;
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x001BEE7C File Offset: 0x001BD07C
		public PropertyInfoFieldInfo(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			this.PropertyInfo = propertyInfo;
			base.Name = this.PropertyInfo.Name;
			base.DataType = this.PropertyInfo.PropertyType;
			base.DisplayName = AttributeHelper.GetValueForDisplayName(propertyInfo);
			base.AllowedRoles = FieldRoles.All;
			base.AutoGenerateField = AttributeHelper.GetValueForAutoGenerateField(propertyInfo);
		}

		// Token: 0x1700272C RID: 10028
		// (get) Token: 0x06007993 RID: 31123 RVA: 0x001BEEEC File Offset: 0x001BD0EC
		// (set) Token: 0x06007994 RID: 31124 RVA: 0x001BEEF4 File Offset: 0x001BD0F4
		public PropertyInfo PropertyInfo { get; private set; }

		// Token: 0x1700272D RID: 10029
		// (get) Token: 0x06007995 RID: 31125 RVA: 0x001BEEFD File Offset: 0x001BD0FD
		// (set) Token: 0x06007996 RID: 31126 RVA: 0x001BEF05 File Offset: 0x001BD105
		public Func<object, object> PropertyAccess { get; private set; }

		// Token: 0x06007997 RID: 31127 RVA: 0x001BEF10 File Offset: 0x001BD110
		public override object GetValue(object item)
		{
			object obj;
			if (this.PropertyAccess != null)
			{
				obj = this.PropertyAccess(item);
			}
			else
			{
				obj = this.PropertyInfo.GetValue(item, null);
			}
			if (obj != DBNull.Value)
			{
				return obj;
			}
			return null;
		}

		// Token: 0x06007998 RID: 31128 RVA: 0x001BEF4F File Offset: 0x001BD14F
		public override void SetValue(object item, object fieldValue)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x001BEF56 File Offset: 0x001BD156
		string IPropertyMetadataReader.GetValueForDisplayName()
		{
			return AttributeHelper.GetValueForDisplayName(this.PropertyInfo);
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x001BEF63 File Offset: 0x001BD163
		bool IPropertyMetadataReader.GetValueForAutoGenerateField()
		{
			return AttributeHelper.GetValueForAutoGenerateField(this.PropertyInfo);
		}
	}
}
