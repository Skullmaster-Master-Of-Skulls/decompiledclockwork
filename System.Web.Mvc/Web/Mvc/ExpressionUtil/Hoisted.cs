using System;
using System.Collections.Generic;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B0 RID: 176
	// (Invoke) Token: 0x060004D0 RID: 1232
	internal delegate TValue Hoisted<TModel, TValue>(TModel model, List<object> capturedConstants);
}
