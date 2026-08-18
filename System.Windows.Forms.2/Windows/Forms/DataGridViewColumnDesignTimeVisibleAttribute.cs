using System;

namespace System.Windows.Forms
{
	// Token: 0x020001C2 RID: 450
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataGridViewColumnDesignTimeVisibleAttribute : Attribute
	{
		// Token: 0x06001FA7 RID: 8103 RVA: 0x00095AF8 File Offset: 0x00093CF8
		public DataGridViewColumnDesignTimeVisibleAttribute(bool visible)
		{
			this.visible = visible;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00095B07 File Offset: 0x00093D07
		public DataGridViewColumnDesignTimeVisibleAttribute()
		{
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00095B0F File Offset: 0x00093D0F
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00095B18 File Offset: 0x00093D18
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataGridViewColumnDesignTimeVisibleAttribute dataGridViewColumnDesignTimeVisibleAttribute = obj as DataGridViewColumnDesignTimeVisibleAttribute;
			return dataGridViewColumnDesignTimeVisibleAttribute != null && dataGridViewColumnDesignTimeVisibleAttribute.Visible == this.visible;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00095B45 File Offset: 0x00093D45
		public override int GetHashCode()
		{
			return typeof(DataGridViewColumnDesignTimeVisibleAttribute).GetHashCode() ^ (this.visible ? -1 : 0);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00095B63 File Offset: 0x00093D63
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DataGridViewColumnDesignTimeVisibleAttribute.Default.Visible;
		}

		// Token: 0x04000D4F RID: 3407
		private bool visible;

		// Token: 0x04000D50 RID: 3408
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute Yes = new DataGridViewColumnDesignTimeVisibleAttribute(true);

		// Token: 0x04000D51 RID: 3409
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute No = new DataGridViewColumnDesignTimeVisibleAttribute(false);

		// Token: 0x04000D52 RID: 3410
		public static readonly DataGridViewColumnDesignTimeVisibleAttribute Default = DataGridViewColumnDesignTimeVisibleAttribute.Yes;
	}
}
