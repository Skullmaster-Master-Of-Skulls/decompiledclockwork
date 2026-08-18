using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200000D RID: 13
	internal class DirectRouteBuilder : IDirectRouteBuilder
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000309C File Offset: 0x0000129C
		public DirectRouteBuilder(IReadOnlyCollection<HttpActionDescriptor> actions, bool targetIsAction)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			this._actions = actions.ToArray<HttpActionDescriptor>();
			this._targetIsAction = targetIsAction;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000030C5 File Offset: 0x000012C5
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000030CD File Offset: 0x000012CD
		public string Name { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000030D6 File Offset: 0x000012D6
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000030DE File Offset: 0x000012DE
		public string Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this.ParsedRoute = null;
				this._template = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000030EE File Offset: 0x000012EE
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000030F6 File Offset: 0x000012F6
		public IDictionary<string, object> Defaults { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000030FF File Offset: 0x000012FF
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003107 File Offset: 0x00001307
		public IDictionary<string, object> Constraints { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003110 File Offset: 0x00001310
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003118 File Offset: 0x00001318
		public IDictionary<string, object> DataTokens { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003121 File Offset: 0x00001321
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003129 File Offset: 0x00001329
		internal HttpParsedRoute ParsedRoute { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003132 File Offset: 0x00001332
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000313A File Offset: 0x0000133A
		public int Order { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003143 File Offset: 0x00001343
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000314B File Offset: 0x0000134B
		public decimal Precedence { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003154 File Offset: 0x00001354
		public IReadOnlyCollection<HttpActionDescriptor> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000315C File Offset: 0x0000135C
		public bool TargetIsAction
		{
			get
			{
				return this._targetIsAction;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003164 File Offset: 0x00001364
		public virtual RouteEntry Build()
		{
			if (this.ParsedRoute == null)
			{
				this.ParsedRoute = RouteParser.Parse(this.Template);
			}
			this.ValidateParameters(this.ParsedRoute);
			HttpRouteValueDictionary defaults = DirectRouteBuilder.Copy(this.Defaults);
			HttpRouteValueDictionary httpRouteValueDictionary = DirectRouteBuilder.Copy(this.Constraints);
			HttpRouteValueDictionary httpRouteValueDictionary2 = DirectRouteBuilder.Copy(this.DataTokens) ?? new HttpRouteValueDictionary();
			httpRouteValueDictionary2["actions"] = this._actions;
			if (!this.TargetIsAction)
			{
				httpRouteValueDictionary2["controller"] = this._actions[0].ControllerDescriptor;
			}
			int order = this.Order;
			if (order != 0)
			{
				httpRouteValueDictionary2["order"] = order;
			}
			decimal precedence = this.Precedence;
			if (precedence != 0m)
			{
				httpRouteValueDictionary2["precedence"] = precedence;
			}
			if (httpRouteValueDictionary != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in httpRouteValueDictionary)
				{
					HttpRoute.ValidateConstraint(this.Template, keyValuePair.Key, keyValuePair.Value);
				}
			}
			HttpMessageHandler handler = null;
			IHttpRoute route = new HttpRoute(this.Template, defaults, httpRouteValueDictionary, httpRouteValueDictionary2, handler, this.ParsedRoute);
			return new RouteEntry(this.Name, route);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032B8 File Offset: 0x000014B8
		internal virtual void ValidateParameters(HttpParsedRoute parsedRoute)
		{
			if (parsedRoute.PathSegments != null)
			{
				foreach (PathContentSegment pathContentSegment in parsedRoute.PathSegments.OfType<PathContentSegment>())
				{
					if (pathContentSegment != null && pathContentSegment.Subsegments != null)
					{
						foreach (PathParameterSubsegment pathParameterSubsegment in pathContentSegment.Subsegments.OfType<PathParameterSubsegment>())
						{
							if (pathParameterSubsegment != null)
							{
								if (string.Equals(pathParameterSubsegment.ParameterName, "controller", StringComparison.OrdinalIgnoreCase))
								{
									throw Error.InvalidOperation(SRResources.DirectRoute_InvalidParameter_Controller, new object[0]);
								}
								if (this.TargetIsAction && string.Equals(pathParameterSubsegment.ParameterName, "action", StringComparison.OrdinalIgnoreCase))
								{
									throw Error.InvalidOperation(SRResources.DirectRoute_InvalidParameter_Action, new object[0]);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033B4 File Offset: 0x000015B4
		internal static void ValidateRouteEntry(RouteEntry entry)
		{
			IHttpRoute route = entry.Route;
			HttpActionDescriptor[] targetActionDescriptors = route.GetTargetActionDescriptors();
			if (targetActionDescriptors == null || targetActionDescriptors.Length == 0)
			{
				throw new InvalidOperationException(SRResources.DirectRoute_MissingActionDescriptors);
			}
			if (route.Handler != null)
			{
				throw new InvalidOperationException(SRResources.DirectRoute_HandlerNotSupported);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000033F5 File Offset: 0x000015F5
		private static HttpRouteValueDictionary Copy(IDictionary<string, object> routeDictionary)
		{
			if (routeDictionary == null)
			{
				return null;
			}
			return new HttpRouteValueDictionary(routeDictionary);
		}

		// Token: 0x0400000F RID: 15
		private readonly HttpActionDescriptor[] _actions;

		// Token: 0x04000010 RID: 16
		private readonly bool _targetIsAction;

		// Token: 0x04000011 RID: 17
		private string _template;
	}
}
