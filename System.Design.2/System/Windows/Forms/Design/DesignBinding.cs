using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002CB RID: 715
	[Editor("System.Windows.Forms.Design.DesignBindingEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	internal class DesignBinding
	{
		// Token: 0x06001C44 RID: 7236 RVA: 0x000AA42F File Offset: 0x000A862F
		public DesignBinding(object dataSource, string dataMember)
		{
			this.dataSource = dataSource;
			this.dataMember = dataMember;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x000AA445 File Offset: 0x000A8645
		public bool IsNull
		{
			get
			{
				return this.dataSource == null;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x000AA450 File Offset: 0x000A8650
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x000AA458 File Offset: 0x000A8658
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x000AA460 File Offset: 0x000A8660
		public string DataField
		{
			get
			{
				if (string.IsNullOrEmpty(this.dataMember))
				{
					return string.Empty;
				}
				int num = this.dataMember.LastIndexOf(".");
				if (num == -1)
				{
					return this.dataMember;
				}
				return this.dataMember.Substring(num + 1);
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000AA4AA File Offset: 0x000A86AA
		public bool Equals(object dataSource, string dataMember)
		{
			return dataSource == this.dataSource && string.Equals(dataMember, this.dataMember, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040016D0 RID: 5840
		private object dataSource;

		// Token: 0x040016D1 RID: 5841
		private string dataMember;

		// Token: 0x040016D2 RID: 5842
		public static DesignBinding Null = new DesignBinding(null, null);
	}
}
