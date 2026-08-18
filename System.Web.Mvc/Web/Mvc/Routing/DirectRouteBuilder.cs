using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000008 RID: 8
	internal class DirectRouteBuilder : IDirectRouteBuilder
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000028EC File Offset: 0x00000AEC
		public DirectRouteBuilder(IReadOnlyCollection<ActionDescriptor> actions, bool targetIsAction)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			this._actions = actions.ToArray<ActionDescriptor>();
			this._targetIsAction = targetIsAction;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002915 File Offset: 0x00000B15
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000291D File Offset: 0x00000B1D
		public string Name { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002926 File Offset: 0x00000B26
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000292E File Offset: 0x00000B2E
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000293E File Offset: 0x00000B3E
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002946 File Offset: 0x00000B46
		public RouteValueDictionary Defaults { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000294F File Offset: 0x00000B4F
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002957 File Offset: 0x00000B57
		public RouteValueDictionary Constraints { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002968 File Offset: 0x00000B68
		public RouteValueDictionary DataTokens { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002971 File Offset: 0x00000B71
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002979 File Offset: 0x00000B79
		internal ParsedRoute ParsedRoute { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002982 File Offset: 0x00000B82
		// (set) Token: 0x0600003F RID: 63 RVA: 0x0000298A File Offset: 0x00000B8A
		public int Order { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002993 File Offset: 0x00000B93
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000299B File Offset: 0x00000B9B
		public decimal Precedence { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000029A4 File Offset: 0x00000BA4
		public IReadOnlyCollection<ActionDescriptor> Actions
		{
			get
			{
				return this._actions;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000029AC File Offset: 0x00000BAC
		public bool TargetIsAction
		{
			get
			{
				return this._targetIsAction;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029B4 File Offset: 0x00000BB4
		public virtual RouteEntry Build()
		{
			if (this.ParsedRoute == null)
			{
				this.ParsedRoute = RouteParser.Parse(this.Template);
			}
			this.ValidateParameters(this.ParsedRoute);
			RouteValueDictionary routeValueDictionary = DirectRouteBuilder.Copy(this.Defaults) ?? new RouteValueDictionary();
			RouteValueDictionary constraints = DirectRouteBuilder.Copy(this.Constraints);
			RouteValueDictionary routeValueDictionary2 = DirectRouteBuilder.Copy(this.DataTokens) ?? new RouteValueDictionary();
			routeValueDictionary2["MS_DirectRouteActions"] = this._actions;
			int order = this.Order;
			if (order != 0)
			{
				routeValueDictionary2["MS_DirectRouteOrder"] = order;
			}
			decimal precedence = this.Precedence;
			if (precedence != 0m)
			{
				routeValueDictionary2["MS_DirectRoutePrecedence"] = precedence;
			}
			ControllerDescriptor controllerDescriptor = this.GetControllerDescriptor();
			if (controllerDescriptor != null)
			{
				routeValueDictionary["controller"] = controllerDescriptor.ControllerName;
			}
			if (this.TargetIsAction && this._actions.Length == 1)
			{
				ActionDescriptor actionDescriptor = this._actions[0];
				routeValueDictionary["action"] = actionDescriptor.ActionName;
				routeValueDictionary2["MS_DirectRouteTargetIsAction"] = true;
			}
			RouteAreaAttribute areaFrom = controllerDescriptor.GetAreaFrom();
			string areaName = controllerDescriptor.GetAreaName(areaFrom);
			if (areaName != null)
			{
				routeValueDictionary2["area"] = areaName;
				routeValueDictionary2["UseNamespaceFallback"] = false;
				Type controllerType = controllerDescriptor.ControllerType;
				if (controllerType != null)
				{
					routeValueDictionary2["Namespaces"] = new string[]
					{
						controllerType.Namespace
					};
				}
			}
			Route route = new Route(this.Template, routeValueDictionary, constraints, routeValueDictionary2, null);
			ConstraintValidation.Validate(route);
			return new RouteEntry(this.Name, route);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B60 File Offset: 0x00000D60
		internal virtual void ValidateParameters(ParsedRoute parsedRoute)
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
									throw Error.InvalidOperation(MvcResources.DirectRoute_InvalidParameter_Controller, new object[0]);
								}
								if (this.TargetIsAction && string.Equals(pathParameterSubsegment.ParameterName, "action", StringComparison.OrdinalIgnoreCase))
								{
									throw Error.InvalidOperation(MvcResources.DirectRoute_InvalidParameter_Action, new object[0]);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C5C File Offset: 0x00000E5C
		internal static void ValidateRouteEntry(RouteEntry entry)
		{
			Route route = entry.Route;
			ActionDescriptor[] targetActionDescriptors = route.GetTargetActionDescriptors();
			if (targetActionDescriptors == null || targetActionDescriptors.Length == 0)
			{
				throw new InvalidOperationException(MvcResources.DirectRoute_MissingActionDescriptors);
			}
			if (route.RouteHandler != null)
			{
				throw new InvalidOperationException(MvcResources.DirectRoute_RouteHandlerNotSupported);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C9D File Offset: 0x00000E9D
		private static RouteValueDictionary Copy(RouteValueDictionary routeDictionary)
		{
			if (routeDictionary == null)
			{
				return null;
			}
			return new RouteValueDictionary(routeDictionary);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002CAC File Offset: 0x00000EAC
		private ControllerDescriptor GetControllerDescriptor()
		{
			ControllerDescriptor controllerDescriptor = null;
			foreach (ActionDescriptor actionDescriptor in this._actions)
			{
				if (controllerDescriptor == null)
				{
					controllerDescriptor = actionDescriptor.ControllerDescriptor;
				}
				else if (actionDescriptor.ControllerDescriptor != controllerDescriptor)
				{
					controllerDescriptor = null;
					break;
				}
			}
			return controllerDescriptor;
		}

		// Token: 0x04000006 RID: 6
		private readonly ActionDescriptor[] _actions;

		// Token: 0x04000007 RID: 7
		private readonly bool _targetIsAction;

		// Token: 0x04000008 RID: 8
		private string _template;
	}
}
