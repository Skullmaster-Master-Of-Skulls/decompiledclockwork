using System;

namespace System.Web.UI
{
	// Token: 0x020002A3 RID: 675
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IDReferencePropertyAttribute : Attribute
	{
		// Token: 0x06001F8F RID: 8079 RVA: 0x0006566F File Offset: 0x0006386F
		public IDReferencePropertyAttribute() : this(typeof(Control))
		{
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x00065681 File Offset: 0x00063881
		public IDReferencePropertyAttribute(Type referencedControlType)
		{
			this._referencedControlType = referencedControlType;
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x00065690 File Offset: 0x00063890
		public Type ReferencedControlType
		{
			get
			{
				return this._referencedControlType;
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00065698 File Offset: 0x00063898
		public override int GetHashCode()
		{
			if (!(this.ReferencedControlType != null))
			{
				return 0;
			}
			return this.ReferencedControlType.GetHashCode();
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000656B8 File Offset: 0x000638B8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			IDReferencePropertyAttribute idreferencePropertyAttribute = obj as IDReferencePropertyAttribute;
			return idreferencePropertyAttribute != null && this.ReferencedControlType == idreferencePropertyAttribute.ReferencedControlType;
		}

		// Token: 0x04001AB4 RID: 6836
		private Type _referencedControlType;
	}
}
