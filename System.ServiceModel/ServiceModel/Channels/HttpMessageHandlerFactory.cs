using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000880 RID: 2176
	public class HttpMessageHandlerFactory
	{
		// Token: 0x06005287 RID: 21127 RVA: 0x00130410 File Offset: 0x0012E610
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public HttpMessageHandlerFactory(params Type[] handlers)
		{
			if (handlers == null)
			{
				throw FxTrace.Exception.ArgumentNull("handlers");
			}
			if (handlers.Length == 0)
			{
				throw FxTrace.Exception.Argument("handlers", SR.GetString("InputTypeListEmptyError"));
			}
			this.handlerCtors = new ConstructorInfo[handlers.Length];
			for (int i = 0; i < handlers.Length; i++)
			{
				Type type = handlers[i];
				if (type == null)
				{
					throw FxTrace.Exception.Argument(string.Format(CultureInfo.InvariantCulture, "handlers[<<{0}>>]", new object[]
					{
						i
					}), SR.GetString("HttpMessageHandlerTypeNotSupported", new object[]
					{
						"null",
						HttpMessageHandlerFactory.delegatingHandlerType.Name
					}));
				}
				if (!HttpMessageHandlerFactory.delegatingHandlerType.IsAssignableFrom(type) || type.IsAbstract)
				{
					throw FxTrace.Exception.Argument(string.Format(CultureInfo.InvariantCulture, "handlers[<<{0}>>]", new object[]
					{
						i
					}), SR.GetString("HttpMessageHandlerTypeNotSupported", new object[]
					{
						type.Name,
						HttpMessageHandlerFactory.delegatingHandlerType.Name
					}));
				}
				ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
				if (constructor == null)
				{
					throw FxTrace.Exception.Argument(string.Format(CultureInfo.InvariantCulture, "handlers[<<{0}>>]", new object[]
					{
						i
					}), SR.GetString("HttpMessageHandlerTypeNotSupported", new object[]
					{
						type.Name,
						HttpMessageHandlerFactory.delegatingHandlerType.Name
					}));
				}
				this.handlerCtors[i] = constructor;
			}
			this.httpMessageHandlers = handlers;
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x001305A9 File Offset: 0x0012E7A9
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public HttpMessageHandlerFactory(Func<IEnumerable<DelegatingHandler>> handlers)
		{
			if (handlers == null)
			{
				throw FxTrace.Exception.ArgumentNull("handlers");
			}
			this.handlerFunc = handlers;
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x001305CB File Offset: 0x0012E7CB
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected HttpMessageHandlerFactory()
		{
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x001305D3 File Offset: 0x0012E7D3
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public HttpMessageHandler Create(HttpMessageHandler innerChannel)
		{
			if (innerChannel == null)
			{
				throw FxTrace.Exception.ArgumentNull("innerChannel");
			}
			return this.OnCreate(innerChannel);
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x001305F0 File Offset: 0x0012E7F0
		internal static HttpMessageHandlerFactory CreateFromConfigurationElement(HttpMessageHandlerFactoryElement configElement)
		{
			if (!string.IsNullOrWhiteSpace(configElement.Type))
			{
				if (configElement.Handlers != null && configElement.Handlers.Count > 0)
				{
					throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.GetString("HttpMessageHandlerFactoryConfigInvalid_WithBothTypeAndHandlerList", new object[]
					{
						"messageHandlerFactory",
						"type",
						"handlers"
					})));
				}
				Type typeFromAssembliesInCurrentDomain = HttpChannelUtilities.GetTypeFromAssembliesInCurrentDomain(configElement.Type);
				if (typeFromAssembliesInCurrentDomain == null)
				{
					throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.GetString("CanNotLoadTypeGotFromConfig", new object[]
					{
						configElement.Type
					})));
				}
				if (!typeof(HttpMessageHandlerFactory).IsAssignableFrom(typeFromAssembliesInCurrentDomain) || typeFromAssembliesInCurrentDomain.IsAbstract)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("WebSocketElementConfigInvalidHttpMessageHandlerFactoryType", new object[]
					{
						typeof(HttpMessageHandlerFactory).Name,
						typeFromAssembliesInCurrentDomain,
						typeof(HttpMessageHandlerFactory).AssemblyQualifiedName
					})));
				}
				return Activator.CreateInstance(typeFromAssembliesInCurrentDomain) as HttpMessageHandlerFactory;
			}
			else
			{
				if (configElement.Handlers == null || configElement.Handlers.Count == 0)
				{
					return null;
				}
				Type[] array = new Type[configElement.Handlers.Count];
				for (int i = 0; i < configElement.Handlers.Count; i++)
				{
					Type typeFromAssembliesInCurrentDomain2 = HttpChannelUtilities.GetTypeFromAssembliesInCurrentDomain(configElement.Handlers[i].Type);
					if (typeFromAssembliesInCurrentDomain2 == null)
					{
						throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.GetString("CanNotLoadTypeGotFromConfig", new object[]
						{
							configElement.Handlers[i].Type
						})));
					}
					array[i] = typeFromAssembliesInCurrentDomain2;
				}
				HttpMessageHandlerFactory result;
				try
				{
					result = new HttpMessageHandlerFactory(array);
				}
				catch (ArgumentException ex)
				{
					throw FxTrace.Exception.AsError(new ConfigurationErrorsException(ex.Message, ex));
				}
				return result;
			}
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x001307D8 File Offset: 0x0012E9D8
		internal HttpMessageHandlerFactoryElement GenerateConfigurationElement()
		{
			if (this.handlerFunc != null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.GetString("HttpMessageHandlerFactoryWithFuncCannotGenerateConfig", new object[]
				{
					typeof(HttpMessageHandlerFactory).Name,
					typeof(Func<IEnumerable<DelegatingHandler>>).Name
				})));
			}
			Type type = base.GetType();
			if (type != typeof(HttpMessageHandlerFactory))
			{
				return new HttpMessageHandlerFactoryElement
				{
					Type = type.AssemblyQualifiedName
				};
			}
			if (this.httpMessageHandlers != null)
			{
				DelegatingHandlerElementCollection delegatingHandlerElementCollection = new DelegatingHandlerElementCollection();
				for (int i = 0; i < this.httpMessageHandlers.Length; i++)
				{
					delegatingHandlerElementCollection.Add(new DelegatingHandlerElement(this.httpMessageHandlers[i]));
				}
				return new HttpMessageHandlerFactoryElement
				{
					Handlers = delegatingHandlerElementCollection
				};
			}
			return null;
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x001308A0 File Offset: 0x0012EAA0
		protected virtual HttpMessageHandler OnCreate(HttpMessageHandler innerChannel)
		{
			if (innerChannel == null)
			{
				throw FxTrace.Exception.ArgumentNull("innerChannel");
			}
			IEnumerable<DelegatingHandler> enumerable = null;
			try
			{
				if (this.handlerFunc != null)
				{
					enumerable = this.handlerFunc();
					if (enumerable == null)
					{
						goto IL_E3;
					}
					using (IEnumerator<DelegatingHandler> enumerator = enumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == null)
							{
								throw FxTrace.Exception.Argument("handlers", SR.GetString("DelegatingHandlerArrayFromFuncContainsNullItem", new object[]
								{
									HttpMessageHandlerFactory.delegatingHandlerType.Name,
									HttpMessageHandlerFactory.GetFuncDetails(this.handlerFunc)
								}));
							}
						}
						goto IL_E3;
					}
				}
				if (this.handlerCtors != null)
				{
					DelegatingHandler[] array = new DelegatingHandler[this.handlerCtors.Length];
					for (int i = 0; i < this.handlerCtors.Length; i++)
					{
						DelegatingHandler[] array2 = array;
						int num = i;
						ConstructorInfo constructorInfo = this.handlerCtors[i];
						object[] emptyTypes = Type.EmptyTypes;
						array2[num] = (DelegatingHandler)constructorInfo.Invoke(emptyTypes);
					}
					enumerable = array;
				}
				IL_E3:;
			}
			catch (TargetInvocationException exception)
			{
				throw FxTrace.Exception.AsError(exception);
			}
			HttpMessageHandler httpMessageHandler = innerChannel;
			if (enumerable != null)
			{
				foreach (DelegatingHandler delegatingHandler in enumerable)
				{
					if (delegatingHandler.InnerHandler != null)
					{
						throw FxTrace.Exception.Argument("handlers", SR.GetString("DelegatingHandlerArrayHasNonNullInnerHandler", new object[]
						{
							HttpMessageHandlerFactory.delegatingHandlerType.Name,
							"InnerHandler",
							delegatingHandler.GetType().Name
						}));
					}
					delegatingHandler.InnerHandler = httpMessageHandler;
					httpMessageHandler = delegatingHandler;
				}
			}
			return httpMessageHandler;
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x00130A54 File Offset: 0x0012EC54
		private static string GetFuncDetails(Func<IEnumerable<DelegatingHandler>> func)
		{
			MethodInfo method = func.Method;
			Type declaringType = method.DeclaringType;
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				declaringType.FullName,
				method.Name
			});
		}

		// Token: 0x04003278 RID: 12920
		private static readonly Type delegatingHandlerType = typeof(DelegatingHandler);

		// Token: 0x04003279 RID: 12921
		private Type[] httpMessageHandlers;

		// Token: 0x0400327A RID: 12922
		private ConstructorInfo[] handlerCtors;

		// Token: 0x0400327B RID: 12923
		private Func<IEnumerable<DelegatingHandler>> handlerFunc;
	}
}
