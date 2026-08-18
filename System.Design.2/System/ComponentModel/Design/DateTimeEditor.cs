using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x0200019A RID: 410
	public class DateTimeEditor : UITypeEditor
	{
		// Token: 0x06000F23 RID: 3875 RVA: 0x000575B0 File Offset: 0x000557B0
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					using (DateTimeEditor.DateTimeUI dateTimeUI = new DateTimeEditor.DateTimeUI())
					{
						dateTimeUI.Start(windowsFormsEditorService, value);
						windowsFormsEditorService.DropDownControl(dateTimeUI);
						value = dateTimeUI.Value;
						dateTimeUI.End();
					}
				}
			}
			return value;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x02000483 RID: 1155
		private class DateTimeUI : Control
		{
			// Token: 0x06002A9B RID: 10907 RVA: 0x00100368 File Offset: 0x000FE568
			public DateTimeUI()
			{
				this.InitializeComponent();
				base.Size = this.monthCalendar.SingleMonthSize;
				this.monthCalendar.Resize += this.MonthCalResize;
			}

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x06002A9C RID: 10908 RVA: 0x001003B4 File Offset: 0x000FE5B4
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x06002A9D RID: 10909 RVA: 0x001003BC File Offset: 0x000FE5BC
			public void End()
			{
				this.edSvc = null;
				this.value = null;
			}

			// Token: 0x06002A9E RID: 10910 RVA: 0x001003CC File Offset: 0x000FE5CC
			private void MonthCalKeyDown(object sender, KeyEventArgs e)
			{
				Keys keyCode = e.KeyCode;
				if (keyCode == Keys.Return)
				{
					this.OnDateSelected(sender, null);
				}
			}

			// Token: 0x06002A9F RID: 10911 RVA: 0x001003F0 File Offset: 0x000FE5F0
			private void InitializeComponent()
			{
				this.monthCalendar.DateSelected += this.OnDateSelected;
				this.monthCalendar.KeyDown += this.MonthCalKeyDown;
				base.Controls.Add(this.monthCalendar);
			}

			// Token: 0x06002AA0 RID: 10912 RVA: 0x0010043C File Offset: 0x000FE63C
			private void MonthCalResize(object sender, EventArgs e)
			{
				base.Size = this.monthCalendar.Size;
			}

			// Token: 0x06002AA1 RID: 10913 RVA: 0x0010044F File Offset: 0x000FE64F
			private void OnDateSelected(object sender, DateRangeEventArgs e)
			{
				this.value = this.monthCalendar.SelectionStart;
				this.edSvc.CloseDropDown();
			}

			// Token: 0x06002AA2 RID: 10914 RVA: 0x00100472 File Offset: 0x000FE672
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				this.monthCalendar.Focus();
			}

			// Token: 0x06002AA3 RID: 10915 RVA: 0x00100488 File Offset: 0x000FE688
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				this.edSvc = edSvc;
				this.value = value;
				if (value != null)
				{
					DateTime dateTime = (DateTime)value;
					this.monthCalendar.SetDate(dateTime.Equals(DateTime.MinValue) ? DateTime.Today : dateTime);
				}
			}

			// Token: 0x04001DD7 RID: 7639
			private MonthCalendar monthCalendar = new DateTimeEditor.DateTimeUI.DateTimeMonthCalendar();

			// Token: 0x04001DD8 RID: 7640
			private object value;

			// Token: 0x04001DD9 RID: 7641
			private IWindowsFormsEditorService edSvc;

			// Token: 0x020005CE RID: 1486
			private class DateTimeMonthCalendar : MonthCalendar
			{
				// Token: 0x06003430 RID: 13360 RVA: 0x0011C6DC File Offset: 0x0011A8DC
				protected override bool IsInputKey(Keys keyData)
				{
					return keyData == Keys.Return || base.IsInputKey(keyData);
				}
			}
		}
	}
}
