using System;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000BE RID: 190
	public class ValueTypeFieldAccessor : FieldGetter, IMemberAccessor, IMemberGetter, IMemberResolver, IValueResolver
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x00015218 File Offset: 0x00013418
		public ValueTypeFieldAccessor(FieldInfo fieldInfo) : base(fieldInfo)
		{
			this._lateBoundFieldSet = fieldInfo;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00015228 File Offset: 0x00013428
		public void SetValue(object destination, object value)
		{
			this._lateBoundFieldSet.SetValue(destination, value);
		}

		// Token: 0x04000107 RID: 263
		private readonly FieldInfo _lateBoundFieldSet;
	}
}
