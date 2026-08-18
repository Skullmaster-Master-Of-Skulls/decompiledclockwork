using System;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000BF RID: 191
	public class ValueTypePropertyAccessor : PropertyGetter, IMemberAccessor, IMemberGetter, IMemberResolver, IValueResolver
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x00015238 File Offset: 0x00013438
		public ValueTypePropertyAccessor(PropertyInfo propertyInfo) : base(propertyInfo)
		{
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			this.HasSetter = (setMethod != null);
			if (this.HasSetter)
			{
				this._lateBoundPropertySet = setMethod;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00015270 File Offset: 0x00013470
		public bool HasSetter { get; }

		// Token: 0x060005A6 RID: 1446 RVA: 0x00015278 File Offset: 0x00013478
		public void SetValue(object destination, object value)
		{
			this._lateBoundPropertySet.Invoke(destination, new object[]
			{
				value
			});
		}

		// Token: 0x04000108 RID: 264
		private readonly MethodInfo _lateBoundPropertySet;
	}
}
