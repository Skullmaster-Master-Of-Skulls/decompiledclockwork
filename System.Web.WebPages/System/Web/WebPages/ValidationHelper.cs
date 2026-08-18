using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using System.Web.WebPages.Scope;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages
{
	// Token: 0x0200005F RID: 95
	public sealed class ValidationHelper
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00008E72 File Offset: 0x00007072
		internal ValidationHelper(HttpContextBase httpContext, ModelStateDictionary modelStateDictionary)
		{
			this._httpContext = httpContext;
			this._modelStateDictionary = modelStateDictionary;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008E98 File Offset: 0x00007098
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00008EC0 File Offset: 0x000070C0
		public static string ValidCssClass
		{
			get
			{
				object obj;
				if (!ValidationHelper.Scope.TryGetValue(ValidationHelper._validCssClassKey, out obj))
				{
					return null;
				}
				return obj as string;
			}
			set
			{
				ValidationHelper.Scope[ValidationHelper._validCssClassKey] = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008ED4 File Offset: 0x000070D4
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00008F00 File Offset: 0x00007100
		public static string InvalidCssClass
		{
			get
			{
				object obj;
				if (!ValidationHelper.Scope.TryGetValue(ValidationHelper._invalidCssClassKey, out obj))
				{
					return "input-validation-error";
				}
				return obj as string;
			}
			set
			{
				ValidationHelper.Scope[ValidationHelper._invalidCssClassKey] = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008F12 File Offset: 0x00007112
		public string FormField
		{
			get
			{
				return "_FORM";
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008F19 File Offset: 0x00007119
		internal static IDictionary<object, object> Scope
		{
			get
			{
				return ValidationHelper._scopeOverride ?? ScopeStorage.CurrentScope;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008F29 File Offset: 0x00007129
		public void RequireField(string field)
		{
			this.RequireField(field, null);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008F34 File Offset: 0x00007134
		public void RequireField(string field, string errorMessage)
		{
			if (string.IsNullOrEmpty(field))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "field");
			}
			this.Add(field, new IValidator[]
			{
				Validator.Required(errorMessage)
			});
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008F74 File Offset: 0x00007174
		public void RequireFields(params string[] fields)
		{
			if (fields == null)
			{
				throw new ArgumentNullException("fields");
			}
			foreach (string field in fields)
			{
				this.RequireField(field);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008FB0 File Offset: 0x000071B0
		public void Add(string field, params IValidator[] validators)
		{
			if (string.IsNullOrEmpty(field))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "field");
			}
			if (validators != null)
			{
				if (!validators.Any((IValidator v) => v == null))
				{
					this.AddFieldValidators(field, validators);
					return;
				}
			}
			throw new ArgumentNullException("validators");
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009010 File Offset: 0x00007210
		public void Add(IEnumerable<string> fields, params IValidator[] validators)
		{
			if (fields == null)
			{
				throw new ArgumentNullException("fields");
			}
			if (validators == null)
			{
				throw new ArgumentNullException("validators");
			}
			foreach (string field in fields)
			{
				this.Add(field, validators);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009078 File Offset: 0x00007278
		public void AddFormError(string errorMessage)
		{
			this._modelStateDictionary.AddFormError(errorMessage);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009086 File Offset: 0x00007286
		public bool IsValid(params string[] fields)
		{
			return !this.Validate(fields).Any<ValidationResult>();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009098 File Offset: 0x00007298
		public IEnumerable<ValidationResult> Validate(params string[] fields)
		{
			IEnumerable<string> fields2 = fields;
			if (fields == null || !fields.Any<string>())
			{
				fields2 = this._validators.Keys.Concat(new string[]
				{
					this.FormField
				});
			}
			return this.ValidateFieldsAndUpdateModelState(fields2);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000090E3 File Offset: 0x000072E3
		public IEnumerable<string> GetErrors(params string[] fields)
		{
			return from r in this.Validate(fields)
			select r.ErrorMessage;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00009110 File Offset: 0x00007310
		public HtmlString For(string field)
		{
			if (string.IsNullOrEmpty(field))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "field");
			}
			IEnumerable<ModelClientValidationRule> clientValidationRules = this.GetClientValidationRules(field);
			return ValidationHelper.GenerateHtmlFromClientValidationRules(clientValidationRules);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009144 File Offset: 0x00007344
		public HtmlString ClassFor(string field)
		{
			if (this._httpContext == null || !string.Equals("POST", this._httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			string text = this.IsValid(new string[]
			{
				field
			}) ? ValidationHelper.ValidCssClass : ValidationHelper.InvalidCssClass;
			if (text != null)
			{
				return new HtmlString(text);
			}
			return null;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000091AC File Offset: 0x000073AC
		internal static IDisposable OverrideScope()
		{
			ValidationHelper._scopeOverride = new Dictionary<object, object>();
			return new DisposableAction(delegate()
			{
				ValidationHelper._scopeOverride = null;
			});
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000091DC File Offset: 0x000073DC
		internal IDictionary<string, object> GetUnobtrusiveValidationAttributes(string field)
		{
			IEnumerable<ModelClientValidationRule> clientValidationRules = this.GetClientValidationRules(field);
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientValidationRules, dictionary);
			return dictionary;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009238 File Offset: 0x00007438
		private IEnumerable<ValidationResult> ValidateFieldsAndUpdateModelState(IEnumerable<string> fields)
		{
			ValidationContext context = new ValidationContext(this._httpContext, null, null);
			List<ValidationResult> list = new List<ValidationResult>();
			using (IEnumerator<string> enumerator = fields.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string field = enumerator.Current;
					IEnumerable<ValidationResult> enumerable = this.ValidateField(field, context);
					IEnumerable<string> enumerable2 = from c in enumerable
					select c.ErrorMessage;
					ModelState modelState = this._modelStateDictionary[field];
					if (modelState != null && modelState.Errors.Any<string>())
					{
						enumerable2 = enumerable2.Except(modelState.Errors, StringComparer.OrdinalIgnoreCase);
						enumerable = enumerable.Concat(from e in modelState.Errors
						select new ValidationResult(e, new string[]
						{
							field
						}));
					}
					foreach (string errorMessage in enumerable2)
					{
						this._modelStateDictionary.AddError(field, errorMessage);
					}
					list.AddRange(enumerable);
				}
			}
			return list;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000093A8 File Offset: 0x000075A8
		private void AddFieldValidators(string field, params IValidator[] validators)
		{
			List<IValidator> list = null;
			if (!this._validators.TryGetValue(field, out list))
			{
				list = new List<IValidator>();
				this._validators[field] = list;
			}
			foreach (IValidator item in validators)
			{
				list.Add(item);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009418 File Offset: 0x00007618
		private IEnumerable<ValidationResult> ValidateField(string field, ValidationContext context)
		{
			List<IValidator> source;
			if (!this._validators.TryGetValue(field, out source))
			{
				return Enumerable.Empty<ValidationResult>();
			}
			context.MemberName = field;
			return from f in source
			select f.Validate(context) into result
			where result != ValidationResult.Success
			select result;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000095C8 File Offset: 0x000077C8
		private IEnumerable<ModelClientValidationRule> GetClientValidationRules(string field)
		{
			List<IValidator> source = null;
			if (!this._validators.TryGetValue(field, out source))
			{
				return Enumerable.Empty<ModelClientValidationRule>();
			}
			return from item in source
			let clientRule = item.ClientValidationRule
			where clientRule != null
			select clientRule;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009654 File Offset: 0x00007854
		internal static HtmlString GenerateHtmlFromClientValidationRules(IEnumerable<ModelClientValidationRule> clientRules)
		{
			if (!clientRules.Any<ModelClientValidationRule>())
			{
				return null;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientRules, dictionary);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				string value = HttpUtility.HtmlEncode(Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture));
				stringBuilder.Append(key).Append("=\"").Append(value).Append('"').Append(' ');
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length--;
			}
			return new HtmlString(stringBuilder.ToString());
		}

		// Token: 0x040000C1 RID: 193
		private static readonly object _invalidCssClassKey = new object();

		// Token: 0x040000C2 RID: 194
		private static readonly object _validCssClassKey = new object();

		// Token: 0x040000C3 RID: 195
		private static IDictionary<object, object> _scopeOverride;

		// Token: 0x040000C4 RID: 196
		private readonly Dictionary<string, List<IValidator>> _validators = new Dictionary<string, List<IValidator>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000C5 RID: 197
		private readonly HttpContextBase _httpContext;

		// Token: 0x040000C6 RID: 198
		private readonly ModelStateDictionary _modelStateDictionary;
	}
}
