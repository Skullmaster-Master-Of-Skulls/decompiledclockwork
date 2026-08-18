using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc.Properties;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace System.Web.Mvc.Html
{
	// Token: 0x02000160 RID: 352
	internal static class TemplateHelpers
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00019A60 File Offset: 0x00017C60
		internal static string ExecuteTemplate(HtmlHelper html, ViewDataDictionary viewData, string templateName, DataBoundControlMode mode, TemplateHelpers.GetViewNamesDelegate getViewNames, TemplateHelpers.GetDefaultActionsDelegate getDefaultActions)
		{
			Dictionary<string, TemplateHelpers.ActionCacheItem> actionCache = TemplateHelpers.GetActionCache(html);
			Dictionary<string, Func<HtmlHelper, string>> dictionary = getDefaultActions(mode);
			string str = TemplateHelpers._modeViewPaths[mode];
			foreach (string text in getViewNames(viewData.ModelMetadata, new string[]
			{
				templateName,
				viewData.ModelMetadata.TemplateHint,
				viewData.ModelMetadata.DataTypeName
			}))
			{
				string text2 = str + "/" + text;
				TemplateHelpers.ActionCacheItem actionCacheItem;
				if (actionCache.TryGetValue(text2, out actionCacheItem))
				{
					if (actionCacheItem != null)
					{
						return actionCacheItem.Execute(html, viewData);
					}
				}
				else
				{
					ViewEngineResult viewEngineResult = ViewEngines.Engines.FindPartialView(html.ViewContext, text2);
					if (viewEngineResult.View != null)
					{
						actionCache[text2] = new TemplateHelpers.ActionCacheViewItem
						{
							ViewName = text2
						};
						using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
						{
							viewEngineResult.View.Render(new ViewContext(html.ViewContext, viewEngineResult.View, viewData, html.ViewContext.TempData, stringWriter), stringWriter);
							return stringWriter.ToString();
						}
					}
					Func<HtmlHelper, string> func;
					if (dictionary.TryGetValue(text, out func))
					{
						actionCache[text2] = new TemplateHelpers.ActionCacheCodeItem
						{
							Action = func
						};
						return func(TemplateHelpers.MakeHtmlHelper(html, viewData));
					}
					actionCache[text2] = null;
				}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.TemplateHelpers_NoTemplate, new object[]
			{
				viewData.ModelMetadata.RealModelType.FullName
			}));
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00019C58 File Offset: 0x00017E58
		internal static Dictionary<string, TemplateHelpers.ActionCacheItem> GetActionCache(HtmlHelper html)
		{
			HttpContextBase httpContext = html.ViewContext.HttpContext;
			Dictionary<string, TemplateHelpers.ActionCacheItem> dictionary;
			if (!httpContext.Items.Contains(TemplateHelpers.CacheItemId))
			{
				dictionary = new Dictionary<string, TemplateHelpers.ActionCacheItem>();
				httpContext.Items[TemplateHelpers.CacheItemId] = dictionary;
			}
			else
			{
				dictionary = (Dictionary<string, TemplateHelpers.ActionCacheItem>)httpContext.Items[TemplateHelpers.CacheItemId];
			}
			return dictionary;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00019CB3 File Offset: 0x00017EB3
		internal static Dictionary<string, Func<HtmlHelper, string>> GetDefaultActions(DataBoundControlMode mode)
		{
			if (mode != DataBoundControlMode.ReadOnly)
			{
				return TemplateHelpers._defaultEditorActions;
			}
			return TemplateHelpers._defaultDisplayActions;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001A0BC File Offset: 0x000182BC
		internal static IEnumerable<string> GetViewNames(ModelMetadata metadata, params string[] templateHints)
		{
			foreach (string templateHint in from s in templateHints
			where !string.IsNullOrEmpty(s)
			select s)
			{
				yield return templateHint;
			}
			Type fieldType = Nullable.GetUnderlyingType(metadata.RealModelType) ?? metadata.RealModelType;
			yield return fieldType.Name;
			if (!(fieldType == typeof(string)))
			{
				if (!metadata.IsComplexType)
				{
					if (fieldType.IsEnum)
					{
						yield return "Enum";
					}
					else if (fieldType == typeof(DateTimeOffset))
					{
						yield return "DateTime";
					}
					yield return "String";
				}
				else if (fieldType.IsInterface)
				{
					if (typeof(IEnumerable).IsAssignableFrom(fieldType))
					{
						yield return "Collection";
					}
					yield return "Object";
				}
				else
				{
					bool isEnumerable = typeof(IEnumerable).IsAssignableFrom(fieldType);
					for (;;)
					{
						fieldType = fieldType.BaseType;
						if (fieldType == null)
						{
							break;
						}
						if (isEnumerable && fieldType == typeof(object))
						{
							yield return "Collection";
						}
						yield return fieldType.Name;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001A0E0 File Offset: 0x000182E0
		internal static MvcHtmlString Template(HtmlHelper html, string expression, string templateName, string htmlFieldName, DataBoundControlMode mode, object additionalViewData)
		{
			return MvcHtmlString.Create(TemplateHelpers.Template(html, expression, templateName, htmlFieldName, mode, additionalViewData, new TemplateHelpers.TemplateHelperDelegate(TemplateHelpers.TemplateHelper)));
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0001A100 File Offset: 0x00018300
		internal static string Template(HtmlHelper html, string expression, string templateName, string htmlFieldName, DataBoundControlMode mode, object additionalViewData, TemplateHelpers.TemplateHelperDelegate templateHelper)
		{
			return templateHelper(html, ModelMetadata.FromStringExpression(expression, html.ViewData), htmlFieldName ?? ExpressionHelper.GetExpressionText(expression), templateName, mode, additionalViewData);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001A126 File Offset: 0x00018326
		internal static MvcHtmlString TemplateFor<TContainer, TValue>(this HtmlHelper<TContainer> html, Expression<Func<TContainer, TValue>> expression, string templateName, string htmlFieldName, DataBoundControlMode mode, object additionalViewData)
		{
			return MvcHtmlString.Create(html.TemplateFor(expression, templateName, htmlFieldName, mode, additionalViewData, new TemplateHelpers.TemplateHelperDelegate(TemplateHelpers.TemplateHelper)));
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001A146 File Offset: 0x00018346
		internal static string TemplateFor<TContainer, TValue>(this HtmlHelper<TContainer> html, Expression<Func<TContainer, TValue>> expression, string templateName, string htmlFieldName, DataBoundControlMode mode, object additionalViewData, TemplateHelpers.TemplateHelperDelegate templateHelper)
		{
			return templateHelper(html, ModelMetadata.FromLambdaExpression<TContainer, TValue>(expression, html.ViewData), htmlFieldName ?? ExpressionHelper.GetExpressionText(expression), templateName, mode, additionalViewData);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001A16C File Offset: 0x0001836C
		internal static string TemplateHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode mode, object additionalViewData)
		{
			return TemplateHelpers.TemplateHelper(html, metadata, htmlFieldName, templateName, mode, additionalViewData, new TemplateHelpers.ExecuteTemplateDelegate(TemplateHelpers.ExecuteTemplate));
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001A188 File Offset: 0x00018388
		internal static string TemplateHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode mode, object additionalViewData, TemplateHelpers.ExecuteTemplateDelegate executeTemplate)
		{
			if (metadata.ConvertEmptyStringToNull && string.Empty.Equals(metadata.Model))
			{
				metadata.Model = null;
			}
			object formattedModelValue = metadata.Model;
			if (metadata.Model == null && mode == DataBoundControlMode.ReadOnly)
			{
				formattedModelValue = metadata.NullDisplayText;
			}
			string text = (mode == DataBoundControlMode.ReadOnly) ? metadata.DisplayFormatString : metadata.EditFormatString;
			if (metadata.Model != null && !string.IsNullOrEmpty(text))
			{
				formattedModelValue = string.Format(CultureInfo.CurrentCulture, text, new object[]
				{
					metadata.Model
				});
			}
			object item = metadata.Model ?? metadata.RealModelType;
			if (html.ViewDataContainer.ViewData.TemplateInfo.VisitedObjects.Contains(item))
			{
				return string.Empty;
			}
			ViewDataDictionary viewDataDictionary = new ViewDataDictionary(html.ViewDataContainer.ViewData)
			{
				Model = metadata.Model,
				ModelMetadata = metadata,
				TemplateInfo = new TemplateInfo
				{
					FormattedModelValue = formattedModelValue,
					HtmlFieldPrefix = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName),
					VisitedObjects = new HashSet<object>(html.ViewContext.ViewData.TemplateInfo.VisitedObjects)
				}
			};
			if (additionalViewData != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in TypeHelper.ObjectToDictionary(additionalViewData))
				{
					viewDataDictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			viewDataDictionary.TemplateInfo.VisitedObjects.Add(item);
			return executeTemplate(html, viewDataDictionary, templateName, mode, new TemplateHelpers.GetViewNamesDelegate(TemplateHelpers.GetViewNames), new TemplateHelpers.GetDefaultActionsDelegate(TemplateHelpers.GetDefaultActions));
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001A358 File Offset: 0x00018558
		private static HtmlHelper MakeHtmlHelper(HtmlHelper html, ViewDataDictionary viewData)
		{
			return new HtmlHelper(new ViewContext(html.ViewContext, html.ViewContext.View, viewData, html.ViewContext.TempData, html.ViewContext.Writer), new TemplateHelpers.ViewDataContainer(viewData))
			{
				Html5DateRenderingMode = html.Html5DateRenderingMode
			};
		}

		// Token: 0x0400027F RID: 639
		private static readonly Dictionary<DataBoundControlMode, string> _modeViewPaths = new Dictionary<DataBoundControlMode, string>
		{
			{
				DataBoundControlMode.ReadOnly,
				"DisplayTemplates"
			},
			{
				DataBoundControlMode.Edit,
				"EditorTemplates"
			}
		};

		// Token: 0x04000280 RID: 640
		private static readonly Dictionary<string, Func<HtmlHelper, string>> _defaultDisplayActions = new Dictionary<string, Func<HtmlHelper, string>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.EmailAddressTemplate)
			},
			{
				"HiddenInput",
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.HiddenInputTemplate)
			},
			{
				"Html",
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.HtmlTemplate)
			},
			{
				"Text",
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.StringTemplate)
			},
			{
				"Url",
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.UrlTemplate)
			},
			{
				"Collection",
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.CollectionTemplate)
			},
			{
				typeof(bool).Name,
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.BooleanTemplate)
			},
			{
				typeof(decimal).Name,
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.DecimalTemplate)
			},
			{
				typeof(string).Name,
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.StringTemplate)
			},
			{
				typeof(object).Name,
				new Func<HtmlHelper, string>(DefaultDisplayTemplates.ObjectTemplate)
			}
		};

		// Token: 0x04000281 RID: 641
		private static readonly Dictionary<string, Func<HtmlHelper, string>> _defaultEditorActions = new Dictionary<string, Func<HtmlHelper, string>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"HiddenInput",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.HiddenInputTemplate)
			},
			{
				"MultilineText",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.MultilineTextTemplate)
			},
			{
				"Password",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.PasswordTemplate)
			},
			{
				"Text",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.StringTemplate)
			},
			{
				"Collection",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.CollectionTemplate)
			},
			{
				"PhoneNumber",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.PhoneNumberInputTemplate)
			},
			{
				"Url",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.UrlInputTemplate)
			},
			{
				"EmailAddress",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.EmailAddressInputTemplate)
			},
			{
				"DateTime",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.DateTimeInputTemplate)
			},
			{
				"DateTime-local",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.DateTimeLocalInputTemplate)
			},
			{
				"Date",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.DateInputTemplate)
			},
			{
				"Time",
				new Func<HtmlHelper, string>(DefaultEditorTemplates.TimeInputTemplate)
			},
			{
				typeof(Color).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.ColorInputTemplate)
			},
			{
				typeof(byte).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.NumberInputTemplate)
			},
			{
				typeof(sbyte).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.NumberInputTemplate)
			},
			{
				typeof(int).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.NumberInputTemplate)
			},
			{
				typeof(uint).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.NumberInputTemplate)
			},
			{
				typeof(long).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.NumberInputTemplate)
			},
			{
				typeof(ulong).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.NumberInputTemplate)
			},
			{
				typeof(bool).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.BooleanTemplate)
			},
			{
				typeof(decimal).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.DecimalTemplate)
			},
			{
				typeof(string).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.StringTemplate)
			},
			{
				typeof(object).Name,
				new Func<HtmlHelper, string>(DefaultEditorTemplates.ObjectTemplate)
			}
		};

		// Token: 0x04000282 RID: 642
		internal static string CacheItemId = Guid.NewGuid().ToString();

		// Token: 0x02000161 RID: 353
		// (Invoke) Token: 0x06000951 RID: 2385
		internal delegate string ExecuteTemplateDelegate(HtmlHelper html, ViewDataDictionary viewData, string templateName, DataBoundControlMode mode, TemplateHelpers.GetViewNamesDelegate getViewNames, TemplateHelpers.GetDefaultActionsDelegate getDefaultActions);

		// Token: 0x02000162 RID: 354
		// (Invoke) Token: 0x06000955 RID: 2389
		internal delegate Dictionary<string, Func<HtmlHelper, string>> GetDefaultActionsDelegate(DataBoundControlMode mode);

		// Token: 0x02000163 RID: 355
		// (Invoke) Token: 0x06000959 RID: 2393
		internal delegate IEnumerable<string> GetViewNamesDelegate(ModelMetadata metadata, params string[] templateHints);

		// Token: 0x02000164 RID: 356
		// (Invoke) Token: 0x0600095D RID: 2397
		internal delegate string TemplateHelperDelegate(HtmlHelper html, ModelMetadata metadata, string htmlFieldName, string templateName, DataBoundControlMode mode, object additionalViewData);

		// Token: 0x02000165 RID: 357
		internal abstract class ActionCacheItem
		{
			// Token: 0x06000960 RID: 2400
			public abstract string Execute(HtmlHelper html, ViewDataDictionary viewData);
		}

		// Token: 0x02000166 RID: 358
		internal class ActionCacheCodeItem : TemplateHelpers.ActionCacheItem
		{
			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06000962 RID: 2402 RVA: 0x0001A7AC File Offset: 0x000189AC
			// (set) Token: 0x06000963 RID: 2403 RVA: 0x0001A7B4 File Offset: 0x000189B4
			public Func<HtmlHelper, string> Action { get; set; }

			// Token: 0x06000964 RID: 2404 RVA: 0x0001A7BD File Offset: 0x000189BD
			public override string Execute(HtmlHelper html, ViewDataDictionary viewData)
			{
				return this.Action(TemplateHelpers.MakeHtmlHelper(html, viewData));
			}
		}

		// Token: 0x02000167 RID: 359
		internal class ActionCacheViewItem : TemplateHelpers.ActionCacheItem
		{
			// Token: 0x17000229 RID: 553
			// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001A7D9 File Offset: 0x000189D9
			// (set) Token: 0x06000967 RID: 2407 RVA: 0x0001A7E1 File Offset: 0x000189E1
			public string ViewName { get; set; }

			// Token: 0x06000968 RID: 2408 RVA: 0x0001A7EC File Offset: 0x000189EC
			public override string Execute(HtmlHelper html, ViewDataDictionary viewData)
			{
				ViewEngineResult viewEngineResult = ViewEngines.Engines.FindPartialView(html.ViewContext, this.ViewName);
				string result;
				using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
				{
					viewEngineResult.View.Render(new ViewContext(html.ViewContext, viewEngineResult.View, viewData, html.ViewContext.TempData, stringWriter), stringWriter);
					result = stringWriter.ToString();
				}
				return result;
			}
		}

		// Token: 0x02000168 RID: 360
		private class ViewDataContainer : IViewDataContainer
		{
			// Token: 0x0600096A RID: 2410 RVA: 0x0001A874 File Offset: 0x00018A74
			public ViewDataContainer(ViewDataDictionary viewData)
			{
				this.ViewData = viewData;
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001A883 File Offset: 0x00018A83
			// (set) Token: 0x0600096C RID: 2412 RVA: 0x0001A88B File Offset: 0x00018A8B
			public ViewDataDictionary ViewData { get; set; }
		}
	}
}
