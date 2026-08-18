using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Compilation;

namespace System.Web.UI.Design
{
	// Token: 0x02000063 RID: 99
	public class ResourceExpressionEditor : ExpressionEditor
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0001020C File Offset: 0x0000E40C
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			ResourceExpressionFields resourceExpressionFields;
			if (parseTimeData is ResourceExpressionFields)
			{
				resourceExpressionFields = (ResourceExpressionFields)parseTimeData;
			}
			else
			{
				resourceExpressionFields = ResourceExpressionBuilder.ParseExpression(expression);
			}
			if (string.IsNullOrEmpty(resourceExpressionFields.ResourceKey))
			{
				return null;
			}
			object obj = null;
			DesignTimeResourceProviderFactory designTimeResourceProviderFactory = ControlDesigner.GetDesignTimeResourceProviderFactory(serviceProvider);
			IResourceProvider resourceProvider;
			if (string.IsNullOrEmpty(resourceExpressionFields.ClassKey))
			{
				resourceProvider = designTimeResourceProviderFactory.CreateDesignTimeLocalResourceProvider(serviceProvider);
			}
			else
			{
				resourceProvider = designTimeResourceProviderFactory.CreateDesignTimeGlobalResourceProvider(serviceProvider, resourceExpressionFields.ClassKey);
			}
			if (resourceProvider != null)
			{
				obj = resourceProvider.GetObject(resourceExpressionFields.ResourceKey, CultureInfo.InvariantCulture);
			}
			if (obj != null)
			{
				Type type = obj.GetType();
				if (!propertyType.IsAssignableFrom(type))
				{
					TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
					if (converter != null && converter.CanConvertFrom(type))
					{
						return converter.ConvertFrom(obj);
					}
				}
			}
			return obj;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000102BC File Offset: 0x0000E4BC
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			return new ResourceExpressionEditorSheet(expression, serviceProvider);
		}
	}
}
