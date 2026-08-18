using System;
using Spire.DataExport.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001E6 RID: 486
	public class CellNotes : Collection
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x000A3008 File Offset: 0x000A2008
		public CellNotes(object Holder)
		{
			this.m_holder = Holder;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000A3024 File Offset: 0x000A2024
		public CellNote Add(CellNote Item)
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

		// Token: 0x170001F0 RID: 496
		public CellNote this[int Index]
		{
			get
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
				return base[Index] as CellNote;
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
