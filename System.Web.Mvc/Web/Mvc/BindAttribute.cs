using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x0200017A RID: 378
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class BindAttribute : Attribute
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001BE9F File Offset: 0x0001A09F
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		public string Exclude
		{
			get
			{
				return this._exclude ?? string.Empty;
			}
			set
			{
				this._exclude = value;
				this._excludeSplit = AuthorizeAttribute.SplitString(value);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001BEC5 File Offset: 0x0001A0C5
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0001BED6 File Offset: 0x0001A0D6
		public string Include
		{
			get
			{
				return this._include ?? string.Empty;
			}
			set
			{
				this._include = value;
				this._includeSplit = AuthorizeAttribute.SplitString(value);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0001BEEB File Offset: 0x0001A0EB
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0001BEF3 File Offset: 0x0001A0F3
		public string Prefix { get; set; }

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001BEFC File Offset: 0x0001A0FC
		internal static bool IsPropertyAllowed(string propertyName, ICollection<string> includeProperties, ICollection<string> excludeProperties)
		{
			bool flag = includeProperties == null || includeProperties.Count == 0 || includeProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
			bool flag2 = excludeProperties != null && excludeProperties.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
			return flag && !flag2;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001BF40 File Offset: 0x0001A140
		public bool IsPropertyAllowed(string propertyName)
		{
			return BindAttribute.IsPropertyAllowed(propertyName, this._includeSplit, this._excludeSplit);
		}

		// Token: 0x040002B6 RID: 694
		private string _exclude;

		// Token: 0x040002B7 RID: 695
		private string[] _excludeSplit = new string[0];

		// Token: 0x040002B8 RID: 696
		private string _include;

		// Token: 0x040002B9 RID: 697
		private string[] _includeSplit = new string[0];
	}
}
