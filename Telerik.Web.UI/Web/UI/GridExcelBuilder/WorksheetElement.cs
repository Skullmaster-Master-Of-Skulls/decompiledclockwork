using System;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B30 RID: 6960
	public class WorksheetElement : ElementBase
	{
		// Token: 0x17005218 RID: 21016
		// (get) Token: 0x06010D80 RID: 68992 RVA: 0x003BC541 File Offset: 0x003BA741
		// (set) Token: 0x06010D81 RID: 68993 RVA: 0x003BC549 File Offset: 0x003BA749
		public bool IsProtected { get; set; }

		// Token: 0x17005219 RID: 21017
		// (get) Token: 0x06010D82 RID: 68994 RVA: 0x003BC554 File Offset: 0x003BA754
		public AutoFilterElement AutoFilter
		{
			get
			{
				AutoFilterElement result;
				if ((result = this._autoFilter) == null)
				{
					result = (this._autoFilter = new AutoFilterElement());
				}
				return result;
			}
		}

		// Token: 0x1700521A RID: 21018
		// (get) Token: 0x06010D83 RID: 68995 RVA: 0x003BC57C File Offset: 0x003BA77C
		// (set) Token: 0x06010D84 RID: 68996 RVA: 0x003BC5A1 File Offset: 0x003BA7A1
		public string Name
		{
			get
			{
				string result;
				if ((result = this._name) == null)
				{
					result = (this._name = string.Empty);
				}
				return result;
			}
			set
			{
				this._name = this.ClearName(value);
			}
		}

		// Token: 0x1700521B RID: 21019
		// (get) Token: 0x06010D85 RID: 68997 RVA: 0x003BC5B0 File Offset: 0x003BA7B0
		public WorksheetOptionsElement WorksheetOptions
		{
			get
			{
				WorksheetOptionsElement result;
				if ((result = this._worksheetOptions) == null)
				{
					result = (this._worksheetOptions = new WorksheetOptionsElement());
				}
				return result;
			}
		}

		// Token: 0x06010D86 RID: 68998 RVA: 0x003BC5D8 File Offset: 0x003BA7D8
		private string ClearName(string name)
		{
			if (name == null)
			{
				return string.Empty;
			}
			return name.Replace("/", string.Empty).Replace("\\", string.Empty).Replace("?", string.Empty).Replace("*", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty);
		}

		// Token: 0x06010D87 RID: 68999 RVA: 0x003BC64C File Offset: 0x003BA84C
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (string.IsNullOrEmpty(this.Name.Trim()) && !base.Attributes.Contains("ss:Name"))
			{
				throw new Exception("Name cannot be empty");
			}
			if (this.IsProtected)
			{
				base.Attributes.Add("ss:Protected", "1");
			}
			base.Attributes.Add("ss:Name", this.Name.Trim());
			base.AppendAttributes(sb);
		}

		// Token: 0x06010D88 RID: 69000 RVA: 0x003BC6C7 File Offset: 0x003BA8C7
		public WorksheetElement(string name)
		{
			this._name = this.ClearName(name);
		}

		// Token: 0x06010D89 RID: 69001 RVA: 0x003BC6DC File Offset: 0x003BA8DC
		public WorksheetElement()
		{
		}

		// Token: 0x1700521C RID: 21020
		// (get) Token: 0x06010D8A RID: 69002 RVA: 0x003BC6E4 File Offset: 0x003BA8E4
		// (set) Token: 0x06010D8B RID: 69003 RVA: 0x003BC6EC File Offset: 0x003BA8EC
		public virtual TableElement Table
		{
			get
			{
				return this._table;
			}
			set
			{
				this._table = value;
			}
		}

		// Token: 0x1700521D RID: 21021
		// (get) Token: 0x06010D8C RID: 69004 RVA: 0x003BC6F5 File Offset: 0x003BA8F5
		protected override string StartTag
		{
			get
			{
				return "<Worksheet{0}>";
			}
		}

		// Token: 0x1700521E RID: 21022
		// (get) Token: 0x06010D8D RID: 69005 RVA: 0x003BC6FC File Offset: 0x003BA8FC
		protected override string EndTag
		{
			get
			{
				return "</Worksheet>";
			}
		}

		// Token: 0x06010D8E RID: 69006 RVA: 0x003BC704 File Offset: 0x003BA904
		protected override void RenderChildElements(StringBuilder sb)
		{
			if (this._table != null)
			{
				this._table.Render(sb);
			}
			if (this._worksheetOptions != null)
			{
				this.WorksheetOptions.Render(sb);
			}
			if (!this.AutoFilter.IsEmpty)
			{
				((IElement)this.AutoFilter).Render(sb);
			}
			base.RenderChildElements(sb);
		}

		// Token: 0x04004B48 RID: 19272
		private AutoFilterElement _autoFilter;

		// Token: 0x04004B49 RID: 19273
		private TableElement _table;

		// Token: 0x04004B4A RID: 19274
		private string _name;

		// Token: 0x04004B4B RID: 19275
		private WorksheetOptionsElement _worksheetOptions;
	}
}
