using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using System.Web.WebPages.Html;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Web.Helpers
{
	// Token: 0x02000021 RID: 33
	[GeneratedCode("RazorSingleFileGenerator", "1.0.0.0")]
	internal class WebGridRenderer : HelperPage
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00007890 File Offset: 0x00005A90
		public static HelperResult GridInitScript(WebGrid webGrid, HttpContextBase httpContext)
		{
			return new HelperResult(delegate(TextWriter __razor_helper_writer)
			{
				if (!webGrid.IsAjaxEnabled)
				{
					return;
				}
				if (!WebGridRenderer.IsGridScriptRendered(httpContext))
				{
					WebGridRenderer.SetGridScriptRendered(httpContext, true);
					HelperPage.WriteLiteralTo(__razor_helper_writer, "        <script type=\"text/javascript\">\r\n        (function($) {\r\n            $.fn.swhgLoad = function(url, containerId, callback) {\r\n                url = url + (url.indexOf('?') == -1 ? '?' : '&') + '__swhg=' + new Date().getTime();\r\n\r\n                $('<div/>').load(url + ' ' + containerId, function(data, status, xhr) {\r\n                    $(containerId).replaceWith($(this).html());\r\n                    if (typeof(callback) === 'function') {\r\n                        callback.apply(this, arguments);\r\n                    }\r\n                });\r\n                return this;\r\n            }\r\n\r\n            $(function() {\r\n                $('table[data-swhgajax=\"true\"],span[data-swhgajax=\"true\"]').each(function() {\r\n                    var self = $(this);\r\n                    var containerId = '#' + self.data('swhgcontainer');\r\n                    var callback = getFunction(self.data('swhgcallback'));\r\n\r\n                    $(containerId).parent().delegate(containerId + ' a[data-swhglnk=\"true\"]', 'click', function() {\r\n                        $(containerId).swhgLoad($(this).attr('href'), containerId, callback);\r\n                        return false;\r\n                    });\r\n                })\r\n            });\r\n\r\n            function getFunction(code, argNames) {\r\n                argNames = argNames || [];\r\n                var fn = window, parts = (code || \"\").split(\".\");\r\n                while (fn && parts.length) {\r\n                    fn = fn[parts.shift()];\r\n                }\r\n                if (typeof (fn) === \"function\") {\r\n                    return fn;\r\n                }\r\n                argNames.push(code);\r\n                return Function.constructor.apply(null, argNames);\r\n            }\r\n        })(jQuery);\r\n        </script>\r\n");
				}
			});
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public static HelperResult Table(WebGrid webGrid, HttpContextBase httpContext, string tableStyle, string headerStyle, string footerStyle, string rowStyle, string alternatingRowStyle, string selectedRowStyle, string caption, bool displayHeader, bool fillEmptyRows, string emptyRowCellValue, IEnumerable<WebGridColumn> columns, IEnumerable<string> exclusions, Func<dynamic, object> footer, object htmlAttributes)
		{
			return new HelperResult(delegate(TextWriter __razor_helper_writer)
			{
				if (emptyRowCellValue == null)
				{
					emptyRowCellValue = "&nbsp;";
				}
				HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridInitScript(webGrid, httpContext));
				RouteValueDictionary routeValueDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
				if (webGrid.IsAjaxEnabled)
				{
					routeValueDictionary["data-swhgajax"] = "true";
					routeValueDictionary["data-swhgcontainer"] = webGrid.AjaxUpdateContainerId;
					routeValueDictionary["data-swhgcallback"] = webGrid.AjaxUpdateCallback;
				}
				HelperPage.WriteLiteralTo(__razor_helper_writer, "    <table");
				HelperPage.WriteTo(__razor_helper_writer, tableStyle.IsEmpty() ? null : WebGridRenderer.Raw(" class=\"" + HttpUtility.HtmlAttributeEncode(tableStyle) + "\""));
				HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.PrintAttributes(routeValueDictionary));
				HelperPage.WriteLiteralTo(__razor_helper_writer, ">\r\n");
				if (!caption.IsEmpty())
				{
					HelperPage.WriteLiteralTo(__razor_helper_writer, "        <caption>");
					HelperPage.WriteTo(__razor_helper_writer, caption);
					HelperPage.WriteLiteralTo(__razor_helper_writer, "</caption>\r\n");
				}
				if (displayHeader)
				{
					HelperPage.WriteLiteralTo(__razor_helper_writer, "    <thead>\r\n        <tr");
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.CssClass(headerStyle));
					HelperPage.WriteLiteralTo(__razor_helper_writer, ">\r\n");
					foreach (WebGridColumn webGridColumn in columns)
					{
						HelperPage.WriteLiteralTo(__razor_helper_writer, "            <th scope=\"col\">\r\n");
						if (WebGridRenderer.ShowSortableColumnHeader(webGrid, webGridColumn))
						{
							string text = webGridColumn.Header.IsEmpty() ? webGridColumn.ColumnName : webGridColumn.Header;
							HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridLink(webGrid, webGrid.GetSortUrl(webGridColumn.ColumnName), text));
						}
						else
						{
							HelperPage.WriteTo(__razor_helper_writer, webGridColumn.Header ?? webGridColumn.ColumnName);
						}
						HelperPage.WriteLiteralTo(__razor_helper_writer, "            </th>\r\n");
					}
					HelperPage.WriteLiteralTo(__razor_helper_writer, "        </tr>\r\n    </thead>\r\n");
				}
				if (footer != null)
				{
					HelperPage.WriteLiteralTo(__razor_helper_writer, "    <tfoot>\r\n        <tr ");
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.CssClass(footerStyle));
					HelperPage.WriteLiteralTo(__razor_helper_writer, ">\r\n            <td colspan=\"");
					HelperPage.WriteTo(__razor_helper_writer, columns.Count<WebGridColumn>());
					HelperPage.WriteLiteralTo(__razor_helper_writer, "\">");
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.Format(footer, null));
					HelperPage.WriteLiteralTo(__razor_helper_writer, "</td>\r\n        </tr>\r\n    </tfoot>\r\n");
				}
				HelperPage.WriteLiteralTo(__razor_helper_writer, "    <tbody>\r\n");
				int i = 0;
				foreach (WebGridRow webGridRow in webGrid.Rows)
				{
					string rowStyle2 = WebGridRenderer.GetRowStyle(webGrid, i++, rowStyle, alternatingRowStyle, selectedRowStyle);
					HelperPage.WriteLiteralTo(__razor_helper_writer, "        <tr");
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.CssClass(rowStyle2));
					HelperPage.WriteLiteralTo(__razor_helper_writer, ">\r\n");
					foreach (WebGridColumn webGridColumn2 in columns)
					{
						string text2 = (webGridColumn2.Format == null) ? HttpUtility.HtmlEncode(webGridRow[webGridColumn2.ColumnName]) : WebGridRenderer.Format(webGridColumn2.Format, webGridRow).ToString();
						HelperPage.WriteLiteralTo(__razor_helper_writer, "            <td");
						HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.CssClass(webGridColumn2.Style));
						HelperPage.WriteLiteralTo(__razor_helper_writer, ">");
						HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.Raw(text2));
						HelperPage.WriteLiteralTo(__razor_helper_writer, "</td>\r\n");
					}
					HelperPage.WriteLiteralTo(__razor_helper_writer, "        </tr>\r\n");
				}
				if (fillEmptyRows)
				{
					i = webGrid.Rows.Count;
					while (i < webGrid.RowsPerPage)
					{
						string rowStyle3 = WebGridRenderer.GetRowStyle(webGrid, i++, rowStyle, alternatingRowStyle, null);
						HelperPage.WriteLiteralTo(__razor_helper_writer, "            <tr");
						HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.CssClass(rowStyle3));
						HelperPage.WriteLiteralTo(__razor_helper_writer, ">\r\n");
						foreach (WebGridColumn webGridColumn3 in columns)
						{
							HelperPage.WriteLiteralTo(__razor_helper_writer, "                    <td");
							HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.CssClass(webGridColumn3.Style));
							HelperPage.WriteLiteralTo(__razor_helper_writer, ">");
							HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.Raw(emptyRowCellValue));
							HelperPage.WriteLiteralTo(__razor_helper_writer, "</td>\r\n");
						}
						HelperPage.WriteLiteralTo(__razor_helper_writer, "            </tr>\r\n");
					}
				}
				HelperPage.WriteLiteralTo(__razor_helper_writer, "    </tbody>\r\n    </table>\r\n");
			});
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008168 File Offset: 0x00006368
		public static HelperResult Pager(WebGrid webGrid, HttpContextBase httpContext, WebGridPagerModes mode, string firstText, string previousText, string nextText, string lastText, int numericLinksCount, bool renderAjaxContainer)
		{
			return new HelperResult(delegate(TextWriter __razor_helper_writer)
			{
				int pageIndex = webGrid.PageIndex;
				int pageCount = webGrid.PageCount;
				int num = pageCount - 1;
				HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridInitScript(webGrid, httpContext));
				if (renderAjaxContainer && webGrid.IsAjaxEnabled)
				{
					HelperPage.WriteLiteralTo(__razor_helper_writer, "        ");
					HelperPage.WriteLiteralTo(__razor_helper_writer, "<span data-swhgajax=\"true\" data-swhgcontainer=\"");
					HelperPage.WriteTo(__razor_helper_writer, webGrid.AjaxUpdateContainerId);
					HelperPage.WriteLiteralTo(__razor_helper_writer, "\" data-swhgcallback=\"");
					HelperPage.WriteTo(__razor_helper_writer, webGrid.AjaxUpdateCallback);
					HelperPage.WriteLiteralTo(__razor_helper_writer, "\">\r\n");
				}
				if (WebGridRenderer.ModeEnabled(mode, WebGridPagerModes.FirstLast) && pageIndex > 1)
				{
					if (string.IsNullOrEmpty(firstText))
					{
						firstText = "<<";
					}
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridLink(webGrid, webGrid.GetPageUrl(0), firstText));
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.Raw(" "));
				}
				if (WebGridRenderer.ModeEnabled(mode, WebGridPagerModes.NextPrevious) && pageIndex > 0)
				{
					if (string.IsNullOrEmpty(previousText))
					{
						previousText = "<";
					}
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridLink(webGrid, webGrid.GetPageUrl(pageIndex - 1), previousText));
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.Raw(" "));
				}
				if (WebGridRenderer.ModeEnabled(mode, WebGridPagerModes.Numeric) && pageCount > 1)
				{
					int num2 = pageIndex + numericLinksCount / 2;
					int num3 = num2 - numericLinksCount + 1;
					if (num2 > num)
					{
						num3 -= num2 - num;
						num2 = num;
					}
					if (num3 < 0)
					{
						num2 = Math.Min(num2 + -num3, num);
						num3 = 0;
					}
					for (int i = num3; i <= num2; i++)
					{
						string text = (i + 1).ToString(CultureInfo.InvariantCulture);
						if (i == pageIndex)
						{
							HelperPage.WriteTo(__razor_helper_writer, text);
						}
						else
						{
							HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridLink(webGrid, webGrid.GetPageUrl(i), text));
						}
						HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.Raw(" "));
					}
				}
				if (WebGridRenderer.ModeEnabled(mode, WebGridPagerModes.NextPrevious) && pageIndex < num)
				{
					if (string.IsNullOrEmpty(nextText))
					{
						nextText = ">";
					}
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridLink(webGrid, webGrid.GetPageUrl(pageIndex + 1), nextText));
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.Raw(" "));
				}
				if (WebGridRenderer.ModeEnabled(mode, WebGridPagerModes.FirstLast) && pageIndex < num - 1)
				{
					if (string.IsNullOrEmpty(lastText))
					{
						lastText = ">>";
					}
					HelperPage.WriteTo(__razor_helper_writer, WebGridRenderer.GridLink(webGrid, webGrid.GetPageUrl(num), lastText));
				}
				if (renderAjaxContainer && webGrid.IsAjaxEnabled)
				{
					HelperPage.WriteLiteralTo(__razor_helper_writer, "        ");
					HelperPage.WriteLiteralTo(__razor_helper_writer, "</span>\r\n");
				}
			});
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000081D0 File Offset: 0x000063D0
		private static bool IsGridScriptRendered(HttpContextBase context)
		{
			bool? flag = (bool?)context.Items[WebGridRenderer._gridScriptRenderedKey];
			return flag != null && flag.Value;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008205 File Offset: 0x00006405
		private static void SetGridScriptRendered(HttpContextBase context, bool value)
		{
			context.Items[WebGridRenderer._gridScriptRenderedKey] = value;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000821D File Offset: 0x0000641D
		private static bool ShowSortableColumnHeader(WebGrid grid, WebGridColumn column)
		{
			return grid.CanSort && column.CanSort && !column.ColumnName.IsEmpty();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008240 File Offset: 0x00006440
		public static IHtmlString GridLink(WebGrid webGrid, string url, string text)
		{
			TagBuilder tagBuilder = new TagBuilder("a");
			tagBuilder.SetInnerText(text);
			tagBuilder.MergeAttribute("href", url);
			if (webGrid.IsAjaxEnabled)
			{
				tagBuilder.MergeAttribute("data-swhglnk", "true");
			}
			return tagBuilder.ToHtmlString(TagRenderMode.Normal);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000828A File Offset: 0x0000648A
		private static IHtmlString Raw(string text)
		{
			return new HtmlString(text);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008292 File Offset: 0x00006492
		private static IHtmlString RawJS(string text)
		{
			return new HtmlString(HttpUtility.JavaScriptStringEncode(text));
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000829F File Offset: 0x0000649F
		private static IHtmlString CssClass(string className)
		{
			return new HtmlString((!className.IsEmpty()) ? (" class=\"" + HttpUtility.HtmlAttributeEncode(className) + "\"") : string.Empty);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000082CC File Offset: 0x000064CC
		private static string GetRowStyle(WebGrid webGrid, int rowIndex, string rowStyle, string alternatingRowStyle, string selectedRowStyle)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (rowIndex % 2 == 0)
			{
				if (!string.IsNullOrEmpty(rowStyle))
				{
					stringBuilder.Append(rowStyle);
				}
			}
			else if (!string.IsNullOrEmpty(alternatingRowStyle))
			{
				stringBuilder.Append(alternatingRowStyle);
			}
			if (!string.IsNullOrEmpty(selectedRowStyle) && rowIndex == webGrid.SelectedIndex)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(selectedRowStyle);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000084F4 File Offset: 0x000066F4
		private static HelperResult Format(Func<dynamic, object> format, dynamic arg)
		{
			WebGridRenderer.<>c__DisplayClass10 CS$<>8__locals1 = new WebGridRenderer.<>c__DisplayClass10();
			WebGridRenderer.<>c__DisplayClass10 CS$<>8__locals2 = CS$<>8__locals1;
			if (WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitea == null)
			{
				WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitea = CallSite<Func<CallSite, Func<object, object>, object, object>>.Create(Binder.Invoke(CSharpBinderFlags.None, typeof(WebGridRenderer), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			CS$<>8__locals2.result = WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitea.Target(WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitea, format, arg);
			return new HelperResult(delegate(TextWriter tw)
			{
				HelperResult helperResult = CS$<>8__locals1.result as HelperResult;
				if (helperResult != null)
				{
					helperResult.WriteTo(tw);
					return;
				}
				IHtmlString htmlString = CS$<>8__locals1.result as IHtmlString;
				if (htmlString != null)
				{
					tw.Write(htmlString);
					return;
				}
				if (WebGridRenderer.<Format>o__SiteContainer9.<>p__Siteb == null)
				{
					WebGridRenderer.<Format>o__SiteContainer9.<>p__Siteb = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(WebGridRenderer), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target = WebGridRenderer.<Format>o__SiteContainer9.<>p__Siteb.Target;
				CallSite <>p__Siteb = WebGridRenderer.<Format>o__SiteContainer9.<>p__Siteb;
				if (WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitec == null)
				{
					WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitec = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(WebGridRenderer), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				if (target(<>p__Siteb, WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitec.Target(WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitec, CS$<>8__locals1.result, null)))
				{
					if (WebGridRenderer.<Format>o__SiteContainer9.<>p__Sited == null)
					{
						WebGridRenderer.<Format>o__SiteContainer9.<>p__Sited = CallSite<Action<CallSite, TextWriter, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Write", null, typeof(WebGridRenderer), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Action<CallSite, TextWriter, object> target2 = WebGridRenderer.<Format>o__SiteContainer9.<>p__Sited.Target;
					CallSite <>p__Sited = WebGridRenderer.<Format>o__SiteContainer9.<>p__Sited;
					if (WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitee == null)
					{
						WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitee = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "HtmlEncode", null, typeof(WebGridRenderer), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					target2(<>p__Sited, tw, WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitee.Target(WebGridRenderer.<Format>o__SiteContainer9.<>p__Sitee, typeof(HttpUtility), CS$<>8__locals1.result));
				}
			});
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008574 File Offset: 0x00006774
		private static IHtmlString PrintAttributes(IDictionary<string, object> attributes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in attributes)
			{
				string s = Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture);
				stringBuilder.Append(' ').Append(HttpUtility.HtmlEncode(keyValuePair.Key)).Append("=\"").Append(HttpUtility.HtmlAttributeEncode(s)).Append('"');
			}
			return new HtmlString(stringBuilder.ToString());
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008610 File Offset: 0x00006810
		private static bool ModeEnabled(WebGridPagerModes mode, WebGridPagerModes modeCheck)
		{
			return (mode & modeCheck) == modeCheck;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00008618 File Offset: 0x00006818
		protected static HttpApplication ApplicationInstance
		{
			get
			{
				return HelperPage.Context.ApplicationInstance;
			}
		}

		// Token: 0x04000086 RID: 134
		private static readonly object _gridScriptRenderedKey = new object();

		// Token: 0x02000042 RID: 66
		[CompilerGenerated]
		private static class <Format>o__SiteContainer9
		{
			// Token: 0x040000FD RID: 253
			public static CallSite<Func<CallSite, Func<object, object>, object, object>> <>p__Sitea;

			// Token: 0x040000FE RID: 254
			public static CallSite<Func<CallSite, object, bool>> <>p__Siteb;

			// Token: 0x040000FF RID: 255
			public static CallSite<Func<CallSite, object, object, object>> <>p__Sitec;

			// Token: 0x04000100 RID: 256
			public static CallSite<Action<CallSite, TextWriter, object>> <>p__Sited;

			// Token: 0x04000101 RID: 257
			public static CallSite<Func<CallSite, Type, object, object>> <>p__Sitee;
		}
	}
}
