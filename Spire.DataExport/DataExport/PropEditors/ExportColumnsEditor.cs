using System;
using System.ComponentModel;
using System.Drawing.Design;
using Spire.DataExport.Collections;
using Spire.DataExport.Common;
using Spire.DataExport.Forms;
using Spire.DataExport.XLS;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200021A RID: 538
	public class ExportColumnsEditor : UITypeEditor
	{
		// Token: 0x06001005 RID: 4101 RVA: 0x000AD29C File Offset: 0x000AC29C
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_33;
					default:
						if (false)
						{
						}
						if (context.Instance != null)
						{
							num = 1;
							continue;
						}
						goto IL_77;
					}
					break;
				case 1:
					return UITypeEditorEditStyle.Modal;
				case 3:
					goto IL_33;
				}
				if (true)
				{
				}
				if (context != null)
				{
					num = 3;
					continue;
				}
				goto IL_77;
				IL_33:
				num = 0;
			}
			return UITypeEditorEditStyle.Modal;
			IL_77:
			return base.GetEditStyle(context);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000AD328 File Offset: 0x000AC328
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EE;
					default:
						if (false)
						{
						}
						if (context.Instance is ExportBase)
						{
							num = 5;
							continue;
						}
						num = 11;
						continue;
					}
					break;
				case 1:
					num = 6;
					continue;
				case 2:
					if (value == (context.Instance as WorkSheet).Columns)
					{
						num = 1;
						continue;
					}
					return value;
				case 3:
					goto IL_159;
				case 4:
					if (DataExportColumnsEditor.RunDataExportColumnsEditor((context.Instance as ExportBase).DataSource, (context.Instance as ExportBase).SQLCommand, (context.Instance as ExportBase).DataTable, (context.Instance as ExportBase).ListView, value as StringListCollection))
					{
						num = 13;
						continue;
					}
					return value;
				case 5:
					goto IL_EE;
				case 6:
					if (DataExportColumnsEditor.RunDataExportColumnsEditor((context.Instance as WorkSheet).DataSource, (context.Instance as WorkSheet).SQLCommand, (context.Instance as WorkSheet).DataTable, (context.Instance as WorkSheet).ListView, value as StringListCollection))
					{
						num = 3;
						continue;
					}
					return value;
				case 7:
					if (context.Instance != null)
					{
						num = 8;
						continue;
					}
					return value;
				case 8:
					num = 0;
					continue;
				case 10:
					num = 7;
					continue;
				case 11:
					if (context.Instance is WorkSheet)
					{
						num = 15;
						continue;
					}
					return value;
				case 12:
					if (value == (context.Instance as ExportBase).Columns)
					{
						num = 14;
						continue;
					}
					return value;
				case 13:
					goto IL_21E;
				case 14:
					num = 4;
					continue;
				case 15:
					num = 2;
					continue;
				}
				if (context != null)
				{
					num = 10;
					continue;
				}
				return value;
				IL_EE:
				num = 12;
			}
			IL_159:
			return (value as StringListCollection).Clone();
			IL_21E:
			return (value as StringListCollection).Clone();
		}
	}
}
