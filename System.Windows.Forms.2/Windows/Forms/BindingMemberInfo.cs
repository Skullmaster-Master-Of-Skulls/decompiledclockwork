using System;

namespace System.Windows.Forms
{
	// Token: 0x02000139 RID: 313
	public struct BindingMemberInfo
	{
		// Token: 0x06000B58 RID: 2904 RVA: 0x0002036C File Offset: 0x0001E56C
		public BindingMemberInfo(string dataMember)
		{
			if (dataMember == null)
			{
				dataMember = "";
			}
			int num = dataMember.LastIndexOf(".");
			if (num != -1)
			{
				this.dataList = dataMember.Substring(0, num);
				this.dataField = dataMember.Substring(num + 1);
				return;
			}
			this.dataList = "";
			this.dataField = dataMember;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x000203C3 File Offset: 0x0001E5C3
		public string BindingPath
		{
			get
			{
				if (this.dataList == null)
				{
					return "";
				}
				return this.dataList;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000203D9 File Offset: 0x0001E5D9
		public string BindingField
		{
			get
			{
				if (this.dataField == null)
				{
					return "";
				}
				return this.dataField;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000203EF File Offset: 0x0001E5EF
		public string BindingMember
		{
			get
			{
				if (this.BindingPath.Length <= 0)
				{
					return this.BindingField;
				}
				return this.BindingPath + "." + this.BindingField;
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002041C File Offset: 0x0001E61C
		public override bool Equals(object otherObject)
		{
			if (otherObject is BindingMemberInfo)
			{
				BindingMemberInfo bindingMemberInfo = (BindingMemberInfo)otherObject;
				return string.Equals(this.BindingMember, bindingMemberInfo.BindingMember, StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002044D File Offset: 0x0001E64D
		public static bool operator ==(BindingMemberInfo a, BindingMemberInfo b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00020462 File Offset: 0x0001E662
		public static bool operator !=(BindingMemberInfo a, BindingMemberInfo b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002047A File Offset: 0x0001E67A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040006CC RID: 1740
		private string dataList;

		// Token: 0x040006CD RID: 1741
		private string dataField;
	}
}
