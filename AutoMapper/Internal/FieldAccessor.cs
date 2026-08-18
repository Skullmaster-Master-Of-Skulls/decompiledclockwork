using System;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x0200009D RID: 157
	public class FieldAccessor : FieldGetter, IMemberAccessor, IMemberGetter, IMemberResolver, IValueResolver
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x00012B6C File Offset: 0x00010D6C
		public FieldAccessor(FieldInfo fieldInfo) : base(fieldInfo)
		{
			this._lateBoundFieldSet = new Lazy<LateBoundFieldSet>(() => MemberGetter.DelegateFactory.CreateSet(fieldInfo));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00012BA9 File Offset: 0x00010DA9
		public void SetValue(object destination, object value)
		{
			this._lateBoundFieldSet.Value(destination, value);
		}

		// Token: 0x040000DC RID: 220
		private readonly Lazy<LateBoundFieldSet> _lateBoundFieldSet;
	}
}
