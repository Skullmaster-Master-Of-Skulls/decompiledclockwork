using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000B5 RID: 181
	public class PropertyGetter : MemberGetter
	{
		// Token: 0x06000557 RID: 1367 RVA: 0x0001421C File Offset: 0x0001241C
		public PropertyGetter(PropertyInfo propertyInfo)
		{
			this._propertyInfo = propertyInfo;
			this.Name = this._propertyInfo.Name;
			this.MemberType = this._propertyInfo.PropertyType;
			Lazy<LateBoundPropertyGet> lateBoundPropertyGet;
			if (!(this._propertyInfo.GetGetMethod(true) != null))
			{
				lateBoundPropertyGet = new Lazy<LateBoundPropertyGet>(() => (object src) => null);
			}
			else
			{
				lateBoundPropertyGet = new Lazy<LateBoundPropertyGet>(() => MemberGetter.DelegateFactory.CreateGet(propertyInfo));
			}
			this._lateBoundPropertyGet = lateBoundPropertyGet;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000142BB File Offset: 0x000124BB
		public override MemberInfo MemberInfo
		{
			get
			{
				return this._propertyInfo;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000142C3 File Offset: 0x000124C3
		public override string Name { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x000142CB File Offset: 0x000124CB
		public override Type MemberType { get; }

		// Token: 0x0600055B RID: 1371 RVA: 0x000142D3 File Offset: 0x000124D3
		public override object GetValue(object source)
		{
			return this._lateBoundPropertyGet.Value(source);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000142E6 File Offset: 0x000124E6
		public override IEnumerable<object> GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._propertyInfo.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000142F5 File Offset: 0x000124F5
		public override IEnumerable<object> GetCustomAttributes(bool inherit)
		{
			return this._propertyInfo.GetCustomAttributes(inherit);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00014303 File Offset: 0x00012503
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._propertyInfo.IsDefined(attributeType, inherit);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00014312 File Offset: 0x00012512
		public bool Equals(PropertyGetter other)
		{
			return other != null && (this == other || object.Equals(other._propertyInfo, this._propertyInfo));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00014330 File Offset: 0x00012530
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != typeof(PropertyGetter)) && this.Equals((PropertyGetter)obj)));
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00014362 File Offset: 0x00012562
		public override int GetHashCode()
		{
			return this._propertyInfo.GetHashCode();
		}

		// Token: 0x040000F5 RID: 245
		private readonly PropertyInfo _propertyInfo;

		// Token: 0x040000F6 RID: 246
		private readonly Lazy<LateBoundPropertyGet> _lateBoundPropertyGet;
	}
}
