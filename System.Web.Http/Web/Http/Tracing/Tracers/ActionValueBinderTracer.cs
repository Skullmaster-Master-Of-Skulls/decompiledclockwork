using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000160 RID: 352
	internal class ActionValueBinderTracer : IActionValueBinder, IDecorator<IActionValueBinder>
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0001D1AA File Offset: 0x0001B3AA
		public ActionValueBinderTracer(IActionValueBinder innerBinder, ITraceWriter traceWriter)
		{
			this._innerBinder = innerBinder;
			this._traceWriter = traceWriter;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0001D1C0 File Offset: 0x0001B3C0
		public IActionValueBinder Inner
		{
			get
			{
				return this._innerBinder;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
		HttpActionBinding IActionValueBinder.GetBinding(HttpActionDescriptor actionDescriptor)
		{
			HttpActionBinding binding = this._innerBinder.GetBinding(actionDescriptor);
			if (binding == null)
			{
				return null;
			}
			HttpParameterBinding[] parameterBindings = binding.ParameterBindings;
			HttpParameterBinding[] array = new HttpParameterBinding[parameterBindings.Length];
			for (int i = 0; i < array.Length; i++)
			{
				HttpParameterBinding httpParameterBinding = parameterBindings[i];
				FormatterParameterBinding formatterParameterBinding = httpParameterBinding as FormatterParameterBinding;
				array[i] = ((formatterParameterBinding != null) ? new FormatterParameterBindingTracer(formatterParameterBinding, this._traceWriter) : new HttpParameterBindingTracer(httpParameterBinding, this._traceWriter));
			}
			binding.ParameterBindings = array;
			if (!(binding is HttpActionBindingTracer))
			{
				return new HttpActionBindingTracer(binding, this._traceWriter);
			}
			return binding;
		}

		// Token: 0x040002A0 RID: 672
		private readonly IActionValueBinder _innerBinder;

		// Token: 0x040002A1 RID: 673
		private readonly ITraceWriter _traceWriter;
	}
}
