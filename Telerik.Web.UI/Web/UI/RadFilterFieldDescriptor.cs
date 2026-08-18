using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020018C7 RID: 6343
	public class RadFilterFieldDescriptor : IEquatable<RadFilterFieldDescriptor>
	{
		// Token: 0x0600F584 RID: 62852 RVA: 0x0037C030 File Offset: 0x0037A230
		public RadFilterFieldDescriptor()
		{
		}

		// Token: 0x0600F585 RID: 62853 RVA: 0x0037C038 File Offset: 0x0037A238
		public RadFilterFieldDescriptor(string fieldName, Type dataType)
		{
			this.FieldName = fieldName;
			this.DataType = dataType;
		}

		// Token: 0x0600F586 RID: 62854 RVA: 0x0037C04E File Offset: 0x0037A24E
		public RadFilterFieldDescriptor(string fieldName, Type dataType, string displayName) : this(fieldName, dataType)
		{
			this.DisplayName = displayName;
		}

		// Token: 0x17004A00 RID: 18944
		// (get) Token: 0x0600F587 RID: 62855 RVA: 0x0037C05F File Offset: 0x0037A25F
		// (set) Token: 0x0600F588 RID: 62856 RVA: 0x0037C067 File Offset: 0x0037A267
		public string FieldName { get; set; }

		// Token: 0x17004A01 RID: 18945
		// (get) Token: 0x0600F589 RID: 62857 RVA: 0x0037C070 File Offset: 0x0037A270
		// (set) Token: 0x0600F58A RID: 62858 RVA: 0x0037C078 File Offset: 0x0037A278
		public string DisplayName { get; set; }

		// Token: 0x17004A02 RID: 18946
		// (get) Token: 0x0600F58B RID: 62859 RVA: 0x0037C081 File Offset: 0x0037A281
		// (set) Token: 0x0600F58C RID: 62860 RVA: 0x0037C089 File Offset: 0x0037A289
		public Type DataType { get; set; }

		// Token: 0x0600F58D RID: 62861 RVA: 0x0037C092 File Offset: 0x0037A292
		public bool Equals(RadFilterFieldDescriptor other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || object.Equals(other.FieldName, this.FieldName));
		}

		// Token: 0x0600F58E RID: 62862 RVA: 0x0037C0BB File Offset: 0x0037A2BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!obj.GetType().IsAssignableFrom(typeof(RadFilterFieldDescriptor)) && this.Equals((RadFilterFieldDescriptor)obj)));
		}

		// Token: 0x0600F58F RID: 62863 RVA: 0x0037C0F8 File Offset: 0x0037A2F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			if (this.FieldName == null)
			{
				return 0;
			}
			return this.FieldName.GetHashCode();
		}
	}
}
