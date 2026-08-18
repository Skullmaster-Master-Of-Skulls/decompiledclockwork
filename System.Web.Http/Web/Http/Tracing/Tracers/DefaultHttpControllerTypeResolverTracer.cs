using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x020000AA RID: 170
	internal class DefaultHttpControllerTypeResolverTracer : DefaultHttpControllerTypeResolver, IDecorator<DefaultHttpControllerTypeResolver>
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000C83C File Offset: 0x0000AA3C
		public DefaultHttpControllerTypeResolverTracer(DefaultHttpControllerTypeResolver innerResolver, ITraceWriter traceWriter)
		{
			this._innerResolver = innerResolver;
			this._traceWriter = traceWriter;
			this._innerTypeName = this._innerResolver.GetType().Name;
			this._innerResolver.SetGetTypesFunc(new Func<Assembly, Type[]>(this.GetTypesAndTrace));
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000C88A File Offset: 0x0000AA8A
		public DefaultHttpControllerTypeResolver Inner
		{
			get
			{
				return this._innerResolver;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C892 File Offset: 0x0000AA92
		protected internal override Predicate<Type> IsControllerTypePredicate
		{
			get
			{
				return this._innerResolver.IsControllerTypePredicate;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		public override ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
		{
			ICollection<Type> result = null;
			this._traceWriter.TraceBeginEnd(null, TraceCategories.ControllersCategory, TraceLevel.Debug, this._innerTypeName, "GetControllerTypes", null, delegate
			{
				result = this._innerResolver.GetControllerTypes(assembliesResolver);
			}, null, null);
			return result;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000C924 File Offset: 0x0000AB24
		private Type[] GetTypesAndTrace(Assembly assembly)
		{
			Type[] types;
			try
			{
				types = DefaultHttpControllerTypeResolver.GetTypes(assembly);
			}
			catch (Exception exception)
			{
				this._traceWriter.Warn(null, TraceCategories.ControllersCategory, exception, SRResources.TraceHttpControllerTypeResolverError, new object[]
				{
					assembly.FullName
				});
				throw;
			}
			return types;
		}

		// Token: 0x04000128 RID: 296
		private readonly DefaultHttpControllerTypeResolver _innerResolver;

		// Token: 0x04000129 RID: 297
		private readonly ITraceWriter _traceWriter;

		// Token: 0x0400012A RID: 298
		private readonly string _innerTypeName;
	}
}
