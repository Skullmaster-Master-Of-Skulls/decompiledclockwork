using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x02000197 RID: 407
	[FormCollection.FormCollectionBinderAttribute]
	public sealed class FormCollection : NameValueCollection, IValueProvider
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x0001E59B File Offset: 0x0001C79B
		public FormCollection()
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0001E5A3 File Offset: 0x0001C7A3
		public FormCollection(NameValueCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			base.Add(collection);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		internal FormCollection(ControllerBase controller, Func<NameValueCollection> validatedValuesThunk, Func<NameValueCollection> unvalidatedValuesThunk)
		{
			base.Add((controller == null || controller.ValidateRequest) ? validatedValuesThunk() : unvalidatedValuesThunk());
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		public ValueProviderResult GetValue(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			string[] values = this.GetValues(name);
			if (values == null)
			{
				return null;
			}
			string attemptedValue = base[name];
			return new ValueProviderResult(values, attemptedValue, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0001E624 File Offset: 0x0001C824
		public IValueProvider ToValueProvider()
		{
			return this;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0001E627 File Offset: 0x0001C827
		bool IValueProvider.ContainsPrefix(string prefix)
		{
			return ValueProviderUtil.CollectionContainsPrefix(this.AllKeys, prefix);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0001E635 File Offset: 0x0001C835
		ValueProviderResult IValueProvider.GetValue(string key)
		{
			return this.GetValue(key);
		}

		// Token: 0x02000199 RID: 409
		private sealed class FormCollectionBinderAttribute : CustomModelBinderAttribute
		{
			// Token: 0x06000B90 RID: 2960 RVA: 0x0001E646 File Offset: 0x0001C846
			public override IModelBinder GetBinder()
			{
				return FormCollection.FormCollectionBinderAttribute._binder;
			}

			// Token: 0x04000311 RID: 785
			private static readonly FormCollection.FormCollectionBinderAttribute.FormCollectionModelBinder _binder = new FormCollection.FormCollectionBinderAttribute.FormCollectionModelBinder();

			// Token: 0x0200019A RID: 410
			private sealed class FormCollectionModelBinder : IModelBinder
			{
				// Token: 0x06000B93 RID: 2963 RVA: 0x0001E69C File Offset: 0x0001C89C
				public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
				{
					if (controllerContext == null)
					{
						throw new ArgumentNullException("controllerContext");
					}
					return new FormCollection(controllerContext.Controller, () => controllerContext.HttpContext.Request.Form, () => controllerContext.HttpContext.Request.Unvalidated.Form);
				}
			}
		}
	}
}
