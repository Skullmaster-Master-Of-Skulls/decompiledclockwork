using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Reflection;
using System.Threading;

namespace System.Web.Mvc
{
	// Token: 0x020001C0 RID: 448
	public static class ModelBinders
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x000232CC File Offset: 0x000214CC
		public static ModelBinderDictionary Binders
		{
			get
			{
				return ModelBinders._binders;
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000232D4 File Offset: 0x000214D4
		internal static IModelBinder GetBinderFromAttributes(Type type, Action<Type> errorAction)
		{
			AttributeList list = new AttributeList(TypeDescriptorHelper.Get(type).GetAttributes());
			CustomModelBinderAttribute customModelBinderAttribute = list.SingleOfTypeDefaultOrError(errorAction, type);
			if (customModelBinderAttribute != null)
			{
				return customModelBinderAttribute.GetBinder();
			}
			return null;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00023308 File Offset: 0x00021508
		internal static IModelBinder GetBinderFromAttributes(ICustomAttributeProvider element, Action<ICustomAttributeProvider> errorAction)
		{
			CustomModelBinderAttribute[] array = (CustomModelBinderAttribute[])element.GetCustomAttributes(typeof(CustomModelBinderAttribute), true);
			if (array == null)
			{
				return null;
			}
			CustomModelBinderAttribute customModelBinderAttribute = array.SingleDefaultOrError(errorAction, element);
			if (customModelBinderAttribute != null)
			{
				return customModelBinderAttribute.GetBinder();
			}
			return null;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00023348 File Offset: 0x00021548
		private static ModelBinderDictionary CreateDefaultBinderDictionary()
		{
			return new ModelBinderDictionary
			{
				{
					typeof(HttpPostedFileBase),
					new HttpPostedFileBaseModelBinder()
				},
				{
					typeof(byte[]),
					new ByteArrayModelBinder()
				},
				{
					typeof(Binary),
					new LinqBinaryModelBinder()
				},
				{
					typeof(CancellationToken),
					new CancellationTokenModelBinder()
				}
			};
		}

		// Token: 0x04000367 RID: 871
		private static readonly ModelBinderDictionary _binders = ModelBinders.CreateDefaultBinderDictionary();
	}
}
