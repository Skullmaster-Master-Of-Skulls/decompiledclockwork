using System;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000022 RID: 34
	internal sealed class EdmModelValidationContext
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000134 RID: 308 RVA: 0x000079B8 File Offset: 0x00005BB8
		// (remove) Token: 0x06000135 RID: 309 RVA: 0x000079F0 File Offset: 0x00005BF0
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x06000136 RID: 310 RVA: 0x00007A25 File Offset: 0x00005C25
		public EdmModelValidationContext(EdmModel model, bool validateSyntax)
		{
			this._model = model;
			this._validateSyntax = validateSyntax;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00007A3B File Offset: 0x00005C3B
		public bool ValidateSyntax
		{
			get
			{
				return this._validateSyntax;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00007A43 File Offset: 0x00005C43
		public EdmModel Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007A4B File Offset: 0x00005C4B
		public bool IsCSpace
		{
			get
			{
				return this._model.Containers.First<EntityContainer>().DataSpace == DataSpace.CSpace;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007A68 File Offset: 0x00005C68
		public void AddError(MetadataItem item, string propertyName, string errorMessage)
		{
			this.RaiseDataModelValidationEvent(new DataModelErrorEventArgs
			{
				ErrorMessage = errorMessage,
				Item = item,
				PropertyName = propertyName
			});
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007A97 File Offset: 0x00005C97
		private void RaiseDataModelValidationEvent(DataModelErrorEventArgs error)
		{
			if (this.OnError != null)
			{
				this.OnError(this, error);
			}
		}

		// Token: 0x040000A8 RID: 168
		private readonly EdmModel _model;

		// Token: 0x040000A9 RID: 169
		private readonly bool _validateSyntax;
	}
}
