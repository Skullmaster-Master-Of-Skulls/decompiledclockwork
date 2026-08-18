using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001A3F RID: 6719
	internal class ResourceAttributePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060104B5 RID: 66741 RVA: 0x003A3DF3 File Offset: 0x003A1FF3
		public ResourceAttributePropertyDescriptor(string propertyName) : base(propertyName, new Attribute[0])
		{
		}

		// Token: 0x060104B6 RID: 66742 RVA: 0x003A3E02 File Offset: 0x003A2002
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x17004F05 RID: 20229
		// (get) Token: 0x060104B7 RID: 66743 RVA: 0x003A3E05 File Offset: 0x003A2005
		public override Type ComponentType
		{
			get
			{
				return typeof(Resource);
			}
		}

		// Token: 0x060104B8 RID: 66744 RVA: 0x003A3E14 File Offset: 0x003A2014
		public override object GetValue(object component)
		{
			Resource resource = (Resource)component;
			return resource.Attributes[this.Name];
		}

		// Token: 0x17004F06 RID: 20230
		// (get) Token: 0x060104B9 RID: 66745 RVA: 0x003A3E39 File Offset: 0x003A2039
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004F07 RID: 20231
		// (get) Token: 0x060104BA RID: 66746 RVA: 0x003A3E3C File Offset: 0x003A203C
		public override Type PropertyType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x060104BB RID: 66747 RVA: 0x003A3E48 File Offset: 0x003A2048
		public override void ResetValue(object component)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060104BC RID: 66748 RVA: 0x003A3E50 File Offset: 0x003A2050
		public override void SetValue(object component, object value)
		{
			Resource resource = (Resource)component;
			resource.Attributes[this.Name] = value.ToString();
		}

		// Token: 0x060104BD RID: 66749 RVA: 0x003A3E7B File Offset: 0x003A207B
		public override bool ShouldSerializeValue(object component)
		{
			throw new NotImplementedException();
		}
	}
}
