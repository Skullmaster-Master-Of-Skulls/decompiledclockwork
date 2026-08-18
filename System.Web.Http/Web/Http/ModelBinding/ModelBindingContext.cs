using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000147 RID: 327
	public class ModelBindingContext
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x0001A8DF File Offset: 0x00018ADF
		public ModelBindingContext() : this(null)
		{
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		public ModelBindingContext(ModelBindingContext bindingContext)
		{
			if (bindingContext != null)
			{
				this.ModelState = bindingContext.ModelState;
				this.ValueProvider = bindingContext.ValueProvider;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0001A90B File Offset: 0x00018B0B
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0001A91E File Offset: 0x00018B1E
		public object Model
		{
			get
			{
				this.EnsureModelMetadata();
				return this.ModelMetadata.Model;
			}
			set
			{
				this.EnsureModelMetadata();
				this.ModelMetadata.Model = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x0001A932 File Offset: 0x00018B32
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x0001A93A File Offset: 0x00018B3A
		public ModelMetadata ModelMetadata { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001A943 File Offset: 0x00018B43
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0001A95E File Offset: 0x00018B5E
		public string ModelName
		{
			get
			{
				if (this._modelName == null)
				{
					this._modelName = string.Empty;
				}
				return this._modelName;
			}
			set
			{
				this._modelName = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001A967 File Offset: 0x00018B67
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x0001A982 File Offset: 0x00018B82
		public ModelStateDictionary ModelState
		{
			get
			{
				if (this._modelState == null)
				{
					this._modelState = new ModelStateDictionary();
				}
				return this._modelState;
			}
			set
			{
				this._modelState = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001A98B File Offset: 0x00018B8B
		public Type ModelType
		{
			get
			{
				this.EnsureModelMetadata();
				return this.ModelMetadata.ModelType;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		public IDictionary<string, ModelMetadata> PropertyMetadata
		{
			get
			{
				if (this._propertyMetadata == null)
				{
					this._propertyMetadata = this.ModelMetadata.Properties.ToDictionary((ModelMetadata m) => m.PropertyName, StringComparer.OrdinalIgnoreCase);
				}
				return this._propertyMetadata;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001A9FB File Offset: 0x00018BFB
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x0001AA22 File Offset: 0x00018C22
		public ModelValidationNode ValidationNode
		{
			get
			{
				if (this._validationNode == null)
				{
					this._validationNode = new ModelValidationNode(this.ModelMetadata, this.ModelName);
				}
				return this._validationNode;
			}
			set
			{
				this._validationNode = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001AA2B File Offset: 0x00018C2B
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0001AA33 File Offset: 0x00018C33
		public IValueProvider ValueProvider { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001AA3C File Offset: 0x00018C3C
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x0001AA44 File Offset: 0x00018C44
		public bool FallbackToEmptyPrefix { get; set; }

		// Token: 0x0600081E RID: 2078 RVA: 0x0001AA4D File Offset: 0x00018C4D
		private void EnsureModelMetadata()
		{
			if (this.ModelMetadata == null)
			{
				throw Error.InvalidOperation(SRResources.ModelBindingContext_ModelMetadataMustBeSet, new object[0]);
			}
		}

		// Token: 0x04000258 RID: 600
		private string _modelName;

		// Token: 0x04000259 RID: 601
		private ModelStateDictionary _modelState;

		// Token: 0x0400025A RID: 602
		private Dictionary<string, ModelMetadata> _propertyMetadata;

		// Token: 0x0400025B RID: 603
		private ModelValidationNode _validationNode;
	}
}
