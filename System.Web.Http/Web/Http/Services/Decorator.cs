using System;

namespace System.Web.Http.Services
{
	// Token: 0x0200009F RID: 159
	public static class Decorator
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
		public static T GetInner<T>(T outer)
		{
			T t = outer;
			IDecorator<T> decorator2;
			for (IDecorator<T> decorator = t as IDecorator<T>; decorator != null; decorator = decorator2)
			{
				t = decorator.Inner;
				decorator2 = (t as IDecorator<T>);
				if (decorator == decorator2)
				{
					break;
				}
			}
			return t;
		}
	}
}
