using System;
using System.Data.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000158 RID: 344
	public class LinqBinaryModelBinder : ByteArrayModelBinder
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x000183E0 File Offset: 0x000165E0
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			byte[] array = (byte[])base.BindModel(controllerContext, bindingContext);
			if (array == null)
			{
				return null;
			}
			return new Binary(array);
		}
	}
}
