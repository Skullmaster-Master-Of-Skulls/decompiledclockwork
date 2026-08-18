using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010FC RID: 4348
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridCsvSettings : ObjectWithState
	{
		// Token: 0x0600B20A RID: 45578 RVA: 0x0026AD62 File Offset: 0x00268F62
		public GridCsvSettings(StateBag OwnerStateBag) : base("gcsvs_", OwnerStateBag)
		{
		}

		// Token: 0x170039AC RID: 14764
		// (get) Token: 0x0600B20B RID: 45579 RVA: 0x0026AD70 File Offset: 0x00268F70
		// (set) Token: 0x0600B20C RID: 45580 RVA: 0x0026AD9F File Offset: 0x00268F9F
		[Description("Gets or sets the file extension for RadGrid CSV export.")]
		[NotifyParentProperty(true)]
		[DefaultValue("csv")]
		public string FileExtension
		{
			get
			{
				if (base.ViewState["_fe"] == null)
				{
					return "csv";
				}
				return (string)base.ViewState["_fe"];
			}
			set
			{
				base.ViewState["_fe"] = value;
			}
		}

		// Token: 0x170039AD RID: 14765
		// (get) Token: 0x0600B20D RID: 45581 RVA: 0x0026ADB2 File Offset: 0x00268FB2
		// (set) Token: 0x0600B20E RID: 45582 RVA: 0x0026ADDD File Offset: 0x00268FDD
		[Description("Gets or sets the row delimiter for RadGrid CSV export.")]
		[DefaultValue(GridCsvDelimiter.NewLine)]
		[NotifyParentProperty(true)]
		public GridCsvDelimiter RowDelimiter
		{
			get
			{
				if (base.ViewState["_rd"] == null)
				{
					return GridCsvDelimiter.NewLine;
				}
				return (GridCsvDelimiter)base.ViewState["_rd"];
			}
			set
			{
				base.ViewState["_rd"] = value;
			}
		}

		// Token: 0x170039AE RID: 14766
		// (get) Token: 0x0600B20F RID: 45583 RVA: 0x0026ADF5 File Offset: 0x00268FF5
		// (set) Token: 0x0600B210 RID: 45584 RVA: 0x0026AE20 File Offset: 0x00269020
		[DefaultValue(GridCsvEncoding.Utf8)]
		public GridCsvEncoding Encoding
		{
			get
			{
				if (base.ViewState["_csvEncoding"] == null)
				{
					return GridCsvEncoding.Utf8;
				}
				return (GridCsvEncoding)base.ViewState["_csvEncoding"];
			}
			set
			{
				base.ViewState["_csvEncoding"] = value;
			}
		}

		// Token: 0x170039AF RID: 14767
		// (get) Token: 0x0600B211 RID: 45585 RVA: 0x0026AE38 File Offset: 0x00269038
		// (set) Token: 0x0600B212 RID: 45586 RVA: 0x0026AE61 File Offset: 0x00269061
		[Description("Determines whether the CSV file will have a BOM header.")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool EnableBomHeader
		{
			get
			{
				object obj = base.ViewState["EnableBomHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["EnableBomHeader"] = value;
			}
		}

		// Token: 0x170039B0 RID: 14768
		// (get) Token: 0x0600B213 RID: 45587 RVA: 0x0026AE79 File Offset: 0x00269079
		// (set) Token: 0x0600B214 RID: 45588 RVA: 0x0026AEA4 File Offset: 0x002690A4
		[Description("Gets or sets the row delimiter for RadGrid CSV export.")]
		[NotifyParentProperty(true)]
		[DefaultValue(GridCsvDelimiter.Comma)]
		public GridCsvDelimiter ColumnDelimiter
		{
			get
			{
				if (base.ViewState["_cd"] == null)
				{
					return GridCsvDelimiter.Comma;
				}
				return (GridCsvDelimiter)base.ViewState["_cd"];
			}
			set
			{
				base.ViewState["_cd"] = value;
			}
		}

		// Token: 0x170039B1 RID: 14769
		// (get) Token: 0x0600B215 RID: 45589 RVA: 0x0026AEBC File Offset: 0x002690BC
		// (set) Token: 0x0600B216 RID: 45590 RVA: 0x0026AEE5 File Offset: 0x002690E5
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Gets or sets whether the data will be enclosed with quotes for RadGrid CSV export.")]
		public bool EncloseDataWithQuotes
		{
			get
			{
				object obj = base.ViewState["EncloseDataWithQuotes"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["EncloseDataWithQuotes"] = value;
			}
		}

		// Token: 0x0600B217 RID: 45591 RVA: 0x0026AF00 File Offset: 0x00269100
		internal static string DelimiterAsString(GridCsvDelimiter delimiter)
		{
			string result = "";
			switch (delimiter)
			{
			case GridCsvDelimiter.NewLine:
				result = Environment.NewLine;
				break;
			case GridCsvDelimiter.Semicolon:
				result = ";";
				break;
			case GridCsvDelimiter.Colon:
				result = ":";
				break;
			case GridCsvDelimiter.Comma:
				result = ",";
				break;
			case GridCsvDelimiter.Tab:
				result = "\t";
				break;
			case GridCsvDelimiter.VerticalBar:
				result = "|";
				break;
			}
			return result;
		}
	}
}
