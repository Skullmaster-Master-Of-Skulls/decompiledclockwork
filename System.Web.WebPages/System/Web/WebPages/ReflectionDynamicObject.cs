using System;
using System.Dynamic;
using System.Globalization;
using System.Reflection;

namespace System.Web.WebPages
{
	// Token: 0x0200008B RID: 139
	internal sealed class ReflectionDynamicObject : DynamicObject
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000D804 File Offset: 0x0000BA04
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000D80C File Offset: 0x0000BA0C
		private object RealObject { get; set; }

		// Token: 0x06000453 RID: 1107 RVA: 0x0000D818 File Offset: 0x0000BA18
		public static object WrapObjectIfInternal(object o)
		{
			if (o == null)
			{
				return null;
			}
			if (o.GetType().IsPublic)
			{
				return o;
			}
			return new ReflectionDynamicObject
			{
				RealObject = o
			};
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000D848 File Offset: 0x0000BA48
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			PropertyInfo property = this.RealObject.GetType().GetProperty(binder.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			if (property == null)
			{
				result = null;
			}
			else
			{
				result = property.GetValue(this.RealObject, null);
				result = ReflectionDynamicObject.WrapObjectIfInternal(result);
			}
			return true;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000D898 File Offset: 0x0000BA98
		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = this.RealObject.GetType().InvokeMember(binder.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, this.RealObject, args, CultureInfo.InvariantCulture);
			return true;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000D8C5 File Offset: 0x0000BAC5
		public override bool TryConvert(ConvertBinder binder, out object result)
		{
			result = this.RealObject;
			return true;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		public override string ToString()
		{
			return this.RealObject.ToString();
		}
	}
}
