using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.XLS;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x0200020F RID: 527
	public class CellColorEditor : ListComponentEditor
	{
		// Token: 0x06000FE5 RID: 4069 RVA: 0x000AB428 File Offset: 0x000AA428
		public override void AdditionalSettings()
		{
			this.m_listBox.DrawMode = DrawMode.OwnerDrawFixed;
			this.m_listBox.DrawItem += this.ᜀ;
			this.m_listBox.Items.Clear();
			IEnumerator enumerator = Enum.GetValues(typeof(CellColor)).GetEnumerator();
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_A5;
					case 3:
						goto IL_C7;
					case 4:
					{
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						CellColor a_ = (CellColor)enumerator.Current;
						this.m_listBox.Items.Add(new CellColorEditor.ᜀ(a_));
						num = 2;
						continue;
					}
					}
					goto IL_6E;
					IL_A5:
					num = 4;
					continue;
					IL_6E:
					if (true)
					{
					}
					goto IL_A5;
				}
				IL_C7:;
			}
			finally
			{
				for (;;)
				{
					IL_DD:
					IDisposable disposable = enumerator as IDisposable;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_11B:
						num = 2;
						break;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_125;
						case 1:
							goto IL_113;
						case 2:
							goto IL_123;
						}
						goto IL_DD;
					}
					IL_113:
					disposable.Dispose();
					goto IL_11B;
				}
				IL_123:
				IL_125:;
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000AB578 File Offset: 0x000AA578
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_52;
				case 1:
					if (this.m_edSvc != null)
					{
						num = 10;
						continue;
					}
					return value;
				case 2:
					num = 12;
					continue;
				case 3:
					if (true)
					{
					}
					this.m_edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
					num = 1;
					continue;
				case 4:
					value = (this.m_listBox.SelectedItem as CellColorEditor.ᜀ).ᜂ();
					num = 7;
					continue;
				case 5:
					if (provider != null)
					{
						num = 3;
						continue;
					}
					return value;
				case 7:
					return value;
				case 8:
					if (this.m_listBox.SelectedIndex >= 0)
					{
						num = 2;
						continue;
					}
					return value;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						if (context.Instance != null)
						{
							num = 11;
							continue;
						}
						return value;
					}
					break;
				case 10:
					this.m_edSvc.DropDownControl(this.m_listBox);
					num = 8;
					continue;
				case 11:
					num = 5;
					continue;
				case 12:
					if (this.m_listBox.SelectedItem is CellColorEditor.ᜀ)
					{
						num = 4;
						continue;
					}
					return value;
				}
				if (context != null)
				{
					num = 0;
					continue;
				}
				break;
				IL_52:
				num = 9;
			}
			return value;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000AB718 File Offset: 0x000AA718
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
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
			return true;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000AB754 File Offset: 0x000AA754
		public override void PaintValue(PaintValueEventArgs e)
		{
			if (e.Value.GetType() == typeof(CellColor))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					if (true)
					{
					}
					Color color = CellColorEditor.ᜀ.ᜀ((CellColor)e.Value);
					Rectangle rect = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width - 1, e.Bounds.Height - 1);
					e.Graphics.DrawRectangle(new Pen(color), rect);
					e.Graphics.FillRectangle(new SolidBrush(color), rect);
					return;
				}
				}
			}
			base.PaintValue(e);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000AB830 File Offset: 0x000AA830
		private void ᜀ(object A_0, DrawItemEventArgs A_1)
		{
			switch (0)
			{
			default:
			{
				Brush brush;
				CellColorEditor.ᜀ ᜀ;
				Color color;
				Rectangle rect;
				for (;;)
				{
					A_1.DrawBackground();
					A_1.DrawFocusRectangle();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (A_1.Index < 0)
							{
								num = 5;
								continue;
							}
							num = 2;
							continue;
						case 1:
							if (true)
							{
							}
							brush = Brushes.White;
							num = 3;
							continue;
						case 2:
							if (!((A_0 as ListBox).Items[A_1.Index] is CellColorEditor.ᜀ))
							{
								num = 6;
								continue;
							}
							ᜀ = ((A_0 as ListBox).Items[A_1.Index] as CellColorEditor.ᜀ);
							color = ᜀ.ᜀ();
							rect = new Rectangle(2, A_1.Bounds.Top + 2, A_1.Bounds.Height, A_1.Bounds.Height - 4);
							brush = null;
							num = 4;
							continue;
						case 3:
							goto IL_77;
						case 4:
							if ((A_1.State & DrawItemState.Selected) == DrawItemState.Selected)
							{
								num = 1;
								continue;
							}
							goto IL_9A;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9A;
							default:
								goto IL_93;
							}
							break;
						case 6:
							return;
						case 7:
							goto IL_AC;
						}
						break;
						IL_9A:
						brush = Brushes.Black;
						num = 7;
					}
				}
				IL_77:
				goto IL_17B;
				IL_93:
				if (false)
				{
				}
				return;
				IL_AC:
				IL_17B:
				A_1.Graphics.DrawRectangle(new Pen(color), rect);
				A_1.Graphics.FillRectangle(new SolidBrush(color), rect);
				rect.Inflate(1, 1);
				A_1.Graphics.DrawRectangle(Pens.Black, rect);
				A_1.Graphics.DrawString(ᜀ.ᜁ(), (A_0 as ListBox).Font, brush, (float)(A_1.Bounds.Height + 5), (float)((A_1.Bounds.Height - (A_0 as ListBox).Font.Height) % 2 + A_1.Bounds.Top));
				return;
			}
			}
		}

		// Token: 0x02000210 RID: 528
		private class ᜀ
		{
			// Token: 0x06000FEB RID: 4075 RVA: 0x000ABA6C File Offset: 0x000AAA6C
			public ᜀ(CellColor A_0)
			{
				this.ᜀ = A_0;
			}

			// Token: 0x06000FEC RID: 4076 RVA: 0x000ABA88 File Offset: 0x000AAA88
			public static Color ᜀ(CellColor A_0)
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
				uint value = spr\u2009.᠑[(int)A_0];
				byte[] bytes = BitConverter.GetBytes(value);
				return Color.FromArgb((int)bytes[0], (int)bytes[1], (int)bytes[2]);
			}

			// Token: 0x06000FED RID: 4077 RVA: 0x000ABAE0 File Offset: 0x000AAAE0
			public CellColor ᜂ()
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
				return this.ᜀ;
			}

			// Token: 0x06000FEE RID: 4078 RVA: 0x000ABB24 File Offset: 0x000AAB24
			public Color ᜀ()
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
				return CellColorEditor.ᜀ.ᜀ(this.ᜀ);
			}

			// Token: 0x06000FEF RID: 4079 RVA: 0x000ABB6C File Offset: 0x000AAB6C
			public string ᜁ()
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
				return this.ᜀ.ToString();
			}

			// Token: 0x04000B9E RID: 2974
			private CellColor ᜀ;
		}
	}
}
