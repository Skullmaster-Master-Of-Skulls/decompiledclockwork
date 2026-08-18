using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Web.Mvc.Ajax
{
	// Token: 0x02000176 RID: 374
	public class AjaxOptions
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001B176 File Offset: 0x00019376
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0001B187 File Offset: 0x00019387
		public string Confirm
		{
			get
			{
				return this._confirm ?? string.Empty;
			}
			set
			{
				this._confirm = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0001B190 File Offset: 0x00019390
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0001B1A1 File Offset: 0x000193A1
		public string HttpMethod
		{
			get
			{
				return this._httpMethod ?? string.Empty;
			}
			set
			{
				this._httpMethod = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001B1AA File Offset: 0x000193AA
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x0001B1B4 File Offset: 0x000193B4
		public InsertionMode InsertionMode
		{
			get
			{
				return this._insertionMode;
			}
			set
			{
				switch (value)
				{
				case InsertionMode.Replace:
				case InsertionMode.InsertBefore:
				case InsertionMode.InsertAfter:
				case InsertionMode.ReplaceWith:
					this._insertionMode = value;
					return;
				default:
					throw new ArgumentOutOfRangeException("value");
				}
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0001B1F0 File Offset: 0x000193F0
		internal string InsertionModeString
		{
			get
			{
				switch (this.InsertionMode)
				{
				case InsertionMode.Replace:
					return "Sys.Mvc.InsertionMode.replace";
				case InsertionMode.InsertBefore:
					return "Sys.Mvc.InsertionMode.insertBefore";
				case InsertionMode.InsertAfter:
					return "Sys.Mvc.InsertionMode.insertAfter";
				default:
					return ((int)this.InsertionMode).ToString(CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0001B240 File Offset: 0x00019440
		internal string InsertionModeUnobtrusive
		{
			get
			{
				switch (this.InsertionMode)
				{
				case InsertionMode.Replace:
					return "replace";
				case InsertionMode.InsertBefore:
					return "before";
				case InsertionMode.InsertAfter:
					return "after";
				case InsertionMode.ReplaceWith:
					return "replace-with";
				default:
					return ((int)this.InsertionMode).ToString(CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0001B297 File Offset: 0x00019497
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x0001B29F File Offset: 0x0001949F
		public int LoadingElementDuration { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0001B2A8 File Offset: 0x000194A8
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x0001B2B9 File Offset: 0x000194B9
		public string LoadingElementId
		{
			get
			{
				return this._loadingElementId ?? string.Empty;
			}
			set
			{
				this._loadingElementId = value;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0001B2C2 File Offset: 0x000194C2
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x0001B2D3 File Offset: 0x000194D3
		public string OnBegin
		{
			get
			{
				return this._onBegin ?? string.Empty;
			}
			set
			{
				this._onBegin = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001B2DC File Offset: 0x000194DC
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x0001B2ED File Offset: 0x000194ED
		public string OnComplete
		{
			get
			{
				return this._onComplete ?? string.Empty;
			}
			set
			{
				this._onComplete = value;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0001B2F6 File Offset: 0x000194F6
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0001B307 File Offset: 0x00019507
		public string OnFailure
		{
			get
			{
				return this._onFailure ?? string.Empty;
			}
			set
			{
				this._onFailure = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0001B310 File Offset: 0x00019510
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0001B321 File Offset: 0x00019521
		public string OnSuccess
		{
			get
			{
				return this._onSuccess ?? string.Empty;
			}
			set
			{
				this._onSuccess = value;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0001B32A File Offset: 0x0001952A
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0001B33B File Offset: 0x0001953B
		public string UpdateTargetId
		{
			get
			{
				return this._updateTargetId ?? string.Empty;
			}
			set
			{
				this._updateTargetId = value;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0001B344 File Offset: 0x00019544
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0001B355 File Offset: 0x00019555
		public string Url
		{
			get
			{
				return this._url ?? string.Empty;
			}
			set
			{
				this._url = value;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0001B35E File Offset: 0x0001955E
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0001B366 File Offset: 0x00019566
		public bool AllowCache { get; set; }

		// Token: 0x060009DA RID: 2522 RVA: 0x0001B370 File Offset: 0x00019570
		internal string ToJavascriptString()
		{
			StringBuilder stringBuilder = new StringBuilder("{");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " insertionMode: {0},", new object[]
			{
				this.InsertionModeString
			});
			stringBuilder.Append(AjaxOptions.PropertyStringIfSpecified("confirm", this.Confirm));
			stringBuilder.Append(AjaxOptions.PropertyStringIfSpecified("httpMethod", this.HttpMethod));
			stringBuilder.Append(AjaxOptions.PropertyStringIfSpecified("loadingElementId", this.LoadingElementId));
			stringBuilder.Append(AjaxOptions.PropertyStringIfSpecified("updateTargetId", this.UpdateTargetId));
			stringBuilder.Append(AjaxOptions.PropertyStringIfSpecified("url", this.Url));
			stringBuilder.Append(AjaxOptions.EventStringIfSpecified("onBegin", this.OnBegin));
			stringBuilder.Append(AjaxOptions.EventStringIfSpecified("onComplete", this.OnComplete));
			stringBuilder.Append(AjaxOptions.EventStringIfSpecified("onFailure", this.OnFailure));
			stringBuilder.Append(AjaxOptions.EventStringIfSpecified("onSuccess", this.OnSuccess));
			stringBuilder.Length--;
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001B49C File Offset: 0x0001969C
		public IDictionary<string, object> ToUnobtrusiveHtmlAttributes()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>
			{
				{
					"data-ajax",
					"true"
				}
			};
			AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-url", this.Url);
			AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-method", this.HttpMethod);
			AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-confirm", this.Confirm);
			AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-begin", this.OnBegin);
			AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-complete", this.OnComplete);
			AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-failure", this.OnFailure);
			AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-success", this.OnSuccess);
			if (this.AllowCache)
			{
				AjaxOptions.AddToDictionaryIfSpecified(dictionary, "data-ajax-cache", "true");
			}
			if (!string.IsNullOrWhiteSpace(this.LoadingElementId))
			{
				dictionary.Add("data-ajax-loading", AjaxOptions.EscapeIdSelector(this.LoadingElementId));
				if (this.LoadingElementDuration > 0)
				{
					dictionary.Add("data-ajax-loading-duration", this.LoadingElementDuration);
				}
			}
			if (!string.IsNullOrWhiteSpace(this.UpdateTargetId))
			{
				dictionary.Add("data-ajax-update", AjaxOptions.EscapeIdSelector(this.UpdateTargetId));
				dictionary.Add("data-ajax-mode", this.InsertionModeUnobtrusive);
			}
			return dictionary;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001B5C7 File Offset: 0x000197C7
		private static void AddToDictionaryIfSpecified(IDictionary<string, object> dictionary, string name, string value)
		{
			if (!string.IsNullOrWhiteSpace(value))
			{
				dictionary.Add(name, value);
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001B5DC File Offset: 0x000197DC
		private static string EventStringIfSpecified(string propertyName, string handler)
		{
			if (!string.IsNullOrEmpty(handler))
			{
				return string.Format(CultureInfo.InvariantCulture, " {0}: Function.createDelegate(this, {1}),", new object[]
				{
					propertyName,
					handler.ToString()
				});
			}
			return string.Empty;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001B61C File Offset: 0x0001981C
		private static string PropertyStringIfSpecified(string propertyName, string propertyValue)
		{
			if (!string.IsNullOrEmpty(propertyValue))
			{
				string text = propertyValue.Replace("'", "\\'");
				return string.Format(CultureInfo.InvariantCulture, " {0}: '{1}',", new object[]
				{
					propertyName,
					text
				});
			}
			return string.Empty;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001B667 File Offset: 0x00019867
		private static string EscapeIdSelector(string selector)
		{
			return '#' + AjaxOptions._idRegex.Replace(selector, "\\$&");
		}

		// Token: 0x040002A2 RID: 674
		private static readonly Regex _idRegex = new Regex("[.:[\\]]");

		// Token: 0x040002A3 RID: 675
		private string _confirm;

		// Token: 0x040002A4 RID: 676
		private string _httpMethod;

		// Token: 0x040002A5 RID: 677
		private InsertionMode _insertionMode;

		// Token: 0x040002A6 RID: 678
		private string _loadingElementId;

		// Token: 0x040002A7 RID: 679
		private string _onBegin;

		// Token: 0x040002A8 RID: 680
		private string _onComplete;

		// Token: 0x040002A9 RID: 681
		private string _onFailure;

		// Token: 0x040002AA RID: 682
		private string _onSuccess;

		// Token: 0x040002AB RID: 683
		private string _updateTargetId;

		// Token: 0x040002AC RID: 684
		private string _url;
	}
}
