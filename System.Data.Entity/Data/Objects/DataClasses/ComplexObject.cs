using System;
using System.Runtime.Serialization;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x0200018B RID: 395
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class ComplexObject : StructuralObject
	{
		// Token: 0x06001C34 RID: 7220 RVA: 0x0005FC35 File Offset: 0x0005DE35
		internal void AttachToParent(StructuralObject parent, string parentPropertyName)
		{
			if (this._parent != null)
			{
				throw EntityUtil.ComplexObjectAlreadyAttachedToParent();
			}
			this._parent = parent;
			this._parentPropertyName = parentPropertyName;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x0005FC53 File Offset: 0x0005DE53
		internal void DetachFromParent()
		{
			this._parent = null;
			this._parentPropertyName = null;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x0005FC63 File Offset: 0x0005DE63
		protected sealed override void ReportPropertyChanging(string property)
		{
			EntityUtil.CheckStringArgument(property, "property");
			base.ReportPropertyChanging(property);
			this.ReportComplexPropertyChanging(null, this, property);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0005FC80 File Offset: 0x0005DE80
		protected sealed override void ReportPropertyChanged(string property)
		{
			EntityUtil.CheckStringArgument(property, "property");
			this.ReportComplexPropertyChanged(null, this, property);
			base.ReportPropertyChanged(property);
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0005FC9D File Offset: 0x0005DE9D
		internal sealed override bool IsChangeTracked
		{
			get
			{
				return this._parent != null && this._parent.IsChangeTracked;
			}
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0005FCB4 File Offset: 0x0005DEB4
		internal sealed override void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			if (this._parent != null)
			{
				this._parent.ReportComplexPropertyChanging(this._parentPropertyName, complexObject, complexMemberName);
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0005FCD1 File Offset: 0x0005DED1
		internal sealed override void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName)
		{
			if (this._parent != null)
			{
				this._parent.ReportComplexPropertyChanged(this._parentPropertyName, complexObject, complexMemberName);
			}
		}

		// Token: 0x04000BA8 RID: 2984
		private StructuralObject _parent;

		// Token: 0x04000BA9 RID: 2985
		private string _parentPropertyName;
	}
}
