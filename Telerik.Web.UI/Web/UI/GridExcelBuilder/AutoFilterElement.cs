using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B0E RID: 6926
	public class AutoFilterElement : ElementBase
	{
		// Token: 0x17005186 RID: 20870
		// (get) Token: 0x06010BFD RID: 68605 RVA: 0x003B8F9F File Offset: 0x003B719F
		// (set) Token: 0x06010BFE RID: 68606 RVA: 0x003B8FBA File Offset: 0x003B71BA
		public string Range
		{
			get
			{
				if (this._range == null)
				{
					this._range = string.Empty;
				}
				return this._range;
			}
			set
			{
				this._range = value;
			}
		}

		// Token: 0x17005187 RID: 20871
		// (get) Token: 0x06010BFF RID: 68607 RVA: 0x003B8FC3 File Offset: 0x003B71C3
		protected override string StartTag
		{
			get
			{
				return "<AutoFilter xmlns=\"urn:schemas-microsoft-com:office:excel\"{0}>";
			}
		}

		// Token: 0x17005188 RID: 20872
		// (get) Token: 0x06010C00 RID: 68608 RVA: 0x003B8FCA File Offset: 0x003B71CA
		protected override string EndTag
		{
			get
			{
				return "</AutoFilter>";
			}
		}

		// Token: 0x17005189 RID: 20873
		// (get) Token: 0x06010C01 RID: 68609 RVA: 0x003B8FD1 File Offset: 0x003B71D1
		public virtual bool IsEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.Range.Trim()) && !base.Attributes.Contains("x:Range");
			}
		}

		// Token: 0x06010C02 RID: 68610 RVA: 0x003B8FFC File Offset: 0x003B71FC
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.IsEmpty)
			{
				throw new Exception("Range cannot be empty");
			}
			if (!string.IsNullOrEmpty(this.Range.Trim()))
			{
				base.Attributes.Add("x:Range", this.Range.Trim());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04004AC4 RID: 19140
		private string _range;
	}
}
