using System;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000B3 RID: 179
	public class PropertyAccessor : PropertyGetter, IMemberAccessor, IMemberGetter, IMemberResolver, IValueResolver
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00013FCC File Offset: 0x000121CC
		public PropertyAccessor(PropertyInfo propertyInfo) : base(propertyInfo)
		{
			this.HasSetter = (propertyInfo.GetSetMethod(true) != null);
			if (this.HasSetter)
			{
				this._lateBoundPropertySet = new Lazy<LateBoundPropertySet>(() => MemberGetter.DelegateFactory.CreateSet(propertyInfo));
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00014029 File Offset: 0x00012229
		public bool HasSetter { get; }

		// Token: 0x06000551 RID: 1361 RVA: 0x00014031 File Offset: 0x00012231
		public virtual void SetValue(object destination, object value)
		{
			this._lateBoundPropertySet.Value(destination, value);
		}

		// Token: 0x040000EE RID: 238
		private readonly Lazy<LateBoundPropertySet> _lateBoundPropertySet;
	}
}
