using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Design;
using System.Web.Compilation;
using System.Web.Configuration;

namespace System.Web.UI.Design
{
	// Token: 0x0200003D RID: 61
	public abstract class ExpressionEditor
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public string ExpressionPrefix
		{
			get
			{
				return this._expressionPrefix;
			}
		}

		// Token: 0x06000226 RID: 550
		public abstract object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider);

		// Token: 0x06000227 RID: 551 RVA: 0x0000EC60 File Offset: 0x0000CE60
		private static IDictionary GetExpressionEditorsCache(IWebApplication webApp)
		{
			IDictionaryService dictionaryService = (IDictionaryService)webApp.GetService(typeof(IDictionaryService));
			if (dictionaryService == null)
			{
				return null;
			}
			IDictionary dictionary = (IDictionary)dictionaryService.GetValue("ExpressionEditors");
			if (dictionary == null)
			{
				dictionary = new HybridDictionary(true);
				dictionaryService.SetValue("ExpressionEditors", dictionary);
			}
			return dictionary;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		private static IDictionary GetExpressionEditorsByTypeCache(IWebApplication webApp)
		{
			IDictionaryService dictionaryService = (IDictionaryService)webApp.GetService(typeof(IDictionaryService));
			if (dictionaryService == null)
			{
				return null;
			}
			IDictionary dictionary = (IDictionary)dictionaryService.GetValue("ExpressionEditorsByType");
			if (dictionary == null)
			{
				dictionary = new HybridDictionary();
				dictionaryService.SetValue("ExpressionEditorsByType", dictionary);
			}
			return dictionary;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000ED00 File Offset: 0x0000CF00
		public static ExpressionEditor GetExpressionEditor(Type expressionBuilderType, IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			if (expressionBuilderType == null)
			{
				throw new ArgumentNullException("expressionBuilderType");
			}
			ExpressionEditor expressionEditor = null;
			IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				IDictionary expressionEditorsByTypeCache = ExpressionEditor.GetExpressionEditorsByTypeCache(webApplication);
				if (expressionEditorsByTypeCache != null)
				{
					expressionEditor = (ExpressionEditor)expressionEditorsByTypeCache[expressionBuilderType];
				}
				if (expressionEditor == null)
				{
					Configuration configuration = webApplication.OpenWebConfiguration(true);
					if (configuration != null)
					{
						CompilationSection compilationSection = (CompilationSection)configuration.GetSection("system.web/compilation");
						ExpressionBuilderCollection expressionBuilders = compilationSection.ExpressionBuilders;
						bool flag = false;
						string fullName = expressionBuilderType.FullName;
						foreach (object obj in expressionBuilders)
						{
							System.Web.Configuration.ExpressionBuilder expressionBuilder = (System.Web.Configuration.ExpressionBuilder)obj;
							if (string.Equals(expressionBuilder.Type, fullName, StringComparison.OrdinalIgnoreCase))
							{
								expressionEditor = ExpressionEditor.GetExpressionEditorInternal(expressionBuilderType, expressionBuilder.ExpressionPrefix, webApplication, serviceProvider);
								flag = true;
							}
						}
						if (!flag)
						{
							object[] customAttributes = expressionBuilderType.GetCustomAttributes(typeof(ExpressionPrefixAttribute), true);
							ExpressionPrefixAttribute expressionPrefixAttribute = null;
							if (customAttributes.Length != 0)
							{
								expressionPrefixAttribute = (ExpressionPrefixAttribute)customAttributes[0];
							}
							if (expressionPrefixAttribute != null)
							{
								System.Web.Configuration.ExpressionBuilder expressionBuilder2 = new System.Web.Configuration.ExpressionBuilder(expressionPrefixAttribute.ExpressionPrefix, expressionBuilderType.FullName);
								configuration = webApplication.OpenWebConfiguration(false);
								compilationSection = (CompilationSection)configuration.GetSection("system.web/compilation");
								expressionBuilders = compilationSection.ExpressionBuilders;
								expressionBuilders.Add(expressionBuilder2);
								configuration.Save();
								expressionEditor = ExpressionEditor.GetExpressionEditorInternal(expressionBuilderType, expressionBuilder2.ExpressionPrefix, webApplication, serviceProvider);
							}
						}
					}
				}
			}
			return expressionEditor;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000EE94 File Offset: 0x0000D094
		internal static ExpressionEditor GetExpressionEditorInternal(Type expressionBuilderType, string expressionPrefix, IWebApplication webApp, IServiceProvider serviceProvider)
		{
			if (expressionBuilderType == null)
			{
				throw new ArgumentNullException("expressionBuilderType");
			}
			ExpressionEditor expressionEditor = null;
			object[] customAttributes = expressionBuilderType.GetCustomAttributes(typeof(ExpressionEditorAttribute), true);
			ExpressionEditorAttribute expressionEditorAttribute = null;
			if (customAttributes.Length != 0)
			{
				expressionEditorAttribute = (ExpressionEditorAttribute)customAttributes[0];
			}
			if (expressionEditorAttribute != null)
			{
				string editorTypeName = expressionEditorAttribute.EditorTypeName;
				Type type = Type.GetType(editorTypeName);
				if (type == null)
				{
					ITypeResolutionService typeResolutionService = (ITypeResolutionService)serviceProvider.GetService(typeof(ITypeResolutionService));
					if (typeResolutionService != null)
					{
						type = typeResolutionService.GetType(editorTypeName);
					}
				}
				if (type != null && typeof(ExpressionEditor).IsAssignableFrom(type))
				{
					expressionEditor = (ExpressionEditor)Activator.CreateInstance(type);
					expressionEditor.SetExpressionPrefix(expressionPrefix);
				}
				IDictionary expressionEditorsCache = ExpressionEditor.GetExpressionEditorsCache(webApp);
				if (expressionEditorsCache != null)
				{
					expressionEditorsCache[expressionPrefix] = expressionEditor;
				}
				IDictionary expressionEditorsByTypeCache = ExpressionEditor.GetExpressionEditorsByTypeCache(webApp);
				if (expressionEditorsByTypeCache != null)
				{
					expressionEditorsByTypeCache[expressionBuilderType] = expressionEditor;
				}
			}
			return expressionEditor;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000EF78 File Offset: 0x0000D178
		public static ExpressionEditor GetExpressionEditor(string expressionPrefix, IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			if (expressionPrefix.Length == 0)
			{
				return null;
			}
			ExpressionEditor expressionEditor = null;
			IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				IDictionary expressionEditorsCache = ExpressionEditor.GetExpressionEditorsCache(webApplication);
				if (expressionEditorsCache != null)
				{
					expressionEditor = (ExpressionEditor)expressionEditorsCache[expressionPrefix];
				}
				if (expressionEditor == null)
				{
					string expressionPrefix2;
					Type expressionBuilderType = ExpressionEditor.GetExpressionBuilderType(expressionPrefix, serviceProvider, out expressionPrefix2);
					if (expressionBuilderType != null)
					{
						expressionEditor = ExpressionEditor.GetExpressionEditorInternal(expressionBuilderType, expressionPrefix2, webApplication, serviceProvider);
					}
				}
			}
			return expressionEditor;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000EFF4 File Offset: 0x0000D1F4
		internal static Type GetExpressionBuilderType(string expressionPrefix, IServiceProvider serviceProvider, out string trueExpressionPrefix)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			trueExpressionPrefix = expressionPrefix;
			if (expressionPrefix.Length == 0)
			{
				return null;
			}
			Type type = null;
			IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
			if (webApplication != null)
			{
				Configuration configuration = webApplication.OpenWebConfiguration(true);
				if (configuration != null)
				{
					CompilationSection compilationSection = (CompilationSection)configuration.GetSection("system.web/compilation");
					ExpressionBuilderCollection expressionBuilders = compilationSection.ExpressionBuilders;
					foreach (object obj in expressionBuilders)
					{
						System.Web.Configuration.ExpressionBuilder expressionBuilder = (System.Web.Configuration.ExpressionBuilder)obj;
						if (string.Equals(expressionPrefix, expressionBuilder.ExpressionPrefix, StringComparison.OrdinalIgnoreCase))
						{
							trueExpressionPrefix = expressionBuilder.ExpressionPrefix;
							type = Type.GetType(expressionBuilder.Type);
							if (type == null)
							{
								ITypeResolutionService typeResolutionService = (ITypeResolutionService)serviceProvider.GetService(typeof(ITypeResolutionService));
								if (typeResolutionService != null)
								{
									type = typeResolutionService.GetType(expressionBuilder.Type);
								}
							}
						}
					}
				}
			}
			return type;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000F104 File Offset: 0x0000D304
		public virtual ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			return new ExpressionEditor.GenericExpressionEditorSheet(expression, serviceProvider);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000F10D File Offset: 0x0000D30D
		internal void SetExpressionPrefix(string expressionPrefix)
		{
			this._expressionPrefix = expressionPrefix;
		}

		// Token: 0x04000154 RID: 340
		private const string expressionEditorsByTypeKey = "ExpressionEditorsByType";

		// Token: 0x04000155 RID: 341
		private const string expressionEditorsKey = "ExpressionEditors";

		// Token: 0x04000156 RID: 342
		private string _expressionPrefix;

		// Token: 0x020003B2 RID: 946
		private class GenericExpressionEditorSheet : ExpressionEditorSheet
		{
			// Token: 0x06002606 RID: 9734 RVA: 0x000EC5B7 File Offset: 0x000EA7B7
			public GenericExpressionEditorSheet(string expression, IServiceProvider serviceProvider) : base(serviceProvider)
			{
				this._expression = expression;
			}

			// Token: 0x17000803 RID: 2051
			// (get) Token: 0x06002607 RID: 9735 RVA: 0x000EC5C7 File Offset: 0x000EA7C7
			// (set) Token: 0x06002608 RID: 9736 RVA: 0x000EC5DD File Offset: 0x000EA7DD
			[DefaultValue("")]
			[SRDescription("ExpressionEditor_Expression")]
			public string Expression
			{
				get
				{
					if (this._expression == null)
					{
						return string.Empty;
					}
					return this._expression;
				}
				set
				{
					this._expression = value;
				}
			}

			// Token: 0x06002609 RID: 9737 RVA: 0x000EC5E6 File Offset: 0x000EA7E6
			public override string GetExpression()
			{
				return this._expression;
			}

			// Token: 0x04001BAF RID: 7087
			private string _expression;
		}
	}
}
