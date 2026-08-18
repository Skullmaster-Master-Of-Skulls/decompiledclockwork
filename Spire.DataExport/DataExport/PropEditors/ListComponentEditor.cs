using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.PropEditors
{
	// Token: 0x02000032 RID: 50
	public abstract class ListComponentEditor : UITypeEditor
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000ED2C File Offset: 0x0000DD2C
		public ListComponentEditor()
		{
			this.m_listBox = new ListBox();
			this.m_listBox.BorderStyle = BorderStyle.None;
			this.m_listBox.SelectedIndexChanged += this.lb_SelectedIndexChanged;
			this.AdditionalSettings();
		}

		// Token: 0x0600019A RID: 410
		public abstract void AdditionalSettings();

		// Token: 0x0600019B RID: 411 RVA: 0x0000ED74 File Offset: 0x0000DD74
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (context.Instance != null)
					{
						num = 3;
						continue;
					}
					goto IL_5B;
				case 2:
					IL_08:
					break;
				case 3:
					return UITypeEditorEditStyle.DropDown;
				}
				if (true)
				{
				}
				if (context != null)
				{
					num = 0;
					continue;
				}
				IL_5B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				default:
					goto IL_71;
				}
			}
			return UITypeEditorEditStyle.DropDown;
			IL_71:
			if (false)
			{
			}
			return base.GetEditStyle(context);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000EE00 File Offset: 0x0000DE00
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.m_edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
					num = 5;
					continue;
				case 1:
					if (provider != null)
					{
						num = 0;
						continue;
					}
					return value;
				case 2:
					if (this.m_listBox.SelectedIndex >= 0)
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					return value;
				case 3:
					if (context.Instance != null)
					{
						num = 4;
						continue;
					}
					return value;
				case 4:
					num = 1;
					continue;
				case 5:
					if (this.m_edSvc != null)
					{
						num = 7;
						continue;
					}
					return value;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						goto IL_119;
					}
					break;
				case 7:
					this.m_edSvc.DropDownControl(this.m_listBox);
					num = 2;
					continue;
				case 9:
					goto IL_86;
				case 10:
					num = 3;
					continue;
				}
				if (context != null)
				{
					num = 10;
					continue;
				}
				return value;
				IL_86:
				value = this.m_listBox.SelectedItem.ToString();
				num = 6;
			}
			IL_119:
			if (false)
			{
			}
			return value;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000EF58 File Offset: 0x0000DF58
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
			return false;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000EF94 File Offset: 0x0000DF94
		public void lb_SelectedIndexChanged(object sender, EventArgs e)
		{
			int a_ = 5;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_73;
				case 1:
					if (this.m_edSvc != null)
					{
						num = 3;
						continue;
					}
					return;
				case 2:
					goto IL_42;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_97;
					default:
						if (false)
						{
						}
						this.m_edSvc.CloseDropDown();
						num = 0;
						continue;
					}
					break;
				}
				if (e == null)
				{
					num = 2;
				}
				else
				{
					num = 1;
				}
			}
			IL_42:
			goto IL_97;
			IL_73:
			return;
			IL_97:
			throw new ArgumentNullException(HyperlinksCollectionEditor.b("Ⱐ⤢椤並娨弪漬䀮䤰焲吴䐶尸强砼嬾⡀㝂⩄㕆獈煊⅌ⵎ๐Rご㭖㱘㡚⥜㩞ՠ⩢୤ͦ౨፪⹬ݮၰᵲቴቶᵸ坺୼Ṿ릂", a_));
		}

		// Token: 0x04000085 RID: 133
		private int \u2460\u008C\u0080\u0080;

		// Token: 0x04000086 RID: 134
		protected ListBox m_listBox;

		// Token: 0x04000087 RID: 135
		protected IWindowsFormsEditorService m_edSvc;
	}
}
