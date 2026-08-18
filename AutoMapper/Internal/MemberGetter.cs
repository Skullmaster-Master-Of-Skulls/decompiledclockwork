using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000AE RID: 174
	public abstract class MemberGetter : IMemberGetter, IMemberResolver, IValueResolver
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000528 RID: 1320
		public abstract MemberInfo MemberInfo { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000529 RID: 1321
		public abstract string Name { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600052A RID: 1322
		public abstract Type MemberType { get; }

		// Token: 0x0600052B RID: 1323
		public abstract object GetValue(object source);

		// Token: 0x0600052C RID: 1324 RVA: 0x00013B77 File Offset: 0x00011D77
		public ResolutionResult Resolve(ResolutionResult source)
		{
			if (source.Value != null)
			{
				return source.New(this.GetValue(source.Value), this.MemberType);
			}
			return source.New(source.Value, this.MemberType);
		}

		// Token: 0x0600052D RID: 1325
		public abstract IEnumerable<object> GetCustomAttributes(Type attributeType, bool inherit);

		// Token: 0x0600052E RID: 1326
		public abstract IEnumerable<object> GetCustomAttributes(bool inherit);

		// Token: 0x0600052F RID: 1327
		public abstract bool IsDefined(Type attributeType, bool inherit);

		// Token: 0x040000E6 RID: 230
		protected static readonly DelegateFactory DelegateFactory = new DelegateFactory();
	}
}
