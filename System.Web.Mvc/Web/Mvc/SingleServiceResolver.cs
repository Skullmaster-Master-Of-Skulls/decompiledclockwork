using System;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020000D4 RID: 212
	internal class SingleServiceResolver<TService> : IResolver<TService> where TService : class
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		public SingleServiceResolver(Func<TService> currentValueThunk, TService defaultValue, string callerMethodName)
		{
			if (currentValueThunk == null)
			{
				throw new ArgumentNullException("currentValueThunk");
			}
			if (defaultValue == null)
			{
				throw new ArgumentNullException("defaultValue");
			}
			this._resolverThunk = (() => DependencyResolver.Current);
			this._currentValueFromResolver = new Lazy<TService>(new Func<TService>(this.GetValueFromResolver));
			this._currentValueThunk = currentValueThunk;
			this._defaultValue = defaultValue;
			this._callerMethodName = callerMethodName;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000F664 File Offset: 0x0000D864
		internal SingleServiceResolver(Func<TService> staticAccessor, TService defaultValue, IDependencyResolver resolver, string callerMethodName) : this(staticAccessor, defaultValue, callerMethodName)
		{
			if (resolver != null)
			{
				this._resolverThunk = (() => resolver);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000F6A9 File Offset: 0x0000D8A9
		public TService Current
		{
			get
			{
				TService result;
				if ((result = this._currentValueFromResolver.Value) == null && (result = this._currentValueThunk()) == null)
				{
					result = this._defaultValue;
				}
				return result;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		private TService GetValueFromResolver()
		{
			TService service = this._resolverThunk().GetService<TService>();
			if (service != null && this._currentValueThunk() != null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.SingleServiceResolver_CannotRegisterTwoInstances, new object[]
				{
					typeof(TService).Name.ToString(),
					this._callerMethodName
				}));
			}
			return service;
		}

		// Token: 0x04000186 RID: 390
		private Lazy<TService> _currentValueFromResolver;

		// Token: 0x04000187 RID: 391
		private Func<TService> _currentValueThunk;

		// Token: 0x04000188 RID: 392
		private TService _defaultValue;

		// Token: 0x04000189 RID: 393
		private Func<IDependencyResolver> _resolverThunk;

		// Token: 0x0400018A RID: 394
		private string _callerMethodName;
	}
}
