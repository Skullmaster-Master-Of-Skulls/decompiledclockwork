using System;
using System.Collections.ObjectModel;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000D8 RID: 216
	public class ParameterBindingRulesCollection : Collection<Func<HttpParameterDescriptor, HttpParameterBinding>>
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x00011208 File Offset: 0x0000F408
		private static Func<HttpParameterDescriptor, HttpParameterBinding> TypeCheck(Type type, Func<HttpParameterDescriptor, HttpParameterBinding> func)
		{
			return delegate(HttpParameterDescriptor param)
			{
				if (!(param.ParameterType == type))
				{
					return null;
				}
				return func(param);
			};
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00011235 File Offset: 0x0000F435
		public void Add(Type typeMatch, Func<HttpParameterDescriptor, HttpParameterBinding> funcInner)
		{
			base.Add(ParameterBindingRulesCollection.TypeCheck(typeMatch, funcInner));
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00011244 File Offset: 0x0000F444
		public void Insert(int index, Type typeMatch, Func<HttpParameterDescriptor, HttpParameterBinding> funcInner)
		{
			base.Insert(index, ParameterBindingRulesCollection.TypeCheck(typeMatch, funcInner));
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00011254 File Offset: 0x0000F454
		public HttpParameterBinding LookupBinding(HttpParameterDescriptor parameter)
		{
			foreach (Func<HttpParameterDescriptor, HttpParameterBinding> func in this)
			{
				HttpParameterBinding httpParameterBinding = func(parameter);
				if (httpParameterBinding != null)
				{
					return httpParameterBinding;
				}
			}
			return null;
		}
	}
}
