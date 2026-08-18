using System;

namespace System.Web.UI
{
	// Token: 0x020002F0 RID: 752
	[AttributeUsage(AttributeTargets.All)]
	public sealed class PersistenceModeAttribute : Attribute
	{
		// Token: 0x060022DE RID: 8926 RVA: 0x00071C82 File Offset: 0x0006FE82
		public PersistenceModeAttribute(PersistenceMode mode)
		{
			if (mode < PersistenceMode.Attribute || mode > PersistenceMode.EncodedInnerDefaultProperty)
			{
				throw new ArgumentOutOfRangeException("mode");
			}
			this.mode = mode;
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x00071CA4 File Offset: 0x0006FEA4
		public PersistenceMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00071CAC File Offset: 0x0006FEAC
		public override int GetHashCode()
		{
			return this.Mode.GetHashCode();
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00071CCD File Offset: 0x0006FECD
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is PersistenceModeAttribute && ((PersistenceModeAttribute)obj).Mode == this.mode);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00071CF5 File Offset: 0x0006FEF5
		public override bool IsDefaultAttribute()
		{
			return this.Equals(PersistenceModeAttribute.Default);
		}

		// Token: 0x04001C82 RID: 7298
		public static readonly PersistenceModeAttribute Attribute = new PersistenceModeAttribute(PersistenceMode.Attribute);

		// Token: 0x04001C83 RID: 7299
		public static readonly PersistenceModeAttribute InnerProperty = new PersistenceModeAttribute(PersistenceMode.InnerProperty);

		// Token: 0x04001C84 RID: 7300
		public static readonly PersistenceModeAttribute InnerDefaultProperty = new PersistenceModeAttribute(PersistenceMode.InnerDefaultProperty);

		// Token: 0x04001C85 RID: 7301
		public static readonly PersistenceModeAttribute EncodedInnerDefaultProperty = new PersistenceModeAttribute(PersistenceMode.EncodedInnerDefaultProperty);

		// Token: 0x04001C86 RID: 7302
		public static readonly PersistenceModeAttribute Default = PersistenceModeAttribute.Attribute;

		// Token: 0x04001C87 RID: 7303
		private PersistenceMode mode;
	}
}
