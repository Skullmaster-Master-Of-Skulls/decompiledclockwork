using System;

namespace System.ComponentModel
{
	// Token: 0x020005B0 RID: 1456
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		// Token: 0x06003639 RID: 13881 RVA: 0x000ECA67 File Offset: 0x000EAC67
		public ToolboxItemFilterAttribute(string filterString) : this(filterString, ToolboxItemFilterType.Allow)
		{
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000ECA71 File Offset: 0x000EAC71
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			if (filterString == null)
			{
				filterString = string.Empty;
			}
			this.filterString = filterString;
			this.filterType = filterType;
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600363B RID: 13883 RVA: 0x000ECA91 File Offset: 0x000EAC91
		public string FilterString
		{
			get
			{
				return this.filterString;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600363C RID: 13884 RVA: 0x000ECA99 File Offset: 0x000EAC99
		public ToolboxItemFilterType FilterType
		{
			get
			{
				return this.filterType;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x000ECAA1 File Offset: 0x000EACA1
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					this.typeId = base.GetType().FullName + this.filterString;
				}
				return this.typeId;
			}
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000ECAD0 File Offset: 0x000EACD0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterType.Equals(this.FilterType) && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000ECB21 File Offset: 0x000EAD21
		public override int GetHashCode()
		{
			return this.filterString.GetHashCode();
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000ECB30 File Offset: 0x000EAD30
		public override bool Match(object obj)
		{
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000ECB5F File Offset: 0x000EAD5F
		public override string ToString()
		{
			return this.filterString + "," + Enum.GetName(typeof(ToolboxItemFilterType), this.filterType);
		}

		// Token: 0x04002A9E RID: 10910
		private ToolboxItemFilterType filterType;

		// Token: 0x04002A9F RID: 10911
		private string filterString;

		// Token: 0x04002AA0 RID: 10912
		private string typeId;
	}
}
