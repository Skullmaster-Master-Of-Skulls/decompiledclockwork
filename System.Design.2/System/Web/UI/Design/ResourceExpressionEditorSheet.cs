using System;
using System.Collections;
using System.ComponentModel;
using System.Design;
using System.Resources;
using System.Web.Compilation;

namespace System.Web.UI.Design
{
	// Token: 0x02000064 RID: 100
	public class ResourceExpressionEditorSheet : ExpressionEditorSheet
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x000102C8 File Offset: 0x0000E4C8
		public ResourceExpressionEditorSheet(string expression, IServiceProvider serviceProvider) : base(serviceProvider)
		{
			if (!string.IsNullOrEmpty(expression))
			{
				ResourceExpressionEditorSheet.ResourceExpressionFields resourceExpressionFields = ResourceExpressionEditorSheet.ParseExpressionInternal(expression);
				this.ClassKey = resourceExpressionFields.ClassKey;
				this.ResourceKey = resourceExpressionFields.ResourceKey;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00010303 File Offset: 0x0000E503
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00010319 File Offset: 0x0000E519
		[DefaultValue("")]
		[SRDescription("ResourceExpressionEditorSheet_ClassKey")]
		public string ClassKey
		{
			get
			{
				if (this._classKey == null)
				{
					return string.Empty;
				}
				return this._classKey;
			}
			set
			{
				this._classKey = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00010322 File Offset: 0x0000E522
		public override bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.ResourceKey);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00010332 File Offset: 0x0000E532
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00010348 File Offset: 0x0000E548
		[DefaultValue("")]
		[SRDescription("ResourceExpressionEditorSheet_ResourceKey")]
		[TypeConverter(typeof(ResourceExpressionEditorSheet.ResourceKeyTypeConverter))]
		public string ResourceKey
		{
			get
			{
				if (this._resourceKey == null)
				{
					return string.Empty;
				}
				return this._resourceKey;
			}
			set
			{
				this._resourceKey = value;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00010354 File Offset: 0x0000E554
		public override string GetExpression()
		{
			string empty = string.Empty;
			if (!string.IsNullOrEmpty(this._classKey))
			{
				return this._classKey + ", " + this._resourceKey;
			}
			return this._resourceKey;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00010394 File Offset: 0x0000E594
		private static ResourceExpressionEditorSheet.ResourceExpressionFields ParseExpressionInternal(string expression)
		{
			ResourceExpressionEditorSheet.ResourceExpressionFields resourceExpressionFields = new ResourceExpressionEditorSheet.ResourceExpressionFields();
			int length = expression.Length;
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
				resourceExpressionFields.ResourceKey = array[0].Trim();
			}
			else
			{
				resourceExpressionFields.ClassKey = array[0].Trim();
				resourceExpressionFields.ResourceKey = array[1].Trim();
			}
			return resourceExpressionFields;
		}

		// Token: 0x04000164 RID: 356
		private string _classKey;

		// Token: 0x04000165 RID: 357
		private string _resourceKey;

		// Token: 0x020003B4 RID: 948
		internal class ResourceExpressionFields
		{
			// Token: 0x04001BB1 RID: 7089
			internal string ClassKey;

			// Token: 0x04001BB2 RID: 7090
			internal string ResourceKey;
		}

		// Token: 0x020003B5 RID: 949
		private class ResourceKeyTypeConverter : StringConverter
		{
			// Token: 0x06002611 RID: 9745 RVA: 0x000EC6C4 File Offset: 0x000EA8C4
			private static ICollection GetResourceKeys(IServiceProvider serviceProvider, string classKey)
			{
				DesignTimeResourceProviderFactory designTimeResourceProviderFactory = ControlDesigner.GetDesignTimeResourceProviderFactory(serviceProvider);
				IResourceProvider resourceProvider;
				if (string.IsNullOrEmpty(classKey))
				{
					resourceProvider = designTimeResourceProviderFactory.CreateDesignTimeLocalResourceProvider(serviceProvider);
				}
				else
				{
					resourceProvider = designTimeResourceProviderFactory.CreateDesignTimeGlobalResourceProvider(serviceProvider, classKey);
				}
				if (resourceProvider != null)
				{
					IResourceReader resourceReader = resourceProvider.ResourceReader;
					if (resourceReader != null)
					{
						ArrayList arrayList = new ArrayList();
						foreach (object obj in resourceReader)
						{
							arrayList.Add(((DictionaryEntry)obj).Key);
						}
						arrayList.Sort(StringComparer.CurrentCultureIgnoreCase);
						return arrayList;
					}
				}
				return null;
			}

			// Token: 0x06002612 RID: 9746 RVA: 0x000EC76C File Offset: 0x000EA96C
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (context != null && context.Instance != null)
				{
					ResourceExpressionEditorSheet resourceExpressionEditorSheet = (ResourceExpressionEditorSheet)context.Instance;
					ICollection resourceKeys = ResourceExpressionEditorSheet.ResourceKeyTypeConverter.GetResourceKeys(resourceExpressionEditorSheet.ServiceProvider, resourceExpressionEditorSheet.ClassKey);
					if (resourceKeys != null && resourceKeys.Count > 0)
					{
						return new TypeConverter.StandardValuesCollection(resourceKeys);
					}
				}
				return base.GetStandardValues(context);
			}

			// Token: 0x06002613 RID: 9747 RVA: 0x0000445B File Offset: 0x0000265B
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return false;
			}

			// Token: 0x06002614 RID: 9748 RVA: 0x000EC7BC File Offset: 0x000EA9BC
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				if (context != null && context.Instance != null)
				{
					ResourceExpressionEditorSheet resourceExpressionEditorSheet = (ResourceExpressionEditorSheet)context.Instance;
					ICollection resourceKeys = ResourceExpressionEditorSheet.ResourceKeyTypeConverter.GetResourceKeys(resourceExpressionEditorSheet.ServiceProvider, resourceExpressionEditorSheet.ClassKey);
					if (resourceKeys != null && resourceKeys.Count > 0)
					{
						return true;
					}
				}
				return base.GetStandardValuesSupported(context);
			}
		}
	}
}
