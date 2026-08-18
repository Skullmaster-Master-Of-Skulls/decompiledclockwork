using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.Helpers
{
	// Token: 0x0200000E RID: 14
	public class DynamicJsonArray : DynamicObject, IEnumerable<object>, IEnumerable
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003A94 File Offset: 0x00001C94
		public DynamicJsonArray(object[] arrayValues)
		{
			this._arrayValues = arrayValues.Select(new Func<object, object>(Json.WrapObject)).ToArray<object>();
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003AB9 File Offset: 0x00001CB9
		public int Length
		{
			get
			{
				return this._arrayValues.Length;
			}
		}

		// Token: 0x1700001C RID: 28
		[Dynamic]
		public dynamic this[int index]
		{
			[return: Dynamic]
			get
			{
				return this._arrayValues[index];
			}
			[param: Dynamic]
			set
			{
				object[] arrayValues = this._arrayValues;
				if (DynamicJsonArray.<set_Item>o__SiteContainer0.<>p__Site1 == null)
				{
					DynamicJsonArray.<set_Item>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "WrapObject", null, typeof(DynamicJsonArray), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				arrayValues[index] = DynamicJsonArray.<set_Item>o__SiteContainer0.<>p__Site1.Target(DynamicJsonArray.<set_Item>o__SiteContainer0.<>p__Site1, typeof(Json), value);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003B48 File Offset: 0x00001D48
		public override bool TryConvert(ConvertBinder binder, out object result)
		{
			if (this._arrayValues.GetType().IsAssignableFrom(binder.Type))
			{
				result = this._arrayValues;
				return true;
			}
			return base.TryConvert(binder, out result);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B74 File Offset: 0x00001D74
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003B7A File Offset: 0x00001D7A
		public IEnumerator GetEnumerator()
		{
			return this._arrayValues.GetEnumerator();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003B87 File Offset: 0x00001D87
		private IEnumerable<object> GetEnumerable()
		{
			return this._arrayValues.AsEnumerable<object>();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003B94 File Offset: 0x00001D94
		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			return this.GetEnumerable().GetEnumerator();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public static implicit operator object[](DynamicJsonArray obj)
		{
			return obj._arrayValues;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003BA9 File Offset: 0x00001DA9
		public static implicit operator Array(DynamicJsonArray obj)
		{
			return obj._arrayValues;
		}

		// Token: 0x0400002E RID: 46
		private readonly object[] _arrayValues;

		// Token: 0x02000033 RID: 51
		[CompilerGenerated]
		private static class <set_Item>o__SiteContainer0
		{
			// Token: 0x040000CE RID: 206
			public static CallSite<Func<CallSite, Type, object, object>> <>p__Site1;
		}
	}
}
