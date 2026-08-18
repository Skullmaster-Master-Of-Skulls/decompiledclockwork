using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc.Properties;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x020000D3 RID: 211
	[AttributeUsage(AttributeTargets.Property)]
	public class RemoteAttribute : ValidationAttribute, IClientValidatable
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0000F232 File Offset: 0x0000D432
		protected RemoteAttribute() : base(MvcResources.RemoteAttribute_RemoteValidationFailed)
		{
			this.RouteData = new RouteValueDictionary();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000F256 File Offset: 0x0000D456
		public RemoteAttribute(string routeName) : this()
		{
			if (string.IsNullOrWhiteSpace(routeName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "routeName");
			}
			this.RouteName = routeName;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000F27D File Offset: 0x0000D47D
		public RemoteAttribute(string action, string controller) : this(action, controller, null)
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000F288 File Offset: 0x0000D488
		public RemoteAttribute(string action, string controller, string areaName) : this()
		{
			if (string.IsNullOrWhiteSpace(action))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "action");
			}
			if (string.IsNullOrWhiteSpace(controller))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "controller");
			}
			this.RouteData["controller"] = controller;
			this.RouteData["action"] = action;
			if (!string.IsNullOrWhiteSpace(areaName))
			{
				this.RouteData["area"] = areaName;
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000F306 File Offset: 0x0000D506
		public RemoteAttribute(string action, string controller, AreaReference areaReference) : this(action, controller)
		{
			if (areaReference == AreaReference.UseRoot)
			{
				this.RouteData["area"] = null;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000F325 File Offset: 0x0000D525
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0000F32D File Offset: 0x0000D52D
		public string HttpMethod { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000F336 File Offset: 0x0000D536
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000F347 File Offset: 0x0000D547
		public string AdditionalFields
		{
			get
			{
				return this._additionalFields ?? string.Empty;
			}
			set
			{
				this._additionalFields = value;
				this._additonalFieldsSplit = AuthorizeAttribute.SplitString(value);
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000F35C File Offset: 0x0000D55C
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0000F364 File Offset: 0x0000D564
		private protected RouteValueDictionary RouteData { protected get; private set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000F36D File Offset: 0x0000D56D
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0000F375 File Offset: 0x0000D575
		protected string RouteName { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000F37E File Offset: 0x0000D57E
		protected virtual RouteCollection Routes
		{
			get
			{
				return RouteTable.Routes;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000F388 File Offset: 0x0000D588
		public string FormatAdditionalFieldsForClientValidation(string property)
		{
			if (string.IsNullOrEmpty(property))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "property");
			}
			string text = RemoteAttribute.FormatPropertyForClientValidation(property);
			foreach (string property2 in this._additonalFieldsSplit)
			{
				text = text + "," + RemoteAttribute.FormatPropertyForClientValidation(property2);
			}
			return text;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		public static string FormatPropertyForClientValidation(string property)
		{
			if (string.IsNullOrEmpty(property))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "property");
			}
			return "*." + property;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000F408 File Offset: 0x0000D608
		protected virtual string GetUrl(ControllerContext controllerContext)
		{
			VirtualPathData virtualPathForArea = this.Routes.GetVirtualPathForArea(controllerContext.RequestContext, this.RouteName, this.RouteData);
			if (virtualPathForArea == null)
			{
				throw new InvalidOperationException(MvcResources.RemoteAttribute_NoUrlFound);
			}
			return virtualPathForArea.VirtualPath;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000F448 File Offset: 0x0000D648
		public override string FormatErrorMessage(string name)
		{
			return string.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, new object[]
			{
				name
			});
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000F471 File Offset: 0x0000D671
		public override bool IsValid(object value)
		{
			return true;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000F59C File Offset: 0x0000D79C
		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientValidationRemoteRule(this.FormatErrorMessage(metadata.GetDisplayName()), this.GetUrl(context), this.HttpMethod, this.FormatAdditionalFieldsForClientValidation(metadata.PropertyName));
			yield break;
		}

		// Token: 0x04000181 RID: 385
		private string _additionalFields;

		// Token: 0x04000182 RID: 386
		private string[] _additonalFieldsSplit = new string[0];
	}
}
