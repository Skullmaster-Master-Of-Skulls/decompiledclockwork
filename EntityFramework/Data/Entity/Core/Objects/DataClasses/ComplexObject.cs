using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200052F RID: 1327
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class ComplexObject : StructuralObject
	{
		// Token: 0x060032CE RID: 13006 RVA: 0x000F05B7 File Offset: 0x000EE7B7
		internal void AttachToParent(StructuralObject parent, string parentPropertyName)
		{
			if (this._parent != null)
			{
				throw new InvalidOperationException(Strings.ComplexObject_ComplexObjectAlreadyAttachedToParent);
			}
			this._parent = parent;
			this._parentPropertyName = parentPropertyName;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000F05DA File Offset: 0x000EE7DA
		internal void DetachFromParent()
		{
			this._parent = null;
			this._parentPropertyName = null;
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000F05EA File Offset: 0x000EE7EA
		protected sealed override void ReportPropertyChanging(string property)
		{
			Check.NotEmpty(property, "property");
			base.ReportPropertyChanging(property);
			this.ReportComplexPropertyChanging(null, this, property);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000F0608 File Offset: 0x000EE808
		protected sealed override void ReportPropertyChanged(string property)
		{
			Check.NotEmpty(property, "property");
			this.ReportComplexPropertyChanged(null, this, property);
			base.ReportPropertyChanged(property);
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000F0626 File Offset: 0x000EE826
		internal sealed override bool IsChangeTracked
		{
			get
			{
				return this._parent != null && this._parent.IsChangeTracked;
			}
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000F063D File Offset: 0x000EE83D
		internal sealed override void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			if (this._parent != null)
			{
				this._parent.ReportComplexPropertyChanging(this._parentPropertyName, complexObject, complexMemberName);
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000F065A File Offset: 0x000EE85A
		internal sealed override void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			if (this._parent != null)
			{
				this._parent.ReportComplexPropertyChanged(this._parentPropertyName, complexObject, complexMemberName);
			}
		}

		// Token: 0x04001369 RID: 4969
		private StructuralObject _parent;

		// Token: 0x0400136A RID: 4970
		private string _parentPropertyName;
	}
}
