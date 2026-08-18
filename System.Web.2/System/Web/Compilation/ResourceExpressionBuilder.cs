using System;
using System.CodeDom;
using System.ComponentModel;
using System.Globalization;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000858 RID: 2136
	[ExpressionPrefix("Resources")]
	[ExpressionEditor("System.Web.UI.Design.ResourceExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ResourceExpressionBuilder : ExpressionBuilder
	{
		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x0600652F RID: 25903 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x00164420 File Offset: 0x00162620
		public static ResourceExpressionFields ParseExpression(string expression)
		{
			return ResourceExpressionBuilder.ParseExpressionInternal(expression);
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x00164428 File Offset: 0x00162628
		public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			ResourceExpressionFields resourceExpressionFields = null;
			try
			{
				resourceExpressionFields = ResourceExpressionBuilder.ParseExpressionInternal(expression);
			}
			catch
			{
			}
			if (resourceExpressionFields == null)
			{
				throw new HttpException(SR.GetString("Invalid_res_expr", new object[]
				{
					expression
				}));
			}
			if (context.VirtualPathObject != null)
			{
				IResourceProvider resourceProvider = ResourceExpressionBuilder.GetResourceProvider(resourceExpressionFields, VirtualPath.Create(context.VirtualPath));
				object obj = null;
				if (resourceProvider != null)
				{
					try
					{
						obj = resourceProvider.GetObject(resourceExpressionFields.ResourceKey, CultureInfo.InvariantCulture);
					}
					catch
					{
					}
				}
				if (obj == null)
				{
					throw new HttpException(SR.GetString("Res_not_found", new object[]
					{
						resourceExpressionFields.ResourceKey
					}));
				}
			}
			return resourceExpressionFields;
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x001644DC File Offset: 0x001626DC
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			ResourceExpressionFields resourceExpressionFields = (ResourceExpressionFields)parsedData;
			if (resourceExpressionFields.ClassKey.Length == 0)
			{
				return this.GetPageResCodeExpression(resourceExpressionFields.ResourceKey, entry);
			}
			return this.GetAppResCodeExpression(resourceExpressionFields.ClassKey, resourceExpressionFields.ResourceKey, entry);
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x00164520 File Offset: 0x00162720
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			ResourceExpressionFields resourceExpressionFields = (ResourceExpressionFields)parsedData;
			IResourceProvider resourceProvider = ResourceExpressionBuilder.GetResourceProvider(resourceExpressionFields, context.VirtualPathObject);
			if (entry.Type == typeof(string))
			{
				return ResourceExpressionBuilder.GetResourceObject(resourceProvider, resourceExpressionFields.ResourceKey, null);
			}
			return ResourceExpressionBuilder.GetResourceObject(resourceProvider, resourceExpressionFields.ResourceKey, null, entry.DeclaringType, entry.PropertyInfo.Name);
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x00164588 File Offset: 0x00162788
		private CodeExpression GetAppResCodeExpression(string classKey, string resourceKey, BoundPropertyEntry entry)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "GetGlobalResourceObject";
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(classKey));
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(resourceKey));
			if (entry.Type != typeof(string) && entry.Type != null)
			{
				codeMethodInvokeExpression.Parameters.Add(new CodeTypeOfExpression(entry.DeclaringType));
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(entry.PropertyInfo.Name));
			}
			return codeMethodInvokeExpression;
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x00164638 File Offset: 0x00162838
		private CodeExpression GetPageResCodeExpression(string resourceKey, BoundPropertyEntry entry)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method.TargetObject = new CodeThisReferenceExpression();
			codeMethodInvokeExpression.Method.MethodName = "GetLocalResourceObject";
			codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(resourceKey));
			if (entry.Type != typeof(string) && entry.Type != null)
			{
				codeMethodInvokeExpression.Parameters.Add(new CodeTypeOfExpression(entry.DeclaringType));
				codeMethodInvokeExpression.Parameters.Add(new CodePrimitiveExpression(entry.PropertyInfo.Name));
			}
			return codeMethodInvokeExpression;
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x001646D6 File Offset: 0x001628D6
		internal static object GetGlobalResourceObject(string classKey, string resourceKey)
		{
			return ResourceExpressionBuilder.GetGlobalResourceObject(classKey, resourceKey, null, null, null);
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x001646E4 File Offset: 0x001628E4
		internal static object GetGlobalResourceObject(string classKey, string resourceKey, Type objType, string propName, CultureInfo culture)
		{
			IResourceProvider globalResourceProvider = ResourceExpressionBuilder.GetGlobalResourceProvider(classKey);
			return ResourceExpressionBuilder.GetResourceObject(globalResourceProvider, resourceKey, culture, objType, propName);
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00164703 File Offset: 0x00162903
		internal static object GetResourceObject(IResourceProvider resourceProvider, string resourceKey, CultureInfo culture)
		{
			return ResourceExpressionBuilder.GetResourceObject(resourceProvider, resourceKey, culture, null, null);
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x00164710 File Offset: 0x00162910
		internal static object GetResourceObject(IResourceProvider resourceProvider, string resourceKey, CultureInfo culture, Type objType, string propName)
		{
			if (resourceProvider == null)
			{
				return null;
			}
			object @object = resourceProvider.GetObject(resourceKey, culture);
			if (objType == null)
			{
				return @object;
			}
			string text = @object as string;
			if (text == null)
			{
				return @object;
			}
			return ResourceExpressionBuilder.ObjectFromString(text, objType, propName);
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x0016474C File Offset: 0x0016294C
		private static object ObjectFromString(string value, Type objType, string propName)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(objType)[propName];
			if (propertyDescriptor == null)
			{
				return null;
			}
			TypeConverter converter = propertyDescriptor.Converter;
			if (converter == null)
			{
				return null;
			}
			return converter.ConvertFromInvariantString(value);
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x00164780 File Offset: 0x00162980
		private static ResourceExpressionFields ParseExpressionInternal(string expression)
		{
			string classKey = null;
			string resourceKey = null;
			if (expression.Length == 0)
			{
				return new ResourceExpressionFields(classKey, resourceKey);
			}
			string[] array = expression.Split(new char[]
			{
				','
			});
			int num = array.Length;
			if (num > 2)
			{
				return null;
			}
			if (num == 1)
			{
				resourceKey = array[0].Trim();
			}
			else
			{
				classKey = array[0].Trim();
				resourceKey = array[1].Trim();
			}
			return new ResourceExpressionFields(classKey, resourceKey);
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x001647EA File Offset: 0x001629EA
		private static IResourceProvider GetResourceProvider(ResourceExpressionFields fields, VirtualPath virtualPath)
		{
			if (fields.ClassKey.Length == 0)
			{
				return ResourceExpressionBuilder.GetLocalResourceProvider(virtualPath);
			}
			return ResourceExpressionBuilder.GetGlobalResourceProvider(fields.ClassKey);
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x0016480C File Offset: 0x00162A0C
		private static void EnsureResourceProviderFactory()
		{
			if (ResourceExpressionBuilder.s_resourceProviderFactory != null)
			{
				return;
			}
			GlobalizationSection globalization = RuntimeConfig.GetAppConfig().Globalization;
			Type resourceProviderFactoryTypeInternal = globalization.ResourceProviderFactoryTypeInternal;
			if (resourceProviderFactoryTypeInternal == null)
			{
				ResourceExpressionBuilder.s_resourceProviderFactory = new ResXResourceProviderFactory();
				return;
			}
			ResourceExpressionBuilder.s_resourceProviderFactory = (ResourceProviderFactory)HttpRuntime.CreatePublicInstanceByWebObjectActivator(resourceProviderFactoryTypeInternal);
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x0016485C File Offset: 0x00162A5C
		private static IResourceProvider GetGlobalResourceProvider(string classKey)
		{
			string str = "Resources." + classKey;
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string key = "A" + str;
			IResourceProvider resourceProvider = internalCache.Get(key) as IResourceProvider;
			if (resourceProvider != null)
			{
				return resourceProvider;
			}
			ResourceExpressionBuilder.EnsureResourceProviderFactory();
			resourceProvider = ResourceExpressionBuilder.s_resourceProviderFactory.CreateGlobalResourceProvider(classKey);
			internalCache.Insert(key, resourceProvider, null);
			return resourceProvider;
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x001648B9 File Offset: 0x00162AB9
		internal static IResourceProvider GetLocalResourceProvider(TemplateControl templateControl)
		{
			return ResourceExpressionBuilder.GetLocalResourceProvider(templateControl.VirtualPath);
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x001648C8 File Offset: 0x00162AC8
		internal static IResourceProvider GetLocalResourceProvider(VirtualPath virtualPath)
		{
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			string key = "A" + virtualPath.VirtualPathString;
			IResourceProvider resourceProvider = internalCache.Get(key) as IResourceProvider;
			if (resourceProvider != null)
			{
				return resourceProvider;
			}
			ResourceExpressionBuilder.EnsureResourceProviderFactory();
			resourceProvider = ResourceExpressionBuilder.s_resourceProviderFactory.CreateLocalResourceProvider(virtualPath.VirtualPathString);
			internalCache.Insert(key, resourceProvider, null);
			return resourceProvider;
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00164923 File Offset: 0x00162B23
		internal static object GetParsedData(string resourceKey)
		{
			return new ResourceExpressionFields(string.Empty, resourceKey);
		}

		// Token: 0x04003432 RID: 13362
		private static ResourceProviderFactory s_resourceProviderFactory;
	}
}
