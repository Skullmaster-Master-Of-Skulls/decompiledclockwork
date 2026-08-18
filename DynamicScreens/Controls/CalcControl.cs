using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DynamicScreens.Controls
{
	// Token: 0x02000047 RID: 71
	public class CalcControl
	{
		// Token: 0x060003F9 RID: 1017 RVA: 0x00034F42 File Offset: 0x00033F42
		public CalcControl(int cid, Control ctrl)
		{
			this.cid = cid;
			this.ctrl = ctrl;
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00034F5C File Offset: 0x00033F5C
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00034F74 File Offset: 0x00033F74
		public int Cid
		{
			get
			{
				return this.cid;
			}
			set
			{
				this.cid = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00034F80 File Offset: 0x00033F80
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00034F98 File Offset: 0x00033F98
		public Control Ctrl
		{
			get
			{
				return this.ctrl;
			}
			set
			{
				this.ctrl = value;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00034FA4 File Offset: 0x00033FA4
		public bool Equals(CalcControl cc)
		{
			return cc.Cid == this.cid && cc.Ctrl == this.ctrl;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00034FD8 File Offset: 0x00033FD8
		public static bool Exists(List<CalcControl> ctrls, CalcControl ctrl)
		{
			foreach (CalcControl calcControl in ctrls)
			{
				if (calcControl.Equals(ctrl))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040002CD RID: 717
		private int cid;

		// Token: 0x040002CE RID: 718
		private Control ctrl;
	}
}
