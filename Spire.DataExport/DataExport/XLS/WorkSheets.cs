using System;
using Spire.DataExport.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001C3 RID: 451
	public class WorkSheets : Collection
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x000945E0 File Offset: 0x000935E0
		public WorkSheets(CellExport ExportCELLExport)
		{
			this.m_holder = ExportCELLExport;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000945FC File Offset: 0x000935FC
		public WorkSheet Add(WorkSheet Item)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.Add(Item);
			return Item;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00094640 File Offset: 0x00093640
		public int IndexOf(WorkSheet Item)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return base.InnerList.IndexOf(Item);
		}

		// Token: 0x17000171 RID: 369
		public WorkSheet this[int Index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return base[Index] as WorkSheet;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				base[Index] = value;
			}
		}
	}
}
