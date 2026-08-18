using System;

namespace System.Web.Mvc
{
	// Token: 0x020001DD RID: 477
	public class ViewDataDictionary<TModel> : ViewDataDictionary
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x00025B7C File Offset: 0x00023D7C
		public ViewDataDictionary() : base(default(TModel))
		{
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00025B9D File Offset: 0x00023D9D
		public ViewDataDictionary(TModel model) : base(model)
		{
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00025BAB File Offset: 0x00023DAB
		public ViewDataDictionary(ViewDataDictionary viewDataDictionary) : base(viewDataDictionary)
		{
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00025BB4 File Offset: 0x00023DB4
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00025BC1 File Offset: 0x00023DC1
		public new TModel Model
		{
			get
			{
				return (TModel)((object)base.Model);
			}
			set
			{
				this.SetModel(value);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x00025BD0 File Offset: 0x00023DD0
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x00025C07 File Offset: 0x00023E07
		public override ModelMetadata ModelMetadata
		{
			get
			{
				ModelMetadata modelMetadata = base.ModelMetadata;
				if (modelMetadata == null)
				{
					modelMetadata = (base.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TModel)));
				}
				return modelMetadata;
			}
			set
			{
				base.ModelMetadata = value;
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00025C10 File Offset: 0x00023E10
		protected override void SetModel(object value)
		{
			bool flag = TypeHelpers.IsCompatibleObject<TModel>(value);
			if (flag)
			{
				base.SetModel((TModel)((object)value));
				return;
			}
			InvalidOperationException ex = (value != null) ? Error.ViewDataDictionary_WrongTModelType(value.GetType(), typeof(TModel)) : Error.ViewDataDictionary_ModelCannotBeNull(typeof(TModel));
			throw ex;
		}
	}
}
