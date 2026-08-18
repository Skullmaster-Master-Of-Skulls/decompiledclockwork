using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000283 RID: 643
	public static class DesignTimeTemplateParser
	{
		// Token: 0x06001E66 RID: 7782 RVA: 0x00061A74 File Offset: 0x0005FC74
		public static Control ParseControl(DesignTimeParseData data)
		{
			Control[] array = DesignTimeTemplateParser.ParseControlsInternal(data, true);
			if (array.Length != 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x00061A92 File Offset: 0x0005FC92
		public static Control[] ParseControls(DesignTimeParseData data)
		{
			return DesignTimeTemplateParser.ParseControlsInternal(data, false);
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00061A9C File Offset: 0x0005FC9C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		internal static Control[] ParseControlsInternal(DesignTimeParseData data, bool returnFirst)
		{
			Control[] result;
			try
			{
				if (data.DesignerHost != null)
				{
					TargetFrameworkUtil.DesignerHost = data.DesignerHost;
				}
				result = DesignTimeTemplateParser.ParseControlsInternalHelper(data, returnFirst);
			}
			finally
			{
				TargetFrameworkUtil.DesignerHost = null;
			}
			return result;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x00061AE0 File Offset: 0x0005FCE0
		private static Control[] ParseControlsInternalHelper(DesignTimeParseData data, bool returnFirst)
		{
			TemplateParser templateParser = new PageParser();
			templateParser.FInDesigner = true;
			templateParser.DesignerHost = data.DesignerHost;
			templateParser.DesignTimeDataBindHandler = data.DataBindingHandler;
			templateParser.Text = data.ParseText;
			templateParser.Parse();
			ArrayList arrayList = new ArrayList();
			ArrayList subBuilders = templateParser.RootBuilder.SubBuilders;
			if (subBuilders != null)
			{
				IEnumerator enumerator = subBuilders.GetEnumerator();
				int num = 0;
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					if (obj is ControlBuilder && !(obj is CodeBlockBuilder))
					{
						ControlBuilder controlBuilder = (ControlBuilder)obj;
						IServiceProvider serviceProvider;
						if (data.DesignerHost != null)
						{
							serviceProvider = data.DesignerHost;
						}
						else
						{
							ServiceContainer serviceContainer = new ServiceContainer();
							serviceContainer.AddService(typeof(IFilterResolutionService), new DesignTimeTemplateParser.SimpleDesignTimeFilterResolutionService(data.Filter));
							serviceProvider = serviceContainer;
						}
						controlBuilder.SetServiceProvider(serviceProvider);
						try
						{
							Control value = (Control)controlBuilder.BuildObject(data.ShouldApplyTheme);
							arrayList.Add(value);
						}
						finally
						{
							controlBuilder.SetServiceProvider(null);
						}
						if (returnFirst)
						{
							break;
						}
					}
					else if (!returnFirst && obj is string)
					{
						LiteralControl value2 = new LiteralControl(obj.ToString());
						arrayList.Add(value2);
					}
					num++;
				}
			}
			data.SetUserControlRegisterEntries(templateParser.UserControlRegisterEntries, templateParser.TagRegisterEntries);
			return (Control[])arrayList.ToArray(typeof(Control));
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x00061C50 File Offset: 0x0005FE50
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static ITemplate ParseTemplate(DesignTimeParseData data)
		{
			TemplateParser templateParser = new PageParser();
			templateParser.FInDesigner = true;
			templateParser.DesignerHost = data.DesignerHost;
			templateParser.DesignTimeDataBindHandler = data.DataBindingHandler;
			templateParser.Text = data.ParseText;
			templateParser.Parse();
			templateParser.RootBuilder.Text = data.ParseText;
			templateParser.RootBuilder.SetDesignerHost(data.DesignerHost);
			return templateParser.RootBuilder;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00061CBC File Offset: 0x0005FEBC
		public static ControlBuilder ParseTheme(IDesignerHost host, string theme, string themePath)
		{
			ControlBuilder rootBuilder;
			try
			{
				TemplateParser templateParser = new DesignTimePageThemeParser(themePath);
				templateParser.FInDesigner = true;
				templateParser.DesignerHost = host;
				templateParser.ThrowOnFirstParseError = true;
				templateParser.Text = theme;
				templateParser.Parse();
				rootBuilder = templateParser.RootBuilder;
			}
			catch (Exception ex)
			{
				throw new Exception(SR.GetString("DesignTimeTemplateParser_ErrorParsingTheme") + " " + ex.Message);
			}
			return rootBuilder;
		}

		// Token: 0x02000966 RID: 2406
		private class SimpleDesignTimeFilterResolutionService : IFilterResolutionService
		{
			// Token: 0x060069F5 RID: 27125 RVA: 0x00178C7D File Offset: 0x00176E7D
			public SimpleDesignTimeFilterResolutionService(string filter)
			{
				this._currentFilter = filter;
			}

			// Token: 0x060069F6 RID: 27126 RVA: 0x00178C8C File Offset: 0x00176E8C
			bool IFilterResolutionService.EvaluateFilter(string filterName)
			{
				return string.IsNullOrEmpty(filterName) || StringUtil.EqualsIgnoreCase((this._currentFilter == null) ? string.Empty : this._currentFilter, filterName);
			}

			// Token: 0x060069F7 RID: 27127 RVA: 0x00178CB8 File Offset: 0x00176EB8
			int IFilterResolutionService.CompareFilters(string filter1, string filter2)
			{
				if (string.IsNullOrEmpty(filter1))
				{
					if (!string.IsNullOrEmpty(filter2))
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (string.IsNullOrEmpty(filter2))
					{
						return -1;
					}
					return 0;
				}
			}

			// Token: 0x04003844 RID: 14404
			private string _currentFilter;
		}
	}
}
